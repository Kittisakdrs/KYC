using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using Emgu.CV;
using Emgu.CV.Structure;
using KioskQexe.IDReaderDotNet;
using Newtonsoft.Json;
using RDNIDWRAPPER;
using LoxleyOrbit.FaceScan.Models;
using System.Security.Permissions;

namespace LoxleyOrbit.FaceScan
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class Form1 : Form
    {
        string StartupPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        public static string GetCurrentExecutingDirectory(System.Reflection.Assembly assembly)
        {
            string filePath = new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            return Path.GetDirectoryName(filePath);
        }
        private IDReaderDotNetService reader = null;

        public void LaunchCamera()
        {
            FormKYC formKYC = new FormKYC();
            formKYC.m_txtID = "";
            formKYC.Show();
        }
        public Form1()
        {
            InitializeComponent();
            InitializeCamera();

            string fileName = StartupPath + "\\RDNIDLib.DLD";

            if (System.IO.File.Exists(fileName) == false)
            {
                MessageBox.Show("RDNIDLib.DLD not found");
            }

            System.Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

            this.Text = String.Format("R&D NID Card Plus C# V{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);

            IntPtr mNIDLibHandle = (IntPtr)0;
            byte[] _lic = String2Byte(fileName);

            int nres = 0;
            nres = RDNIDLib.openNIDLibRD(_lic);                     //for openLib  

            long newlong = (long)mNIDLibHandle;

            if (nres != 0)
            {
                String m;
                m = String.Format("Error: {0} ", nres);
                MessageBox.Show(m);
            }

            byte[] Licinfo = new byte[1024];

            RDNIDLib.getLicenseInfoRD(Licinfo);

            //m_lblDLDInfo.Text = "License Info: " + aByteToString(Licinfo);

            byte[] Softinfo = new byte[1024];
            RDNIDLib.getSoftwareInfoRD(Softinfo);
            //m_lblSoftwareInfo.Text = "RDNIDLib Info: " + aByteToString(Softinfo);

            byte[] ZDLibinfo = new byte[1024];

            ListCardReader();

            //lblStatus.Text = "Status: Ready";

            reader = new IDReaderDotNetService("Identiv uTrust 2700 R Smart Card Reader 0", this); //ตัวอ่านบัตรตัวใหม่
            reader.OnCardInserted += new EventHandler(OnCardInserted);

            m_txtID.Text = "1570800044928";
        }

        private void m_ListReaderCard_SelectedIndexChanged(object sender, EventArgs e)
        {
            //reader = new IDReaderDotNetService("Identiv uTrust 2700 R Smart Card Reader 0", this); //ตัวอ่านบัตรตัวใหม่
            //reader.OnCardInserted += new EventHandler(OnCardInserted);
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

        private void btnReadcard_Click(object sender, EventArgs e)
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
            m_txtIssueplace.Text = "";
            m_picPhoto.Image = null;
            lblStatus.Text = "Status: Reading...";

            ReadCard();
        }

        private void btngetReaderID_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "Status: Reading...";
            lbl_Time.Text = "";
            m_txtReaderID.ResetText();
            this.Refresh();
            String strTerminal = m_ListReaderCard.GetItemText(m_ListReaderCard.SelectedItem);

            int obj = selectReader(strTerminal);
            if (obj < 0)
            {
                String m;
                m = String.Format("Error: {0} ", obj);
                lblStatus.Text = "Status: Error " + obj;
                MessageBox.Show(m, Application.ProductName);

                RDNIDLib.disconnectCardRD(obj);
                RDNIDLib.deselectReaderRD(obj);
                return;
            }
            byte[] Rid = new byte[16];
            int res = RDNIDLib.getRidDD(obj, Rid);
            if (res > 0)
            {
                m_txtReaderID.Text = BitConverter.ToString(Rid).Replace("-", " ");
                lblStatus.Text = "Status: Ready";
            }
            else
            {
                m_txtReaderID.Text = String.Format("{0}", res);
                lblStatus.Text = String.Format("Status: Error {0}", res);

                MessageBox.Show(String.Format("Error: {0}", res), Application.ProductName);
            }

            //m_txtGetRidRY.Text = mRDNIDWRAPPER.getRidDD();

            RDNIDLib.deselectReaderRD(obj);
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

        public int selectReader(String reader)
        {
            byte[] _reader = String2Byte(reader);
            int res = RDNIDLib.selectReaderRD(_reader);
            return res;
        }

        private void ListCardReader()
        {
            byte[] szReaders = new byte[1024 * 2];
            int size = szReaders.Length;
            int numreader = RDNIDLib.getReaderListRD(szReaders, size);
            if (numreader <= 0)
                return;
            String s = aByteToString(szReaders);
            String[] readlist = s.Split(';');
            if (readlist != null)
            {
                for (int i = 0; i < readlist.Length; i++)
                    m_ListReaderCard.Items.Add(readlist[i]);
                m_ListReaderCard.SelectedIndex = 0;
            }
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

        Stopwatch speedReadText;
        Stopwatch speedReadAll;

        protected int ReadCard()
        {
            speedReadAll = new Stopwatch();
            speedReadText = new Stopwatch();

            speedReadAll.Start();
            speedReadText.Start();

            String strTerminal = m_ListReaderCard.GetItemText(m_ListReaderCard.SelectedItem);
            int obj = selectReader(strTerminal);
            if (obj < 0)
            {
                String m;
                m = String.Format("Error: {0} ", obj);

                lblStatus.Text = "Status: Error " + obj;
                MessageBox.Show(m, Application.ProductName);

                RDNIDLib.disconnectCardRD(obj);
                RDNIDLib.deselectReaderRD(obj);
                return obj;
            }

            Int32 nInsertCard = 0;
            nInsertCard = RDNIDLib.connectCardRD(obj);
            if (nInsertCard != DefineConstants.NID_SUCCESS)
            {
                String m;
                m = String.Format("Error: {0} ", nInsertCard);

                lblStatus.Text = "Status: Error " + nInsertCard;
                MessageBox.Show(m, Application.ProductName);

                RDNIDLib.disconnectCardRD(obj);
                RDNIDLib.deselectReaderRD(obj);
                return nInsertCard;
            }

            byte[] data = new byte[1024];
            int res = RDNIDLib.getNIDTextRD(obj, data, data.Length);
            if (res != DefineConstants.NID_SUCCESS)
            {
                String m;
                m = String.Format("Error: {0} ", res);

                lblStatus.Text = "Status: Error " + res;
                MessageBox.Show(m, Application.ProductName);
                speedReadText.Stop();
                speedReadAll.Stop();
                RDNIDLib.disconnectCardRD(obj);
                RDNIDLib.deselectReaderRD(obj);
                return res;
            }

            speedReadText.Stop();

            String NIDData = aByteToString(data);
            if (NIDData == "")
            {
                lblStatus.Text = "Status: Read Text error";
                MessageBox.Show("Read Text error");
            }
            else
            {
                string[] fields = NIDData.Split('#');

                m_txtID.Text = fields[(int)NID_FIELD.NID_Number];                             // or use m_txtID.Text = fields[(int)NID_FIELD.NID_Number];

                String fullname = fields[(int)NID_FIELD.TITLE_T] + " " +
                                    fields[(int)NID_FIELD.NAME_T] + " " +
                                    fields[(int)NID_FIELD.MIDNAME_T] + " " +
                                    fields[(int)NID_FIELD.SURNAME_T].TrimEnd();  //Remove space
                m_txtFullNameT.Text = fullname;

                fullname = fields[(int)NID_FIELD.TITLE_E] + " " +
                                    fields[(int)NID_FIELD.NAME_E] + " " +
                                    fields[(int)NID_FIELD.MIDNAME_E] + " " +
                                    fields[(int)NID_FIELD.SURNAME_E].TrimEnd();  //Remove space
                ;
                m_txtFullNameE.Text = fullname;

                m_txtBrithDate.Text = _yyyymmdd_(fields[(int)NID_FIELD.BIRTH_DATE]);


                string txtAddrees = "";

                if (fields[(int)NID_FIELD.HOME_NO] != "") txtAddrees += fields[(int)NID_FIELD.HOME_NO] + "   ";
                if (fields[(int)NID_FIELD.MOO] != "") txtAddrees += fields[(int)NID_FIELD.MOO] + "   ";
                if (fields[(int)NID_FIELD.TROK] != "") txtAddrees += fields[(int)NID_FIELD.TROK] + "   ";
                if (fields[(int)NID_FIELD.SOI] != "") txtAddrees += fields[(int)NID_FIELD.SOI] + "   ";
                if (fields[(int)NID_FIELD.ROAD] != "") txtAddrees += fields[(int)NID_FIELD.ROAD] + "   ";
                if (fields[(int)NID_FIELD.TUMBON] != "") txtAddrees += fields[(int)NID_FIELD.TUMBON] + "   ";
                if (fields[(int)NID_FIELD.AMPHOE] != "") txtAddrees += fields[(int)NID_FIELD.AMPHOE] + "   ";
                if (fields[(int)NID_FIELD.PROVINCE] != "") txtAddrees += fields[(int)NID_FIELD.PROVINCE].TrimEnd();

                m_txtAddress.Text = txtAddrees;

                if (fields[(int)NID_FIELD.GENDER] == "1")
                {
                    m_txtGender.Text = "ชาย";
                }
                else if (fields[(int)NID_FIELD.GENDER] == "2")
                {
                    m_txtGender.Text = "หญิง";
                }

                m_txtIssueDate.Text = _yyyymmdd_(fields[(int)NID_FIELD.ISSUE_DATE]);
                m_txtExpiryDate.Text = _yyyymmdd_(fields[(int)NID_FIELD.EXPIRY_DATE]);
                if ("99999999" == m_txtExpiryDate.Text)
                    m_txtExpiryDate.Text = "99999999 ตลอดชีพ";
                m_txtIssueNum.Text = fields[(int)NID_FIELD.ISSUE_NUM];
                m_txtIssueplace.Text = fields[(int)NID_FIELD.ISSUE_PLACE].TrimEnd();  //Remove space                
            }

            byte[] NIDPicture = new byte[1024 * 5];
            int imgsize = NIDPicture.Length;
            res = RDNIDLib.getNIDPhotoRD(obj, NIDPicture, out imgsize);
            if (res != DefineConstants.NID_SUCCESS)
                if (res != DefineConstants.NID_SUCCESS)
                {
                    String m;
                    m = String.Format("Error: {0} ", res);

                    lblStatus.Text = "Status: Error " + res;
                    MessageBox.Show(m, Application.ProductName);
                    speedReadAll.Stop();
                    RDNIDLib.disconnectCardRD(obj);
                    RDNIDLib.deselectReaderRD(obj);
                    return res;
                }
            speedReadAll.Stop();



            lbl_Time.Text = string.Format("Read Text: {0} s, Text + Photo: {1} s", speedReadText.Elapsed.TotalSeconds.ToString("f2"), speedReadAll.Elapsed.TotalSeconds.ToString("f2"));



            byte[] byteImage = NIDPicture;
            if (byteImage == null)
            {
                lblStatus.Text = "Status: Read Photo error";
                MessageBox.Show("Read Photo error", Application.ProductName);
            }
            else
            {
                string fileName = @"Images/face-detect-set/fromReaderCard/" + m_txtID.Text + ".jpg";
                //m_picPhoto
                Image img = Image.FromStream(new MemoryStream(byteImage));
                Bitmap MyImage = new Bitmap(img, m_picPhoto.Width - 2, m_picPhoto.Height - 2);
                m_picPhoto.Image = (Image)MyImage;

                if (System.IO.File.Exists(fileName) == false)
                {
                    m_picPhoto.Image.Save(fileName, ImageFormat.Jpeg);
                }

                lblStatus.Text = "Status: Ready";
            }

            RDNIDLib.disconnectCardRD(obj);
            RDNIDLib.deselectReaderRD(obj);
            return 0;
        }

        FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        VideoCaptureDevice videoSource = null;
        public int islemdurumu = 0; //CAMERA STATUS
        public int selected = 0;
        public static int durdur = 0;
        public static int gondermesayisi = 0;
        public int kamerabaslat = 0;

        private void START_Click(object sender, EventArgs e)
        {
            selected = comboBox1.SelectedIndex;

            if (islemdurumu == 0)
            {
                if (kamerabaslat > 0)
                    return;

                try
                {
                    videoSource = new VideoCaptureDevice(videoDevices[selected].MonikerString);
                    SetResolution(videoSource);
                    videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
                    chk = true;
                    videoSource.Start();
                    kamerabaslat = 1; //CAMERA STARTRED
                }
                catch
                {
                    MessageBox.Show("RESTART THE PROGRAM");

                    if (!(videoSource == null))
                        if (videoSource.IsRunning)
                        {
                            chk = false;
                            videoSource.SignalToStop();
                            videoSource = null;
                        }
                }
            }
        }

        private void InitializeCamera()
        {
            try
            {
                this.label15.Text = "";
                //Enumerate all video input devices
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (videoDevices.Count == 0)
                {
                    label15.Text = "No local capture devices";
                }
                foreach (FilterInfo device in videoDevices)
                {
                    int i = 1;
                    comboBox1.Items.Add(device.Name);
                    label15.Text = ("camera" + i + "initialization completed..." + "\n");
                    i++;
                }
                comboBox1.SelectedIndex = 0;
            }
            catch (ApplicationException)
            {
                this.label15.Text = "No local capture devices";
                videoDevices = null;
            }
        }

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            //Bitmap img = (Bitmap)eventArgs.Frame.Clone();
            //imgVideo.Image = img;

            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            Image overlayImage = Image.FromFile(@"Images\overlay.png");
            OverlayImage(bitmap, overlayImage, bitmap);
            imgVideo.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void SetResolution(VideoCaptureDevice videoSource)
        {
            VideoCapabilities[] videoCapabilities = videoSource.VideoCapabilities;

            // Select the desired resolution from the list of available resolutions
            int desiredWidth = 640;
            int desiredHeight = 480;

            foreach (VideoCapabilities capabilty in videoCapabilities)
            {
                if (capabilty.FrameSize.Width == desiredWidth && capabilty.FrameSize.Height == desiredHeight)
                {
                    videoSource.VideoResolution = capabilty;
                    break;
                }
            }
        }

        bool chk = false;
        private void OverlayImage(Image baseImage, Image overlayImage, Bitmap bitmap)
        {
            int x1 = (hid_canv.Width / 2) - 110;//กรอบใน
            int x2 = (hid_canv.Width / 2) - 170;
            int x3 = (hid_canv.Width / 2) - 190;//กรอบนอก

            int y1 = (hid_canv.Height / 2) - 90;//กรอบใน
            int y2 = (hid_canv.Height / 2) - 150;
            int y3 = (hid_canv.Height / 2) - 170;//กรอบนอก

            using (Graphics g = Graphics.FromImage(baseImage))
            {
                g.DrawImage(overlayImage, new Rectangle(new Point(0, 0), new Size(baseImage.Width, baseImage.Height)));
                g.DrawRectangle(new Pen(Color.Red, 3), new Rectangle(new Point(x1, y1), new Size(260, 270))); //กรอบใน
                g.DrawRectangle(new Pen(Color.Green, 3), new Rectangle(new Point(x2, y2), new Size(390, 410)));
                g.DrawRectangle(new Pen(Color.Blue, 3), new Rectangle(new Point(x3, y3), new Size(430, 450)));//กรอบนอก
            }

            //Image<Bgr, byte> grayImage = new Image<Bgr, byte>(bitmap);
            //Rectangle[] rectangles = cascadeClassifier.DetectMultiScale(grayImage, 1.2, 1);

            //foreach (Rectangle rectangle in rectangles)
            //{
            //    using (Graphics graphics = Graphics.FromImage(bitmap))
            //    {
            //        using (Pen pen = new Pen(Color.Yellow, 3))
            //        {
            //            graphics.DrawRectangle(pen, rectangle);

            //            if((rectangle.Location.X >= x3 && rectangle.Location.X <= x1) && (rectangle.Location.Y >= y3 && rectangle.Location.Y <= y1)
            //                && rectangle.Size.Width >= 0 && rectangle.Size.Height >= 0)
            //                if (chk)
            //                    CAPTURE_Click(null, null);
            //        }
            //    }
            //}

            hid_canv.Image = baseImage;
        }

        private void CAPTURE_Click(object sender, EventArgs e)
        {
            chk = false;
            string fileName = @"Images/face-detect-set/fromCamera/" + m_txtID.Text + ".jpg";
            imgVideo.Image.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            try
            {
                videoSource.SignalToStop();
                videoSource = null;
            }
            catch { }
        }

        private async void FACEDETECTION_Click(object sender, EventArgs e)
        {
            bool testWayOne = true;
            if (testWayOne)
            {
                string fileName1 = @"Images/face-detect-set/fromReaderCard/" + m_txtID.Text.Trim() + ".jpg";
                string fileName2 = @"Images/face-detect-set/fromCamera/" + m_txtID.Text.Trim() + ".jpg";

                FaceDetectorFromBase64 req = new FaceDetectorFromBase64();

                req.PersonalId = m_txtID.Text.ToString();
                req.RequestFaceDetector1.Base64 = ToBase64String(fileName1);
                req.RequestFaceDetector1.Blur_check = false;
                req.RequestFaceDetector1.Pad_check = false;
                req.RequestFaceDetector2.Base64 = ToBase64String(fileName2);
                req.RequestFaceDetector2.Blur_check = true;
                req.RequestFaceDetector2.Pad_check = true;

                var face = await APIHelper.FaceDetectorFromBase64(req);

                textBox1.Text = JsonConvert.SerializeObject(face);
            }
            else
            {
                RequestFaceDetectorFromPath req = new RequestFaceDetectorFromPath();

                req.RequestFaceDetector1.Path = @"Images/face-detect-set/face/8.jpg";
                req.RequestFaceDetector1.Blur_check = false;
                req.RequestFaceDetector1.Pad_check = false;

                req.RequestFaceDetector2.Path = @"Images/face-detect-set/face/9.jpg";
                req.RequestFaceDetector2.Blur_check = true;
                req.RequestFaceDetector2.Pad_check = true;

                var face = await APIHelper.FaceDetectorFromPath(req);

                textBox1.Text = JsonConvert.SerializeObject(face);
            }
        }

        private string ToBase64String(string Path)
        {
            using (Image image = Image.FromFile(Path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();

                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }

        static readonly CascadeClassifier cascadeClassifier = new CascadeClassifier("haarcascade_frontalface_alt_tree.xml");

        private void btnEKCY_Click(object sender, EventArgs e)
        {

        }
    }
}
