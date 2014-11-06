using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;
using IP_Task_1;

namespace ImageOperationsPackage
{
    class ImageHistogram
    {
        Bitmap Img;
        public int[] grayHist;
        public int[] greenHist;
        public int[] blueHist;
        public int[] redHist;


        public ImageHistogram(Bitmap Img)
        {
            this.Img = Img;
            grayHist = new int[256];
            greenHist = new int[256];
            blueHist = new int[256];
            redHist = new int[256];
        }

        public void calcHist()
        {
            for (int i = 0; i < 256; i++)
                grayHist[i] = redHist[i] = greenHist[i] = blueHist[i] = 0;

            for (int col = 0; col < Img.Width; col++)
                for (int row = 0; row < Img.Height; row++)
                {
                    Color curCol = Img.GetPixel(col, row);
                    redHist[curCol.R]++;
                    greenHist[curCol.G]++;
                    blueHist[curCol.B]++;
                    grayHist[(curCol.R + curCol.G + curCol.B) / 3]++;
                }
        }

        public void changeBrightness(int change)
        {

            for (int col = 0; col < Img.Width; col++)
                for (int row = 0; row < Img.Height; row++)
                {
                    Color curCol = Img.GetPixel(col, row);
                    int newR = curCol.R + change;
                    newR = Math.Min(newR, 255);
                    newR = Math.Max(newR, 0);
                    int newG = curCol.G + change;
                    newG = Math.Min(newG, 255);
                    newG = Math.Max(newG, 0);
                    int newB = curCol.R + change;
                    newB = Math.Min(newB, 255);
                    newB = Math.Max(newB, 0);
                    Img.SetPixel(col, row, Color.FromArgb(newR, newG, newB));
                }
            calcHist();
        }

        public void changeContrast(int newMin, int newMax)
        {
            int maxR = 0;
            int maxG = 0;
            int maxB = 0;
            int minR = 255;
            int minG = 255;
            int minB = 255;
            for (int col = 0; col < Img.Width; col++)
                for (int row = 0; row < Img.Height; row++)
                {
                    Color curCol = Img.GetPixel(col, row);
                    maxR = Math.Max(maxR, curCol.R);
                    minR = Math.Min(minR, curCol.R);
                    maxG = Math.Max(maxG, curCol.G);
                    minG = Math.Min(minG, curCol.G);
                    maxB = Math.Max(maxB, curCol.B);
                    minB = Math.Min(minB, curCol.B);
                }
            for (int col = 0; col < Img.Width; col++)
                for (int row = 0; row < Img.Height; row++)
                {
                    Color curCol = Img.GetPixel(col, row);
                    int newR = (int)((((double)(curCol.R - minR) / (double)(maxR - minR)) * (double)newMax) + newMin);
                    int newG = (int)((((double)(curCol.G - minG) / (double)(maxG - minG)) * (double)newMax) + newMin);
                    int newB = (int)((((double)(curCol.B - minB) / (double)(maxB - minB)) * (double)newMax) + newMin);
                    newR = Math.Min(newR, 255);
                    newR = Math.Max(newR, 0);
                    newG = Math.Min(newG, 255);
                    newG = Math.Max(newG, 0);
                    newB = Math.Min(newB, 255);
                    newB = Math.Max(newB, 0);
                    Img.SetPixel(col,row,Color.FromArgb(newR,newG,newB));
                }
            calcHist();
        }

        public void changeGamma(double gamma)
        {

            for (int col = 0; col < Img.Width; col++)
                for (int row = 0; row < Img.Height; row++)
                {
                    Color curCol = Img.GetPixel(col, row);
                    int newR = (int)(Math.Pow((double)curCol.R / 256.0, 1.0 / gamma) * 255.0);
                    int newG = (int)(Math.Pow((double)curCol.G / 256.0, 1.0 / gamma) * 255.0);
                    int newB = (int)(Math.Pow((double)curCol.B / 256.0, 1.0 / gamma) * 255.0);
                    newR = Math.Min(newR, 255);
                    newR = Math.Max(newR, 0);
                    newG = Math.Min(newG, 255);
                    newG = Math.Max(newG, 0);
                    newB = Math.Min(newB, 255);
                    newB = Math.Max(newB, 0);
                    Img.SetPixel(col, row, Color.FromArgb(newR, newG, newB));
                }
            calcHist();
        }

        public void histEqualization()
        {
            calcHist();
            double[] redHistSum = new double[256];
            double[] greenHistSum = new double[256];
            double[] blueHistSum = new double[256];
            redHistSum[0] = redHist[0];
            greenHistSum[0] = greenHist[0];
            blueHistSum[0] = blueHist[0];

            // Calcualte CDF
            for (int i = 1; i < 256; i++)
            {
                redHistSum[i] = redHistSum[i - 1] + redHist[i];
                greenHistSum[i] = greenHistSum[i - 1] + greenHist[i];
                blueHistSum[i] = blueHistSum[i - 1] + blueHist[i];
            }
            for (int i = 0; i < 256; i++)
            {
                redHistSum[i] /= (double)(Img.Width * Img.Height);
                greenHistSum[i] /= (double)(Img.Width * Img.Height);
                blueHistSum[i] /= (double)(Img.Width * Img.Height);
            }

            for (int col = 0; col < Img.Width; col++)
                for (int row = 0; row < Img.Height; row++)
                {
                    Color curCol = Img.GetPixel(col, row);
                    int newR = (int)(redHistSum[curCol.R] * 255.0);
                    int newG = (int)(greenHistSum[curCol.G] * 255.0);
                    int newB = (int)(blueHistSum[curCol.B] * 255.0);
                    newR = Math.Min(newR, 255);
                    newR = Math.Max(newR, 0);
                    newG = Math.Min(newG, 255);
                    newG = Math.Max(newG, 0);
                    newB = Math.Min(newB, 255);
                    newB = Math.Max(newB, 0);
                    Img.SetPixel(col, row, Color.FromArgb(newR, newG, newB));
                }
            calcHist();
        }

        public static Bitmap histMatching(Bitmap Img1, Bitmap Img2)
        {
            double[] redHistSum = new double[256];
            double[] greenHistSum = new double[256];
            double[] blueHistSum = new double[256];
            int[,] directMapImg1 = new int[3, 256];
            //int[,] directMapImg2 = new int[3, 256];
            int[,] inverseMapImg2 = new int[3,256];
            int[] redH; int[] greenH; int[] blueH;
            redH = new int[256];
            greenH = new int[256];
            blueH = new int[256];
            for (int i = 0; i < 256; i++)
                redH[i] = greenH[i] = blueH[i] = 0;

            for (int col = 0; col < Img1.Width; col++)
                for (int row = 0; row < Img1.Height; row++)
                {
                    Color curCol = Img1.GetPixel(col, row);
                    redH[curCol.R]++;
                    greenH[curCol.G]++;
                    blueH[curCol.B]++;
                }

            redHistSum[0] = redH[0];
            greenHistSum[0] = greenH[0];
            blueHistSum[0] = blueH[0];

            // Calcualte CDF
            for (int i = 1; i < 256; i++)
            {
                redHistSum[i] = redHistSum[i - 1] + redH[i];
                greenHistSum[i] = greenHistSum[i - 1] + greenH[i];
                blueHistSum[i] = blueHistSum[i - 1] + blueH[i];
            }
            for (int i = 0; i < 256; i++)
            {
                redHistSum[i] /= (double)(Img1.Width * Img1.Height);
                greenHistSum[i] /= (double)(Img1.Width * Img1.Height);
                blueHistSum[i] /= (double)(Img1.Width * Img1.Height);
            }
            for (int i = 0; i < 256; i++)
            {
                directMapImg1[0, i] = (int)(redHistSum[i] * 255.0);
                directMapImg1[1, i] = (int)(greenHistSum[i] * 255.0);
                directMapImg1[2, i] = (int)(blueHistSum[i] * 255.0);
            }
            for (int i = 0; i < 256; i++)
            {
                redHistSum[i] = blueHistSum[i] = greenHistSum[i] = 0.0;
                redH[i] = greenH[i] = blueH[i] = 0;
            }

            for (int col = 0; col < Img2.Width; col++)
                for (int row = 0; row < Img2.Height; row++)
                {
                    Color curCol = Img2.GetPixel(col, row);
                    redH[curCol.R]++;
                    greenH[curCol.G]++;
                    blueH[curCol.B]++;
                }

            redHistSum[0] = redH[0];
            greenHistSum[0] = greenH[0];
            blueHistSum[0] = blueH[0];

            // Calcualte CDF
            for (int i = 1; i < 256; i++)
            {
                redHistSum[i] = redHistSum[i - 1] + redH[i];
                greenHistSum[i] = greenHistSum[i - 1] + greenH[i];
                blueHistSum[i] = blueHistSum[i - 1] + blueH[i];
            }
            for (int i = 0; i < 256; i++)
            {
                redHistSum[i] /= (double)(Img2.Width * Img2.Height);
                greenHistSum[i] /= (double)(Img2.Width * Img2.Height);
                blueHistSum[i] /= (double)(Img2.Width * Img2.Height);
            }
            for (int j = 0; j < 3; j++)
                for (int i = 0; i < 256; i++)
                    inverseMapImg2[j, i] = -1;
                    for (int i = 0; i < 256; i++)
                    {
                        if (inverseMapImg2[0, (int)(redHistSum[i] * 255.0)]==-1)
                        inverseMapImg2[0, (int)(redHistSum[i] * 255.0)] = i;
                        if (inverseMapImg2[1, (int)(greenHistSum[i] * 255.0)] == -1)
                        inverseMapImg2[1, (int)(greenHistSum[i] * 255.0)] = i;
                        if (inverseMapImg2[2, (int)(blueHistSum[i] * 255.0)] == -1)
                        inverseMapImg2[2, (int)(blueHistSum[i] * 255.0)] = i;
                    }


            for (int col = 0; col < Img1.Width; col++)
                for (int row = 0; row < Img1.Height; row++)
                {
                    Color curCol = Img1.GetPixel(col, row);
                    int newR = (int)(inverseMapImg2[0, directMapImg1[0, curCol.R]] * 255.0);
                    int newG = (int)(inverseMapImg2[1, directMapImg1[1, curCol.G]] * 255.0);
                    int newB = (int)(inverseMapImg2[2, directMapImg1[2, curCol.B]] * 255.0);
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

        public void grayScale()
        {

            Bitmap newImg = new Bitmap(Img.Width, Img.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            for (int col = 0; col < newImg.Width; col++)
                for (int row = 0; row < newImg.Height; row++)
                {
                    Color oldColor = Img.GetPixel(col, row);
                    int avg = (int)((oldColor.R + oldColor.G + oldColor.B) / 3.0);
                    newImg.SetPixel(col, row, Color.FromArgb(avg, avg, avg));
                }
            Img = newImg;
            calcHist();
        }

        public Bitmap getImage()
        {
            return Img;
        }

    }
}
