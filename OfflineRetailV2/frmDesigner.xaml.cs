using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OfflineRetailV2.Data;
using System.Data;
using System.Data.SqlClient;
using DevExpress.XtraReports.UI;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm;
using System.Text.RegularExpressions;

namespace OfflineRetailV2
{
    /// <summary>
    /// Interaction logic for frm_GLDlg.xaml
    /// </summary>
    public partial class frmDesigner : Window
    {

        private bool boolSaveDesigner;
        public bool SaveDesigner
        {
            get { return boolSaveDesigner; }
            set { boolSaveDesigner = value; }
        }
        private int intID;
        private string strTemplateType;
        private string strTemplateName;
        private bool boolControlChanged;

        public int ID
        {
            get { return intID; }
            set { intID = value; }
        }

        public string TemplateType
        {
            get { return strTemplateType; }
            set { strTemplateType = value; }
        }

        public string TemplateName
        {
            get { return strTemplateName; }
            set { strTemplateName = value; }
        }

      

        public frmDesigner()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnCloseCOmmand);
        }

        private void OnCloseCOmmand(object obj)
        {
            
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Regex illegalInFileName = new Regex(string.Format("[{0}]", Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars()))), RegexOptions.Compiled);

            strTemplateName = illegalInFileName.Replace(strTemplateName, "");

            string repx = GeneralFunctions.CheckLabelPrintingCustomisedFile(strTemplateType, intID == 0 ? "new" : intID.ToString());
            if (repx == "")
            {
                rd.OpenDocument(GeneralFunctions.GetLabelPrintingDefaultFile(strTemplateType));
            }
            else
            {
                rd.OpenDocument(repx);
            }

        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            
            Close();
        }

        private void TxtID_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private bool IsValidAll()
        {
           
            return true;
        }

        





        private void Full_GotFocus(object sender, RoutedEventArgs e)
        {
            

        }

        private void FKybrd_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           
        }

        private void Full_LostFocus(object sender, RoutedEventArgs e)
        {
            
        }

        private void CloseDocumentButton_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                rd.ActiveDocument.Report.SaveLayout(GeneralFunctions.GetLabelPrintingCustomisedFilePathForSave(strTemplateType, intID == 0 ? "new" : intID.ToString()));
                if (intID == 0) boolSaveDesigner = true;
                Close();
            }
            catch
            {

            }
            
        }

        private void Rd_DocumentClosing(object sender, DevExpress.Xpf.Reports.UserDesigner.ReportDesignerDocumentClosingEventArgs e)
        {
            e.ShowConfirmationMessageIfDocumentContainsUnsavedChanges = false;
        }
    }

    public class MainWindowViewModel : ViewModelBase
    {
        public XtraReport Report { get; private set; }
        public ICommand TestCommand { get; private set; }

        public MainWindowViewModel(XtraReport report)
        {
            Report = report;
            TestCommand = new DelegateCommand(RunTestCommand);
        }

        void RunTestCommand()
        {
            GetService<IMessageBoxService>().ShowMessage("Test command.");
        }
    }
}
