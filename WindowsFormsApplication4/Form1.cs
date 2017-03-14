using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{

    public partial class Form1 : Form
    {
        string[,] a;
        string[] b;
        int TurnCount = 0;
        Graphics gr;
        Pen p;
        Draw_Krest dr;
        bool[] prov;
        bool DrawFlag;
        public Form1()
        {
            InitializeComponent();
            gr = this.CreateGraphics();
            dr = new Draw_Krest();
            a = new string[3, 3];
            b = new string[8];
            prov = new bool[9];
            DrawFlag = new bool();
            p = new Pen(Color.Red, 5);
            this.ClearAll();
        }
        public void ClearAll()
        {
            TurnCount = 0;
            for (int z = 0; z < 3; z++)
                for (int j = 0; j < 3; j++)
                {
                    a[z, j] = "";
                }
            Array.Clear(b, 0, 8);
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            gr.Dispose();
            gr = this.CreateGraphics();
            Pen mp = new Pen(Color.Green, 5);
            Pen p = new Pen(Color.Red, 5);
            gr.Clear(Color.Wheat);
            gr.DrawLine(mp, this.ClientRectangle.Width / 3, 0, this.ClientRectangle.Width / 3, this.ClientRectangle.Height);
            gr.DrawLine(mp, (this.ClientRectangle.Width / 3) * 2, 0, (this.ClientRectangle.Width / 3) * 2, this.ClientRectangle.Height);
            gr.DrawLine(mp, 0, this.ClientRectangle.Height / 3, this.ClientRectangle.Width, this.ClientRectangle.Height / 3);
            gr.DrawLine(mp, 0, (this.ClientRectangle.Height / 3) * 2, this.ClientRectangle.Width, (this.ClientRectangle.Height / 3) * 2);
            //this.ClearAll();\
            for (int z = 0; z < 3; z++)
                for (int j = 0; j < 3; j++)
                {
                    if (a[z, j] == "X")
                        dr.DrawKrest(p, (this.ClientRectangle.Width / 3) * j, (this.ClientRectangle.Height / 3) * z, this.ClientRectangle.Width / 3, this.ClientRectangle.Height / 3, gr);
                    if (a[z, j] == "O")
                        gr.DrawEllipse(p, (this.ClientRectangle.Width / 3) * j, (this.ClientRectangle.Height / 3) * z, this.ClientRectangle.Width / 3, this.ClientRectangle.Height / 3);
                }
        }
        private void Form1_ClientSizeChanged(object sender, EventArgs e)
        {
            Form1_Paint(sender, new PaintEventArgs(gr, this.ClientRectangle));
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {

            for (int z = 0; z < 3; z++)
                for (int j = 0; j < 3; j++)
                {
                    if (e.X > (this.ClientRectangle.Width / 3) * j && e.X < (this.ClientRectangle.Width / 3) * (j + 1) && e.Y > (this.ClientRectangle.Width / 3) * z && e.Y < (this.ClientRectangle.Width / 3) * (z + 1) && a[z, j] == "")
                    {
                        if (TurnCount % 2 == 0)
                        {
                            a[z, j] = "X";
                        }
                        else
                        {
                            a[z, j] = "O";
                        }
                        TurnCount++;
                    }
                }
            Form1_Paint(sender, new PaintEventArgs(gr, this.ClientRectangle));

            b[0] = a[0, 0] + a[0, 1] + a[0, 2];
            b[1] = a[1, 0] + a[1, 1] + a[1, 2];
            b[2] = a[2, 0] + a[2, 1] + a[2, 2];
            //---------------
            b[3] = a[0, 0] + a[1, 0] + a[2, 0];
            b[4] = a[0, 1] + a[1, 1] + a[2, 1];
            b[5] = a[0, 2] + a[1, 2] + a[2, 2];
            //---------------
            b[6] = a[0, 0] + a[1, 1] + a[2, 2];
            b[7] = a[0, 2] + a[1, 1] + a[2, 0];
            DrawFlag = true;
            for (int x = 0; x < 8; x++)
            {
                if (b[x] == "XXX" || b[x] == "OOO")
                {
                    MessageBox.Show("The Winner is = " + b[x][0]);
                    this.ClearAll();
                    Form1_Paint(sender, new PaintEventArgs(gr, this.ClientRectangle));
                    DrawFlag = false;
                }
            }
            if (TurnCount == 9)
            {
                if (DrawFlag)
                {
                    this.ClearAll();
                    MessageBox.Show("Draw!");
                    Form1_Paint(sender, new PaintEventArgs(gr, this.ClientRectangle));
                }
            }
        }
    }
    public class Draw_Krest
    {
        public void DrawKrest(Pen p, int x1, int y1, int width, int heigth, Graphics gr)
        {
            gr.DrawLine(p, x1, y1, x1 + width, y1 + heigth);
            gr.DrawLine(p, x1, y1 + heigth, x1 + width, y1);
        }
    }
}
