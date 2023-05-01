using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskQexe.IDReaderDotNet.Card
{

	public class DefaultStatusWordFilter : IStatusWordFilter
	{
		public static readonly int MAX_STATUS_WORDS = 10;

		private int swCount = 0;

		private StatusWord[] swArray = new StatusWord[MAX_STATUS_WORDS];

		public DefaultStatusWordFilter()
		{
			AddStatusWord(new StatusWord(144, 0));
			AddStatusWord(new StatusWord(97, 0, compareHiOnly: true));
		}

		public virtual bool Accept(byte[] rxBuffer)
		{
			StatusWord statusWord = new StatusWord(rxBuffer[rxBuffer.Length - 2], rxBuffer[rxBuffer.Length - 1]);
			for (int i = 0; i < swCount; i++)
			{
				if (statusWord.Equals(swArray[i]))
				{
					return true;
				}
			}
			return false;
		}

		public virtual bool AddStatusWord(StatusWord sw)
		{
			if (swCount < MAX_STATUS_WORDS)
			{
				swArray[swCount] = sw;
				swCount++;
				return true;
			}
			return false;
		}
	}
}
