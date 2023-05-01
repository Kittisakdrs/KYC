using KioskQexe.IDReaderDotNet.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KioskQexe.IDReaderDotNet.Card
{

	internal class ThaiNidCard : CardBase
	{
		private const string aid = "A000000054480001";

		private string msgErr;

		public ThaiNidCard(IntPtr hCard, IStatusWordFilter sw)
			: base(hCard, sw)
		{
		}

		public ThaiNidCard(IntPtr hCard)
			: base(hCard)
		{
		}

		public string GetErrorMessage()
		{
			return msgErr;
		}

		public bool Select()
		{
			string text = Send("00A40400" + "A000000054480001".Length.ToString("X2") + "A000000054480001");
			if (text.Substring(0, 2).Equals("61"))
			{
				text = Send("00C00000" + text.Substring(2, 2));
				text = text.Substring(text.Length - 4, 4);
			}
			if (!text.Equals("9000"))
			{
				msgErr = string.Format("Select applet {0} failed ({1})", "A000000054480001", text);
				return false;
			}
			msgErr = string.Empty;
			return true;
		}

		public DataTable ReadHolderProfile(bool photoRequired)
		{
			byte[] bytes = ReadBinary(0, 0, 4);
			string @string = Encoding.ASCII.GetString(bytes, 0, 4);
			DataTable dataTable = null;
			msgErr = string.Empty;
			switch (@string)
			{
				case "0002":
					dataTable = Helper.CreateDataTable();
					ReadCardFormat002(photoRequired, dataTable.Rows[0]);
					break;
				case "0003":
					dataTable = Helper.CreateDataTable();
					ReadCardFormat003(photoRequired, dataTable.Rows[0]);
					break;
				case "0004":
					dataTable = Helper.CreateDataTable();
					ReadCardFormat003(photoRequired, dataTable.Rows[0]);
					break;
				default:
					msgErr = "Unknow card data format.";
					break;
			}
			return dataTable;
		}

		private void ReadCardFormat002(bool photoRequired, DataRow rowData)
		{
			Encoding encoding = Encoding.GetEncoding(874);
			string[] array = null;
			byte[] bytes = ReadBinary(1, 0, 226);
			string text = (string)(rowData["NationalID"] = encoding.GetString(bytes, 4, 13));
			text = encoding.GetString(bytes, 17, 100);
			text = text.Trim();
			array = text.Split('#');
			rowData["ThaiTitleName"] = array[0];
			rowData["ThaiFirstName"] = array[1];
			rowData["ThaiMiddleName"] = array[2];
			rowData["ThaiLastName"] = array[3];
			text = encoding.GetString(bytes, 117, 100);
			text = text.Trim();
			array = text.Split('#');
			rowData["EnglishTitleName"] = array[0];
			rowData["EnglishFirstName"] = array[1];
			rowData["EnglishMiddleName"] = array[2];
			rowData["EnglishLastName"] = array[3];
			text = (string)(rowData["Birthdate"] = encoding.GetString(bytes, 217, 8));
			rowData["Sex"] = encoding.GetString(bytes, 225, 1);
			bytes = ReadBinary(1, 226, 151);
			rowData["IssueCode"] = encoding.GetString(bytes, 0, 20);
			text = encoding.GetString(bytes, 20, 100);
			rowData["IssuePlace"] = text.Trim();
			rowData["IssuerID"] = encoding.GetString(bytes, 120, 13);
			rowData["IssueDate"] = encoding.GetString(bytes, 133, 8);
			rowData["ExpireDate"] = encoding.GetString(bytes, 141, 8);
			rowData["CardType"] = encoding.GetString(bytes, 149, 2);
			bytes = ReadBinary(0, 4, 164);
			text = encoding.GetString(bytes, 0, 150);
			text = text.Trim();
			array = text.Split('#');
			int num = array[0].IndexOf("หม\u0e39\u0e48ท\u0e35\u0e48");
			if (num >= 0)
			{
				rowData["Address"] = array[0].Substring(0, num);
				rowData["Moo"] = array[0].Substring(num);
			}
			else
			{
				rowData["Address"] = array[0];
				rowData["Moo"] = string.Empty;
			}
			rowData["Trok"] = array[1];
			rowData["Soi"] = array[2];
			rowData["Thanon"] = array[3];
			rowData["Tumbol"] = array[4];
			rowData["Amphur"] = array[5];
			rowData["Province"] = array[6];
			rowData["PhotoRefNo"] = encoding.GetString(bytes, 150, 14);
			if (photoRequired)
			{
				byte[] array2 = new byte[5116];
				int num2 = 5116;
				ushort num3 = 381;
				int num4 = 0;
				int num5 = 240;
				int num6 = num2 / num5;
				if (num2 % num5 > 0)
				{
					num6++;
				}
				while (num6-- > 0)
				{
					int num8 = num2;
					if (num8 > num5)
					{
						num8 = num5;
					}
					bytes = ReadBinary(1, num3, num8);
					Array.Copy(bytes, 0, array2, num4, num8);
					num3 = (ushort)(num3 + (ushort)num8);
					num4 += num8;
					num2 -= num8;
					Thread.Sleep(20);
				}
				rowData["Photo"] = array2;
			}
		}

		private void ReadCardFormat003(bool photoRequired, DataRow rowData)
		{
			Encoding encoding = Encoding.GetEncoding(874);
			string[] array = null;
			byte[] bytes = ReadBinary(0, 0, 226);
			string text = (string)(rowData["NationalID"] = encoding.GetString(bytes, 4, 13));
			text = encoding.GetString(bytes, 17, 100);
			text = text.Trim();
			array = text.Split('#');
			rowData["ThaiTitleName"] = array[0];
			rowData["ThaiFirstName"] = array[1];
			rowData["ThaiMiddleName"] = array[2];
			rowData["ThaiLastName"] = array[3];
			text = encoding.GetString(bytes, 117, 100);
			text = text.Trim();
			array = text.Split('#');
			rowData["EnglishTitleName"] = array[0];
			rowData["EnglishFirstName"] = array[1];
			rowData["EnglishMiddleName"] = array[2];
			rowData["EnglishLastName"] = array[3];
			text = (string)(rowData["Birthdate"] = encoding.GetString(bytes, 217, 8));
			rowData["Sex"] = encoding.GetString(bytes, 225, 1);
			bytes = ReadBinary(0, 226, 151);
			rowData["IssueCode"] = encoding.GetString(bytes, 0, 20);
			text = encoding.GetString(bytes, 20, 100);
			rowData["IssuePlace"] = text.Trim();
			rowData["IssuerID"] = encoding.GetString(bytes, 120, 13);
			rowData["IssueDate"] = encoding.GetString(bytes, 133, 8);
			rowData["ExpireDate"] = encoding.GetString(bytes, 141, 8);
			rowData["CardType"] = encoding.GetString(bytes, 149, 2);
			bytes = ReadBinary(0, 5497, 174);
			text = encoding.GetString(bytes, 0, 160);
			text = text.Trim();
			array = text.Split('#');
			rowData["Address"] = array[0];
			rowData["Moo"] = array[1];
			rowData["Trok"] = array[2];
			rowData["Soi"] = array[3];
			rowData["Thanon"] = array[4];
			rowData["Tumbol"] = array[5];
			rowData["Amphur"] = array[6];
			rowData["Province"] = array[7];
			rowData["PhotoRefNo"] = encoding.GetString(bytes, 160, 14);
			if (photoRequired)
			{
				byte[] array2 = new byte[5118];
				int num = 5118;
				ushort num2 = 379;
				int num3 = 0;
				int num4 = 240;
				int num5 = num / num4;
				if (num % num4 > 0)
				{
					num5++;
				}
				while (num5-- > 0)
				{
					int num7 = num;
					if (num7 > num4)
					{
						num7 = num4;
					}
					bytes = ReadBinary(0, num2, num7);
					Array.Copy(bytes, 0, array2, num3, num7);
					num2 = (ushort)(num2 + (ushort)num7);
					num3 += num7;
					num -= num7;
					Thread.Sleep(20);
				}
				rowData["Photo"] = array2;
			}
		}

		private byte[] ReadBinary(int block, ushort offset, int length)
		{
			string hexString = string.Format("80B0{0}02{1}{2}", offset.ToString("X4"), block.ToString("X2"), length.ToString("X2"));
			byte[] array = Send(Utils.HexToBin(hexString));
			if (array.Length == 2 && array[0] == 97)
			{
				array = Send(Utils.HexToBin(string.Format("00C00000{0}", length.ToString("X2"))));
			}
			Array.Resize(ref array, array.Length - 2);
			return array;
		}
	}
}
