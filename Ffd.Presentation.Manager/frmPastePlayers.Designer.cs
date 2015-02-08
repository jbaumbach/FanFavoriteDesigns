namespace Ffd.Presentation.Manager
{
    partial class frmPastePlayers
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtPastePlayerList = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvPlayers = new System.Windows.Forms.DataGridView();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblFound = new System.Windows.Forms.Label();
            this.playerSeasonBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.JerseyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.jerseyNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MiddleInitial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkSingleNamesAsFirst = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlayers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerSeasonBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Paste player list into this textbox";
            // 
            // txtPastePlayerList
            // 
            this.txtPastePlayerList.BackColor = System.Drawing.SystemColors.Info;
            this.txtPastePlayerList.Location = new System.Drawing.Point(12, 29);
            this.txtPastePlayerList.Multiline = true;
            this.txtPastePlayerList.Name = "txtPastePlayerList";
            this.txtPastePlayerList.Size = new System.Drawing.Size(602, 21);
            this.txtPastePlayerList.TabIndex = 1;
            this.txtPastePlayerList.TextChanged += new System.EventHandler(this.txtPastePlayerList_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Results";
            // 
            // dgvPlayers
            // 
            this.dgvPlayers.AutoGenerateColumns = false;
            this.dgvPlayers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPlayers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.JerseyName,
            this.jerseyNumberDataGridViewTextBoxColumn,
            this.FirstName,
            this.MiddleInitial,
            this.lastNameDataGridViewTextBoxColumn});
            this.dgvPlayers.DataSource = this.playerSeasonBindingSource;
            this.dgvPlayers.Location = new System.Drawing.Point(12, 102);
            this.dgvPlayers.Name = "dgvPlayers";
            this.dgvPlayers.Size = new System.Drawing.Size(526, 332);
            this.dgvPlayers.TabIndex = 3;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(544, 377);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(544, 408);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblFound
            // 
            this.lblFound.Location = new System.Drawing.Point(544, 345);
            this.lblFound.Name = "lblFound";
            this.lblFound.Size = new System.Drawing.Size(75, 29);
            this.lblFound.TabIndex = 6;
            this.lblFound.Text = "lblFound";
            // 
            // playerSeasonBindingSource
            // 
            this.playerSeasonBindingSource.DataSource = typeof(Ffd.Data.PlayerSeason);
            // 
            // JerseyName
            // 
            this.JerseyName.DataPropertyName = "JerseyName";
            this.JerseyName.HeaderText = "JerseyName";
            this.JerseyName.Name = "JerseyName";
            // 
            // jerseyNumberDataGridViewTextBoxColumn
            // 
            this.jerseyNumberDataGridViewTextBoxColumn.DataPropertyName = "JerseyNumber";
            this.jerseyNumberDataGridViewTextBoxColumn.HeaderText = "JerseyNumber";
            this.jerseyNumberDataGridViewTextBoxColumn.Name = "jerseyNumberDataGridViewTextBoxColumn";
            this.jerseyNumberDataGridViewTextBoxColumn.Width = 80;
            // 
            // FirstName
            // 
            this.FirstName.DataPropertyName = "FirstName";
            this.FirstName.HeaderText = "FirstName";
            this.FirstName.Name = "FirstName";
            // 
            // MiddleInitial
            // 
            this.MiddleInitial.DataPropertyName = "MiddleInitial";
            this.MiddleInitial.HeaderText = "MiddleInitial";
            this.MiddleInitial.Name = "MiddleInitial";
            // 
            // lastNameDataGridViewTextBoxColumn
            // 
            this.lastNameDataGridViewTextBoxColumn.DataPropertyName = "LastName";
            this.lastNameDataGridViewTextBoxColumn.HeaderText = "LastName";
            this.lastNameDataGridViewTextBoxColumn.Name = "lastNameDataGridViewTextBoxColumn";
            // 
            // chkSingleNamesAsFirst
            // 
            this.chkSingleNamesAsFirst.AutoSize = true;
            this.chkSingleNamesAsFirst.Checked = true;
            this.chkSingleNamesAsFirst.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSingleNamesAsFirst.Location = new System.Drawing.Point(12, 56);
            this.chkSingleNamesAsFirst.Name = "chkSingleNamesAsFirst";
            this.chkSingleNamesAsFirst.Size = new System.Drawing.Size(256, 17);
            this.chkSingleNamesAsFirst.TabIndex = 7;
            this.chkSingleNamesAsFirst.Text = "Treat single names as first names (e.g. Brazilians)";
            this.chkSingleNamesAsFirst.UseVisualStyleBackColor = true;
            // 
            // frmPastePlayers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 446);
            this.Controls.Add(this.chkSingleNamesAsFirst);
            this.Controls.Add(this.lblFound);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.dgvPlayers);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPastePlayerList);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPastePlayers";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import Players";
            this.Load += new System.EventHandler(this.frmPastePlayers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlayers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerSeasonBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPastePlayerList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvPlayers;
        private System.Windows.Forms.BindingSource playerSeasonBindingSource;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblFound;
        private System.Windows.Forms.DataGridViewTextBoxColumn JerseyName;
        private System.Windows.Forms.DataGridViewTextBoxColumn jerseyNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FirstName;
        private System.Windows.Forms.DataGridViewTextBoxColumn MiddleInitial;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.CheckBox chkSingleNamesAsFirst;
    }
}