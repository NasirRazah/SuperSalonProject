namespace OfflineRetailV2.Report.Invoice
{
    partial class repReceiptMain
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
            this.srGC = new DevExpress.XtraReports.UI.XRSubreport();
            this.srChange = new DevExpress.XtraReports.UI.XRSubreport();
            this.srFooter = new DevExpress.XtraReports.UI.XRSubreport();
            this.srTender = new DevExpress.XtraReports.UI.XRSubreport();
            this.srTaxTotal = new DevExpress.XtraReports.UI.XRSubreport();
            this.srItem = new DevExpress.XtraReports.UI.XRSubreport();
            this.srSubheader = new DevExpress.XtraReports.UI.XRSubreport();
            this.srHeader = new DevExpress.XtraReports.UI.XRSubreport();
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.srGC,
            this.srChange,
            this.srFooter,
            this.srTender,
            this.srTaxTotal,
            this.srItem,
            this.srSubheader,
            this.srHeader});
            this.Detail.HeightF = 241F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // srGC
            // 
            this.srGC.LocationFloat = new DevExpress.Utils.PointFloat(0F, 182F);
            this.srGC.Name = "srGC";
            this.srGC.SizeF = new System.Drawing.SizeF(100F, 25F);
            // 
            // srChange
            // 
            this.srChange.LocationFloat = new DevExpress.Utils.PointFloat(0F, 154F);
            this.srChange.Name = "srChange";
            this.srChange.SizeF = new System.Drawing.SizeF(100F, 25F);
            // 
            // srFooter
            // 
            this.srFooter.LocationFloat = new DevExpress.Utils.PointFloat(0F, 209F);
            this.srFooter.Name = "srFooter";
            this.srFooter.SizeF = new System.Drawing.SizeF(100F, 25F);
            // 
            // srTender
            // 
            this.srTender.LocationFloat = new DevExpress.Utils.PointFloat(0F, 124F);
            this.srTender.Name = "srTender";
            this.srTender.SizeF = new System.Drawing.SizeF(100F, 25F);
            // 
            // srTaxTotal
            // 
            this.srTaxTotal.LocationFloat = new DevExpress.Utils.PointFloat(0F, 96F);
            this.srTaxTotal.Name = "srTaxTotal";
            this.srTaxTotal.SizeF = new System.Drawing.SizeF(100F, 25F);
            // 
            // srItem
            // 
            this.srItem.LocationFloat = new DevExpress.Utils.PointFloat(0F, 67F);
            this.srItem.Name = "srItem";
            this.srItem.SizeF = new System.Drawing.SizeF(100F, 25F);
            // 
            // srSubheader
            // 
            this.srSubheader.LocationFloat = new DevExpress.Utils.PointFloat(0F, 38F);
            this.srSubheader.Name = "srSubheader";
            this.srSubheader.SizeF = new System.Drawing.SizeF(100F, 25F);
            // 
            // srHeader
            // 
            this.srHeader.LocationFloat = new DevExpress.Utils.PointFloat(0F, 8F);
            this.srHeader.Name = "srHeader";
            this.srHeader.SizeF = new System.Drawing.SizeF(100F, 25F);
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
            // repReceiptMain
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.topMarginBand1,
            this.bottomMarginBand1});
            this.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.Margins = new DevExpress.Drawing.DXMargins(200, 200, 20, 20);
            this.Version = "10.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        public DevExpress.XtraReports.UI.XRSubreport srSubheader;
        public DevExpress.XtraReports.UI.XRSubreport srHeader;
        public DevExpress.XtraReports.UI.XRSubreport srTender;
        public DevExpress.XtraReports.UI.XRSubreport srTaxTotal;
        public DevExpress.XtraReports.UI.XRSubreport srItem;
        public DevExpress.XtraReports.UI.XRSubreport srFooter;
        public DevExpress.XtraReports.UI.DetailBand Detail;
        public DevExpress.XtraReports.UI.XRSubreport srGC;
        public DevExpress.XtraReports.UI.XRSubreport srChange;
        private DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
        private DevExpress.XtraReports.UI.BottomMarginBand bottomMarginBand1;
    }
}
