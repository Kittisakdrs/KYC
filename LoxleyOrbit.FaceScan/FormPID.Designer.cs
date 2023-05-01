
namespace LoxleyOrbit.FaceScan
{
    partial class FormPID
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPID));
            this.label1 = new System.Windows.Forms.Label();
            this.btngetReaderID = new System.Windows.Forms.Button();
            this.m_txtReaderID = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.m_txtIssueplace = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnClearScreen = new System.Windows.Forms.Button();
            this.m_txtIssueNum = new System.Windows.Forms.TextBox();
            this.m_txtFullNameE = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_txtGender = new System.Windows.Forms.TextBox();
            this.m_picPhoto = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.m_ListReaderCard = new System.Windows.Forms.ComboBox();
            this.m_txtAddress = new System.Windows.Forms.TextBox();
            this.m_txtID = new System.Windows.Forms.TextBox();
            this.m_txtBrithDate = new System.Windows.Forms.TextBox();
            this.m_txtExpiryDate = new System.Windows.Forms.TextBox();
            this.m_txtIssueDate = new System.Windows.Forms.TextBox();
            this.m_txtFullNameT = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnReadcard = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lbl_Time = new System.Windows.Forms.Label();
            this.btnEKCY = new System.Windows.Forms.Button();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.tm_sensor = new System.Windows.Forms.Timer(this.components);
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.m_picPhoto)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(27, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 20);
            this.label1.TabIndex = 146;
            this.label1.Text = "ข้อมูลบัตรประจำตัวประชาชน";
            // 
            // btngetReaderID
            // 
            this.btngetReaderID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btngetReaderID.Location = new System.Drawing.Point(206, 409);
            this.btngetReaderID.Name = "btngetReaderID";
            this.btngetReaderID.Size = new System.Drawing.Size(84, 28);
            this.btngetReaderID.TabIndex = 145;
            this.btngetReaderID.Text = "Reader ID";
            this.btngetReaderID.UseVisualStyleBackColor = false;
            this.btngetReaderID.Click += new System.EventHandler(this.btngetReaderID_Click);
            // 
            // m_txtReaderID
            // 
            this.m_txtReaderID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.m_txtReaderID.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.m_txtReaderID.Location = new System.Drawing.Point(117, 342);
            this.m_txtReaderID.Name = "m_txtReaderID";
            this.m_txtReaderID.ReadOnly = true;
            this.m_txtReaderID.Size = new System.Drawing.Size(462, 24);
            this.m_txtReaderID.TabIndex = 144;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label11.Location = new System.Drawing.Point(21, 340);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 18);
            this.label11.TabIndex = 143;
            this.label11.Text = "Reader ID";
            // 
            // m_txtIssueplace
            // 
            this.m_txtIssueplace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_txtIssueplace.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.m_txtIssueplace.Location = new System.Drawing.Point(146, 310);
            this.m_txtIssueplace.Name = "m_txtIssueplace";
            this.m_txtIssueplace.ReadOnly = true;
            this.m_txtIssueplace.Size = new System.Drawing.Size(434, 24);
            this.m_txtIssueplace.TabIndex = 142;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label13.Location = new System.Drawing.Point(22, 313);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(119, 18);
            this.label13.TabIndex = 141;
            this.label13.Text = "หน่วยงานที่ออกบัตร";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label12.Location = new System.Drawing.Point(21, 112);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(51, 20);
            this.label12.TabIndex = 140;
            this.label12.Text = "Name";
            // 
            // btnClearScreen
            // 
            this.btnClearScreen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnClearScreen.Location = new System.Drawing.Point(296, 409);
            this.btnClearScreen.Name = "btnClearScreen";
            this.btnClearScreen.Size = new System.Drawing.Size(90, 28);
            this.btnClearScreen.TabIndex = 139;
            this.btnClearScreen.Text = "Clear Screen";
            this.btnClearScreen.UseVisualStyleBackColor = false;
            this.btnClearScreen.Click += new System.EventHandler(this.btnClearScreen_Click);
            // 
            // m_txtIssueNum
            // 
            this.m_txtIssueNum.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_txtIssueNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.m_txtIssueNum.Location = new System.Drawing.Point(445, 211);
            this.m_txtIssueNum.Name = "m_txtIssueNum";
            this.m_txtIssueNum.ReadOnly = true;
            this.m_txtIssueNum.Size = new System.Drawing.Size(133, 22);
            this.m_txtIssueNum.TabIndex = 138;
            this.m_txtIssueNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // m_txtFullNameE
            // 
            this.m_txtFullNameE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_txtFullNameE.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.m_txtFullNameE.Location = new System.Drawing.Point(118, 109);
            this.m_txtFullNameE.Name = "m_txtFullNameE";
            this.m_txtFullNameE.ReadOnly = true;
            this.m_txtFullNameE.Size = new System.Drawing.Size(317, 26);
            this.m_txtFullNameE.TabIndex = 137;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(401, 245);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 20);
            this.label2.TabIndex = 136;
            this.label2.Text = "เพศ";
            // 
            // m_txtGender
            // 
            this.m_txtGender.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_txtGender.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.m_txtGender.Location = new System.Drawing.Point(442, 242);
            this.m_txtGender.Name = "m_txtGender";
            this.m_txtGender.ReadOnly = true;
            this.m_txtGender.Size = new System.Drawing.Size(138, 26);
            this.m_txtGender.TabIndex = 135;
            // 
            // m_picPhoto
            // 
            this.m_picPhoto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_picPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_picPhoto.Location = new System.Drawing.Point(445, 43);
            this.m_picPhoto.Name = "m_picPhoto";
            this.m_picPhoto.Size = new System.Drawing.Size(133, 159);
            this.m_picPhoto.TabIndex = 134;
            this.m_picPhoto.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label9.Location = new System.Drawing.Point(21, 376);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 20);
            this.label9.TabIndex = 133;
            this.label9.Text = "เครื่องอ่านบัตร";
            // 
            // m_ListReaderCard
            // 
            this.m_ListReaderCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.m_ListReaderCard.FormattingEnabled = true;
            this.m_ListReaderCard.Location = new System.Drawing.Point(118, 372);
            this.m_ListReaderCard.Name = "m_ListReaderCard";
            this.m_ListReaderCard.Size = new System.Drawing.Size(462, 28);
            this.m_ListReaderCard.TabIndex = 132;
            // 
            // m_txtAddress
            // 
            this.m_txtAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_txtAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.m_txtAddress.Location = new System.Drawing.Point(118, 143);
            this.m_txtAddress.Multiline = true;
            this.m_txtAddress.Name = "m_txtAddress";
            this.m_txtAddress.ReadOnly = true;
            this.m_txtAddress.Size = new System.Drawing.Size(317, 90);
            this.m_txtAddress.TabIndex = 130;
            // 
            // m_txtID
            // 
            this.m_txtID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_txtID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.m_txtID.Location = new System.Drawing.Point(176, 41);
            this.m_txtID.Name = "m_txtID";
            this.m_txtID.ReadOnly = true;
            this.m_txtID.Size = new System.Drawing.Size(259, 26);
            this.m_txtID.TabIndex = 129;
            // 
            // m_txtBrithDate
            // 
            this.m_txtBrithDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_txtBrithDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.m_txtBrithDate.Location = new System.Drawing.Point(118, 242);
            this.m_txtBrithDate.Name = "m_txtBrithDate";
            this.m_txtBrithDate.ReadOnly = true;
            this.m_txtBrithDate.Size = new System.Drawing.Size(137, 26);
            this.m_txtBrithDate.TabIndex = 128;
            // 
            // m_txtExpiryDate
            // 
            this.m_txtExpiryDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_txtExpiryDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.m_txtExpiryDate.Location = new System.Drawing.Point(442, 277);
            this.m_txtExpiryDate.Name = "m_txtExpiryDate";
            this.m_txtExpiryDate.ReadOnly = true;
            this.m_txtExpiryDate.Size = new System.Drawing.Size(138, 26);
            this.m_txtExpiryDate.TabIndex = 127;
            // 
            // m_txtIssueDate
            // 
            this.m_txtIssueDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_txtIssueDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.m_txtIssueDate.Location = new System.Drawing.Point(118, 277);
            this.m_txtIssueDate.Name = "m_txtIssueDate";
            this.m_txtIssueDate.ReadOnly = true;
            this.m_txtIssueDate.Size = new System.Drawing.Size(137, 26);
            this.m_txtIssueDate.TabIndex = 131;
            // 
            // m_txtFullNameT
            // 
            this.m_txtFullNameT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.m_txtFullNameT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.m_txtFullNameT.Location = new System.Drawing.Point(118, 75);
            this.m_txtFullNameT.Name = "m_txtFullNameT";
            this.m_txtFullNameT.ReadOnly = true;
            this.m_txtFullNameT.Size = new System.Drawing.Size(317, 26);
            this.m_txtFullNameT.TabIndex = 126;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label8.Location = new System.Drawing.Point(21, 245);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 20);
            this.label8.TabIndex = 125;
            this.label8.Text = "เกิดวันที่";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label7.Location = new System.Drawing.Point(334, 280);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 20);
            this.label7.TabIndex = 124;
            this.label7.Text = "วันบัตรหมดอายุ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.Location = new System.Drawing.Point(21, 146);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 20);
            this.label5.TabIndex = 122;
            this.label5.Text = "ที่อยู่";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(21, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 20);
            this.label4.TabIndex = 121;
            this.label4.Text = "เลขประจำตัวประชาชน";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(21, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 20);
            this.label3.TabIndex = 120;
            this.label3.Text = "ชื่อ - สกุล";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label6.Location = new System.Drawing.Point(21, 280);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 20);
            this.label6.TabIndex = 123;
            this.label6.Text = "วันออกบัตร";
            // 
            // btnReadcard
            // 
            this.btnReadcard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnReadcard.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnReadcard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReadcard.Location = new System.Drawing.Point(121, 409);
            this.btnReadcard.Name = "btnReadcard";
            this.btnReadcard.Size = new System.Drawing.Size(79, 28);
            this.btnReadcard.TabIndex = 119;
            this.btnReadcard.Text = "Read Card";
            this.btnReadcard.UseVisualStyleBackColor = false;
            this.btnReadcard.Click += new System.EventHandler(this.btnReadcard_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.lblStatus);
            this.panel1.Controls.Add(this.lbl_Time);
            this.panel1.Location = new System.Drawing.Point(31, 467);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(561, 25);
            this.panel1.TabIndex = 147;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(-1, -2);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(263, 21);
            this.label10.TabIndex = 1;
            this.label10.Text = "Status: ";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Location = new System.Drawing.Point(129, -2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(424, 25);
            this.panel2.TabIndex = 106;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblStatus.ForeColor = System.Drawing.Color.Black;
            this.lblStatus.Location = new System.Drawing.Point(-1, -2);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(189, 21);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Status: ";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Time
            // 
            this.lbl_Time.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbl_Time.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lbl_Time.Location = new System.Drawing.Point(287, 0);
            this.lbl_Time.Name = "lbl_Time";
            this.lbl_Time.Size = new System.Drawing.Size(263, 21);
            this.lbl_Time.TabIndex = 0;
            this.lbl_Time.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnEKCY
            // 
            this.btnEKCY.BackColor = System.Drawing.Color.Lime;
            this.btnEKCY.Location = new System.Drawing.Point(392, 409);
            this.btnEKCY.Name = "btnEKCY";
            this.btnEKCY.Size = new System.Drawing.Size(90, 28);
            this.btnEKCY.TabIndex = 148;
            this.btnEKCY.Text = "eKCY";
            this.btnEKCY.UseVisualStyleBackColor = false;
            this.btnEKCY.Click += new System.EventHandler(this.btnEKCY_Click);
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(0, 0);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.ScriptErrorsSuppressed = true;
            this.webBrowser.Size = new System.Drawing.Size(632, 504);
            this.webBrowser.TabIndex = 149;
            this.webBrowser.Url = new System.Uri("http://www.google.com", System.UriKind.Absolute);
            // 
            // tm_sensor
            // 
            this.tm_sensor.Tick += new System.EventHandler(this.tm_sensor_Tick);
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(0, 0);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(632, 504);
            this.axWindowsMediaPlayer1.TabIndex = 150;
            this.axWindowsMediaPlayer1.Visible = false;
            this.axWindowsMediaPlayer1.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.axWindowsMediaPlayer1_PlayStateChange);
            this.axWindowsMediaPlayer1.ClickEvent += new AxWMPLib._WMPOCXEvents_ClickEventHandler(this.axWindowsMediaPlayer1_ClickEvent);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(463, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 151;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.UseWaitCursor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormPID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 504);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.axWindowsMediaPlayer1);
            this.Controls.Add(this.webBrowser);
            this.Controls.Add(this.btnEKCY);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btngetReaderID);
            this.Controls.Add(this.m_txtReaderID);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.m_txtIssueplace);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.btnClearScreen);
            this.Controls.Add(this.m_txtIssueNum);
            this.Controls.Add(this.m_txtFullNameE);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_txtGender);
            this.Controls.Add(this.m_picPhoto);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.m_ListReaderCard);
            this.Controls.Add(this.m_txtAddress);
            this.Controls.Add(this.m_txtID);
            this.Controls.Add(this.m_txtBrithDate);
            this.Controls.Add(this.m_txtExpiryDate);
            this.Controls.Add(this.m_txtIssueDate);
            this.Controls.Add(this.m_txtFullNameT);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnReadcard);
            this.Name = "FormPID";
            this.Text = "FormPID";
            this.Load += new System.EventHandler(this.FormPID_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_picPhoto)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btngetReaderID;
        private System.Windows.Forms.TextBox m_txtReaderID;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox m_txtIssueplace;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnClearScreen;
        private System.Windows.Forms.TextBox m_txtIssueNum;
        private System.Windows.Forms.TextBox m_txtFullNameE;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox m_txtGender;
        private System.Windows.Forms.PictureBox m_picPhoto;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox m_ListReaderCard;
        private System.Windows.Forms.TextBox m_txtAddress;
        private System.Windows.Forms.TextBox m_txtID;
        private System.Windows.Forms.TextBox m_txtBrithDate;
        private System.Windows.Forms.TextBox m_txtExpiryDate;
        private System.Windows.Forms.TextBox m_txtIssueDate;
        private System.Windows.Forms.TextBox m_txtFullNameT;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnReadcard;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lbl_Time;
        private System.Windows.Forms.Button btnEKCY;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.Timer tm_sensor;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private System.Windows.Forms.Button button1;
    }
}