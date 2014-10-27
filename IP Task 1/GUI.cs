using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageOperationsPackage;

namespace IP_GUI
{
    public partial class GUI : Form
    {
        Bitmap sourceBitmap;

        public GUI()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            float[] transParam;
            transParam = new float[5];
            transParam[0] = float.Parse(ScaleXTB.Text);
            transParam[1] = float.Parse(ScaleYTB.Text);
            transParam[2] = float.Parse(RotateTB.Text);
            transParam[3] = float.Parse(ShearXTB.Text);
            transParam[4] = float.Parse(ShearYTB.Text);
            ImageTransform IT = new ImageTransform(transParam);
          //  try
            {
                Bitmap B =  IT.GeometricLinearTransform(sourceBitmap);
                AfterBox.Image = B;
            }
         //   catch
            {
         //       MessageBox.Show("Image can't be Transformed !", "Error", MessageBoxButtons.OK);
            }
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "All Picture Files |*.bmp;*.jpg;*.jpeg;*.jpe;*.png;*.tif;*.tiff;*.ppm;|All Files (*.*)|*.*";
            openFileDialog1.Title = "Open an Image";
            openFileDialog1.CheckPathExists = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                sourceBitmap = null;
                //Open the browsed image and display it
                string OpenedFilePath = openFileDialog1.FileName;
                if (OpenedFilePath.Split('.')[1] != "ppm")
                    sourceBitmap = FileExtOpener.OpenCommonImg(OpenedFilePath);
                else
                    sourceBitmap = FileExtOpener.OpenPPM(OpenedFilePath);
                if (sourceBitmap == null)
                    MessageBox.Show("Image can't be opened !", "Error", MessageBoxButtons.OK);
                else
                {
                    filePath.Text = OpenedFilePath;
                    BeforeBox.Image = sourceBitmap;
                }

            }

        }
    }
}
