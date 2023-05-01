using KioskQexe.IDReaderDotNet.Common;
using KioskQexe.IDReaderDotNet.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskQexe.IDReaderDotNet.Card
{

	public class CardBase
	{
		public delegate void UnhandledStatusWordHandler(object source, StatusWord sw);

		public delegate void TransmissionHandler(object source, int transmission);

		protected IntPtr hCard = IntPtr.Zero;

		protected IStatusWordFilter sw;

		protected bool doGetResponse = false;

		protected bool fixBadLcLength = true;

		public IntPtr Handle => hCard;

		public SmartStatus Status => SCardWrapper.GetStatus(hCard);

		public IStatusWordFilter StatusWordFilter
		{
			get
			{
				return sw;
			}
			set
			{
				sw = value;
			}
		}

		public virtual bool DoGetResponse
		{
			get
			{
				return doGetResponse;
			}
			set
			{
				doGetResponse = value;
			}
		}

		public event TransmissionHandler StartTransmit;

		public event TransmissionHandler EndTransmit;

		public event UnhandledStatusWordHandler UnhandledStatusWord;

		public CardBase(IntPtr hCard, IStatusWordFilter sw)
		{
			this.hCard = hCard;
			this.sw = sw;
			if (hCard == IntPtr.Zero)
			{
				throw new Exception("Card handle is invalid");
			}
		}

		public CardBase(IntPtr hCard)
			: this(hCard, new DefaultStatusWordFilter())
		{
		}

		public CardBase(CardBase card)
		{
			hCard = card.hCard;
			sw = card.sw;
		}

		public virtual string Send(string apdu)
		{
			return SCardWrapper.BinToHex(Send(SCardWrapper.HexToBin(apdu)));
		}

		public virtual byte[] Send(byte[] apdu)
		{
			if (apdu.Length > 5 && apdu[4] != apdu.Length - 5 && fixBadLcLength)
			{
				apdu[4] = (byte)(apdu.Length - 5);
			}
			if (this.StartTransmit != null)
			{
				this.StartTransmit(this, 0);
			}
			byte[] array = SCardWrapper.Send(hCard, apdu);
			if (this.EndTransmit != null)
			{
				this.EndTransmit(this, 1);
			}
			Accept(array);
			if ($"{array[array.Length - 2]:X2}".Contains("61") && doGetResponse)
			{
				return GetResponse(array[array.Length - 1]);
			}
			return array;
		}

		protected virtual string RemoveSW(string response)
		{
			return Utils.BinToHex(RemoveSW(Utils.HexToBin(response)));
		}

		protected virtual byte[] RemoveSW(byte[] response)
		{
			byte[] array = new byte[response.Length - 2];
			Array.Copy(response, 0, array, 0, response.Length - 2);
			return array;
		}

		protected virtual void Accept(byte[] rxBuffer)
		{
			if (!sw.Accept(rxBuffer))
			{
				if (this.UnhandledStatusWord == null)
				{
					throw new Exception(Utils.BinToHex(rxBuffer, rxBuffer.Length - 2, 2));
				}
				StatusWord statusWord = new StatusWord(rxBuffer[rxBuffer.Length - 2], rxBuffer[rxBuffer.Length - 1]);
				this.UnhandledStatusWord(this, statusWord);
			}
		}

		public virtual byte[] GetResponse(byte len)
		{
			return Send(SCardWrapper.HexToBin("00C00000" + $"{len:X2}"));
		}

		public virtual bool Reset()
		{
			return SCardWrapper.Reset(hCard);
		}
	}
}
