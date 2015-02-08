using Ffd.App.Core;
using Ffd.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ffd.Presentation.Manager
{
    public partial class frmPastePlayers : Form
    {

        private Franchise _currentFranchise = null;
        private List<PlayerSeason> _playerSeasons = null;
        private DialogResult _result = DialogResult.Cancel;
        private Season _currentSeason = null;

        /// <summary>
        /// The franchise for these players.
        /// </summary>
        public Franchise CurrentFranchise
        {
            get { return _currentFranchise; }
            set { _currentFranchise = value; }
        }

        /// <summary>
        /// The player list for/from the interface.
        /// </summary>
        public List<PlayerSeason> PlayerSeasons
        {
            get { return _playerSeasons; }
            set { _playerSeasons = value; }
        }
        public DialogResult Result
        {
            get { return _result; }
            set { _result = value; }
        }

        /// <summary>
        /// The season for these players.
        /// </summary>
        public Season CurrentSeason
        {
            get { return _currentSeason; }
            set { _currentSeason = value; }
        }

        public frmPastePlayers()
        {
            InitializeComponent();
        }

        private void txtPastePlayerList_TextChanged(object sender, EventArgs e)
        {
            if (txtPastePlayerList.Text != "")
            {
                //
                // Parse pasted text and get player list
                // 
                int skipped;

                _playerSeasons = ApplicationManager.GetPlayerSeasonListFromStringLines(txtPastePlayerList.Text, _currentFranchise, _currentSeason, chkSingleNamesAsFirst.Checked, out skipped);
                dgvPlayers.DataSource = _playerSeasons;
                txtPastePlayerList.Text = "";

                RefreshUI();
            }
            
        }

        private void frmPastePlayers_Load(object sender, EventArgs e)
        {
            if (_currentFranchise != null)
            {
                this.Text = string.Format("{0} - {1}", this.Text, _currentFranchise.FranchiseDesc);
            }

            if (_playerSeasons != null)
            {
                dgvPlayers.DataSource = _playerSeasons;
            }

            RefreshUI();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _result = DialogResult.OK;
            _playerSeasons = new List<PlayerSeason>();

            foreach (DataGridViewRow playerRow in dgvPlayers.Rows)
            {
                _playerSeasons.Add(playerRow.DataBoundItem as PlayerSeason);
            }

            CloseDialog();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CloseDialog();
        }

        private void CloseDialog()
        {
            this.Close();
        }

        private void RefreshUI()
        {
            if (_playerSeasons != null)
            {
                lblFound.Text = string.Format("{0} items", _playerSeasons.Count.ToString());
            }
            else
            {
                lblFound.Text = "0 items";
            }
        }
    }
}