namespace OfflineRetailV2.Report.Sales.PrePrint
{
    partial class repPPInv
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
            this.subrepL = new DevExpress.XtraReports.UI.XRSubreport();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.subrepH = new DevExpress.XtraReports.UI.XRSubreport();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.subrepT = new DevExpress.XtraReports.UI.XRSubreport();
            this.subrepX = new DevExpress.XtraReports.UI.XRSubreport();
            this.subrepS = new DevExpress.XtraReports.UI.XRSubreport();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.rReportHeader = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.subrepL});
            this.Detail.HeightF = 19F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // subrepL
            // 
            this.subrepL.Id = 0;
            this.subrepL.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.subrepL.Name = "subrepL";
            this.subrepL.SizeF = new System.Drawing.SizeF(100F, 17F);
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 0F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 60F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.subrepH});
            this.ReportHeader.HeightF = 30F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // subrepH
            // 
            this.subrepH.Id = 0;
            this.subrepH.LocationFloat = new DevExpress.Utils.PointFloat(0F, 5.999994F);
            this.subrepH.Name = "subrepH";
            this.subrepH.SizeF = new System.Drawing.SizeF(100F, 17F);
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.subrepT,
            this.subrepX,
            this.subrepS});
            this.ReportFooter.HeightF = 61F;
            this.ReportFooter.Name = "ReportFooter";
            this.ReportFooter.PrintAtBottom = true;
            // 
            // subrepT
            // 
            this.subrepT.Id = 0;
            this.subrepT.LocationFloat = new DevExpress.Utils.PointFloat(0F, 34.00002F);
            this.subrepT.Name = "subrepT";
            this.subrepT.SizeF = new System.Drawing.SizeF(100F, 17F);
            // 
            // subrepX
            // 
            this.subrepX.Id = 0;
            this.subrepX.LocationFloat = new DevExpress.Utils.PointFloat(0F, 17.00001F);
            this.subrepX.Name = "subrepX";
            this.subrepX.SizeF = new System.Drawing.SizeF(100F, 17F);
            // 
            // subrepS
            // 
            this.subrepS.Id = 0;
            this.subrepS.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.subrepS.Name = "subrepS";
            this.subrepS.SizeF = new System.Drawing.SizeF(100F, 17F);
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.rReportHeader});
            this.PageFooter.HeightF = 131F;
            this.PageFooter.Name = "PageFooter";
            // 
            // rReportHeader
            // 
            this.rReportHeader.Font = new DevExpress.Drawing.DXFont("Arial", 8.25F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rReportHeader.LocationFloat = new DevExpress.Utils.PointFloat(38.54167F, 13.00001F);
            this.rReportHeader.Multiline = true;
            this.rReportHeader.Name = "rReportHeader";
            this.rReportHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rReportHeader.SizeF = new System.Drawing.SizeF(358F, 108F);
            this.rReportHeader.StylePriority.UseFont = false;
            this.rReportHeader.StylePriority.UseTextAlignment = false;
            this.rReportHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // repPPInv
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.ReportFooter,
            this.PageFooter});
            this.Margins = new DevExpress.Drawing.DXMargins(0, 0, 0, 60);
            this.Version = "13.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        public DevExpress.XtraReports.UI.XRSubreport subrepL;
        public DevExpress.XtraReports.UI.XRSubreport subrepH;
        public DevExpress.XtraReports.UI.XRSubreport subrepT;
        public DevExpress.XtraReports.UI.XRSubreport subrepX;
        public DevExpress.XtraReports.UI.XRSubreport subrepS;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        public DevExpress.XtraReports.UI.XRLabel rReportHeader;
    }
}
