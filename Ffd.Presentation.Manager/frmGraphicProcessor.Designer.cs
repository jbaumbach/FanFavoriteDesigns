namespace Ffd.Presentation.Manager
{
    partial class frmGraphicProcessor
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
            this.btnClose = new System.Windows.Forms.Button();
            this.pbJerseyImage = new System.Windows.Forms.PictureBox();
            this.btnTryAgain = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.lblNumber = new System.Windows.Forms.Label();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.gbImageType = new System.Windows.Forms.GroupBox();
            this.rbMarketing = new System.Windows.Forms.RadioButton();
            this.rbCutting = new System.Windows.Forms.RadioButton();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbJerseyImage)).BeginInit();
            this.gbImageType.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(222, 468);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Hasta";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pbJerseyImage
            // 
            this.pbJerseyImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbJerseyImage.Location = new System.Drawing.Point(12, 117);
            this.pbJerseyImage.Name = "pbJerseyImage";
            this.pbJerseyImage.Size = new System.Drawing.Size(285, 345);
            this.pbJerseyImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbJerseyImage.TabIndex = 1;
            this.pbJerseyImage.TabStop = false;
            this.pbJerseyImage.Click += new System.EventHandler(this.pbJerseyImage_Click);
            // 
            // btnTryAgain
            // 
            this.btnTryAgain.Location = new System.Drawing.Point(222, 42);
            this.btnTryAgain.Name = "btnTryAgain";
            this.btnTryAgain.Size = new System.Drawing.Size(75, 23);
            this.btnTryAgain.TabIndex = 2;
            this.btnTryAgain.Text = "Show";
            this.btnTryAgain.UseVisualStyleBackColor = true;
            this.btnTryAgain.Click += new System.EventHandler(this.btnTryAgain_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(18, 16);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 3;
            this.lblName.Text = "Name";
            this.lblName.Click += new System.EventHandler(this.lblName_Click);
            // 
            // txtFullName
            // 
            this.txtFullName.Location = new System.Drawing.Point(59, 13);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(208, 20);
            this.txtFullName.TabIndex = 4;
            this.txtFullName.TextChanged += new System.EventHandler(this.txtFullName_TextChanged);
            // 
            // lblNumber
            // 
            this.lblNumber.AutoSize = true;
            this.lblNumber.Location = new System.Drawing.Point(9, 42);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(44, 13);
            this.lblNumber.TabIndex = 5;
            this.lblNumber.Text = "Number";
            this.lblNumber.Click += new System.EventHandler(this.lblNumber_Click);
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new System.Drawing.Point(59, 39);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(46, 20);
            this.txtNumber.TabIndex = 6;
            this.txtNumber.TextChanged += new System.EventHandler(this.txtNumber_TextChanged);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(12, 468);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 7;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // gbImageType
            // 
            this.gbImageType.Controls.Add(this.btnSave);
            this.gbImageType.Controls.Add(this.rbMarketing);
            this.gbImageType.Controls.Add(this.rbCutting);
            this.gbImageType.Location = new System.Drawing.Point(13, 68);
            this.gbImageType.Name = "gbImageType";
            this.gbImageType.Size = new System.Drawing.Size(284, 43);
            this.gbImageType.TabIndex = 8;
            this.gbImageType.TabStop = false;
            this.gbImageType.Text = "Image Type";
            this.gbImageType.Enter += new System.EventHandler(this.gbImageType_Enter);
            // 
            // rbMarketing
            // 
            this.rbMarketing.AutoSize = true;
            this.rbMarketing.Location = new System.Drawing.Point(80, 20);
            this.rbMarketing.Name = "rbMarketing";
            this.rbMarketing.Size = new System.Drawing.Size(72, 17);
            this.rbMarketing.TabIndex = 1;
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
            this.rbCutting.TabIndex = 0;
            this.rbCutting.TabStop = true;
            this.rbCutting.Tag = "0";
            this.rbCutting.Text = "Cutting";
            this.rbCutting.UseVisualStyleBackColor = true;
            this.rbCutting.Click += new System.EventHandler(this.btnTryAgain_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(203, 14);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmGraphicProcessor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 503);
            this.Controls.Add(this.gbImageType);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.txtNumber);
            this.Controls.Add(this.lblNumber);
            this.Controls.Add(this.txtFullName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.btnTryAgain);
            this.Controls.Add(this.pbJerseyImage);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmGraphicProcessor";
            this.Text = "Process Graphics";
            this.Load += new System.EventHandler(this.frmGraphicProcessor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbJerseyImage)).EndInit();
            this.gbImageType.ResumeLayout(false);
            this.gbImageType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.PictureBox pbJerseyImage;
        private System.Windows.Forms.Button btnTryAgain;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label lblNumber;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.GroupBox gbImageType;
        private System.Windows.Forms.RadioButton rbMarketing;
        private System.Windows.Forms.RadioButton rbCutting;
        private System.Windows.Forms.Button btnSave;
    }
}

