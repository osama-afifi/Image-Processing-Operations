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
            try
            {
                Bitmap B =  IT.GeometricLinearTransform(sourceBitmap);
                AfterBox.Image = B;
            }
            catch
            {
                MessageBox.Show("Image can't be Transformed !", "Error", MessageBoxButtons.OK);
            }
        }

        string browse()
        { 
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "All Picture Files |*.bmp;*.jpg;*.jpeg;*.jpe;*.png;*.tif;*.tiff;*.ppm;|All Files (*.*)|*.*";
            openFileDialog1.Title = "Open an Image";
            openFileDialog1.CheckPathExists = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                return  openFileDialog1.FileName;
            return null;
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
                if (OpenedFilePath.Split('.')[1] != "ppm" && OpenedFilePath.Split('.')[1] != "PPM")
                    sourceBitmap = FileExtOpener.OpenCommonImg(OpenedFilePath);
                else
                    sourceBitmap = FileExtOpener.OpenPPM(OpenedFilePath);
                if (sourceBitmap == null)
                    MessageBox.Show("Image can't be opened !", "Error", MessageBoxButtons.OK);
                else
                {
                    filePath.Text = OpenedFilePath;
                    AfterBox.Image = sourceBitmap;
                }

            }

        }


        private void refreshHistograms()
        {
            RedHistogramChart.Series.Clear();
            RedHistogramChart.Series.Add("Pixel Values");
            GreenHistogramChart.Series.Clear();
            GreenHistogramChart.Series.Add("Pixel Values");
            BlueHistogramChart.Series.Clear();
            BlueHistogramChart.Series.Add("Pixel Values");
            GrayHistogramChart.Series.Clear();
            GrayHistogramChart.Series.Add("Pixel Values");
            

            ImageHistogram imageHist = new ImageHistogram(new Bitmap(AfterBox.Image));
            imageHist.calcHist();
            Random r = new Random();
            int maxR = 0;
            int maxG = 0;
            int maxB = 0;
            int maxGray = 0;
            for (int i = 0; i < 255; i++)
            {
                maxR = Math.Max(imageHist.redHist[i], maxR);
                maxG = Math.Max(imageHist.greenHist[i], maxG);
                maxB = Math.Max(imageHist.blueHist[i], maxB);
                maxGray = Math.Max(imageHist.grayHist[i], maxGray);
            }

            for (int i = 0; i < 255; i++)
            {
                GrayHistogramChart.Series[0].Points.Add(imageHist.grayHist[i] / (double)maxGray);
                RedHistogramChart.Series[0].Points.Add(imageHist.redHist[i] / (double)maxR);
                GreenHistogramChart.Series[0].Points.Add(imageHist.greenHist[i] / (double)maxG);
                BlueHistogramChart.Series[0].Points.Add(imageHist.blueHist[i] / (double)maxB);
            }
            
        }

    
        private void ApplyHistButton_Click(object sender, EventArgs e)
        {
            if (HistMatchCheck.Checked)
            {
                Bitmap Img1 = new Bitmap(SourceBox.Text);
                Bitmap Img2 = new Bitmap(TargetBox.Text);
                Bitmap newImg = ImageHistogram.histMatching(Img1, Img2);
                AfterBox.Image = newImg;
                refreshHistograms();
                return;
            }
            Bitmap B;
            if (sourceBitmap == null) return;
            B = new Bitmap(sourceBitmap);
            ImageHistogram imageHist = new ImageHistogram(B);
            if (GrayCheck.Checked)
                imageHist.grayScale();
            if (BrightCheck.Checked)
                imageHist.changeBrightness(int.Parse(BrightBox.Text));
            if(ContrastCheck.Checked)
                imageHist.changeContrast(int.Parse(ContrastMinBox.Text),(int.Parse(ContrastMaxBox.Text)));
            if (GammaCheck.Checked)
                imageHist.changeGamma(double.Parse(GammaBox.Text));
            if (HistEqCheck.Checked)
                imageHist.histEqualization();
            AfterBox.Image = imageHist.getImage();
            refreshHistograms();
        }

        private void ApplyArithemticButton_Click(object sender, EventArgs e)
        {
            Bitmap Img1 = new Bitmap(Img1Box.Text);
            Bitmap Img2 = new Bitmap(Img2Box.Text);

            if (AddRadio.Checked)
                AfterBox.Image = ImageArithmetic.Add(Img1, Img2, double.Parse(AddFractBox.Text));
            if (SubtractRadio.Checked)
                AfterBox.Image = ImageArithmetic.Subtract(Img1, Img2);
            if (NOTRadio.Checked)
                AfterBox.Image = ImageArithmetic.NotImage(Img1);
            if (BitRadio.Checked)
                AfterBox.Image = ImageArithmetic.BitplaneSlicing(Img1, int.Parse(BitBox.Text));
            refreshHistograms();
        }

        private void BrowseImg1Button_Click(object sender, EventArgs e)
        {
            Img1Box.Text = browse();
        }

        private void BrowseImg2Button_Click(object sender, EventArgs e)
        {
            Img2Box.Text = browse();
        }

        private void SourceButton_Click(object sender, EventArgs e)
        {
            SourceBox.Text = browse();
        }

        private void TargetButton_Click(object sender, EventArgs e)
        {
            TargetBox.Text = browse();
        }

        private void FilterApplyButton_Click(object sender, EventArgs e)
        {
            Bitmap B;
            if (sourceBitmap == null) return;
            B = new Bitmap(sourceBitmap);
            ImageFilter imageFilter = new ImageFilter(B);

            if (meanRadio.Checked)
            {
                int maskWidth = int.Parse(MeanSizeXBox.Text);
                int maskHeight = int.Parse(MeanSizeYBox.Text);
                int maskOrgX = int.Parse(MeanOrgXBox.Text);
                int maskOrgY = int.Parse(MeanOrgYBox.Text);
                imageFilter.meanFilter(maskHeight, maskWidth, maskOrgX, maskOrgY);
            }
            else if (gaussian1Radio.Checked)
            {
                int maskSize = int.Parse(Gaussian1MaskBox.Text);
                double sigma = double.Parse(Gaussian1SigmaBox.Text);
                imageFilter.gaussianFilter(maskSize, sigma);
            }

            else if (gaussian2Radio.Checked)
            {
                double sigma = double.Parse(Gaussian2SigmaBox.Text);
                imageFilter.gaussianFilter(sigma);
            }

            else if (laplaceRadio.Checked)
            {
                imageFilter.laplaceSharpen();
            }

            else if (highBoostRadio.Checked)
            {
                int maskSize = int.Parse(HighBoostMaskBox.Text);
                double sigma = double.Parse(HighBoostSigmaBox.Text);
                double k = double.Parse(HighBoostKBox.Text);
                imageFilter.highBoost(maskSize, sigma, k);
            }

            else if (kirshRadio.Checked)
            {
                KirshEdgeDir dir = (KirshEdgeDir)KirshComboBox.SelectedIndex;
                imageFilter.kirshEdgeDetection(dir);
            }
            else
            {
                return;
            }
            sourceBitmap = imageFilter.getImage(); // ensures cummulative ops
            AfterBox.Image = sourceBitmap;
        }

    }
}
