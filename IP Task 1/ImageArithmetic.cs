using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ImageOperationsPackage
{
    public static class ImageArithmetic
    {

        public static Bitmap ResizeImage(Bitmap imgToResize, Size size)
        {
            Bitmap b = new Bitmap(size.Width, size.Height);
            try
            {
                using (Graphics g = Graphics.FromImage((Image)b))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(imgToResize, 0, 0, size.Width, size.Height);
                }
                return b;
            }
            catch { }

            return b;
        }

        public static Bitmap Add(Bitmap Img1, Bitmap Img2, double fraction)
        {
            Img2 = ResizeImage(Img2, new Size(Img1.Width, Img1.Height));
            if (Img1.PixelFormat != System.Drawing.Imaging.PixelFormat.Format24bppRgb && Img1.PixelFormat != System.Drawing.Imaging.PixelFormat.Format32bppArgb)
                Img1 = toGray(Img1);
            if (Img2.PixelFormat != System.Drawing.Imaging.PixelFormat.Format24bppRgb && Img2.PixelFormat != System.Drawing.Imaging.PixelFormat.Format32bppArgb)
                Img2 = toGray(Img2);

            for (int col = 0; col < Img1.Width; col++)
                for (int row = 0; row < Img1.Height; row++)
                {
                    Color curCol1 = Img1.GetPixel(col, row);
                    Color curCol2 = Img2.GetPixel(col, row);
                    int newR = (int)((double)curCol1.R * fraction + (double)curCol2.R * (1.0 - fraction));
                    int newG = (int)((double)curCol1.G * fraction + (double)curCol2.G * (1.0 - fraction));
                    int newB = (int)((double)curCol1.B * fraction + (double)curCol2.B * (1.0 - fraction));
                    newR = Math.Min(newR, 255);
                    newR = Math.Max(newR, 0);
                    newG = Math.Min(newG, 255);
                    newG = Math.Max(newG, 0);
                    newB = Math.Min(newB, 255);
                    newB = Math.Max(newB, 0);
                    Img1.SetPixel(col, row, Color.FromArgb(newR, newG, newB));
                }
            return Img1;
        }

        public static Bitmap toGray(Bitmap B)
        {
            Bitmap ret = new Bitmap(B.Width, B.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            for (int col = 0; col < B.Width; col++)
                for (int row = 0; row < B.Height; row++)
                {
                    Color curColor = B.GetPixel(col, row);
                    int gray = (curColor.R + curColor.G + curColor.B) / 3;
                    ret.SetPixel(col, row, Color.FromArgb(gray, gray, gray));
                }
            return ret;
        }

        public static Bitmap Subtract(Bitmap Img1, Bitmap Img2)
        {
            Img2 = ResizeImage(Img2, new Size(Img1.Width, Img1.Height));
            if (Img1.PixelFormat != System.Drawing.Imaging.PixelFormat.Format24bppRgb && Img1.PixelFormat != System.Drawing.Imaging.PixelFormat.Format32bppArgb)
            Img1 = toGray(Img1);
            if (Img2.PixelFormat != System.Drawing.Imaging.PixelFormat.Format24bppRgb && Img2.PixelFormat != System.Drawing.Imaging.PixelFormat.Format32bppArgb)
            Img2 = toGray(Img2);

            int maxR = (int)(-1e9);
            int maxG = (int)(-1e9);
            int maxB = (int)(-1e9);
            int minR = (int)(1e9);
            int minG = (int)(1e9);
            int minB = (int)(1e9);
            for (int col = 0; col < Img1.Width; col++)
                for (int row = 0; row < Img1.Height; row++)
                {
                    Color curCol1 = Img1.GetPixel(col, row);
                    Color curCol2 = Img2.GetPixel(col, row);
                    int newR = curCol1.R - curCol2.R;
                    int newG = curCol1.G - curCol2.G;
                    int newB = curCol1.B - curCol2.B;
                    maxR = Math.Max(maxR, newR);
                    minR = Math.Min(minR, newR);
                    maxG = Math.Max(maxG, newG);
                    minG = Math.Min(minG, newG);
                    maxB = Math.Max(maxB, newB);
                    minB = Math.Min(minB, newB);
                }

            for (int col = 0; col < Img1.Width; col++)
                for (int row = 0; row < Img1.Height; row++)
                {
                    Color curCol1 = Img1.GetPixel(col, row);
                    Color curCol2 = Img2.GetPixel(col, row);
                    int newR = (int)((((double)((curCol1.R - curCol2.R) - minR) / (double)(maxR - minR)) * (double)255.0));
                    int newG = (int)((((double)((curCol1.G - curCol2.G) - minG) / (double)(maxG - minG)) * (double)255.0));
                    int newB = (int)((((double)((curCol1.B - curCol2.B) - minB) / (double)(maxB - minB)) * (double)255.0));
                    newR = Math.Min(newR, 255);
                    newR = Math.Max(newR, 0);
                    newG = Math.Min(newG, 255);
                    newG = Math.Max(newG, 0);
                    newB = Math.Min(newB, 255);
                    newB = Math.Max(newB, 0);
                    Img1.SetPixel(col, row, Color.FromArgb(newR, newG, newB));
                }

            return Img1;
        }


        public static Bitmap NotImage(Bitmap Img1)
        {
            if (Img1.PixelFormat != System.Drawing.Imaging.PixelFormat.Format24bppRgb && Img1.PixelFormat != System.Drawing.Imaging.PixelFormat.Format32bppArgb)
                Img1 = toGray(Img1);

            for (int col = 0; col < Img1.Width; col++)
                for (int row = 0; row < Img1.Height; row++)
                {
                    Color curCol1 = Img1.GetPixel(col, row);
                    int newR = 255 - curCol1.R;
                    int newG = 255 - curCol1.G;
                    int newB = 255 - curCol1.B;
                    Img1.SetPixel(col, row, Color.FromArgb(newR, newG, newB));
                }
            return Img1;
        }

        public static Bitmap BitplaneSlicing(Bitmap Img1, int bit)
        {
            if (Img1.PixelFormat != System.Drawing.Imaging.PixelFormat.Format24bppRgb && Img1.PixelFormat != System.Drawing.Imaging.PixelFormat.Format32bppArgb)
                Img1 = toGray(Img1);

            int mask = 1 << bit;
            for (int col = 0; col < Img1.Width; col++)
                for (int row = 0; row < Img1.Height; row++)
                {
                    Color curCol1 = Img1.GetPixel(col, row);
                    int gray = (curCol1.R + curCol1.G + curCol1.B) / 3;
                    int color = ((gray & mask) == 0) ? 0 : 255;
                    Img1.SetPixel(col, row, Color.FromArgb(color, color, color));
                }
            return Img1;
        }
    }
}
