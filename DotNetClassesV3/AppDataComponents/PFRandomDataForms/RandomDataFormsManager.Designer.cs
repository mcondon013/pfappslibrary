namespace PFRandomDataForms
{
    partial class RandomDataFormsManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RandomDataFormsManager));
            this.cmdExit = new System.Windows.Forms.Button();
            this.cmdManage = new System.Windows.Forms.Button();
            this.grpSelections = new System.Windows.Forms.GroupBox();
            this.optRandomDataElements = new System.Windows.Forms.RadioButton();
            this.optRandomBytes = new System.Windows.Forms.RadioButton();
            this.optRandomStrings = new System.Windows.Forms.RadioButton();
            this.optCustomRandomValues = new System.Windows.Forms.RadioButton();
            this.optRandomBooleans = new System.Windows.Forms.RadioButton();
            this.optRandomDatesAndTimes = new System.Windows.Forms.RadioButton();
            this.optRandomWords = new System.Windows.Forms.RadioButton();
            this.optRandomNumbers = new System.Windows.Forms.RadioButton();
            this.optRandomNamesAndLocations = new System.Windows.Forms.RadioButton();
            this.cmdHelp = new System.Windows.Forms.Button();
            this.appHelpProvider = new System.Windows.Forms.HelpProvider();
            this.grpSelections.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdExit
            // 
            this.cmdExit.Location = new System.Drawing.Point(293, 253);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(98, 34);
            this.cmdExit.TabIndex = 0;
            this.cmdExit.Text = "E&xit";
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdManage
            // 
            this.cmdManage.Location = new System.Drawing.Point(293, 63);
            this.cmdManage.Name = "cmdManage";
            this.cmdManage.Size = new System.Drawing.Size(98, 34);
            this.cmdManage.TabIndex = 1;
            this.cmdManage.Text = "&Edit Sources";
            this.cmdManage.UseVisualStyleBackColor = true;
            this.cmdManage.Click += new System.EventHandler(this.cmdManage_Click);
            // 
            // grpSelections
            // 
            this.grpSelections.Controls.Add(this.optRandomDataElements);
            this.grpSelections.Controls.Add(this.optRandomBytes);
            this.grpSelections.Controls.Add(this.optRandomStrings);
            this.grpSelections.Controls.Add(this.optCustomRandomValues);
            this.grpSelections.Controls.Add(this.optRandomBooleans);
            this.grpSelections.Controls.Add(this.optRandomDatesAndTimes);
            this.grpSelections.Controls.Add(this.optRandomWords);
            this.grpSelections.Controls.Add(this.optRandomNumbers);
            this.grpSelections.Controls.Add(this.optRandomNamesAndLocations);
            this.grpSelections.Location = new System.Drawing.Point(23, 24);
            this.grpSelections.Name = "grpSelections";
            this.grpSelections.Size = new System.Drawing.Size(264, 263);
            this.grpSelections.TabIndex = 2;
            this.grpSelections.TabStop = false;
            this.grpSelections.Text = "Select Randomizer Type Sources to Manage";
            // 
            // optRandomDataElements
            // 
            this.optRandomDataElements.AutoSize = true;
            this.optRandomDataElements.Location = new System.Drawing.Point(20, 229);
            this.optRandomDataElements.Name = "optRandomDataElements";
            this.optRandomDataElements.Size = new System.Drawing.Size(137, 17);
            this.optRandomDataElements.TabIndex = 8;
            this.optRandomDataElements.TabStop = true;
            this.optRandomDataElements.Text = "Random Data Elements";
            this.optRandomDataElements.UseVisualStyleBackColor = true;
            // 
            // optRandomBytes
            // 
            this.optRandomBytes.AutoSize = true;
            this.optRandomBytes.Location = new System.Drawing.Point(20, 205);
            this.optRandomBytes.Name = "optRandomBytes";
            this.optRandomBytes.Size = new System.Drawing.Size(94, 17);
            this.optRandomBytes.TabIndex = 7;
            this.optRandomBytes.TabStop = true;
            this.optRandomBytes.Text = "Random Bytes";
            this.optRandomBytes.UseVisualStyleBackColor = true;
            // 
            // optRandomStrings
            // 
            this.optRandomStrings.AutoSize = true;
            this.optRandomStrings.Location = new System.Drawing.Point(20, 181);
            this.optRandomStrings.Name = "optRandomStrings";
            this.optRandomStrings.Size = new System.Drawing.Size(100, 17);
            this.optRandomStrings.TabIndex = 6;
            this.optRandomStrings.TabStop = true;
            this.optRandomStrings.Text = "Random Strings";
            this.optRandomStrings.UseVisualStyleBackColor = true;
            // 
            // optCustomRandomValues
            // 
            this.optCustomRandomValues.AutoSize = true;
            this.optCustomRandomValues.Location = new System.Drawing.Point(20, 158);
            this.optCustomRandomValues.Name = "optCustomRandomValues";
            this.optCustomRandomValues.Size = new System.Drawing.Size(138, 17);
            this.optCustomRandomValues.TabIndex = 5;
            this.optCustomRandomValues.TabStop = true;
            this.optCustomRandomValues.Text = "Custom Random Values";
            this.optCustomRandomValues.UseVisualStyleBackColor = true;
            // 
            // optRandomBooleans
            // 
            this.optRandomBooleans.AutoSize = true;
            this.optRandomBooleans.Location = new System.Drawing.Point(20, 135);
            this.optRandomBooleans.Name = "optRandomBooleans";
            this.optRandomBooleans.Size = new System.Drawing.Size(112, 17);
            this.optRandomBooleans.TabIndex = 4;
            this.optRandomBooleans.TabStop = true;
            this.optRandomBooleans.Text = "Random Booleans";
            this.optRandomBooleans.UseVisualStyleBackColor = true;
            // 
            // optRandomDatesAndTimes
            // 
            this.optRandomDatesAndTimes.AutoSize = true;
            this.optRandomDatesAndTimes.Location = new System.Drawing.Point(20, 111);
            this.optRandomDatesAndTimes.Name = "optRandomDatesAndTimes";
            this.optRandomDatesAndTimes.Size = new System.Drawing.Size(149, 17);
            this.optRandomDatesAndTimes.TabIndex = 3;
            this.optRandomDatesAndTimes.TabStop = true;
            this.optRandomDatesAndTimes.Text = "Random Dates And Times";
            this.optRandomDatesAndTimes.UseVisualStyleBackColor = true;
            // 
            // optRandomWords
            // 
            this.optRandomWords.AutoSize = true;
            this.optRandomWords.Location = new System.Drawing.Point(20, 87);
            this.optRandomWords.Name = "optRandomWords";
            this.optRandomWords.Size = new System.Drawing.Size(99, 17);
            this.optRandomWords.TabIndex = 2;
            this.optRandomWords.TabStop = true;
            this.optRandomWords.Text = "Random Words";
            this.optRandomWords.UseVisualStyleBackColor = true;
            // 
            // optRandomNumbers
            // 
            this.optRandomNumbers.AutoSize = true;
            this.optRandomNumbers.Location = new System.Drawing.Point(20, 63);
            this.optRandomNumbers.Name = "optRandomNumbers";
            this.optRandomNumbers.Size = new System.Drawing.Size(110, 17);
            this.optRandomNumbers.TabIndex = 1;
            this.optRandomNumbers.TabStop = true;
            this.optRandomNumbers.Text = "Random Numbers";
            this.optRandomNumbers.UseVisualStyleBackColor = true;
            // 
            // optRandomNamesAndLocations
            // 
            this.optRandomNamesAndLocations.AutoSize = true;
            this.optRandomNamesAndLocations.Location = new System.Drawing.Point(20, 39);
            this.optRandomNamesAndLocations.Name = "optRandomNamesAndLocations";
            this.optRandomNamesAndLocations.Size = new System.Drawing.Size(172, 17);
            this.optRandomNamesAndLocations.TabIndex = 0;
            this.optRandomNamesAndLocations.TabStop = true;
            this.optRandomNamesAndLocations.Text = "Random Names And Locations";
            this.optRandomNamesAndLocations.UseVisualStyleBackColor = true;
            // 
            // cmdHelp
            // 
            this.cmdHelp.Location = new System.Drawing.Point(293, 150);
            this.cmdHelp.Name = "cmdHelp";
            this.cmdHelp.Size = new System.Drawing.Size(98, 34);
            this.cmdHelp.TabIndex = 3;
            this.cmdHelp.Text = "&Help";
            this.cmdHelp.UseVisualStyleBackColor = true;
            this.cmdHelp.Click += new System.EventHandler(this.cmdHelp_Click);
            // 
            // appHelpProvider
            // 
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\PFRandomDataForms\\InitWinFormsAppWithToolb" +
    "ar\\InitWinFormsHelpFile.chm";
            // 
            // RandomDataFormsManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 318);
            this.Controls.Add(this.cmdHelp);
            this.Controls.Add(this.grpSelections);
            this.Controls.Add(this.cmdManage);
            this.Controls.Add(this.cmdExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RandomDataFormsManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RandomDataFormsManager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PFWindowsForm_FormClosing);
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.grpSelections.ResumeLayout(false);
            this.grpSelections.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.Button cmdManage;
        private System.Windows.Forms.GroupBox grpSelections;
        private System.Windows.Forms.RadioButton optRandomNumbers;
        private System.Windows.Forms.RadioButton optRandomNamesAndLocations;
        private System.Windows.Forms.RadioButton optRandomDataElements;
        private System.Windows.Forms.RadioButton optRandomBytes;
        private System.Windows.Forms.RadioButton optRandomStrings;
        private System.Windows.Forms.RadioButton optCustomRandomValues;
        private System.Windows.Forms.RadioButton optRandomBooleans;
        private System.Windows.Forms.RadioButton optRandomDatesAndTimes;
        private System.Windows.Forms.RadioButton optRandomWords;
        private System.Windows.Forms.Button cmdHelp;
        private System.Windows.Forms.HelpProvider appHelpProvider;
    }
}