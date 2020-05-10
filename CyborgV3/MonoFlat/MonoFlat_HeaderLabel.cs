using System.Drawing;
using System.Windows.Forms;

namespace MonoFlat
{
	public class MonoFlat_HeaderLabel : Label
	{
		public MonoFlat_HeaderLabel()
		{
			Font = new Font("Segoe UI", 11f, FontStyle.Bold);
			ForeColor = Color.FromArgb(255, 255, 255);
			BackColor = Color.Transparent;
		}
	}
}
