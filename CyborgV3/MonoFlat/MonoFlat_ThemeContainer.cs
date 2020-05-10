using System;
using System.Drawing;
using System.Windows.Forms;

namespace MonoFlat
{
	public class MonoFlat_ThemeContainer : ContainerControl
	{
		public enum MouseState
		{
			None,
			Over,
			Down,
			Block
		}

		private Rectangle HeaderRect;

		protected MouseState State;

		private int MoveHeight;

		private Point MouseP = new Point(0, 0);

		private bool Cap = false;

		private bool HasShown;

		private bool _Sizable = true;

		private bool _SmartBounds = true;

		private bool _RoundCorners = true;

		private bool _IsParentForm;

		private bool _ControlMode;

		private FormStartPosition _StartPosition;

		private Point GetIndexPoint;

		private bool B1x;

		private bool B2x;

		private bool B3;

		private bool B4;

		private int Current;

		private int Previous;

		private Message[] Messages = new Message[9];

		private bool WM_LMBUTTONDOWN;

		public bool Sizable
		{
			get
			{
				return _Sizable;
			}
			set
			{
				_Sizable = value;
			}
		}

		public bool SmartBounds
		{
			get
			{
				return _SmartBounds;
			}
			set
			{
				_SmartBounds = value;
			}
		}

		public bool RoundCorners
		{
			get
			{
				return _RoundCorners;
			}
			set
			{
				_RoundCorners = value;
				Invalidate();
			}
		}

		protected bool IsParentForm => _IsParentForm;

		protected bool IsParentMdi
		{
			get
			{
				if (base.Parent == null)
				{
					return false;
				}
				return base.Parent.Parent != null;
			}
		}

		protected bool ControlMode
		{
			get
			{
				return _ControlMode;
			}
			set
			{
				_ControlMode = value;
				Invalidate();
			}
		}

		public FormStartPosition StartPosition
		{
			get
			{
				if (_IsParentForm && !_ControlMode)
				{
					return base.ParentForm.StartPosition;
				}
				return _StartPosition;
			}
			set
			{
				_StartPosition = value;
				if (_IsParentForm && !_ControlMode)
				{
					base.ParentForm.StartPosition = value;
				}
			}
		}

		protected sealed override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
			if (base.Parent == null)
			{
				return;
			}
			_IsParentForm = (base.Parent is Form);
			if (_ControlMode)
			{
				return;
			}
			InitializeMessages();
			if (_IsParentForm)
			{
				base.ParentForm.FormBorderStyle = FormBorderStyle.None;
				base.ParentForm.TransparencyKey = Color.Fuchsia;
				if (!base.DesignMode)
				{
					base.ParentForm.Shown += FormShown;
				}
			}
			base.Parent.BackColor = BackColor;
		}

		protected sealed override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			if (!_ControlMode)
			{
				HeaderRect = new Rectangle(0, 0, base.Width - 14, MoveHeight - 7);
			}
			Invalidate();
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			Focus();
			if (e.Button == MouseButtons.Left)
			{
				SetState(MouseState.Down);
			}
			if ((!_IsParentForm || base.ParentForm.WindowState != FormWindowState.Maximized) && !_ControlMode)
			{
				if (HeaderRect.Contains(e.Location))
				{
					base.Capture = false;
					WM_LMBUTTONDOWN = true;
					DefWndProc(ref Messages[0]);
				}
				else if (_Sizable && Previous != 0)
				{
					base.Capture = false;
					WM_LMBUTTONDOWN = true;
					DefWndProc(ref Messages[Previous]);
				}
			}
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			Cap = false;
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if ((!_IsParentForm || base.ParentForm.WindowState != FormWindowState.Maximized) && _Sizable && !_ControlMode)
			{
				InvalidateMouse();
			}
			if (Cap)
			{
				base.Parent.Location = (Point)(object)(Convert.ToDouble(Control.MousePosition) - Convert.ToDouble(MouseP));
			}
		}

		protected override void OnInvalidated(InvalidateEventArgs e)
		{
			base.OnInvalidated(e);
			base.ParentForm.Text = Text;
		}

		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);
		}

		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			Invalidate();
		}

		private void FormShown(object sender, EventArgs e)
		{
			if (!_ControlMode && !HasShown)
			{
				if (_StartPosition == FormStartPosition.CenterParent || _StartPosition == FormStartPosition.CenterScreen)
				{
					Rectangle bounds = Screen.PrimaryScreen.Bounds;
					Rectangle bounds2 = base.ParentForm.Bounds;
					base.ParentForm.Location = new Point(bounds.Width / 2 - bounds2.Width / 2, bounds.Height / 2 - bounds2.Width / 2);
				}
				HasShown = true;
			}
		}

		private void SetState(MouseState current)
		{
			State = current;
			Invalidate();
		}

		private int GetIndex()
		{
			GetIndexPoint = PointToClient(Control.MousePosition);
			B1x = (GetIndexPoint.X < 7);
			B2x = (GetIndexPoint.X > base.Width - 7);
			B3 = (GetIndexPoint.Y < 7);
			B4 = (GetIndexPoint.Y > base.Height - 7);
			if (B1x && B3)
			{
				return 4;
			}
			if (B1x && B4)
			{
				return 7;
			}
			if (B2x && B3)
			{
				return 5;
			}
			if (B2x && B4)
			{
				return 8;
			}
			if (B1x)
			{
				return 1;
			}
			if (B2x)
			{
				return 2;
			}
			if (B3)
			{
				return 3;
			}
			if (B4)
			{
				return 6;
			}
			return 0;
		}

		private void InvalidateMouse()
		{
			Current = GetIndex();
			if (Current != Previous)
			{
				Previous = Current;
				switch (Previous)
				{
				case 0:
					Cursor = Cursors.Default;
					break;
				case 6:
					Cursor = Cursors.SizeNS;
					break;
				case 8:
					Cursor = Cursors.SizeNWSE;
					break;
				case 7:
					Cursor = Cursors.SizeNESW;
					break;
				}
			}
		}

		private void InitializeMessages()
		{
			Messages[0] = Message.Create(base.Parent.Handle, 161, new IntPtr(2), IntPtr.Zero);
			for (int i = 1; i <= 8; i++)
			{
				Messages[i] = Message.Create(base.Parent.Handle, 161, new IntPtr(i + 9), IntPtr.Zero);
			}
		}

		private void CorrectBounds(Rectangle bounds)
		{
			if (base.Parent.Width > bounds.Width)
			{
				base.Parent.Width = bounds.Width;
			}
			if (base.Parent.Height > bounds.Height)
			{
				base.Parent.Height = bounds.Height;
			}
			int num = base.Parent.Location.X;
			int num2 = base.Parent.Location.Y;
			if (num < bounds.X)
			{
				num = bounds.X;
			}
			if (num2 < bounds.Y)
			{
				num2 = bounds.Y;
			}
			int num3 = bounds.X + bounds.Width;
			int num4 = bounds.Y + bounds.Height;
			if (num + base.Parent.Width > num3)
			{
				num = num3 - base.Parent.Width;
			}
			if (num2 + base.Parent.Height > num4)
			{
				num2 = num4 - base.Parent.Height;
			}
			base.Parent.Location = new Point(num, num2);
		}

		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
			if (!WM_LMBUTTONDOWN || m.Msg != 513)
			{
				return;
			}
			WM_LMBUTTONDOWN = false;
			SetState(MouseState.Over);
			if (_SmartBounds)
			{
				if (IsParentMdi)
				{
					CorrectBounds(new Rectangle(Point.Empty, base.Parent.Parent.Size));
				}
				else
				{
					CorrectBounds(Screen.FromControl(base.Parent).WorkingArea);
				}
			}
		}

		protected override void CreateHandle()
		{
			base.CreateHandle();
		}

		public MonoFlat_ThemeContainer()
		{
			SetStyle(ControlStyles.UserPaint | ControlStyles.Opaque | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value: true);
			BackColor = Color.FromArgb(32, 41, 50);
			base.Padding = new Padding(10, 70, 10, 9);
			DoubleBuffered = true;
			Dock = DockStyle.Fill;
			MoveHeight = 66;
			Font = new Font("Segoe UI", 9f);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics graphics = e.Graphics;
			graphics.Clear(Color.FromArgb(32, 41, 50));
			graphics.FillRectangle(new SolidBrush(Color.FromArgb(181, 41, 42)), new Rectangle(0, 0, base.Width, 60));
			if (_RoundCorners)
			{
				graphics.FillRectangle(Brushes.Fuchsia, 0, 0, 1, 1);
				graphics.FillRectangle(Brushes.Fuchsia, 1, 0, 1, 1);
				graphics.FillRectangle(Brushes.Fuchsia, 2, 0, 1, 1);
				graphics.FillRectangle(Brushes.Fuchsia, 3, 0, 1, 1);
				graphics.FillRectangle(Brushes.Fuchsia, 0, 1, 1, 1);
				graphics.FillRectangle(Brushes.Fuchsia, 0, 2, 1, 1);
				graphics.FillRectangle(Brushes.Fuchsia, 0, 3, 1, 1);
				graphics.FillRectangle(Brushes.Fuchsia, 1, 1, 1, 1);
				graphics.FillRectangle(new SolidBrush(Color.FromArgb(181, 41, 42)), 1, 3, 1, 1);
				graphics.FillRectangle(new SolidBrush(Color.FromArgb(181, 41, 42)), 1, 2, 1, 1);
				graphics.FillRectangle(new SolidBrush(Color.FromArgb(181, 41, 42)), 2, 1, 1, 1);
				graphics.FillRectangle(new SolidBrush(Color.FromArgb(181, 41, 42)), 3, 1, 1, 1);
				graphics.FillRectangle(Brushes.Fuchsia, base.Width - 1, 0, 1, 1);
				graphics.FillRectangle(Brushes.Fuchsia, base.Width - 2, 0, 1, 1);
				graphics.FillRectangle(Brushes.Fuchsia, base.Width - 3, 0, 1, 1);
				graphics.FillRectangle(Brushes.Fuchsia, base.Width - 4, 0, 1, 1);
				graphics.FillRectangle(Brushes.Fuchsia, base.Width - 1, 1, 1, 1);
				graphics.FillRectangle(Brushes.Fuchsia, base.Width - 1, 2, 1, 1);
				graphics.FillRectangle(Brushes.Fuchsia, base.Width - 1, 3, 1, 1);
				graphics.FillRectangle(Brushes.Fuchsia, base.Width - 2, 1, 1, 1);
				graphics.FillRectangle(new SolidBrush(Color.FromArgb(181, 41, 42)), base.Width - 2, 3, 1, 1);
				graphics.FillRectangle(new SolidBrush(Color.FromArgb(181, 41, 42)), base.Width - 2, 2, 1, 1);
				graphics.FillRectangle(new SolidBrush(Color.FromArgb(181, 41, 42)), base.Width - 3, 1, 1, 1);
				graphics.FillRectangle(new SolidBrush(Color.FromArgb(181, 41, 42)), base.Width - 4, 1, 1, 1);
				graphics.FillRectangle(Brushes.Fuchsia, 0, base.Height - 1, 1, 1);
				graphics.FillRectangle(Brushes.Fuchsia, 0, base.Height - 2, 1, 1);
				graphics.FillRectangle(Brushes.Fuchsia, 0, base.Height - 3, 1, 1);
				graphics.FillRectangle(Brushes.Fuchsia, 0, base.Height - 4, 1, 1);
				graphics.FillRectangle(Brushes.Fuchsia, 1, base.Height - 1, 1, 1);
				graphics.FillRectangle(Brushes.Fuchsia, 2, base.Height - 1, 1, 1);
				graphics.FillRectangle(Brushes.Fuchsia, 3, base.Height - 1, 1, 1);
				graphics.FillRectangle(Brushes.Fuchsia, 1, base.Height - 1, 1, 1);
				graphics.FillRectangle(Brushes.Fuchsia, 1, base.Height - 2, 1, 1);
				graphics.FillRectangle(new SolidBrush(Color.FromArgb(32, 41, 50)), 1, base.Height - 3, 1, 1);
				graphics.FillRectangle(new SolidBrush(Color.FromArgb(32, 41, 50)), 1, base.Height - 4, 1, 1);
				graphics.FillRectangle(new SolidBrush(Color.FromArgb(32, 41, 50)), 3, base.Height - 2, 1, 1);
				graphics.FillRectangle(new SolidBrush(Color.FromArgb(32, 41, 50)), 2, base.Height - 2, 1, 1);
				graphics.FillRectangle(Brushes.Fuchsia, base.Width - 1, base.Height, 1, 1);
				graphics.FillRectangle(Brushes.Fuchsia, base.Width - 2, base.Height, 1, 1);
				graphics.FillRectangle(Brushes.Fuchsia, base.Width - 3, base.Height, 1, 1);
				graphics.FillRectangle(Brushes.Fuchsia, base.Width - 4, base.Height, 1, 1);
				graphics.FillRectangle(Brushes.Fuchsia, base.Width - 1, base.Height - 1, 1, 1);
				graphics.FillRectangle(Brushes.Fuchsia, base.Width - 1, base.Height - 2, 1, 1);
				graphics.FillRectangle(Brushes.Fuchsia, base.Width - 1, base.Height - 3, 1, 1);
				graphics.FillRectangle(Brushes.Fuchsia, base.Width - 2, base.Height - 1, 1, 1);
				graphics.FillRectangle(Brushes.Fuchsia, base.Width - 3, base.Height - 1, 1, 1);
				graphics.FillRectangle(Brushes.Fuchsia, base.Width - 4, base.Height - 1, 1, 1);
				graphics.FillRectangle(Brushes.Fuchsia, base.Width - 1, base.Height - 4, 1, 1);
				graphics.FillRectangle(Brushes.Fuchsia, base.Width - 2, base.Height - 2, 1, 1);
				graphics.FillRectangle(new SolidBrush(Color.FromArgb(32, 41, 50)), base.Width - 2, base.Height - 3, 1, 1);
				graphics.FillRectangle(new SolidBrush(Color.FromArgb(32, 41, 50)), base.Width - 2, base.Height - 4, 1, 1);
				graphics.FillRectangle(new SolidBrush(Color.FromArgb(32, 41, 50)), base.Width - 4, base.Height - 2, 1, 1);
				graphics.FillRectangle(new SolidBrush(Color.FromArgb(32, 41, 50)), base.Width - 3, base.Height - 2, 1, 1);
			}
			graphics.DrawString(Text, new Font("Microsoft Sans Serif", 12f, FontStyle.Bold), new SolidBrush(Color.FromArgb(255, 254, 255)), new Rectangle(20, 20, base.Width - 1, base.Height), new StringFormat
			{
				Alignment = StringAlignment.Near,
				LineAlignment = StringAlignment.Near
			});
		}
	}
}
