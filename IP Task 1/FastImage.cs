using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;

namespace ImageOperationsPackage
{
    // An unsafe access image
    class FastImage
    {
        public Bitmap Img;
        public int Depth { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public byte[] Pixels { get; set; }

        public FastImage()
        { }

        public FastImage(Bitmap Img)
        {
            this.Img = Img;
            Width = this.Img.Width;
            Height = this.Img.Height;
            this.Depth = Image.GetPixelFormatSize(this.Img.PixelFormat);
            LockImgBits(this.Img);  
        }

        private bool LockImgBits(Bitmap Image) 
        {
            try
            {
                BitmapData bitdata = Image.LockBits(new Rectangle(0, 0, Image.Width, Image.Height),
                      ImageLockMode.ReadWrite, Image.PixelFormat);
                Pixels = new byte[Width * Height * (Depth / 8)];
                Marshal.Copy(bitdata.Scan0, Pixels, 0, Pixels.Length);
                Img.UnlockBits(bitdata);
            }
            catch (Exception)
            { return false;}
            
            return true;
        }

        //Set Pixel in position (x,y) in the byte array "pixels"
        public void SetPixel(int x, int y, Color color)
        {
            // Get color components count
            int cCount = Depth / 8;

            // Get start index of the specified pixel
            int i = ((y * Width) + x) * cCount;

            if (Depth == 32) // For 32 bpp set Red, Green, Blue and Alpha
            {
                Pixels[i] = color.B;
                Pixels[i + 1] = color.G;
                Pixels[i + 2] = color.R;
                Pixels[i + 3] = color.A;
            }
            if (Depth == 24) // For 24 bpp set Red, Green and Blue
            {
                Pixels[i] = color.B;
                Pixels[i + 1] = color.G;
                Pixels[i + 2] = color.R;
            }
            if (Depth == 8)
            // For 8 bpp set color value (Red, Green and Blue values are the same)
            {
                Pixels[i] = color.B;
            }
        }

        //Get Pixel in position (x,y) from the byte array "Pixels"
        public Color GetPixel(int x, int y)
        {
            Color clr = Color.Empty;

            // Get color components count
            int cCount = Depth / 8;

            // Get start index of the specified pixel
            int i = ((y * Width) + x) * cCount;

            if (i > Pixels.Length - cCount)
                throw new IndexOutOfRangeException();

            if (Depth == 32) // For 32 bpp get Red, Green, Blue and Alpha
            {
                byte b = Pixels[i];
                byte g = Pixels[i + 1];
                byte r = Pixels[i + 2];
                byte a = Pixels[i + 3]; // a
                clr = Color.FromArgb(a, r, g, b);
            }
            
            if (Depth == 24) // For 24 bpp get Red, Green and Blue
            {
                byte b = Pixels[i];
                byte g = Pixels[i + 1];
                byte r = Pixels[i + 2];
                clr = Color.FromArgb(r, g, b);
            }
            
            if (Depth == 8)
            // For 8 bpp get color value (Red, Green and Blue values are the same)
            {
                byte c = Pixels[i];
                clr = Color.FromArgb(c, c, c);
            }
            return clr;
        }
        public Bitmap getBitmap()
        {
            BitmapData bmData = Img.LockBits(new Rectangle(0, 0, Img.Width, Img.Height),
            ImageLockMode.ReadWrite, Img.PixelFormat);
            Marshal.Copy(Pixels, 0, bmData.Scan0, Width * Height * (Depth / 8));
            Img.UnlockBits(bmData);
            return Img;
        }
          

    }
}
