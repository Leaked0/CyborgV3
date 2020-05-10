using System.Drawing;
using System.Windows.Forms;

namespace MonoFlat
{
	public class MonoFlat_Label : Label
	{
		public MonoFlat_Label()
		{
			Font = new Font("Segoe UI", 9f);
			ForeColor = Color.FromArgb(116, 125, 132);
			BackColor = Color.Transparent;
		}
	}
}
