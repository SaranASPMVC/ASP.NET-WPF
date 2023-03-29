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
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
namespace WPFWatermarking
{
    /// <summary>
    /// Interaction logic for WatermarkingClient.xaml
    /// </summary>
    public partial class WatermarkingClient : UserControl
    {
        public delegate void ClosedHandler(object sender, EventArgs e);
        public event ClosedHandler CloseWaterMarkingClient;
        public TcpClient myclient;
        public MemoryStream ms;
        public NetworkStream myns;
        public BinaryWriter mysw;
        public Thread myth;
        public TcpListener mytcpl;
        public Socket mysocket;
        NetworkStream ns;
        public WatermarkingClient()
        {
            InitializeComponent();
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            //if (myth.IsAlive)
            //    myth.Abort();
            if (mysocket != null)
                mysocket.Close();
            CloseWaterMarkingClient(this, e);
        }

        private void cb_close_Click(object sender, RoutedEventArgs e)
        {
            //if (myth.IsAlive)
            //    myth.Abort();
            mysocket.Close();
            CloseWaterMarkingClient(this, e);
        }
        public delegate void Calculate();
        private void cb_startlistening_Click(object sender, RoutedEventArgs e)
        {
            //Calculate videoConf = new Calculate(Start_Receiving_Video_Conference);
        //   videoConf.BeginInvoke(
            myth = new Thread(new System.Threading.ThreadStart(Start_Receiving_Video_Conference)); // Start Thread Session
            myth.Start(); //

        }
        int li_count = 0;
        private void Start_Receiving_Video_Conference()
        {
            try
            {
                li_count++;
                // Open The Port
                mytcpl = new TcpListener(6000);
                mytcpl.Start();						 // Start Listening on That Port
                mysocket = mytcpl.AcceptSocket();		 // Accept Any Request From Client and Start a Session
                ns = new NetworkStream(mysocket);	 // Receives The Binary Data From Port

                System.Drawing.Image image = System.Drawing.Image.FromStream(ns);

                System.Drawing.Bitmap bmp = (System.Drawing.Bitmap)image;
                bmp.Save("D://da" + li_count + ".bmp");
                System.Windows.Threading.DispatcherOperation
         dispatcherOp = image1.
         Dispatcher.BeginInvoke(
         System.Windows.Threading.DispatcherPriority.Send,
         new Action(
           delegate()
           {
               
               Uri uri = new Uri("D://da" + li_count + ".bmp", UriKind.Absolute);

               ImageSource imgSource = new BitmapImage(uri);
               image1.Source = imgSource;

             
           }
              ));

                // Close TCP Session
                mytcpl.Stop();
                if (mysocket.Connected == true)		     // Looping While Connected to Receive Another Message 
                {
                    while (true)
                    {
                        Start_Receiving_Video_Conference();				 // Back to First Method
                    }
                }
                else
                {


                }
                myns.Flush();

            }
            catch (Exception ex)
            {
                //myth.Abort(); }
            }
        }
    }
}
