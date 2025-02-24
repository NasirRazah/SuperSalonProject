using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace OfflineRetailV2.UserControls.Report
{
    /// <summary>
    /// Interaction logic for frm_PreviewControl.xaml
    /// </summary>
    public partial class frm_PreviewControl : Window
    {
        public frm_PreviewControl()
        {
            InitializeComponent();
        }

        private string strReportName;
        public string ReportName
        {
            get { return strReportName; }
            set { strReportName = value; }
        }

        protected XtraReport fReport;
        protected string fileName = "";

        public class Helper
        {
            public const int ICC_USEREX_CLASSES = 0x00000200;

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public class INITCOMMONCONTROLSEX
            {
                public int dwSize = 8; //ndirect.DllLib.sizeOf(this);
                public int dwICC;
            }
            [DllImport("comctl32.dll")]
            public static extern bool InitCommonControlsEx(INITCOMMONCONTROLSEX icc);

            [DllImport("kernel32.dll")]
            public static extern IntPtr LoadLibrary(string libname);

            [DllImport("kernel32.dll")]
            public static extern bool FreeLibrary(IntPtr hModule);

            public static string GetReportPath(DevExpress.XtraReports.UI.XtraReport fReport, string ext)
            {
                System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
                string repName = fReport.Name;
                if (repName.Length == 0) repName = fReport.GetType().Name;
                string dirName = Path.GetDirectoryName(asm.Location);
                return Path.Combine(dirName, repName + "." + ext);
            }
            public static string GetRelativePath(string name)
            {
                return DevExpress.Utils.FilesHelper.FindingFileName(Environment.CurrentDirectory, "Data\\" + name);
            }
            public static string GetRelativeStyleSheetPath(string styleSheetPath)
            {
                if (Assembly.GetEntryAssembly() != Assembly.GetExecutingAssembly())
                    return styleSheetPath;
                int index = styleSheetPath.LastIndexOf(@"\");
                return GetRelativePath(styleSheetPath.Substring(++index));
            }
            public static System.Drawing.Image LoadImage(string name)
            {
                Bitmap bmp = new Bitmap(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("XtraReportsDemos." + name));
                bmp.MakeTransparent(Color.Magenta);
                return bmp;
            }
        }

        public XtraReport Report
        {
            get { return fReport; }
            set
            {
                if (fReport != value)
                {
                    if (fReport != null)
                        fReport.Dispose();
                    fReport = value;
                    if (fReport == null)
                        return;
                    //printingSystem.ClearContent();--Sam
                    //Invalidate();
                    //Update();
                    fileName = Helper.GetReportPath(fReport, "repx");
                    //fReport.PrintingSystem = printingSystem;
                    fReport.CreateDocument();
                    //printControl.ExecCommand(PrintingSystemCommand.DocumentMap, new object[] {false});
                    //previewBar.Buttons[0].Pushed = false;
                }
            }
        }

        public void Activate()
        {
            /*ProgressReflector.RegisterReflector(pnlCtrl.ProgressReflector);
            try
            {
                base.Activate();
            }
            finally
            {
                ProgressReflector.MaximizeValue();
                ProgressReflector.UnregisterReflector(pnlCtrl.ProgressReflector);
            }*/
        }

        protected virtual XtraReport CreateReport()
        {
            return new OfflineRetailV2.Report.Customer.repCustomerSnap();
        }

        private void ShowDesignerForm(System.Windows.Forms.Form designForm, System.Windows.Forms.Form parentForm)
        {
            designForm.MinimumSize = parentForm.MinimumSize;
            if (parentForm.WindowState == System.Windows.Forms.FormWindowState.Normal)
                designForm.Bounds = parentForm.Bounds;
            designForm.WindowState = parentForm.WindowState;
            parentForm.Visible = false;
            designForm.ShowDialog();
            parentForm.Visible = true;
        }
    }
}
