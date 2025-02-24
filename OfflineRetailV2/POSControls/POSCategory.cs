using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using DevExpress.XtraEditors;

namespace POSControls
{
    public class POSCategory : DevExpress.XtraEditors.GroupControl
    {
        private System.ComponentModel.Container components = null;
        //private SimpleButton AttachButton;
        public Label HeaderCaption;
        private int intCategoryID = 0;
        private string strCategoryName;
        private int intMaxProducts = 0;
        private string strCategoryColor = "";
        private string strCategoryStyle = "";
        private string strCategoryBackground = "";
        private string strApplicationStyle = "";
        private Font fntFont;
        private string strCategoryForeColor = "";
        private int intParentCategoryID = 0;
        private string strCategoryFontFamily = "";
        private string strCategoryFontSize = "";
        private string strCategoryFontBold = "";
        private string strCategoryFontItalic = "";

        private string strScaleCategoryID = "";
        
        public POSCategory()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.AutoSize = true;
            //this.Padding = new Padding(5, 2, 5, 10);
            this.AppearanceCaption.Font = new Font("Tahoma", 24, FontStyle.Bold);
            this.Dock = DockStyle.Top;
            this.Text = "";
            AddChildControls();

            PositionButton();
        }

        public void AddChildControls()
        {
            HeaderCaption = new Label();
            //HeaderCaption.Font = new Font("Tahoma", 9, FontStyle.Bold);
            HeaderCaption.AutoSize = false;
            HeaderCaption.Cursor = Cursors.Hand;
            HeaderCaption.TextAlign = ContentAlignment.MiddleCenter;
            HeaderCaption.Top = 0;
            HeaderCaption.Left = 0;
            HeaderCaption.Height = 38;
            HeaderCaption.BackColor = Color.Transparent;
            HeaderCaption.Parent = this;
            HeaderCaption.Visible = true;
            HeaderCaption.Width = Width;
        }


        public int ParentCategoryID
        {
            get { return intParentCategoryID; }
            set { intParentCategoryID = value; }
        }
        public string CategoryFontFamily
        {
            get { return strCategoryFontFamily; }
            set { strCategoryFontFamily = value; }
        }

        public string CategoryFontSize
        {
            get { return strCategoryFontSize; }
            set { strCategoryFontSize = value; }
        }

        public string CategoryFontBold
        {
            get { return strCategoryFontBold; }
            set { strCategoryFontBold = value; }
        }

        public string CategoryFontItalic
        {
            get { return strCategoryFontItalic; }
            set { strCategoryFontItalic = value; }
        }

        [Category("POS")]
        [Description("Gets or sets the Category ID of Database Table")]
        public int CategoryID
        {
            get { return intCategoryID; }
            set { intCategoryID = value; }
        }
        
        [Category("POS")]
        [Description("Gets or sets the Category Name of Database Table")]
        public string CategoryName
        {
            get { return strCategoryName; }
            set { strCategoryName = value; HeaderCaption.Text = strCategoryName; }
        }

        [Category("POS")]
        [Description("Gets or sets the Max Products for this Category")]
        public int MaxProducts
        {
            get { return intMaxProducts; }
            set { intMaxProducts = value; }
        }
        
        [Category("POS")]
        [Description("Gets or sets the Background for this Category")]
        public string CategoryBackground
        {
            get { return strCategoryBackground; }
            set { strCategoryBackground = value; }
        }

        [Category("POS")]
        [Description("Gets or sets the Application Style for this Category")]
        public string ApplicationStyle
        {
            get { return strApplicationStyle; }
            set { strApplicationStyle = value; }
        }

        [Category("POS")]
        [Description("Gets or sets the color for this Category")]
        public string CategoryColor
        {
            get { return strCategoryColor; }
            set { strCategoryColor = value; ChangeColor(); } //ChangeColor(); 
        }

        [Category("POS")]
        [Description("Gets or sets the Style for this Category")]
        public string CategoryStyle
        {
            get { return strCategoryStyle; }
            set { strCategoryStyle = value; ChangeStyle(); } // ChangeStyle();
        }

        [Category("POS")]
        [Description("Gets or sets the Font for this Category")]
        public Font CategoryFont
        {
            get { return fntFont; }
            set { fntFont = value; HeaderCaption.Font = fntFont; }
        }

        
        [Category("POS")]
        [Description("Gets or sets the forecolor for this Category")]
        public string CategoryForeColor
        {
            get { return strCategoryForeColor; }
            set { strCategoryForeColor = value; GetForeColor(); } //  GetForeColor(); 
        }


        [Category("SCALE")]
        [Description("Gets or sets the Category ID of Database Table")]
        public string ScaleCategoryID
        {
            get { return strScaleCategoryID; }
            set { strScaleCategoryID = value; }
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
            
            //components = new System.ComponentModel.Container();

            this.ResumeLayout(false);
        }

        private void PositionButton()
        {
            HeaderCaption.Width = Width;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            PositionButton();
        }


        private void ChangeColor()
        {
            if (strCategoryBackground == "Color")
            {
                if ((strCategoryColor == "") || (strCategoryColor.StartsWith("0")))
                {
                    this.LookAndFeel.UseDefaultLookAndFeel = true;
                    this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
                }
                else
                {
                    this.LookAndFeel.UseDefaultLookAndFeel = false;
                    this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
                    this.AppearanceCaption.BackColor = Color.FromName(strCategoryColor);
                    this.AppearanceCaption.BackColor2 = GetGradientColor(this.AppearanceCaption.BackColor);
                    this.Refresh();
                }
            }
        }

        private void ChangeStyle()
        {
            if (strCategoryBackground == "Skin")
            {
                if (strApplicationStyle == "")
                {
                    this.LookAndFeel.UseDefaultLookAndFeel = true;
                    this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
                }
                else
                {
                    this.LookAndFeel.UseDefaultLookAndFeel = false;
                    this.LookAndFeel.SetSkinStyle(strCategoryStyle);
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
            if ((strCategoryForeColor == "") || (strCategoryForeColor.StartsWith("0")))
            {
                //HeaderCaption.ForeColor = Color.Transparent;
            }
            else
            {
                HeaderCaption.ForeColor = Color.FromName(strCategoryForeColor);
            }
        }

        public void SetScaleControlHeight(float CaptionFontSize, int CaptionHeight )
        {
            this.AppearanceCaption.Font = new Font("Tahoma", CaptionFontSize, FontStyle.Bold);
            HeaderCaption.Height = CaptionHeight;
        }
    }
}
