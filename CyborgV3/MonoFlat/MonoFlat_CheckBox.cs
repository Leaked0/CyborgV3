using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MonoFlat
{
	[DefaultEvent("CheckedChanged")]
	public class MonoFlat_CheckBox : Control
	{
		public delegate void CheckedChangedEventHandler(object sender);

		private int X;

		private bool _Checked = false;

		private GraphicsPath Shape;

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

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			X = e.Location.X;
			Invalidate();
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			_Checked = !_Checked;
			Focus();
			if (CheckedChangedEvent != null)
			{
				CheckedChangedEvent(this);
			}
			base.OnMouseDown(e);
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			base.Height = 16;
			Shape = new GraphicsPath();
			Shape.AddArc(0, 0, 10, 10, 180f, 90f);
			Shape.AddArc(base.Width - 11, 0, 10, 10, -90f, 90f);
			Shape.AddArc(base.Width - 11, base.Height - 11, 10, 10, 0f, 90f);
			Shape.AddArc(0, base.Height - 11, 10, 10, 90f, 90f);
			Shape.CloseAllFigures();
			Invalidate();
		}

		public MonoFlat_CheckBox()
		{
			base.Width = 148;
			base.Height = 16;
			Font = new Font("Microsoft Sans Serif", 9f);
			DoubleBuffered = true;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics graphics = e.Graphics;
			graphics.Clear(base.Parent.BackColor);
			if (_Checked)
			{
				graphics.FillRectangle(new SolidBrush(Color.FromArgb(66, 76, 85)), new Rectangle(0, 0, 16, 16));
				graphics.FillRectangle(new SolidBrush(Color.FromArgb(66, 76, 85)), new Rectangle(1, 1, 14, 14));
			}
			else
			{
				graphics.FillRectangle(new SolidBrush(Color.FromArgb(66, 76, 85)), new Rectangle(0, 0, 16, 16));
				graphics.FillRectangle(new SolidBrush(Color.FromArgb(66, 76, 85)), new Rectangle(1, 1, 14, 14));
			}
			if (base.Enabled)
			{
				if (_Checked)
				{
					graphics.DrawString("a", new Font("Marlett", 16f), new SolidBrush(Color.FromArgb(181, 41, 42)), new Point(-5, -3));
				}
			}
			else if (_Checked)
			{
				graphics.DrawString("a", new Font("Marlett", 16f), new SolidBrush(Color.Gray), new Point(-5, -3));
			}
			graphics.DrawString(Text, Font, new SolidBrush(Color.FromArgb(116, 125, 132)), new Point(20, 0));
		}
	}
}
