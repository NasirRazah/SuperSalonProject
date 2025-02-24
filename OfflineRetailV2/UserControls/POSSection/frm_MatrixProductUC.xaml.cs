using DevExpress.Xpf.Editors;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using OfflineRetailV2.Data;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_MatrixProductUC.xaml
    /// </summary>
    public partial class frm_MatrixProductUC : UserControl
    {
        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;
        private frm_MatrixProduct pform;

        public frm_MatrixProduct ParentForm
        {
            get { return pform; }
            set { pform = value; }
        }

        public frm_MatrixProductUC()
        {
            InitializeComponent();
            nkybrd = new NumKeyboard();
        }

        private void TcMatrix_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void TxtOption1_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        public void CloseKeyboards()
        {
           
            if (nkybrd != null)
            {
                nkybrd.Close();
            }
        }

        private void GridView4_ShownEditor(object sender, DevExpress.Xpf.Grid.EditorEventArgs e)
        {
            if (e.Column.FieldName == "QtyonHand")
            {
                TextEdit editor = (TextEdit)e.Editor;
                if (Settings.UseTouchKeyboardInAdmin == "N") return;

                CloseKeyboards();

                if (editor.Text == "")
                {
                    editor.Text = "0.00";
                }
                Dispatcher.BeginInvoke(new Action(() => editor.SelectAll()));


                if (!IsAboutNumKybrdOpen)
                {
                    nkybrd = new NumKeyboard();
                    nkybrd.CallFromGrid = true;
                    nkybrd.GridInputControl = editor;
                    nkybrd.WindowName = "Matrix";
                    nkybrd.GridColumnName = "QtyonHand";
                    nkybrd.GridDecimal = 2;
                    nkybrd.GridRowIndex = gridView4.FocusedRowHandle;
                    var location = editor.PointToScreen(new Point(0, 0)); nkybrd.Left = location.X + 385 <= System.Windows.SystemParameters.WorkArea.Width ? location.X : location.X - (location.X + 385 - System.Windows.SystemParameters.WorkArea.Width);
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
                    nkybrd.CalledForm = pform;
                    nkybrd.Closing += new System.ComponentModel.CancelEventHandler(NKybrd_Closing);
                    nkybrd.Show();
                    IsAboutNumKybrdOpen = true;
                }
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
        public void UpdateGridValueByOnscreenKeyboard(string strgridname, string gridcol, int rindx, string val)
        {
            if (gridcol == "QtyonHand") grdQty.SetCellValue(rindx, colQtyonHand, val);

        }


    }
}
