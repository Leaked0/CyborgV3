using PS3Lib;
using System;
using System.Threading;
using System.Windows.Forms;

namespace Tutorial_For_YouTube
{
	internal class RPC
	{
		public class Notify_C
		{
			public static void Notify_Cl(int Client, string Note)
			{
				if (Notify)
				{
					iPrintln(Client, Note);
				}
			}
		}

		public static uint g_entity = 23830304u;

		public static uint G_SetMoel = 2585764u;

		public static uint entitySize = 796u;

		private static uint function_address = 8036432u;

		public static PS3API PS3 = new PS3API();

		public static bool Notify = false;

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
					PS3.Extension.WriteInt32(268566528 + num2 * 4, (int)parameters[i]);
					num2++;
				}
				else if (parameters[i] is uint)
				{
					PS3.Extension.WriteUInt32(268566528 + num2 * 4, (uint)parameters[i]);
					num2++;
				}
				else if (parameters[i] is string)
				{
					uint num6 = 268574720 + num3 * 1024;
					PS3.Extension.WriteString(num6, Convert.ToString(parameters[i]));
					PS3.Extension.WriteUInt32(268566528 + num2 * 4, num6);
					num2++;
					num3++;
				}
				else if (parameters[i] is float)
				{
					PS3.Extension.WriteFloat(268566564 + num4 * 4, (float)parameters[i]);
					num4++;
				}
				else if (parameters[i] is float[])
				{
					float[] array = (float[])parameters[i];
					uint num6 = 268570624 + num5 * 4;
					WriteSingle(num6, array);
					PS3.Extension.WriteUInt32(268566528 + num2 * 4, num6);
					num2++;
					num5 = (uint)((int)num5 + array.Length);
				}
			}
			PS3.Extension.WriteUInt32(268566604u, func_address);
			Thread.Sleep(20);
			return PS3.Extension.ReadInt32(268566608u);
		}

		public static void WriteSingle(uint address, float[] input)
		{
			int num = input.Length;
			byte[] array = new byte[num * 4];
			for (int i = 0; i < num; i++)
			{
				ReverseBytes(BitConverter.GetBytes(input[i])).CopyTo(array, i * 4);
			}
			PS3.SetMemory(address, array);
		}

		public static byte[] ReverseBytes(byte[] inArray)
		{
			Array.Reverse(inArray);
			return inArray;
		}

		public static int Init()
		{
			function_address = 8036432u;
			Enable();
			return 0;
		}

		public static void iPrintln(int client, string txt)
		{
			SV_GameSendServerCommand(client, "O \"" + txt + "\"");
		}

		public static void iPrintlnBold(int client, string txt)
		{
			SV_GameSendServerCommand(client, "< \"" + txt + "\"");
		}

		public static void Enable()
		{
			if (PS3.GetBytes(function_address, 4) == new byte[4]
			{
				248,
				33,
				255,
				145
			})
			{
				MessageBox.Show("Already Enabled");
				return;
			}
			PS3.SetMemory(function_address, new byte[4]
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
			PS3.SetMemory(function_address + 4, buffer);
			PS3.SetMemory(268566528u, new byte[10324]);
			PS3.SetMemory(function_address, new byte[4]
			{
				248,
				33,
				255,
				145
			});
		}

		public static void SV_GameSendServerCommand(int client, string command)
		{
			Call(3448684u, client, 0, command);
		}

		public static void G_SetModel(int Client, string Model)
		{
			Call(G_SetMoel, (uint)((int)g_entity + (int)(entitySize * Client)), Model);
		}

		public static void SvKick(int Client, string Reason)
		{
			SV_GameSendServerCommand(Client, "5 \"\n" + Reason + "\"");
		}

		public static void KickClient(int Client)
		{
			cbuf_addtext("clientKick " + Client);
		}

		public static void BlurClient(int Client, bool On)
		{
			if (On)
			{
				SV_GameSendServerCommand(Client, "d 100000000 20");
				Notify_C.Notify_Cl(Client, "Blur : ^2On");
			}
			else
			{
				SV_GameSendServerCommand(Client, "d 0 0");
				Notify_C.Notify_Cl(Client, "Blur : ^1Off");
			}
		}

		public static void PS3Freeze(int Client)
		{
			iPrintlnBold(Client, "^1Warning: ^3Your PS3 Will be froze about 3 sec");
			Thread.Sleep(2500);
			SV_GameSendServerCommand(Client, "^ 6 90 ");
		}

		public static void Player_Die(int Killer, int Victim, int meansOfDeath = 18, int iWeapon = 0)
		{
			uint num = G_Entity(Killer);
			uint num2 = G_Entity(Victim);
			Call(2085744u, num2, num, num, 255, meansOfDeath, iWeapon, 55884950860L);
			Thread.Sleep(100);
		}

		public static uint G_Entity(int entityIndex, uint Mod = 0u)
		{
			return (uint)((int)(23830304 + Mod) + entityIndex * 796);
		}

		public static void cbuf_addtext(string Command)
		{
			Call(3226648u, 0, Command);
		}

		public static void Vision(int Client, string vision)
		{
			SV_GameSendServerCommand(Client, "2 1060 \"" + vision + "\"");
		}

		public static void Clone(int Client)
		{
			PS3.Extension.WriteBytes(2057156u, new byte[4]
			{
				56,
				96,
				0,
				0
			});
			PS3.Extension.WriteInt16(2057158u, (short)Client);
			Call(2057096u, Client);
		}

		public static void Fov(int fov)
		{
			SV_GameSendServerCommand(-1, "^ 5 " + fov);
		}

		public static void KillCamDuration(int Duration)
		{
			cbuf_addtext("set scr_killcam_time " + Duration);
		}

		public static void G_InitalizeAmmo(int Client)
		{
			Call(1992344u, (uint)(23830304 + 796 * Client));
		}

		public static void Add_Ammo(int Client, int Ammo)
		{
			Call(2132392u, (uint)(23830304 + 796 * Client), ReturnHeldWeapon(Client), Ammo, Ammo, Ammo, Ammo);
		}

		public static int ReturnHeldWeapon(int Client)
		{
			return PS3.Extension.ReadByte((uint)(24645859 + 22536 * Client));
		}

		public static void playSound(int clientIndex, string soundName)
		{
			int num = Call(5195196u, soundName);
			SV_GameSendServerCommand(clientIndex, "B " + num);
		}

		public static string char_to_wchar(string text)
		{
			string text2 = text;
			for (int i = 0; i < text.Length; i++)
			{
				text2 = text2.Insert(i * 2, "\0");
			}
			return text2;
		}

		public static string doKeyboard(int KeyboardType = 2, string Title = "Title", string PresetText = "", int MaxLength = 20)
		{
			PS3.Extension.WriteByte(4617479u, (byte)KeyboardType);
			Call(4616460u, 224913792, char_to_wchar(Title), char_to_wchar(PresetText), MaxLength, 13989216);
			while (PS3.Extension.ReadInt32(14164288u) != 0)
			{
			}
			return PS3.Extension.ReadString(50925090u);
		}

		public static int precacheShader(string Shader)
		{
			PS3.Extension.WriteInt32(23101760u, 1);
			return Call(2580096u, Shader);
		}

		public static void G_EntAttach(int entityIndex, string modelName, string tagName)
		{
			int num = Call(4777752u, tagName);
			Call(2586268u, G_Entity(entityIndex), modelName, num, 0);
		}

		public static void AddEvent(int clientIndex, int Event, object eventParams)
		{
			Call(2594736u, G_Entity(clientIndex), Event, eventParams);
		}

		public static void playRumble(int clientIndex, int RumbleIndex)
		{
			if (RumbleIndex != 0)
			{
				AddEvent(clientIndex, 112, RumbleIndex);
			}
		}
	}
}
