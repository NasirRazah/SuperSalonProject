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

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_POSAllProducts.xaml
    /// </summary>
    public partial class frm_POSAllProducts : Window
    {

        #region Variables

        private int intCategoryID;
        private int inStep = 0;
        private static int ITEMSEPARATOR = 5;

        private double frmValue1;
        private double frmValue2;
        private double frmValue3;
        private double frmValue4;
        private int frmValue5;

        public double FValue1
        {
            get { return frmValue1; }
            set { frmValue1 = value; }
        }

        public double FValue2
        {
            get { return frmValue2; }
            set { frmValue2 = value; }
        }

        public double FValue3
        {
            get { return frmValue3; }
            set { frmValue3 = value; }
        }

        public double FValue4
        {
            get { return frmValue4; }
            set { frmValue4 = value; }
        }

        public int FValue5
        {
            get { return frmValue5; }
            set { frmValue5 = value; }
        }


        private POSControl frmPosDlg;

        public POSControl POSForm
        {
            get { return frmPosDlg; }
            set { frmPosDlg = value; }
        }

        #endregion

        public frm_POSAllProducts()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Height = 400;
            Width = frmValue4;
            Left = frmValue1;
            Top = frmValue2;
            intCategoryID = frmValue5;
            LoadPOSItems();
        }

        private void OnCloseCOmmand(object obj)
        {
            frmPosDlg.SetQtyFromCategory(1);
            Close();
        }


        private void LoadPOSItems()
        {
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = SystemVariables.Conn;
            DataTable dtblItemButtons = objPOS.FetchPOSItemsforCategory(intCategoryID, true);
            int intCount = 0;
            int intInc = 0;
            int intRowCount = 0;
            
            foreach (DataRow dr in dtblItemButtons.Rows)
            {
                intCount++;
                intInc++;
                POSControls.POSItem PosItem = new POSControls.POSItem(2);

                PosItem.SuspendLayout();
                try
                {
                    
                    PosItem.Name = dr["ID"].ToString();
                    PosItem.ItemID = GeneralFunctions.fnInt32(dr["ID"].ToString());
                    PosItem.ItemFont = GeneralFunctions.GetPOSFont(dr["POSFONTTYPE"].ToString(), dr["POSFONTSIZE"].ToString(), dr["ISBOLD"].ToString(), dr["ISITALICS"].ToString());
                    PosItem.ItemFontFamily = dr["POSFONTTYPE"].ToString();
                    PosItem.ItemFontSize = dr["POSFONTSIZE"].ToString();
                    PosItem.ItemFontBold = dr["ISBOLD"].ToString();
                    PosItem.ItemFontItalic = dr["ISITALICS"].ToString();
                    PosItem.ItemForeColor = dr["POSFONTCOLOR"].ToString();
                    PosItem.ItemBackground = dr["POSBACKGROUND"].ToString();
                    PosItem.ApplicationStyle = GeneralFunctions.GetPOSApplicationStyle(dr["POSSCREENSTYLE"].ToString());
                    PosItem.ItemColor = dr["POSSCREENCOLOR"].ToString();
                    PosItem.ItemStyle = GeneralFunctions.GetPOSStyle(dr["POSBACKGROUND"].ToString(), dr["POSSCREENSTYLE"].ToString());
                    PosItem.ItemName = dr["DESCRIPTION"].ToString();
                    PosItem.ItemSKU = dr["SKU"].ToString();
                    PosItem.ShowSKU = (Settings.ShowSKUOnPOSButton == "Y");

                    double qtyhand = 0;
                    if (dr["LINKSKU"].ToString() == "0")
                    {
                        PosItem.DisplayStockinPOS = (dr["DISPLAYSTOCKINPOS"].ToString() == "Y");
                        qtyhand = GeneralFunctions.fnDouble(dr["QTYONHAND"].ToString());
                    }
                    else
                    {
                        if (GeneralFunctions.fnDouble(dr["BREAKPACKRATIO"].ToString()) == 1)
                        {

                            PosItem.DisplayStockinPOS = (dr["DISPLAYSTOCKINPOS"].ToString() == "Y");
                            PosDataObject.POS objPOS1 = new PosDataObject.POS();
                            objPOS1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                            qtyhand = objPOS1.GetOnHandQty(GeneralFunctions.fnInt32(dr["LINKSKU"].ToString()));
                        }
                        else
                        {
                            PosItem.DisplayStockinPOS = false;
                            qtyhand = 0;
                        }
                    }

                    PosItem.CurrentStock = GeneralFunctions.fnInt32(qtyhand);


                    PosItem.ItemType = dr["PRODUCTTYPE"].ToString();
                    PosItem.NegativeStock = (dr["ALLOWZEROSTOCK"].ToString() == "Y");
                    GeneralFunctions.LoadPhotofromDB(PosItem);

                    string itemtext = "";

                    if (PosItem.DisplayStockinPOS)
                    {
                        itemtext = PosItem.ShowSKU ? PosItem.ItemSKU + " - " + PosItem.ItemName + " (" + PosItem.CurrentStock.ToString() + ")" : PosItem.ItemName + " (" + PosItem.CurrentStock.ToString() + ")";
                    }
                    else
                    {
                        itemtext = PosItem.ShowSKU ? PosItem.ItemSKU + " - " + PosItem.ItemName : PosItem.ItemName;
                    }

                    System.Windows.Controls.Button btn = new System.Windows.Controls.Button();


                    TextBlock tb = new TextBlock();
                    tb.Text = itemtext;
                    tb.TextAlignment = TextAlignment.Center;
                    tb.TextWrapping = TextWrapping.WrapWithOverflow;
                    tb.VerticalAlignment = VerticalAlignment.Center;
                    tb.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    GeneralFunctions.SetWpfFont(tb, dr["POSFONTTYPE"].ToString(), dr["POSFONTSIZE"].ToString(), dr["ISBOLD"].ToString(), dr["ISITALICS"].ToString());
                    if ((PosItem.ItemForeColor == "") || (PosItem.ItemForeColor == "0") || (PosItem.ItemForeColor.Contains("#00000000")))
                    {

                    }
                    else
                    {
                        tb.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(PosItem.ItemForeColor));
                    }

                    btn.Width = 140;
                    btn.Height = 70;
                    btn.Margin = new Thickness(5);
                    btn.BorderThickness = new Thickness(3);
                    btn.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6EC1DA"));
                    
                    btn.Content = tb;

                    
                    


                    if ((PosItem.ItemColor == "") || (PosItem.ItemColor == "0") || (PosItem.ItemColor.Contains("#00000000")))
                    {
                        btn.Background = System.Windows.Media.Brushes.Transparent;
                    }
                    else
                    {
                        btn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(PosItem.ItemColor));
                        
                    }

                    
                    
                    btn.Tag = PosItem;
                    btn.PreviewMouseLeftButtonDown += frmPosDlg.Lbi_PreviewMouseLeftButtonDown;
                    pnlBody.Children.Add(btn);
                }
                finally
                {
                    PosItem.ResumeLayout();
                }
            }
            
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            frmPosDlg.SetQtyFromCategory(1);
            Close();
        }

        double posActionListBoxVerticalUpOffset = 0;
        double posActionListBoxVerticalDownOffset = 0;

        private void BtnDownHeader_Click(object sender, RoutedEventArgs e)
        {
            if (POSActionScrollViewer is null) return;

            if (posActionListBoxVerticalUpOffset < POSActionScrollViewer.ExtentHeight)
                posActionListBoxVerticalDownOffset = posActionListBoxVerticalUpOffset = POSActionScrollViewer.VerticalOffset + 159;
            POSActionScrollViewer.ScrollToVerticalOffset(posActionListBoxVerticalUpOffset);
        }

        private void BtnUpHeader_Click(object sender, RoutedEventArgs e)
        {
            if (POSActionScrollViewer is null) return;



            posActionListBoxVerticalDownOffset = posActionListBoxVerticalUpOffset = POSActionScrollViewer.VerticalOffset - 159;
            if (posActionListBoxVerticalDownOffset < 0)
            {
                posActionListBoxVerticalDownOffset = posActionListBoxVerticalUpOffset = 0;
            }
            POSActionScrollViewer.ScrollToVerticalOffset(posActionListBoxVerticalDownOffset);
        }

        private void BtnQty_Click(object sender, RoutedEventArgs e)
        {
            blurGrid.Visibility = Visibility.Visible;
            frm_POSQty frmQty = new frm_POSQty();
            try
            {
                frmQty.ShowDialog();
                if (frmQty.DialogResult == true)
                {
                    frmPosDlg.SetQtyFromCategory(frmQty.ReturnQty);
                }
            }
            finally
            {
                frmQty.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void ScrlViewer_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }
    }
}
