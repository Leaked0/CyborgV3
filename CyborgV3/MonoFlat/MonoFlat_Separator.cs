using System.Drawing;
using System.Windows.Forms;

namespace MonoFlat
{
	public class MonoFlat_Separator : Control
	{
		public MonoFlat_Separator()
		{
			SetStyle(ControlStyles.ResizeRedraw, value: true);
			base.Size = (Size)new Point(120, 10);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			e.Graphics.DrawLine(new Pen(Color.FromArgb(45, 57, 68)), 0, 5, base.Width, 5);
		}
	}
}
