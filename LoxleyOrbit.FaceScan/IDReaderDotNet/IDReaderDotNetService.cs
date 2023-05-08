using KioskQexe.IDReaderDotNet.Card;
using KioskQexe.IDReaderDotNet.Common;
using KioskQexe.IDReaderDotNet.Reader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace KioskQexe.IDReaderDotNet
{

    public class IDReaderDotNetService
    {
        private delegate void CardEventCallback(string message);

        private string readerName;
        private Form formUI;
        private System.Timers.Timer timerDetect;
        private string msgErr;
        private bool lastState;
        public EventHandler OnCardInserted;
        public EventHandler OnCardRemoved;
        public IDReaderDotNetService(string readerName)
        {
            this.readerName = readerName;
            formUI = null;
            timerDetect = null;
            lastState = false;
        }
        public IDReaderDotNetService(string readerName, Form formUI)
        {
            this.readerName = readerName;
            if (formUI != null)
            {
                StartPolling();
            }
            this.formUI = formUI;
            lastState = false;
        }

        public DataTable ReadData(bool photoRequired)
        {
            if (string.IsNullOrEmpty(readerName))
            {
                msgErr = "Smartcard reader not specified";
                return null;
            }
            DataTable dataTable = null;
            if (GetReaderStatus(readerName))
            {
                msgErr = string.Empty;
                try
                {
                    IntPtr hCard = SCardWrapper.Connect(readerName);
                    Thread.Sleep(5);
                    ThaiNidCard thaiNidCard = new ThaiNidCard(hCard);
                    if (thaiNidCard.Select())
                    {
                        dataTable = thaiNidCard.ReadHolderProfile(photoRequired);
                        dataTable.Rows[0]["AtrString"] = Utils.BinToHex(thaiNidCard.Status.ATR);
                    }
                    else
                    {
                        msgErr = thaiNidCard.GetErrorMessage();
                    }
                    SCardWrapper.Disconnect(hCard);
                }
                catch (Exception ex)
                {
                    msgErr = ex.Message;
                }
            }
            else
            {
                msgErr = $"Card is not presented in reader ({readerName})";
            }
            return dataTable;
        }

        public string GetErrorMessage()
        {
            return msgErr;
        }

        private bool GetReaderStatus(string readerName)
        {
            //SCardState statusChange = SCardWrapper.GetStatusChange(readerName, 0, SCardState.SCARD_STATE_UNAWARE);
            //if ((statusChange & SCardState.SCARD_STATE_PRESENT) == SCardState.SCARD_STATE_PRESENT)
            //{
                return true;
            //}
            //return false;
        }
        public static string[] GetReaderLists()
        {
            return SCardWrapper.ListReaders();
        }
        public static string GetReaderSerial(string readerName)
        {
            IntPtr intPtr = SCardWrapper.Connect(readerName, SCardShare.SCARD_SHARE_DIRECT, (SCardProtocol)0);
            string result = string.Empty;
            if (intPtr != IntPtr.Zero)
            {
                uint dwAttrId = 65795u;
                IntPtr pbAttr = IntPtr.Zero;
                uint pcbAttrLen = uint.MaxValue;
                if (SCardDll.SCardGetAttrib(intPtr, dwAttrId, ref pbAttr, ref pcbAttrLen) == 0)
                {
                    result = Marshal.PtrToStringAnsi(pbAttr, (int)pcbAttrLen);
                    SCardDll.SCardFreeMemory(SCardWrapper.Context, pbAttr);
                    StringBuilder stringBuilder = new StringBuilder(result);
                    stringBuilder = stringBuilder.Replace('\0', ' ');
                    result = stringBuilder.ToString().Trim();
                }
                SCardWrapper.Disconnect(intPtr);
            }
            return result;
        }


        private string GetReaderName(string modelName)
        {
            string[] array = SCardWrapper.ListReaders();
            if (array == null || array.Length == 0)
            {
                return null;
            }
            string result = null;
            if (modelName.Equals("Not specified"))
            {
                result = array[0];
            }
            else
            {
                string[] array2 = array;
                foreach (string text in array2)
                {
                    if (text.CompareTo(modelName) > 0)
                    {
                        result = text;
                        break;
                    }
                }
            }
            return result;
        }

        private void timerDetect_Elapsed(object sender, ElapsedEventArgs e)
        {
            bool readerStatus = GetReaderStatus(readerName);
            if (!lastState && readerStatus)
            {
                lastState = true;
                raise_OnCardInserted();
            }
            if (lastState && !readerStatus)
            {
                lastState = false;
                raise_OnCardRemoved();
                GC.Collect();
            }
        }

        private void raise_OnCardInserted()
        {
            Debug.WriteLine("Card inserted!");
            if (formUI.InvokeRequired)
            {
                formUI.BeginInvoke(new CardEventCallback(OnCardStatusChanged), "insert");
            }
            else
            {
                OnCardStatusChanged("insert");
            }
        }

        private void raise_OnCardRemoved()
        {
            Debug.WriteLine("Card removed!");
            if (formUI.InvokeRequired)
            {
                formUI.BeginInvoke(new CardEventCallback(OnCardStatusChanged), "remove");
            }
            else
            {
                OnCardStatusChanged("remove");
            }
        }

        private void OnCardStatusChanged(string status)
        {
            if (status.Contains("insert"))
            {
                StopPolling();
                if (OnCardInserted != null)
                {
                    OnCardInserted(this, null);
                }
                StartPolling();
            }
            if (status.Contains("remove"))
            {
                if (OnCardRemoved != null)
                {
                    OnCardRemoved(this, null);
                }
                GC.Collect();
            }
        }
        private void StartPolling()
        {
            timerDetect = new System.Timers.Timer();
            timerDetect.Interval = 1000.0;
            timerDetect.Elapsed += timerDetect_Elapsed;
            timerDetect.Start();
        }

        private void StopPolling()
        {
            if (timerDetect != null)
            {
                timerDetect.Elapsed -= timerDetect_Elapsed;
                timerDetect.Stop();
                timerDetect.Dispose();
                timerDetect = null;
            }
        }
    }
}
