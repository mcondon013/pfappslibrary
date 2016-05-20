namespace TestprogLoadAWDW
{
    partial class HelpAboutForm
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
            this.lblVersion = new System.Windows.Forms.Label();
            this.txtSystemInfo = new System.Windows.Forms.TextBox();
            this.cmdOK = new System.Windows.Forms.Button();
            this.txtApplicationName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtApplicationInfo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRegistrationInfo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Location = new System.Drawing.Point(125, 51);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(98, 20);
            this.lblVersion.TabIndex = 2;
            this.lblVersion.Text = "Version 0.x";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtSystemInfo
            // 
            this.txtSystemInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSystemInfo.Location = new System.Drawing.Point(27, 350);
            this.txtSystemInfo.Multiline = true;
            this.txtSystemInfo.Name = "txtSystemInfo";
            this.txtSystemInfo.ReadOnly = true;
            this.txtSystemInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSystemInfo.Size = new System.Drawing.Size(292, 141);
            this.txtSystemInfo.TabIndex = 7;
            this.txtSystemInfo.WordWrap = false;
            // 
            // cmdOK
            // 
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdOK.Location = new System.Drawing.Point(129, 509);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(77, 35);
            this.cmdOK.TabIndex = 0;
            this.cmdOK.Text = "&OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // txtApplicationName
            // 
            this.txtApplicationName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtApplicationName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtApplicationName.Location = new System.Drawing.Point(27, 24);
            this.txtApplicationName.Name = "txtApplicationName";
            this.txtApplicationName.ReadOnly = true;
            this.txtApplicationName.Size = new System.Drawing.Size(292, 24);
            this.txtApplicationName.TabIndex = 1;
            this.txtApplicationName.Text = "TestprogLoadAWDW";
            this.txtApplicationName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 320);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "System Information:";
            // 
            // txtApplicationInfo
            // 
            this.txtApplicationInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtApplicationInfo.Location = new System.Drawing.Point(27, 89);
            this.txtApplicationInfo.Multiline = true;
            this.txtApplicationInfo.Name = "txtApplicationInfo";
            this.txtApplicationInfo.ReadOnly = true;
            this.txtApplicationInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtApplicationInfo.Size = new System.Drawing.Size(292, 109);
            this.txtApplicationInfo.TabIndex = 3;
            this.txtApplicationInfo.WordWrap = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(24, 212);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Registered To:";
            // 
            // txtRegistrationInfo
            // 
            this.txtRegistrationInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRegistrationInfo.Location = new System.Drawing.Point(27, 238);
            this.txtRegistrationInfo.Multiline = true;
            this.txtRegistrationInfo.Name = "txtRegistrationInfo";
            this.txtRegistrationInfo.ReadOnly = true;
            this.txtRegistrationInfo.Size = new System.Drawing.Size(292, 69);
            this.txtRegistrationInfo.TabIndex = 5;
            // 
            // HelpAboutForm
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdOK;
            this.ClientSize = new System.Drawing.Size(345, 575);
            this.Controls.Add(this.txtRegistrationInfo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtApplicationInfo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtApplicationName);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.txtSystemInfo);
            this.Controls.Add(this.lblVersion);
            this.Name = "HelpAboutForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CHelpAbout";
            this.Load += new System.EventHandler(this.CHelpAbout_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.TextBox txtSystemInfo;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.TextBox txtApplicationName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtApplicationInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRegistrationInfo;
    }
}