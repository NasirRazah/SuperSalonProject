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
using System.Data;
using System.Data.SqlClient;
using OfflineRetailV2.Data;
using Microsoft.Win32;
using DevExpress.Xpf.Grid.LookUp;
using DevExpress.Xpf.Grid;

namespace OfflineRetailV2.UserControls
{
    /// <summary>
    /// Interaction logic for frm_PriceAdjustmentDlg.xaml
    /// </summary>
    public partial class frm_PriceAdjustmentDlg : Window
    {
        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;
        public frm_PriceAdjustmentDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            CloseKeyboards();
            Close();
        }

        public void PopulateCategory()
        {
            PosDataObject.Category objCategory = new PosDataObject.Category();
            objCategory.Connection = SystemVariables.Conn;
            DataTable dbtblCat = new DataTable();
            dbtblCat = objCategory.FetchLookupData();

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtblCat.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            cmbCategory.ItemsSource = dtblTemp;

            string strval = "0";
            foreach (DataRow dr in dbtblCat.Rows)
            {
                strval = dr["ID"].ToString();
                break;
            }
            cmbCategory.EditValue = strval;
            dbtblCat.Dispose();
        }

        public void PopulateDepartment()
        {
            PosDataObject.Department objDepartment = new PosDataObject.Department();
            objDepartment.Connection = SystemVariables.Conn;
            DataTable dbtblDept = new DataTable();
            dbtblDept = objDepartment.FetchLookupData();

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtblDept.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            cmbGroup.ItemsSource = dtblTemp;
            
            string strval = "0";
            foreach (DataRow dr in dbtblDept.Rows)
            {
                strval = dr["ID"].ToString();
                break;
            }
            cmbGroup.EditValue = strval;
            dbtblDept.Dispose();
        }

        public void PopulateBrand()
        {
            PosDataObject.Brand objDepartment = new PosDataObject.Brand();
            objDepartment.Connection = SystemVariables.Conn;
            DataTable dbtblDept = new DataTable();
            dbtblDept = objDepartment.FetchLookupData1();

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            DataTable dtblTemp = dbtblDept.DefaultView.ToTable();
            DataColumn column = new DataColumn("Image");
            column.DataType = System.Type.GetType("System.Byte[]");
            column.AllowDBNull = true;
            column.Caption = "Image";
            dtblTemp.Columns.Add(column);

            foreach (DataRow dr in dtblTemp.Rows)
            {
                dr["Image"] = strip;
            }

            cmbBrand.ItemsSource = dtblTemp;

            string strval = "0";
            foreach (DataRow dr in dbtblDept.Rows)
            {
                strval = dr["ID"].ToString();
                break;
            }
            cmbBrand.EditValue = strval;
            dbtblDept.Dispose();
        }

        private void InitializePriceCheckBox()
        {
            chkAllPrice.IsChecked = true;
            chkPriceA.IsChecked = false;
            chkPriceB.IsChecked = false;
            chkPriceC.IsChecked = false;
            chkPriceA.IsEnabled = false;
            chkPriceB.IsEnabled = false;
            chkPriceC.IsEnabled = false;
        }

        private void RbD_Checked(object sender, RoutedEventArgs e)
        {
            PopulateCategory();
            PopulateDepartment();
            PopulateBrand();
            if (rbX.IsChecked == true)
            {
                lbBrand.Visibility = Visibility.Hidden;
                lbCat.Visibility = Visibility.Hidden;
                lbGroup.Visibility = Visibility.Hidden;
                cmbCategory.Visibility = Visibility.Hidden;
                cmbGroup.Visibility = Visibility.Hidden;
                cmbBrand.Visibility = Visibility.Hidden;
            }
            if (rbD.IsChecked == true)
            {
                lbBrand.Visibility = Visibility.Hidden;
                lbCat.Visibility = Visibility.Hidden;
                lbGroup.Visibility = Visibility.Visible;
                cmbCategory.Visibility = Visibility.Hidden;
                cmbGroup.Visibility = Visibility.Visible;
                cmbBrand.Visibility = Visibility.Hidden;
            }

            if (rbC.IsChecked == true)
            {
                lbBrand.Visibility = Visibility.Hidden;
                lbCat.Visibility = Visibility.Visible;
                lbGroup.Visibility = Visibility.Hidden;
                cmbCategory.Visibility = Visibility.Visible;
                cmbGroup.Visibility = Visibility.Hidden;
                cmbBrand.Visibility = Visibility.Hidden;
            }

            if (rbB.IsChecked == true)
            {
                lbBrand.Visibility = Visibility.Visible;
                lbCat.Visibility = Visibility.Hidden;
                lbGroup.Visibility = Visibility.Hidden;
                cmbCategory.Visibility = Visibility.Hidden;
                cmbGroup.Visibility = Visibility.Hidden;
                cmbBrand.Visibility = Visibility.Visible;
            }

            InitializePriceCheckBox();
        }

        private void RbP_Checked(object sender, RoutedEventArgs e)
        {
            if (rbP.IsChecked == true)
            {
                numPerc.Visibility = Visibility.Visible;
                numAbsolute.Visibility = Visibility.Hidden;
            }

            if (rbA.IsChecked == true)
            {
                numPerc.Visibility = Visibility.Hidden;
                numAbsolute.Visibility = Visibility.Visible;
            }
        }

        private void ChkAllPrice_Checked(object sender, RoutedEventArgs e)
        {
            if (chkAllPrice.IsChecked == true)
            {
                chkPriceA.IsChecked = false;
                chkPriceB.IsChecked = false;
                chkPriceC.IsChecked = false;
                chkPriceA.IsEnabled = false;
                chkPriceB.IsEnabled = false;
                chkPriceC.IsEnabled = false;
            }
            else
            {
                chkPriceA.IsChecked = false;
                chkPriceB.IsChecked = false;
                chkPriceC.IsChecked = false;
                chkPriceA.IsEnabled = true;
                chkPriceB.IsEnabled = true;
                chkPriceC.IsEnabled = true;
            }
        }

        private bool IsProceed()
        {
            if (rbD.IsChecked == true)
            {
                if (cmbGroup.EditValue == null)
                {
                    DocMessage.MsgInformation(Properties.Resources.Please_select_a_department);
                    GeneralFunctions.SetFocus(cmbGroup);
                    return false;
                }
            }
            if (rbC.IsChecked == true)
            {
                if (cmbCategory.EditValue == null)
                {
                    DocMessage.MsgInformation(Properties.Resources.Please_select_a_category);
                    GeneralFunctions.SetFocus(cmbCategory);
                    return false;
                }
            }

            if (rbB.IsChecked == true)
            {
                if (cmbBrand.EditValue == null)
                {
                    DocMessage.MsgInformation(Properties.Resources.Please_select_a_brand);
                    GeneralFunctions.SetFocus(cmbBrand);
                    return false;
                }
            }

            if (rbP.IsChecked == true)
            {
                if (GeneralFunctions.fnDouble(numPerc.Text) <= 0)
                {
                    DocMessage.MsgInformation(Properties.Resources.Please_enter_valid_percentage);
                    GeneralFunctions.SetFocus(numPerc);
                    return false;
                }
            }

            if (rbA.IsChecked == true)
            {
                if (GeneralFunctions.fnDouble(numAbsolute.Text) <= 0)
                {
                    DocMessage.MsgInformation(Properties.Resources.Please_enter_valid_absolute_price);
                    GeneralFunctions.SetFocus(numAbsolute);
                    return false;
                }
            }

            if (chkAllPrice.IsChecked == false)
            {
                if ((chkPriceA.IsChecked == false) && (chkPriceB.IsChecked == false) && (chkPriceC.IsChecked == false))
                {
                    DocMessage.MsgInformation(Properties.Resources.Please_select_atleast_1_choice_from_Price_A_Price_B__Price_C);
                    GeneralFunctions.SetFocus(chkPriceA);
                    return false;
                }
            }
            return true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();
            rbX.IsChecked = true;
            lbBrand.Visibility = Visibility.Hidden;
            lbCat.Visibility = Visibility.Hidden;
            lbGroup.Visibility = Visibility.Hidden;
            cmbCategory.Visibility = Visibility.Hidden;
            cmbGroup.Visibility = Visibility.Hidden;
            cmbBrand.Visibility = Visibility.Hidden;
            rbP.IsChecked = true;
            numPerc.Visibility = Visibility.Visible;
            numAbsolute.Visibility = Visibility.Hidden;
            rbMore.IsChecked = true;
            InitializePriceCheckBox();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (IsProceed())
            {
                string prmprall = "N";
                string prmprA = "N";
                string prmprB = "N";
                string prmprC = "N";
                string prmactive = "Y";
                double prmprval = 0;
                int selindx = 0;
                int selindx1 = 0;
                int prmcatgrp = 0;
                if (rbP.IsChecked == true)
                {
                    prmprval = GeneralFunctions.fnDouble(numPerc.Text);
                    selindx1 = 0;
                }
                if (rbA.IsChecked == true)
                {
                    prmprval = GeneralFunctions.fnDouble(numAbsolute.Text);
                    selindx1 = 1;
                }
                if (rbX.IsChecked == true)
                {
                    prmcatgrp = 0;
                    selindx = 0;
                }
                else
                {
                    if (rbX.IsChecked == true) prmcatgrp = 0;
                    if (rbD.IsChecked == true)
                    {
                        selindx = 1;
                        prmcatgrp = GeneralFunctions.fnInt32(cmbGroup.EditValue.ToString());
                    }
                    if (rbC.IsChecked == true)
                    {
                        selindx = 2;
                        prmcatgrp = GeneralFunctions.fnInt32(cmbCategory.EditValue.ToString());
                    }
                    if (rbB.IsChecked == true)
                    {
                        selindx = 3;
                        prmcatgrp = GeneralFunctions.fnInt32(cmbBrand.EditValue.ToString());
                    }
                }
                if (chkAllPrice.IsChecked == true) prmprall = "Y";
                if (chkPriceA.IsChecked == true) prmprA = "Y";
                if (chkPriceB.IsChecked == true) prmprB = "Y";
                if (chkPriceC.IsChecked == true) prmprC = "Y";

                if (chkNonzero.IsChecked == true) prmactive = "Y"; else prmactive = "N";

                int ret = 0;

                PosDataObject.Setup objstup = new PosDataObject.Setup();
                objstup.Connection = SystemVariables.Conn;
                ret = objstup.AdjustPrice(selindx, prmcatgrp, rbMore.IsChecked == true?0 : 1, selindx1,
                                          prmprval, prmprall, prmprA, prmprB, prmprC, prmactive);
                if (ret == 0)
                {
                    DocMessage.MsgInformation(Properties.Resources.Price_changed_successfully);
                    CloseKeyboards();
                    Close();
                }
                else
                {
                    DocMessage.MsgInformation("Error");
                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
            Close();
        }

        private void CmbGroup_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
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

                var location = (sender as DevExpress.Xpf.Editors.TextEdit).PointToScreen(new Point(0, 0));
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

        private void gen_PopupOpened(object sender, RoutedEventArgs e)
        {
            var editor = (LookUpEdit)sender;
            var grid = editor.GetGridControl();
            var view = (TableView)grid.View;
            view.BestFitColumns();
        }
    }
}
