namespace PFRandomDataForms
{
    partial class RandomDataRequestRenameForm
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
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.lblInputData = new System.Windows.Forms.Label();
            this.txtInputData = new System.Windows.Forms.TextBox();
            this.lblOriginalData = new System.Windows.Forms.Label();
            this.txtOriginalData = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(195, 152);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(98, 34);
            this.cmdOK.TabIndex = 2;
            this.cmdOK.Text = "&Rename";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(195, 205);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(98, 34);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "&Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // lblInputData
            // 
            this.lblInputData.AutoSize = true;
            this.lblInputData.Location = new System.Drawing.Point(31, 86);
            this.lblInputData.Name = "lblInputData";
            this.lblInputData.Size = new System.Drawing.Size(187, 13);
            this.lblInputData.TabIndex = 0;
            this.lblInputData.Text = "New Name for Random Data Request";
            // 
            // txtInputData
            // 
            this.txtInputData.Location = new System.Drawing.Point(34, 102);
            this.txtInputData.Name = "txtInputData";
            this.txtInputData.Size = new System.Drawing.Size(259, 20);
            this.txtInputData.TabIndex = 1;
            // 
            // lblOriginalData
            // 
            this.lblOriginalData.AutoSize = true;
            this.lblOriginalData.Location = new System.Drawing.Point(34, 35);
            this.lblOriginalData.Name = "lblOriginalData";
            this.lblOriginalData.Size = new System.Drawing.Size(184, 13);
            this.lblOriginalData.TabIndex = 4;
            this.lblOriginalData.Text = "Current Random Data Request Name";
            // 
            // txtOriginalData
            // 
            this.txtOriginalData.BackColor = System.Drawing.SystemColors.Window;
            this.txtOriginalData.Location = new System.Drawing.Point(34, 52);
            this.txtOriginalData.Name = "txtOriginalData";
            this.txtOriginalData.ReadOnly = true;
            this.txtOriginalData.Size = new System.Drawing.Size(259, 20);
            this.txtOriginalData.TabIndex = 5;
            // 
            // RandomDataRequestRenameForm
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(327, 286);
            this.Controls.Add(this.txtOriginalData);
            this.Controls.Add(this.lblOriginalData);
            this.Controls.Add(this.txtInputData);
            this.Controls.Add(this.lblInputData);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "RandomDataRequestRenameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rename Random Data Request";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Label lblInputData;
        private System.Windows.Forms.TextBox txtInputData;
        private System.Windows.Forms.Label lblOriginalData;
        private System.Windows.Forms.TextBox txtOriginalData;
    }
}