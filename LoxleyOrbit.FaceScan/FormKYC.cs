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
using System.Drawing.Drawing2D;

namespace LoxleyOrbit.FaceScan
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class FormKYC : Form
    {
        #region var
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
        public bool Detection = false;
        private string Closing_State = "";

        public string m_txtID = "1570800044928";
        public string m_type = "ID";

        static readonly CascadeClassifier cascadeClassifier = new CascadeClassifier("haarcascade_frontalface_alt_tree.xml");
        //Image overlayImage = Image.FromFile(@"Images\Head&Shoulder_Black.png");

        int desiredWidth = 1920;
        int desiredHeight = 1080;
        FaceResult faceResult = new FaceResult();

        string retry = "1st";

        int x1 = 0;//กรอบใน
        int x3 = 0;//กรอบนอก

        int y1 = 0;//กรอบใน
        int y3 = 0;//กรอบนอก

        int sizeX1 = 210;//กรอบนอก
        int sizeX3 = 400;//กรอบนอก

        double focusCenterX = 0.5; // 50%
        double focusCenterY = 0.5; // 50%
        double scaleFactor = 1.75;
        double aspectRatio = 16.0 / 9.0; // 16:9 aspect ratio
        #endregion

        #region Audio Var
        static string audio1 = @"Audio\Audio1.wav";
        static string audio2 = @"Audio\Audio2.wav";
        static string audio3 = @"Audio\Audio3.wav";
        static string audio4 = @"Audio\Audio4.wav";

        private DateTime dateTimeStart = DateTime.Now;
        private double totalMilliseconds = 0;
        private int lasttimeOut = 0;
        private bool isAudioPlaying = true;
        private SoundPlayer player;

        #region Delay_Detection
        private DateTime dateTimeStart_delay = DateTime.Now;
        static int Delay_detection = 6500;
        private TimeSpan Delay_Count = new TimeSpan();
        private double milisec = 0;
        private bool IsDelay_Active = false;
        #endregion

        #region Delay_Close_Form
        private DateTime dateTimeStart_Close = DateTime.Now;
        static int Delay_Close = 6500;
        private TimeSpan Delay_Count_c = new TimeSpan();
        private double milisec_c = 0;
        private bool IsDelay_C_Active = false;
        #endregion

        #endregion

        public FormKYC()
        {
            InitializeComponent();
            InitializeCamera();
            InitializeObject();
            InitializeSound();
        }

        private void InitializeObject()
        {
            //int x = this.Width - Cam_pic.Width;
            //int y = this.Height - Cam_pic.Height;
            //Cam_pic.Location = new Point() { X = (x / 2), Y = (y / 2) };
            //loading_box.Location = new Point() { X = (Cam_pic.Width / 2) - (loading_box.Width / 2), Y = (Cam_pic.Height / 2) - (loading_box.Height / 2) };
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
            RESET_CAMERA();
            Closing_State = "";
            SetPictureBox3(false);
            SetOverlayBox(false);
            Set_result(true,false);

            //Automatic Select Camera 1
            selected = comboBox1.SelectedIndex;
            START_Click(null, null);

            //Zoom Scale 1.75
            radioButton4.Checked = true;

            int x = this.Width;
            int y = this.Height;

            Cam_pic.Width = this.Width;
            Cam_pic.Height = this.Height;
            Cam_pic.Location = new Point(0, 0);
            Cam_pic.Controls.Add(Overlay_box);

            Overlay_box.Width = this.Width;
            Overlay_box.Height = this.Height;
            Overlay_box.Location = new Point(0, 0);
            Overlay_box.BackColor = Color.Transparent;

            Overlay_box.Controls.Add(loading_box);
            int lx = (x / 2) - (loading_box.Width / 2);
            int ly = (y / 2) - (loading_box.Height / 2);
            loading_box.Location = new Point(lx, ly);
            loading_box.BackColor = Color.Transparent;

            lb_remark.Visible = true;
            lb_remark.Location = new Point() { X = (this.Width / 2) - (lb_remark.Width / 2), Y = y - 100 };
            lb_remark.Text = mgs_remark;

            Cam_pic.SendToBack();
            Overlay_box.BringToFront();

            #region result_panel
            Label Lhead = result_panel.Controls["Head"] as Label;
            Label Ldesc = result_panel.Controls["desc"] as Label;

            PictureBox Pbox = result_panel.Controls["pictureBox"] as PictureBox;
            PictureBox Pbtn_exit = result_panel.Controls["pictureBox3"] as PictureBox;
            Button B2 = result_panel.Controls["button2"] as Button;

            result_panel.Location = new Point(x = x/2 - result_panel.Width/2, y = y / 2 - result_panel.Height / 2);
            Lhead.Location = new Point(x = result_panel.Width / 2 - Lhead.Width / 2 , y = Lhead.Location.Y);
            Ldesc.Location = new Point(x = result_panel.Width / 2 - Ldesc.Width / 2, y = Ldesc.Location.Y);

            result_panel.Visible = false;
            pnl_no_retry.Visible = false;
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

        private void FormKYC_FormClosing(object sender, FormClosingEventArgs e)
        {
            RESET_CAMERA();

            if (e.CloseReason == CloseReason.UserClosing || e.CloseReason == CloseReason.ApplicationExitCall)
            {
                // Close all forms in the application
                //Application.Exit();
            }

            #region PID
            try
            {
                FormPID pid = (FormPID)Application.OpenForms["FormPID"];
                if (pid != null)
                {
                    if (Closing_State == "Pass")
                    {
                        pid.trigger_when_Close("Pass");
                    }

                    else if (Closing_State == "Fail")
                    {
                        pid.trigger_when_Close("Fail");
                    }

                    else if (Closing_State == "Close")
                    {
                        pid.trigger_when_Close("Close");
                    }

                    if (this.Owner != null)
                    {
                        this.Owner.Show();
                        this.Owner.Focus();
                    }
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                //Application.Exit();
            }
            #endregion
        }


        #region Audio Control
        private void PlayAudio(string audio, int timeOut)
        {
            try
            {
                //SoundPlayer soundPlayer = new SoundPlayer(audio);
                player.SoundLocation = audio;
                if(audio == audio1) // เมื่อเล่นเสียง Audio1 จะเริ่มนับถอยหลัง
                {
                    dateTimeStart_delay = DateTime.Now;
                    IsDelay_Active = true;
                }
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
                    Detection = false;
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
        private void CAPTURE_Click(object sender, EventArgs e)
        {
            Capture_cam_pic(true);
        }
        private void FACEDETECTION_Click(object sender, EventArgs e)
        {
            //FaceDetection();
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

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Application.Exit();
            }
        }


        #region CMMD
        //private void OverlayImage(Bitmap baseImage)
        //{
        //    if (!result_panel.Visible) // ถ้า Result Panel ไม่แสดงจะทำการประมวลผลตามปกติ
        //    {
        //        x1 = (desiredWidth / 2) - 190;//กรอบใน
        //        x3 = (desiredWidth / 2) - 300;//กรอบนอก

        //        y1 = (desiredHeight / 2) - 200;//กรอบใน
        //        y3 = (desiredHeight / 2) - 320;//กรอบนอก

        //        Mat imgMat = baseImage.ToMat();
        //        CvInvoke.CvtColor(imgMat, imgMat, ColorConversion.Bgr2Gray);
        //        Image<Gray, byte> imgGray = imgMat.ToImage<Gray, byte>();

        //        Rectangle[] rectangles = cascadeClassifier.DetectMultiScale(imgGray, 1.2, 1);
        //        foreach (Rectangle rectangle in rectangles)
        //        {
        //            using (Graphics graphics = Graphics.FromImage(baseImage))
        //            {
        //                using (Pen pen = new Pen(Color.Red, 3))
        //                {
        //                    graphics.DrawRectangle(pen, rectangle);
        //                    //int x1 = 0;//กรอบใน
        //                    //int x3 = 0;//กรอบนอก

        //                    //int y1 = 0;//กรอบใน
        //                    //int y3 = 0;//กรอบนอก

        //                    //int sizeX1 = 210;//กรอบนอก
        //                    //int sizeX3 = 400;//กรอบนอก

        //                    if ((
        //                        rectangle.Location.X >= x3 &&  //จุดซ้ายบนของกรอบแดงต้องมากกว่ากรอบนอก
        //                        rectangle.Location.X <= x1
        //                        ) && (
        //                        rectangle.Location.Y >= y3 &&
        //                        rectangle.Location.Y <= y1
        //                        ) &&
        //                        rectangle.Size.Width >= 0 &&
        //                        rectangle.Size.Height >= 0
        //                        )
        //                    {
        //                        Setlb_txt(msg_fit);
        //                        this.lb_txt.BackColor = Color.Green;
        //                        FaceDetection();
        //                    }
        //                    else if (rectangle.Location.X < (x3 - 100))//|| rectangle.Location.X > (x3 + sizeX1))
        //                    {
        //                        Setlb_txt(msg_center);//ไม่อยู่ตรงกลาง
        //                        this.lb_txt.BackColor = Color.Blue;
        //                        PlayAudio(audio2, 6500);
        //                        //SetPictureBoxLeft(true);
        //                        //SetTimerClearText(true);
        //                        Console.WriteLine("Left " + rectangle.Location.X);
        //                    }
        //                    else if (rectangle.Location.X < (x3 + 600))
        //                    {
        //                        Setlb_txt(msg_center);//ไม่อยู่ตรงกลาง
        //                        this.lb_txt.BackColor = Color.Blue;
        //                        PlayAudio(audio2, 6500);
        //                        //SetPictureBoxRight(true);
        //                        //SetTimerClearText(true);
        //                        Console.WriteLine("Right " + rectangle.Location.X);
        //                    }
        //                    else if (rectangle.Width <= sizeX3)
        //                    {
        //                        Setlb_txt(msg_near);//ไกลเกินไป
        //                        this.lb_txt.BackColor = Color.Red;
        //                        PlayAudio(audio4, 6500);
        //                        //SetTimerClearText(true);
        //                        Console.WriteLine("Too far " + rectangle.Width);
        //                    }
        //                    else if (rectangle.Width >= sizeX1)
        //                    {
        //                        Setlb_txt(msg_far);//ใกล้เกินไป
        //                        this.lb_txt.BackColor = Color.Red;
        //                        PlayAudio(audio4, 6500);
        //                        //SetTimerClearText(true);
        //                        Console.WriteLine("Too close " + rectangle.Width);
        //                    }
        //                }
        //            }
        //        }
        //        #region comment
        //        //using (Graphics g = Graphics.FromImage(baseImage))
        //        //{
        //        //    //g.DrawImage(overlayImage, new Rectangle(new Point(0, 0), new Size(baseImage.Width, baseImage.Height)));

        //        //    g.DrawRectangle(new Pen(Color.Yellow, 3), new Rectangle(new Point(x1, y1), new Size(380, 380))); //กรอบใน
        //        //    g.DrawRectangle(new Pen(Color.Blue, 3), new Rectangle(new Point(x3, y3), new Size(600, 620)));//กรอบนอก

        //        //    g.DrawRectangle(new Pen(Color.White, 3), new Rectangle(new Point(desiredWidth / 2, 1), new Size(1, desiredHeight)));
        //        //    g.DrawRectangle(new Pen(Color.White, 3), new Rectangle(new Point(1, desiredHeight / 2), new Size(desiredWidth, 1)));
        //        //}
        //        #endregion
        //    }
        //    else
        //    {
        //        //Do nothing
        //    }

        //    Cam_pic.Image = baseImage;
        //    //baseImage.Dispose();
        //}
        #endregion

        private void OverlayImage(Bitmap baseImage)
        {
            #region Delay_Detection
                Delay_Count = DateTime.Now - dateTimeStart_delay;
                milisec = Delay_Count.TotalMilliseconds;

                //Console.WriteLine(milisec);
                if ((milisec >= Delay_detection) && IsDelay_Active && !pnl_no_retry.Visible)
                {
                    Console.WriteLine("BABO");
                    IsDelay_Active = false;
                    Detection = true;
                }
            #endregion

            #region Delay_Close_Form
            Delay_Count_c = DateTime.Now - dateTimeStart_Close;
            milisec_c = Delay_Count_c.TotalMilliseconds;

            if ((milisec_c >= Delay_Close) && IsDelay_C_Active && pnl_no_retry.Visible)
            {
                PictureBox pbtn = pnl_no_retry.Controls["pictureBox2"] as PictureBox;
                if (pbtn.Visible)
                {//กรณี Fail
                    CloseForm();
                    //webBrowser.Navigate("http://test-kiosk.chulacareapp.com/OneMLWeb/SelectUserType.aspx");
                    IsDelay_Active = false;
                }
                else
                {//กรณี Pass
                    CloseForm();
                    //webBrowser.Navigate("http://test-kiosk.chulacareapp.com/OneMLWeb/User_detail.aspx?facedetection=off");
                    IsDelay_Active = false;
                }
            }
            #endregion

            if (Detection)
            {

                x1 = (desiredWidth / 2) - 190;//กรอบใน
                x3 = (desiredWidth / 2) - 300;//กรอบนอก

                y1 = (desiredHeight / 2) - 200;//กรอบใน
                y3 = (desiredHeight / 2) - 320;//กรอบนอก

                Mat imgMat = baseImage.ToMat();
                CvInvoke.CvtColor(imgMat, imgMat, ColorConversion.Bgr2Gray);
                Image<Gray, byte> imgGray = imgMat.ToImage<Gray, byte>();

                Rectangle[] rectangles = cascadeClassifier.DetectMultiScale(imgGray, 1.2, 1); // Scan
                Rectangle Dzone = new Rectangle(new Point(x3, y3), new Size(600, 620)); //กรอบนอก
                Rectangle Lzone = new Rectangle(new Point(x1, y1), new Size(380, 380)); //กรอบใน

                foreach (Rectangle Fzone in rectangles)
                {
                    using (Graphics graphics = Graphics.FromImage(baseImage))
                    {
                        using (Pen pen = new Pen(Color.Red, 3))
                        {
                            graphics.DrawRectangle(pen, Fzone);
                            graphics.DrawRectangle(new Pen(Color.Yellow, 3), new Rectangle(new Point(x1, y1), new Size(380, 380))); //กรอบใน
                            graphics.DrawRectangle(new Pen(Color.Blue, 3), new Rectangle(new Point(x3, y3), new Size(600, 620)));//กรอบนอก

                            if(Dzone.Contains(Fzone) && Fzone.Contains(Lzone))
                            {
                                Console.WriteLine("Booya"); //ผ่าน
                                Setlb_txt(msg_fit);
                                this.lb_txt.BackColor = Color.Green;
                                FaceDetection();
                            }
                            else if (Dzone.Contains(Fzone))
                            {
                                Console.WriteLine("Too Far");
                                Setlb_txt(msg_adjust);//ไกลเกินไป
                                this.lb_txt.BackColor = Color.Red;
                                PlayAudio(audio4, 6500);
                            }
                            else if (Fzone.Contains(Dzone))
                            {
                                Console.WriteLine("Too Close");
                                Setlb_txt(msg_adjust);//ใกล้เกินไป
                                this.lb_txt.BackColor = Color.Red;
                                PlayAudio(audio4, 6500);
                            }
                            else
                            {
                                Setlb_txt(msg_center);//ไม่อยู่ตรงกลาง
                                this.lb_txt.BackColor = Color.Blue;
                                PlayAudio(audio2, 6500);
                                Console.WriteLine("Please Center");
                            }
                        }
                    }
                }
            }
            else
            {
                //do nothing
            }
            Cam_pic.Image = baseImage;
        }

        private void FaceDetection()
        {
            SetPictureBox3(true);
            isAudioPlaying = true;
            SetOverlayBox(true);
            PlayAudio(audio3, 3900);
            CAPTURE_Click(null, null);
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
            }

            APIHelper.StatusCodeSame = Same;

            if (Same == 1)
            {
                SetPictureBox3(false);
                Set_result(true,true); // Result ผ่าน
                 // Loading
            }
            else
            {
                SetPictureBox3(false); // Loading
                if (retry == "1st")
                {
                    SetResultBox(true); // Retry
                    retry = "no";
                }
                else
                {
                    Set_result(false,true); // Result ไม่ผ่าน
                }
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
                //RESET_CAMERA();
                CloseForm();
            }
        }

        #region util
        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
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

                    // Convert byte[] to Base64 String
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }
        #endregion

        #region Set Delegate Func
        delegate void SetSetlbTxtCallback(string text);
        delegate void SetlbRemarkCallback(string text);
        delegate void SetVideoSourceCallback(VideoCaptureDevice c);
        delegate void SetPictureBoxLeftCallback(bool c);
        delegate void SetPictureBoxRightCallback(bool c);
        delegate void SetOverlayBoxCallback(bool c);
        delegate void SetResultBoxCallback(bool c);
        delegate void Setpnl_no_retryCallback(bool status, bool c);
        delegate void SetPictureBox3Callback(bool c);
        delegate void SetCapture_cam_pic(bool c);
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
            Detection = !c;
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
            Overlay_box.Width = this.Width;
            Overlay_box.Height = this.Height;


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
            Detection = !c;
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
        private void Capture_cam_pic(bool c)
        {
            if (this.Cam_pic.InvokeRequired)
            {
                SetCapture_cam_pic d = new SetCapture_cam_pic(Capture_cam_pic);
                this.Invoke(d, new object[] { c });
            }
            else
            {
                string fileName = @"Images/face-detect-set/fromCamera/" + m_txtID + ".jpg";
                //Cam_pic.Image.Save(fileName, ImageFormat.Jpeg);

                // Step 1: Determine the size of the cropped portion
                double cW = Cam_pic.Image.Width / 2.4; //More Divider = Less Resolution
                double cH = Cam_pic.Image.Height / 1.25;
                int cropWidth = Convert.ToInt32(cW);
                int cropHeight = Convert.ToInt32(cH);

                // Step 2: Determine the location of the cropped portion
                double cX = (Cam_pic.Image.Width - cropWidth) / 2;
                double cY = (Cam_pic.Image.Height - cropHeight) / 1.8;
                int cropX = Convert.ToInt32(cX);
                int cropY = Convert.ToInt32(cY);

                // Step 3: Create a new Bitmap to hold the cropped portion
                Bitmap croppedImage = new Bitmap(cropWidth, cropHeight);

                // Step 4: Crop the image
                using (Graphics g = Graphics.FromImage(croppedImage))
                {
                    g.DrawImage(Cam_pic.Image, new Rectangle(0, 0, cropWidth, cropHeight), new Rectangle(cropX, cropY, cropWidth, cropHeight), GraphicsUnit.Pixel);
                }

                croppedImage.Save(fileName, ImageFormat.Jpeg);
            }
        }


        private void CloseForm()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(CloseForm));
            }
            else
            {
                RESET_CAMERA();
                this.Close();
            }
        }
        #endregion

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

        #region pnl_no_retry
        private void Set_result(bool status, bool c)
        {
            Detection = !c;
            //RESET_CAMERA();

            dateTimeStart_Close = DateTime.Now;
            IsDelay_C_Active = true;

            if (this.pnl_no_retry.InvokeRequired)
            {
                Setpnl_no_retryCallback d = new Setpnl_no_retryCallback(Set_result);
                this.Invoke(d, new object[] { status, c });
            }
            else
            {
                pnl_no_retry.Width = this.Width;
                pnl_no_retry.Height = this.Height;
                pnl_no_retry.Location = new Point(0, 0);
                PictureBox pgif = pnl_no_retry.Controls["pictureBox1"] as PictureBox;
                PictureBox pbtn = pnl_no_retry.Controls["pictureBox2"] as PictureBox;
                pgif.Visible = false;
                pbtn.Visible = false;

                if (status)
                {
                    pgif.Visible = true;
                    pgif.Image = Properties.Resources.Check;
                    pgif.Location = new Point(1050, 600);
                    pgif.Width = 200;
                    pgif.Height = 200;
                    pnl_no_retry.BackgroundImage = Properties.Resources.bg1920_success;
                    pnl_no_retry.Visible = true;

                    Closing_State = "Pass";
                } // Result ผ่าน
                else
                {
                    pbtn.Visible = true;
                    pbtn.Image = Properties.Resources.button_ok;
                    pbtn.Width = 300;
                    pbtn.Height = 114;
                    pbtn.Location = new Point(pnl_no_retry.Width / 2 - pbtn.Width / 2, 780);
                    pnl_no_retry.BackgroundImage = Properties.Resources.bg1920_fail;
                    pnl_no_retry.Visible = true;

                    Closing_State = "Fail";
                } // Result ไม่ผ่าน

                // hide or show the panel depending on the value of the 'c' parameter
                this.pnl_no_retry.Visible = c;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pnl_no_retry.Visible = false;
            Closing_State = "Fail";
            CloseForm();

        }
        #endregion

        #region result_panel
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            result_panel.Visible = false;
            RESET_CAMERA();
            selected = comboBox1.SelectedIndex;
            START_Click(null, null);
        }//สแกนซ้ำ

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            result_panel.Visible = false;
            videoSource.SignalToStop();
            Closing_State = "Close";
            CloseForm();
        }//ยกเลิก
        #endregion

        #region Unit Test button
        private void button1_Click(object sender, EventArgs e)
        {
            CloseFrom("ต้องการทำการ eKYC ใหม่หรือไม่?", "แจ้งเตือน");
        }
        private void button4_Click(object sender, EventArgs e)
        {
            SetResultBox(true);
        }//Retry

        private void button5_Click(object sender, EventArgs e)
        {
            Set_result(true, true);
        }//Pass

        private void button6_Click(object sender, EventArgs e)
        {
            Set_result(false,true);
        }//Fail

        private void close_result_Click(object sender, EventArgs e)
        {
            Set_result(false, false);
        }//Close Panel Pass/Fail
        #endregion

        private void FormKYC_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }


}
