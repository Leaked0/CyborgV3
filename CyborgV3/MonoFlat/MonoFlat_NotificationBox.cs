using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace MonoFlat
{
	public class MonoFlat_NotificationBox : Control
	{
		public enum Type
		{
			Notice,
			Success,
			Warning,
			Error
		}

		private Point CloseCoordinates;

		private bool IsOverClose;

		private int _BorderCurve = 8;

		private GraphicsPath CreateRoundPath;

		private string NotificationText = null;

		private Type _NotificationType;

		private bool _RoundedCorners;

		private bool _ShowCloseButton;

		private Image _Image;

		private Size _ImageSize;

		public Type NotificationType
		{
			get
			{
				return _NotificationType;
			}
			set
			{
				_NotificationType = value;
				Invalidate();
			}
		}

		public bool RoundCorners
		{
			get
			{
				return _RoundedCorners;
			}
			set
			{
				_RoundedCorners = value;
				Invalidate();
			}
		}

		public bool ShowCloseButton
		{
			get
			{
				return _ShowCloseButton;
			}
			set
			{
				_ShowCloseButton = value;
				Invalidate();
			}
		}

		public int BorderCurve
		{
			get
			{
				return _BorderCurve;
			}
			set
			{
				_BorderCurve = value;
				Invalidate();
			}
		}

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

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (e.X >= base.Width - 19 && e.X <= base.Width - 10 && e.Y > CloseCoordinates.Y && e.Y < CloseCoordinates.Y + 12)
			{
				IsOverClose = true;
			}
			else
			{
				IsOverClose = false;
			}
			Invalidate();
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (_ShowCloseButton && IsOverClose)
			{
				Dispose();
			}
		}

		internal GraphicsPath CreateRoundRect(Rectangle r, int curve)
		{
			try
			{
				CreateRoundPath = new GraphicsPath(FillMode.Winding);
				CreateRoundPath.AddArc(r.X, r.Y, curve, curve, 180f, 90f);
				CreateRoundPath.AddArc(r.Right - curve, r.Y, curve, curve, 270f, 90f);
				CreateRoundPath.AddArc(r.Right - curve, r.Bottom - curve, curve, curve, 0f, 90f);
				CreateRoundPath.AddArc(r.X, r.Bottom - curve, curve, curve, 90f, 90f);
				CreateRoundPath.CloseFigure();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + Environment.NewLine + Environment.NewLine + "Value must be either '1' or higher", "Invalid Integer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				_BorderCurve = 8;
				BorderCurve = 8;
			}
			return CreateRoundPath;
		}

		public MonoFlat_NotificationBox()
		{
			SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value: true);
			Font = new Font("Tahoma", 9f);
			MinimumSize = new Size(100, 40);
			RoundCorners = false;
			ShowCloseButton = true;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics graphics = e.Graphics;
			Color color = default(Color);
			Color color2 = default(Color);
			Color color3 = default(Color);
			Font font = new Font(Font.FontFamily, Font.Size, FontStyle.Bold);
			Rectangle rectangle = new Rectangle(0, 0, base.Width - 1, base.Height - 1);
			GraphicsPath graphicsPath = CreateRoundRect(rectangle, _BorderCurve);
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			graphics.Clear(base.Parent.BackColor);
			switch (_NotificationType)
			{
			case Type.Notice:
				color2 = Color.FromArgb(111, 177, 199);
				color3 = Color.FromArgb(111, 177, 199);
				color = Color.White;
				break;
			case Type.Success:
				color2 = Color.FromArgb(91, 195, 162);
				color3 = Color.FromArgb(91, 195, 162);
				color = Color.White;
				break;
			case Type.Warning:
				color2 = Color.FromArgb(254, 209, 108);
				color3 = Color.FromArgb(254, 209, 108);
				color = Color.DimGray;
				break;
			case Type.Error:
				color2 = Color.FromArgb(217, 103, 93);
				color3 = Color.FromArgb(217, 103, 93);
				color = Color.White;
				break;
			}
			if (_RoundedCorners)
			{
				graphics.FillPath(new SolidBrush(color2), graphicsPath);
				graphics.DrawPath(new Pen(color3), graphicsPath);
			}
			else
			{
				graphics.FillRectangle(new SolidBrush(color2), rectangle);
				graphics.DrawRectangle(new Pen(color3), rectangle);
			}
			switch (_NotificationType)
			{
			case Type.Notice:
				NotificationText = "NOTICE";
				break;
			case Type.Success:
				NotificationText = "SUCCESS";
				break;
			case Type.Warning:
				NotificationText = "WARNING";
				break;
			case Type.Error:
				NotificationText = "ERROR";
				break;
			}
			if (Image == null)
			{
				graphics.DrawString(NotificationText, font, new SolidBrush(color), new Point(10, 5));
				graphics.DrawString(Text, Font, new SolidBrush(color), new Rectangle(10, 21, base.Width - 17, base.Height - 5));
			}
			else
			{
				graphics.DrawImage(_Image, 12, 4, 16, 16);
				graphics.DrawString(NotificationText, font, new SolidBrush(color), new Point(30, 5));
				graphics.DrawString(Text, Font, new SolidBrush(color), new Rectangle(10, 21, base.Width - 17, base.Height - 5));
			}
			CloseCoordinates = new Point(base.Width - 26, 4);
			if (_ShowCloseButton)
			{
				graphics.DrawString("r", new Font("Marlett", 7f, FontStyle.Regular), new SolidBrush(Color.FromArgb(130, 130, 130)), new Rectangle(base.Width - 20, 10, base.Width, base.Height), new StringFormat
				{
					Alignment = StringAlignment.Near,
					LineAlignment = StringAlignment.Near
				});
			}
			graphicsPath.Dispose();
		}
	}
}
