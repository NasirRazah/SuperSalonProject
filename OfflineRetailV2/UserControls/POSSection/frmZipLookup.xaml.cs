using OfflineRetailV2.Data;
using System;
using System.Collections;
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
    /// Interaction logic for frmZipLookup.xaml
    /// </summary>
    public partial class frmZipLookup : Window
    {
        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;
        public frmZipLookup()
        {
            InitializeComponent();

            ModalWindow.CloseCommand = new CommandBase(OnCloseCommand);
        }

        private void OnCloseCommand(object obj)
        {

            DialogResult = false;
            CloseKeyboards();
            Close();
        }

        public async Task<int> GetID()
        {
            int intRowID = 0;
            if ((gridControl1.ItemsSource as ICollection).Count == 0) return 0;
            intRowID = gridView1.FocusedRowHandle;
            if (intRowID < 0) return 0;
            return GeneralFunctions.fnInt32((await GeneralFunctions.GetCellValue1(intRowID, gridControl1, colID)));
        }

        public async Task<string> GetColoum1Value()
        {
            int intRowID = 0;
            if ((gridControl1.ItemsSource as DataTable).Rows.Count == 0) return "";
            intRowID = gridView1.FocusedRowHandle;
            if (intRowID < 0) return "";
            return ((await GeneralFunctions.GetCellValue1(intRowID, gridControl1, col1)).Trim());
        }
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            CloseKeyboards();
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            CloseKeyboards();
            Close();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text.Trim() != "")
            {
                FindFilterData();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fkybrd = new FullKeyboard();
            Title.Text =Properties.Resources.Zip_Codes;
            GeneralFunctions.SetFocus(txtSearch);
        }

        private void FindFilterData()
        {

            PosDataObject.Zip objLookupData = new PosDataObject.Zip();
            objLookupData.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            dbtbl = objLookupData.FetchSearchData(txtSearch.Text.Trim());
            gridControl1.ItemsSource = dbtbl;
            dbtbl.Dispose();
        }

        private void CloseKeyboards()
        {
            if (fkybrd != null)
            {
                fkybrd.Close();
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
    }
}
