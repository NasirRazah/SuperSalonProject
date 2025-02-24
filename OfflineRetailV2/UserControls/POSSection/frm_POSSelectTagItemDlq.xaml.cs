using OfflineRetailV2.Data;
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

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_POSSelectTagItemDlq.xaml
    /// </summary>
    public partial class frm_POSSelectTagItemDlq : Window
    {
        public frm_POSSelectTagItemDlq()
        {
            InitializeComponent();

            Loaded += Frm_POSSelectTagItemDlq_Loaded;
        }

        private void Frm_POSSelectTagItemDlq_Loaded(object sender, RoutedEventArgs e)
        {
            dtblAvail = new DataTable();
            dtblAvail.Columns.Add("INV", System.Type.GetType("System.String"));
            dtblAvail.Columns.Add("ID", System.Type.GetType("System.String"));
            dtblAvail.Columns.Add("PID", System.Type.GetType("System.String"));
            dtblAvail.Columns.Add("SKU", System.Type.GetType("System.String"));
            dtblAvail.Columns.Add("PNAME", System.Type.GetType("System.String"));
            dtblAvail.Columns.Add("PTYPE", System.Type.GetType("System.String"));
            dtblAvail.Columns.Add("QTY", System.Type.GetType("System.Double"));
            dtblAvail.Columns.Add("PRICE", System.Type.GetType("System.Double"));
            dtblAvail.Columns.Add("TAG", System.Type.GetType("System.String"));


            GetDataForgrd1();
            GetDataForgrd2();

            dtblAvail = grd1.ItemsSource as DataTable;
            dtblInit = dtblAvail;
        }

        private DataTable dtblSelectData;
        private DataTable dtblAvail = null;
        private DataTable dtblInit;
        private int intParentID;
        private int intInvNo;
        private int intItemID;
        public int ItemID
        {
            get { return intItemID; }
            set { intItemID = value; }
        }

        public int ParentID
        {
            get { return intParentID; }
            set { intParentID = value; }
        }
        public int InvNo
        {
            get { return intInvNo; }
            set { intInvNo = value; }
        }

        public DataTable SelectData
        {
            get { return dtblSelectData; }
            set { dtblSelectData = value; }
        }

        private void GetDataForgrd1()
        {
            DataTable dtbl1 = new DataTable();
            PosDataObject.POS objp = new PosDataObject.POS();
            objp.Connection = SystemVariables.Conn;
            dtbl1 = objp.FetchAvailableTagItem(intInvNo, intParentID, intItemID);

            if (dtblSelectData.Rows.Count > 0)
            {
                int i = 0;
                foreach (DataRow dr in dtbl1.Rows)
                {
                    foreach (DataRow dr1 in dtblSelectData.Rows)
                    {
                        if (dr1["INV"].ToString() != dr["INV"].ToString()) continue;
                        if ((dr1["ID"].ToString() == dr["ID"].ToString()) && (dr1["PID"].ToString() == dr["PID"].ToString()))
                        {
                            double val = GeneralFunctions.fnDouble(dr["QTY"].ToString()) - GeneralFunctions.fnDouble(dr1["QTY"].ToString());
                            if (val > 0) dr["QTY"] = val.ToString();
                            else dr["TAG"] = "X";
                        }
                    }
                    i++;
                }

                foreach (DataRow dr2 in dtbl1.Rows)
                {
                    if (dr2["TAG"].ToString() == "X") continue;
                    dtblAvail.Rows.Add(new object[]
                                                {
                                                dr2["INV"].ToString(),
                                                dr2["ID"].ToString(),
                                                dr2["PID"].ToString(),
                                                dr2["SKU"].ToString(),
                                                dr2["PNAME"].ToString(),
                                                dr2["PTYPE"].ToString(),
                                                GeneralFunctions.fnDouble(dr2["QTY"].ToString()),
                                                GeneralFunctions.fnDouble(dr2["PRICE"].ToString()),
                                                dr2["TAG"].ToString()
                                                });
                }
                grd1.ItemsSource = dtblAvail;
                dtbl1.Dispose();
            }
            else
            {
                grd1.ItemsSource = dtbl1;
                dtblAvail = dtbl1;
                dtbl1.Dispose();
            }

        }

        private void GetDataForgrd2()
        {
            grd2.ItemsSource = dtblSelectData;
        }

        private async void btnTagReturn_Click(object sender, RoutedEventArgs e)
        {
            if (gridView1.FocusedRowHandle < 0) return;
            int inv = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle,grd1, col1INV));
            int id = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle,grd1, col1ID));
            int pid = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle,grd1, col1PID));
            string sku = await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle,grd1, col1SKU);
            string pname = await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle,grd1, col1Desc);
            string ptype = await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle,grd1, col1PType);
            double qty = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle,grd1, col1Qty));
            double price = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle,grd1, col1Price));
            bool fnd = false;
            foreach (DataRow dr in dtblSelectData.Rows)
            {
                if ((GeneralFunctions.fnInt32(dr["INV"].ToString()) == inv) && (GeneralFunctions.fnInt32(dr["ID"].ToString()) == id) && (GeneralFunctions.fnInt32(dr["PID"].ToString()) == pid))
                {
                    double val = GeneralFunctions.fnDouble(dr["QTY"].ToString());
                    dr["Qty"] = (val + qty).ToString();
                    fnd = true;
                    break;
                }
            }
            if (!fnd)
            {
                dtblSelectData.Rows.Add(new object[] {
                                                inv.ToString(),
                                                id.ToString(),
                                                pid.ToString(),
                                                sku.ToString(),
                                                pname.ToString(),
                                                ptype.ToString(),
                                                qty,
                                                price,inv.ToString(),qty});
            }
            gridView1.DeleteRow(gridView1.FocusedRowHandle);
            dtblAvail = grd1.ItemsSource as DataTable;
            grd1.ItemsSource = dtblAvail;
            grd2.ItemsSource = dtblSelectData;
        }

        private async void btnLessQty_Click(object sender, RoutedEventArgs e)
        {
            if (gridView2.FocusedRowHandle < 0) return;
            if (intInvNo != GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2CInv))) return;
            double qty = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2Qty));

            int inv = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2Inv));
            int id = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2ID));
            int pid = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2PID));
            string sku = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2SKU);
            string pname = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2Desc);
            string ptype = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2PType);
            double qty1 = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2Qty));
            double price = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2Price));


            if (qty == 1)
            {
                gridView2.DeleteRow(gridView2.FocusedRowHandle);
            }
            else
            {
                grd2.SetCellValue(gridView2.FocusedRowHandle, col2Qty, (qty - 1).ToString());
            }
            dtblSelectData = grd2.ItemsSource as DataTable;

            bool fnd = false;
            foreach (DataRow dr in dtblAvail.Rows)
            {
                if ((GeneralFunctions.fnInt32(dr["INV"].ToString()) == inv) && (GeneralFunctions.fnInt32(dr["ID"].ToString()) == id) && (GeneralFunctions.fnInt32(dr["PID"].ToString()) == pid))
                {
                    double val = GeneralFunctions.fnDouble(dr["QTY"].ToString());
                    dr["Qty"] = (val + 1).ToString();
                    fnd = true;
                    break;
                }
            }
            if (!fnd)
            {
                dtblAvail.Rows.Add(new object[] {
                                                inv.ToString(),
                                                id.ToString(),
                                                pid.ToString(),
                                                sku.ToString(),
                                                pname.ToString(),
                                                ptype.ToString(),
                                                1,
                                                price,"Y"});
            }

            grd1.ItemsSource = dtblAvail;
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            dtblSelectData = grd2.ItemsSource as DataTable;
            DialogResult = true;
        }

        private async void btnMoreQty_Click(object sender, RoutedEventArgs e)
        {
            if (gridView2.FocusedRowHandle < 0) return;
            if (intInvNo != GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2CInv))) return;
            double qty = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2Qty));

            int inv = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2Inv));
            int id = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2ID));
            int pid = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2PID));
            string sku = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2SKU);
            string pname = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2Desc);
            string ptype = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2PType);

            double price = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2Price));
            double qty1 = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2TQty));


            if (qty == qty1)
            {
                return;
            }
            else
            {
                grd2.SetCellValue(gridView2.FocusedRowHandle, col2Qty, (qty + 1).ToString());
            }
            dtblSelectData = grd2.ItemsSource as DataTable;

            bool fnd = false;
            int i = 0;
            foreach (DataRow dr in dtblAvail.Rows)
            {
                if ((GeneralFunctions.fnInt32(dr["INV"].ToString()) == inv) && (GeneralFunctions.fnInt32(dr["ID"].ToString()) == id) && (GeneralFunctions.fnInt32(dr["PID"].ToString()) == pid))
                {
                    double val = GeneralFunctions.fnDouble(dr["QTY"].ToString());
                    dr["Qty"] = (val - 1).ToString();
                    if (GeneralFunctions.fnDouble(dr["Qty"].ToString()) == 0)
                    {
                        dtblAvail.Rows.RemoveAt(i);
                    }
                    fnd = true;
                    break;
                }
                i++;
            }

            grd1.ItemsSource = dtblAvail;
        }

        private async void repSelect_DefaultButtonClick(object sender, RoutedEventArgs e)
        {
            //if (e.Button.Index == 0)
            //{
            //    if (gridView1.FocusedRowHandle < 0) return;
            //    int inv = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle,grd1, col1INV));
            //    int id = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle,grd1, col1ID));
            //    int pid = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle,grd1, col1PID));
            //    string sku = await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle,grd1, col1SKU);
            //    string pname = await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle,grd1, col1Desc);
            //    string ptype = await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle,grd1, col1PType);
            //    double qty = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle,grd1, col1Qty));
            //    double price = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(gridView1.FocusedRowHandle,grd1, col1Price));
            //    bool fnd = false;
            //    foreach (DataRow dr in dtblSelectData.Rows)
            //    {
            //        if ((GeneralFunctions.fnInt32(dr["INV"].ToString()) == inv) && (GeneralFunctions.fnInt32(dr["ID"].ToString()) == id) && (GeneralFunctions.fnInt32(dr["PID"].ToString()) == pid))
            //        {
            //            double val = GeneralFunctions.fnDouble(dr["QTY"].ToString());
            //            dr["Qty"] = (val + qty).ToString();
            //            fnd = true;
            //            break;
            //        }
            //    }
            //    if (!fnd)
            //    {
            //        dtblSelectData.Rows.Add(new object[] {
            //                                    inv.ToString(),
            //                                    id.ToString(),
            //                                    pid.ToString(),
            //                                    sku.ToString(),
            //                                    pname.ToString(),
            //                                    ptype.ToString(),
            //                                    qty,
            //                                    price,inv.ToString(),qty});
            //    }
            //    gridView1.DeleteRow(gridView1.FocusedRowHandle);
            //    dtblAvail = grd1.ItemsSource as DataTable;
            //    grd1.ItemsSource = dtblAvail;
            //    grd2.ItemsSource = dtblSelectData;
            //}


        }

        private async void repBtn_DefaultButtonClick(object sender, RoutedEventArgs e)
        {
            //if (e.Button.Index == 0)
            //{
            //    if (gridView2.FocusedRowHandle < 0) return;
            //    if (intInvNo != GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2CInv))) return;
            //    double qty = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2Qty));

            //    int inv = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2Inv));
            //    int id = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2ID));
            //    int pid = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2PID));
            //    string sku = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2SKU);
            //    string pname = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2Desc);
            //    string ptype = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2PType);

            //    double price = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2Price));
            //    double qty1 = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2TQty));


            //    if (qty == qty1)
            //    {
            //        return;
            //    }
            //    else
            //    {
            //        grd2.SetCellValue(gridView2.FocusedRowHandle, col2Qty, (qty + 1).ToString());
            //    }
            //    dtblSelectData = grd2.ItemsSource as DataTable;

            //    bool fnd = false;
            //    int i = 0;
            //    foreach (DataRow dr in dtblAvail.Rows)
            //    {
            //        if ((GeneralFunctions.fnInt32(dr["INV"].ToString()) == inv) && (GeneralFunctions.fnInt32(dr["ID"].ToString()) == id) && (GeneralFunctions.fnInt32(dr["PID"].ToString()) == pid))
            //        {
            //            double val = GeneralFunctions.fnDouble(dr["QTY"].ToString());
            //            dr["Qty"] = (val - 1).ToString();
            //            if (GeneralFunctions.fnDouble(dr["Qty"].ToString()) == 0)
            //            {
            //                dtblAvail.Rows.RemoveAt(i);
            //            }
            //            fnd = true;
            //            break;
            //        }
            //        i++;
            //    }

            //    grd1.ItemsSource = dtblAvail;
            //}

            //if (e.Button.Index == 1)
            //{
            //    if (gridView2.FocusedRowHandle < 0) return;
            //    if (intInvNo != GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2CInv))) return;
            //    double qty = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2Qty));

            //    int inv = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2Inv));
            //    int id = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2ID));
            //    int pid = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2PID));
            //    string sku = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2SKU);
            //    string pname = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2Desc);
            //    string ptype = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2PType);
            //    double qty1 = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2Qty));
            //    double price = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2Price));


            //    if (qty == 1)
            //    {
            //        gridView2.DeleteRow(gridView2.FocusedRowHandle);
            //    }
            //    else
            //    {
            //        grd2.SetCellValue(gridView2.FocusedRowHandle, col2Qty, (qty - 1).ToString());
            //    }
            //    dtblSelectData = grd2.ItemsSource as DataTable;

            //    bool fnd = false;
            //    foreach (DataRow dr in dtblAvail.Rows)
            //    {
            //        if ((GeneralFunctions.fnInt32(dr["INV"].ToString()) == inv) && (GeneralFunctions.fnInt32(dr["ID"].ToString()) == id) && (GeneralFunctions.fnInt32(dr["PID"].ToString()) == pid))
            //        {
            //            double val = GeneralFunctions.fnDouble(dr["QTY"].ToString());
            //            dr["Qty"] = (val + 1).ToString();
            //            fnd = true;
            //            break;
            //        }
            //    }
            //    if (!fnd)
            //    {
            //        dtblAvail.Rows.Add(new object[] {
            //                                    inv.ToString(),
            //                                    id.ToString(),
            //                                    pid.ToString(),
            //                                    sku.ToString(),
            //                                    pname.ToString(),
            //                                    ptype.ToString(),
            //                                    1,
            //                                    price,"Y"});
            //    }

            //    grd1.ItemsSource = dtblAvail;
            //}

            //if (e.Button.Index == 2)
            //{
            //    if (gridView2.FocusedRowHandle < 0) return;
            //    if (intInvNo != GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2CInv))) return;
            //    double qty = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2Qty));

            //    int inv = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2Inv));
            //    int id = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2ID));
            //    int pid = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2PID));
            //    string sku = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2SKU);
            //    string pname = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2Desc);
            //    string ptype = await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2PType);
            //    double qty1 = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2Qty));
            //    double price = GeneralFunctions.fnDouble(await GeneralFunctions.GetCellValue1(gridView2.FocusedRowHandle,grd2, col2Price));


            //    gridView2.DeleteRow(gridView2.FocusedRowHandle);

            //    dtblSelectData = grd2.ItemsSource as DataTable;

            //    bool fnd = false;
            //    foreach (DataRow dr in dtblAvail.Rows)
            //    {
            //        if ((GeneralFunctions.fnInt32(dr["INV"].ToString()) == inv) && (GeneralFunctions.fnInt32(dr["ID"].ToString()) == id) && (GeneralFunctions.fnInt32(dr["PID"].ToString()) == pid))
            //        {
            //            double val = GeneralFunctions.fnDouble(dr["QTY"].ToString());
            //            dr["Qty"] = (val + qty).ToString();
            //            fnd = true;
            //            break;
            //        }
            //    }
            //    if ((!fnd) && (intInvNo == inv))
            //    {
            //        dtblAvail.Rows.Add(new object[] {
            //                                    inv.ToString(),
            //                                    id.ToString(),
            //                                    pid.ToString(),
            //                                    sku.ToString(),
            //                                    pname.ToString(),
            //                                    ptype.ToString(),
            //                                    qty,
            //                                    price,"Y"});
            //    }

            //    grd1.ItemsSource = dtblAvail;

            //}
        }
    }
}
