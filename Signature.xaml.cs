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
using System.Windows.Shapes;
using System.IO;
namespace WPFWatermarking
{
    /// <summary>
    /// Interaction logic for Signature.xaml
    /// </summary>
    public partial class Signature : Window
    {
        public Signature()
        {
            InitializeComponent();
        }
        private Model DataModel
        {
            get { return this.Resources["gmodel"] as Model; }
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MemoryStream memstream = this.DataModel.GenerateImage(inkCanvas1, (int)inkCanvas1.ActualWidth, (int)inkCanvas1.ActualHeight, ImageFormat.JPG);
            byte[] test = memstream.ToArray();
            System.Drawing.Image im = System.Drawing.Image.FromStream(new MemoryStream(test));
            System.Drawing.Bitmap imgSignature = new System.Drawing.Bitmap(new System.Drawing.Bitmap(im));
            imgSignature.Save("d://Sig.jpeg");
        }
        //private byte[] Trim(byte[] array)
        //{
        //    byte[] result=null;
        //    for (int i = 0; i < array.Length; i++)
        //    {
        //        if (array[i] != 0x00)
        //        {
        //            result = new byte[array.Length - i];
        //            Buffer.BlockCopy(array, i, result, 0, result.Length);
        //            return result;
        //        }
        //    }
        //    return null;
        //}


        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
