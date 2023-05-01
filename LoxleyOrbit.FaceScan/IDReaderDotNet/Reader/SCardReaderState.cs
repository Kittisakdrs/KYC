using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskQexe.IDReaderDotNet.Reader
{
	public enum SCardReaderState
	{
		SCARD_UNKNOWN,
		SCARD_ABSENT,
		SCARD_PRESENT,
		SCARD_SWALLOWED,
		SCARD_POWERED,
		SCARD_NEGOTIABLE,
		SCARD_SPECIFIC
	}
}
