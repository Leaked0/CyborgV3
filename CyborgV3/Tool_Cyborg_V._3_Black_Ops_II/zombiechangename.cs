using LogIn_Theme_Dll_By_xVenoxi;
using MonoFlat;
using PS3Lib;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Tool_Cyborg_V._3_Black_Ops_II
{
	public class zombiechangename : Form
	{
		private PS3API PS3 = new PS3API();

		private IContainer components = null;

		private MonoFlat_ThemeContainer monoFlat_ThemeContainer1;

		private MonoFlat_Button monoFlat_Button1;

		private LogInNormalTextBox logInNormalTextBox4;

		private MonoFlat_Button monoFlat_Button27;

		private LogInNormalTextBox logInNormalTextBox2;

		private MonoFlat_Button monoFlat_Button3;

		private LogInNormalTextBox logInNormalTextBox3;

		private MonoFlat_Button monoFlat_Button4;

		private LogInNormalTextBox logInNormalTextBox1;

		private MonoFlat_Button monoFlat_Button2;

		public zombiechangename()
		{
			InitializeComponent();
		}

		private void monoFlat_Button1_Click(object sender, EventArgs e)
		{
			PS3.ChangeAPI(SelectAPI.ControlConsole);
			PS3.ConnectTarget();
			PS3.AttachProcess();
			MessageBox.Show("Vous éte connecter & attacher ;)");
			monoFlat_Button1.Text = "PS3 connecter";
		}

		private void monoFlat_Button27_Click(object sender, EventArgs e)
		{
			byte[] array = Encoding.ASCII.GetBytes(logInNormalTextBox4.Text);
			Array.Resize(ref array, array.Length + 1);
			PS3.SetMemory(24667244u, array);
			PS3.SetMemory(24667244u, array);
		}

		private void monoFlat_Button2_Click(object sender, EventArgs e)
		{
			byte[] array = Encoding.ASCII.GetBytes(logInNormalTextBox1.Text);
			Array.Resize(ref array, array.Length + 1);
			PS3.SetMemory(24667244u, array);
			PS3.SetMemory(24667244u, array);
		}

		private void monoFlat_Button4_Click(object sender, EventArgs e)
		{
			byte[] array = Encoding.ASCII.GetBytes(logInNormalTextBox3.Text);
			Array.Resize(ref array, array.Length + 1);
			PS3.SetMemory(24667244u, array);
			PS3.SetMemory(24667244u, array);
		}

		private void monoFlat_Button3_Click(object sender, EventArgs e)
		{
			byte[] array = Encoding.ASCII.GetBytes(logInNormalTextBox2.Text);
			Array.Resize(ref array, array.Length + 1);
			PS3.SetMemory(24667244u, array);
			PS3.SetMemory(24667244u, array);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tool_Cyborg_V._3_Black_Ops_II.zombiechangename));
			monoFlat_ThemeContainer1 = new MonoFlat.MonoFlat_ThemeContainer();
			logInNormalTextBox2 = new LogIn_Theme_Dll_By_xVenoxi.LogInNormalTextBox();
			monoFlat_Button3 = new MonoFlat.MonoFlat_Button();
			logInNormalTextBox3 = new LogIn_Theme_Dll_By_xVenoxi.LogInNormalTextBox();
			monoFlat_Button4 = new MonoFlat.MonoFlat_Button();
			logInNormalTextBox1 = new LogIn_Theme_Dll_By_xVenoxi.LogInNormalTextBox();
			monoFlat_Button2 = new MonoFlat.MonoFlat_Button();
			logInNormalTextBox4 = new LogIn_Theme_Dll_By_xVenoxi.LogInNormalTextBox();
			monoFlat_Button27 = new MonoFlat.MonoFlat_Button();
			monoFlat_Button1 = new MonoFlat.MonoFlat_Button();
			monoFlat_ThemeContainer1.SuspendLayout();
			SuspendLayout();
			monoFlat_ThemeContainer1.BackColor = System.Drawing.Color.FromArgb(32, 41, 50);
			monoFlat_ThemeContainer1.Controls.Add(logInNormalTextBox2);
			monoFlat_ThemeContainer1.Controls.Add(monoFlat_Button3);
			monoFlat_ThemeContainer1.Controls.Add(logInNormalTextBox3);
			monoFlat_ThemeContainer1.Controls.Add(monoFlat_Button4);
			monoFlat_ThemeContainer1.Controls.Add(logInNormalTextBox1);
			monoFlat_ThemeContainer1.Controls.Add(monoFlat_Button2);
			monoFlat_ThemeContainer1.Controls.Add(logInNormalTextBox4);
			monoFlat_ThemeContainer1.Controls.Add(monoFlat_Button27);
			monoFlat_ThemeContainer1.Controls.Add(monoFlat_Button1);
			monoFlat_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			monoFlat_ThemeContainer1.Font = new System.Drawing.Font("Segoe UI", 9f);
			monoFlat_ThemeContainer1.Location = new System.Drawing.Point(0, 0);
			monoFlat_ThemeContainer1.Name = "monoFlat_ThemeContainer1";
			monoFlat_ThemeContainer1.Padding = new System.Windows.Forms.Padding(10, 70, 10, 9);
			monoFlat_ThemeContainer1.RoundCorners = true;
			monoFlat_ThemeContainer1.Sizable = true;
			monoFlat_ThemeContainer1.Size = new System.Drawing.Size(603, 218);
			monoFlat_ThemeContainer1.SmartBounds = true;
			monoFlat_ThemeContainer1.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			monoFlat_ThemeContainer1.TabIndex = 0;
			monoFlat_ThemeContainer1.Text = "                                             Change name zombie";
			logInNormalTextBox2.BackColor = System.Drawing.Color.Transparent;
			logInNormalTextBox2.BackgroundColour = System.Drawing.Color.FromArgb(42, 42, 42);
			logInNormalTextBox2.BorderColour = System.Drawing.Color.FromArgb(35, 35, 35);
			logInNormalTextBox2.Location = new System.Drawing.Point(304, 166);
			logInNormalTextBox2.MaxLength = 32767;
			logInNormalTextBox2.Multiline = false;
			logInNormalTextBox2.Name = "logInNormalTextBox2";
			logInNormalTextBox2.ReadOnly = false;
			logInNormalTextBox2.Size = new System.Drawing.Size(247, 29);
			logInNormalTextBox2.Style = LogIn_Theme_Dll_By_xVenoxi.LogInNormalTextBox.Styles.NotRounded;
			logInNormalTextBox2.TabIndex = 62;
			logInNormalTextBox2.Text = "^1ExoTiQueMoDz";
			logInNormalTextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			logInNormalTextBox2.TextColour = System.Drawing.Color.FromArgb(255, 255, 255);
			logInNormalTextBox2.UseSystemPasswordChar = false;
			monoFlat_Button3.BackColor = System.Drawing.Color.Transparent;
			monoFlat_Button3.Font = new System.Drawing.Font("Segoe UI", 12f);
			monoFlat_Button3.Image = null;
			monoFlat_Button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			monoFlat_Button3.Location = new System.Drawing.Point(557, 166);
			monoFlat_Button3.Name = "monoFlat_Button3";
			monoFlat_Button3.Size = new System.Drawing.Size(33, 29);
			monoFlat_Button3.TabIndex = 61;
			monoFlat_Button3.Text = "V";
			monoFlat_Button3.TextAlignment = System.Drawing.StringAlignment.Center;
			monoFlat_Button3.Click += new System.EventHandler(monoFlat_Button3_Click);
			logInNormalTextBox3.BackColor = System.Drawing.Color.Transparent;
			logInNormalTextBox3.BackgroundColour = System.Drawing.Color.FromArgb(42, 42, 42);
			logInNormalTextBox3.BorderColour = System.Drawing.Color.FromArgb(35, 35, 35);
			logInNormalTextBox3.Location = new System.Drawing.Point(305, 131);
			logInNormalTextBox3.MaxLength = 32767;
			logInNormalTextBox3.Multiline = false;
			logInNormalTextBox3.Name = "logInNormalTextBox3";
			logInNormalTextBox3.ReadOnly = false;
			logInNormalTextBox3.Size = new System.Drawing.Size(247, 29);
			logInNormalTextBox3.Style = LogIn_Theme_Dll_By_xVenoxi.LogInNormalTextBox.Styles.NotRounded;
			logInNormalTextBox3.TabIndex = 60;
			logInNormalTextBox3.Text = "^1ExoTiQueMoDz";
			logInNormalTextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			logInNormalTextBox3.TextColour = System.Drawing.Color.FromArgb(255, 255, 255);
			logInNormalTextBox3.UseSystemPasswordChar = false;
			monoFlat_Button4.BackColor = System.Drawing.Color.Transparent;
			monoFlat_Button4.Font = new System.Drawing.Font("Segoe UI", 12f);
			monoFlat_Button4.Image = null;
			monoFlat_Button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			monoFlat_Button4.Location = new System.Drawing.Point(558, 131);
			monoFlat_Button4.Name = "monoFlat_Button4";
			monoFlat_Button4.Size = new System.Drawing.Size(33, 29);
			monoFlat_Button4.TabIndex = 59;
			monoFlat_Button4.Text = "V";
			monoFlat_Button4.TextAlignment = System.Drawing.StringAlignment.Center;
			monoFlat_Button4.Click += new System.EventHandler(monoFlat_Button4_Click);
			logInNormalTextBox1.BackColor = System.Drawing.Color.Transparent;
			logInNormalTextBox1.BackgroundColour = System.Drawing.Color.FromArgb(42, 42, 42);
			logInNormalTextBox1.BorderColour = System.Drawing.Color.FromArgb(35, 35, 35);
			logInNormalTextBox1.Location = new System.Drawing.Point(12, 166);
			logInNormalTextBox1.MaxLength = 32767;
			logInNormalTextBox1.Multiline = false;
			logInNormalTextBox1.Name = "logInNormalTextBox1";
			logInNormalTextBox1.ReadOnly = false;
			logInNormalTextBox1.Size = new System.Drawing.Size(247, 29);
			logInNormalTextBox1.Style = LogIn_Theme_Dll_By_xVenoxi.LogInNormalTextBox.Styles.NotRounded;
			logInNormalTextBox1.TabIndex = 58;
			logInNormalTextBox1.Text = "^1OldModz95^2<3";
			logInNormalTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			logInNormalTextBox1.TextColour = System.Drawing.Color.FromArgb(255, 255, 255);
			logInNormalTextBox1.UseSystemPasswordChar = false;
			monoFlat_Button2.BackColor = System.Drawing.Color.Transparent;
			monoFlat_Button2.Font = new System.Drawing.Font("Segoe UI", 12f);
			monoFlat_Button2.Image = null;
			monoFlat_Button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			monoFlat_Button2.Location = new System.Drawing.Point(265, 166);
			monoFlat_Button2.Name = "monoFlat_Button2";
			monoFlat_Button2.Size = new System.Drawing.Size(33, 29);
			monoFlat_Button2.TabIndex = 57;
			monoFlat_Button2.Text = "V";
			monoFlat_Button2.TextAlignment = System.Drawing.StringAlignment.Center;
			monoFlat_Button2.Click += new System.EventHandler(monoFlat_Button2_Click);
			logInNormalTextBox4.BackColor = System.Drawing.Color.Transparent;
			logInNormalTextBox4.BackgroundColour = System.Drawing.Color.FromArgb(42, 42, 42);
			logInNormalTextBox4.BorderColour = System.Drawing.Color.FromArgb(35, 35, 35);
			logInNormalTextBox4.Location = new System.Drawing.Point(12, 131);
			logInNormalTextBox4.MaxLength = 32767;
			logInNormalTextBox4.Multiline = false;
			logInNormalTextBox4.Name = "logInNormalTextBox4";
			logInNormalTextBox4.ReadOnly = false;
			logInNormalTextBox4.Size = new System.Drawing.Size(248, 29);
			logInNormalTextBox4.Style = LogIn_Theme_Dll_By_xVenoxi.LogInNormalTextBox.Styles.NotRounded;
			logInNormalTextBox4.TabIndex = 56;
			logInNormalTextBox4.Text = "^1OldModz95^2<3";
			logInNormalTextBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			logInNormalTextBox4.TextColour = System.Drawing.Color.FromArgb(255, 255, 255);
			logInNormalTextBox4.UseSystemPasswordChar = false;
			monoFlat_Button27.BackColor = System.Drawing.Color.Transparent;
			monoFlat_Button27.Font = new System.Drawing.Font("Segoe UI", 12f);
			monoFlat_Button27.Image = null;
			monoFlat_Button27.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			monoFlat_Button27.Location = new System.Drawing.Point(266, 131);
			monoFlat_Button27.Name = "monoFlat_Button27";
			monoFlat_Button27.Size = new System.Drawing.Size(33, 29);
			monoFlat_Button27.TabIndex = 55;
			monoFlat_Button27.Text = "V";
			monoFlat_Button27.TextAlignment = System.Drawing.StringAlignment.Center;
			monoFlat_Button27.Click += new System.EventHandler(monoFlat_Button27_Click);
			monoFlat_Button1.BackColor = System.Drawing.Color.Transparent;
			monoFlat_Button1.Font = new System.Drawing.Font("Segoe UI", 12f);
			monoFlat_Button1.Image = null;
			monoFlat_Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			monoFlat_Button1.Location = new System.Drawing.Point(155, 73);
			monoFlat_Button1.Name = "monoFlat_Button1";
			monoFlat_Button1.Size = new System.Drawing.Size(286, 41);
			monoFlat_Button1.TabIndex = 0;
			monoFlat_Button1.Text = "Connexion PS3";
			monoFlat_Button1.TextAlignment = System.Drawing.StringAlignment.Center;
			monoFlat_Button1.Click += new System.EventHandler(monoFlat_Button1_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(603, 218);
			base.Controls.Add(monoFlat_ThemeContainer1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "zombiechangename";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "                                             Change name zombie";
			base.TransparencyKey = System.Drawing.Color.Fuchsia;
			monoFlat_ThemeContainer1.ResumeLayout(false);
			ResumeLayout(false);
		}
	}
}
