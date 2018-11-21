using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace image_thresholding
{
    class Program
    {
        static void Main(string[] args)
        {
            Mat colorImage = new Mat(@"./fruit.jpg", LoadImageType.Color);
            
            //convert the image to hsv
            Mat hsvImage = new Mat();
            CvInvoke.CvtColor(colorImage, hsvImage, ColorConversion.Bgr2Hsv);
            
            //set up our lower and upper bounds for our orange colour
            UMat lowerBorder = new UMat(hsvImage.Rows, hsvImage.Cols, DepthType.Cv8U, 3);
            Hsv lowOrange = new Hsv(0, 180, 200);
            lowerBorder.SetTo(lowOrange.MCvScalar);

            UMat upperBorder = new UMat(hsvImage.Rows, hsvImage.Cols, DepthType.Cv8U, 3);
            Hsv highOrange = new Hsv(25, 255, 255);
            upperBorder.SetTo(highOrange.MCvScalar);
            
            //find only pixels that are between the borders
            Mat mask = new Mat();
            CvInvoke.InRange(hsvImage, lowerBorder, upperBorder, mask);

            lowerBorder.Dispose();
            upperBorder.Dispose();
            hsvImage.Dispose();

            //convert bgr to gray, but keeping a depth of 3
            Mat grayImage = new Mat();
            CvInvoke.CvtColor(colorImage, grayImage, ColorConversion.Bgr2Gray);
            CvInvoke.CvtColor(grayImage, grayImage, ColorConversion.Gray2Bgr);

            //copy only the orange pixels onto the gray image
            colorImage.CopyTo(grayImage, mask);

            colorImage.Dispose();

            ImageViewer.Show(grayImage, "Image Thresholding");

            grayImage.Dispose();
        }
    }
}
