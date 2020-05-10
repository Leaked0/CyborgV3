using System.Collections.Generic;

public static class Functions
{
	public static byte[] List(this byte[] A, byte[] B)
	{
		List<byte> list = new List<byte>();
		int num = 0;
		for (int i = 0; i < A.Length; i++)
		{
			byte b = 0;
			for (int j = 0; j < B.Length; j++)
			{
				short num2 = (short)(A[i] * B[j] + b);
				b = (byte)((uint)num2 >> 8);
				byte b2 = (byte)num2;
				num = i + j;
				if (num < list.Count)
				{
					list = _add_(list, b2, num, 0);
				}
				else
				{
					list.Add(b2);
				}
			}
			if (b > 0)
			{
				if (num + 1 < list.Count)
				{
					list = _add_(list, b, num + 1, 0);
				}
				else
				{
					list.Add(b);
				}
			}
		}
		return list.ToArray();
	}

	private static List<byte> _add_(List<byte> A, byte b, int idx = 0, byte rem = 0)
	{
		if (idx < A.Count)
		{
			short num = (short)(A[idx] + b);
			A[idx] = (byte)(num % 256);
			rem = (byte)((num - A[idx]) % 255);
			if (rem > 0)
			{
				return _add_(A, rem, idx + 1, 0);
			}
		}
		else
		{
			A.Add(b);
		}
		return A;
	}
}
