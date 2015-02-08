using Ffd.App.Core;
using Ffd.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Ffd.Common;
using System.Drawing.Imaging;

namespace Ffd.Presentation.Manager
{
    public partial class frmGraphicProcessor : Form
    {
        private int _playerLoop = 0;
        private List<Player> _playerList = null;

        private const string CUT_OUTFILE = "cut-outfile.bmp";
        private const string MKT_OUTFILE = "marketing-outfile.gif";

        public frmGraphicProcessor()
        {
            InitializeComponent();
        }

        private void frmGraphicProcessor_Load(object sender, EventArgs e)
        {
            _playerList = ApplicationManager.GetPlayers();
            ProcessGraphics(_playerList[0]);
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

        private void ProcessGraphics(Player player)
        {
            txtFullName.Text = player.FullName;
            txtNumber.Text = player.Number;

            // pbJerseyImage.Image = ApplicationManager.GetHockeyJerseyFromFileSystemTemplate(player, SelectedImageType());
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnTryAgain_Click(object sender, EventArgs e)
        {
            //
            // Show button
            //
            Player player = new Player(txtFullName.Text, txtNumber.Text);
            ProcessGraphics(player);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            _playerLoop = (++_playerLoop) % _playerList.Count;
            ProcessGraphics(_playerList[_playerLoop]);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (pbJerseyImage.Image != null)
            {
                bool res = false;

                if (SelectedImageType() == ApplicationManager.StickerImageType.Cutting)
                {
                    // pbJerseyImage.Image.Save(

                    // Save the image as a huge bitmap for importing into Cutstudio
                    string fileName = Functions.BuildFilenameFromElements(Common.Config.GraphicsRootDirectory(), CUT_OUTFILE);

                    res = ApplicationManager.SaveImageToFile(pbJerseyImage.Image, fileName, ImageFormat.Bmp, 24L); 
                }
                else if (SelectedImageType() == ApplicationManager.StickerImageType.Marketing)
                {
                    string fileName = Functions.BuildFilenameFromElements(Config.GraphicsRootDirectory(), MKT_OUTFILE);

                    res = ApplicationManager.SaveImageToFile(pbJerseyImage.Image, fileName, ImageFormat.Gif, 4L, 320);
                }
                else
                {
                    throw new ApplicationException("Sticker image type not implemented");
                }

                MessageBox.Show(string.Format("Save operation returned: {0}", res), "Results of Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblNumber_Click(object sender, EventArgs e)
        {

        }

        private void txtFullName_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblName_Click(object sender, EventArgs e)
        {

        }

        private void pbJerseyImage_Click(object sender, EventArgs e)
        {

        }

        private void gbImageType_Enter(object sender, EventArgs e)
        {

        }
    }
}