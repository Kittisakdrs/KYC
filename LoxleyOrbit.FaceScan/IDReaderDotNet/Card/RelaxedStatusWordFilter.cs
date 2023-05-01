using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskQexe.IDReaderDotNet.Card
{
	public sealed class RelaxedStatusWordFilter : IStatusWordFilter
	{
		public bool Accept(byte[] rxBuffer)
		{
			return true;
		}

		public bool AddStatusWord(StatusWord sw)
		{
			return true;
		}
	}
}
