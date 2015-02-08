namespace Ffd.Presentation.Manager
{
    partial class TeamSelector
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbSeasons = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbFranchises = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbLeagues = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbSeasons
            // 
            this.cmbSeasons.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSeasons.FormattingEnabled = true;
            this.cmbSeasons.Location = new System.Drawing.Point(82, 40);
            this.cmbSeasons.Name = "cmbSeasons";
            this.cmbSeasons.Size = new System.Drawing.Size(197, 21);
            this.cmbSeasons.TabIndex = 3;
            this.cmbSeasons.SelectedIndexChanged += new System.EventHandler(this.cmbSeasons_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(7, 43);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(43, 13);
            this.label13.TabIndex = 2;
            this.label13.Text = "Season";
            // 
            // cmbFranchises
            // 
            this.cmbFranchises.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFranchises.FormattingEnabled = true;
            this.cmbFranchises.Location = new System.Drawing.Point(82, 67);
            this.cmbFranchises.Name = "cmbFranchises";
            this.cmbFranchises.Size = new System.Drawing.Size(197, 21);
            this.cmbFranchises.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Franchise";
            // 
            // cmbLeagues
            // 
            this.cmbLeagues.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLeagues.FormattingEnabled = true;
            this.cmbLeagues.Location = new System.Drawing.Point(82, 13);
            this.cmbLeagues.Name = "cmbLeagues";
            this.cmbLeagues.Size = new System.Drawing.Size(197, 21);
            this.cmbLeagues.TabIndex = 1;
            this.cmbLeagues.SelectedIndexChanged += new System.EventHandler(this.cmbLeagues_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "League";
            // 
            // TeamSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbSeasons);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.cmbFranchises);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbLeagues);
            this.Controls.Add(this.label1);
            this.Name = "TeamSelector";
            this.Size = new System.Drawing.Size(290, 99);
            this.Load += new System.EventHandler(this.TeamSelector_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbSeasons;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cmbFranchises;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbLeagues;
        private System.Windows.Forms.Label label1;
    }
}
