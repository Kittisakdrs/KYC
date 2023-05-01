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
    public partial class FaceResult : Form
    {

        public string State = "";
        public string which_button = "none";

        public FaceResult()
        {
            InitializeComponent();
        }

        private void FaceResult_Activated(object sender, EventArgs e)
        {
            if (State == "1st_try")
            {
                button1.Text = "สแกนซ้ำ";
                button2.Text = "ยกเลิกการทำรายการ";

                label1.Text = "คุณไม่ผ่านการยืนยันตัวตน";
                label2.Text = "ต้องการสแกนใบหน้าใหม่อีกครั้งหรือไม่";

                //int x = this.Width - label1.Width;
                //int y = this.Height - label1.Height;
                //label1.Location = new Point() {X=x ,Y=y};
            }
            else if (State == "exit")
            {

            }
            else
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            which_button = "button1";
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            which_button = "button2";
            // raise the ButtonClicked event

        }
    }
}
