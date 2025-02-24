namespace OfflineRetailV2.Report.Product
{
    partial class OneUpCustom
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
            this.lbPrice = new DevExpress.XtraReports.UI.XRLabel();
            this.lbBarCode = new DevExpress.XtraReports.UI.XRBarCode();
            this.lbProduct = new DevExpress.XtraReports.UI.XRLabel();
            this.lbSKU = new DevExpress.XtraReports.UI.XRLabel();
            this.dataSet1 = new System.Data.DataSet();
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lbPrice,
            this.lbBarCode,
            this.lbProduct,
            this.lbSKU});
            this.Detail.Font = new DevExpress.Drawing.DXFont("Times New Roman", 9.75F);
            this.Detail.HeightF = 129F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.StylePriority.UseFont = false;
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lbPrice
            // 
            this.lbPrice.CanGrow = false;
            this.lbPrice.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PRICE")});
            this.lbPrice.Font = new DevExpress.Drawing.DXFont("Arial", 8.25F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {
            new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.lbPrice.LocationFloat = new DevExpress.Utils.PointFloat(5F, 72.00002F);
            this.lbPrice.Name = "lbPrice";
            this.lbPrice.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.lbPrice.SizeF = new System.Drawing.SizeF(270F, 14F);
            this.lbPrice.StylePriority.UsePadding = false;
            this.lbPrice.Text = "[PRICE]";
            this.lbPrice.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lbBarCode
            // 
            this.lbBarCode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "SKU")});
            this.lbBarCode.Font = new DevExpress.Drawing.DXFont("Arial", 6F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {
            new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.lbBarCode.LocationFloat = new DevExpress.Utils.PointFloat(5F, 44.00002F);
            this.lbBarCode.Module = 1F;
            this.lbBarCode.Name = "lbBarCode";
            this.lbBarCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.lbBarCode.ShowText = false;
            this.lbBarCode.SizeF = new System.Drawing.SizeF(270F, 28.00001F);
            this.lbBarCode.StylePriority.UsePadding = false;
            this.lbBarCode.StylePriority.UseTextAlignment = false;
            this.lbBarCode.Symbology = code128Generator1;
            this.lbBarCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lbProduct
            // 
            this.lbProduct.CanGrow = false;
            this.lbProduct.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DESC")});
            this.lbProduct.Font = new DevExpress.Drawing.DXFont("Arial", 8.25F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {
            new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.lbProduct.LocationFloat = new DevExpress.Utils.PointFloat(5F, 16F);
            this.lbProduct.Multiline = true;
            this.lbProduct.Name = "lbProduct";
            this.lbProduct.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.lbProduct.SizeF = new System.Drawing.SizeF(270F, 28F);
            this.lbProduct.StylePriority.UsePadding = false;
            this.lbProduct.Text = "[DESC]";
            this.lbProduct.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lbSKU
            // 
            this.lbSKU.CanGrow = false;
            this.lbSKU.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "SKU")});
            this.lbSKU.Font = new DevExpress.Drawing.DXFont("Arial", 72F);
            this.lbSKU.LocationFloat = new DevExpress.Utils.PointFloat(5F, 0F);
            this.lbSKU.Name = "lbSKU";
            this.lbSKU.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.lbSKU.SizeF = new System.Drawing.SizeF(270F, 16F);
            this.lbSKU.StylePriority.UseFont = false;
            this.lbSKU.StylePriority.UsePadding = false;
            this.lbSKU.StylePriority.UseTextAlignment = false;
            this.lbSKU.Text = "[SKU]";
            this.lbSKU.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
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
            // OneUpCustom
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.topMarginBand1,
            this.bottomMarginBand1});
            this.DataSource = this.dataSet1;
            this.Margins = new DevExpress.Drawing.DXMargins(0F, 0F, 0F, 0F);
            this.PageHeight = 125;
            this.PageWidth = 280;
            this.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.Custom;
            this.Version = "23.1";
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion
        private System.Data.DataSet dataSet1;
        public DevExpress.XtraReports.UI.XRLabel lbPrice;
        public DevExpress.XtraReports.UI.XRBarCode lbBarCode;
        public DevExpress.XtraReports.UI.XRLabel lbProduct;
        public DevExpress.XtraReports.UI.XRLabel lbSKU;
        private DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
        private DevExpress.XtraReports.UI.BottomMarginBand bottomMarginBand1;
        public DevExpress.XtraReports.UI.DetailBand Detail;
    }
}
