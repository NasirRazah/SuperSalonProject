namespace OfflineRetailV2.Report.Sales
{
    partial class repSalesSummary
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
            DevExpress.XtraPrinting.Shape.ShapeBracket shapeBracket1 = new DevExpress.XtraPrinting.Shape.ShapeBracket();
            DevExpress.XtraPrinting.Shape.ShapeBracket shapeBracket2 = new DevExpress.XtraPrinting.Shape.ShapeBracket();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.rv = new DevExpress.XtraReports.UI.XRLabel();
            this.rtxt = new DevExpress.XtraReports.UI.XRLabel();
            this.rs = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.rReportHeader = new DevExpress.XtraReports.UI.XRLabel();
            this.rReportHeaderCompany = new DevExpress.XtraReports.UI.XRLabel();
            this.rHeader = new DevExpress.XtraReports.UI.XRLabel();
            this.rDate = new DevExpress.XtraReports.UI.XRLabel();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrShape2 = new DevExpress.XtraReports.UI.XRShape();
            this.xrShape1 = new DevExpress.XtraReports.UI.XRShape();
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.rv,
            this.rtxt,
            this.rs});
            this.Detail.HeightF = 30F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // rv
            // 
            this.rv.BackColor = System.Drawing.Color.Transparent;
            this.rv.Font = new DevExpress.Drawing.DXFont("Arial", 9F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rv.LocationFloat = new DevExpress.Utils.PointFloat(251.38F, 0F);
            this.rv.Name = "rv";
            this.rv.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rv.SizeF = new System.Drawing.SizeF(118.4166F, 30F);
            this.rv.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.rv.PrintOnPage += new DevExpress.XtraReports.UI.PrintOnPageEventHandler(this.rv_PrintOnPage);
            // 
            // rtxt
            // 
            this.rtxt.BackColor = System.Drawing.Color.Transparent;
            this.rtxt.Font = new DevExpress.Drawing.DXFont("Arial", 9F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rtxt.LocationFloat = new DevExpress.Utils.PointFloat(9.999998F, 0F);
            this.rtxt.Name = "rtxt";
            this.rtxt.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rtxt.SizeF = new System.Drawing.SizeF(241.38F, 30F);
            this.rtxt.StylePriority.UseTextAlignment = false;
            this.rtxt.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.rtxt.PrintOnPage += new DevExpress.XtraReports.UI.PrintOnPageEventHandler(this.xrLabel1_PrintOnPage);
            // 
            // rs
            // 
            this.rs.BackColor = System.Drawing.Color.Transparent;
            this.rs.BorderColor = System.Drawing.Color.Transparent;
            this.rs.Font = new DevExpress.Drawing.DXFont("Arial", 9F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rs.ForeColor = System.Drawing.Color.Transparent;
            this.rs.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.rs.Name = "rs";
            this.rs.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rs.SizeF = new System.Drawing.SizeF(8.000031F, 8.083327F);
            this.rs.StylePriority.UseBorderColor = false;
            this.rs.StylePriority.UseForeColor = false;
            this.rs.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.rs.PrintOnPage += new DevExpress.XtraReports.UI.PrintOnPageEventHandler(this.rs_PrintOnPage);
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.rReportHeader,
            this.rReportHeaderCompany,
            this.rHeader,
            this.rDate});
            this.PageHeader.HeightF = 115.4167F;
            this.PageHeader.Name = "PageHeader";
            this.PageHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // rReportHeader
            // 
            this.rReportHeader.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.25F, DevExpress.Drawing.DXFontStyle.Italic);
            this.rReportHeader.LocationFloat = new DevExpress.Utils.PointFloat(422.8751F, 43.20834F);
            this.rReportHeader.Multiline = true;
            this.rReportHeader.Name = "rReportHeader";
            this.rReportHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rReportHeader.SizeF = new System.Drawing.SizeF(317.1249F, 59F);
            this.rReportHeader.StylePriority.UseFont = false;
            this.rReportHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // rReportHeaderCompany
            // 
            this.rReportHeaderCompany.BackColor = System.Drawing.Color.Transparent;
            this.rReportHeaderCompany.Font = new DevExpress.Drawing.DXFont("Segoe UI", 12F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rReportHeaderCompany.LocationFloat = new DevExpress.Utils.PointFloat(422.8751F, 12.20835F);
            this.rReportHeaderCompany.Name = "rReportHeaderCompany";
            this.rReportHeaderCompany.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rReportHeaderCompany.SizeF = new System.Drawing.SizeF(317.1249F, 25F);
            this.rReportHeaderCompany.StylePriority.UseFont = false;
            this.rReportHeaderCompany.StylePriority.UseTextAlignment = false;
            this.rReportHeaderCompany.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // rHeader
            // 
            this.rHeader.BackColor = System.Drawing.Color.Transparent;
            this.rHeader.Font = new DevExpress.Drawing.DXFont("Segoe UI Light", 26.25F);
            this.rHeader.LocationFloat = new DevExpress.Utils.PointFloat(9.999998F, 12.20835F);
            this.rHeader.Name = "rHeader";
            this.rHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rHeader.SizeF = new System.Drawing.SizeF(412.8751F, 60.00001F);
            this.rHeader.StylePriority.UseFont = false;
            this.rHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // rDate
            // 
            this.rDate.BackColor = System.Drawing.Color.Transparent;
            this.rDate.Font = new DevExpress.Drawing.DXFont("Segoe UI Light", 12F);
            this.rDate.LocationFloat = new DevExpress.Utils.PointFloat(9.999998F, 72.20834F);
            this.rDate.Name = "rDate";
            this.rDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rDate.SizeF = new System.Drawing.SizeF(412.8751F, 30F);
            this.rDate.StylePriority.UseFont = false;
            this.rDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo2,
            this.xrPageInfo1,
            this.xrShape2,
            this.xrShape1});
            this.PageFooter.HeightF = 35F;
            this.PageFooter.Name = "PageFooter";
            this.PageFooter.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPageInfo2
            // 
            this.xrPageInfo2.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.25F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrPageInfo2.ForeColor = System.Drawing.Color.Gray;
            this.xrPageInfo2.Format = "{0:dd-MMM-yyyy}";
            this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(597F, 3.374995F);
            this.xrPageInfo2.Name = "xrPageInfo2";
            this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo2.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrPageInfo2.SizeF = new System.Drawing.SizeF(86.87512F, 20.625F);
            this.xrPageInfo2.StylePriority.UseFont = false;
            this.xrPageInfo2.StylePriority.UseForeColor = false;
            this.xrPageInfo2.StylePriority.UseTextAlignment = false;
            this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.25F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrPageInfo1.ForeColor = System.Drawing.Color.Gray;
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(684.8751F, 3.374995F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(43.12482F, 20.625F);
            this.xrPageInfo1.StylePriority.UseFont = false;
            this.xrPageInfo1.StylePriority.UseForeColor = false;
            this.xrPageInfo1.StylePriority.UseTextAlignment = false;
            this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrShape2
            // 
            this.xrShape2.Angle = 180;
            this.xrShape2.ForeColor = System.Drawing.Color.Gray;
            this.xrShape2.LocationFloat = new DevExpress.Utils.PointFloat(727.9999F, 0F);
            this.xrShape2.Name = "xrShape2";
            this.xrShape2.Shape = shapeBracket1;
            this.xrShape2.SizeF = new System.Drawing.SizeF(12F, 27.62497F);
            this.xrShape2.StylePriority.UseForeColor = false;
            // 
            // xrShape1
            // 
            this.xrShape1.ForeColor = System.Drawing.Color.Gray;
            this.xrShape1.LocationFloat = new DevExpress.Utils.PointFloat(583.4584F, 0F);
            this.xrShape1.Name = "xrShape1";
            this.xrShape1.Shape = shapeBracket2;
            this.xrShape1.SizeF = new System.Drawing.SizeF(13.54166F, 27.62497F);
            this.xrShape1.StylePriority.UseForeColor = false;
            // 
            // topMarginBand1
            // 
            this.topMarginBand1.HeightF = 20F;
            this.topMarginBand1.Name = "topMarginBand1";
            // 
            // bottomMarginBand1
            // 
            this.bottomMarginBand1.HeightF = 20F;
            this.bottomMarginBand1.Name = "bottomMarginBand1";
            // 
            // repSalesSummary
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.PageFooter,
            this.topMarginBand1,
            this.bottomMarginBand1});
            this.Margins = new DevExpress.Drawing.DXMargins(50, 50, 20, 20);
            this.Version = "13.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        public DevExpress.XtraReports.UI.XRLabel rt;
        private DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
        private DevExpress.XtraReports.UI.BottomMarginBand bottomMarginBand1;
        public DevExpress.XtraReports.UI.XRLabel rv;
        public DevExpress.XtraReports.UI.XRLabel rtxt;
        public DevExpress.XtraReports.UI.XRLabel rs;
        public DevExpress.XtraReports.UI.XRLabel rReportHeader;
        public DevExpress.XtraReports.UI.XRLabel rReportHeaderCompany;
        public DevExpress.XtraReports.UI.XRLabel rHeader;
        public DevExpress.XtraReports.UI.XRLabel rDate;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo2;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo1;
        private DevExpress.XtraReports.UI.XRShape xrShape2;
        private DevExpress.XtraReports.UI.XRShape xrShape1;
    }
}
