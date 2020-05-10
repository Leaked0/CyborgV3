using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MonoFlat
{
	public class MonoFlat_SocialButton : Control
	{
		private Image _Image;

		private Size _ImageSize;

		private Color EllipseColor;

		public Image Image
		{
			get
			{
				return _Image;
			}
			set
			{
				if (value == null)
				{
					_ImageSize = Size.Empty;
				}
				else
				{
					_ImageSize = value.Size;
				}
				_Image = value;
				Invalidate();
			}
		}

		protected Size ImageSize => _ImageSize;

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			base.Size = new Size(54, 54);
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			EllipseColor = Color.FromArgb(181, 41, 42);
			Refresh();
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			EllipseColor = Color.FromArgb(66, 76, 85);
			Refresh();
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			EllipseColor = Color.FromArgb(153, 34, 34);
			Focus();
			Refresh();
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			EllipseColor = Color.FromArgb(181, 41, 42);
			Refresh();
		}

		private static PointF ImageLocation(StringFormat SF, SizeF Area, SizeF ImageArea)
		{
			PointF result = default(PointF);
			StringAlignment alignment = SF.Alignment;
			if (alignment == StringAlignment.Center)
			{
				result.X = (Area.Width - ImageArea.Width) / 2f;
			}
			alignment = SF.LineAlignment;
			if (alignment == StringAlignment.Center)
			{
				result.Y = (Area.Height - ImageArea.Height) / 2f;
			}
			return result;
		}

		private StringFormat GetStringFormat(ContentAlignment _ContentAlignment)
		{
			StringFormat stringFormat = new StringFormat();
			if (_ContentAlignment == ContentAlignment.MiddleCenter)
			{
				stringFormat.LineAlignment = StringAlignment.Center;
				stringFormat.Alignment = StringAlignment.Center;
			}
			return stringFormat;
		}

		public MonoFlat_SocialButton()
		{
			DoubleBuffered = true;
			EllipseColor = Color.FromArgb(66, 76, 85);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			graphics.Clear(base.Parent.BackColor);
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			PointF pointF = ImageLocation(GetStringFormat(ContentAlignment.MiddleCenter), base.Size, ImageSize);
			graphics.FillEllipse(new SolidBrush(EllipseColor), new Rectangle(0, 0, 53, 53));
			if (Image != null)
			{
				graphics.DrawImage(_Image, (int)pointF.X, (int)pointF.Y, ImageSize.Width, ImageSize.Height);
			}
		}
	}
}
