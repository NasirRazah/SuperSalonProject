namespace OfflineRetailV2.Report.Product
{
    partial class Avery8195
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
            this.lbBarCode = new DevExpress.XtraReports.UI.XRBarCode();
            this.lbPrice = new DevExpress.XtraReports.UI.XRLabel();
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
            this.xrPanel1});
            this.Detail.HeightF = 66F;
            this.Detail.MultiColumn.ColumnSpacing = 35F;
            this.Detail.MultiColumn.ColumnWidth = 175F;
            this.Detail.MultiColumn.Layout = DevExpress.XtraPrinting.ColumnLayout.AcrossThenDown;
            this.Detail.MultiColumn.Mode = DevExpress.XtraReports.UI.MultiColumnMode.UseColumnWidth;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.Detail.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.Detail_BeforePrint);
            // 
            // xrPanel1
            // 
            this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lbBarCode,
            this.lbPrice,
            this.lbProduct,
            this.lbSKU});
            this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPanel1.Name = "xrPanel1";
            this.xrPanel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrPanel1.SizeF = new System.Drawing.SizeF(173F, 65F);
            // 
            // lbBarCode
            // 
            this.lbBarCode.AutoModule = true;
            this.lbBarCode.BorderWidth = 0F;
            this.lbBarCode.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "SKU")});
            this.lbBarCode.Font = new DevExpress.Drawing.DXFont("Arial", 6F);
            this.lbBarCode.LocationFloat = new DevExpress.Utils.PointFloat(2F, 1.00001F);
            this.lbBarCode.Module = 0.49F;
            this.lbBarCode.Name = "lbBarCode";
            this.lbBarCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 0, 0, 100F);
            this.lbBarCode.ShowText = false;
            this.lbBarCode.SizeF = new System.Drawing.SizeF(161F, 28.99999F);
            this.lbBarCode.StylePriority.UseFont = false;
            this.lbBarCode.StylePriority.UsePadding = false;
            this.lbBarCode.StylePriority.UseTextAlignment = false;
            this.lbBarCode.Symbology = code128Generator1;
            this.lbBarCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lbBarCode.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.lbBarCode_BeforePrint);
            // 
            // lbPrice
            // 
            this.lbPrice.BorderWidth = 0F;
            this.lbPrice.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PRICE")});
            this.lbPrice.Font = new DevExpress.Drawing.DXFont("Times New Roman", 6.75F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {
            new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.lbPrice.LocationFloat = new DevExpress.Utils.PointFloat(117F, 30F);
            this.lbPrice.Name = "lbPrice";
            this.lbPrice.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbPrice.SizeF = new System.Drawing.SizeF(53F, 12F);
            this.lbPrice.Text = "lbPrice";
            this.lbPrice.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lbPrice.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.lbBarCode_BeforePrint);
            // 
            // lbProduct
            // 
            this.lbProduct.BorderWidth = 0F;
            this.lbProduct.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DESC")});
            this.lbProduct.Font = new DevExpress.Drawing.DXFont("Arial", 6.75F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {
            new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.lbProduct.LocationFloat = new DevExpress.Utils.PointFloat(0F, 42F);
            this.lbProduct.Multiline = true;
            this.lbProduct.Name = "lbProduct";
            this.lbProduct.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbProduct.SizeF = new System.Drawing.SizeF(170F, 22F);
            this.lbProduct.Text = "lbProduct";
            this.lbProduct.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.lbProduct.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.lbBarCode_BeforePrint);
            // 
            // lbSKU
            // 
            this.lbSKU.BorderWidth = 0F;
            this.lbSKU.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "SKU")});
            this.lbSKU.Font = new DevExpress.Drawing.DXFont("Arial", 6.75F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {
            new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.lbSKU.LocationFloat = new DevExpress.Utils.PointFloat(0F, 30F);
            this.lbSKU.Name = "lbSKU";
            this.lbSKU.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbSKU.SizeF = new System.Drawing.SizeF(110F, 12F);
            this.lbSKU.Text = "lbSKU";
            this.lbSKU.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lbSKU.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.lbBarCode_BeforePrint);
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            // 
            // topMarginBand1
            // 
            this.topMarginBand1.HeightF = 50F;
            this.topMarginBand1.Name = "topMarginBand1";
            // 
            // bottomMarginBand1
            // 
            this.bottomMarginBand1.HeightF = 0F;
            this.bottomMarginBand1.Name = "bottomMarginBand1";
            // 
            // Avery8195
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.topMarginBand1,
            this.bottomMarginBand1});
            this.DataSource = this.dataSet1;
            this.Margins = new DevExpress.Drawing.DXMargins(19F, 0F, 50F, 0F);
            this.ReportPrintOptions.DetailCountOnEmptyDataSource = 60;
            this.Version = "23.1";
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRPanel xrPanel1;
        public DevExpress.XtraReports.UI.XRLabel lbSKU;
        public DevExpress.XtraReports.UI.XRLabel lbProduct;
        public DevExpress.XtraReports.UI.XRLabel lbPrice;
        public DevExpress.XtraReports.UI.XRBarCode lbBarCode;
        private System.Data.DataSet dataSet1;
        private DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
        private DevExpress.XtraReports.UI.BottomMarginBand bottomMarginBand1;
    }
}
