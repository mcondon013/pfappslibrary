namespace TestprogTextBoxRuler
{
    partial class ApplicationOptionsForm
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
            this.cmdExit = new System.Windows.Forms.Button();
            this.chkSaveErrorMessagesToErrorLog = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtApplicationLogFileName = new System.Windows.Forms.TextBox();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdApply = new System.Windows.Forms.Button();
            this.cmdHelp = new System.Windows.Forms.Button();
            this.appOptionsMenu = new System.Windows.Forms.MenuStrip();
            this.ShowAllOptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.appOptionsMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdExit
            // 
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdExit.Location = new System.Drawing.Point(507, 303);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(93, 37);
            this.cmdExit.TabIndex = 12;
            this.cmdExit.Text = "E&xit";
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // chkSaveErrorMessagesToErrorLog
            // 
            this.chkSaveErrorMessagesToErrorLog.AutoSize = true;
            this.chkSaveErrorMessagesToErrorLog.Location = new System.Drawing.Point(44, 56);
            this.chkSaveErrorMessagesToErrorLog.Name = "chkSaveErrorMessagesToErrorLog";
            this.chkSaveErrorMessagesToErrorLog.Size = new System.Drawing.Size(185, 17);
            this.chkSaveErrorMessagesToErrorLog.TabIndex = 13;
            this.chkSaveErrorMessagesToErrorLog.Text = "Save Error Messages to Error Log";
            this.chkSaveErrorMessagesToErrorLog.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Application Log File Name:";
            // 
            // txtApplicationLogFileName
            // 
            this.txtApplicationLogFileName.Location = new System.Drawing.Point(44, 120);
            this.txtApplicationLogFileName.Name = "txtApplicationLogFileName";
            this.txtApplicationLogFileName.Size = new System.Drawing.Size(427, 20);
            this.txtApplicationLogFileName.TabIndex = 15;
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(507, 56);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(93, 37);
            this.cmdOK.TabIndex = 20;
            this.cmdOK.Text = "&OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdApply
            // 
            this.cmdApply.Location = new System.Drawing.Point(507, 111);
            this.cmdApply.Name = "cmdApply";
            this.cmdApply.Size = new System.Drawing.Size(93, 37);
            this.cmdApply.TabIndex = 21;
            this.cmdApply.Text = "&Apply";
            this.cmdApply.UseVisualStyleBackColor = true;
            this.cmdApply.Click += new System.EventHandler(this.cmdApply_Click);
            // 
            // cmdHelp
            // 
            this.cmdHelp.Location = new System.Drawing.Point(507, 232);
            this.cmdHelp.Name = "cmdHelp";
            this.cmdHelp.Size = new System.Drawing.Size(93, 37);
            this.cmdHelp.TabIndex = 22;
            this.cmdHelp.Text = "&Help";
            this.cmdHelp.UseVisualStyleBackColor = true;
            this.cmdHelp.Click += new System.EventHandler(this.cmdHelp_Click);
            // 
            // appOptionsMenu
            // 
            this.appOptionsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowAllOptionToolStripMenuItem});
            this.appOptionsMenu.Location = new System.Drawing.Point(0, 0);
            this.appOptionsMenu.Name = "appOptionsMenu";
            this.appOptionsMenu.Size = new System.Drawing.Size(652, 24);
            this.appOptionsMenu.TabIndex = 24;
            this.appOptionsMenu.Text = "menuStrip1";
            // 
            // ShowAllOptionToolStripMenuItem
            // 
            this.ShowAllOptionToolStripMenuItem.Name = "ShowAllOptionToolStripMenuItem";
            this.ShowAllOptionToolStripMenuItem.Size = new System.Drawing.Size(110, 20);
            this.ShowAllOptionToolStripMenuItem.Text = "Show &All Options";
            this.ShowAllOptionToolStripMenuItem.Click += new System.EventHandler(this.ShowAllOptionToolStripMenuItem_Click);
            // 
            // ApplicationOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 375);
            this.Controls.Add(this.cmdHelp);
            this.Controls.Add(this.cmdApply);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.txtApplicationLogFileName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkSaveErrorMessagesToErrorLog);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.appOptionsMenu);
            this.MainMenuStrip = this.appOptionsMenu;
            this.Name = "ApplicationOptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Application Options";
            this.Load += new System.EventHandler(this.AppOptionsForm_Load);
            this.appOptionsMenu.ResumeLayout(false);
            this.appOptionsMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.CheckBox chkSaveErrorMessagesToErrorLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtApplicationLogFileName;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdApply;
        private System.Windows.Forms.Button cmdHelp;
        private System.Windows.Forms.MenuStrip appOptionsMenu;
        private System.Windows.Forms.ToolStripMenuItem ShowAllOptionToolStripMenuItem;
    }
}