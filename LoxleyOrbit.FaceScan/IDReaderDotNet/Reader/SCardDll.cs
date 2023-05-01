using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KioskQexe.IDReaderDotNet.Reader
{

	public class SCardDll
	{
		[DllImport("winscard")]
		public static extern uint SCardEstablishContext(SCardScope dwScope, IntPtr pvReserved1, IntPtr pvReserved2, ref IntPtr phContext);

		[DllImport("winscard")]
		public static extern uint SCardReleaseContext(IntPtr hContext);

		[DllImport("winscard")]
		public static extern uint SCardListReaders(IntPtr hContext, byte[] mszGroups, byte[] mszReaders, ref int pcchReaders);

		[DllImport("winscard")]
		public static extern uint SCardConnect(IntPtr hContext, [MarshalAs(UnmanagedType.LPStr)] string szReader, SCardShare dwShareMode, SCardProtocol dwPreferredProtocols, ref IntPtr phCard, ref int pdwActiveProtocol);

		[DllImport("winscard")]
		public static extern uint SCardReconnect(ref IntPtr phCard, SCardShare dwShareMode, SCardProtocol dwPreferredProtocols, SCardDisposition dwInitialization, ref int pdwActiveProtocol);

		[DllImport("winscard")]
		public static extern uint SCardDisconnect(IntPtr hCard, SCardDisposition dwDisposition);

		[DllImport("winscard")]
		public static extern uint SCardTransmit(IntPtr hCard, ref SCARD_IO_REQUEST pioSendPci, byte[] pbSendBuffer, int cbSendBuffer, ref SCARD_IO_REQUEST pioRecvPci, byte[] pbRecvBuffer, ref int pcbRecvLength);

		[DllImport("winscard", CharSet = CharSet.Ansi)]
		public static extern uint SCardStatus(IntPtr hCard, byte[] szReaderName, ref int pcchReaderLen, ref int pdwState, ref int pdwProtocol, byte[] pbAtr, ref int pcbAtrLen);

		[DllImport("winscard")]
		public static extern uint SCardGetStatusChange(IntPtr hContext, int dwTimeout, [In][Out] SCARD_READERSTATE[] rgReaderStates, int cReaders);

		[DllImport("winscard")]
		public static extern uint SCardCancel(IntPtr hContext);

		[DllImport("WINSCARD.DLL")]
		internal static extern int SCardGetAttrib(IntPtr hCard, uint dwAttrId, ref IntPtr pbAttr, ref uint pcbAttrLen);

		[DllImport("WINSCARD.DLL")]
		internal static extern int SCardFreeMemory(IntPtr hContext, IntPtr pvMem);
	}
}
