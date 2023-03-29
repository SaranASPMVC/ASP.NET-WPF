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
using ImagingDevice;
using System.IO;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Threading;
using System.Threading;
using System.Net;
using System.Net.Sockets;
namespace WPFWatermarking
{
    /// <summary>
    /// Interaction logic for WatermarkingServer.xaml
    /// </summary>
    public partial class WatermarkingServer : UserControl
    {
        public delegate void ClosedHandler(object sender, EventArgs e);
        public event ClosedHandler CloseWaterMarkingServer;
        private DispatcherTimer timer;
        public WatermarkingServer()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer1_Tick;

        }
        private void timer1_Tick(object sender, EventArgs e)
        {

            try
            {
                //MessageBox.Show(cmb_iplist.SelectedItem.ToString());
                Start_Sending_Video_Conference(6000);
            }
            catch (Exception ex)
            {
                timer.Stop();
                MessageBox.Show(ex.ToString(), "Alert", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            for(int i=0;i<ServerDetails.obj_networkingData.Count;i++)
            {
                bool lb_found = false;
                NetworkingData obj_data = ServerDetails.obj_networkingData.ElementAt<NetworkingData>(i);
                for (int k = 0; k < cmb_remoteSystem.Items.Count; k++)
                {
                    ComboBoxItem cmbItem = (ComboBoxItem)cmb_remoteSystem.Items.GetItemAt(k);
                    if (obj_data.ipaddress == cmbItem.Content.ToString())
                    {
                        lb_found = true;
                        break;
                    }

                }
                if (!lb_found)
                {
                    ComboBoxItem cmb_item = new ComboBoxItem();
                    cmb_item.Content = obj_data.ipaddress;
                    cmb_remoteSystem.Items.Add(cmb_item);
                }
            }
           


        }
        public TcpClient myclient;
        public MemoryStream ms;
        public NetworkStream myns;
        public BinaryWriter mysw;
        public Thread myth;
        public TcpListener mytcpl;
        public Socket mysocket;
        NetworkStream ns;
        int li_opacity = 125;
        private void Start_Sending_Video_Conference(int port_number)
        {
            try
            {
                //MessageBox.Show(remote_IP);
                    ms = new MemoryStream();
                    System.Drawing.Bitmap bmap = new System.Drawing.Bitmap(new System.Drawing.Bitmap(userUploadImage));
                    System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmap);

                    System.Drawing.Brush mybrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(li_opacity, myWatermarkColor));
                    System.Drawing.SizeF sz = g.MeasureString(txt_copyright.Text, myFont);
                    g.DrawString(txt_copyright.Text, myFont, mybrush, new System.Drawing.Point(System.Convert.ToInt32(txt_xposition.Text), System.Convert.ToInt32(txt_yposition.Text)));

                    bmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
               
               
                byte[] arrImage = ms.GetBuffer();
                ComboBoxItem cmbItem = (ComboBoxItem)cmb_remoteSystem.SelectedItem;

                myclient = new TcpClient(cmbItem.Content.ToString(), port_number);//Connecting with server
                myns = myclient.GetStream();
                mysw = new BinaryWriter(myns);
                mysw.Write(arrImage);//send the stream to above address
                ms.Flush();
                mysw.Flush();
                myns.Flush();
                ms.Close();
                mysw.Close();
                myns.Close();
                myclient.Close();
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.ToString(), "Video Conference Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            cmb_remoteSystem.Items.Clear();
            if (_grabber != null)
            {
                _grabber.Stop();

            }
            CloseWaterMarkingServer(this, e);
        }

        private void cb_stopcapture_Click(object sender, RoutedEventArgs e)
        {
            if (_grabber != null)
            {
                _grabber.Stop();
               
            }
            cb_startCapture.IsEnabled = true;
        }
        private void grabber_ImageCaptured(object source, ImageGrabberEventArgs e)
        {
            
            img_original.Source = e.DeviceImage.ToBitmapSource();

            userUploadImage = (System.Drawing.Bitmap)e.DeviceImage;
        }
        System.Drawing.Image userUploadImage = null;
        public void grabImage()
        {
            System.Windows.Threading.DispatcherOperation
       dispatcherOp = img_original.Dispatcher.BeginInvoke(
       System.Windows.Threading.DispatcherPriority.Normal,
       new Action(
         delegate()
         {

             var hwndSource = PresentationSource.FromVisual(img_original) as HwndSource;
             if (hwndSource != null)
             {
                 _hwnd = hwndSource.Handle;
                 _grabber = new ImageGrabber(_hwnd.ToInt32());
                 _grabber.ImageCaptured += grabber_ImageCaptured;
                 _grabber.Start();

                 dispatcherOp = cb_startCapture.Dispatcher.BeginInvoke(
                 System.Windows.Threading.DispatcherPriority.Normal,
                 new Action(
                   delegate()
                   {

                       cb_startCapture.IsEnabled = false;


                   }
                        ));


             }

         }
              ));
      
           
           
        }
        Thread thread;
        private void cb_startCapture_Click(object sender, RoutedEventArgs e)
        {

            thread = new Thread(new ThreadStart(grabImage));
            thread.Start();
        }
        private ImageGrabber _grabber;
        private IntPtr _hwnd = IntPtr.Zero;
        System.Drawing.Font myFont;
        System.Drawing.Color myWatermarkColor;
        DirectoryInfo dir = null;
        private void cb_setfromt_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FontDialog font_text = new System.Windows.Forms.FontDialog();
            font_text.ShowColor = true;
            if (font_text.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                myFont = font_text.Font;
                myWatermarkColor = font_text.Color;
                txt_copyright.FontSize = font_text.Font.Size;
                FontFamily fontFamily = new FontFamily(font_text.Font.Name);
                txt_copyright.FontFamily = fontFamily;
                txt_copyright.Foreground = new SolidColorBrush(Color.FromScRgb(font_text.Color.A, font_text.Color.R, font_text.Color.G, font_text.Color.B));
            }
        }

        private void cb_close_Click(object sender, RoutedEventArgs e)
        {
            if (_grabber != null)
            {
                _grabber.Stop();

            }
            cmb_remoteSystem.Items.Clear();
            CloseWaterMarkingServer(this, e);
        }

        private void cb_startsending_Click(object sender, RoutedEventArgs e)
        {           
            timer.Start();
        }

        private void cmb_opacity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem itm = (ComboBoxItem)cmb_opacity.SelectedItem;
            switch (itm.Content.ToString())
            {
                case"1":
                    {
                        li_opacity=1*255;
                        break;
                    }
                case ".75":
                    {
                        li_opacity = System.Convert.ToInt32(.75 * 255);
                        break;
                    }
                case ".5":
                    {
                        li_opacity = System.Convert.ToInt32(.5 * 255);
                        break;
                    }
                case ".25":
                    {
                        li_opacity = System.Convert.ToInt32(.25 * 255);
                        break;
                    }
                case ".10":
                    {
                        li_opacity = System.Convert.ToInt32(.10 * 255);
                        break;
                    }
            }
        }
    }
    public static class ExtensionMethods
    {
        /// <summary>
        /// Converts a <see cref="System.Drawing.Image"/> into a WPF <see cref="BitmapSource"/>.
        /// </summary>
        /// <param name="source">The source image.</param>
        /// <returns>A BitmapSource</returns>
        public static BitmapSource ToBitmapSource(this System.Drawing.Image source)
        {
            var bitmap = new System.Drawing.Bitmap(source);

            var bitSrc = bitmap.ToBitmapSource();

            bitmap.Dispose();

            return bitSrc;
        }

        /// <summary>
        /// Converts a <see cref="System.Drawing.Bitmap"/> into a WPF <see cref="BitmapSource"/>.
        /// </summary>
        /// <remarks>Uses GDI to do the conversion. Hence the call to the marshalled DeleteObject.
        /// </remarks>
        /// <param name="source">The source bitmap.</param>
        /// <returns>A BitmapSource</returns>
        public static BitmapSource ToBitmapSource(this System.Drawing.Bitmap source)
        {
            BitmapSource bitSrc;

            IntPtr hBitmap = source.GetHbitmap();

            try
            {
                bitSrc = Imaging.CreateBitmapSourceFromHBitmap(
                    hBitmap,
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());
            }
            catch (Win32Exception)
            {
                bitSrc = null;
            }
            finally
            {
                NativeMethods.DeleteObject(hBitmap);
            }

            return bitSrc;
        }

        #region Nested type: NativeMethods

        /// <summary>
        /// FxCop requires all Marshalled functions to be in a class called NativeMethods.
        /// </summary>
        internal static class NativeMethods
        {
            [DllImport("gdi32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool DeleteObject(IntPtr hObject);
        }

        #endregion
    }

}
