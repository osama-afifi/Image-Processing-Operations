using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ImageOperationsPackage
{
    public enum PostProcessingType { NoPostProcess, CutOff, Absolute, Normalize };
    public enum KirshEdgeDir { Horizontal, Vertical, DiagonalNorthWest, DiagonalNorthEast };

    class ImageFilter
    {
        Bitmap Img;

        public ImageFilter(Bitmap Img)
        {
            if (Img.PixelFormat != System.Drawing.Imaging.PixelFormat.Format24bppRgb && Img.PixelFormat != System.Drawing.Imaging.PixelFormat.Format32bppArgb)
                this.Img = ImageArithmetic.toGray(Img);
            else
                this.Img = Img;
        }

        public void linearFilter(double[,] filter, int origX, int origY, PostProcessingType postType = PostProcessingType.CutOff)
        {
            int filterHeight = filter.GetLength(0);
            int filterWidth = filter.GetLength(1);
            Color[,] colorBuffer = new Color[Img.Height, Img.Width];
            double[,,] resBuffer = new double[3,Img.Height, Img.Width];
            for (int col = 0; col < Img.Width; col++)
                for (int row = 0; row < Img.Height; row++)
                    colorBuffer[row, col] = Img.GetPixel(col, row);
            double maxR = 0;
            double maxG = 0;
            double maxB = 0;
            double minR = 255;
            double minG = 255;
            double minB = 255;

            for (int col = -filterWidth + 1; col < Img.Width; col++)
                for (int row = -filterHeight + 1; row < Img.Height; row++)
                {
                    double newR = 0, newG = 0, newB = 0;

                    for (int i = 0; i < filterHeight; i++)
                        for (int j = 0; j < filterWidth; j++)
                        {
                            Color curColor;
                            // White Padding
                            if (row + i < 0 || col + j < 0 || row + i >= Img.Height || col + j >= Img.Width)
                                curColor = Color.White;
                            else
                                curColor = colorBuffer[row + i, col + j];
                            double newMaskR = (double)curColor.R * filter[i, j];
                            double newMaskG = (double)curColor.G * filter[i, j];
                            double newMaskB = (double)curColor.B * filter[i, j];
                            newR += newMaskR;
                            newG += newMaskG;
                            newB += newMaskB;
                        }

                    maxR = Math.Max(maxR, newR);
                    minR = Math.Min(minR, newR);
                    maxG = Math.Max(maxG, newG);
                    minG = Math.Min(minG, newG);
                    maxB = Math.Max(maxB, newB);
                    minB = Math.Min(minB, newB);

                    if (postType == PostProcessingType.CutOff)
                    {
                        newR = Math.Min(newR, 255);
                        newR = Math.Max(newR, 0);
                        newG = Math.Min(newG, 255);
                        newG = Math.Max(newG, 0);
                        newB = Math.Min(newB, 255);
                        newB = Math.Max(newB, 0);
                    }

                    else if (postType == PostProcessingType.Absolute)
                    {
                        newR = Math.Abs(newR);
                        newG = Math.Abs(newG);
                        newB = Math.Abs(newB);
                    }

                    if (row + origY < 0 || col + origX < 0 || row + origY >= Img.Height || col + origX >= Img.Width)
                        continue;

                    if (postType == PostProcessingType.Normalize)
                    {
                        resBuffer[0, row + origY, col + origX] = newR;
                        resBuffer[1, row + origY, col + origX] = newG;
                        resBuffer[2, row + origY, col + origX] = newB;
                    }
                    else
                        Img.SetPixel(col + origX, row + origY, Color.FromArgb((int)newR, (int)newG, (int)newB));

                }

            if (postType == PostProcessingType.Normalize)
            {
                for (int col = 0; col < Img.Width; col++)
                    for (int row = 0; row < Img.Height; row++)
                    {
                        int newR = (int)(((resBuffer[0,row, col] - minR) / (maxR - minR)) * 255.0);
                        int newG = (int)(((resBuffer[1,row, col] - minG) / (maxG - minG)) * 255.0);
                        int newB = (int)(((resBuffer[2,row, col] - minB) / (maxB - minB)) * 255.0);
                        Img.SetPixel(col, row, Color.FromArgb((int)newR, (int)newG, (int)newB));
                    }
            }

        }

        public void meanFilter(int maskHeight, int maskWidth, int orgX, int orgY)
        {
            --orgX;
            --orgY;
            orgX = Math.Min(maskWidth - 1, orgX);
            orgX = Math.Max(0, orgX);
            orgY = Math.Min(maskHeight - 1, orgY);
            orgY = Math.Max(0, orgY);
            double[,] filter = new double[maskHeight, maskWidth];
            for (int i = 0; i < maskHeight; i++)
                for (int j = 0; j < maskWidth; j++)
                    filter[i, j] = 1.0 / (double)(maskHeight * maskWidth);
            linearFilter(filter, orgX, orgY, PostProcessingType.NoPostProcess);
        }

        public void gaussianFilter(int maskSize, double sigma)
        {
            double[,] filter = new double[maskSize, maskSize];
            double sum = 0;
            for (int i = 0; i < maskSize; i++)
                for (int j = 0; j < maskSize; j++)
                {
                    int x = j - maskSize / 2;
                    int y = i - maskSize / 2;
                    double gaussian = Math.Exp(-(Math.Pow(x, 2.0) + Math.Pow(y, 2.0)) / (2.0 * sigma * sigma));
                    filter[i, j] = gaussian;
                    sum += gaussian;
                }
            for (int i = 0; i < maskSize; i++)
                for (int j = 0; j < maskSize; j++)
                    filter[i, j] /= sum;

            linearFilter(filter, maskSize / 2, maskSize / 2, PostProcessingType.NoPostProcess);
        }

        public void gaussianFilter(double sigma)
        {
            int maskSize = (int)(2.0 * Math.Round(3.7 * sigma - 0.5) + 1);
            double[,] filter = new double[maskSize, maskSize];
            for (int i = 0; i < maskSize; i++)
                for (int j = 0; j < maskSize; j++)
                {
                    int x = j - maskSize / 2;
                    int y = i - maskSize / 2;
                    double gaussian = (1.0 / (2.0 * Math.PI * sigma * sigma)) * Math.Exp(-((double)x * (double)x + (double)y * (double)y) / (2.0 * sigma * sigma));
                    filter[i, j] = gaussian;
                }
            linearFilter(filter, maskSize / 2, maskSize / 2, PostProcessingType.NoPostProcess);
        }

        public void laplaceSharpen()
        {
            int maskSize = 3;
            double[,] filter = new double[maskSize, maskSize];
            for (int i = 0; i < maskSize; i++)
                for (int j = 0; j < maskSize; j++)
                    filter[i, j] = -1;
            filter[1, 1] = 9;
            linearFilter(filter, maskSize / 2, maskSize / 2, PostProcessingType.CutOff);
        }


        public void highBoost(int maskSize, double sigma, double k)
        {
            // k==1 Unsharp
            // k>1 highboost
            Bitmap original = new Bitmap(Img);
            gaussianFilter(maskSize,sigma);
            Bitmap MaskImage = ImageArithmetic.Subtract(original, Img);
            Bitmap newImg = new Bitmap(Img.Width, Img.Height);
            for (int col = 0; col < Img.Width; col++)
                for (int row = 0; row < Img.Height; row++)
                {
                    Color curCol = Img.GetPixel(col, row);
                    Color maskCol = MaskImage.GetPixel(col, row);
                    int newR = (int)(curCol.R + k * maskCol.R);
                    int newG = (int)(curCol.G + k * maskCol.G);
                    int newB = (int)(curCol.B + k * maskCol.B);

                    newR = Math.Min(newR, 255);
                    newR = Math.Max(newR, 0);
                    newG = Math.Min(newG, 255);
                    newG = Math.Max(newG, 0);
                    newB = Math.Min(newB, 255);
                    newB = Math.Max(newB, 0);
                    newImg.SetPixel(col, row, Color.FromArgb(newR, newG, newB));
                }
            this.Img = newImg;
        }


        public void kirshEdgeDetection(KirshEdgeDir dir)
        {
            int maskSize = 3;
            double[,] filter = new double[maskSize, maskSize];
            for (int i = 0; i < maskSize; i++)
                for (int j = 0; j < maskSize; j++)
                    filter[i, j] = -3;

            if (dir == KirshEdgeDir.Horizontal)
            {
                filter[0, 0] = filter[0, 1] = filter[0, 2] = 5;
                filter[1, 1] = 0;
                linearFilter(filter, maskSize / 2, maskSize / 2, PostProcessingType.Normalize);
            }
            else if (dir == KirshEdgeDir.Vertical)
            {
                filter[0, 0] = filter[1, 0] = filter[2, 0] = 5;
                filter[1, 1] = 0;
                linearFilter(filter, maskSize / 2, maskSize / 2, PostProcessingType.Normalize);
            }
            else if (dir == KirshEdgeDir.DiagonalNorthWest)
            {
                filter[0, 1] = filter[0, 2] = filter[1, 2] = 5;
                filter[1, 1] = 0;
                linearFilter(filter, maskSize / 2, maskSize / 2, PostProcessingType.Normalize);
            }
            else if (dir == KirshEdgeDir.DiagonalNorthEast)
            {
                filter[0, 0] = filter[0, 1] = filter[1, 0] = 5;
                filter[1, 1] = 0;
                linearFilter(filter, maskSize / 2, maskSize / 2, PostProcessingType.Normalize);
            }

        }

        public Bitmap getImage()
        {
            return Img;
        }
    }
}
