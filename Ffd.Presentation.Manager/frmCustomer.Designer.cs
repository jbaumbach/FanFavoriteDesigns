namespace Ffd.Presentation.Manager
{
    partial class frmCustomer
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
            Ffd.Data.Address address3 = new Ffd.Data.Address();
            Ffd.Data.Address address4 = new Ffd.Data.Address();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCustomerId = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblCreateDate = new System.Windows.Forms.Label();
            this.txtCompany = new System.Windows.Forms.TextBox();
            this.tcAddresses = new System.Windows.Forms.TabControl();
            this.tpShipping = new System.Windows.Forms.TabPage();
            this.tpShipping_ctlAddress = new Ffd.Presentation.Manager.AddressControl();
            this.tpBilling = new System.Windows.Forms.TabPage();
            this.tpBilling_ctlAddress = new Ffd.Presentation.Manager.AddressControl();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbLead = new System.Windows.Forms.GroupBox();
            this.cmbStatusCode = new System.Windows.Forms.ComboBox();
            this.cmbSourceCode = new System.Windows.Forms.ComboBox();
            this.lblLeadDate = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtEnrollment = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.chkIsAthletics = new System.Windows.Forms.CheckBox();
            this.chkIsSchool = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnNew = new System.Windows.Forms.Button();
            this.chkUpdateFromShipping = new System.Windows.Forms.CheckBox();
            this.tcAddresses.SuspendLayout();
            this.tpShipping.SuspendLayout();
            this.tpBilling.SuspendLayout();
            this.gbLead.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Customer Id";
            // 
            // lblCustomerId
            // 
            this.lblCustomerId.AutoSize = true;
            this.lblCustomerId.Location = new System.Drawing.Point(83, 13);
            this.lblCustomerId.Name = "lblCustomerId";
            this.lblCustomerId.Size = new System.Drawing.Size(70, 13);
            this.lblCustomerId.TabIndex = 1;
            this.lblCustomerId.Text = "lblCustomerId";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Email Addr";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "First Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Last Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Company";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 148);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Phone Day";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 175);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Create Date";
            // 
            // lblCreateDate
            // 
            this.lblCreateDate.AutoSize = true;
            this.lblCreateDate.Location = new System.Drawing.Point(83, 175);
            this.lblCreateDate.Name = "lblCreateDate";
            this.lblCreateDate.Size = new System.Drawing.Size(71, 13);
            this.lblCreateDate.TabIndex = 9;
            this.lblCreateDate.Text = "lblCreateDate";
            // 
            // txtCompany
            // 
            this.txtCompany.Location = new System.Drawing.Point(71, 118);
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.Size = new System.Drawing.Size(169, 20);
            this.txtCompany.TabIndex = 10;
            this.txtCompany.Text = "txtCompany";
            // 
            // tcAddresses
            // 
            this.tcAddresses.Controls.Add(this.tpShipping);
            this.tcAddresses.Controls.Add(this.tpBilling);
            this.tcAddresses.Location = new System.Drawing.Point(246, 13);
            this.tcAddresses.Name = "tcAddresses";
            this.tcAddresses.SelectedIndex = 0;
            this.tcAddresses.Size = new System.Drawing.Size(419, 187);
            this.tcAddresses.TabIndex = 11;
            // 
            // tpShipping
            // 
            this.tpShipping.Controls.Add(this.tpShipping_ctlAddress);
            this.tpShipping.Location = new System.Drawing.Point(4, 22);
            this.tpShipping.Name = "tpShipping";
            this.tpShipping.Padding = new System.Windows.Forms.Padding(3);
            this.tpShipping.Size = new System.Drawing.Size(411, 161);
            this.tpShipping.TabIndex = 0;
            this.tpShipping.Text = "Shipping Address";
            this.tpShipping.UseVisualStyleBackColor = true;
            // 
            // tpShipping_ctlAddress
            // 
            address3.Address1 = "txtAddress1";
            address3.Address2 = "txtAddress2";
            address3.Address3 = null;
            address3.AddressId = -1;
            address3.City = "txtCity";
            address3.CompanyName = "txtCompanyName";
            address3.CountryCode = null;
            address3.Domestic = true;
            address3.FirstName = "txtFName";
            address3.LastName = "txtLName";
            address3.Phone = null;
            address3.StateProvAbbrev = "txtState";
            address3.StateProvCode = 0;
            address3.Taxable = false;
            address3.ZipPostalCode = "txtZip";
            this.tpShipping_ctlAddress.CurrentAddress = address3;
            this.tpShipping_ctlAddress.Location = new System.Drawing.Point(7, 7);
            this.tpShipping_ctlAddress.Name = "tpShipping_ctlAddress";
            this.tpShipping_ctlAddress.Size = new System.Drawing.Size(385, 146);
            this.tpShipping_ctlAddress.TabIndex = 0;
            // 
            // tpBilling
            // 
            this.tpBilling.Controls.Add(this.tpBilling_ctlAddress);
            this.tpBilling.Location = new System.Drawing.Point(4, 22);
            this.tpBilling.Name = "tpBilling";
            this.tpBilling.Padding = new System.Windows.Forms.Padding(3);
            this.tpBilling.Size = new System.Drawing.Size(411, 161);
            this.tpBilling.TabIndex = 1;
            this.tpBilling.Text = "Billing Address";
            this.tpBilling.UseVisualStyleBackColor = true;
            // 
            // tpBilling_ctlAddress
            // 
            address4.Address1 = "txtAddress1";
            address4.Address2 = "txtAddress2";
            address4.Address3 = null;
            address4.AddressId = -1;
            address4.City = "txtCity";
            address4.CompanyName = "txtCompanyName";
            address4.CountryCode = null;
            address4.Domestic = true;
            address4.FirstName = "txtFName";
            address4.LastName = "txtLName";
            address4.Phone = null;
            address4.StateProvAbbrev = "txtState";
            address4.StateProvCode = 0;
            address4.Taxable = false;
            address4.ZipPostalCode = "txtZip";
            this.tpBilling_ctlAddress.CurrentAddress = address4;
            this.tpBilling_ctlAddress.Location = new System.Drawing.Point(7, 7);
            this.tpBilling_ctlAddress.Name = "tpBilling_ctlAddress";
            this.tpBilling_ctlAddress.Size = new System.Drawing.Size(385, 146);
            this.tpBilling_ctlAddress.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(509, 367);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(590, 367);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // gbLead
            // 
            this.gbLead.Controls.Add(this.cmbStatusCode);
            this.gbLead.Controls.Add(this.cmbSourceCode);
            this.gbLead.Controls.Add(this.lblLeadDate);
            this.gbLead.Controls.Add(this.label11);
            this.gbLead.Controls.Add(this.txtEnrollment);
            this.gbLead.Controls.Add(this.label10);
            this.gbLead.Controls.Add(this.chkIsAthletics);
            this.gbLead.Controls.Add(this.chkIsSchool);
            this.gbLead.Controls.Add(this.label9);
            this.gbLead.Controls.Add(this.label8);
            this.gbLead.Location = new System.Drawing.Point(16, 219);
            this.gbLead.Name = "gbLead";
            this.gbLead.Size = new System.Drawing.Size(649, 138);
            this.gbLead.TabIndex = 14;
            this.gbLead.TabStop = false;
            this.gbLead.Text = "Lead";
            // 
            // cmbStatusCode
            // 
            this.cmbStatusCode.FormattingEnabled = true;
            this.cmbStatusCode.Location = new System.Drawing.Point(89, 49);
            this.cmbStatusCode.Name = "cmbStatusCode";
            this.cmbStatusCode.Size = new System.Drawing.Size(158, 21);
            this.cmbStatusCode.TabIndex = 11;
            // 
            // cmbSourceCode
            // 
            this.cmbSourceCode.FormattingEnabled = true;
            this.cmbSourceCode.Location = new System.Drawing.Point(89, 21);
            this.cmbSourceCode.Name = "cmbSourceCode";
            this.cmbSourceCode.Size = new System.Drawing.Size(158, 21);
            this.cmbSourceCode.TabIndex = 10;
            // 
            // lblLeadDate
            // 
            this.lblLeadDate.AutoSize = true;
            this.lblLeadDate.Location = new System.Drawing.Point(512, 21);
            this.lblLeadDate.Name = "lblLeadDate";
            this.lblLeadDate.Size = new System.Drawing.Size(64, 13);
            this.lblLeadDate.TabIndex = 9;
            this.lblLeadDate.Text = "lblLeadDate";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(449, 21);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(57, 13);
            this.label11.TabIndex = 8;
            this.label11.Text = "Lead Date";
            // 
            // txtEnrollment
            // 
            this.txtEnrollment.Location = new System.Drawing.Point(315, 21);
            this.txtEnrollment.Name = "txtEnrollment";
            this.txtEnrollment.Size = new System.Drawing.Size(100, 20);
            this.txtEnrollment.TabIndex = 7;
            this.txtEnrollment.Text = "txtEnrollment";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(253, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "Enrollment";
            // 
            // chkIsAthletics
            // 
            this.chkIsAthletics.AutoSize = true;
            this.chkIsAthletics.Location = new System.Drawing.Point(16, 104);
            this.chkIsAthletics.Name = "chkIsAthletics";
            this.chkIsAthletics.Size = new System.Drawing.Size(74, 17);
            this.chkIsAthletics.TabIndex = 5;
            this.chkIsAthletics.Text = "IsAthletics";
            this.chkIsAthletics.UseVisualStyleBackColor = true;
            // 
            // chkIsSchool
            // 
            this.chkIsSchool.AutoSize = true;
            this.chkIsSchool.Location = new System.Drawing.Point(16, 81);
            this.chkIsSchool.Name = "chkIsSchool";
            this.chkIsSchool.Size = new System.Drawing.Size(67, 17);
            this.chkIsSchool.TabIndex = 4;
            this.chkIsSchool.Text = "IsSchool";
            this.chkIsSchool.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 52);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Status Code";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Source Code";
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(16, 367);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 15;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // chkUpdateFromShipping
            // 
            this.chkUpdateFromShipping.AutoSize = true;
            this.chkUpdateFromShipping.Checked = true;
            this.chkUpdateFromShipping.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUpdateFromShipping.Location = new System.Drawing.Point(16, 196);
            this.chkUpdateFromShipping.Name = "chkUpdateFromShipping";
            this.chkUpdateFromShipping.Size = new System.Drawing.Size(131, 17);
            this.chkUpdateFromShipping.TabIndex = 16;
            this.chkUpdateFromShipping.Text = "Update From Shipping";
            this.chkUpdateFromShipping.UseVisualStyleBackColor = true;
            // 
            // frmCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 399);
            this.Controls.Add(this.chkUpdateFromShipping);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.gbLead);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tcAddresses);
            this.Controls.Add(this.txtCompany);
            this.Controls.Add(this.lblCreateDate);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblCustomerId);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCustomer";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add/Edit Customer";
            this.tcAddresses.ResumeLayout(false);
            this.tpShipping.ResumeLayout(false);
            this.tpBilling.ResumeLayout(false);
            this.gbLead.ResumeLayout(false);
            this.gbLead.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCustomerId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblCreateDate;
        private System.Windows.Forms.TextBox txtCompany;
        private System.Windows.Forms.TabControl tcAddresses;
        private System.Windows.Forms.TabPage tpShipping;
        private System.Windows.Forms.TabPage tpBilling;
        private AddressControl tpShipping_ctlAddress;
        private AddressControl tpBilling_ctlAddress;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox gbLead;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox chkIsAthletics;
        private System.Windows.Forms.CheckBox chkIsSchool;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblLeadDate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtEnrollment;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.ComboBox cmbStatusCode;
        private System.Windows.Forms.ComboBox cmbSourceCode;
        private System.Windows.Forms.CheckBox chkUpdateFromShipping;
    }
}