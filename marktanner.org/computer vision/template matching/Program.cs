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

namespace template_matching
{
    class Program
    {
        static void Main(string[] args)
        {
            string patchImagePath = @"";
            string fullImagePath = @"";

            Image<Bgr, Byte> patchImage = new Image<Bgr, byte>(patchImagePath);
            Image<Bgr, Byte> fullImage = new Image<Bgr, byte>(fullImagePath);

            TemplateMatchingType matchMethod = TemplateMatchingType.CcoeffNormed;

            Image<Gray, float> imgMatch = fullImage.MatchTemplate(patchImage, matchMethod);

            //i have seen people wanting to normalize here, but i dont fully understand why
            CvInvoke.Normalize(imgMatch, imgMatch, 0, 1, NormType.MinMax, DepthType.Default, new Mat());

            //find the best match with minMax
            double[] min, max;
            Point[] pointMin, pointMax;
            imgMatch.MinMax(out min, out max, out pointMin, out pointMax);

            Point matchLoc;

            //for Sqdiff and SqdiffNormed, the best matches are lower values. For all the other methods, the higher the better
            if (matchMethod == TemplateMatchingType.Sqdiff || matchMethod == TemplateMatchingType.SqdiffNormed)
            {
                matchLoc = pointMin[0];
            }
            else
            {
                matchLoc = pointMax[0];
            }

            //draw a red box around the the match
            Rectangle region = new Rectangle(matchLoc.X, matchLoc.Y, patchImage.Width, patchImage.Height);
            fullImage.Draw(region, new Bgr(Color.Red));

            ImageViewer.Show(fullImage, "template matching");
        }
    }
}
