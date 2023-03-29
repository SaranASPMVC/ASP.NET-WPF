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
    /// Interaction logic for CreateBank.xaml
    /// </summary>
    public partial class CreateBank : UserControl
    {
        public delegate void ClosedHandler(object sender, EventArgs e);
        public event ClosedHandler CloseCreateBank;
        public CreateBank()
        {
            InitializeComponent();
        }
        DataSet lds_bankdetails = null;
        DataStore_sql obj_datastore = new DataStore_sql();
        String ls_TASK = "INSERT";
        byte[] byte_banklogo = null;
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
           
            txt_address.Text = "";
            txt_address2.Text = "";
            txt_area.Text = "";
            txt_bankid.Text = "";
            txt_bankname.Text = "";
            txt_currency.Text = "";
            txt_maxamount.Text = "";
            txt_phoneno.Text = "";
            txt_pincode.Text = "";
            txt_state.Text = ""; 
            lds_bankdetails=null;
            byte_banklogo = null;
            ls_TASK = "INSERT";
            lbl_msg.Content = "";
            txt_bankidSearch.Text = "";
            img_logo.Source = null;
            CreateBank.bankcode = "";
            CloseCreateBank(this, e);
        }

        private void cb_close_Click(object sender, RoutedEventArgs e)
        {
            ls_TASK = "INSERT";
            txt_bankidSearch.Text = "";
            img_logo.Source = null;
            txt_address.Text = "";
            txt_address2.Text = "";
            txt_area.Text = "";
            txt_bankid.Text = "";
            txt_bankname.Text = "";
            txt_currency.Text = "";
            txt_maxamount.Text = "";
            txt_phoneno.Text = "";
            txt_pincode.Text = "";
            txt_state.Text = "";
            CreateBank.bankcode = "";
            lbl_msg.Content = "";
            CloseCreateBank(this, e);
        }
        public static String bankcode = "";
        private void cb_save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lds_bankdetails = obj_datastore.GetRecords("select tbl_bankdetails.* from tbl_bankdetails");
                if (lds_bankdetails != null)
                {
                    lds_bankdetails.Tables[0].PrimaryKey = new DataColumn[] {lds_bankdetails.Tables[0].Columns["bankcode"] };
                    if (ls_TASK.Equals("INSERT"))
                    {
                        CreateBank.bankcode = getUsercode(); ;
                        DataRow ldr_newRow = lds_bankdetails.Tables[0].NewRow();
                        ldr_newRow["bankcode"] = CreateBank.bankcode;
                        ldr_newRow["bankid"] = txt_bankid.Text;
                        ldr_newRow["bankname"] = txt_bankname.Text;
                        ComboBoxItem cbiAcc = (ComboBoxItem)cmb_banktype.SelectedItem;
                        ldr_newRow["acctype"] = cbiAcc.Content.ToString();
                        ldr_newRow["address"] = txt_address.Text;
                        ldr_newRow["address2"] = txt_address2.Text;
                        ldr_newRow["area"] = txt_area.Text;
                        ldr_newRow["currency"] = txt_currency.Text;
                        ldr_newRow["maxamount"] = txt_maxamount.Text;
                        ldr_newRow["phoneno"] = txt_phoneno.Text;
                        ldr_newRow["pincode"] = txt_pincode.Text;
                        ldr_newRow["state"] = txt_state.Text;
                        ComboBoxItem cbi = (ComboBoxItem)cmb_banktype.SelectedItem;
                        ldr_newRow["banktype"] = cbi.Content.ToString();
                        ldr_newRow["banklogo"] = byte_banklogo;
                        lds_bankdetails.Tables[0].Rows.Add(ldr_newRow);
                        obj_datastore.Update(lds_bankdetails, "select tbl_bankdetails.* from tbl_bankdetails");
                        lbl_msg.Content = "Saved successfully !";
                        ls_TASK = "UPDATE";
                        // banklogo image
                    }
                    else
                    {
                        DataRow ldr_findRow = lds_bankdetails.Tables[0].Rows.Find(CreateBank.bankcode);
                        ldr_findRow.BeginEdit();                        
                        ldr_findRow["bankid"] = txt_bankid.Text;
                        ldr_findRow["bankname"] = txt_bankname.Text;
                        ComboBoxItem cbiAcc = (ComboBoxItem)cmb_banktype.SelectedItem;
                        ldr_findRow["acctype"] = cbiAcc.Content.ToString();
                        ldr_findRow["address"] = txt_address.Text;
                        ldr_findRow["address2"] = txt_address2.Text;
                        ldr_findRow["area"] = txt_area.Text;
                        ldr_findRow["currency"] = txt_currency.Text;
                        ldr_findRow["maxamount"] = txt_maxamount.Text;
                        ldr_findRow["phoneno"] = txt_phoneno.Text;
                        ldr_findRow["pincode"] = txt_pincode.Text;
                        ldr_findRow["state"] = txt_state.Text;
                        ComboBoxItem cbi = (ComboBoxItem)cmb_banktype.SelectedItem;
                        ldr_findRow["banktype"] = cbi.Content.ToString();
                        ldr_findRow["banklogo"] = byte_banklogo;
                        ldr_findRow.EndEdit();
                        obj_datastore.Update(lds_bankdetails, "select tbl_bankdetails.* from tbl_bankdetails");
                        lbl_msg.Content = "Updated successfully !";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cb_add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ls_TASK = "INSERT";
               
                txt_address.Text = "";
                txt_address2.Text = "";
                txt_area.Text = "";
                txt_bankid.Text = "";
                txt_bankname.Text = "";
                txt_currency.Text = "";
                txt_maxamount.Text = "";
                txt_phoneno.Text = "";
                txt_pincode.Text = "";
                txt_state.Text = "";
                byte_banklogo = null;
                CreateBank.bankcode = "";
                lbl_msg.Content = "";
                img_logo.Source = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public String getUsercode()
        {
            try
            {
                String usercode = "";
                DataSet lds_userdetails = obj_datastore.GetRecords("select tbl_bankdetails.* from tbl_bankdetails order by bankcode");
                if (lds_userdetails != null)
                {
                    if (lds_userdetails.Tables[0].Rows.Count == 0)
                    {

                        usercode = "BNK0000001";
                    }
                    else
                    {
                        usercode = lds_userdetails.Tables[0].Rows[lds_userdetails.Tables[0].Rows.Count - 1]["bankcode"].ToString();
                        usercode = usercode.Replace("BNK", "");
                        int li_curValue = Convert.ToInt32(usercode);
                        li_curValue++;
                        int li_padlength = 10 - ("BNK".Length + li_curValue.ToString().Length);
                        usercode = li_curValue.ToString();
                        for (int i = 0; i < li_padlength; i++)
                        {
                            usercode = "0" + usercode;
                        }
                        usercode = "BNK" + usercode;
                    }
                }
                return usercode;
            }
            catch (Exception ex)
            {
                return "ERROR";
            }
        }

        private void cb_translogo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog();
                fileDialog.Filter = " Image Files (*.bmp, *.jpg,*.gif)|*.bmp;*.jpg;*.gif";
                if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    byte_banklogo = System.IO.File.ReadAllBytes(fileDialog.FileName);
                    Uri uri = new Uri(fileDialog.FileName, UriKind.Absolute);
                    ImageSource imgSource = new BitmapImage(uri);
                    img_logo.Source = imgSource;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception : " + ex.Message, "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void cb_search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                 DataSet lds_bankdetail = obj_datastore.GetRecords("select tbl_bankdetails.* from tbl_bankdetails where bankid='"+txt_bankidSearch.Text+"'");
                 if (lds_bankdetail != null)
                 {
                     if (lds_bankdetail.Tables[0].Rows.Count > 0)
                     {
                         ls_TASK = "UPDATE";
                         CreateBank.bankcode = lds_bankdetail.Tables[0].Rows[0]["bankcode"].ToString();
                         if (lds_bankdetail.Tables[0].Rows[0]["acctype"].ToString().ToUpper().Equals("CURRENT"))
                         {
                             cmb_acctype.SelectedIndex = 0;
                         }
                         else
                         {
                             if (lds_bankdetail.Tables[0].Rows[0]["acctype"].ToString().ToUpper().Equals("SAVINGS"))
                             {
                                 cmb_acctype.SelectedIndex = 1;
                             }
                         }
                         txt_address.Text = lds_bankdetail.Tables[0].Rows[0]["address"].ToString();
                         txt_address2.Text = lds_bankdetail.Tables[0].Rows[0]["address2"].ToString();
                         txt_area.Text = lds_bankdetail.Tables[0].Rows[0]["area"].ToString();
                         txt_bankid.Text = lds_bankdetail.Tables[0].Rows[0]["bankid"].ToString();
                         txt_bankname.Text = lds_bankdetail.Tables[0].Rows[0]["bankname"].ToString();
                         txt_currency.Text = lds_bankdetail.Tables[0].Rows[0]["currency"].ToString();
                         txt_maxamount.Text = lds_bankdetail.Tables[0].Rows[0]["maxamount"].ToString();
                         txt_phoneno.Text = lds_bankdetail.Tables[0].Rows[0]["phoneno"].ToString();
                         txt_pincode.Text = lds_bankdetail.Tables[0].Rows[0]["pincode"].ToString();
                         txt_state.Text = lds_bankdetail.Tables[0].Rows[0]["state"].ToString();
                         switch (lds_bankdetail.Tables[0].Rows[0]["banktype"].ToString().ToLower())
                         {
                             case "public":
                                 {
                                     cmb_banktype.SelectedIndex = 0;
                                     break;
                                 }
                             case "private":
                                 {
                                     cmb_banktype.SelectedIndex = 1;
                                     break;
                                 }
                             case "goverment":
                                 {
                                     cmb_banktype.SelectedIndex = 2;
                                     break;
                                 }
                         }
                         switch (lds_bankdetail.Tables[0].Rows[0]["acctype"].ToString().ToLower())
                         {
                             case "current":
                                 {
                                     cmb_banktype.SelectedIndex = 0;
                                     break;
                                 }
                             case "savings":
                                 {
                                     cmb_banktype.SelectedIndex = 1;
                                     break;
                                 }
                         }
                         byte_banklogo = (byte[])lds_bankdetail.Tables[0].Rows[0]["banklogo"];
                         System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(System.Drawing.Image.FromStream(new System.IO.MemoryStream(byte_banklogo)));
                         bitmap.Save(AppDomain.CurrentDomain.BaseDirectory + "Test.bmp");
                         Uri uri = new Uri(AppDomain.CurrentDomain.BaseDirectory + "Test.bmp", UriKind.Absolute);
                         ImageSource imgSource = new BitmapImage(uri);
                         img_logo.Source = imgSource;
                     }
                 }
                 else
                 {
                     ls_TASK = "INSERT";
                   
                     txt_address.Text = "";
                     txt_address2.Text = "";
                     txt_area.Text = "";
                     txt_bankid.Text = "";
                     txt_bankname.Text = "";
                     txt_currency.Text = "";
                     txt_maxamount.Text = "";
                     txt_phoneno.Text = "";
                     txt_pincode.Text = "";
                     txt_state.Text = "";
                     byte_banklogo = null;
                     CreateBank.bankcode = "";
                     lbl_msg.Content = "";
                     img_logo.Source = null;
                 }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception : " + ex.Message, "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
