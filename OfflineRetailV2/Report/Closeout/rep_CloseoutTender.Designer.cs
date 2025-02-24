namespace OfflineRetailV2.Report.Closeout
{
    partial class repCloseoutTender
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
            this.xrSubreport1 = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrSubreport4 = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrSubreport3 = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrSubreport2 = new DevExpress.XtraReports.UI.XRSubreport();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubreport1,
            this.xrSubreport4,
            this.xrSubreport3,
            this.xrSubreport2});
            this.Detail.HeightF = 769F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrSubreport1
            // 
            this.xrSubreport1.Id = 0;
            this.xrSubreport1.LocationFloat = new DevExpress.Utils.PointFloat(367F, 383F);
            this.xrSubreport1.Name = "xrSubreport1";
            this.xrSubreport1.SizeF = new System.Drawing.SizeF(325F, 367F);
            // 
            // xrSubreport4
            // 
            this.xrSubreport4.Id = 0;
            this.xrSubreport4.LocationFloat = new DevExpress.Utils.PointFloat(367F, 0F);
            this.xrSubreport4.Name = "xrSubreport4";
            this.xrSubreport4.SizeF = new System.Drawing.SizeF(325F, 375F);
            // 
            // xrSubreport3
            // 
            this.xrSubreport3.Id = 0;
            this.xrSubreport3.LocationFloat = new DevExpress.Utils.PointFloat(17F, 383F);
            this.xrSubreport3.Name = "xrSubreport3";
            this.xrSubreport3.SizeF = new System.Drawing.SizeF(325F, 367F);
            // 
            // xrSubreport2
            // 
            this.xrSubreport2.Id = 0;
            this.xrSubreport2.LocationFloat = new DevExpress.Utils.PointFloat(17F, 0F);
            this.xrSubreport2.Name = "xrSubreport2";
            this.xrSubreport2.SizeF = new System.Drawing.SizeF(325F, 375F);
            // 
            // PageHeader
            // 
            this.PageHeader.HeightF = 30F;
            this.PageHeader.Name = "PageHeader";
            this.PageHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // PageFooter
            // 
            this.PageFooter.HeightF = 30F;
            this.PageFooter.Name = "PageFooter";
            this.PageFooter.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.PageFooter.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
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
            // repCloseoutTender
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.PageFooter,
            this.topMarginBand1,
            this.bottomMarginBand1});
            this.Margins = new DevExpress.Drawing.DXMargins(50, 50, 20, 20);
            this.Version = "13.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        public DevExpress.XtraReports.UI.XRSubreport xrSubreport2;
        public DevExpress.XtraReports.UI.XRSubreport xrSubreport4;
        public DevExpress.XtraReports.UI.XRSubreport xrSubreport3;
        public DevExpress.XtraReports.UI.XRSubreport xrSubreport1;
        private DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
        private DevExpress.XtraReports.UI.BottomMarginBand bottomMarginBand1;
    }
}
