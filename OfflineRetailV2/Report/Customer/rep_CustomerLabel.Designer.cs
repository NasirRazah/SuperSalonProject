namespace OfflineRetailV2.Report.Customer
{
    partial class repCustomerLabel
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
            this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
            this.rAttnLb = new DevExpress.XtraReports.UI.XRLabel();
            this.rAttn = new DevExpress.XtraReports.UI.XRLabel();
            this.rAddress = new DevExpress.XtraReports.UI.XRLabel();
            this.rCompany = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.dataSet1 = new System.Data.DataSet();
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
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.Detail.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.Detail_BeforePrint);
            // 
            // xrPanel1
            // 
            this.xrPanel1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrPanel1.BorderWidth = 0;
            this.xrPanel1.CanGrow = false;
            this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.rAttn,
            this.rAddress,
            this.rCompany,
            this.rAttnLb});
            this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPanel1.Name = "xrPanel1";
            this.xrPanel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrPanel1.SizeF = new System.Drawing.SizeF(262F, 100F);
            // 
            // rAttnLb
            // 
            this.rAttnLb.BorderWidth = 0;
            this.rAttnLb.Font = new DevExpress.Drawing.DXFont("Arial", 9F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rAttnLb.LocationFloat = new DevExpress.Utils.PointFloat(3.999996F, 74.29166F);
            this.rAttnLb.Name = "rAttnLb";
            this.rAttnLb.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rAttnLb.SizeF = new System.Drawing.SizeF(38.45835F, 22.70833F);
            this.rAttnLb.StylePriority.UseFont = false;
            this.rAttnLb.Text = "Attn:";
            this.rAttnLb.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.rAttnLb.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.rCompany_BeforePrint);
            // 
            // rAttn
            // 
            this.rAttn.BorderWidth = 0;
            this.rAttn.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Attn")});
            this.rAttn.Font = new DevExpress.Drawing.DXFont("Arial", 9F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rAttn.LocationFloat = new DevExpress.Utils.PointFloat(42.45834F, 74.29166F);
            this.rAttn.Name = "rAttn";
            this.rAttn.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rAttn.SizeF = new System.Drawing.SizeF(216.5416F, 22.70833F);
            this.rAttn.Text = "rAttn";
            this.rAttn.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.rAttn.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.rCompany_BeforePrint);
            // 
            // rAddress
            // 
            this.rAddress.BorderWidth = 0;
            this.rAddress.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Address")});
            this.rAddress.Font = new DevExpress.Drawing.DXFont("Arial", 9F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rAddress.LocationFloat = new DevExpress.Utils.PointFloat(3.999996F, 29.41667F);
            this.rAddress.Multiline = true;
            this.rAddress.Name = "rAddress";
            this.rAddress.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rAddress.SizeF = new System.Drawing.SizeF(255F, 43.87499F);
            this.rAddress.Text = "rAddress";
            this.rAddress.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.rAddress.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.rCompany_BeforePrint);
            // 
            // rCompany
            // 
            this.rCompany.BorderWidth = 0;
            this.rCompany.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Company")});
            this.rCompany.Font = new DevExpress.Drawing.DXFont("Arial", 9F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
            this.rCompany.LocationFloat = new DevExpress.Utils.PointFloat(4F, 3.999996F);
            this.rCompany.Name = "rCompany";
            this.rCompany.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.rCompany.SizeF = new System.Drawing.SizeF(255F, 24.41666F);
            this.rCompany.Text = "rCompany";
            this.rCompany.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.rCompany.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.rCompany_BeforePrint);
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
            this.topMarginBand1.HeightF = 50F;
            this.topMarginBand1.Name = "topMarginBand1";
            // 
            // bottomMarginBand1
            // 
            this.bottomMarginBand1.HeightF = 0F;
            this.bottomMarginBand1.Name = "bottomMarginBand1";
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            // 
            // repCustomerLabel
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.PageFooter,
            this.topMarginBand1,
            this.bottomMarginBand1});
            this.DataSource = this.dataSet1;
            this.Margins = new DevExpress.Drawing.DXMargins(19, 0, 50, 0);
            this.Version = "10.1";
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        private DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
        private DevExpress.XtraReports.UI.BottomMarginBand bottomMarginBand1;
        private System.Data.DataSet dataSet1;
        private DevExpress.XtraReports.UI.XRPanel xrPanel1;
        public DevExpress.XtraReports.UI.XRLabel rAttn;
        public DevExpress.XtraReports.UI.XRLabel rAddress;
        public DevExpress.XtraReports.UI.XRLabel rCompany;
        public DevExpress.XtraReports.UI.XRLabel rAttnLb;
    }
}
