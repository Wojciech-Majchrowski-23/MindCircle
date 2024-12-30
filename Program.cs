using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using MessageBox = System.Windows.MessageBox;

namespace CircleMoverWPF
{

    public class CircleMover : Form
    {

        private int circleX;
        private int circleY;
        private int circleDiameter = 50;
        private ThingGearController _thingGearController;

        public CircleMover()
        {
            this.Text = "Poruszanie czerwonym kołem";
            this.Size = new System.Drawing.Size(400, 400);
            this.DoubleBuffered = true;

            circleX = (this.ClientSize.Width - circleDiameter) / 2;
            circleY = (this.ClientSize.Height - circleDiameter) / 2;


            this.KeyDown += new KeyEventHandler(OnKeyDown);
            this.Paint += new PaintEventHandler(OnPaint);
        }
        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.Black);

            // Rysowanie czerwonego koła
            using (Brush brush = new SolidBrush(Color.Red))
            {
                g.FillEllipse(brush, circleX, circleY, circleDiameter, circleDiameter);
            }
        }


        //Here instead of moving with arrowKeys
        //use dependencies between currentData.meditation and latestData.meditaion
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            const int step = 10; // Ilość pikseli do przesunięcia

            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (circleY - step >= 0) // Sprawdzamy, czy nie wyjdziemy poza górną krawędź
                    {
                        circleY -= step;
                    }
                    break;

                case Keys.Down:
                    if (circleY + circleDiameter + step <= this.ClientSize.Height) // Sprawdzamy, czy nie wyjdziemy poza dolną krawędź
                    {
                        circleY += step;
                    }
                    break;
            }

            // Odświeżenie okna, aby zaktualizować pozycję koła
            this.Invalidate();
        }

        [STAThread]

        //here put refresh methods from MindWaveReader
        public static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.Run(new CircleMover());
        }
    }
}

