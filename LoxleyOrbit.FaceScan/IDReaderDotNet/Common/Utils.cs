using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KioskQexe.IDReaderDotNet.Common
{

	internal class Utils
	{
		public static byte[] HexToBin(string hexString)
		{
			int num = hexString.Length / 2;
			byte[] array = new byte[num];
			for (int i = 0; i < num; i++)
			{
				int startIndex = i * 2;
				try
				{
					array[i] = byte.Parse(hexString.Substring(startIndex, 2), NumberStyles.AllowHexSpecifier);
				}
				catch (Exception)
				{
					array[i] = 0;
				}
			}
			return array;
		}

		public static string BinToHex(byte[] bytesArr)
		{
			string text = string.Empty;
			for (int i = 0; i < bytesArr.Length; i++)
			{
				byte b = bytesArr[i];
				text += b.ToString("X2");
			}
			return text;
		}

		public static string BinToHex(byte[] bytesArr, int startIndex, int length)
		{
			string text = string.Empty;
			for (int i = 0; i < length; i++)
			{
				text += bytesArr[i + startIndex].ToString("X2");
			}
			return text;
		}

		public static string HexToAscii(string hexString)
		{
			byte[] bytes = HexToBin(hexString);
			return Encoding.ASCII.GetString(bytes);
		}

		public static ushort MakeShort(byte hiByte, byte loByte)
		{
			ushort num = hiByte;
			num = (ushort)(num << 8);
			return (ushort)(num + loByte);
		}

		public static string ShortToHex(short value)
		{
			return value.ToString("X4");
		}

		public static byte[] Concat(byte[] buffer1, byte[] buffer2)
		{
			List<byte> list = new List<byte>();
			list.AddRange(buffer1);
			list.AddRange(buffer2);
			return list.ToArray();
		}

		public static string GetParamFromINI(string section, string iniFileName)
		{
			string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			string path = Path.Combine(directoryName, iniFileName);
			bool flag = false;
			string[] array = File.ReadAllLines(path);
			foreach (string text in array)
			{
				if (flag)
				{
					return text;
				}
				if (text.Equals(section, StringComparison.InvariantCultureIgnoreCase))
				{
					flag = true;
				}
			}
			return string.Empty;
		}
	}
}
