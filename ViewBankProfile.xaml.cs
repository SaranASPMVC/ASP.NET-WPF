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
namespace WPFWatermarking
{
    /// <summary>
    /// Interaction logic for ViewBankProfile.xaml
    /// </summary>
    public partial class ViewBankProfile : UserControl
    {
        public delegate void ClosedHandler(object sender, EventArgs e);
        public event ClosedHandler CloseBankProfiles;
        public ViewBankProfile()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            CloseBankProfiles(this, e);
        }
        DataSet lds_bankDetails = null;
        DataSet lds_bankProfile = null;
        DataStore_sql obj_datastore = new DataStore_sql();
        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                lds_bankDetails = obj_datastore.GetRecords("select tbl_bankdetails.* from tbl_bankdetails");
                if (lds_bankDetails != null)
                {
                    cmb_bankid.DataContext = lds_bankDetails.Tables[0].DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dg_userDetails_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void dg_userDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmb_bankid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                lds_bankProfile = obj_datastore.GetRecords("select tbl_bankdetails.* from tbl_bankdetails where bankcode='"+cmb_bankid.SelectedValue+"'");
                if (lds_bankProfile != null)
                {
                    DataColumn newColumn = new DataColumn("slno");
                    lds_bankProfile.Tables[0].Columns.Add(newColumn);
                    int li_count=0;
                    for (int i = 0; i < lds_bankProfile.Tables[0].Rows.Count; i++)
                    {
                        li_count++;
                        lds_bankProfile.Tables[0].Rows[i]["slno"] = li_count;
                    }
                    dg_viewProfile.DataContext = lds_bankProfile.Tables[0].DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void cb_close_Click(object sender, RoutedEventArgs e)
        {
            CloseBankProfiles(this, e);
        }
    }
}
