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
using System.Threading;
using System.Collections.ObjectModel;

namespace WPFWatermarking
{
    /// <summary>
    /// Interaction logic for ServerDetails.xaml
    /// </summary>
    public partial class ServerDetails : UserControl
    {
        public delegate void ClosedHandler(object sender, EventArgs e);
        public event ClosedHandler CloseServerDetails;
        public Thread TcpListners;
        //UDP Sending 	
        public const int TcpServerPort = 4567;
        public const int TcpClientPort = 4568;
        public const int sampleUdpPort = 4569;
        public String ssss;
        public int MsgFlag = 1;
        public int udpflag = 0;
        public bool tcpServerflag = false;
        private Socket soUdpSender = null;
        private Socket soUdpListner = null;
        public Thread UdpListners;
        public Thread UdpSenders;
        ObservableCollection<NetworkingData> IPCollection=new ObservableCollection<NetworkingData>();
        public ServerDetails()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            IPCollection.Clear();
            lv_serverdetails.Items.Clear();
            lb_runServer = false;
             MsgFlag = 1;
          udpflag = 0;
         tcpServerflag = false;
         soUdpSender = null;
         soUdpListner = null;
         UdpListners=null;
          UdpSenders=null;
         Ipaddress = ""; host = "";        
         txt_ipaddress.Text = "";
         txt_ipaddress.Text = "";
         
        CloseServerDetails(this, e);
        }

        private void cb_close_Click(object sender, RoutedEventArgs e)
        {
            IPCollection.Clear();
            lv_serverdetails.Items.Clear();
            lb_runServer = false;
            MsgFlag = 1;
            udpflag = 0;
            tcpServerflag = false;
            soUdpSender = null;
            soUdpListner = null;
            UdpListners = null;
            UdpSenders = null;
            Ipaddress = ""; host = "";           
            txt_ipaddress.Text = "";
            txt_ipaddress.Text = "";
          
            CloseServerDetails(this, e);
        }
        string Ipaddress = "", host = "";
        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                host = Dns.GetHostName();
                System.Net.IPHostEntry myIPs =Dns.GetHostEntry(host);
                if (myIPs.AddressList.Length >= 2)
                {
                    Ipaddress = Dns.GetHostEntry(host).AddressList[1].ToString();

                }
                else
                {
                    Ipaddress = Dns.GetHostByName(host).AddressList[0].ToString();

                }
                txt_ipaddress.Text = Ipaddress;
                txt_systemname.Text = host;
            }
            catch (Exception ex)
            {
              
            }
        }

        private void cb_startserver_Click(object sender, RoutedEventArgs e)
        {
            if (udpflag == 0)
            {
                lb_runServer = true;
                udpflag = 1;
                lv_serverdetails.Items.Clear();
                // create a UDP socket to rcive data
                CreateUDPReceiverThread();
                // Create UDP socket to Send data
                CreateUDPSenderThread();
            }
        }

        public void CreateUDPReceiverThread()
        {
            try
            {
                UdpListners = new Thread(new ThreadStart(StartReceiveFromUdp));
                UdpListners.IsBackground = true;
                UdpListners.Start();
            }
            catch (Exception se)
            {
                WriteMessage("Error UDP Socket creation :!" + se.Message);

            }
        }

        public void CreateUDPSenderThread()
        {
            try
            {
                UdpSenders = new Thread(new ThreadStart(StartUdpSending));
                UdpSenders.IsBackground = true;
                UdpSenders.Start();
            }
            catch (Exception se)
            {
                WriteMessage("Error Sending UDP socket :" + se.Message);
            }
        }
        bool lb_runServer = true;
        public void StartUdpSending()
        {
            // string Ipaddress;
            // string host;
           
                string Sendstring;
                byte[] data;
                while (lb_runServer)
                {
                    try
                    {
                        // Construct the message that is to be brodcasted
                        // host = Dns.GetHostName();
                        // System.Net.IPHostEntry myIPs = System.Net.Dns.GetHostByName(host);
                       //  Ipaddress = txt_ipaddress.Text;
                        Sendstring = host + "=" + Ipaddress;
                       // MessageBox.Show("send"+Sendstring);
                        // MessageBox.Show(Sendstring);
                        WriteMessage("UDP Message Broadcasted : " + Sendstring);
                        // Crate a UDP socket
                        soUdpSender = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                        IPEndPoint iep = new IPEndPoint(IPAddress.Broadcast, sampleUdpPort);
                        // Convr string to a byte streem
                        data = Encoding.ASCII.GetBytes(Sendstring);
                        soUdpSender.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
                        try
                        {
                            soUdpSender.SendTo(data, iep);
                        }
                        catch (Exception se)
                        {
                            WriteMessage("Error during broadcast :" + se.ToString());
                        }
                        Thread.Sleep(1000);
                        WriteMessage("Send Successfully");
                        soUdpSender.Close();
                    }
                    catch (Exception se)
                    {
                        WriteMessage("Error " + se.Message);
                    }
                }
           
        }
        public static ObservableCollection<NetworkingData> obj_networkingData = new ObservableCollection<NetworkingData>();
        public void StartReceiveFromUdp()
        {
           
                IPHostEntry localHostEntry;
                string dataReceived = "";

                IPEndPoint localIpEndPoint, tmpIpEndPoint;
                Byte[] received = new Byte[1024];
                EndPoint remoteEP;
                int bytesReceived;
                try
                {
                    //Get the Localhost
                    localHostEntry = Dns.GetHostByName(Dns.GetHostName());
                    // Start the accepting packets
                    soUdpListner = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    localIpEndPoint = new IPEndPoint(IPAddress.Any, sampleUdpPort);
                    soUdpListner.Bind(localIpEndPoint);
                    while (lb_runServer)
                    {
                        tmpIpEndPoint = new IPEndPoint(localHostEntry.AddressList[0], sampleUdpPort);
                        remoteEP = (tmpIpEndPoint);
                        bytesReceived = soUdpListner.ReceiveFrom(received, ref remoteEP);
                        dataReceived = System.Text.Encoding.ASCII.GetString(received);
                      //  MessageBox.Show("rcvd" + dataReceived);
                        // MessageBox.Show(dataReceived);
                        WriteMessage("UDP data receivecd : " + dataReceived);
                        this.displayserver(dataReceived);

                    }

                }
                catch (SocketException se)
                {
                    WriteMessage("SocketException : " + se.Message);

                }
                catch (Exception se)
                {
                    WriteMessage("SocketException : " + se.Message);

                }
                if (soUdpListner != null) 
                soUdpListner.Close();
           

        }
          public ObservableCollection<NetworkingData> AddressCollection
    { get { return IPCollection; } }

        public void displayserver(string servernameIP)
        {
          //  MessageBox.Show(servernameIP);
            string Servername = "";
            string Ipaddress = "";
            string[] words = servernameIP.Split('=');
            string[] store = new string[2];
            string str = "";
            int rowcount = lv_serverdetails.Items.Count;
            int k = 1;
            foreach (string word in words)
            {
                if (k == 1) { Servername = word; }
                if (k == 2) { Ipaddress = word; }
                k += 1;
                //MessageBox.Show(word);
                if (k >= 3) break;
            }
            Ipaddress = Ipaddress.Replace("\0", "");
            Ipaddress = Ipaddress.Trim();
            string[] Ipformat = Ipaddress.Split('.');
            if (Ipformat != null)
            {
                if (Ipformat.Length == 4)
                {
                    if (Ipformat[3].Length > 3)
                    {
                        Ipformat[3] = Ipformat[3].Substring(0, 3);
                        Ipaddress = Ipformat[0] + "." + Ipformat[1] + "." + Ipformat[2] + "." + Ipformat[3];
                    }
                }
            }
            for (int i = 0; i < rowcount; i++)
            {
              

               NetworkingData obj_networkingData = IPCollection.ElementAt<NetworkingData>(i);
               if (obj_networkingData.ipaddress.Equals(Ipaddress))
               {
                   return;
               }              
            }
            string[] Ipaddr = new string[3];
            rowcount++;
            NetworkingData obj_ipdetails = new NetworkingData();
            obj_ipdetails.slno=rowcount;
            obj_ipdetails.hostname=Servername;           
            obj_ipdetails.ipaddress = Ipaddress;
            IPCollection.Add(obj_ipdetails);
            ServerDetails.obj_networkingData.Add(obj_ipdetails);
            //Ipaddr[0] = rowcount.ToString();
            //// MessageBox.Show(Servername);
          
            //Ipaddr[2] = Ipaddress;
           // System.Windows.Forms.ListViewItem TempItem = new System.Windows.Forms.ListViewItem(Ipaddr);
            System.Windows.Threading.DispatcherOperation   dispatcherOps = lv_serverdetails.Dispatcher.BeginInvoke(
       System.Windows.Threading.DispatcherPriority.Normal,
       new Action(
         delegate()
         {

             this.lv_serverdetails.Items.Add(obj_ipdetails);


         }
              ));
               
          


        }
        private void WriteMessage(String str)
        {
            try
            {             

                  
                    txt_log.Text += Environment.NewLine;
                    txt_log.AppendText(str);
              
            }
            catch (Exception ex)
            {
                
            }
        }

        private void cb_stopdetection_Click(object sender, RoutedEventArgs e)
        {
            if (udpflag == 1)
            {
                udpflag = 0;
                txt_log.Text = "";
                udpflag = 0;
                if(UdpListners.IsAlive)
                       UdpListners.Abort();
                if (UdpSenders.IsAlive)
                      UdpSenders.Abort();
                
                soUdpSender.Close();
                soUdpListner.Close();
                lb_runServer = false;
            }

        }
    }
}
