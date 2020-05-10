using System.Text;

internal static class Encoder
{
	private static Encoding DefEncoding = Encoding.GetEncoding("iso-8859-1");

	public static string GetString(byte[] value)
	{
		return GetString(value, DefEncoding);
	}

	public static string GetString(byte[] value, Encoding encoding)
	{
		return encoding.GetString(value);
	}

	public static byte[] GetBytes(string value)
	{
		return GetBytes(value, DefEncoding);
	}

	public static byte[] GetBytes(string value, Encoding encoding)
	{
		return encoding.GetBytes(value);
	}
}
