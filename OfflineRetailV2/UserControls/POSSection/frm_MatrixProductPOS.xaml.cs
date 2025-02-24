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
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_MatrixProduct.xaml
    /// </summary>
    public partial class frm_MatrixProductPOS : UserControl
    {
        public frm_MatrixProductPOS()
        {
            InitializeComponent();
            
        }

        private int intFID;
        private int intPID;
        private int intID;
        private bool boolRefreshQty;
        private bool boolControlChanged;
        private int intPrevRow;
        private int intActiveGrid;
        private string strCalledFrom;
        private DataTable PrevQtyData = null;

        private bool boolOK;

        public bool OK
        {
            get { return boolOK; }
            set { boolOK = value; }
        }

        public int FID
        {
            get { return intFID; }
            set { intFID = value; }
        }


        public int PID
        {
            get { return intPID; }
            set { intPID = value; }
        }

        public int ActiveGrid
        {
            get { return intActiveGrid; }
            set { intActiveGrid = value; }
        }

        public int ID
        {
            get { return intID; }
            set { intID = value; }
        }

        public string CalledFrom
        {
            get { return strCalledFrom; }
            set { strCalledFrom = value; }
        }








        public void LoadData()
        {
            PosDataObject.Matrix objMatrix = new PosDataObject.Matrix();
            objMatrix.Connection = SystemVariables.Conn;
            objMatrix.ProductID = intFID;
            intID = 0;
            DataTable dtbl = new DataTable();
            dtbl = objMatrix.FetchData();
            foreach (DataRow dr in dtbl.Rows)
            {
                intID = GeneralFunctions.fnInt32(dr["ID"].ToString());
                frm_MatrixProductUC.txtOption1.Text = dr["Option1Name"].ToString();
                frm_MatrixProductUC.txtOption2.Text = dr["Option2Name"].ToString();
                frm_MatrixProductUC.txtOption3.Text = dr["Option3Name"].ToString();

                frm_MatrixProductUC.colOptionValue1.Header = frm_MatrixProductUC.txtOption1.Text;
                frm_MatrixProductUC.colOptionValue2.Header = frm_MatrixProductUC.txtOption2.Text;
                frm_MatrixProductUC.colOptionValue3.Header = frm_MatrixProductUC.txtOption3.Text;
            }
            dtbl.Dispose();


            PosDataObject.Matrix objOption = new PosDataObject.Matrix();
            objOption.Connection = SystemVariables.Conn;
            DataTable dbtblOption1 = new DataTable();
            DataTable dbtblOption2 = new DataTable();
            DataTable dbtblOption3 = new DataTable();
            DataTable dbtblQty = new DataTable();
            try
            {
                //Fetch data for the first grid
                objOption.MatrixOptionID = intID;
                objOption.OptionValueID = 1;
                dbtblOption1 = objOption.FetchOptionValues();

                // 03-12-2013    Matrix Item – Display Size in numeric order
                frm_MatrixProductUC.grdOption1.ItemsSource = CheckAllNumericValue(dbtblOption1);

                //Fetch data for the second grid
                objOption.MatrixOptionID = intID;
                objOption.OptionValueID = 2;
                dbtblOption2 = objOption.FetchOptionValues();

                // 03-12-2013    Matrix Item – Display Size in numeric order
                frm_MatrixProductUC.grdOption2.ItemsSource = CheckAllNumericValue(dbtblOption2);


                //Fetch data for the third grid
                objOption.MatrixOptionID = intID;
                objOption.OptionValueID = 3;
                dbtblOption3 = objOption.FetchOptionValues();

                // 03-12-2013    Matrix Item – Display Size in numeric order
                frm_MatrixProductUC.grdOption3.ItemsSource = CheckAllNumericValue(dbtblOption3);

                //Fetch data for the Qty grid
                objOption.MatrixOptionID = intID;
                dbtblQty = objOption.FetchMatrixData(strCalledFrom);


                byte[] strip = GeneralFunctions.GetImageAsByteArray();
                DataTable dtblTemp1 = dbtblQty.DefaultView.ToTable();
                DataColumn column1 = new DataColumn("Image");
                column1.DataType = System.Type.GetType("System.Byte[]");
                column1.AllowDBNull = true;
                column1.Caption = "Image";
                dtblTemp1.Columns.Add(column1);

                foreach (DataRow dr in dtblTemp1.Rows)
                {
                    dr["Image"] = strip;
                }


                frm_MatrixProductUC.grdQty.ItemsSource = dtblTemp1;

                if (PrevQtyData == null)
                {
                    PrevQtyData = new DataTable();
                    PrevQtyData.Columns.Add("OptionValue1", System.Type.GetType("System.String"));
                    PrevQtyData.Columns.Add("OptionValue2", System.Type.GetType("System.String"));
                    PrevQtyData.Columns.Add("OptionValue3", System.Type.GetType("System.String"));
                    PrevQtyData.Columns.Add("QtyonHand", System.Type.GetType("System.Double"));
                }

                foreach (DataRow dr in dbtblQty.Rows)
                {
                    PrevQtyData.Rows.Add(new object[] { dr["OptionValue1"].ToString(), dr["OptionValue2"].ToString(), dr["OptionValue3"].ToString(),
                    dr["QtyonHand"].ToString()});
                }
            }
            finally
            {
                dbtblOption1.Dispose();
                dbtblOption2.Dispose();
                dbtblOption3.Dispose();
                dbtblQty.Dispose();
            }

            frm_MatrixProductUC.gridView1.FocusedRowHandle = FocusDefault(frm_MatrixProductUC.grdOption1.ItemsSource as DataTable);
            frm_MatrixProductUC.gridView2.FocusedRowHandle = FocusDefault(frm_MatrixProductUC.grdOption2.ItemsSource as DataTable);
            frm_MatrixProductUC.gridView3.FocusedRowHandle = FocusDefault(frm_MatrixProductUC.grdOption3.ItemsSource as DataTable);

            frm_MatrixProductUC.tcMatrix.TabIndex = 0;
            boolControlChanged = false;
            boolRefreshQty = false;

            if (intFID != intPID) intID = 0;
        }

        private int FocusDefault(DataTable dtbl)
        {
            int focusrowc = -1;
            bool isdefault = false;
            foreach (DataRow dr in dtbl.Rows)
            {
                focusrowc++;
                if (dr["OptionDefault"].ToString() == "True")
                {
                    isdefault = true;
                    break;
                }
            }
            if (isdefault) return focusrowc; else return 0;
        }

        private DataTable CheckAllNumericValue(DataTable dtbl)
        {
            bool f = true;
            foreach (DataRow dr in dtbl.Rows)
            {
                double d = 0;
                if (!double.TryParse(dr["OptionValue"].ToString(), out d))
                {
                    f = false;
                    break;
                }
            }
            if (dtbl.Rows.Count > 0)
            {
                if (f)
                {
                    DataTable d1 = new DataTable();
                    d1.Columns.Add("ID", System.Type.GetType("System.String"));
                    d1.Columns.Add("OptionValue", System.Type.GetType("System.Double"));
                    d1.Columns.Add("OptionDefault", System.Type.GetType("System.Boolean"));

                    foreach (DataRow dr in dtbl.Rows)
                    {
                        d1.Rows.Add(new object[] { dr["ID"].ToString(), GeneralFunctions.fnDouble(dr["OptionValue"].ToString()),
                    Convert.ToBoolean(dr["OptionDefault"].ToString())});
                    }
                    d1.DefaultView.Sort = "OptionValue asc";
                    d1.DefaultView.ApplyDefaultSort = true;

                    dtbl = d1.DefaultView.ToTable();
                    dtbl.DefaultView.Sort = "OptionValue asc";
                    dtbl.DefaultView.ApplyDefaultSort = true;
                }
            }
            else
            {
                DataTable d1 = new DataTable();
                d1.Columns.Add("ID", System.Type.GetType("System.String"));
                d1.Columns.Add("OptionValue", System.Type.GetType("System.String"));
                d1.Columns.Add("OptionDefault", System.Type.GetType("System.Boolean"));

                foreach (DataRow dr in dtbl.Rows)
                {
                    d1.Rows.Add(new object[] { dr["ID"].ToString(), dr["OptionValue"].ToString(),
                    Convert.ToBoolean(dr["OptionDefault"].ToString())});
                }
                d1.DefaultView.Sort = "OptionValue asc";
                d1.DefaultView.ApplyDefaultSort = true;

                dtbl = d1.DefaultView.ToTable();
                dtbl.DefaultView.Sort = "OptionValue asc";
                dtbl.DefaultView.ApplyDefaultSort = true;
            }

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dtbl.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }




            return dtblTemp;
        }

        private int FetchMatrixOptionID()
        {
            PosDataObject.Matrix objMatrix = new PosDataObject.Matrix();
            objMatrix.Connection = new SqlConnection(SystemVariables.ConnectionString);
            return objMatrix.FetchMatrixOptionID(intPID);
        }

        private void GridView1_GotFocus(object sender, RoutedEventArgs e)
        {
            intActiveGrid = 1;
        }

        private void GridView2_GotFocus(object sender, RoutedEventArgs e)
        {
            intActiveGrid = 2;
        }

        private void GridView3_GotFocus(object sender, RoutedEventArgs e)
        {
            intActiveGrid = 3;
        }

        private void TxtOption1_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if ((sender as TextEdit).Text == "") boolRefreshQty = true;
            boolControlChanged = true;
        }


        private void PrepareQtyGridData()
        {
            DataTable dbtblOption1 = new DataTable();
            DataTable dbtblOption2 = new DataTable();
            DataTable dbtblOption3 = new DataTable();
            DataTable dtbl = new DataTable();

            try
            {
                dbtblOption1 = (frm_MatrixProductUC.grdOption1.ItemsSource as DataTable);
                dbtblOption2 = (frm_MatrixProductUC.grdOption2.ItemsSource as DataTable);
                dbtblOption3 = (frm_MatrixProductUC.grdOption3.ItemsSource as DataTable);

                dtbl.Columns.Add("OptionValue1", System.Type.GetType("System.String"));
                dtbl.Columns.Add("OptionValue2", System.Type.GetType("System.String"));
                dtbl.Columns.Add("OptionValue3", System.Type.GetType("System.String"));
                dtbl.Columns.Add("QtyonHand", System.Type.GetType("System.Int32"));
                dtbl.Columns.Add("QtyonHand", System.Type.GetType("System.Byte[]"));

                byte[] strip = GeneralFunctions.GetImageAsByteArray();

                string strValue1 = "";
                string strValue2 = "";
                string strValue3 = "";

                foreach (DataRow dr1 in dbtblOption1.Rows)
                {
                    strValue1 = dr1["OptionValue"].ToString();
                    if (dbtblOption2.Rows.Count > 0)
                    {
                        foreach (DataRow dr2 in dbtblOption2.Rows)
                        {
                            strValue2 = dr2["OptionValue"].ToString();
                            if (dbtblOption3.Rows.Count > 0)
                            {
                                foreach (DataRow dr3 in dbtblOption3.Rows)
                                {
                                    strValue3 = dr3["OptionValue"].ToString();
                                    dtbl.Rows.Add(new object[] { strValue1, strValue2, strValue3,
                                                ReturnQtyWithOptionValueCombination(strValue1, strValue2, strValue3), strip});
                                }
                            }
                            else
                            {
                                dtbl.Rows.Add(new object[] { strValue1, strValue2, strValue3,
                                                ReturnQtyWithOptionValueCombination(strValue1, strValue2, strValue3), strip});
                            }
                        }
                    }
                    else
                    {
                        dtbl.Rows.Add(new object[] { strValue1, strValue2, strValue3,
                                                ReturnQtyWithOptionValueCombination(strValue1, strValue2, strValue3), strip});
                    }
                }

                frm_MatrixProductUC.grdQty.ItemsSource = dtbl;
            }
            finally
            {
                dbtblOption1.Dispose();
                dbtblOption2.Dispose();
                dbtblOption3.Dispose();
                dtbl.Dispose();
                boolRefreshQty = false;
            }

        }

        private double ReturnQtyWithOptionValueCombination(string val1, string val2, string val3)
        {
            double qty = 0.00;
            foreach (DataRow dr in PrevQtyData.Rows)
            {
                if ((dr["OptionValue1"].ToString() == val1) && (dr["OptionValue2"].ToString() == val2) && (dr["OptionValue3"].ToString() == val3))
                {
                    qty = GeneralFunctions.fnDouble(dr["QtyonHand"].ToString());
                    break;
                }
            }
            return qty;
        }

        private void UpdatePreviousSavedOptionValue(int pos, string oldval, string newval)
        {
            if (pos == 1)
            {
                foreach (DataRow dr1 in PrevQtyData.Rows)
                {
                    if (dr1["OptionValue1"].ToString() == oldval)
                    {
                        dr1["OptionValue1"] = newval;
                    }
                }
            }

            if (pos == 2)
            {
                foreach (DataRow dr2 in PrevQtyData.Rows)
                {
                    if (dr2["OptionValue2"].ToString() == oldval)
                    {
                        dr2["OptionValue2"] = newval;
                    }
                }
            }

            if (pos == 3)
            {
                foreach (DataRow dr3 in PrevQtyData.Rows)
                {
                    if (dr3["OptionValue3"].ToString() == oldval)
                    {
                        dr3["OptionValue3"] = newval;
                    }
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            frm_MatrixProductUC.barButtonItem1.Click += new RoutedEventHandler(BtnAdd1Click);
            frm_MatrixProductUC.barButtonItem4.Click += new RoutedEventHandler(BtnAdd2Click);
            frm_MatrixProductUC.barButtonItem7.Click += new RoutedEventHandler(BtnAdd3Click);

            frm_MatrixProductUC.barButtonItem2.Click += new RoutedEventHandler(BtnEdit1Click);
            frm_MatrixProductUC.barButtonItem5.Click += new RoutedEventHandler(BtnEdit2Click);
            frm_MatrixProductUC.barButtonItem8.Click += new RoutedEventHandler(BtnEdit3Click);

            frm_MatrixProductUC.barButtonItem3.Click += new RoutedEventHandler(BtnDelete1Click);
            frm_MatrixProductUC.barButtonItem6.Click += new RoutedEventHandler(BtnDelete2Click);
            frm_MatrixProductUC.barButtonItem9.Click += new RoutedEventHandler(BtnDelete3Click);

            frm_MatrixProductUC.gridView1.GotFocus += new RoutedEventHandler(GridView1_GotFocus);
            frm_MatrixProductUC.gridView2.GotFocus += new RoutedEventHandler(GridView2_GotFocus);
            frm_MatrixProductUC.gridView3.GotFocus += new RoutedEventHandler(GridView3_GotFocus);

            frm_MatrixProductUC.txtOption1.EditValueChanged += new EditValueChangedEventHandler(TxtOption1_EditValueChanged);
            frm_MatrixProductUC.txtOption2.EditValueChanged += new EditValueChangedEventHandler(TxtOption1_EditValueChanged);
            frm_MatrixProductUC.txtOption3.EditValueChanged += new EditValueChangedEventHandler(TxtOption1_EditValueChanged);

            frm_MatrixProductUC.tcMatrix.MouseLeftButtonUp += TcMatrix_MouseLeftButtonUp;

            frm_MatrixProductUC.gridView1.MouseDoubleClick += GridView1_MouseDoubleClick;
            frm_MatrixProductUC.gridView2.MouseDoubleClick += GridView2_MouseDoubleClick;
            frm_MatrixProductUC.gridView3.MouseDoubleClick += GridView3_MouseDoubleClick;
            frm_MatrixProductUC.gridView4.CellValueChanging += GridView4_CellValueChanging;
            frm_MatrixProductUC.grdQty.PreviewKeyDown += RepQty_PreviewKeyDown;

            PrevQtyData = new DataTable();
            PrevQtyData.Columns.Add("OptionValue1", System.Type.GetType("System.String"));
            PrevQtyData.Columns.Add("OptionValue2", System.Type.GetType("System.String"));
            PrevQtyData.Columns.Add("OptionValue3", System.Type.GetType("System.String"));
            PrevQtyData.Columns.Add("QtyonHand", System.Type.GetType("System.Double"));

            if (strCalledFrom == "Stock Journal")
            {
                frm_MatrixProductUC.tpDefinition.Visibility = Visibility.Hidden;
                frm_MatrixProductUC.tcMatrix.TabIndex = 1;
            }
            if (strCalledFrom == "Product")
            {
                if (FetchMatrixOptionID() != 0)
                {
                    frm_MatrixProductUC.colQtyonHand.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                    frm_MatrixProductUC.colQtyonHand.AllowFocus = true;
                    frm_MatrixProductUC.colQtyonHand.AllowGrouping = DevExpress.Utils.DefaultBoolean.False;
                    frm_MatrixProductUC.colQtyonHand.AllowIncrementalSearch = true;
                    frm_MatrixProductUC.colQtyonHand.AllowCellMerge = false;
                    frm_MatrixProductUC.colQtyonHand.AllowSorting = DevExpress.Utils.DefaultBoolean.False;
                }
                else
                {
                    frm_MatrixProductUC.colQtyonHand.AllowEditing = DevExpress.Utils.DefaultBoolean.True;
                    frm_MatrixProductUC.colQtyonHand.AllowFocus = true;
                    frm_MatrixProductUC.colQtyonHand.AllowGrouping = DevExpress.Utils.DefaultBoolean.True;
                    frm_MatrixProductUC.colQtyonHand.AllowIncrementalSearch = true;
                    frm_MatrixProductUC.colQtyonHand.AllowCellMerge = true;
                    frm_MatrixProductUC.colQtyonHand.AllowSorting = DevExpress.Utils.DefaultBoolean.True;

                }
                

            }
            LoadData();
        }



        void BtnAdd1Click(object sender, RoutedEventArgs e)
        {
            AddMatrixValue(frm_MatrixProductUC.grdOption1);
        }

        void BtnAdd2Click(object sender, RoutedEventArgs e)
        {
            AddMatrixValue(frm_MatrixProductUC.grdOption2);
        }

        void BtnAdd3Click(object sender, RoutedEventArgs e)
        {
            AddMatrixValue(frm_MatrixProductUC.grdOption3);
        }

        async void BtnEdit1Click(object sender, RoutedEventArgs e)
        {
            await EditMatrixValue(frm_MatrixProductUC.grdOption1, frm_MatrixProductUC.colOption1Value, frm_MatrixProductUC.colOption1Default);
        }

        async void BtnEdit2Click(object sender, RoutedEventArgs e)
        {
            await EditMatrixValue(frm_MatrixProductUC.grdOption2, frm_MatrixProductUC.colOption2Value, frm_MatrixProductUC.colOption2Default);
        }

        async void BtnEdit3Click(object sender, RoutedEventArgs e)
        {
            await EditMatrixValue(frm_MatrixProductUC.grdOption3, frm_MatrixProductUC.colOption3Value, frm_MatrixProductUC.colOption3Default);
        }

        void BtnDelete1Click(object sender, RoutedEventArgs e)
        {
            DeleteMatrixValue(frm_MatrixProductUC.grdOption1);
        }

        void BtnDelete2Click(object sender, RoutedEventArgs e)
        {
            DeleteMatrixValue(frm_MatrixProductUC.grdOption2);
        }

        void BtnDelete3Click(object sender, RoutedEventArgs e)
        {
            DeleteMatrixValue(frm_MatrixProductUC.grdOption3);
        }

        async void GridView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            await EditMatrixValue(frm_MatrixProductUC.grdOption1, frm_MatrixProductUC.colOption1Value, frm_MatrixProductUC.colOption1Default);
        }

        async void GridView2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            await EditMatrixValue(frm_MatrixProductUC.grdOption2, frm_MatrixProductUC.colOption2Value, frm_MatrixProductUC.colOption2Default);
        }

        async void GridView3_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            await EditMatrixValue(frm_MatrixProductUC.grdOption3, frm_MatrixProductUC.colOption3Value, frm_MatrixProductUC.colOption3Default);
        }

        private void DeleteMatrixValue(GridControl grdDelete)
        {
            if ((grdDelete.ItemsSource as DataTable).Rows.Count == 0) return;
            int intRowNum = 0;
            intRowNum = grdDelete.View.FocusedRowHandle;
            if (intRowNum < 0) return;
            if (DocMessage.MsgDelete())
            {
                (grdDelete.View as TableView).DeleteRow(intRowNum);
                DataTable dtbl = grdDelete.ItemsSource as DataTable;
                // 03-12-2013    Matrix Item – Display Size in numeric order
                grdDelete.ItemsSource = CheckAllNumericValue(dtbl);
                dtbl.Dispose();
                boolRefreshQty = true;
                boolControlChanged = true;
                PrepareQtyGridData();
            }
        }

        private void EditGridValue(GridControl gvEdit, GridColumn colValue, GridColumn colDefault, string MatValue, Boolean MatDefault)
        {
            if ((gvEdit.ItemsSource as DataTable).Rows.Count == 0) return;
            int intRowNum = 0;
            intRowNum = gvEdit.View.FocusedRowHandle;
            if (intRowNum < 0) return;

            try
            {
                string s = colValue.FieldType.ToString();
                double d = 0;
                if ((s == "System.Double") && (!double.TryParse(MatValue, out d)))
                {
                    DocMessage.MsgInformation(Properties.Resources.Only_numeric_value_is_accepted_as_all_the_other_values_are_numeric);
                    return;
                }
                gvEdit.SetCellValue(intRowNum, colValue, MatValue);
            }
            catch
            {
                DocMessage.MsgInformation(Properties.Resources.Only_numeric_value_is_accepted_as_all_the_other_values_are_numeric);
                return;
            }

            gvEdit.SetCellValue(intRowNum, colDefault, MatDefault);
            
            DataTable dtbl = gvEdit.ItemsSource as DataTable;
            // 03-12-2013    Matrix Item – Display Size in numeric order
            gvEdit.ItemsSource = CheckAllNumericValue(dtbl);
            dtbl.Dispose();

            /*Todo:
            ColumnView View = gvEdit.GridControl.MainView as ColumnView;
            View.BeginUpdate();
            try
            {
                int rowHandle = 0;
                DevExpress.XtraGrid.Columns.GridColumn col = View.Columns["OptionValue"];
                while (true)
                {
                    // locating the next row 
                    rowHandle = View.LocateByValue(rowHandle, col, MatValue);
                    // exiting the loop if no row is found 
                    if (rowHandle == DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                        break;
                    // perform specific operations on the row found here 
                    // ... 
                    rowHandle++;
                }
            }
            finally { View.EndUpdate(); }*/

        }

        private async Task EditMatrixValue(GridControl grdEdit, GridColumn colValue, GridColumn colDefault)
        {
            if ((grdEdit.ItemsSource as DataTable).Rows.Count == 0) return;
            int intRowNum = 0;
            intRowNum = grdEdit.View.FocusedRowHandle;
            if (intRowNum < 0) return;
            blurGrid.Visibility = Visibility.Visible;
            frm_MatrixValue frmMatrixValue = new frm_MatrixValue();
            try
            {
                frmMatrixValue.MatrixValue = await GeneralFunctions.GetCellValue1(intRowNum, grdEdit, colValue);
                if (await GeneralFunctions.GetCellValue1(intRowNum, grdEdit, colDefault) == "True")
                {
                    frmMatrixValue.MatrixDefault = true;
                }
                else
                {
                    frmMatrixValue.MatrixDefault = false;
                }

                string oldval = frmMatrixValue.MatrixValue;

                frmMatrixValue.MatrixData = (grdEdit.ItemsSource as DataTable);
                frmMatrixValue.ShowDialog();
                if (frmMatrixValue.OK == true)
                {
                    if (frmMatrixValue.chkDefault.IsChecked == true)
                    {
                        DataTable dtbl = grdEdit.ItemsSource as DataTable;
                        foreach (DataRow dr in dtbl.Rows)
                        {
                            dr["OptionDefault"] = "False";
                        }
                        grdEdit.ItemsSource = dtbl;
                        dtbl.Dispose();
                    }

                    EditGridValue(grdEdit, colValue, colDefault, frmMatrixValue.txtValue.Text.Trim(), frmMatrixValue.chkDefault.IsChecked == true ? true : false);


                    int pos = 0;

                    if (grdEdit.Name == "grdOption1") pos = 1;
                    if (grdEdit.Name == "grdOption2") pos = 2;
                    if (grdEdit.Name == "grdOption3") pos = 3;
                    UpdatePreviousSavedOptionValue(pos, oldval, frmMatrixValue.txtValue.Text.Trim());

                    boolRefreshQty = true;
                    boolControlChanged = true;

                    PrepareQtyGridData();
                }
            }
            finally
            {
                frmMatrixValue.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

    


        private bool SaveData()
        {
            

            PosDataObject.Matrix objMatrix = new PosDataObject.Matrix();
            objMatrix.Connection = SystemVariables.Conn;
            objMatrix.LoginUserID = SystemVariables.CurrentUserID;
            objMatrix.ProductID = intPID;
            objMatrix.ID = intID;
            objMatrix.Option1Name = frm_MatrixProductUC.txtOption1.Text;
            objMatrix.Option2Name = frm_MatrixProductUC.txtOption2.Text;
            objMatrix.Option3Name = frm_MatrixProductUC.txtOption3.Text;
            objMatrix.MatrixValues1 = (frm_MatrixProductUC.grdOption1.ItemsSource as DataTable);
            objMatrix.MatrixValues2 = (frm_MatrixProductUC.grdOption2.ItemsSource as DataTable);
            objMatrix.MatrixValues3 = (frm_MatrixProductUC.grdOption3.ItemsSource as DataTable);
            objMatrix.MatrixData = (frm_MatrixProductUC.grdQty.ItemsSource as DataTable);
            return (objMatrix.PostData() == "");
        }

        private bool IsValidAll()
        {
            return true;
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (strCalledFrom == "Product")
            {
                if (IsValidAll())
                {
                    if (SaveData())
                    {
                        boolControlChanged = false;
                        boolOK = true;
                        
                    }
                }
            }
            if (strCalledFrom == "Stock Journal")
            {
                boolControlChanged = false;
                boolOK = false;
                
            }
        }


        private void AddMatrixValue(GridControl grdAdd)
        {
            blurGrid.Visibility = Visibility.Visible;
            frm_MatrixValue frmMatrixValue = new frm_MatrixValue();
            try
            {
                frmMatrixValue.MatrixValue = "";
                frmMatrixValue.MatrixDefault = false;
                frmMatrixValue.MatrixData = (grdAdd.ItemsSource as DataTable);
                frmMatrixValue.ShowDialog();

                if (frmMatrixValue.OK)
                {
                    if (frmMatrixValue.chkDefault.IsChecked == true)
                    {
                        DataTable dtbl = grdAdd.ItemsSource as DataTable;
                        foreach (DataRow dr in dtbl.Rows)
                        {
                            dr["OptionDefault"] = "False";
                        }
                        grdAdd.ItemsSource = dtbl;
                        dtbl.Dispose();
                    }

                    AddtoGrid(grdAdd, frmMatrixValue.txtValue.Text.Trim(), frmMatrixValue.chkDefault.IsChecked == true? true : false);
                    FocusRowAfterAdd(grdAdd, frmMatrixValue.txtValue.Text.Trim());
                    boolRefreshQty = true;
                    boolControlChanged = true;

                    PrepareQtyGridData();
                }
            }
            finally
            {
                frmMatrixValue.Close();
                blurGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void FocusRowAfterAdd(GridControl grdAdd, string val)
        {
            int rH = -1;
            foreach(DataRow dr in (grdAdd.ItemsSource as DataTable).Rows)
            {
                rH++;
                if (dr["OptionValue"].ToString() == val)
                {
                    break;
                }
            }

            grdAdd.View.FocusedRowHandle = rH;
        }

        private void AddtoGrid(GridControl grdAdd, string MatValue, Boolean MatDefault)
        {
            DataTable dtbl = new DataTable();
            dtbl = (grdAdd.ItemsSource) as DataTable;
            grdAdd.ItemsSource = CheckAllNumericValue(dtbl);
            try
            {
                dtbl.Rows.Add(new object[] { "0", MatValue, MatDefault });
            }
            catch
            {
                DocMessage.MsgInformation(Properties.Resources.Only_numeric_value_is_accepted_as_all_the_other_values_are_numeric);
            }
            // 03-12-2013    Matrix Item – Display Size in numeric order
            grdAdd.ItemsSource = CheckAllNumericValue(dtbl);
            dtbl.Dispose();

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (boolControlChanged)
            {
                MessageBoxResult DlgResult = new MessageBoxResult();
                DlgResult = new MessageBoxWindow().Show(Properties.Resources.strSaveChanges, Properties.Resources.Confirm, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                if (DlgResult == MessageBoxResult.Yes)
                {
                    if (IsValidAll())
                    {
                        if (SaveData())
                        {
                            boolControlChanged = false;
                            boolOK = true;
                        }
                        else
                        {
                            e.Cancel = true;
                        }
                    }

                    else
                        e.Cancel = true;
                }

                if (DlgResult == MessageBoxResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            boolControlChanged = false;
            boolOK = false;
            
        }

        private void GridView4_CellValueChanging(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "QtyonHand")
            {
                e.Source.PostEditor();
                frm_MatrixProductUC.grdQty.UpdateTotalSummary();
                boolControlChanged = true;
            }

        }

        private void RepQty_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int rH = frm_MatrixProductUC.gridView4.FocusedRowHandle + 1;

                if (frm_MatrixProductUC.grdQty.IsValidRowHandle(rH))
                {
                    frm_MatrixProductUC.grdQty.View.FocusedRowHandle = rH;
                    frm_MatrixProductUC.grdQty.CurrentColumn = frm_MatrixProductUC.grdQty.Columns["QtyonHand"];
                    (frm_MatrixProductUC.grdQty.View as TableView).SelectCell(rH, frm_MatrixProductUC.colQtyonHand);
                }
            }
        }

        private void TcMatrix_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            frm_MatrixProductUC.colOptionValue1.Header = frm_MatrixProductUC.txtOption1.Text == "" ? " " : frm_MatrixProductUC.txtOption1.Text;
            frm_MatrixProductUC.colOptionValue2.Header = frm_MatrixProductUC.txtOption2.Text == "" ? " " : frm_MatrixProductUC.txtOption2.Text;
            frm_MatrixProductUC.colOptionValue3.Header = frm_MatrixProductUC.txtOption3.Text == "" ? " " : frm_MatrixProductUC.txtOption3.Text;

            if (frm_MatrixProductUC.tcMatrix.SelectedIndex == 1)
            {

                if ((boolRefreshQty) || ((frm_MatrixProductUC.grdQty.ItemsSource as DataTable).Rows.Count == 0))
                {
                    PrepareQtyGridData();
                }
                try
                {
                    (frm_MatrixProductUC.grdQty.View as TableView).SelectCell(frm_MatrixProductUC.grdQty.View.FocusedRowHandle, frm_MatrixProductUC.colQtyonHand);
                }
                catch
                {

                }
            }

            if (frm_MatrixProductUC.tcMatrix.SelectedIndex == 0)
            {
                DataTable d = new DataTable();
                d = frm_MatrixProductUC.grdQty.ItemsSource as DataTable;
                PrevQtyData.Rows.Clear();
                foreach (DataRow d1 in d.Rows)
                {
                    PrevQtyData.ImportRow(d1);
                }

                d.Dispose();
            }
        }

        public double GetStockTotal()
        {
            return GeneralFunctions.fnDouble(frm_MatrixProductUC.colQtyonHand.TotalSummaries[0].Value);
        }
    }
}
