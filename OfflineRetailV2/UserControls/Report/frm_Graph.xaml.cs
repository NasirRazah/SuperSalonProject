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
using DevExpress.XtraReports.UI;
using DevExpress.Xpf.Charts;

namespace OfflineRetailV2.UserControls.Report
{
    /// <summary>
    /// Interaction logic for frm_Graph.xaml
    /// </summary>
    public partial class frm_Graph : Window
    {
        public frm_Graph()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommandExecute);
        }

        private void OnCloseCommandExecute(object obj)
        {
            Close();
        }

        private string strGraphFor;
        private string strGraphParameter1;
        private string strGraphParameter2;
        private string strGraphParameter3;
        private DataTable dtblChartData;

        public DataTable ChartData
        {
            get { return dtblChartData; }
            set { dtblChartData = value; }
        }

        public string GraphFor
        {
            get { return strGraphFor; }
            set { strGraphFor = value; }
        }

        public string GraphParameter1
        {
            get { return strGraphParameter1; }
            set { strGraphParameter1 = value; }
        }

        public string GraphParameter2
        {
            get { return strGraphParameter2; }
            set { strGraphParameter2 = value; }
        }

        public string GraphParameter3
        {
            get { return strGraphParameter3; }
            set { strGraphParameter3 = value; }
        }

        private void SetChartTitle()
        {
            if ((cmbView.SelectedIndex == 0) || (cmbView.SelectedIndex == 2))
            {
                if (strGraphFor == "Sales By Period")
                {
                    if (strGraphParameter1 == "Hourly")
                    {
                        cc.Titles[0].Content = "Hourly Sales";
                    }
                    if (strGraphParameter1 == "Daily")
                    {
                        cc.Titles[0].Content = "Daily Sales";
                    }
                    if (strGraphParameter1 == "Weekly")
                    {
                        cc.Titles[0].Content = "Weekly Sales";
                    }
                    if (strGraphParameter1 == "Monthly")
                    {
                        cc.Titles[0].Content = "Monthly Sales";
                    }
                    if (strGraphParameter1 == "Yearly")
                    {
                        cc.Titles[0].Content = "Yearly Sales";
                    }

                    if (strGraphParameter2 != "")
                    {
                        cc.Titles[0].Content = cc.Titles[0].Content + strGraphParameter2;
                    }

                    cc.Titles[1].Content = strGraphParameter3;

                    if (SystemVariables.CurrencySymbol != "")
                    {
                        cc.Titles[2].Content = "Amount in " + SystemVariables.CurrencySymbol;
                    }
                    else
                    {
                        cc.Titles[2].Content = "Amount";
                    }
                }
            }

            if (cmbView.SelectedIndex == 1)
            {
                if (strGraphFor == "Sales By Period")
                {
                    if (strGraphParameter1 == "Hourly")
                    {
                        cc1.Titles[0].Content = "Hourly Sales";
                    }
                    if (strGraphParameter1 == "Daily")
                    {
                        cc1.Titles[0].Content = "Daily Sales";
                    }
                    if (strGraphParameter1 == "Weekly")
                    {
                        cc1.Titles[0].Content = "Weekly Sales";
                    }
                    if (strGraphParameter1 == "Monthly")
                    {
                        cc1.Titles[0].Content = "Monthly Sales";
                    }
                    if (strGraphParameter1 == "Yearly")
                    {
                        cc1.Titles[0].Content = "Yearly Sales";
                    }

                    if (strGraphParameter2 != "")
                    {
                        cc1.Titles[0].Content = cc1.Titles[0].Content + strGraphParameter2;
                    }

                    cc1.Titles[1].Content = strGraphParameter3;

                    if (SystemVariables.CurrencySymbol != "")
                    {
                        cc1.Titles[2].Content = "Amount in " + SystemVariables.CurrencySymbol;
                    }
                    else
                    {
                        cc1.Titles[2].Content = "Amount";
                    }
                }
            }

        }

        private void PrepareCharts()
        {
            if ((cmbView.SelectedIndex == 0) || (cmbView.SelectedIndex == 1))
            {
                cc.Visibility = Visibility.Visible;
                cc1.Visibility = Visibility.Collapsed;
            }
            if (cmbView.SelectedIndex == 1)
            {
                cc1.Visibility = Visibility.Visible;
                cc.Visibility = Visibility.Collapsed;
            }

            if (cmbView.SelectedIndex == 0)
            {
                diagram.Series.Clear();
                DataTable dtbl1 = new DataTable();
                dtbl1 = dtblChartData;
                BarSideBySideSeries2D series = new BarSideBySideSeries2D();
                //SeriesLabel label = new SeriesLabel();
                
                
                diagram.Series.Add(series);

                foreach (DataRow dr in dtbl1.Rows)
                {
                    series.Points.Add(new SeriesPoint(dr["Argument"].ToString(), GeneralFunctions.fnDouble(dr["Value"].ToString())));
                    //cc.Series[0].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr["Argument"].ToString(), new double[] { GeneralFunctions.fnDouble(dr["Value"].ToString()) }));
                }
                

                //series.Label.TextPattern = "{A}: {V:F2}";
                //series.PointOptions.PointView = PointView.Values;
                //series.PointOptions.ValueNumericOptions.Format = NumericFormat.FixedPoint;
                //series.PointOptions.ValueNumericOptions.Precision = Settings.DecimalPlace;
                series.ValueScaleType = ScaleType.Numerical;
                series.ArgumentScaleType = ScaleType.Qualitative;
            }

            if (cmbView.SelectedIndex == 2)
            {
                diagram.Series.Clear();
                DataTable dtbl1 = new DataTable();
                dtbl1 = dtblChartData;
                LineStepSeries2D series = new LineStepSeries2D();
                //SeriesLabel label = new SeriesLabel();


                diagram.Series.Add(series);

                foreach (DataRow dr in dtbl1.Rows)
                {
                    series.Points.Add(new SeriesPoint(dr["Argument"].ToString(), GeneralFunctions.fnDouble(dr["Value"].ToString())));
                    //cc.Series[0].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr["Argument"].ToString(), new double[] { GeneralFunctions.fnDouble(dr["Value"].ToString()) }));
                }


                //series.Label.TextPattern = "{A}: {V:F2}";
                //series.PointOptions.PointView = PointView.Values;
                //series.PointOptions.ValueNumericOptions.Format = NumericFormat.FixedPoint;
                //series.PointOptions.ValueNumericOptions.Precision = Settings.DecimalPlace;
                series.ValueScaleType = ScaleType.Numerical;
                series.ArgumentScaleType = ScaleType.Qualitative;
            }

            if (cmbView.SelectedIndex == 1)
            {
                diagram1.Series.Clear();
                PieSeries2D series = new PieSeries2D();
                diagram1.Series.Add(series);

                DataTable dtbl1 = new DataTable();
                dtbl1 = dtblChartData;

                foreach (DataRow dr in dtbl1.Rows)
                {
                    series.Points.Add(new SeriesPoint(dr["Argument"].ToString(), GeneralFunctions.fnDouble(dr["Value"].ToString())));
                    //cc.Series[0].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr["Argument"].ToString(), new double[] { GeneralFunctions.fnDouble(dr["Value"].ToString()) }));
                }

                
                //series.Label.TextPattern = "{A}: {V:P}";
                /*series.PointOptions.PointView = PointView.ArgumentAndValues;

                series.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                series.PointOptions.ValueNumericOptions.Precision = 0;*/
                series.ValueScaleType = ScaleType.Numerical;
                series.ArgumentScaleType = ScaleType.Qualitative;
            }




            /*

            if (cmbView.SelectedIndex == 0)
            {
                cc.Series.Add("", DevExpress.XtraCharts.ViewType.Bar);
            }
            if (cmbView.SelectedIndex == 1)
            {
                cc.Series.Add("", DevExpress.XtraCharts.ViewType.Pie);
            }
            if (cmbView.SelectedIndex == 2)
            {
                cc.Series.Add("", DevExpress.XtraCharts.ViewType.StepLine);
            }
            if (cmbView.SelectedIndex == 3)
            {
                cc.Series.Add("", DevExpress.XtraCharts.ViewType.Line);
            }

            




            if (cmbView.SelectedIndex != 1)
            {
                cc.Series[0].PointOptions.PointView = DevExpress.XtraCharts.PointView.Values;
                cc.Series[0].PointOptions.ValueNumericOptions.Format = DevExpress.XtraCharts.NumericFormat.FixedPoint;
                cc.Series[0].PointOptions.ValueNumericOptions.Precision = Settings.DecimalPlace;
                cc.Series[0].ValueScaleType = ScaleType.Numerical;
                cc.Series[0].ArgumentScaleType = ScaleType.Qualitative;
            }
            else
            {
                cc.Series[0].PointOptions.PointView = DevExpress.XtraCharts.PointView.ArgumentAndValues;

                cc.Series[0].PointOptions.ValueNumericOptions.Format = DevExpress.XtraCharts.NumericFormat.Percent;
                cc.Series[0].PointOptions.ValueNumericOptions.Precision = 0;
                cc.Series[0].ValueScaleType = ScaleType.Numerical;
                cc.Series[0].ArgumentScaleType = ScaleType.Qualitative;

                cc.Legend.Visible = false;

            }*/
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetChartTitle();
            PrepareCharts();
        }

        private void CmbView_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            SetChartTitle();
            PrepareCharts();
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            if ((cmbView.SelectedIndex == 0) || (cmbView.SelectedIndex == 2))
            {
                cc.ShowPrintPreviewDialog(this);
            }

            if (cmbView.SelectedIndex == 1)
            {
                cc1.ShowPrintPreviewDialog(this);
            }
        }

        private void CmbView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.ComboBoxEdit editor = sender as DevExpress.Xpf.Editors.ComboBoxEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }
    }
}
