using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Threading;
using Size = System.Drawing.Size;

namespace OfflineRetailV2.Data
{
    public class SecondMonitor
    {
        private static Form frm_sm;

        // Cart Grid and Column
        private static GridControl grd;
        private static DevExpress.XtraGrid.Views.Grid.GridView gridview1;
        private static GridColumn colID;
        private static GridColumn colProduct;
        private static GridColumn colQty;
        private static GridColumn colRate;
        private static GridColumn colPrice;
        private static GridColumn colProductType;
        private static GridColumn colMID;
        private static GridColumn colMOV1;
        private static GridColumn colMOV2;
        private static GridColumn colMOV3;
        private static GridColumn colDP;
        private static GridColumn colUOMC;
        private static GridColumn colNotes;
        private static GridColumn colDisc;
        private static GridColumn colDiscountID;
        private static GridColumn colDiscountText;
        private static GridColumn colIndex;
        private static GridColumn colDLogic;
        private static GridColumn colDVal;
        private static GridColumn colService;
        private static GridColumn colRentType;
        private static GridColumn colRentDuration;
        private static GridColumn colRentAmount;
        private static GridColumn colrepairsl;
        private static GridColumn colrepairtag;
        private static GridColumn colrepairpurchase;
        private static GridColumn colBuyGetFreeHeader;
        private static GridColumn colBuyGetFreeCat;
        private static GridColumn colPromotion;

        private static GridColumn colGRate;
        private static GridColumn colGPrice;
        private static GridColumn colUOM;

        private static RepositoryItemMemoEdit repprod;
        private static RepositoryItemTextEdit rep2;
        private static RepositoryItemTextEdit rep3;

        private static StyleFormatCondition styleFormatCondition1;
        private static StyleFormatCondition styleFormatCondition2;
        private static StyleFormatCondition styleFormatCondition3;
        private static PictureBox pic;
        private static Panel pnlborderleft;
        private static Panel pnlborderright;
        private static Panel pnlgridseparator;
        private static Panel pnlbottom;
        private static Panel pnlbottom1;
        private static Panel pnlup;
        private static Panel pnlright;
        private static Panel pnlclient;
        private static Panel pnlpic;
        private static Panel pnltender;

        private static Panel pnlweight;

        private static Panel pnltenderinfo;
        private static Panel pnlLaneClosed;
        private static Panel pnlScaleInfo;

        private static Panel pnltenderT1; // top of right panel
        private static Panel pnltenderT2; // next below pnltenderT1 // display of total, tax amt
        private static Panel pnltenderT3; // next below pnltenderT2 // display of product image , while tendering visible false
        private static Panel pnltenderT4; // below tender screen show due,change amount, visible while tendering 

        private static Panel pnltop1; // subtotal
        private static Panel pnltop2; // item discount
        private static Panel pnltop3; // tax
        private static Panel pnltop4; // total
        private static Panel pnltop5; //security deposit
        private static Panel pnltop6; // ticket discount
        private static Panel pnltop7; // Fees & Charges
        private static Panel pnlline1;
        private static Panel pnltopr2;
        private static Panel pnltopr3;
        private static Panel pnltopr4;

        private static Label lbSD1;
        private static Label lbSD2;
        private static Label lbST1;
        private static Label lbST2;
        private static Label lbTX1;
        private static Label lbTX2;
        private static Label lbD1;
        private static Label lbD2;
        private static Label lbTD1;
        private static Label lbTD2;
        private static Label lbT1;
        private static Label lbT2;
        private static Label lbF1;
        private static Label lbF2;

        private static Panel pnlTAmt;
        private static Panel pnlBAmt;
        private static Panel pnlCAmt;

        private static Label lbTT1;
        private static Label lbTT2;
        private static Label lbBB1;
        private static Label lbBB2;
        private static Label lbCC1;
        private static Label lbCC2;

        private static Label lbTran1;

        private static Label lbTran2;
        private static Label lbCustName;
        private static Label lbWeight;
        private static Label lbWeight_Value;
        private static Label lbWeight_Info1;
        private static Label lbWeight_Info2;

        private static Label lbLane;

        private static Label lbScaleProductHeader;
        private static Label lbScaleTareHeader;
        private static Label lbScaleWeightHeader;
        private static Label lbScaleTPHeader;
        private static Label lbScaleUPHeader;

        private static Label lbScaleProduct;
        private static Label lbScaleTare;
        private static Label lbScaleWeight;
        private static Label lbScaleUP;
        private static Label lbScaleTP;

        private static Panel pnlScaleValue;
        private static Panel pnlScaleTP;
        private static Panel pnlScaleUP;
        private static Panel pnlScaleP;
        private static Panel pnlScaleT;
        private static Panel pnlScaleW;
        private static Panel pnlScaleGraduation;
        private static Label lbScaleGraduation;

        private static GridControl grdTender;
        private static DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private static GridColumn colTID;
        private static GridColumn colTenderType;
        private static GridColumn colAmount;
        private static GridColumn colGC;
        private static GridColumn colGCFlag;
        private static GridColumn gridColumn1;
        private static GridColumn colGCOld;
        private static GridColumn colGCOldAmt;

        private static Panel pnlbleft;
        private static Panel pnlbright;
        private static Panel pnlbup;
        private static Panel pnlbdown;

        private static Panel pnlad;

        private static Panel pnlad1;
        private static Panel pnlad2;
        private static Panel pnlad3;

        private static Panel pnladb1;
        private static Panel pnladb2;

        private static Random rnd;

        private static DataTable dtblAdImage = null;

        private static int CurrentSequenceNo = 0;

        private static int PrevSequenceNo = 0;

        private static Timer tmr;

        private static bool timertick = false;

        private static bool SingleLoad = false;

        // Create New Instance and initialize controls

        public SecondMonitor(bool flag)
        {
            timertick = flag;

            bool IsOpen = false;
            bool IsOpenWinPlayer = false;
            foreach (Window f in System.Windows.Application.Current.Windows)
            {
                if (f.Name == "SM")
                {
                    IsOpen = true;
                    break;
                }
            }

            foreach (Window f in System.Windows.Application.Current.Windows)
            {
                if (f.Name == "WnPlayer")
                {
                    IsOpenWinPlayer = true;
                    break;
                }
            }

            if (IsOpenWinPlayer)
            {
                WinMediaPlayer.CloseWinPlayer();
            }

            if (!IsOpen)
            {
                Process[] pArry = Process.GetProcesses();
                foreach (Process p in pArry)
                {
                    int procid = p.Id;
                    if ((procid == SystemVariables.SecondMonitorAppID) && (SystemVariables.SecondMonitorAppID > 0))
                    {
                        try
                        {
                            p.Kill();
                        }
                        catch
                        {
                        }
                    }
                }

                int posL = 0;
                int posT = 0;
                int posW = 600;
                int posH = 800;

                if (Screen.AllScreens.Length >= Settings.ScreenCountForSecondMonitor)
                {
                    if (Screen.AllScreens.Length > 1)
                    {
                        if (Screen.AllScreens[1] != null)
                        {
                            Rectangle secondMonitor = Screen.AllScreens[1].WorkingArea;
                            posL = secondMonitor.Left;
                            posT = secondMonitor.Top;
                            posW = secondMonitor.Width;
                            posH = secondMonitor.Height;
                        }
                    }
                }

                frm_sm = new Form();
                frm_sm.FormBorderStyle = FormBorderStyle.None;
                frm_sm.StartPosition = FormStartPosition.Manual;
                frm_sm.WindowState = FormWindowState.Maximized;
                frm_sm.Left = posL;
                frm_sm.Top = posT;
                frm_sm.Width = posW;
                frm_sm.Height = posH;
                //frm_sm.TopMost = true;
                frm_sm.Name = "SM";
                rnd = new Random();
                PrevSequenceNo = 0;
                CurrentSequenceNo = 0;
                frm_sm.Show();

                InitializeComponent(); // Initialize controls on Load

                if (Settings.TaxInclusive == "Y")
                {
                    colRate.Visible = colPrice.Visible = false;
                }

                if (Settings.TaxInclusive == "N")
                {
                    colGRate.Visible = colGPrice.Visible = false;
                }
                SetDynamicScreenAdvertisementArea();
                AdvertisementAreaDisplaySettings(); // Set Display Advertisement Area
                tmr.Stop();
                tmr.Interval = 100;
                tmr.Start();
            }
        }

        // Close Display Form

        public static void CloseSMForm()
        {
            tmr.Stop();
            tmr.Dispose();
            frm_sm.Close();
            frm_sm.Dispose();

        }

        // Show Display Form

        private static void CreateSMform()
        {
            frm_sm.StartPosition = FormStartPosition.Manual;
            frm_sm.WindowState = FormWindowState.Maximized;
            frm_sm.Left = 0;
            frm_sm.Top = 0;
            frm_sm.Width = 800;//Screen.PrimaryScreen.Bounds.Width;
            frm_sm.Height = 800; //Screen.PrimaryScreen.Bounds.Height;
            frm_sm.Show();
        }

        // Add controls in Runtime

        private void InitializeComponent()
        {
            pnlborderleft = new Panel();
            pnlborderright = new Panel();
            pnlgridseparator = new Panel();

            pnlbottom = new Panel();
            pnlbottom1 = new Panel();
            pnlup = new Panel();
            pnlright = new Panel();
            pnlclient = new Panel();
            pnlpic = new Panel();
            pnltender = new Panel();

            pnlweight = new Panel();

            pnltenderT1 = new Panel();
            pnltenderT2 = new Panel();
            pnltenderT3 = new Panel();
            pnltenderT4 = new Panel();

            pnltop1 = new Panel();
            pnltop2 = new Panel();
            pnltop3 = new Panel();
            pnltop4 = new Panel();
            pnltop5 = new Panel();
            pnltop6 = new Panel();
            pnltop7 = new Panel();

            pnlline1 = new Panel();

            pnltopr2 = new Panel();
            pnltopr3 = new Panel();
            pnltopr4 = new Panel();

            pnlbleft = new Panel();
            pnlbright = new Panel();
            pnlbup = new Panel();
            pnlbdown = new Panel();

            pnlad = new Panel();
            pnlad1 = new Panel();
            pnlad2 = new Panel();
            pnlad3 = new Panel();

            pnladb1 = new Panel();
            pnladb2 = new Panel();

            lbSD1 = new Label();
            lbSD2 = new Label();
            lbST1 = new Label();
            lbST2 = new Label();
            lbTX1 = new Label();
            lbTX2 = new Label();
            lbD1 = new Label();
            lbD2 = new Label();
            lbTD1 = new Label();
            lbTD2 = new Label();
            lbT1 = new Label();
            lbT2 = new Label();
            lbF1 = new Label();
            lbF2 = new Label();

            pnlTAmt = new Panel();
            pnlBAmt = new Panel();
            pnlCAmt = new Panel();

            lbTT1 = new Label();
            lbTT2 = new Label();
            lbBB1 = new Label();
            lbBB2 = new Label();
            lbCC1 = new Label();
            lbCC2 = new Label();

            lbCustName = new Label();
            lbTran1 = new Label();
            lbTran2 = new Label();

            lbWeight = new Label();
            lbWeight_Value = new Label();
            lbWeight_Info1 = new Label();
            lbWeight_Info2 = new Label();

            pnltenderinfo = new Panel();
            pnlLaneClosed = new Panel();
            pnlScaleInfo = new Panel();

            lbLane = new Label();

            pnlScaleP = new Panel();
            pnlScaleT = new Panel();
            pnlScaleW = new Panel();
            pnlScaleUP = new Panel();
            pnlScaleTP = new Panel();
            pnlScaleGraduation = new Panel();
            lbScaleProduct = new Label();
            lbScaleTare = new Label();
            lbScaleWeight = new Label();
            lbScaleProductHeader = new Label();
            lbScaleTareHeader = new Label();
            lbScaleWeightHeader = new Label();
            lbScaleTP = new Label();
            lbScaleTPHeader = new Label();
            lbScaleUP = new Label();
            lbScaleUPHeader = new Label();
            lbScaleGraduation = new Label();
            pnlScaleValue = new Panel();
            pnlScaleUP = new Panel();
            pnlScaleTP = new Panel();

            pic = new PictureBox();

            colID = new GridColumn();
            colProduct = new GridColumn();
            colQty = new GridColumn();
            colRate = new GridColumn();
            colPrice = new GridColumn();
            colProductType = new GridColumn();
            colMID = new GridColumn();
            colMOV1 = new GridColumn();
            colMOV2 = new GridColumn();
            colMOV3 = new GridColumn();
            colDP = new GridColumn();
            colUOMC = new GridColumn();
            colNotes = new GridColumn();
            colDisc = new GridColumn();
            colDiscountID = new GridColumn();
            colDiscountText = new GridColumn();
            colIndex = new GridColumn();
            colDLogic = new GridColumn();
            colDVal = new GridColumn();
            colService = new GridColumn();
            colRentType = new GridColumn();
            colRentDuration = new GridColumn();
            colRentAmount = new GridColumn();
            colrepairsl = new GridColumn();
            colrepairtag = new GridColumn();
            colrepairpurchase = new GridColumn();

            colBuyGetFreeHeader = new GridColumn();
            colBuyGetFreeCat = new GridColumn();
            colPromotion = new GridColumn();

            colGRate = new GridColumn();
            colGPrice = new GridColumn();
            colUOM = new GridColumn();

            repprod = new RepositoryItemMemoEdit();
            rep2 = new RepositoryItemTextEdit();
            rep3 = new RepositoryItemTextEdit();

            tmr = new Timer();
            grd = new GridControl();
            gridview1 = new GridView();
            grd.MainView = gridview1;

            pnltenderT1.SuspendLayout();
            pnltenderT2.SuspendLayout();
            pnltop1.SuspendLayout();
            pnltop2.SuspendLayout();
            pnltop3.SuspendLayout();
            pnltop4.SuspendLayout();
            pnltop5.SuspendLayout();
            pnltop6.SuspendLayout();
            pnltop7.SuspendLayout();
            pnlline1.SuspendLayout();
            pnlTAmt.SuspendLayout();
            pnlBAmt.SuspendLayout();
            pnlCAmt.SuspendLayout();
            pnltenderT3.SuspendLayout();
            pnltenderT4.SuspendLayout();
            pnlpic.SuspendLayout();
            pnlbottom.SuspendLayout();
            pnlbottom1.SuspendLayout();
            pnlup.SuspendLayout();
            pnlweight.SuspendLayout();
            pnlclient.SuspendLayout();
            pnltender.SuspendLayout();
            pnlright.SuspendLayout();
            pnlbleft.SuspendLayout();
            pnlbright.SuspendLayout();
            pnlbup.SuspendLayout();
            pnlbdown.SuspendLayout();
            pnlad.SuspendLayout();
            pnlad1.SuspendLayout();
            pnlad2.SuspendLayout();
            pnlad3.SuspendLayout();
            pnladb1.SuspendLayout();
            pnladb2.SuspendLayout();

            

            tmr.Enabled = false;
            tmr.Tick += new System.EventHandler(tmr_tick);

            grdTender = new DevExpress.XtraGrid.GridControl();
            gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            colTID = new DevExpress.XtraGrid.Columns.GridColumn();
            colTenderType = new DevExpress.XtraGrid.Columns.GridColumn();
            colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            colGC = new DevExpress.XtraGrid.Columns.GridColumn();
            colGCFlag = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            colGCOld = new DevExpress.XtraGrid.Columns.GridColumn();
            colGCOldAmt = new DevExpress.XtraGrid.Columns.GridColumn();

            styleFormatCondition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            styleFormatCondition2 = new DevExpress.XtraGrid.StyleFormatCondition();
            styleFormatCondition3 = new DevExpress.XtraGrid.StyleFormatCondition();

            grd.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            grd.LookAndFeel.UseDefaultLookAndFeel = false;

            grd.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { rep2, rep3, repprod });
            grd.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridview1 });

            gridview1.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(240)))));
            gridview1.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Transparent;
            gridview1.Appearance.FocusedCell.Options.UseBackColor = true;
            gridview1.Appearance.FocusedCell.Options.UseForeColor = true;
            gridview1.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(240)))));
            gridview1.Appearance.FocusedRow.Options.UseBackColor = true;
            gridview1.ColumnPanelRowHeight = 50;

            gridview1.RowHeight = 50;

            gridview1.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(68)))), ((int)(((byte)(88)))));
            gridview1.Appearance.Empty.Options.UseBackColor = true;
            gridview1.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(240)))));
            gridview1.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Transparent;
            gridview1.Appearance.FocusedCell.Options.UseBackColor = true;
            gridview1.Appearance.FocusedCell.Options.UseForeColor = true;
            gridview1.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(240)))));
            gridview1.Appearance.FocusedRow.Options.UseBackColor = true;
            gridview1.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(47)))), ((int)(((byte)(65)))));
            gridview1.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            gridview1.Appearance.HeaderPanel.Options.UseBackColor = true;
            gridview1.Appearance.HeaderPanel.Options.UseForeColor = true;
            gridview1.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(110)))), ((int)(((byte)(193)))), ((int)(((byte)(218)))));
            gridview1.Appearance.Row.ForeColor = System.Drawing.Color.White;
            gridview1.Appearance.Row.Options.UseBackColor = true;
            gridview1.Appearance.Row.Options.UseForeColor = true;


            gridview1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            colID,
            colProduct,
            colQty,
            colRate,
            colPrice,
            colProductType,
            colMID,
            colMOV1,
            colMOV2,
            colMOV3,
            colDP,
            colUOMC,
            colNotes,
            colDisc,
            colDiscountID,
            colDiscountText,
            colIndex,
            colDLogic,
            colDVal,
            colService,
            colRentType,
            colRentDuration,
            colRentAmount,
            colrepairsl,
            colrepairtag,
            colrepairpurchase,
            colBuyGetFreeHeader,
            colBuyGetFreeCat,
            colPromotion,
            colGRate,
            colGPrice,
            colUOM});
            gridview1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;

            styleFormatCondition1.Value1 = "B";
            styleFormatCondition1.Column = colProductType;
            styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
            styleFormatCondition1.Appearance.BackColor = System.Drawing.Color.Gainsboro;
            styleFormatCondition1.Appearance.BackColor2 = System.Drawing.Color.Gainsboro;
            styleFormatCondition1.Appearance.Options.UseBackColor = true;
            styleFormatCondition1.ApplyToRow = true;

            styleFormatCondition2.Value1 = "C";
            styleFormatCondition2.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
            styleFormatCondition2.Column = gridview1.Columns["PRODUCTTYPE"];
            styleFormatCondition2.Appearance.Font = new System.Drawing.Font("Open Sans", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            styleFormatCondition2.Appearance.ForeColor = System.Drawing.Color.ForestGreen;
            styleFormatCondition2.Appearance.Options.UseFont = true;
            styleFormatCondition2.Appearance.Options.UseForeColor = true;
            styleFormatCondition2.ApplyToRow = true;

            styleFormatCondition3.Value1 = "Z";
            styleFormatCondition3.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
            styleFormatCondition3.Column = gridview1.Columns["PRODUCTTYPE"];
            styleFormatCondition3.Appearance.Font = new System.Drawing.Font("Open Sans", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            styleFormatCondition3.Appearance.ForeColor = System.Drawing.Color.SaddleBrown;
            styleFormatCondition3.Appearance.Options.UseFont = true;
            styleFormatCondition3.Appearance.Options.UseForeColor = true;
            styleFormatCondition3.ApplyToRow = true;


            gridview1.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] { styleFormatCondition1, styleFormatCondition2, styleFormatCondition3 });

            gridview1.GridControl = grd;
            gridview1.Name = "gridview1";
            gridview1.OptionsBehavior.Editable = false;
            gridview1.OptionsCustomization.AllowColumnMoving = false;
            gridview1.OptionsCustomization.AllowColumnResizing = false;
            gridview1.OptionsCustomization.AllowFilter = false;
            gridview1.OptionsCustomization.AllowSort = false;
            gridview1.OptionsView.RowAutoHeight = true;
            gridview1.OptionsView.ShowGroupPanel = false;
            gridview1.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            gridview1.OptionsView.ShowIndicator = false;
            gridview1.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;

            gridview1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridview1_FocusedRowChanged);
            gridview1.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(gridview1_CustomRowCellEdit);
            gridview1.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(gridview1_CustomDrawCell);

            // colID
            // 
            colID.Caption = Properties.Resources.ID;
            colID.FieldName = "ID";
            colID.Name = "colID";
            // 
            // colProduct
            // 
            colProduct.AppearanceCell.Font = new System.Drawing.Font("Open Sans", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            colProduct.AppearanceCell.Options.UseFont = true;
            colProduct.AppearanceCell.Options.UseTextOptions = true;
            colProduct.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            colProduct.AppearanceHeader.Font = new System.Drawing.Font("Open Sans", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            colProduct.AppearanceHeader.Options.UseFont = true;
            colProduct.Caption = Properties.Resources.Item;
            colProduct.ColumnEdit = repprod;
            colProduct.FieldName = "PRODUCT";
            colProduct.Name = "colProduct";
            colProduct.Visible = true;
            colProduct.VisibleIndex = 0;
            colProduct.Width = 158;
            // 
            // repprod
            // 
            repprod.Name = "repprod";
            repprod.ReadOnly = true;
            repprod.ScrollBars = System.Windows.Forms.ScrollBars.None;

            // colProductType
            // 
            colProductType.Caption = Properties.Resources.Type;
            colProductType.FieldName = "PRODUCTTYPE";
            colProductType.Name = "colProductType";
            // 
            // colQty
            // 
            colQty.AppearanceCell.Font = new System.Drawing.Font("Open Sans", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            colQty.AppearanceCell.Options.UseFont = true;
            colQty.AppearanceCell.Options.UseTextOptions = true;
            colQty.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colQty.AppearanceHeader.Font = new System.Drawing.Font("Open Sans", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            colQty.AppearanceHeader.Options.UseFont = true;
            colQty.AppearanceHeader.Options.UseTextOptions = true;
            colQty.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colQty.Caption = Properties.Resources.Qty;
            colQty.FieldName = "QTY";
            colQty.Name = "colQty";
            colQty.Visible = true;
            colQty.VisibleIndex = 1;
            colQty.Width = 45;
            // 
            // colRate
            // 
            colRate.AppearanceCell.Font = new System.Drawing.Font("Open Sans", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            colRate.AppearanceCell.Options.UseFont = true;
            colRate.AppearanceCell.Options.UseTextOptions = true;
            colRate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colRate.AppearanceHeader.Font = new System.Drawing.Font("Open Sans", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            colRate.AppearanceHeader.Options.UseFont = true;
            colRate.AppearanceHeader.Options.UseTextOptions = true;
            colRate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colRate.Caption = Properties.Resources.Price;
            colRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colRate.DisplayFormat.FormatString = "f";
            colRate.FieldName = "RATE";
            colRate.Name = "colRate";
            colRate.Visible = true;
            colRate.VisibleIndex = 2;
            colRate.Width = 55;
            // 
            // colPrice
            // 
            colPrice.AppearanceCell.Font = new System.Drawing.Font("Open Sans", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            colPrice.AppearanceCell.Options.UseFont = true;
            colPrice.AppearanceCell.Options.UseTextOptions = true;
            colPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colPrice.AppearanceHeader.Font = new System.Drawing.Font("Open Sans", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            colPrice.AppearanceHeader.Options.UseFont = true;
            colPrice.AppearanceHeader.Options.UseTextOptions = true;
            colPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colPrice.Caption = Properties.Resources.ExtPrice;

            colPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPrice.DisplayFormat.FormatString = "f";
            colPrice.FieldName = "PRICE";
            colPrice.Name = "colPrice";
            colPrice.Visible = true;
            colPrice.VisibleIndex = 3;
            colPrice.Width = 82;
            // 
            // colMID
            // 
            colMID.Caption = "MatxID";
            colMID.FieldName = "MATRIXOID";
            colMID.Name = "colMID";
            // 
            // colMOV1
            // 
            colMOV1.Caption = "MatxOV1";
            colMOV1.FieldName = "MATRIXOV1";
            colMOV1.Name = "colMOV1";
            // 
            // colMOV2
            // 
            colMOV2.Caption = "MatxOV2";
            colMOV2.FieldName = "MATRIXOV2";
            colMOV2.Name = "colMOV2";
            // 
            // colMOV3
            // 
            colMOV3.Caption = "MatxOV3";
            colMOV3.FieldName = "MATRIXOV3";
            colMOV3.Name = "colMOV3";
            // 
            // colDP
            // 
            colDP.Caption = Properties.Resources.Decimalplace;
            colDP.FieldName = "DP";
            colDP.Name = "colDP";
            // 
            // colUOMC
            // 
            colUOMC.Caption = Properties.Resources.UOMCount;
            colUOMC.FieldName = "UOMCount";
            colUOMC.Name = "colUOMC";
            // 
            // colNotes
            // 
            colNotes.Caption = Properties.Resources.Notes;
            colNotes.FieldName = "NOTES";
            colNotes.Name = "colNotes";
            // 
            // colDisc
            // 
            colDisc.Caption = Properties.Resources.Discount;
            colDisc.FieldName = "DISCOUNT";
            colDisc.Name = "colDisc";
            // 
            // colDiscountID
            // 
            colDiscountID.Caption = Properties.Resources.DiscountID;
            colDiscountID.FieldName = "DISCOUNTID";
            colDiscountID.Name = "colDiscountID";
            // 
            // colDiscountText
            // 
            colDiscountText.Caption = Properties.Resources.DiscountText;
            colDiscountText.FieldName = "DISCOUNTTEXT";
            colDiscountText.Name = "colDiscountText";
            // 
            // colIndex
            // 
            colIndex.Caption = "Top Index";
            colIndex.FieldName = "ITEMINDEX";
            colIndex.Name = "colIndex";
            // 
            // colDLogic
            // 
            colDLogic.Caption = Properties.Resources.DiscountLogic;
            colDLogic.FieldName = "DISCLOGIC";
            colDLogic.Name = "colDLogic";
            // 
            // colDVal
            // 
            colDVal.Caption = Properties.Resources.DiscountValue;
            colDVal.FieldName = "DISCVALUE";
            colDVal.Name = "colDVal";
            // 
            // colService
            // 
            colService.Caption = Properties.Resources.Service;
            colService.FieldName = "SERVICE";
            colService.Name = "colService";
            // 
            // colRentType
            // 
            colRentType.Caption = Properties.Resources.RentalType;
            colRentType.FieldName = "RENTTYPE";
            colRentType.Name = "colRentType";
            // 
            // colRentDuration
            // 
            colRentDuration.Caption = Properties.Resources.RentalDuration;
            colRentDuration.FieldName = "RENTDURATION";
            colRentDuration.Name = "colRentDuration";
            // 
            // colRentAmount
            // 
            colRentAmount.Caption = Properties.Resources.RentalAmount;
            colRentAmount.FieldName = "RENTAMOUNT";
            colRentAmount.Name = "colRentAmount";
            // 
            // colrepairsl
            // 
            colrepairsl.Caption = "gridColumn1";
            colrepairsl.FieldName = "REPAIRITEMSLNO";
            colrepairsl.Name = "colrepairsl";
            // 
            // colrepairtag
            // 
            colrepairtag.Caption = "gridColumn2";
            colrepairtag.FieldName = "REPAIRITEMTAG";
            colrepairtag.Name = "colrepairtag";
            // 
            // colrepairpurchase
            // 
            colrepairpurchase.Caption = "gridColumn3";
            colrepairpurchase.FieldName = "REPAIRITEMPURCHASEDATE";
            colrepairpurchase.Name = "colrepairpurchase";

            // 
            // colBuyGetFreeHeader
            // 
            colrepairpurchase.Caption = "gridColumn4";
            colrepairpurchase.FieldName = "BUYNGETFREEHEADERID";
            colrepairpurchase.Name = "colBuyGetFreeHeader";

            // 
            // colBuyGetFreeCat
            // 
            colBuyGetFreeCat.Caption = "gridColumn5";
            colBuyGetFreeCat.FieldName = "BUYNGETFREECATEGORY";
            colBuyGetFreeCat.Name = "colBuyGetFreeCat";

            // 
            // colPromotion
            // 
            colPromotion.Caption = "gridColumn6";
            colPromotion.FieldName = "BUYNGETFREENAME";
            colPromotion.Name = "colPromotion";

            // 
            // colGRate
            // 
            colGRate.AppearanceCell.Font = new System.Drawing.Font("Open Sans", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            colGRate.AppearanceCell.Options.UseFont = true;
            colGRate.AppearanceCell.Options.UseTextOptions = true;
            colGRate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colGRate.AppearanceHeader.Font = new System.Drawing.Font("Open Sans", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            colGRate.AppearanceHeader.Options.UseFont = true;
            colGRate.AppearanceHeader.Options.UseTextOptions = true;
            colGRate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colGRate.Caption = "Price";
            colGRate.FieldName = "GRATE";
            colGRate.Name = "colGRate";
            colGRate.Visible = true;
            colGRate.VisibleIndex = 4;
            // 
            // colGPrice
            // 
            colGPrice.AppearanceCell.Font = new System.Drawing.Font("Open Sans", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            colGPrice.AppearanceCell.Options.UseFont = true;
            colGPrice.AppearanceCell.Options.UseTextOptions = true;
            colGPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colGPrice.AppearanceHeader.Font = new System.Drawing.Font("Open Sans", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            colGPrice.AppearanceHeader.Options.UseFont = true;
            colGPrice.AppearanceHeader.Options.UseTextOptions = true;
            colGPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colGPrice.Caption = "Total";
            colGPrice.DisplayFormat.FormatString = "f";
            colGPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colGPrice.FieldName = "GPRICE";
            colGPrice.Name = "colGPrice";
            colGPrice.Visible = true;
            colGPrice.VisibleIndex = 5;
            // 
            // colUOM
            // 
            colUOM.Caption = "UOM";
            colUOM.FieldName = "UOM";
            colUOM.Name = "colUOM";
            // 

            // 
            // rep2
            // 
            rep2.AutoHeight = false;
            rep2.DisplayFormat.FormatString = "f";
            rep2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            rep2.Mask.EditMask = "f";
            rep2.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            rep2.Name = "rep2";
            rep2.ReadOnly = true;
            // 
            // rep3
            // 
            rep3.AutoHeight = false;
            rep3.DisplayFormat.FormatString = "f3";
            rep3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            rep3.Mask.EditMask = "f3";
            rep3.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            rep3.Name = "rep3";
            rep3.ReadOnly = true;
            // 

            // grdTender
            // 
            grdTender.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            grdTender.LookAndFeel.UseDefaultLookAndFeel = false;
            grdTender.EmbeddedNavigator.Name = "";
            grdTender.Location = new System.Drawing.Point(33, 43);
            grdTender.MainView = gridView2;
            grdTender.Name = "grdTender";
            grdTender.Size = new System.Drawing.Size(314, 142);
            grdTender.TabIndex = 103;
            grdTender.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView2});
            // 
            // gridView2
            // 

            gridView2.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(68)))), ((int)(((byte)(88)))));
            gridView2.Appearance.Empty.Options.UseBackColor = true;
            gridView2.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(240)))));
            gridView2.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Transparent;
            gridView2.Appearance.FocusedCell.Options.UseBackColor = true;
            gridView2.Appearance.FocusedCell.Options.UseForeColor = true;
            gridView2.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(240)))));
            gridView2.Appearance.FocusedRow.Options.UseBackColor = true;
            gridView2.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(47)))), ((int)(((byte)(65)))));
            gridView2.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
            gridView2.Appearance.HeaderPanel.Options.UseBackColor = true;
            gridView2.Appearance.HeaderPanel.Options.UseForeColor = true;
            gridView2.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(110)))), ((int)(((byte)(193)))), ((int)(((byte)(218)))));
            gridView2.Appearance.Row.ForeColor = System.Drawing.Color.White;
            gridView2.Appearance.Row.Options.UseBackColor = true;
            gridView2.Appearance.Row.Options.UseForeColor = true;

            gridView2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            gridView2.ColumnPanelRowHeight = 30;
            gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            colTID,
            colTenderType,
            colAmount,
            colGC,
            colGCFlag,
            gridColumn1,
            colGCOld,
            colGCOldAmt});
            gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            gridView2.GridControl = grdTender;
            gridView2.Name = "gridView2";
            gridView2.OptionsBehavior.Editable = false;
            gridView2.OptionsCustomization.AllowColumnMoving = false;
            gridView2.OptionsCustomization.AllowFilter = false;
            gridView2.OptionsCustomization.AllowSort = false;
            gridView2.OptionsMenu.EnableColumnMenu = false;
            gridView2.OptionsMenu.EnableFooterMenu = false;
            gridView2.OptionsMenu.EnableGroupPanelMenu = false;
            gridView2.OptionsView.ShowGroupPanel = false;
            gridView2.OptionsView.ShowIndicator = false;
            gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView2.OptionsSelection.EnableAppearanceFocusedRow = false;

            gridView2.CalcRowHeight += new DevExpress.XtraGrid.Views.Grid.RowHeightEventHandler(gridView2_CalcRowHeight);
            // 
            // colTID
            // 
            colTID.Caption = Properties.Resources.ID;
            colTID.FieldName = "ID";
            colTID.Name = "colTID";
            // 
            // colTenderType
            // 
            colTenderType.AppearanceCell.Font = new System.Drawing.Font("Open Sans", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            colTenderType.AppearanceCell.Options.UseFont = true;
            colTenderType.AppearanceHeader.Font = new System.Drawing.Font("Open Sans", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            colTenderType.AppearanceHeader.Options.UseFont = true;
            colTenderType.Caption = Properties.Resources.TenderType;
            colTenderType.FieldName = "TENDER";
            colTenderType.Name = "colTenderType";
            colTenderType.Width = 169;
            // 
            // colAmount
            // 
            colAmount.AppearanceCell.Font = new System.Drawing.Font("Open Sans", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            colAmount.AppearanceCell.Options.UseFont = true;
            colAmount.AppearanceHeader.Font = new System.Drawing.Font("Open Sans", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            colAmount.AppearanceHeader.Options.UseFont = true;
            colAmount.AppearanceHeader.Options.UseTextOptions = true;
            colAmount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colAmount.Caption = Properties.Resources.Amount;
            colAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colAmount.DisplayFormat.FormatString = "f";
            colAmount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            colAmount.FieldName = "AMOUNT";
            colAmount.Name = "colAmount";
            colAmount.Visible = true;
            colAmount.VisibleIndex = 1;
            colAmount.Width = 98;
            // 
            // colGC
            // 
            colGC.FieldName = "GIFTCERTIFICATE";
            colGC.Name = "colGC";
            // 
            // colGCFlag
            // 
            colGCFlag.Caption = "NEWGC";
            colGCFlag.FieldName = "NEWGC";
            colGCFlag.Name = "colGCFlag";
            // 
            // gridColumn1
            // 
            gridColumn1.AppearanceCell.Font = new System.Drawing.Font("Open Sans", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridColumn1.AppearanceCell.Options.UseFont = true;
            gridColumn1.AppearanceHeader.Font = new System.Drawing.Font("Open Sans", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridColumn1.AppearanceHeader.Options.UseFont = true;
            gridColumn1.Caption = Properties.Resources.TenderType;
            gridColumn1.FieldName = "DISPLAY";
            gridColumn1.Name = "gridColumn1";
            gridColumn1.Visible = true;
            gridColumn1.VisibleIndex = 0;
            // 
            // colGCOld
            // 
            colGCOld.Caption = "colGCOld";
            colGCOld.FieldName = "OLDGC";
            colGCOld.Name = "colGCOld";
            // 
            // colGCOldAmt
            // 
            colGCOldAmt.Caption = "gridColumn3";
            colGCOldAmt.FieldName = "OLDGCAMT";
            colGCOldAmt.Name = "colGCOldAmt";
            // 

            pnlborderleft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(47)))), ((int)(((byte)(65)))));

            pnlborderright.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(47)))), ((int)(((byte)(65)))));

            pnlgridseparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(47)))), ((int)(((byte)(65)))));

            pnlborderleft.Dock = DockStyle.Left;
            pnlborderright.Dock = DockStyle.Right;
            pnlgridseparator.Dock = DockStyle.Right;
            pnlborderright.Width = 5;
            pnlborderleft.Width = 5;
            pnlgridseparator.Width = 5;

            pnlbottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(47)))), ((int)(((byte)(65)))));
            //Color.Aqua;
            pnlbottom1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(47)))), ((int)(((byte)(65)))));
            //Color.Black;
            pnlup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(47)))), ((int)(((byte)(65)))));

            pnlweight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(47)))), ((int)(((byte)(65)))));

            pnlclient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(47)))), ((int)(((byte)(65)))));

            pnlright.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(47)))), ((int)(((byte)(65)))));

            pnlpic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(47)))), ((int)(((byte)(65)))));

            pnltender.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(47)))), ((int)(((byte)(65)))));


            pnltenderT1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(47)))), ((int)(((byte)(65)))));
            //Color.GhostWhite;
            pnltenderT2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(47)))), ((int)(((byte)(65)))));
            // Color.GhostWhite;
            pnltenderT3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(47)))), ((int)(((byte)(65)))));
            // Color.GhostWhite;
            pnltenderT4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(47)))), ((int)(((byte)(65)))));
            //Color.Black;

            pnlline1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(47)))), ((int)(((byte)(65)))));
            //Color.Black;

            pnltenderT1.Dock = DockStyle.Top;
            pnltenderT1.Height = 40;

            pnltenderT2.Dock = DockStyle.Top;
            //pnltenderT2.Height = 120;

            pnltop1.Dock = DockStyle.Top;
            pnltop2.Dock = DockStyle.Top;
            pnltop3.Dock = DockStyle.Top;
            pnltop7.Dock = DockStyle.Top;
            pnltop4.Dock = DockStyle.Top;
            pnltop5.Dock = DockStyle.Top;
            pnltop6.Dock = DockStyle.Top;

            pnlline1.Dock = DockStyle.Top;

            pnltop1.Height = 30;
            pnltop2.Height = 30;
            pnltop3.Height = 30;
            pnltop7.Height = 30;
            pnltop4.Height = 30;
            pnltop5.Height = 30;
            pnltop6.Height = 30;
            pnlline1.Height = 2;

            pnlTAmt.Height = 40;
            pnlBAmt.Height = 40;
            pnlCAmt.Height = 40;

            pnltopr2.Dock = DockStyle.Right;
            pnltopr3.Dock = DockStyle.Right;
            pnltopr4.Dock = DockStyle.Right;

            pnlTAmt.Dock = DockStyle.Top;
            pnlBAmt.Dock = DockStyle.Top;
            pnlCAmt.Dock = DockStyle.Top;

            lbST1.Dock = DockStyle.Left;
            lbST1.AutoSize = false;
            lbST1.Width = 170;
            lbST1.Font = new System.Drawing.Font("Open Sans", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbST1.Text = Properties.Resources.SubTotal;
            lbST1.ForeColor = Color.White;

            lbTX1.Dock = DockStyle.Left;
            lbTX1.AutoSize = false;
            lbTX1.Width = 170;
            lbTX1.Font = new System.Drawing.Font("Open Sans", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbTX1.Text = Properties.Resources.Tax;
            lbTX1.ForeColor = Color.White;

            lbD1.Dock = DockStyle.Left;
            lbD1.AutoSize = false;
            lbD1.Width = 170;
            lbD1.Font = new System.Drawing.Font("Open Sans", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbD1.Text = Properties.Resources.ItemDiscount;
            lbD1.ForeColor = Color.White;

            lbF1.Dock = DockStyle.Left;
            lbF1.AutoSize = false;
            lbF1.Width = 170;
            lbF1.Font = new System.Drawing.Font("Open Sans", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbF1.Text = Properties.Resources.FeesCharges;
            lbF1.ForeColor = Color.White;

            lbT1.Dock = DockStyle.Left;
            lbT1.AutoSize = false;
            lbT1.Width = 170;
            lbT1.Font = new System.Drawing.Font("Open Sans", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbT1.Text = Properties.Resources.Total;
            lbT1.ForeColor = Color.White;

            lbSD1.Dock = DockStyle.Left;
            lbSD1.AutoSize = false;
            lbSD1.Width = 170;
            lbSD1.Font = new System.Drawing.Font("Open Sans", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbSD1.Text = Properties.Resources.SecurityDeposit;
            lbSD1.ForeColor = Color.White;

            lbTD1.Dock = DockStyle.Left;
            lbTD1.AutoSize = false;
            lbTD1.Width = 170;
            lbTD1.Font = new System.Drawing.Font("Open Sans", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbTD1.Text = Properties.Resources.TicketDiscount;
            lbTD1.ForeColor = Color.White;

            lbTT1.Dock = DockStyle.Left;
            lbTT1.AutoSize = false;
            lbTT1.TextAlign = ContentAlignment.MiddleLeft;
            lbTT1.Width = 200;
            lbTT1.Font = new System.Drawing.Font("Open Sans", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbTT1.Text = Properties.Resources.TenderAmount;
            lbTT1.ForeColor = Color.White;


            lbBB1.Dock = DockStyle.Left;
            lbBB1.AutoSize = false;
            lbBB1.TextAlign = ContentAlignment.MiddleLeft;
            lbBB1.Width = 170;
            lbBB1.Font = new System.Drawing.Font("Open Sans", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbBB1.Text = Properties.Resources.BalanceDue;
            lbBB1.ForeColor = Color.White;

            lbCC1.Dock = DockStyle.Left;
            lbCC1.AutoSize = false;
            lbCC1.TextAlign = ContentAlignment.MiddleLeft;
            lbCC1.Width = 170;
            lbCC1.Font = new System.Drawing.Font("Open Sans", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbCC1.Text = Properties.Resources.ChangeDue;
            lbCC1.ForeColor = Color.White;

            lbST2.Dock = DockStyle.Right;
            lbST2.TextAlign = ContentAlignment.MiddleRight;
            lbST2.Font = new System.Drawing.Font("Open Sans", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbST2.ForeColor = Color.White;

            lbTX2.Dock = DockStyle.Right;
            lbTX2.TextAlign = ContentAlignment.MiddleRight;
            lbTX2.Font = new System.Drawing.Font("Open Sans", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbTX2.ForeColor = Color.White;

            lbD2.Dock = DockStyle.Right;
            lbD2.TextAlign = ContentAlignment.MiddleRight;
            lbD2.Font = new System.Drawing.Font("Open Sans", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbD2.ForeColor = Color.White;

            lbF2.Dock = DockStyle.Right;
            lbF2.TextAlign = ContentAlignment.MiddleRight;
            lbF2.Font = new System.Drawing.Font("Open Sans", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbF2.ForeColor = Color.White;

            lbT2.Dock = DockStyle.Right;
            lbT2.TextAlign = ContentAlignment.MiddleRight;
            lbT2.Font = new System.Drawing.Font("Open Sans", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbT2.ForeColor = Color.White;

            lbSD2.Dock = DockStyle.Right;
            lbSD2.TextAlign = ContentAlignment.MiddleRight;
            lbSD2.Font = new System.Drawing.Font("Open Sans", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbSD2.ForeColor = Color.White;

            lbTD2.Dock = DockStyle.Right;
            lbTD2.TextAlign = ContentAlignment.MiddleRight;
            lbTD2.Font = new System.Drawing.Font("Open Sans", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbTD2.ForeColor = Color.White;

            lbTT2.Dock = DockStyle.Right;
            lbTT2.TextAlign = ContentAlignment.MiddleRight;
            lbTT2.Font = new System.Drawing.Font("Open Sans", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbTT2.ForeColor = Color.Aqua;

            lbBB2.Dock = DockStyle.Right;
            lbBB2.TextAlign = ContentAlignment.MiddleRight;
            lbBB2.Font = new System.Drawing.Font("Open Sans", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbBB2.ForeColor = Color.Aqua;

            lbCC2.Dock = DockStyle.Right;
            lbCC2.TextAlign = ContentAlignment.MiddleRight;
            lbCC2.Font = new System.Drawing.Font("Open Sans", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbCC2.ForeColor = Color.Aqua;

            lbLane.AutoSize = true;
            lbLane.TextAlign = ContentAlignment.MiddleCenter;
            lbLane.Font = new System.Drawing.Font("Book Antiqua", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbLane.Text = Properties.Resources.LaneClosed;
            lbLane.ForeColor = Color.White;


            lbTran1.AutoSize = false;
            lbTran1.Width = 500;
            lbTran1.Height = 50;
            lbTran1.TextAlign = ContentAlignment.MiddleCenter;
            lbTran1.Font = new System.Drawing.Font("Book Antiqua", 22F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbTran1.Text = Properties.Resources.ThankYouvisitagain;
            lbTran1.ForeColor = Color.White;

            lbTran2.AutoSize = false;
            lbTran2.Width = 400;
            lbTran2.Height = 50;
            lbTran2.TextAlign = ContentAlignment.MiddleCenter;
            lbTran2.Font = new System.Drawing.Font("Book Antiqua", 22F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbTran2.ForeColor = Color.Yellow;
            lbTran2.Text = "";

            lbScaleProductHeader.AutoSize = false;
            lbScaleProductHeader.TextAlign = ContentAlignment.MiddleLeft;
            lbScaleProductHeader.Font = new System.Drawing.Font("Open Sans", 28.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbScaleProductHeader.Text = "";
            lbScaleProductHeader.Width = 150;
            lbScaleProductHeader.ForeColor = Color.White;

            lbScaleTareHeader.AutoSize = false;
            lbScaleTareHeader.TextAlign = ContentAlignment.MiddleCenter;
            lbScaleTareHeader.Font = new System.Drawing.Font("Open Sans", 28.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbScaleTareHeader.Text = "";
            lbScaleTareHeader.Width = 150;
            lbScaleTareHeader.ForeColor = Color.White;

            lbScaleWeightHeader.AutoSize = false;
            lbScaleWeightHeader.TextAlign = ContentAlignment.MiddleCenter;
            lbScaleWeightHeader.Font = new System.Drawing.Font("Open Sans", 28.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbScaleWeightHeader.Text = "";
            lbScaleWeightHeader.Width = 150;
            lbScaleWeightHeader.ForeColor = Color.White;

            lbScaleUPHeader.AutoSize = false;
            lbScaleUPHeader.TextAlign = ContentAlignment.MiddleCenter;
            lbScaleUPHeader.Font = new System.Drawing.Font("Open Sans", 28.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbScaleUPHeader.Text = "";
            lbScaleUPHeader.Width = 150;
            lbScaleUPHeader.ForeColor = Color.White;

            lbScaleTPHeader.AutoSize = false;
            lbScaleTPHeader.TextAlign = ContentAlignment.MiddleCenter;
            lbScaleTPHeader.Font = new System.Drawing.Font("Open Sans", 28.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbScaleTPHeader.Text = "";
            lbScaleTPHeader.Width = 150;
            lbScaleTPHeader.ForeColor = Color.White;

            lbScaleProduct.AutoSize = false;
            lbScaleProduct.TextAlign = ContentAlignment.MiddleCenter;
            lbScaleProduct.Font = new System.Drawing.Font("Open Sans", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbScaleProduct.Text = "";
            lbScaleProduct.ForeColor = Color.White;

            lbScaleTare.AutoSize = false;
            lbScaleTare.TextAlign = ContentAlignment.MiddleCenter;
            lbScaleTare.Font = new System.Drawing.Font("Open Sans", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbScaleTare.Text = "";
            lbScaleTare.ForeColor = Color.Aqua;

            lbScaleWeight.AutoSize = false;
            lbScaleWeight.TextAlign = ContentAlignment.MiddleCenter;
            lbScaleWeight.Font = new System.Drawing.Font("Open Sans", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbScaleWeight.Text = "";
            lbScaleWeight.ForeColor = Color.Aqua;

            lbScaleUP.AutoSize = false;
            lbScaleUP.TextAlign = ContentAlignment.MiddleCenter;
            lbScaleUP.Font = new System.Drawing.Font("Open Sans", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbScaleUP.Text = "";
            lbScaleUP.ForeColor = Color.Aqua;

            lbScaleTP.AutoSize = false;
            lbScaleTP.TextAlign = ContentAlignment.MiddleCenter;
            lbScaleTP.Font = new System.Drawing.Font("Open Sans", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbScaleTP.Text = "";
            lbScaleTP.ForeColor = Color.Aqua;

            lbScaleGraduation.AutoSize = false;
            lbScaleGraduation.TextAlign = ContentAlignment.MiddleCenter;
            lbScaleGraduation.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbScaleGraduation.Text = "";
            lbScaleGraduation.ForeColor = Color.Gold;

            pnlScaleP.Dock = DockStyle.Top;
            pnlScaleP.Height = 100;
            lbScaleProduct.Dock = DockStyle.Top;
            pnlScaleP.Controls.Add(lbScaleProduct);

            pnlScaleT.Dock = DockStyle.Left;

            lbScaleTareHeader.Dock = DockStyle.Bottom;
            lbScaleTare.Dock = DockStyle.Top;
            pnlScaleT.Controls.Add(lbScaleTare);
            pnlScaleT.Controls.Add(lbScaleTareHeader);

            pnlScaleW.Dock = DockStyle.Left;
            lbScaleWeight.Dock = DockStyle.Top;
            lbScaleWeightHeader.Dock = DockStyle.Bottom;
            pnlScaleW.Controls.Add(lbScaleWeight);
            pnlScaleW.Controls.Add(lbScaleWeightHeader);

            pnlScaleUP.Dock = DockStyle.Left;
            lbScaleUP.Dock = DockStyle.Top;
            lbScaleUPHeader.Dock = DockStyle.Bottom;
            pnlScaleUP.Controls.Add(lbScaleUP);
            pnlScaleUP.Controls.Add(lbScaleUPHeader);

            pnlScaleTP.Dock = DockStyle.Left;
            lbScaleTP.Dock = DockStyle.Top;
            lbScaleTPHeader.Dock = DockStyle.Bottom;
            pnlScaleTP.Controls.Add(lbScaleTP);
            pnlScaleTP.Controls.Add(lbScaleTPHeader);

            pnlScaleGraduation.Dock = DockStyle.Bottom;
            pnlScaleGraduation.Height = 30;
            lbScaleGraduation.Dock = DockStyle.Top;
            pnlScaleGraduation.Controls.Add(lbScaleGraduation);

            pnlScaleInfo.Height = 400;
            pnlScaleInfo.Width = 600;
            pnlScaleInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            //Color.Black;

            pnlScaleValue.Controls.Add(pnlScaleTP);
            pnlScaleValue.Controls.Add(pnlScaleUP);
            pnlScaleValue.Controls.Add(pnlScaleW);
            pnlScaleValue.Controls.Add(pnlScaleT);
            pnlScaleValue.Controls.Add(pnlScaleGraduation);
            pnlScaleValue.Dock = System.Windows.Forms.DockStyle.Fill;

            pnlScaleInfo.Controls.Add(pnlScaleValue);
            pnlScaleInfo.Controls.Add(pnlScaleP);


            pnlLaneClosed.Height = 200;
            pnlLaneClosed.Width = 600;
            pnlLaneClosed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            //Color.Black;
            pnlLaneClosed.Controls.Add(lbLane);

            pnltenderinfo.Height = 200;
            pnltenderinfo.Width = 600;
            pnltenderinfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(88)))), ((int)(((byte)(88)))));
            //Color.Black;

            pnltenderinfo.Controls.Add(lbTran1);
            pnltenderinfo.Controls.Add(lbTran2);

            pnlTAmt.Controls.Add(lbTT1);
            pnlTAmt.Controls.Add(lbTT2);

            pnlBAmt.Controls.Add(lbBB1);
            pnlBAmt.Controls.Add(lbBB2);

            pnlCAmt.Controls.Add(lbCC1);
            pnlCAmt.Controls.Add(lbCC2);

            pnltenderT4.Controls.Add(pnlCAmt);
            pnltenderT4.Controls.Add(pnlBAmt);
            pnltenderT4.Controls.Add(pnlTAmt);

            pnltop5.Controls.Add(lbSD1);
            pnltop5.Controls.Add(lbSD2);

            pnltop1.Controls.Add(lbST1);
            pnltop1.Controls.Add(lbST2);

            pnltop2.Controls.Add(lbD1);
            pnltop2.Controls.Add(lbD2);

            pnltop3.Controls.Add(lbTX1);
            pnltop3.Controls.Add(lbTX2);

            pnltop4.Controls.Add(lbT1);
            pnltop4.Controls.Add(lbT2);

            pnltop6.Controls.Add(lbTD1);
            pnltop6.Controls.Add(lbTD2);

            pnltop7.Controls.Add(lbF1);
            pnltop7.Controls.Add(lbF2);

            pnltenderT2.Controls.Add(pnltop4);
            pnltenderT2.Controls.Add(pnlline1);
            pnltenderT2.Controls.Add(pnltop6);
            pnltenderT2.Controls.Add(pnltop7);
            pnltenderT2.Controls.Add(pnltop3);
            pnltenderT2.Controls.Add(pnltop2);
            pnltenderT2.Controls.Add(pnltop1);
            pnltenderT2.Controls.Add(pnltop5);

            pnltenderT3.Dock = DockStyle.Top;

            pnltenderT4.Dock = DockStyle.Bottom;
            pnltenderT4.Height = 100;

            pnlpic.Dock = DockStyle.Top;
            pnlbottom.Dock = DockStyle.Bottom;

            //pnlbottom.Height = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(frm_sm.Height * GeneralFunctions.fnDouble(((GeneralFunctions.RegisteredModules().Contains("SCALE")) && Settings.AdvtScale == "Y") ? "100": Settings.AdvtDispArea) ) / 100)); //replace registered module ALL || SCALE
            pnlbottom.Height = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(frm_sm.Height * GeneralFunctions.fnDouble(Settings.AdvtDispArea)) / 100));
            pnlbottom1.Dock = DockStyle.Bottom;
            pnlbottom1.Height = 0;

            pnlup.Dock = DockStyle.Top;
            pnlup.Height = 100;
            lbCustName.AutoSize = false;
            lbCustName.Width = 300;
            lbCustName.Font = new System.Drawing.Font("Book Antiqua", 14.25F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbCustName.ForeColor = Color.White;
            lbCustName.Left = 10;
            lbCustName.Top = 5;
            pnlup.Controls.Add(lbCustName);

            pnlweight.Dock = DockStyle.Right;
            pnlweight.Width = 220;
            lbWeight.AutoSize = false;
            lbWeight.Width = 200;
            lbWeight.Font = new System.Drawing.Font("Open Sans", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbWeight.ForeColor = Color.White;
            lbWeight.Left = 10;
            lbWeight.Top = 5;
            lbWeight.TextAlign = ContentAlignment.MiddleCenter;
            pnlweight.Controls.Add(lbWeight);

            lbWeight_Value.AutoSize = false;
            lbWeight_Value.Width = 200;
            lbWeight_Value.Font = new System.Drawing.Font("Open Sans", 16.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbWeight_Value.ForeColor = Color.White;
            lbWeight_Value.Left = 10;
            lbWeight_Value.Top = 30;
            lbWeight_Value.TextAlign = ContentAlignment.MiddleCenter;
            pnlweight.Controls.Add(lbWeight_Value);

            lbWeight_Info1.AutoSize = false;
            lbWeight_Info1.Width = 200;
            lbWeight_Info1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbWeight_Info1.ForeColor = Color.White;
            lbWeight_Info1.Left = 10;
            lbWeight_Info1.Top = 55;
            lbWeight_Info1.TextAlign = ContentAlignment.MiddleLeft;
            pnlweight.Controls.Add(lbWeight_Info1);

            lbWeight_Info2.AutoSize = false;
            lbWeight_Info2.Height = 50;
            lbWeight_Info2.Width = 200;
            lbWeight_Info2.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbWeight_Info2.ForeColor = Color.White;
            lbWeight_Info2.Left = 10;
            lbWeight_Info2.Top = 75;
            lbWeight_Info2.TextAlign = ContentAlignment.TopLeft;
            pnlweight.Controls.Add(lbWeight_Info2);

            pnlup.Controls.Add(pnlweight);

            pnlclient.Controls.Add(grd);
            pnlclient.Controls.Add(pnlgridseparator);
            pnlclient.Controls.Add(pnlright);
            pnlclient.Dock = DockStyle.Fill;

            pnltender.Dock = DockStyle.Fill;
            grdTender.Dock = DockStyle.Fill;
            pnltender.Controls.Add(grdTender);

            pic.Height = 112;
            pic.Width = 128;
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.Dock = DockStyle.None;
            pnlright.Controls.Add(pnltender);
            pnltenderT3.Controls.Add(pic);

            pnlright.Controls.Add(pnltenderT4);
            pnlright.Controls.Add(pnltenderT3);
            pnlright.Controls.Add(pnltenderT2);
            pnlright.Controls.Add(pnltenderT1);

            grd.Dock = DockStyle.Fill;
            pnlright.Dock = DockStyle.Right;
            pnlright.Width = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(frm_sm.Width * 40) / 100));

            pnlbleft.Dock = DockStyle.Left;
            pnlbleft.Width = 4;
            pnlbleft.BackColor = Color.Transparent;
            pnlbright.Dock = DockStyle.Right;
            pnlbright.Width = 4;
            pnlbright.BackColor = Color.Transparent;
            pnlbup.Dock = DockStyle.Top;
            pnlbup.Height = 4;
            pnlbup.BackColor = Color.Transparent;
            pnlbdown.Dock = DockStyle.Bottom;
            pnlbdown.Height = 4;
            pnlbdown.BackColor = Color.Transparent;

            pnlad.Dock = DockStyle.Fill;
            pnlad.BackColor = Color.Transparent;

            pnlad1.Dock = DockStyle.Left;
            pnlad1.BackgroundImageLayout = ImageLayout.Center;
            pnlad1.BackColor = Color.Transparent;
            pnlad2.Dock = DockStyle.Left;
            pnlad2.BackgroundImageLayout = ImageLayout.Center;
            pnlad2.BackColor = Color.Transparent;
            pnlad3.Dock = DockStyle.Left;
            pnlad3.BackgroundImageLayout = ImageLayout.Center;
            pnlad3.BackColor = Color.Transparent;

            pnladb1.Dock = DockStyle.Left;
            pnladb1.Width = 4;
            pnladb1.BackColor = Color.Transparent;
            pnladb2.Dock = DockStyle.Left;
            pnladb2.Width = 4;
            pnladb2.BackColor = Color.Transparent;

            pnlad.Controls.Add(pnlad3);
            pnlad.Controls.Add(pnladb2);
            pnlad.Controls.Add(pnlad2);
            pnlad.Controls.Add(pnladb1);
            pnlad.Controls.Add(pnlad1);

            pnlbottom.BackgroundImageLayout = ImageLayout.Center;

            pnlbottom.Controls.Add(pnlad);
            pnlbottom.Controls.Add(pnlbleft);
            pnlbottom.Controls.Add(pnlbright);
            pnlbottom.Controls.Add(pnlbup);
            pnlbottom.Controls.Add(pnlbdown);

            frm_sm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(47)))), ((int)(((byte)(65)))));

            frm_sm.Controls.Add(pnlScaleInfo);
            frm_sm.Controls.Add(pnlLaneClosed);
            frm_sm.Controls.Add(pnltenderinfo);
            frm_sm.Controls.Add(pnlclient);
            frm_sm.Controls.Add(pnlup);
            frm_sm.Controls.Add(pnlbottom1);
            frm_sm.Controls.Add(pnlbottom);
            frm_sm.Controls.Add(pnlborderleft);
            frm_sm.Controls.Add(pnlborderright);

            pnltenderinfo.SendToBack();
            pnlLaneClosed.SendToBack();
            pnlScaleInfo.SendToBack();

            pnltenderT1.ResumeLayout();
            pnltenderT2.ResumeLayout();
            pnltop1.ResumeLayout();
            pnltop2.ResumeLayout();
            pnltop3.ResumeLayout();
            pnltop4.ResumeLayout();
            pnltop5.ResumeLayout();
            pnltop6.ResumeLayout();
            pnltop7.ResumeLayout();
            pnlline1.ResumeLayout();
            pnlTAmt.ResumeLayout();
            pnlBAmt.ResumeLayout();
            pnlCAmt.ResumeLayout();
            pnltenderT3.ResumeLayout();
            pnltenderT4.ResumeLayout();
            pnlpic.ResumeLayout();
            pnlbottom.ResumeLayout();
            pnlbottom1.ResumeLayout();
            pnlup.ResumeLayout();
            pnlclient.ResumeLayout();
            pnltender.ResumeLayout();
            pnlright.ResumeLayout();
            pnlbleft.ResumeLayout();
            pnlbright.ResumeLayout();
            pnlbup.ResumeLayout();
            pnlbdown.ResumeLayout();
            pnlad.ResumeLayout();
            pnlad1.ResumeLayout();
            pnlad2.ResumeLayout();
            pnlad3.ResumeLayout();
            pnladb1.ResumeLayout();
            pnladb2.ResumeLayout();
        }

        // 100% Advertisement Display if no active order

        private void SetDynamicScreenAdvertisementArea()
        {
            if (grd.DataSource == null)
            {
                pnlbottom.Height = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(frm_sm.Height)));
            }
            else
            {
                if ((grd.DataSource as DataTable).Rows.Count == 0)
                {
                    pnlbottom.Height = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(frm_sm.Height)));
                }
                else
                {
                    if (GeneralFunctions.fnDouble(Settings.AdvtDispArea) <= 25)
                    {
                        pnlbottom.Height = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(frm_sm.Height * GeneralFunctions.fnDouble(Settings.AdvtDispArea)) / 100));
                    }
                    else
                    {
                        pnlbottom.Height = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(frm_sm.Height * 25) / 100));
                    }
                }
            }
        }


        // Move to Tender Screen

        public static void GoTenderScreen(double totalsale, double securitydeposit)
        {
            if (securitydeposit <= 0) pnltop5.Height = 0; else pnltop5.Height = 30;
            pnltenderT2.Height = 5 + pnltop1.Height + pnltop2.Height + pnltop3.Height + pnltop4.Height + pnltop5.Height + pnltop6.Height + +pnltop7.Height;
            lbT2.Text = GeneralFunctions.FormatDoubleWithCurrency(totalsale);
            lbSD2.Text = GeneralFunctions.FormatDoubleWithCurrency(securitydeposit);
            grdTender.DataSource = null;
            pnltenderT3.Height = 0;
            pnltenderT4.Height = 120;
        }

        // Display after complete transaction 

        public static void FinalTenderingDisplay(double val)
        {
            pnltenderinfo.Left = GeneralFunctions.fnInt32(Math.Ceiling(GeneralFunctions.fnDouble(frm_sm.Width - pnltenderinfo.Width) / 2));
            pnltenderinfo.Top = GeneralFunctions.fnInt32(Math.Ceiling(GeneralFunctions.fnDouble(frm_sm.Height - pnltenderinfo.Height) / 2));
            if (val == 0)
            {
                lbTran1.Left = 100;
                lbTran1.Top = 60;
            }
            else
            {
                lbTran1.Left = 100;
                lbTran1.Top = 20;
                lbTran2.Left = 100;
                lbTran2.Top = 100;
                lbTran2.Text = Properties.Resources.ChangeAmt +  " " +  GeneralFunctions.FormatDoubleWithCurrency(val);
            }
            pnltenderinfo.BringToFront();
            pnltenderinfo.Refresh();
            frm_sm.Refresh();
        }

        // Hide Tender Display ( Initialize Customer Display )

        public static void ClearTenderingDisplay()
        {
            lbTran1.Left = 100;
            lbTran1.Top = 60;
            lbTran2.Text = "";
            pnltenderinfo.SendToBack();
            pnltenderinfo.Refresh();
            frm_sm.Refresh();
        }

        // Display on Application Load

        public static void SetupDisplayOnLoad()
        {
            pnlLaneClosed.Left = 0;
            pnlLaneClosed.Top = 0;
            pnlLaneClosed.Size = new System.Drawing.Size(frm_sm.Width, frm_sm.Height - pnlbottom.Height);
            lbLane.Left = (frm_sm.Width - lbLane.Width) / 2;
            lbLane.Top = (frm_sm.Height - pnlbottom.Height) / 2;
            pnlLaneClosed.BringToFront();
            pnlLaneClosed.Refresh();
            frm_sm.Refresh();
        }

        // Display on POS module Load

        public static void SetupDisplayOnPOSLoad()
        {
            pnlScaleInfo.SendToBack();
            pnlScaleInfo.Refresh();
            pnlLaneClosed.SendToBack();
            pnlLaneClosed.Refresh();
            frm_sm.Refresh();
        }

        // Display on SCALE module Load

        public static void SetupDisplayOnScaleLoad()
        {
            pnlLaneClosed.SendToBack();
            pnlLaneClosed.Refresh();
            pnltenderinfo.SendToBack();
            pnltenderinfo.Refresh();
            pnlScaleInfo.Left = 0;
            pnlScaleInfo.Top = 0;
            pnlScaleInfo.Size = new System.Drawing.Size(frm_sm.Width, frm_sm.Height - pnlbottom.Height);

            if (Settings.AdvtScale == "Y")
            {
                pnlScaleInfo.Size = new System.Drawing.Size(frm_sm.Width, pnlup.Height);
                lbScaleProduct.Font = new System.Drawing.Font("Open Sans", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbScaleTareHeader.Font = new System.Drawing.Font("Open Sans", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbScaleWeightHeader.Font = new System.Drawing.Font("Open Sans", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbScaleUPHeader.Font = new System.Drawing.Font("Open Sans", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbScaleTPHeader.Font = new System.Drawing.Font("Open Sans", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));


                lbScaleTare.Font = new System.Drawing.Font("Open Sans", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbScaleWeight.Font = new System.Drawing.Font("Open Sans", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbScaleUP.Font = new System.Drawing.Font("Open Sans", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbScaleTP.Font = new System.Drawing.Font("Open Sans", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));


                pnlScaleP.Height = pnlScaleInfo.Height / 3;
                lbScaleProduct.Height = pnlScaleP.Height;
                pnlScaleT.Width = pnlScaleW.Width = pnlScaleUP.Width = pnlScaleTP.Width = pnlScaleValue.Width / 4;
                lbScaleTareHeader.Height = pnlScaleT.Height / 2;
                lbScaleTare.Height = pnlScaleT.Height / 2;

                lbScaleWeightHeader.Height = pnlScaleW.Height / 2;
                lbScaleWeight.Height = pnlScaleW.Height / 2;

                lbScaleUPHeader.Height = pnlScaleUP.Height / 2;
                lbScaleUP.Height = pnlScaleUP.Height / 2;

                lbScaleTPHeader.Height = pnlScaleTP.Height / 2;
                lbScaleTP.Height = pnlScaleTP.Height / 2;

            }
            else
            {
                pnlScaleP.Height = pnlScaleInfo.Height / 2;
                lbScaleProduct.Height = pnlScaleP.Height;
                pnlScaleT.Width = pnlScaleW.Width = pnlScaleUP.Width = pnlScaleTP.Width = pnlScaleValue.Width / 4;
                lbScaleTareHeader.Height = pnlScaleT.Height / 2;
                lbScaleTare.Height = pnlScaleT.Height / 2;

                lbScaleWeightHeader.Height = pnlScaleW.Height / 2;
                lbScaleWeight.Height = pnlScaleW.Height / 2;

                lbScaleUPHeader.Height = pnlScaleUP.Height / 2;
                lbScaleUP.Height = pnlScaleUP.Height / 2;

                lbScaleTPHeader.Height = pnlScaleTP.Height / 2;
                lbScaleTP.Height = pnlScaleTP.Height / 2;
            }

            lbScaleGraduation.Text = Settings.WeightDisplayType;

            lbScaleGraduation.Text = (Settings.NTEPCert != "" ? "  " + Properties.Resources.NTEPCert + "  " + Settings.NTEPCert : "") +
                        (GeneralFunctions.GetScaleGraduationText() == "" ? "" :
                            "  " + Properties.Resources.Scale+ "  " + GeneralFunctions.GetScaleGraduationText());

            /*if (Settings.ScaleDevice != "(None)")
            {
                lbScaleGraduation.Text = (Settings.NTEPCert != "" ? "  NTEP Cert. No. : " + Settings.NTEPCert : "") +
                        (GeneralFunctions.GetScaleGraduationText() == "" ? "" :
                            "  Scale  " + GeneralFunctions.GetScaleGraduationText());
            }
            else
            {
                lbScaleGraduation.Text = "";
            }*/
            pnlScaleInfo.BringToFront();
            pnlScaleInfo.Refresh();
            frm_sm.Refresh();
        }

        // Display on SCALE Exit

        public static void SetupDisplayOnScaleExit()
        {
            pnlScaleInfo.SendToBack();
            pnlScaleInfo.Refresh();
        }

        // Display when Tender placed / discard

        public static void InsertTender(DataTable dtbl, double val1, double val2, double val3)
        {
            grdTender.DataSource = null;

            if (dtbl != null)
            {
                DataTable dtblTender1 = new DataTable();
               
                dtblTender1.Columns.Add("ID", System.Type.GetType("System.String"));
                dtblTender1.Columns.Add("TENDER", System.Type.GetType("System.String"));
                dtblTender1.Columns.Add("DISPLAY", System.Type.GetType("System.String"));
                dtblTender1.Columns.Add("AMOUNT", System.Type.GetType("System.String"));
                dtblTender1.Columns.Add("GIFTCERTIFICATE", System.Type.GetType("System.String"));
                dtblTender1.Columns.Add("NEWGC", System.Type.GetType("System.String"));
                dtblTender1.Columns.Add("OLDGC", System.Type.GetType("System.String"));
                dtblTender1.Columns.Add("OLDGCAMT", System.Type.GetType("System.String"));
                dtblTender1.Columns.Add("CCTRANNO", System.Type.GetType("System.String"));
                dtblTender1.Columns.Add("GCSTORE", System.Type.GetType("System.String"));
                dtblTender1.Columns.Add("MANUAL", System.Type.GetType("System.String"));
                dtblTender1.Columns.Add("PROCESSCARD", System.Type.GetType("System.String"));
                dtblTender1.Columns.Add("XeConnectID", System.Type.GetType("System.String"));

                foreach (DataRow dr in dtbl.Rows)
                {
                    dtblTender1.ImportRow(dr);
                }


                foreach (DataRow dr in dtblTender1.Rows)
                {
                    
                    try
                    {
                        dr["AMOUNT"] = GeneralFunctions.FormatDoubleWithCurrency(GeneralFunctions.fnDouble(dr["AMOUNT"].ToString()));
                    }
                    catch
                    {

                    }
                }

                grdTender.DataSource = dtblTender1;
            }
            else
            {
                grdTender.DataSource = dtbl;
            }

            lbTT2.Text = GeneralFunctions.FormatDoubleWithCurrency(val1);
            lbBB2.Text = GeneralFunctions.FormatDoubleWithCurrency(val2);
            lbCC2.Text = GeneralFunctions.FormatDoubleWithCurrency(val3);
        }

        // Display shopping cart along with customer ( if any )

        public static void InsertItem(DataTable dtbl, int focushndle, string cust, double val1, double val2, double val3, double val4,
                                      double val5, double val6, double val7)
        {
            grd.DataSource = null;
            pnltenderT4.Height = 0;
            if (val5 <= 0) pnltop5.Height = 0; else pnltop5.Height = 30;
            if (val6 <= 0) pnltop6.Height = 0; else pnltop6.Height = 30;
            if (val7 == 0) pnltop7.Height = 0; else pnltop7.Height = 30;

            pnltenderT2.Height = 5 + pnltop1.Height + pnltop2.Height + pnltop3.Height + pnltop4.Height + pnltop5.Height + pnltop6.Height + +pnltop7.Height;
            pnltenderT3.Height = pnlright.Height - pnltenderT1.Height - pnltenderT2.Height;
            pic.Height = 112;
            pic.Width = 128;
            pic.Left = pnltenderT2.Left + GeneralFunctions.fnInt32(Math.Ceiling(GeneralFunctions.fnDouble((pnltenderT2.Width - pic.Width) / 2)));
            pic.Top = pnltenderT2.Top + GeneralFunctions.fnInt32(Math.Ceiling(GeneralFunctions.fnDouble((pnltenderT2.Height) / 2)));


            DataTable dtblTemp = new DataTable();
            dtblTemp.Columns.Add("ID", System.Type.GetType("System.String"));//1
            dtblTemp.Columns.Add("PRODUCT", System.Type.GetType("System.String"));//2
            dtblTemp.Columns.Add("PRODUCTTYPE", System.Type.GetType("System.String"));//3
            dtblTemp.Columns.Add("ONHANDQTY", System.Type.GetType("System.String"));//4
            dtblTemp.Columns.Add("NORMALQTY", System.Type.GetType("System.String"));//5
            dtblTemp.Columns.Add("COST", System.Type.GetType("System.String"));//6
            dtblTemp.Columns.Add("QTY", System.Type.GetType("System.String"));//7
            dtblTemp.Columns.Add("RATE", System.Type.GetType("System.String"));//8
            dtblTemp.Columns.Add("NRATE", System.Type.GetType("System.String"));//9
            dtblTemp.Columns.Add("PRICE", System.Type.GetType("System.String"));//10
            dtblTemp.Columns.Add("UOMCOUNT", System.Type.GetType("System.String"));//11
            dtblTemp.Columns.Add("UOMPRICE", System.Type.GetType("System.String"));//12
            dtblTemp.Columns.Add("UOMDESC", System.Type.GetType("System.String"));//13
            dtblTemp.Columns.Add("MATRIXOID", System.Type.GetType("System.String"));//14
            dtblTemp.Columns.Add("MATRIXOV1", System.Type.GetType("System.String"));//15
            dtblTemp.Columns.Add("MATRIXOV2", System.Type.GetType("System.String"));//16
            dtblTemp.Columns.Add("MATRIXOV3", System.Type.GetType("System.String"));//17
            dtblTemp.Columns.Add("UNIQUE", System.Type.GetType("System.String"));//18
            dtblTemp.Columns.Add("DP", System.Type.GetType("System.String"));//19
            dtblTemp.Columns.Add("NOTES", System.Type.GetType("System.String"));//20

            dtblTemp.Columns.Add("DISCLOGIC", System.Type.GetType("System.String"));//21
            dtblTemp.Columns.Add("DISCVALUE", System.Type.GetType("System.String"));//22
            dtblTemp.Columns.Add("DISCOUNT", System.Type.GetType("System.String"));//23
            dtblTemp.Columns.Add("DISCOUNTID", System.Type.GetType("System.String"));//24
            dtblTemp.Columns.Add("DISCOUNTTEXT", System.Type.GetType("System.String"));//25
            dtblTemp.Columns.Add("ITEMINDEX", System.Type.GetType("System.String"));//26

            // for blankline
            dtblTemp.Columns.Add("TAXID1", System.Type.GetType("System.String"));//27
            dtblTemp.Columns.Add("TAXID2", System.Type.GetType("System.String"));//28
            dtblTemp.Columns.Add("TAXID3", System.Type.GetType("System.String"));//29
            dtblTemp.Columns.Add("TAXNAME1", System.Type.GetType("System.String"));//30
            dtblTemp.Columns.Add("TAXNAME2", System.Type.GetType("System.String"));//31
            dtblTemp.Columns.Add("TAXNAME3", System.Type.GetType("System.String"));//32
            dtblTemp.Columns.Add("TAXRATE1", System.Type.GetType("System.String"));//33
            dtblTemp.Columns.Add("TAXRATE2", System.Type.GetType("System.String"));//34
            dtblTemp.Columns.Add("TAXRATE3", System.Type.GetType("System.String"));//35
            dtblTemp.Columns.Add("TAXABLE1", System.Type.GetType("System.String"));//36
            dtblTemp.Columns.Add("TAXABLE2", System.Type.GetType("System.String"));//37
            dtblTemp.Columns.Add("TAXABLE3", System.Type.GetType("System.String"));//38

            // service type
            dtblTemp.Columns.Add("SERVICE", System.Type.GetType("System.String"));//39

            // for rent
            dtblTemp.Columns.Add("RENTTYPE", System.Type.GetType("System.String"));//40
            dtblTemp.Columns.Add("RENTDURATION", System.Type.GetType("System.Double"));//41
            dtblTemp.Columns.Add("RENTAMOUNT", System.Type.GetType("System.Double"));//42
            dtblTemp.Columns.Add("RENTDEPOSIT", System.Type.GetType("System.Double"));//43

            // for repair
            dtblTemp.Columns.Add("REPAIRITEMTAG", System.Type.GetType("System.String"));//44
            dtblTemp.Columns.Add("REPAIRITEMSLNO", System.Type.GetType("System.String"));//45
            dtblTemp.Columns.Add("REPAIRITEMPURCHASEDATE", System.Type.GetType("System.String"));//46

            // for Tax pickup from Tax Table
            dtblTemp.Columns.Add("TX1TYPE", System.Type.GetType("System.Int32"));//47
            dtblTemp.Columns.Add("TX2TYPE", System.Type.GetType("System.Int32"));//48
            dtblTemp.Columns.Add("TX3TYPE", System.Type.GetType("System.Int32"));//49
            dtblTemp.Columns.Add("TX1ID", System.Type.GetType("System.Int32"));//50
            dtblTemp.Columns.Add("TX2ID", System.Type.GetType("System.Int32"));//51
            dtblTemp.Columns.Add("TX3ID", System.Type.GetType("System.Int32"));//52
            dtblTemp.Columns.Add("TX1", System.Type.GetType("System.Double"));//53
            dtblTemp.Columns.Add("TX2", System.Type.GetType("System.Double"));//54
            dtblTemp.Columns.Add("TX3", System.Type.GetType("System.Double"));//55

            // for Mix and Match
            dtblTemp.Columns.Add("MIXMATCHID", System.Type.GetType("System.Int32"));//56
            dtblTemp.Columns.Add("MIXMATCHFLAG", System.Type.GetType("System.String"));//57
            dtblTemp.Columns.Add("MIXMATCHTYPE", System.Type.GetType("System.String"));//58
            dtblTemp.Columns.Add("MIXMATCHVALUE", System.Type.GetType("System.Double"));//59
            dtblTemp.Columns.Add("MIXMATCHQTY", System.Type.GetType("System.Int32"));//60
            dtblTemp.Columns.Add("MIXMATCHUNIQUE", System.Type.GetType("System.Int32"));//61
            dtblTemp.Columns.Add("MIXMATCHLAST", System.Type.GetType("System.String"));//62

            // for Fees & Charges
            dtblTemp.Columns.Add("FEESID", System.Type.GetType("System.String"));//63
            dtblTemp.Columns.Add("FEESLOGIC", System.Type.GetType("System.String"));//64
            dtblTemp.Columns.Add("FEESVALUE", System.Type.GetType("System.String"));//65
            dtblTemp.Columns.Add("FEESTAXRATE", System.Type.GetType("System.String"));//66
            dtblTemp.Columns.Add("FEES", System.Type.GetType("System.String"));//67
            dtblTemp.Columns.Add("FEESTAX", System.Type.GetType("System.String"));//68
            dtblTemp.Columns.Add("FEESTEXT", System.Type.GetType("System.String"));//69
            dtblTemp.Columns.Add("FEESQTY", System.Type.GetType("System.String"));//70

            //for Sale Price
            dtblTemp.Columns.Add("SALEPRICEID", System.Type.GetType("System.Int32"));//71


            // customer Destination Tax
            dtblTemp.Columns.Add("DTXID", System.Type.GetType("System.Int32"));//72
            dtblTemp.Columns.Add("DTXTYPE", System.Type.GetType("System.Int32"));//73
            dtblTemp.Columns.Add("DTXRATE", System.Type.GetType("System.Double"));//74
            dtblTemp.Columns.Add("DTX", System.Type.GetType("System.Double"));//75

            dtblTemp.Columns.Add("EDITF", System.Type.GetType("System.String"));//76

            dtblTemp.Columns.Add("PROMPTPRICE", System.Type.GetType("System.String"));//77

            // Buy 'n Get Free

            dtblTemp.Columns.Add("BUYNGETFREEHEADERID", System.Type.GetType("System.String"));
            dtblTemp.Columns.Add("BUYNGETFREECATEGORY", System.Type.GetType("System.String"));
            dtblTemp.Columns.Add("SL", System.Type.GetType("System.Int32"));
            dtblTemp.Columns.Add("BUYNGETFREENAME", System.Type.GetType("System.String"));

            // Age Restriction if Applicable for Item
            dtblTemp.Columns.Add("AGE", System.Type.GetType("System.Int32"));

            // Add for Tax Inclusive
            dtblTemp.Columns.Add("GRATE", System.Type.GetType("System.String"));
            dtblTemp.Columns.Add("GPRICE", System.Type.GetType("System.String"));

            dtblTemp.Columns.Add("UOM", System.Type.GetType("System.String"));

            dtblTemp.Columns.Add("DISPLAY_ITEM", System.Type.GetType("System.String"));
            dtblTemp.Columns.Add("DISPLAY_QTY", System.Type.GetType("System.String"));
            dtblTemp.Columns.Add("DISPLAY_RATE", System.Type.GetType("System.String"));
            dtblTemp.Columns.Add("DISPLAY_TOTAL", System.Type.GetType("System.String"));

            dtblTemp.Columns.Add("PM", System.Type.GetType("System.String"));
            if (dtbl != null)
            {
                foreach (DataRow dr in dtbl.Rows)
                {
                    dtblTemp.ImportRow(dr);
                }
            }
            if (dtblTemp.Rows.Count > 0)
            {
                foreach (DataRow dr in dtblTemp.Rows)
                {
                    if ((dr["RATE"].ToString() == "") || (dr["RATE"].ToString() == "99988863777") || (dr["RATE"].ToString() == "99988863777.000") || (dr["RATE"].ToString() == "99988863777.00"))
                    {

                    }
                    else
                    {
                        if (!dr["RATE"].ToString().Contains(SystemVariables.CurrencySymbol))
                        {
                            try
                            {

                                dr["RATE"] = GeneralFunctions.FormatDoubleWithCurrency(GeneralFunctions.fnDouble(dr["RATE"].ToString()));
                            }
                            catch
                            {

                            }
                        }
                    }

                    if ((dr["PRICE"].ToString() == "") || (dr["PRICE"].ToString() == "99988863777") || (dr["PRICE"].ToString() == "99988863777.000") || (dr["PRICE"].ToString() == "99988863777.00"))
                    {

                    }
                    else
                    {
                        if (!dr["PRICE"].ToString().Contains(SystemVariables.CurrencySymbol))
                        {
                            try
                            {
                                dr["PRICE"] = GeneralFunctions.FormatDoubleWithCurrency(GeneralFunctions.fnDouble(dr["PRICE"].ToString()));
                            }
                            catch
                            {

                            }
                        }
                    }

                    if ((dr["GRATE"].ToString() == "") || (dr["GRATE"].ToString() == "99988863777") || (dr["GRATE"].ToString() == "99988863777.000") || (dr["GRATE"].ToString() == "99988863777.00"))
                    {

                    }
                    else
                    {
                        if (!dr["GRATE"].ToString().Contains(SystemVariables.CurrencySymbol))
                        {
                            try
                            {
                                dr["GRATE"] = GeneralFunctions.FormatDoubleWithCurrency(GeneralFunctions.fnDouble(dr["GRATE"].ToString()));
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }

                    if ((dr["GPRICE"].ToString() == "") || (dr["GPRICE"].ToString() == "99988863777") || (dr["GPRICE"].ToString() == "99988863777.000") || (dr["GPRICE"].ToString() == "99988863777.00"))
                    {

                    }
                    else
                    {
                        if (!dr["GPRICE"].ToString().Contains(SystemVariables.CurrencySymbol))
                        {
                            try
                            {
                                dr["GPRICE"] = GeneralFunctions.FormatDoubleWithCurrency(GeneralFunctions.fnDouble(dr["GPRICE"].ToString()));
                            }
                            catch
                            {

                            }
                        }
                    }

                }

            }



            grd.DataSource = dtblTemp;
            gridview1.FocusedRowHandle = focushndle;

            if (cust == "") lbCustName.Text = Properties.Resources.Welcome; else lbCustName.Text = Properties.Resources.Welcome + "  " + cust;
            lbSD2.Text = GeneralFunctions.FormatDoubleWithCurrency(val5);
            lbST2.Text = GeneralFunctions.FormatDoubleWithCurrency(val1);
            lbD2.Text = GeneralFunctions.FormatDoubleWithCurrency(val2);
            lbTX2.Text = GeneralFunctions.FormatDoubleWithCurrency(val3);
            lbTD2.Text = GeneralFunctions.FormatDoubleWithCurrency(val6);
            lbT2.Text = GeneralFunctions.FormatDoubleWithCurrency(val4);
            lbF2.Text = GeneralFunctions.FormatDoubleWithCurrency(val7);

            if (grd.DataSource == null)
            {
                pnlbottom.Height = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(frm_sm.Height)));
                //AdvertisementAreaDisplaySettings();
                //tmr.Stop();
                //tmr.Interval = 100;
                //tmr.Start();
            }
            else
            {
                if ((grd.DataSource as DataTable).Rows.Count == 0)
                {
                    pnlbottom.Height = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(frm_sm.Height)));
                    //AdvertisementAreaDisplaySettings();
                    //tmr.Stop();
                    //tmr.Interval = 100;
                    //tmr.Start();
                }
                else
                {
                    if (GeneralFunctions.fnDouble(Settings.AdvtDispArea) <= 25)
                    {
                        pnlbottom.Height = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(frm_sm.Height * GeneralFunctions.fnDouble(Settings.AdvtDispArea)) / 100));
                    }
                    else
                    {
                        pnlbottom.Height = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(frm_sm.Height * 25) / 100));
                    }
                    
                }
            }
        }
        /// Show Image of selected item in cart
        private static void gridview1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int PID = 0;
            if (e.FocusedRowHandle <= 0) return;
            PID = Convert.ToInt32(GeneralFunctions.GetCellValue(e.FocusedRowHandle, gridview1, colID));
            ShowPhoto("Product", PID);
        }
        /// Set Decimal Place in cart
        private static void gridview1_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName != "RATE") return;
            DevExpress.XtraGrid.Views.Grid.GridView gv = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (gv.GetRowCellValue(e.RowHandle, gv.Columns["DP"]) == null) return;
            string fieldValue = gv.GetRowCellValue(e.RowHandle, gv.Columns["DP"]).ToString();
            switch (fieldValue)
            {
                case "2":
                    e.RepositoryItem = rep2;
                    break;
                case "3":
                    e.RepositoryItem = rep3;
                    break;
            }
        }
        /// Set Cart Row Height
        private static void gridView2_CalcRowHeight(object sender, DevExpress.XtraGrid.Views.Grid.RowHeightEventArgs e)
        {
            e.RowHeight = 40;
        }
        /// Custom Display Cart based on different conditions
        private static void gridview1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "PRODUCT")
            {
                int RH = e.RowHandle;
                string displayval = "";
                string srv = "";
                srv = GeneralFunctions.GetCellValue(RH, gridview1, colService);
                if (srv == "Rent")
                {
                    string rnttype = "";
                    string rnttime = "";
                    rnttype = GeneralFunctions.GetCellValue(RH, gridview1, colRentType);
                    rnttime = GeneralFunctions.GetCellValue(RH, gridview1, colRentDuration);

                    if (rnttype == "MI") displayval = " (" + rnttime + " " + Properties.Resources.min+ ")";
                    else if (rnttype == "HR") displayval = " (" + rnttime + " " + Properties.Resources.hr + ")";
                    else if (rnttype == "HD") displayval = " (" + rnttime + " " + Properties.Resources.halfday + ")";
                    else if (rnttype == "DY") displayval = " (" + rnttime + " " + Properties.Resources.day + ")";
                    else if (rnttype == "WK") displayval = " (" + rnttime + " " + Properties.Resources.week + ")";
                    else displayval = " (" + rnttime + " " + Properties.Resources.month + ")";
                }

                string promotiontxt = "";
                if (GeneralFunctions.GetCellValue(RH, gridview1, colBuyGetFreeCat) == "B")
                {
                    promotiontxt = GeneralFunctions.GetCellValue(RH, gridview1, colPromotion);
                    Font fnt = new Font("Open Sans", 10, System.Drawing.FontStyle.Regular | System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline);
                    Brush bsh = Brushes.DeepPink;
                    StringFormat sf = new StringFormat(StringFormat.GenericDefault);
                    sf.Alignment = StringAlignment.Near;
                    Rectangle rectCell = e.Bounds;
                    if (!e.DisplayText.StartsWith("\n"))
                    {
                        rectCell.Height = rectCell.Height + 11;
                        e.DisplayText = "\n" + e.DisplayText;
                    }

                    rectCell.X = rectCell.X;
                    rectCell.Y = rectCell.Y;
                    rectCell.Width = rectCell.Width;
                    rectCell.Height = rectCell.Height - 2;
                    e.Cache.DrawString(promotiontxt, fnt, bsh, rectCell, sf);
                }

                string disc = "";
                disc = GeneralFunctions.GetCellValue(RH, gridview1, colDiscountText);
                if (disc != "")
                {
                    Font fnt = new Font("Open Sans", 9.75F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic);
                    Brush bsh = Brushes.DeepSkyBlue;
                    StringFormat sf = new StringFormat(StringFormat.GenericDefault);
                    sf.Alignment = StringAlignment.Near;

                    Rectangle rectCell = e.Bounds;
                    rectCell.X = rectCell.X + 2;
                    rectCell.Y = rectCell.Y + (e.Bounds.Height - 25);
                    rectCell.Width = rectCell.Width;
                    rectCell.Height = rectCell.Height - 25;
                    e.Cache.DrawString(disc, fnt, bsh, rectCell, sf);
                }

                if ((disc == "") && (displayval != ""))
                {
                    Font fnt = new Font("Open Sans", 9.75F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic);
                    Brush bsh = Brushes.DeepSkyBlue;
                    StringFormat sf = new StringFormat(StringFormat.GenericDefault);
                    sf.Alignment = StringAlignment.Near;

                    Rectangle rectCell = e.Bounds;
                    rectCell.X = rectCell.X + 2;
                    rectCell.Y = rectCell.Y + (e.Bounds.Height - 25);
                    rectCell.Width = rectCell.Width;
                    rectCell.Height = rectCell.Height - 25;
                    e.Cache.DrawString(displayval, fnt, bsh, rectCell, sf);
                }

                if ((disc != "") && (displayval != ""))
                {
                    Font fnt = new Font("Open Sans", 9.75F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic);
                    Brush bsh = Brushes.DeepSkyBlue;
                    StringFormat sf = new StringFormat(StringFormat.GenericDefault);
                    sf.Alignment = StringAlignment.Near;

                    Rectangle rectCell = e.Bounds;
                    rectCell.X = rectCell.X + 2;
                    rectCell.Y = rectCell.Y + (e.Bounds.Height - 25);
                    rectCell.Width = rectCell.Width;
                    rectCell.Height = rectCell.Height - 25;
                    e.Cache.DrawString(disc + displayval, fnt, bsh, rectCell, sf);
                }

            }

            if (e.Column.FieldName == "QTY")
            {
                int rH = e.RowHandle;
                string ProdTY = gridview1.GetRowCellValue(rH, colProductType).ToString();
                string ProdUOM = gridview1.GetRowCellValue(rH, colUOM).ToString();
                string val = e.CellValue.ToString();
                if (e.CellValue != null)
                {
                    if (val == "999888777") e.DisplayText = "";
                    if (ProdTY == "W")
                    {
                        e.DisplayText = val + " " + ProdUOM;
                    }
                }
                /*string val = e.DisplayText;
                if (e.DisplayText != "")
                {
                    if (val == "999888777") e.DisplayText = "";
                }*/
            }

            if (e.Column.FieldName == "RATE")
            {
                int rH = e.RowHandle;
                string ProdTY = gridview1.GetRowCellValue(rH, colProductType).ToString();
                string ProdUOM = gridview1.GetRowCellValue(rH, colUOM).ToString();
                string val = e.CellValue.ToString();
                if (e.CellValue != null)
                {
                    if ((val == "99988863777") || (val == "99988863777.00") || (val == "99988863777.000")) e.DisplayText = "";

                    if (ProdTY == "W")
                    {
                        e.DisplayText = GeneralFunctions.FormatDoubleForPrint(val) + "/" + ProdUOM;
                    }
                }

                /*string val = e.DisplayText;
                if (e.DisplayText != "")
                {
                    if ((val == "99988863777.00") || (val == "99988863777.000") || (val == "99988863777")) e.DisplayText = "";
                }*/
            }

            if (e.Column.FieldName == "PRICE")
            {
                string val = e.DisplayText;
                if (e.DisplayText != "")
                {
                    if ((val == "99988863777.00") || (val == "99988863777.000") || (val == "99988863777")) e.DisplayText = "";
                }
            }


            if (e.Column.FieldName == "GRATE")
            {
                int rH = e.RowHandle;
                string ProdTY = gridview1.GetRowCellValue(rH, colProductType).ToString();
                string ProdUOM = gridview1.GetRowCellValue(rH, colUOM).ToString();
                string val = e.CellValue.ToString();
                if (e.CellValue != null)
                {
                    if ((val == "99988863777") || (val == "99988863777.00") || (val == "99988863777.000")) e.DisplayText = "";

                    if (ProdTY == "W")
                    {
                        e.DisplayText = GeneralFunctions.FormatDoubleForPrint(val) + "/" + ProdUOM;
                    }
                }

                /*
                string val = e.DisplayText;
                if (e.DisplayText != "")
                {
                    if ((val == "99988863777") || (val == "99988863777.00") || (val == "99988863777.000")) e.DisplayText = "";
                }*/
            }

            if (e.Column.FieldName == "GPRICE")
            {
                string val = e.DisplayText;
                if (val != "")
                {
                    if ((val == "99988863777") || (val == "99988863777.00") || (val == "99988863777.000")) e.DisplayText = "";
                    
                }
                else
                { 
                   
                }
            }
        }

        // Get Photo from Database and assign it

        private static void ShowPhoto(string pType, int pID)
        {
            PictureBox p = new PictureBox();
            p = pic;
            string strPhotoFile = "";

            string csStorePath = "";
            int intImageWidth = 112;
            int intImageHeight = 128;

            if (!GeneralFunctions.GetPhotoFromTable1(p, pID, pType))
            {
                p.Image = null;
            }
            else
            {
                double AspectRatio = 0.00;
                int intWidth, intHeight;
                AspectRatio = GeneralFunctions.fnDouble(p.Image.Width) /
                    GeneralFunctions.fnDouble(p.Image.Height);
                intHeight = p.Height;
                intWidth = Convert.ToInt16(GeneralFunctions.fnDouble(intHeight) * AspectRatio);

                if (intWidth > intImageWidth)
                {
                    intWidth = intImageWidth;
                    intHeight = Convert.ToInt16(GeneralFunctions.fnDouble(intWidth) / AspectRatio);
                }
                p.Width = intWidth;
                p.Height = intHeight;
            }
        }

        // Set Display Advertisement Area

        private static void AdvertisementAreaDisplaySettings()
        {
            

            
            if ((Settings.AdvtColor.Trim() == "") || (Settings.AdvtColor.Trim() == "0") || (Settings.AdvtColor.Trim().Contains("#00000000"))) pnlbottom.BackColor = Color.Transparent;
            else
            {
                System.Windows.Media.Brush brush = (System.Windows.Media.SolidColorBrush)new System.Windows.Media.BrushConverter().ConvertFromString("#64C2DB");
                System.Windows.Media.Color color = ((System.Windows.Media.SolidColorBrush)brush).Color;
                Color color1 = System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
                pnlbottom.BackColor = color1;
            }
            if (Settings.AdvtBackground.Trim() != "")
            {
                FileInfo fi = new FileInfo(Settings.AdvtBackground.Trim());
                if (fi.Exists)
                {
                    try
                    {
                        pnlbottom.BackgroundImage = resizeImage(Image.FromFile(Settings.AdvtBackground.Trim()), new System.Drawing.Size(pnlbottom.Width, pnlbottom.Height));
                        pnlbottom.Refresh();
                    }
                    finally
                    {
                    }
                }
            }


            GetValidAdImageFile();

            if (dtblAdImage.Rows.Count > 0)
            {

                if (GeneralFunctions.fnInt32(Settings.AdvtDispNo) == 1)
                {
                    pnlad2.Width = 0;
                    pnlad3.Width = 0;
                    pnladb1.Width = 0;
                    pnladb2.Width = 0;
                    pnlad1.Width = pnlad.Width - 8;
                }

                if (GeneralFunctions.fnInt32(Settings.AdvtDispNo) == 2)
                {
                    pnlad3.Width = 0;
                    pnladb1.Width = 0;
                    pnladb2.Width = 0;
                    pnlad1.Width = GeneralFunctions.fnInt32(Math.Ceiling(GeneralFunctions.fnDouble((pnlad.Width - 12) / 2)));
                    pnlad2.Width = pnlad1.Width;
                }

                if (GeneralFunctions.fnInt32(Settings.AdvtDispNo) == 3)
                {
                    pnlad1.Width = GeneralFunctions.fnInt32(Math.Ceiling(GeneralFunctions.fnDouble((pnlad.Width - 16) / 3)));
                    pnlad2.Width = pnlad1.Width;
                    pnlad3.Width = pnlad1.Width;
                }
            }
        }

        // Check if valid Advertisement File Type or not 

        private static void GetValidAdImageFile()
        {
            dtblAdImage = new DataTable();
            dtblAdImage.Columns.Add("FileName", System.Type.GetType("System.String"));
            dtblAdImage.DefaultView.Sort = "FileName asc";
            dtblAdImage.DefaultView.ApplyDefaultSort = true;

            if (Settings.AdvtDir.Trim() != "")
            {
                if (Directory.Exists(Settings.AdvtDir.Trim()))
                {
                    foreach (string f in Directory.GetFiles(Settings.AdvtDir.Trim()))
                    {
                        FileInfo fi = new FileInfo(f);
                        if (fi.Exists)
                        {
                            if ((fi.Extension.ToLower() == ".jpg") || (fi.Extension.ToLower() == ".jpeg") || (fi.Extension.ToLower() == ".png")
                                || (fi.Extension.ToLower() == ".gif") || (fi.Extension.ToLower() == ".bmp") || (fi.Extension.ToLower() == ".wmf"))
                            {
                                dtblAdImage.Rows.Add(new object[] { f });
                            }
                        }
                    }
                }
            }
        }

        // Display Advertisement

        private static void tmr_tick(object sender, EventArgs e)
        {
            if (!timertick) return;
            if (SingleLoad) return;
            tmr.Interval = GeneralFunctions.fnInt32(Settings.AdvtDispTime) * 1000;
            if (dtblAdImage.Rows.Count > 0)
            {
                int i = 0;
                int j = 0;
                int k = 0;
                string imgfile1 = "";
                string imgfile2 = "";
                string imgfile3 = "";

                if (Settings.AdvtDispOrder == "Display File Name Alphabetically")
                {
                    if (GeneralFunctions.fnInt32(Settings.AdvtDispNo) == 1)
                    {
                        CurrentSequenceNo++;
                        if (CurrentSequenceNo > dtblAdImage.Rows.Count) CurrentSequenceNo = 1;
                        i = 0;
                        imgfile1 = "";
                        foreach (DataRowView dr1 in dtblAdImage.DefaultView)
                        {
                            i++;
                            if (i != CurrentSequenceNo) continue;
                            if (i == CurrentSequenceNo)
                            {
                                imgfile1 = dr1["FileName"].ToString();
                                break;
                            }
                        }
                        try
                        {
                            pnlad1.SuspendLayout();
                            pnlad1.BackgroundImage = resizeImage(Image.FromFile(imgfile1), new Size(pnlad1.Width, pnlad1.Height));
                            //pnlad1.Refresh();
                        }
                        finally
                        {
                            pnlad1.ResumeLayout();
                        }
                    }

                    if (GeneralFunctions.fnInt32(Settings.AdvtDispNo) == 2)
                    {
                        CurrentSequenceNo++;
                        if (CurrentSequenceNo > dtblAdImage.Rows.Count) CurrentSequenceNo = 1;
                        i = 0;
                        j = 0;
                        imgfile1 = "";
                        imgfile2 = "";
                        foreach (DataRowView dr2 in dtblAdImage.DefaultView)
                        {
                            i++;
                            if (i != CurrentSequenceNo) continue;
                            if (i == CurrentSequenceNo)
                            {
                                imgfile1 = dr2["FileName"].ToString();
                                break;
                            }
                        }

                        CurrentSequenceNo++;
                        if (CurrentSequenceNo > dtblAdImage.Rows.Count) CurrentSequenceNo = 1;
                        foreach (DataRowView dr3 in dtblAdImage.DefaultView)
                        {
                            j++;
                            if (j != CurrentSequenceNo) continue;
                            if (j == CurrentSequenceNo)
                            {
                                imgfile2 = dr3["FileName"].ToString();
                                break;
                            }
                        }


                        if (imgfile2 == imgfile1) imgfile2 = "";

                        pnlad1.BackgroundImage = resizeImage(Image.FromFile(imgfile1), new System.Drawing.Size(pnlad1.Width, pnlad1.Height));
                        pnlad1.Refresh();

                        if (imgfile2 != "")
                        {
                            pnlad2.BackgroundImage = resizeImage(Image.FromFile(imgfile2), new Size(pnlad2.Width, pnlad2.Height));
                            pnlad2.Refresh();
                        }
                        else
                        {
                            pnlad2.BackgroundImage = null;
                            pnlad2.Refresh();
                        }
                    }



                    if (GeneralFunctions.fnInt32(Settings.AdvtDispNo) == 3)
                    {
                        CurrentSequenceNo++;
                        if (CurrentSequenceNo > dtblAdImage.Rows.Count) CurrentSequenceNo = 1;
                        i = 0;
                        j = 0;
                        k = 0;
                        imgfile1 = "";
                        imgfile2 = "";
                        imgfile3 = "";
                        foreach (DataRowView dr4 in dtblAdImage.DefaultView)
                        {
                            i++;
                            if (i != CurrentSequenceNo) continue;
                            if (i == CurrentSequenceNo)
                            {
                                imgfile1 = dr4["FileName"].ToString();
                                break;
                            }
                        }

                        CurrentSequenceNo++;
                        if (CurrentSequenceNo > dtblAdImage.Rows.Count) CurrentSequenceNo = 1;
                        foreach (DataRowView dr5 in dtblAdImage.DefaultView)
                        {
                            j++;
                            if (j != CurrentSequenceNo) continue;
                            if (j == CurrentSequenceNo)
                            {
                                imgfile2 = dr5["FileName"].ToString();
                                break;
                            }
                        }

                        CurrentSequenceNo++;
                        if (CurrentSequenceNo > dtblAdImage.Rows.Count) CurrentSequenceNo = 1;
                        foreach (DataRowView dr6 in dtblAdImage.DefaultView)
                        {
                            k++;
                            if (k != CurrentSequenceNo) continue;
                            if (k == CurrentSequenceNo)
                            {
                                imgfile3 = dr6["FileName"].ToString();
                                break;
                            }
                        }


                        if (imgfile2 == imgfile1) imgfile2 = "";
                        if (imgfile3 == imgfile2) imgfile3 = "";

                        try
                        {
                            pnlad1.SuspendLayout();
                            pnlad1.BackgroundImage = resizeImage(Image.FromFile(imgfile1), new Size(pnlad1.Width, pnlad1.Height));
                            pnlad1.Refresh();
                        }
                        finally
                        {
                            pnlad1.ResumeLayout();
                        }

                        if (imgfile2 != "")
                        {
                            try
                            {
                                pnlad2.SuspendLayout();
                                pnlad2.BackgroundImage = resizeImage(Image.FromFile(imgfile2), new Size(pnlad2.Width, pnlad2.Height));
                                pnlad2.Refresh();
                            }
                            finally
                            {
                                pnlad2.ResumeLayout();
                            }
                        }
                        else
                        {
                            pnlad2.BackgroundImage = null;
                            pnlad2.Refresh();
                        }

                        if (imgfile3 != "")
                        {
                            try
                            {
                                pnlad3.SuspendLayout();
                                pnlad3.BackgroundImage = resizeImage(Image.FromFile(imgfile3), new Size(pnlad3.Width, pnlad3.Height));
                                pnlad3.Refresh();
                            }
                            finally
                            {
                                pnlad3.ResumeLayout();
                            }
                        }
                        else
                        {
                            pnlad3.BackgroundImage = null;
                            pnlad3.Refresh();
                        }
                    }
                }


                if (Settings.AdvtDispOrder == "Display Randomly")
                {
                    CurrentSequenceNo = 0;

                    CurrentSequenceNo = rnd.Next(1, dtblAdImage.Rows.Count);

                    if (PrevSequenceNo != 0)
                    {

                        if (CurrentSequenceNo == PrevSequenceNo)
                        {
                            CurrentSequenceNo = rnd.Next(1, dtblAdImage.Rows.Count);
                            PrevSequenceNo = CurrentSequenceNo;
                            /*while (CurrentSequenceNo != PrevSequenceNo)
                            {
                                CurrentSequenceNo = rnd.Next(1, dtblAdImage.Rows.Count);
                            }
                            PrevSequenceNo = CurrentSequenceNo;*/
                        }
                        else
                        {
                            PrevSequenceNo = CurrentSequenceNo;
                        }
                    }
                    else
                    {
                        PrevSequenceNo = CurrentSequenceNo;
                    }

                    //lbCustName.Text = CurrentSequenceNo.ToString();
                    //lbCustName.Refresh();
                    if (GeneralFunctions.fnInt32(Settings.AdvtDispNo) == 1)
                    {
                        if (CurrentSequenceNo > dtblAdImage.Rows.Count) CurrentSequenceNo = 1;
                        i = 0;
                        imgfile1 = "";
                        foreach (DataRowView dr1 in dtblAdImage.DefaultView)
                        {
                            i++;
                            if (i != CurrentSequenceNo) continue;
                            if (i == CurrentSequenceNo)
                            {
                                imgfile1 = dr1["FileName"].ToString();
                                break;
                            }
                        }
                        try
                        {
                            pnlad1.SuspendLayout();
                            pnlad1.BackgroundImage = resizeImage(Image.FromFile(imgfile1), new Size(pnlad1.Width, pnlad1.Height));
                            //pnlad1.Refresh();
                        }
                        finally
                        {
                            pnlad1.ResumeLayout();
                        }
                    }


                    if (GeneralFunctions.fnInt32(Settings.AdvtDispNo) == 2)
                    {
                        if (CurrentSequenceNo > dtblAdImage.Rows.Count) CurrentSequenceNo = 1;
                        i = 0;
                        j = 0;
                        imgfile1 = "";
                        imgfile2 = "";
                        foreach (DataRowView dr2 in dtblAdImage.DefaultView)
                        {
                            i++;
                            if (i != CurrentSequenceNo) continue;
                            if (i == CurrentSequenceNo)
                            {
                                imgfile1 = dr2["FileName"].ToString();
                                break;
                            }
                        }

                        CurrentSequenceNo++;
                        if (CurrentSequenceNo > dtblAdImage.Rows.Count) CurrentSequenceNo = 1;
                        foreach (DataRowView dr3 in dtblAdImage.DefaultView)
                        {
                            j++;
                            if (j != CurrentSequenceNo) continue;
                            if (j == CurrentSequenceNo)
                            {
                                imgfile2 = dr3["FileName"].ToString();
                                break;
                            }
                        }


                        if (imgfile2 == imgfile1) imgfile2 = "";

                        pnlad1.BackgroundImage = resizeImage(Image.FromFile(imgfile1), new Size(pnlad1.Width, pnlad1.Height));
                        pnlad1.Refresh();

                        if (imgfile2 != "")
                        {
                            pnlad2.BackgroundImage = resizeImage(Image.FromFile(imgfile2), new Size(pnlad2.Width, pnlad2.Height));
                            pnlad2.Refresh();
                        }
                        else
                        {
                            pnlad2.BackgroundImage = null;
                            pnlad2.Refresh();
                        }
                    }

                    if (GeneralFunctions.fnInt32(Settings.AdvtDispNo) == 3)
                    {
                        if (CurrentSequenceNo > dtblAdImage.Rows.Count) CurrentSequenceNo = 1;
                        i = 0;
                        j = 0;
                        k = 0;
                        imgfile1 = "";
                        imgfile2 = "";
                        imgfile3 = "";
                        foreach (DataRowView dr4 in dtblAdImage.DefaultView)
                        {
                            i++;
                            if (i != CurrentSequenceNo) continue;
                            if (i == CurrentSequenceNo)
                            {
                                imgfile1 = dr4["FileName"].ToString();
                                break;
                            }
                        }

                        CurrentSequenceNo++;
                        if (CurrentSequenceNo > dtblAdImage.Rows.Count) CurrentSequenceNo = 1;
                        foreach (DataRowView dr5 in dtblAdImage.DefaultView)
                        {
                            j++;
                            if (j != CurrentSequenceNo) continue;
                            if (j == CurrentSequenceNo)
                            {
                                imgfile2 = dr5["FileName"].ToString();
                                break;
                            }
                        }

                        CurrentSequenceNo++;
                        if (CurrentSequenceNo > dtblAdImage.Rows.Count) CurrentSequenceNo = 1;
                        foreach (DataRowView dr6 in dtblAdImage.DefaultView)
                        {
                            k++;
                            if (k != CurrentSequenceNo) continue;
                            if (k == CurrentSequenceNo)
                            {
                                imgfile3 = dr6["FileName"].ToString();
                                break;
                            }
                        }


                        if (imgfile2 == imgfile1) imgfile2 = "";
                        if (imgfile3 == imgfile2) imgfile3 = "";

                        try
                        {
                            pnlad1.SuspendLayout();
                            pnlad1.BackgroundImage = resizeImage(Image.FromFile(imgfile1), new Size(pnlad1.Width, pnlad1.Height));
                            pnlad1.Refresh();
                        }
                        finally
                        {
                            pnlad1.ResumeLayout();
                        }

                        if (imgfile2 != "")
                        {
                            try
                            {
                                pnlad2.SuspendLayout();
                                pnlad2.BackgroundImage = resizeImage(Image.FromFile(imgfile2), new Size(pnlad2.Width, pnlad2.Height));
                                pnlad2.Refresh();
                            }
                            finally
                            {
                                pnlad2.ResumeLayout();
                            }
                        }
                        else
                        {
                            pnlad2.BackgroundImage = null;
                            pnlad2.Refresh();
                        }

                        if (imgfile3 != "")
                        {
                            try
                            {
                                pnlad3.SuspendLayout();
                                pnlad3.BackgroundImage = resizeImage(Image.FromFile(imgfile3), new Size(pnlad3.Width, pnlad3.Height));
                                pnlad3.Refresh();
                            }
                            finally
                            {
                                pnlad3.ResumeLayout();
                            }
                        }
                        else
                        {
                            pnlad3.BackgroundImage = null;
                            pnlad3.Refresh();
                        }
                    }

                }

                if (dtblAdImage.Rows.Count == 1)
                {
                    SingleLoad = true;
                }

            }
        }

        // Resize Picture 

        private static Image resizeImage(Image imgToResize, Size size)
        {
            Size imgsize = new Size(imgToResize.Width, imgToResize.Height);
            if ((size.Width >= imgToResize.Width) && (size.Height >= imgToResize.Height))
            {
                return imgToResize;
            }
            else
            {
                int sourceWidth = imgToResize.Width;
                int sourceHeight = imgToResize.Height;

                float nPercent = 0;
                float nPercentW = 0;
                float nPercentH = 0;

                nPercentW = ((float)size.Width / (float)sourceWidth);
                nPercentH = ((float)size.Height / (float)sourceHeight);

                if (nPercentH < nPercentW)
                    nPercent = nPercentH;
                else
                    nPercent = nPercentW;

                int destWidth = (int)(sourceWidth * nPercent);
                int destHeight = (int)(sourceHeight * nPercent);

                Bitmap b = new Bitmap(destWidth, destHeight);
                Graphics g = Graphics.FromImage((Image)b);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
                g.Dispose();

                return (Image)b;
            }
        }

        // clear weight diaplay

        public static void ClearWeightInfo()
        {
            lbWeight.Text = "";
            lbWeight_Value.Text = "";
            lbWeight_Info1.Text = lbWeight_Info2.Text = "";
            pnlweight.Refresh();
            //frm_sm.Refresh();
        }

        // display weighted information of added item

        public static void AddWeightInfo(string wgt, double tare)
        {
            try
            {
                lbWeight.Text = tare <= 0 ? Properties.Resources.GROSSlb : Properties.Resources.NETlb;
                if (wgt.Contains("lb") && wgt.Contains("-") && tare <= 0)
                {
                    lbWeight_Value.Text = "vvv";
                }
                else
                {
                    lbWeight_Value.Text = wgt;
                }

                //lbWeight_Info1.Text = "Capacity: 30 lb x 0.01 lb";
                //lbWeight_Info2.Text = "nmax 3000  d=0.01 lb  Max = 30 lb";


                lbWeight_Info1.Text = Settings.NTEPCert != "" ?
                    Properties.Resources.NTEPCert + "  " + Settings.NTEPCert
                    : "";

                lbWeight_Info2.Text = GeneralFunctions.GetScaleGraduationText() == "" ?
                    "" :
                    Properties.Resources.Scale+ "  " + GeneralFunctions.GetScaleGraduationText();

                /*
                if (Settings.ScaleDevice != "(None)")
                {
                    lbWeight_Info1.Text = Settings.NTEPCert != "" ? "NTEP Cert. No. : " + Settings.NTEPCert : "";

                    lbWeight_Info2.Text = GeneralFunctions.GetScaleGraduationText() == "" ? "" :
                                "Scale  " + GeneralFunctions.GetScaleGraduationText();
                }
                else
                {
                    lbWeight_Info1.Text = "";
                    lbWeight_Info2.Text = "";
                }*/

                pnlweight.Refresh();
                //frm_sm.Refresh();
            }
            catch
            {
            }
        }

        // display weighted information for Live Weight

        public static void AddScaleInfo(string pname, string tare, string wt, bool countty, string up, string tp, bool fixedwt)
        {
            lbScaleTareHeader.Text = Properties.Resources.TARElb;
            lbScaleWeightHeader.Text = countty == true ? Properties.Resources.Countfor : (tare == "" ? Properties.Resources.GROSSlb : Properties.Resources.NETlb);
            lbScaleUPHeader.Text = countty == true ? Properties.Resources.Price : (fixedwt == true ? Properties.Resources.Price : Properties.Resources.PRICElb);
            lbScaleTPHeader.Text = Properties.Resources.TOTALPRICE;
            lbScaleProduct.Text = pname;
            lbScaleTare.Text = tare;
            lbScaleWeight.Text = wt;
            try
            {
                lbScaleUP.Text = up == "" ? "" : GeneralFunctions.FormatDoubleForPrint(up);
            }
            catch
            {
            }
            if ((fixedwt) && (wt.Contains("for")))
            {
                lbScaleWeightHeader.Text = Properties.Resources.Countfor;
            }
            lbScaleTP.Text = tp;
            pnlScaleP.Refresh();
            pnlScaleT.Refresh();
            pnlScaleW.Refresh();
            pnlScaleUP.Refresh();
            pnlScaleTP.Refresh();
            pnlScaleInfo.Refresh();
            //frm_sm.Refresh();
        }

        // display zero scale for Live Weight

        public static void AddZeroScaleInfo(string str1, string str2)
        {
            lbScaleProduct.Text = str1;
            lbScaleTare.Text = "";
            lbScaleWeight.Text = str2;
            lbScaleTare.Text = "";
            lbScaleUP.Text = "";
            lbScaleTP.Text = "";
            pnlScaleP.Refresh();
            pnlScaleT.Refresh();
            pnlScaleW.Refresh();
            pnlScaleUP.Refresh();
            pnlScaleTP.Refresh();
            pnlScaleInfo.Refresh();
            //frm_sm.Refresh();
        }

        // Initialize screen for Live Weight

        public static void ClearScaleInfo()
        {
            lbScaleTareHeader.Text = Properties.Resources.TARElb;
            lbScaleWeightHeader.Text = Properties.Resources.GROSSlb;
            lbScaleUPHeader.Text = Properties.Resources.PRICElb;
            lbScaleTPHeader.Text = Properties.Resources.TOTALPRICE;

            lbScaleProduct.Text = "";
            lbScaleTare.Text = "";
            lbScaleUP.Text = "";
            lbScaleTP.Text = "";
            pnlScaleP.Refresh();
            pnlScaleT.Refresh();
            pnlScaleW.Refresh();
            pnlScaleUP.Refresh();
            pnlScaleTP.Refresh();
            pnlScaleInfo.Refresh();
            //frm_sm.Refresh();
        }

        /// Initialize Screen for Scale Only Registered ( Used in Application Login)
        public static void ClearScaleInfo1()
        {
            lbScaleTareHeader.Text = "";
            lbScaleWeightHeader.Text = "";
            lbScaleUPHeader.Text = "";
            lbScaleTPHeader.Text = "";
            lbScaleWeight.Text = "";
            lbScaleProduct.Text = "";
            lbScaleTare.Text = "";
            lbScaleUP.Text = "";
            lbScaleTP.Text = "";
            pnlScaleP.Refresh();
            pnlScaleT.Refresh();
            pnlScaleW.Refresh();
            pnlScaleUP.Refresh();
            pnlScaleTP.Refresh();
            pnlScaleInfo.Refresh();
            //frm_sm.Refresh();
        }
    }
}
