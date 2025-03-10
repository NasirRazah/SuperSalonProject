/*
 USER CLASS : Report - Employee Payroll
 */
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Native.Presenters;
using System.Globalization;
using OfflineRetailV2.Data;
using OfflineRetailV2;
namespace OfflineRetailV2.Report.Employee
{
    public partial class repPayroll : DevExpress.XtraReports.UI.XtraReport
    {
        private int intDecimalPlace;
        public repPayroll()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }
        protected override BandPresenter CreateBandPresenter()
        {
            var bandPresenter = base.CreateBandPresenter();

            return new BandPresenterWrapper(bandPresenter, Settings.SetCulture(), this);
        }
        public int DecimalPlace
        {
            get { return intDecimalPlace; }
            set { intDecimalPlace = value; }
        }
            
        public GroupField CreateGroupField(string groupFieldName)
        {
            // Create a GroupField object.
            GroupField groupField = new GroupField();

            // Set its field name.
            groupField.FieldName = groupFieldName;

            return groupField;
        }

        private void FormatTime(XRLabel rLabel)
        {
            int intTaskTime = 0;
            if (rLabel.Text != "") intTaskTime = GeneralFunctions.fnInt32(rLabel.Text);
            int taskHH = 0;
            int taskMM = 0;
            if (intTaskTime > 0)
            {
                if (intTaskTime < 60)
                {
                    rLabel.Text = Convert.ToString(intTaskTime) + " " + OfflineRetailV2.Properties.Resources.min;
                }
                else
                {
                    taskHH = GeneralFunctions.fnInt32(Math.Ceiling(GeneralFunctions.fnDouble(intTaskTime / 60)));
                    if (intTaskTime - taskHH * 60 == 0)
                    {
                        rLabel.Text = Convert.ToString(taskHH) + " " + OfflineRetailV2.Properties.Resources.hr;
                    }
                    else
                    {
                        rLabel.Text = Convert.ToString(taskHH) + " " + OfflineRetailV2.Properties.Resources.hr + " " + Convert.ToString(intTaskTime - taskHH * 60) + " " + OfflineRetailV2.Properties.Resources.min;
                    }
                }

            }
            else
            {
                rLabel.Text = "-";
            }
        }

        private void rTime_BeforePrint(object sender, CancelEventArgs e)
        {
            //FormatTime(rTime);
        }

        private void rTime_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatTime(rTime);
        }

        private void rRate_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rRate.Text = GeneralFunctions.fnDouble(rRate.Text).ToString("f3");
            else rRate.Text = GeneralFunctions.fnDouble(rRate.Text).ToString("f");
        }

        private void rTotRate_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (intDecimalPlace == 3) rTotRate.Text = GeneralFunctions.fnDouble(rTotRate.Text).ToString("f3");
            else rTotRate.Text = GeneralFunctions.fnDouble(rTotRate.Text).ToString("f");
        }

    }
}
