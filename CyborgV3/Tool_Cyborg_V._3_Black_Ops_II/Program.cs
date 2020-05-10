using System;
using System.Windows.Forms;

namespace Tool_Cyborg_V._3_Black_Ops_II
{
	internal static class Program
	{
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(defaultValue: false);
			Application.Run(new CyborgV3());
		}
	}
}
