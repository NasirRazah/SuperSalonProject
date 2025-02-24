namespace OfflineRetailV2.Report.Sales
{
    partial class repInvSign
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
            this.rsign2 = new DevExpress.XtraReports.UI.XRLabel();
            this.rsign1 = new DevExpress.XtraReports.UI.XRLine();
            this.rTxt = new DevExpress.XtraReports.UI.XRLabel();
            this.rsign3 = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.HeightF = 3.125F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // rsign2
            // 
            this.rsign2.Font = new DevExpress.Drawing.DXFont("Segoe UI Light", 12F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rsign2.ForeColor = System.Drawing.Color.SlateGray;
            this.rsign2.LocationFloat = new DevExpress.Utils.PointFloat(460.9999F, 89.58337F);
            this.rsign2.Name = "rsign2";
            this.rsign2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rsign2.SizeF = new System.Drawing.SizeF(309F, 25F);
            this.rsign2.StylePriority.UseFont = false;
            this.rsign2.StylePriority.UseForeColor = false;
            this.rsign2.Text = "S   I   G   N   A   T   U   R   E";
            this.rsign2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // rsign1
            // 
            this.rsign1.ForeColor = System.Drawing.Color.SlateGray;
            this.rsign1.LocationFloat = new DevExpress.Utils.PointFloat(460.9999F, 76.70835F);
            this.rsign1.Name = "rsign1";
            this.rsign1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.rsign1.SizeF = new System.Drawing.SizeF(309F, 12.875F);
            this.rsign1.StylePriority.UseForeColor = false;
            // 
            // rTxt
            // 
            this.rTxt.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.25F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rTxt.LocationFloat = new DevExpress.Utils.PointFloat(460.9999F, 0F);
            this.rTxt.Name = "rTxt";
            this.rTxt.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rTxt.SizeF = new System.Drawing.SizeF(309F, 34F);
            this.rTxt.StylePriority.UseFont = false;
            this.rTxt.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // rsign3
            // 
            this.rsign3.Font = new DevExpress.Drawing.DXFont("Arial", 8.25F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rsign3.LocationFloat = new DevExpress.Utils.PointFloat(431.8333F, 47.91667F);
            this.rsign3.Name = "rsign3";
            this.rsign3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rsign3.SizeF = new System.Drawing.SizeF(16F, 17F);
            this.rsign3.Text = "X";
            this.rsign3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
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
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.rsign1,
            this.rsign3,
            this.rTxt,
            this.rsign2});
            this.ReportFooter.HeightF = 117.7083F;
            this.ReportFooter.Name = "ReportFooter";
            this.ReportFooter.PrintAtBottom = true;
            // 
            // repInvSign
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportFooter});
            this.Margins = new DevExpress.Drawing.DXMargins(50, 20, 0, 0);
            this.Version = "13.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        public DevExpress.XtraReports.UI.XRLabel rsign2;
        public DevExpress.XtraReports.UI.XRLine rsign1;
        public DevExpress.XtraReports.UI.XRLabel rTxt;
        public DevExpress.XtraReports.UI.XRLabel rsign3;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
    }
}
