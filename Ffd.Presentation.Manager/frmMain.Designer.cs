namespace Ffd.Presentation.Manager
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "Products"}, 2, System.Drawing.Color.Empty, System.Drawing.SystemColors.Window, null);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Sales", 1);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Orders", 3);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("Database", 0);
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("Reports", 4);
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ilMainMenu = new System.Windows.Forms.ImageList(this.components);
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ssMainStatus = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlMainMenuPadding = new System.Windows.Forms.Panel();
            this.lvMainMenu = new System.Windows.Forms.ListView();
            this.splMainMenu = new System.Windows.Forms.Splitter();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tcMainAppArea = new Ffd.Presentation.Manager.TabControl();
            this.tpProducts = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btntpProductsAddToTeam = new System.Windows.Forms.Button();
            this.txtLName = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtMI = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtFName = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tpProductsTeamSelector = new Ffd.Presentation.Manager.TeamSelector();
            this.btnSaveAs = new System.Windows.Forms.Button();
            this.gbStatus = new System.Windows.Forms.GroupBox();
            this.txtCutFileName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCreateCST = new System.Windows.Forms.Button();
            this.lblCSTExists = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.gbCustom = new System.Windows.Forms.GroupBox();
            this.lblPasteStatus = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.chkLeadingZero = new System.Windows.Forms.CheckBox();
            this.txtEbayPaste = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnUppercaseDisplayName = new System.Windows.Forms.Button();
            this.cmbTemplate = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.lblNumber = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.btnTryAgain = new System.Windows.Forms.Button();
            this.gbImageType = new System.Windows.Forms.GroupBox();
            this.chkBoundingBox = new System.Windows.Forms.CheckBox();
            this.rbMarketing = new System.Windows.Forms.RadioButton();
            this.rbCutting = new System.Windows.Forms.RadioButton();
            this.pbJerseyImage = new System.Windows.Forms.PictureBox();
            this.tpSales = new System.Windows.Forms.TabPage();
            this.gbSalesMarketing = new System.Windows.Forms.GroupBox();
            this.tpSales_Mkt_BuildMsgBrd = new System.Windows.Forms.Button();
            this.label31 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.btntbSales_createTLCustomsFile = new System.Windows.Forms.Button();
            this.gbSalesEbay = new System.Windows.Forms.GroupBox();
            this.tpSales_chkUploadAll = new System.Windows.Forms.CheckBox();
            this.tpSales_lblEbayGraphicsStatus = new System.Windows.Forms.Label();
            this.tpSales_lblEbayToolsStatus = new System.Windows.Forms.Label();
            this.chktpSales_Generic = new System.Windows.Forms.CheckBox();
            this.chktpSales_CreateGraphics = new System.Windows.Forms.CheckBox();
            this.label18 = new System.Windows.Forms.Label();
            this.chkNewPlayersOnly = new System.Windows.Forms.CheckBox();
            this.btnUploadGraphics = new System.Windows.Forms.Button();
            this.tsTabSalesTeamSelector = new Ffd.Presentation.Manager.TeamSelector();
            this.btnCreateTurbolisterFile = new System.Windows.Forms.Button();
            this.tpOrders = new System.Windows.Forms.TabPage();
            this.tpOrders_btnArchiveMonsterFile = new System.Windows.Forms.Button();
            this.gbOrdersDetail = new System.Windows.Forms.GroupBox();
            this.tpOrders_lblColor = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.tpOrders_lblQty = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tpOrders_pbJerseyImage = new System.Windows.Forms.PictureBox();
            this.tpOrders_lblJerseyNumber = new System.Windows.Forms.Label();
            this.tpOrders_lblJerseyName = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tpOrders_lblTemplateDesc = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tpOrders_lblCustName = new System.Windows.Forms.Label();
            this.button8 = new System.Windows.Forms.Button();
            this.tpOrders_GoCutting = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_orders_ProcessMonsterFile = new System.Windows.Forms.Button();
            this.gbOrdersStatus = new System.Windows.Forms.GroupBox();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.tpOrders_orderGrid = new System.Windows.Forms.DataGridView();
            this.orderIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShippingFirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShippingLastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShippingCity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShippingState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShippingZip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShippingCountry = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumberOfItems = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FirstItemDescriptionExternal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FirstItemTemplateId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FirstItemTemplateDescriptionShort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FirstItemQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FirstItemJerseyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FirstItemJerseyNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FirstItemColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orderDisplayableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.btnOrders_CreatePaypalShippingFile = new System.Windows.Forms.Button();
            this.tpDatabase = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tpDB_btnImportBadAddrs = new System.Windows.Forms.Button();
            this.tpDB_btnRefresh = new System.Windows.Forms.Button();
            this.tpDB_lstBadAddresses = new System.Windows.Forms.ListBox();
            this.tpDB_btnFixAddr = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tpDB_btnMailed = new System.Windows.Forms.Button();
            this.tpDB_chkMustBeAthletics = new System.Windows.Forms.CheckBox();
            this.tpDB_lblStatus = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.tpDB_txtSkipEvery = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.tpDB_txtMinEnrollment = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.tpDB_txtOutputFName = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.tpDB_txtQtyToGet = new System.Windows.Forms.TextBox();
            this.tpDB_lblSupplyQty = new System.Windows.Forms.Label();
            this.tpDB_cmbLeadStatus = new System.Windows.Forms.ComboBox();
            this.tpDB_cmbLeadSource = new System.Windows.Forms.ComboBox();
            this.btnDB_GoLeadOutputFile = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDB_GetSchoolsFromSUCWebsite = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDB_ScreenscrapeStatus = new System.Windows.Forms.TextBox();
            this.btnDB_GetSchoolsFromHSCWebsite = new System.Windows.Forms.Button();
            this.gbImportData = new System.Windows.Forms.GroupBox();
            this.tpDB_btnPastePlayers = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.tsTabDBTeamSelector = new Ffd.Presentation.Manager.TeamSelector();
            this.btnImportTeam = new System.Windows.Forms.Button();
            this.tpReports = new System.Windows.Forms.TabPage();
            this.button9 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.orderBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.menuStripMain.SuspendLayout();
            this.ssMainStatus.SuspendLayout();
            this.pnlMainMenuPadding.SuspendLayout();
            this.tcMainAppArea.SuspendLayout();
            this.tpProducts.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbStatus.SuspendLayout();
            this.gbCustom.SuspendLayout();
            this.gbImageType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbJerseyImage)).BeginInit();
            this.tpSales.SuspendLayout();
            this.gbSalesMarketing.SuspendLayout();
            this.gbSalesEbay.SuspendLayout();
            this.tpOrders.SuspendLayout();
            this.gbOrdersDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tpOrders_pbJerseyImage)).BeginInit();
            this.gbOrdersStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tpOrders_orderGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderDisplayableBindingSource)).BeginInit();
            this.tpDatabase.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gbImportData.SuspendLayout();
            this.tpReports.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.orderBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // ilMainMenu
            // 
            this.ilMainMenu.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilMainMenu.ImageStream")));
            this.ilMainMenu.TransparentColor = System.Drawing.Color.Transparent;
            this.ilMainMenu.Images.SetKeyName(0, "database-icon-64x64.jpg");
            this.ilMainMenu.Images.SetKeyName(1, "ebay-icon-75x75.jpg");
            this.ilMainMenu.Images.SetKeyName(2, "brown-jersey-icon2-64x64.gif");
            this.ilMainMenu.Images.SetKeyName(3, "headset2-64x64.jpg");
            this.ilMainMenu.Images.SetKeyName(4, "reports-icon-64x64.jpg");
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(797, 24);
            this.menuStripMain.TabIndex = 3;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // ssMainStatus
            // 
            this.ssMainStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.ssMainStatus.Location = new System.Drawing.Point(0, 593);
            this.ssMainStatus.Name = "ssMainStatus";
            this.ssMainStatus.Size = new System.Drawing.Size(797, 22);
            this.ssMainStatus.TabIndex = 4;
            this.ssMainStatus.Text = "statusStrip1";
            this.ssMainStatus.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ssMainStatus_ItemClicked);
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(112, 17);
            this.toolStripStatusLabel.Text = "toolStripStatusLabel";
            // 
            // pnlMainMenuPadding
            // 
            this.pnlMainMenuPadding.BackColor = System.Drawing.SystemColors.Window;
            this.pnlMainMenuPadding.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlMainMenuPadding.Controls.Add(this.lvMainMenu);
            this.pnlMainMenuPadding.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMainMenuPadding.Location = new System.Drawing.Point(0, 24);
            this.pnlMainMenuPadding.Name = "pnlMainMenuPadding";
            this.pnlMainMenuPadding.Padding = new System.Windows.Forms.Padding(0, 20, 0, 20);
            this.pnlMainMenuPadding.Size = new System.Drawing.Size(114, 569);
            this.pnlMainMenuPadding.TabIndex = 5;
            // 
            // lvMainMenu
            // 
            this.lvMainMenu.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvMainMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvMainMenu.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5});
            this.lvMainMenu.LargeImageList = this.ilMainMenu;
            this.lvMainMenu.Location = new System.Drawing.Point(0, 20);
            this.lvMainMenu.MultiSelect = false;
            this.lvMainMenu.Name = "lvMainMenu";
            this.lvMainMenu.Size = new System.Drawing.Size(110, 525);
            this.lvMainMenu.TabIndex = 1;
            this.lvMainMenu.UseCompatibleStateImageBehavior = false;
            this.lvMainMenu.SelectedIndexChanged += new System.EventHandler(this.lvMainMenu_SelectedIndexChanged);
            // 
            // splMainMenu
            // 
            this.splMainMenu.Location = new System.Drawing.Point(114, 24);
            this.splMainMenu.Name = "splMainMenu";
            this.splMainMenu.Size = new System.Drawing.Size(3, 569);
            this.splMainMenu.TabIndex = 6;
            this.splMainMenu.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // tcMainAppArea
            // 
            this.tcMainAppArea.Controls.Add(this.tpProducts);
            this.tcMainAppArea.Controls.Add(this.tpSales);
            this.tcMainAppArea.Controls.Add(this.tpOrders);
            this.tcMainAppArea.Controls.Add(this.tpDatabase);
            this.tcMainAppArea.Controls.Add(this.tpReports);
            this.tcMainAppArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMainAppArea.Location = new System.Drawing.Point(117, 24);
            this.tcMainAppArea.Name = "tcMainAppArea";
            this.tcMainAppArea.SelectedIndex = 0;
            this.tcMainAppArea.Size = new System.Drawing.Size(680, 569);
            this.tcMainAppArea.TabIndex = 7;
            // 
            // tpProducts
            // 
            this.tpProducts.Controls.Add(this.groupBox1);
            this.tpProducts.Controls.Add(this.btnSaveAs);
            this.tpProducts.Controls.Add(this.gbStatus);
            this.tpProducts.Controls.Add(this.label7);
            this.tpProducts.Controls.Add(this.gbCustom);
            this.tpProducts.Controls.Add(this.gbImageType);
            this.tpProducts.Controls.Add(this.pbJerseyImage);
            this.tpProducts.Location = new System.Drawing.Point(4, 23);
            this.tpProducts.Name = "tpProducts";
            this.tpProducts.Padding = new System.Windows.Forms.Padding(3);
            this.tpProducts.Size = new System.Drawing.Size(672, 542);
            this.tpProducts.TabIndex = 0;
            this.tpProducts.Text = "tpProducts";
            this.tpProducts.UseVisualStyleBackColor = true;
            this.tpProducts.Enter += new System.EventHandler(this.tpProducts_Enter);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btntpProductsAddToTeam);
            this.groupBox1.Controls.Add(this.txtLName);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.txtMI);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.txtFName);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.tpProductsTeamSelector);
            this.groupBox1.Location = new System.Drawing.Point(19, 314);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(298, 202);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add Player To Team";
            // 
            // btntpProductsAddToTeam
            // 
            this.btntpProductsAddToTeam.Enabled = false;
            this.btntpProductsAddToTeam.Location = new System.Drawing.Point(203, 166);
            this.btntpProductsAddToTeam.Name = "btntpProductsAddToTeam";
            this.btntpProductsAddToTeam.Size = new System.Drawing.Size(78, 23);
            this.btntpProductsAddToTeam.TabIndex = 7;
            this.btntpProductsAddToTeam.Text = "Add";
            this.btntpProductsAddToTeam.UseVisualStyleBackColor = true;
            this.btntpProductsAddToTeam.Click += new System.EventHandler(this.btntpProductsAddToTeam_Click);
            // 
            // txtLName
            // 
            this.txtLName.Location = new System.Drawing.Point(84, 140);
            this.txtLName.Name = "txtLName";
            this.txtLName.Size = new System.Drawing.Size(197, 20);
            this.txtLName.TabIndex = 6;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(12, 143);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(58, 13);
            this.label16.TabIndex = 5;
            this.label16.Text = "Last Name";
            // 
            // txtMI
            // 
            this.txtMI.Location = new System.Drawing.Point(229, 112);
            this.txtMI.MaxLength = 1;
            this.txtMI.Name = "txtMI";
            this.txtMI.Size = new System.Drawing.Size(34, 20);
            this.txtMI.TabIndex = 4;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(203, 115);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(19, 13);
            this.label15.TabIndex = 3;
            this.label15.Text = "MI";
            // 
            // txtFName
            // 
            this.txtFName.Location = new System.Drawing.Point(84, 112);
            this.txtFName.MaxLength = 40;
            this.txtFName.Name = "txtFName";
            this.txtFName.Size = new System.Drawing.Size(100, 20);
            this.txtFName.TabIndex = 2;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(9, 115);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(57, 13);
            this.label14.TabIndex = 1;
            this.label14.Text = "First Name";
            // 
            // tpProductsTeamSelector
            // 
            this.tpProductsTeamSelector.Location = new System.Drawing.Point(2, 13);
            this.tpProductsTeamSelector.Name = "tpProductsTeamSelector";
            this.tpProductsTeamSelector.Size = new System.Drawing.Size(290, 99);
            this.tpProductsTeamSelector.TabIndex = 0;
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Location = new System.Drawing.Point(536, 371);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(102, 23);
            this.btnSaveAs.TabIndex = 22;
            this.btnSaveAs.Text = "Save As...";
            this.btnSaveAs.UseVisualStyleBackColor = true;
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // gbStatus
            // 
            this.gbStatus.Controls.Add(this.txtCutFileName);
            this.gbStatus.Controls.Add(this.label2);
            this.gbStatus.Controls.Add(this.btnCreateCST);
            this.gbStatus.Controls.Add(this.lblCSTExists);
            this.gbStatus.Controls.Add(this.label12);
            this.gbStatus.Location = new System.Drawing.Point(19, 216);
            this.gbStatus.Name = "gbStatus";
            this.gbStatus.Size = new System.Drawing.Size(298, 92);
            this.gbStatus.TabIndex = 11;
            this.gbStatus.TabStop = false;
            this.gbStatus.Text = "Status";
            // 
            // txtCutFileName
            // 
            this.txtCutFileName.Location = new System.Drawing.Point(111, 58);
            this.txtCutFileName.Name = "txtCutFileName";
            this.txtCutFileName.Size = new System.Drawing.Size(169, 20);
            this.txtCutFileName.TabIndex = 16;
            this.txtCutFileName.Text = "txtCutFileName";
            this.txtCutFileName.Enter += new System.EventHandler(this.txtCutFileName_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Save CST File As:";
            // 
            // btnCreateCST
            // 
            this.btnCreateCST.Location = new System.Drawing.Point(174, 22);
            this.btnCreateCST.Name = "btnCreateCST";
            this.btnCreateCST.Size = new System.Drawing.Size(106, 26);
            this.btnCreateCST.TabIndex = 14;
            this.btnCreateCST.Text = "Go Cutting";
            this.btnCreateCST.UseVisualStyleBackColor = true;
            this.btnCreateCST.Click += new System.EventHandler(this.btnCreateCST_Click);
            // 
            // lblCSTExists
            // 
            this.lblCSTExists.AutoSize = true;
            this.lblCSTExists.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCSTExists.Location = new System.Drawing.Point(91, 29);
            this.lblCSTExists.Name = "lblCSTExists";
            this.lblCSTExists.Size = new System.Drawing.Size(77, 13);
            this.lblCSTExists.TabIndex = 13;
            this.lblCSTExists.Text = "lblCSTExists";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 29);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 13);
            this.label12.TabIndex = 12;
            this.label12.Text = "Cut File Exists";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(337, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Preview";
            // 
            // gbCustom
            // 
            this.gbCustom.Controls.Add(this.lblPasteStatus);
            this.gbCustom.Controls.Add(this.label17);
            this.gbCustom.Controls.Add(this.chkLeadingZero);
            this.gbCustom.Controls.Add(this.txtEbayPaste);
            this.gbCustom.Controls.Add(this.label1);
            this.gbCustom.Controls.Add(this.btnUppercaseDisplayName);
            this.gbCustom.Controls.Add(this.cmbTemplate);
            this.gbCustom.Controls.Add(this.label11);
            this.gbCustom.Controls.Add(this.txtNumber);
            this.gbCustom.Controls.Add(this.lblNumber);
            this.gbCustom.Controls.Add(this.txtFullName);
            this.gbCustom.Controls.Add(this.lblName);
            this.gbCustom.Controls.Add(this.btnTryAgain);
            this.gbCustom.Location = new System.Drawing.Point(19, 9);
            this.gbCustom.Name = "gbCustom";
            this.gbCustom.Size = new System.Drawing.Size(298, 201);
            this.gbCustom.TabIndex = 0;
            this.gbCustom.TabStop = false;
            this.gbCustom.Text = "Enter One-Off";
            // 
            // lblPasteStatus
            // 
            this.lblPasteStatus.AutoSize = true;
            this.lblPasteStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPasteStatus.Location = new System.Drawing.Point(43, 49);
            this.lblPasteStatus.Name = "lblPasteStatus";
            this.lblPasteStatus.Size = new System.Drawing.Size(43, 13);
            this.lblPasteStatus.TabIndex = 13;
            this.lblPasteStatus.Text = "Ready";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(3, 49);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(40, 13);
            this.label17.TabIndex = 12;
            this.label17.Text = "Status:";
            // 
            // chkLeadingZero
            // 
            this.chkLeadingZero.AutoSize = true;
            this.chkLeadingZero.Location = new System.Drawing.Point(157, 110);
            this.chkLeadingZero.Name = "chkLeadingZero";
            this.chkLeadingZero.Size = new System.Drawing.Size(89, 17);
            this.chkLeadingZero.TabIndex = 11;
            this.chkLeadingZero.Text = "Leading Zero";
            this.chkLeadingZero.UseVisualStyleBackColor = true;
            // 
            // txtEbayPaste
            // 
            this.txtEbayPaste.BackColor = System.Drawing.SystemColors.Info;
            this.txtEbayPaste.Location = new System.Drawing.Point(124, 23);
            this.txtEbayPaste.Name = "txtEbayPaste";
            this.txtEbayPaste.Size = new System.Drawing.Size(139, 20);
            this.txtEbayPaste.TabIndex = 10;
            this.txtEbayPaste.Text = "txtEbayPaste";
            this.txtEbayPaste.TextChanged += new System.EventHandler(this.txtEbayPaste_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Paste from eBay Desc";
            // 
            // btnUppercaseDisplayName
            // 
            this.btnUppercaseDisplayName.Location = new System.Drawing.Point(269, 77);
            this.btnUppercaseDisplayName.Name = "btnUppercaseDisplayName";
            this.btnUppercaseDisplayName.Size = new System.Drawing.Size(21, 23);
            this.btnUppercaseDisplayName.TabIndex = 6;
            this.btnUppercaseDisplayName.Text = "^";
            this.btnUppercaseDisplayName.UseVisualStyleBackColor = true;
            this.btnUppercaseDisplayName.Click += new System.EventHandler(this.btnUppercaseDisplayName_Click);
            // 
            // cmbTemplate
            // 
            this.cmbTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTemplate.FormattingEnabled = true;
            this.cmbTemplate.Location = new System.Drawing.Point(89, 137);
            this.cmbTemplate.Name = "cmbTemplate";
            this.cmbTemplate.Size = new System.Drawing.Size(121, 21);
            this.cmbTemplate.TabIndex = 4;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(24, 140);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 13);
            this.label11.TabIndex = 6;
            this.label11.Text = "Template";
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new System.Drawing.Point(89, 107);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(46, 20);
            this.txtNumber.TabIndex = 3;
            // 
            // lblNumber
            // 
            this.lblNumber.AutoSize = true;
            this.lblNumber.Location = new System.Drawing.Point(31, 110);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(44, 13);
            this.lblNumber.TabIndex = 4;
            this.lblNumber.Text = "Number";
            // 
            // txtFullName
            // 
            this.txtFullName.Location = new System.Drawing.Point(89, 78);
            this.txtFullName.MaxLength = 20;
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(174, 20);
            this.txtFullName.TabIndex = 2;
            this.txtFullName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFullName_KeyPress);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(3, 81);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(72, 13);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Display Name";
            // 
            // btnTryAgain
            // 
            this.btnTryAgain.Location = new System.Drawing.Point(203, 167);
            this.btnTryAgain.Name = "btnTryAgain";
            this.btnTryAgain.Size = new System.Drawing.Size(75, 23);
            this.btnTryAgain.TabIndex = 5;
            this.btnTryAgain.Text = "Refresh >>";
            this.btnTryAgain.UseVisualStyleBackColor = true;
            this.btnTryAgain.Click += new System.EventHandler(this.btnTryAgain_Click);
            // 
            // gbImageType
            // 
            this.gbImageType.Controls.Add(this.chkBoundingBox);
            this.gbImageType.Controls.Add(this.rbMarketing);
            this.gbImageType.Controls.Add(this.rbCutting);
            this.gbImageType.Location = new System.Drawing.Point(340, 9);
            this.gbImageType.Name = "gbImageType";
            this.gbImageType.Size = new System.Drawing.Size(298, 43);
            this.gbImageType.TabIndex = 17;
            this.gbImageType.TabStop = false;
            this.gbImageType.Text = "Image Type";
            // 
            // chkBoundingBox
            // 
            this.chkBoundingBox.AutoSize = true;
            this.chkBoundingBox.Location = new System.Drawing.Point(196, 20);
            this.chkBoundingBox.Name = "chkBoundingBox";
            this.chkBoundingBox.Size = new System.Drawing.Size(92, 17);
            this.chkBoundingBox.TabIndex = 20;
            this.chkBoundingBox.Text = "Bounding Box";
            this.chkBoundingBox.UseVisualStyleBackColor = true;
            this.chkBoundingBox.CheckedChanged += new System.EventHandler(this.btnTryAgain_Click);
            // 
            // rbMarketing
            // 
            this.rbMarketing.AutoSize = true;
            this.rbMarketing.Location = new System.Drawing.Point(85, 20);
            this.rbMarketing.Name = "rbMarketing";
            this.rbMarketing.Size = new System.Drawing.Size(72, 17);
            this.rbMarketing.TabIndex = 19;
            this.rbMarketing.Tag = "1";
            this.rbMarketing.Text = "Marketing";
            this.rbMarketing.UseVisualStyleBackColor = true;
            this.rbMarketing.Click += new System.EventHandler(this.btnTryAgain_Click);
            // 
            // rbCutting
            // 
            this.rbCutting.AutoSize = true;
            this.rbCutting.Checked = true;
            this.rbCutting.Location = new System.Drawing.Point(16, 20);
            this.rbCutting.Name = "rbCutting";
            this.rbCutting.Size = new System.Drawing.Size(58, 17);
            this.rbCutting.TabIndex = 18;
            this.rbCutting.TabStop = true;
            this.rbCutting.Tag = "0";
            this.rbCutting.Text = "Cutting";
            this.rbCutting.UseVisualStyleBackColor = true;
            this.rbCutting.Click += new System.EventHandler(this.btnTryAgain_Click);
            // 
            // pbJerseyImage
            // 
            this.pbJerseyImage.BackColor = System.Drawing.Color.Black;
            this.pbJerseyImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbJerseyImage.Location = new System.Drawing.Point(340, 84);
            this.pbJerseyImage.Name = "pbJerseyImage";
            this.pbJerseyImage.Size = new System.Drawing.Size(298, 281);
            this.pbJerseyImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbJerseyImage.TabIndex = 9;
            this.pbJerseyImage.TabStop = false;
            // 
            // tpSales
            // 
            this.tpSales.Controls.Add(this.gbSalesMarketing);
            this.tpSales.Controls.Add(this.btntbSales_createTLCustomsFile);
            this.tpSales.Controls.Add(this.gbSalesEbay);
            this.tpSales.Location = new System.Drawing.Point(4, 23);
            this.tpSales.Name = "tpSales";
            this.tpSales.Padding = new System.Windows.Forms.Padding(3);
            this.tpSales.Size = new System.Drawing.Size(672, 542);
            this.tpSales.TabIndex = 1;
            this.tpSales.Text = "tpSales";
            this.tpSales.UseVisualStyleBackColor = true;
            this.tpSales.Enter += new System.EventHandler(this.tpSales_Enter);
            // 
            // gbSalesMarketing
            // 
            this.gbSalesMarketing.Controls.Add(this.tpSales_Mkt_BuildMsgBrd);
            this.gbSalesMarketing.Controls.Add(this.label31);
            this.gbSalesMarketing.Controls.Add(this.label30);
            this.gbSalesMarketing.Controls.Add(this.label29);
            this.gbSalesMarketing.Controls.Add(this.label28);
            this.gbSalesMarketing.Location = new System.Drawing.Point(454, 20);
            this.gbSalesMarketing.Name = "gbSalesMarketing";
            this.gbSalesMarketing.Size = new System.Drawing.Size(200, 307);
            this.gbSalesMarketing.TabIndex = 7;
            this.gbSalesMarketing.TabStop = false;
            this.gbSalesMarketing.Text = "Marketing";
            // 
            // tpSales_Mkt_BuildMsgBrd
            // 
            this.tpSales_Mkt_BuildMsgBrd.Location = new System.Drawing.Point(107, 72);
            this.tpSales_Mkt_BuildMsgBrd.Name = "tpSales_Mkt_BuildMsgBrd";
            this.tpSales_Mkt_BuildMsgBrd.Size = new System.Drawing.Size(75, 23);
            this.tpSales_Mkt_BuildMsgBrd.TabIndex = 4;
            this.tpSales_Mkt_BuildMsgBrd.Text = "Go";
            this.tpSales_Mkt_BuildMsgBrd.UseVisualStyleBackColor = true;
            this.tpSales_Mkt_BuildMsgBrd.Click += new System.EventHandler(this.tpSales_Mkt_BuildMsgBrd_Click);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(104, 42);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(30, 13);
            this.label31.TabIndex = 3;
            this.label31.Text = "NFL";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(7, 43);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(43, 13);
            this.label30.TabIndex = 2;
            this.label30.Text = "League";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(101, 20);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(14, 13);
            this.label29.TabIndex = 1;
            this.label29.Text = "5";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(7, 20);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(87, 13);
            this.label28.TabIndex = 0;
            this.label28.Text = "Number of teams";
            // 
            // btntbSales_createTLCustomsFile
            // 
            this.btntbSales_createTLCustomsFile.Location = new System.Drawing.Point(49, 363);
            this.btntbSales_createTLCustomsFile.Name = "btntbSales_createTLCustomsFile";
            this.btntbSales_createTLCustomsFile.Size = new System.Drawing.Size(199, 23);
            this.btntbSales_createTLCustomsFile.TabIndex = 6;
            this.btntbSales_createTLCustomsFile.Text = "Create TurboLister Customs File";
            this.btntbSales_createTLCustomsFile.UseVisualStyleBackColor = true;
            this.btntbSales_createTLCustomsFile.Click += new System.EventHandler(this.btntbSales_createTLCustomsFile_Click);
            // 
            // gbSalesEbay
            // 
            this.gbSalesEbay.Controls.Add(this.tpSales_chkUploadAll);
            this.gbSalesEbay.Controls.Add(this.tpSales_lblEbayGraphicsStatus);
            this.gbSalesEbay.Controls.Add(this.tpSales_lblEbayToolsStatus);
            this.gbSalesEbay.Controls.Add(this.chktpSales_Generic);
            this.gbSalesEbay.Controls.Add(this.chktpSales_CreateGraphics);
            this.gbSalesEbay.Controls.Add(this.label18);
            this.gbSalesEbay.Controls.Add(this.chkNewPlayersOnly);
            this.gbSalesEbay.Controls.Add(this.btnUploadGraphics);
            this.gbSalesEbay.Controls.Add(this.tsTabSalesTeamSelector);
            this.gbSalesEbay.Controls.Add(this.btnCreateTurbolisterFile);
            this.gbSalesEbay.Location = new System.Drawing.Point(6, 20);
            this.gbSalesEbay.Name = "gbSalesEbay";
            this.gbSalesEbay.Size = new System.Drawing.Size(441, 307);
            this.gbSalesEbay.TabIndex = 0;
            this.gbSalesEbay.TabStop = false;
            this.gbSalesEbay.Text = "Ebay Tools";
            // 
            // tpSales_chkUploadAll
            // 
            this.tpSales_chkUploadAll.Location = new System.Drawing.Point(318, 239);
            this.tpSales_chkUploadAll.Name = "tpSales_chkUploadAll";
            this.tpSales_chkUploadAll.Size = new System.Drawing.Size(104, 31);
            this.tpSales_chkUploadAll.TabIndex = 11;
            this.tpSales_chkUploadAll.Text = "Upload all regardless of DB";
            this.tpSales_chkUploadAll.UseVisualStyleBackColor = true;
            // 
            // tpSales_lblEbayGraphicsStatus
            // 
            this.tpSales_lblEbayGraphicsStatus.Location = new System.Drawing.Point(191, 272);
            this.tpSales_lblEbayGraphicsStatus.Name = "tpSales_lblEbayGraphicsStatus";
            this.tpSales_lblEbayGraphicsStatus.Size = new System.Drawing.Size(244, 27);
            this.tpSales_lblEbayGraphicsStatus.TabIndex = 10;
            this.tpSales_lblEbayGraphicsStatus.Text = "tpSales_lblEbayGraphicsStatus";
            // 
            // tpSales_lblEbayToolsStatus
            // 
            this.tpSales_lblEbayToolsStatus.Location = new System.Drawing.Point(6, 272);
            this.tpSales_lblEbayToolsStatus.Name = "tpSales_lblEbayToolsStatus";
            this.tpSales_lblEbayToolsStatus.Size = new System.Drawing.Size(179, 27);
            this.tpSales_lblEbayToolsStatus.TabIndex = 9;
            this.tpSales_lblEbayToolsStatus.Text = "tpSales_lblEbayToolsStatus";
            // 
            // chktpSales_Generic
            // 
            this.chktpSales_Generic.Enabled = false;
            this.chktpSales_Generic.Location = new System.Drawing.Point(20, 158);
            this.chktpSales_Generic.Name = "chktpSales_Generic";
            this.chktpSales_Generic.Size = new System.Drawing.Size(333, 17);
            this.chktpSales_Generic.TabIndex = 8;
            this.chktpSales_Generic.Text = "Build Generic Last Names and Numbers only";
            this.chktpSales_Generic.UseVisualStyleBackColor = true;
            this.chktpSales_Generic.CheckedChanged += new System.EventHandler(this.chktpSales_Generic_CheckedChanged);
            // 
            // chktpSales_CreateGraphics
            // 
            this.chktpSales_CreateGraphics.AutoSize = true;
            this.chktpSales_CreateGraphics.Checked = true;
            this.chktpSales_CreateGraphics.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chktpSales_CreateGraphics.Location = new System.Drawing.Point(20, 135);
            this.chktpSales_CreateGraphics.Name = "chktpSales_CreateGraphics";
            this.chktpSales_CreateGraphics.Size = new System.Drawing.Size(271, 17);
            this.chktpSales_CreateGraphics.TabIndex = 7;
            this.chktpSales_CreateGraphics.Text = "Create and save marking graphics (500 px x 500 px)";
            this.chktpSales_CreateGraphics.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(6, 212);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(275, 27);
            this.label18.TabIndex = 6;
            this.label18.Text = "Note: leave franchise blank in order to export an entire season.";
            // 
            // chkNewPlayersOnly
            // 
            this.chkNewPlayersOnly.AutoSize = true;
            this.chkNewPlayersOnly.Location = new System.Drawing.Point(20, 112);
            this.chkNewPlayersOnly.Name = "chkNewPlayersOnly";
            this.chkNewPlayersOnly.Size = new System.Drawing.Size(173, 17);
            this.chkNewPlayersOnly.TabIndex = 5;
            this.chkNewPlayersOnly.Text = "Unexported (New) Players Only";
            this.chkNewPlayersOnly.UseVisualStyleBackColor = true;
            // 
            // btnUploadGraphics
            // 
            this.btnUploadGraphics.Location = new System.Drawing.Point(194, 242);
            this.btnUploadGraphics.Name = "btnUploadGraphics";
            this.btnUploadGraphics.Size = new System.Drawing.Size(111, 23);
            this.btnUploadGraphics.TabIndex = 4;
            this.btnUploadGraphics.Text = "Upload Graphics";
            this.btnUploadGraphics.UseVisualStyleBackColor = true;
            this.btnUploadGraphics.Click += new System.EventHandler(this.btnUploadGraphics_Click);
            // 
            // tsTabSalesTeamSelector
            // 
            this.tsTabSalesTeamSelector.Location = new System.Drawing.Point(0, 16);
            this.tsTabSalesTeamSelector.Name = "tsTabSalesTeamSelector";
            this.tsTabSalesTeamSelector.Size = new System.Drawing.Size(290, 99);
            this.tsTabSalesTeamSelector.TabIndex = 3;
            // 
            // btnCreateTurbolisterFile
            // 
            this.btnCreateTurbolisterFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateTurbolisterFile.Location = new System.Drawing.Point(6, 242);
            this.btnCreateTurbolisterFile.Name = "btnCreateTurbolisterFile";
            this.btnCreateTurbolisterFile.Size = new System.Drawing.Size(148, 23);
            this.btnCreateTurbolisterFile.TabIndex = 2;
            this.btnCreateTurbolisterFile.Text = "Create TurboLister File";
            this.btnCreateTurbolisterFile.UseVisualStyleBackColor = true;
            this.btnCreateTurbolisterFile.Click += new System.EventHandler(this.btnCreateTurbolisterFile_Click);
            // 
            // tpOrders
            // 
            this.tpOrders.Controls.Add(this.tpOrders_btnArchiveMonsterFile);
            this.tpOrders.Controls.Add(this.gbOrdersDetail);
            this.tpOrders.Controls.Add(this.btn_orders_ProcessMonsterFile);
            this.tpOrders.Controls.Add(this.gbOrdersStatus);
            this.tpOrders.Controls.Add(this.btnOrders_CreatePaypalShippingFile);
            this.tpOrders.Location = new System.Drawing.Point(4, 23);
            this.tpOrders.Name = "tpOrders";
            this.tpOrders.Padding = new System.Windows.Forms.Padding(3);
            this.tpOrders.Size = new System.Drawing.Size(672, 542);
            this.tpOrders.TabIndex = 2;
            this.tpOrders.Text = "tpOrders";
            this.tpOrders.UseVisualStyleBackColor = true;
            // 
            // tpOrders_btnArchiveMonsterFile
            // 
            this.tpOrders_btnArchiveMonsterFile.Location = new System.Drawing.Point(345, 6);
            this.tpOrders_btnArchiveMonsterFile.Name = "tpOrders_btnArchiveMonsterFile";
            this.tpOrders_btnArchiveMonsterFile.Size = new System.Drawing.Size(126, 23);
            this.tpOrders_btnArchiveMonsterFile.TabIndex = 10;
            this.tpOrders_btnArchiveMonsterFile.Text = "Archive Monster File";
            this.tpOrders_btnArchiveMonsterFile.UseVisualStyleBackColor = true;
            this.tpOrders_btnArchiveMonsterFile.Click += new System.EventHandler(this.tpOrders_btnArchiveMonsterFile_Click);
            // 
            // gbOrdersDetail
            // 
            this.gbOrdersDetail.Controls.Add(this.tpOrders_lblColor);
            this.gbOrdersDetail.Controls.Add(this.label32);
            this.gbOrdersDetail.Controls.Add(this.label19);
            this.gbOrdersDetail.Controls.Add(this.tpOrders_lblQty);
            this.gbOrdersDetail.Controls.Add(this.label8);
            this.gbOrdersDetail.Controls.Add(this.tpOrders_pbJerseyImage);
            this.gbOrdersDetail.Controls.Add(this.tpOrders_lblJerseyNumber);
            this.gbOrdersDetail.Controls.Add(this.tpOrders_lblJerseyName);
            this.gbOrdersDetail.Controls.Add(this.label6);
            this.gbOrdersDetail.Controls.Add(this.label5);
            this.gbOrdersDetail.Controls.Add(this.tpOrders_lblTemplateDesc);
            this.gbOrdersDetail.Controls.Add(this.label4);
            this.gbOrdersDetail.Controls.Add(this.tpOrders_lblCustName);
            this.gbOrdersDetail.Controls.Add(this.button8);
            this.gbOrdersDetail.Controls.Add(this.tpOrders_GoCutting);
            this.gbOrdersDetail.Controls.Add(this.label3);
            this.gbOrdersDetail.Location = new System.Drawing.Point(3, 379);
            this.gbOrdersDetail.Name = "gbOrdersDetail";
            this.gbOrdersDetail.Size = new System.Drawing.Size(661, 157);
            this.gbOrdersDetail.TabIndex = 1;
            this.gbOrdersDetail.TabStop = false;
            this.gbOrdersDetail.Text = "Order Detail";
            // 
            // tpOrders_lblColor
            // 
            this.tpOrders_lblColor.BackColor = System.Drawing.Color.RosyBrown;
            this.tpOrders_lblColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tpOrders_lblColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpOrders_lblColor.Location = new System.Drawing.Point(352, 19);
            this.tpOrders_lblColor.Name = "tpOrders_lblColor";
            this.tpOrders_lblColor.Size = new System.Drawing.Size(152, 47);
            this.tpOrders_lblColor.TabIndex = 23;
            this.tpOrders_lblColor.Text = "tpOrders_lblColor";
            this.tpOrders_lblColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(315, 36);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(31, 13);
            this.label32.TabIndex = 22;
            this.label32.Text = "Color";
            this.label32.Click += new System.EventHandler(this.label32_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(492, 105);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(12, 13);
            this.label19.TabIndex = 21;
            this.label19.Text = "x";
            this.label19.Click += new System.EventHandler(this.label19_Click);
            // 
            // tpOrders_lblQty
            // 
            this.tpOrders_lblQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpOrders_lblQty.ForeColor = System.Drawing.Color.Red;
            this.tpOrders_lblQty.Location = new System.Drawing.Point(420, 88);
            this.tpOrders_lblQty.Name = "tpOrders_lblQty";
            this.tpOrders_lblQty.Size = new System.Drawing.Size(66, 46);
            this.tpOrders_lblQty.TabIndex = 20;
            this.tpOrders_lblQty.Text = "tpOrders_lblQty";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(389, 105);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Qty";
            // 
            // tpOrders_pbJerseyImage
            // 
            this.tpOrders_pbJerseyImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tpOrders_pbJerseyImage.Location = new System.Drawing.Point(514, 19);
            this.tpOrders_pbJerseyImage.Name = "tpOrders_pbJerseyImage";
            this.tpOrders_pbJerseyImage.Size = new System.Drawing.Size(128, 126);
            this.tpOrders_pbJerseyImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.tpOrders_pbJerseyImage.TabIndex = 18;
            this.tpOrders_pbJerseyImage.TabStop = false;
            this.tpOrders_pbJerseyImage.Click += new System.EventHandler(this.tpOrders_pbJerseyImage_Click);
            // 
            // tpOrders_lblJerseyNumber
            // 
            this.tpOrders_lblJerseyNumber.AutoSize = true;
            this.tpOrders_lblJerseyNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpOrders_lblJerseyNumber.Location = new System.Drawing.Point(100, 76);
            this.tpOrders_lblJerseyNumber.Name = "tpOrders_lblJerseyNumber";
            this.tpOrders_lblJerseyNumber.Size = new System.Drawing.Size(154, 13);
            this.tpOrders_lblJerseyNumber.TabIndex = 17;
            this.tpOrders_lblJerseyNumber.Text = "tpOrders_lblJerseyNumber";
            this.tpOrders_lblJerseyNumber.Click += new System.EventHandler(this.label9_Click);
            // 
            // tpOrders_lblJerseyName
            // 
            this.tpOrders_lblJerseyName.AutoSize = true;
            this.tpOrders_lblJerseyName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpOrders_lblJerseyName.Location = new System.Drawing.Point(100, 59);
            this.tpOrders_lblJerseyName.Name = "tpOrders_lblJerseyName";
            this.tpOrders_lblJerseyName.Size = new System.Drawing.Size(143, 13);
            this.tpOrders_lblJerseyName.TabIndex = 16;
            this.tpOrders_lblJerseyName.Text = "tpOrders_lblJerseyName";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Jersey Number";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Jersey Name";
            // 
            // tpOrders_lblTemplateDesc
            // 
            this.tpOrders_lblTemplateDesc.AutoSize = true;
            this.tpOrders_lblTemplateDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpOrders_lblTemplateDesc.Location = new System.Drawing.Point(100, 42);
            this.tpOrders_lblTemplateDesc.Name = "tpOrders_lblTemplateDesc";
            this.tpOrders_lblTemplateDesc.Size = new System.Drawing.Size(156, 13);
            this.tpOrders_lblTemplateDesc.TabIndex = 13;
            this.tpOrders_lblTemplateDesc.Text = "tpOrders_lblTemplateDesc";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Jersey Template";
            // 
            // tpOrders_lblCustName
            // 
            this.tpOrders_lblCustName.AutoSize = true;
            this.tpOrders_lblCustName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tpOrders_lblCustName.Location = new System.Drawing.Point(100, 25);
            this.tpOrders_lblCustName.Name = "tpOrders_lblCustName";
            this.tpOrders_lblCustName.Size = new System.Drawing.Size(132, 13);
            this.tpOrders_lblCustName.TabIndex = 11;
            this.tpOrders_lblCustName.Text = "tpOrders_lblCustName";
            // 
            // button8
            // 
            this.button8.Enabled = false;
            this.button8.Location = new System.Drawing.Point(132, 124);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(111, 23);
            this.button8.TabIndex = 10;
            this.button8.Text = "Mark as Shipped!";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // tpOrders_GoCutting
            // 
            this.tpOrders_GoCutting.Location = new System.Drawing.Point(6, 124);
            this.tpOrders_GoCutting.Name = "tpOrders_GoCutting";
            this.tpOrders_GoCutting.Size = new System.Drawing.Size(117, 23);
            this.tpOrders_GoCutting.TabIndex = 8;
            this.tpOrders_GoCutting.Text = "Open in Cutstudio...";
            this.tpOrders_GoCutting.UseVisualStyleBackColor = true;
            this.tpOrders_GoCutting.Click += new System.EventHandler(this.tpOrders_GoCutting_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Cust Name";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // btn_orders_ProcessMonsterFile
            // 
            this.btn_orders_ProcessMonsterFile.Location = new System.Drawing.Point(2, 6);
            this.btn_orders_ProcessMonsterFile.Name = "btn_orders_ProcessMonsterFile";
            this.btn_orders_ProcessMonsterFile.Size = new System.Drawing.Size(163, 23);
            this.btn_orders_ProcessMonsterFile.TabIndex = 2;
            this.btn_orders_ProcessMonsterFile.Text = "Process Monster Order File";
            this.btn_orders_ProcessMonsterFile.UseVisualStyleBackColor = true;
            this.btn_orders_ProcessMonsterFile.Click += new System.EventHandler(this.btn_orders_ProcessMonsterFile_Click);
            // 
            // gbOrdersStatus
            // 
            this.gbOrdersStatus.Controls.Add(this.radioButton5);
            this.gbOrdersStatus.Controls.Add(this.radioButton4);
            this.gbOrdersStatus.Controls.Add(this.tpOrders_orderGrid);
            this.gbOrdersStatus.Controls.Add(this.radioButton3);
            this.gbOrdersStatus.Controls.Add(this.radioButton1);
            this.gbOrdersStatus.Location = new System.Drawing.Point(6, 35);
            this.gbOrdersStatus.Name = "gbOrdersStatus";
            this.gbOrdersStatus.Size = new System.Drawing.Size(658, 338);
            this.gbOrdersStatus.TabIndex = 0;
            this.gbOrdersStatus.TabStop = false;
            this.gbOrdersStatus.Text = "Order List";
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Enabled = false;
            this.radioButton5.Location = new System.Drawing.Point(181, 20);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(124, 17);
            this.radioButton5.TabIndex = 5;
            this.radioButton5.TabStop = true;
            this.radioButton5.Text = "Received - Complete";
            this.radioButton5.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Enabled = false;
            this.radioButton4.Location = new System.Drawing.Point(95, 20);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(64, 17);
            this.radioButton4.TabIndex = 4;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "Shipped";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // tpOrders_orderGrid
            // 
            this.tpOrders_orderGrid.AllowUserToAddRows = false;
            this.tpOrders_orderGrid.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tpOrders_orderGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.tpOrders_orderGrid.AutoGenerateColumns = false;
            this.tpOrders_orderGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tpOrders_orderGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.orderIdDataGridViewTextBoxColumn,
            this.ShippingFirstName,
            this.ShippingLastName,
            this.ShippingCity,
            this.ShippingState,
            this.ShippingZip,
            this.ShippingCountry,
            this.NumberOfItems,
            this.FirstItemDescriptionExternal,
            this.FirstItemTemplateId,
            this.FirstItemTemplateDescriptionShort,
            this.FirstItemQuantity,
            this.FirstItemJerseyName,
            this.FirstItemJerseyNumber,
            this.FirstItemColor});
            this.tpOrders_orderGrid.DataSource = this.orderDisplayableBindingSource;
            this.tpOrders_orderGrid.Location = new System.Drawing.Point(6, 43);
            this.tpOrders_orderGrid.MultiSelect = false;
            this.tpOrders_orderGrid.Name = "tpOrders_orderGrid";
            this.tpOrders_orderGrid.ReadOnly = true;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Transparent;
            this.tpOrders_orderGrid.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.tpOrders_orderGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tpOrders_orderGrid.Size = new System.Drawing.Size(646, 289);
            this.tpOrders_orderGrid.TabIndex = 3;
            this.tpOrders_orderGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tpOrders_orderGrid_CellContentClick);
            this.tpOrders_orderGrid.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.tpOrders_orderGrid_DataBindingComplete);
            this.tpOrders_orderGrid.SelectionChanged += new System.EventHandler(this.tpOrders_orderGrid_SelectionChanged);
            // 
            // orderIdDataGridViewTextBoxColumn
            // 
            this.orderIdDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.orderIdDataGridViewTextBoxColumn.DataPropertyName = "OrderId";
            this.orderIdDataGridViewTextBoxColumn.HeaderText = "OrderId";
            this.orderIdDataGridViewTextBoxColumn.Name = "orderIdDataGridViewTextBoxColumn";
            this.orderIdDataGridViewTextBoxColumn.ReadOnly = true;
            this.orderIdDataGridViewTextBoxColumn.Width = 67;
            // 
            // ShippingFirstName
            // 
            this.ShippingFirstName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ShippingFirstName.DataPropertyName = "ShippingFirstName";
            this.ShippingFirstName.HeaderText = "First Name";
            this.ShippingFirstName.Name = "ShippingFirstName";
            this.ShippingFirstName.ReadOnly = true;
            this.ShippingFirstName.Width = 76;
            // 
            // ShippingLastName
            // 
            this.ShippingLastName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ShippingLastName.DataPropertyName = "ShippingLastName";
            this.ShippingLastName.HeaderText = "Last Name";
            this.ShippingLastName.Name = "ShippingLastName";
            this.ShippingLastName.ReadOnly = true;
            this.ShippingLastName.Width = 77;
            // 
            // ShippingCity
            // 
            this.ShippingCity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ShippingCity.DataPropertyName = "ShippingCity";
            this.ShippingCity.HeaderText = "City";
            this.ShippingCity.Name = "ShippingCity";
            this.ShippingCity.ReadOnly = true;
            this.ShippingCity.Width = 49;
            // 
            // ShippingState
            // 
            this.ShippingState.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ShippingState.DataPropertyName = "ShippingState";
            this.ShippingState.HeaderText = "State";
            this.ShippingState.Name = "ShippingState";
            this.ShippingState.ReadOnly = true;
            this.ShippingState.Width = 57;
            // 
            // ShippingZip
            // 
            this.ShippingZip.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ShippingZip.DataPropertyName = "ShippingZip";
            this.ShippingZip.HeaderText = "Zip";
            this.ShippingZip.Name = "ShippingZip";
            this.ShippingZip.ReadOnly = true;
            this.ShippingZip.Width = 47;
            // 
            // ShippingCountry
            // 
            this.ShippingCountry.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ShippingCountry.DataPropertyName = "ShippingCountry";
            this.ShippingCountry.HeaderText = "Country";
            this.ShippingCountry.Name = "ShippingCountry";
            this.ShippingCountry.ReadOnly = true;
            this.ShippingCountry.Width = 68;
            // 
            // NumberOfItems
            // 
            this.NumberOfItems.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.NumberOfItems.DataPropertyName = "NumberOfItems";
            this.NumberOfItems.HeaderText = "No. Items";
            this.NumberOfItems.Name = "NumberOfItems";
            this.NumberOfItems.ReadOnly = true;
            this.NumberOfItems.Width = 71;
            // 
            // FirstItemDescriptionExternal
            // 
            this.FirstItemDescriptionExternal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.FirstItemDescriptionExternal.DataPropertyName = "ItemDescriptionExternal";
            this.FirstItemDescriptionExternal.HeaderText = "Description External";
            this.FirstItemDescriptionExternal.Name = "FirstItemDescriptionExternal";
            this.FirstItemDescriptionExternal.ReadOnly = true;
            this.FirstItemDescriptionExternal.Width = 115;
            // 
            // FirstItemTemplateId
            // 
            this.FirstItemTemplateId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.FirstItemTemplateId.DataPropertyName = "ItemTemplateId";
            this.FirstItemTemplateId.HeaderText = "TemplateId";
            this.FirstItemTemplateId.Name = "FirstItemTemplateId";
            this.FirstItemTemplateId.ReadOnly = true;
            this.FirstItemTemplateId.Width = 85;
            // 
            // FirstItemTemplateDescriptionShort
            // 
            this.FirstItemTemplateDescriptionShort.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.FirstItemTemplateDescriptionShort.DataPropertyName = "ItemTemplateDescriptionShort";
            this.FirstItemTemplateDescriptionShort.HeaderText = "Template Desc Short";
            this.FirstItemTemplateDescriptionShort.Name = "FirstItemTemplateDescriptionShort";
            this.FirstItemTemplateDescriptionShort.ReadOnly = true;
            this.FirstItemTemplateDescriptionShort.Width = 98;
            // 
            // FirstItemQuantity
            // 
            this.FirstItemQuantity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.FirstItemQuantity.DataPropertyName = "ItemQuantity";
            this.FirstItemQuantity.HeaderText = "Qty";
            this.FirstItemQuantity.Name = "FirstItemQuantity";
            this.FirstItemQuantity.ReadOnly = true;
            this.FirstItemQuantity.Width = 48;
            // 
            // FirstItemJerseyName
            // 
            this.FirstItemJerseyName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.FirstItemJerseyName.DataPropertyName = "ItemJerseyName";
            this.FirstItemJerseyName.HeaderText = "Jersey Name";
            this.FirstItemJerseyName.Name = "FirstItemJerseyName";
            this.FirstItemJerseyName.ReadOnly = true;
            this.FirstItemJerseyName.Width = 86;
            // 
            // FirstItemJerseyNumber
            // 
            this.FirstItemJerseyNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.FirstItemJerseyNumber.DataPropertyName = "ItemJerseyNumber";
            this.FirstItemJerseyNumber.HeaderText = "Jersey Number";
            this.FirstItemJerseyNumber.Name = "FirstItemJerseyNumber";
            this.FirstItemJerseyNumber.ReadOnly = true;
            this.FirstItemJerseyNumber.Width = 94;
            // 
            // FirstItemColor
            // 
            this.FirstItemColor.DataPropertyName = "ItemColor";
            this.FirstItemColor.HeaderText = "Color";
            this.FirstItemColor.Name = "FirstItemColor";
            this.FirstItemColor.ReadOnly = true;
            // 
            // orderDisplayableBindingSource
            // 
            this.orderDisplayableBindingSource.DataSource = typeof(Ffd.Data.OrderDisplayable);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Enabled = false;
            this.radioButton3.Location = new System.Drawing.Point(473, 20);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(36, 17);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "All";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Enabled = false;
            this.radioButton1.Location = new System.Drawing.Point(17, 20);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(47, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "New";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // btnOrders_CreatePaypalShippingFile
            // 
            this.btnOrders_CreatePaypalShippingFile.Location = new System.Drawing.Point(183, 6);
            this.btnOrders_CreatePaypalShippingFile.Name = "btnOrders_CreatePaypalShippingFile";
            this.btnOrders_CreatePaypalShippingFile.Size = new System.Drawing.Size(146, 23);
            this.btnOrders_CreatePaypalShippingFile.TabIndex = 9;
            this.btnOrders_CreatePaypalShippingFile.Text = "Create Shipping Label File For Paypal";
            this.btnOrders_CreatePaypalShippingFile.UseVisualStyleBackColor = true;
            this.btnOrders_CreatePaypalShippingFile.Click += new System.EventHandler(this.btnOrders_CreatePaypalShippingFile_Click);
            // 
            // tpDatabase
            // 
            this.tpDatabase.Controls.Add(this.groupBox4);
            this.tpDatabase.Controls.Add(this.groupBox3);
            this.tpDatabase.Controls.Add(this.groupBox2);
            this.tpDatabase.Controls.Add(this.gbImportData);
            this.tpDatabase.Location = new System.Drawing.Point(4, 23);
            this.tpDatabase.Name = "tpDatabase";
            this.tpDatabase.Padding = new System.Windows.Forms.Padding(3);
            this.tpDatabase.Size = new System.Drawing.Size(672, 542);
            this.tpDatabase.TabIndex = 3;
            this.tpDatabase.Text = "tpDatabase";
            this.tpDatabase.UseVisualStyleBackColor = true;
            this.tpDatabase.Enter += new System.EventHandler(this.tpDatabase_Enter);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tpDB_btnImportBadAddrs);
            this.groupBox4.Controls.Add(this.tpDB_btnRefresh);
            this.groupBox4.Controls.Add(this.tpDB_lstBadAddresses);
            this.groupBox4.Controls.Add(this.tpDB_btnFixAddr);
            this.groupBox4.Location = new System.Drawing.Point(309, 308);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(354, 216);
            this.groupBox4.TabIndex = 27;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Bad Addresses (for Lead Source above)";
            // 
            // tpDB_btnImportBadAddrs
            // 
            this.tpDB_btnImportBadAddrs.Location = new System.Drawing.Point(123, 168);
            this.tpDB_btnImportBadAddrs.Name = "tpDB_btnImportBadAddrs";
            this.tpDB_btnImportBadAddrs.Size = new System.Drawing.Size(122, 23);
            this.tpDB_btnImportBadAddrs.TabIndex = 21;
            this.tpDB_btnImportBadAddrs.Text = "Import Bad Addrs...";
            this.tpDB_btnImportBadAddrs.UseVisualStyleBackColor = true;
            this.tpDB_btnImportBadAddrs.Click += new System.EventHandler(this.tpDB_btnImportBadAddrs_Click);
            // 
            // tpDB_btnRefresh
            // 
            this.tpDB_btnRefresh.Location = new System.Drawing.Point(6, 170);
            this.tpDB_btnRefresh.Name = "tpDB_btnRefresh";
            this.tpDB_btnRefresh.Size = new System.Drawing.Size(89, 21);
            this.tpDB_btnRefresh.TabIndex = 20;
            this.tpDB_btnRefresh.Text = "Refresh";
            this.tpDB_btnRefresh.UseVisualStyleBackColor = true;
            this.tpDB_btnRefresh.Click += new System.EventHandler(this.tpDB_RefreshBadAddresses);
            // 
            // tpDB_lstBadAddresses
            // 
            this.tpDB_lstBadAddresses.FormattingEnabled = true;
            this.tpDB_lstBadAddresses.Location = new System.Drawing.Point(7, 20);
            this.tpDB_lstBadAddresses.Name = "tpDB_lstBadAddresses";
            this.tpDB_lstBadAddresses.Size = new System.Drawing.Size(337, 134);
            this.tpDB_lstBadAddresses.TabIndex = 19;
            this.tpDB_lstBadAddresses.SelectedIndexChanged += new System.EventHandler(this.tpDB_lstBadAddresses_SelectedIndexChanged);
            this.tpDB_lstBadAddresses.DoubleClick += new System.EventHandler(this.tpDB_lstBadAddresses_DoubleClick);
            // 
            // tpDB_btnFixAddr
            // 
            this.tpDB_btnFixAddr.Location = new System.Drawing.Point(251, 168);
            this.tpDB_btnFixAddr.Name = "tpDB_btnFixAddr";
            this.tpDB_btnFixAddr.Size = new System.Drawing.Size(93, 23);
            this.tpDB_btnFixAddr.TabIndex = 18;
            this.tpDB_btnFixAddr.Text = "Fix Address";
            this.tpDB_btnFixAddr.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tpDB_btnMailed);
            this.groupBox3.Controls.Add(this.tpDB_chkMustBeAthletics);
            this.groupBox3.Controls.Add(this.tpDB_lblStatus);
            this.groupBox3.Controls.Add(this.label27);
            this.groupBox3.Controls.Add(this.tpDB_txtSkipEvery);
            this.groupBox3.Controls.Add(this.label26);
            this.groupBox3.Controls.Add(this.tpDB_txtMinEnrollment);
            this.groupBox3.Controls.Add(this.label24);
            this.groupBox3.Controls.Add(this.tpDB_txtOutputFName);
            this.groupBox3.Controls.Add(this.label25);
            this.groupBox3.Controls.Add(this.tpDB_txtQtyToGet);
            this.groupBox3.Controls.Add(this.tpDB_lblSupplyQty);
            this.groupBox3.Controls.Add(this.tpDB_cmbLeadStatus);
            this.groupBox3.Controls.Add(this.tpDB_cmbLeadSource);
            this.groupBox3.Controls.Add(this.btnDB_GoLeadOutputFile);
            this.groupBox3.Controls.Add(this.label23);
            this.groupBox3.Controls.Add(this.label22);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.label20);
            this.groupBox3.Location = new System.Drawing.Point(309, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(355, 296);
            this.groupBox3.TabIndex = 26;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Lead Output File";
            // 
            // tpDB_btnMailed
            // 
            this.tpDB_btnMailed.Location = new System.Drawing.Point(241, 259);
            this.tpDB_btnMailed.Name = "tpDB_btnMailed";
            this.tpDB_btnMailed.Size = new System.Drawing.Size(108, 23);
            this.tpDB_btnMailed.TabIndex = 23;
            this.tpDB_btnMailed.Text = "All Mailed!";
            this.tpDB_btnMailed.UseVisualStyleBackColor = true;
            this.tpDB_btnMailed.Click += new System.EventHandler(this.tpDB_btnMailed_Click);
            // 
            // tpDB_chkMustBeAthletics
            // 
            this.tpDB_chkMustBeAthletics.AutoSize = true;
            this.tpDB_chkMustBeAthletics.Checked = true;
            this.tpDB_chkMustBeAthletics.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tpDB_chkMustBeAthletics.Location = new System.Drawing.Point(241, 78);
            this.tpDB_chkMustBeAthletics.Name = "tpDB_chkMustBeAthletics";
            this.tpDB_chkMustBeAthletics.Size = new System.Drawing.Size(108, 17);
            this.tpDB_chkMustBeAthletics.TabIndex = 22;
            this.tpDB_chkMustBeAthletics.Text = "Must Be Athletics";
            this.tpDB_chkMustBeAthletics.UseVisualStyleBackColor = true;
            this.tpDB_chkMustBeAthletics.CheckedChanged += new System.EventHandler(this.LeadOutputFileRefreshSupply);
            // 
            // tpDB_lblStatus
            // 
            this.tpDB_lblStatus.Location = new System.Drawing.Point(13, 234);
            this.tpDB_lblStatus.Name = "tpDB_lblStatus";
            this.tpDB_lblStatus.Size = new System.Drawing.Size(222, 48);
            this.tpDB_lblStatus.TabIndex = 16;
            this.tpDB_lblStatus.Text = "tpDB_lblStatus";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(230, 174);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(91, 13);
            this.label27.TabIndex = 15;
            this.label27.Text = "(blank for no skip)";
            // 
            // tpDB_txtSkipEvery
            // 
            this.tpDB_txtSkipEvery.Location = new System.Drawing.Point(124, 171);
            this.tpDB_txtSkipEvery.Name = "tpDB_txtSkipEvery";
            this.tpDB_txtSkipEvery.Size = new System.Drawing.Size(100, 20);
            this.tpDB_txtSkipEvery.TabIndex = 14;
            this.tpDB_txtSkipEvery.Text = "tpDB_txtSkipEvery";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(6, 174);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(58, 13);
            this.label26.TabIndex = 13;
            this.label26.Text = "Use every:";
            this.label26.Click += new System.EventHandler(this.label26_Click);
            // 
            // tpDB_txtMinEnrollment
            // 
            this.tpDB_txtMinEnrollment.Location = new System.Drawing.Point(123, 75);
            this.tpDB_txtMinEnrollment.Name = "tpDB_txtMinEnrollment";
            this.tpDB_txtMinEnrollment.Size = new System.Drawing.Size(101, 20);
            this.tpDB_txtMinEnrollment.TabIndex = 12;
            this.tpDB_txtMinEnrollment.Text = "tpDB_txtMinEnrollment";
            this.tpDB_txtMinEnrollment.TextChanged += new System.EventHandler(this.LeadOutputFileRefreshSupply);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(10, 78);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(76, 13);
            this.label24.TabIndex = 11;
            this.label24.Text = "Min Enrollment";
            // 
            // tpDB_txtOutputFName
            // 
            this.tpDB_txtOutputFName.Location = new System.Drawing.Point(124, 200);
            this.tpDB_txtOutputFName.Name = "tpDB_txtOutputFName";
            this.tpDB_txtOutputFName.Size = new System.Drawing.Size(225, 20);
            this.tpDB_txtOutputFName.TabIndex = 10;
            this.tpDB_txtOutputFName.Text = "tpDB_txtOutputFName";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(7, 203);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(89, 13);
            this.label25.TabIndex = 9;
            this.label25.Text = "Output File Name";
            // 
            // tpDB_txtQtyToGet
            // 
            this.tpDB_txtQtyToGet.Location = new System.Drawing.Point(124, 142);
            this.tpDB_txtQtyToGet.Name = "tpDB_txtQtyToGet";
            this.tpDB_txtQtyToGet.Size = new System.Drawing.Size(100, 20);
            this.tpDB_txtQtyToGet.TabIndex = 8;
            this.tpDB_txtQtyToGet.Text = "tpDB_txtQtyToGet";
            this.tpDB_txtQtyToGet.TextChanged += new System.EventHandler(this.SkipEveryLeadRefresh);
            // 
            // tpDB_lblSupplyQty
            // 
            this.tpDB_lblSupplyQty.AutoSize = true;
            this.tpDB_lblSupplyQty.Location = new System.Drawing.Point(121, 117);
            this.tpDB_lblSupplyQty.Name = "tpDB_lblSupplyQty";
            this.tpDB_lblSupplyQty.Size = new System.Drawing.Size(95, 13);
            this.tpDB_lblSupplyQty.TabIndex = 7;
            this.tpDB_lblSupplyQty.Text = "tpDB_lblSupplyQty";
            // 
            // tpDB_cmbLeadStatus
            // 
            this.tpDB_cmbLeadStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tpDB_cmbLeadStatus.FormattingEnabled = true;
            this.tpDB_cmbLeadStatus.Location = new System.Drawing.Point(121, 45);
            this.tpDB_cmbLeadStatus.Name = "tpDB_cmbLeadStatus";
            this.tpDB_cmbLeadStatus.Size = new System.Drawing.Size(162, 21);
            this.tpDB_cmbLeadStatus.TabIndex = 6;
            this.tpDB_cmbLeadStatus.SelectedIndexChanged += new System.EventHandler(this.LeadOutputFileRefreshSupply);
            // 
            // tpDB_cmbLeadSource
            // 
            this.tpDB_cmbLeadSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tpDB_cmbLeadSource.FormattingEnabled = true;
            this.tpDB_cmbLeadSource.Location = new System.Drawing.Point(121, 17);
            this.tpDB_cmbLeadSource.Name = "tpDB_cmbLeadSource";
            this.tpDB_cmbLeadSource.Size = new System.Drawing.Size(162, 21);
            this.tpDB_cmbLeadSource.TabIndex = 5;
            this.tpDB_cmbLeadSource.SelectedIndexChanged += new System.EventHandler(this.LeadOutputFileRefreshSupply);
            // 
            // btnDB_GoLeadOutputFile
            // 
            this.btnDB_GoLeadOutputFile.Location = new System.Drawing.Point(241, 229);
            this.btnDB_GoLeadOutputFile.Name = "btnDB_GoLeadOutputFile";
            this.btnDB_GoLeadOutputFile.Size = new System.Drawing.Size(108, 23);
            this.btnDB_GoLeadOutputFile.TabIndex = 4;
            this.btnDB_GoLeadOutputFile.Text = "Create!";
            this.btnDB_GoLeadOutputFile.UseVisualStyleBackColor = true;
            this.btnDB_GoLeadOutputFile.Click += new System.EventHandler(this.btnDB_GoLeadOutputFile_Click);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(7, 145);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(79, 13);
            this.label23.TabIndex = 3;
            this.label23.Text = "Quantity to get:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(7, 117);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(96, 13);
            this.label22.TabIndex = 2;
            this.label22.Text = "Existing supply qty:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(7, 48);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(64, 13);
            this.label21.TabIndex = 1;
            this.label21.Text = "Lead Status";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(7, 20);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(68, 13);
            this.label20.TabIndex = 0;
            this.label20.Text = "Lead Source";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnDB_GetSchoolsFromSUCWebsite);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtDB_ScreenscrapeStatus);
            this.groupBox2.Controls.Add(this.btnDB_GetSchoolsFromHSCWebsite);
            this.groupBox2.Location = new System.Drawing.Point(7, 206);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(296, 318);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Lead Screen Scraping";
            // 
            // btnDB_GetSchoolsFromSUCWebsite
            // 
            this.btnDB_GetSchoolsFromSUCWebsite.Location = new System.Drawing.Point(154, 280);
            this.btnDB_GetSchoolsFromSUCWebsite.Name = "btnDB_GetSchoolsFromSUCWebsite";
            this.btnDB_GetSchoolsFromSUCWebsite.Size = new System.Drawing.Size(130, 23);
            this.btnDB_GetSchoolsFromSUCWebsite.TabIndex = 28;
            this.btnDB_GetSchoolsFromSUCWebsite.Text = "StateUniversity.Com!";
            this.btnDB_GetSchoolsFromSUCWebsite.UseVisualStyleBackColor = true;
            this.btnDB_GetSchoolsFromSUCWebsite.Click += new System.EventHandler(this.btnDB_GetSchoolsFromSUCWebsite_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 13);
            this.label9.TabIndex = 27;
            this.label9.Text = "Status";
            // 
            // txtDB_ScreenscrapeStatus
            // 
            this.txtDB_ScreenscrapeStatus.Location = new System.Drawing.Point(12, 41);
            this.txtDB_ScreenscrapeStatus.Multiline = true;
            this.txtDB_ScreenscrapeStatus.Name = "txtDB_ScreenscrapeStatus";
            this.txtDB_ScreenscrapeStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDB_ScreenscrapeStatus.Size = new System.Drawing.Size(272, 222);
            this.txtDB_ScreenscrapeStatus.TabIndex = 26;
            // 
            // btnDB_GetSchoolsFromHSCWebsite
            // 
            this.btnDB_GetSchoolsFromHSCWebsite.Location = new System.Drawing.Point(13, 280);
            this.btnDB_GetSchoolsFromHSCWebsite.Name = "btnDB_GetSchoolsFromHSCWebsite";
            this.btnDB_GetSchoolsFromHSCWebsite.Size = new System.Drawing.Size(121, 23);
            this.btnDB_GetSchoolsFromHSCWebsite.TabIndex = 25;
            this.btnDB_GetSchoolsFromHSCWebsite.Text = "HighSchools.Com!";
            this.btnDB_GetSchoolsFromHSCWebsite.UseVisualStyleBackColor = true;
            this.btnDB_GetSchoolsFromHSCWebsite.Click += new System.EventHandler(this.btnDB_GetSchoolsFromHSCWebsite_Click);
            // 
            // gbImportData
            // 
            this.gbImportData.Controls.Add(this.tpDB_btnPastePlayers);
            this.gbImportData.Controls.Add(this.label13);
            this.gbImportData.Controls.Add(this.tsTabDBTeamSelector);
            this.gbImportData.Controls.Add(this.btnImportTeam);
            this.gbImportData.Location = new System.Drawing.Point(6, 6);
            this.gbImportData.Name = "gbImportData";
            this.gbImportData.Size = new System.Drawing.Size(297, 194);
            this.gbImportData.TabIndex = 23;
            this.gbImportData.TabStop = false;
            this.gbImportData.Text = "Team Players";
            // 
            // tpDB_btnPastePlayers
            // 
            this.tpDB_btnPastePlayers.Location = new System.Drawing.Point(165, 157);
            this.tpDB_btnPastePlayers.Name = "tpDB_btnPastePlayers";
            this.tpDB_btnPastePlayers.Size = new System.Drawing.Size(121, 23);
            this.tpDB_btnPastePlayers.TabIndex = 26;
            this.tpDB_btnPastePlayers.Text = "Paste Players...";
            this.tpDB_btnPastePlayers.UseVisualStyleBackColor = true;
            this.tpDB_btnPastePlayers.Click += new System.EventHandler(this.tpDB_btnPastePlayers_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 132);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(93, 13);
            this.label13.TabIndex = 25;
            this.label13.Text = "File Type: SI.COM";
            // 
            // tsTabDBTeamSelector
            // 
            this.tsTabDBTeamSelector.Location = new System.Drawing.Point(1, 16);
            this.tsTabDBTeamSelector.Name = "tsTabDBTeamSelector";
            this.tsTabDBTeamSelector.Size = new System.Drawing.Size(290, 99);
            this.tsTabDBTeamSelector.TabIndex = 24;
            // 
            // btnImportTeam
            // 
            this.btnImportTeam.Location = new System.Drawing.Point(165, 127);
            this.btnImportTeam.Name = "btnImportTeam";
            this.btnImportTeam.Size = new System.Drawing.Size(121, 23);
            this.btnImportTeam.TabIndex = 23;
            this.btnImportTeam.Text = "Import Players...";
            this.btnImportTeam.UseVisualStyleBackColor = true;
            this.btnImportTeam.Click += new System.EventHandler(this.btnImportTeam_Click);
            // 
            // tpReports
            // 
            this.tpReports.Controls.Add(this.button9);
            this.tpReports.Controls.Add(this.label10);
            this.tpReports.Controls.Add(this.listView1);
            this.tpReports.Location = new System.Drawing.Point(4, 23);
            this.tpReports.Name = "tpReports";
            this.tpReports.Padding = new System.Windows.Forms.Padding(3);
            this.tpReports.Size = new System.Drawing.Size(672, 542);
            this.tpReports.TabIndex = 4;
            this.tpReports.Text = "tpReports";
            this.tpReports.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(530, 291);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 2;
            this.button9.Text = "View";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 13);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(114, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Select Report To View";
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(18, 29);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(587, 256);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // orderBindingSource
            // 
            this.orderBindingSource.DataSource = typeof(Ffd.Data.Order);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 615);
            this.Controls.Add(this.tcMainAppArea);
            this.Controls.Add(this.splMainMenu);
            this.Controls.Add(this.pnlMainMenuPadding);
            this.Controls.Add(this.ssMainStatus);
            this.Controls.Add(this.menuStripMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "Fan Favorite Designs - Management Console";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.ssMainStatus.ResumeLayout(false);
            this.ssMainStatus.PerformLayout();
            this.pnlMainMenuPadding.ResumeLayout(false);
            this.tcMainAppArea.ResumeLayout(false);
            this.tpProducts.ResumeLayout(false);
            this.tpProducts.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbStatus.ResumeLayout(false);
            this.gbStatus.PerformLayout();
            this.gbCustom.ResumeLayout(false);
            this.gbCustom.PerformLayout();
            this.gbImageType.ResumeLayout(false);
            this.gbImageType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbJerseyImage)).EndInit();
            this.tpSales.ResumeLayout(false);
            this.gbSalesMarketing.ResumeLayout(false);
            this.gbSalesMarketing.PerformLayout();
            this.gbSalesEbay.ResumeLayout(false);
            this.gbSalesEbay.PerformLayout();
            this.tpOrders.ResumeLayout(false);
            this.gbOrdersDetail.ResumeLayout(false);
            this.gbOrdersDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tpOrders_pbJerseyImage)).EndInit();
            this.gbOrdersStatus.ResumeLayout(false);
            this.gbOrdersStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tpOrders_orderGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderDisplayableBindingSource)).EndInit();
            this.tpDatabase.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gbImportData.ResumeLayout(false);
            this.gbImportData.PerformLayout();
            this.tpReports.ResumeLayout(false);
            this.tpReports.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.orderBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList ilMainMenu;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.StatusStrip ssMainStatus;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Panel pnlMainMenuPadding;
        private System.Windows.Forms.ListView lvMainMenu;
        private System.Windows.Forms.Splitter splMainMenu;
        private TabControl tcMainAppArea;
        private System.Windows.Forms.TabPage tpProducts;
        private System.Windows.Forms.TabPage tpSales;
        private System.Windows.Forms.GroupBox gbImageType;
        private System.Windows.Forms.RadioButton rbMarketing;
        private System.Windows.Forms.RadioButton rbCutting;
        private System.Windows.Forms.PictureBox pbJerseyImage;
        private System.Windows.Forms.GroupBox gbCustom;
        private System.Windows.Forms.TabPage tpOrders;
        private System.Windows.Forms.TabPage tpDatabase;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.Label lblNumber;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnTryAgain;
        private System.Windows.Forms.GroupBox gbSalesEbay;
        private System.Windows.Forms.Button btnCreateTurbolisterFile;
        private System.Windows.Forms.GroupBox gbOrdersDetail;
        private System.Windows.Forms.GroupBox gbOrdersStatus;
        private System.Windows.Forms.DataGridView tpOrders_orderGrid;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button tpOrders_GoCutting;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.TabPage tpReports;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.CheckBox chkBoundingBox;
        private System.Windows.Forms.ComboBox cmbTemplate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox gbStatus;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnCreateCST;
        private System.Windows.Forms.Label lblCSTExists;
        private System.Windows.Forms.GroupBox gbImportData;
        private System.Windows.Forms.Button btnImportTeam;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private TeamSelector tsTabDBTeamSelector;
        private TeamSelector tsTabSalesTeamSelector;
        private System.Windows.Forms.Button btnUppercaseDisplayName;
        private System.Windows.Forms.TextBox txtEbayPaste;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCutFileName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSaveAs;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btnUploadGraphics;
        private System.Windows.Forms.CheckBox chkNewPlayersOnly;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtLName;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtMI;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtFName;
        private System.Windows.Forms.Label label14;
        private TeamSelector tpProductsTeamSelector;
        private System.Windows.Forms.Button btntpProductsAddToTeam;
        private System.Windows.Forms.CheckBox chkLeadingZero;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblPasteStatus;
        private System.Windows.Forms.Button btntbSales_createTLCustomsFile;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button tpDB_btnPastePlayers;
        private System.Windows.Forms.CheckBox chktpSales_CreateGraphics;
        private System.Windows.Forms.CheckBox chktpSales_Generic;
        private System.Windows.Forms.Button btn_orders_ProcessMonsterFile;
        private System.Windows.Forms.Button btnOrders_CreatePaypalShippingFile;
        private System.Windows.Forms.BindingSource orderBindingSource;
        private System.Windows.Forms.BindingSource orderDisplayableBindingSource;
        private System.Windows.Forms.Label tpOrders_lblTemplateDesc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label tpOrders_lblCustName;
        private System.Windows.Forms.Label tpOrders_lblJerseyNumber;
        private System.Windows.Forms.Label tpOrders_lblJerseyName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox tpOrders_pbJerseyImage;
        private System.Windows.Forms.Label tpOrders_lblQty;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button tpOrders_btnArchiveMonsterFile;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDB_ScreenscrapeStatus;
        private System.Windows.Forms.Button btnDB_GetSchoolsFromHSCWebsite;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnDB_GoLeadOutputFile;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox tpDB_txtQtyToGet;
        private System.Windows.Forms.Label tpDB_lblSupplyQty;
        private System.Windows.Forms.ComboBox tpDB_cmbLeadStatus;
        private System.Windows.Forms.ComboBox tpDB_cmbLeadSource;
        private System.Windows.Forms.TextBox tpDB_txtOutputFName;
        private System.Windows.Forms.TextBox tpDB_txtMinEnrollment;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox tpDB_txtSkipEvery;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label tpDB_lblStatus;
        private System.Windows.Forms.Button btnDB_GetSchoolsFromSUCWebsite;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListBox tpDB_lstBadAddresses;
        private System.Windows.Forms.Button tpDB_btnFixAddr;
        private System.Windows.Forms.Button tpDB_btnRefresh;
        private System.Windows.Forms.CheckBox tpDB_chkMustBeAthletics;
        private System.Windows.Forms.Button tpDB_btnImportBadAddrs;
        private System.Windows.Forms.Button tpDB_btnMailed;
        private System.Windows.Forms.Label tpSales_lblEbayToolsStatus;
        private System.Windows.Forms.Label tpSales_lblEbayGraphicsStatus;
        private System.Windows.Forms.CheckBox tpSales_chkUploadAll;
        private System.Windows.Forms.GroupBox gbSalesMarketing;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Button tpSales_Mkt_BuildMsgBrd;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label tpOrders_lblColor;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShippingFirstName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShippingLastName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShippingCity;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShippingState;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShippingZip;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShippingCountry;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumberOfItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn FirstItemDescriptionExternal;
        private System.Windows.Forms.DataGridViewTextBoxColumn FirstItemTemplateId;
        private System.Windows.Forms.DataGridViewTextBoxColumn FirstItemTemplateDescriptionShort;
        private System.Windows.Forms.DataGridViewTextBoxColumn FirstItemQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn FirstItemJerseyName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FirstItemJerseyNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn FirstItemColor;
    }
}