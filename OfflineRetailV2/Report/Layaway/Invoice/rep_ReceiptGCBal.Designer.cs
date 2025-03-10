namespace OfflineRetailV2.Report.Invoice
{
    partial class repReceiptGCBal
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
            this.rGCAMount = new DevExpress.XtraReports.UI.XRLabel();
            this.rGC = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.rGCDate = new DevExpress.XtraReports.UI.XRLabel();
            this.rLine = new DevExpress.XtraReports.UI.XRLine();
            this.rTax = new DevExpress.XtraReports.UI.XRLabel();
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.rGCAMount,
            this.rGC});
            this.Detail.HeightF = 21F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // rGCAMount
            // 
            this.rGCAMount.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rGCAMount.LocationFloat = new DevExpress.Utils.PointFloat(317F, 0F);
            this.rGCAMount.Name = "rGCAMount";
            this.rGCAMount.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rGCAMount.SizeF = new System.Drawing.SizeF(133F, 17F);
            this.rGCAMount.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.rGCAMount.PrintOnPage += new DevExpress.XtraReports.UI.PrintOnPageEventHandler(this.rGCAMount_PrintOnPage);
            // 
            // rGC
            // 
            this.rGC.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rGC.LocationFloat = new DevExpress.Utils.PointFloat(58F, 0F);
            this.rGC.Name = "rGC";
            this.rGC.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rGC.SizeF = new System.Drawing.SizeF(250F, 17F);
            this.rGC.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.rGC.PrintOnPage += new DevExpress.XtraReports.UI.PrintOnPageEventHandler(this.rGC_PrintOnPage);
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.rGCDate,
            this.rLine,
            this.rTax});
            this.PageHeader.HeightF = 39F;
            this.PageHeader.Name = "PageHeader";
            this.PageHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // rGCDate
            // 
            this.rGCDate.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rGCDate.LocationFloat = new DevExpress.Utils.PointFloat(233F, 8F);
            this.rGCDate.Name = "rGCDate";
            this.rGCDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rGCDate.SizeF = new System.Drawing.SizeF(200F, 17F);
            this.rGCDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // rLine
            // 
            this.rLine.LocationFloat = new DevExpress.Utils.PointFloat(50F, 25F);
            this.rLine.Name = "rLine";
            this.rLine.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.rLine.SizeF = new System.Drawing.SizeF(400F, 9F);
            // 
            // rTax
            // 
            this.rTax.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rTax.LocationFloat = new DevExpress.Utils.PointFloat(50F, 8F);
            this.rTax.Name = "rTax";
            this.rTax.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rTax.SizeF = new System.Drawing.SizeF(183F, 17F);
            this.rTax.Text = "GC(s) with balance as on : ";
            this.rTax.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // topMarginBand1
            // 
            this.topMarginBand1.Name = "topMarginBand1";
            // 
            // bottomMarginBand1
            // 
            this.bottomMarginBand1.Name = "bottomMarginBand1";
            // 
            // repReceiptGCBal
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.topMarginBand1,
            this.bottomMarginBand1});
            this.Margins = new DevExpress.Drawing.DXMargins(200, 200, 100, 100);
            this.Version = "13.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        public DevExpress.XtraReports.UI.XRLabel rGCAMount;
        public DevExpress.XtraReports.UI.XRLabel rGC;
        public DevExpress.XtraReports.UI.XRLabel rTax;
        public DevExpress.XtraReports.UI.XRLine rLine;
        public DevExpress.XtraReports.UI.XRLabel rGCDate;
        private DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
        private DevExpress.XtraReports.UI.BottomMarginBand bottomMarginBand1;
    }
}
