using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using DevExpress.XtraEditors;

namespace POSControls
{
    public class ScaleCategory : DevExpress.XtraEditors.GroupControl
    {
        private System.ComponentModel.Container components = null;
        //private SimpleButton AttachButton;
        public Label HeaderCaption;
        private string strCategoryID = "";
        private string strCategoryName;
        private int intMaxProducts = 0;
        private string strCategoryColor = "";
        private string strCategoryStyle = "";
        private string strCategoryBackground = "";
        private string strApplicationStyle = "";
        private Font fntFont;
        private string strCategoryForeColor = "";

        private int intAddHeightPerc = 0;

        public ScaleCategory(int intHPerc)
        {
            InitializeComponent();
            intAddHeightPerc = intHPerc;
            this.DoubleBuffered = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.AutoSize = true;
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

            ChangeControlHeight();
        }

        

        [Category("POS")]
        [Description("Gets or sets the Category ID of Database Table")]
        public string CategoryID
        {
            get { return strCategoryID; }
            set { strCategoryID = value; }
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
            //ChangeControlHeight();
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

        private void ChangeControlHeight()
        {
            if (intAddHeightPerc != 0)
            {
                float NewFontSize = 24 + Convert.ToInt32(Math.Ceiling(Convert.ToDouble(24 * intAddHeightPerc / 100)));
                this.AppearanceCaption.Font = new Font("Tahoma", NewFontSize, FontStyle.Bold);
                HeaderCaption.Height = 38 + Convert.ToInt32(Math.Ceiling(Convert.ToDouble(38 * intAddHeightPerc / 100)));
            }
            else
            {
                this.AppearanceCaption.Font = new Font("Tahoma", 24, FontStyle.Bold);
                HeaderCaption.Height = 38;
            }
        }



    }
}
