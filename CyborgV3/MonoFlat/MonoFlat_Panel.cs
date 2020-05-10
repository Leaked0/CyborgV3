using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MonoFlat
{
	public class MonoFlat_Panel : ContainerControl
	{
		private GraphicsPath Shape;

		public MonoFlat_Panel()
		{
			SetStyle(ControlStyles.SupportsTransparentBackColor, value: true);
			SetStyle(ControlStyles.UserPaint, value: true);
			BackColor = Color.FromArgb(39, 51, 63);
			base.Size = new Size(187, 117);
			base.Padding = new Padding(5, 5, 5, 5);
			DoubleBuffered = true;
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			Shape = new GraphicsPath();
			Shape.AddArc(0, 0, 10, 10, 180f, 90f);
			Shape.AddArc(base.Width - 11, 0, 10, 10, -90f, 90f);
			Shape.AddArc(base.Width - 11, base.Height - 11, 10, 10, 0f, 90f);
			Shape.AddArc(0, base.Height - 11, 10, 10, 90f, 90f);
			Shape.CloseAllFigures();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Bitmap bitmap = new Bitmap(base.Width, base.Height);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.Clear(Color.FromArgb(32, 41, 50));
			graphics.FillPath(new SolidBrush(Color.FromArgb(39, 51, 63)), Shape);
			graphics.DrawPath(new Pen(Color.FromArgb(39, 51, 63)), Shape);
			graphics.Dispose();
			e.Graphics.DrawImage((Image)bitmap.Clone(), 0, 0);
			bitmap.Dispose();
		}
	}
}
