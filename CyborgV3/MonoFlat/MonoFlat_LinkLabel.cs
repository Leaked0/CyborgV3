using System.Drawing;
using System.Windows.Forms;

namespace MonoFlat
{
	public class MonoFlat_LinkLabel : LinkLabel
	{
		public MonoFlat_LinkLabel()
		{
			Font = new Font("Segoe UI", 9f, FontStyle.Regular);
			BackColor = Color.Transparent;
			base.LinkColor = Color.FromArgb(181, 41, 42);
			base.ActiveLinkColor = Color.FromArgb(153, 34, 34);
			base.VisitedLinkColor = Color.FromArgb(181, 41, 42);
			base.LinkBehavior = LinkBehavior.NeverUnderline;
		}
	}
}
