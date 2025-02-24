using System;
using System.Collections.Generic;
using System.Data;
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
using OfflineRetailV2.UserControls.POSSection;

namespace OfflineRetailV2.UserControls
{
    /// <summary>
    /// Interaction logic for frm_QuestionDlg.xaml
    /// </summary>
    public partial class frm_QuestionDlg : Window
    {
        public frm_QuestionDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnClose);

            Closing += Frm_QuestionDlg_Closing;
            Loaded += Frm_QuestionDlg_Loaded;
        }

        private AddProductWindow frmBrowse;
        private int intID;
        private int intNewID;
        private bool boolControlChanged;
        private int intRefID;
        private int intHeaderID;
        private int intLevelID;
        private string strQuestionType;
        private string strQuestionCategory;
        public int ID
        {
            get { return intID; }
            set { intID = value; }
        }

        public int NewID
        {
            get { return intNewID; }
            set { intNewID = value; }
        }

        public int RefID
        {
            get { return intRefID; }
            set { intRefID = value; }
        }

        public int HeaderID
        {
            get { return intHeaderID; }
            set { intHeaderID = value; }
        }

        public int LevelID
        {
            get { return intLevelID; }
            set { intLevelID = value; }
        }

        public string QuestionType
        {
            get { return strQuestionType; }
            set { strQuestionType = value; }
        }

        public string QuestionCategory
        {
            get { return strQuestionCategory; }
            set { strQuestionCategory = value; }

        }
        public AddProductWindow BrowseForm
        {
            get
            {
                if (frmBrowse == null)
                    frmBrowse = new AddProductWindow();
                return frmBrowse;
            }
            set
            {
                frmBrowse = value;
            }
        }

        private bool IsValidAll()
        {
            if (txtQuestion.Text.Trim() == "")
            {
                DocMessage.MsgEnter( Properties.Resources.Question);
                GeneralFunctions.SetFocus(txtQuestion);
                return false;
            }
            return true;
        }

        private bool SaveData()
        {
            string strError = "";
            PosDataObject.Questions objQuestions = new PosDataObject.Questions();
            objQuestions.Connection = SystemVariables.Conn;
            objQuestions.LoginUserID = SystemVariables.CurrentUserID;
            objQuestions.RefID = intRefID;
            objQuestions.HeaderID = intHeaderID;
            objQuestions.LevelID = intLevelID;
            objQuestions.QType = strQuestionType;
            objQuestions.QDesc = txtQuestion.Text.Trim();

            objQuestions.ID = intID;

            if (intID == 0)
            {
                strError = objQuestions.InsertData();
                NewID = objQuestions.ID;
            }
            else
            {
                strError = objQuestions.UpdateData();
            }
            if (strError == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void ShowData()
        {
            PosDataObject.Questions objQuestions = new PosDataObject.Questions();
            objQuestions.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            dbtbl = objQuestions.ShowRecord(intID);
            foreach (DataRow dr in dbtbl.Rows)
            {
                txtQuestion.Text = dr["Question"].ToString();
                intHeaderID = GeneralFunctions.fnInt32(dr["HeaderID"].ToString());
                intLevelID = GeneralFunctions.fnInt32(dr["LevelID"].ToString());
                intRefID = GeneralFunctions.fnInt32(dr["RefID"].ToString());
            }
            dbtbl.Dispose();
        }
        private void Frm_QuestionDlg_Loaded(object sender, RoutedEventArgs e)
        {
            if (intID == 0)
            {
                if (strQuestionCategory == "") Title.Text = " Add " + strQuestionType + " Question";
                if (strQuestionCategory == "R") Title.Text = " Add Root " + strQuestionType + " Question";
            }
            else
            {
                ShowData();
                if (strQuestionCategory == "") Title.Text = " Edit " + strQuestionType + " Question";
                if (strQuestionCategory == "R") Title.Text = " Edit Root " + strQuestionType + " Question";
            }
            boolControlChanged = false;
        }

        private void Frm_QuestionDlg_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (boolControlChanged)
            {
                MessageBoxResult DlgResult = new MessageBoxResult();
                DlgResult = DocMessage.MsgSaveChanges();

                if (DlgResult == MessageBoxResult.Yes)
                {
                    if (IsValidAll())
                    {
                        if (SaveData())
                        {
                            boolControlChanged = false;
                        }
                        else
                            e.Cancel = true;
                    }
                    else
                        e.Cancel = true;
                }

                if (DlgResult == MessageBoxResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void OnClose(object obj)
        {
            DialogResult = false;
            Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidAll())
            {
                if (SaveData())
                {
                    boolControlChanged = false;
                    BrowseForm.CreateQuestionTree();
                    Close();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            boolControlChanged = false;
            DialogResult = false;
            Close();
        }

        private void txtQuestion_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }
    }
}
