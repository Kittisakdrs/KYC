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

        private DateTime dateTimeStart = DateTime.Now;
        private double totalMilliseconds = 0;
        private int lasttimeOut = 10000;

        public FaceResult()
        {
            InitializeComponent();
        }

        private void FaceResult_Activated(object sender, EventArgs e)
        {
            TimeSpan timedddd = DateTime.Now - dateTimeStart;
            totalMilliseconds = timedddd.TotalMilliseconds;
            timer1.Start();
        }

        private void FaceResult_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text += totalMilliseconds / 10 + "วินาที";
            try{
                if (totalMilliseconds >= lasttimeOut)
                {
                    this.Close();
                    timer1.Stop();
                }
            }
            catch (Exception ex){

                timer1.Stop();
            }
        }
    }
}
