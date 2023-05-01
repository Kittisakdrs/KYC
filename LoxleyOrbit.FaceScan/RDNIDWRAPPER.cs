﻿//#define TEST_BMP

using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.IO;

namespace RDNIDWRAPPER
{
    internal static partial class DefineConstants
    {
        public const int NID_SUCCESS = 0;
        public const int NID_INTERNAL_ERROR = -1;
        public const int NID_INVALID_LICENSE = -2;
        public const int NID_READER_NOT_FOUND = -3;
        public const int NID_CONNECTION_ERROR = -4;
        public const int NID_GET_PHOTO_ERROR = -5;
        public const int NID_GET_DATA_ERROR = -6;
        public const int NID_INVALID_CARD = -7;
        public const int NID_UNKNOWN_CARD_VERSION = -8;
        public const int NID_DISCONNECTION_ERROR = -9;
        public const int NID_INIT_ERROR = -10;
        public const int NID_SUPPORTED_READER_NOT_FOUND = -11;
    }

    class RDNIDLib
    {

        const string _RDNIDLib_DLL_ = "RDNIDLibD.DLL";

        //Function openlib Old.
        [DllImport(_RDNIDLib_DLL_,
                EntryPoint = "OpenNIDLib", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern Int32 OpenNIDLib(byte[] _szReaders);

        //Function openlib New.
        [DllImport(_RDNIDLib_DLL_,
                EntryPoint = "openNIDLibRD", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Int32 openNIDLibRD(byte[] _szReaders);

        [DllImport(_RDNIDLib_DLL_,
                EntryPoint = "getReaderListRD", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Int32 getReaderListRD([Out] byte[] _szReaders, int size);

        [DllImport(_RDNIDLib_DLL_,
                EntryPoint = "selectReaderRD", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int selectReaderRD(byte[] _szReaders);

        [DllImport(_RDNIDLib_DLL_,
                EntryPoint = "deselectReaderRD", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Int32 deselectReaderRD(int obj);

        //Function connectCard Old.
        [DllImport(_RDNIDLib_DLL_,
                EntryPoint = "isCardInsertRD", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Int32 isCardInsertRD(int obj);

        //Function connectCard New.
        [DllImport(_RDNIDLib_DLL_,
                EntryPoint = "connectCardRD", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Int32 connectCardRD(int obj);

        [DllImport(_RDNIDLib_DLL_,
                EntryPoint = "disconnectCardRD", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Int32 disconnectCardRD(int obj);

        [DllImport(_RDNIDLib_DLL_,
                EntryPoint = "getNIDNumberRD", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Int32 getNIDNumberRD(int obj, [Out] byte[] strID);

        [DllImport(_RDNIDLib_DLL_,
                 EntryPoint = "getNIDTextRD", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Int32 getNIDTextRD(int obj, [Out] byte[] strData, int sizeData);

        [DllImport(_RDNIDLib_DLL_,
        EntryPoint = "getATextDD", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Int32 getATextDD(int obj, [Out] byte[] strData, int sizeData);

        [DllImport(_RDNIDLib_DLL_,
                EntryPoint = "getNIDPhotoRD", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Int32 getNIDPhotoRD(int obj, [Out] byte[] strData, out int sizeData);

        [DllImport(_RDNIDLib_DLL_,
                EntryPoint = "getSoftwareInfoRD", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Int32 getSoftwareInfoRD([Out] byte[] strData);

        //Function getLicenseInfo Old.
        [DllImport(_RDNIDLib_DLL_,
                EntryPoint = "getDLXInfoRD", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Int32 getDLXInfoRD([Out] byte[] strData);

        //Function getLicenseInfo New.
        [DllImport(_RDNIDLib_DLL_,
                EntryPoint = "getLicenseInfoRD", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Int32 getLicenseInfoRD([Out] byte[] strData);

        [DllImport(_RDNIDLib_DLL_,
                EntryPoint = "updateLicenseFileRD", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Int32 updateLicenseFileRD(byte[] targetPath);

        [DllImport(_RDNIDLib_DLL_,
                EntryPoint = "getRidDD", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Int32 getRidDD(int obj, [Out] byte[] strData);


        [DllImport(_RDNIDLib_DLL_)]
        public static extern Int32 closeNIDLibRD();


    }
}
