using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        private Random rand = new Random();
        private List<Point> raindrops = new List<Point>();
        private Bitmap bit = new Bitmap("rain.png");

        public Form1()
        {
            InitializeComponent();
            bit.MakeTransparent();
            timer1.Start();
            timer1.Interval = 500;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < 20; i++)
            {
                int x = rand.Next(ClientSize.Width);
                int y = 0;
                raindrops.Add(new Point(x, y));
                raindrops[i] = new Point(raindrops[i].X, raindrops[i].Y + rand.Next(5, 100));  
                if (raindrops[i].Y > ClientSize.Height)
                {
                    raindrops[i] = new Point(rand.Next(ClientSize.Width), 0); 
                }

                e.Graphics.DrawImage(bit, raindrops[i]);
            }
        }
    }
}
