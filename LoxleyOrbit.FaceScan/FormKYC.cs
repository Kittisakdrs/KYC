using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;

using Newtonsoft.Json;
using LoxleyOrbit.FaceScan.Models;
using System.Security.Permissions;
using System.Drawing.Imaging;
using System.Threading;
using Emgu.CV.Dnn;
using CSCore.CoreAudioAPI;
using System.Media;

namespace LoxleyOrbit.FaceScan
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class FormKYC : Form
    {
        private readonly string msg_near = "กรุณาขยับเข้าหากล้องเพื่อสแกนใบหน้า";
        private readonly string msg_far = "กรุณาถอยห่างจากกล้องเพื่อสแกนใบหน้า";
        private readonly string msg_adjust = "กรุณายืนห่างประมาณ 40-60 เซนติเมตร";
        private readonly string msg_fit = "กรุณาอย่าขยับเพื่อสแกนใบหน้า";
        private readonly string msg_center = "กรุณาอยู่ตรงกลาง";
        private readonly string mgs_remark = "(วางใบหน้าบนกรอบที่กำหนด ไม่ปิดคิ้วตาจมูก ปากและคาง)";

        FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        VideoCaptureDevice videoSource = null;
        public int islemdurumu = 0; //CAMERA STATUS
        public int selected = 0;
        public static int durdur = 0;
        public static int gondermesayisi = 0;
        public int kamerabaslat = 0;
        private bool chkCamera = true;

        public string m_txtID = "1570800044928";
        public string m_type = "ID";

        static readonly CascadeClassifier cascadeClassifier = new CascadeClassifier("haarcascade_frontalface_alt_tree.xml");
        Image overlayImage = Image.FromFile(@"Images\Head&Shoulder_Black.png");

        int desiredWidth = 1920;
        int desiredHeight = 1080;

        public FaceResult modal = new FaceResult();

        #region comment
        //int x1 = (desiredWidth / 2) - 150;//กรอบใน
        //int x3 = (desiredWidth / 2) - 190;//กรอบนอก

        //int y1 = (desiredHeight / 2) - 150;//กรอบใน
        //int y3 = (desiredHeight / 2) - 210;//กรอบนอก
        #endregion

        int x1 = 0;//กรอบใน
        int x3 = 0;//กรอบนอก

        int y1 = 0;//กรอบใน
        int y3 = 0;//กรอบนอก

        int sizeX1 = 210;//กรอบนอก
        int sizeX3 = 400;//กรอบนอก

        #region Audio
        static string audio1 = @"Audio\Audio1.wav";
        static string audio2 = @"Audio\Audio2.wav";
        static string audio3 = @"Audio\Audio3.wav";
        static string audio4 = @"Audio\Audio4.wav";
        private DateTime dateTimeStart = DateTime.Now;
        private double totalMilliseconds = 0;
        private int lasttimeOut = 0;
        private bool isAudioPlaying = true;
        private SoundPlayer player;
        #endregion

        //// Calculate the width and height of the cropped area based on the TrackBar value and the aspect ratio of 16:9
        //int cropWidth = 1366;
        //int cropHeight = 768;

        //// Center the cropped area on the original image by adjusting the X and Y coordinates
        //int cropX = 0;
        //int cropY = 0;

        #region 1234
        public FormKYC()
        {
            InitializeComponent();
            InitializeCamera();
            InitializeObject();
            InitializeSound();
        }

        private void InitializeObject()
        {
            int x = this.Width - Cam_pic.Width;
            int y = this.Height - Cam_pic.Height;
            Cam_pic.Location = new Point() { X = (x / 2), Y = (y / 2) };
            //pictureBox2.Location = new Point() { X = (x / 2), Y = (y / 2) };
            //pic_arrow_left.Location = new Point() { X = 0, Y = (pictureBox1.Height / 2) };
            //pic_arrow_rigth.Location = new Point() { X = (x - pic_arrow_rigth.Width), Y = (pictureBox1.Height / 2) };


            loading_box.Location = new Point() { X = (Cam_pic.Width / 2) - (loading_box.Width / 2), Y = (Cam_pic.Height / 2) - (loading_box.Height / 2) };
            SetPictureBox3(false);


        }
        private void InitializeCamera()
        {
            try
            {
                this.label1.Text = "";
                //Enumerate all video input devices
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (videoDevices.Count == 0)
                {
                    label1.Text = "No local capture devices";
                }
                foreach (FilterInfo device in videoDevices)
                {
                    int i = 1;
                    comboBox1.Items.Add(device.Name);
                    label1.Text = ("camera" + i + "initialization completed..." + "\n");
                    i++;
                }
                comboBox1.SelectedIndex = 0;
                lb_txt.Text = "";
                //lalala.Text = "cropX=" + cropX.ToString() + ", cropY=" + cropY.ToString() + ", cropWidth=" + cropWidth.ToString() + ", cropHeight=" + cropHeight.ToString();

                if ("Y" != "Y")
                {
                    //ControlBox = true;
                    //MinimizeBox = true;
                    //MaximizeBox = true;
                    //FormBorderStyle = FormBorderStyle.Sizable;
                    //WindowState = FormWindowState.Minimized;
                }
                else
                {
                    ControlBox = false;
                    MinimizeBox = false;
                    MaximizeBox = false;
                    //FormBorderStyle = FormBorderStyle.None;
                    //WindowState = FormWindowState.Maximized;
                }
            }
            catch (ApplicationException)
            {
                this.label1.Text = "No local capture devices";
                videoDevices = null;
            }
        }

        private void InitializeSound()
        {
            // Create an instance of the SoundPlayer class.
            player = new SoundPlayer();

            // Listen for the LoadCompleted event.
            player.LoadCompleted += new AsyncCompletedEventHandler(player_LoadCompleted);

            // Listen for the SoundLocationChanged event.
            player.SoundLocationChanged += new EventHandler(player_LocationChanged);
        }

        private void FormKYC_Load(object sender, EventArgs e)
        {
            selected = comboBox1.SelectedIndex;
            START_Click(null, null);

            int x = this.Width;
            int y = this.Height;
            radioButton4.Checked = true;
            Cam_pic.Width = this.Width;
            Cam_pic.Height = this.Height;
            //pictureBox2.Width = this.Width;
            //pictureBox2.Height = this.Height;
            Overlay_box.Width = this.Width;
            Overlay_box.Height = this.Height;

            Cam_pic.Location = new Point(0, 0);
            //pictureBox2.Location = new Point(0, 0);

            Cam_pic.Controls.Add(Overlay_box);
            Overlay_box.Location = new Point(0, 0);
            Overlay_box.BackColor = Color.Transparent;
            Overlay_box.Image = overlayImage;

            Overlay_box.Controls.Add(loading_box);
            int lx = (x / 2) - (loading_box.Width / 2);
            int ly = (y / 2) - (loading_box.Height / 2);
            loading_box.Location = new Point(lx, ly);
            loading_box.BackColor = Color.Transparent;

            lb_remark.Visible = false;
            lb_remark.Location = new Point() { X = (this.Width / 2) - (lb_remark.Width / 2), Y = y - 100 };
            lb_remark.Text = mgs_remark;


            #region result_panel
            Label Lhead = result_panel.Controls["Head"] as Label;
            Label Ldesc = result_panel.Controls["desc"] as Label;
            PictureBox Pbox = result_panel.Controls["pictureBox"] as PictureBox;
            Button B2 = result_panel.Controls["button2"] as Button;
            Button B3 = result_panel.Controls["button3"] as Button;

            result_panel.Visible = false;
            #endregion


            #region comment
            //webBrowser1.Navigate(new Uri("https://test-kiosk.chulacareapp.com/onemlweb/FaceScan.aspx"));

            //ChromiumWebBrowser chrome;

            //CefSettings settings = new CefSettings();

            //settings.CefCommandLineArgs.Add("enable-media-stream", "1");

            //Cef.Initialize(settings);
            //chrome = new ChromiumWebBrowser("https://test-kiosk.chulacareapp.com/onemlweb/FaceScan.aspx");
            //this.pContainer.Controls.Add(chrome);
            //chrome.Dock = DockStyle.Fill;
            //chrome.AddressChanged += Chrome_AddressChanged;
            #endregion
        }

        #region Audio Control
        private void PlayAudio(string audio, int timeOut)
        {
            //SoundPlayer soundPlayer = new SoundPlayer(audio);
            player.SoundLocation = audio;

            try
            {
                TimeSpan timedddd = DateTime.Now - dateTimeStart;
                totalMilliseconds = timedddd.TotalMilliseconds;
                //txt_log.Items.Add(audio + " = " + isAudioPlaying + " == " + totalMilliseconds + " >= " + lasttimeOut);

                if (isAudioPlaying || totalMilliseconds >= lasttimeOut)
                {
                    lasttimeOut = timeOut;
                    totalMilliseconds = 0;
                    dateTimeStart = DateTime.Now;
                    isAudioPlaying = false;
                    player.Play();
                    TimerClearAudio.Enabled = true;
                    TimerClearAudio.Interval = timeOut;
                    TimerClearAudio.Start();
                }
            }
            catch (Exception ex)
            {
                isAudioPlaying = true;
                TimerClearAudio.Stop();
                player.Stop();
            }
        }

        //public TimeSpan GetSoundDuration(string filePath)
        //{
        //    using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        //    using (var soundPlayer = new SoundPlayer(fileStream))
        //    {
        //        soundPlayer.Load();
        //        return soundPlayer.SoundDuration;
        //    }
        //}

        private void TimerClearAudio_Tick(object sender, EventArgs e)
        {
            //txt_log.Items.Add("TimerClearAudio_Tick");
            isAudioPlaying = true;
            TimerClearAudio.Stop();
            TimerClearAudio.Enabled = false;
        }

        // Gets the default device for the system
        public static MMDevice GetDefaultRenderDevice()
        {
            using (var enumerator = new MMDeviceEnumerator())
            {
                return enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
            }
        }

        // Checks if audio is playing on a certain device
        public static bool IsAudioPlaying(MMDevice device)
        {
            using (var meter = AudioMeterInformation.FromDevice(device))
            {
                return meter.PeakValue > 0;
            }
        }
        #endregion

        #region comment
        //private void Chrome_AddressChanged(object sender, AddressChangedEventArgs e)
        //{
        //    //this.Invoke(new MethodInvoker(() =>
        //    //{
        //    //    //txtUrl.Text = e.Address;
        //    //}));
        //}
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            //FaceDetection();
            //CAPTURE_Click(null, null);
            //ClearVideo();
            CloseFrom("ต้องการทำการ eKYC ใหม่หรือไม่?", "แจ้งเตือน");
        }

        private void RESET_CAMERA()
        {
            if (!(videoSource == null))
                if (videoSource.IsRunning)
                {
                    videoSource.SignalToStop();
                    videoSource = null;
                }

            kamerabaslat = 1;
            Cam_pic.Image = null;
            //pictureBox2.Image = null;
        }

        private void START_Click(object sender, EventArgs e)
        {
            if (islemdurumu == 0)
            {
                //if (kamerabaslat > 0)
                //    return;

                try
                {
                    videoSource = new VideoCaptureDevice(videoDevices[selected].MonikerString);

                    SetResolution(videoSource);

                    videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
                    Setlb_remark(mgs_remark);
                    SetOverlayBox(true);
                    videoSource.Start();
                    chkCamera = true;
                    isAudioPlaying = true;
                    PlayAudio(audio1, 6500);
                    kamerabaslat = 1; //CAMERA STARTRED

                }
                catch (Exception ex)
                {
                    MessageBox.Show("RESTART THE PROGRAM : " + ex.Message);
                    if (!(videoSource == null))
                        if (videoSource.IsRunning)
                        {
                            SetVideoSource(null);
                            videoSource.Stop();
                        }
                }
            }
        }

        private void CAPTURE_Click(object sender, EventArgs e)
        {
            string fileName = @"Images/face-detect-set/fromCamera/" + m_txtID + ".jpg";
            //Cam_pic.Image.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);

            //try
            //{
            //    if (!(videoSource == null))
            //        if (videoSource.IsRunning)
            //        {
            //            SetVideoSource(null);

            //            videoSource = new VideoCaptureDevice(videoDevices[selected].MonikerString);
            //            SetResolution(videoSource);
            //            videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
            //            videoSource.Start();
            //        }
            //}

            //catch { }
        }

        private void FACEDETECTION_Click(object sender, EventArgs e)
        {
            //FaceDetection();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer1.Stop();
        }

        private void FormKYC_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Owner != null)
            {
                this.Owner.Show();
                this.Owner.Focus();
            }
        }

        private void FaceDetection()
        {
            isAudioPlaying = true;
            SetPictureBox3(true);
            SetOverlayBox(true);

            PlayAudio(audio3, 3900);
            CAPTURE_Click(null, null);
            //Task.Delay(2000).Wait();
            //RESET_CAMERA();
            Setlb_txt("");
            Setlb_remark("");

            int Same = 0;
            bool testWayOne = true;
            string fileName1 = "";

            if (testWayOne)
            {
                if (m_type == "ID")
                    fileName1 = @"Images/face-detect-set/fromReaderCard/" + m_txtID.Trim() + ".jpg";
                else
                    fileName1 = @"Images/face-detect-set/fromServices/" + m_txtID.Trim() + ".jpg";

                string fileName2 = @"Images/face-detect-set/fromCamera/" + m_txtID.Trim() + ".jpg";

                FaceDetectorFromBase64 req = new FaceDetectorFromBase64();

                req.PersonalId = m_txtID.ToString();
                req.RequestFaceDetector1.Base64 = ToBase64String(fileName1);
                req.RequestFaceDetector1.Pad_check = false;
                req.RequestFaceDetector1.Blur_check = false;

                req.RequestFaceDetector2.Base64 = ToBase64String(fileName2);
                req.RequestFaceDetector2.Pad_check = true;
                req.RequestFaceDetector2.Blur_check = true;

                var face = APIHelper.FaceDetectorFromBase64(req).Result;

                Same = face.Same;
                //textBox1.Text = JsonConvert.SerializeObject(face);
                //MessageBox.Show((face.Same == 1 ? "MATCH" : "Not MATCH"), "แจ้งเตือน");
                //CloseFrom("ต้องการทำการ eKYC ใหม่หรือไม่?", "แจ้งเตือน");
            }
            else
            {
                RequestFaceDetectorFromPath req = new RequestFaceDetectorFromPath();

                req.RequestFaceDetector1.Path = @"Images/face-detect-set/face/8.jpg";
                req.RequestFaceDetector1.Pad_check = false;
                req.RequestFaceDetector1.Blur_check = false;

                req.RequestFaceDetector2.Path = @"Images/face-detect-set/face/9.jpg";
                req.RequestFaceDetector2.Pad_check = true;
                req.RequestFaceDetector2.Blur_check = true;

                var face = APIHelper.FaceDetectorFromPath(req).Result;

                Same = face.Same;
                //MessageBox.Show((face.Same == 1 ? "MATCH" : "Not MATCH"), "แจ้งเตือน");
                //CloseFrom("ต้องการทำการ eKYC ใหม่หรือไม่?", "แจ้งเตือน");

                //textBox1.Text = JsonConvert.SerializeObject(face);
            }

            APIHelper.StatusCodeSame = Same;

            if (Same == 1)
            {

                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show("MATCH", "แจ้งเตือน", buttons);
                if (result == DialogResult.OK)
                {
                    SetPictureBox3(false);
                    //SetPictureBox4(false);
                    this.Close();
                }
            }
            else
            {
                SetResultBox(true);
                SetPictureBox3(false);
                ////SetPictureBox4(false);
                CloseFrom("ต้องการทำการ eKYC ใหม่หรือไม่?", "แจ้งเตือน");
            }
        }

        private void CloseFrom(string message, string title)
        {
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);

            if (result == DialogResult.Yes)
            {
                if (!(videoSource == null))
                    if (videoSource.IsRunning)
                    {
                        videoSource.Stop();
                        SetVideoSource(null);
                    }

                RESET_CAMERA();
                kamerabaslat = 0;
                Setlb_txt("");
                Setlb_remark("");

                //Task.Delay(2000).Wait();

                try
                {
                    START_Click(null, null);
                }
                catch { }
            }
            else
            {
                RESET_CAMERA();
                this.Close();
            }
        }

        //private object lockObject = new object();

        //algo1backgroundworker_DoWork()
        //{
        //    Image imgclone;
        //    lock (lockObject)
        //    {
        //        Image img = this.picturebox.Image;
        //        imgclone = img.clone();
        //    }

        //    //operate on imgclone and output it
        //}

        private string ToBase64String(string Path)
        {
            using (Image image = Image.FromFile(Path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }
        #endregion

        double focusCenterX = 0.5; // 50%
        double focusCenterY = 0.5; // 50%
        double scaleFactor = 1.75;
        double aspectRatio = 16.0 / 9.0; // 16:9 aspect ratio

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (radioButton1.Checked)
            {
                scaleFactor = 1;
            }
            else if (radioButton2.Checked)
            {
                scaleFactor = 1.25;
            }
            else if (radioButton3.Checked)
            {
                scaleFactor = 1.5;
            }
            else if (radioButton4.Checked)
            {
                scaleFactor = 1.75;
            }
            else if (radioButton5.Checked)
            {
                scaleFactor = 2;
            }

            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();

            int centerX = (int)(bitmap.Width * focusCenterX);
            int centerY = (int)(bitmap.Height * focusCenterY);

            // Calculate the crop area around the focus center point
            int cropWidth = (int)(bitmap.Height * aspectRatio * (1.0 / scaleFactor));
            int cropHeight = (int)(bitmap.Height * (1.0 / scaleFactor));
            int cropX = centerX - cropWidth / 2;
            int cropY = centerY - cropHeight / 2;
            cropX = Math.Max(cropX, 0);
            cropY = Math.Max(cropY, 0);
            cropWidth = Math.Min(cropWidth, bitmap.Width - cropX);
            cropHeight = Math.Min(cropHeight, bitmap.Height - cropY);
            Rectangle cropArea = new Rectangle(cropX, cropY, cropWidth, cropHeight);

            // Apply the crop filter to the video frame
            Bitmap croppedFrame = new AForge.Imaging.Filters.Crop(cropArea).Apply(bitmap);

            // Resize the cropped image if necessary
            if (scaleFactor > 1.0)
            {
                int newWidth = (int)(croppedFrame.Width * scaleFactor);
                int newHeight = (int)(croppedFrame.Height * scaleFactor);

                AForge.Imaging.Filters.ResizeBilinear filter = new AForge.Imaging.Filters.ResizeBilinear(newWidth, newHeight);
                croppedFrame = filter.Apply(croppedFrame);
            }

            //// Display the zoomed frame in the VideoSourcePlayer control
            croppedFrame.RotateFlip(RotateFlipType.RotateNoneFlipX);
            //Cam_pic.Image = croppedFrame;

            OverlayImage(croppedFrame);
        }

        private void OverlayImage(Bitmap baseImage)
        {
            x1 = (desiredWidth / 2) - 190;//กรอบใน
            x3 = (desiredWidth / 2) - 300;//กรอบนอก

            y1 = (desiredHeight / 2) - 200;//กรอบใน
            y3 = (desiredHeight / 2) - 320;//กรอบนอก


            if (modal.Visible) // ถ้า modal ยังแสดงอยู่จะไม่ประมวลผลใบหน้า
            {

            }
            else
            {
                Mat imgMat = baseImage.ToMat();
                CvInvoke.CvtColor(imgMat, imgMat, ColorConversion.Bgr2Gray);
                Image<Gray, byte> imgGray = imgMat.ToImage<Gray, byte>();

                Rectangle[] rectangles = cascadeClassifier.DetectMultiScale(imgGray, 1.2, 1);

                foreach (Rectangle rectangle in rectangles)
                {
                    using (Graphics graphics = Graphics.FromImage(baseImage))
                    {
                        using (Pen pen = new Pen(Color.Red, 3))
                        {
                            graphics.DrawRectangle(pen, rectangle);
                            //int x1 = 0;//กรอบใน
                            //int x3 = 0;//กรอบนอก

                            //int y1 = 0;//กรอบใน
                            //int y3 = 0;//กรอบนอก

                            //int sizeX1 = 210;//กรอบนอก
                            //int sizeX3 = 400;//กรอบนอก

                            if ((
                                rectangle.Location.X >= x3 &&  //จุดซ้ายบนของกรอบแดงต้องมากกว่ากรอบนอก
                                rectangle.Location.X <= x1
                                ) && (
                                rectangle.Location.Y >= y3 &&
                                rectangle.Location.Y <= y1
                                ) &&
                                rectangle.Size.Width >= 0  &&
                                rectangle.Size.Height >= 0
                                )
                            {
                                Setlb_txt(msg_fit);
                                this.lb_txt.BackColor = Color.Green;
                                //PlayAudio(audio3);
                                //SetTimerClearText(true);
                                //SetPictureBoxLeft(false);
                                //SetPictureBoxRight(false);
                                //FaceDetection();
                            }
                            else if (rectangle.Location.X < (x3 - 100))//|| rectangle.Location.X > (x3 + sizeX1))
                            {
                                Setlb_txt(msg_center);//ไม่อยู่ตรงกลาง
                                this.lb_txt.BackColor = Color.Blue;
                                PlayAudio(audio2, 6500);
                                //SetPictureBoxLeft(true);
                                //SetTimerClearText(true);
                                Console.WriteLine("Left " + rectangle.Location.X);
                            }
                            else if (rectangle.Location.X < (x3 + 600))
                            {
                                Setlb_txt(msg_center);//ไม่อยู่ตรงกลาง
                                this.lb_txt.BackColor = Color.Blue;
                                PlayAudio(audio2, 6500);
                                //SetPictureBoxRight(true);
                                //SetTimerClearText(true);
                                Console.WriteLine("Right " + rectangle.Location.X);
                            }
                            else if (rectangle.Width <= sizeX3)
                            {
                                Setlb_txt(msg_near);//ไกลเกินไป
                                this.lb_txt.BackColor = Color.Red;
                                PlayAudio(audio4, 6500);
                                //SetTimerClearText(true);
                                Console.WriteLine("Too far " + rectangle.Width);
                            }
                            else if (rectangle.Width >= sizeX1)
                            {
                                Setlb_txt(msg_far);//ใกล้เกินไป
                                this.lb_txt.BackColor = Color.Red;
                                PlayAudio(audio4, 6500);
                                //SetTimerClearText(true);
                                Console.WriteLine("Too close " + rectangle.Width);
                            }
                        }
                    }
                }
            }
            #region comment
            using (Graphics g = Graphics.FromImage(baseImage))
            {
                //g.DrawImage(overlayImage, new Rectangle(new Point(0, 0), new Size(baseImage.Width, baseImage.Height)));

                g.DrawRectangle(new Pen(Color.Yellow, 3), new Rectangle(new Point(x1, y1), new Size(380, 380))); //กรอบใน
                g.DrawRectangle(new Pen(Color.Blue, 3), new Rectangle(new Point(x3, y3), new Size(600, 620)));//กรอบนอก

                g.DrawRectangle(new Pen(Color.White, 3), new Rectangle(new Point(desiredWidth / 2, 1), new Size(1, desiredHeight)));
                g.DrawRectangle(new Pen(Color.White, 3), new Rectangle(new Point(1, desiredHeight / 2), new Size(desiredWidth, 1)));
            }

            //hid_canv.Image = baseImage;
            #endregion

            Cam_pic.Image = baseImage;
            //baseImage.Dispose();
        }

        private void SetResolution(VideoCaptureDevice videoSource)
        {
            VideoCapabilities[] videoCapabilities = videoSource.VideoCapabilities;
            foreach (VideoCapabilities capabilty in videoCapabilities)
            {
                if (capabilty.FrameSize.Width == desiredWidth && capabilty.FrameSize.Height == desiredHeight)
                {
                    videoSource.VideoResolution = capabilty;
                    break;
                }
            }
        }

        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }

        delegate void SetSetlbTxtCallback(string text);
        delegate void SetlbRemarkCallback(string text);
        delegate void SetPictureBox3Callback(bool c);
        //delegate void SetPictureBox4Callback(bool c);
        delegate void SetVideoSourceCallback(VideoCaptureDevice c);
        delegate void SetPictureBoxLeftCallback(bool c);
        delegate void SetPictureBoxRightCallback(bool c);
        delegate void SetOverlayBoxCallback(bool c);
        delegate void SetResultBoxCallback(bool c);
        private void SetVideoSource(VideoCaptureDevice c)
        {
            if (this.loading_box.InvokeRequired)
            {
                SetVideoSourceCallback d = new SetVideoSourceCallback(SetVideoSource);
                this.Invoke(d, new object[] { c });
            }
            else
            {
                videoSource.SignalToStop();
                videoSource = c;
            }
        }

        private void Setlb_txt(string text)
        {
            if (this.lb_txt.InvokeRequired)
            {
                SetSetlbTxtCallback d = new SetSetlbTxtCallback(Setlb_txt);
                this.Invoke(d, new object[] { text });
                lb_txt.Location = new Point() { X = (this.Width / 2) - (lb_txt.Width / 2), Y = 30 };
            }
            else
            {
                this.lb_txt.Text = text;
                lb_txt.Location = new Point() { X = (this.Width / 2) - (lb_txt.Width / 2), Y = 30 };
            }
        }

        private void Setlb_remark(string text)
        {
            int y = this.Height;

            if (this.lb_remark.InvokeRequired)
            {
                SetlbRemarkCallback d = new SetlbRemarkCallback(Setlb_remark);
                this.Invoke(d, new object[] { text });
                //lb_remark.Location = new Point() { X = (this.Width / 2) - (lb_remark.Width / 2), Y = y - 100 };
            }
            else
            {
                this.lb_remark.Text = text;
                //lb_remark.Location = new Point() { X = (this.Width / 2) - (lb_remark.Width / 2), Y = y - 100 };
            }
        }

        private void SetPictureBox3(bool c)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.loading_box.InvokeRequired)
            {
                SetPictureBox3Callback d = new SetPictureBox3Callback(SetPictureBox3);
                this.Invoke(d, new object[] { c });
            }
            else
            {
                this.loading_box.Visible = c;
                loading_box.BringToFront();
            }
        }

        private void SetOverlayBox(bool c)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.Overlay_box.InvokeRequired)
            {
                SetOverlayBoxCallback d = new SetOverlayBoxCallback(SetOverlayBox);
                this.Invoke(d, new object[] { c });
            }
            else
            {
                this.Overlay_box.Visible = c;
            }
        }

        private void SetResultBox(bool c)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.result_panel.InvokeRequired)
            {
                SetResultBoxCallback d = new SetResultBoxCallback(SetResultBox);
                this.Invoke(d, new object[] { c });
            }
            else
            {
                this.result_panel.Visible = c;
            }
        }



        //private void SetPictureBoxLeft(bool c)
        //{
        //    // InvokeRequired required compares the thread ID of the
        //    // calling thread to the thread ID of the creating thread.
        //    // If these threads are different, it returns true.
        //    if (this.pic_arrow_left.InvokeRequired)
        //    {
        //        SetPictureBoxLeftCallback d = new SetPictureBoxLeftCallback(SetPictureBoxLeft);
        //        this.Invoke(d, new object[] { c });
        //    }
        //    else
        //    {
        //        this.pic_arrow_left.Visible = c;
        //    }
        //    pic_arrow_left.BackColor = Color.Transparent;
        //}

        //private void SetPictureBoxRight(bool c)
        //{
        //    // InvokeRequired required compares the thread ID of the
        //    // calling thread to the thread ID of the creating thread.
        //    // If these threads are different, it returns true.
        //    if (this.pic_arrow_rigth.InvokeRequired)
        //    {
        //        SetPictureBoxRightCallback d = new SetPictureBoxRightCallback(SetPictureBoxRight);
        //        this.Invoke(d, new object[] { c });
        //    }
        //    else
        //    {
        //        this.pic_arrow_rigth.Visible = c;
        //    }
        //    pic_arrow_rigth.BackColor = Color.Transparent;
        //}

        private void SetTimerClearText(bool c)
        {
            if (c)
            {
                timerClearText.Stop();
                timerClearText.Enabled = false;
                timerClearText.Enabled = c;
                timerClearText.Interval = 3000;
                timerClearText.Start();
            }
            else
            {
                timerClearText.Stop();
                timerClearText.Enabled = c;
            }
        }
        private void timerClearText_Tick(object sender, EventArgs e)
        {
            Setlb_txt("");
        }

        // Step 1 ผู้ใช้งานทำการสแกนใบหน้า ในกรณีที่ผ่านจะแสดงหน้าผ่าน
        // Step 2 ในกรณีที่สแกนใบหน้าไม่ผ่าน ครั้งแรก จะเข้าสู่ State == 1st_try และจะได้สแกนใหม่อีกครั้ง
        // Step 3 ในกรณีที่อยู่ใน State == 1st_try ถ้ายังสแกนไม่ผ่านจะเข้าสู่ State == Exit
        // Step 4 ในกรณีที่อยู่ใน State == exit แล้วกดปุ่ม button1 จะทำการปิด modal และ Return Main page
        // Step 4.1 หากไม่กดปุ่มภายใน 5 วิจะทำการปิดอัตโนมัติ และ Return Main page


        //private void SetPictureBox4(bool c)
        //{
        //    // InvokeRequired required compares the thread ID of the
        //    // calling thread to the thread ID of the creating thread.
        //    // If these threads are different, it returns true.
        //    if (this.pictureBox4.InvokeRequired)
        //    {
        //        SetPictureBox4Callback d = new SetPictureBox4Callback(SetPictureBox4);
        //        this.Invoke(d, new object[] { c });
        //    }
        //    else
        //    {
        //        this.pictureBox4.Visible = c;
        //    }
        //}

        #region Handler
        // Handler for the LoadCompleted event.
        private void player_LoadCompleted(object sender,
            AsyncCompletedEventArgs e)
        {
            Console.WriteLine("Load Complete");
        }

        // Handler for the SoundLocationChanged event.
        private void player_LocationChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Change File");
        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
