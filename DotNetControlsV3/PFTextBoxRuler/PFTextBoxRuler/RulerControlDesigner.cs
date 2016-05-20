using System;

namespace RulerControl
{
	/// <summary>
	/// Summary description for RulerControlDesigner.
	/// </summary>
	public class RulerControlDesigner : System.Windows.Forms.Design.ScrollableControlDesigner
	{
		/// <summary>
		/// Default constructor for RulerControlDesigner.
		/// </summary>
		public RulerControlDesigner()
		{
		}

		/// <summary>
		/// Customized override to remove unsupported properties.
		/// </summary>
		/// <param name="properties"></param>
		protected override void PreFilterProperties(System.Collections.IDictionary properties)
		{
			//base.PreFilterProperties (properties);
			properties.Remove("AllowDrop");
			properties.Remove("BackgroundImage");
			//properties.Remove("AutoScroll");
			//properties.Remove("AutoScrollMargin");
			//properties.Remove("AutoScrollMinSize");
			properties.Remove("Font");

		}
	}
}
