/*
 USER CLASS : Report - Employee Attendance Listing
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
    public partial class repAttendanceDetails : DevExpress.XtraReports.UI.XtraReport
    {
        public repAttendanceDetails()
        {
            InitializeComponent();
            //Translation.SetMultilingualTextInXtraReport(this);
        }
        protected override BandPresenter CreateBandPresenter()
        {
            var bandPresenter = base.CreateBandPresenter();

            return new BandPresenterWrapper(bandPresenter, Settings.SetCulture(), this);
        }
        public GroupField CreateGroupField(string groupFieldName)
        {
            GroupField groupField = new GroupField();
            groupField.FieldName = groupFieldName;
            return groupField;
        }

        private void rM_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (rM.Text == "N") rM.Text = "";
            else
            {
                rM.Text = OfflineRetailV2.Properties.Resources.modified;
            }
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

        private void rT_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatTime(rT);
        }

        private void rsumT_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            FormatTime(rsumT);
        }
    }
}
