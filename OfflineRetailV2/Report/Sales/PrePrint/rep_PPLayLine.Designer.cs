namespace OfflineRetailV2.Report.Sales.PrePrint
{
    partial class repPPLayLine
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
            this.rdeposit = new DevExpress.XtraReports.UI.XRLabel();
            this.rdeposittxt = new DevExpress.XtraReports.UI.XRLabel();
            this.rDisc = new DevExpress.XtraReports.UI.XRLabel();
            this.rTot = new DevExpress.XtraReports.UI.XRLabel();
            this.rRate = new DevExpress.XtraReports.UI.XRLabel();
            this.rQty = new DevExpress.XtraReports.UI.XRLabel();
            this.rItem = new DevExpress.XtraReports.UI.XRLabel();
            this.rSKU = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.rInv = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.rdeposit,
            this.rdeposittxt});
            this.Detail.HeightF = 20F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // rdeposit
            // 
            this.rdeposit.Font = new DevExpress.Drawing.DXFont("Arial", 8.25F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rdeposit.LocationFloat = new DevExpress.Utils.PointFloat(691.2081F, 0F);
            this.rdeposit.Name = "rdeposit";
            this.rdeposit.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rdeposit.SizeF = new System.Drawing.SizeF(105.2083F, 20F);
            this.rdeposit.StylePriority.UseFont = false;
            this.rdeposit.StylePriority.UseTextAlignment = false;
            this.rdeposit.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // rdeposittxt
            // 
            this.rdeposittxt.Font = new DevExpress.Drawing.DXFont("Arial", 8.25F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rdeposittxt.LocationFloat = new DevExpress.Utils.PointFloat(482.8749F, 0F);
            this.rdeposittxt.Multiline = true;
            this.rdeposittxt.Name = "rdeposittxt";
            this.rdeposittxt.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rdeposittxt.SizeF = new System.Drawing.SizeF(208.3332F, 20F);
            this.rdeposittxt.StylePriority.UseFont = false;
            // 
            // rDisc
            // 
            this.rDisc.Font = new DevExpress.Drawing.DXFont("Arial", 8.25F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rDisc.LocationFloat = new DevExpress.Utils.PointFloat(178.7083F, 40F);
            this.rDisc.Multiline = true;
            this.rDisc.Name = "rDisc";
            this.rDisc.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rDisc.SizeF = new System.Drawing.SizeF(403.17F, 15F);
            this.rDisc.StylePriority.UseFont = false;
            // 
            // rTot
            // 
            this.rTot.Font = new DevExpress.Drawing.DXFont("Arial", 8.25F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rTot.LocationFloat = new DevExpress.Utils.PointFloat(691.2081F, 20F);
            this.rTot.Name = "rTot";
            this.rTot.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rTot.SizeF = new System.Drawing.SizeF(105.2083F, 20F);
            this.rTot.StylePriority.UseFont = false;
            this.rTot.StylePriority.UseTextAlignment = false;
            this.rTot.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.rTot.PrintOnPage += new DevExpress.XtraReports.UI.PrintOnPageEventHandler(this.rTot_PrintOnPage);
            // 
            // rRate
            // 
            this.rRate.Font = new DevExpress.Drawing.DXFont("Arial", 8.25F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rRate.LocationFloat = new DevExpress.Utils.PointFloat(582.8748F, 20F);
            this.rRate.Name = "rRate";
            this.rRate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rRate.SizeF = new System.Drawing.SizeF(108.3333F, 20F);
            this.rRate.StylePriority.UseFont = false;
            this.rRate.StylePriority.UseTextAlignment = false;
            this.rRate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.rRate.PrintOnPage += new DevExpress.XtraReports.UI.PrintOnPageEventHandler(this.rRate_PrintOnPage);
            // 
            // rQty
            // 
            this.rQty.Font = new DevExpress.Drawing.DXFont("Arial", 8.25F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rQty.LocationFloat = new DevExpress.Utils.PointFloat(482.8749F, 20F);
            this.rQty.Name = "rQty";
            this.rQty.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rQty.SizeF = new System.Drawing.SizeF(99.99994F, 20F);
            this.rQty.StylePriority.UseFont = false;
            this.rQty.StylePriority.UseTextAlignment = false;
            this.rQty.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.rQty.PrintOnPage += new DevExpress.XtraReports.UI.PrintOnPageEventHandler(this.rQty_PrintOnPage);
            // 
            // rItem
            // 
            this.rItem.Font = new DevExpress.Drawing.DXFont("Arial", 8.25F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rItem.LocationFloat = new DevExpress.Utils.PointFloat(178.7083F, 20F);
            this.rItem.Multiline = true;
            this.rItem.Name = "rItem";
            this.rItem.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rItem.SizeF = new System.Drawing.SizeF(304.1666F, 20F);
            this.rItem.StylePriority.UseFont = false;
            // 
            // rSKU
            // 
            this.rSKU.Font = new DevExpress.Drawing.DXFont("Arial", 8.25F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rSKU.LocationFloat = new DevExpress.Utils.PointFloat(31.87499F, 20F);
            this.rSKU.Name = "rSKU";
            this.rSKU.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rSKU.SizeF = new System.Drawing.SizeF(144.7917F, 20F);
            this.rSKU.StylePriority.UseFont = false;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 0F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.rInv,
            this.rSKU,
            this.rItem,
            this.rQty,
            this.rRate,
            this.rTot,
            this.rDisc});
            this.ReportHeader.HeightF = 55F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // rInv
            // 
            this.rInv.Font = new DevExpress.Drawing.DXFont("Arial", 8.25F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rInv.LocationFloat = new DevExpress.Utils.PointFloat(31.87499F, 0F);
            this.rInv.Name = "rInv";
            this.rInv.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rInv.SizeF = new System.Drawing.SizeF(144.7917F, 20F);
            this.rInv.StylePriority.UseFont = false;
            // 
            // repPPLayLine
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader});
            this.Margins = new DevExpress.Drawing.DXMargins(0, 0, 0, 0);
            this.Version = "13.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        public DevExpress.XtraReports.UI.XRLabel rTot;
        public DevExpress.XtraReports.UI.XRLabel rRate;
        public DevExpress.XtraReports.UI.XRLabel rQty;
        public DevExpress.XtraReports.UI.XRLabel rItem;
        public DevExpress.XtraReports.UI.XRLabel rSKU;
        public DevExpress.XtraReports.UI.XRLabel rDisc;
        public DevExpress.XtraReports.UI.XRLabel rdeposittxt;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        public DevExpress.XtraReports.UI.XRLabel rInv;
        public DevExpress.XtraReports.UI.XRLabel rdeposit;
    }
}
