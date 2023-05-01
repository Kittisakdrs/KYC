using KioskQexe.IDReaderDotNet.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskQexe.IDReaderDotNet.Reader
{

	public class SCardWrapper
	{
		public delegate void ConnectionEvent(string readerName);

		private static IntPtr ctx;

		private static bool context;

		private static SCardProtocol currentProtocol;

		public static IntPtr Context => ctx;

		public static event ConnectionEvent Connected;

		public static event ConnectionEvent Disconnected;

		static SCardWrapper()
		{
			context = false;
			currentProtocol = SCardProtocol.SCARD_PROTOCOL_T0;
			if (!context)
			{
				if (0 != SCardDll.SCardEstablishContext(SCardScope.SCARD_SCOPE_SYSTEM, IntPtr.Zero, IntPtr.Zero, ref ctx))
				{
					throw new Exception("Unable to establish smart card context");
				}
				context = true;
			}
		}

		public static string[] ListReaders()
		{
			int pcchReaders = 256;
			byte[] array = new byte[pcchReaders];
			if (0 != SCardDll.SCardListReaders(ctx, null, array, ref pcchReaders))
			{
				return null;
			}
			int num = 0;
			int i;
			for (i = 0; i < pcchReaders - 1; i++)
			{
				if (array[i] == 0)
				{
					num++;
				}
			}
			string[] array2 = new string[num];
			i = 0;
			int num2 = 0;
			for (; i < num; i++)
			{
				array2[i] = byteArrayToString(array, num2);
				num2 += array2[i].Length + 1;
			}
			return array2;
		}

		private static string byteArrayToString(byte[] array, int offset)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = offset; i < array.Length && array[i] != 0; i++)
			{
				stringBuilder.Append($"{(char)array[i]}");
			}
			return stringBuilder.ToString();
		}

		public static IntPtr Connect(string readerName)
		{
			return Connect(readerName, SCardShare.SCARD_SHARE_SHARED, (SCardProtocol)3);
		}

		public static IntPtr Connect(string readerName, SCardShare access)
		{
			return Connect(readerName, access, (SCardProtocol)3);
		}

		public static IntPtr Connect(string readerName, SCardShare access, SCardProtocol protocol)
		{
			int pdwActiveProtocol = 1;
			IntPtr phCard = IntPtr.Zero;
			uint num = SCardDll.SCardConnect(ctx, readerName, access, protocol, ref phCard, ref pdwActiveProtocol);
			currentProtocol = (SCardProtocol)pdwActiveProtocol;
			if (num == 0)
			{
				if (SCardWrapper.Connected != null)
				{
					SCardWrapper.Connected(readerName);
				}
				return phCard;
			}
			return IntPtr.Zero;
		}

		public static uint Reconnect(IntPtr hCard)
		{
			return Reconnect(hCard, SCardShare.SCARD_SHARE_SHARED, (SCardProtocol)3);
		}

		public static uint Reconnect(IntPtr hCard, SCardShare access)
		{
			return Reconnect(hCard, access, (SCardProtocol)3);
		}

		public static uint Reconnect(IntPtr hCard, SCardShare access, SCardProtocol protocol)
		{
			int pdwActiveProtocol = 1;
			uint result = SCardDll.SCardReconnect(ref hCard, access, protocol, SCardDisposition.UNPOWER_CARD, ref pdwActiveProtocol);
			currentProtocol = (SCardProtocol)pdwActiveProtocol;
			return result;
		}

		public static void Disconnect(IntPtr hCard)
		{
			SCardDll.SCardDisconnect(hCard, SCardDisposition.UNPOWER_CARD);
			if (SCardWrapper.Disconnected != null)
			{
				SCardWrapper.Disconnected("");
			}
		}

		public static byte[] Send(IntPtr hCard, byte[] apdu)
		{
			int pcbRecvLength = 256;
			byte[] array = new byte[pcbRecvLength];
			if (apdu.Length < 4 || apdu.Length > 300)
			{
				throw new Exception("Invalid APDU length. APDU length must be between 4 and 300");
			}
			SCARD_IO_REQUEST pioSendPci = default(SCARD_IO_REQUEST);
			SCARD_IO_REQUEST pioRecvPci = default(SCARD_IO_REQUEST);
			pioRecvPci.dwProtocol = currentProtocol;
			pioRecvPci.cbPciLength = 264;
			pioSendPci.dwProtocol = currentProtocol;
			pioSendPci.cbPciLength = 8;
			uint num = SCardDll.SCardTransmit(hCard, ref pioSendPci, apdu, apdu.Length, ref pioRecvPci, array, ref pcbRecvLength);
			if (num != 0)
			{
				throw new Exception($"Error sending data to card: {num:X4}");
			}
			byte[] array2 = new byte[pcbRecvLength];
			Array.Copy(array, array2, pcbRecvLength);
			return array2;
		}

		public static SmartStatus GetStatus(IntPtr hCard)
		{
			SmartStatus result = default(SmartStatus);
			if (hCard == IntPtr.Zero)
			{
				return result;
			}
			byte[] array = new byte[256];
			int pcchReaderLen = 256;
			int pdwState = 0;
			int pdwProtocol = 0;
			byte[] array2 = new byte[32];
			int pcbAtrLen = 32;
			if (SCardDll.SCardStatus(hCard, array, ref pcchReaderLen, ref pdwState, ref pdwProtocol, array2, ref pcbAtrLen) == 0)
			{
				result.ReaderName = Utils.HexToAscii(Utils.BinToHex(array, 0, pcchReaderLen - 2));
				result.ReaderState = (SCardReaderState)pdwState;
				result.ReaderProtocol = (SCardProtocol)pdwProtocol;
				result.ATR = new byte[pcbAtrLen];
				Array.Copy(array2, result.ATR, pcbAtrLen);
			}
			return result;
		}

		public static bool Reset(IntPtr hCard)
		{
			int pdwActiveProtocol = 1;
			uint num = SCardDll.SCardReconnect(ref hCard, SCardShare.SCARD_SHARE_SHARED, SCardProtocol.SCARD_PROTOCOL_T0, SCardDisposition.RESET_CARD, ref pdwActiveProtocol);
			return num == 0;
		}

		public static SCardState GetStatusChange(string readerName, int timeout, SCardState currentState)
		{
			SCARD_READERSTATE[] array = new SCARD_READERSTATE[1];
			array[0].szReader = readerName;
			array[0].dwCurrentState = currentState;
			array[0].pvUserData = IntPtr.Zero;
			array[0].dwEventState = SCardState.SCARD_STATE_UNAWARE;
			array[0].cbAtr = 0;
			array[0].rgbAtr = null;
			if (SCardDll.SCardGetStatusChange(ctx, timeout, array, array.Length) == 0)
			{
				return array[0].dwEventState;
			}
			return SCardState.SCARD_STATE_UNKNOWN;
		}

		public static string CardStateToString(SCardState state)
		{
			string text = "";
			if ((state & SCardState.SCARD_STATE_CHANGED) == SCardState.SCARD_STATE_CHANGED)
			{
				text += "Changed:";
			}
			if ((state & SCardState.SCARD_STATE_EMPTY) == SCardState.SCARD_STATE_EMPTY)
			{
				text += "Empty:";
			}
			if ((state & SCardState.SCARD_STATE_ATRMATCH) == SCardState.SCARD_STATE_ATRMATCH)
			{
				text += "ATR Match:";
			}
			if ((state & SCardState.SCARD_STATE_EXCLUSIVE) == SCardState.SCARD_STATE_EXCLUSIVE)
			{
				text += "Exclusive:";
			}
			if ((state & SCardState.SCARD_STATE_IGNORE) == SCardState.SCARD_STATE_IGNORE)
			{
				text += "Ignore:";
			}
			if ((state & SCardState.SCARD_STATE_INUSE) == SCardState.SCARD_STATE_INUSE)
			{
				text += "In Use:";
			}
			if ((state & SCardState.SCARD_STATE_MUTE) == SCardState.SCARD_STATE_MUTE)
			{
				text += "Mute:";
			}
			if ((state & SCardState.SCARD_STATE_PRESENT) == SCardState.SCARD_STATE_PRESENT)
			{
				text += "Present:";
			}
			if ((state & SCardState.SCARD_STATE_UNAVAILABLE) == SCardState.SCARD_STATE_UNAVAILABLE)
			{
				text += "Unavailable:";
			}
			if ((state & SCardState.SCARD_STATE_UNKNOWN) == SCardState.SCARD_STATE_UNKNOWN)
			{
				text += "Unknown:";
			}
			if ((state & SCardState.SCARD_STATE_UNPOWERED) == SCardState.SCARD_STATE_UNPOWERED)
			{
				text += "Unpowered:";
			}
			return text;
		}

		public static byte[] HexToBin(string hex)
		{
			return Utils.HexToBin(hex);
		}

		public static string BinToHex(byte[] bin)
		{
			return Utils.BinToHex(bin);
		}

		~SCardWrapper()
		{
			if (context)
			{
				SCardDll.SCardReleaseContext(ctx);
				context = false;
			}
		}

		public static string ReaderStateToString(SCardReaderState s)
		{
			switch (s)
			{
				case SCardReaderState.SCARD_ABSENT:
					return "Card is absent";
				case SCardReaderState.SCARD_NEGOTIABLE:
					return "Card is awaiting PTS negotiation";
				case SCardReaderState.SCARD_POWERED:
					return "Card is powered";
				case SCardReaderState.SCARD_PRESENT:
					return "Card is present";
				case SCardReaderState.SCARD_SPECIFIC:
					return "Card is powered and PTS is negotiated";
				case SCardReaderState.SCARD_SWALLOWED:
					return "Card is swallowed";
				case SCardReaderState.SCARD_UNKNOWN:
					return "Unknown state";
				default:
					return "Unknown state - unknown state returned";
			}
		}

		public static string ProtocolToString(SCardProtocol p)
		{
			switch (p)
			{
				case SCardProtocol.SCARD_PROTOCOL_RAW:
					return "RAW";
				case SCardProtocol.SCARD_PROTOCOL_T0:
					return "T=0";
				case SCardProtocol.SCARD_PROTOCOL_T1:
					return "T=1";
				default:
					return "Unknown";
			}
		}
	}
}
