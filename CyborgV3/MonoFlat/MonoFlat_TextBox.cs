using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MonoFlat
{
	[DefaultEvent("TextChanged")]
	public class MonoFlat_TextBox : Control
	{
		public TextBox MonoFlatTB = new TextBox();

		private int _maxchars = 32767;

		private bool _ReadOnly;

		private bool _Multiline;

		private Image _Image;

		private Size _ImageSize;

		private HorizontalAlignment ALNType;

		private bool isPasswordMasked = false;

		private Pen P1;

		private SolidBrush B1;

		private GraphicsPath Shape;

		public HorizontalAlignment TextAlignment
		{
			get
			{
				return ALNType;
			}
			set
			{
				ALNType = value;
				Invalidate();
			}
		}

		public int MaxLength
		{
			get
			{
				return _maxchars;
			}
			set
			{
				_maxchars = value;
				MonoFlatTB.MaxLength = MaxLength;
				Invalidate();
			}
		}

		public bool UseSystemPasswordChar
		{
			get
			{
				return isPasswordMasked;
			}
			set
			{
				MonoFlatTB.UseSystemPasswordChar = UseSystemPasswordChar;
				isPasswordMasked = value;
				Invalidate();
			}
		}

		public bool ReadOnly
		{
			get
			{
				return _ReadOnly;
			}
			set
			{
				_ReadOnly = value;
				if (MonoFlatTB != null)
				{
					MonoFlatTB.ReadOnly = value;
				}
			}
		}

		public bool Multiline
		{
			get
			{
				return _Multiline;
			}
			set
			{
				_Multiline = value;
				if (MonoFlatTB != null)
				{
					MonoFlatTB.Multiline = value;
					if (value)
					{
						MonoFlatTB.Height = base.Height - 23;
					}
					else
					{
						base.Height = MonoFlatTB.Height + 23;
					}
				}
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
				if (Image == null)
				{
					MonoFlatTB.Location = new Point(8, 10);
				}
				else
				{
					MonoFlatTB.Location = new Point(35, 11);
				}
				Invalidate();
			}
		}

		protected Size ImageSize => _ImageSize;

		private void _Enter(object Obj, EventArgs e)
		{
			P1 = new Pen(Color.FromArgb(181, 41, 42));
			Refresh();
		}

		private void _Leave(object Obj, EventArgs e)
		{
			P1 = new Pen(Color.FromArgb(32, 41, 50));
			Refresh();
		}

		private void OnBaseTextChanged(object s, EventArgs e)
		{
			Text = MonoFlatTB.Text;
		}

		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			MonoFlatTB.Text = Text;
			Invalidate();
		}

		protected override void OnForeColorChanged(EventArgs e)
		{
			base.OnForeColorChanged(e);
			MonoFlatTB.ForeColor = ForeColor;
			Invalidate();
		}

		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			MonoFlatTB.Font = Font;
		}

		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);
		}

		private void _OnKeyDown(object Obj, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.A)
			{
				MonoFlatTB.SelectAll();
				e.SuppressKeyPress = true;
			}
			if (e.Control && e.KeyCode == Keys.C)
			{
				MonoFlatTB.Copy();
				e.SuppressKeyPress = true;
			}
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if (_Multiline)
			{
				MonoFlatTB.Height = base.Height - 23;
			}
			else
			{
				base.Height = MonoFlatTB.Height + 23;
			}
			Shape = new GraphicsPath();
			Shape.AddArc(0, 0, 10, 10, 180f, 90f);
			Shape.AddArc(base.Width - 11, 0, 10, 10, -90f, 90f);
			Shape.AddArc(base.Width - 11, base.Height - 11, 10, 10, 0f, 90f);
			Shape.AddArc(0, base.Height - 11, 10, 10, 90f, 90f);
			Shape.CloseAllFigures();
		}

		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			MonoFlatTB.Focus();
		}

		public void _TextChanged(object sender, EventArgs e)
		{
			Text = MonoFlatTB.Text;
		}

		public void _BaseTextChanged(object sender, EventArgs e)
		{
			MonoFlatTB.Text = Text;
		}

		public void AddTextBox()
		{
			MonoFlatTB.Location = new Point(8, 10);
			MonoFlatTB.Text = string.Empty;
			MonoFlatTB.BorderStyle = BorderStyle.None;
			MonoFlatTB.TextAlign = HorizontalAlignment.Left;
			MonoFlatTB.Font = new Font("Tahoma", 11f);
			MonoFlatTB.UseSystemPasswordChar = UseSystemPasswordChar;
			MonoFlatTB.Multiline = false;
			MonoFlatTB.BackColor = Color.FromArgb(66, 76, 85);
			MonoFlatTB.ScrollBars = ScrollBars.None;
			MonoFlatTB.KeyDown += _OnKeyDown;
			MonoFlatTB.Enter += _Enter;
			MonoFlatTB.Leave += _Leave;
			MonoFlatTB.TextChanged += OnBaseTextChanged;
		}

		public MonoFlat_TextBox()
		{
			SetStyle(ControlStyles.SupportsTransparentBackColor, value: true);
			SetStyle(ControlStyles.UserPaint, value: true);
			AddTextBox();
			base.Controls.Add(MonoFlatTB);
			P1 = new Pen(Color.FromArgb(32, 41, 50));
			B1 = new SolidBrush(Color.FromArgb(66, 76, 85));
			BackColor = Color.Transparent;
			ForeColor = Color.FromArgb(176, 183, 191);
			Text = null;
			Font = new Font("Tahoma", 11f);
			base.Size = new Size(135, 43);
			DoubleBuffered = true;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Bitmap bitmap = new Bitmap(base.Width, base.Height);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			if (Image == null)
			{
				MonoFlatTB.Width = base.Width - 18;
			}
			else
			{
				MonoFlatTB.Width = base.Width - 45;
			}
			MonoFlatTB.TextAlign = TextAlignment;
			MonoFlatTB.UseSystemPasswordChar = UseSystemPasswordChar;
			graphics.Clear(Color.Transparent);
			graphics.FillPath(B1, Shape);
			graphics.DrawPath(P1, Shape);
			if (Image != null)
			{
				graphics.DrawImage(_Image, 5, 8, 24, 24);
			}
			e.Graphics.DrawImage((Image)bitmap.Clone(), 0, 0);
			graphics.Dispose();
			bitmap.Dispose();
		}
	}
}
