using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace mandelbrot_MultiCore
{
    class Program
    {
        const int Res = 1000;
        const int Edg = 2;
        const string fileName = "dupa.png";
        const int MandelPower = 2;


        static void Main(string[] args)
        {
           


        }

        private static void DetectHosts()
        {
            Hosty.Add(new ClusterHost("TEST", "192.168.1.22", 5));
        }

        private static void RunCluster()
        {
            DateTime StartDate = DateTime.Now;
            ComplexNumber UL = new ComplexNumber(-2, 1.5);
            ComplexNumber LR = new ComplexNumber(1, -1.5);
            int hpx = 10000; //Pixels height
            int wpx = 10000; //Pixels width
            int th = Environment.ProcessorCount; //threads
            int sc = 20 * th; //sections
            Bitmap BMPFile = GetMultiMaps(UL, LR, hpx, wpx, th, sc);
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            BMPFile.Save(fileName, ImageFormat.Png);
            TimeSpan ProcessTime = DateTime.Now - StartDate;
            Console.WriteLine("Done! Took[sec]: " + ProcessTime.TotalSeconds);
        }

        private static Bitmap GetMultiMaps(ComplexNumber uL, ComplexNumber lR, int hpx, int wpx, int th, int sc)
        {
            List<MarkedBitmap> BMPList = new List<MarkedBitmap>();
            List<ComplexPointer> Pointers = new List<ComplexPointer>();
            for (int i = 0; i < sc; i++)
            {
                ComplexPointer CP = new ComplexPointer();
                CP.Ul.Real = uL.Real;
                CP.Ul.Imag = uL.Imag - i * (Math.Abs(uL.Imag - lR.Imag)/sc);
                CP.Lr.Real = lR.Real;
                CP.Lr.Imag = uL.Imag - (i + 1) * (Math.Abs(uL.Imag - lR.Imag) / sc);
                CP.PointerID = i;
                Pointers.Add(CP);
            }

            Parallel.ForEach(
                Pointers,
                new ParallelOptions { MaxDegreeOfParallelism = th },
                Pointer => {
                    BMPList.Add(new MarkedBitmap(GetMandelBitmapBlock(Pointer.Ul, Pointer.Lr, hpx/sc, wpx),Pointer.PointerID));
                    }
            );

            return MergeBitmaps(BMPList);
        }

        private static Bitmap MergeBitmaps(List<MarkedBitmap> bMPList)
        {

            bMPList.Sort((x, y) => x.BitmapID.CompareTo(y.BitmapID));
            int width = bMPList[0].Bitmap.Width;
            int heigth = bMPList.Sum(a => a.Bitmap.Height);
            Bitmap finalImage = new Bitmap(width, heigth);
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(finalImage))
            {
                g.Clear(System.Drawing.Color.Black);
                int offset = 0;
                foreach (MarkedBitmap image in bMPList)
                {
                    g.DrawImage(image.Bitmap, new Rectangle(0, offset, image.Bitmap.Width, image.Bitmap.Height));
                    offset += image.Bitmap.Height;
                }
            }

            return finalImage;

        }

        private static Bitmap GetMandelBitmapBlock(ComplexNumber uL, ComplexNumber lR, int hpx, int wpx)
        {
            double Width = Math.Abs(uL.Real - lR.Real);
            double Wincrement = Width / wpx;
            double Height = Math.Abs(uL.Imag - lR.Imag);
            double Hincrement = Height / hpx;
            Bitmap bmp = new Bitmap(wpx, hpx);

            for (int i = 0; i < hpx; i++)
            {
                for (int ii = 0; ii < wpx; ii++)
                {
                    double hPos = uL.Real + ii * Wincrement;
                    double vPos = uL.Imag - i * Hincrement;
                    bmp.SetPixel(ii, i, Manderbrot(new ComplexNumber(hPos,vPos)));
                }
            }


            return bmp;
        }

        static System.Drawing.Color Manderbrot(ComplexNumber pos)
        {
            ComplexNumber Edge = new ComplexNumber(Edg, 0);
            ComplexNumber tmp = new ComplexNumber();
            for (int i = 0; i < Res; i++)
            {
                tmp = tmp.Power(MandelPower) + pos;
                if (tmp > Edge)
                {
                    //byte[] values = BitConverter.GetBytes(i);
                    //if (!BitConverter.IsLittleEndian) Array.Reverse(values);
                    int colorval =  i % 255 ;
                    Color myColor = Color.FromArgb(colorval, 0, 0);
                    return myColor;
                }
            }
            return Color.Black;
        }
    }
}
