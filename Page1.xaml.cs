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
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }
        public static string ls_usercode = "";
        public static string ls_usertype = "";
        void PopUpImagetotext_Close(object sender, EventArgs e)
        {
            imagetotext.IsOpen = false;

        }
        void PopUpImagetoImage_Close(object sender, EventArgs e)
        {
            imagetoimg.IsOpen = false;

        }
        void PopUpImagetoEncrypt_Close(object sender, EventArgs e)
        {
            imageEncrypt.IsOpen = false;

        }
        void PopUpExtractWatermark_Close(object sender, EventArgs e)
        {
            extractwatermark.IsOpen = false;

        }
        void PopUpServerDetails_Close(object sender, EventArgs e)
        {
            serverDetails.IsOpen = false;

        }
        void PopUpUserDetails_Close(object sender, EventArgs e)
        {
            userDetails.IsOpen = false;

        }
        void PopUpCreateBank_Close(object sender, EventArgs e)
        {
            bankDetails.IsOpen = false;

        }
        void PopUpBankProfile_Close(object sender, EventArgs e)
        {
            viewBankProfile.IsOpen = false;

        }
        void PopUpCreateAccount_Close(object sender, EventArgs e)
        {
            createAccount.IsOpen = false;

        }
        void PopUpViewAccount_Close(object sender, EventArgs e)
        {
            viewAccount.IsOpen = false;

        }
        void PopUpMoneyTransaction_Close(object sender, EventArgs e)
        {
            moneyTransaction.IsOpen = false;

        }
        void PopUpMoneyTransfer_Close(object sender, EventArgs e)
        {
            moneyTransfer.IsOpen = false;

        }
        void PopUpwatermarkingServer_Close(object sender, EventArgs e)
        {
            watermarkingServer.IsOpen = false;

        }
        void PopUpwatermarkingClient_Close(object sender, EventArgs e)
        {
            watermarkingClient.IsOpen = false;

        }
        void PopUpServerDetails_Close(object sender, EventArgs e,bool isadmin,bool status)
        {
            if (status)
            {
                login.IsOpen = false;
            }
            else
            {
                lbl_signin.Content = "SignOut";
                if (isadmin)
                {
                    menu_admin.Visibility = Visibility.Visible;
                    menu_user.Visibility = Visibility.Hidden;
                    grid_admin.Visibility = Visibility.Visible;
                    grid_user.Visibility = Visibility.Hidden;
                    menu_admin.IsEnabled = true;
                    menu_user.IsEnabled = false;
                    grid_admin.IsEnabled = true;
                    grid_user.IsEnabled = true;
                    login.IsOpen = false;
                }
                else
                {
                    menu_user.Visibility = Visibility.Visible;
                    menu_admin.Visibility = Visibility.Hidden;
                    grid_user.Visibility = Visibility.Visible;
                    grid_admin.Visibility = Visibility.Hidden;
                    menu_user.IsEnabled = true;
                    menu_admin.IsEnabled = false;
                    grid_user.IsEnabled = true;
                    grid_admin.IsEnabled = true;
                    login.IsOpen = false;
                }
            }

        }
        private void menu_watermark_Click(object sender, RoutedEventArgs e)
        {

        }

        private void menu1_Click(object sender, RoutedEventArgs e)
        {
             MenuItem menuItem = e.Source as MenuItem;
             switch (menuItem.Name)
             {
                 case "m_imagetotext":
                 case "m_uimagetotext":
                     {
                         imagetotext.IsOpen = true;
                         break;
                     }
                 case "m_imagetoimage":
                 case "m_uimagetoimage":
                     {
                         imagetoimg.IsOpen = true;
                         break;
                     }
                 case "m_extractwatermark":
                 case "m_uextractwatermark":
                     {
                         extractwatermark.IsOpen = true;
                         break;
                     }
                 case "m_encryptWater":
                 case "m_uencryptWater":
                     {
                         imageEncrypt.IsOpen = true;
                         break;
                     }
                 case "m_serverdetails":
                 case "m_userverdetails":
                     {
                         serverDetails.IsOpen = true;
                         break;
                     }
                 case "m_awtrserver":
                 case "m_wtrserver":
                     {
                         WaterMarkingSrv obj = new WaterMarkingSrv();
                         obj.Show();
                         break;
                     }
                 case "m_awtrclient":
                 case "m_wtrclient":
                     {
                         watermarkingClient.IsOpen = true;
                         break;
                     }
             }
        }

        private void acc_imgtext_Click(object sender, RoutedEventArgs e)
        {
            imagetotext.IsOpen = true;
        }

        private void acc_imgimg_Click(object sender, RoutedEventArgs e)
        {
            imagetoimg.IsOpen = true;
        }

        private void acc_imgpixel_Click(object sender, RoutedEventArgs e)
        {
            imageEncrypt.IsOpen = true;
        }

        private void acc_exctwater_Click(object sender, RoutedEventArgs e)
        {
            extractwatermark.IsOpen = true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
           
            grid_user.Visibility = Visibility.Hidden;
            grid_admin.Visibility = Visibility.Hidden;
            menu_user.IsEnabled = false;
            menu_admin.IsEnabled = false;
            grid_user.IsEnabled = false;
            grid_admin.IsEnabled = false;
            login.IsOpen = true;
        }

        private void acc_viewusers_Click(object sender, RoutedEventArgs e)
        {
            userDetails.IsOpen = true;
        }

        private void lbl_signin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lbl_signin.Content = "SignIn";
            menu_admin.Visibility = Visibility.Hidden;
            menu_user.Visibility = Visibility.Hidden;
            grid_admin.Visibility = Visibility.Hidden;
            grid_user.Visibility = Visibility.Hidden;
            login.IsOpen = true;
        }

        private void acc_createaccount_Click(object sender, RoutedEventArgs e)
        {
            createAccount.IsOpen = true;
        }

        private void acc_createbank_Click(object sender, RoutedEventArgs e)
        {
            bankDetails.IsOpen = true;
        }

        private void acc_viewProfile_Click(object sender, RoutedEventArgs e)
        {
            viewBankProfile.IsOpen = true;
        }
        private Model DataModel
        {
            get { return this.Resources["gmodel"] as Model; }
        }

        private void acc_viewAccounts_Click(object sender, RoutedEventArgs e)
        {
            viewAccount.IsOpen = true;
        }

        private void acc_moneyTransacion_Click(object sender, RoutedEventArgs e)
        {
            moneyTransaction.IsOpen = true;
        }

        private void acc_viewTransfers_Click(object sender, RoutedEventArgs e)
        {
            moneyTransfer.IsOpen = true;
        }
       
    }
}
