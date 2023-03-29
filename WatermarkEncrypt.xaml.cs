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
using System.IO;
using System.Threading;
namespace WPFWatermarking
{
    /// <summary>
    /// Interaction logic for WatermarkEncrypt.xaml
    /// </summary>
    public partial class WatermarkEncrypt : UserControl
    {
        public delegate void ClosedHandler(object sender, EventArgs e);
        public event ClosedHandler CloseImagetoEncrypt;
        public WatermarkEncrypt()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            bmp_cov1 = null;
            bmp_cov2 = null;
            img_cov1 = null;
            img_cov2 = null;
            lb_imgORGByteArray = null;
            lb_imgWTRByteArray = null;
            ls_ORGFilename = "";
            ls_WTRFilename = "";
            finalBitmap = null;
            imgSource = null;
            uri = null;
            li_count = 0;
            lb_threadState = false;
            txt_encryptKey.Text = "";
            img_original = null;
            img_preview = null;
            img_watermark = null;
            CloseImagetoEncrypt(this, e);
        }
        System.Drawing.Bitmap bmp_cov1, bmp_cov2;
        System.Drawing.Image img_cov1, img_cov2;
        byte[] lb_imgORGByteArray = null;
        byte[] lb_imgWTRByteArray = null;
        String ls_ORGFilename = "";
        String ls_WTRFilename = "";
        PixelMixer class_pixelmixer = new PixelMixer();
        System.Drawing.Bitmap finalBitmap = null;
        ImageSource imgSource = null;
        int li_count = 0;
        Uri uri = null;
        private void cb_browseorg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog();
                fileDialog.Filter = " Image Files (*.bmp, *.jpg,*.gif)|*.bmp;*.jpg;*.gif";
                if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {    
                    ls_ORGFilename=fileDialog.FileName;
                    img_cov1 = System.Drawing.Image.FromFile(ls_ORGFilename);
                    Uri uri = new Uri(ls_ORGFilename, UriKind.Absolute);
                    ImageSource imgSource = new BitmapImage(uri);
                    img_original.Source = imgSource;
                    bmp_cov1 = new System.Drawing.Bitmap(new System.Drawing.Bitmap(img_cov1));

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cb_watermark_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog();
                fileDialog.Filter = " Image Files (*.bmp, *.jpg,*.gif)|*.bmp;*.jpg;*.gif";
                if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ls_WTRFilename = fileDialog.FileName;
                    img_cov2 = System.Drawing.Image.FromFile(ls_WTRFilename);
                    Uri uri = new Uri(ls_WTRFilename, UriKind.Absolute);
                    ImageSource imgSource = new BitmapImage(uri);
                    img_watermark.Source = imgSource;
                    bmp_cov2 = new System.Drawing.Bitmap(new System.Drawing.Bitmap(img_cov2));
                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        Thread _thread;
     
        private void cb_preview_Click(object sender, RoutedEventArgs e)
        {

            if (txt_encryptKey.Text.Equals("") && string.IsNullOrEmpty(txt_encryptKey.Text))
            {
                MessageBox.Show("Please give your unique key", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (bmp_cov1.Width > bmp_cov2.Width && bmp_cov1.Height > bmp_cov2.Height)
                {
                    lb_threadState = true;
                    ls_encryptText = txt_encryptKey.Text;
                    opacity = (int)slider_opacity.Value;
                    _thread = new Thread(new ThreadStart(GetImage));
                    _thread.Start();
                    _thread.IsBackground = true;
                }
                else
                {
                    MessageBox.Show("Watermark model is bigger than original imahe", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                }
              
            }
        }
        bool lb_threadState = true;
        int x = 0, y = 0;
        int k = 0, l = 0;

        Random random1 = new Random();
        Random random2 = new Random();
        StreamWriter stream_writer;
        FileStream MyFileStream;
        String ls_encryptText = "";
        int opacity = 0;
        private void GetImage()
        {
           
            while (lb_threadState)
            {
                try
                {
                    li_count++;
                    lb_threadState = false;
                    FileInfo info = new FileInfo(ls_WTRFilename);
                    System.Windows.Threading.DispatcherOperation
          dispatcherOp = img_preview.Dispatcher.BeginInvoke(
          System.Windows.Threading.DispatcherPriority.Normal,
          new Action(
            delegate()
            {
               
                img_preview.Source = null;
              
               
            }
                 ));
      
                    FileInfo infoFile = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "\\" + ls_encryptText + ".txt");
                    if (!infoFile.Exists)
                        infoFile.Delete();
                  
                MyFileStream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\" + ls_encryptText + ".txt", FileMode.Create, FileAccess.Write, FileShare.None);
                stream_writer = new StreamWriter(MyFileStream);
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(bmp_cov1);
               
                for (int i = 0; i < bmp_cov2.Width; i++)
                {

                    for (int j = 0; j < bmp_cov2.Height; j++)
                    {
                        try
                        {
                            // k=random1.Next(
                            k = random1.Next(i, bmp_cov1.Width);
                            l = random2.Next(j, bmp_cov1.Height);
                            
                            // Console.WriteLine(i + "i" + j + "j," + k + " " + l);
                            System.Drawing.Color image1Pixel = bmp_cov2.GetPixel(i, j), image2Pixel = bmp_cov1.GetPixel(k, l);
                            System.Drawing.Color write = System.Drawing.Color.FromArgb(image1Pixel.R, image1Pixel.G, image1Pixel.B);
                            System.Drawing.Color newColor = System.Drawing.Color.FromArgb((image1Pixel.R * opacity + image2Pixel.R * (100 - opacity)) / 100,
                                                            (image1Pixel.G * opacity + image2Pixel.G * (100 - opacity)) / 100,
                                                            (image1Pixel.B * opacity + image2Pixel.B * (100 - opacity)) / 100);


                         
                            stream_writer.WriteLine(k.ToString() + "," + l.ToString() + ";" + image2Pixel.R.ToString() + "," + image2Pixel.G.ToString() + "," + image2Pixel.B.ToString() + ";" + i.ToString() + "," + j.ToString() + ";" + image1Pixel.R.ToString() + "," + image1Pixel.G.ToString() + "," + image1Pixel.B.ToString());

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
                if (!(Directory.Exists("D:\\Image")))
                {
                    Directory.CreateDirectory("D:\\Image");
                }
              //  finalBitmap = new System.Drawing.Bitmap(new System.Drawing.Bitmap(final));
                bmp.Save("D:\\Image\\Test" + li_count + ".bmp");
                finalBitmap = bmp;
                    
                
                    dispatcherOp = img_preview.Dispatcher.BeginInvoke(
          System.Windows.Threading.DispatcherPriority.Normal,
          new Action(
            delegate()
            {
                uri = new Uri("D:\\Image\\Test" + li_count + ".bmp", UriKind.Absolute);
                imgSource = new BitmapImage(uri); 
                img_preview.Source = imgSource;
               
            }
                 ));
                
                   
                }
                catch (Exception ex)
                {
                    
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void cb_close_Click(object sender, RoutedEventArgs e)
        {
            bmp_cov1 = null;
            bmp_cov2 = null;
            img_cov1 = null;
            img_cov2 = null;
            lb_imgORGByteArray = null;
            lb_imgWTRByteArray = null;
            ls_ORGFilename = "";
            ls_WTRFilename = "";
            finalBitmap = null;
            imgSource = null;
            uri = null;
            li_count = 0;
            lb_threadState = false;
            txt_encryptKey.Text = "";
            img_original = null;
            img_preview = null;
            img_watermark = null;
            CloseImagetoEncrypt(this, e);
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
        }
        string ls_imgextension = "";
        private void cb_saveas_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ls_imgextension = System.IO.Path.GetExtension(ls_ORGFilename);
                ls_imgextension = ls_imgextension.ToUpper();
                ls_imgextension = ls_imgextension.Remove(0, 1);
                System.Windows.Forms.SaveFileDialog sfd_saveimg = new System.Windows.Forms.SaveFileDialog();
                sfd_saveimg.Title = "Save File";
                sfd_saveimg.DefaultExt = ls_imgextension;
                sfd_saveimg.Filter = ls_imgextension + " Image Files|*." + ls_imgextension;
                sfd_saveimg.FilterIndex = 1;
                if (sfd_saveimg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (sfd_saveimg.FileName == "")
                    {
                        MessageBox.Show("Please specify the file name", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    else
                    {
                        // save the file with the name supplied by the user
                        finalBitmap.Save(sfd_saveimg.FileName);
                        MessageBox.Show("Image  saved.", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
