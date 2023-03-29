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
    /// Interaction logic for MoneyTransfer.xaml
    /// </summary>
    public partial class MoneyTransfer : UserControl
    {
        public delegate void ClosedHandler(object sender, EventArgs e);
        public event ClosedHandler CloseMoneyTransfer;
        public MoneyTransfer()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            CloseMoneyTransfer(this, e);
        }

        private void cb_close_Click(object sender, RoutedEventArgs e)
        {
            CloseMoneyTransfer(this, e);
        }
        DataSet lds_transactionDetails = new DataSet();
        DataStore_sql obj_datastore = new DataStore_sql();
        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                lds_transactionDetails = obj_datastore.GetRecords("select tbl_transactionDetails.* from tbl_transactionDetails");
                if (lds_transactionDetails != null)
                {
                    DataColumn slnoDataColumn = new DataColumn("slno");
                    lds_transactionDetails.Tables[0].Columns.Add(slnoDataColumn);
                    int li_slno=0;
                    for (int i = 0; i < lds_transactionDetails.Tables[0].Rows.Count; i++)
                    {
                        li_slno++;
                        lds_transactionDetails.Tables[0].Rows[i]["slno"] = li_slno;
                    }
                    dg_viewTransactions.DataContext = lds_transactionDetails.Tables[0].DefaultView;

                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
