﻿namespace OfflineRetailV2.Report.Sales
{
    partial class repDashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.rHeader = new DevExpress.XtraReports.UI.XRLabel();
            this.rDate = new DevExpress.XtraReports.UI.XRLabel();
            this.rReportHeaderCompany = new DevExpress.XtraReports.UI.XRLabel();
            this.rReportHeader = new DevExpress.XtraReports.UI.XRLabel();
            this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
            this.srepSH = new DevExpress.XtraReports.UI.XRSubreport();
            this.srepSC = new DevExpress.XtraReports.UI.XRSubreport();
            this.DetailReport2 = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail3 = new DevExpress.XtraReports.UI.DetailBand();
            this.srepSD = new DevExpress.XtraReports.UI.XRSubreport();
            this.DetailReport1 = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail2 = new DevExpress.XtraReports.UI.DetailBand();
            this.srepTX = new DevExpress.XtraReports.UI.XRSubreport();
            this.srepGC = new DevExpress.XtraReports.UI.XRSubreport();
            this.srepT = new DevExpress.XtraReports.UI.XRSubreport();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.HeightF = 1.041667F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 5F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 3.131549F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // rHeader
            // 
            this.rHeader.BackColor = System.Drawing.Color.Transparent;
            this.rHeader.Font = new DevExpress.Drawing.DXFont("Segoe UI Light", 26.25F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rHeader.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 10.00001F);
            this.rHeader.Name = "rHeader";
            this.rHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.rHeader.SizeF = new System.Drawing.SizeF(427.17F, 60F);
            this.rHeader.StylePriority.UseFont = false;
            this.rHeader.StylePriority.UsePadding = false;
            this.rHeader.StylePriority.UseTextAlignment = false;
            this.rHeader.Text = "Sales Summary Report";
            this.rHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // rDate
            // 
            this.rDate.BackColor = System.Drawing.Color.Transparent;
            this.rDate.Font = new DevExpress.Drawing.DXFont("Segoe UI Light", 12F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rDate.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 70F);
            this.rDate.Name = "rDate";
            this.rDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rDate.SizeF = new System.Drawing.SizeF(427.17F, 30F);
            this.rDate.StylePriority.UseFont = false;
            this.rDate.Text = "Report";
            this.rDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // rReportHeaderCompany
            // 
            this.rReportHeaderCompany.BackColor = System.Drawing.Color.Transparent;
            this.rReportHeaderCompany.Font = new DevExpress.Drawing.DXFont("Segoe UI", 12F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rReportHeaderCompany.LocationFloat = new DevExpress.Utils.PointFloat(449.3332F, 10.00001F);
            this.rReportHeaderCompany.Name = "rReportHeaderCompany";
            this.rReportHeaderCompany.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rReportHeaderCompany.SizeF = new System.Drawing.SizeF(357.6668F, 25F);
            this.rReportHeaderCompany.StylePriority.UseFont = false;
            this.rReportHeaderCompany.StylePriority.UseTextAlignment = false;
            this.rReportHeaderCompany.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // rReportHeader
            // 
            this.rReportHeader.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.25F, DevExpress.Drawing.DXFontStyle.Italic, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rReportHeader.LocationFloat = new DevExpress.Utils.PointFloat(449.3332F, 40.625F);
            this.rReportHeader.Multiline = true;
            this.rReportHeader.Name = "rReportHeader";
            this.rReportHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rReportHeader.SizeF = new System.Drawing.SizeF(357.6667F, 59.37501F);
            this.rReportHeader.StylePriority.UseFont = false;
            this.rReportHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // DetailReport
            // 
            this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1,
            this.DetailReport2});
            this.DetailReport.Level = 0;
            this.DetailReport.Name = "DetailReport";
            // 
            // Detail1
            // 
            this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.srepSH,
            this.srepSC});
            this.Detail1.HeightF = 37.50331F;
            this.Detail1.KeepTogether = true;
            this.Detail1.KeepTogetherWithDetailReports = true;
            this.Detail1.Name = "Detail1";
            // 
            // srepSH
            // 
            this.srepSH.CanShrink = true;
            this.srepSH.Id = 0;
            this.srepSH.LocationFloat = new DevExpress.Utils.PointFloat(420.0001F, 0F);
            this.srepSH.Name = "srepSH";
            this.srepSH.SizeF = new System.Drawing.SizeF(412.9999F, 30.21164F);
            // 
            // srepSC
            // 
            this.srepSC.CanShrink = true;
            this.srepSC.Id = 0;
            this.srepSC.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.srepSC.Name = "srepSC";
            this.srepSC.SizeF = new System.Drawing.SizeF(407F, 30.21164F);
            // 
            // DetailReport2
            // 
            this.DetailReport2.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail3});
            this.DetailReport2.Level = 0;
            this.DetailReport2.Name = "DetailReport2";
            // 
            // Detail3
            // 
            this.Detail3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.srepSD});
            this.Detail3.HeightF = 36.45833F;
            this.Detail3.KeepTogether = true;
            this.Detail3.KeepTogetherWithDetailReports = true;
            this.Detail3.Name = "Detail3";
            // 
            // srepSD
            // 
            this.srepSD.CanShrink = true;
            this.srepSD.Id = 0;
            this.srepSD.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.srepSD.Name = "srepSD";
            this.srepSD.SizeF = new System.Drawing.SizeF(833F, 32.29167F);
            // 
            // DetailReport1
            // 
            this.DetailReport1.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail2});
            this.DetailReport1.Level = 1;
            this.DetailReport1.Name = "DetailReport1";
            // 
            // Detail2
            // 
            this.Detail2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.srepTX,
            this.srepGC,
            this.srepT});
            this.Detail2.HeightF = 292.0801F;
            this.Detail2.KeepTogether = true;
            this.Detail2.KeepTogetherWithDetailReports = true;
            this.Detail2.Name = "Detail2";
            // 
            // srepTX
            // 
            this.srepTX.CanShrink = true;
            this.srepTX.Id = 0;
            this.srepTX.LocationFloat = new DevExpress.Utils.PointFloat(426F, 128.1185F);
            this.srepTX.Name = "srepTX";
            this.srepTX.SizeF = new System.Drawing.SizeF(407F, 163.9616F);
            // 
            // srepGC
            // 
            this.srepGC.CanShrink = true;
            this.srepGC.Id = 0;
            this.srepGC.LocationFloat = new DevExpress.Utils.PointFloat(426F, 0F);
            this.srepGC.Name = "srepGC";
            this.srepGC.SizeF = new System.Drawing.SizeF(407F, 104.5801F);
            // 
            // srepT
            // 
            this.srepT.CanShrink = true;
            this.srepT.Id = 0;
            this.srepT.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.srepT.Name = "srepT";
            this.srepT.SizeF = new System.Drawing.SizeF(412F, 292.08F);
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.rHeader,
            this.rDate,
            this.rReportHeaderCompany,
            this.rReportHeader});
            this.PageHeader.HeightF = 108.3334F;
            this.PageHeader.Name = "PageHeader";
            // 
            // repDashboard
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.DetailReport,
            this.DetailReport1,
            this.PageHeader});
            this.Margins = new DevExpress.Drawing.DXMargins(5, 5, 5, 3);
            this.Version = "13.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        public DevExpress.XtraReports.UI.XRLabel rHeader;
        public DevExpress.XtraReports.UI.XRLabel rDate;
        public DevExpress.XtraReports.UI.XRLabel rReportHeaderCompany;
        public DevExpress.XtraReports.UI.XRLabel rReportHeader;
        private DevExpress.XtraReports.UI.DetailReportBand DetailReport;
        private DevExpress.XtraReports.UI.DetailBand Detail1;
        public DevExpress.XtraReports.UI.XRSubreport srepSH;
        public DevExpress.XtraReports.UI.XRSubreport srepSC;
        private DevExpress.XtraReports.UI.DetailReportBand DetailReport1;
        private DevExpress.XtraReports.UI.DetailBand Detail2;
        public DevExpress.XtraReports.UI.XRSubreport srepTX;
        public DevExpress.XtraReports.UI.XRSubreport srepGC;
        public DevExpress.XtraReports.UI.XRSubreport srepT;
        private DevExpress.XtraReports.UI.DetailReportBand DetailReport2;
        private DevExpress.XtraReports.UI.DetailBand Detail3;
        public DevExpress.XtraReports.UI.XRSubreport srepSD;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
    }
}
