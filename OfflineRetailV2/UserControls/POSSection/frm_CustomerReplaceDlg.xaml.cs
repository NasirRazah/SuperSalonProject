using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OfflineRetailV2.Data;
using System.Data;
using System.Data.SqlClient;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_CustomerReplaceDlg.xaml
    /// </summary>
    public partial class frm_CustomerReplaceDlg : Window
    {
        private int intID;
        public int ID
        {
            get { return intID; }
            set { intID = value; }
        }

        private bool blOk;
        public bool Ok
        {
            get { return blOk; }
            set { blOk = value; }
        }

        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;

        public frm_CustomerReplaceDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnClose);
        }

        private void OnClose(object obj)
        {
            CloseKeyboards();
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fkybrd = new FullKeyboard();

            Title.Text = Properties.Resources.Combine_Customer;
            lbmsg.Visibility = Visibility.Hidden;
            txtFind.Text = Properties.Resources.Enter_First_Name_Last_Name_Address;
            txtFind.Tag = Properties.Resources.Enter_First_Name_Last_Name_Address;
            txtFind.FontFamily = new FontFamily("Trebuchet MS");
            txtFind.Foreground = Brushes.CornflowerBlue;
            PosDataObject.Customer objcust = new PosDataObject.Customer();
            objcust.Connection = SystemVariables.Conn;
            DataTable dtb = new DataTable();
            dtb = objcust.ShowRecord(intID);

            string strname = "";
            string strid = "";
            string stradd = "";
            foreach (DataRow dr in dtb.Rows)
            {
                string strAddress1 = "";
                string strAddress2 = "";
                string strCity = "";
                string strState = "";
                string strPostalCode = "";
                string strPhone = "";
                string strcitystatezip = "";
                strAddress1 = Settings.RegAddress1;
                strAddress2 = Settings.RegAddress2;
                strCity = Settings.RegCity;
                strState = Settings.RegState;
                strPostalCode = Settings.RegZip;
                strPhone = Settings.Phone;

                strAddress1 = dr["Address1"].ToString();
                strAddress2 = dr["Address2"].ToString();
                strCity = dr["City"].ToString();
                strState = dr["State"].ToString();
                strPostalCode = dr["Zip"].ToString();
                strPhone = dr["WorkPhone"].ToString();

                strname = dr["LastName"].ToString() + ", " + dr["FirstName"].ToString();
                if (Settings.AutoCustomer == "N") strid = "ID: " + dr["CustomerID"].ToString();

                if (strAddress1 != "") stradd = stradd + strAddress1 + "\n";

                if (strAddress2 != null)
                {
                    if (strAddress2 != "")
                    {
                        if (stradd != "") stradd = stradd + strAddress2 + "\n"; else stradd = strAddress2 + "\n";
                    }
                }

                if (strCity != null)
                {
                    if (strCity != "") strcitystatezip = strcitystatezip + strCity;
                }
                if (strState != null)
                {
                    if (strState != "")
                    {
                        if (strcitystatezip == "") strcitystatezip = strcitystatezip + strState;
                        else strcitystatezip = strcitystatezip + ", " + strState;
                    }
                }
                if (strPostalCode != null)
                {
                    if (strPostalCode != "")
                    {
                        if (strcitystatezip == "") strcitystatezip = strcitystatezip + strPostalCode;
                        else strcitystatezip = strcitystatezip + " " + strPostalCode;
                    }
                }
                if (strcitystatezip != "")
                {
                    if (stradd != "") stradd = stradd + strcitystatezip + "\n"; else stradd = strcitystatezip + "\n";
                }
                dtb.Dispose();
            }
            lbcustname.Text = strname;
            lbcustid.Text = strid;
            lbcustadd.Text = stradd;
            PosDataObject.Customer objcust1 = new PosDataObject.Customer();
            objcust1.Connection = SystemVariables.Conn;
            int val = objcust1.ActiveAppointmentCount(intID);
            if (val > 0) lbmsg.Visibility = Visibility.Visible;
            FetchData("");
        }

        private void FetchData(string findtext)
        {
            PosDataObject.Customer objcust = new PosDataObject.Customer();
            objcust.Connection = SystemVariables.Conn;
            DataTable dtb = new DataTable();
            dtb = objcust.GetCustomersToReplacedWith(intID, findtext, Settings.AutoCustomer, Settings.StoreCode);

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dtb.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            grdcust.ItemsSource = dtblTemp;
            dtb.Dispose();
            dtblTemp.Dispose();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtFind.Text.Trim() != "")
            {
                if (txtFind.Text.Trim() != Properties.Resources.Enter_First_Name_Last_Name_Address) FetchData(txtFind.Text.Trim());
            }
            else
            {
                txtFind.Text = Properties.Resources.Enter_First_Name_Last_Name_Address;
                txtFind.FontFamily = new FontFamily("Trebuchet MS");
                txtFind.Foreground = Brushes.CornflowerBlue;
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
            Close();
        }

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            int intRowID = -1;
            intRowID = gridView1.FocusedRowHandle;
            if ((grdcust.ItemsSource as DataTable).Rows.Count == 0) return;
            int RcrdID = GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, grdcust, colID)));
            if (RcrdID > 0)
            {
                if (DocMessage.MsgConfirmation("Do you want to continue?") == MessageBoxResult.Yes)
                {
                    PosDataObject.Customer objc = new PosDataObject.Customer();
                    objc.Connection = SystemVariables.Conn;
                    int ret = objc.ReplaceCustomer(intID, RcrdID, SystemVariables.CurrentUserID);
                    if (ret == 0) blOk = true;
                }
                CloseKeyboards();
                Close();
            }
        }

        private void TxtFind_GotFocus(object sender, RoutedEventArgs e)
        {
            txtFind.Foreground = Brushes.White;
            if (txtFind.Text.Trim() == Properties.Resources.Enter_First_Name_Last_Name_Address)
            {
                txtFind.Text = "";
                txtFind.FontFamily = new FontFamily("Tahoma");
            }

            if (Settings.UseTouchKeyboardInAdmin == "N") return;
            CloseKeyboards();

            if (!IsAboutFullKybrdOpen)
            {
                fkybrd = new FullKeyboard();

                var location = (sender as DevExpress.Xpf.Editors.TextEdit).PointToScreen(new Point(0, 0));
                fkybrd.Left = GeneralFunctions.fnInt32((SystemParameters.WorkArea.Width - 800) / 2);
                if (location.Y + 35 + 320 > System.Windows.SystemParameters.WorkArea.Height)
                {
                    fkybrd.Top = location.Y - 35 - 320;
                }
                else
                {
                    fkybrd.Top = location.Y + 35;
                }

                fkybrd.Height = 320;
                fkybrd.Width = 800;
                fkybrd.IsWindow = true;
                fkybrd.calledform = this;
                fkybrd.Closing += new System.ComponentModel.CancelEventHandler(FKybrd_Closing);
                fkybrd.Show();
                IsAboutFullKybrdOpen = true;
            }
        }

        private void FKybrd_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsAboutFullKybrdOpen)
            {
                e.Cancel = true;
            }
            else
            {
                IsAboutFullKybrdOpen = false;
                e.Cancel = false;
            }
        }

        private void TxtFind_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtFind.Text.Trim() == "")
            {
                txtFind.Text = Properties.Resources.Enter_First_Name_Last_Name_Address;
                txtFind.FontFamily = new FontFamily("Trebuchet MS");
                txtFind.Foreground = Brushes.CornflowerBlue;
            }
            CloseKeyboards();
        }

        private void CloseKeyboards()
        {
            if (fkybrd != null)
            {
                fkybrd.Close();
            }
           
        }
    }
}
