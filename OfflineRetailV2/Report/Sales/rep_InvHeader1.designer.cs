namespace OfflineRetailV2.Report.Sales
{
    partial class repInvHeader1
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
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.rRefundCaption = new DevExpress.XtraReports.UI.XRLabel();
            this.rPic = new DevExpress.XtraReports.UI.XRPictureBox();
            this.rTraining = new DevExpress.XtraReports.UI.XRLabel();
            this.rReportHeaderCompany = new DevExpress.XtraReports.UI.XRLabel();
            this.rType = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.rOrderNo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.rOrderDate = new DevExpress.XtraReports.UI.XRTableCell();
            this.rReprint = new DevExpress.XtraReports.UI.XRLabel();
            this.rReportHeader = new DevExpress.XtraReports.UI.XRLabel();
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.HeightF = 0F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.rRefundCaption,
            this.rPic,
            this.rTraining,
            this.rReportHeaderCompany,
            this.rType,
            this.xrTable1,
            this.rReprint,
            this.rReportHeader});
            this.PageHeader.HeightF = 137.625F;
            this.PageHeader.Name = "PageHeader";
            this.PageHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // rRefundCaption
            // 
            this.rRefundCaption.Font = new DevExpress.Drawing.DXFont("Segoe UI", 11.25F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {
            new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rRefundCaption.LocationFloat = new DevExpress.Utils.PointFloat(275.6684F, 113.5417F);
            this.rRefundCaption.Name = "rRefundCaption";
            this.rRefundCaption.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rRefundCaption.SizeF = new System.Drawing.SizeF(178.2065F, 21.45832F);
            this.rRefundCaption.StylePriority.UseFont = false;
            this.rRefundCaption.StylePriority.UseTextAlignment = false;
            this.rRefundCaption.Text = "Credit Invoice for Refund";
            this.rRefundCaption.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.rRefundCaption.Visible = false;
            // 
            // rPic
            // 
            this.rPic.LocationFloat = new DevExpress.Utils.PointFloat(322.25F, 18.00001F);
            this.rPic.Name = "rPic";
            this.rPic.SizeF = new System.Drawing.SizeF(91.66666F, 88.54169F);
            this.rPic.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // rTraining
            // 
            this.rTraining.Font = new DevExpress.Drawing.DXFont("Segoe UI", 11.25F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {
            new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rTraining.LocationFloat = new DevExpress.Utils.PointFloat(624.6667F, 115.1667F);
            this.rTraining.Name = "rTraining";
            this.rTraining.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rTraining.SizeF = new System.Drawing.SizeF(115.3333F, 21.45831F);
            this.rTraining.StylePriority.UseFont = false;
            this.rTraining.StylePriority.UseTextAlignment = false;
            this.rTraining.Text = "Training";
            this.rTraining.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.rTraining.Visible = false;
            // 
            // rReportHeaderCompany
            // 
            this.rReportHeaderCompany.BackColor = System.Drawing.Color.Transparent;
            this.rReportHeaderCompany.Font = new DevExpress.Drawing.DXFont("Segoe UI", 12F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {
            new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rReportHeaderCompany.LocationFloat = new DevExpress.Utils.PointFloat(428.8749F, 18F);
            this.rReportHeaderCompany.Name = "rReportHeaderCompany";
            this.rReportHeaderCompany.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rReportHeaderCompany.SizeF = new System.Drawing.SizeF(311.125F, 25F);
            this.rReportHeaderCompany.StylePriority.UseFont = false;
            this.rReportHeaderCompany.StylePriority.UseTextAlignment = false;
            this.rReportHeaderCompany.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // rType
            // 
            this.rType.Font = new DevExpress.Drawing.DXFont("Segoe UI", 14.25F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {
            new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rType.LocationFloat = new DevExpress.Utils.PointFloat(9.999998F, 20.00001F);
            this.rType.Name = "rType";
            this.rType.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rType.SizeF = new System.Drawing.SizeF(253.2101F, 35.99999F);
            this.rType.StylePriority.UseFont = false;
            this.rType.StylePriority.UseTextAlignment = false;
            this.rType.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTable1
            // 
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(10F, 60F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 96F);
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1,
            this.xrTableRow2});
            this.xrTable1.SizeF = new System.Drawing.SizeF(265.6684F, 69.99998F);
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell2,
            this.rOrderNo});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 96F);
            this.xrTableRow1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableRow1.Weight = 0.31578946672931008D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.BorderColor = System.Drawing.Color.SlateGray;
            this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrTableCell2.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9.75F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {
            new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 2, 0, 0, 100F);
            this.xrTableCell2.StylePriority.UseBorderColor = false;
            this.xrTableCell2.StylePriority.UseBorders = false;
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.StylePriority.UsePadding = false;
            this.xrTableCell2.Text = "Order # ";
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell2.Weight = 0.461646772201528D;
            // 
            // rOrderNo
            // 
            this.rOrderNo.BorderColor = System.Drawing.Color.SlateGray;
            this.rOrderNo.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
            this.rOrderNo.Font = new DevExpress.Drawing.DXFont("Segoe UI", 14.25F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {
            new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rOrderNo.Name = "rOrderNo";
            this.rOrderNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 2, 0, 0, 100F);
            this.rOrderNo.StylePriority.UseBorderColor = false;
            this.rOrderNo.StylePriority.UseBorders = false;
            this.rOrderNo.StylePriority.UseFont = false;
            this.rOrderNo.StylePriority.UsePadding = false;
            this.rOrderNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.rOrderNo.Weight = 0.53835322779847272D;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell4,
            this.rOrderDate});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 96F);
            this.xrTableRow2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableRow2.Weight = 0.31578946922257628D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.BorderColor = System.Drawing.Color.SlateGray;
            this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell4.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9.75F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {
            new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 2, 0, 0, 100F);
            this.xrTableCell4.StylePriority.UseBorderColor = false;
            this.xrTableCell4.StylePriority.UseBorders = false;
            this.xrTableCell4.StylePriority.UseFont = false;
            this.xrTableCell4.StylePriority.UsePadding = false;
            this.xrTableCell4.Text = "Order Date";
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell4.Weight = 0.46164676882725009D;
            // 
            // rOrderDate
            // 
            this.rOrderDate.BorderColor = System.Drawing.Color.SlateGray;
            this.rOrderDate.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.rOrderDate.Font = new DevExpress.Drawing.DXFont("Segoe UI", 12F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {
            new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rOrderDate.Name = "rOrderDate";
            this.rOrderDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 2, 0, 0, 100F);
            this.rOrderDate.StylePriority.UseBorderColor = false;
            this.rOrderDate.StylePriority.UseBorders = false;
            this.rOrderDate.StylePriority.UseFont = false;
            this.rOrderDate.StylePriority.UsePadding = false;
            this.rOrderDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.rOrderDate.Weight = 0.5383532311727498D;
            this.rOrderDate.PrintOnPage += new DevExpress.XtraReports.UI.PrintOnPageEventHandler(this.rOrderDate_PrintOnPage);
            // 
            // rReprint
            // 
            this.rReprint.Font = new DevExpress.Drawing.DXFont("Segoe UI", 9F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {
            new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rReprint.LocationFloat = new DevExpress.Utils.PointFloat(8F, 0F);
            this.rReprint.Name = "rReprint";
            this.rReprint.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rReprint.SizeF = new System.Drawing.SizeF(731.9999F, 17F);
            this.rReprint.StylePriority.UseFont = false;
            this.rReprint.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // rReportHeader
            // 
            this.rReportHeader.Font = new DevExpress.Drawing.DXFont("Segoe UI", 8.25F, DevExpress.Drawing.DXFontStyle.Italic, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {
            new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rReportHeader.LocationFloat = new DevExpress.Utils.PointFloat(428.8749F, 45.12502F);
            this.rReportHeader.Multiline = true;
            this.rReportHeader.Name = "rReportHeader";
            this.rReportHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rReportHeader.SizeF = new System.Drawing.SizeF(311.1251F, 64.49998F);
            this.rReportHeader.StylePriority.UseFont = false;
            this.rReportHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // topMarginBand1
            // 
            this.topMarginBand1.HeightF = 0F;
            this.topMarginBand1.Name = "topMarginBand1";
            // 
            // bottomMarginBand1
            // 
            this.bottomMarginBand1.HeightF = 1.250013F;
            this.bottomMarginBand1.Name = "bottomMarginBand1";
            // 
            // repInvHeader1
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.topMarginBand1,
            this.bottomMarginBand1});
            this.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {
            new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.Margins = new DevExpress.Drawing.DXMargins(50F, 50F, 0F, 1.250013F);
            this.Version = "23.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        public DevExpress.XtraReports.UI.XRLabel rReportHeader;
        public DevExpress.XtraReports.UI.XRLabel rReprint;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        public DevExpress.XtraReports.UI.XRTableCell rOrderNo;
        public DevExpress.XtraReports.UI.XRTableCell rOrderDate;
        private DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
        private DevExpress.XtraReports.UI.BottomMarginBand bottomMarginBand1;
        public DevExpress.XtraReports.UI.XRLabel rType;
        public DevExpress.XtraReports.UI.XRLabel rReportHeaderCompany;
        public DevExpress.XtraReports.UI.XRLabel rTraining;
        public DevExpress.XtraReports.UI.XRPictureBox rPic;
        public DevExpress.XtraReports.UI.XRLabel rRefundCaption;
        public DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        public DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
    }
}
