using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace assignment1
{
    public partial class Form1 : Form
    {
        Bitmap off;
        Bitmap img = new Bitmap("1.jpg");
        Bitmap img1 = new Bitmap("kayn.png");
        PointF img11 = new PointF(10, 10);
        int x1 = 50;
        int y1 = 50;
        int x2, y2;
        int f = 0;



        int flagstart = 0;
        DDALine L1;

        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.Paint += Form1_Paint;
            this.Load += Form1_Load;
            this.MouseDown += Form1_MouseDown;
            img1.MakeTransparent();

            Timer t = new Timer();
            t.Start();
            t.Tick += T_Tick;

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (f == 0)
            {
                x2 = e.X;
                y2 = e.Y;
                img11.X = x1;
                img11.Y = y1;
                L1 = new DDALine(x1, y1, x2, y2);
                flagstart = 1;
                f = 1;

            }
            else
            {
                if (f > 0)
                {

                    img11.X = x2;
                    img11.Y = y2;

                    x1 = e.X;
                    y1 = e.Y;
                    L1 = new DDALine(x2, y2, x1, y1);
                    f = 0;

                }

            }


        }

        private void T_Tick(object sender, EventArgs e)
        {
            if (flagstart == 1)
            {

                img11 = L1.getnextpoint(img11.X, img11.Y);
                if (L1.flagstop == true)
                {

                    img11.X = L1.xe;
                    img11.Y = L1.ye;

                }

            }
            Dubblebuffer(this.CreateGraphics());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Dubblebuffer(e.Graphics);
        }
        void Dubblebuffer(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            Drawscene(g2);
            g.DrawImage(off, 0, 0);
        }
        void Drawscene(Graphics g)
        {
            //g.Clear(col);
            g.DrawImage(img, 0, 0, this.Width, this.Height);

            if (flagstart == 1)
            {
                g.DrawImage(img1, img11.X - 15, img11.Y - 15, 100, 100);
            }

        }
    }
}


