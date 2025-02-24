using System;
using System.Collections.Generic;
using System.Data;
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

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_POSProductAddnDlg.xaml
    /// </summary>
    public partial class frm_POSProductAddnDlg : Window
    {
        private string strProductType;
        private frm_MatrixProductPOS frmMatrix;
        private frm_UOMBrwPOS frm_UOMBrw;
        private frm_SerialBrwPOS frmSerial;
        private int intPID;

        private string strUOMCount;
        private string strUOMPrice;
        private string strUOMDesc;

        private string strMatrixOID;
        private string strMatrixOV1;
        private string strMatrixOV2;
        private string strMatrixOV3;

        private string strHeading;

        private string strSLID;
        private DataTable dtblSL;

        private string strSLExpiryDate;

        private bool ControlAdded = false;

        public DataTable dtblS
        {
            get { return dtblSL; }
            set { dtblSL = value; }
        }

        public string ProductType
        {
            get { return strProductType; }
            set { strProductType = value; }
        }

        public string Heading
        {
            get { return strHeading; }
            set { strHeading = value; }
        }

        public string SLID
        {
            get { return strSLID; }
            set { strSLID = value; }
        }

        public string UOMCount
        {
            get { return strUOMCount; }
            set { strUOMCount = value; }
        }

        public string UOMPrice
        {
            get { return strUOMPrice; }
            set { strUOMPrice = value; }
        }

        public string UOMDesc
        {
            get { return strUOMDesc; }
            set { strUOMDesc = value; }
        }

        public string MatrixOID
        {
            get { return strMatrixOID; }
            set { strMatrixOID = value; }
        }

        public string MatrixOV1
        {
            get { return strMatrixOV1; }
            set { strMatrixOV1 = value; }
        }

        public string MatrixOV2
        {
            get { return strMatrixOV2; }
            set { strMatrixOV2 = value; }
        }

        public string MatrixOV3
        {
            get { return strMatrixOV3; }
            set { strMatrixOV3 = value; }
        }

        public int PID
        {
            get { return intPID; }
            set { intPID = value; }
        }
        public frm_POSProductAddnDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnClose);
        }

        private void OnClose(object obj)
        {
            DialogResult = false;
            Close();
        }

        private async void BtnProdtSelect_Click(object sender, RoutedEventArgs e)
        {
            if (strProductType == "Unit of Measure")
            {
                strUOMCount = await GeneralFunctions.GetCellValue1(frm_UOMBrw.frm_UOMBrwUC.gridView1.FocusedRowHandle, frm_UOMBrw.frm_UOMBrwUC.grdUOM, frm_UOMBrw.frm_UOMBrwUC.colPackageCount);
                strUOMPrice = await GeneralFunctions.GetCellValue1(frm_UOMBrw.frm_UOMBrwUC.gridView1.FocusedRowHandle, frm_UOMBrw.frm_UOMBrwUC.grdUOM, frm_UOMBrw.frm_UOMBrwUC.colUnitPrice);
                strUOMDesc = await GeneralFunctions.GetCellValue1(frm_UOMBrw.frm_UOMBrwUC.gridView1.FocusedRowHandle, frm_UOMBrw.frm_UOMBrwUC.grdUOM, frm_UOMBrw.frm_UOMBrwUC.colDescription);
            }

            if (strProductType == "Matrix")
            {
                strMatrixOID = GetMatrixOptionID();
                strMatrixOV1 = await GeneralFunctions.GetCellValue1(frmMatrix.frm_MatrixProductUC.gridView1.FocusedRowHandle, frmMatrix.frm_MatrixProductUC.grdOption1, frmMatrix.frm_MatrixProductUC.colOption1Value);
                strMatrixOV2 = await GeneralFunctions.GetCellValue1(frmMatrix.frm_MatrixProductUC.gridView2.FocusedRowHandle, frmMatrix.frm_MatrixProductUC.grdOption2, frmMatrix.frm_MatrixProductUC.colOption2Value);
                strMatrixOV3 = await GeneralFunctions.GetCellValue1(frmMatrix.frm_MatrixProductUC.gridView3.FocusedRowHandle, frmMatrix.frm_MatrixProductUC.grdOption3, frmMatrix.frm_MatrixProductUC.colOption3Value);
            }

            if (strProductType == "Serialized")
            {
                strSLID = await GeneralFunctions.GetCellValue1(frmSerial.frm_SerialBrwUC.gridView1.FocusedRowHandle, frmSerial.frm_SerialBrwUC.grdSerialized, frmSerial.frm_SerialBrwUC.colID);
                strSLExpiryDate = await GeneralFunctions.GetCellValue1(frmSerial.frm_SerialBrwUC.gridView1.FocusedRowHandle, frmSerial.frm_SerialBrwUC.grdSerialized, frmSerial.frm_SerialBrwUC.colExpiry);
            }

            if (strProductType == "Matrix")
            {
                if (strMatrixOID != "0")
                {
                    DialogResult = true;
                    Close();
                }
                else DialogResult = false;
            }
            if (strProductType == "Unit of Measure")
            {
                if (strUOMCount != "")
                {
                    DialogResult = true;
                    Close();
                }
                else DialogResult = false;
            }

            if (strProductType == "Serialized")
            {
                if (strSLID != "")
                {
                    if (strSLExpiryDate != "")
                    {
                        PosDataObject.Product objMatx = new PosDataObject.Product();
                        objMatx.Connection = SystemVariables.Conn;
                        string actualexpiry = objMatx.GetProductExpiryForSerialisedItem(GeneralFunctions.fnInt32(strSLID));
                      
                        DisplayItemExpiryAlert(actualexpiry);
                    }

                    DialogResult = true;
                    Close();
                }
                else DialogResult = false;
            }

        }

        public async Task SelectSerialProduct()
        {

            strSLID = await GeneralFunctions.GetCellValue1(frmSerial.frm_SerialBrwUC.gridView1.FocusedRowHandle, frmSerial.frm_SerialBrwUC.grdSerialized, frmSerial.frm_SerialBrwUC.colID);
            strSLExpiryDate = await GeneralFunctions.GetCellValue1(frmSerial.frm_SerialBrwUC.gridView1.FocusedRowHandle, frmSerial.frm_SerialBrwUC.grdSerialized, frmSerial.frm_SerialBrwUC.colExpiry);

            if (strSLID != "")
            {
                if (strSLExpiryDate != "")
                {
                    PosDataObject.Product objMatx = new PosDataObject.Product();
                    objMatx.Connection = SystemVariables.Conn;
                    string actualexpiry = objMatx.GetProductExpiryForSerialisedItem(GeneralFunctions.fnInt32(strSLID));

                    DisplayItemExpiryAlert(actualexpiry);
                }

                DialogResult = true;
                Close();
            }
            else DialogResult = false;
        }


        private void DisplayItemExpiryAlert(string ExpiryDate)
        {
            if (GeneralFunctions.fnDate(ExpiryDate).Date < DateTime.Today.Date)
            {
                new MessageBoxWindow().Show("This product has expired on \r\n" + GeneralFunctions.fnDate(ExpiryDate).ToString("MMMM d, yyyy"), "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                int dayBetween = (int)(GeneralFunctions.fnDate(ExpiryDate).Date - DateTime.Today.Date).TotalDays;
                if (dayBetween <= Settings.ItemExpiryAlertDay)
                {
                    if (dayBetween == 0)
                    {
                        new MessageBoxWindow().Show("This product will be expire on Today", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else if (dayBetween == 1)
                    {
                        new MessageBoxWindow().Show("This product will be expire on Tommorrow", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        new MessageBoxWindow().Show("This product will be expire on \r\n" + GeneralFunctions.fnDate(ExpiryDate).ToString("MMMM d, yyyy"), "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }

        }

        private string GetMatrixOptionID()
        {
            PosDataObject.Matrix objMatx = new PosDataObject.Matrix();
            objMatx.Connection = SystemVariables.Conn;
            return Convert.ToString(objMatx.FetchMatrixOptionID(intPID));
        }

        

        private async void BtnUpProduct_Click(object sender, RoutedEventArgs e)
        {

            if (strProductType == "Matrix")
            {
                if (frmMatrix.ActiveGrid == 1) frmMatrix.frm_MatrixProductUC.gridView1.FocusedRowHandle = frmMatrix.frm_MatrixProductUC.gridView1.FocusedRowHandle - 1;
                if (frmMatrix.ActiveGrid == 2) frmMatrix.frm_MatrixProductUC.gridView2.FocusedRowHandle = frmMatrix.frm_MatrixProductUC.gridView2.FocusedRowHandle - 1;
                if (frmMatrix.ActiveGrid == 3) frmMatrix.frm_MatrixProductUC.gridView3.FocusedRowHandle = frmMatrix.frm_MatrixProductUC.gridView3.FocusedRowHandle - 1;
            }
            if (strProductType == "Unit of Measure")
            {
                frm_UOMBrw.frm_UOMBrwUC.gridView1.FocusedRowHandle = frm_UOMBrw.frm_UOMBrwUC.gridView1.FocusedRowHandle - 1;
            }
            if (strProductType == "Serialized")
            {
                await frmSerial.UpScroll();
                //frmSerial.frm_SerialBrwUC.gridView1.FocusedRowHandle = frmSerial.frm_SerialBrwUC.gridView1.FocusedRowHandle - 1;
            }
        }

        private async void BtnDownProduct_Click(object sender, RoutedEventArgs e)
        {

            if (strProductType == "Matrix")
            {
                if (frmMatrix.ActiveGrid == 1) frmMatrix.frm_MatrixProductUC.gridView1.FocusedRowHandle = frmMatrix.frm_MatrixProductUC.gridView1.FocusedRowHandle + 1;
                if (frmMatrix.ActiveGrid == 2) frmMatrix.frm_MatrixProductUC.gridView2.FocusedRowHandle = frmMatrix.frm_MatrixProductUC.gridView2.FocusedRowHandle + 1;
                if (frmMatrix.ActiveGrid == 3) frmMatrix.frm_MatrixProductUC.gridView3.FocusedRowHandle = frmMatrix.frm_MatrixProductUC.gridView3.FocusedRowHandle + 1;
            }
            if (strProductType == "Unit of Measure")
            {
                frm_UOMBrw.frm_UOMBrwUC.gridView1.FocusedRowHandle = frm_UOMBrw.frm_UOMBrwUC.gridView1.FocusedRowHandle + 1;
            }
            if (strProductType == "Serialized")
            {
                //frmSerial.frm_SerialBrwUC.gridView1.FocusedRowHandle = frmSerial.frm_SerialBrwUC.gridView1.FocusedRowHandle + 1;

                await frmSerial.DownScroll();
            }
        }

        private void SimpleButton1_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }



        private  void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
            Title.Text = strHeading;
            if (strProductType == "Unit of Measure") // Unit of Measure
            {

                frm_UOMBrw = new frm_UOMBrwPOS();
                frm_UOMBrw.frm_UOMBrwUC.bar1.Visibility = Visibility.Collapsed;
                frm_UOMBrw.pnlbottom.Visibility = Visibility.Collapsed;
                frm_UOMBrw.PID = intPID;
                pnlMain.Children.Add(frm_UOMBrw);
                frm_UOMBrw.FetchData();
                //await frm_UOMBrw.FocusDefault();
                //ControlAdded = true;
            }

            if (strProductType == "Matrix") // Matrix
            {

                frmMatrix = new frm_MatrixProductPOS();
                frmMatrix.FID = intPID;
                frmMatrix.PID = intPID;
          

                frmMatrix.frm_MatrixProductUC.bar1.Visibility = Visibility.Collapsed;
                frmMatrix.frm_MatrixProductUC.bar2.Visibility = Visibility.Collapsed;
                frmMatrix.frm_MatrixProductUC.bar3.Visibility = Visibility.Collapsed;
                frmMatrix.panel2.Visibility = Visibility.Collapsed;
                frmMatrix.frm_MatrixProductUC.tpOnHand.Visibility = Visibility.Collapsed;
                pnlMain.Children.Add(frmMatrix);
                frmMatrix.LoadData();
                frmMatrix.ActiveGrid = 1;
            }

            if (strProductType == "Serialized") // Serialized
            {

                frmSerial = new frm_SerialBrwPOS();
                frmSerial.CAT = "POS";
                frmSerial.dtblS = dtblSL;
                frmSerial.PID = intPID;
                if (frmSerial.FetchPOSFlag() == "N")
                   frmSerial.frm_SerialBrwUC.bar1.Visibility = Visibility.Collapsed;
                else
                {
                   frmSerial.frm_SerialBrwUC.bar1.Visibility = Visibility.Visible;
                    frmSerial.frm_SerialBrwUC.btnAdd.Visibility = Visibility.Visible;
                    frmSerial.frm_SerialBrwUC.btnEdit.Visibility = Visibility.Visible;
                    frmSerial.frm_SerialBrwUC.btnDelete.Visibility = Visibility.Visible;
                }
                frmSerial.frm_SerialBrwUC.colSold.Visible = false;
                pnlMain.Children.Add(frmSerial);
                frmSerial.FetchData();
            }
        }
    }
}
