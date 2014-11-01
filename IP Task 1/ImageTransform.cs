using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing;
using IP_Task_1;

namespace ImageOperationsPackage
{
    class ImageTransform
    {
		private Matrix transMatrix;
        private FastImage oldFastImage;
        private FastImage newFastImage;
        private Bitmap oldBitmap;
        private Bitmap newBitmap;
        private PointF[,] newPixelMap;
        private PointF[,] reversePixelMap;
		private float minX;
        private float minY;
        private float maxX;
		private float maxY;
        private int newWidth;
        private int newHeight;
        private int oldWidth;
        private int oldHeight;
        private float scale_x;
        private float scale_y;
        private float rotation;
        private float shear_x;
        private float shear_y;

		public ImageTransform()
		{
            transMatrix = new Matrix();
            scale_x = 1.0f;
            scale_y = 1.0f;
            rotation = 0.0f;
            shear_x = 0.0f;
            shear_y = 0.0f;
		}

        public ImageTransform(float[] transParam)
		{
            transMatrix = new Matrix();
			scale_x = transParam[0];
            scale_y = transParam[1];
            rotation = transParam[2];
            shear_x = transParam[3];
            shear_y = transParam[4];
            transMatrix.Scale(scale_x, scale_y);
            transMatrix.Rotate(rotation, MatrixOrder.Append);
            transMatrix.Shear(shear_x, shear_y, MatrixOrder.Append);
		}

        void setShear(float shear_x, float shear_y)
        {
            this.shear_x = shear_x;
            this.shear_y = shear_y;
        }

        void setScale(float scale_x, float scale_y)
        {
            this.scale_x = scale_x;
            this.scale_y = scale_y;
        }
        void setRotation(float rotation)
        {
            this.rotation = rotation;
        }


		private void getBoundary(int n, int m)
		{
            PointF []p = new PointF[4];
            p[0] = new PointF(0, 0);
            p[1] = new PointF(n, 0);
            p[2] = new PointF(0, m);
            p[3] = new PointF(n, m);
            transMatrix.TransformPoints(p);
            minX = Math.Min(Math.Min(p[0].X, p[1].X), Math.Min(p[2].X, p[3].X));
            minY = Math.Min(Math.Min(p[0].Y, p[1].Y), Math.Min(p[2].Y, p[3].Y));
            maxX = Math.Max(Math.Max(p[0].X, p[1].X), Math.Max(p[2].X, p[3].X));
            maxY = Math.Max(Math.Max(p[0].Y, p[1].Y), Math.Max(p[2].Y, p[3].Y));
            newHeight = (int)Math.Ceiling(maxY - minY);
            newWidth = (int)Math.Ceiling(maxY - minX);
            transMatrix.Translate(-minX, -minY);
		}

		private Color interpolate(Point p)
        {

            return new Color();
        }

        public Bitmap GeometricLinearTransform(Bitmap oldBitmap)
        {
			oldWidth = oldBitmap.Width;
            oldHeight = oldBitmap.Height;
			this.oldBitmap = oldBitmap;
            getBoundary(oldHeight, oldWidth);
            oldFastImage = new FastImage(oldBitmap);

			//build new buffer
            newBitmap = new Bitmap(newWidth, newHeight);
            newFastImage = new FastImage(newBitmap);  
            newPixelMap = new PointF[oldHeight, oldWidth];
			reversePixelMap = new PointF[newHeight, newWidth];

			// map from original to new image 
			for(int row = 0 ; row < oldHeight ; row++)
				for(int col = 0 ; col< oldWidth ; col++)
                {
                    newPixelMap[row, col] = new PointF(col, row);
					transMatrix.TransformPoints(new[]{newPixelMap[row,col]});
                }

            Matrix invMatrix = transMatrix;
            invMatrix.Invert();

			// map from new image back to original
            for (int row = 0; row < newHeight; row++)
                for (int col = 0; col < newWidth; col++)
                {
                    reversePixelMap[row, col] = new PointF(col, row);
                    invMatrix.TransformPoints(new[] { reversePixelMap[row, col] });
                }

            //testing github 
			// interpolate
			  for (int row = 0; row < newHeight; row++)
                  for (int col = 0; col < newWidth; col++)
                  {
                      if (reversePixelMap[row, col].X >= 0 && reversePixelMap[row, col].X < oldWidth && reversePixelMap[row, col].Y >= 0 && reversePixelMap[row, col].Y < oldHeight)
                      {
                          int X1 = (int)Math.Floor(reversePixelMap[row, col].X);
                          int X2 = X1 + 1;
                          int Y1 = (int)Math.Floor(reversePixelMap[row, col].Y);
                          int Y2 = Y1 + 1;

                          Color P1 = oldFastImage.GetPixel(X1, Y1);
                          Color P2 = oldFastImage.GetPixel(X2, Y1);
                          Color P3 = oldFastImage.GetPixel(X1, Y2);
                          Color P4 = oldFastImage.GetPixel(X2, Y2);

                          float Xfraction = reversePixelMap[row, col].X - (float)X1;
                          float Yfraction = reversePixelMap[row, col].Y - (float)Y1;

                          //Z1 = P1 × (1 – Xfraction) + P2 × Xfraction
                          //Z2 = P3 × (1 – Xfraction) + P4 × Xfraction

                          float Z1R = (float)P1.R * (1f - Xfraction) + P2.R * Xfraction;
                          float Z1G = (float)P1.G * (1f - Xfraction) + P2.G * Xfraction;
                          float Z1B = (float)P1.B * (1f - Xfraction) + P2.B * Xfraction;

                          float Z2R = (float)P3.R * (1f - Xfraction) + P4.R * Xfraction;
                          float Z2G = (float)P3.G * (1f - Xfraction) + P4.G * Xfraction;
                          float Z2B = (float)P3.B * (1f - Xfraction) + P4.B * Xfraction;


                          int R = (int)(Z1R * (1f - Yfraction) + Z2R * Yfraction);
                          int G = (int)(Z1G * (1f - Yfraction) + Z2G * Yfraction);
                          int B = (int)(Z1B * (1f - Yfraction) + Z2B * Yfraction);

						  newFastImage.SetPixel(col, row, Color.FromArgb(R, G, B));
                      }
                      else
                      {
                          newFastImage.SetPixel(col, row, Color.FromArgb(0,0,0));
                      }
                  }
              return newFastImage.getBitmap();
        }

    }
}
