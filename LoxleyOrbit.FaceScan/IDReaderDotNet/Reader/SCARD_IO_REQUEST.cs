using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KioskQexe.IDReaderDotNet.Reader
{
	public struct SCARD_IO_REQUEST
	{
		public SCardProtocol dwProtocol;

		public int cbPciLength;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		public byte[] buffer;
	}
}
