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
using System.Data;
using System.Data.SqlClient;



namespace OfflineRetailV2.UserControls.Administrator
{
    /// <summary>
    /// Interaction logic for frm_DummyBrwUC.xaml
    /// </summary>
    public partial class frm_SMDiscountBrwUC : UserControl
    {
        public frm_SMDiscountBrwUC()
        {
            InitializeComponent();
        }

        private frm_MainAdmin fParent;

        public frm_MainAdmin ParentForm
        {
            get { return fParent; }
            set { fParent = value; }
        }

        
        public void LoadSubmenu()
        {
            pnlBody.Children.Clear();
            foreach (DataRow dr in SystemVariables.Menu.Rows)
            {
                if (dr["MainMenu"].ToString() != "Discounts") continue;
                if (dr["Visible"].ToString() == "N") continue;

                System.Windows.Controls.Button btn = new System.Windows.Controls.Button();
                btn.Tag = dr["SubMenu"].ToString();
                TextBlock tb = new TextBlock();
                tb.Text = dr["SubMenu"].ToString();
                tb.TextWrapping = TextWrapping.WrapWithOverflow;
                tb.TextAlignment = TextAlignment.Center;
                tb.FontFamily = this.FindResource("OSSemiBold") as FontFamily;
                tb.Foreground = Brushes.White;
                btn.Width = 130;
                btn.Height = 75;
                btn.Margin = new Thickness(5);
                btn.BorderThickness = new Thickness(3);
                btn.Style = this.FindResource("SubmenuButton") as Style;

                btn.Content = tb;
                btn.PreviewMouseLeftButtonDown += Btn_PreviewMouseLeftButtonDown;
                pnlBody.Children.Add(btn);
            }
        }

        private void Btn_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            fParent.ExecuteClickFromSubMenu((sender as System.Windows.Controls.Button).Tag.ToString());
        }

        
    }
}
