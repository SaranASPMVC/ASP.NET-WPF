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
    /// Interaction logic for UserDetails.xaml
    /// </summary>
    public partial class UserDetails : UserControl
    {
        public delegate void ClosedHandler(object sender, EventArgs e);
        public event ClosedHandler CloseUsers;
        public UserDetails()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            lbl_status.Content = "";
            lds_userdetails = null;
            dg_userDetails.DataContext = null;
            CloseUsers(this, e);
        }

        private void dg_userDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dg_userDetails.SelectedIndex > -1)
                {
                    bool lb_status = Convert.ToBoolean(lds_userdetails.Tables[0].Rows[dg_userDetails.SelectedIndex]["selected"]);
                    for (int i = 0; i < lds_userdetails.Tables[0].Rows.Count; i++)
                    {
                        lds_userdetails.Tables[0].Rows[i]["selected"] = false;
                    }
                    if (lb_status)
                    {
                        lds_userdetails.Tables[0].Rows[dg_userDetails.SelectedIndex]["selected"] = false;
                    }
                    else
                    {
                        lds_userdetails.Tables[0].Rows[dg_userDetails.SelectedIndex]["selected"] = true;
                    }
                    dg_userDetails.DataContext = lds_userdetails.Tables[0].DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception :" + ex.ToString(), "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void dg_userDetails_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dg_userDetails.SelectedIndex > -1)
                {
                    bool lb_status = Convert.ToBoolean(lds_userdetails.Tables[0].Rows[dg_userDetails.SelectedIndex]["selected"]);
                    if (lb_status)
                    {
                        lds_userdetails.Tables[0].Rows[dg_userDetails.SelectedIndex]["selected"] = false;
                    }
                    else
                    {
                        lds_userdetails.Tables[0].Rows[dg_userDetails.SelectedIndex]["selected"] = true;
                    }
                    dg_userDetails.DataContext = lds_userdetails.Tables[0].DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception :" + ex.ToString(), "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void cb_close_Click(object sender, RoutedEventArgs e)
        {
            lbl_status.Content = "";
            lds_userdetails = null;
            dg_userDetails.DataContext = null;
            CloseUsers(this, e);
        }
        DataSet lds_userdetails = null;
        DataStore_sql obj_dataStrore = new DataStore_sql();
        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                lds_userdetails = obj_dataStrore.GetRecords("select tbl_userdetails.* from tbl_userdetails where usertype !='ADMIN' or usertype !='admin'");
                if (lds_userdetails != null)
                {
                    DataColumn slnoColumn = new DataColumn("slno");
                    DataColumn selectColumn = new DataColumn("selected");
                    lds_userdetails.Tables[0].Columns.Add(slnoColumn);
                    lds_userdetails.Tables[0].Columns.Add(selectColumn);
                    for (int i = 0; i < lds_userdetails.Tables[0].Rows.Count; i++)
                    {
                        int slno = i;
                        slno++;
                        lds_userdetails.Tables[0].Rows[i]["slno"] = slno;
                        lds_userdetails.Tables[0].Rows[i]["selected"] = false;
                    }
                    dg_userDetails.DataContext = lds_userdetails.Tables[0].DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception :" + ex.ToString(), "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void cb_approv_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dg_userDetails.SelectedIndex > -1)
                {
                    String ls_usercode = lds_userdetails.Tables[0].Rows[dg_userDetails.SelectedIndex]["usercode"].ToString();
                    lds_userdetails.Tables[0].PrimaryKey = new DataColumn[] { lds_userdetails.Tables[0].Columns["usercode"] };
                    DataRow ldr_editRow = lds_userdetails.Tables[0].Rows.Find(ls_usercode);
                    ldr_editRow.BeginEdit();
                    if (ldr_editRow["usertype"].ToString().ToUpper().Equals("NON"))
                    {
                        ldr_editRow["usertype"] = "USER";
                    }
                    ldr_editRow.EndEdit();
                    obj_dataStrore.Update(lds_userdetails, "select tbl_userdetails.* from tbl_userdetails where usertype !='ADMIN' or usertype !='admin'");
                    lbl_status.Content = "User approved sucessfully !";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception :" + ex.ToString(), "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
