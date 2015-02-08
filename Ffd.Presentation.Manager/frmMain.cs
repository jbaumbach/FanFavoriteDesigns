using Ffd.App.Core;
using Ffd.Common;
using Ffd.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Collections;
using System.Diagnostics;

namespace Ffd.Presentation.Manager
{
    public partial class frmMain : Form
    {
        // private int _playerLoop = 0;
        // private List<Player> _playerList = null;

        // private const string CUT_OUTFILE = "cut-outfile.bmp";
        // private const string MKT_OUTFILE = "marketing-outfile.gif";

        private bool _playerCstExist = false;

        private List<Order> _ordersToProcess = null;
        private OrderDisplayable _currentOrderDisplaying = null;
        private string _orderFileName = string.Empty;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            tcMainAppArea.HideTabs = true;

            // _playerList = ApplicationManager.GetPlayers();
            // ProcessGraphics(_playerList[0]);

            cmbTemplate.DataSource = DataManager.GetTemplates();    // .Items.Add(new Template(1, "Hockey"));
            txtEbayPaste.Text = string.Empty;
            txtCutFileName.Text = string.Empty;

            //
            // The tag indicates whether that tab page has been seen before and requires a load or not.
            // The pages have to go to the database, so might as well load them on demand only to reduce
            // initial application load times.
            //
            foreach (TabPage tab in tcMainAppArea.TabPages)
            {
                tab.Tag = 0;
            }

            RefreshUI();

            OrderInGridSelected(-1);

            tcMainAppArea.Focus();
            tpProducts.Focus();

            toolStripStatusLabel.Text = string.Format("Build date: {0}", Workstation.CurrentRunningEXEFileDate);

            Console.WriteLine(Config.GetAppSetting("GraphicsRootDirectory"));
        }

        //***********************************************************************
        // Tab initializers 
        //***********************************************************************

        private void tpDatabase_Enter(object sender, EventArgs e)
        {
            //
            // Tag = 0, not initialized, tag = 1 initialized.
            //
            if (tpDatabase.Tag.ToString() == "0")
            {
                tsTabDBTeamSelector.LoadData();

                Functions.SetItemsIntoDropdownWithExtraDefaultValue(tpDB_cmbLeadSource, DataManager.GetLeadSourceCodes());
                Functions.SetItemsIntoDropdownWithExtraDefaultValue(tpDB_cmbLeadStatus, DataManager.GetLeadStatusCodes());

                tpDB_txtMinEnrollment.Text = "250";
                tpDB_lblSupplyQty.Text = "(select params)";

                tpDB_txtQtyToGet.Text = "2500";
                tpDB_txtOutputFName.Text = Functions.BuildFilenameFromElements(Config.LeadCSVOutputDirectory, string.Format("ffd-leads-{0:yyyy-MM-dd}.csv", DateTime.Now));
                tpDB_txtSkipEvery.Text = "";
                tpDB_lblStatus.Text = "";

                // For debugging only - remove
                tpDB_lstBadAddresses.Items.Add(DataManager.GetLeadByCustomerId(34875));

                tpDatabase.Tag = "1";
            }
        }

        private void tpSales_Enter(object sender, EventArgs e)
        {
            if (tpSales.Tag.ToString() == "0")
            {
                tsTabSalesTeamSelector.LoadData();
                tpSales.Tag = "1";
            }

            tpSales_lblEbayGraphicsStatus.Text = "";
            tpSales_lblEbayToolsStatus.Text = "";

            tsTabSalesTeamSelector.Focus();
        }

        private void tpProducts_Enter(object sender, EventArgs e)
        {
            //
            // Tag = 0, not initialized, tag = 1 initialized.
            //
            if (tpProducts.Tag.ToString() == "0")
            {
                tpProductsTeamSelector.LoadData();
                tpProducts.Tag = "1";
            }


            // 
            // When tab becomes active - set focus to this control
            //
            txtFullName.Focus();
        }

        //***********************************************************************
        // End Tab initializers 
        //***********************************************************************



        private void lvMainMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvMainMenu.SelectedItems.Count > 0)
            {
                int selectedIndex = lvMainMenu.SelectedItems[0].Index;
                tcMainAppArea.SelectTab(selectedIndex);
            }
        }

        private ApplicationManager.StickerImageType SelectedImageType()
        {
            ApplicationManager.StickerImageType result = ApplicationManager.StickerImageType.Cutting;

            if (rbMarketing.Checked)
            {
                result = ApplicationManager.StickerImageType.Marketing;
            }

            return result;
        }

        /// <summary>
        /// Process graphics from main tab page.
        /// </summary>
        /// <param name="playerSeason"></param>
        /// <param name="template"></param>
        private void ProcessGraphics(PlayerSeason playerSeason, Template template)
        {
            ApplicationManager.StickerImageType type = SelectedImageType();
            ProcessGraphics(playerSeason, template, pbJerseyImage, type, chkBoundingBox.Checked);
        }

        /// <summary>
        /// Get the type of image that's currently stored in the picturebox.  If nothing, returns unknown.
        /// </summary>
        /// <remarks>
        /// Huh huh, you said "box".
        /// </remarks>
        /// <param name="box">The picturebox to check.</param>
        /// <returns>The enum value.</returns>
        private ApplicationManager.StickerImageType PictureBoxStickerType(PictureBox box)
        {
            if (box.Tag != null)
            {
                foreach (ApplicationManager.StickerImageType type in Enum.GetValues(typeof(ApplicationManager.StickerImageType)))
                {
                    if (box.Tag.ToString() == type.ToString())
                    {
                        return type;
                    }
                }
            }

            return ApplicationManager.StickerImageType.Unknown;
        }

        /// <summary>
        /// Process graphics from any other page.
        /// </summary>
        /// <param name="playerSeason"></param>
        /// <param name="template"></param>
        /// <param name="pbTargetBox"></param>
        /// <param name="type"></param>
        /// <param name="drawBoundingBoxes"></param>
        private void ProcessGraphics(PlayerSeason playerSeason, Template template, PictureBox pbTargetBox, ApplicationManager.StickerImageType type, bool drawBoundingBoxes)
        {
            ProcessGraphics(playerSeason, template, pbTargetBox, type, drawBoundingBoxes, null);
        }

        /// <summary>
        /// Process graphics from any other page.
        /// </summary>
        /// <param name="playerSeason"></param>
        /// <param name="template"></param>
        /// <param name="pbTargetBox"></param>
        /// <param name="type"></param>
        /// <param name="drawBoundingBoxes"></param>
        private void ProcessGraphics(PlayerSeason playerSeason, Template template, PictureBox pbTargetBox, ApplicationManager.StickerImageType type, bool drawBoundingBoxes, Material material)
        {
            using (CursorManager cm = new CursorManager(this, Cursors.WaitCursor))
            {
                ProductItemJersey item = new ProductItemJersey(playerSeason, template);
                item.Material = material;

                pbTargetBox.BackColor = (type == ApplicationManager.StickerImageType.Cutting ? Color.White : Color.Black);
                pbTargetBox.Image = ApplicationManager.GetImageFromProductItem(item, type, drawBoundingBoxes);
                pbTargetBox.Tag = type;
            }
        }

        private bool ValidatePlayerSeasonForm()
        {
            bool result = ((Functions.IsNumeric(txtNumber.Text) || txtFullName.Text != string.Empty) && GetTemplateFromUI() != null);

            //if (!result)
            //{
            //    if (MessageBox.Show("Form fields not all filled in - ok to continue?", "Validate Form", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        result = true;
            //    }
            //}

            return result;
        }

        private bool ValidateUIForCutting()
        {
            //if (pbJerseyImage.Image == null)
            //{
            //    RefreshImage();
            //}
            return (rbCutting.Checked && !chkBoundingBox.Checked && (pbJerseyImage.Image != null || RefreshImage()));
        }

        private PlayerSeason GetPlayerSeasonFromUI()
        {
            return new PlayerSeason(txtFullName.Text, txtNumber.Text);
        }

        private Template GetTemplateFromUI()
        {
            return (Template)cmbTemplate.SelectedItem;
        }

        /// <summary>
        /// Refreshes the image box if the player season form is valid.
        /// </summary>
        /// <returns>Whether the player season form is valid or not.</returns>
        private bool RefreshImage()
        {
            bool result = ValidatePlayerSeasonForm();

            if (result)
            {
                PlayerSeason playerSeason = GetPlayerSeasonFromUI();
                Template template = GetTemplateFromUI();

                ProcessGraphics(playerSeason, template);
            }

            return result;
        }

        /// <summary>
        /// Respond to "Refresh >>" button click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTryAgain_Click(object sender, EventArgs e)
        {
            //
            // Show button
            //
            // Player player = new Player(txtFullName.Text, txtNumber.Text);

            if (!RefreshImage())
            {
                MessageBox.Show("Not enough info to display the form!", "Enter More", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }

            RefreshUI();
        }

        private void RefreshUI()
        {
            _playerCstExist = false;

            if (ValidatePlayerSeasonForm())
            {
                _playerCstExist = ApplicationManager.SourceCutFileExist(GetPlayerSeasonFromUI(), GetTemplateFromUI());

                PlayerSeason playerSeason = GetPlayerSeasonFromUI();
                Template template = GetTemplateFromUI();

                txtCutFileName.Text = ApplicationManager.BuildPlayerSourceCutfileName(playerSeason, template);
                lblCSTExists.Text = _playerCstExist ? "Yes" : "No";
            }
            else
            {
                lblCSTExists.Text = "n/a";
            }

            // btnCut.Enabled = _playerCstExist;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            // _playerLoop = (++_playerLoop) % _playerList.Count;
            // ProcessGraphics(_playerList[_playerLoop]);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check dirty...

            if (true)
            {
                Application.Exit();
            }
        }

        private void GoCutting(PlayerSeason playerSeason, Template template, Image nameNumberImage)
        {
            const int waitTimeBetweenCommands = 750;

            string nameNumberBmpFilename = ApplicationManager.BuildPlayerImportableNameNumberFilename(playerSeason, template);
            bool res = ApplicationManager.SaveImageToFile(nameNumberImage, nameNumberBmpFilename, ImageFormat.Bmp, 24L);

            string desiredTemplateCst = ApplicationManager.BuildTemplateCutfileName(template);

            string playerCutFileName = ApplicationManager.BuildPlayerSourceCutfileName(playerSeason, template);
            txtCutFileName.Text = playerCutFileName;

            if (!ApplicationManager.SourceCutFileExist(playerSeason, template))
            {

                if (!Workstation.IsCutStudioRunning())
                {
                    Debug.WriteLine("********* 1 - load template: " + desiredTemplateCst);

                    // Open and run cut studio, loading our template
                    Workstation.StartProgram(Config.CutStudioFullPathToEXE(), string.Format("-o \"{0}\"", desiredTemplateCst));

                    // Make splash screen go away
                    Workstation.SwitchFocusToCutStudio(" ");

                    // SendKeys.SendWait(" ");

                    Thread.Sleep(waitTimeBetweenCommands);

                    // Workstation.SwitchFocusToCutStudio("%fg");
                    Debug.WriteLine("********* 2 - send ALT-F, G");
                    SendKeys.SendWait("%fg");

                    Thread.Sleep(waitTimeBetweenCommands);

                    Debug.WriteLine("********* 3 - send ALT-P");
                    SendKeys.SendWait("%p");

                    Thread.Sleep(waitTimeBetweenCommands);

                    Debug.WriteLine("********* 4 - send ALT-I");
                    SendKeys.SendWait("%i");

                    Thread.Sleep(waitTimeBetweenCommands);

                    const float cutHeight = 6.75f;
                    Debug.WriteLine("********* 5 - send ALT-L, 6.75f");
                    SendKeys.SendWait(string.Format("%l{0}", cutHeight));

                    Thread.Sleep(waitTimeBetweenCommands);

                    Debug.WriteLine("********* 6 - send ENTER");
                    SendKeys.SendWait("{ENTER}");

                    // This part not working for some reason.  CS just sits there no matter how many enters are sent.
                    Thread.Sleep(waitTimeBetweenCommands);

                    Debug.WriteLine("********* 7 - send TAB");
                    SendKeys.SendWait("{TAB}");

                    Thread.Sleep(waitTimeBetweenCommands);

                    Debug.WriteLine("********* 7.5 - send ENTER");
                    SendKeys.SendWait("{ENTER}");

                    Thread.Sleep(waitTimeBetweenCommands);

                    this.Focus();
                    MessageBox.Show("Click \"Enter\" in the Cutting Setup dialog (if it exists), and click OK here to continue", "Silly Bug", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Workstation.SwitchFocusToCutStudio(" ");

                    Debug.WriteLine("********* 8 - all menus and dialogs should be closed");

                }
                else
                {
                    //Workstation.SwitchFocusToCutStudio(string.Format("^o{0}", desiredTemplateCst));
                    Workstation.SwitchFocusToCutStudio(" ");

                    Thread.Sleep(waitTimeBetweenCommands);

                    Debug.WriteLine("********* 9 - send CTRL-O");
                    SendKeys.SendWait("^o");

                    Thread.Sleep(waitTimeBetweenCommands);


                    Debug.WriteLine(string.Format("********* 10 - send {0}", desiredTemplateCst));
                    SendKeys.SendWait(desiredTemplateCst);

                    Thread.Sleep(waitTimeBetweenCommands);

                    Debug.WriteLine("********* 11 - send ENTER");
                    SendKeys.SendWait("{ENTER}");

                    Thread.Sleep(waitTimeBetweenCommands);
                }


                // File - this is doing "File / Open" for some crazy reason
                Debug.WriteLine("********* 12 - send ALT-F");
                SendKeys.SendWait("%f");

                Thread.Sleep(waitTimeBetweenCommands);
                Thread.Sleep(waitTimeBetweenCommands);
                Thread.Sleep(waitTimeBetweenCommands);

                // Import
                Debug.WriteLine("********* 13 - send m (import)");
                SendKeys.SendWait("m");

                Thread.Sleep(waitTimeBetweenCommands);

                SendKeys.SendWait(nameNumberBmpFilename);

                Thread.Sleep(waitTimeBetweenCommands);

                SendKeys.SendWait("{ENTER}");

                Thread.Sleep(waitTimeBetweenCommands);

                SendKeys.SendWait("%ou");

                Thread.Sleep(waitTimeBetweenCommands);

                SendKeys.SendWait("{ENTER}");

                Thread.Sleep(waitTimeBetweenCommands);

                SendKeys.SendWait("{ENTER}");

                Thread.Sleep(waitTimeBetweenCommands);

                SendKeys.SendWait("%oo");

                Thread.Sleep(waitTimeBetweenCommands);

                SendKeys.SendWait("%op");

                Thread.Sleep(waitTimeBetweenCommands);

                SendKeys.SendWait("%p");

                Thread.Sleep(waitTimeBetweenCommands);

                // const float nameNumberHeight = 2.8f;
                float nameNumberHeight = template.NameNoUnitHeight;
                SendKeys.SendWait(string.Format("%h{0}", nameNumberHeight));

                Thread.Sleep(waitTimeBetweenCommands);

                SendKeys.SendWait("{ENTER}");

                Thread.Sleep(waitTimeBetweenCommands);

                SendKeys.SendWait("{TAB}");

                Thread.Sleep(waitTimeBetweenCommands);

                SendKeys.SendWait("{DELETE}");

                Thread.Sleep(waitTimeBetweenCommands);

                this.Focus();
                DialogResult okToContinue = MessageBox.Show("Position the name and number at the appropriate place on the graphic, then hit \"OK\".  At that point the file will be saved.  Then you need to manually do the cutting thing.\r\n\r\nIf the whole thing is screwed up at this point, hit \"Cancel\"", "So Far So Good?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (okToContinue == DialogResult.OK)
                {
                    Thread.Sleep(waitTimeBetweenCommands);

                    Workstation.SwitchFocusToCutStudio("%fa");

                    Thread.Sleep(waitTimeBetweenCommands);

                    SendKeys.SendWait(playerCutFileName);

                    Thread.Sleep(waitTimeBetweenCommands);

                    SendKeys.SendWait("{ENTER}");

                    Thread.Sleep(waitTimeBetweenCommands);


                }
                else
                {

                }
            }
            else
            {
                if (!Workstation.IsCutStudioRunning())
                {
                    // Open and run cut studio, loading an existing cut file
                    Workstation.StartProgram(Config.CutStudioFullPathToEXE(), string.Format("-o \"{0}\"", playerCutFileName));
                }
                else
                {
                    Workstation.SwitchFocusToCutStudio(string.Format("^o{0}", playerCutFileName));

                    Thread.Sleep(waitTimeBetweenCommands);

                    SendKeys.SendWait("{ENTER}");

                }
            }

            RefreshUI();

            //
            // Clear BMP files or they will build up and hose the machine
            //
            using (new CursorManager(this, Cursors.WaitCursor))
            {
                ApplicationManager.ClearTemporaryBMPFiles();
            }

        }

        /// <summary>
        /// Yes, this is the "Go Cutting" button.  Sorry.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateCST_Click(object sender, EventArgs e)
        {
            if (ValidatePlayerSeasonForm() && ValidateUIForCutting())
            {
                PlayerSeason playerSeason = GetPlayerSeasonFromUI();
                Template template = GetTemplateFromUI();

                Image nameNumberImage = pbJerseyImage.Image;

                GoCutting(playerSeason, template, nameNumberImage);
            }
            else
            {
                MessageBox.Show("Sorry - can't do it just yet.  Make the form more better.", "No Workie", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCut_Click(object sender, EventArgs e)
        {
            //if (ValidatePlayerSeasonForm())
            //{
            //    PlayerSeason playerSeason = GetPlayerSeasonFromUI();
            //    Template template = GetTemplateFromUI();

            //    // string fileToUse = ApplicationManager.BuildPlayerWorkingCutfileName(playerSeason, template);

            //    if (!ApplicationManager.SourceCutFileExist(playerSeason, template, true))
            //    {
            //        //File.Copy(ApplicationManager.BuildPlayerSourceCutfileName(playerSeason, template),
            //        //    ApplicationManager.BuildPlayerWorkingCutfileName(playerSeason, template));
            //    }

            //    if (Workstation.IsCutStudioRunning())
            //    {
            //        Workstation.SwitchFocusToCutStudio(string.Format("^o{0}~", fileToUse));
            //    }
            //    else
            //    {
            //        // Open and run cut studio
            //        Workstation.StartProgram(Config.CutStudioFullPathToEXE(), string.Format("-o \"{0}\"", fileToUse));
            //    }
            //}
        }

        private void txtFullName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
            {
                if (Char.IsLower(e.KeyChar))
                {
                    e.KeyChar = Char.ToUpper(e.KeyChar);
                }
                else
                {
                    e.KeyChar = Char.ToLower(e.KeyChar);
                }
            }
        }

        private void UpdatetpSales_lblEbayToolsStatus(string msg)
        {
            tpSales_lblEbayToolsStatus.Text = msg;
            Application.DoEvents();
        }

        private void btnCreateTurbolisterFile_Click(object sender, EventArgs e)
        {
            Season season = tsTabSalesTeamSelector.CurrentSeason;

            if (season == null)
            {
                MessageBox.Show("Please select a season first!", "Just Can't Go On", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            Franchise franchise = tsTabSalesTeamSelector.CurrentFranchise;
            bool useGeneric = chktpSales_Generic.Checked;
            DataManager.GetPlayerSeasonType type = useGeneric ? DataManager.GetPlayerSeasonType.GroupedByLastnameAndNumber : DataManager.GetPlayerSeasonType.TeamsAndPlayers;
            string errMsg = string.Empty;

            //
            // If you wanna use a file/open dialog, here's where to do it.  Otherwise, read from config file.
            //
            openFileDialog1.InitialDirectory = Path.GetFullPath(Config.EbayCSVOutputFullTemplateFilename);
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Turbolister Output Files (*.csv)|*.csv";
            openFileDialog1.Title = string.Format("Select {0} CSV File To Parse To Generate Listings", useGeneric ? "GENERIC" : "NON-GENERIC");

            DialogResult dr = openFileDialog1.ShowDialog();

            if (dr == DialogResult.Cancel)
            {
                return;
            }

            // string ebayCSVFilename = useGeneric ? Config.EbayCSVOutputGenericTemplateFilename : Config.EbayCSVOutputFullTemplateFilename;

            string ebayCSVFilename = openFileDialog1.FileName;

            using (CursorManager cm = new CursorManager(this, Cursors.WaitCursor))
            {
                bool res = ApplicationManager.CreateTurbolisterFile(season, 
                    franchise, 
                    chkNewPlayersOnly.Checked, 
                    chktpSales_CreateGraphics.Checked,
                    type,
                    useGeneric,
                    ebayCSVFilename,
                    out errMsg,
                    UpdatetpSales_lblEbayToolsStatus,
                    UploadGraphicsProgress);

                if (res)
                {
                    MessageBox.Show("Awesome, it worked!  Now go kick ass!", "Export Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(string.Format("Nope, no workie.  Error message was: \"{0}\"", errMsg), "Output Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnImportTeam_Click(object sender, EventArgs e)
        {
            Franchise franchise = tsTabDBTeamSelector.CurrentFranchise;
            Season season = tsTabDBTeamSelector.CurrentSeason;

            if (franchise == null || season == null)
            {
                MessageBox.Show("Make sure to select a franchise and season!", "I Just Cant Do It Captain", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            openFileDialog1.InitialDirectory = Config.ImportFileDir();
            openFileDialog1.FileName = "";
            openFileDialog1.Title = string.Format("Select Tab-Delimited File For \"{0}\" From SI.com", franchise.FranchiseDesc);

            DialogResult dr = openFileDialog1.ShowDialog();

            if (dr == DialogResult.OK)
            {
                int skipped;
                List<PlayerSeason> players = ApplicationManager.GetPlayerSeasonListFromFile(openFileDialog1.FileName, franchise, season, out skipped);

                if (players.Count > 0)
                {
                    string msgBoxText = string.Format("Found {0} players:\r\n\r\n{1}\r\n...\r\n{2}\r\n\r\nOk to import?",
                        players.Count,
                        players[0],
                        players[players.Count - 1]);

                    if (MessageBox.Show(msgBoxText, "Data File Results", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        bool res = DataManager.WritePlayerSeasonListToDB(players);
                        MessageBox.Show(string.Format("Write to db result: {0}", res));
                    }
                }
                else
                {
                    MessageBox.Show("There were no players in that file!", "Bad Input File", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btnUppercaseDisplayName_Click(object sender, EventArgs e)
        {
            txtFullName.Text = txtFullName.Text.ToUpper();
        }

        private void txtEbayPaste_TextChanged(object sender, EventArgs e)
        {
            bool parseError = false;

            string[] parts = txtEbayPaste.Text.Split('#');

            if (parts.Length == 2)
            {
                string lastName = string.Empty;

                string[] names = parts[0].Trim().Split(' ');

                if (names.Length == 1)
                {
                    // We have only a first name
                    lastName = names[0];
                }
                else if (names.Length > 1)
                {
                    // Have a bunch of names, just get the last one.
                    lastName = names[names.Length - 1];
                }

                if (lastName != string.Empty)
                {
                    // txtFullName.Text = parts[0].Substring(parts[0].Trim().LastIndexOf(" ")).Trim().ToUpper();
                    txtFullName.Text = lastName.ToUpper();
                    txtNumber.Text = parts[1].Substring(0, parts[1].IndexOf(" ")).Trim();

                    List<PlayerSeason> players = DataManager.GetPlayerSeasons(txtFullName.Text, txtNumber.Text);

                    if (players.Count == 0)
                    {
                        lblPasteStatus.Text = "No records found!";
                        lblPasteStatus.ForeColor = Color.Red;
                    }
                    else
                    {
                        Template template = players[0].TemplateCurrent;
                        bool haveUniqueTemplate = true;

                        if (players.Count > 1)
                        {
                            foreach (PlayerSeason playerSeason in players)
                            {
                                if (playerSeason.TemplateCurrent.TemplateId != template.TemplateId)
                                {
                                    haveUniqueTemplate = false;
                                    break;
                                }
                            }
                        }

                        if (haveUniqueTemplate)
                        {
                            cmbTemplate.Text = template.TemplateDescShort;

                            lblPasteStatus.Text = "Found!";
                            lblPasteStatus.ForeColor = Color.Green;

                            if (ValidateUIForCutting())
                            {
                                ProcessGraphics(players[0], template);
                                GoCutting(players[0], template, pbJerseyImage.Image);
                            }
                            else
                            {
                                MessageBox.Show("Make sure you have cuttable graphics options selected, and try again.", "Can't go Cutting", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            lblPasteStatus.Text = "Multiple players found - manually select template";
                            lblPasteStatus.ForeColor = Color.Yellow;

                            MessageBox.Show(lblPasteStatus.Text, "Ambiguous Template", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    parseError = true;
                }

                txtEbayPaste.Text = string.Empty;

                RefreshUI();
            }
            else
            {
                if (txtEbayPaste.Text != string.Empty)
                {
                    parseError = true;
                }
            }

            if (parseError)
            {
                MessageBox.Show("Sorry, dunno what to do.  String should be in format \"NAME #11\" (no quotes).", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtCutFileName_Enter(object sender, EventArgs e)
        {
            txtCutFileName.SelectAll();
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            if (pbJerseyImage.Image != null)
            {
                bool res = false;

                if (SelectedImageType() == ApplicationManager.StickerImageType.Cutting)
                {

                    //string fileName = Functions.BuildFilenameFromElements(Common.Config.GraphicsRootDirectory(), "notyet.bmp");

                    //res = ApplicationManager.SaveImageToFile(pbJerseyImage.Image, fileName, ImageFormat.Bmp, 24L);

                    MessageBox.Show("I haven't really implemented saving the cutting file yet.", "Not Yet", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else if (SelectedImageType() == ApplicationManager.StickerImageType.Marketing)
                {
                    saveFileDialog1.Title = "Select File To Save GIF File As";
                    DialogResult dr = saveFileDialog1.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        string fileName = saveFileDialog1.FileName;

                        //Functions.BuildFilenameFromElements(Config.GraphicsRootDirectory(), "notyet.gif");

                        res = ApplicationManager.SaveImageToFile(pbJerseyImage.Image, fileName, ImageFormat.Gif, 4L, 1800);     // 320);

                        MessageBox.Show(string.Format("Save operation returned: {0}", res), "Results of Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    throw new ApplicationException("Sticker image type not implemented");
                }

            }

        }

        //private void UploadGraphicsProgressWorkerThread(string progress)
        //{
        //    if (this.tpSales_lblEbayGraphicsStatus.InvokeRequired)
        //    {
        //        ApplicationManager.LogDelegate d = new ApplicationManager.LogDelegate(UploadGraphicsProgress);
        //        this.Invoke(d, new object[] { progress });
        //    }
        //    else
        //    {
        //        // throw new ApplicationException("Weirdness - doesn't seem like invoke is required, but it should be?");
        //        Debug.WriteLine(string.Format("Weirdness - doesn't seem like invoke is required, but it should be? \"{0}\"", progress));
        //    }
        //}

        private void UploadGraphicsProgress(string progress)
        {
            //// InvokeRequired required compares the thread ID of the
            //// calling thread to the thread ID of the creating thread.
            //// If these threads are different, it returns true.
            //if (this.textBox1.InvokeRequired)
            //{
            //    SetTextCallback d = new SetTextCallback(SetText);
            //    this.Invoke(d, new object[] { text });
            //}
            //else
            //{
            //    this.textBox1.Text = text;
            //}

            if (this.tpSales_lblEbayGraphicsStatus.InvokeRequired)
            {
                // throw new ApplicationException("Weirdness - seems like invoke is required, but it shouldn't be?");
                // Debug.WriteLine(string.Format("Weirdness - seems like invoke is required, but it shouldn't be? \"{0}\"", progress));
                ApplicationManager.LogDelegate d = new ApplicationManager.LogDelegate(UploadGraphicsProgress);
                this.Invoke(d, new object[] { progress });
            }
            else
            {
                tpSales_lblEbayGraphicsStatus.Text = progress;
            }

            Application.DoEvents();
        }

        private void btnUploadGraphics_Click(object sender, EventArgs e)
        {
            Season season = tsTabSalesTeamSelector.CurrentSeason;
            Franchise franchise = tsTabSalesTeamSelector.CurrentFranchise;

            using (CursorManager cm = new CursorManager(this, Cursors.WaitCursor))
            {
                bool res = ApplicationManager.UploadMarketingImages(season, franchise, chkNewPlayersOnly.Checked, UploadGraphicsProgress, tpSales_chkUploadAll.Checked);

                MessageBox.Show(string.Format("Application manager says operation results are: {0}", res),
                    "Upload Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btntpProductsAddToTeam_Click(object sender, EventArgs e)
        {
            throw new ApplicationException("I think this code is working - but it hasn't be tested.  Do that first.");

            //PlayerSeason player = new PlayerSeason();

            //player.FranchiseCode = tpProductsTeamSelector.CurrentFranchise.FranchiseCode;
            //player.SeasonId = tpProductsTeamSelector.CurrentSeason.SeasonId;
            //player.FirstName = txtFName.Text;
            //player.LastName = txtLName.Text;
            //player.MiddleInitial = txtMI.Text;
            //player.JerseyName = txtFullName.Text;
            //player.JerseyNumber = txtNumber.Text;

            //List<PlayerSeason> singlePlayer = new List<PlayerSeason>();
            //singlePlayer.Add(player);

            //bool res = DataManager.WritePlayerSeasonListToDB(singlePlayer);

            //MessageBox.Show(string.Format("Database says: {0}", res), "Player Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btntbSales_createTLCustomsFile_Click(object sender, EventArgs e)
        {
            using (CursorManager cm = new CursorManager(this, Cursors.WaitCursor))
            {
                bool res = ApplicationManager.CreateTurbolisterCustomsFile();

                MessageBox.Show(string.Format("Application manager says operation results are: {0}", res),
                    "Export Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tpDB_btnPastePlayers_Click(object sender, EventArgs e)
        {
            if ((tsTabDBTeamSelector.CurrentSeason == null) || (tsTabDBTeamSelector.CurrentFranchise == null))
            {
                MessageBox.Show("Please select both a season and a franchise.", "Bad User", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                frmPastePlayers playerForm = new frmPastePlayers();

                playerForm.CurrentFranchise = tsTabDBTeamSelector.CurrentFranchise;
                playerForm.CurrentSeason = tsTabDBTeamSelector.CurrentSeason;

                playerForm.ShowDialog();

                if (playerForm.Result == DialogResult.OK)
                {
                    //
                    // We have some players!
                    //
                    bool res = DataManager.WritePlayerSeasonListToDB(playerForm.PlayerSeasons);
                    MessageBox.Show(string.Format("Write to db result: {0}", res));
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void btn_orders_ProcessMonsterFile_Click(object sender, EventArgs e)
        {
            string importFName = string.Empty;

            openFileDialog1.InitialDirectory = Config.MonsterImportFileDir;
            openFileDialog1.FileName = "";
            openFileDialog1.Title = "Select Monster XML Orders File Tab-Delimited File For \"{0}\" From SI.com";
            openFileDialog1.Filter = "Monster Files (*.xml)|*.xml|All Files|*.*";

            DialogResult dr = openFileDialog1.ShowDialog();

            if (dr == DialogResult.OK)
            {
                importFName = openFileDialog1.FileName;
            }
            else
            {
                return;
            }

            if (File.Exists(importFName))
            {
                // DataSet importData = new DataSet();
                // importData.ReadXml(importFName, XmlReadMode.InferSchema);  // Causes strange error
                string statusMsg;
                List<Order> orders = ApplicationManager.GetOrdersFromMonsterXMLFile(importFName, out statusMsg);

                if (orders.Count == 0)
                {
                    MessageBox.Show("There were no orders found in the file.", "No Orders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //
                    // Save processed data for future reference
                    //
                    _ordersToProcess = orders;
                    _orderFileName = importFName;

                    //
                    // Populate the grid
                    //
                    List<OrderDisplayable> orderDisplayList = ApplicationManager.BuildGridListFromOrderList(orders);

                    tpOrders_orderGrid.DataSource = orderDisplayList;
                    // tpOrders_orderGrid.databin

                    if (statusMsg != string.Empty)
                    {
                        MessageBox.Show(string.Format("The orders were processed, but there was additional information:/r/n/r/n{0}", statusMsg),
                            string.Format("{0} Orders Processed", orders.Count),
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Just the grid having rows is information enough for the user.
                    }
                }
            }
            else
            {
                MessageBox.Show(string.Format("No import file found (expected: \"{0}\")", importFName), "No Workie", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            }
        }

        private void btnOrders_CreatePaypalShippingFile_Click(object sender, EventArgs e)
        {
            if (_ordersToProcess != null)
            {
                string statusMsg;
                string otherAddrsFName = string.Empty;

                if (ApplicationManager.CreatePaypalMultishipCSVFile(_ordersToProcess, out statusMsg, out otherAddrsFName))
                {
                    if (otherAddrsFName != string.Empty)
                    {
                        Workstation.OpenFile(otherAddrsFName);
                    }

                    MessageBox.Show("Cool, all set.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(string.Format("There was an error:\r\n\r\n{0}", statusMsg), "No Can Do", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("You have to import a file first.", "Can't do it", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        /// <summary>
        /// Set the values of the selected item in the orders grid.  Pass -1 to clear the ui.
        /// </summary>
        /// <param name="index">The index of the item in the grid's datasource to load.  Not the same as the row!</param>
        private void OrderInGridSelected(int index)
        {
            // 
            // Fill in data
            //
            if (index >= 0)
            {
                List<OrderDisplayable> orderItems = (List<OrderDisplayable>)tpOrders_orderGrid.DataSource;
                OrderDisplayable orderDisp = orderItems[index];
                _currentOrderDisplaying = orderDisp;

                tpOrders_lblCustName.Text = string.Format("{0} {1}", orderDisp.ShippingFirstName, orderDisp.ShippingLastName);
                PlayerSeason playerSeason = orderDisp.Order.Items[orderDisp.Index].PlayerSeason;
                tpOrders_lblTemplateDesc.Text = playerSeason.TemplateCurrent.TemplateDescShort;
                tpOrders_lblJerseyName.Text = playerSeason.JerseyName;
                tpOrders_lblJerseyNumber.Text = playerSeason.JerseyNumber;
                tpOrders_lblQty.Text = orderDisp.Order.Items[orderDisp.Index].Quantity.ToString();
                tpOrders_lblColor.Text = orderDisp.ItemMaterial.ToString();
                tpOrders_lblColor.ForeColor = ApplicationManager.GetWindowsColorFromRGBString(orderDisp.ItemMaterial.RGBColorHex);
                tpOrders_lblColor.BackColor = ApplicationManager.RecommendedBackgroundColor(orderDisp.ItemMaterial.RGBColorHex) == ProductItemJersey.PreviewImageBackgroundColorType.Dark ? Color.Black : Color.White;

                ProcessGraphics(playerSeason, playerSeason.TemplateCurrent, tpOrders_pbJerseyImage, ApplicationManager.StickerImageType.Marketing, false, orderDisp.ItemMaterial);
            }
            else
            {
                tpOrders_lblCustName.Text = string.Empty;
                tpOrders_lblTemplateDesc.Text = string.Empty;
                tpOrders_lblJerseyName.Text = string.Empty;
                tpOrders_lblJerseyNumber.Text = string.Empty;
                tpOrders_lblQty.Text = string.Empty;
                tpOrders_pbJerseyImage.Image = null;
            }
        }

        private void tpOrders_orderGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //int itemIndex = e.RowIndex;
            //OrderInGridSelected(itemIndex);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void tpOrders_orderGrid_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = tpOrders_orderGrid.SelectedRows;
            int itemIndex;

            if (rows.Count > 0)
            {
                itemIndex = rows[0].Index;
            }
            else
            {
                itemIndex = -1;
            }

            OrderInGridSelected(itemIndex);
        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void tpOrders_GoCutting_Click(object sender, EventArgs e)
        {
            if (PictureBoxStickerType(tpOrders_pbJerseyImage) != ApplicationManager.StickerImageType.Cutting)
            {
                if (_currentOrderDisplaying != null)
                {
                    PlayerSeason playerSeason = _currentOrderDisplaying.Order.Items[_currentOrderDisplaying.Index].PlayerSeason;
                    ProcessGraphics(playerSeason, playerSeason.TemplateCurrent, tpOrders_pbJerseyImage, ApplicationManager.StickerImageType.Cutting, false);
                }
                else
                {
                    MessageBox.Show("Please select a row in the grid", "No Can Do", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

            }

            GoCutting(_currentOrderDisplaying.ItemPlayerSeason,
                _currentOrderDisplaying.ItemTemplate,
                tpOrders_pbJerseyImage.Image);

        }

        private void chktpSales_Generic_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tpOrders_pbJerseyImage_Click(object sender, EventArgs e)
        {
            tpOrders_GoCutting_Click(pbJerseyImage, null);
        }

        private void tpOrders_btnArchiveMonsterFile_Click(object sender, EventArgs e)
        {
            string importFName = Functions.BuildFilenameFromElements(Config.MonsterImportFileDir, Config.MonsterImportFName);
            string archiveFName = Functions.BuildFilenameFromElements(Config.MonsterArchiveDir, string.Format("{0:yyyy-MM-dd}-{1}", DateTime.Now, Config.MonsterImportFName));

            try
            {
                File.Move(importFName, archiveFName);

                if (!File.Exists(importFName))
                {
                    tpOrders_orderGrid.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Can't archive file: {0} - gotta do it yourself.", importFName),
                    "Bummer Dude",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void ssMainStatus_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void AddScreenScrapeStatus(string line)
        {
            LogFile logFile = new LogFile();

            try
            {

                ArrayList lines = new ArrayList(txtDB_ScreenscrapeStatus.Lines);
                lines.Add(line);
                if (lines.Count > 300)
                {
                    lines.RemoveRange(0, lines.Count - 300);
                }
                txtDB_ScreenscrapeStatus.Lines = (string[])lines.ToArray(typeof(string));

                txtDB_ScreenscrapeStatus.SelectionStart = txtDB_ScreenscrapeStatus.Text.Length - 1;
                txtDB_ScreenscrapeStatus.ScrollToCaret();

                Application.DoEvents();
            }
            catch (Exception e)
            {
            }

            logFile.Log(line);

        }

        private void btnDB_GetSchoolsFromHSCWebsite_Click(object sender, EventArgs e)
        {
            ScreenScrapeHighSchoolCom.ScreenScrapeHSCWebsite(AddScreenScrapeStatus);
        }

        private void SkipEveryLeadRefresh(object sender, EventArgs e)
        {
            if (Functions.IsNumeric(tpDB_lblSupplyQty.Text) && Functions.IsNumeric(tpDB_txtQtyToGet.Text))
            {
                int leads = int.Parse(tpDB_lblSupplyQty.Text);
                int qtyToGet = int.Parse(tpDB_txtQtyToGet.Text);

                if (leads > qtyToGet && qtyToGet != 0)
                {
                    int include = (int)(leads / qtyToGet);
                    tpDB_txtSkipEvery.Text = include.ToString();
                }
                else
                {
                    tpDB_txtSkipEvery.Text = "";
                }
            }
        }

        private void LeadOutputFileRefreshSupply(object sender, EventArgs e)
        {
            Code leadSourceCode = tpDB_cmbLeadSource.SelectedItem as Code;
            Code leadStatusCode = tpDB_cmbLeadStatus.SelectedItem as Code;
            int leads = 0;

            if (leadSourceCode != null && leadStatusCode != null)
            {
                leads = DataManager.GetLeadQty(leadSourceCode.CodeValue, leadStatusCode.CodeValue, tpDB_txtMinEnrollment.Text, tpDB_chkMustBeAthletics.Checked);
            }

            tpDB_lblSupplyQty.Text = leads.ToString();

            SkipEveryLeadRefresh(sender, e);
        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void LogDBTabLeadGettingStatus(string formatStr, params string[] message)
        {
            tpDB_lblStatus.Text = string.Format(formatStr, message);
            Application.DoEvents();
        }

        private void LoadDBTabDatabaseProgress(string progressNumber)
        {
            //if (int.Parse(progressNumber) % 5 == 0)
            //{
                LogDBTabLeadGettingStatus("Processing result lead: {0}", progressNumber);
            //}
        }

        private void LoadDBTabCSVProgress(string progressNumber)
        {
            LogDBTabLeadGettingStatus("Processing CSV lead: {0}", progressNumber);
        }

        /// <summary>
        /// Creates a lead output file, updates the db.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDB_GoLeadOutputFile_Click(object sender, EventArgs e)
        {
            if (!Functions.IsNumeric(tpDB_txtQtyToGet.Text))
            {
                MessageBox.Show("Please enter a quantity to get.", "Doesn't make sense", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }

            if (Functions.IsEmptyString(tpDB_txtOutputFName.Text))
            {
                MessageBox.Show("Please enter an output file name.", "Not much to do", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }

            Code leadSourceCode = tpDB_cmbLeadSource.SelectedItem as Code;
            Code leadStatusCode = tpDB_cmbLeadStatus.SelectedItem as Code;
            List<Lead> leads = null;

            if (leadSourceCode != null && leadStatusCode != null)
            {
                leads = DataManager.GetLeads(leadSourceCode.CodeValue,
                    leadStatusCode.CodeValue,
                    tpDB_txtMinEnrollment.Text,
                    tpDB_txtSkipEvery.Text,
                    LoadDBTabDatabaseProgress,
                    DataManager.AddressPresence.Required,
                    tpDB_chkMustBeAthletics.Checked);
            }

            if (leads != null && leads.Count > 0)
            {
                int qtyToGet = int.Parse(tpDB_txtQtyToGet.Text);

                //
                // Truncate the list to the qty that we want
                //
                if (leads.Count > qtyToGet)
                {
                    leads.RemoveRange(qtyToGet, leads.Count - qtyToGet);
                }

                //
                // Set the "title" field; not sure which will perform better, let's try both for now.  1/3 principal, 2/3 athletic director
                //
                int rowCount = 0;
                foreach (Lead lead in leads)
                {
                    lead.FirstName = rowCount++ % 3 == 0 ? "Principal" : "Athletic Director";
                }

                CSVSerializer csv = new CSVSerializer(leads);

                csv.AddCSVColumn("lead_id", "CustomerId");
                csv.AddCSVColumn("title", "FirstName");
                csv.AddCSVColumn("company", "ShippingAddress.CompanyName");
                csv.AddCSVColumn("address", "ShippingAddress.Address1");
                csv.AddCSVColumn("address2", "ShippingAddress.Address2");
                csv.AddCSVColumn("city", "ShippingAddress.City");
                csv.AddCSVColumn("st", "ShippingAddress.StateProvAbbrev");
                csv.AddCSVColumn("zip", "ShippingAddress.ZipPostalCode");
                csv.AddCSVColumn("country", "ShippingAddress.CountryCode");
                csv.AddCSVColumn("lead_src_code", "LeadSourceCode");

                if (csv.CreateCSV(tpDB_txtOutputFName.Text, LoadDBTabCSVProgress))
                {
                    //
                    // Sweet - we have our file
                    // 
                    foreach (Lead lead in leads)
                    {
                        LogDBTabLeadGettingStatus("Updating status log for lead: {0}", lead.CompanyName);
                        DataManager.WriteLeadStatusLog(lead, Lead.LeadStatusCode.lscLeadOutputToMailingListCSV, tpDB_txtOutputFName.Text);
                    }

                    LogDBTabLeadGettingStatus("All leads processed!  Go kick some ass!");
                }
                else
                {
                    LogDBTabLeadGettingStatus("Can't create CSV file!");
                }
            }
            else
            {
                LogDBTabLeadGettingStatus("No leads to process!");
            }
        }

        private void btnDB_GetSchoolsFromSUCWebsite_Click(object sender, EventArgs e)
        {
            ScreenScrapeStateUniversityCom.ScreenScrapeSUCWebsite(AddScreenScrapeStatus);
        }

        private void tpDB_RefreshBadAddresses(object sender, EventArgs e)
        {
            tpDB_lstBadAddresses.Items.Clear();

            Code leadSourceCode = tpDB_cmbLeadSource.SelectedItem as Code;
            Code leadStatusCode = tpDB_cmbLeadStatus.SelectedItem as Code;
            List<Lead> leads = null;

            if (leadSourceCode != null && leadStatusCode != null)
            {
                leads = DataManager.GetLeads(leadSourceCode.CodeValue,
                    leadStatusCode.CodeValue,
                    "",
                    "",
                    LoadDBTabDatabaseProgress,
                    DataManager.AddressPresence.MustBeBlank,
                    tpDB_chkMustBeAthletics.Checked);
            }

            if (leads != null && leads.Count > 0)
            {
                foreach (Lead lead in leads)
                {
                    tpDB_lstBadAddresses.Items.Add(lead);
                }
            }

        }

        private void tpDB_lstBadAddresses_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tpDB_lstBadAddresses_DoubleClick(object sender, EventArgs e)
        {
            Lead lead = tpDB_lstBadAddresses.SelectedItem as Lead;

            if (lead != null)
            {
                frmCustomer customerForm = new frmCustomer(lead);
                customerForm.ShowDialog();

                if (customerForm.Result == DialogResult.OK)
                {
                    lead = customerForm.CurrentCustomer as Lead;
                    bool res = DataManager.WriteCustomer(lead, DataManager.ObjectWriteMode.Update);
                    MessageBox.Show(string.Format("Updated db, success results are: {0}", res), "Update Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (res)
                    {
                        tpDB_lstBadAddresses.Items[tpDB_lstBadAddresses.SelectedIndex] = lead;
                    }
                }
            }
        }

        private void tpDB_btnImportBadAddrs_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Mailing List Errors (*.csv)|*.csv|All Files|*.*";
            openFileDialog1.Title = "Select Error File";
            openFileDialog1.InitialDirectory = System.Windows.Forms.Application.StartupPath;
            int updatedLeads = 0;
            int leadsNotFound = 0;
            int leadsUnexpectedStatus = 0;
            int cantUpdateDb = 0;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader reader = new StreamReader(openFileDialog1.OpenFile());

                DataTable table = CSVReader.Parse(reader.ReadToEnd(), true);

                AddScreenScrapeStatus(string.Format("Found {0} leads in data file.", table.Rows.Count));

                foreach (DataRow row in table.Rows)
                {
                    Lead existingLead = DataManager.GetLeadFromMailingListErrorCSVDataRow(row);

                    if (existingLead != null)
                    {
                        if (existingLead.LeadStatusCodeCurrent == Lead.LeadStatusCode.lscLeadOutputToMailingListCSV)
                        {
                            if (DataManager.WriteLeadStatusLog(existingLead, Lead.LeadStatusCode.lscInvalidMailingAddress))
                            {
                                updatedLeads++;
                            }
                            else
                            {
                                AddScreenScrapeStatus(string.Format("Can't update db!: \"{0}\"", existingLead.CompanyName));
                                cantUpdateDb++;
                            }
                        }
                        else
                        {
                            AddScreenScrapeStatus(string.Format("Unexpected lead status: \"{0}\" was \"{1}\"", existingLead.CompanyName, existingLead.LeadStatusCodeCurrent.ToString()));
                            leadsUnexpectedStatus++;
                        }
                    }
                    else
                    {
                        AddScreenScrapeStatus("Can't find lead in database!");
                        leadsNotFound++;
                    }
                }

                AddScreenScrapeStatus(string.Format("Done, updated: {0}, can't update: {1}, bad status: {2}, not found: {3}",
                    updatedLeads, cantUpdateDb, leadsUnexpectedStatus, leadsNotFound));
            }
        }

        private void tpDB_btnMailed_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Click this after the mailing list error file has been processed.\r\n\r\nHas the mailing list error file been processed?", "After Error File", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //bool res = DataManager.UpdateLeadsStatusCode(Lead.LeadStatusCode.lscLeadOutputToMailingListCSV, Lead.LeadStatusCode.lscLeadMailedPostcard);
                //MessageBox.Show(string.Format("Update of db result: {0}", res), "All Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tpSales_Mkt_BuildMsgBrd_Click(object sender, EventArgs e)
        {
            bool res = ApplicationManager.GenerateMarketingMessageBoardsWorksheetCSV(5, null);
        }

        private void label32_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Highlight the rows in the grid with the vinyl color.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tpOrders_orderGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in tpOrders_orderGrid.Rows)
            {
                OrderDisplayable order = row.DataBoundItem as OrderDisplayable;
                row.DefaultCellStyle.BackColor = row.DefaultCellStyle.SelectionBackColor = ApplicationManager.GetWindowsColorFromRGBString(order.ItemMaterial.RGBColorHex);
                row.DefaultCellStyle.ForeColor = row.DefaultCellStyle.SelectionForeColor = ApplicationManager.RecommendedBackgroundColor(order.ItemMaterial.RGBColorHex) == ProductItemJersey.PreviewImageBackgroundColorType.Dark ? Color.Black : Color.White;
            }
        }
    }
}
