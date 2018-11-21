using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gif_parser
{
    public partial class Form1 : Form
    {
        private GIF gif;

        public Form1()
        {
            InitializeComponent();

            byte[] data = getBytesFromURL("http://upload.wikimedia.org/wikipedia/commons/2/2c/Rotating_earth_%28large%29.gif");
            
            if (data != null)
            {
                try
                {
                    gif = new GIF(data);
                    pictureBox1.Image = gif.images[0].toImage();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
            }
            else
            {
                //error handling
            }
        }

        /// <summary>
        /// gets the bytes from a file
        /// </summary>
        /// <param name="fullFilePath">the path to the file</param>
        /// <returns>a byte array containing the data</returns>
        private byte[] GetBytesFromFile(string fullFilePath)
        {
            // this method is limited to 2^32 byte files (4.2 GB)

            FileStream fs = null;
            try
            {
                fs = File.OpenRead(fullFilePath);
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
                return bytes;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
        }

        /// <summary>
        /// takes a url, downloads the file and returns it as a byte array
        /// </summary>
        /// <param name="url">the url</param>
        /// <returns>a byte array containing the data, or null if there was a problem</returns>
        private byte[] getBytesFromURL(string url)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    return client.DownloadData(url);
                }
            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}
