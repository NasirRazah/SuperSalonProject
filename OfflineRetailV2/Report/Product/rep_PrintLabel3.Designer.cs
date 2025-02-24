namespace OfflineRetailV2.Report.Product
{
    partial class repPrintLabel3
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
            DevExpress.XtraPrinting.BarCode.Code128Generator code128Generator1 = new DevExpress.XtraPrinting.BarCode.Code128Generator();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
            this.lbPrice = new DevExpress.XtraReports.UI.XRLabel();
            this.dataSet1 = new System.Data.DataSet();
            this.lbBarCode = new DevExpress.XtraReports.UI.XRBarCode();
            this.lbProduct = new DevExpress.XtraReports.UI.XRLabel();
            this.lbSKU = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1});
            this.Detail.HeightF = 125F;
            this.Detail.MultiColumn.ColumnSpacing = 2F;
            this.Detail.MultiColumn.ColumnWidth = 120F;
            this.Detail.MultiColumn.Direction = DevExpress.XtraReports.UI.ColumnDirection.AcrossThenDown;
            this.Detail.MultiColumn.Mode = DevExpress.XtraReports.UI.MultiColumnMode.UseColumnWidth;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.RepeatCountOnEmptyDataSource = 2;
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.Detail.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.Detail_BeforePrint);
            // 
            // xrPanel1
            // 
            this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lbPrice,
            this.lbBarCode,
            this.lbProduct,
            this.lbSKU});
            this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 8F);
            this.xrPanel1.Name = "xrPanel1";
            this.xrPanel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrPanel1.SizeF = new System.Drawing.SizeF(117F, 109F);
            // 
            // lbPrice
            // 
            this.lbPrice.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PRICE")});
            this.lbPrice.Font = new DevExpress.Drawing.DXFont("Arial", 6.75F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.lbPrice.LocationFloat = new DevExpress.Utils.PointFloat(0F, 78F);
            this.lbPrice.Name = "lbPrice";
            this.lbPrice.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbPrice.SizeF = new System.Drawing.SizeF(87F, 16F);
            this.lbPrice.Text = "lbPrice";
            this.lbPrice.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.lbPrice.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.lbSKU_BeforePrint);
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            // 
            // lbBarCode
            // 
            this.lbBarCode.AutoModule = true;
            this.lbBarCode.BarCodeOrientation = DevExpress.XtraPrinting.BarCode.BarCodeOrientation.UpsideDown;
            this.lbBarCode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "SKU")});
            this.lbBarCode.Font = new DevExpress.Drawing.DXFont("Arial", 6F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.lbBarCode.LocationFloat = new DevExpress.Utils.PointFloat(0F, 52F);
            this.lbBarCode.Module = 1F;
            this.lbBarCode.Name = "lbBarCode";
            this.lbBarCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 0, 0, 100F);
            this.lbBarCode.ShowText = false;
            this.lbBarCode.SizeF = new System.Drawing.SizeF(100F, 20F);
            this.lbBarCode.Symbology = code128Generator1;
            this.lbBarCode.Text = "lbBarCode";
            this.lbBarCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.lbBarCode.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.lbSKU_BeforePrint);
            // 
            // lbProduct
            // 
            this.lbProduct.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DESC")});
            this.lbProduct.Font = new DevExpress.Drawing.DXFont("Arial", 6.75F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.lbProduct.LocationFloat = new DevExpress.Utils.PointFloat(0F, 23F);
            this.lbProduct.Name = "lbProduct";
            this.lbProduct.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbProduct.SizeF = new System.Drawing.SizeF(96F, 26F);
            this.lbProduct.Text = "lbProduct";
            this.lbProduct.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.lbProduct.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.lbSKU_BeforePrint);
            // 
            // lbSKU
            // 
            this.lbSKU.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "SKU")});
            this.lbSKU.Font = new DevExpress.Drawing.DXFont("Arial", 6.75F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.lbSKU.LocationFloat = new DevExpress.Utils.PointFloat(0F, 8F);
            this.lbSKU.Name = "lbSKU";
            this.lbSKU.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbSKU.SizeF = new System.Drawing.SizeF(100F, 12F);
            this.lbSKU.Text = "lbSKU";
            this.lbSKU.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.lbSKU.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.lbSKU_BeforePrint);
            // 
            // PageHeader
            // 
            this.PageHeader.HeightF = 0F;
            this.PageHeader.Name = "PageHeader";
            this.PageHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
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
            // repPrintLabel3
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.topMarginBand1,
            this.bottomMarginBand1});
            this.DataSource = this.dataSet1;
            this.Margins = new DevExpress.Drawing.DXMargins(0, 0, 0, 0);
            this.PageHeight = 125;
            this.PageWidth = 300;
            this.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.Custom;
            this.Version = "10.1";
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRPanel xrPanel1;
        private System.Data.DataSet dataSet1;
        public DevExpress.XtraReports.UI.XRLabel lbPrice;
        public DevExpress.XtraReports.UI.XRBarCode lbBarCode;
        public DevExpress.XtraReports.UI.XRLabel lbProduct;
        public DevExpress.XtraReports.UI.XRLabel lbSKU;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
        private DevExpress.XtraReports.UI.BottomMarginBand bottomMarginBand1;
    }
}
