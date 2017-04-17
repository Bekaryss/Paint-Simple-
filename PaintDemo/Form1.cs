using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaintDemo
{
    public partial class Form1 : Form
    {
        public bool FirstStart = false;
        bool mouseDown;
        Bitmap snapshot;
        Bitmap tempDraw;
        int lineWidth = 2;
        int x1;
        int y1;
        int initX;
        int initY;
        Color foreColor = Color.Black;
        enum PType
        {
            pen,
            rectangle,
            line,
            circle,
            triangle,
            eraser
        }
        PType pt = PType.pen;
        public Form1()
        {
            InitializeComponent();
            snapshot = new Bitmap(pnl_Draw.ClientRectangle.Width, this.ClientRectangle.Height);
            numWidth.Value = 2;
        }  

        private void pnl_Draw_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            x1 = e.X;
            y1 = e.Y;
            tempDraw = (Bitmap)snapshot.Clone();
        }

        private void pnl_Draw_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
            snapshot = (Bitmap)tempDraw.Clone();
        }

        private void pnl_Draw_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                initX = e.X;
                initY = e.Y;
                pnl_Draw.Invalidate();
                pnl_Draw.Update();
            }
        }


        private void btn_Pen_Click(object sender, EventArgs e)
        {
            pt = PType.pen;
        }

        private void btn_Rectangle_Click(object sender, EventArgs e)
        {
            pt = PType.rectangle;
        }

        private void btn_Line_Click(object sender, EventArgs e)
        {
            pt = PType.line;
        }
        private void btn_circle_Click(object sender, EventArgs e)
        {
            pt = PType.circle;
        }
        private void btn_triangle_Click_1(object sender, EventArgs e)
        {
            pt = PType.triangle;
        }
        private void btn_eraser_Click(object sender, EventArgs e)
        {
            pt = PType.eraser;
        }
        private void colorPickerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                foreColor = colorDialog1.Color;
            }
        }

        private void pnl_Draw_Paint(object sender, PaintEventArgs e)
        {
            if (tempDraw != null)
            {             
                if (pt == PType.pen)
                {
                    Graphics g = Graphics.FromImage(tempDraw);
                    Pen p = new Pen(foreColor, lineWidth);
                    p.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                    p.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                    g.DrawLine(p, x1, y1, initX, initY);
                    p.Dispose();
                    e.Graphics.DrawImageUnscaled(tempDraw, 0, 0);
                    g.Dispose();
                    x1 = initX;
                    y1 = initY;
                }
                else if (pt == PType.eraser)
                {
                    Graphics g = Graphics.FromImage(tempDraw);
                    Pen p = new Pen(Color.White, lineWidth);
                    p.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                    p.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                    g.DrawLine(p, x1, y1, initX, initY);
                    p.Dispose();
                    e.Graphics.DrawImageUnscaled(tempDraw, 0, 0);
                    g.Dispose();
                    x1 = initX;
                    y1 = initY;
                }
                else if (pt == PType.line)
                {
                    tempDraw = (Bitmap)snapshot.Clone();
                    Graphics g = Graphics.FromImage(tempDraw);
                    Pen p = new Pen(foreColor, lineWidth);
                    p.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                    p.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                    g.DrawLine(p, x1, y1, (int)initX, (int)initY);
                    p.Dispose();
                    e.Graphics.DrawImageUnscaled(tempDraw, 0, 0);
                    g.Dispose();
                }
                else if (pt == PType.rectangle)
                {
                    tempDraw = (Bitmap)snapshot.Clone();
                    Graphics g = Graphics.FromImage(tempDraw);
                    Pen p = new Pen(foreColor, lineWidth);
                    p.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                    p.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                    g.DrawRectangle(p, Math.Min(x1, initX), Math.Min(y1, initY), Math.Abs(x1 - initX), Math.Abs(y1 - initY));                    
                    p.Dispose();
                    e.Graphics.DrawImageUnscaled(tempDraw, 0, 0);
                    g.Dispose();
                }
                else if (pt == PType.circle)
                {
                    tempDraw = (Bitmap)snapshot.Clone();
                    Graphics g = Graphics.FromImage(tempDraw);
                    Pen p = new Pen(foreColor, lineWidth);
                    p.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                    p.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                    g.DrawEllipse(p, Math.Min(x1, initX), Math.Min(y1, initY), Math.Abs(x1 - initX), Math.Abs(y1 - initY));
                    p.Dispose();
                    e.Graphics.DrawImageUnscaled(tempDraw, 0, 0);
                    g.Dispose();
                }
                else if (pt == PType.triangle)
                {
                    tempDraw = (Bitmap)snapshot.Clone();
                    Graphics g = Graphics.FromImage(tempDraw);
                    Pen p = new Pen(foreColor, lineWidth);
                    p.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                    p.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                    int tpX = (initX + x1) / 2;
                    int tpY = initY;
                    g.DrawLine(p, x1, y1, tpX, tpY);
                    g.DrawLine(p, initX, y1, tpX, tpY);
                    g.DrawLine(p, x1, y1, initX, y1);
                    p.Dispose();
                    e.Graphics.DrawImageUnscaled(tempDraw, 0, 0);
                    g.Dispose();
                }
            }
        }

        private void numWidth_ValueChanged(object sender, EventArgs e)
        {
            lineWidth = (int)numWidth.Value;
        }

    }
}
