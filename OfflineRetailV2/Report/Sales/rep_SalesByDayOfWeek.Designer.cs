namespace OfflineRetailV2.Report.Sales
{
    partial class repSalesByDayOfWeek
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
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraPrinting.Shape.ShapeBracket shapeBracket1 = new DevExpress.XtraPrinting.Shape.ShapeBracket();
            DevExpress.XtraPrinting.Shape.ShapeBracket shapeBracket2 = new DevExpress.XtraPrinting.Shape.ShapeBracket();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrChart1 = new DevExpress.XtraReports.UI.XRChart();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.rReportHeaderCompany = new DevExpress.XtraReports.UI.XRLabel();
            this.rDetSum = new DevExpress.XtraReports.UI.XRLabel();
            this.rDate = new DevExpress.XtraReports.UI.XRLabel();
            this.rHeader = new DevExpress.XtraReports.UI.XRLabel();
            this.rReportHeader = new DevExpress.XtraReports.UI.XRLabel();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrShape2 = new DevExpress.XtraReports.UI.XRShape();
            this.xrShape1 = new DevExpress.XtraReports.UI.XRShape();
            this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            ((System.ComponentModel.ISupportInitialize)(this.xrChart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrChart1});
            this.Detail.HeightF = 675F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrChart1
            // 
            this.xrChart1.AppearanceNameSerializable = "Light";
            this.xrChart1.BorderColor = System.Drawing.Color.Black;
            this.xrChart1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            xyDiagram1.AxisX.Range.AlwaysShowZeroLevel = true;
            xyDiagram1.AxisX.Range.SideMarginsEnabled = true;
            xyDiagram1.AxisX.Title.DXFont = new DevExpress.Drawing.DXFont("Arial", 9.75F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            xyDiagram1.AxisX.Title.Text = "Day of Week";
            xyDiagram1.AxisX.Title.Visible = true;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.Range.AlwaysShowZeroLevel = true;
            xyDiagram1.AxisY.Range.SideMarginsEnabled = true;
            xyDiagram1.AxisY.Title.DXFont = new DevExpress.Drawing.DXFont("Arial", 9.75F);
            xyDiagram1.AxisY.Title.Text = "Sales";
            xyDiagram1.AxisY.Title.Visible = true;
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            this.xrChart1.Diagram = xyDiagram1;
            this.xrChart1.Legend.Antialiasing = true;
            this.xrChart1.LocationFloat = new DevExpress.Utils.PointFloat(9.999998F, 7.999992F);
            this.xrChart1.Name = "xrChart1";
            this.xrChart1.PaletteName = "Nature Colors";
            series1.Name = "Series 1";
            series1.ShowInLegend = false;
            this.xrChart1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.xrChart1.SizeF = new System.Drawing.SizeF(790F, 659F);
            this.xrChart1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.rReportHeaderCompany,
            this.rDetSum,
            this.rDate,
            this.rHeader,
            this.rReportHeader});
            this.PageHeader.HeightF = 130F;
            this.PageHeader.Name = "PageHeader";
            this.PageHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // rReportHeaderCompany
            // 
            this.rReportHeaderCompany.BackColor = System.Drawing.Color.Transparent;
            this.rReportHeaderCompany.Font = new DevExpress.Drawing.DXFont("Segoe UI", 12F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rReportHeaderCompany.LocationFloat = new DevExpress.Utils.PointFloat(432.625F, 7.999992F);
            this.rReportHeaderCompany.Name = "rReportHeaderCompany";
            this.rReportHeaderCompany.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rReportHeaderCompany.SizeF = new System.Drawing.SizeF(367.375F, 25F);
            this.rReportHeaderCompany.StylePriority.UseFont = false;
            this.rReportHeaderCompany.StylePriority.UseTextAlignment = false;
            this.rReportHeaderCompany.Text = "Company";
            this.rReportHeaderCompany.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // rDetSum
            // 
            this.rDetSum.BackColor = System.Drawing.Color.Transparent;
            this.rDetSum.Font = new DevExpress.Drawing.DXFont("Segoe UI", 12F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rDetSum.LocationFloat = new DevExpress.Utils.PointFloat(9.999998F, 105F);
            this.rDetSum.Name = "rDetSum";
            this.rDetSum.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rDetSum.SizeF = new System.Drawing.SizeF(409.4167F, 25F);
            this.rDetSum.StylePriority.UseFont = false;
            this.rDetSum.Text = "Report";
            this.rDetSum.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // rDate
            // 
            this.rDate.BackColor = System.Drawing.Color.Transparent;
            this.rDate.Font = new DevExpress.Drawing.DXFont("Segoe UI Light", 12F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rDate.LocationFloat = new DevExpress.Utils.PointFloat(9.999998F, 68.99999F);
            this.rDate.Name = "rDate";
            this.rDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rDate.SizeF = new System.Drawing.SizeF(409.4167F, 30F);
            this.rDate.StylePriority.UseFont = false;
            this.rDate.StylePriority.UseTextAlignment = false;
            this.rDate.Text = "Report";
            this.rDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // rHeader
            // 
            this.rHeader.BackColor = System.Drawing.Color.Transparent;
            this.rHeader.Font = new DevExpress.Drawing.DXFont("Segoe UI Light", 26.25F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rHeader.LocationFloat = new DevExpress.Utils.PointFloat(9.999998F, 7.999992F);
            this.rHeader.Name = "rHeader";
            this.rHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rHeader.SizeF = new System.Drawing.SizeF(409.4167F, 60F);
            this.rHeader.StylePriority.UseFont = false;
            this.rHeader.StylePriority.UseTextAlignment = false;
            this.rHeader.Text = "Report";
            this.rHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // rReportHeader
            // 
            this.rReportHeader.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.25F, DevExpress.Drawing.DXFontStyle.Italic, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rReportHeader.LocationFloat = new DevExpress.Utils.PointFloat(432.625F, 39.87499F);
            this.rReportHeader.Multiline = true;
            this.rReportHeader.Name = "rReportHeader";
            this.rReportHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rReportHeader.SizeF = new System.Drawing.SizeF(367.375F, 62.125F);
            this.rReportHeader.StylePriority.UseFont = false;
            this.rReportHeader.Text = "Header";
            this.rReportHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1,
            this.xrShape2,
            this.xrShape1,
            this.xrPageInfo2});
            this.PageFooter.HeightF = 35F;
            this.PageFooter.Name = "PageFooter";
            this.PageFooter.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.25F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrPageInfo1.ForeColor = System.Drawing.Color.Gray;
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(736.5209F, 4.375013F);
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
            this.xrShape2.LocationFloat = new DevExpress.Utils.PointFloat(780.6456F, 0.3750483F);
            this.xrShape2.Name = "xrShape2";
            this.xrShape2.Shape = shapeBracket1;
            this.xrShape2.SizeF = new System.Drawing.SizeF(12F, 27.62497F);
            this.xrShape2.StylePriority.UseForeColor = false;
            // 
            // xrShape1
            // 
            this.xrShape1.ForeColor = System.Drawing.Color.Gray;
            this.xrShape1.LocationFloat = new DevExpress.Utils.PointFloat(636.1044F, 0.3750483F);
            this.xrShape1.Name = "xrShape1";
            this.xrShape1.Shape = shapeBracket2;
            this.xrShape1.SizeF = new System.Drawing.SizeF(13.54166F, 27.62497F);
            this.xrShape1.StylePriority.UseForeColor = false;
            // 
            // xrPageInfo2
            // 
            this.xrPageInfo2.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.25F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrPageInfo2.ForeColor = System.Drawing.Color.Gray;
            this.xrPageInfo2.Format = "{0:dd-MMM-yyyy}";
            this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(649.6459F, 4.375013F);
            this.xrPageInfo2.Name = "xrPageInfo2";
            this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo2.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrPageInfo2.SizeF = new System.Drawing.SizeF(86.87512F, 20.625F);
            this.xrPageInfo2.StylePriority.UseFont = false;
            this.xrPageInfo2.StylePriority.UseForeColor = false;
            this.xrPageInfo2.StylePriority.UseTextAlignment = false;
            this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1});
            this.ReportFooter.HeightF = 20F;
            this.ReportFooter.Name = "ReportFooter";
            this.ReportFooter.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.ReportFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel1
            // 
            this.xrLabel1.BackColor = System.Drawing.Color.Transparent;
            this.xrLabel1.Font = new DevExpress.Drawing.DXFont("Arial", 8.25F, DevExpress.Drawing.DXFontStyle.Italic, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(25F, 0F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(350F, 17F);
            this.xrLabel1.Text = "* Sales figure do not include Taxes.";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // topMarginBand1
            // 
            this.topMarginBand1.HeightF = 50F;
            this.topMarginBand1.Name = "topMarginBand1";
            // 
            // bottomMarginBand1
            // 
            this.bottomMarginBand1.HeightF = 50F;
            this.bottomMarginBand1.Name = "bottomMarginBand1";
            // 
            // repSalesByDayOfWeek
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.PageFooter,
            this.ReportFooter,
            this.topMarginBand1,
            this.bottomMarginBand1});
            this.Margins = new DevExpress.Drawing.DXMargins(20, 20, 50, 50);
            this.Version = "13.1";
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrChart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        public DevExpress.XtraReports.UI.XRChart xrChart1;
        public DevExpress.XtraReports.UI.XRLabel rDetSum;
        public DevExpress.XtraReports.UI.XRLabel rDate;
        public DevExpress.XtraReports.UI.XRLabel rHeader;
        public DevExpress.XtraReports.UI.XRLabel rReportHeader;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        public DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo1;
        private DevExpress.XtraReports.UI.XRShape xrShape2;
        private DevExpress.XtraReports.UI.XRShape xrShape1;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo2;
        private DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
        private DevExpress.XtraReports.UI.BottomMarginBand bottomMarginBand1;
        public DevExpress.XtraReports.UI.XRLabel rReportHeaderCompany;
    }
}
