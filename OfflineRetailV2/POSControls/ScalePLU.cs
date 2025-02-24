using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;

namespace POSControls
{
    public class ScalePLU : DevExpress.XtraEditors.SimpleButton
    //public class POSItem : System.Windows.Forms.Button
    {
        private System.ComponentModel.Container components = null;
        private int intItemID = 0;
        private string strPLU;
        private string strItemName;

        public ScalePLU(int ButtonType)
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
                this.Height = 75;
                this.Width = 120;
            }
            this.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.AllowFocus = false;
            this.DoubleBuffered = true;
            this.Appearance.Font = new Font("Tahome", 14.25f);
            //this.FlatAppearance.BorderSize = 0;
            //this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            //this.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //this.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
        }

        
        public int ItemID
        {
            get { return intItemID; }
            set { intItemID = value; }
        }
        
        public string PLU
        {
            get { return strPLU; }
            set { strPLU = value; SetCaption(); }
        }

        public string ItemName
        {
            get { return strItemName; }
            set { strItemName = value; }
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
            Text = strPLU;
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