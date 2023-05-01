using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KioskQexe.IDReaderDotNet.Reader
{
	public struct SCARD_READERSTATE
	{
		[MarshalAs(UnmanagedType.LPStr)]
		public string szReader;

		public IntPtr pvUserData;

		public SCardState dwCurrentState;

		public SCardState dwEventState;

		public int cbAtr;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 36)]
		public byte[] rgbAtr;
	}
}
