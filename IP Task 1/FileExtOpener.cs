using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace ImageOperationsPackage
{
    class FileExtOpener
    {
        private static Bitmap Img;
        private static int Width;
        private static int Height;
        private static int Depth;
        private static byte[] Pixels;

        public static Bitmap OpenCommonImg(string Path) 
        {
                Img = new Bitmap(Path);
                return Img;
        }

        public static Bitmap OpenPPM(string Path)
        {
            try
            {
                FileStream FS = new FileStream(@Path, FileMode.Open);
                StreamReader SR = new StreamReader(FS);
                string photo = SR.ReadToEnd();
                SR.Close();
                FS.Close();
                if (photo.Substring(0, 2) == "P3")
                   openP3(photo);
                else if (photo.Substring(0, 2) == "P6")
                   openP6(Path, photo);
                else
                    return null;
            }
            catch (Exception)
            { return null; }
            return Img;
        }
        
        private static void openP3(string photo)
        {
            int index = ParsePPMHeader(photo);
            try
            {
                string[] str = photo.Substring(index).Split(new char[] { ' ', '\n' },
                    StringSplitOptions.RemoveEmptyEntries); 
                Pixels = new byte[str.Length];
                //Img = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
                //int idx = 0;
                //for (int i = 0; i < Width; i++)
                //    for (int j = 0; j < Height; j++)
                //    {
                //        int R=-1,G=-1,B=-1;
                //        for (int k = 0; k < 3; k++) 
                //            {

                //                if (k == 0)
                //                    R = byte.Parse(str[idx++]);
                //                else if (k == 1)
                //                    G = byte.Parse(str[idx++]);
                //                else if (k == 2)
                //                    B = byte.Parse(str[idx++]);
                //            }
                //        Img.SetPixel(i, j, Color.FromArgb(R, G, B));
                //    }
                int idx=0;
                foreach (string text in str)
                    Pixels[idx++] = byte.Parse(text);
                BitmapData bmData = Img.LockBits(new Rectangle(0, 0, Img.Width, Img.Height),
                     ImageLockMode.ReadWrite, Img.PixelFormat);
                Marshal.Copy(Pixels, 0, bmData.Scan0, Width * Height * (Depth / 8));
                Img.UnlockBits(bmData);
				
            }
            catch (Exception) { throw; }
          }

        private static void openP6(string Path, string photo)
        {
            int index = ParsePPMHeader(photo);    
            try
            {   Pixels = File.ReadAllBytes(Path).Skip(index).ToArray(); }
            catch (Exception) 
            { throw; }

            BitmapData bmData = Img.LockBits(new Rectangle(0, 0, Img.Width, Img.Height),
                     ImageLockMode.ReadWrite, Img.PixelFormat);
            Marshal.Copy(Pixels, 0, bmData.Scan0, Width * Height * (Depth / 8));
            Img.UnlockBits(bmData);
        }

		     private static int ParsePPMHeader(string photo) 
        {
            int index = 3;
            if (photo[index] == '#')
                index = photo.IndexOf("\n", index) + 1;
            string[] dimensions = photo.Substring(index, photo.IndexOf("\n", index) - index).Split(' ');
            index = photo.IndexOf("\n", index) + 1;
            Width = Convert.ToInt32(dimensions[0]);
            Height = Convert.ToInt32(dimensions[1]);
            Depth = 24;
            Img = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
            string MaxNumber = photo.Substring(index, photo.IndexOf("\n", index) - index);
            index = photo.IndexOf("\n", index) + 1;
            return index;
        }

    }
}
