using System.Drawing;

namespace mandelbrot_MultiCore
{
    class MarkedBitmap
    {
        public Bitmap Bitmap;
        public int BitmapID;

        public MarkedBitmap(Bitmap Bitmap, int BitmapID)
        {
            this.Bitmap = Bitmap;
            this.BitmapID = BitmapID;
        }

        public MarkedBitmap()
        {
            this.Bitmap = new Bitmap(1, 1);
            this.BitmapID = 0;
        }
    }
}
