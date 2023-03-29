using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
namespace WPFWatermarking
{
    public class PixelMixer
    {
        int x = 0, y = 0;
        int k = 0, l = 0;
        
        Random random1 = new Random();
        Random random2 = new Random();
        StreamWriter stream_writer;
        FileStream MyFileStream;
        public  Bitmap MIX(Bitmap image1, Bitmap image2, int opacity,string _path,string _name)
        {
            try
            {
                //this.writeColorpixel();

                FileInfo info = new FileInfo( _path + "\\" + _name + ".txt");
                if (info.Exists)
                    info.Delete();
                MyFileStream = new FileStream( _path + "\\" + _name + ".txt", FileMode.CreateNew, FileAccess.Write, FileShare.None);
                stream_writer = new StreamWriter(MyFileStream);
               //MessageBox.Show(image1.Height+ "Image1"+image1.Width+","+image2.Height+"Image2"+image2.Width);
                Bitmap bmp = new Bitmap(image2);
               // Bitmap newBmp = new Bitmap(Math.Min(image1.Width, image2.Width), Math.Min(image1.Height, image2.Height));
                for (int i = 0; i < image1.Width ; i++)
                {
                    
                        for (int j = 0; j < image1.Height; j++)
                        {
                            try
                            {
                               // k=random1.Next(
                                k = random1.Next(i, image2.Width);
                                l = random2.Next(j, image2.Height);

                               // Console.WriteLine(i + "i" + j + "j," + k + " " + l);
                                Color image1Pixel = image1.GetPixel(i, j), image2Pixel = image2.GetPixel(k, l);
                                Color write = Color.FromArgb(image1Pixel.R, image1Pixel.G, image1Pixel.B);
                                Color newColor = Color.FromArgb((image1Pixel.R * opacity + image2Pixel.R * (100 - opacity)) / 100,
                                                                (image1Pixel.G * opacity + image2Pixel.G * (100 - opacity)) / 100,
                                                                (image1Pixel.B * opacity + image2Pixel.B * (100 - opacity)) / 100);
                                stream_writer.WriteLine(k.ToString() + "," + l.ToString() + ";"+image2Pixel.R.ToString() +","+image2Pixel.G.ToString()+","+image2Pixel.B.ToString()+";"+ i.ToString() + "," + j.ToString() + ";" + image1Pixel.R.ToString() + "," + image1Pixel.G.ToString() + "," + image1Pixel.B.ToString());
                                // newBmp.SetPixel(i, j, newColor);
                                bmp.SetPixel(k, l, newColor);
                                // 
                            }
                            catch (Exception ex)
                            {
                                continue;
                            }
                        }
                   
                }
                stream_writer.Flush();
                stream_writer.Close();
                return bmp;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        private void writeColorpixel(Bitmap image1,string _totalpath)
        {
            try
            {
               
                for (int i = 0; i < image1.Width; i++)
                {
                    for (int j = 0; j < image1.Height; j++)
                    {
                        Color image1Pixel = image1.GetPixel(i, j);
                        stream_writer.WriteLine(image1Pixel.ToString());
                    }
                }
               
            }
            catch(Exception ex)
            {
                
            }
        }
    }
}
