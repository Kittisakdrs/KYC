using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskQexe.IDReaderDotNet.Card
{
	public interface IStatusWordFilter
	{
		bool Accept(byte[] rxBuffer);
		bool AddStatusWord(StatusWord sw);
	}
}
