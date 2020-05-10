using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MonoFlat
{
	[DefaultEvent("ValueChanged")]
	public class MonoFlat_TrackBar : Control
	{
		public enum ValueDivisor
		{
			By1 = 1,
			By10 = 10,
			By100 = 100,
			By1000 = 1000
		}

		public delegate void ValueChangedEventHandler();

		private Rectangle FillValue;

		private Rectangle PipeBorder;

		private Rectangle TrackBarHandleRect;

		private bool Cap;

		private int ValueDrawer;

		private Size ThumbSize = new Size(14, 14);

		private Rectangle TrackThumb;

		private int _Minimum = 0;

		private int _Maximum = 10;

		private int _Value = 0;

		private bool _JumpToMouse = false;

		private ValueDivisor DividedValue = ValueDivisor.By1;

		private ValueChangedEventHandler ValueChangedEvent;

		public int Minimum
		{
			get
			{
				return _Minimum;
			}
			set
			{
				if (value >= _Maximum)
				{
					value = _Maximum - 10;
				}
				if (_Value < value)
				{
					_Value = value;
				}
				_Minimum = value;
				Invalidate();
			}
		}

		public int Maximum
		{
			get
			{
				return _Maximum;
			}
			set
			{
				if (value <= _Minimum)
				{
					value = _Minimum + 10;
				}
				if (_Value > value)
				{
					_Value = value;
				}
				_Maximum = value;
				Invalidate();
			}
		}

		public int Value
		{
			get
			{
				return _Value;
			}
			set
			{
				if (_Value != value)
				{
					if (value < _Minimum)
					{
						_Value = _Minimum;
					}
					else if (value > _Maximum)
					{
						_Value = _Maximum;
					}
					else
					{
						_Value = value;
					}
					Invalidate();
					if (ValueChangedEvent != null)
					{
						ValueChangedEvent();
					}
				}
			}
		}

		public ValueDivisor ValueDivison
		{
			get
			{
				return DividedValue;
			}
			set
			{
				DividedValue = value;
				Invalidate();
			}
		}

		[Browsable(false)]
		public float ValueToSet
		{
			get
			{
				return _Value / (int)DividedValue;
			}
			set
			{
				Value = (int)(value * (float)DividedValue);
			}
		}

		public bool JumpToMouse
		{
			get
			{
				return _JumpToMouse;
			}
			set
			{
				_JumpToMouse = value;
				Invalidate();
			}
		}

		public event ValueChangedEventHandler ValueChanged
		{
			add
			{
				ValueChangedEvent = (ValueChangedEventHandler)Delegate.Combine(ValueChangedEvent, value);
			}
			remove
			{
				ValueChangedEvent = (ValueChangedEventHandler)Delegate.Remove(ValueChangedEvent, value);
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			checked
			{
				if (Cap && e.X > -1 && e.X < base.Width + 1)
				{
					Value = _Minimum + (int)Math.Round((double)(_Maximum - _Minimum) * ((double)e.X / (double)base.Width));
				}
			}
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (e.Button == MouseButtons.Left)
			{
				ValueDrawer = (int)Math.Round((double)(_Value - _Minimum) / (double)(_Maximum - _Minimum) * (double)(base.Width - 11));
				TrackBarHandleRect = new Rectangle(ValueDrawer, 0, 25, 25);
				Cap = TrackBarHandleRect.Contains(e.Location);
				Focus();
				if (_JumpToMouse)
				{
					Value = _Minimum + (int)Math.Round((double)(_Maximum - _Minimum) * ((double)e.X / (double)base.Width));
				}
			}
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			Cap = false;
		}

		public MonoFlat_TrackBar()
		{
			SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, value: true);
			base.Size = new Size(80, 22);
			MinimumSize = new Size(47, 22);
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			base.Height = 22;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics graphics = e.Graphics;
			graphics.Clear(base.Parent.BackColor);
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			TrackThumb = new Rectangle(7, 10, base.Width - 16, 2);
			PipeBorder = new Rectangle(1, 10, base.Width - 3, 2);
			try
			{
				ValueDrawer = (int)Math.Round((double)(_Value - _Minimum) / (double)(_Maximum - _Minimum) * (double)base.Width);
			}
			catch (Exception)
			{
			}
			TrackBarHandleRect = new Rectangle(ValueDrawer, 0, 3, 20);
			graphics.FillRectangle(new SolidBrush(Color.FromArgb(124, 131, 137)), PipeBorder);
			FillValue = new Rectangle(0, 10, TrackBarHandleRect.X + TrackBarHandleRect.Width - 4, 3);
			graphics.ResetClip();
			graphics.SmoothingMode = SmoothingMode.Default;
			graphics.DrawRectangle(new Pen(Color.FromArgb(124, 131, 137)), PipeBorder);
			graphics.FillRectangle(new SolidBrush(Color.FromArgb(181, 41, 42)), FillValue);
			graphics.ResetClip();
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.FillEllipse(new SolidBrush(Color.FromArgb(181, 41, 42)), TrackThumb.X + (int)Math.Round((double)TrackThumb.Width * ((double)Value / (double)Maximum)) - (int)Math.Round((double)ThumbSize.Width / 2.0), TrackThumb.Y + (int)Math.Round((double)TrackThumb.Height / 2.0) - (int)Math.Round((double)ThumbSize.Height / 2.0), ThumbSize.Width, ThumbSize.Height);
			graphics.DrawEllipse(new Pen(Color.FromArgb(181, 41, 42)), TrackThumb.X + (int)Math.Round((double)TrackThumb.Width * ((double)Value / (double)Maximum)) - (int)Math.Round((double)ThumbSize.Width / 2.0), TrackThumb.Y + (int)Math.Round((double)TrackThumb.Height / 2.0) - (int)Math.Round((double)ThumbSize.Height / 2.0), ThumbSize.Width, ThumbSize.Height);
		}
	}
}
