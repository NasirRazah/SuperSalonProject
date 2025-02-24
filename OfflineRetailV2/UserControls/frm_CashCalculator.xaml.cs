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
using DevExpress.Xpf.Editors;
using System.Data;

namespace OfflineRetailV2.UserControls
{
    /// <summary>
    /// Interaction logic for frm_CashCalculator.xaml
    /// </summary>
    public partial class frm_CashCalculator : Window
    {
        NumKeyboard nkybrd;
        bool IsAboutNumKybrdOpen = false;
        public frm_CashCalculator()
        {
            InitializeComponent();
            Loaded += Frm_Loaded;
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }
        private void OnCloseCOmmand(object obj)
        {
            ResMan.closeKeyboard();
            CloseKeyboards();
            Close();
        }
        private double dblTotal;
        private DataTable dtblCurrencyCount;

        public DataTable CurrencyCount
        {
            get { return dtblCurrencyCount; }
            set { dtblCurrencyCount = value; }

        }

        public double Total
        {
            get { return dblTotal; }
            set { dblTotal = value; }
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

        private void NKybrd_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if ((!IsAboutNumKybrdOpen) && (Settings.UseTouchKeyboardInAdmin == "Y"))
            {
                Dispatcher.BeginInvoke(new Action(() => (sender as DevExpress.Xpf.Editors.TextEdit).SelectAll()));
                //nkybrd = new NumKeyboard();

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
                //Dispatcher.BeginInvoke(new Action(() => nkybrd.Show()));
                Dispatcher.BeginInvoke(new Action(() => nkybrd.Visibility = Visibility.Visible));
                IsAboutNumKybrdOpen = true;
            }
        }

        private void Num_GotFocus(object sender, RoutedEventArgs e)
        {

            if (Settings.UseTouchKeyboardInAdmin == "N") return;
            CloseKeyboards();
            if ((sender as DevExpress.Xpf.Editors.TextEdit).Text == "")
            {
                (sender as DevExpress.Xpf.Editors.TextEdit).Text = "0";
            }
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


        private void Frm_Loaded(object sender, RoutedEventArgs e)
        {
            nkybrd = new NumKeyboard();
            Title.Text = Properties.Resources.Currency_Calculator;
            ArrangeCurrencyDenominations();

            foreach (DataRow dr in dtblCurrencyCount.Rows)
            {
                foreach (DataColumn dc in dr.Table.Columns)
                {
                    if (dc.ColumnName == "Penny")
                    {
                        foreach (UIElement ctrl in pnlDynamic.Children)
                        {
                            if (ctrl is TextEdit)
                            {
                                if (!(ctrl as TextEdit).Name.StartsWith("input_")) continue;
                                if ((ctrl as TextEdit).Tag.ToString() == "0.01")
                                {
                                    (ctrl as TextEdit).Text = dr[dc].ToString();
                                    break;
                                }
                            }
                        }
                    }

                    if (dc.ColumnName == "TwoPenny")
                    {
                        foreach (UIElement ctrl in pnlDynamic.Children)
                        {
                            if (ctrl is TextEdit)
                            {
                                if (!(ctrl as TextEdit).Name.StartsWith("input_")) continue;
                                if ((ctrl as TextEdit).Tag.ToString() == "0.02")
                                {
                                    (ctrl as TextEdit).Text = dr[dc].ToString();
                                    break;
                                }
                            }
                        }
                    }

                    if (dc.ColumnName == "Nickel")
                    {
                        foreach (UIElement ctrl in pnlDynamic.Children)
                        {
                            if (ctrl is TextEdit)
                            {
                                if (!(ctrl as TextEdit).Name.StartsWith("input_")) continue;
                                if ((ctrl as TextEdit).Tag.ToString() == "0.05")
                                {
                                    (ctrl as TextEdit).Text = dr[dc].ToString();
                                    break;
                                }
                            }
                        }
                    }

                    if (dc.ColumnName == "Dime")
                    {
                        foreach (UIElement ctrl in pnlDynamic.Children)
                        {
                            if (ctrl is TextEdit)
                            {
                                if (!(ctrl as TextEdit).Name.StartsWith("input_")) continue;
                                if ((ctrl as TextEdit).Tag.ToString() == "0.1")
                                {
                                    (ctrl as TextEdit).Text = dr[dc].ToString();
                                    break;
                                }
                            }
                        }
                    }

                    if (dc.ColumnName == "TwentyPenny")
                    {
                        foreach (UIElement ctrl in pnlDynamic.Children)
                        {
                            if (ctrl is TextEdit)
                            {
                                if (!(ctrl as TextEdit).Name.StartsWith("input_")) continue;
                                if ((ctrl as TextEdit).Tag.ToString() == "0.2")
                                {
                                    (ctrl as TextEdit).Text = dr[dc].ToString();
                                    break;
                                }
                            }
                        }
                    }

                    if (dc.ColumnName == "Quarter")
                    {
                        foreach (UIElement ctrl in pnlDynamic.Children)
                        {
                            if (ctrl is TextEdit)
                            {
                                if (!(ctrl as TextEdit).Name.StartsWith("input_")) continue;
                                if ((ctrl as TextEdit).Tag.ToString() == "0.25")
                                {
                                    (ctrl as TextEdit).Text = dr[dc].ToString();
                                    break;
                                }
                            }
                        }
                    }


                    if (dc.ColumnName == "Halve")
                    {
                        foreach (UIElement ctrl in pnlDynamic.Children)
                        {
                            if (ctrl is TextEdit)
                            {
                                if (!(ctrl as TextEdit).Name.StartsWith("input_")) continue;
                                if ((ctrl as TextEdit).Tag.ToString() == "0.5")
                                {
                                    (ctrl as TextEdit).Text = dr[dc].ToString();
                                    break;
                                }
                            }
                        }
                    }

                    if (dc.ColumnName == "One")
                    {
                        foreach (UIElement ctrl in pnlDynamic.Children)
                        {
                            if (ctrl is TextEdit)
                            {
                                if (!(ctrl as TextEdit).Name.StartsWith("input_")) continue;
                                if ((ctrl as TextEdit).Tag.ToString() == "1")
                                {
                                    (ctrl as TextEdit).Text = dr[dc].ToString();
                                    break;
                                }
                            }
                        }
                    }

                    if (dc.ColumnName == "Two")
                    {
                        foreach (UIElement ctrl in pnlDynamic.Children)
                        {
                            if (ctrl is TextEdit)
                            {
                                if (!(ctrl as TextEdit).Name.StartsWith("input_")) continue;
                                if ((ctrl as TextEdit).Tag.ToString() == "2")
                                {
                                    (ctrl as TextEdit).Text = dr[dc].ToString();
                                    break;
                                }
                            }
                        }
                    }

                    if (dc.ColumnName == "Five")
                    {
                        foreach (UIElement ctrl in pnlDynamic.Children)
                        {
                            if (ctrl is TextEdit)
                            {
                                if (!(ctrl as TextEdit).Name.StartsWith("input_")) continue;
                                if ((ctrl as TextEdit).Tag.ToString() == "5")
                                {
                                    (ctrl as TextEdit).Text = dr[dc].ToString();
                                    break;
                                }
                            }
                        }
                    }

                    if (dc.ColumnName == "Ten")
                    {
                        foreach (UIElement ctrl in pnlDynamic.Children)
                        {
                            if (ctrl is TextEdit)
                            {
                                if (!(ctrl as TextEdit).Name.StartsWith("input_")) continue;
                                if ((ctrl as TextEdit).Tag.ToString() == "10")
                                {
                                    (ctrl as TextEdit).Text = dr[dc].ToString();
                                    break;
                                }
                            }
                        }
                    }

                    if (dc.ColumnName == "Twenty")
                    {
                        foreach (UIElement ctrl in pnlDynamic.Children)
                        {
                            if (ctrl is TextEdit)
                            {
                                if (!(ctrl as TextEdit).Name.StartsWith("input_")) continue;
                                if ((ctrl as TextEdit).Tag.ToString() == "20")
                                {
                                    (ctrl as TextEdit).Text = dr[dc].ToString();
                                    break;
                                }
                            }
                        }
                    }

                    if (dc.ColumnName == "Fifty")
                    {
                        foreach (UIElement ctrl in pnlDynamic.Children)
                        {
                            if (ctrl is TextEdit)
                            {
                                if (!(ctrl as TextEdit).Name.StartsWith("input_")) continue;
                                if ((ctrl as TextEdit).Tag.ToString() == "50")
                                {
                                    (ctrl as TextEdit).Text = dr[dc].ToString();
                                    break;
                                }
                            }
                        }
                    }

                    if (dc.ColumnName == "Hundred")
                    {
                        foreach (UIElement ctrl in pnlDynamic.Children)
                        {
                            if (ctrl is TextEdit)
                            {
                                if (!(ctrl as TextEdit).Name.StartsWith("input_")) continue;
                                if ((ctrl as TextEdit).Tag.ToString() == "100")
                                {
                                    (ctrl as TextEdit).Text = dr[dc].ToString();
                                    break;
                                }
                            }
                        }
                    }

                    if (dc.ColumnName == "TwoHundred")
                    {
                        foreach (UIElement ctrl in pnlDynamic.Children)
                        {
                            if (ctrl is TextEdit)
                            {
                                if (!(ctrl as TextEdit).Name.StartsWith("input_")) continue;
                                if ((ctrl as TextEdit).Tag.ToString() == "200")
                                {
                                    (ctrl as TextEdit).Text = dr[dc].ToString();
                                    break;
                                }
                            }
                        }
                    }

                    if (dc.ColumnName == "FiveHundred")
                    {
                        foreach (UIElement ctrl in pnlDynamic.Children)
                        {
                            if (ctrl is TextEdit)
                            {
                                if (!(ctrl as TextEdit).Name.StartsWith("input_")) continue;
                                if ((ctrl as TextEdit).Tag.ToString() == "500")
                                {
                                    (ctrl as TextEdit).Text = dr[dc].ToString();
                                    break;
                                }
                            }
                        }
                    }

                    if (dc.ColumnName == "OneThousand")
                    {
                        foreach (UIElement ctrl in pnlDynamic.Children)
                        {
                            if (ctrl is TextEdit)
                            {
                                if (!(ctrl as TextEdit).Name.StartsWith("input_")) continue;
                                if ((ctrl as TextEdit).Tag.ToString() == "1000")
                                {
                                    (ctrl as TextEdit).Text = dr[dc].ToString();
                                    break;
                                }
                            }
                        }
                    }


                }
            }
        }

        private void ArrangeCurrencyDenominations()
        {
            DataTable data = GeneralFunctions.GetCurrencyDenominations();
            
            data.DefaultView.Sort = "CoinOrCurrency,DisplayOrder";
            data.DefaultView.ApplyDefaultSort = true;


            TextBlock label;
            TextBlock equal;
            TextEdit edit;
            TextEdit readonlyedit;

            int i = 0;
            int j = 0;
            bool blcurrency = false;
            foreach (DataRowView dv in data.DefaultView)
            {

                if (!blcurrency)
                {
                    if (GeneralFunctions.fnInt32(dv["CoinOrCurrency"].ToString()) == 2)
                    {
                        j = j + 2;
                        blcurrency = true;
                    }
                }

                label = new TextBlock();
                label.Text = dv["CurrencyName"].ToString();
                label.SetValue(Grid.RowProperty, j);
                label.SetValue(Grid.ColumnProperty, 0);
                pnlDynamic.Children.Add(label);
                

                edit = new TextEdit();
                edit.Name = "input_" + i.ToString();
                edit.Mask = "N0";
                edit.MaskType = MaskType.Numeric;
                edit.MaskUseAsDisplayFormat = true;
                edit.HorizontalContentAlignment = HorizontalAlignment.Right;

                edit.SetValue(Grid.RowProperty, j);
                edit.SetValue(Grid.ColumnProperty, 2);
                edit.Tag = dv["CurrencyValue"].ToString();
                edit.IsTabStop = true;
                edit.GotFocus += Num_GotFocus;
                edit.LostFocus += Num_LostFocus;
                edit.PreviewMouseLeftButtonDown += NKybrd_PreviewMouseLeftButtonDown;
                edit.EditValueChanging += Input_EditValueChanging;
                pnlDynamic.Children.Add(edit);

               

                equal = new TextBlock();
                equal.Text = "=";
                equal.SetValue(Grid.RowProperty, j);
                equal.SetValue(Grid.ColumnProperty, 3);
                equal.TextAlignment = TextAlignment.Center;
                pnlDynamic.Children.Add(equal);


                readonlyedit = new TextEdit();
                readonlyedit.Name = "tot_" + i.ToString();

                readonlyedit.Mask = "N2";
                readonlyedit.MaskType = MaskType.Numeric;
                readonlyedit.MaskUseAsDisplayFormat = true;
                readonlyedit.HorizontalContentAlignment = HorizontalAlignment.Right;

                readonlyedit.SetValue(Grid.RowProperty, j);
                readonlyedit.SetValue(Grid.ColumnProperty, 4);
                readonlyedit.IsTabStop = false;
                readonlyedit.IsReadOnly = true;
                pnlDynamic.Children.Add(readonlyedit);

                i++;
                j = j + 2;
            }



        }

        private void CalculateTotalAmount()
        {
            double dblTotal = 0;
            foreach (UIElement ctrl in pnlDynamic.Children)
            {
                if (ctrl is TextEdit)
                {
                    if ((ctrl as TextEdit).Name.StartsWith("tot_"))
                    {
                        dblTotal = dblTotal + GeneralFunctions.fnDouble((ctrl as TextEdit).Text);
                    }
                }
            }

            valTotal.Text = dblTotal.ToString("f2");


            if (GeneralFunctions.fnDouble(valTotal.Text) == 0) valTotal.Text = "";
        }

        private void FullKbd_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
        }
        private bool blFinalFlag;
        private string strPaidOutDesc;
        private double dblPaidOutAmount;
        private int intTranNo;
        private int intInvNo;
        private bool blFunctionBtnAccess;
        private int intSuperUserID;

        public bool FinalFlag
        {
            get { return blFinalFlag; }
            set { blFinalFlag = value; }
        }

        public string PaidOutDesc
        {
            get { return strPaidOutDesc; }
            set { strPaidOutDesc = value; }
        }

        public double PaidOutAmount
        {
            get { return dblPaidOutAmount; }
            set { dblPaidOutAmount = value; }
        }

        public int TranNo
        {
            get { return intTranNo; }
            set { intTranNo = value; }
        }

        public int InvNo
        {
            get { return intInvNo; }
            set { intInvNo = value; }
        }

        public int SuperUserID
        {
            get { return intSuperUserID; }
            set { intSuperUserID = value; }
        }

        public bool FunctionBtnAccess
        {
            get { return blFunctionBtnAccess; }
            set { blFunctionBtnAccess = value; }
        }

        

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            dtblCurrencyCount.Rows.Clear();

            string coin_1 = "";
            string coin_2 = "";
            string coin_3 = "";
            string coin_4 = "";
            string coin_5 = "";
            string coin_6 = "";
            string coin_7 = "";

            string currency_1 = "";
            string currency_2 = "";
            string currency_3 = "";
            string currency_4 = "";
            string currency_5 = "";
            string currency_6 = "";
            string currency_7 = "";
            string currency_8 = "";
            string currency_9 = "";
            string currency_10 = "";

            foreach (UIElement ctrl in pnlDynamic.Children)
            {
                if (ctrl is TextEdit)
                {
                    if (!(ctrl as TextEdit).Name.StartsWith("input_")) continue;
                    if ((ctrl as TextEdit).Tag.ToString() == "0.01") coin_1 = (ctrl as TextEdit).Text;
                    if ((ctrl as TextEdit).Tag.ToString() == "0.02") coin_2 = (ctrl as TextEdit).Text;
                    if ((ctrl as TextEdit).Tag.ToString() == "0.05") coin_3 = (ctrl as TextEdit).Text;
                    if ((ctrl as TextEdit).Tag.ToString() == "0.1") coin_4 = (ctrl as TextEdit).Text;
                    if ((ctrl as TextEdit).Tag.ToString() == "0.2") coin_5 = (ctrl as TextEdit).Text;
                    if ((ctrl as TextEdit).Tag.ToString() == "0.25") coin_6 = (ctrl as TextEdit).Text;
                    if ((ctrl as TextEdit).Tag.ToString() == "0.5") coin_7 = (ctrl as TextEdit).Text;

                    if ((ctrl as TextEdit).Tag.ToString() == "1") currency_1 = (ctrl as TextEdit).Text;
                    if ((ctrl as TextEdit).Tag.ToString() == "2") currency_2 = (ctrl as TextEdit).Text;
                    if ((ctrl as TextEdit).Tag.ToString() == "5") currency_3 = (ctrl as TextEdit).Text;
                    if ((ctrl as TextEdit).Tag.ToString() == "10") currency_4 = (ctrl as TextEdit).Text;
                    if ((ctrl as TextEdit).Tag.ToString() == "20") currency_5 = (ctrl as TextEdit).Text;
                    if ((ctrl as TextEdit).Tag.ToString() == "50") currency_6 = (ctrl as TextEdit).Text;
                    if ((ctrl as TextEdit).Tag.ToString() == "100") currency_7 = (ctrl as TextEdit).Text;
                    if ((ctrl as TextEdit).Tag.ToString() == "200") currency_8 = (ctrl as TextEdit).Text;
                    if ((ctrl as TextEdit).Tag.ToString() == "500") currency_9 = (ctrl as TextEdit).Text;
                    if ((ctrl as TextEdit).Tag.ToString() == "1000") currency_10 = (ctrl as TextEdit).Text;
                }
            }



            dtblCurrencyCount.Rows.Add(new object[] { coin_1, coin_2, coin_3, coin_4, coin_5, coin_6, coin_7,
            currency_1,currency_2,currency_3,currency_4,currency_5,currency_6,currency_7,currency_8,currency_9,currency_10 });

            dblTotal = GeneralFunctions.fnDouble(valTotal.Text);
            ResMan.closeKeyboard();
            //CloseKeyboards();
            DialogResult = true;
        }

        private void Input_EditValueChanging(object sender, EditValueChangingEventArgs e)
        {
            TextEdit totCtrl = new TextEdit();

            foreach (UIElement ctrl in pnlDynamic.Children)
            {
                if (ctrl is TextEdit)
                {
                    if ((ctrl as TextEdit).Name == "tot_" + (sender as TextEdit).Name.Substring(6))
                    {
                        totCtrl = ctrl as TextEdit;
                        break;
                    }
                }
            }

            if (e.NewValue.ToString() == "")
            {
                totCtrl.Text = "";
                CalculateTotalAmount();
            }
            else
            {
                totCtrl.Text = (GeneralFunctions.fnInt32(e.NewValue) * GeneralFunctions.fnDouble((sender as TextEdit).Tag)).ToString("f2");
                CalculateTotalAmount();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ResMan.closeKeyboard();
            //CloseKeyboards();
            DialogResult = false;
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            foreach (UIElement ctrl in pnlDynamic.Children)
            {
                if (ctrl is TextEdit)
                {
                    if ((ctrl as TextEdit).Name.StartsWith("input_"))
                    {
                        (ctrl as TextEdit).Text = "";
                    }
                }
            }
        }

        private void BtnKybrd_Click(object sender, RoutedEventArgs e)
        {
            ResMan.ShowFullKeyboard();
        }

        private void ScrlViewer_ManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }
    }
}
