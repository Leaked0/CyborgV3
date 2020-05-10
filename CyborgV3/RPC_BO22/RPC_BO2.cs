using PS3Lib;
using System;
using System.Threading;
using Tool_Cyborg_BO2_1._19;

namespace RPC_BO22
{
	public class RPC_BO2
	{
		private static PS3API Ar = new PS3API();

		private static uint function_address;

		public static int Call(uint func_address, params object[] parameters)
		{
			int num = parameters.Length;
			int i = 0;
			uint num2 = 0u;
			uint num3 = 0u;
			uint num4 = 0u;
			uint num5 = 0u;
			for (; i < num; i++)
			{
				if (parameters[i] is int)
				{
					Ar.Extension.WriteInt32(268566528 + num2 * 4, (int)parameters[i]);
					num2++;
				}
				else if (parameters[i] is uint)
				{
					Ar.Extension.WriteUInt32(268566528 + num2 * 4, (uint)parameters[i]);
					num2++;
				}
				else if (parameters[i] is string)
				{
					uint num6 = 268574720 + num3 * 1024;
					Ar.Extension.WriteString(num6, Convert.ToString(parameters[i]));
					Ar.Extension.WriteUInt32(268566528 + num2 * 4, num6);
					num2++;
					num3++;
				}
				else if (parameters[i] is float)
				{
					Ar.Extension.WriteFloat(268566564 + num4 * 4, (float)parameters[i]);
					num4++;
				}
				else if (parameters[i] is float[])
				{
					float[] array = (float[])parameters[i];
					uint num7 = 268570624 + num5 * 4;
					Lib.WriteSingle(num7, array);
					Ar.Extension.WriteUInt32(268566528 + num2 * 4, num7);
					num2++;
					num5 = (uint)((int)num5 + array.Length);
				}
			}
			Ar.Extension.WriteUInt32(268566604u, func_address);
			Thread.Sleep(20);
			return Ar.Extension.ReadInt32(268566608u);
		}

		public static void Enable()
		{
			Ar.SetMemory(function_address, new byte[4]
			{
				78,
				128,
				0,
				32
			});
			Thread.Sleep(20);
			byte[] buffer = new byte[136]
			{
				124,
				8,
				2,
				166,
				248,
				1,
				0,
				128,
				60,
				96,
				16,
				2,
				129,
				131,
				0,
				76,
				44,
				12,
				0,
				0,
				65,
				130,
				0,
				100,
				128,
				131,
				0,
				4,
				128,
				163,
				0,
				8,
				128,
				195,
				0,
				12,
				128,
				227,
				0,
				16,
				129,
				3,
				0,
				20,
				129,
				35,
				0,
				24,
				129,
				67,
				0,
				28,
				129,
				99,
				0,
				32,
				192,
				35,
				0,
				36,
				192,
				67,
				0,
				40,
				192,
				99,
				0,
				44,
				192,
				131,
				0,
				48,
				192,
				163,
				0,
				52,
				192,
				195,
				0,
				56,
				192,
				227,
				0,
				60,
				193,
				3,
				0,
				64,
				193,
				35,
				0,
				72,
				128,
				99,
				0,
				0,
				125,
				137,
				3,
				166,
				78,
				128,
				4,
				33,
				60,
				128,
				16,
				2,
				56,
				160,
				0,
				0,
				144,
				164,
				0,
				76,
				144,
				100,
				0,
				80,
				232,
				1,
				0,
				128,
				124,
				8,
				3,
				166,
				56,
				33,
				0,
				112,
				78,
				128,
				0,
				32
			};
			Ar.SetMemory(function_address + 4, buffer);
			Ar.SetMemory(268566528u, new byte[10324]);
			Ar.SetMemory(function_address, new byte[4]
			{
				248,
				33,
				255,
				145
			});
		}

		public static int Init()
		{
			function_address = 8036432u;
			Enable();
			return 0;
		}

		public static void G_SetModel(int Client, string Model)
		{
			Call(2586180u, (uint)(23830304 + 796 * Client), Model);
		}

		public static void Cbuf_AddText(int client, string text)
		{
			Call(3227272u, client, text);
		}

		public static void iPrintln(int Client, string Text)
		{
			SV_GameSendServerCommand(Client, "O \"" + Text + "\"");
		}

		public static void iPrintlnBold(int Client, string Text)
		{
			SV_GameSendServerCommand(Client, "< \"" + Text + "\"");
		}

		public static void CBuf_Addtext(string Command)
		{
			Call(315938u, 0, Command);
		}

		public static int G_GetWeaponIndexForName(string Weapon)
		{
			return Call(2780136u, Weapon);
		}

		public static void SV_GameSendServerCommand(int Client, string Command)
		{
			Call(3448684u, Client, 1, Command);
		}
	}
}
