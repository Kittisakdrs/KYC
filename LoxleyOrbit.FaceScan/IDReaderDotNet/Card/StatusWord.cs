using KioskQexe.IDReaderDotNet.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskQexe.IDReaderDotNet.Card
{
	public class StatusWord
	{
		private byte _hibyte;

		private byte _lobyte;

		private bool _compareHiOnly = false;

		public bool CompareHiOnly
		{
			get
			{
				return _compareHiOnly;
			}
			set
			{
				_compareHiOnly = value;
			}
		}

		public byte HiByte => _hibyte;

		public byte LoByte => _lobyte;

		public StatusWord(byte hibyte, byte lobyte)
			: this(hibyte, lobyte, compareHiOnly: false)
		{
		}

		public StatusWord(byte hibyte, byte lobyte, bool compareHiOnly)
		{
			_compareHiOnly = compareHiOnly;
			_hibyte = hibyte;
			_lobyte = lobyte;
		}

		public bool Equals(StatusWord eq)
		{
			bool flag = CompareHiOnly || eq.CompareHiOnly;
			if (HiByte == eq.HiByte)
			{
				if (flag)
				{
					return true;
				}
				return LoByte == eq.LoByte;
			}
			return false;
		}

		public override string ToString()
		{
			return Utils.ShortToHex((short)Utils.MakeShort(_hibyte, _lobyte));
		}
	}
}
