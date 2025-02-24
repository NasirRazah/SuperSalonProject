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
using OfflineRetailV2;
using OfflineRetailV2.Data;
using System.Data;
using System.Data.SqlClient;
using DevExpress.XtraReports.UI;
using Microsoft.PointOfService;
using System.IO;
using System.IO.Ports;
using DevExpress.Xpf.Printing;
using System.Drawing;
using DevExpress.Xpf.Grid.LookUp;
using DevExpress.Xpf.Grid;


// using Seagull.BarTender.Print;

namespace OfflineRetailV2.UserControls.Administrator
{
    /// <summary>
    /// Interaction logic for frm_PrintBarcodeDlg.xaml
    /// </summary>
    public partial class frm_PrintBarcodeDlg : Window
    {
        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;
        SerialPort _sport = null;
        PosExplorer m_posExplorer = null;
        PosCommon m_posCommon = null;

        private string strProductDesc;
        private string strSKU;
        private string strProductPrice;
        private int intProductDecimalPlace;
        private int intQty;
        private int intskip = 0;
        private int intLabelType;


        private string VID = "";
        private string VNAME = "";
        private string VPART = "";
        private string LD = "";
        private string LD2 = "";
        private string INGD = "";
        private string SPLM = "";
        private string BC = "";
        private string WHT = "";

        private string comport = "";
        private string comprintcommand = "";
        private int comdefaultqty = 0;
        private int comwrap = 0;
        private string comparafont = "";

        private bool blisbatchprint;

        public bool isbatchprint
        {
            get { return blisbatchprint; }
            set { blisbatchprint = value; }
        }

        public int LabelType
        {
            get { return intLabelType; }
            set { intLabelType = value; }
        }

        public int Qty
        {
            get { return intQty; }
            set { intQty = value; }
        }

        public string SKU
        {
            get { return strSKU; }
            set { strSKU = value; }
        }

        public string ProductDesc
        {
            get { return strProductDesc; }
            set { strProductDesc = value; }
        }

        public string ProductPrice
        {
            get { return strProductPrice; }
            set { strProductPrice = value; }
        }

        public int ProductDecimalPlace
        {
            get { return intProductDecimalPlace; }
            set { intProductDecimalPlace = value; }
        }

        public frm_PrintBarcodeDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommand);
        }

        private void OnCloseCommand(object obj)
        {
            CloseKeyboards();
            Close();
        }

        public void PopulateCOMPrinter()
        {
            PosDataObject.Setup objShift = new PosDataObject.Setup();
            objShift.Connection = SystemVariables.Conn;
            DataTable dbtblShift = new DataTable();
            dbtblShift = objShift.FetchCOMPrinterLookup();

            cmbCOMPrinter.ItemsSource = dbtblShift;
           
            cmbCOMPrinter.EditValue = null;
            dbtblShift.Dispose();
        }


        private void PopulateWindowsPrinter()
        {
            DataTable dtbl = new DataTable();

            dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
            dtbl.Columns.Add("Printer", System.Type.GetType("System.String"));

            foreach (string strPrinter in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                dtbl.Rows.Add(new object[] { strPrinter, strPrinter });
            }

            cmbWinPrinter.ItemsSource = dtbl;
            
            cmbWinPrinter.EditValue = null;
            dtbl.Dispose();

        }

        private void RgLabelPrinter_0_Checked(object sender, RoutedEventArgs e)
        {
            if (rgLabelPrinter_0.IsChecked == true)
            {
                cmbWinPrinter.Visibility = Visibility.Visible;
                cmbCOMPrinter.Visibility = Visibility.Hidden;
                pnlWin.Visibility = Visibility.Visible;
                
            }

            if (rgLabelPrinter_1.IsChecked == true)
            {
                cmbWinPrinter.Visibility = Visibility.Hidden;
                cmbCOMPrinter.Visibility = Visibility.Visible;
                pnlWin.Visibility = Visibility.Hidden;
            }
        }

        private void Rg_1_Checked(object sender, RoutedEventArgs e)
        {
            if (Settings.BarTenderLabelPrint == "Y")
            {
                //bartender
                lbRow.Visibility = Visibility.Hidden;
                lbColumn.Visibility = Visibility.Hidden;
                spnrow.Visibility = Visibility.Hidden;
                spncol.Visibility = Visibility.Hidden;
            }
            else
            {
                if (rg_4.IsChecked == true)
                {
                    lbRow.Visibility = Visibility.Visible;
                    lbColumn.Visibility = Visibility.Visible;
                    spnrow.Visibility = Visibility.Visible;
                    spncol.Visibility = Visibility.Visible;
                    spncol.Items.Clear();
                    spncol.Items.Add("1");
                    spncol.Items.Add("2");
                    spncol.Items.Add("3");
                    spncol.SelectedIndex = 0;
                    spnrow.Items.Clear();
                    spnrow.Items.Add("1");
                    spnrow.Items.Add("2");
                    spnrow.Items.Add("3");
                    spnrow.Items.Add("4");
                    spnrow.Items.Add("5");
                    spnrow.Items.Add("6");
                    spnrow.Items.Add("7");
                    spnrow.Items.Add("8");
                    spnrow.Items.Add("9");
                    spnrow.Items.Add("10");
                    spnrow.SelectedIndex = 0;
                }
                else if (rg_5.IsChecked == true)
                {
                    lbRow.Visibility = Visibility.Visible;
                    lbColumn.Visibility = Visibility.Visible;
                    spnrow.Visibility = Visibility.Visible;
                    spncol.Visibility = Visibility.Visible;
                    spncol.Items.Clear();
                    spncol.Items.Add("1");
                    spncol.Items.Add("2");
                    spncol.Items.Add("3");
                    spncol.Items.Add("4");
                    spncol.SelectedIndex = 0;
                    spnrow.Items.Clear();
                    spnrow.Items.Add("1");
                    spnrow.Items.Add("2");
                    spnrow.Items.Add("3");
                    spnrow.Items.Add("4");
                    spnrow.Items.Add("5");
                    spnrow.Items.Add("6");
                    spnrow.Items.Add("7");
                    spnrow.Items.Add("8");
                    spnrow.Items.Add("9");
                    spnrow.Items.Add("10");
                    spnrow.Items.Add("11");
                    spnrow.Items.Add("12");
                    spnrow.Items.Add("13");
                    spnrow.Items.Add("14");
                    spnrow.Items.Add("15");
                    spnrow.SelectedIndex = 0;
                }
                else
                {
                    lbRow.Visibility = Visibility.Hidden;
                    lbColumn.Visibility = Visibility.Hidden;
                    spnrow.Visibility = Visibility.Hidden;
                    spncol.Visibility = Visibility.Hidden;

                    if (rg_2.IsChecked == true)
                    {
                        lbColumn.Visibility = Visibility.Visible;
                        spncol.Visibility = Visibility.Visible;
                        spncol.Items.Clear();
                        spncol.Items.Add("1");
                        spncol.Items.Add("2");
                        spncol.SelectedIndex = 0;
                    }
                }

                if (rg_1.IsChecked == true) txtwidth.Text = "0";
                if (rg_2.IsChecked == true) txtwidth.Text = "224";

                if (rg_1.IsChecked == true)
                {
                    populateOneUpTemplate();
                }
                if (rg_2.IsChecked == true)
                {
                    populateTwoUpTemplate();
                }
                if (rg_3.IsChecked == true)
                {
                    populateButterflyTemplate();
                }
                if (rg_4.IsChecked == true)
                {
                    populateAvery5160Template();
                }
                if (rg_5.IsChecked == true)
                {
                    populateAvery8195Template();
                }
            }
        }

        private void Chkadv_Checked(object sender, RoutedEventArgs e)
        {
            pnltest.Visibility = chkadv.IsChecked == true? Visibility.Visible : Visibility.Hidden;
        }

        private void BtnAdvLx_Click(object sender, RoutedEventArgs e)
        {
            GeneralFunctions.CreateBarCodePrintCommandInFile(rg_2.IsChecked == true ? true : false, GeneralFunctions.fnInt32(txtwidth.Text), strSKU, strProductDesc, intQty, strProductPrice);
            RawPrinterHelper.SendFileToPrinter(cmbWinPrinter.EditValue.ToString(), GeneralFunctions.FetchCurrentUserPath_BarCodePrinting() + "\\prncmd.txt");
        }

        private void setPrintPosition()
        {
            int r = 0;
            int c = 0;
            r = GeneralFunctions.fnInt32(spnrow.Text);
            c = GeneralFunctions.fnInt32(spncol.Text);
            if (r == 1)
            {
                intskip = c - 1;
            }
            else
            {
                intskip = (r * 3) - 3 + c - 1;
            }
        }

        private void setPrintPosition1()
        {
            int r = 0;
            int c = 0;
            r = GeneralFunctions.fnInt32(spnrow.Text);
            c = GeneralFunctions.fnInt32(spncol.Text);
            if (r == 1)
            {
                intskip = c - 1;
            }
            else
            {
                intskip = (r * 4) - 4 + c - 1;
            }
        }

        private DataTable LoadPrintDataTable()
        {
            string strPrice = "";
            if (strProductPrice != "")
            {
                double dblPrice = GeneralFunctions.fnDouble(strProductPrice);

                if (intProductDecimalPlace == 3)
                {
                    strPrice = SystemVariables.CurrencySymbol + " " + dblPrice.ToString("f3");
                }
                else
                {
                    strPrice = SystemVariables.CurrencySymbol + " " + dblPrice.ToString("f");
                }
            }
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("COMPANY", System.Type.GetType("System.String"));
            dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
            dtbl.Columns.Add("DESC", System.Type.GetType("System.String"));
            dtbl.Columns.Add("PRICE", System.Type.GetType("System.String"));
            if ((rg_1.IsChecked == true) || (rg_3.IsChecked == true))
            {
                if ((rg_3.IsChecked == true) && (spncol.SelectedIndex == 1)) // starts from 2nd col
                {
                    dtbl.Rows.Add(new object[] { "", "", "", "" });
                }
                for (int i = 1; i <= GeneralFunctions.fnInt32(spinEdit1.Text); i++)
                {
                    dtbl.Rows.Add(new object[] { Settings.Company, strSKU, strProductDesc, strPrice });
                }
            }
            if ((rg_4.IsChecked == true) || (rg_2.IsChecked == true) || (rg_5.IsChecked == true))
            {
                for (int i = 1; i <= GeneralFunctions.fnInt32(spinEdit1.Text) + 1; i++)
                {
                    dtbl.Rows.Add(new object[] { Settings.Company, strSKU, strProductDesc, strPrice });
                }
            }
            return dtbl;
        }

        private void ExecuteLabel4()
        {
            if (Settings.BarTenderLabelPrint == "Y")
            {
                System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
                string dirName = System.IO.Path.GetDirectoryName(asm.Location);
                string strLabelFile = dirName + @"\LabelFormats\" + "Avery8195.btw";
                PrintBarTenderLabel(strLabelFile);

            }

            else
            {
                if (lbOneUpTemplate.Visibility == Visibility.Visible)
                {
                    setPrintPosition1();

                    DataTable dtbl = new DataTable();
                    dtbl = LoadPrintDataTable();


                    XtraReport fReport = new XtraReport();

                    if (File.Exists(GeneralFunctions.CheckLabelPrintingCustomisedFile("Avery8195", cmbOneUpTemplate.EditValue.ToString())))
                    {
                        fReport = XtraReport.FromFile(GeneralFunctions.CheckLabelPrintingCustomisedFile("Avery8195", cmbOneUpTemplate.EditValue.ToString()), true);
                    }
                    else
                    {
                        fReport = XtraReport.FromFile(GeneralFunctions.GetLabelPrintingDefaultFile("Avery8195"), true);
                    }

                    (fReport as OfflineRetailV2.Report.Product.Avery8195).skpno = intskip;
                    (fReport as OfflineRetailV2.Report.Product.Avery8195).prtno = GeneralFunctions.fnInt32(spinEdit1.Text);
                    if (intskip == 0)
                    {
                        (fReport as OfflineRetailV2.Report.Product.Avery8195).firstp = true;
                    }
                    else
                    {
                        (fReport as OfflineRetailV2.Report.Product.Avery8195).firstp = false;
                    }

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dtbl);
                    fReport.Report.DataSource = ds;
                    fReport.DataSource = ds;

                    try
                    {
                        fReport.PrinterName = cmbWinPrinter.EditValue.ToString();
                        fReport.CreateDocument();
                        fReport.PrintingSystem.ShowMarginsWarning = false;
                        fReport.PrintingSystem.ShowPrintStatusDialog = false;
                        DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                        window.PreviewControl.DocumentSource = fReport;
                        window.ShowDialog();

                    }
                    finally
                    {
                        fReport.Dispose();

                        dtbl.Dispose();
                    }



                }
                else
                {
                    setPrintPosition1();

                    DataTable dtbl = new DataTable();
                    dtbl = LoadPrintDataTable();

                    OfflineRetailV2.Report.Product.Avery8195 rep_PrintLabel5 = new OfflineRetailV2.Report.Product.Avery8195();

                    string repxFile = "";
                    repxFile = "Avery8195.repx";

                    XtraReport fReport = new XtraReport();


                    if (File.Exists(GeneralFunctions.GetLabelPrintingFilePath(repxFile)))
                        fReport = XtraReport.FromFile(GeneralFunctions.GetLabelPrintingFilePath(repxFile), true);
                    else
                    {
                        fReport = rep_PrintLabel5;
                    }
                    if (!File.Exists(GeneralFunctions.GetLabelPrintingFilePath(repxFile)))
                    {
                        string fileName = GeneralFunctions.GetLabelPrintingPath(fReport, "repx");
                        fReport.SaveLayout(fileName);
                    }

                (fReport as OfflineRetailV2.Report.Product.Avery8195).skpno = intskip;
                    (fReport as OfflineRetailV2.Report.Product.Avery8195).prtno = GeneralFunctions.fnInt32(spinEdit1.Text);
                    if (intskip == 0)
                    {
                        (fReport as OfflineRetailV2.Report.Product.Avery8195).firstp = true;
                    }
                    else
                    {
                        (fReport as OfflineRetailV2.Report.Product.Avery8195).firstp = false;
                    }

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dtbl);

                    fReport.Report.DataSource = ds;
                    fReport.DataSource = ds;

                    ////frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
                    try
                    {
                        fReport.PrinterName = cmbWinPrinter.EditValue.ToString();
                        fReport.CreateDocument();
                        fReport.PrintingSystem.ShowMarginsWarning = false;
                        fReport.PrintingSystem.ShowPrintStatusDialog = false;

                        //fReport.ShowPreviewDialog();

                        DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                        window.PreviewControl.DocumentSource = fReport;
                        window.ShowDialog();

                    }
                    finally
                    {
                        fReport.Dispose();
                        rep_PrintLabel5.Dispose();

                        dtbl.Dispose();
                    }
                }
            }
        }

        private void ExecuteLabel()
        {
            if (Settings.BarTenderLabelPrint == "Y")
            {
                System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
                string dirName = System.IO.Path.GetDirectoryName(asm.Location);
                string strLabelFile = dirName + @"\LabelFormats\" + "Avery5160.btw";
                PrintBarTenderLabel(strLabelFile);
                //bartender
            }
            else
            {
                if (lbOneUpTemplate.Visibility == Visibility.Visible)
                {
                    setPrintPosition();

                    DataTable dtbl = new DataTable();
                    dtbl = LoadPrintDataTable();


                    XtraReport fReport = new XtraReport();

                    if (File.Exists(GeneralFunctions.CheckLabelPrintingCustomisedFile("Avery5160", cmbOneUpTemplate.EditValue.ToString())))
                    {
                        fReport = XtraReport.FromFile(GeneralFunctions.CheckLabelPrintingCustomisedFile("Avery5160", cmbOneUpTemplate.EditValue.ToString()), true);
                    }
                    else
                    {
                        fReport = XtraReport.FromFile(GeneralFunctions.GetLabelPrintingDefaultFile("Avery5160"), true);
                    }

                    (fReport as OfflineRetailV2.Report.Product.Avery5160).skpno = intskip;
                    (fReport as OfflineRetailV2.Report.Product.Avery5160).prtno = GeneralFunctions.fnInt32(spinEdit1.Text);
                    if (intskip == 0)
                    {
                        (fReport as OfflineRetailV2.Report.Product.Avery5160).firstp = true;
                    }
                    else
                    {
                        (fReport as OfflineRetailV2.Report.Product.Avery5160).firstp = false;
                    }

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dtbl);
                    fReport.Report.DataSource = ds;
                    fReport.DataSource = ds;

                    try
                    {
                        fReport.PrinterName = cmbWinPrinter.EditValue.ToString();
                        fReport.CreateDocument();
                        fReport.PrintingSystem.ShowMarginsWarning = false;
                        fReport.PrintingSystem.ShowPrintStatusDialog = false;
                        DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                        window.PreviewControl.DocumentSource = fReport;
                        window.ShowDialog();

                    }
                    finally
                    {
                        fReport.Dispose();

                        dtbl.Dispose();
                    }



                }
                else
                {
                    setPrintPosition();


                    DataTable dtbl = new DataTable();
                    dtbl = LoadPrintDataTable();

                    OfflineRetailV2.Report.Product.Avery5160 rep_PrintLabel = new OfflineRetailV2.Report.Product.Avery5160();

                    string repxFile = "";
                    repxFile = "Avery5160.repx";

                    XtraReport fReport = new XtraReport();


                    if (File.Exists(GeneralFunctions.GetLabelPrintingFilePath(repxFile)))
                        fReport = XtraReport.FromFile(GeneralFunctions.GetLabelPrintingFilePath(repxFile), true);
                    else
                    {
                        fReport = rep_PrintLabel;
                    }
                    if (!File.Exists(GeneralFunctions.GetLabelPrintingFilePath(repxFile)))
                    {
                        string fileName = GeneralFunctions.GetLabelPrintingPath(fReport, "repx");
                        fReport.SaveLayout(fileName);
                    }

                    (fReport as OfflineRetailV2.Report.Product.Avery5160).skpno = intskip;
                    (fReport as OfflineRetailV2.Report.Product.Avery5160).prtno = GeneralFunctions.fnInt32(spinEdit1.Text);
                    if (intskip == 0)
                    {
                        (fReport as OfflineRetailV2.Report.Product.Avery5160).firstp = true;
                    }
                    else
                    {
                        (fReport as OfflineRetailV2.Report.Product.Avery5160).firstp = false;
                    }

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dtbl);

                    fReport.Report.DataSource = ds;
                    fReport.DataSource = ds;

                    try
                    {
                        fReport.PrinterName = cmbWinPrinter.EditValue.ToString();
                        fReport.CreateDocument();
                        fReport.PrintingSystem.ShowMarginsWarning = false;
                        fReport.PrintingSystem.ShowPrintStatusDialog = false;


                        DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                        window.PreviewControl.DocumentSource = fReport;
                        window.ShowDialog();

                    }
                    finally
                    {
                        fReport.Dispose();
                        rep_PrintLabel.Dispose();

                        dtbl.Dispose();
                    }
                }
            }
        }

        private void ExecuteLabel1()
        {
            //frmPreviewControl frm_PreviewControl = new frmPreviewControl();

            DataTable dtbl = new DataTable();
            dtbl = LoadPrintDataTable();

            OfflineRetailV2.Report.Product.repPrintLabel rep_PrintLabel = new OfflineRetailV2.Report.Product.repPrintLabel();
            DataSet ds = new DataSet();
            ds.Tables.Add(dtbl);
            rep_PrintLabel.DataSource = ds;


            //frmPreviewControl1 frm_PreviewControl = new frmPreviewControl1();
            try
            {
                rep_PrintLabel.PrinterName = cmbWinPrinter.EditValue.ToString();
                rep_PrintLabel.CreateDocument();
                rep_PrintLabel.PrintingSystem.ShowMarginsWarning = false;
                rep_PrintLabel.PrintingSystem.ShowPrintStatusDialog = false;

                //rep_PrintLabel.ShowPreviewDialog();

                DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                window.PreviewControl.DocumentSource = rep_PrintLabel;
                window.ShowDialog();

            }
            finally
            {
                rep_PrintLabel.Dispose();


                dtbl.Dispose();
            }
        }

        private void ExecuteLabel3()
        {
            if (Settings.BarTenderLabelPrint == "Y")
            {
                 System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
                 string dirName = System.IO.Path.GetDirectoryName(asm.Location);
                string strLabelFile = dirName + @"\LabelFormats\" + "Butterfly.btw";
                PrintBarTenderLabel(strLabelFile);
                //bartender
            }
            else
            {

                if (lbOneUpTemplate.Visibility == Visibility.Visible)
                {
                    DataTable dtbl = new DataTable();
                    dtbl = LoadPrintDataTable();


                    XtraReport fReport = new XtraReport();

                    if (File.Exists(GeneralFunctions.CheckLabelPrintingCustomisedFile("Butterfly", cmbOneUpTemplate.EditValue.ToString())))
                    {
                        fReport = XtraReport.FromFile(GeneralFunctions.CheckLabelPrintingCustomisedFile("Butterfly", cmbOneUpTemplate.EditValue.ToString()), true);
                    }
                    else
                    {
                        fReport = XtraReport.FromFile(GeneralFunctions.GetLabelPrintingDefaultFile("Butterfly"), true);
                    }

                   

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dtbl);
                    fReport.Report.DataSource = ds;
                    fReport.DataSource = ds;

                    try
                    {
                        fReport.PrinterName = cmbWinPrinter.EditValue.ToString();
                        fReport.CreateDocument();
                        fReport.PrintingSystem.ShowMarginsWarning = false;
                        fReport.PrintingSystem.ShowPrintStatusDialog = false;
                        DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                        window.PreviewControl.DocumentSource = fReport;
                        window.ShowDialog();

                    }
                    finally
                    {
                        fReport.Dispose();

                        dtbl.Dispose();
                    }



                }
                else
                {
                    DataTable dtbl = new DataTable();
                    dtbl = LoadPrintDataTable();

                    OfflineRetailV2.Report.Product.Butterfly rep_PrintLabelJW = new OfflineRetailV2.Report.Product.Butterfly();

                    string repxFile = "";
                    repxFile = "Butterfly.repx";

                    XtraReport fReport = new XtraReport();


                    if (File.Exists(GeneralFunctions.GetLabelPrintingFilePath(repxFile)))
                        fReport = XtraReport.FromFile(GeneralFunctions.GetLabelPrintingFilePath(repxFile), true);
                    else
                    {
                        fReport = rep_PrintLabelJW;
                    }
                    if (!File.Exists(GeneralFunctions.GetLabelPrintingFilePath(repxFile)))
                    {
                        string fileName = GeneralFunctions.GetLabelPrintingPath(fReport, "repx");
                        fReport.SaveLayout(fileName);
                    }

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dtbl);
                    fReport.Report.DataSource = ds;
                    fReport.DataSource = ds;


                    try
                    {
                        fReport.PrinterName = cmbWinPrinter.EditValue.ToString();
                        fReport.CreateDocument();
                        fReport.PrintingSystem.ShowMarginsWarning = false;
                        fReport.PrintingSystem.ShowPrintStatusDialog = false;



                        DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                        window.PreviewControl.DocumentSource = fReport;
                        window.ShowDialog();

                    }
                    finally
                    {
                        fReport.Dispose();
                        rep_PrintLabelJW.Dispose();


                        dtbl.Dispose();
                    }
                }
            }
        }

        private void ExecuteLabel1up()
        {
            if (Settings.BarTenderLabelPrint == "Y")
            {
                System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
                string dirName = System.IO.Path.GetDirectoryName(asm.Location);
                string strLabelFile = dirName + @"\LabelFormats\" + "1UP.btw";
                PrintBarTenderLabel(strLabelFile);
                //bartender
            }
            else
            {
                if (lbOneUpTemplate.Visibility == Visibility.Visible)
                {
                    DataTable dtbl = new DataTable();
                    dtbl = LoadPrintDataTable();


                    XtraReport fReport = new XtraReport();
                   
                    string repxFile = "";
                    repxFile = "OneUp.repx";

                    if (File.Exists(GeneralFunctions.CheckLabelPrintingCustomisedFile("OneUp", cmbOneUpTemplate.EditValue.ToString())))
                    {
                        fReport = XtraReport.FromFile(GeneralFunctions.CheckLabelPrintingCustomisedFile("OneUp", cmbOneUpTemplate.EditValue.ToString()), true);
                    }
                    else
                    {
                        fReport = XtraReport.FromFile(GeneralFunctions.GetLabelPrintingDefaultFile("OneUp"), true);
                    }

                    
                    /*if (!File.Exists(GeneralFunctions.GetLabelPrintingFilePath(repxFile)))
                    {
                        string fileName = GeneralFunctions.GetLabelPrintingPath(fReport, "repx");
                        fReport.SaveLayout(fileName);
                    }*/
                    
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dtbl);
                    fReport.Report.DataSource = ds;
                    fReport.DataSource = ds;

                    try
                    {
                        fReport.PrinterName = cmbWinPrinter.EditValue.ToString();
                        fReport.CreateDocument();
                        fReport.PrintingSystem.ShowMarginsWarning = false;
                        fReport.PrintingSystem.ShowPrintStatusDialog = false;
                        DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                        window.PreviewControl.DocumentSource = fReport;
                        window.ShowDialog();

                    }
                    finally
                    {
                        fReport.Dispose();
                        
                        dtbl.Dispose();
                    }


                    /*
                    int pageW = 0;
                    int pageH = 0;
                    PosDataObject.ReceiptTemplate objTenderTypes = new PosDataObject.ReceiptTemplate();
                    objTenderTypes.Connection = SystemVariables.Conn;
                    DataTable dbtbl = new DataTable();
                    dbtbl = objTenderTypes.ShowData(GeneralFunctions.fnInt32(cmbOneUpTemplate.EditValue));
                    foreach (DataRow dr in dbtbl.Rows)
                    {
                        
                        pageW = GeneralFunctions.fnInt32(dr["LabelTemplateWidth"].ToString());
                        pageH = GeneralFunctions.fnInt32(dr["LabelTemplateHeight"].ToString());
                    }
                    dbtbl.Dispose();

                    PosDataObject.ReceiptTemplate objCategory = new PosDataObject.ReceiptTemplate();
                    objCategory.Connection = SystemVariables.Conn;
                   
                    DataTable dtblTemplate = objCategory.FetchLinkData(GeneralFunctions.fnInt32(cmbOneUpTemplate.EditValue));


                    bool bsku = false;
                    bool bname = false;
                    bool bprice = false;
                    bool bbar = false;

                    OfflineRetailV2.Report.Product.OneUpCustom rep = new OfflineRetailV2.Report.Product.OneUpCustom();


                    XtraReport fReport = new XtraReport();

                    rep.PageSize = new System.Drawing.Size(pageW, pageH);
                    rep.Detail.HeightF = pageH + 4;


                    foreach (DataRow dr in dtblTemplate.Rows)
                    {
                        if (dr["GroupName"].ToString() == "SKU")
                        {
                            bsku = true;
                            string align = dr["TextAlign"].ToString();
                            int fz = GeneralFunctions.fnInt32(dr["FontSize"].ToString());
                            string fs = dr["TextStyle"].ToString();

                            rep.lbSKU.WidthF = pageW - 10;
                            rep.lbSKU.HeightF = fz + 6;
                            if (dr["TextAlign"].ToString() == "left") rep.lbSKU.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
                            if (dr["TextAlign"].ToString() == "center") rep.lbSKU.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
                            if (dr["TextAlign"].ToString() == "right") rep.lbSKU.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;

                            rep.lbSKU.TopF = GeneralFunctions.fnInt32(dr["CtrlPositionTop"].ToString());

                            if (fs == "normal") rep.lbSKU.Font = new Font("Arial", fz, System.Drawing.FontStyle.Regular);
                            if (fs == "bold") rep.lbSKU.Font = new Font("Arial", fz, System.Drawing.FontStyle.Bold);
                            if (fs == "italic") rep.lbSKU.Font = new Font("Arial", fz, System.Drawing.FontStyle.Italic);



                        }

                        if (dr["GroupName"].ToString() == "Item Name")
                        {
                            bname = true;

                            string align = dr["TextAlign"].ToString();
                            int fz = GeneralFunctions.fnInt32(dr["FontSize"].ToString());
                            string fs = dr["TextStyle"].ToString();

                            rep.lbProduct.WidthF = pageW - 10;
                            rep.lbProduct.HeightF = fz + 6;
                            if (dr["TextAlign"].ToString() == "left") rep.lbProduct.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
                            if (dr["TextAlign"].ToString() == "center") rep.lbProduct.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
                            if (dr["TextAlign"].ToString() == "right") rep.lbProduct.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;

                            rep.lbProduct.TopF = GeneralFunctions.fnInt32(dr["CtrlPositionTop"].ToString());

                            if (fs == "normal") rep.lbProduct.Font = new Font("Arial", fz, System.Drawing.FontStyle.Regular);
                            if (fs == "bold") rep.lbProduct.Font = new Font("Arial", fz, System.Drawing.FontStyle.Bold);
                            if (fs == "italic") rep.lbProduct.Font = new Font("Arial", fz, System.Drawing.FontStyle.Italic);
                        }

                        if (dr["GroupName"].ToString() == "Item Price")
                        {
                            bprice = true;

                            string align = dr["TextAlign"].ToString();
                            int fz = GeneralFunctions.fnInt32(dr["FontSize"].ToString());
                            string fs = dr["TextStyle"].ToString();

                            rep.lbPrice.WidthF = pageW - 10;
                            rep.lbPrice.HeightF = fz + 6;
                            if (dr["TextAlign"].ToString() == "left") rep.lbPrice.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
                            if (dr["TextAlign"].ToString() == "center") rep.lbPrice.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
                            if (dr["TextAlign"].ToString() == "right") rep.lbPrice.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;

                            rep.lbPrice.TopF = GeneralFunctions.fnInt32(dr["CtrlPositionTop"].ToString());

                            if (fs == "normal") rep.lbPrice.Font = new Font("Arial", fz, System.Drawing.FontStyle.Regular);
                            if (fs == "bold") rep.lbPrice.Font = new Font("Arial", fz, System.Drawing.FontStyle.Bold);
                            if (fs == "italic") rep.lbPrice.Font = new Font("Arial", fz, System.Drawing.FontStyle.Italic);

                        }

                        if (dr["GroupName"].ToString() == "Barcode")
                        {
                            bbar = true;

                            string align = dr["TextAlign"].ToString();
                            int fz = GeneralFunctions.fnInt32(dr["CtrlHeight"].ToString());
                            string fs = dr["TextStyle"].ToString();

                            rep.lbBarCode.WidthF = pageW - 10;
                            rep.lbBarCode.HeightF = fz;
                            if (dr["TextAlign"].ToString() == "left") rep.lbBarCode.Alignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
                            if (dr["TextAlign"].ToString() == "center") rep.lbBarCode.Alignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
                            if (dr["TextAlign"].ToString() == "right") rep.lbBarCode.Alignment = DevExpress.XtraPrinting.TextAlignment.TopRight;

                            rep.lbBarCode.TopF = GeneralFunctions.fnInt32(dr["CtrlPositionTop"].ToString());
                        }
                    }

                    if (!bsku) rep.lbSKU.Visible = false;
                    if (!bname) rep.lbProduct.Visible = false;
                    if (!bprice) rep.lbPrice.Visible = false;
                    if (!bbar) rep.lbBarCode.Visible = false;

                    fReport = rep;
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dtbl);
                    fReport.Report.DataSource = ds;
                    fReport.DataSource = ds;

                    try
                    {
                        fReport.PrinterName = cmbWinPrinter.EditValue.ToString();
                        fReport.CreateDocument();
                        fReport.PrintingSystem.ShowMarginsWarning = false;
                        fReport.PrintingSystem.ShowPrintStatusDialog = false;
                        DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                        window.PreviewControl.DocumentSource = fReport;
                        window.ShowDialog();

                    }
                    finally
                    {
                        fReport.Dispose();
                        rep.Dispose();

                        dtbl.Dispose();
                    }
                    */
                }
                else
                {
                    DataTable dtbl = new DataTable();
                    dtbl = LoadPrintDataTable();
                    OfflineRetailV2.Report.Product.OneUp rep_PrintLabelJW = new OfflineRetailV2.Report.Product.OneUp();

                    XtraReport fReport = new XtraReport();
                    fReport = rep_PrintLabelJW;

                    /*
                    string repxFile = "";
                    repxFile = "OneUp.repx";

                    if (File.Exists(GeneralFunctions.GetLabelPrintingFilePath(repxFile)))
                        fReport = XtraReport.FromFile(GeneralFunctions.GetLabelPrintingFilePath(repxFile), true);
                    else
                    {
                        fReport = rep_PrintLabelJW;
                    }
                    if (!File.Exists(GeneralFunctions.GetLabelPrintingFilePath(repxFile)))
                    {
                        string fileName = GeneralFunctions.GetLabelPrintingPath(fReport, "repx");
                        fReport.SaveLayout(fileName);
                    }
                    */
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dtbl);
                    fReport.Report.DataSource = ds;
                    fReport.DataSource = ds;

                    try
                    {
                        fReport.PrinterName = cmbWinPrinter.EditValue.ToString();
                        fReport.CreateDocument();
                        fReport.PrintingSystem.ShowMarginsWarning = false;
                        fReport.PrintingSystem.ShowPrintStatusDialog = false;
                        DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                        window.PreviewControl.DocumentSource = fReport;
                        window.ShowDialog();

                    }
                    finally
                    {
                        fReport.Dispose();
                        rep_PrintLabelJW.Dispose();

                        dtbl.Dispose();
                    }
                }
            }
        }

        private void PrintBarTenderLabel(string LabeFileName)
        {
            if (cmbWinPrinter.EditValue == null)
            {
                DocMessage.MsgInformation("Select Windows Printer");
                return;
            }

            using (Seagull.BarTender.Print.Engine btEngine = new Seagull.BarTender.Print.Engine())
            {
                string strPrice = "";
                if (strProductPrice != "")
                {
                    double dblPrice = GeneralFunctions.fnDouble(strProductPrice);

                    if (intProductDecimalPlace == 3)
                    {
                        strPrice = SystemVariables.CurrencySymbol + " " + dblPrice.ToString("f3");
                    }
                    else
                    {
                        strPrice = SystemVariables.CurrencySymbol + " " + dblPrice.ToString("f");
                    }
                }

                btEngine.Start();
                Seagull.BarTender.Print.LabelFormatDocument labelformat = btEngine.Documents.Open(LabeFileName, cmbWinPrinter.EditValue.ToString());

                labelformat.SubStrings["DESC"].Value = strProductDesc;
                labelformat.SubStrings["BARSKU"].Value = strSKU;
                labelformat.SubStrings["PRICE"].Value = strPrice;

                labelformat.PrintSetup.IdenticalCopiesOfLabel = Convert.ToInt32(spinEdit1.Text);
                //labelformat.PrintSetup.AutoPrintAgain = true;
                labelformat.Print();
            }
        }

        private void ExecuteLabel2up()
        {
            if (Settings.BarTenderLabelPrint == "Y")
            {
                System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
                string dirName = System.IO.Path.GetDirectoryName(asm.Location);
                string strLabelFile = dirName + @"\LabelFormats\" + "2UP.btw";
                PrintBarTenderLabel(strLabelFile);
                //bartender
            }
            else
            {
                if (lbOneUpTemplate.Visibility == Visibility.Visible)
                {
                    DataTable dtbl = new DataTable();
                    dtbl = LoadPrintDataTable();


                    XtraReport fReport = new XtraReport();

                    if (File.Exists(GeneralFunctions.CheckLabelPrintingCustomisedFile("TwoUp", cmbOneUpTemplate.EditValue.ToString())))
                    {
                        fReport = XtraReport.FromFile(GeneralFunctions.CheckLabelPrintingCustomisedFile("TwoUp", cmbOneUpTemplate.EditValue.ToString()), true);
                    }
                    else
                    {
                        fReport = XtraReport.FromFile(GeneralFunctions.GetLabelPrintingDefaultFile("TwoUp"), true);
                    }

                    intskip = GeneralFunctions.fnInt32(spncol.Text) - 1;

                    (fReport as OfflineRetailV2.Report.Product.TwoUp).skpno = intskip;
                    (fReport as OfflineRetailV2.Report.Product.TwoUp).prtno = GeneralFunctions.fnInt32(spinEdit1.Text);
                    if (intskip == 0)
                    {
                        (fReport as OfflineRetailV2.Report.Product.TwoUp).firstp = true;
                    }
                    else
                    {
                        (fReport as OfflineRetailV2.Report.Product.TwoUp).firstp = false;
                    }

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dtbl);
                    fReport.Report.DataSource = ds;
                    fReport.DataSource = ds;

                    try
                    {
                        fReport.PrinterName = cmbWinPrinter.EditValue.ToString();
                        fReport.CreateDocument();
                        fReport.PrintingSystem.ShowMarginsWarning = false;
                        fReport.PrintingSystem.ShowPrintStatusDialog = false;
                        DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                        window.PreviewControl.DocumentSource = fReport;
                        window.ShowDialog();

                    }
                    finally
                    {
                        fReport.Dispose();

                        dtbl.Dispose();
                    }



                }

                else
                {
                    intskip = GeneralFunctions.fnInt32(spncol.Text) - 1;


                    DataTable dtbl = new DataTable();
                    dtbl = LoadPrintDataTable();

                    OfflineRetailV2.Report.Product.TwoUp rep_PrintLabel = new OfflineRetailV2.Report.Product.TwoUp();

                    string repxFile = "";
                    repxFile = "TwoUp.repx";

                    XtraReport fReport = new XtraReport();

                    if (File.Exists(GeneralFunctions.GetLabelPrintingFilePath(repxFile)))
                        fReport = XtraReport.FromFile(GeneralFunctions.GetLabelPrintingFilePath(repxFile), true);
                    else
                    {
                        fReport = rep_PrintLabel;
                    }
                    if (!File.Exists(GeneralFunctions.GetLabelPrintingFilePath(repxFile)))
                    {
                        string fileName = GeneralFunctions.GetLabelPrintingPath(fReport, "repx");
                        fReport.SaveLayout(fileName);
                    }

                    (fReport as OfflineRetailV2.Report.Product.TwoUp).skpno = intskip;
                    (fReport as OfflineRetailV2.Report.Product.TwoUp).prtno = GeneralFunctions.fnInt32(spinEdit1.Text);
                    if (intskip == 0)
                    {
                        (fReport as OfflineRetailV2.Report.Product.TwoUp).firstp = true;
                    }
                    else
                    {
                        (fReport as OfflineRetailV2.Report.Product.TwoUp).firstp = false;
                    }
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dtbl);

                    //rep_PrintLabel.DataSource = ds;



                    fReport.Report.DataSource = ds;
                    fReport.DataSource = ds;
                    fReport.CreateDocument();

                    try
                    {
                        fReport.PrinterName = cmbWinPrinter.EditValue.ToString();
                        fReport.CreateDocument();
                        fReport.PrintingSystem.ShowMarginsWarning = false;
                        fReport.PrintingSystem.ShowPrintStatusDialog = false;

                        DocumentPreviewWindow window = new DocumentPreviewWindow() { WindowState = WindowState.Maximized };
                        window.PreviewControl.DocumentSource = fReport;
                        window.ShowDialog();

                    }
                    finally
                    {
                        fReport.Dispose();
                        rep_PrintLabel.Dispose();


                        dtbl.Dispose();
                    }
                }
            }
        }

        private void populateOneUpTemplate()
        {
            PosDataObject.ReceiptTemplate obj = new PosDataObject.ReceiptTemplate();
            obj.Connection = SystemVariables.Conn;
            DataTable dtbl = obj.FetchActiveTemplateListForLabel("Label - 1 Up");
            cmbOneUpTemplate.ItemsSource = dtbl;
            if (dtbl.Rows.Count > 0)
            {
                int fid = 0;
                foreach(DataRow dr in dtbl.Rows)
                {
                    fid = GeneralFunctions.fnInt32(dr["ID"].ToString());
                    break;
                }

                cmbOneUpTemplate.EditValue = fid.ToString();
                lbOneUpTemplate.Text = "1 Up Template";
                lbOneUpTemplate.Visibility = Visibility.Visible;
                cmbOneUpTemplate.Visibility = Visibility.Visible;
            }
            else
            {
                lbOneUpTemplate.Visibility = Visibility.Collapsed;
                cmbOneUpTemplate.Visibility = Visibility.Collapsed;
            }
        }

        private void populateTwoUpTemplate()
        {
            PosDataObject.ReceiptTemplate obj = new PosDataObject.ReceiptTemplate();
            obj.Connection = SystemVariables.Conn;
            DataTable dtbl = obj.FetchActiveTemplateListForLabel("Label - 2 Up");
            cmbOneUpTemplate.ItemsSource = dtbl;
            if (dtbl.Rows.Count > 0)
            {
                int fid = 0;
                foreach (DataRow dr in dtbl.Rows)
                {
                    fid = GeneralFunctions.fnInt32(dr["ID"].ToString());
                    break;
                }

                cmbOneUpTemplate.EditValue = fid.ToString();
                lbOneUpTemplate.Text = "2 Up Template";
                lbOneUpTemplate.Visibility = Visibility.Visible;
                cmbOneUpTemplate.Visibility = Visibility.Visible;
            }
            else
            {
                lbOneUpTemplate.Visibility = Visibility.Collapsed;
                cmbOneUpTemplate.Visibility = Visibility.Collapsed;
            }
        }

        private void populateButterflyTemplate()
        {
            PosDataObject.ReceiptTemplate obj = new PosDataObject.ReceiptTemplate();
            obj.Connection = SystemVariables.Conn;
            DataTable dtbl = obj.FetchActiveTemplateListForLabel("Label - Butterfly");
            cmbOneUpTemplate.ItemsSource = dtbl;
            if (dtbl.Rows.Count > 0)
            {
                int fid = 0;
                foreach (DataRow dr in dtbl.Rows)
                {
                    fid = GeneralFunctions.fnInt32(dr["ID"].ToString());
                    break;
                }

                cmbOneUpTemplate.EditValue = fid.ToString();
                lbOneUpTemplate.Text = "Butterfly Template";
                lbOneUpTemplate.Visibility = Visibility.Visible;
                cmbOneUpTemplate.Visibility = Visibility.Visible;
            }
            else
            {
                lbOneUpTemplate.Visibility = Visibility.Collapsed;
                cmbOneUpTemplate.Visibility = Visibility.Collapsed;
            }
        }

        private void populateAvery5160Template()
        {
            PosDataObject.ReceiptTemplate obj = new PosDataObject.ReceiptTemplate();
            obj.Connection = SystemVariables.Conn;
            DataTable dtbl = obj.FetchActiveTemplateListForLabel("Label - Avery 5160 / NEBS 12650");
            cmbOneUpTemplate.ItemsSource = dtbl;
            if (dtbl.Rows.Count > 0)
            {
                int fid = 0;
                foreach (DataRow dr in dtbl.Rows)
                {
                    fid = GeneralFunctions.fnInt32(dr["ID"].ToString());
                    break;
                }

                cmbOneUpTemplate.EditValue = fid.ToString();
                lbOneUpTemplate.Text = "Avery 5160 / NEBS 12650 Template";
                lbOneUpTemplate.Visibility = Visibility.Visible;
                cmbOneUpTemplate.Visibility = Visibility.Visible;
            }
            else
            {
                lbOneUpTemplate.Visibility = Visibility.Collapsed;
                cmbOneUpTemplate.Visibility = Visibility.Collapsed;
            }
        }

        private void populateAvery8195Template()
        {
            PosDataObject.ReceiptTemplate obj = new PosDataObject.ReceiptTemplate();
            obj.Connection = SystemVariables.Conn;
            DataTable dtbl = obj.FetchActiveTemplateListForLabel("Label - Avery 8195");
            cmbOneUpTemplate.ItemsSource = dtbl;
            if (dtbl.Rows.Count > 0)
            {
                int fid = 0;
                foreach (DataRow dr in dtbl.Rows)
                {
                    fid = GeneralFunctions.fnInt32(dr["ID"].ToString());
                    break;
                }

                cmbOneUpTemplate.EditValue = fid.ToString();
                lbOneUpTemplate.Text = "Avery 8195 Template";
                lbOneUpTemplate.Visibility = Visibility.Visible;
                cmbOneUpTemplate.Visibility = Visibility.Visible;
            }
            else
            {
                lbOneUpTemplate.Visibility = Visibility.Collapsed;
                cmbOneUpTemplate.Visibility = Visibility.Collapsed;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Settings.BarTenderLabelPrint = "N";
            nkybrd = new NumKeyboard();
            spinEdit1.Text = intQty.ToString();
            lbProduct.Text = strProductDesc;
            lbSKU.Text = "SKU : " + strSKU;

            PopulateWindowsPrinter();
            PopulateCOMPrinter();

            if (Settings.LabelPrinterType == "1")
            {
                rgLabelPrinter_0.IsChecked = true;
            }
            else
            {
                rgLabelPrinter_1.IsChecked = true;

            }

            cmbWinPrinter.Visibility = rgLabelPrinter_0.IsChecked == true ? Visibility.Visible : Visibility.Hidden;
            cmbCOMPrinter.Visibility = rgLabelPrinter_1.IsChecked == true ? Visibility.Visible : Visibility.Hidden;

            if (rgLabelPrinter_0.IsChecked == true)
            {
                cmbWinPrinter.EditValue = Settings.LabelPrinterName;
                pnlWin.Visibility = Visibility.Visible;
            }

            if (rgLabelPrinter_1.IsChecked == true)
            {
                cmbCOMPrinter.EditValue = Settings.LabelPrinterName;
                pnlWin.Visibility = Visibility.Hidden;
            }

            rg_1.IsChecked = true;

            if (intLabelType == 0) rg_1.IsChecked = true;
            if (intLabelType == 1) rg_2.IsChecked = true;
            if (intLabelType == 2) rg_3.IsChecked = true;
            if (intLabelType == 3) rg_4.IsChecked = true;
            if (intLabelType == 4) rg_5.IsChecked = true;


            lbRow.Visibility = Visibility.Hidden;
            lbColumn.Visibility = Visibility.Hidden;
            spnrow.Visibility = Visibility.Hidden;
            spncol.Visibility = Visibility.Hidden;

            //if (blisbatchprint) return;

            if (rg_4.IsChecked == true)
            {
                lbRow.Visibility = Visibility.Visible;
                lbColumn.Visibility = Visibility.Visible;
                spnrow.Visibility = Visibility.Visible;
                spncol.Visibility = Visibility.Visible;
                spncol.Items.Clear();
                spncol.Items.Add("1");
                spncol.Items.Add("2");
                spncol.Items.Add("3");
                spncol.SelectedIndex = 0;
                spnrow.Items.Clear();
                spnrow.Items.Add("1");
                spnrow.Items.Add("2");
                spnrow.Items.Add("3");
                spnrow.Items.Add("4");
                spnrow.Items.Add("5");
                spnrow.Items.Add("6");
                spnrow.Items.Add("7");
                spnrow.Items.Add("8");
                spnrow.Items.Add("9");
                spnrow.Items.Add("10");
                spnrow.SelectedIndex = 0;
            }
            else if (rg_5.IsChecked == true)
                {
                lbRow.Visibility = Visibility.Visible;
                lbColumn.Visibility = Visibility.Visible;
                spnrow.Visibility = Visibility.Visible;
                spncol.Visibility = Visibility.Visible;
                spncol.Items.Clear();
                spncol.Items.Add("1");
                spncol.Items.Add("2");
                spncol.Items.Add("3");
                spncol.Items.Add("4");
                spncol.SelectedIndex = 0;
                spnrow.Items.Clear();
                spnrow.Items.Add("1");
                spnrow.Items.Add("2");
                spnrow.Items.Add("3");
                spnrow.Items.Add("4");
                spnrow.Items.Add("5");
                spnrow.Items.Add("6");
                spnrow.Items.Add("7");
                spnrow.Items.Add("8");
                spnrow.Items.Add("9");
                spnrow.Items.Add("10");
                spnrow.Items.Add("11");
                spnrow.Items.Add("12");
                spnrow.Items.Add("13");
                spnrow.Items.Add("14");
                spnrow.Items.Add("15");
                spnrow.SelectedIndex = 0;
            }
            else
            {
                lbRow.Visibility = Visibility.Hidden;
                lbColumn.Visibility = Visibility.Hidden;
                spnrow.Visibility = Visibility.Hidden;
                spncol.Visibility = Visibility.Hidden;

                if (rg_2.IsChecked == true)
                {
                    lbColumn.Visibility = Visibility.Visible;
                    spncol.Visibility = Visibility.Visible;
                    spncol.Items.Clear();
                    spncol.Items.Add("1");
                    spncol.Items.Add("2");
                    spncol.SelectedIndex = 0;
                }
            }
        }

        private void CmbCOMPrinter_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            comport = "";
            comprintcommand = "";
            comdefaultqty = 0;
            comwrap = 0;
            if (cmbCOMPrinter.EditValue != null)
            {
                PosDataObject.Setup objstup = new PosDataObject.Setup();
                objstup.Connection = SystemVariables.Conn;
                DataTable dtbl = objstup.ShowCOMPrinterCommand(GeneralFunctions.fnInt32(cmbCOMPrinter.EditValue));
                foreach (DataRow dr in dtbl.Rows)
                {
                    comport = dr["PortName"].ToString();
                    comprintcommand = dr["PrinterCommand"].ToString();
                    comdefaultqty = GeneralFunctions.fnInt32(dr["Qty"].ToString());
                    comwrap = GeneralFunctions.fnInt32(dr["WordWrap"].ToString());
                    comparafont = dr["FontType"].ToString();
                }
                dtbl.Dispose();
                spinEdit1.Text = comdefaultqty > 0 ? comdefaultqty.ToString() : "1";
            }
        }

        private void CmbWinPrinter_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {

        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            bool bprint = false;
            try
            {
                Cursor = Cursors.Wait;
                if ((rgLabelPrinter_0.IsChecked == true) && (cmbWinPrinter.EditValue == null))
                {
                    DocMessage.MsgInformation("Please select windows printer");
                    return;
                }

                /*
                if ((rgLabelPrinter.SelectedIndex == 1) && (cmbCOMPrinter.EditValue == ""))
                {
                    DocMessage.MsgInformation(Translation.SetMultilingualTextInCodes("Please select com printer");
                    return;
                }*/


                if (GeneralFunctions.fnInt32(spinEdit1.Text) == 0)
                {
                    DocMessage.MsgEnter("Quantity");
                    GeneralFunctions.SetFocus(spinEdit1);
                    return;
                }

                if (rgLabelPrinter_0.IsChecked == true)
                {
                    if (rg_2.IsChecked == false)
                    {
                        if (GeneralFunctions.fnInt32(spinEdit1.Text) > 100)
                        {
                            if (Settings.BarTenderLabelPrint != "Y")
                            {
                                DocMessage.MsgInformation("Maximum 100 labels are allowed for printing.");
                                GeneralFunctions.SetFocus(spinEdit1);
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (GeneralFunctions.fnInt32(spinEdit1.Text) > 20)
                        {
                            if (Settings.BarTenderLabelPrint != "Y")
                            {
                                DocMessage.MsgInformation("Maximum 20 labels are allowed for 2 Up printing.");
                                GeneralFunctions.SetFocus(spinEdit1);
                                return;
                            }
                        }
                    }
                }

                if (rgLabelPrinter_0.IsChecked == true)
                {
                    if (rg_1.IsChecked == true) ExecuteLabel1up(); //ExecuteLabel1()
                    if (rg_2.IsChecked == true) ExecuteLabel2up();
                    if (rg_3.IsChecked == true) ExecuteLabel3();
                    if (rg_4.IsChecked == true) ExecuteLabel();
                    if (rg_5.IsChecked == true) ExecuteLabel4();
                }

                if (rgLabelPrinter_1.IsChecked == true)
                {
                    PrintWithCOM();
                }

                //AdustData();
                bprint = true;
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private string WordWrappedString(string sText)
        {
            string sTextRows = "";
            int lPos = 0;
            string sTemp = "";
            string sPos = "";
            int RstC = 0;
            sText = sText.Trim();
            if ((sText.Length > comwrap) && (comwrap > 0))
            {
                sTemp = sText;
                lPos = comwrap;
                while (sTemp.Length > comwrap)
                {
                    sPos = sTemp.Substring(lPos, 1).Trim();
                    if ((sPos == "") || (sPos == ".") || (sPos == "?") || (sPos == "!") || (sPos == ","))
                    {
                        sTextRows = sTextRows + sTemp.Substring(0, lPos).Trim() + Environment.NewLine;
                        sTemp = sTemp.Substring(lPos, sTemp.Length - lPos).Trim();
                        lPos = comwrap;
                    }
                    else
                    {
                        lPos = lPos - 1;
                    }
                }
                sTextRows = sTextRows + sTemp.Trim();
            }
            else
            {
                sTextRows = sText + Environment.NewLine;
            }
            return sTextRows;
        }

        private void PrintWithCOM()
        {
            PosDataObject.Setup objstup1 = new PosDataObject.Setup();
            objstup1.Connection = SystemVariables.Conn;
            DataTable dtbl1 = objstup1.GetCOMPrinterVendorData(strSKU);

            foreach (DataRow dr1 in dtbl1.Rows)
            {
                VID = dr1["VID"].ToString();
                VNAME = dr1["VNAME"].ToString();
                VPART = dr1["VPART"].ToString();
            }
            PosDataObject.Setup objstup2 = new PosDataObject.Setup();
            objstup2.Connection = SystemVariables.Conn;
            DataTable dtbl2 = objstup2.GetCOMPrinterScaleData(strSKU);

            foreach (DataRow dr2 in dtbl2.Rows)
            {
                LD = dr2["LD"].ToString();
                LD2 = dr2["LD2"].ToString();
                INGD = dr2["INGD"].ToString();
                SPLM = dr2["SPLM"].ToString();
                BC = dr2["BC"].ToString();
                WHT = dr2["WHT"].ToString();
            }

            if (INGD != "") INGD = WordWrappedString(INGD);
            if (SPLM != "") SPLM = WordWrappedString(SPLM);

            // rearrange com printer command

            string priceformat = "";
            if (strProductPrice == "")
            {
                priceformat = "";
            }
            else
            {
                priceformat = GeneralFunctions.fnDouble(strProductPrice).ToString("f");
            }

            if (comprintcommand.Contains("\"<SKU>\"") || comprintcommand.Contains("\"<DESCRIPTION>\"") ||
                comprintcommand.Contains("\"<BARCODE>\"") || comprintcommand.Contains("\"<PRICE>\"") ||
                comprintcommand.Contains("\"<QTY>\"") || comprintcommand.Contains("\"<VENDOR ID>\"")
                || comprintcommand.Contains("\"<VENDOR NAME>\"") || comprintcommand.Contains("\"<VENDOR PART>\"")
                || comprintcommand.Contains("\"<LABEL DESCRIPTION>\"") || comprintcommand.Contains("\"<LABEL DESCRIPTION 2>\"")
                || comprintcommand.Contains("\"<INGREDIENT TEXT>\"") || comprintcommand.Contains("\"<RECIPE>\"")
                || comprintcommand.Contains("\"<BY COUNT>\"") || comprintcommand.Contains("\"<PACKAGE WEIGHT>\""))
            {

                string[] sss = comprintcommand.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                string OriginalINGD = "";
                string OriginalSPLM = "";
                foreach (string s in sss)
                {
                    if (s.Contains("\"<INGREDIENT TEXT>\"")) OriginalINGD = s;
                    if (s.Contains("\"<RECIPE>\"")) OriginalSPLM = s;
                }

                if (OriginalINGD != "")
                {
                    comprintcommand = comprintcommand.Replace(OriginalINGD, RearrangeMultiline(OriginalINGD, INGD));
                }

                if (OriginalSPLM != "")
                {
                    comprintcommand = comprintcommand.Replace(OriginalSPLM, RearrangeMultiline(OriginalSPLM, SPLM));
                }


                comprintcommand = comprintcommand.Replace("\"<SKU>\"", strSKU);
                comprintcommand = comprintcommand.Replace("\"<DESCRIPTION>\"", strProductDesc);
                comprintcommand = comprintcommand.Replace("\"<BARCODE>\"", strSKU);
                comprintcommand = comprintcommand.Replace("\"<PRICE>\"", priceformat);
                comprintcommand = comprintcommand.Replace("\"<QTY>\"", spinEdit1.Text);

                comprintcommand = comprintcommand.Replace("\"<VENDOR ID>\"", VID);
                comprintcommand = comprintcommand.Replace("\"<VENDOR NAME>\"", VNAME);
                comprintcommand = comprintcommand.Replace("\"<VENDOR PART>\"", VPART);

                comprintcommand = comprintcommand.Replace("\"<LABEL DESCRIPTION>\"", LD);
                comprintcommand = comprintcommand.Replace("\"<LABEL DESCRIPTION 2>\"", LD2);
                comprintcommand = comprintcommand.Replace("\"<BY COUNT>\"", BC);
                comprintcommand = comprintcommand.Replace("\"<PACKAGE WEIGHT>\"", WHT);
            }
            else
            {
                string[] sss = comprintcommand.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                string OriginalINGD = "";
                string OriginalSPLM = "";
                foreach (string s in sss)
                {
                    if (s.Contains("<INGREDIENT TEXT>")) OriginalINGD = s;
                    if (s.Contains("<RECIPE>")) OriginalSPLM = s;
                }

                if (OriginalINGD != "")
                {
                    comprintcommand = comprintcommand.Replace(OriginalINGD, RearrangeMultiline(OriginalINGD, INGD));
                }

                if (OriginalSPLM != "")
                {
                    comprintcommand = comprintcommand.Replace(OriginalSPLM, RearrangeMultiline(OriginalSPLM, SPLM));
                }

                comprintcommand = comprintcommand.Replace("<SKU>", strSKU);
                comprintcommand = comprintcommand.Replace("<DESCRIPTION>", strProductDesc);
                comprintcommand = comprintcommand.Replace("<BARCODE>", strSKU);
                comprintcommand = comprintcommand.Replace("<PRICE>", priceformat);
                comprintcommand = comprintcommand.Replace("<QTY>", spinEdit1.Text);

                comprintcommand = comprintcommand.Replace("<VENDOR ID>", VID);
                comprintcommand = comprintcommand.Replace("<VENDOR NAME>", VNAME);
                comprintcommand = comprintcommand.Replace("<VENDOR PART>", VPART);

                comprintcommand = comprintcommand.Replace("<LABEL DESCRIPTION>", LD);
                comprintcommand = comprintcommand.Replace("<LABEL DESCRIPTION 2>", LD2);
                comprintcommand = comprintcommand.Replace("<BY COUNT>", BC);
                comprintcommand = comprintcommand.Replace("<PACKAGE WEIGHT>", WHT);
            }

            if (_sport == null) _sport = new SerialPort(comport);
            try
            {
                if (!_sport.IsOpen) _sport.Open();
                _sport.Write(comprintcommand);
            }
            catch
            {
                DocMessage.MsgInformation("Error while communicating COM Printer...");
            }
        }

        private string RearrangeMultiline(string ParentString, string ValueString)
        {
            string[] pstr = ParentString.Split(',');

            string fnt = pstr[0];
            string xpos = pstr[1];
            int ypos = GeneralFunctions.fnInt32(pstr[2]);
            string other1 = pstr[3];
            string other2 = pstr[4];
            string other3 = pstr[5];
            string other4 = pstr[6];

            string tstr = "";

            if (ValueString != "")
            {
                string[] vstr = ValueString.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);



                if (comparafont != "") fnt = comparafont;

                int inc = 0;

                if (fnt == "AA") inc = 19;
                if (fnt == "AB") inc = 26;
                if (fnt == "AC") inc = 32;
                if (fnt == "AD") inc = 38;
                if (fnt == "AE") inc = 44;
                if (fnt == "AF") inc = 58;
                if (fnt == "AG") inc = 76;
                if (fnt == "AH") inc = 96;

                int cnt = 0;

                foreach (string s in vstr)
                {
                    tstr = tstr == "" ? fnt + "," + xpos + "," + (ypos + (inc * cnt)).ToString() + "," + other1 + "," + other2 + "," + other3 + "," + other4 + "," + s :
                        tstr + Environment.NewLine + fnt + "," + xpos + "," + (ypos + (inc * cnt)).ToString() + "," + other1 + "," + other2 + "," + other3 + "," + other4 + "," + s;
                    cnt++;
                }

            }
            else
            {
                tstr = fnt + "," + xpos + "," + ypos.ToString() + "," + other1 + "," + other2 + "," + other3 + "," + other4 + "," + "";
            }

            return tstr;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            
            CloseKeyboards();
            Close();
        }

        private void CmbWinPrinter_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.ComboBoxEdit editor = sender as DevExpress.Xpf.Editors.ComboBoxEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void CloseKeyboards()
        {
            if (nkybrd != null)
            {
                nkybrd.Close();
            }
        }

        private void Num_LostFocus(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
        }

        private void Num_GotFocus(object sender, RoutedEventArgs e)
        {

            if (Settings.UseTouchKeyboardInAdmin == "N") return;
            CloseKeyboards();

            Dispatcher.BeginInvoke(new Action(() => (sender as DevExpress.Xpf.Editors.TextEdit).SelectAll()));


            if (!IsAboutNumKybrdOpen)
            {
                nkybrd = new NumKeyboard();

                var location = (sender as DevExpress.Xpf.Editors.TextEdit).PointToScreen(new System.Windows.Point(0, 0));
                nkybrd.Left = location.X + 385 <= System.Windows.SystemParameters.WorkArea.Width ? location.X : location.X - (location.X + 385 - System.Windows.SystemParameters.WorkArea.Width);
                if (location.Y + 35 + 270 > System.Windows.SystemParameters.WorkArea.Height)
                {
                    nkybrd.Top = location.Y - 270;
                }
                else
                {
                    nkybrd.Top = location.Y + 35;
                }

                nkybrd.Height = 270;
                nkybrd.Width = 385;
                nkybrd.IsWindow = true;
                nkybrd.CalledForm = this;
                nkybrd.Closing += new System.ComponentModel.CancelEventHandler(NKybrd_Closing);
                nkybrd.Show();
                IsAboutNumKybrdOpen = true;
            }
        }

        private void NKybrd_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsAboutNumKybrdOpen)
            {
                e.Cancel = true;
            }
            else
            {
                IsAboutNumKybrdOpen = false;
                e.Cancel = false;
            }
        }

        private void CmbTemplateType1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void gen_PopupOpened(object sender, RoutedEventArgs e)
        {
            var editor = (LookUpEdit)sender;
            var grid = editor.GetGridControl();
            var view = (TableView)grid.View;
            view.BestFitColumns();
        }
    }
}
