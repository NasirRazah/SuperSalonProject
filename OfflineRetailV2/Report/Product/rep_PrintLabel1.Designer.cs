namespace OfflineRetailV2.Report.Product
{
    partial class repPrintLabel1
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
            this.lbProduct = new DevExpress.XtraReports.UI.XRLabel();
            this.lbSKU = new DevExpress.XtraReports.UI.XRLabel();
            this.lbBarCode = new DevExpress.XtraReports.UI.XRBarCode();
            this.lbCompany = new DevExpress.XtraReports.UI.XRLabel();
            this.dataSet1 = new System.Data.DataSet();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
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
            this.Detail.MultiColumn.ColumnSpacing = 13F;
            this.Detail.MultiColumn.ColumnWidth = 262F;
            this.Detail.MultiColumn.Direction = DevExpress.XtraReports.UI.ColumnDirection.AcrossThenDown;
            this.Detail.MultiColumn.Mode = DevExpress.XtraReports.UI.MultiColumnMode.UseColumnWidth;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.RepeatCountOnEmptyDataSource = 30;
            this.Detail.StylePriority.UseTextAlignment = false;
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.Detail.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.Detail_BeforePrint);
            // 
            // xrPanel1
            // 
            this.xrPanel1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrPanel1.BorderWidth = 0;
            this.xrPanel1.CanGrow = false;
            this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lbPrice,
            this.lbProduct,
            this.lbSKU,
            this.lbBarCode,
            this.lbCompany});
            this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPanel1.Name = "xrPanel1";
            this.xrPanel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrPanel1.SizeF = new System.Drawing.SizeF(262F, 100F);
            this.xrPanel1.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.xrPanel1_BeforePrint);
            // 
            // lbPrice
            // 
            this.lbPrice.BorderWidth = 0;
            this.lbPrice.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PRICE")});
            this.lbPrice.Font = new DevExpress.Drawing.DXFont("Times New Roman", 15.75F, DevExpress.Drawing.DXFontStyle.Bold);
            this.lbPrice.LocationFloat = new DevExpress.Utils.PointFloat(156F, 54F);
            this.lbPrice.Name = "lbPrice";
            this.lbPrice.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbPrice.SizeF = new System.Drawing.SizeF(99F, 41F);
            this.lbPrice.StylePriority.UseFont = false;
            this.lbPrice.Text = "lbPrice";
            this.lbPrice.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.lbPrice.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.lbCompany_BeforePrint);
            // 
            // lbProduct
            // 
            this.lbProduct.BorderWidth = 0;
            this.lbProduct.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DESC")});
            this.lbProduct.Font = new DevExpress.Drawing.DXFont("Arial", 9F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.lbProduct.LocationFloat = new DevExpress.Utils.PointFloat(6F, 66F);
            this.lbProduct.Name = "lbProduct";
            this.lbProduct.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbProduct.SizeF = new System.Drawing.SizeF(150F, 30F);
            this.lbProduct.Text = "lbProduct";
            this.lbProduct.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.lbProduct.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.lbCompany_BeforePrint);
            // 
            // lbSKU
            // 
            this.lbSKU.BorderWidth = 0;
            this.lbSKU.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "SKU")});
            this.lbSKU.Font = new DevExpress.Drawing.DXFont("Arial", 9F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.lbSKU.LocationFloat = new DevExpress.Utils.PointFloat(6F, 50F);
            this.lbSKU.Name = "lbSKU";
            this.lbSKU.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbSKU.SizeF = new System.Drawing.SizeF(150F, 16F);
            this.lbSKU.Text = "lbSKU";
            this.lbSKU.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lbSKU.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.lbCompany_BeforePrint);
            // 
            // lbBarCode
            // 
            this.lbBarCode.BarCodeOrientation = DevExpress.XtraPrinting.BarCode.BarCodeOrientation.UpsideDown;
            this.lbBarCode.BorderWidth = 0;
            this.lbBarCode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "SKU")});
            this.lbBarCode.LocationFloat = new DevExpress.Utils.PointFloat(6F, 22F);
            this.lbBarCode.Module = 1F;
            this.lbBarCode.Name = "lbBarCode";
            this.lbBarCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 0, 0, 100F);
            this.lbBarCode.ShowText = false;
            this.lbBarCode.SizeF = new System.Drawing.SizeF(250F, 25F);
            this.lbBarCode.Symbology = code128Generator1;
            this.lbBarCode.Text = "lbBarCode";
            this.lbBarCode.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.lbCompany_BeforePrint);
            // 
            // lbCompany
            // 
            this.lbCompany.BorderWidth = 0;
            this.lbCompany.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "COMPANY")});
            this.lbCompany.Font = new DevExpress.Drawing.DXFont("Arial", 9F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.lbCompany.LocationFloat = new DevExpress.Utils.PointFloat(6F, 3F);
            this.lbCompany.Name = "lbCompany";
            this.lbCompany.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbCompany.SizeF = new System.Drawing.SizeF(250F, 18F);
            this.lbCompany.Text = "lbCompany";
            this.lbCompany.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lbCompany.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.lbCompany_BeforePrint);
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            // 
            // PageFooter
            // 
            this.PageFooter.HeightF = 30F;
            this.PageFooter.Name = "PageFooter";
            this.PageFooter.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.PageFooter.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.PageFooter_BeforePrint);
            // 
            // PageHeader
            // 
            this.PageHeader.HeightF = 30F;
            this.PageHeader.Name = "PageHeader";
            this.PageHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.PageHeader.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.PageHeader_BeforePrint);
            // 
            // topMarginBand1
            // 
            this.topMarginBand1.HeightF = 50F;
            this.topMarginBand1.Name = "topMarginBand1";
            this.topMarginBand1.StylePriority.UseTextAlignment = false;
            this.topMarginBand1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // bottomMarginBand1
            // 
            this.bottomMarginBand1.HeightF = 0F;
            this.bottomMarginBand1.Name = "bottomMarginBand1";
            // 
            // repPrintLabel1
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageFooter,
            this.PageHeader,
            this.topMarginBand1,
            this.bottomMarginBand1});
            this.DataSource = this.dataSet1;
            this.Margins = new DevExpress.Drawing.DXMargins(19, 0, 50, 0);
            this.Version = "10.1";
            this.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.repPrintLabel1_BeforePrint);
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRPanel xrPanel1;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        public DevExpress.XtraReports.UI.XRLabel lbCompany;
        public DevExpress.XtraReports.UI.XRBarCode lbBarCode;
        public DevExpress.XtraReports.UI.XRLabel lbSKU;
        public DevExpress.XtraReports.UI.XRLabel lbProduct;
        public DevExpress.XtraReports.UI.XRLabel lbPrice;
        private System.Data.DataSet dataSet1;
        private DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
        private DevExpress.XtraReports.UI.BottomMarginBand bottomMarginBand1;
    }
}
