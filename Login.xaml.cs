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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public delegate void ClosedHandler(object sender, EventArgs e,bool isAdmin,bool status);
        public event ClosedHandler CloseLogin;
        public Login()
        {
            InitializeComponent();
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            txt_password.Password = "";
            txt_username.Text = "";
            CloseLogin(this, e,false,true);
        }

        private void cb_close_Click(object sender, RoutedEventArgs e)
        {
            txt_password.Password = "";
            txt_username.Text = "";
            CloseLogin(this, e,false,true);
        }
        DataStore_sql obj_datastore = new DataStore_sql();
        private void cb_login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataSet lds_userdetails = obj_datastore.GetRecords("select tbl_userdetails.* from tbl_userdetails");
                if (lds_userdetails != null)
                {
                    if (lds_userdetails.Tables[0].Rows.Count == 0)
                    {
                        DataRow ldr_newRow = lds_userdetails.Tables[0].NewRow();
                        ldr_newRow["usercode"] = getUsercode(); ;
                        ldr_newRow["username"] = "admin";
                        ldr_newRow["password"] = "admin";
                        ldr_newRow["usertype"] = "ADMIN";
                        lds_userdetails.Tables[0].Rows.Add(ldr_newRow);
                        obj_datastore.Update(lds_userdetails, "select tbl_userdetails.* from tbl_userdetails");
                        if (txt_password.Password.Equals("admin") && txt_username.Text.Equals("admin"))
                        {
                            txt_password.Password = "";
                            txt_username.Text = "";
                            CloseLogin(this, e,true,false);
                        }
                    }
                    else
                    {
                        lds_userdetails = obj_datastore.GetRecords("select tbl_userdetails.* from tbl_userdetails where username='"+txt_username.Text+"' and password='"+txt_password.Password+"'");
                        if (lds_userdetails != null)
                        {
                            if (lds_userdetails.Tables[0].Rows.Count == 1)
                            {
                                Page1.ls_usercode = lds_userdetails.Tables[0].Rows[0]["usercode"].ToString();
                                Page1.ls_usertype = lds_userdetails.Tables[0].Rows[0]["usertype"].ToString();
                                if (Page1.ls_usertype.ToUpper().Equals("ADMIN"))
                                {
                                    txt_password.Password = "";
                                    txt_username.Text = "";
                                    CloseLogin(this, e, true, false);
                                }
                                else
                                {
                                    if (Page1.ls_usertype.ToUpper().Equals("USER"))
                                    {
                                        txt_password.Password = "";
                                        txt_username.Text = "";
                                        CloseLogin(this, e, false, false);
                                    }
                                    else
                                    {
                                        lbl_msg.Content = "User unApproved !";
                                    }
                                }
                            }
                            else
                            {
                                lbl_msg.Content = "Invalid user !";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lbl_msg.Content = e.ToString(); 
            }
        }

        public String getUsercode()
        {
            try
            {
                String usercode = "";
                 DataSet lds_userdetails = obj_datastore.GetRecords("select tbl_userdetails.* from tbl_userdetails order by usercode");
                 if (lds_userdetails != null)
                 {
                     if (lds_userdetails.Tables[0].Rows.Count == 0)
                     {
                        
                         usercode = "USR0000001";
                     }
                     else
                     {
                         usercode = lds_userdetails.Tables[0].Rows[lds_userdetails.Tables[0].Rows.Count - 1]["usercode"].ToString();
                         usercode = usercode.Replace("USR", "");
                         int li_curValue = Convert.ToInt32(usercode);
                         li_curValue++;
                         int li_padlength = 10 - ("USR".Length + li_curValue.ToString().Length);
                         usercode = li_curValue.ToString();
                         for (int i = 0; i < li_padlength; i++)
                         {
                             usercode = "0" + usercode;
                         }
                         usercode = "USR" + usercode;
                     }
                 }
                 return usercode;
            }
            catch (Exception ex)
            {
                return "ERROR";
            }
        }

        private void cb_applyforuser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                 DataSet lds_userdetails = obj_datastore.GetRecords("select tbl_userdetails.* from tbl_userdetails");
                 if (lds_userdetails != null)
                 {
                    
                         DataRow ldr_newRow = lds_userdetails.Tables[0].NewRow();
                         ldr_newRow["usercode"] = getUsercode(); ;
                         ldr_newRow["username"] = txt_username.Text;
                         ldr_newRow["password"] = txt_password.Password;
                         ldr_newRow["usertype"] = "NON";
                         lds_userdetails.Tables[0].Rows.Add(ldr_newRow);
                         obj_datastore.Update(lds_userdetails, "select tbl_userdetails.* from tbl_userdetails");
                         lbl_msg.Content = "Submited user !";
                         txt_username.Text = "";
                         txt_password.Password = "";
                 }
            }
            catch (Exception ex)
            {
                lbl_msg.Content = e.ToString(); 
            }
        }
    }
}
