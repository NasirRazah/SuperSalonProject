namespace OfflineRetailV2.Report.Product
{
    partial class repProductSnapDetail
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
            DevExpress.XtraPrinting.Shape.ShapeBracket shapeBracket2 = new DevExpress.XtraPrinting.Shape.ShapeBracket();
            DevExpress.XtraPrinting.Shape.ShapeBracket shapeBracket1 = new DevExpress.XtraPrinting.Shape.ShapeBracket();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.subrepbreakpack = new DevExpress.XtraReports.UI.XRSubreport();
            this.subrepTax = new DevExpress.XtraReports.UI.XRSubreport();
            this.subrepVendor = new DevExpress.XtraReports.UI.XRSubreport();
            this.subrepGeneral = new DevExpress.XtraReports.UI.XRSubreport();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrShape1 = new DevExpress.XtraReports.UI.XRShape();
            this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrShape2 = new DevExpress.XtraReports.UI.XRShape();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.subrepbreakpack,
            this.subrepTax,
            this.subrepVendor,
            this.subrepGeneral});
            this.Detail.HeightF = 131F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // subrepbreakpack
            // 
            this.subrepbreakpack.Id = 0;
            this.subrepbreakpack.LocationFloat = new DevExpress.Utils.PointFloat(0F, 100F);
            this.subrepbreakpack.Name = "subrepbreakpack";
            this.subrepbreakpack.SizeF = new System.Drawing.SizeF(100F, 25F);
            // 
            // subrepTax
            // 
            this.subrepTax.Id = 0;
            this.subrepTax.LocationFloat = new DevExpress.Utils.PointFloat(0F, 66F);
            this.subrepTax.Name = "subrepTax";
            this.subrepTax.SizeF = new System.Drawing.SizeF(100F, 25F);
            // 
            // subrepVendor
            // 
            this.subrepVendor.Id = 0;
            this.subrepVendor.LocationFloat = new DevExpress.Utils.PointFloat(0F, 33F);
            this.subrepVendor.Name = "subrepVendor";
            this.subrepVendor.SizeF = new System.Drawing.SizeF(100F, 25F);
            // 
            // subrepGeneral
            // 
            this.subrepGeneral.Id = 0;
            this.subrepGeneral.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.subrepGeneral.Name = "subrepGeneral";
            this.subrepGeneral.SizeF = new System.Drawing.SizeF(100F, 25F);
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
            // xrShape1
            // 
            this.xrShape1.ForeColor = System.Drawing.Color.Gray;
            this.xrShape1.LocationFloat = new DevExpress.Utils.PointFloat(574.8542F, 4.000042F);
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
            this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(588.3959F, 8.000007F);
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
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(676.2709F, 8.000007F);
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
            this.xrShape2.LocationFloat = new DevExpress.Utils.PointFloat(719.3958F, 4.000042F);
            this.xrShape2.Name = "xrShape2";
            this.xrShape2.Shape = shapeBracket1;
            this.xrShape2.SizeF = new System.Drawing.SizeF(12F, 27.62497F);
            this.xrShape2.StylePriority.UseForeColor = false;
            // 
            // repProductSnapDetail
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageFooter,
            this.topMarginBand1,
            this.bottomMarginBand1});
            this.Margins = new DevExpress.Drawing.DXMargins(50, 50, 50, 50);
            this.Version = "13.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        public DevExpress.XtraReports.UI.XRSubreport subrepVendor;
        public DevExpress.XtraReports.UI.XRSubreport subrepGeneral;
        public DevExpress.XtraReports.UI.XRSubreport subrepTax;
        public DevExpress.XtraReports.UI.XRSubreport subrepbreakpack;
        private DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
        private DevExpress.XtraReports.UI.BottomMarginBand bottomMarginBand1;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo1;
        private DevExpress.XtraReports.UI.XRShape xrShape2;
        private DevExpress.XtraReports.UI.XRShape xrShape1;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo2;
    }
}
