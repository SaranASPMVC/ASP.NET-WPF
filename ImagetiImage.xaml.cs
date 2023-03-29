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
namespace WPFWatermarking
{
    /// <summary>
    /// Interaction logic for ImagetiImage.xaml
    /// </summary>
    public partial class ImagetiImage : UserControl
    {
        public delegate void ClosedHandler(object sender, EventArgs e);
        public event ClosedHandler CloseImagetoImage;
        public ImagetiImage()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            lb_imgORGByteArray = null;
            ls_ORGFilename = "";
            ls_WTRFilename = "";
            coverImg = null;
            watermarkImg = null;
            watermarkBmp = null;
            coverBmp = null;
            resultBmp = null;
            imgSource = null;
            uri = null;
            finalBitmap = null;
            lb_imgWTRByteArray = null;
            img_preview.Source = null;
            img_original.Source = null;
            img_watermark.Source = null;
            CloseImagetoImage(this, e);
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
                    lb_imgWTRByteArray = File.ReadAllBytes(fileDialog.FileName);
                    Uri uri = new Uri(ls_WTRFilename, UriKind.Absolute);
                    ImageSource imgSource = new BitmapImage(uri);
                    img_watermark.Source = imgSource;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        byte[] lb_imgORGByteArray = null;
        byte[] lb_imgWTRByteArray = null;
        String ls_ORGFilename = "";
        String ls_WTRFilename = "";
        private System.Drawing.Image coverImg;
        private System.Drawing.Image watermarkImg;
        private System.Drawing.Bitmap watermarkBmp;
        private System.Drawing.Bitmap coverBmp;
        private System.Drawing.Bitmap resultBmp = null;
        ImageSource imgSource = null;
        int li_count = 0;
        Uri uri = null;
        System.Drawing.Bitmap finalBitmap = null;
        private void cb_browseorg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog();
                fileDialog.Filter = " Image Files (*.bmp, *.jpg,*.gif)|*.bmp;*.jpg;*.gif";
                if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ls_ORGFilename = fileDialog.FileName;
                    lb_imgORGByteArray = File.ReadAllBytes(fileDialog.FileName);
                    Uri uri = new Uri(ls_ORGFilename, UriKind.Absolute);
                    ImageSource imgSource = new BitmapImage(uri);
                    img_original.Source = imgSource;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cb_close_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lb_imgORGByteArray = null;
                ls_ORGFilename = "";
                ls_WTRFilename = "";
                lb_imgWTRByteArray = null;
                coverImg = null;
                watermarkImg = null;
                watermarkBmp = null;
                coverBmp = null;
                resultBmp = null;
                imgSource = null;
                uri = null;
                finalBitmap = null;
                img_preview.Source = null;
                img_original.Source = null;
                img_watermark.Source = null;
                CloseImagetoImage(this, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
       
        private void cb_preview_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                li_count++;
                coverImg = System.Drawing.Image.FromFile(ls_ORGFilename);
                watermarkImg = System.Drawing.Image.FromFile(ls_WTRFilename);
                int ii_x = 0, ii_y = 0;
                ii_x = System.Convert.ToInt32(txt_xposition.Text);
                ii_y = System.Convert.ToInt32(txt_yposition.Text);
                System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(coverImg);
                System.Drawing.Bitmap TransparentLogo = new System.Drawing.Bitmap(watermarkImg.Width, watermarkImg.Height);
                System.Drawing.Graphics TGraphics = System.Drawing.Graphics.FromImage(TransparentLogo);
                System.Drawing.Imaging.ColorMatrix ColorMatrix = new System.Drawing.Imaging.ColorMatrix();
                ColorMatrix.Matrix33 = 1F;
                System.Drawing.Imaging.ImageAttributes ImgAttributes = new System.Drawing.Imaging.ImageAttributes();
                ImgAttributes.SetColorMatrix(ColorMatrix, System.Drawing.Imaging.ColorMatrixFlag.Default, System.Drawing.Imaging.ColorAdjustType.Bitmap);
                TGraphics.DrawImage(watermarkImg, new System.Drawing.Rectangle(0, 0, TransparentLogo.Width, TransparentLogo.Height), 0, 0, TransparentLogo.Width, TransparentLogo.Height, System.Drawing.GraphicsUnit.Pixel, ImgAttributes);
                TGraphics.Dispose();
                g.DrawImage(TransparentLogo, ii_x, ii_y);
                if (!(Directory.Exists("D:\\Image")))
                {
                    Directory.CreateDirectory("D:\\Image");
                }
                finalBitmap = new System.Drawing.Bitmap(new System.Drawing.Bitmap(coverImg));
                finalBitmap.Save("D:\\Image\\Test" + li_count + ".bmp");

                uri = new Uri("D:\\Image\\Test" + li_count + ".bmp", UriKind.Absolute);
                imgSource = new BitmapImage(uri);
                img_preview.Source = imgSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        string ls_imgextension="";
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
