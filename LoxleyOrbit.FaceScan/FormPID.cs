using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KioskQexe.IDReaderDotNet;
using LoxleyOrbit.FaceScan.Models;
using Microsoft.Win32;
using RDNIDWRAPPER;
using WMPLib;

namespace LoxleyOrbit.FaceScan
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class FormPID : Form
    {
        private IDReaderDotNetService reader = null;
        string StartupPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        Stopwatch speedReadText;
        Stopwatch speedReadAll;

        public FormPID()
        {
            InitializeComponent();
            SetBrowserCompatibilityMode();

            string fileName = StartupPath + "\\RDNIDLib.DLD";

            if (System.IO.File.Exists(fileName) == false)
            {
                MessageBox.Show("RDNIDLib.DLD not found");
            }

            System.Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

            this.Text = String.Format("Loxley Orbit NID Card Plus C# V{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);

            //IntPtr mNIDLibHandle = (IntPtr)0;
            byte[] _lic = String2Byte(fileName);

            int nres = 0;
            //nres = RDNIDLib.openNIDLibRD(_lic);                     //for openLib

            //long newlong = (long)mNIDLibHandle;

            if (nres != 0)
            {
                String m;
                m = String.Format("Error: {0} ", nres);
                MessageBox.Show(m);
            }

            byte[] Licinfo = new byte[1024];

            //RDNIDLib.getLicenseInfoRD(Licinfo);

            //m_lblDLDInfo.Text = "License Info: " + aByteToString(Licinfo);

            byte[] Softinfo = new byte[1024];
            //RDNIDLib.getSoftwareInfoRD(Softinfo);
            //m_lblSoftwareInfo.Text = "RDNIDLib Info: " + aByteToString(Softinfo);

            byte[] ZDLibinfo = new byte[1024];

            ListCardReader();

            //lblStatus.Text = "Status: Ready";

            reader = new IDReaderDotNetService("Identiv uTrust 2700 R Smart Card Reader 0", this); //ตัวอ่านบัตรตัวใหม่
            reader.OnCardInserted += new EventHandler(OnCardInserted);

            //m_txtID.Text = "1570800044928";

            string fullscreen = Properties.Settings.Default.FullScreen.ToString();
            if (fullscreen == "Y") //Non Kiosk
            {
                this.ControlBox = true;
                this.MinimizeBox = true;
                this.MaximizeBox = true;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Maximized;
                webBrowser.Height = this.Height;
                webBrowser.Width = this.Width;
                webBrowser.ScrollBarsEnabled = true;
            }
            else // Kiosk
            {
                this.ControlBox = false;
                this.MinimizeBox = false;
                this.MaximizeBox = false;
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                webBrowser.Height = this.Height;
                webBrowser.Width = this.Width;
                webBrowser.ScrollBarsEnabled = false;
            }

            webBrowser.AllowWebBrowserDrop = false;
            webBrowser.IsWebBrowserContextMenuEnabled = false;
            webBrowser.WebBrowserShortcutsEnabled = false;

            webBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser_DocumentCompleted);
        }

        private void SetBrowserCompatibilityMode()
        {
            var fileName = Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);

            if (String.Compare(fileName, "devenv.exe", true) == 0) // make sure we're not running inside Visual Studio
                return;

            using (var key = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION",
                RegistryKeyPermissionCheck.ReadWriteSubTree))
            {
                UInt32 mode = 10000;
                key.SetValue(fileName, mode, RegistryValueKind.DWord);
            }

            using (var key = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BLOCK_LMZ_SCRIPT",
                RegistryKeyPermissionCheck.ReadWriteSubTree))
            {
                UInt32 mode = 0;
                key.SetValue(fileName, mode, RegistryValueKind.DWord);
            }

            using (var key = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_NINPUT_LEGACYMODE",
                RegistryKeyPermissionCheck.ReadWriteSubTree))
            {
                UInt32 mode = 0;
                key.SetValue(fileName, mode, RegistryValueKind.DWord);
            }
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string text = webBrowser.DocumentTitle;
            //if (text.ToLower().Contains("cancel") || text.ToLower().Contains("this page can’t be displayed")
            //    || text.ToLower().Contains("cannot"))
            //    Server_down = true;
            //else
            //    Server_down = false;
        }

        private void FormPID_Load(object sender, EventArgs e)
        {
            webBrowser.AllowWebBrowserDrop = false;
            webBrowser.IsWebBrowserContextMenuEnabled = false;
            webBrowser.WebBrowserShortcutsEnabled = false;
            webBrowser.ObjectForScripting = this;
            //webBrowser.Navigate("https://localhost:44342/SelectUserType");
            //webBrowser.Navigate("https://localhost:44342/User_detail");
            //webBrowser.Navigate("https://localhost:44342/User_Hn");
            webBrowser.Navigate("http://test-kiosk.chulacareapp.com/OneMLWeb/SelectUserType.aspx");
            StartTimer();
            axWindowsMediaPlayer1.SendToBack();
            axWindowsMediaPlayer1.Visible = false;
            //RegisterUserAsync();
        }

        private void btnReadcard_Click(object sender, EventArgs e)
        {
            StopTimer();
            lbl_Time.Text = "";
            m_txtID.Text = "";
            m_txtFullNameT.Text = "";
            m_txtFullNameE.Text = "";
            m_txtAddress.Text = "";
            m_txtBrithDate.Text = "";
            m_txtGender.Text = "";
            m_txtIssueDate.Text = "";
            m_txtExpiryDate.Text = "";
            m_txtIssueNum.Text = "";
            m_txtIssueplace.Text = "";
            m_picPhoto.Image = null;
            lblStatus.Text = "Status: Reading...";

            ReadCard();
        }

        private void btngetReaderID_Click(object sender, EventArgs e)
        {
            //lblStatus.Text = "Status: Reading...";
            //lbl_Time.Text = "";
            //m_txtReaderID.ResetText();
            //this.Refresh();
            //String strTerminal = m_ListReaderCard.GetItemText(m_ListReaderCard.SelectedItem);

            //int obj = selectReader(strTerminal);
            //if (obj < 0)
            //{
            //    String m;
            //    m = String.Format("Error: {0} ", obj);
            //    lblStatus.Text = "Status: Error " + obj;
            //    MessageBox.Show(m, Application.ProductName);

            //    RDNIDLib.disconnectCardRD(obj);
            //    RDNIDLib.deselectReaderRD(obj);
            //    return;
            //}
            //byte[] Rid = new byte[16];
            //int res = RDNIDLib.getRidDD(obj, Rid);
            //if (res > 0)
            //{
            //    m_txtReaderID.Text = BitConverter.ToString(Rid).Replace("-", " ");
            //    lblStatus.Text = "Status: Ready";
            //}
            //else
            //{
            //    m_txtReaderID.Text = String.Format("{0}", res);
            //    lblStatus.Text = String.Format("Status: Error {0}", res);

            //    MessageBox.Show(String.Format("Error: {0}", res), Application.ProductName);
            //}

            ////m_txtGetRidRY.Text = mRDNIDWRAPPER.getRidDD();

            //RDNIDLib.deselectReaderRD(obj);
        }

        private void btnClearScreen_Click(object sender, EventArgs e)
        {
            lbl_Time.Text = "";
            m_txtID.Text = "";
            m_txtFullNameT.Text = "";
            m_txtFullNameE.Text = "";
            m_txtAddress.Text = "";
            m_txtBrithDate.Text = "";
            m_txtGender.Text = "";
            m_txtIssueDate.Text = "";
            m_txtExpiryDate.Text = "";
            m_txtIssueNum.Text = "";
            m_txtReaderID.Text = "";
            m_txtIssueplace.Text = "";
            m_picPhoto.Image = null;
        }

        public void to_complete(string param)
        {
            //FormKYC formKYC = new FormKYC();
            //formKYC.m_txtID = m_txtID.Text.Trim();
            //formKYC.m_type = "ID"
            //formKYC.Show();

            //try
            //{
            //    this.Invoke(new MethodInvoker(delegate ()
            //    {
            //        object result = webBrowser.Document.InvokeScript("to_complete", new string[] { param }); // second parameter is jsonString
            //    }));
            //}
            //catch (WebException)
            //{
            //    return;
            //}
        }

        public void to_notsuccess(String TitleText, String ContentText, bool showImage)
        {
            StartTimer();
            // web hide progress
            this.Invoke(new MethodInvoker(delegate ()
            {
                object result = webBrowser.Document.InvokeScript("redirect_to_User_Hn"); // second parameter is jsonString
            }));
        }

        protected int ReadCard()
        {
            //speedReadAll = new Stopwatch();
            //speedReadText = new Stopwatch();

            //speedReadAll.Start();
            //speedReadText.Start();

            //StopTimer();

            //try
            //{
            //    this.Invoke(new MethodInvoker(delegate ()
            //    {
            //        object result = webBrowser.Document.InvokeScript("open_progress"); // second parameter is jsonString
            //    }));
            //}
            //catch { };

            //String strTerminal = m_ListReaderCard.GetItemText(m_ListReaderCard.SelectedItem);
            //int obj = selectReader(strTerminal);
            //if (obj < 0)
            //{
            //    String m;
            //    m = String.Format("Error: {0} ", obj);

            //    lblStatus.Text = "Status: Error " + obj;
            //    MessageBox.Show(m, Application.ProductName);

            //    RDNIDLib.disconnectCardRD(obj);
            //    RDNIDLib.deselectReaderRD(obj);

            //    to_notsuccess("", "", false);
            //    return obj;
            //}

            //Int32 nInsertCard = 0;
            //nInsertCard = RDNIDLib.connectCardRD(obj);
            //if (nInsertCard != DefineConstants.NID_SUCCESS)
            //{
            //    String m;
            //    m = String.Format("Error: {0} ", nInsertCard);

            //    lblStatus.Text = "Status: Error " + nInsertCard;
            //    MessageBox.Show(m, Application.ProductName);

            //    RDNIDLib.disconnectCardRD(obj);
            //    RDNIDLib.deselectReaderRD(obj);

            //    to_notsuccess("", "", false);
            //    return nInsertCard;
            //}

            //byte[] data = new byte[1024];
            //int res = RDNIDLib.getNIDTextRD(obj, data, data.Length);
            //if (res != DefineConstants.NID_SUCCESS)
            //{
            //    String m;
            //    m = String.Format("Error: {0} ", res);

            //    lblStatus.Text = "Status: Error " + res;
            //    MessageBox.Show(m, Application.ProductName);
            //    speedReadText.Stop();
            //    speedReadAll.Stop();
            //    RDNIDLib.disconnectCardRD(obj);
            //    RDNIDLib.deselectReaderRD(obj);

            //    to_notsuccess("", "", false);
            //    return res;
            //}

            //speedReadText.Stop();

            //String NIDData = aByteToString(data);
            //if (NIDData == "")
            //{
            //    lblStatus.Text = "Status: Read Text error";

            //    to_notsuccess("", "", false);
            //    MessageBox.Show("Read Text error");
            //}
            //else
            //{
            //    string[] fields = NIDData.Split('#');

            //    m_txtID.Text = fields[(int)NID_FIELD.NID_Number];                             // or use m_txtID.Text = fields[(int)NID_FIELD.NID_Number];

            //    String fullname = fields[(int)NID_FIELD.TITLE_T] + " " +
            //                        fields[(int)NID_FIELD.NAME_T] + " " +
            //                        fields[(int)NID_FIELD.MIDNAME_T] + " " +
            //                        fields[(int)NID_FIELD.SURNAME_T].TrimEnd();  //Remove space
            //    m_txtFullNameT.Text = fullname;

            //    fullname = fields[(int)NID_FIELD.TITLE_E] + " " +
            //                        fields[(int)NID_FIELD.NAME_E] + " " +
            //                        fields[(int)NID_FIELD.MIDNAME_E] + " " +
            //                        fields[(int)NID_FIELD.SURNAME_E].TrimEnd();  //Remove space
            //    ;
            //    m_txtFullNameE.Text = fullname;

            //    m_txtBrithDate.Text = _yyyymmdd_(fields[(int)NID_FIELD.BIRTH_DATE]);

            //    string txtAddrees = "";

            //    if (fields[(int)NID_FIELD.HOME_NO] != "") txtAddrees += fields[(int)NID_FIELD.HOME_NO] + "   ";
            //    if (fields[(int)NID_FIELD.MOO] != "") txtAddrees += fields[(int)NID_FIELD.MOO] + "   ";
            //    if (fields[(int)NID_FIELD.TROK] != "") txtAddrees += fields[(int)NID_FIELD.TROK] + "   ";
            //    if (fields[(int)NID_FIELD.SOI] != "") txtAddrees += fields[(int)NID_FIELD.SOI] + "   ";
            //    if (fields[(int)NID_FIELD.ROAD] != "") txtAddrees += fields[(int)NID_FIELD.ROAD] + "   ";
            //    if (fields[(int)NID_FIELD.TUMBON] != "") txtAddrees += fields[(int)NID_FIELD.TUMBON] + "   ";
            //    if (fields[(int)NID_FIELD.AMPHOE] != "") txtAddrees += fields[(int)NID_FIELD.AMPHOE] + "   ";
            //    if (fields[(int)NID_FIELD.PROVINCE] != "") txtAddrees += fields[(int)NID_FIELD.PROVINCE].TrimEnd();

            //    m_txtAddress.Text = txtAddrees;

            //    if (fields[(int)NID_FIELD.GENDER] == "1")
            //    {
            //        m_txtGender.Text = "ชาย";
            //    }
            //    else if (fields[(int)NID_FIELD.GENDER] == "2")
            //    {
            //        m_txtGender.Text = "หญิง";
            //    }

            //    m_txtIssueDate.Text = _yyyymmdd_(fields[(int)NID_FIELD.ISSUE_DATE]);
            //    m_txtExpiryDate.Text = _yyyymmdd_(fields[(int)NID_FIELD.EXPIRY_DATE]);
            //    if ("99999999" == m_txtExpiryDate.Text)
            //        m_txtExpiryDate.Text = "99999999 ตลอดชีพ";
            //    m_txtIssueNum.Text = fields[(int)NID_FIELD.ISSUE_NUM];
            //    m_txtIssueplace.Text = fields[(int)NID_FIELD.ISSUE_PLACE].TrimEnd();  //Remove space
            //}

            //byte[] NIDPicture = new byte[1024 * 5];
            //int imgsize = NIDPicture.Length;
            //res = RDNIDLib.getNIDPhotoRD(obj, NIDPicture, out imgsize);

            //if (res != DefineConstants.NID_SUCCESS)
            //{
            //    String m;
            //    m = String.Format("Error: {0} ", res);

            //    lblStatus.Text = "Status: Error " + res;
            //    MessageBox.Show(m, Application.ProductName);
            //    speedReadAll.Stop();
            //    RDNIDLib.disconnectCardRD(obj);
            //    RDNIDLib.deselectReaderRD(obj);

            //    to_notsuccess("", "", false);
            //    return res;
            //}

            //speedReadAll.Stop();

            //lbl_Time.Text = string.Format("Read Text: {0} s, Text + Photo: {1} s", speedReadText.Elapsed.TotalSeconds.ToString("f2"), speedReadAll.Elapsed.TotalSeconds.ToString("f2"));

            //byte[] byteImage = NIDPicture;
            //if (byteImage == null)
            //{
            //    lblStatus.Text = "Status: Read Photo error";
            //    MessageBox.Show("Read Photo error", Application.ProductName);
            //    to_notsuccess("", "", false);
            //}
            //else
            //{
            //    string fileName = @"Images/face-detect-set/fromReaderCard/" + m_txtID.Text + ".jpg";
            //    //m_picPhoto
            //    Image img = Image.FromStream(new MemoryStream(byteImage));
            //    Bitmap MyImage = new Bitmap(img, m_picPhoto.Width - 2, m_picPhoto.Height - 2);
            //    m_picPhoto.Image = (Image)MyImage;

            //    if (System.IO.File.Exists(fileName) == false)
            //    {
            //        m_picPhoto.Image.Save(fileName, ImageFormat.Jpeg);
            //    }

            //    lblStatus.Text = "Status: Ready";

            //    string param = "?id=" + m_txtID.Text.ToString()
            //                + "&com_name=" + ""
            //                + "&hn=" + ""
            //                + "&name=" + m_txtFullNameT.Text.ToString()
            //                + "&BirthDate=" + m_txtBrithDate.Text.ToString()
            //                + "&Sex=" + m_txtGender.Text.ToString()
            //                + "&Address=" + ""
            //                + "&Moo=" + ""
            //                + "&Soi=" + ""
            //                + "&Thanon=" + ""
            //                + "&Tumbol=" + ""
            //                + "&Amphur=" + ""
            //                + "&Province=" + ""
            //                + "&ExpireDate=" + ""
            //                + "&IssueDate=" + ""
            //                + "&p_name=" + ""
            //                + "&f_name=" + ""
            //                + "&l_name=" + ""
            //                + "&e_p_name=" + ""
            //                + "&e_f_name=" + ""
            //                + "&e_m_name=" + ""
            //                + "&e_l_name=" + ""
            //                + "&correlationId=" + ""
            //                + "&claimCode=" + ""
            //                + "&claimType=" + ""
            //                + "&createdDate=" + "";

            //    //to_complete(param);
            //    LaunchCamera("");
            //}

            //RDNIDLib.disconnectCardRD(obj);
            //RDNIDLib.deselectReaderRD(obj);

            //try
            //{
            //    this.Invoke(new MethodInvoker(delegate ()
            //    {
            //        object result = webBrowser.Document.InvokeScript("close_popup"); // second parameter is jsonString
            //    }));
            //}
            //catch { };

            return 0;
        }

        private void OnCardInserted(object sender, EventArgs e)
        {
            this.Enabled = false;
            Application.DoEvents();
            System.Threading.Thread.Sleep(200);
            reader = new IDReaderDotNetService("Identiv uTrust 2700 R Smart Card Reader 0", this);

            btnReadcard_Click(null, null);

            Application.DoEvents();
            this.Enabled = true;
        }

        public int selectReader(String reader)
        {
            byte[] _reader = String2Byte(reader);
            int res = RDNIDLib.selectReaderRD(_reader);
            return res;
        }

        private void ListCardReader()
        {
            //byte[] szReaders = new byte[1024 * 2];
            //int size = szReaders.Length;
            //int numreader = RDNIDLib.getReaderListRD(szReaders, size);
            //if (numreader <= 0)
            //    return;
            //String s = aByteToString(szReaders);
            //String[] readlist = s.Split(';');
            //if (readlist != null)
            //{
            //    for (int i = 0; i < readlist.Length; i++)
            //        m_ListReaderCard.Items.Add(readlist[i]);
            //    m_ListReaderCard.SelectedIndex = 0;
            //}
        }

        static string aByteToString(byte[] b)
        {
            Encoding ut = Encoding.GetEncoding(874); // 874 for Thai langauge
            int i;
            for (i = 0; b[i] != 0; i++) ;

            string s = ut.GetString(b);
            s = s.Substring(0, i);
            return s;
        }

        static byte[] String2Byte(string s)
        {
            // Create two different encodings.
            Encoding ascii = Encoding.GetEncoding(874);
            Encoding unicode = Encoding.Unicode;

            // Convert the string into a byte array.
            byte[] unicodeBytes = unicode.GetBytes(s);

            // Perform the conversion from one encoding to the other.
            byte[] asciiBytes = Encoding.Convert(unicode, ascii, unicodeBytes);

            return asciiBytes;
        }

        String _yyyymmdd_(String d)
        {
            string s = "";
            string _yyyy = d.Substring(0, 4);
            string _mm = d.Substring(4, 2);
            string _dd = d.Substring(6, 2);


            string[] mm = { "", "ม.ค.", "ก.พ.", "มี.ค.", "เม.ย.", "พ.ค.", "มิ.ย.", "ก.ค.", "ส.ค.", "ก.ย.", "ต.ค.", "พ.ย.", "ธ.ค." };
            string _tm = "-";
            if (_mm == "00")
            {
                _tm = "-";
            }
            else
            {
                _tm = mm[int.Parse(_mm)];
            }

            if (_yyyy == "0000")
                _yyyy = "-";

            if (_dd == "00")
                _dd = "-";

            s = _dd + " " + _tm + " " + _yyyy;
            return s;
        }

        enum NID_FIELD
        {
            NID_Number,   //1234567890123#

            TITLE_T,    //Thai title#
            NAME_T,     //Thai name#
            MIDNAME_T,  //Thai mid name#
            SURNAME_T,  //Thai surname#

            TITLE_E,    //Eng title#
            NAME_E,     //Eng name#
            MIDNAME_E,  //Eng mid name#
            SURNAME_E,  //Eng surname#

            HOME_NO,    //12/34#
            MOO,        //10#
            TROK,       //ตรอกxxx#
            SOI,        //ซอยxxx#
            ROAD,       //ถนนxxx#
            TUMBON,     //ตำบลxxx#
            AMPHOE,     //อำเภอxxx#
            PROVINCE,   //จังหวัดxxx#

            GENDER,     //1#			//1=male,2=female

            BIRTH_DATE, //25200131#	    //YYYYMMDD
            ISSUE_PLACE,//xxxxxxx#      //
            ISSUE_DATE, //25580131#     //YYYYMMDD
            EXPIRY_DATE,//25680130      //YYYYMMDD
            ISSUE_NUM,  //12345678901234 //14-Char
            END
        };

        private void btnEKCY_Click(object sender, EventArgs e)
        {
            StopTimer();
            LaunchCamera("");
        }

        public void LaunchCamera(string hn , string base64)
        {
            FormKYC formKYC = new FormKYC();

            APIHelper.StatusCodeSame = 0;

            if (string.IsNullOrEmpty(hn))
            {
                formKYC.m_txtID = m_txtID.Text.Trim();
                formKYC.m_type = "ID";
            }
            else
            {
                formKYC.m_txtID = hn;
                formKYC.m_type = "HN";

                string fileName = @"Images/face-detect-set/fromServices/" + hn + ".jpg";
                byte[] bytes1 = Convert.FromBase64String(base64);
                using (MemoryStream ms = new MemoryStream(bytes1))
                {
                    Image.FromStream(ms).Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                }

            }

            this.Hide(); // this will hide current form
            formKYC.Show(this);
        }

        public void LaunchCamera(string hn)
        {
            FormKYC formKYC = new FormKYC();

            APIHelper.StatusCodeSame = 0;

            if (string.IsNullOrEmpty(hn))
            {
                formKYC.m_txtID = m_txtID.Text.Trim();
                formKYC.m_type = "ID";
            }
            else
            {
                formKYC.m_txtID = hn;
                formKYC.m_type = "HN";
            }

            this.Hide(); // this will hide current form
            formKYC.Show(this);

            //FormKYC formKYC = new FormKYC();
            //formKYC.m_txtID = m_txtID.Text.Trim();
            //this.Hide();
            //formKYC.Show();
        }

        public Boolean blSensor_Tel_Flag = false;
        private void tm_sensor_Tick(object sender, EventArgs e)
        {
            try
            {
                if (blSensor_Tel_Flag == true)
                {
                    StopTimer();
                    StartTimer();
                    return;
                }
                //if (_appConfig.TestResponseCodeEDC.Equals("N"))
                PlayVDO();
            }
            catch { }
        }

        public void StartTimer()
        {
            tm_sensor.Interval = 30000;
            tm_sensor.Enabled = true;
            tm_sensor.Start();
        }

        public void StopTimer()
        {
            tm_sensor.Stop();
            tm_sensor.Enabled = false;
        }

        private void PlayVDO()
        {
            string strPlayList = "";
            strPlayList = @" <ASX  Version='3.0' > " +
                           " <title></title> " +
                           " <entry> " +
                           " <title></title> " +
                           " <author></author> " +
                           " <Ref href='" + Application.StartupPath + "\\Video\\VDOPlaylist.wmv' /> " +
                           " </entry>  ";

            strPlayList += " </ASX> ";

            string strPL = strPlayList;

            StreamWriter sw = new StreamWriter($"{Application.StartupPath}\\playlist.asx");
            sw.WriteLine(strPL.Replace("'", "\""));
            sw.Close();
            sw.Dispose();

            if (axWindowsMediaPlayer1.playState != WMPPlayState.wmppsPlaying)
            {
                axWindowsMediaPlayer1.Visible = true;
                axWindowsMediaPlayer1.BringToFront();
                axWindowsMediaPlayer1.URL = $"{Application.StartupPath}\\playlist.asx";
                axWindowsMediaPlayer1.uiMode = "none";
                axWindowsMediaPlayer1.Ctlcontrols.play();
                axWindowsMediaPlayer1.settings.setMode("loop", true);
                //axWindowsMediaPlayer1.Height = pbError.Height ;
                //axWindowsMediaPlayer1.Width = pbError.Width;
            }
        }

        private void axWindowsMediaPlayer1_ClickEvent(object sender, AxWMPLib._WMPOCXEvents_ClickEvent e)
        {
            //StopTimer();
            //StartTimer();

            axWindowsMediaPlayer1.Ctlcontrols.stop();
            axWindowsMediaPlayer1.Visible = false;
        }

        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            //if (e.newState == 8)
            //{

            //}
            //else if (e.newState == 3)
            //{
            //    axWindowsMediaPlayer1.Ctlcontrols.play();
            //}
        }

        public static async Task<string> RegisterUserAsync(string hn = "", string id = "")
        {
            var userInfor = await APIHelper.Post(APIHelper._GetUserInformation, new Models.RequestModel() { id = id });

            if (userInfor.StatusCode == 0)
            {
                var rightApproved = APIHelper.Post(APIHelper._GetRightApproved, new Models.RequestModel() { id = id }).Result;

                if (rightApproved.StatusCode == 0 && rightApproved.RightApproveModel.RightStatus == "Y")
                {

                }
                else
                {
                    var right = APIHelper.Post(APIHelper._GetRight, new Models.RequestModel() { id = id }).Result;

                    if (right.StatusCode == 0)
                    {

                    }
                }
            }

            return "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LaunchCamera("57000012");
        }
    }
}
