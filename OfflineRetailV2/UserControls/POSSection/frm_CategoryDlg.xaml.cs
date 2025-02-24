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
    /// Interaction logic for frm_CategoryDlg.xaml
    /// </summary>
    public partial class frm_CategoryDlg : Window
    {
        public frm_CategoryDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            CloseKeyboards();
            Close();
        }

        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;

        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;

        private frm_CategoryBrwUC frmBrowse;
        private bool blLoading = false;
        private int intID;
        private int intNewID;
        private bool boolControlChanged;
        private bool boolControlChangedItem;
        private int intPrevParentCategory;
        private int intPrevDisplayOrder;
        private int intPosDisplayOrder;
        private int intInitPosDisplayOrder;
        private bool blSetStyle = false;
        private string prevcolor = "";
        private bool blOnScreenCall = false;
        private bool blDataSaved = false;

        public bool DataSaved
        {
            get { return blDataSaved; }
            set { blDataSaved = value; }
        }
        public bool OnScreenCall
        {
            get { return blOnScreenCall; }
            set { blOnScreenCall = value; }
        }

        public int ID
        {
            get { return intID; }
            set { intID = value; }
        }
        public int NewID
        {
            get { return intNewID; }
            set { intNewID = value; }
        }

        private void CmbParentCategory_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {

                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void CmbParentCategory_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
            if (cmbParentCategory.EditValue == null)
            {
                lnkroot.Visibility = Visibility.Hidden;
            }
            else if (GeneralFunctions.fnInt32(cmbParentCategory.EditValue) == 0)
            {
                lnkroot.Visibility = Visibility.Hidden;
            }
            else
            {
                lnkroot.Visibility = Visibility.Visible;
            }


            txtDisplayOrder.Text = FetchMaxPosDisPlayOrder().ToString();
        }

        private void HyperlinkEdit_RequestNavigation(object sender, DevExpress.Xpf.Editors.HyperlinkEditRequestNavigationEventArgs e)
        {
            cmbParentCategory.EditValue = null;
        }
        public frm_CategoryBrwUC BrowseForm
        {
            get
            {
                if (frmBrowse == null)
                    frmBrowse = new frm_CategoryBrwUC();
                return frmBrowse;
            }
            set
            {
                frmBrowse = value;
            }
        }

        private void PopulateAndSetFormSkin()
        {
            DataTable data = new DataTable();
            data.Columns.Add("skin");
            foreach (DevExpress.Skins.SkinContainer sk in DevExpress.Skins.SkinManager.Default.Skins)
            {
                data.Rows.Add(new object[] { sk.SkinName });
            }
            cmbSkin.ItemsSource = data;
            blSetStyle = true;
        }

        private void InitialStyleLoading()
        {
            rbColor.IsChecked = true;
            txtFontType.Text = Settings.DefaultCategoryFontType;
            txtFontSize.Text = Settings.DefaultCategoryFontSize;
        }

        private int FetchMaxPosDisPlayOrder()
        {
            int CurrParentCategory = cmbParentCategory.EditValue == null ? 0 : GeneralFunctions.fnInt32(cmbParentCategory.EditValue.ToString());
            if ((intID > 0) && (intPrevParentCategory == CurrParentCategory))
            {
                return intPrevDisplayOrder;
            }
            else
            {


                PosDataObject.Category objCategory = new PosDataObject.Category();
                objCategory.Connection = new SqlConnection(SystemVariables.ConnectionString);
                return objCategory.MaxPosDisplayOrder(CurrParentCategory) + 1;
            }

            //return objCategory.MaxPosDisplayOrder() + 1;
        }

        public void PopulateTax()
        {

            DataTable dbtblgrp = new DataTable();
            dbtblgrp.Columns.Add("ID");
            dbtblgrp.Columns.Add("TaxID");
            dbtblgrp.Columns.Add("TaxName");
            dbtblgrp.Columns.Add("Image", System.Type.GetType("System.Byte[]"));

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            PosDataObject.Category objCustomer = new PosDataObject.Category();
            objCustomer.Connection = SystemVariables.Conn;
            DataTable dbtblgrp1 = objCustomer.ShowTaxes(intID);

            foreach (DataRow dr in dbtblgrp1.Rows)
            {
                dbtblgrp.Rows.Add(new object[] { dr["ID"].ToString(), dr["TaxID"].ToString(), dr["TaxName"].ToString(), strip });
            }

            for (int i = dbtblgrp.Rows.Count; i <= 3; i++)
            {
                dbtblgrp.Rows.Add(new object[]
                {
                    "0","0","", strip
                });
            }


            grdTax.ItemsSource = dbtblgrp;

        }

        public void PopulateTaxPopup()
        {
            DataTable dbtblgrp = new DataTable();
            dbtblgrp.Columns.Add("TaxID");
            dbtblgrp.Columns.Add("TaxName");
            dbtblgrp.Columns.Add("Image", System.Type.GetType("System.Byte[]"));

            PosDataObject.Tax objGroup = new PosDataObject.Tax();
            objGroup.Connection = SystemVariables.Conn;
            DataTable dbtblgrp1 = objGroup.ShowTaxCombo();

            byte[] strip = GeneralFunctions.GetImageAsByteArray();

            foreach (DataRow dr in dbtblgrp1.Rows)
            {
                dbtblgrp.Rows.Add(new object[] { dr["TaxID"].ToString(), dr["TaxName"].ToString(), strip });
            }

            PART_Editor.AutoPopulateColumns = false;
            PART_Editor.ItemsSource = dbtblgrp;

        }

        private int DuplicateCount()
        {
            PosDataObject.Category objCategory = new PosDataObject.Category();
            objCategory.Connection = SystemVariables.Conn;
            return objCategory.DuplicateCount(txtID.Text.Trim());
        }

        private bool IsValidAll()
        {
            if (txtID.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Category_ID);
                GeneralFunctions.SetFocus(txtID);
                return false;
            }

            if (intID == 0)
            {
                if (DuplicateCount() == 1)
                {
                    DocMessage.MsgInformation(Properties.Resources.Duplicate_Category_ID);
                    GeneralFunctions.SetFocus(txtID);
                    return false;
                }
            }


            if (txtDescription.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Category_Description);
                GeneralFunctions.SetFocus(txtDescription);
                return false;
            }

            //if (GeneralFunctions.fnInt32(txtDisplayOrder.Text) == 0)
            //{
            //    DocMessage.MsgEnter(Properties.Resources.Display_Order);
            //    GeneralFunctions.SetFocus(txtDisplayOrder);
            //    return false;
            //}

            if (GeneralFunctions.fnInt32(txtProducts.Text) == 0)
            {
                DocMessage.MsgEnter(Properties.Resources.No_of_Products);
                GeneralFunctions.SetFocus(txtProducts);
                return false;
            }
            /*
                        if (intID == 0)
                        {
                            PosDataObject.Category objCategory = new PosDataObject.Category();
                            objCategory.Connection = SystemVariables.Conn;
                            if (objCategory.IsDuplicatePOSOrder(GeneralFunctions.fnInt32(txtDisplayOrder.Text)))
                            {
                                DocMessage.MsgInformation(Properties.Resources.Duplicate_Display_Order);
                                GeneralFunctions.SetFocus(txtDisplayOrder);
                                return false;
                            }
                        }
                        else
                        {
                            if (intInitPosDisplayOrder != GeneralFunctions.fnInt32(txtDisplayOrder.Text))
                            {
                                PosDataObject.Category objCategory = new PosDataObject.Category();
                                objCategory.Connection = SystemVariables.Conn;
                                if (objCategory.IsDuplicatePOSOrder(GeneralFunctions.fnInt32(txtDisplayOrder.Text)))
                                {
                                    DocMessage.MsgInformation(Properties.Resources.Duplicate_Display_Order);
                                    GeneralFunctions.SetFocus(txtDisplayOrder);
                                    return false;
                                }
                            }
                        }*/

            if (intID != 0)
            {
                PosDataObject.Product objProd = new PosDataObject.Product();
                objProd.Connection = SystemVariables.Conn;
                int intProd = objProd.GetPOSProductsforCategory(intID);

                if (GeneralFunctions.fnInt32(txtProducts.Text) < intProd)
                {
                    DocMessage.MsgInformation(intProd.ToString() + " " + Properties.Resources.Products_for_category_entered);
                    return false;
                }
            }




            return true;
        }


        private void ShowData()
        {
            PosDataObject.Category objCategory = new PosDataObject.Category();
            objCategory.Connection = SystemVariables.Conn;
            string strColor = "";
            string strFontColor = "";
            string strItemColor = "";
            DataTable dbtbl = new DataTable();
            dbtbl = objCategory.ShowRecord(intID);
            foreach (DataRow dr in dbtbl.Rows)
            {
                txtID.Text = dr["CategoryID"].ToString();
                txtDescription.Text = dr["Description"].ToString();
                txtDisplayOrder.Text = GeneralFunctions.fnInt32(dr["POSDisplayOrder"].ToString()).ToString();
                txtProducts.Text = GeneralFunctions.fnInt32(dr["MaxItemsforPOS"].ToString()).ToString();
                intPosDisplayOrder = GeneralFunctions.fnInt32(dr["PosDisplayOrder"].ToString());
                intInitPosDisplayOrder = intPosDisplayOrder;

                //   if (dr["Enabled"].ToString() == "Y") chkActive.IsChecked = true; else chkActive.IsChecked = false;


                cmbParentCategory.EditValue = GeneralFunctions.fnInt32(dr["ParentCategory"].ToString());
                intPrevParentCategory = GeneralFunctions.fnInt32(dr["ParentCategory"].ToString());
                intPrevDisplayOrder = intPosDisplayOrder;
                txtDisplayOrder.Text = intPosDisplayOrder.ToString();
                if (dr["FoodStampEligible"].ToString() == "Y")
                {
                    chkFoodStamp.IsChecked = true;
                }
                else
                {
                    chkFoodStamp.IsChecked = false;
                }

                strColor = dr["POSScreenColor"].ToString();

                if (!((strColor == "") || (strColor == "0") || (strColor.Contains("#00000000"))))
                {
                    txtColor.Color = (Color)ColorConverter.ConvertFromString(strColor);
                }
                strFontColor = dr["POSFontColor"].ToString();
                if (!((strFontColor == "") || (strFontColor == "0") || (strFontColor.Contains("#00000000"))))
                {
                    txtFontColor.Color = (Color)ColorConverter.ConvertFromString(strFontColor);
                }

                strItemColor = dr["POSItemFontColor"].ToString();
                if (!((strItemColor == "") || (strItemColor == "0") || (strItemColor.Contains("#00000000"))))
                {
                    txtItemColor.Color = (Color)ColorConverter.ConvertFromString(strItemColor);
                }
                prevcolor = strItemColor;

                if (dr["POSBackground"].ToString() == "Skin")
                {
                    rbSkin.IsChecked = true;
                    cmbSkin.Text = dr["POSScreenStyle"].ToString();
                }
                if (dr["POSBackground"].ToString() == "Color") rbColor.IsChecked = true;



                txtFontType.Text = dr["POSFontType"].ToString();
                txtFontSize.Text = dr["POSFontSize"].ToString();

                if (dr["IsBold"].ToString() == "Y")
                {
                    chkFontBold.IsChecked = true;
                }
                else
                {
                    chkFontBold.IsChecked = false;
                }
                if (dr["IsItalics"].ToString() == "Y")
                {
                    chkFontItalics.IsChecked = true;
                }
                else
                {
                    chkFontItalics.IsChecked = false;
                }


                if (dr["AddToPOSScreen"].ToString() == "Y")
                {
                    chkPOSScreen.IsChecked = true;
                }
                else
                {
                    chkPOSScreen.IsChecked = false;
                }



                if (dr["ScaleBarCode"].ToString() == "Y")
                {
                    chkBarCode.IsChecked = true;
                }
                else
                {
                    chkBarCode.IsChecked = false;
                }

                if (dr["AllowZeroStock"].ToString() == "Y")
                {
                    chkZeroStock.IsChecked = true;
                }
                else
                {
                    chkZeroStock.IsChecked = false;
                }

                if (dr["DisplayStockinPOS"].ToString() == "Y")
                {
                    chkDisplayStock.IsChecked = true;
                }
                else
                {
                    chkDisplayStock.IsChecked = false;
                }

                if (dr["FoodStampEligibleForProduct"].ToString() == "Y")
                {
                    chkFoodStampProduct.IsChecked = true;
                }
                else
                {
                    chkFoodStampProduct.IsChecked = false;
                }

                if (dr["PrintBarCode"].ToString() == "Y")
                {
                    chkPrintLabel.IsChecked = true;
                }
                else
                {
                    chkPrintLabel.IsChecked = false;
                }

                if (dr["NoPriceOnLabel"].ToString() == "Y")
                {
                    chkNoPrice.IsChecked = true;
                }
                else
                {
                    chkNoPrice.IsChecked = false;
                }


                if (dr["ProductStatus"].ToString() == "Y")
                {
                    chkActive.IsChecked = true;
                }
                else
                {
                    chkActive.IsChecked = false;
                }
                numMinAge.Text = GeneralFunctions.fnInt32(dr["MinimumAge"].ToString()).ToString();
                numLabel.Text = GeneralFunctions.fnInt32(dr["QtyToPrint"].ToString()).ToString();

                if (dr["LabelType"].ToString() == "0")
                {
                    cmbLabelType.SelectedIndex = 0;
                }
                else if (dr["LabelType"].ToString() == "1")
                {
                    cmbLabelType.SelectedIndex = 1;
                }
                else if (dr["LabelType"].ToString() == "2")
                {
                    cmbLabelType.SelectedIndex = 2;
                }
                else if (dr["LabelType"].ToString() == "3")
                {
                    cmbLabelType.SelectedIndex = 3;
                }
                else
                {
                    cmbLabelType.SelectedIndex = -1;
                }

                if (dr["RepairPromptForTag"].ToString() == "Y") chkRepairPromptTag.IsChecked = true;
                else chkRepairPromptTag.IsChecked = false;

                if (dr["NonDiscountable"].ToString() == "Y") chkNoDiscount.IsChecked = true; else chkNoDiscount.IsChecked = false;
            }
        }

        private int GetParentCategoryLevel()
        {
            PosDataObject.Category objCategory = new PosDataObject.Category();
            objCategory.Connection = SystemVariables.Conn;
            return objCategory.GetParentCategoryLevel(cmbParentCategory.EditValue == null ? 0 : GeneralFunctions.fnInt32(cmbParentCategory.EditValue.ToString())) + 1;
        }
        private bool SaveData()
        {
            string strError = "";
            PosDataObject.Category objCategory = new PosDataObject.Category();
            objCategory.Connection = SystemVariables.Conn;
            objCategory.LoginUserID = SystemVariables.CurrentUserID;
            objCategory.GeneralCode = txtID.Text.Trim();
            objCategory.GeneralDesc = txtDescription.Text.Trim();
            objCategory.ID = intID;
            objCategory.FoodStamp = "N";
            objCategory.PosDisplayOrder = GeneralFunctions.fnInt32(txtDisplayOrder.Text);
            objCategory.MaxItemsforPOS = GeneralFunctions.fnInt32(txtProducts.Text);


            if (rbSkin.IsChecked == true)
            {
                objCategory.POSBackground = "Skin";
                objCategory.POSScreenColor = "0";
                objCategory.POSScreenStyle = cmbSkin.Text.Trim();

            }
            if (rbColor.IsChecked == true)
            {
                objCategory.POSBackground = "Color";
                objCategory.POSScreenColor = txtColor.Color.ToString();
                objCategory.POSScreenStyle = "";
            }

            objCategory.POSFontType = txtFontType.Text.Trim();
            objCategory.POSFontSize = GeneralFunctions.fnInt32(txtFontSize.Text.Trim());
            objCategory.POSFontColor = txtFontColor.Color.ToString();
            objCategory.POSItemFontColor = txtItemColor.Color.ToString();


            objCategory.ParentCategory = cmbParentCategory.EditValue == null ? 0 : GeneralFunctions.fnInt32(cmbParentCategory.EditValue.ToString());
            objCategory.ParentCategoryLevel = GetParentCategoryLevel();
            if (chkFoodStamp.IsChecked == true)
            {
                objCategory.FoodStamp = "Y";
            }
            else
            {
                objCategory.FoodStamp = "N";
            }

            if (chkFontBold.IsChecked == true)
            {
                objCategory.IsBold = "Y";
            }
            else
            {
                objCategory.IsBold = "N";
            }
            if (chkFontItalics.IsChecked == true)
            {
                objCategory.IsItalics = "Y";
            }
            else
            {
                objCategory.IsItalics = "N";
            }
            if (chkPOSScreen.IsChecked == true)
            {
                objCategory.AddToPOSScreen = "Y";
            }
            else
            {
                objCategory.AddToPOSScreen = "N";
            }



            if (chkBarCode.IsChecked == true)
            {
                objCategory.ScaleBarCode = "Y";
            }
            else
            {
                objCategory.ScaleBarCode = "N";
            }

            if (chkFoodStampProduct.IsChecked == true)
            {
                objCategory.FoodStampEligibleForProduct = "Y";
            }
            else
            {
                objCategory.FoodStampEligibleForProduct = "N";
            }

            if (chkPrintLabel.IsChecked == true)
            {
                objCategory.PrintBarCode = "Y";
            }
            else
            {
                objCategory.PrintBarCode = "N";
            }

            if (chkNoPrice.IsChecked == true)
            {
                objCategory.NoPriceOnLabel = "Y";
            }
            else
            {
                objCategory.NoPriceOnLabel = "N";
            }

            if (chkZeroStock.IsChecked == true)
            {
                objCategory.AllowZeroStock = "Y";
            }
            else
            {
                objCategory.AllowZeroStock = "N";
            }

            if (chkDisplayStock.IsChecked == true)
            {
                objCategory.DisplayStockinPOS = "Y";
            }
            else
            {
                objCategory.DisplayStockinPOS = "N";
            }

            if (chkActive.IsChecked == true)
            {
                objCategory.ProductStatus = "Y";
            }
            else
            {
                objCategory.ProductStatus = "N";
            }

            if (cmbLabelType.SelectedIndex == 0)
            {
                objCategory.LabelType = 0;
            }
            else if (cmbLabelType.SelectedIndex == 1)
            {
                objCategory.LabelType = 1;
            }
            else if (cmbLabelType.SelectedIndex == 2)
            {
                objCategory.LabelType = 2;
            }
            else if (cmbLabelType.SelectedIndex == 3)
            {
                objCategory.LabelType = 3;
            }
            else
            {
                objCategory.LabelType = -1;
            }

            if (chkNoDiscount.IsChecked == true)
            {
                objCategory.NonDiscountable = "Y";
            }
            else
            {
                objCategory.NonDiscountable = "N";
            }
            if (chkRepairPromptTag.IsChecked == true) objCategory.RepairPromptForTag = "Y"; else objCategory.RepairPromptForTag = "N";
            objCategory.MinimumAge = GeneralFunctions.fnInt32(numMinAge.Text.Trim());
            objCategory.QtyToPrint = GeneralFunctions.fnInt32(numLabel.Text.Trim());

            gridView3.PostEditor();
            objCategory.TaxDataTable = GetAttachedTax();

            strError = objCategory.PostData();
            intNewID = objCategory.ID;

            if (strError == "")
            {
                if (intID > 0)
                {
                    /*if (txtItemColor.Color.Name != prevcolor)
                    {
                        PosDataObject.Category objCategory1 = new PosDataObject.Category();
                        objCategory1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                        objCategory1.ID = intID;
                        objCategory1.POSItemFontColor = txtItemColor.Color.Name;
                        strError = objCategory1.UpdateItemFontColor();
                    }*/

                }
                blDataSaved = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        private DataTable GetAttachedTax()
        {
            DataTable dtblG = new DataTable();
            dtblG.Columns.Add("TaxID", System.Type.GetType("System.String"));

            DataTable dsource = new DataTable();
            dsource = grdTax.ItemsSource as DataTable;

            foreach (DataRow dr in dsource.Rows)
            {
                if (GeneralFunctions.fnInt32(dr["TaxID"].ToString()) > 0)
                {
                    dtblG.Rows.Add(new object[] { dr["TaxID"].ToString() });
                }
            }

            return dtblG;
        }


        private void CloseKeyboards()
        {
            if (fkybrd != null)
            {
                fkybrd.Close();
            }
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




        private void Full_GotFocus(object sender, RoutedEventArgs e)
        {
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



        private void Full_LostFocus(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
        }


        private void populateParentCategoryLookup()
        {
            PosDataObject.Category objcat = new PosDataObject.Category();
            objcat.Connection = SystemVariables.Conn;
            DataTable dbtblPCat = new DataTable();
            dbtblPCat = objcat.LookupCategory(intID, intID == 0 ? true : false);


            cmbParentCategory.ItemsSource = dbtblPCat;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();
            fkybrd = new FullKeyboard();
            populateParentCategoryLookup();
            PopulateAndSetFormSkin();
            InitialStyleLoading();
            //PopulateTax();
            PopulateTaxPopup();
            if (intID == 0)
            {
                Title.Text = Properties.Resources.Add_Category;
                txtID.IsEnabled = true;
                intPosDisplayOrder = FetchMaxPosDisPlayOrder();
                txtDisplayOrder.Text = intPosDisplayOrder.ToString();
                txtProducts.Text = "10";
                //chkPOSScreen.IsChecked = true;
                GeneralFunctions.SetFocus(txtID);
            }
            else
            {
                Title.Text = Properties.Resources.Edit_Category;
                txtID.IsEnabled = true;
                ShowData();
                GeneralFunctions.SetFocus(txtDescription);
            }

            if ((intID == 0) && (Settings.UseStyle == "NO"))
            {
                txtFontColor.Color = (Color)ColorConverter.ConvertFromString("#FEFEFEFE");
            }
            PopulateTax();



            if (rbSkin.IsChecked == true)  // Skin
            {
                lbColor.Text = Properties.Resources.Style;
                cmbSkin.Visibility = Visibility.Visible;
                txtColor.Visibility = Visibility.Collapsed;
                lbdefaultcolor.Text = Properties.Resources.Leave_blank_default_style;
            }
            else    // Absolute color
            {
                lbColor.Text = Properties.Resources.Color;
                cmbSkin.Visibility = Visibility.Collapsed;
                txtColor.Visibility = Visibility.Visible;
                lbdefaultcolor.Text = Properties.Resources.Leave_blank_default_color;
            }

            boolControlChangedItem = false;
            boolControlChanged = false;
            blLoading = true;
        }

        private void RbSkin_Checked(object sender, RoutedEventArgs e)
        {
            if (!blLoading) return;
            boolControlChanged = true;
            if (rbSkin.IsChecked == true)  // Skin
            {
                lbColor.Text = Properties.Resources.Style;
                cmbSkin.Visibility = Visibility.Visible;
                txtColor.Visibility = Visibility.Collapsed;
                lbdefaultcolor.Text = Properties.Resources.Leave_blank_default_style;
            }
            else    // Absolute color
            {
                lbColor.Text = Properties.Resources.Color;
                cmbSkin.Visibility = Visibility.Collapsed;
                txtColor.Visibility = Visibility.Visible;
                lbdefaultcolor.Text = Properties.Resources.Leave_blank_default_color;
            }
        }

        private void GrdTax_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (grdTax.CurrentColumn.Name == "colTaxID")
            {

                DevExpress.Xpf.Editors.LookUpEditBase editor = grdTax.View.ActiveEditor as DevExpress.Xpf.Editors.LookUpEditBase;

                if (editor != null)
                {

                    editor.IsPopupOpen = true;
                    editor.ShowPopup();
                    e.Handled = true;
                }
            }
        }

        private void PART_Editor_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.LookUpEditBase editor = sender as DevExpress.Xpf.Editors.LookUpEditBase;
            if (editor != null)
            {

                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidAll())
            {
                if (SaveData())
                {
                    if (intID > 0)
                    {
                        if (boolControlChangedItem)
                        {
                            if (ItemCount() > 0)
                            {
                                if (DocMessage.MsgConfirmation(Properties.Resources.apply_settings_change_to_all_products + " " + Properties.Resources.currently_attached_Category) == MessageBoxResult.Yes)
                                {
                                    PosDataObject.Category objCategory1 = new PosDataObject.Category();
                                    objCategory1.Connection = SystemVariables.Conn;
                                    objCategory1.LoginUserID = SystemVariables.CurrentUserID;
                                    int p = objCategory1.UpdateProductOnCategorySettingChange(intID);
                                }
                            }
                            boolControlChangedItem = false;
                        }
                    }
                    boolControlChanged = false;
                    if (!blOnScreenCall) BrowseForm.FetchData();
                    CloseKeyboards();
                    Close();
                }
            }
        }

        private int ItemCount()
        {
            PosDataObject.Category objCategory = new PosDataObject.Category();
            objCategory.Connection = SystemVariables.Conn;
            return objCategory.GetItemCount(intID);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            boolControlChanged = false;
            boolControlChangedItem = false;
            CloseKeyboards();
            Close();
        }

        private void TxtFontColor_GetColorName(object sender, DevExpress.Xpf.Editors.GetColorNameEventArgs e)
        {
            txtFontColor.Text = e.ColorName;
        }

        private void GridView3_CellValueChanging(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colTaxID")
            {
                if (IsDuplicateTax(GeneralFunctions.fnInt32(e.Value)))
                {
                    DocMessage.MsgInformation(Properties.Resources.Tax_already_entered);

                    Dispatcher.BeginInvoke(new Action(() => gridView3.HideEditor()));
                    e.Handled = true;
                }
                else
                {
                    PosDataObject.Tax objGroup = new PosDataObject.Tax();
                    objGroup.Connection = SystemVariables.Conn;
                    grdTax.SetCellValue(e.RowHandle, colTaxName, objGroup.GetTaxDesc(GeneralFunctions.fnInt32(e.Value)));

                    boolControlChanged = true;
                    boolControlChangedItem = true;
                }


            }
        }

        private bool IsDuplicateTax(int refTax)
        {
            bool bDuplicate = false;
            DataTable dsource = grdTax.ItemsSource as DataTable;
            int intc = 0;
            foreach (DataRow dr in dsource.Rows)
            {
                if (GeneralFunctions.fnInt32(dr["TaxID"].ToString()) == 0) continue;
                if (GeneralFunctions.fnInt32(dr["TaxID"].ToString()) == refTax)
                {
                    bDuplicate = true;
                    break;
                }
            }
            return bDuplicate;
        }

        private void GridView3_CellValueChanged(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {

        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            int TaxID = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(gridView3.FocusedRowHandle, grdTax, colTaxID));
            if (TaxID > 0)
            {
                if (DocMessage.MsgDelete())
                {
                    gridView3.DeleteRow(gridView3.FocusedRowHandle);
                    boolControlChangedItem = true;
                    boolControlChanged = true;
                }
            }
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
                            if (intID > 0)
                            {
                                if (boolControlChangedItem)
                                {
                                    if (ItemCount() > 0)
                                    {
                                        if (DocMessage.MsgConfirmation(Properties.Resources.apply_settings_change_to_all_products + " " + Properties.Resources.currently_attached_Category) == MessageBoxResult.Yes)
                                        {
                                            PosDataObject.Category objCategory1 = new PosDataObject.Category();
                                            objCategory1.Connection = new SqlConnection(SystemVariables.ConnectionString);
                                            objCategory1.LoginUserID = SystemVariables.CurrentUserID;
                                            int p = objCategory1.UpdateProductOnCategorySettingChange(intID);
                                        }
                                    }
                                    boolControlChangedItem = false;
                                }
                            }
                            boolControlChanged = false;
                            if (!blOnScreenCall) BrowseForm.FetchData();
                        }
                        else
                            e.Cancel = true;
                    }
                    else
                        e.Cancel = true;
                }

                if (DlgResult == MessageBoxResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void TxtID_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void ChkActive_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
            boolControlChangedItem = true;
        }

        private void CmbLabelType_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
            boolControlChangedItem = true;
        }

        private void NumLabel_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
            boolControlChangedItem = true;
        }

        private void NumMinAge_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
            boolControlChangedItem = true;
        }

        private void ChkFontBold_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void ChkFontItalics_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void CmbSkin_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.ComboBoxEdit editor = sender as DevExpress.Xpf.Editors.ComboBoxEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void TxtColor_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.PopupColorEdit editor = sender as DevExpress.Xpf.Editors.PopupColorEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void TxtFontType_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.FontEdit editor = sender as DevExpress.Xpf.Editors.FontEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void Lbdefaultcolor_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            txtColor.Clear();
        }

        private void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            txtFontColor.Clear();
        }

        private void TextBlock_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            txtItemColor.Clear();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChkPOSScreen_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void CmbLabelType_EditValueChanged_1(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {

        }
    }
}
