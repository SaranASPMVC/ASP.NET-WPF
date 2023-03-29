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
using System.IO;
using Microsoft.Win32;
using System.Threading;
namespace WPFWatermarking
{
    /// <summary>
    /// Interaction logic for CreateAccount.xaml
    /// </summary>
    public partial class CreateAccount : UserControl
    {
        private Model DataModel
        {
            get { return this.Resources["gmodel"] as Model; }
        }
        public delegate void ClosedHandler(object sender, EventArgs e);
        public event ClosedHandler CloseCreateAccount;
        public CreateAccount()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            byte_userSig = null;
            image1.Source = null;
            txt_accid.Text = "";
            txt_age.Text = "";
            txt_area.Text = "";
            txt_username.Text = "";
            txt_city.Text = "";
            txt_fathername.Text = "";
            txt_initialamount.Text = "";
            txt_mothername.Text = "";
            txt_phoneno.Text = "";
            txt_pincode.Text = "";
            txt_state.Text = "";          
            lds_bankDetails = null;
            lbl_msg.Content = "";
            ls_task = "INSERT";

            CreateAccount.ls_acccode = "";
            CloseCreateAccount(this, e);
        }
        String bankCode = "";
        private void cb_save_Click(object sender, RoutedEventArgs e)
        {
           // SignatureToByte();
            try
            {
                lds_accdetails = obj_datastore.GetRecords("select tbl_accountdetails.* from tbl_accountdetails");
                if (lds_accdetails != null)
                {
                    lds_accdetails.Tables[0].PrimaryKey = new DataColumn[] { lds_accdetails.Tables[0].Columns["acccode"] };
                    switch (ls_task)
                    {
                        case "INSERT":
                            {
                                DataRow ldr_newRow = lds_accdetails.Tables[0].NewRow();
                                CreateAccount.ls_acccode = getUsercode();
                                ldr_newRow["acccode"] = CreateAccount.ls_acccode;
                                ldr_newRow["accholdername"] = txt_username.Text;
                                ldr_newRow["accid"] = txt_accid.Text;
                                ldr_newRow["fathername"] = txt_fathername.Text;
                                ldr_newRow["mothername"] = txt_mothername.Text;
                                ldr_newRow["age"] = txt_age.Text;
                                ldr_newRow["state"] = txt_state.Text;
                                ldr_newRow["city"] = txt_city.Text;
                                ldr_newRow["area"] = txt_area.Text;
                                ldr_newRow["pincode"] = txt_pincode.Text;
                                ldr_newRow["phoneno"] = txt_phoneno.Text;
                                ldr_newRow["status"] = "N";
                                ldr_newRow["iniamount"] = txt_initialamount.Text;
                                ComboBoxItem acctype = (ComboBoxItem)cmb_acctype.SelectedItem;
                                ldr_newRow["acctype"] = acctype.Content.ToString();
                                ldr_newRow["usercode"] = Page1.ls_usercode;
                                ldr_newRow["bankcode"] = cmb_bankid.SelectedValue;
                                ldr_newRow["signature"] = byte_userSig;
                                lds_accdetails.Tables[0].Rows.Add(ldr_newRow);
                                int count = obj_datastore.Update(lds_accdetails, "select tbl_accountdetails.* from tbl_accountdetails");
                                if (count >= 0)
                                {
                                    ls_task = "UPDATE";
                                    lbl_msg.Content = "Account added sucessfully !";
                                }
                                break;
                            }
                        case "UPDATE":
                            {
                                DataRow lds_findRow = lds_accdetails.Tables[0].Rows.Find(CreateAccount.ls_acccode);
                                if (lds_findRow != null)
                                {
                                    lds_findRow.BeginEdit();
                                    lds_findRow["accholdername"] = txt_username.Text;
                                    lds_findRow["accid"] = txt_accid.Text;
                                    lds_findRow["fathername"] = txt_fathername.Text;
                                    lds_findRow["mothername"] = txt_mothername.Text;
                                    lds_findRow["age"] = txt_age.Text;
                                    lds_findRow["state"] = txt_state.Text;
                                    lds_findRow["city"] = txt_city.Text;
                                    lds_findRow["area"] = txt_area.Text;
                                    lds_findRow["pincode"] = txt_pincode.Text;
                                    lds_findRow["phoneno"] = txt_phoneno.Text;                                   
                                    lds_findRow["iniamount"] = txt_initialamount.Text;
                                    ComboBoxItem acctype = (ComboBoxItem)cmb_acctype.SelectedItem;
                                    lds_findRow["acctype"] = acctype.Content.ToString();
                                    lds_findRow["usercode"] = Page1.ls_usercode;
                                    lds_findRow["signature"] = byte_userSig;
                                    lds_findRow["bankcode"] = cmb_bankid.SelectedValue;
                                    lds_findRow.EndEdit();
                                    obj_datastore.Update(lds_accdetails, "select tbl_accountdetails.* from tbl_accountdetails");
                                }
                                break;
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public static string ls_acccode = "";
        DataSet lds_accdetails = new DataSet();
        String ls_task = "INSERT";
        DataSet lds_bankDetails = null;
        DataStore_sql obj_datastore = new DataStore_sql();
        Thread _thread;
        byte[] byte_userSig = null;
        bool lb_threadState = true;
        System.Drawing.Bitmap originalImage = null;
        int opacity = 0;
        String ls_Imagecode = "";
        int li_count = 0;
        StreamWriter stream_writer;
        FileStream MyFileStream;
        Random random1 = new Random();
        Random random2 = new Random();
        int x = 0, y = 0;
        int k = 0, l = 0;
        String ls_accountId = "";
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
       
        private void cb_close_Click(object sender, RoutedEventArgs e)
        {
            byte_userSig = null;
            image1.Source = null;
            txt_accid.Text = "";
            txt_age.Text = "";
            txt_area.Text = "";
            txt_username.Text = "";
            txt_city.Text = "";
            txt_fathername.Text = "";
            txt_initialamount.Text = "";
            txt_mothername.Text = "";
            txt_phoneno.Text = "";
            txt_pincode.Text = "";
            txt_state.Text = "";           
            lds_bankDetails = null;
            lbl_msg.Content = "";
            ls_task = "INSERT";
            CreateAccount.ls_acccode = "";
            CloseCreateAccount(this, e);
        }

        private void cb_add_Click(object sender, RoutedEventArgs e)
        {
            image1.Source = null;
            txt_accid.Text = "";
            txt_age.Text = "";
            txt_area.Text = "";
            txt_username.Text = "";
            txt_city.Text = "";
            txt_fathername.Text = "";
            txt_initialamount.Text = "";
            txt_mothername.Text = "";
            txt_phoneno.Text = "";
            txt_pincode.Text = "";
            txt_state.Text = "";
            lds_bankDetails = null;
            lbl_msg.Content = "";
            ls_task = "INSERT";
            CreateAccount.ls_acccode = "";
            byte_userSig = null;
        }

        public String getUsercode()
        {
            try
            {
                String usercode = "";
                DataSet lds_userdetails = obj_datastore.GetRecords("select tbl_accountdetails.* from tbl_accountdetails order by acccode");
                if (lds_userdetails != null)
                {
                    if (lds_userdetails.Tables[0].Rows.Count == 0)
                    {

                        usercode = "ACC0000001";
                    }
                    else
                    {
                        usercode = lds_userdetails.Tables[0].Rows[lds_userdetails.Tables[0].Rows.Count - 1]["acccode"].ToString();
                        usercode = usercode.Replace("ACC", "");
                        int li_curValue = Convert.ToInt32(usercode);
                        li_curValue++;
                        int li_padlength = 10 - ("ACC".Length + li_curValue.ToString().Length);
                        usercode = li_curValue.ToString();
                        for (int i = 0; i < li_padlength; i++)
                        {
                            usercode = "0" + usercode;
                        }
                        usercode = "ACC" + usercode;
                    }
                }
                return usercode;
            }
            catch (Exception ex)
            {
                return "ERROR";
            }
        }
        public String getAccId()
        {
            try
            {
                String usercode = "";
                DataSet lds_userdetails = obj_datastore.GetRecords("select tbl_accountdetails.* from tbl_accountdetails order by accid");
                if (lds_userdetails != null)
                {
                    if (lds_userdetails.Tables[0].Rows.Count == 0)
                    {

                        usercode = "AID0000001";
                    }
                    else
                    {
                        usercode = lds_userdetails.Tables[0].Rows[lds_userdetails.Tables[0].Rows.Count - 1]["accid"].ToString();
                        usercode = usercode.Replace("AID", "");
                        int li_curValue = Convert.ToInt32(usercode);
                        li_curValue++;
                        int li_padlength = 10 - ("AID".Length + li_curValue.ToString().Length);
                        usercode = li_curValue.ToString();
                        for (int i = 0; i < li_padlength; i++)
                        {
                            usercode = "0" + usercode;
                        }
                        usercode = "AID" + usercode;
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
            Signature sig = new Signature();
            sig.ShowDialog();
        }
     
        private void cb_view_Click(object sender, RoutedEventArgs e)
        {
            FileInfo info = new FileInfo("d://Sig.jpeg");
            if (info.Exists)
            {
                byte_userSig = System.IO.File.ReadAllBytes("d://Sig.jpeg");

                Uri uri = new Uri("d://Sig.jpeg", UriKind.Absolute);
                ImageSource imgSource = new BitmapImage(uri);
                image1.Source = imgSource;
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

            txt_accid.Text = getAccId();
            ls_accountId = txt_accid.Text;
        }
      
        private void cb_watermarkedImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Drawing.Bitmap bitmapWatermark=new System.Drawing.Bitmap(new System.Drawing.Bitmap(new MemoryStream(byte_userSig)));
                DataSet lds_bank = obj_datastore.GetRecords("select tbl_bankdetails.* from tbl_bankdetails where bankcode='" + cmb_bankid.SelectedValue + "'");
                if (lds_bank != null)
                {
                    if (lds_bank.Tables[0].Rows.Count > 0)
                    {
                        byte[] byte_imageArray = (byte[])lds_bank.Tables[0].Rows[0]["banklogo"];
                        originalImage = new System.Drawing.Bitmap(new System.Drawing.Bitmap(new MemoryStream(byte_imageArray)));
                        GetImage();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
       
        private void GetImage()
        {

           
                try
                {
                    li_count++;
                    lb_threadState = false;
                   

                    FileInfo infoFile = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "\\" + ls_accountId + ".txt");
                    if (!infoFile.Exists)
                        infoFile.Delete();

                    MyFileStream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\" + ls_accountId + ".txt", FileMode.Create, FileAccess.Write, FileShare.None);
                    stream_writer = new StreamWriter(MyFileStream);
                    System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(originalImage);
                    System.Drawing.Bitmap bmp_cov2 = new System.Drawing.Bitmap(new System.Drawing.Bitmap(new MemoryStream(byte_userSig)));

                    for (int i = 0; i < bmp_cov2.Width; i++)
                    {

                        for (int j = 0; j < bmp_cov2.Height; j++)
                        {
                            try
                            {
                                // k=random1.Next(
                                k = random1.Next(i, originalImage.Width);
                                l = random2.Next(j, originalImage.Height);

                                // Console.WriteLine(i + "i" + j + "j," + k + " " + l);
                                System.Drawing.Color image1Pixel = bmp_cov2.GetPixel(i, j), image2Pixel = originalImage.GetPixel(k, l);
                                System.Drawing.Color write = System.Drawing.Color.FromArgb(image1Pixel.R, image1Pixel.G, image1Pixel.B);
                                System.Drawing.Color newColor = System.Drawing.Color.FromArgb((image1Pixel.R * opacity + image2Pixel.R * (100 - opacity)) / 100,
                                                                (image1Pixel.G * opacity + image2Pixel.G * (100 - opacity)) / 100,
                                                                (image1Pixel.B * opacity + image2Pixel.B * (100 - opacity)) / 100);



                                stream_writer.WriteLine(k.ToString() + "," + l.ToString() + ";" + image2Pixel.R.ToString() + "," + image2Pixel.G.ToString() + "," + image2Pixel.B.ToString() + ";" + i.ToString() + "," + j.ToString() + ";" + image1Pixel.R.ToString() + "," + image1Pixel.G.ToString() + "," + image1Pixel.B.ToString());

                                // newBmp.SetPixel(i, j, newColor);
                                bmp.SetPixel(k, l, newColor);
                                // 
                            }
                            catch (Exception ex)
                            {
                                continue;
                            }
                        }

                    }
                    stream_writer.Flush();
                    stream_writer.Close();
                    System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
                    saveFileDialog.ShowDialog();
                    saveFileDialog.Title = "Save File";
                    saveFileDialog.DefaultExt = "bmp";
                    saveFileDialog.Filter = "bmp Image Files|*.bmp";
                    //  finalBitmap = new System.Drawing.Bitmap(new System.Drawing.Bitmap(final));
                    if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (saveFileDialog.FileName == "")
                        {
                            MessageBox.Show("Please specify the file name", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
                        else
                        {
                            // save the file with the name supplied by the user
                            bmp.Save(saveFileDialog.FileName);                           
                            MessageBox.Show("Image  saved.", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                        }


                    }                 

                  

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            
        }

    }
}
