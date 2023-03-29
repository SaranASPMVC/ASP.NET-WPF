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
    /// Interaction logic for Extractwatermark.xaml
    /// </summary>
    public partial class Extractwatermark : UserControl
    {
        public delegate void ClosedHandler(object sender, EventArgs e);
        public event ClosedHandler CloseExtractWatermark;
        public Extractwatermark()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            ls_path = "";
            img_cov1 = null;
            img_cov2 = null;
            ls_cov1 = "";
            ls_cov2 = "";
            bmp_cov1 = null;
            img_cov2 = null;
            ls_color = "";
            lsa_mix=null;
            lia_cordintes=null;
           lsa_rgb=null;
           lsa_baseCordinate=null;
           li_count = 0;
           img_original = null;
           img_watermarked = null;
           imgSource = null;
           uri = null;
            CloseExtractWatermark(this, e);
        }
        System.Drawing.Image img_cov1, img_cov2;
        string ls_cov1 = "", ls_cov2 = "";
        System.Drawing.Bitmap bmp_cov1, bmp_cov2;
        private void cb_browseorg_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog();
                fileDialog.Filter = " Image Files (*.bmp, *.jpg,*.gif)|*.bmp;*.jpg;*.gif";
                if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    img_cov1 = System.Drawing.Image.FromFile(fileDialog.FileName);
                    ls_cov1 = fileDialog.FileName;

                    bmp_cov1 = new System.Drawing.Bitmap(new System.Drawing.Bitmap(img_cov1));
                    li_width = bmp_cov1.Width;
                    li_height = bmp_cov1.Height;
                    Uri uri = new Uri(ls_cov1, UriKind.Absolute);
                    ImageSource imgSource = new BitmapImage(uri);
                    img_watermarked.Source = imgSource;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cb_close_Click(object sender, RoutedEventArgs e)
        {
            ls_path = "";
            img_cov1 = null;
            img_cov2 = null;
            ls_cov1 = "";
            ls_cov2 = "";
            bmp_cov1 = null;
            img_cov2 = null;
            ls_color = "";
            lsa_mix = null;
            lia_cordintes = null;
            lsa_rgb = null;
            lsa_baseCordinate = null;
            li_count = 0;
            img_original = null;
            img_watermarked = null;
            imgSource = null;
            uri = null;
            CloseExtractWatermark(this, e);
        }
        String ls_path = "";
        Thread _thread;
        private void cb_preview_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                img_original.Source = null;
                ls_path =AppDomain.CurrentDomain.BaseDirectory+ "\\" + txt_encryptkey.Text + ".txt";
                FileInfo info = new FileInfo(ls_path);
                if (info.Exists)
                {
                    _thread = new Thread(new ThreadStart(get));
                    _thread.Start();
                    _thread.IsBackground = true;
                }
                else
                    MessageBox.Show("Sorry wrong password");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void get()
        {
            ComparePixel(bmp_cov1);
        }
        StreamReader stream_reader;
        string ls_color = "";
       
        string[] lsa_mix;
        string[] lia_cordintes;
        string[] lsa_rgb;
        string[] lsa_baseCordinate;
        int li_count = 0;
        int li_width = -1;
        int li_height = -1;
        private void ComparePixel(System.Drawing.Image _watermarked)
        {
            try
            {
                li_count++;
                lsa_mix = new string[3];
                int x = -1, y = -1;
                int R = -1, G = -1, B = -1;
                int li_R = -1, li_G = -1, li_B = -1;
                string is_coordinate1 = "";
                string is_coordinate2 = "", ls_cordinate3 = "";
                string is_color = "";
                lia_cordintes = new string[2];
                System.Drawing.Bitmap watermarkedbmp = new System.Drawing.Bitmap(new System.Drawing.Bitmap(_watermarked));
                System.Drawing.Bitmap final = new System.Drawing.Bitmap(li_width,li_height);
                stream_reader = new StreamReader(ls_path);                
                int h = 0, k = 0;
                while ((ls_color = stream_reader.ReadLine()) != null)
                {
                    //xt_log.Text += Environment.NewLine;    

                    if (!ls_color.Equals("") && !string.IsNullOrEmpty(ls_color))
                    {
                        lsa_mix = ls_color.Split(';');

                        is_coordinate1 = lsa_mix[0];

                        is_coordinate2 = lsa_mix[2];

                        ls_cordinate3 = lsa_mix[1];

                        is_color = lsa_mix[3];

                        lsa_rgb = is_color.Split(',');
                        lsa_baseCordinate = ls_cordinate3.Split(',');
                        //MessageBox.Show(is_color+"watermarkRGB");
                        R = System.Convert.ToInt32(lsa_rgb[0]);
                        G = System.Convert.ToInt32(lsa_rgb[1]);
                        B = System.Convert.ToInt32(lsa_rgb[2]);
                        lia_cordintes = is_coordinate1.Split(',');
                        //   MessageBox.Show(ls_cordinate3+"BaseRGB");
                        li_R = System.Convert.ToInt32(lsa_baseCordinate[0]);
                        li_G = System.Convert.ToInt32(lsa_baseCordinate[1]);
                        li_B = System.Convert.ToInt32(lsa_baseCordinate[2]);
                        //  MessageBox.Show(is_coordinate1+"WaterCordinate");
                        h = System.Convert.ToInt32(lia_cordintes[0]);
                        k = System.Convert.ToInt32(lia_cordintes[1]);
                        lia_cordintes = is_coordinate2.Split(',');
                        //  MessageBox.Show(is_coordinate2+"Base Coordinate");
                        x = System.Convert.ToInt32(lia_cordintes[0]);
                        y = System.Convert.ToInt32(lia_cordintes[1]);
                        System.Drawing.Color add = System.Drawing.Color.FromName(is_color);

                        if (x < watermarkedbmp.Width && y < watermarkedbmp.Height)
                        {


                            if (R == 0 && G == 0 && B == 0)
                            {

                            }
                            else
                            {
                                try
                                {
                                    System.Drawing.Color baseColor = System.Drawing.Color.FromArgb(li_R, li_G, li_B);
                                    System.Drawing.Color match = watermarkedbmp.GetPixel(h, k);
                                    System.Drawing.Color set = System.Drawing.Color.FromArgb(R, G, B);

                                    //MessageBox.Show(match + "test" + set);
                                    // textBox1.Text += Environment.NewLine;
                                    //   textBox1.Text += baseColor + "test" + match;
                                    if (match == baseColor)
                                        final.SetPixel(x, y, set);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.ToString(), "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                                    break;
                                }


                            }
                        }
                    }
                }

                if (!(Directory.Exists("D:\\Image")))
                {
                    Directory.CreateDirectory("D:\\Image");
                }
                final.Save("D:\\Image\\Teste" + li_count + ".bmp");
                System.Windows.Threading.DispatcherOperation
         dispatcherOp = img_original.Dispatcher.BeginInvoke(
         System.Windows.Threading.DispatcherPriority.Normal,
         new Action(
           delegate()
           {

               uri = new Uri("D:\\Image\\Teste" + li_count + ".bmp", UriKind.Absolute);
               imgSource = new BitmapImage(uri);
               img_original.Source = imgSource;

           }
                ));
               
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.ToString(), "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
              //  MessageBox.Show("Error in base iamge please give correct iamge", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        ImageSource imgSource = null;
        Uri uri = null;
    }
}
