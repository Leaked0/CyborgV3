using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MonoFlat
{
	[DefaultEvent("CheckedChanged")]
	public class MonoFlat_RadioButton : Control
	{
		public delegate void CheckedChangedEventHandler(object sender);

		private int X;

		private bool _Checked;

		private CheckedChangedEventHandler CheckedChangedEvent;

		public bool Checked
		{
			get
			{
				return _Checked;
			}
			set
			{
				_Checked = value;
				InvalidateControls();
				if (CheckedChangedEvent != null)
				{
					CheckedChangedEvent(this);
				}
				Invalidate();
			}
		}

		public event CheckedChangedEventHandler CheckedChanged
		{
			add
			{
				CheckedChangedEvent = (CheckedChangedEventHandler)Delegate.Combine(CheckedChangedEvent, value);
			}
			remove
			{
				CheckedChangedEvent = (CheckedChangedEventHandler)Delegate.Remove(CheckedChangedEvent, value);
			}
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (!_Checked)
			{
				Checked = true;
			}
			Focus();
			base.OnMouseDown(e);
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			X = e.X;
			Invalidate();
		}

		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			int num = 0;
			num = (int)CreateGraphics().MeasureString(Text, Font).Width;
			base.Width = 28 + num;
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			base.Height = 17;
		}

		public MonoFlat_RadioButton()
		{
			base.Width = 159;
			base.Height = 17;
			DoubleBuffered = true;
		}

		private void InvalidateControls()
		{
			if (base.IsHandleCreated && _Checked)
			{
				foreach (Control control in base.Parent.Controls)
				{
					if (control != this && control is MonoFlat_RadioButton)
					{
						((MonoFlat_RadioButton)control).Checked = false;
					}
				}
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics graphics = e.Graphics;
			graphics.Clear(base.Parent.BackColor);
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.FillEllipse(new SolidBrush(Color.FromArgb(66, 76, 85)), new Rectangle(0, 0, 16, 16));
			if (_Checked)
			{
				graphics.DrawString("a", new Font("Marlett", 15f), new SolidBrush(Color.FromArgb(181, 41, 42)), new Point(-3, -2));
			}
			graphics.DrawString(Text, Font, new SolidBrush(Color.FromArgb(116, 125, 132)), new Point(20, 0));
		}
	}
}
