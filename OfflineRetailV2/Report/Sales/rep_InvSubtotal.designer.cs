namespace OfflineRetailV2.Report.Sales
{
    partial class repInvSubtotal
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
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.SubTotal = new DevExpress.XtraReports.UI.XRTableCell();
            this.rSubTotal = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.rlDiscount = new DevExpress.XtraReports.UI.XRTableCell();
            this.rDiscount = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.rTax = new DevExpress.XtraReports.UI.XRTableCell();
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.HeightF = 0F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.PageHeader.HeightF = 75F;
            this.PageHeader.Name = "PageHeader";
            this.PageHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable1
            // 
            this.xrTable1.BackColor = System.Drawing.Color.Transparent;
            this.xrTable1.BorderColor = System.Drawing.Color.White;
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(425F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 96F);
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4,
            this.xrTableRow2,
            this.xrTableRow3});
            this.xrTable1.SizeF = new System.Drawing.SizeF(315F, 75F);
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.SubTotal,
            this.rSubTotal});
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 96F);
            this.xrTableRow4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableRow4.Weight = 0.33333333333333331D;
            // 
            // SubTotal
            // 
            this.SubTotal.BorderColor = System.Drawing.Color.SlateGray;
            this.SubTotal.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Double;
            this.SubTotal.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.SubTotal.Font = new DevExpress.Drawing.DXFont("Segoe UI", 11.25F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.SubTotal.Name = "SubTotal";
            this.SubTotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.SubTotal.StylePriority.UseBorderColor = false;
            this.SubTotal.StylePriority.UseBorderDashStyle = false;
            this.SubTotal.StylePriority.UseBorders = false;
            this.SubTotal.StylePriority.UseFont = false;
            this.SubTotal.Text = "SubTotal";
            this.SubTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.SubTotal.Weight = 0.63492063492063489D;
            // 
            // rSubTotal
            // 
            this.rSubTotal.BorderColor = System.Drawing.Color.SlateGray;
            this.rSubTotal.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Double;
            this.rSubTotal.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.rSubTotal.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9.75F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rSubTotal.Name = "rSubTotal";
            this.rSubTotal.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rSubTotal.StylePriority.UseBorderColor = false;
            this.rSubTotal.StylePriority.UseBorderDashStyle = false;
            this.rSubTotal.StylePriority.UseBorders = false;
            this.rSubTotal.StylePriority.UseFont = false;
            this.rSubTotal.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.rSubTotal.Weight = 0.36507936507936506D;
            this.rSubTotal.PrintOnPage += new DevExpress.XtraReports.UI.PrintOnPageEventHandler(this.rSubTotal_PrintOnPage);
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.BackColor = System.Drawing.Color.Transparent;
            this.xrTableRow2.BorderColor = System.Drawing.Color.Transparent;
            this.xrTableRow2.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.rlDiscount,
            this.rDiscount});
            this.xrTableRow2.Font = new DevExpress.Drawing.DXFont("Arial", 8.25F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 96F);
            this.xrTableRow2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableRow2.Weight = 0.33333333333333331D;
            // 
            // rlDiscount
            // 
            this.rlDiscount.Font = new DevExpress.Drawing.DXFont("Segoe UI", 11.25F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rlDiscount.Multiline = true;
            this.rlDiscount.Name = "rlDiscount";
            this.rlDiscount.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rlDiscount.StylePriority.UseFont = false;
            this.rlDiscount.Text = "Total Discount";
            this.rlDiscount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.rlDiscount.Weight = 0.63492063492063489D;
            this.rlDiscount.PrintOnPage += new DevExpress.XtraReports.UI.PrintOnPageEventHandler(this.rlDiscount_PrintOnPage);
            // 
            // rDiscount
            // 
            this.rDiscount.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9.75F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rDiscount.Name = "rDiscount";
            this.rDiscount.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rDiscount.StylePriority.UseFont = false;
            this.rDiscount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.rDiscount.Weight = 0.36507936507936506D;
            this.rDiscount.PrintOnPage += new DevExpress.XtraReports.UI.PrintOnPageEventHandler(this.rDiscount_PrintOnPage);
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.BorderColor = System.Drawing.Color.Transparent;
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell14,
            this.rTax});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 96F);
            this.xrTableRow3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableRow3.Weight = 0.33333333333333331D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.BorderColor = System.Drawing.Color.SlateGray;
            this.xrTableCell14.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell14.Font = new DevExpress.Drawing.DXFont("Segoe UI", 11.25F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrTableCell14.StylePriority.UseBorderColor = false;
            this.xrTableCell14.StylePriority.UseBorders = false;
            this.xrTableCell14.StylePriority.UseFont = false;
            this.xrTableCell14.Text = "Total tax";
            this.xrTableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell14.Weight = 0.63492063492063489D;
            // 
            // rTax
            // 
            this.rTax.BorderColor = System.Drawing.Color.SlateGray;
            this.rTax.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.rTax.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9.75F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rTax.Name = "rTax";
            this.rTax.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rTax.StylePriority.UseBorderColor = false;
            this.rTax.StylePriority.UseBorders = false;
            this.rTax.StylePriority.UseFont = false;
            this.rTax.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.rTax.Weight = 0.36507936507936506D;
            this.rTax.PrintOnPage += new DevExpress.XtraReports.UI.PrintOnPageEventHandler(this.rTax_PrintOnPage);
            // 
            // topMarginBand1
            // 
            this.topMarginBand1.HeightF = 0F;
            this.topMarginBand1.Name = "topMarginBand1";
            // 
            // bottomMarginBand1
            // 
            this.bottomMarginBand1.HeightF = 0F;
            this.bottomMarginBand1.Name = "bottomMarginBand1";
            // 
            // repInvSubtotal
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.topMarginBand1,
            this.bottomMarginBand1});
            this.Margins = new DevExpress.Drawing.DXMargins(50, 50, 0, 0);
            this.Version = "13.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        public DevExpress.XtraReports.UI.XRTableCell rlDiscount;
        public DevExpress.XtraReports.UI.XRTableCell rDiscount;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell14;
        public DevExpress.XtraReports.UI.XRTableCell rTax;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow4;
        private DevExpress.XtraReports.UI.XRTableCell SubTotal;
        public DevExpress.XtraReports.UI.XRTableCell rSubTotal;
        private DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
        private DevExpress.XtraReports.UI.BottomMarginBand bottomMarginBand1;
    }
}
