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
    /// Interaction logic for ViewAccounts.xaml
    /// </summary>
    public partial class ViewAccounts : UserControl
    {
        public delegate void ClosedHandler(object sender, EventArgs e);
        public event ClosedHandler CloseViewAccount;
        public ViewAccounts()
        {
            InitializeComponent();
        }
        DataSet lds_accountDetails = null;
        DataStore_sql obj_datastore = new DataStore_sql();
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            lds_accountDetails = null;
            CloseViewAccount(this, e);
        }

        private void cb_close_Click(object sender, RoutedEventArgs e)
        {
            lds_accountDetails = null;
            CloseViewAccount(this, e);
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                lds_accountDetails = obj_datastore.GetRecords("select tbl_accountdetails.* from tbl_accountdetails");
                if (lds_accountDetails != null)
                {
                    DataColumn slnoDataColumn = new DataColumn("slno");
                    lds_accountDetails.Tables[0].Columns.Add(slnoDataColumn);
                    int li_Count = 0;
                    for (int i = 0; i < lds_accountDetails.Tables[0].Rows.Count; i++)
                    {
                        li_Count++;
                        lds_accountDetails.Tables[0].Rows[i]["slno"] = li_Count;
                    }
                    dg_viewAccounts.DataContext = lds_accountDetails.Tables[0].DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
