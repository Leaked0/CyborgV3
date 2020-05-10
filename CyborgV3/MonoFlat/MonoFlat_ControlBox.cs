using System;
using System.Drawing;
using System.Windows.Forms;

namespace MonoFlat
{
	internal class MonoFlat_ControlBox : Control
	{
		public enum ButtonHoverState
		{
			Minimize,
			Maximize,
			Close,
			None
		}

		private ButtonHoverState ButtonHState = ButtonHoverState.None;

		private bool _EnableMaximize = true;

		private bool _EnableMinimize = true;

		private bool _EnableHoverHighlight = false;

		public bool EnableMaximizeButton
		{
			get
			{
				return _EnableMaximize;
			}
			set
			{
				_EnableMaximize = value;
				Invalidate();
			}
		}

		public bool EnableMinimizeButton
		{
			get
			{
				return _EnableMinimize;
			}
			set
			{
				_EnableMinimize = value;
				Invalidate();
			}
		}

		public bool EnableHoverHighlight
		{
			get
			{
				return _EnableHoverHighlight;
			}
			set
			{
				_EnableHoverHighlight = value;
				Invalidate();
			}
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			base.Size = new Size(100, 25);
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			int x = e.Location.X;
			int y = e.Location.Y;
			if (y > 0 && y < base.Height - 2)
			{
				if (x > 0 && x < 34)
				{
					ButtonHState = ButtonHoverState.Minimize;
				}
				else if (x > 33 && x < 65)
				{
					ButtonHState = ButtonHoverState.Maximize;
				}
				else if (x > 64 && x < base.Width)
				{
					ButtonHState = ButtonHoverState.Close;
				}
				else
				{
					ButtonHState = ButtonHoverState.None;
				}
			}
			else
			{
				ButtonHState = ButtonHoverState.None;
			}
			Invalidate();
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			switch (ButtonHState)
			{
			case ButtonHoverState.Close:
				base.Parent.FindForm().Close();
				break;
			case ButtonHoverState.Minimize:
				if (_EnableMinimize)
				{
					base.Parent.FindForm().WindowState = FormWindowState.Minimized;
				}
				break;
			case ButtonHoverState.Maximize:
				if (_EnableMaximize)
				{
					if (base.Parent.FindForm().WindowState == FormWindowState.Normal)
					{
						base.Parent.FindForm().WindowState = FormWindowState.Maximized;
					}
					else
					{
						base.Parent.FindForm().WindowState = FormWindowState.Normal;
					}
				}
				break;
			}
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			ButtonHState = ButtonHoverState.None;
			Invalidate();
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			Focus();
		}

		public MonoFlat_ControlBox()
		{
			DoubleBuffered = true;
			Anchor = (AnchorStyles.Top | AnchorStyles.Right);
		}

		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			try
			{
				base.Location = new Point(base.Parent.Width - 112, 15);
			}
			catch (Exception)
			{
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics graphics = e.Graphics;
			graphics.Clear(Color.FromArgb(181, 41, 42));
			if (_EnableHoverHighlight)
			{
				switch (ButtonHState)
				{
				case ButtonHoverState.None:
					graphics.Clear(Color.FromArgb(181, 41, 42));
					break;
				case ButtonHoverState.Minimize:
					if (_EnableMinimize)
					{
						graphics.FillRectangle(new SolidBrush(Color.FromArgb(156, 35, 35)), new Rectangle(3, 0, 30, base.Height));
					}
					break;
				case ButtonHoverState.Maximize:
					if (_EnableMaximize)
					{
						graphics.FillRectangle(new SolidBrush(Color.FromArgb(156, 35, 35)), new Rectangle(35, 0, 30, base.Height));
					}
					break;
				case ButtonHoverState.Close:
					graphics.FillRectangle(new SolidBrush(Color.FromArgb(156, 35, 35)), new Rectangle(66, 0, 35, base.Height));
					break;
				}
			}
			graphics.DrawString("r", new Font("Marlett", 12f), new SolidBrush(Color.FromArgb(255, 254, 255)), new Point(base.Width - 16, 8), new StringFormat
			{
				Alignment = StringAlignment.Center
			});
			switch (base.Parent.FindForm().WindowState)
			{
			case FormWindowState.Maximized:
				if (_EnableMaximize)
				{
					graphics.DrawString("2", new Font("Marlett", 12f), new SolidBrush(Color.FromArgb(255, 254, 255)), new Point(51, 7), new StringFormat
					{
						Alignment = StringAlignment.Center
					});
				}
				else
				{
					graphics.DrawString("2", new Font("Marlett", 12f), new SolidBrush(Color.LightGray), new Point(51, 7), new StringFormat
					{
						Alignment = StringAlignment.Center
					});
				}
				break;
			case FormWindowState.Normal:
				if (_EnableMaximize)
				{
					graphics.DrawString("1", new Font("Marlett", 12f), new SolidBrush(Color.FromArgb(255, 254, 255)), new Point(51, 7), new StringFormat
					{
						Alignment = StringAlignment.Center
					});
				}
				else
				{
					graphics.DrawString("1", new Font("Marlett", 12f), new SolidBrush(Color.LightGray), new Point(51, 7), new StringFormat
					{
						Alignment = StringAlignment.Center
					});
				}
				break;
			}
			if (_EnableMinimize)
			{
				graphics.DrawString("0", new Font("Marlett", 12f), new SolidBrush(Color.FromArgb(255, 254, 255)), new Point(20, 7), new StringFormat
				{
					Alignment = StringAlignment.Center
				});
			}
			else
			{
				graphics.DrawString("0", new Font("Marlett", 12f), new SolidBrush(Color.LightGray), new Point(20, 7), new StringFormat
				{
					Alignment = StringAlignment.Center
				});
			}
		}
	}
}
