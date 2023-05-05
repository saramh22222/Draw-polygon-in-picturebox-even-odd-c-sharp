using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private List<List<PointF>> polygons = new List<List<PointF>>();
        private List<PointF> newPolygon = null;
        private PointF newPoint;
        private bool isDrawing = false;
        private Color fillColor = Color.FromArgb(128, Color.Blue);

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDrawing = true;
                if (newPolygon == null)
                {
                    newPolygon = new List<PointF>();
                }
                newPolygon.Add(e.Location);
            }
            else if (e.Button == MouseButtons.Right)
            {
                isDrawing = false;
                if (newPolygon != null && newPolygon.Count > 2)
                {
                    polygons.Add(newPolygon);
                    newPolygon = null;
                    pictureBox1.Invalidate();
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                newPoint = e.Location;
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            foreach (List<PointF> polygon in polygons)
            {
                e.Graphics.FillPolygon(new SolidBrush(fillColor), polygon.ToArray(), FillMode.Alternate);
                e.Graphics.DrawPolygon(Pens.Blue, polygon.ToArray());
            }

            if (newPolygon != null)
            {
                if (newPolygon.Count > 1)
                {
                    e.Graphics.DrawLines(Pens.Green, newPolygon.ToArray());
                }

                if (newPolygon.Count > 2)
                {
                    using (Pen dashedPen = new Pen(Color.Green))
                    {
                        dashedPen.DashPattern = new float[] { 3, 3 };
                        e.Graphics.DrawLine(dashedPen, newPolygon[newPolygon.Count - 1], newPoint);
                    }
                }
            }
        }
    }
}
