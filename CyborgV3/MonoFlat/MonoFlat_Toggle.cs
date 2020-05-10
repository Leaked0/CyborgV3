using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace MonoFlat
{
	[DefaultEvent("ToggledChanged")]
	public class MonoFlat_Toggle : Control
	{
		public enum _Type
		{
			CheckMark,
			OnOff,
			YesNo,
			IO
		}

		public delegate void ToggledChangedEventHandler();

		private ToggledChangedEventHandler ToggledChangedEvent;

		private bool _Toggled;

		private _Type ToggleType;

		private Rectangle Bar;

		private int _Width;

		private int _Height;

		public bool Toggled
		{
			get
			{
				return _Toggled;
			}
			set
			{
				_Toggled = value;
				Invalidate();
				if (ToggledChangedEvent != null)
				{
					ToggledChangedEvent();
				}
			}
		}

		public _Type Type
		{
			get
			{
				return ToggleType;
			}
			set
			{
				ToggleType = value;
				Invalidate();
			}
		}

		public event ToggledChangedEventHandler ToggledChanged
		{
			add
			{
				ToggledChangedEvent = (ToggledChangedEventHandler)Delegate.Combine(ToggledChangedEvent, value);
			}
			remove
			{
				ToggledChangedEvent = (ToggledChangedEventHandler)Delegate.Remove(ToggledChangedEvent, value);
			}
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			base.Size = new Size(76, 33);
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			Toggled = !Toggled;
			Focus();
		}

		public MonoFlat_Toggle()
		{
			SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, value: true);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics graphics = e.Graphics;
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.Clear(base.Parent.BackColor);
			_Width = base.Width - 1;
			_Height = base.Height - 1;
			GraphicsPath graphicsPath = null;
			GraphicsPath graphicsPath2 = new GraphicsPath();
			Rectangle rectangle = new Rectangle(0, 0, _Width, _Height);
			Rectangle rectangle2 = new Rectangle(_Width / 2, 0, 38, _Height);
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			graphics.Clear(BackColor);
			graphicsPath = RoundRectangle.RoundRect(rectangle, 4);
			rectangle2 = new Rectangle(4, 4, 36, _Height - 8);
			graphicsPath2 = RoundRectangle.RoundRect(rectangle2, 4);
			graphics.FillPath(new SolidBrush(Color.FromArgb(66, 76, 85)), graphicsPath);
			graphics.FillPath(new SolidBrush(Color.FromArgb(32, 41, 50)), graphicsPath2);
			if (_Toggled)
			{
				graphicsPath = RoundRectangle.RoundRect(rectangle, 4);
				rectangle2 = new Rectangle(_Width / 2 - 2, 4, 36, _Height - 8);
				graphicsPath2 = RoundRectangle.RoundRect(rectangle2, 4);
				graphics.FillPath(new SolidBrush(Color.FromArgb(181, 41, 42)), graphicsPath);
				graphics.FillPath(new SolidBrush(Color.FromArgb(32, 41, 50)), graphicsPath2);
			}
			switch (ToggleType)
			{
			case _Type.CheckMark:
				if (Toggled)
				{
					graphics.DrawString("ü", new Font("Wingdings", 18f, FontStyle.Regular), Brushes.WhiteSmoke, Bar.X + 18, Bar.Y + 19, new StringFormat
					{
						Alignment = StringAlignment.Center,
						LineAlignment = StringAlignment.Center
					});
				}
				else
				{
					graphics.DrawString("r", new Font("Marlett", 14f, FontStyle.Regular), Brushes.DimGray, Bar.X + 59, Bar.Y + 18, new StringFormat
					{
						Alignment = StringAlignment.Center,
						LineAlignment = StringAlignment.Center
					});
				}
				break;
			case _Type.OnOff:
				if (Toggled)
				{
					graphics.DrawString("ON", new Font("Segoe UI", 12f, FontStyle.Regular), Brushes.WhiteSmoke, Bar.X + 18, Bar.Y + 16, new StringFormat
					{
						Alignment = StringAlignment.Center,
						LineAlignment = StringAlignment.Center
					});
				}
				else
				{
					graphics.DrawString("OFF", new Font("Segoe UI", 12f, FontStyle.Regular), Brushes.DimGray, Bar.X + 57, Bar.Y + 16, new StringFormat
					{
						Alignment = StringAlignment.Center,
						LineAlignment = StringAlignment.Center
					});
				}
				break;
			case _Type.YesNo:
				if (Toggled)
				{
					graphics.DrawString("YES", new Font("Segoe UI", 12f, FontStyle.Regular), Brushes.WhiteSmoke, Bar.X + 19, Bar.Y + 16, new StringFormat
					{
						Alignment = StringAlignment.Center,
						LineAlignment = StringAlignment.Center
					});
				}
				else
				{
					graphics.DrawString("NO", new Font("Segoe UI", 12f, FontStyle.Regular), Brushes.DimGray, Bar.X + 56, Bar.Y + 16, new StringFormat
					{
						Alignment = StringAlignment.Center,
						LineAlignment = StringAlignment.Center
					});
				}
				break;
			case _Type.IO:
				if (Toggled)
				{
					graphics.DrawString("I", new Font("Segoe UI", 12f, FontStyle.Regular), Brushes.WhiteSmoke, Bar.X + 18, Bar.Y + 16, new StringFormat
					{
						Alignment = StringAlignment.Center,
						LineAlignment = StringAlignment.Center
					});
				}
				else
				{
					graphics.DrawString("O", new Font("Segoe UI", 12f, FontStyle.Regular), Brushes.DimGray, Bar.X + 57, Bar.Y + 16, new StringFormat
					{
						Alignment = StringAlignment.Center,
						LineAlignment = StringAlignment.Center
					});
				}
				break;
			}
		}
	}
}
