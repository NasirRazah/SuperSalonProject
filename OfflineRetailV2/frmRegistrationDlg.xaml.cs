using OfflineRetailV2.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
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

namespace OfflineRetailV2
{
    /// <summary>
    /// Interaction logic for frmRegistrationDlg.xaml
    /// </summary>
    public partial class frmRegistrationDlg : Window
    {
        #region definining private variables

        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;

        private int intID;
        private int intNoUsers;
        private DateTime dtFirstLoggedOn;
        private string strSerialNo;
        private string strCompanyName;
        private string strAddress1;
        private string strAddress2;
        private string strCity;
        private string strZip;
        private string strState;
        private string strEMail;
        private string strPOSAccess;
        private string strScaleAccess;
        private string strOrderingAccess;
        private string strActivationKey;
        private DateTime dtLastChangedOn;
        private bool blRegistered = false;
        private int intFirstChar = 0;
        private bool checkflag = false;
        private string strLabelDgn;
        private bool blFirstTimeCall;

        private static char[] charSeparators = new char[] { };
        private static String[] result;

        #endregion

        public bool FirstTimeCall
        {
            get { return blFirstTimeCall; }
            set { blFirstTimeCall = value; }
        }

        public bool Registered
        {
            get { return blRegistered; }
            set { blRegistered = value; }
        }
        public frmRegistrationDlg()
        {
            InitializeComponent();

            printDlg_reg = new System.Windows.Forms.PrintDialog();
            printDoc_reg = new System.Drawing.Printing.PrintDocument();
            KeyDown += frmRegistrationDlg_KeyDown;
            Loaded += frmRegistrationDlg_Load;
            printDoc_reg.BeginPrint += printDoc_reg_BeginPrint;
            printDoc_reg.EndPrint += printDoc_reg_EndPrint;
            printDoc_reg.PrintPage += printDoc_reg_PrintPage;
            ModalWindow.CloseCommand = new CommandBase(OnCloseCommand);
        }

        private void OnCloseCommand(object obj)
        {
            CloseKeyboards();
            Close();
        }

        private System.Windows.Forms.PrintDialog printDlg_reg;
        private System.Drawing.Printing.PrintDocument printDoc_reg;
        #region Save data
        private bool SaveData()
        {
            string strError = "";
            // boolControlChanged = false;
            PosDataObject.Registration objRegistration = new PosDataObject.Registration();
            objRegistration.Connection = SystemVariables.Conn;

            objRegistration.ID = intID;
            objRegistration.NoUsers = GeneralFunctions.fnInt32(txtUsers.Text.Trim());
            objRegistration.SerialNo = lbl_Key.Text;
            objRegistration.CompanyName = txtCompany.Text.Trim();
            objRegistration.Address1 = txtAdd1.Text.Trim();
            objRegistration.Address2 = txtAdd2.Text.Trim();
            objRegistration.City = txtCity.Text.Trim();
            objRegistration.State = txtState.Text.Trim();
            objRegistration.Zip = txtZip.Text.Trim();
            objRegistration.EMail = txtEMail.Text.Trim();
            objRegistration.ActivationKey = txtActivationKey.Text.Trim();
            if (intID == 0) { objRegistration.FirstLoggedOn = DateTime.Now; }
            else { objRegistration.FirstLoggedOn = dtFirstLoggedOn; }
            objRegistration.LastChangedOn = DateTime.Now;

            objRegistration.POSAccess = chkPOS.IsChecked == true ? "Y" : "N";
            objRegistration.ScaleAccess = chkScale.IsChecked == true ? "Y" : "N";
            objRegistration.OrderingAccess = chkOrdering.IsChecked == true ? "Y" : "N";
            objRegistration.LabelDesigner = chkLabelDesigner.IsChecked == true ? "Y" : "N";
            if (intID == 0)
            {
                strError = objRegistration.InsertData();
            }
            else
            {
                strError = objRegistration.UpdateData();
            }

            if (strError != "")
            {
                DocMessage.ShowException("Saving Registration", strError);
                return false;
            }
            else
                return true;
        }
        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                string strKey = GeneralFunctions.CalculateKey();
                GeneralFunctions.ActivationKey = txtActivationKey.Text.Trim();
                strSerialNo = GeneralFunctions.CalcSerialNo(strKey, txtCompany.Text.Trim(), txtAdd1.Text.Trim(), txtAdd2.Text.Trim(), txtCity.Text.Trim(), txtZip.Text.Trim(), txtState.Text.Trim(), txtEMail.Text.Trim(), GeneralFunctions.fnInt32(txtUsers.Text), RegisteredModule(), UseLabelDesigner());
                lbl_Key.Text = strKey.Substring(0, 4) + "-" +
                    strKey.Substring(4, 4) + "-" +
                    strKey.Substring(8, 4) + "-" +
                    strKey.Substring(12, 4);

                if (txtActivationKey.Text.Trim() != strSerialNo)
                {
                    DocMessage.MsgInvalid(Properties.Resources.Activation_Key);
                    DialogResult = null;
                }
                else
                {
                    SaveData();
                    DocMessage.MsgInformation(Properties.Resources.Registered_Successfully);
                    Settings.LoadRegistrationVariables();
                    Settings.LoadMainHeader();
                    blRegistered = true;
                    DialogResult = true;
                }
            }
        }

        private string RegisteredModule()
        {
            if ((chkPOS.IsChecked==true) && (chkScale.IsChecked==false) && (chkOrdering.IsChecked == false))
            {
                return "";
            }
            else if ((chkPOS.IsChecked == false) && (chkScale.IsChecked == true) && (chkOrdering.IsChecked == false))
            {
                return "10";
            }
            else if ((chkPOS.IsChecked == true) && (chkScale.IsChecked == true) && (chkOrdering.IsChecked == false))
            {
                return "1000";
            }
            else if ((chkPOS.IsChecked == false) && (chkScale.IsChecked == false) && (chkOrdering.IsChecked == true))
            {
                return "100000";
            }
            else if ((chkPOS.IsChecked == true) && (chkScale.IsChecked == false) && (chkOrdering.IsChecked == true))
            {
                return "10000000";
            }
            else if ((chkPOS.IsChecked == false) && (chkScale.IsChecked == true) && (chkOrdering.IsChecked == true))
            {
                return "1000000000";
            }
            else
            {
                return "100000000000";
            }
        }

        private string UseLabelDesigner()
        {
            if (chkLabelDesigner.IsChecked == true) return "";
            else return "100";
        }

        private bool IsValidData()
        {

            if (txtCompany.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Company_Name);
                GeneralFunctions.SetFocus(txtCompany);
                return false;
            }

            if (txtCompany.Text.Trim().Length < 5)
            {
                DocMessage.MsgInformation(Properties.Resources.Company_Name_must_be_atleast_5_characters);
                GeneralFunctions.SetFocus(txtCompany);
                return false;
            }

            if (txtAdd1.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Address_1);
                GeneralFunctions.SetFocus(txtAdd1);
                return false;
            }

            if (txtAdd1.Text.Trim().Length < 5)
            {
                DocMessage.MsgInformation(Properties.Resources.Address_1_must_be_atleast_5_characters );
                GeneralFunctions.SetFocus(txtAdd1);
                return false;
            }

            if (txtCity.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.City );
                GeneralFunctions.SetFocus(txtCity);
                return false;
            }

            if (txtZip.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Zip);
                GeneralFunctions.SetFocus(txtZip);
                return false;
            }

            if (txtState.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.State );
                GeneralFunctions.SetFocus(txtState);
                return false;
            }

            if (txtEMail.Text.Trim() == "")
            {
                DocMessage.MsgEnter(Properties.Resources.Email );
                GeneralFunctions.SetFocus(txtEMail);
                return false;
            }

            if (txtEMail.Text.Trim() != "")
            {
                if (!GeneralFunctions.isEmail(txtEMail.Text.Trim()))
                {
                    DocMessage.MsgInvalid(Properties.Resources.Email);
                    GeneralFunctions.SetFocus(txtEMail);
                    return false;
                }
            }

            if ((chkUsers.IsChecked==false) && (GeneralFunctions.fnInt32(txtUsers.Text) < 1))
            {
                DocMessage.MsgEnter(Properties.Resources.Number_of_Users );
                GeneralFunctions.SetFocus(txtUsers);
                return false;
            }

            if ((chkPOS.IsChecked == false) && (chkScale.IsChecked == false) && (chkOrdering.IsChecked == false))
            {
                DocMessage.MsgInformation(Properties.Resources.Please_select_atleast_1_module_for_Registration );
                GeneralFunctions.SetFocus(chkPOS);
                return false;
            }

            return true;
        }
        
        private string CalcKey()
        {
            int intChar;
            string strKey, strChar, strDateCreated;
            strKey = "";
            strDateCreated = "";

            dtFirstLoggedOn = new DateTime(2005, 1, 1);
            strDateCreated = dtFirstLoggedOn.ToFileTime().ToString();


            int intCharCount = strDateCreated.Length;
            if (intCharCount > 0)
            {
                for (int iIndex = 1; iIndex < intCharCount; iIndex++)
                {
                    strChar = strDateCreated.Substring(iIndex, 1);
                    intChar = GeneralFunctions.fnInt32(strChar) * iIndex;
                    strKey = strKey + intChar.ToString();
                }
            }

            return strKey.Substring(0, 16);
        }

        private void btnSerialNo_Click(object sender, EventArgs e)
        {
            string strKey = GeneralFunctions.CalculateKey();
            txtActivationKey.Text = GeneralFunctions.CalcSerialNo(strKey, txtCompany.Text.Trim(), txtAdd1.Text.Trim(), txtAdd2.Text.Trim(), txtCity.Text.Trim(), txtZip.Text.Trim(), txtState.Text.Trim(), txtEMail.Text.Trim(), GeneralFunctions.fnInt32(txtUsers.Text), RegisteredModule(), UseLabelDesigner());
        }

        private void frmRegistrationDlg_Load(object sender, EventArgs e)
        {
            fkybrd = new FullKeyboard();
            Title.Text = Properties.Resources.Registration;
            /*if (GeneralFunctions.GetRecordCount("REGISTRATION") == 1)
                btnDemo.Visible = false; */

            PosDataObject.Registration objRegistration = new PosDataObject.Registration();
            objRegistration.Connection = SystemVariables.Conn;
            DataTable dbtbl = new DataTable();
            dbtbl = objRegistration.FetchData();
            foreach (DataRow dr in dbtbl.Rows)
            {
                intID = GeneralFunctions.fnInt32(dr["ID"].ToString());
                strSerialNo = dr["SERIALNO"].ToString();
                strCompanyName = dr["COMPANYNAME"].ToString();
                strAddress1 = dr["ADDRESS1"].ToString();
                strAddress2 = dr["ADDRESS2"].ToString();
                strCity = dr["CITY"].ToString();
                strZip = dr["ZIP"].ToString();
                strState = dr["STATE"].ToString();
                strEMail = dr["EMAIL"].ToString();

                strActivationKey = dr["ACTIVATIONKEY"].ToString();
                intNoUsers = GeneralFunctions.fnInt32(dr["NOUSERS"].ToString());
                dtFirstLoggedOn = GeneralFunctions.fnDate(dr["FIRSTLOGGEDON"].ToString());
                dtLastChangedOn = GeneralFunctions.fnDate(dr["LASTCHANGEDON"].ToString());

                strPOSAccess = dr["POSACCESS"].ToString();
                strScaleAccess = dr["SCALEACCESS"].ToString();
                strOrderingAccess = dr["ORDERINGACCESS"].ToString();

                strLabelDgn = dr["LABELDESIGNER"].ToString();
            }
            string strKey = GeneralFunctions.CalculateKey();
            lbl_Key.Text = strKey.Substring(0, 4) + "-" +
                strKey.Substring(4, 4) + "-" +
                strKey.Substring(8, 4) + "-" +
                strKey.Substring(12, 4);

            txtCompany.Text = strCompanyName;
            txtAdd1.Text = strAddress1;
            txtAdd2.Text = strAddress2;
            txtCity.Text = strCity;
            txtZip.Text = strZip;
            txtState.Text = strState;
            txtEMail.Text = strEMail;
            txtActivationKey.Text = strActivationKey;
            txtUsers.Text = Convert.ToString(intNoUsers);

            chkPOS.IsChecked = strPOSAccess == "Y";
            chkScale.IsChecked = strScaleAccess == "Y";
            chkOrdering.IsChecked = strOrderingAccess == "Y";
            chkLabelDesigner.IsChecked = strLabelDgn == "Y";
            dbtbl.Dispose();

            if (intNoUsers == 0)
            {
                chkUsers.IsChecked = true;
                txtUsers.IsEnabled = false;
            }
            else
            {
                chkUsers.IsChecked = false;
                txtUsers.IsEnabled = true;
            }
            checkflag = true;
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            CloseKeyboards();
            Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                if (txtEMail.Text.Trim() != "")
                {
                    if (!GeneralFunctions.isEmail(txtEMail.Text.Trim()))
                    {
                        DocMessage.MsgInvalid(Properties.Resources.Email);
                        GeneralFunctions.SetFocus(txtEMail);
                        return;
                    }
                }
                string strKey = GeneralFunctions.CalculateKey();
                strSerialNo = GeneralFunctions.CalcSerialNo(strKey, txtCompany.Text.Trim(), txtAdd1.Text.Trim(), txtAdd2.Text.Trim(), txtCity.Text.Trim(), txtZip.Text.Trim(), txtState.Text.Trim(), txtEMail.Text.Trim(), GeneralFunctions.fnInt32(txtUsers.Text), RegisteredModule(), UseLabelDesigner());
                lbl_Key.Text = strKey.Substring(0, 4) + "-" +
                    strKey.Substring(4, 4) + "-" +
                    strKey.Substring(8, 4) + "-" +
                    strKey.Substring(12, 4);

                printDlg_reg.Document = printDoc_reg;
                string strLD = "";
                string UsersNo = "";
                string mod = "";
                if (printDlg_reg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    UsersNo = txtUsers.Text.Trim();

                    if (UsersNo == "0") { UsersNo = "Unlimited"; }

                    if (chkPOS.IsChecked==true)
                    {
                        mod = "POS";
                    }
                    if (chkScale.IsChecked == true)
                    {
                        mod = mod == "" ? "SCALE" : mod + ", SCALE";
                    }
                    if (chkOrdering.IsChecked == true)
                    {
                        mod = mod == "" ? "ORDERING" : mod + ", ORDERING";
                    }

                    strLD = chkLabelDesigner.IsChecked == true ? Properties.Resources.Yes : Properties.Resources.No;

                    red_Print.Text = "\n" +Properties.Resources.POS_Software___Registration_Details_  + "\n" + "\n" + "\n" +
                    Properties.Resources.Name_of_the_Company__  + txtCompany.Text.Trim() + "\n" +
                    Properties.Resources.Address_1__  + txtAdd1.Text.Trim( ) + "\n" +
                    Properties.Resources.Address2__  + txtAdd2.Text.Trim( ) + "\n" +
                    Properties.Resources.City__  + "\n" +
                    Properties.Resources.Zip__  + txtZip.Text.Trim()  + "\n" +
                    Properties.Resources.State___  + txtState.Text.Trim () + "\n" +
                    Properties.Resources.Email__ + txtEMail.Text.Trim() + "\n" +
                    Properties.Resources.Number_of_Users__  + UsersNo + "\n";

                    printDoc_reg.Print();
                }
            }
        }
        private DocManager.Controls.DocRichTextBox red_Print;
        private void printDoc_reg_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            intFirstChar = 0;
        }

        private void printDoc_reg_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            red_Print.FormatRangeDone();
        }

        private void printDoc_reg_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            intFirstChar = red_Print.FormatRange(false, e, intFirstChar, red_Print.TextLength);
            if (intFirstChar < red_Print.TextLength)
                e.HasMorePages = true;
            else
                e.HasMorePages = false;
        }

        private void btnMail_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                if (txtEMail.Text.Trim() == "")
                {
                    DocMessage.MsgEnter(Properties.Resources.Email );
                    GeneralFunctions.SetFocus(txtEMail);
                    return;
                }
                if (txtEMail.Text.Trim() != "")
                {
                    if (!GeneralFunctions.isEmail(txtEMail.Text.Trim()))
                    {
                        DocMessage.MsgInvalid(Properties.Resources.Email);
                        GeneralFunctions.SetFocus(txtEMail);
                        return;
                    }
                }
                string strLD = "";
                string mod = "";
                string UsersNo = "";
                UsersNo = txtUsers.Text.Trim();
                if (UsersNo == "0") { UsersNo = "Unlimited"; }
                if (chkPOS.IsChecked == true)
                {
                    mod = "POS";
                }
                if (chkScale.IsChecked == true)
                {
                    mod = mod == "" ? "SCALE" : mod + ", SCALE";
                }
                if (chkOrdering.IsChecked == true)
                {
                    mod = mod == "" ? "ORDERING" : mod + ", ORDERING";
                }

                strLD = chkLabelDesigner.IsChecked == true ? Properties.Resources.Yes  : Properties.Resources.No;

                string strMailSubject =Properties.Resources.Request_for_POS_Activation_Key;
                string strMailBody = Properties.Resources.Name_of_the_Company__  + txtCompany.Text.Trim() + "    " +
                                           Properties.Resources.Address_1__ + txtAdd1.Text.Trim() + "    " +
                                          Properties.Resources.Address2__   + txtAdd2.Text.Trim() + "    " +
                                           Properties.Resources.City__  + txtCity.Text.Trim() + "    " +
                                           Properties.Resources.Zip__ + txtZip.Text.Trim() + "    " +
                                           Properties.Resources.State___  + txtState.Text.Trim() + "    " +
                                          Properties.Resources.Email__   + txtEMail.Text.Trim() + "    " +
                                           Properties.Resources.Number_of_Users__  + UsersNo + "    ";
                Process p = new Process();
                p.StartInfo.FileName = "mailto:?subject=" + strMailSubject + "&body=" + strMailBody;
                p.Start();
            }

        }

        
        private void frmRegistrationDlg_KeyDown(object sender, KeyEventArgs e)
        {
            
        }


        private string GetLogPath()
        {
            string csConnPath = "";
            string strfilename = "";
            string strdirpath = "";
            string LogFile = "reg_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() +
                    DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() +
                    DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() +
                    DateTime.Now.Millisecond.ToString() + ".log";

            //csConnPath = Application.StartupPath;

            csConnPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            if (csConnPath.EndsWith("\\"))
            {
                strdirpath = csConnPath + SystemVariables.BrandName + "\\Logs";
            }
            else
            {
                strdirpath = csConnPath + "\\" + SystemVariables.BrandName + "\\Logs";
            }
            if (Directory.Exists(strdirpath))
            {
                strfilename = strdirpath + "\\" + LogFile;
            }
            else
            {
                Directory.CreateDirectory(strdirpath);
                strfilename = strdirpath + "\\" + LogFile;
            }
            return strfilename;
        }

        private void WriteToLogFile(string FileLine, int CSVLine)
        {
            string LogFullPath = GetLogPath();
            FileStream fileStrm;
            if (File.Exists(LogFullPath))
            {
                fileStrm = new FileStream(LogFullPath, FileMode.Append, FileAccess.Write);
            }
            else
            {
                fileStrm = new FileStream(LogFullPath, FileMode.OpenOrCreate, FileAccess.Write);
            }
            StreamWriter sw = new StreamWriter(fileStrm);
            sw.WriteLine("Line no. " + (CSVLine + 1).ToString() + " - " + FileLine);
            sw.Close();
            fileStrm.Close();
        }

        private void btnSetupHelp_Click(object sender, EventArgs e)
        {
            if (btnSetupHelp.Tag.ToString() == "")
            {
                new MessageBoxWindow().Show(Properties.Resources.This_help_topic_is_currently_not_available_ , "Help Validation", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                string ret = GeneralFunctions.IsHelpFileExists(btnSetupHelp.Tag.ToString());
                if (ret == "")
                {
                    new MessageBoxWindow().Show(Properties.Resources.This_help_topic_is_currently_not_available_, "Help Validation", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
                {
                    System.Diagnostics.Process p = new System.Diagnostics.Process();
                    p.StartInfo.FileName = ret;
                    p.Start();
                }
            }
        }

        private void chkUsers_Checked(object sender, RoutedEventArgs e)
        {
            txtUsers.Text = "0";
            txtUsers.IsEnabled = false;
        }

        private void chkUsers_Unchecked(object sender, RoutedEventArgs e)
        {
            txtUsers.IsEnabled = true;
            if (checkflag) txtUsers.Text = "5";
        }

        

        private void btnDemo_Click(object sender, RoutedEventArgs e)
        {
            if (DocMessage.MsgConfirmation(Properties.Resources.Are_you_sure_you_want_to_Register_as_Demo__) == MessageBoxResult.No) return;
            txtCompany.Text = "demo company";
            txtAdd1.Text = "address 1";
            txtAdd2.Text = "address 2";
            txtCity.Text = "city";
            txtZip.Text = "11111";
            txtState.Text = "ST";
            txtEMail.Text = "demo@demo.com";
            chkUsers.IsChecked = false;
            txtUsers.Value = 1;
            chkPOS.IsChecked = true;
            chkScale.IsChecked = false;
            chkOrdering.IsChecked = false;
            chkLabelDesigner.IsChecked = false;
            txtActivationKey.Text = "7870-1574-0031-4878";

            if (IsValidData())
            {
                string strKey = GeneralFunctions.CalculateKey();
                strSerialNo = GeneralFunctions.CalcSerialNo(strKey, txtCompany.Text, txtAdd1.Text, txtAdd2.Text, txtCity.Text, txtZip.Text, txtState.Text, txtEMail.Text, GeneralFunctions.fnInt32(txtUsers.Text), RegisteredModule(), UseLabelDesigner());
                lbl_Key.Text = strKey.Substring(0, 4) + "-" +
                    strKey.Substring(4, 4) + "-" +
                    strKey.Substring(8, 4) + "-" +
                    strKey.Substring(12, 4);

                if (txtActivationKey.Text.Trim() != strSerialNo)
                {
                    DocMessage.MsgInvalid(Properties.Resources.Activation_Key);
                    DialogResult = null;
                }
                else
                {
                    SaveData();
                    DocMessage.MsgInformation(Properties.Resources.Demo_Version_Registered_Successfully);
                    Settings.LoadRegistrationVariables();
                    Settings.LoadMainHeader();
                    blRegistered = true;
                    CloseKeyboards();
                    DialogResult = true;
                    
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            CloseKeyboards();
            DialogResult = false;
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

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidData())
            {
                string strKey = GeneralFunctions.CalculateKey();
                GeneralFunctions.ActivationKey = txtActivationKey.Text.Trim();
                strSerialNo = GeneralFunctions.CalcSerialNo(strKey, txtCompany.Text.Trim(), txtAdd1.Text.Trim(), txtAdd2.Text.Trim(), txtCity.Text.Trim(), txtZip.Text.Trim(), txtState.Text.Trim(), txtEMail.Text.Trim(), GeneralFunctions.fnInt32(txtUsers.Text), RegisteredModule(), UseLabelDesigner());
                lbl_Key.Text = strKey.Substring(0, 4) + "-" +
                    strKey.Substring(4, 4) + "-" +
                    strKey.Substring(8, 4) + "-" +
                    strKey.Substring(12, 4);

                if (txtActivationKey.Text.Trim() != strSerialNo)
                {
                    DocMessage.MsgInvalid("Activation Key");
                    DialogResult = null;
                }
                else
                {
                    SaveData();
                    DocMessage.MsgInformation("Registered Successfully");
                    Settings.LoadRegistrationVariables();
                    Settings.LoadMainHeader();
                    blRegistered = true;
                    CloseKeyboards();
                    DialogResult = true;
                }
            }
        }

        private void btnMail_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidData())
            {
                if (txtEMail.Text.Trim() == "")
                {
                    DocMessage.MsgEnter(Properties.Resources.Email);
                    GeneralFunctions.SetFocus(txtEMail);
                    return;
                }
                if (txtEMail.Text.Trim() != "")
                {
                    if (!GeneralFunctions.isEmail(txtEMail.Text.Trim()))
                    {
                        DocMessage.MsgInvalid(Properties.Resources.Email);
                        GeneralFunctions.SetFocus(txtEMail);
                        return;
                    }
                }
                string strLD = "";
                string mod = "";
                string UsersNo = "";
                UsersNo = txtUsers.Text.Trim();
                if (UsersNo == "0") { UsersNo = "Unlimited"; }
                if (chkPOS.IsChecked == true)
                {
                    mod = "POS";
                }
                if (chkScale.IsChecked == true)
                {
                    mod = mod == "" ? "SCALE" : mod + ", SCALE";
                }
                if (chkOrdering.IsChecked == true)
                {
                    mod = mod == "" ? "ORDERING" : mod + ", ORDERING";
                }

                strLD = chkLabelDesigner.IsChecked == true ? Properties.Resources.Yes : Properties.Resources.No;

                string strMailSubject = Properties.Resources.Request_for_POS_Activation_Key;
                string strMailBody = Properties.Resources.Name_of_the_Company__ + txtCompany.Text.Trim() + "    " +
                                           Properties.Resources.Address_1__ + txtAdd1.Text.Trim() + "    " +
                                          Properties.Resources.Address2__ + txtAdd2.Text.Trim() + "    " +
                                           Properties.Resources.City__ + txtCity.Text.Trim() + "    " +
                                           Properties.Resources.Zip__ + txtZip.Text.Trim() + "    " +
                                           Properties.Resources.State___ + txtState.Text.Trim() + "    " +
                                          Properties.Resources.Email__ + txtEMail.Text.Trim() + "    " +
                                           Properties.Resources.Number_of_Users__ + UsersNo + "    ";
                Process p = new Process();
                p.StartInfo.FileName = "mailto:?subject=" + strMailSubject + "&body=" + strMailBody;
                p.Start();
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidData())
            {
                if (txtEMail.Text.Trim() != "")
                {
                    if (!GeneralFunctions.isEmail(txtEMail.Text.Trim()))
                    {
                        DocMessage.MsgInvalid(Properties.Resources.Email);
                        GeneralFunctions.SetFocus(txtEMail);
                        return;
                    }
                }
                string strKey = GeneralFunctions.CalculateKey();
                strSerialNo = GeneralFunctions.CalcSerialNo(strKey, txtCompany.Text.Trim(), txtAdd1.Text.Trim(), txtAdd2.Text.Trim(), txtCity.Text.Trim(), txtZip.Text.Trim(), txtState.Text.Trim(), txtEMail.Text.Trim(), GeneralFunctions.fnInt32(txtUsers.Text), RegisteredModule(), UseLabelDesigner());
                lbl_Key.Text = strKey.Substring(0, 4) + "-" +
                    strKey.Substring(4, 4) + "-" +
                    strKey.Substring(8, 4) + "-" +
                    strKey.Substring(12, 4);

                printDlg_reg.Document = printDoc_reg;
                string strLD = "";
                string UsersNo = "";
                string mod = "";
                if (printDlg_reg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    UsersNo = txtUsers.Text.Trim();

                    if (UsersNo == "0") { UsersNo = "Unlimited"; }

                    if (chkPOS.IsChecked == true)
                    {
                        mod = "POS";
                    }
                    if (chkScale.IsChecked == true)
                    {
                        mod = mod == "" ? "SCALE" : mod + ", SCALE";
                    }
                    if (chkOrdering.IsChecked == true)
                    {
                        mod = mod == "" ? "ORDERING" : mod + ", ORDERING";
                    }

                    strLD = chkLabelDesigner.IsChecked == true ? Properties.Resources.Yes : Properties.Resources.No;

                    red_Print.Text = "\n" + Properties.Resources.POS_Software___Registration_Details_ + "\n" + "\n" + "\n" +
                    Properties.Resources.Name_of_the_Company__ + txtCompany.Text.Trim() + "\n" +
                    Properties.Resources.Address_1__ + txtAdd1.Text.Trim() + "\n" +
                    Properties.Resources.Address2__ + txtAdd2.Text.Trim() + "\n" +
                    Properties.Resources.City__ + "\n" +
                    Properties.Resources.Zip__ + txtZip.Text.Trim() + "\n" +
                    Properties.Resources.State___ + txtState.Text.Trim() + "\n" +
                    Properties.Resources.Email__ + txtEMail.Text.Trim() + "\n" +
                    Properties.Resources.Number_of_Users__ + UsersNo + "\n";

                    printDoc_reg.Print();
                }
            }
        }
    }
}
