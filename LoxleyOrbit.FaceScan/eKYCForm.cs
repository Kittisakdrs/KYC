using AForge.Video;
using AForge.Video.DirectShow;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoxleyOrbit.FaceScan
{
    public partial class eKYCForm : Form
    {
        FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        VideoCaptureDevice videoSource = null;

        static readonly CascadeClassifier cascadeClassifier = new CascadeClassifier("haarcascade_frontalface_alt_tree.xml");
        Image overlayImage = Image.FromFile(@"Images\Head&Shoulder_Black.png");

        private readonly string msg_near = "กรุณาขยับเข้าหากล้องเพื่อสแกนใบหน้า";
        private readonly string msg_far = "กรุณาถอยห่างจากกล้องเพื่อสแกนใบหน้า";
        private readonly string msg_fit = "กรุณาอย่าขยับเพื่อสแกนใบหน้า";
        private readonly string msg_center = "กรุณาอยู่ตรงกลาง";
        private readonly string mgs_remark = "(วางใบหน้าบนกรอบที่กำหนด ไม่ปิดคิ้วตาจมูก ปากและคาง)";

        public string m_txtID = "";
        public string m_type = "";

        public int islemdurumu = 0; //CAMERA STATUS
        public int selected = 0;
        public static int durdur = 0;
        public static int gondermesayisi = 0;
        public int kamerabaslat = 0;
        private bool chkCamera = true;
        double focusCenterX = 0.5; // 50%
        double focusCenterY = 0.5; // 50%
        double scaleFactor = 1.75;
        double aspectRatio = 16.0 / 9.0; // 16:9 aspect ratio

        public eKYCForm()
        {
            InitializeComponent();
            InitializeCamera();
        }

        private void InitializeCamera()
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"Audio1.wav");
            player.Play();

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
                }
            }
            catch (ApplicationException)
            {
                this.label1.Text = "No local capture devices";
                videoDevices = null;
            }
        }
        private void eKYCForm_Load(object sender, EventArgs e)
        {
            selected = comboBox1.SelectedIndex;
            StartCamera();
        }
        private void StartCamera()
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
                    videoSource.Start();
                    chkCamera = true;
                    kamerabaslat = 1; //CAMERA STARTRED
                }
                catch (Exception ex)
                {
                    MessageBox.Show("RESTART THE PROGRAM : " + ex.Message);
                    if (!(videoSource == null))
                        if (videoSource.IsRunning)
                        {
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
                if (capabilty.FrameSize.Width == PicBox1.Width && capabilty.FrameSize.Height == PicBox1.Height)
                {
                    videoSource.VideoResolution = capabilty;
                    break;
                }
            }
        }

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            int scaleFactor = 2;

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

            // Display the zoomed frame in the VideoSourcePlayer control
            croppedFrame.RotateFlip(RotateFlipType.RotateNoneFlipX);
            PicBox1.Image = croppedFrame;

            OverlayImage(croppedFrame);
        }

        int x1 = 0;//กรอบใน
        int x3 = 0;//กรอบนอก

        int y1 = 0;//กรอบใน
        int y3 = 0;//กรอบนอก

        int sizeX1 = 210;//กรอบนอก
        int sizeX3 = 400;//กรอบนอก

        private void OverlayImage(Bitmap baseImage)
        {
            x1 = (picBox2.Width / 2) - 190;//กรอบใน
            x3 = (picBox2.Width / 2) - 300;//กรอบนอก

            y1 = (picBox2.Height / 2) - 200;//กรอบใน
            y3 = (picBox2.Height / 2) - 320;//กรอบนอก

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

                        if ((rectangle.Location.X >= x3 && rectangle.Location.X <= x1)
                            && (rectangle.Location.Y >= y3 && rectangle.Location.Y <= y1) 
                            && rectangle.Size.Width >= 0 && rectangle.Size.Height >= 0)
                        {
                            SetlbTxtWarningCallback(msg_fit, Color.Green);
                            //FaceDetection();
                        }
                        else if (rectangle.Location.X < (x3 - 100))//|| rectangle.Location.X > (x3 + sizeX1))
                        {
                            SetlbTxtWarningCallback(msg_center, Color.Blue);//ไม่อยู่ตรงกลาง
                        }
                        else if (rectangle.Location.X < (x3 + 600))
                        {
                            SetlbTxtWarningCallback(msg_center, Color.Blue);//ไม่อยู่ตรงกลาง
                        }
                        else if (rectangle.Width <= sizeX3)
                        {
                            SetlbTxtWarningCallback(msg_near, Color.Red);//ไกลเกินไป
                        }
                        else if (rectangle.Width >= sizeX1)
                        {
                            SetlbTxtWarningCallback(msg_far, Color.Red);//ใกล้เกินไป

                        }
                    }
                }
            }

            using (Graphics g = Graphics.FromImage(baseImage))
            {
                g.DrawRectangle(new Pen(Color.Yellow, 3), new Rectangle(new Point(x1, y1), new Size(380, 380))); //กรอบใน
                g.DrawRectangle(new Pen(Color.Blue, 3), new Rectangle(new Point(x3, y3), new Size(600, 620)));//กรอบนอก
            }

            picBox2.Image = baseImage;
        }

        delegate void SetlbTxtCallback(string text, Color color);
        private void SetlbTxtWarningCallback(string text, Color color)
        {
            if (this.lb_warning.InvokeRequired)
            {
                SetlbTxtCallback d = new SetlbTxtCallback(SetlbTxtWarningCallback);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.lb_warning.Text = text;
                this.lb_warning.BackColor = color;
            }
        }
    }
}
