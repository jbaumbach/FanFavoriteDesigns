using Ffd.Common;
using Ffd.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Ffd.Presentation.Manager
{
    public partial class TeamSelector : UserControl
    {
        // private Franchise _currentFranchise;
        // private League _currentLeague;
        // private Season _currentSeason;

        public Franchise CurrentFranchise
        {
            get { return cmbFranchises.SelectedItem as Franchise; }
            // set { _currentFranchise = value; }
        }

        public League CurrentLeague
        {
            get { return cmbLeagues.SelectedItem as League; }
        }

        public Season CurrentSeason
        {
            get { return cmbSeasons.SelectedItem as Season; }
        }


        public void LoadData()
        {
            Functions.SetItemsIntoDropdownWithExtraDefaultValue(cmbLeagues, DataManager.GetLeagues(true));
        }

        public TeamSelector()
        {
            InitializeComponent();
        }

        private void cmbLeagues_SelectedIndexChanged(object sender, EventArgs e)
        {
            League currentLeague = CurrentLeague;

            if (currentLeague != null)
            {
                // cmbSeasons.DataSource = DataManager.GetSeasonsForLeague(CurrentLeague);
                Functions.SetItemsIntoDropdownWithExtraDefaultValue(cmbSeasons, DataManager.GetSeasonsForLeague(currentLeague));
            }
            else
            {
                cmbSeasons.DataSource = null;
                cmbSeasons.Items.Clear();
            }
        }

        private void cmbSeasons_SelectedIndexChanged(object sender, EventArgs e)
        {
            Season currentSeason = CurrentSeason;

            if (currentSeason != null)
            {
                // cmbFranchises.DataSource = DataManager.GetFranchisesForSeason(currentSeason);
                Functions.SetItemsIntoDropdownWithExtraDefaultValue(cmbFranchises, DataManager.GetFranchisesForSeason(currentSeason));
            }
            else
            {
                cmbFranchises.DataSource = null;
                cmbFranchises.Items.Clear();
            }
        }

        private void TeamSelector_Load(object sender, EventArgs e)
        {

        }
    }
}
