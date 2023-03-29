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
using AForge.Imaging;

namespace WPFWatermarking
{
    /// <summary>
    /// Interaction logic for MoneyTransaction.xaml
    /// </summary>
    public partial class MoneyTransaction : UserControl
    {
        public delegate void ClosedHandler(object sender, EventArgs e);
        public event ClosedHandler CloseMoneyTransaction;
        public MoneyTransaction()
        {
            InitializeComponent();
        }
        DataSet lds_bankDetails = null;
        DataStore_sql obj_datastore = new DataStore_sql();
        DataSet lds_accdetails = new DataSet();
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            txt_accid.Text = "";
            img_logo.Source = null;
            CloseMoneyTransaction(this, e);
        }

        private void cb_getaccdetails_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lds_accdetails = obj_datastore.GetRecords("select tbl_accountdetails.* from tbl_accountdetails where accid='" + txt_accid.Text + "'");
                if (lds_accdetails != null)
                {
                    DataColumn slnoColumn = new DataColumn("slno");
                    lds_accdetails.Tables[0].Columns.Add(slnoColumn);
                    int li_count=0;
                    for (int i = 0; i < lds_accdetails.Tables[0].Rows.Count; i++)
                    {
                        li_count++;
                        lds_accdetails.Tables[0].Rows[i]["slno"] = li_count;
                    }
                    dg_viewAccounts.DataContext = lds_accdetails.Tables[0].DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cb_watermarkedlogo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog();
                fileDialog.Filter = " Image Files (*.bmp, *.jpg,*.gif)|*.bmp;*.jpg;*.gif";
                if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    img_cov1 = System.Drawing.Image.FromFile(fileDialog.FileName);
                    ls_cov1 = fileDialog.FileName;
                    bmp_cov1 = new System.Drawing.Bitmap(new System.Drawing.Bitmap(img_cov1));
                    li_width = bmp_cov1.Width;
                    li_height = bmp_cov1.Height;
                    Uri uri = new Uri(fileDialog.FileName, UriKind.Absolute);
                    ImageSource imgSource = new BitmapImage(uri);
                    img_logo.Source = imgSource;
                    ls_path = AppDomain.CurrentDomain.BaseDirectory + txt_accid.Text + ".txt";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception : " + ex.Message, "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cb_close_Click(object sender, RoutedEventArgs e)
        {
            txt_accid.Text = "";
            img_logo.Source = null;
            CloseMoneyTransaction(this, e);
        }

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

        private void cb_transferamount_Click(object sender, RoutedEventArgs e)
        {
            if (txt_accid.Text != txt_fromaccount.Text)
                ComparePixel(bmp_cov1);
            else
                lbl_msg.Content = "From and to account found same !";
        }
        StreamReader stream_reader;
        string ls_color = "";

        string[] lsa_mix;
        string[] lia_cordintes;
        string[] lsa_rgb;
        string[] lsa_baseCordinate;
        int li_count = 0;
        int li_width = -1;
        int li_height = -1;
        String ls_path = "";
        System.Drawing.Image img_cov1, img_cov2;
        string ls_cov1 = "", ls_cov2 = "";
        System.Drawing.Bitmap bmp_cov1, bmp_cov2;
        System.Drawing.Bitmap final;
        private void ComparePixel(System.Drawing.Image _watermarked)
        {
            try
            {
                li_count++;
                lsa_mix = new string[3];
                int x = -1, y = -1;
                int R = -1, G = -1, B = -1;
                int li_R = -1, li_G = -1, li_B = -1;
                string is_coordinate1 = "";
                string is_coordinate2 = "", ls_cordinate3 = "";
                string is_color = "";
                lia_cordintes = new string[2];
                System.Drawing.Bitmap watermarkedbmp = new System.Drawing.Bitmap(new System.Drawing.Bitmap(_watermarked));
                final = new System.Drawing.Bitmap(li_width, li_height);
                stream_reader = new StreamReader(ls_path);
                int h = 0, k = 0;
                while ((ls_color = stream_reader.ReadLine()) != null)
                {
                    //xt_log.Text += Environment.NewLine;    

                    if (!ls_color.Equals("") && !string.IsNullOrEmpty(ls_color))
                    {
                        lsa_mix = ls_color.Split(';');

                        is_coordinate1 = lsa_mix[0];

                        is_coordinate2 = lsa_mix[2];

                        ls_cordinate3 = lsa_mix[1];

                        is_color = lsa_mix[3];

                        lsa_rgb = is_color.Split(',');
                        lsa_baseCordinate = ls_cordinate3.Split(',');
                        //MessageBox.Show(is_color+"watermarkRGB");
                        R = System.Convert.ToInt32(lsa_rgb[0]);
                        G = System.Convert.ToInt32(lsa_rgb[1]);
                        B = System.Convert.ToInt32(lsa_rgb[2]);
                        lia_cordintes = is_coordinate1.Split(',');
                        //   MessageBox.Show(ls_cordinate3+"BaseRGB");
                        li_R = System.Convert.ToInt32(lsa_baseCordinate[0]);
                        li_G = System.Convert.ToInt32(lsa_baseCordinate[1]);
                        li_B = System.Convert.ToInt32(lsa_baseCordinate[2]);
                        //  MessageBox.Show(is_coordinate1+"WaterCordinate");
                        h = System.Convert.ToInt32(lia_cordintes[0]);
                        k = System.Convert.ToInt32(lia_cordintes[1]);
                        lia_cordintes = is_coordinate2.Split(',');
                        //  MessageBox.Show(is_coordinate2+"Base Coordinate");
                        x = System.Convert.ToInt32(lia_cordintes[0]);
                        y = System.Convert.ToInt32(lia_cordintes[1]);
                        System.Drawing.Color add = System.Drawing.Color.FromName(is_color);

                        if (x < watermarkedbmp.Width && y < watermarkedbmp.Height)
                        {


                            if (R == 0 && G == 0 && B == 0)
                            {

                            }
                            else
                            {
                                try
                                {
                                    System.Drawing.Color baseColor = System.Drawing.Color.FromArgb(li_R, li_G, li_B);
                                    System.Drawing.Color match = watermarkedbmp.GetPixel(h, k);
                                    System.Drawing.Color set = System.Drawing.Color.FromArgb(R, G, B);

                                    //MessageBox.Show(match + "test" + set);
                                    // textBox1.Text += Environment.NewLine;
                                    //   textBox1.Text += baseColor + "test" + match;
                                    if (match == baseColor)
                                        final.SetPixel(x, y, set);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.ToString(), "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                                    break;
                                }


                            }
                        }
                    }
                }
                DataSet lds_accountdetails = obj_datastore.GetRecords("select tbl_accountdetails.* from tbl_accountdetails where accid='" +txt_fromaccount.Text + "'");
                if (lds_accountdetails != null)
                {
                    if (lds_accountdetails.Tables[0].Rows.Count > 0)
                    {
                        byte[] byte_sourceImage = (byte[])lds_accountdetails.Tables[0].Rows[0]["signature"];
                        System.Drawing.Bitmap sourceImageBpp = new System.Drawing.Bitmap(new System.Drawing.Bitmap(new MemoryStream(byte_sourceImage)));
                        System.Drawing.Bitmap sourceImage = ColorToGrayscale(sourceImageBpp);
                        System.Drawing.Bitmap sourceImageTemp = ColorToGrayscale(final);
                        double lb_intialAmount=Convert.ToDouble(lds_accountdetails.Tables[0].Rows[0]["iniamount"]);
                        double lb_transferAmount=Convert.ToDouble(txt_amounttoTransfer.Text);
                        if (lb_intialAmount < lb_transferAmount)
                        {
                            lbl_msg.Content = "Insufficient Amount in Account !";
                            return;
                        }
                        else
                        {
                            lds_accountdetails.Tables[0].Rows[0]["iniamount"] = lb_intialAmount - lb_transferAmount;
                            lb_intialAmount = Convert.ToDouble(lds_accdetails.Tables[0].Rows[0]["iniamount"]);
                            lds_accdetails.Tables[0].Rows[0]["iniamount"] = lb_intialAmount + lb_transferAmount;
                            int li_rows=obj_datastore.Update(lds_accdetails, "select tbl_accountdetails.* from tbl_accountdetails where accid='" + txt_accid.Text + "'");
                            li_rows = obj_datastore.Update(lds_accountdetails, "select tbl_accountdetails.* from tbl_accountdetails where accid='" + txt_fromaccount.Text + "'");
                            DataSet lds_transaction = obj_datastore.GetRecords("select tbl_transactionDetails.* from tbl_transactionDetails");
                            if (lds_transaction != null)
                            {
                                DataRow newRow = lds_transaction.Tables[0].NewRow();
                                newRow["transid"] = getUsercode();
                                newRow["fromacc"] = txt_fromaccount.Text;
                                newRow["toacc"] = txt_accid.Text;
                                newRow["ondate"] = DateTime.Now;
                                newRow["amount"] = txt_amounttoTransfer.Text;
                                lds_transaction.Tables[0].Rows.Add(newRow);
                                obj_datastore.Update(lds_transaction, "select tbl_transactionDetails.* from tbl_transactionDetails");
                            }
                            lbl_msg.Content = "Amount transfered sucessfully !";
                        }
                       
                    }
                }
               



               
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString(), "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
                //  MessageBox.Show("Error in base iamge please give correct iamge", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public System.Drawing.Bitmap ColorToGrayscale(System.Drawing.Bitmap bmp)
        {
            int w = bmp.Width,
                h = bmp.Height,
                r, ic, oc, bmpStride, outputStride, bytesPerPixel;
            System.Drawing.Imaging.PixelFormat pfIn = bmp.PixelFormat;
            System.Drawing.Imaging.ColorPalette palette;
            System.Drawing.Bitmap output;
            System.Drawing.Imaging.BitmapData bmpData, outputData;

            //Create the new bitmap
            output = new System.Drawing.Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

            //Build a grayscale color Palette
            palette = output.Palette;
            for (int i = 0; i < 256; i++)
            {
                System.Drawing.Color tmp = System.Drawing.Color.FromArgb(255, i, i, i);
                palette.Entries[i] = System.Drawing.Color.FromArgb(255, i, i, i);
            }
            output.Palette = palette;

            //No need to convert formats if already in 8 bit
            if (pfIn == System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
            {
                output = (System.Drawing.Bitmap)bmp.Clone();

                //Make sure the palette is a grayscale palette and not some other
                //8-bit indexed palette
                output.Palette = palette;

                return output;
            }

            //Get the number of bytes per pixel          
            switch (pfIn)
            {
                case System.Drawing.Imaging.PixelFormat.Format24bppRgb: bytesPerPixel = 3; break;
                case System.Drawing.Imaging.PixelFormat.Format32bppArgb: bytesPerPixel = 4; break;
                case System.Drawing.Imaging.PixelFormat.Format32bppRgb: bytesPerPixel = 4; break;
                default: throw new InvalidOperationException("Image format not supported");
            }

            //Lock the images
            bmpData = bmp.LockBits(new System.Drawing.Rectangle(0, 0, w, h), System.Drawing.Imaging.ImageLockMode.ReadOnly,
                                   pfIn);
            outputData = output.LockBits(new System.Drawing.Rectangle(0, 0, w, h), System.Drawing.Imaging.ImageLockMode.WriteOnly,
                                         System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            bmpStride = bmpData.Stride;
            outputStride = outputData.Stride;

            //Traverse each pixel of the image
            unsafe
            {
                byte* bmpPtr = (byte*)bmpData.Scan0.ToPointer(),
                outputPtr = (byte*)outputData.Scan0.ToPointer();

                if (bytesPerPixel == 3)
                {
                    //Convert the pixel to it's luminance using the formula:
                    // L = .299*R + .587*G + .114*B
                    //Note that ic is the input column and oc is the output column
                    for (r = 0; r < h; r++)
                        for (ic = oc = 0; oc < w; ic += 3, ++oc)
                            outputPtr[r * outputStride + oc] = (byte)(int)
                                (0.299f * bmpPtr[r * bmpStride + ic] +
                                 0.587f * bmpPtr[r * bmpStride + ic + 1] +
                                 0.114f * bmpPtr[r * bmpStride + ic + 2]);
                }
                else //bytesPerPixel == 4
                {
                    //Convert the pixel to it's luminance using the formula:
                    // L = alpha * (.299*R + .587*G + .114*B)
                    //Note that ic is the input column and oc is the output column
                    for (r = 0; r < h; r++)
                        for (ic = oc = 0; oc < w; ic += 4, ++oc)
                            outputPtr[r * outputStride + oc] = (byte)(int)
                                ((bmpPtr[r * bmpStride + ic] / 255.0f) *
                                (0.299f * bmpPtr[r * bmpStride + ic + 1] +
                                 0.587f * bmpPtr[r * bmpStride + ic + 2] +
                                 0.114f * bmpPtr[r * bmpStride + ic + 3]));
                }
            }

            //Unlock the images
            bmp.UnlockBits(bmpData);
            output.UnlockBits(outputData);

            return output;
        }
        public String getUsercode()
        {
            try
            {
                String usercode = "";
                DataSet lds_userdetails = obj_datastore.GetRecords("select tbl_transactionDetails.* from tbl_transactionDetails order by transid");
                if (lds_userdetails != null)
                {
                    if (lds_userdetails.Tables[0].Rows.Count == 0)
                    {

                        usercode = "TID0000001";
                    }
                    else
                    {
                        usercode = lds_userdetails.Tables[0].Rows[lds_userdetails.Tables[0].Rows.Count - 1]["transid"].ToString();
                        usercode = usercode.Replace("TID", "");
                        int li_curValue = Convert.ToInt32(usercode);
                        li_curValue++;
                        int li_padlength = 10 - ("TID".Length + li_curValue.ToString().Length);
                        usercode = li_curValue.ToString();
                        for (int i = 0; i < li_padlength; i++)
                        {
                            usercode = "0" + usercode;
                        }
                        usercode = "TID" + usercode;
                    }
                }
                return usercode;
            }
            catch (Exception ex)
            {
                return "ERROR";
            }
        }

        ImageSource imgSource = null;
        Uri uri = null;
    }
}
