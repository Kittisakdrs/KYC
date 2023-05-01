﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskQexe.IDReaderDotNet.Reader
{
	public enum SCardError : uint
	{
		SCARD_S_SUCCESS = 0u,
		SCARD_F_INTERNAL_ERROR = 2148532225u,
		SCARD_E_CANCELLED = 2148532226u,
		SCARD_E_INVALID_HANDLE = 2148532227u,
		SCARD_E_INVALID_PARAMETER = 2148532228u,
		SCARD_E_INVALID_TARGET = 2148532229u,
		SCARD_E_NO_MEMORY = 2148532230u,
		SCARD_F_WAITED_TOO_LONG = 2148532231u,
		SCARD_E_INSUFFICIENT_BUFFER = 2148532232u,
		SCARD_E_UNKNOWN_READER = 2148532233u,
		SCARD_E_TIMEOUT = 2148532234u,
		SCARD_E_SHARING_VIOLATION = 2148532235u,
		SCARD_E_NO_SMARTCARD = 2148532236u,
		SCARD_E_UNKNOWN_CARD = 2148532237u,
		SCARD_E_CANT_DISPOSE = 2148532238u,
		SCARD_E_PROTO_MISMATCH = 2148532239u,
		SCARD_E_NOT_READY = 2148532240u,
		SCARD_E_INVALID_VALUE = 2148532241u,
		SCARD_E_SYSTEM_CANCELLED = 2148532242u,
		SCARD_F_COMM_ERROR = 2148532243u,
		SCARD_F_UNKNOWN_ERROR = 2148532244u,
		SCARD_E_INVALID_ATR = 2148532245u,
		SCARD_E_NOT_TRANSACTED = 2148532246u,
		SCARD_E_READER_UNAVAILABLE = 2148532247u,
		SCARD_P_SHUTDOWN = 2148532248u,
		SCARD_E_PCI_TOO_SMALL = 2148532249u,
		SCARD_E_READER_UNSUPPORTED = 2148532250u,
		SCARD_E_DUPLICATE_READER = 2148532251u,
		SCARD_E_CARD_UNSUPPORTED = 2148532252u,
		SCARD_E_NO_SERVICE = 2148532253u,
		SCARD_E_SERVICE_STOPPED = 2148532254u,
		SCARD_E_UNEXPECTED = 2148532255u,
		SCARD_E_ICC_INSTALLATION = 2148532256u,
		SCARD_E_ICC_CREATEORDER = 2148532257u,
		SCARD_E_UNSUPPORTED_FEATURE = 2148532258u,
		SCARD_E_DIR_NOT_FOUND = 2148532259u,
		SCARD_E_FILE_NOT_FOUND = 2148532260u,
		SCARD_E_NO_DIR = 2148532261u,
		SCARD_E_NO_FILE = 2148532262u,
		SCARD_E_NO_ACCESS = 2148532263u,
		SCARD_E_WRITE_TOO_MANY = 2148532264u,
		SCARD_E_BAD_SEEK = 2148532265u,
		SCARD_E_INVALID_CHV = 2148532266u,
		SCARD_E_UNKNOWN_RES_MNG = 2148532267u,
		SCARD_E_NO_SUCH_CERTIFICATE = 2148532268u,
		SCARD_E_CERTIFICATE_UNAVAILABLE = 2148532269u,
		SCARD_E_NO_READERS_AVAILABLE = 2148532270u,
		SCARD_E_COMM_DATA_LOST = 2148532271u,
		SCARD_E_NO_KEY_CONTAINER = 2148532272u,
		SCARD_E_SERVER_TOO_BUSY = 2148532273u,
		SCARD_W_UNSUPPORTED_CARD = 2148532325u,
		SCARD_W_UNRESPONSIVE_CARD = 2148532326u,
		SCARD_W_UNPOWERED_CARD = 2148532327u,
		SCARD_W_RESET_CARD = 2148532328u,
		SCARD_W_REMOVED_CARD = 2148532329u,
		SCARD_W_SECURITY_VIOLATION = 2148532330u,
		SCARD_W_WRONG_CHV = 2148532331u,
		SCARD_W_CHV_BLOCKED = 2148532332u,
		SCARD_W_EOF = 2148532333u,
		SCARD_W_CANCELLED_BY_USER = 2148532334u,
		SCARD_W_CARD_NOT_AUTHENTICATED = 2148532335u
	}
}
