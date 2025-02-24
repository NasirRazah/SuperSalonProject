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
using System.IO;
using Microsoft.Win32;

namespace OfflineRetailV2.UserControls.POSSection
{
    /// <summary>
    /// Interaction logic for frm_CustNotesDlg.xaml
    /// </summary>
    public partial class frm_CustNotesDlg : Window
    {
        public frm_CustNotesDlg()
        {
            InitializeComponent();
            ModalWindow.CloseCommand = new CommandBase(OnClose);
        }

        private void OnClose(object obj)
        {
            CloseKeyboards();
            Close();
        }
        private frm_CustomerDlg frmBrowsec;

        private frm_EmployeeDlg frmBrowsee;

        private int intID;
        private int intRefID;
        private string strRefType;
        private int intNewID;
        private bool boolControlChanged;

        private int ReScan = 0;
        private string strPhotoFile = "";
        private string pstrPhotoFile = "";
        private double intImageWidth;
        private double intImageHeight;
        private string csStorePath = "";
        private string docpath = "";
        private string pdocpath = "";

        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;

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

        public string RefType
        {
            get { return strRefType; }
            set { strRefType = value; }
        }

        public frm_CustomerDlg BrowseFormC
        {
            get
            {
                return frmBrowsec;
            }
            set
            {
                frmBrowsec = value;
            }
        }

        public frm_EmployeeDlg BrowseFormE
        {
            get
            {
                return frmBrowsee;
            }
            set
            {
                frmBrowsee = value;
            }
        }

        private bool IsValidAll()
        {
            if ((pictPhoto.Source == null) && (docpath == ""))
            {
                if (txtNote.Text.Trim() == "")
                {
                    DocMessage.MsgEnter("Note");
                    GeneralFunctions.SetFocus(txtNote);
                    return false;
                }
            }
            if (chkEvent.IsChecked == true)
            {
                if (dtNote.EditValue == null)
                {
                    DocMessage.MsgEnter("Event DateTime");
                    GeneralFunctions.SetFocus(dtNote);
                    return false;
                }
            }
            return true;
        }

        private bool SaveData()
        {
            DateTime FormatDateTime = Convert.ToDateTime(null);
            FormatDateTime = new DateTime(dtNote.DateTime.Year, dtNote.DateTime.Month,
                dtNote.DateTime.Day, dTime.DateTime.Hour, dTime.DateTime.Minute, dTime.DateTime.Second, dTime.DateTime.Kind);
            string strError = "";
            PosDataObject.Notes objNotes = new PosDataObject.Notes();
            objNotes.Connection = SystemVariables.Conn;
            objNotes.LoginUserID = SystemVariables.CurrentUserID;
            objNotes.RefType = strRefType;
            objNotes.RefID = intRefID;
            objNotes.Note = txtNote.Text.Trim();

            objNotes.ID = intID;


            if (chkEvent.IsChecked == true)
            {
                objNotes.SpecialEvent = "Y";
                objNotes.DateTime = FormatDateTime;
            }
            else
            {
                objNotes.SpecialEvent = "N";
                objNotes.DateTime = Convert.ToDateTime(null);
            }

            if (intID == 0)
            {
                strError = objNotes.InsertNoteData();
                NewID = objNotes.ID;
            }
            else
            {
                strError = objNotes.UpdateNoteData();
                NewID = intID;
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
            PosDataObject.Notes objNotes = new PosDataObject.Notes();
            objNotes.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            dbtbl = objNotes.ShowNoteRecord(intID);
            string sdoc = "";
            string sscan = "";
            foreach (DataRow dr in dbtbl.Rows)
            {
                txtNote.Text = dr["Note"].ToString();

                if (dr["SpecialEvent"].ToString() == "Y")
                {
                    chkEvent.IsChecked = true;
                    dtNote.EditValue = GeneralFunctions.fnDate(dr["DateTime"].ToString());
                    dTime.DateTime = GeneralFunctions.fnDate(dr["DateTime"].ToString());
                }
                else
                {
                    chkEvent.IsChecked = false;
                    dtNote.EditValue = null;
                    dTime.DateTime = Convert.ToDateTime(null);
                }
                sdoc = dr["DocumentFile"].ToString();
                sscan = dr["ScanFile"].ToString();
            }
            dbtbl.Dispose();

            txtDoc.Text = sdoc;

            csStorePath = "";
            csStorePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);


            intImageWidth = 128;
            intImageHeight = 112;
            if (strRefType == "Customer")
            {
                if (csStorePath.EndsWith("\\")) csStorePath = csStorePath + "XEPOS\\RetailV2\\Customer\\Notes\\";
                else csStorePath = csStorePath + "\\XEPOS\\RetailV2\\Customer\\Notes\\";
                if (sdoc != "")
                {
                    DirectoryInfo di = new DirectoryInfo(csStorePath);
                    FileInfo[] fi = di.GetFiles();
                    foreach (FileInfo f in fi)
                    {
                        if (f.Name.Contains(intID.ToString() + "_d"))
                        {
                            docpath = csStorePath + f.Name;
                            pdocpath = docpath;
                            break;
                        }
                    }
                }

                if (sscan != "")
                {
                    DirectoryInfo di = new DirectoryInfo(csStorePath);
                    FileInfo[] fi = di.GetFiles();
                    foreach (FileInfo f in fi)
                    {
                        if (f.Name.Contains(intID.ToString() + "_s"))
                        {
                            strPhotoFile = csStorePath + f.Name;
                            pstrPhotoFile = strPhotoFile;
                            break;
                        }
                    }
                }
            }

            if (strRefType == "Employee")
            {
                if (csStorePath.EndsWith("\\")) csStorePath = csStorePath + "XEPOS\\RetailV2\\Employee\\Notes\\";
                else csStorePath = csStorePath + "\\XEPOS\\RetailV2\\Employee\\Notes\\";
                if (sdoc != "")
                {
                    DirectoryInfo di = new DirectoryInfo(csStorePath);
                    FileInfo[] fi = di.GetFiles();
                    foreach (FileInfo f in fi)
                    {
                        if (f.Name.Contains(intID.ToString() + "_d"))
                        {
                            docpath = csStorePath + f.Name;
                            pdocpath = docpath;
                            break;
                        }
                    }
                }

                if (sscan != "")
                {
                    DirectoryInfo di = new DirectoryInfo(csStorePath);
                    FileInfo[] fi = di.GetFiles();
                    foreach (FileInfo f in fi)
                    {
                        if (f.Name.Contains(intID.ToString() + "_s"))
                        {
                            strPhotoFile = csStorePath + f.Name;
                            pstrPhotoFile = strPhotoFile;
                            break;
                        }
                    }
                }
            }

            OpenFileDialog op = new OpenFileDialog();
            op.FileName = strPhotoFile;

            if (sscan == "")
            {
                pictPhoto.Source = null;
            }
            else
            {
                try
                {
                    
                    //FileStream fs = new FileStream(strPhotoFile, FileMode.Open);
                    pictPhoto.Source = new BitmapImage(new Uri(op.FileName));
                    
                    /*double AspectRatio = 0.00;
                    double intWidth, intHeight;
                    AspectRatio = GeneralFunctions.fnDouble(pictPhoto.Source.Width) /
                        GeneralFunctions.fnDouble(pictPhoto.Source.Height);
                    intHeight = pictPhoto.Height;
                    intWidth = Convert.ToInt16(GeneralFunctions.fnDouble(intHeight) * AspectRatio);

                    if (intWidth > intImageWidth)
                    {
                        intWidth = intImageWidth;
                        intHeight = Convert.ToInt16(GeneralFunctions.fnDouble(intWidth) / AspectRatio);
                    }
                    pictPhoto.Width = intWidth;
                    pictPhoto.Height = intHeight;*/
                }
                catch
                {
                    pictPhoto.Source = null;
                }
            }
        }


        public void savedoc(string pfile)
        {
            string cssp = "";
            cssp = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (strRefType == "Customer")
            {
                if (cssp.EndsWith("\\")) cssp = cssp + "XEPOS\\RetailV2\\Customer\\Notes\\";
                else cssp = cssp + "\\XEPOS\\RetailV2\\Customer\\Notes\\";
            }

            if (strRefType == "Employee")
            {
                if (cssp.EndsWith("\\")) cssp = cssp + "XEPOS\\RetailV2\\Employee\\Notes\\";
                else cssp = cssp + "\\XEPOS\\RetailV2\\Employee\\Notes\\";
            }

            if (!Directory.Exists(cssp)) Directory.CreateDirectory(cssp);
            if (File.Exists(pfile))
            {
                FileInfo fi = new FileInfo(pfile);
                string ext = fi.Extension;
                string filename = fi.Name;
                fi.CopyTo(cssp + NewID.ToString() + "_d" + ext, true);
                updatedocfile(filename);
            }
        }

        public void deldoc(string pfile)
        {
            if (File.Exists(pfile))
            {
                FileInfo fi = new FileInfo(pfile);
                fi.Delete();
                updatedocfile("");
            }
        }

        public void delscan(string pfile)
        {
            string cssp = "";
            cssp = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (strRefType == "Customer")
            {
                if (cssp.EndsWith("\\")) cssp = cssp + "XEPOS\\RetailV2\\Customer\\Notes\\";
                else cssp = cssp + "\\XEPOS\\RetailV2\\Customer\\Notes\\";
            }
            if (strRefType == "Employee")
            {
                if (cssp.EndsWith("\\")) cssp = cssp + "XEPOS\\RetailV2\\Employee\\Notes\\";
                else cssp = cssp + "\\XEPOS\\RetailV2\\Employee\\Notes\\";
            }
            if (File.Exists(pfile))
            {
                try
                {
                    FileInfo fi = new FileInfo(pfile);
                    fi.Delete();
                    updatescanfile("");
                }
                catch
                {
                }
            }
        }

        public void savescan(string pfile)
        {
            string cssp = "";
            cssp = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (strRefType == "Customer")
            {
                if (cssp.EndsWith("\\")) cssp = cssp + "XEPOS\\RetailV2\\Customer\\Notes\\";
                else cssp = cssp + "\\XEPOS\\RetailV2\\Customer\\Notes\\";
            }

            if (strRefType == "Employee")
            {
                if (cssp.EndsWith("\\")) cssp = cssp + "XEPOS\\RetailV2\\Employee\\Notes\\";
                else cssp = cssp + "\\XEPOS\\RetailV2\\Employee\\Notes\\";
            }
            if (!Directory.Exists(cssp)) Directory.CreateDirectory(cssp);
            if (File.Exists(pfile))
            {
                FileInfo fi = new FileInfo(pfile);
                string ext = fi.Extension;
                string filename = fi.Name;
                fi.CopyTo(cssp + NewID.ToString() + "_s" + ext, true);
                updatescanfile(filename);
            }
        }

        private void updatedocfile(string pfile)
        {
            PosDataObject.Notes objnt = new PosDataObject.Notes();
            objnt.Connection = SystemVariables.Conn;
            objnt.UpdateDocumentFile(NewID, pfile);
        }

        private void updatescanfile(string pfile)
        {
            PosDataObject.Notes objnt = new PosDataObject.Notes();
            objnt.Connection = SystemVariables.Conn;
            objnt.UpdateScanFile(NewID, pfile);
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fkybrd = new FullKeyboard();
            Date.Visibility = Visibility.Hidden;
            dtNote.Visibility = Visibility.Hidden;
            dTime.Visibility = Visibility.Hidden;

            
            if (intID == 0)
            {
                Title.Text = "Add" + " " + strRefType + " " + "Note";
                dtNote.EditValue = DateTime.Now;
                dTime.DateTime = DateTime.Now;
            }
            else
            {
                ShowData();
                Title.Text = "Edit" + " " + strRefType + " " + "Note";
            }
            GeneralFunctions.SetFocus(txtNote);
            txtNote.SelectionLength = 1;
            txtNote.SelectionStart = txtNote.Text.Length;

            csStorePath = "";
            csStorePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (strRefType == "Customer")
            {
                if (csStorePath.EndsWith("\\")) csStorePath = csStorePath + "XEPOS\\RetailV2\\Customer\\Notes\\";
                else csStorePath = csStorePath + "\\XEPOS\\RetailV2\\Customer\\Notes\\";
            }

            if (strRefType == "Employee")
            {
                if (csStorePath.EndsWith("\\")) csStorePath = csStorePath + "XEPOS\\RetailV2\\Employee\\Notes\\";
                else csStorePath = csStorePath + "\\XEPOS\\RetailV2\\Employee\\Notes\\";
            }

            intImageWidth = 128;
            intImageHeight = 112;

            boolControlChanged = false;
        }

        private void ChkEvent_Checked(object sender, RoutedEventArgs e)
        {
            boolControlChanged = true;

            if (chkEvent.IsChecked == true)
            {
                Date.Visibility = Visibility.Visible;
                dtNote.Visibility = Visibility.Visible;
                dTime.Visibility = Visibility.Visible;

                if (dtNote.EditValue == null)
                {
                    dtNote.EditValue = DateTime.Now;
                    dTime.EditValue = DateTime.Now;
                }
            }
            else
            {
                Date.Visibility = Visibility.Hidden;
                dtNote.Visibility = Visibility.Hidden;
                dTime.Visibility = Visibility.Hidden;
            }
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png;*.gif;*.bmp|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png|" +
              "GIF Files(*.gif) | *.gif|Bitmap Files(*.bmp) | *.bmp";

            if (op.ShowDialog() == true)
            {
                pictPhoto.Source = new BitmapImage(new Uri(op.FileName));
                strPhotoFile = op.FileName;
                boolControlChanged = true;
            }
        }

        private void BtnClearImage_Click(object sender, RoutedEventArgs e)
        {
            pictPhoto.Source = null;
            strPhotoFile = "";
            boolControlChanged = true;
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a document";
            if (op.ShowDialog() == true)
            {
                boolControlChanged = true;
                docpath = op.FileName;
                FileInfo fi = new FileInfo(docpath);
                txtDoc.Text = fi.Name;
            }
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            docpath = "";
            txtDoc.Text = "";
            boolControlChanged = true;
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidAll())
            {
                if (SaveData())
                {
                    if (pdocpath != docpath)
                    {
                        if (docpath != "")
                        {
                            savedoc(docpath);
                        }
                        else
                        {
                            deldoc(pdocpath);
                        }
                    }

                    if (pstrPhotoFile != strPhotoFile)
                    {
                        if (strPhotoFile != "")
                        {
                            savescan(strPhotoFile);
                        }
                        else
                        {
                            delscan(pstrPhotoFile);
                        }
                    }

                    boolControlChanged = false;
                    if (strRefType == "Customer")
                    {
                        frmBrowsec.FetchNote("", strRefType, intRefID);
                    }

                    if (strRefType == "Employee")
                    {
                        frmBrowsee.FetchNote("", strRefType, intRefID);
                    }
                    CloseKeyboards();
                    Close();
                }
            }
        }

        private void TxtNote_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            boolControlChanged = true;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            boolControlChanged = false;
            CloseKeyboards();
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (boolControlChanged)
            {
                MessageBoxResult DlgResult = new MessageBoxResult();
                DlgResult = new MessageBoxWindow().Show(Properties.Resources.strSaveChanges, Properties.Resources.Confirm, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                if (DlgResult == MessageBoxResult.Yes)
                {
                    if (IsValidAll())
                    {
                        if (SaveData())
                        {
                            if (pdocpath != docpath)
                            {
                                if (docpath != "")
                                {
                                    savedoc(docpath);
                                }
                                else
                                {
                                    deldoc(pdocpath);
                                }
                            }

                            if (pstrPhotoFile != strPhotoFile)
                            {
                                if (strPhotoFile != "")
                                {
                                    savescan(strPhotoFile);
                                }
                                else
                                {
                                    delscan(pstrPhotoFile);
                                }
                            }

                            boolControlChanged = false;
                            if (strRefType == "Customer")
                            {
                                frmBrowsec.NoteYear = dtNote.DateTime.Year;
                                frmBrowsec.NoteMonth = dtNote.DateTime.Month;
                                frmBrowsec.FetchNote("", strRefType, intRefID);
                            }
                            if (strRefType == "Employee")
                            {
                                frmBrowsee.NoteYear = dtNote.DateTime.Year;
                                frmBrowsee.NoteMonth = dtNote.DateTime.Month;
                                frmBrowsee.FetchNote("", strRefType, intRefID);
                            }
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

        private void DtNote_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DevExpress.Xpf.Editors.DateEdit editor = sender as DevExpress.Xpf.Editors.DateEdit;
            if (editor != null)
            {
                editor.IsPopupOpen = true;
                editor.ShowPopup();
                e.Handled = true;
            }
        }

        private void CloseKeyboards()
        {
            if (fkybrd != null)
            {
                fkybrd.Close();
            }
        }

        private void Full_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Settings.UseTouchKeyboardInAdmin == "N") return;
            CloseKeyboards();

            if (!IsAboutFullKybrdOpen)
            {
                fkybrd = new FullKeyboard();

                var location = (sender as DevExpress.Xpf.Editors.TextEdit).PointToScreen(new Point(0, 0));
                fkybrd.Left = GeneralFunctions.fnInt32((SystemParameters.WorkArea.Width - 800) / 2);
                if (location.Y + 35 + 320 > System.Windows.SystemParameters.WorkArea.Height)
                {
                    fkybrd.Top = location.Y - 35 - 320;
                }
                else
                {
                    fkybrd.Top = location.Y + 35;
                }

                fkybrd.Height = 320;
                fkybrd.Width = 800;
                fkybrd.IsWindow = true;
                fkybrd.calledform = this;
                fkybrd.Closing += new System.ComponentModel.CancelEventHandler(FKybrd_Closing);
                fkybrd.Show();
                IsAboutFullKybrdOpen = true;
            }

        }

        private void FKybrd_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsAboutFullKybrdOpen)
            {
                e.Cancel = true;
            }
            else
            {
                IsAboutFullKybrdOpen = false;
                e.Cancel = false;
            }
        }



        private void Full_LostFocus(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
        }

    }
}
