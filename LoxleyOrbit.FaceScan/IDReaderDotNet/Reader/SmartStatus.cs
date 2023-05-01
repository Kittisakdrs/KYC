using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskQexe.IDReaderDotNet.Reader
{
	public struct SmartStatus
	{
		public string ReaderName;

		public SCardReaderState ReaderState;

		public SCardProtocol ReaderProtocol;

		public byte[] ATR;
	}
}
