using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RulerControl
{
	/// <summary>
	/// Customized user control that allows a user to load a line of text into the control and draw lines between characters they wish.
	/// The offsets of these lines are accessed by the FieldOffsets property.
    /// Code was originally downloaded from SourceForge at http://sourceforge.net/projects/netrulercontrol/.
    /// Code was extensively modified to allow for scrolling of text to be parsed and display of multiple lines.
	/// </summary>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(RulerControl))]
	[DesignerAttribute(typeof(RulerControlDesigner))]
	public class RulerControl : System.Windows.Forms.UserControl
	{
		private ArrayList _myLines;
		private System.Drawing.Pen _myPen;
		private System.Drawing.Pen _myBluePen;
		private Font _myFont;
		private SolidBrush _myBrush;
		private StringFormat _myStringFormat;
		private PictureBox objLine;
		private string _strValue = "";
		internal System.Windows.Forms.TextBox txtMain;
        internal System.Windows.Forms.Panel panel1;
        private Panel panel2;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Default constructor for RulerControl.
		/// </summary>
		public RulerControl()
		{
			InitializeComponent();
		}


		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}


		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.txtMain = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMain
            // 
            this.txtMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMain.BackColor = System.Drawing.Color.White;
            this.txtMain.CausesValidation = false;
            this.txtMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtMain.Font = new System.Drawing.Font("Lucida Console", 11.25F);
            this.txtMain.Location = new System.Drawing.Point(0, 3);
            this.txtMain.Multiline = true;
            this.txtMain.Name = "txtMain";
            this.txtMain.ReadOnly = true;
            this.txtMain.Size = new System.Drawing.Size(230, 72);
            this.txtMain.TabIndex = 0;
            this.txtMain.TabStop = false;
            this.txtMain.Text = "                                                          ";
            this.txtMain.WordWrap = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(233, 132);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel2.Controls.Add(this.txtMain);
            this.panel2.Location = new System.Drawing.Point(3, 35);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(230, 77);
            this.panel2.TabIndex = 0;
            // 
            // RulerControl
            // 
            this.Controls.Add(this.panel1);
            this.Name = "RulerControl";
            this.Size = new System.Drawing.Size(233, 138);
            this.Load += new System.EventHandler(this.RulerControl_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		private void RulerControl_Load(object sender, System.EventArgs e)
		{
			_myFont = new Font("Lucida Console", 8);
			_myBrush = new SolidBrush(Color.Black);
			_myStringFormat = new StringFormat();
			_myStringFormat.FormatFlags = StringFormatFlags.NoWrap;
			_myPen = new System.Drawing.Pen(Color.Black);
			_myPen.ResetTransform();
            _myPen.Width = 1;
            _myPen.StartCap = LineCap.Round;
            _myPen.EndCap = LineCap.Round;
            _myPen.DashStyle = DashStyle.Solid;

			_myBluePen = new System.Drawing.Pen(Color.Blue);
			_myBluePen.ResetTransform();
            _myBluePen.Width = 1;
            _myBluePen.StartCap = LineCap.ArrowAnchor;
            _myBluePen.EndCap = LineCap.Round;
            _myBluePen.DashStyle = DashStyle.Solid;

			_myLines = new ArrayList();
			DrawLines();	
		}

		private void DrawLines()
		{
			int i;
			System.Drawing.Point dp;

			try
			{
				this.panel1.Controls.Clear();
				for(i=0; i<this.txtMain.Text.Length; i++)
				{
					objLine = new PictureBox();
					objLine.Width = 7;
					objLine.Height = 25;
					objLine.Tag = Convert.ToString(i+1).Trim();
					objLine.Click += new EventHandler(LineClickHandler);
					//objLine.Paint += new EventHandler(PictureBox_Paint);
					objLine.Paint += new PaintEventHandler(objLine_Paint);
					this.panel1.Controls.Add(objLine);

					if((i+1) % 10 == 0)
					{
						objLine = new PictureBox();
						objLine.Width = 30;
						objLine.Height = 10;
						objLine.Tag = "A" + Convert.ToString(i+1).Trim();
						objLine.Paint += new PaintEventHandler(PictureBoxNumbers_Paint);
						this.panel1.Controls.Add(objLine);
					}

					foreach(PictureBox objControl in this.panel1.Controls)
					{
						if(objControl.Tag.ToString().Trim().Substring(0,1) == "A")
						{
							dp = new System.Drawing.Point((Convert.ToInt32(objControl.Tag.ToString().Trim().Substring(1)) * 9) - 15, 0);
							objControl.Location = dp;
						}
						else
						{
							dp = new System.Drawing.Point((Convert.ToInt32(objControl.Tag) * 9) - 6, 9);
							objControl.Location = dp;
						}
						objControl.Update();
						this.panel1.Update();
					}
                }
                this.panel1.Controls.Add(this.panel2);
            }
			catch(Exception ex)
			{
				throw ex;
			}
		}

		private void LineClickHandler(Object sender, System.EventArgs e)
		{
			int intPos;
			PictureBox objBlueLine;
			System.Drawing.Point dp;

			if(((PictureBox) sender).Tag.ToString().Trim().Substring(0,1) == "A")
				intPos = Convert.ToInt32(((PictureBox) sender).Tag.ToString().Trim().Substring(1));
			else
				intPos = Convert.ToInt32(((PictureBox) sender).Tag.ToString().Trim());

			if(_myLines.Contains(intPos))
			{
				_myLines.Remove(intPos);
				foreach(PictureBox objBlueLineObj in this.txtMain.Controls)
				{
					if(objBlueLineObj.Tag.ToString() == "BL" + ((PictureBox) sender).Tag.ToString().Trim())
					{
						this.txtMain.Controls.Remove(objBlueLineObj);
						break;
					}
				}
			}
			else
			{
				_myLines.Add(intPos);
				objBlueLine = new PictureBox();
				objBlueLine.BackColor = Color.Blue;
				objBlueLine.Width = 1;
				objBlueLine.Height = this.txtMain.Height;
				objBlueLine.Tag = "BL" + ((PictureBox) sender).Tag.ToString().Trim();

				this.txtMain.Controls.Add(objBlueLine);
				foreach(PictureBox objBlueLineObj in this.txtMain.Controls)
				{
					if(objBlueLineObj.Tag == objBlueLine.Tag)
					{
						dp = new System.Drawing.Point((Convert.ToInt32(objBlueLine.Tag.ToString().Substring(2)) * 9) - 7, 0);
						objBlueLineObj.Location = dp;
						objBlueLineObj.Update();
						this.txtMain.Update();
					}
				}
			}
				((PictureBox) sender).Invalidate();
		}

		private void PictureBoxNumbers_Paint(Object sender, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.Clear(((PictureBox) sender).BackColor);
			e.Graphics.DrawString(((PictureBox) sender).Tag.ToString().Trim().Substring(1), _myFont, _myBrush, 1, 1);
		}

		private void objLine_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(((PictureBox) sender).BackColor);
			if(_myLines.Contains(Convert.ToInt32(((PictureBox) sender).Tag)))
				e.Graphics.DrawLine(_myBluePen, 1, 2, 1, 25);
			else
			{
				if(Convert.ToInt32(((PictureBox) sender).Tag) % 10 == 0)
					e.Graphics.DrawLine(_myPen, 1, 2, 1, 25);
				else
					e.Graphics.DrawLine(_myPen, 1, 8, 1, 25);
			}
		}


		/// <summary>
		/// Default Paint event for Ruler Control.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPaint(PaintEventArgs e)
		{
			int i;
			e.Graphics.Clear(this.BackColor);
			for(i=1; i <= this.txtMain.Text.Length; i++)
			{
				if(i % 10 == 0)
					e.Graphics.DrawString(i.ToString().Trim(), _myFont, _myBrush, (i * 9) - 7, 5);
			}
			base.OnPaint (e);
		}


		/// <summary>
		/// Returns or sets the text value of the RulerControl
		/// </summary>
		[Bindable(true), Category("Data"), DefaultValue(""), 
		Description("Returns or sets the text value displayed within the Ruler Control.")]
		public string RulerText
		{
			get
			{
				return _strValue;
			}
			set
			{
				if(value != null)
					_strValue = value.ToString() + " "; //extra space added to adjust end of line just past end of ruler
				else
					_strValue = "";
				if(_myLines != null)
					_myLines.Clear();
				this.txtMain.Text = _strValue;
                //this.label1.Text = _strValue;
				//this.txtMain.Size = new Size(this.txtMain.Text.Length * 9, 48);
				//this.panel1.Width = this.txtMain.Width;
				this.txtMain.Controls.Clear();
				DrawLines();
                //this.panel1.Width = this.txtMain.Text.Length * 15;
                this.panel2.Width = this.txtMain.Text.Length * 11;
                this.txtMain.Width = this.panel2.Width;
			}
		}


        /// <summary>
        /// Returns or sets the text value of the RulerControl
        /// </summary>
        [Bindable(true), Category("Data"), DefaultValue(""),
        Description("Returns or sets the text value displayed on lines 2 to n within the Ruler Control.")]
        public string RulerTextLinesExtra
        {
            get
            {
                return _strValue;
            }
            set
            {
                if (value != null)
                    _strValue = value.ToString() + " "; //extra space added to adjust end of line just past end of ruler
                else
                    _strValue = "";
                if (_myLines != null)
                    _myLines.Clear();
                this.txtMain.Text += Environment.NewLine;
                this.txtMain.Text += _strValue;
            }
        }
        /// <summary>
		/// Returns an integer ArrayList of all of the positions in the Text value that have been marked with a blue vertical line.
		/// </summary>
		[Bindable(true), Category("Data"), 
		Description("Returns ArrayList of integer values specifying all of the positions within the RulerText that has been marked with blue vertical line")]
		public ArrayList FieldOffSets
		{
			get
			{
				_myLines.Sort();
				return _myLines;
			}
		}

        /// <summary>
        /// Windows scroll bars.
        /// </summary>
        public System.Windows.Forms.ScrollBars TextScrollBars
        {
            get
            {
                return this.txtMain.ScrollBars;
            }
            set
            {
                this.txtMain.ScrollBars = value;
            }
        }
	}
}
