using OfflineRetailV2.Data;
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

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_POSUsePriceLevelDlg.xaml
    /// </summary>
    public partial class frm_POSUsePriceLevelDlg : Window
    {
        public frm_POSUsePriceLevelDlg()
        {
            InitializeComponent();

            Loaded += Frm_POSUsePriceLevelDlg_Loaded;
            ModalWindow.CloseCommand = new CommandBase(OnClose);
        }

        private void OnClose(object obj)
        {
            DialogResult = false;
            Close();
        }

        private void Frm_POSUsePriceLevelDlg_Loaded(object sender, RoutedEventArgs e)
        {
            Title.Text =Properties.Resources.Select_Price_Level;
            blchange = false;
            if ((Settings.PriceLevelForOneTime == "N") && (Settings.PriceLevelForThisSale == "N"))
            {
                switch (Settings.UsePriceLevel)
                {
                    case 0:
                        rg1.IsChecked = true;
                        break;
                    case 1:
                        rg2.IsChecked = true;
                        break;
                    case 2:
                        rg3.IsChecked = true;
                        break;
                    case 3:
                        rg4.IsChecked = true;
                        break;
                }
                cmbScope1.IsChecked = true;
            }
            if ((Settings.PriceLevelForOneTime == "N") && (Settings.PriceLevelForThisSale == "Y"))
            {
                switch (Settings.TempPriceLevel)
                {
                    case 0:
                        rg1.IsChecked = true;
                        break;
                    case 1:
                        rg2.IsChecked = true;
                        break;
                    case 2:
                        rg3.IsChecked = true;
                        break;
                    case 3:
                        rg4.IsChecked = true;
                        break;
                }
                cmbScope2.IsChecked = true;
            }
            if ((Settings.PriceLevelForOneTime == "Y") && (Settings.PriceLevelForThisSale == "N"))
            {
                switch (Settings.TempPriceLevel)
                {
                    case 0:
                        rg1.IsChecked = true;
                        break;
                    case 1:
                        rg2.IsChecked = true;
                        break;
                    case 2:
                        rg3.IsChecked = true;
                        break;
                    case 3:
                        rg4.IsChecked = true;
                        break;
                }
                cmbScope3.IsChecked = true;
            }
        }

        private bool blFunctionBtnAccess;
        private int intSuperUserID;
        private bool blchange;

        public int SuperUserID
        {
            get { return intSuperUserID; }
            set { intSuperUserID = value; }
        }
        public bool ChangeFlag
        {
            get { return blchange; }
            set { blchange = value; }
        }
        public bool FunctionBtnAccess
        {
            get { return blFunctionBtnAccess; }
            set { blFunctionBtnAccess = value; }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (blchange)
            {
                if (cmbScope1.IsChecked==true)
                {
                    PosDataObject.Setup objSetup = new PosDataObject.Setup();
                    objSetup.Connection = SystemVariables.Conn;
                    if (rg1.IsChecked == true)
                        objSetup.UsePriceLevel = 0;
                    else if (rg2.IsChecked == true)
                        objSetup.UsePriceLevel = 1;
                    else if (rg3.IsChecked == true)
                        objSetup.UsePriceLevel = 2;
                    else if (rg4.IsChecked == true)
                        objSetup.UsePriceLevel = 3;
                    objSetup.LoginUserID = SystemVariables.CurrentUserID;
                    objSetup.ChangedByAdmin = intSuperUserID;
                    objSetup.FunctionButtonAccess = blFunctionBtnAccess;
                    string strErr = objSetup.UpdateUsePriceLevel();
                    if (strErr == "")
                    {
                        Settings.PriceLevelForOneTime = "N";
                        Settings.PriceLevelForThisSale = "N";
                        Settings.TempPriceLevel = 0;
                        Settings.LoadSettingsVariables();
                        Settings.LoadScaleGraduation();
                        Settings.LoadCentralStoreInfo();
                        DialogResult = true;
                    }
                }
                if (cmbScope2.IsChecked==true)
                {
                    Settings.PriceLevelForOneTime = "N";
                    Settings.PriceLevelForThisSale = "Y";
                    if (rg1.IsChecked == true)
                        Settings.TempPriceLevel = 0;
                    else if (rg2.IsChecked == true)
                        Settings.TempPriceLevel = 1;
                    else if (rg3.IsChecked == true)
                        Settings.TempPriceLevel = 2;
                    else if (rg4.IsChecked == true)
                        Settings.TempPriceLevel = 3;
                    DialogResult = true;
                }
                if (cmbScope3.IsChecked==true)
                {
                    Settings.PriceLevelForOneTime = "Y";
                    Settings.PriceLevelForThisSale = "N";
                    if (rg1.IsChecked == true)
                        Settings.TempPriceLevel = 0;
                    else if (rg2.IsChecked == true)
                        Settings.TempPriceLevel = 1;
                    else if (rg3.IsChecked == true)
                        Settings.TempPriceLevel = 2;
                    else if (rg4.IsChecked == true)
                        Settings.TempPriceLevel = 3;
                    DialogResult = true;
                }

            }
            else
                DialogResult = true;
        }

        private void rg1_Checked(object sender, RoutedEventArgs e)
        {
            blchange = true;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
