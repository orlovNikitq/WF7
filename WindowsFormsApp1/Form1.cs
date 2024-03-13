using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Bitmap sky, plane, cloud;
        Graphics g;
        int dx, dxCloud;
        Rectangle rct, rctCloud;
        Random rnd;
        Boolean demo = true;
        float cloudOpacity = 1.0f;
        private void timer1_Tick(object sender, EventArgs e)
        {
            g.DrawImage(sky, new Point(0, 0));

            if (rct.X < ClientRectangle.Width)
            {
                rct.X += dx;
            }
            else
            {
                rct.X = -40;
                rct.Y = 20 + rnd.Next(ClientSize.Height - 40 - plane.Height);
                dx = 2 + rnd.Next(5);
            }
            if (rctCloud.X > -120)
            {
                rctCloud.X -= dxCloud;
            }
            else
            {
                rctCloud.X = ClientSize.Width - 40;
                rctCloud.Y = rct.Y;
                dxCloud = 2 + rnd.Next(5);
            }

            GraphicsPath planePath = new GraphicsPath();
            planePath.AddRectangle(new Rectangle(rct.X, rct.Y, plane.Width, plane.Height));
            if (planePath.IsVisible(rctCloud.X, rctCloud.Y))
            {
                float transparency = 0.5f;
                ColorMatrix colorMatrix = new ColorMatrix();
                colorMatrix.Matrix33 = transparency;
                ImageAttributes imageAttributes = new ImageAttributes();
                imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                
                g.DrawImage(cloud, new Rectangle(rctCloud.X, rctCloud.Y, cloud.Width/ 5 - 20, cloud.Height /5 - 20),
                    0, 0, cloud.Width, cloud.Height, GraphicsUnit.Pixel, imageAttributes);
            }
            else
            {
                g.DrawImage(cloud, rctCloud.X, rctCloud.Y);
            }
          
            g.DrawImage(plane, rct.X, rct.Y);

            if (!demo) this.Invalidate(rct);
            else
            {
                Rectangle reg = new Rectangle(20, 20, sky.Width - 40, sky.Height - 40);
                g.DrawRectangle(Pens.Black, reg.X, reg.Y, reg.Width - 1, reg.Height - 1);
                Invalidate(reg);
            }
        }



    public Form1()
        {
            InitializeComponent();
            rnd = new Random();
            try
            {
                sky = new Bitmap("sky.bmp");
                plane = new Bitmap("plane2.bmp");
                cloud = new Bitmap("cloud.png"); 
                BackgroundImage = new Bitmap("sky.bmp");
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Ошибка загрузка файлов", "Полет в облаках",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

            plane.MakeTransparent();
            cloud.MakeTransparent();
            ClientSize = new System.Drawing.Size(new Point(BackgroundImage.Width, BackgroundImage.Height));
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            g = Graphics.FromImage(BackgroundImage);
            rct.X = -40;
            rct.Y = 20 + rnd.Next(20);
            rct.Width = plane.Width;
            rct.Height = plane.Height;
            dx = 5;

            rctCloud.X = ClientRectangle.Width;
            rctCloud.Y = rct.Y;
            rctCloud.Width = cloud.Width;
            rctCloud.Height = cloud.Height;
            dxCloud = 5;

            timer1.Interval = 20;
            timer1.Enabled = true;
        }
    }
}
