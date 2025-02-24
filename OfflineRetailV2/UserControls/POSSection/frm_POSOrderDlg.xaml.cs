using OfflineRetailV2.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_POSOrderDlg.xaml
    /// </summary>
    public partial class frm_POSOrderDlg : Window
    {
        public frm_POSOrderDlg()
        {
            InitializeComponent();

            Loaded += Frm_POSOrderDlg_Loaded;
        }

        private void Frm_POSOrderDlg_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text =Properties.Resources.List_of_Active_Customer_Orders ;
            blFlag = false;
            SetInitialFilter();
            blFlag = true;
            FetchData();
        }

        private void FullKbd_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
        }

        private int intSelectID;
        private bool blFlag;
        private int iCustomerID = 0;

        public bool Flag
        {
            get { return blFlag; }
            set { blFlag = value; }
        }

        public int SelectID
        {
            get { return intSelectID; }
            set { intSelectID = value; }
        }

        private void SetCustomerInfo()
        {
            //lbC2.Text = "";--Sam
            //lbC3.Text = "";
            //lbC4.Text = "";
            //picPhone.Visible = false;
            PosDataObject.PO objDObj = new PosDataObject.PO();
            objDObj.Connection = SystemVariables.Conn;
            DataTable dtbl = objDObj.FetchCustomerInfo(iCustomerID);
            foreach (DataRow dr in dtbl.Rows)
            {
                //lbC3.Text = dr["MailAddress"].ToString();--Sam
                if (dr["Company"].ToString() != "")
                {
                    //lbC2.Text = dr["Company"].ToString();--Sam
                }

                if (dr["MobilePhone"].ToString() != "")
                {
                    //lbC4.Text = dr["MobilePhone"].ToString(); --Sam
                    //picPhone.Visible = true;
                }
                btnSearchCustomer.Content = dr["CustomerName"].ToString();
                if (btnSearchCustomer.Content.ToString().Length > 20)
                {
                    ShowCustomerPhoto(48);
                }
                else
                {
                    ShowCustomerPhoto(76);
                }
            }
        }

        private void ShowCustomerPhoto(int imgsize)
        {
            string strPhotoFile = "";
            // *** Display [PHOTO]  *** //	
            string csStorePath = Environment.CurrentDirectory + "\\CustomerPhotos";
            int intImageWidth = 48;
            int intImageHeight = 48;
            strPhotoFile = csStorePath + "\\" + iCustomerID.ToString() + ".jpg";
            DocManager.Controls.DocPictureBox pic = new DocManager.Controls.DocPictureBox();

            //if (!GeneralFunctions.GetPhotoFromTable(pic, iCustomerID, "Customer")) --Sam
            //{
            //    //pic.Image = null;
            //    //btnSearchCustomer.Image = pos.Properties.Resources.cust;
            //}
            //else
            //{
            //    double AspectRatio = 0.00;
            //    int intWidth, intHeight;
            //    AspectRatio = GeneralFunctions.fnDouble(pic.Image.Width) /
            //        GeneralFunctions.fnDouble(pic.Image.Height);
            //    intHeight = pic.Height;
            //    intWidth = Convert.ToInt16(GeneralFunctions.fnDouble(intHeight) * AspectRatio);

            //    if (intWidth > intImageWidth)
            //    {
            //        intWidth = intImageWidth;
            //        intHeight = Convert.ToInt16(GeneralFunctions.fnDouble(intWidth) / AspectRatio);
            //    }
            //    pic.Width = intWidth;
            //    pic.Height = intHeight;

            //    //Bitmap image = new Bitmap(ScaleImage(pic.Image, imgsize, imgsize));
            //    //btnSearchCustomer.Image = image;

            //    pic.Dispose();
            //}
        }

        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
            return newImage;
        }

        public void SetInitialFilter()
        {
            tswOrderDate.IsChecked = false;
            dtOrder1.DateTime = DateTime.Today.Date;
            dtOrder2.DateTime = DateTime.Today.Date;
            tswPickupDate.IsChecked = true;
            dtPickup1.DateTime = DateTime.Today.Date;
            dtPickup2.DateTime = DateTime.Today.Date;
            tswCustomer.IsChecked = false;
        }

        private void tswPickupDate_Checked(object sender, RoutedEventArgs e)
        {
            FetchData();
        }

        private void tswCustomer_Checked(object sender, RoutedEventArgs e)
        {
            FetchData();
        }

        private void btnSearchCustomer_MouseEnter(object sender, MouseEventArgs e)
        {
            if (iCustomerID == 0)
            {

                btnSearchCustomer.ToolTip = Properties.Resources.Select_Customer;
            }
            else
            {
                btnSearchCustomer.ToolTip = Properties.Resources.Change_to_All_Customers; 
            }
        }

        public void FetchData()
        {
            if (blFlag)
            {
                if (tswPickupDate.IsChecked==true)
                {
                    if ((dtPickup1.EditValue == null) || (dtPickup2.EditValue == null)) return;
                }

                if (tswPickupDate.IsChecked==true)
                {
                    if ((dtPickup1.EditValue == null) || (dtPickup2.EditValue == null)) return;
                }

                try
                {
                    try
                    {
                        (grd.ItemsSource as DataTable).Rows.Clear();
                    }
                    catch
                    {
                    }
                    DataTable dtbl = new DataTable();
                    PosDataObject.POS objPO = new PosDataObject.POS();
                    objPO.Connection = SystemVariables.Conn;
                    dtbl = objPO.FetchActiveCustomerOrders(tswOrderDate.IsChecked==true ? "Y" : "N", tswPickupDate.IsChecked == true ? "Y" : "N", tswCustomer.IsChecked == true ? "Y" : "N",
                        tswOrderDate.IsChecked == true ? dtOrder1.DateTime.Date : DateTime.Today.Date, tswOrderDate.IsChecked == true ? dtOrder2.DateTime.Date : DateTime.Today.Date,
                        tswPickupDate.IsChecked == true ? dtPickup1.DateTime.Date : DateTime.Today.Date, tswPickupDate.IsChecked == true ? dtPickup2.DateTime.Date : DateTime.Today.Date,
                        iCustomerID, txtFirstName.Text.Trim(), txtLastName.Text.Trim(), txtPhone.Text.Trim());

                    grd.ItemsSource = dtbl;

                    dtbl.Dispose();
                }
                catch
                {

                }

            }
        }

        private void dtPickup1_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            FetchData();
        }

        private void dtPickup2_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            FetchData();
        }

        private void txtFirstName_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            FetchData();
        }

        private void tswOrderDate_Checked(object sender, RoutedEventArgs e)
        {
            FetchData();
        }

        private void dtOrder1_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            FetchData();
        }

        private void dtOrder2_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            FetchData();
        }
    }
}
