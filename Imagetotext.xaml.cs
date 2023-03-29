using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.IO;

namespace WPFWatermarking
{
    /// <summary>
    /// Interaction logic for Imagetotext.xaml
    /// </summary>
    public partial class Imagetotext : UserControl
    {
        public delegate void ClosedHandler(object sender, EventArgs e);
        public event ClosedHandler CloseImagetoText;

        public Imagetotext()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                imgSource = null;
                uri = null;
                finalBitmap = null;
                userImage.Source = null;
              //  DeleteDirectory("D:\\Image");
                txt_path.Text = "";
                txt_text.Text = "";
                txt_xposition.Text = "";
                txt_yposition.Text = "";
                lb_imgByteArray = null;
             
               
                CloseImagetoText(this, e);
            }
            catch (Exception ex)
            {
            }
        }
        byte[] lb_imgByteArray = null;

        private void cb_browse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog();
                fileDialog.Filter = " Image Files (*.bmp, *.jpg,*.gif)|*.bmp;*.jpg;*.gif";
                if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    lb_imgByteArray = File.ReadAllBytes(fileDialog.FileName);

                    txt_path.Text = fileDialog.FileName;
                    Uri uri = new Uri(txt_path.Text, UriKind.Absolute);
                    ImageSource imgSource = new BitmapImage(uri);
                    userImage.Source = imgSource;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        public  void DeleteDirectory(string target_dir)
        {
           

            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);

           
        }

        private void cb_close_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                imgSource = null;
                uri = null;
                userImage.Source = null;
                finalBitmap = null;
             //   DeleteDirectory("D:\\Image");               
                txt_path.Text = "";
                txt_text.Text = "";
                txt_xposition.Text = "";
                txt_yposition.Text = "";
                lb_imgByteArray = null;
              
                CloseImagetoText(this, e);
            }
            catch (Exception ex)
            {
            }
        }
        int li_opacity = 0, li_xposition = 0, li_yposition = 0;
        string ls_opacity = "";
        System.Drawing.Bitmap finalBitmap = null;
        int li_count = 0;
          Uri uri=null;
        private void cb_preview_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ComboBoxItem cbi = (ComboBoxItem)cmb_opacity.SelectedItem;
                li_count++;

                if (cbi == null)
                {
                    MessageBox.Show("Select opacity of Text", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (txt_xposition.Text.Equals("") || txt_yposition.Text.Equals("") || string.IsNullOrEmpty(txt_yposition.Text) || string.IsNullOrEmpty(txt_xposition.Text))
                {
                    MessageBox.Show("Please give x-position and y-position of image", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                string str1 = cbi.Content.ToString();
                ls_opacity = str1;
                li_xposition = System.Convert.ToInt32(txt_xposition.Text);
                li_yposition = System.Convert.ToInt32(txt_yposition.Text);
               
                switch (ls_opacity)
                {
                    case "100%":
                        {
                            li_opacity = 255;
                            break;
                        }
                    case "75%":
                        {
                            li_opacity = System.Convert.ToInt32(.75 * 255);
                            break;
                        }
                    case "50%":
                        {
                            li_opacity = System.Convert.ToInt32(.5 * 255);
                            break;
                        }
                    case "25%":
                        {
                            li_opacity = System.Convert.ToInt32(.25 * 255);
                            break;
                        }
                    case "10%":
                        {
                            li_opacity = System.Convert.ToInt32(.10 * 255);
                            break;
                        }
                    default:
                        {
                            li_opacity = 125;
                            break;
                        }
                }
                System.Drawing.Image img = System.Drawing.Image.FromStream(new MemoryStream(lb_imgByteArray));
                finalBitmap = new System.Drawing.Bitmap(new System.Drawing.Bitmap(img));
                System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(finalBitmap);
                System.Drawing.Brush mybrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(li_opacity, myWatermarkColor));
                System.Drawing.SizeF sz = g.MeasureString(txt_text.Text, myFont);
                g.DrawString(txt_text.Text, myFont, mybrush, new System.Drawing.Point(li_xposition, li_yposition));
            //    FileInfo fileInfo = new FileInfo("D:\\Image");
                
                if (!(Directory.Exists("D:\\Image")))
                {
                    Directory.CreateDirectory("D:\\Image");         
                }
                finalBitmap.Save("D:\\Image\\Test" + li_count + ".bmp");
               
                uri = new Uri("D:\\Image\\Test" + li_count + ".bmp", UriKind.Absolute);
                imgSource = new BitmapImage(uri);
                userImage.Source = imgSource;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        ImageSource imgSource = null;
        System.Drawing.Font myFont;
        System.Drawing.Color myWatermarkColor;
        DirectoryInfo dir = null;
        public System.Drawing.Bitmap ColorToGrayscale(System.Drawing.Bitmap bmp)
        {
            int w = bmp.Width,
                h = bmp.Height,
                r, ic, oc, bmpStride, outputStride, bytesPerPixel;
            System.Drawing.Imaging.PixelFormat pfIn = bmp.PixelFormat;
            System.Drawing.Imaging.ColorPalette palette;
            System.Drawing.Bitmap output;
            System.Drawing.Imaging.BitmapData bmpData, outputData;

            //Create the new bitmap
            output = new System.Drawing.Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

            //Build a grayscale color Palette
            palette = output.Palette;
            for (int i = 0; i < 256; i++)
            {
                System.Drawing.Color tmp = System.Drawing.Color.FromArgb(255, i, i, i);
                palette.Entries[i] = System.Drawing.Color.FromArgb(255, i, i, i);
            }
            output.Palette = palette;

            //No need to convert formats if already in 8 bit
            if (pfIn == System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
            {
                output = (System.Drawing.Bitmap)bmp.Clone();

                //Make sure the palette is a grayscale palette and not some other
                //8-bit indexed palette
                output.Palette = palette;

                return output;
            }

            //Get the number of bytes per pixel          
            switch (pfIn)
            {
                case System.Drawing.Imaging.PixelFormat.Format24bppRgb: bytesPerPixel = 3; break;
                case System.Drawing.Imaging.PixelFormat.Format32bppArgb: bytesPerPixel = 4; break;
                case System.Drawing.Imaging.PixelFormat.Format32bppRgb: bytesPerPixel = 4; break;
                default: throw new InvalidOperationException("Image format not supported");
            }

            //Lock the images
            bmpData = bmp.LockBits(new System.Drawing.Rectangle(0, 0, w, h), System.Drawing.Imaging.ImageLockMode.ReadOnly,
                                   pfIn);
            outputData = output.LockBits(new System.Drawing.Rectangle(0, 0, w, h), System.Drawing.Imaging.ImageLockMode.WriteOnly,
                                         System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            bmpStride = bmpData.Stride;
            outputStride = outputData.Stride;

            //Traverse each pixel of the image
            unsafe
            {
                byte* bmpPtr = (byte*)bmpData.Scan0.ToPointer(),
                outputPtr = (byte*)outputData.Scan0.ToPointer();

                if (bytesPerPixel == 3)
                {
                    //Convert the pixel to it's luminance using the formula:
                    // L = .299*R + .587*G + .114*B
                    //Note that ic is the input column and oc is the output column
                    for (r = 0; r < h; r++)
                        for (ic = oc = 0; oc < w; ic += 3, ++oc)
                            outputPtr[r * outputStride + oc] = (byte)(int)
                                (0.299f * bmpPtr[r * bmpStride + ic] +
                                 0.587f * bmpPtr[r * bmpStride + ic + 1] +
                                 0.114f * bmpPtr[r * bmpStride + ic + 2]);
                }
                else //bytesPerPixel == 4
                {
                    //Convert the pixel to it's luminance using the formula:
                    // L = alpha * (.299*R + .587*G + .114*B)
                    //Note that ic is the input column and oc is the output column
                    for (r = 0; r < h; r++)
                        for (ic = oc = 0; oc < w; ic += 4, ++oc)
                            outputPtr[r * outputStride + oc] = (byte)(int)
                                ((bmpPtr[r * bmpStride + ic] / 255.0f) *
                                (0.299f * bmpPtr[r * bmpStride + ic + 1] +
                                 0.587f * bmpPtr[r * bmpStride + ic + 2] +
                                 0.114f * bmpPtr[r * bmpStride + ic + 3]));
                }
            }

            //Unlock the images
            bmp.UnlockBits(bmpData);
            output.UnlockBits(outputData);

            return output;
        }

        private void cb_setfront_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FontDialog font_text = new System.Windows.Forms.FontDialog();
            font_text.ShowColor = true;
            if (font_text.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                myFont = font_text.Font;
                myWatermarkColor = font_text.Color;
                txt_text.FontSize = font_text.Font.Size;
                FontFamily fontFamily = new FontFamily(font_text.Font.Name);
                txt_text.FontFamily = fontFamily;
                txt_text.Foreground = new SolidColorBrush(Color.FromScRgb(font_text.Color.A, font_text.Color.R, font_text.Color.G, font_text.Color.B));
            }
        }
        string ls_imgextension = "";
        private void cb_saveas_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ls_imgextension = System.IO.Path.GetExtension(txt_path.Text);
                ls_imgextension = ls_imgextension.ToUpper();
                ls_imgextension = ls_imgextension.Remove(0, 1);
                System.Windows.Forms.SaveFileDialog fd_saveimg = new System.Windows.Forms.SaveFileDialog();
                fd_saveimg.Title = "Save File";
                fd_saveimg.DefaultExt = ls_imgextension;
                fd_saveimg.Filter = ls_imgextension + " Image Files|*." + ls_imgextension;
                fd_saveimg.FilterIndex = 1;
                if (fd_saveimg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (fd_saveimg.FileName == "")
                    {
                        MessageBox.Show("Please specify the file name", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    else
                    {
                        // save the file with the name supplied by the user
                       finalBitmap.Save(fd_saveimg.FileName);
                        MessageBox.Show("Image  saved.", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                    }


                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
