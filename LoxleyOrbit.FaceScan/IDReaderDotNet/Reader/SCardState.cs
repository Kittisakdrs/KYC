using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskQexe.IDReaderDotNet.Reader
{
	public enum SCardState
	{
		SCARD_STATE_UNAWARE = 0,
		SCARD_STATE_IGNORE = 1,
		SCARD_STATE_CHANGED = 2,
		SCARD_STATE_UNKNOWN = 4,
		SCARD_STATE_UNAVAILABLE = 8,
		SCARD_STATE_EMPTY = 0x10,
		SCARD_STATE_PRESENT = 0x20,
		SCARD_STATE_ATRMATCH = 0x40,
		SCARD_STATE_EXCLUSIVE = 0x80,
		SCARD_STATE_INUSE = 0x100,
		SCARD_STATE_MUTE = 0x200,
		SCARD_STATE_UNPOWERED = 0x400
	}
}
