using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Controls;
using System.Windows;
using DevExpress.Utils;
using System.Windows.Media;

namespace POSControls
{
    public partial class POSCoupon : Button
    //public class POSItem : System.Windows.Forms.Button
    {
        private System.ComponentModel.Container components = null;
        private int intItemID = 0;
        private string strItemType;
        private string strItemName;
        private string strItemAmount;
        private string strItemGridName;
        private string strCheckDiscount;
        private string strCheckTax;
        private string strCheckQty;
        private string strCurrencySymbol;
        private string strDisplayText;
        public POSCoupon()
        {
            InitializeComponent();
            this.Height = 100;
            this.Width = 150;
            this.Background = System.Windows.Media.Brushes.Transparent;
            this.Focusable = false;
        }

        [Category("POS")]
        [Description("Gets or sets the Currency Symbol")]
        public string CurrencySymbol
        {
            get { return strCurrencySymbol; }
            set { strCurrencySymbol = value; }
        }

        [Category("POS")]
        [Description("Gets or sets the Item ID of Database Table")]
        public int ItemID
        {
            get { return intItemID; }
            set { intItemID = value; }
        }

        
        [Category("POS")]
        [Description("Gets or sets the Item Type of Database Table")]
        

        public string ItemName
        {
            get { return strItemName; }
            set { strItemName = value; }
        }

        public string ItemGridName
        {
            get { return strItemGridName; }
            set { strItemGridName = value; }
        }

        public string DisplayText
        {
            get { return strDisplayText; }
            set { strDisplayText = value; }
        }

        [Category("POS")]
        [Description("Gets or sets Displaying of Stocl Level")]
        public string ItemAmount
        {
            get { return strItemAmount; }
            set { strItemAmount = value; }
        }

        public string ItemType
        {
            get { return strItemType; }
            set { strItemType = value; SetCaption(); }
        }


        public string CheckDiscount
        {
            get { return strCheckDiscount; }
            set { strCheckDiscount = value; }
        }

        public string CheckTax
        {
            get { return strCheckTax; }
            set { strCheckTax = value; }
        }

        public string CheckQty
        {
            get { return strCheckQty; }
            set { strCheckQty = value; }
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        if (components != null)
        //        {
        //            components.Dispose();
        //        }
        //    }
        //    base.Dispose(disposing);
        //}


        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }

        public void SetCaption()
        {
            string contenttext = "";
            if (strItemType == "P")
            {
                contenttext = strItemName + "\n" + "@ "+strItemAmount+"%";
                strItemGridName = strItemName + "  @ " + strItemAmount + "%";
            }
            if (strItemType == "A")
            {
                contenttext = strItemName + "\n" + "@ " + strCurrencySymbol + strItemAmount;
                strItemGridName = strItemName + "  @ " + strCurrencySymbol + strItemAmount;
            }

            if (strItemType == "N")
            {
                contenttext = strItemName;
                strItemGridName = strItemName;
            }

            strDisplayText = contenttext;
        }


        
    }
}
