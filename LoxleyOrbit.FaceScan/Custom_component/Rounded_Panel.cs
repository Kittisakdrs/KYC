using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoxleyOrbit.FaceScan.Custom_component
{
    class Rounded_Panel : Panel
    {
        private int _cornerRadius = 20;

        public int Radius
        {
            get { return _cornerRadius; }
            set { _cornerRadius = value; Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Create a graphics path to draw the rounded rectangle
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, _cornerRadius * 2, _cornerRadius * 2, 180, 90);
            path.AddArc(Width - _cornerRadius * 2, 0, _cornerRadius * 2, _cornerRadius * 2, 270, 90);
            path.AddArc(Width - _cornerRadius * 2, Height - _cornerRadius * 2, _cornerRadius * 2, _cornerRadius * 2, 0, 90);
            path.AddArc(0, Height - _cornerRadius * 2, _cornerRadius * 2, _cornerRadius * 2, 90, 90);
            path.CloseFigure();

            // Set the region of the panel to the graphics path
            Region = new Region(path);

            // Draw the background of the panel
            e.Graphics.FillPath(new SolidBrush(BackColor), path);
        }
    }
}
