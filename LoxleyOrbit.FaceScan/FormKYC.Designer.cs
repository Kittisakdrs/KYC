
namespace LoxleyOrbit.FaceScan
{
    partial class FormKYC
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.FACEDETECTION = new System.Windows.Forms.Button();
            this.CAPTURE = new System.Windows.Forms.Button();
            this.START = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.lb_txt = new System.Windows.Forms.Label();
            this.lb_remark = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.timerClearText = new System.Windows.Forms.Timer(this.components);
            this.TimerClearAudio = new System.Windows.Forms.Timer(this.components);
            this.txt_log = new System.Windows.Forms.ListBox();
            this.result_panel = new System.Windows.Forms.Panel();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.Desc = new System.Windows.Forms.Label();
            this.Head = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.pnl_no_retry = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.loading_box = new System.Windows.Forms.PictureBox();
            this.Overlay_box = new System.Windows.Forms.PictureBox();
            this.Cam_pic = new System.Windows.Forms.PictureBox();
            this.close_result = new System.Windows.Forms.Button();
            this.result_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.pnl_no_retry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loading_box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Overlay_box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cam_pic)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(447, 35);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(363, 21);
            this.comboBox1.TabIndex = 118;
            // 
            // FACEDETECTION
            // 
            this.FACEDETECTION.Location = new System.Drawing.Point(695, -1);
            this.FACEDETECTION.Margin = new System.Windows.Forms.Padding(4);
            this.FACEDETECTION.Name = "FACEDETECTION";
            this.FACEDETECTION.Size = new System.Drawing.Size(144, 28);
            this.FACEDETECTION.TabIndex = 123;
            this.FACEDETECTION.Text = "FACEDETECTION";
            this.FACEDETECTION.UseVisualStyleBackColor = true;
            this.FACEDETECTION.Visible = false;
            this.FACEDETECTION.Click += new System.EventHandler(this.FACEDETECTION_Click);
            // 
            // CAPTURE
            // 
            this.CAPTURE.Location = new System.Drawing.Point(543, -1);
            this.CAPTURE.Margin = new System.Windows.Forms.Padding(4);
            this.CAPTURE.Name = "CAPTURE";
            this.CAPTURE.Size = new System.Drawing.Size(144, 28);
            this.CAPTURE.TabIndex = 122;
            this.CAPTURE.Text = "CAPTURE";
            this.CAPTURE.UseVisualStyleBackColor = true;
            this.CAPTURE.Visible = false;
            this.CAPTURE.Click += new System.EventHandler(this.CAPTURE_Click);
            // 
            // START
            // 
            this.START.Location = new System.Drawing.Point(391, -1);
            this.START.Margin = new System.Windows.Forms.Padding(4);
            this.START.Name = "START";
            this.START.Size = new System.Drawing.Size(144, 28);
            this.START.TabIndex = 121;
            this.START.Text = "START CAMERA";
            this.START.UseVisualStyleBackColor = true;
            this.START.Visible = false;
            this.START.Click += new System.EventHandler(this.START_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1034, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 124;
            this.label1.Text = "label1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1139, -1);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 28);
            this.button1.TabIndex = 125;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lb_txt
            // 
            this.lb_txt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_txt.AutoSize = true;
            this.lb_txt.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.lb_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 48.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_txt.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lb_txt.Location = new System.Drawing.Point(465, 239);
            this.lb_txt.Name = "lb_txt";
            this.lb_txt.Size = new System.Drawing.Size(526, 74);
            this.lb_txt.TabIndex = 128;
            this.lb_txt.Text = "=============";
            this.lb_txt.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lb_remark
            // 
            this.lb_remark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_remark.AutoSize = true;
            this.lb_remark.BackColor = System.Drawing.SystemColors.Highlight;
            this.lb_remark.Font = new System.Drawing.Font("Microsoft Sans Serif", 42.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lb_remark.Location = new System.Drawing.Point(499, 845);
            this.lb_remark.Name = "lb_remark";
            this.lb_remark.Size = new System.Drawing.Size(457, 65);
            this.lb_remark.TabIndex = 129;
            this.lb_remark.Text = "=============";
            this.lb_remark.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(89, 274);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(31, 17);
            this.radioButton1.TabIndex = 134;
            this.radioButton1.Text = "1";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(89, 298);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(46, 17);
            this.radioButton2.TabIndex = 134;
            this.radioButton2.Text = "1.25";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(89, 321);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(40, 17);
            this.radioButton3.TabIndex = 134;
            this.radioButton3.Text = "1.5";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(89, 344);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(46, 17);
            this.radioButton4.TabIndex = 134;
            this.radioButton4.Text = "1.75";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(89, 367);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(31, 17);
            this.radioButton5.TabIndex = 134;
            this.radioButton5.Text = "2";
            this.radioButton5.UseVisualStyleBackColor = true;
            // 
            // timerClearText
            // 
            this.timerClearText.Interval = 3000;
            this.timerClearText.Tick += new System.EventHandler(this.timerClearText_Tick);
            // 
            // TimerClearAudio
            // 
            this.TimerClearAudio.Tick += new System.EventHandler(this.TimerClearAudio_Tick);
            // 
            // txt_log
            // 
            this.txt_log.FormattingEnabled = true;
            this.txt_log.Location = new System.Drawing.Point(15, 456);
            this.txt_log.Name = "txt_log";
            this.txt_log.Size = new System.Drawing.Size(236, 290);
            this.txt_log.TabIndex = 137;
            this.txt_log.Visible = false;
            // 
            // result_panel
            // 
            this.result_panel.BackColor = System.Drawing.Color.White;
            this.result_panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.result_panel.Controls.Add(this.pictureBox4);
            this.result_panel.Controls.Add(this.pictureBox3);
            this.result_panel.Controls.Add(this.pictureBox);
            this.result_panel.Controls.Add(this.Desc);
            this.result_panel.Controls.Add(this.Head);
            this.result_panel.Location = new System.Drawing.Point(391, 128);
            this.result_panel.Name = "result_panel";
            this.result_panel.Size = new System.Drawing.Size(800, 600);
            this.result_panel.TabIndex = 141;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.Image = global::LoxleyOrbit.FaceScan.Properties.Resources.button_retry_21;
            this.pictureBox4.Location = new System.Drawing.Point(15, 469);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(15);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(300, 114);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 10;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Click += new System.EventHandler(this.pictureBox4_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.Image = global::LoxleyOrbit.FaceScan.Properties.Resources.button_no;
            this.pictureBox3.Location = new System.Drawing.Point(483, 469);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(15);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(300, 114);
            this.pictureBox3.TabIndex = 9;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.BackgroundImage = global::LoxleyOrbit.FaceScan.Properties.Resources.Screenshot_2023_05_01_115107;
            this.pictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox.Location = new System.Drawing.Point(195, 147);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(363, 218);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // Desc
            // 
            this.Desc.AutoSize = true;
            this.Desc.Font = new System.Drawing.Font("Angsana New", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Desc.ForeColor = System.Drawing.Color.MediumBlue;
            this.Desc.Location = new System.Drawing.Point(121, 389);
            this.Desc.Name = "Desc";
            this.Desc.Size = new System.Drawing.Size(544, 65);
            this.Desc.TabIndex = 8;
            this.Desc.Text = "ต้องการสแกนใบหน้าใหม่อีกครั้งหรือไม่";
            // 
            // Head
            // 
            this.Head.AutoSize = true;
            this.Head.Font = new System.Drawing.Font("Angsana New", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Head.ForeColor = System.Drawing.Color.Red;
            this.Head.Location = new System.Drawing.Point(37, 11);
            this.Head.Name = "Head";
            this.Head.Size = new System.Drawing.Size(746, 133);
            this.Head.TabIndex = 7;
            this.Head.Text = "คุณไม่ผ่านการยืนยันตัวตน";
            this.Head.UseMnemonic = false;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(44, 128);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 142;
            this.button4.Text = "Retry";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(44, 176);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 143;
            this.button5.Text = "Pass";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(44, 221);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 144;
            this.button6.Text = "Fail";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // pnl_no_retry
            // 
            this.pnl_no_retry.BackColor = System.Drawing.Color.Transparent;
            this.pnl_no_retry.Controls.Add(this.close_result);
            this.pnl_no_retry.Controls.Add(this.pictureBox2);
            this.pnl_no_retry.Controls.Add(this.pictureBox1);
            this.pnl_no_retry.Location = new System.Drawing.Point(1575, 22);
            this.pnl_no_retry.Name = "pnl_no_retry";
            this.pnl_no_retry.Size = new System.Drawing.Size(262, 187);
            this.pnl_no_retry.TabIndex = 145;
            this.pnl_no_retry.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Location = new System.Drawing.Point(145, 35);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(117, 152);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Location = new System.Drawing.Point(0, 35);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(111, 152);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // loading_box
            // 
            this.loading_box.BackColor = System.Drawing.Color.Transparent;
            this.loading_box.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.loading_box.Image = global::LoxleyOrbit.FaceScan.Properties.Resources.Load_Clock;
            this.loading_box.Location = new System.Drawing.Point(1510, 388);
            this.loading_box.Name = "loading_box";
            this.loading_box.Size = new System.Drawing.Size(306, 243);
            this.loading_box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.loading_box.TabIndex = 146;
            this.loading_box.TabStop = false;
            // 
            // Overlay_box
            // 
            this.Overlay_box.BackColor = System.Drawing.Color.Transparent;
            this.Overlay_box.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Overlay_box.Image = global::LoxleyOrbit.FaceScan.Properties.Resources.Head_Shoulder_Black;
            this.Overlay_box.Location = new System.Drawing.Point(1510, 252);
            this.Overlay_box.Name = "Overlay_box";
            this.Overlay_box.Size = new System.Drawing.Size(201, 109);
            this.Overlay_box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Overlay_box.TabIndex = 147;
            this.Overlay_box.TabStop = false;
            // 
            // Cam_pic
            // 
            this.Cam_pic.BackColor = System.Drawing.Color.Transparent;
            this.Cam_pic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Cam_pic.Location = new System.Drawing.Point(1282, 224);
            this.Cam_pic.Name = "Cam_pic";
            this.Cam_pic.Size = new System.Drawing.Size(222, 137);
            this.Cam_pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Cam_pic.TabIndex = 148;
            this.Cam_pic.TabStop = false;
            // 
            // close_result
            // 
            this.close_result.Location = new System.Drawing.Point(3, 3);
            this.close_result.Name = "close_result";
            this.close_result.Size = new System.Drawing.Size(75, 23);
            this.close_result.TabIndex = 2;
            this.close_result.Text = "button2";
            this.close_result.UseVisualStyleBackColor = true;
            this.close_result.Click += new System.EventHandler(this.close_result_Click);
            // 
            // FormKYC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.pnl_no_retry);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.result_panel);
            this.Controls.Add(this.txt_log);
            this.Controls.Add(this.loading_box);
            this.Controls.Add(this.radioButton5);
            this.Controls.Add(this.radioButton4);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.Overlay_box);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CAPTURE);
            this.Controls.Add(this.FACEDETECTION);
            this.Controls.Add(this.lb_remark);
            this.Controls.Add(this.lb_txt);
            this.Controls.Add(this.Cam_pic);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.START);
            this.Controls.Add(this.comboBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormKYC";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormKYC";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormKYC_FormClosing);
            this.Load += new System.EventHandler(this.FormKYC_Load);
            this.result_panel.ResumeLayout(false);
            this.result_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.pnl_no_retry.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loading_box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Overlay_box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Cam_pic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button FACEDETECTION;
        private System.Windows.Forms.Button CAPTURE;
        private System.Windows.Forms.Button START;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox Cam_pic;
        private System.Windows.Forms.Label lb_txt;
        private System.Windows.Forms.Label lb_remark;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox loading_box;
        private System.Windows.Forms.PictureBox Overlay_box;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.Timer timerClearText;
        private System.Windows.Forms.Timer TimerClearAudio;
        private System.Windows.Forms.ListBox txt_log;
        private System.Windows.Forms.Panel result_panel;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label Desc;
        private System.Windows.Forms.Label Head;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Panel pnl_no_retry;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Button close_result;
    }
}