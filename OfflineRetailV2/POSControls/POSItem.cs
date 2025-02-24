using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;

namespace POSControls
{
    public class POSItem : DevExpress.XtraEditors.SimpleButton
    //public class POSItem : System.Windows.Forms.Button
    {
        private System.ComponentModel.Container components = null;
        private int intItemID = 0;
        private int intCurrentStock = 0;
        private string strItemSKU;
        private string strItemName;
        private string strItemType;
        private bool boolDisplayStockinPOS = true;
        private bool boolNegativeStock = true;
        private bool boolShowSKU = false;
        private string strItemColor = "";
        private string strItemStyle = "";
        private string strItemBackground = "";
        private Font fntFont;
        private string strItemForeColor = "";
        private string strApplicationStyle = "";


        private string strItemFontFamily = "";
        private string strItemFontSize = "";
        private string strItemFontBold = "";
        private string strItemFontItalic = "";

        private string strExpiryDate = "";

        public POSItem(int ButtonType)
        {
            InitializeComponent();
            // ButtonType = 1 is Main POS Screen
            // ButtonType = 2 is Dialog displaying all products
            if (ButtonType == 1)
            {
                this.Height = 40;
                this.Dock = System.Windows.Forms.DockStyle.Top;
                this.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            }
            else
            {
                this.Height = 55;
            }
            this.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.AllowFocus = false;
            this.DoubleBuffered = true;
            //this.FlatAppearance.BorderSize = 0;
            //this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            //this.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //this.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
        }

        public string ItemExpiryDate
        {
            get { return strExpiryDate; }
            set { strExpiryDate = value; }
        }

        public string ItemFontFamily
        {
            get { return strItemFontFamily; }
            set { strItemFontFamily = value; }
        }

        public string ItemFontSize
        {
            get { return strItemFontSize; }
            set { strItemFontSize = value; }
        }

        public string ItemFontBold
        {
            get { return strItemFontBold; }
            set { strItemFontBold = value; }
        }

        public string ItemFontItalic
        {
            get { return strItemFontItalic; }
            set { strItemFontItalic = value; }
        }


        [Category("POS")]
        [Description("Gets or sets the Item ID of Database Table")]
        public int ItemID
        {
            get { return intItemID; }
            set { intItemID = value; }
        }

        [Category("POS")]
        [Description("Gets or sets the Item SKU Display")]
        public bool ShowSKU
        {
            get { return boolShowSKU; }
            set { boolShowSKU = value; }
        }

        [Category("POS")]
        [Description("Gets or sets the Item SKU of Database Table")]
        public string ItemSKU
        {
            get { return strItemSKU; }
            set { strItemSKU = value; }
        }

        [Category("POS")]
        [Description("Gets or sets the Item Name of Database Table")]
        public string ItemName
        {
            get { return strItemName; }
            set { strItemName = value; }
        }

        [Category("POS")]
        [Description("Gets or sets the Item Type of Database Table")]
        public string ItemType
        {
            get { return strItemType; }
            set { strItemType = value; }
        }

        [Category("POS")]
        [Description("Gets or sets Displaying of Stocl Level")]
        public bool DisplayStockinPOS
        {
            get { return boolDisplayStockinPOS; }
            set { boolDisplayStockinPOS = value; SetCaption(); }
        }
        
        [Category("POS")]
        [Description("Gets or sets Negative Stock")]
        public bool NegativeStock
        {
            get { return boolNegativeStock; }
            set { boolNegativeStock = value; }
        }
        [Category("POS")]
        [Description("Gets or sets the CurrentStock of the item")]
        public int CurrentStock
        {
            get { return intCurrentStock; }
            set { intCurrentStock = value; SetCaption(); }
        }


        [Category("POS")]
        [Description("Gets or sets the Background for this item")]
        public string ItemBackground
        {
            get { return strItemBackground; }
            set { strItemBackground = value; }
        }

        [Category("POS")]
        [Description("Gets or sets the Application Style for this item")]
        public string ApplicationStyle
        {
            get { return strApplicationStyle; }
            set { strApplicationStyle = value; }
        }

        [Category("POS")]
        [Description("Gets or sets the color for this item")]
        public string ItemColor
        {
            get { return strItemColor; }
            set { strItemColor = value; ChangeColor(); } //ChangeColor();
        }

        [Category("POS")]
        [Description("Gets or sets the Style for this item")]
        public string ItemStyle
        {
            get { return strItemStyle; }
            set { strItemStyle = value; ChangeStyle(); } // ChangeStyle();
        }

        [Category("POS")]
        [Description("Gets or sets the Font for this item")]
        public Font ItemFont
        {
            get { return fntFont; }
            set { fntFont = value; this.Font = fntFont; }
        }


        [Category("POS")]
        [Description("Gets or sets the forecolor for this item")]
        public string ItemForeColor
        {
            get { return strItemForeColor; }
            set { strItemForeColor = value; GetForeColor(); } //GetForeColor();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

            
        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }

        public void SetCaption()
        {
            if (boolDisplayStockinPOS)
            {
                Text = boolShowSKU ? strItemSKU + " - " + strItemName + " (" + intCurrentStock.ToString() + ")" : strItemName + " (" + intCurrentStock.ToString() + ")";
            }
            else
            {
                Text = boolShowSKU ? strItemSKU + " - " +  strItemName : strItemName;
            }
        }


        private void ChangeColor()
        {
            if (strItemBackground == "Color")
            {
                if ((strItemColor == "") || (strItemColor.StartsWith("0")))
                {
                    this.LookAndFeel.UseDefaultLookAndFeel = true;
                    this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
                }
                else
                {
                    this.LookAndFeel.UseDefaultLookAndFeel = false;
                    this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
                    this.Appearance.BackColor = Color.FromName(strItemColor);
                    this.Appearance.BackColor2 = GetGradientColor(this.Appearance.BackColor);
                    this.Refresh();
                }
            }
        }

        private void ChangeStyle()
        {
            if (strItemBackground == "Skin")
            {
                if (strApplicationStyle == "")
                {
                    this.LookAndFeel.UseDefaultLookAndFeel = true;
                    this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
                }
                else
                {
                    this.LookAndFeel.UseDefaultLookAndFeel = false;
                    this.LookAndFeel.SetSkinStyle(strItemStyle);
                    this.Refresh();
                }
            }
        }

        private Color GetGradientColor(Color StartColor)
        {
            int R; int G; int B;
            R = StartColor.R;
            G = StartColor.G;
            B = StartColor.B;

            R = Math.Max(R - 50, 0);
            G = Math.Max(G - 50, 0);
            B = Math.Max(B - 50, 0);
            StartColor = Color.FromArgb(R, G, B);
            return StartColor;
        }

        private void GetForeColor()
        {
            if ((strItemForeColor == "") || (strItemForeColor.StartsWith("0")))
            {
                //this.ForeColor = Color.Transparent;
            }
            else
            {
                this.ForeColor = Color.FromName(strItemForeColor);
            }
        }

        public void SetHeightForButtonType1(int NewHeight)
        {
            this.Height = NewHeight;
        }

        public void SetHeightForButtonType2(int NewHeight)
        {
            this.Height = NewHeight;
        }
    }
}
