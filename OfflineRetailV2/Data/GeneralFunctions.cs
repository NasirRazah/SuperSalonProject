using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using Microsoft.Win32;
using System.Windows;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using DevExpress.XtraEditors;
using System.Net;
using System.Reflection;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Xml;
using System.Threading;
using System.Net.Mail;
using DevExpress.XtraPrinting;
using System.Runtime.Serialization.Formatters.Binary;
using alabel;
using DevExpress.XtraReports.UserDesigner;
using OfflineRetailV2.Report.Scale;
using System.Collections;
using OfflineRetailV2.UserControls.POSSection;
using System.Windows.Input;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;

namespace OfflineRetailV2.Data
{
    public class GeneralFunctions
    {
        public GeneralFunctions()
        {

        }

        public static string FormatDouble2(double dbl)
        {
            string val = "";
            val = String.Format("{0:0.00}", dbl);

            if (val.StartsWith("-"))
            {
                string strTemp = val.Remove(0, 1);
                val = "(" + strTemp + ")";
            }

            return val;
        }

        public static string GetOSVersion()
        {
            string returnval = "";
            string osdesc = RuntimeInformation.OSDescription;
            if (osdesc.StartsWith("Microsoft Windows 10")) //  if (osdesc.StartsWith("Microsoft Windows 10.0."))
            {
                int builtNo = 0;
                try
                {
                    builtNo = fnInt32(osdesc.Substring(23));
                    if (builtNo >= 22000)
                    {
                        returnval = "Win 11";
                    }
                    else
                    {
                        returnval = "Win 10";
                    }
                }
                catch
                {
                    returnval = "Win 10";
                }
            }
            else
            {
                returnval = "Win 10";
            }
            return returnval;
        }

        public static void SetDecimal(DevExpress.Xpf.Editors.TextEdit textEdit, int decimalpoint)
        {

        }

        // Variable Declaration

        private static bool blCG = false;
        private static string CGresp = "";
        private static string CGresptxt = "";
        private static string CGmonitor = "";
        private static string CGrequestfile = "";
        private static string CGanswerfile = "";
        private static string CGtrantype = "";
        private static double CGamt = 0;
        private static int CGinv = 0;
        private static string LogFile = "";
        public static string ActivationKey = "";

        // Constant Declaration

        private const string SEPARATOR = "  |  ";
        private const string strRegPath = "Software\\POS\\";


        // Mercury Admin Password Encryption

        public static string EncryptString(string InputText, string Password)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();

            byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(InputText);
            byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());

            //This class uses an extension of the PBKDF1 algorithm defined in the PKCS#5 v2.0 
            //standard to derive bytes suitable for use as key material from a password. 
            //The standard is documented in IETF RRC 2898.

            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);
            //Creates a symmetric encryptor object. 
            ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
            MemoryStream memoryStream = new MemoryStream();
            //Defines a stream that links data streams to cryptographic transformations
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(PlainText, 0, PlainText.Length);
            //Writes the final state and clears the buffer
            cryptoStream.FlushFinalBlock();
            byte[] CipherBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            string EncryptedData = Convert.ToBase64String(CipherBytes);
            return EncryptedData;

        }

        // Mercury Admin Password Decryption

        public static string DecryptString(string InputText, string Password)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();
            byte[] EncryptedData = Convert.FromBase64String(InputText);
            byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());
            //Making of the key for decryption
            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);
            //Creates a symmetric Rijndael decryptor object.
            ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
            MemoryStream memoryStream = new MemoryStream(EncryptedData);
            //Defines the cryptographics stream for decryption.THe stream contains decrpted data
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);
            byte[] PlainText = new byte[EncryptedData.Length];
            int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);
            memoryStream.Close();
            cryptoStream.Close();
            //Converting to string
            string DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);
            return DecryptedData;

        }

        #region Get / Set Regisrty Values

        public static string GetDatabaseName()
        {
            RegistryKey RegDataKey = Registry.CurrentUser;
            RegistryKey RegDatabaseKey = RegDataKey.OpenSubKey(strRegPath + "\\DatabaseName");
            if (!(RegDatabaseKey == null))
            {
                return RegDatabaseKey.GetValue("DatabaseName").ToString();
            }
            else
            {
                return "";
            }
        }

        public static string GetServerName()
        {
            RegistryKey RegDataKey = Registry.CurrentUser;
            RegistryKey RegServerKey = RegDataKey.OpenSubKey(strRegPath + "\\Data\\ServerName");
            if (!(RegServerKey == null))
            {
                return RegServerKey.GetValue("ServerName").ToString();
            }
            else
            {
                return "";
            }
        }

        public static void SetDatabaseName(string strDatabaseName)
        {
            RegistryKey RegDataKey = Registry.CurrentUser;
            RegistryKey RegDatabaseKey = RegDataKey.CreateSubKey(strRegPath + "\\Data\\DatabaseName");
            RegDatabaseKey.SetValue("DatabaseName", strDatabaseName);
        }

        public static void SetServerName(string strServerName)
        {
            RegistryKey RegDataKey = Registry.CurrentUser;
            RegistryKey RegServerKey = RegDataKey.CreateSubKey(strRegPath + "\\Data\\ServerName");
            RegServerKey.SetValue("ServerName", strServerName);
        }

        public static string GetModuleName()
        {
            RegistryKey RegDataKey = Registry.CurrentUser;
            RegistryKey RegDatabaseKey = RegDataKey.OpenSubKey(strRegPath + "\\ModuleName");
            if (!(RegDatabaseKey == null))
            {
                return RegDatabaseKey.GetValue("ModuleName").ToString();
            }
            else
            {
                return "";
            }
        }

        public static void SetModuleName(string strModuleName)
        {
            RegistryKey RegDataKey = Registry.CurrentUser;
            RegistryKey RegDatabaseKey = RegDataKey.CreateSubKey(strRegPath + "\\ModuleName");
            RegDatabaseKey.SetValue("ModuleName", strModuleName);
        }

        #endregion

        // Get no. of Decimal Place in number
        public static int GetDecimalLength(string mVal)
        {
            if (!mVal.Contains("."))
            {
                return 0;
            }
            else
            {
                string strDecimal = mVal.Substring(mVal.IndexOf(".") + 1);
                if ((strDecimal == "0") || (strDecimal == "00") || (strDecimal == "000"))
                {
                    return 0;
                }
                else
                {
                    return strDecimal.Length;
                }
            }

        }

        public static int GetDecimalLength1(string mVal)
        {
            if (!mVal.Contains("."))
            {
                return 0;
            }
            else
            {
                string strDecimal = mVal.Substring(mVal.IndexOf(".") + 1);
                if ((strDecimal == "0") || (strDecimal == "00") || (strDecimal == "000"))
                {
                    return 0;
                }
                else
                {
                    return strDecimal.TrimEnd('0').Length;
                }
            }

        }

        // Display Item Quantity depending on the Item Type
        public static string GetDisplayQty(string pQty, string pDecimal, string pProdType)
        {
            if (pDecimal == "")
            {
                return pQty;
            }
            else
            {
                if ((pProdType == "Z") || (pProdType == "C") || (pProdType == "H"))
                {
                    return pQty;
                }
                else
                {
                    decimal dQty = fnDecimal(pQty);
                    int IPart = (int)Decimal.Truncate(dQty);
                    Decimal decimal_part = dQty - Decimal.Truncate(IPart);
                    if (decimal_part == 0)
                    {
                        return IPart.ToString();
                    }
                    else
                    {
                        if (pDecimal == "0")
                        {
                            return pQty;
                        }
                        else
                        {
                            string TempDecimal = "";
                            string strDecimal = decimal_part.ToString();
                            TempDecimal = strDecimal.Substring(2);
                            string ReturnS = pQty;
                            if (pDecimal == "1") ReturnS = String.Format("{0:0.0}", fnDecimal(IPart + "." + TempDecimal));
                            if (pDecimal == "2") ReturnS = String.Format("{0:0.00}", fnDecimal(IPart + "." + TempDecimal));
                            if (pDecimal == "3") ReturnS = String.Format("{0:0.000}", fnDecimal(IPart + "." + TempDecimal));
                            return ReturnS;
                        }
                    }
                }
            }
        }


        public static string GetDisplayQty1(string pQty, string pDecimal)
        {
            if (pDecimal == "")
            {
                return pQty;
            }
            else
            {
                decimal dQty = fnDecimal(pQty);
                int IPart = (int)Decimal.Truncate(dQty);
                Decimal decimal_part = dQty - Decimal.Truncate(IPart);
                if (decimal_part == 0)
                {
                    return IPart.ToString();
                }
                else
                {
                    if (pDecimal == "0")
                    {
                        return pQty;
                    }
                    else
                    {
                        string TempDecimal = "";
                        string strDecimal = decimal_part.ToString();
                        TempDecimal = strDecimal.Substring(2);
                        string ReturnS = pQty;
                        if (pDecimal == "1") ReturnS = String.Format("{0:0.0}", fnDecimal(IPart + "." + TempDecimal));
                        if (pDecimal == "2") ReturnS = String.Format("{0:0.00}", fnDecimal(IPart + "." + TempDecimal));
                        if (pDecimal == "3") ReturnS = String.Format("{0:0.000}", fnDecimal(IPart + "." + TempDecimal));
                        return ReturnS;
                    }
                }
            }
        }

        // Mask Double ( for Tare in Scale Module)
        public static double DoubleFromMaskedValue(string maskval)
        {
            if (maskval == "") return 0;
            else
            {
                if (maskval.Length == 1) maskval = "00" + maskval;
                if (maskval.Length == 2) maskval = "0" + maskval;
                double val1 = GeneralFunctions.fnDouble(maskval.Insert(maskval.Length - 3, "."));
                return val1;
            }
        }


        // Mask Double to String ( for Tare in Scale Module)
        public static string StringMaskFromDouble(decimal mVal)
        {
            int IPart = (int)Decimal.Truncate(mVal);
            Decimal decimal_part = mVal - Decimal.Truncate(IPart);

            if (decimal_part == 0)
            {
                return (IPart * 1000).ToString();
            }
            else
            {
                string strDecimal = decimal_part.ToString().Substring(decimal_part.ToString().IndexOf(".") + 1);
                int tlen = strDecimal.Length;
                if (tlen == 2) strDecimal = strDecimal + "0";
                if (tlen == 3) strDecimal = strDecimal + "00";
                return ((IPart * 1000) + fnInt32(strDecimal)).ToString();
            }

        }

        // Common Save File Dialog

        public static string ShowSaveFileDialog(string title, string filter)
        {
            System.Windows.Forms.SaveFileDialog dlg = new System.Windows.Forms.SaveFileDialog();
            string name = System.Windows.Application.ResourceAssembly.GetName().Name;
            int n = name.LastIndexOf(".") + 1;
            if (n > 0) name = name.Substring(n, name.Length - n);
            dlg.Title = Properties.Resources.ExportTo + " " + title;
            dlg.FileName = name;
            dlg.Filter = filter;
            if (dlg.ShowDialog() == DialogResult.OK) return dlg.FileName;
            return "";
        }

        // Open File

        public static void OpenFile(string fileName)
        {
            if (ResMan.MessageBox.Show(Properties.Resources.Doyouwanttoopenthisfile, Properties.Resources.ExportTo, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    process.StartInfo.FileName = fileName;
                    process.StartInfo.Verb = "Open";
                    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                    process.Start();
                }
                catch
                {
                    ResMan.MessageBox.Show(Properties.Resources.Cannotfindanapplication, System.Windows.Application.ResourceAssembly.GetName().Name, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        #region Date Check Functions  

        public static bool IsBlankDate(DateTime dt)
        {
            if ((dt.Year == 1) && (dt.Month == 1) && (dt.Day == 1) && (dt.Hour == 0) &&
                (dt.Minute == 0) && (dt.Second == 0))
            {
                return true;
            }
            return false;
        }

        public static bool IsValidDate(DateTime dtGeneral)
        {
            try
            {
                DateTime dt = dtGeneral;

                if (IsBlankDate(dtGeneral)) return true;

                if ((dt.Date.Year < 1753) || (dt.Date.Year > 9999))
                { return false; }
                if ((dt.Date.Month < 1) || (dt.Date.Month > 12))
                { return false; }
                if ((dt.Date.Day < 1) || (dt.Date.Day > 31))
                { return false; }
            }
            catch
            {
                return false;
            }

            return true;
        }

        #endregion

        #region Focus to Control

        public static void SetFocus(System.Windows.Controls.Control objControl)
        {
            if ((!objControl.IsFocused) && (objControl.IsEnabled))
                objControl.Focus();
        }

        #endregion

        #region Get cell value from grid

        public static string GetCellValue(int intRowIndex, DevExpress.XtraGrid.Views.Grid.GridView GrdVw, DevExpress.XtraGrid.Columns.GridColumn Column)
        {
            //if (GrdVw.FocusedRowHandle < 0) return "";
            if (GrdVw.GetRowCellValue(intRowIndex, Column) == null) return "";
            return GrdVw.GetRowCellValue(intRowIndex, Column).ToString();

        }

        public static string GetCellValue(int intRowIndex, DevExpress.Xpf.Grid.GridControl GrdVw, DevExpress.Xpf.Grid.GridColumn Column)
        {
            //if (GrdVw.FocusedRowHandle < 0) return "";
            if (GrdVw.GetCellValue(intRowIndex, Column) == null) return "";
            return GrdVw.GetCellValue(intRowIndex, Column).ToString();
        }

        public static async Task<string> GetCellValue1(int intRowIndex, DevExpress.Xpf.Grid.GridControl GrdVw,
            DevExpress.Xpf.Grid.GridColumn Column)
        {
            await Task.Delay(1);
            if ((GrdVw.GetCellValue(intRowIndex, Column)) == null) return "";
            return (GrdVw.GetCellValue(intRowIndex, Column)).ToString();

        }

        #endregion

        #region Scan Image

        public static void DeleteTempFiles(string strTempDirectory)
        {
            string[] strFileNames = Directory.GetFiles(strTempDirectory);
            int intFileCount = strFileNames.Length;

            if (intFileCount > 0)
            {
                for (int iCount = 0; iCount < intFileCount; iCount++)
                {
                    FileInfo tempFile = new FileInfo(strFileNames[iCount]);
                    try
                    {
                        tempFile.Delete();
                    }
                    catch { }
                }
            }
        }

        #endregion

        #region POS Main Button Interface

        public static void LoadPhotofromDB(POSControls.POSItem PosItem)
        {
            if (Settings.POSDisplayProductImage == "N")
            {
                PosItem.Image = null;
                return;
            }
            string strSQLComm = "";
            strSQLComm = "select ProductPhoto from Product where ID = @CODE";

            SqlConnection objSQLCon = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, objSQLCon);
            objSQlComm.Parameters.Add(new SqlParameter("@CODE", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@CODE"].Value = PosItem.ItemID;

            try
            {
                if (objSQLCon.State == System.Data.ConnectionState.Closed) { objSQLCon.Open(); }
                byte[] content = (byte[])objSQlComm.ExecuteScalar();
                try
                {
                    // assign byte array data into memory stream 
                    MemoryStream stream = new MemoryStream(content);

                    // set transparent bitmap with 32 X 32 size by memory stream data 
                    Bitmap b = new Bitmap(stream);
                    Bitmap output = new Bitmap(b, new System.Drawing.Size(32, 32));
                    output.MakeTransparent();

                    //Old Code
                    //PosItem.Image = (Image)output;

                    // New Code
                    // assign bitmap to memory stream again
                    Image tempImage = (Image)output;
                    MemoryStream ms = new MemoryStream();
                    tempImage.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

                    // finally load image from memory stream 
                    PosItem.Image = Image.FromStream(ms);

                    // dispose all
                    tempImage.Dispose();
                    ms.Close();
                    ms.Dispose();
                    stream.Close();
                    output.Dispose();
                    b.Dispose();
                    stream.Dispose();

                }
                catch
                {
                }
                finally
                {
                }
                objSQlComm.Dispose();
                objSQLCon.Close();
                objSQLCon.Dispose();
            }
            catch
            {
                objSQlComm.Dispose();
                objSQLCon.Close();
                objSQLCon.Dispose();
            }
        }

        #endregion

        public static string FormatDoubleForPrint(string pvalue)
        {
            int length = 0;
            try
            {
                int dotIndex = pvalue.IndexOf(".");
                if (dotIndex >= 0) // ✅ Ensure dot exists
                {
                    length = pvalue.Substring(dotIndex).Length;
                }
            }
            catch
            {
                length = 0;
            }

            if ((length == 0) || (length == 1) || (length == 2))
            {
                return string.Format("{0:0.00}", fnDouble(pvalue));
            }
            else
            {
                return pvalue;
            }
        }


        //public static string FormatDoubleForPrint(string pvalue)
        //{
        //    int length = 0;
        //    try
        //    {
        //        length = pvalue.Substring(pvalue.IndexOf(".")).Length;
        //    }
        //    catch
        //    {
        //        length = 0;
        //    }

        //    if ((length == 0) || (length == 1) || (length == 2))
        //    {
        //        return String.Format("{0:0.00}", fnDouble(pvalue));
        //    }
        //    else
        //    {
        //        return pvalue;
        //    }
        //}

        // Not Used

        public static void MaskDecimalWithSpecificDigit(DevExpress.XtraGrid.Views.Grid.GridView GView, KeyPressEventArgs e, int dgt)
        {
            string strinput = GView.EditingValue.ToString();
            int Decimals = dgt;
            string zero = dgt == 4 ? "0.0000" : dgt == 3 ? "0.000" : "0.00";
            int inCaretPos = 0;
            int inDecimalPoint = strinput.IndexOf('.', 0, strinput.Length);
            if ((inDecimalPoint >= 0) && ((inDecimalPoint + Decimals) < strinput.Length)
                && (strinput != zero) && (e.KeyChar != 8) && (inCaretPos >
                (inDecimalPoint - 1)))
            {
                e.Handled = true;
                return;
            }
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == 8 ||
                e.KeyChar == '-')
            {
                //Allow "." only once for decimal separator.
                if (e.KeyChar == '.')
                {
                    if (strinput.IndexOf('.', 0, strinput.Length) >= 0)
                    {
                        e.Handled = true;
                        return;
                    }
                }
                // Allow -ve only at the first position.
                if (e.KeyChar == '-')
                {
                    if (strinput.IndexOf('-', 0, strinput.Length) == 0)
                    {
                        e.Handled = true;
                        return;
                    }
                    else
                    {
                        strinput = "";
                    }
                    //e.Handled = true;
                    //return;

                }

                //allow only two decimal places
                int intPos = strinput.IndexOf('.', 0, strinput.Length);
                if ((intPos > 0))
                {
                    if (strinput.Length > intPos + dgt)
                    {
                        if (Char.IsDigit(e.KeyChar))
                        {
                            e.Handled = true;
                            return;
                        }
                    }
                }
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

        }

        // Not Used

        public static void MaskDecimal(DevExpress.XtraGrid.Views.Grid.GridView GView, KeyPressEventArgs e)
        {
            string strinput = GView.EditingValue.ToString();
            int Decimals = 2;
            int inCaretPos = 0;
            int inDecimalPoint = strinput.IndexOf('.', 0, strinput.Length);
            if ((inDecimalPoint >= 0) && ((inDecimalPoint + Decimals) < strinput.Length)
                && (strinput != "0.00") && (e.KeyChar != 8) && (inCaretPos >
                (inDecimalPoint - 1)))
            {
                e.Handled = true;
                return;
            }
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == 8 ||
                e.KeyChar == '-')
            {
                //Allow "." only once for decimal separator.
                if (e.KeyChar == '.')
                {
                    if (strinput.IndexOf('.', 0, strinput.Length) >= 0)
                    {
                        e.Handled = true;
                        return;
                    }
                }
                // Allow -ve only at the first position.
                if (e.KeyChar == '-')
                {
                    if (strinput.IndexOf('-', 0, strinput.Length) == 0)
                    {
                        e.Handled = true;
                        return;
                    }
                    else
                    {
                        strinput = "";
                    }
                    //e.Handled = true;
                    //return;

                }

                //allow only two decimal places
                int intPos = strinput.IndexOf('.', 0, strinput.Length);
                if ((intPos > 0))
                {
                    if (strinput.Length > intPos + 2)
                    {
                        if (Char.IsDigit(e.KeyChar))
                        {
                            e.Handled = true;
                            return;
                        }
                    }
                }
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

        }

        // Masking Decimal upto 2 digits

        public static void MaskDecimal1(DevExpress.XtraGrid.Views.Grid.GridView GView, KeyPressEventArgs e)
        {
            string strinput = GView.EditingValue.ToString();
            int Decimals = 2;
            int inCaretPos = 0;
            int inDecimalPoint = strinput.IndexOf('.', 0, strinput.Length);
            if ((inDecimalPoint >= 0) && ((inDecimalPoint + Decimals) < strinput.Length)
                && (strinput != "0.00") && (e.KeyChar != 8) && (inCaretPos >
                (inDecimalPoint - 1)))
            {
                e.Handled = true;
                return;
            }
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == 8 ||
                e.KeyChar == '-')
            {
                //Allow "." only once for decimal separator.
                if (e.KeyChar == '.')
                {
                    if (strinput.IndexOf('.', 0, strinput.Length) >= 0)
                    {
                        e.Handled = true;
                        return;
                    }
                }
                // Allow -ve only at the first position.
                if (e.KeyChar == '-')
                {
                    if (strinput.IndexOf('-', 0, strinput.Length) == 0)
                    {
                        e.Handled = true;
                        return;
                    }
                    else
                    {
                        strinput = "";
                    }

                }

                //allow only two decimal places
                int intPos = strinput.IndexOf('.', 0, strinput.Length);
                if ((intPos > 0))
                {
                    if (strinput.Length > intPos + 2)
                    {
                        if (Char.IsDigit(e.KeyChar))
                        {
                            e.Handled = true;
                            return;
                        }
                    }
                }
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

        }

        public static void MaskDecimal2(DevExpress.XtraGrid.Views.Grid.GridView GView, KeyPressEventArgs e)
        {
            string strinput = GView.EditingValue.ToString();
            int Decimals = 2;
            int inCaretPos = 0;
            int inDecimalPoint = strinput.IndexOf('.', 0, strinput.Length);
            if ((inDecimalPoint >= 0) && ((inDecimalPoint + Decimals) < strinput.Length)
                && (strinput != "0.00") && (e.KeyChar != 8) && (inCaretPos >
                (inDecimalPoint - 1)))
            {
                e.Handled = true;
                return;
            }
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == 8)
            {
                //Allow "." only once for decimal separator.
                if (e.KeyChar == '.')
                {
                    if (strinput.IndexOf('.', 0, strinput.Length) >= 0)
                    {
                        e.Handled = true;
                        return;
                    }
                }
                // Allow -ve only at the first position.
                if (e.KeyChar == '-')
                {
                    if (strinput.IndexOf('-', 0, strinput.Length) == 0)
                    {
                        e.Handled = true;
                        return;
                    }
                    else
                    {
                        strinput = "";
                    }

                }

                //allow only two decimal places
                int intPos = strinput.IndexOf('.', 0, strinput.Length);
                if ((intPos > 0))
                {
                    if (strinput.Length > intPos + 2)
                    {
                        if (Char.IsDigit(e.KeyChar))
                        {
                            e.Handled = true;
                            return;
                        }
                    }
                }
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

        }



        // Check Email Validation

        public static bool isEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        #region Set Record Count Status of Main Form

        public static void SetRecordCountStatus(int TotalRecords)
        {
            if (SystemVariables.RecordStatusItem == null) { return; }
            if (TotalRecords < 0) { SystemVariables.RecordStatusItem.Caption = " "; }
            else if (TotalRecords == 0) { SystemVariables.RecordStatusItem.Caption = Properties.Resources.NoRecord; }
            else if (TotalRecords == 1) { SystemVariables.RecordStatusItem.Caption = TotalRecords.ToString() + " " + Properties.Resources.Record; }
            else if (TotalRecords > 1) { SystemVariables.RecordStatusItem.Caption = TotalRecords.ToString() + " " + Properties.Resources.Records; }
        }

        #endregion

        #region Set Record Count In Tablets

        public static void SetRecordCountStatus(LabelControl lb, int TotalRecords)
        {
            if (TotalRecords < 0) { lb.Text = " "; }
            else if (TotalRecords == 0) { lb.Text = Properties.Resources.NoRecord; }
            else if (TotalRecords == 1) { lb.Text = TotalRecords.ToString() + " " + Properties.Resources.Record; }
            else if (TotalRecords > 1) { lb.Text = TotalRecords.ToString() + " " + Properties.Resources.Records; }
        }

        #endregion

        #region Set Record Count Status of Employee Main Form

        public static void SetRecordCountStatusOfEmployee(int TotalRecords)
        {
            if (SystemVariables.RecordStatusItemofEmp == null) { return; }
            if (TotalRecords < 0) { SystemVariables.RecordStatusItemofEmp.Caption = " "; }
            else if (TotalRecords == 0) { SystemVariables.RecordStatusItemofEmp.Caption = Properties.Resources.NoRecord; }
            else if (TotalRecords == 1) { SystemVariables.RecordStatusItemofEmp.Caption = TotalRecords.ToString() + " " + Properties.Resources.Record; }
            else if (TotalRecords > 1) { SystemVariables.RecordStatusItemofEmp.Caption = TotalRecords.ToString() + " " + Properties.Resources.Records; }
        }

        #endregion

        public static int GetHour(string strTime)
        {
            int intSeparatorIndex = strTime.IndexOf(":");
            int intSpaceIndex = strTime.IndexOf(" ");
            string strampm = strTime.Substring(intSpaceIndex + 1, strTime.Length - intSpaceIndex - 1);
            int intHour = GeneralFunctions.fnInt32(strTime.Substring(0, intSeparatorIndex));
            if (intHour == 0) return 0;
            if (strampm == "PM")
            {
                if (intHour < 12)
                {
                    intHour = intHour + 12;
                }
            }
            return intHour;
        }

        public static int GetMinute(string strTime)
        {
            int intSeparatorIndex = strTime.IndexOf(":");
            int intSpaceIndex = strTime.IndexOf(" ");
            string strampm = strTime.Substring(intSpaceIndex + 1, strTime.Length - intSpaceIndex - 1);
            int intMin = GeneralFunctions.fnInt32(strTime.Substring(intSeparatorIndex + 1, strTime.Length - intSpaceIndex - 1));
            return intMin;
        }

        public static string AMPM(string strTime)
        {
            int intSpaceIndex = strTime.IndexOf(" ");
            return strTime.Substring(intSpaceIndex + 1, strTime.Length - intSpaceIndex - 1);
        }

        // Rearrange Email Message Subject Text 

        public static string GetMessageSubject(string strRequest, string strSubject)
        {
            string strReturn = "";
            if (strSubject.StartsWith(strRequest))
            {
                strReturn = strSubject;
            }
            else
            {
                strReturn = strRequest + strSubject;
            }
            return strReturn;
        }

        public static string GetSpellChkDirectoryPath()
        {
            string csConnPath = "";
            string strfilename = "";

            csConnPath = System.Environment.CurrentDirectory;
            if (csConnPath.EndsWith("\\"))
            {
                strfilename = csConnPath + "american.xlg";
            }
            else
            {
                strfilename = csConnPath + "\\american.xlg";
            }

            return strfilename;
        }

        public static string GetSpellChkGrammarPath()
        {
            string csConnPath = "";
            string strfilename = "";

            csConnPath = System.Environment.CurrentDirectory;
            if (csConnPath.EndsWith("\\"))
            {
                strfilename = csConnPath + "english.aff";
            }
            else
            {
                strfilename = csConnPath + "\\english.aff";
            }

            return strfilename;
        }

        public static string GetSpellChkAlphabetPath()
        {
            string csConnPath = "";
            string strfilename = "";

            csConnPath = System.Environment.CurrentDirectory;
            if (csConnPath.EndsWith("\\"))
            {
                strfilename = csConnPath + "EnglishAlphabet.txt";
            }
            else
            {
                strfilename = csConnPath + "\\EnglishAlphabet.txt";
            }

            return strfilename;
        }

        #region Product Registration

        public static string CalcSerialNo(string strKey, string strCompName, string strAdd1, string strAdd2,
            string strCity, string strZip, string strState, string strEMail, int intTotalUser, string strModule, string strLD)
        {
            int intChar, intSerialNo, intAccess, intLicDate, intCompLen, intConLen, intAsciiVal1, intAsciiVal2,
                intAsciiVal3, intAsciiVal4, intAdd2Len, intCityLen, intZiLenp, intStateLen, intEMailLen, intModuleLen, intLDLen;
            string strTmpSerialNo, strChar, strSerialNo;
            int intDay, intMonth;
            strCompName = strCompName.ToUpper();
            intCompLen = strCompName.Length;
            intModuleLen = strModule.Length;
            intLDLen = strLD.Length;
            if (intCompLen > 0)
            {
                intAsciiVal1 = Convert.ToInt16(strCompName[1]);  // 2nd char
                intAsciiVal2 = Convert.ToInt16(strCompName[3]);	 //	4th char
            }
            else
            {
                intAsciiVal1 = 0;
                intAsciiVal2 = 0;
            }
            //WriteToLogFile("Calculate Serial No - AsciiVal 1 : " + intAsciiVal1.ToString(), 13);
            //WriteToLogFile("Calculate Serial No - AsciiVal 2 : " + intAsciiVal2.ToString(), 14);
            strAdd1 = strAdd1.ToUpper();
            intConLen = strAdd1.Length;
            if (intConLen > 0)
            {
                intAsciiVal3 = Convert.ToInt16(strAdd1[0]);  // 1nd char
                intAsciiVal4 = Convert.ToInt16(strAdd1[2]);	 //	3th char
            }
            else
            {
                intAsciiVal3 = 0;
                intAsciiVal4 = 0;
            }

            //WriteToLogFile("Calculate Serial No - AsciiVal 3 : " + intAsciiVal3.ToString(), 15);
            //WriteToLogFile("Calculate Serial No - AsciiVal 4 : " + intAsciiVal4.ToString(), 16);

            intAdd2Len = strAdd2.Length;
            intCityLen = strCity.Length;
            intZiLenp = strZip.Length;
            intStateLen = strState.Length;
            intEMailLen = strEMail.Length;

            intAccess = 1478;	//Arbitrary number
            // int intOptionCount = 1 + intTotalUser;

            int intOptionCount = 1 + intTotalUser + intCompLen + intConLen +
                                 intAsciiVal1 + intAsciiVal2 + intAsciiVal3 + intAsciiVal4 +
                                 intAdd2Len + intCityLen + intZiLenp + intStateLen + intEMailLen + intModuleLen + intLDLen;

            //WriteToLogFile("Calculate Serial No - intOptionCount : " + intOptionCount.ToString(), 17);

            if (intTotalUser == 0) intTotalUser = 9999;

            intSerialNo = intAccess * intOptionCount;
            intAccess = GeneralFunctions.fnInt32(intSerialNo.ToString().Substring(0, 4));


            intSerialNo = intAccess * intTotalUser;
            intSerialNo = GeneralFunctions.fnInt32(intSerialNo.ToString().Substring(1, 3));

            strTmpSerialNo = "";
            int intCharCount = strKey.Length;
            for (int iIndex = 1; iIndex < intCharCount; iIndex++)
            {
                strChar = strKey.Substring(iIndex, 1);
                intChar = GeneralFunctions.fnInt32(strChar) * intSerialNo;
                strTmpSerialNo = strTmpSerialNo + intChar.ToString();
            }
            //WriteToLogFile("Calculate Serial No - Temp Serial No 1 : " + strTmpSerialNo, 18);
            long lngTime = new DateTime(2099, 12, 31, 14, 14, 14).ToFileTime();
            //WriteToLogFile("Calculate Serial No - lngTime : " + lngTime.ToString(), 19);
            intLicDate = GeneralFunctions.fnInt32(lngTime.ToString().Substring(0, 4));
            //WriteToLogFile("Calculate Serial No - LicDate : " + intLicDate.ToString(), 20);
            strSerialNo = "";
            intCharCount = strTmpSerialNo.Length;

            for (int iIndex = 1; iIndex < intCharCount; iIndex++)
            {
                strChar = strTmpSerialNo.Substring(iIndex, 1);
                intChar = GeneralFunctions.fnInt32(strChar) * intLicDate;
                strSerialNo = strSerialNo + intChar.ToString();
            }
            //WriteToLogFile("Calculate Serial No - Temp Serial No 2 : " + strSerialNo, 21);
            strSerialNo = strSerialNo.Substring(0, 4) + "-" +
                strSerialNo.Substring(4, 4) + "-" +
                strSerialNo.Substring(8, 4) + "-" +
                strSerialNo.Substring(12, 4);
            //WriteToLogFile("Calculate Serial No - Serial No : " + strSerialNo, 22);



            //WriteToLogFile("-----------------Input--------------------", 23);
            //WriteToLogFile("Company : " + strCompName, 24);
            //WriteToLogFile("Address 1 : " + strAdd1, 25);
            //WriteToLogFile("Address 2 : " + strAdd2, 26);
            //WriteToLogFile("City : " + strCity, 27);
            //WriteToLogFile("Zip : " + strZip, 28);
            //WriteToLogFile("State : " + strState, 29);
            //WriteToLogFile("E-mail : " + strEMail, 30);
            //WriteToLogFile("User : " + intTotalUser.ToString(), 31);
            //WriteToLogFile("SL. No. : " + ActivationKey, 32);

            return strSerialNo;
        }

        public static int IsRegistrationValid(string strKeyCode, string strCompName, string strAdd1, string strAdd2,
            string strCity, string strZip, string strState, string strEMail, int intTotalUser, string CheckSerialNo, int intNoOfRecords, string strModule, string strLD)
        {
            string NewSerial = "";
            if (intNoOfRecords > 0)
            {
                NewSerial = CalcSerialNo(strKeyCode, strCompName, strAdd1, strAdd2, strCity, strZip, strState, strEMail, intTotalUser, strModule, strLD);
            }
            if (intNoOfRecords == 0)
            {
                return 0;
            }
            else if (NewSerial != CheckSerialNo)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }

        public static string CalculateKey()
        {
            string l3 = "Date Separator : " + System.Globalization.DateTimeFormatInfo.CurrentInfo.DateSeparator;
            string l1 = "Short Date Pattern : " + System.Globalization.DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
            string l2 = "Long Date Pattern : " + System.Globalization.DateTimeFormatInfo.CurrentInfo.LongDatePattern;

            string l4 = "Time Separator : " + System.Globalization.DateTimeFormatInfo.CurrentInfo.TimeSeparator;
            string l5 = "Short Time Pattern : " + System.Globalization.DateTimeFormatInfo.CurrentInfo.ShortTimePattern;
            string l6 = "Long Time Pattern : " + System.Globalization.DateTimeFormatInfo.CurrentInfo.LongTimePattern;

            string l7 = "AM Format : " + System.Globalization.DateTimeFormatInfo.CurrentInfo.AMDesignator;
            string l8 = "PM Format : " + System.Globalization.DateTimeFormatInfo.CurrentInfo.PMDesignator;
            string l9 = "Month Day Pattern : " + System.Globalization.DateTimeFormatInfo.CurrentInfo.MonthDayPattern;
            string l10 = "Year Month Pattern : " + System.Globalization.DateTimeFormatInfo.CurrentInfo.YearMonthPattern;

            WriteToLogFile(l3, 0);
            WriteToLogFile(l1, 1);
            WriteToLogFile(l2, 2);
            WriteToLogFile(l9, 3);
            WriteToLogFile(l10, 4);
            WriteToLogFile(l4, 5);
            WriteToLogFile(l5, 6);
            WriteToLogFile(l6, 7);
            WriteToLogFile(l7, 8);
            WriteToLogFile(l8, 9);

            int intChar;
            string strKey, strChar, strDateCreated;
            strKey = "";
            strDateCreated = "";

            DateTime DtTemp = new DateTime(2005, 1, 1);
            strDateCreated = DtTemp.ToFileTime().ToString();

            //WriteToLogFile("Calculate Key - Step 1 : " + strDateCreated, 10);

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
            //WriteToLogFile("Calculate Key - Step 2 : " + strKey, 11);
            //WriteToLogFile("Calculate Key - Step 3 : " + strKey.Substring(0, 16), 12);
            return strKey.Substring(0, 16);
        }

        public static string GetRegStatus()
        {
            RegistryKey RegDataKey = Registry.CurrentUser;
            RegistryKey RegDatabaseKey = RegDataKey.OpenSubKey(strRegPath + "\\Registration\\LastOperation");
            if (!(RegDatabaseKey == null))
            {
                return RegDatabaseKey.GetValue("EntryID").ToString();
            }
            else
            {
                return "";
            }
        }

        public static void SetRegStatus(string strEntryID)
        {
            RegistryKey RegDataKey = Registry.CurrentUser;
            RegistryKey RegDatabaseKey = RegDataKey.CreateSubKey(strRegPath + "\\Registration\\LastOperation");
            RegDatabaseKey.SetValue("EntryID", strEntryID);
        }

        #endregion

        // Get Terninal Name

        public static string GetHostName()
        {
            return Dns.GetHostName();
        }

        // Get Terninal IP

        public static string GetHostIP()
        {
            string strHostName = Dns.GetHostName();
            string strHostIP = "";
            IPHostEntry iphostentry = Dns.GetHostByName(strHostName);
            foreach (IPAddress ipaddress in iphostentry.AddressList)
            {
                strHostIP = ipaddress.ToString();
                break;
            }
            return strHostIP;
        }

        #region Set Item / Item Category control Font/Style

        public static Font GetPOSFont(string fFamily, string fSize, string B, string I)
        {
            if (fFamily == "") fFamily = "Tahoma";
            if (fSize == "") fSize = "9";
            if (fSize == "0") fSize = "9";
            if (B == "") B = "N";
            if (I == "") I = "N";

            Font f = new Font("Tahoma", 9, System.Drawing.FontStyle.Regular);
            if ((B == "N") && (I == "N"))
            {
                f = new Font(fFamily, float.Parse(fSize), System.Drawing.FontStyle.Regular);
            }
            else if ((B == "Y") && (I == "N"))
            {
                f = new Font(fFamily, float.Parse(fSize), System.Drawing.FontStyle.Bold);
            }
            else if ((B == "N") && (I == "Y"))
            {
                f = new Font(fFamily, float.Parse(fSize), System.Drawing.FontStyle.Italic);
            }
            else
            {
                f = new Font(fFamily, float.Parse(fSize), System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic);
            }
            return f;
        }

        public static string GetPOSApplicationStyle(string style)
        {
            string appstyle = "";
            if ((Settings.FormSkin == "") || (Settings.FormSkin == null))
            {
                appstyle = Settings.DefaultFormSkin;
            }
            else
            {
                appstyle = Settings.FormSkin;
            }

            if (style == "")
            {
                return "";
            }
            else if (style == appstyle)
            {
                return "";
            }
            else
            {
                return appstyle;
            }
        }

        public static string GetPOSStyle(string category, string style)
        {
            if (category == "Color")
            {
                return style;
            }
            else
            {
                if (style != "")
                {
                    return style;
                }
                else
                {
                    string appstyle = "";
                    if ((Settings.FormSkin == "") || (Settings.FormSkin == null))
                    {
                        appstyle = Settings.DefaultFormSkin;
                    }
                    else
                    {
                        appstyle = Settings.FormSkin;
                    }

                    return appstyle;
                }
            }
        }

        #endregion

        #region Custom Functions on Numeric Value

        public static Double RoundOff(double val)
        {
            return Math.Ceiling(val / .05) * .05;
        }

        public static decimal CustomRound(decimal x)
        {
            return decimal.Round(x - 0.001m, 2, MidpointRounding.AwayFromZero);
        }

        public static double FormatDouble(double dbl)
        {
            if (Settings.DecimalPlace == 3)
                return GeneralFunctions.fnDouble(dbl.ToString("f3"));
            else
                return GeneralFunctions.fnDouble(dbl.ToString("f"));
        }

        public static string FormatDouble1(double dbl)
        {
            if (Settings.DecimalPlace == 3)
                return String.Format("{0:0.000}", dbl);
            else
                return String.Format("{0:0.00}", dbl);
        }

        #endregion

        // Check if help file exists
        public static string IsHelpFileExists(string helpstr)
        {
            string ret = "";
            int len = helpstr.Length;
            string resultstr = helpstr.Substring(8, len - 8).ToLower().Trim();
            string exepath = System.Environment.CurrentDirectory;
            if (!exepath.EndsWith("\\"))
            {
                exepath = exepath + "\\";
            }
            exepath = exepath + "Help Files\\" + resultstr + ".htm";
            if (File.Exists(exepath))
            {
                ret = exepath;
            }
            return ret;
        }

        private static void WriteToLogFile(string FileLine, int CSVLine)
        {
            /*string LogFullPath = GetLogPath();
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
            fileStrm.Close();*/
        }

        #region Version Info

        public static string VersionInfo()
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                AssemblyName assemblyname = assembly.GetName();

                string strVersion = Properties.Resources.Version + " " + assemblyname.Version.Major.ToString() + "." +
                                    assemblyname.Version.Minor.ToString() + " " + Properties.Resources.Build + " " +
                                    assemblyname.Version.Build.ToString() + " " + Properties.Resources.Release + " " +
                                    assemblyname.Version.Revision.ToString();
                return strVersion;
            }
            catch
            {
                return "";
            }
        }

        public static string AppVersionInfo()
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                AssemblyName assemblyname = assembly.GetName();

                string strVersion = assemblyname.Version.Major.ToString() + "." +
                                    assemblyname.Version.Minor.ToString() + "." +
                                    assemblyname.Version.Build.ToString() + "." +
                                    assemblyname.Version.Revision.ToString();
                return strVersion;
            }
            catch
            {
                return "";
            }
        }

        public static string ShortVersionInfo()
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                AssemblyName assemblyname = assembly.GetName();

                string strVersion = "Version " + assemblyname.Version.Major.ToString() + "." +
                    assemblyname.Version.Minor.ToString();
                return strVersion;
            }
            catch
            {
                return "";
            }
        }

        public static string PaymentGatewayApplicationVersion()
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                AssemblyName assemblyname = assembly.GetName();

                string strVersion = assemblyname.Version.Major.ToString() + "." +
                    assemblyname.Version.Minor.ToString() + "." + assemblyname.Version.Build.ToString();
                return strVersion;
            }
            catch
            {
                return "";
            }
        }

        #endregion

        // Return Integer

        public static int fnInt32(object pval)
        {
            int rval = 0;
            bool blret = false;
            try
            {
                blret = Int32.TryParse(pval.ToString(), out rval);
                if (blret) rval = Convert.ToInt32((object)pval); else rval = 0;
                return rval;
            }
            catch
            {
                return rval;
            }
        }

        // Return Decimal
        public static decimal fnDecimal(object pval)
        {
            decimal rval = 0.0m;
            bool blret = false;
            try
            {
                blret = decimal.TryParse(pval.ToString(), out rval);
                if (blret) rval = Convert.ToDecimal((object)pval); else rval = 0;
                return rval;
            }
            catch
            {
                return rval;
            }
        }

        // Return Float

        public static double fnDouble(object pval)
        {
            double rval = 0.0;
            bool blret = false;
            try
            {
                blret = double.TryParse(pval.ToString(), out rval);
                if (blret) rval = Convert.ToDouble((object)pval); else rval = 0;
                return rval;
            }
            catch
            {
                return rval;
            }
        }

        // Return DateTime

        public static DateTime fnDate(object pval)
        {
            DateTime rval = DateTime.Now;
            bool blret = false;
            try
            {
                blret = DateTime.TryParse(pval.ToString(), out rval);
                if (blret) rval = Convert.ToDateTime((object)pval); else rval = DateTime.Now;
                return rval;
            }
            catch
            {
                return rval;
            }
        }

        // Make All Child Controls Read Only

        public static void MakeReadOnly(Control ParentControl)
        {
            int intChildCount = ParentControl.Controls.Count;
            int intChildIndex;
            for (intChildIndex = 0; intChildIndex < intChildCount; intChildIndex++)
            {
                if (ParentControl.Controls[intChildIndex] is DevExpress.XtraEditors.BaseEdit)
                    (ParentControl.Controls[intChildIndex] as DevExpress.XtraEditors.BaseEdit).Properties.ReadOnly = true;

            }
        }

        // Make DEMO Watermark in Reports

        public static void MakeReportWatermark(XtraReport xrRep)
        {
            if (Settings.DemoVersion == "Y")
            {
                xrRep.Watermark.Text = Properties.Resources.DEMOVERSION;
                xrRep.Watermark.Font = new Font("Segoe UI", 20.25f, System.Drawing.FontStyle.Regular);
                xrRep.Watermark.ForeColor = Color.SlateGray;
                xrRep.Watermark.TextDirection = DevExpress.XtraPrinting.Drawing.DirectionMode.BackwardDiagonal;
                xrRep.Watermark.ShowBehind = true;
                xrRep.Watermark.TextTransparency = 20;
            }
        }

        #region SQL from Database

        public static int GetSKUDetails(int SKU, int Vendor, ref string pSKU, ref string Desciption, ref double Cost, ref string PrintLabel,
                                        ref string VendPartNo, ref string DP, ref int CaseQty, ref string CaseUPC, ref string PType, ref string SBar)
        {
            int intID = 0;

            int intVendorExist = IfExistVendorPartNo(SKU, Vendor);

            string strSQL = "";

            if (intVendorExist == 0)
            {
                strSQL = " Select ID,SKU,Description,Cost,PrintBarCode, '' as PartNumber,DecimalPlace,CaseQty,CaseUPC,ProductType,ScaleBarCode from Product Where ID = @PID ";
            }
            else
            {
                strSQL = " Select P.ID as ID,P.SKU,P.Description as Description,P.PrintBarCode as PrintBarCode,P.ProductType,P.ScaleBarCode, "
                       + " P.DecimalPlace, VP.PartNumber as PartNumber, VP.Price as Cost,VP.PackQty as CaseQty,P.CaseUPC as CaseUPC from Product P left outer join "
                       + " VendPart VP on P.ID = VP.ProductID where P.ID = @PID and VP.VendorID = @VID ";
            }
            SqlConnection objConnection = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objCommand = new SqlCommand(strSQL, objConnection);
            objCommand.Parameters.Add(new SqlParameter("@PID", System.Data.SqlDbType.Int));
            objCommand.Parameters["@PID"].Value = SKU;
            if (intVendorExist != 0)
            {
                objCommand.Parameters.Add(new SqlParameter("@VID", System.Data.SqlDbType.Int));
                objCommand.Parameters["@VID"].Value = Vendor;
            }

            try
            {
                if (objConnection.State == System.Data.ConnectionState.Open) { objConnection.Close(); }
                objConnection.Open();

                SqlDataReader objReader = null;
                objReader = objCommand.ExecuteReader();
                if (objReader.Read())
                {
                    pSKU = objReader["SKU"].ToString().Trim();
                    VendPartNo = objReader["PartNumber"].ToString().Trim(); ;
                    intID = GeneralFunctions.fnInt32(objReader["ID"].ToString().Trim());
                    Desciption = objReader["Description"].ToString().Trim();
                    if (objReader["Cost"].ToString().Trim() != "")
                    {
                        Cost = GeneralFunctions.fnDouble(objReader["Cost"].ToString().Trim());
                    }
                    else
                    {
                        Cost = 0;
                    }
                    CaseQty = GeneralFunctions.fnInt32(objReader["CaseQty"].ToString().Trim());
                    CaseUPC = objReader["CaseUPC"].ToString().Trim();

                    PrintLabel = objReader["PrintBarCode"].ToString().Trim();
                    DP = objReader["DecimalPlace"].ToString().Trim();

                    SBar = objReader["ScaleBarCode"].ToString().Trim();
                    PType = objReader["ProductType"].ToString().Trim();
                }
                if (Settings.NotReadBarcodeCheckDigit == "N")
                {
                    /*if (((pSKU.Length == 12) || (pSKU.Length == 13)) && (pSKU.Substring(0, 1) == "2") && (SBar == "Y"))
                    {
                        SBar = "Y";
                    }
                    else
                    {
                        SBar = "N";
                    }*/

                    if (((pSKU.Length == 12) || (pSKU.Length == 13)) && (pSKU.Substring(0, 1) == Settings.EmbeddedBarcodeNumberSystemChar) && (SBar == "Y"))
                    {
                        SBar = "Y";
                    }
                    else
                    {
                        SBar = "N";
                    }
                }
                if (Settings.NotReadBarcodeCheckDigit == "Y")
                {
                    /*if (((pSKU.Length == 11) || (pSKU.Length == 12)) && (pSKU.Substring(0, 1) == "2") && (SBar == "Y"))
                    {
                        SBar = "Y";
                    }
                    else
                    {
                        SBar = "N";
                    }*/

                    if (((pSKU.Length == 11) || (pSKU.Length == 12)) && (pSKU.Substring(0, 1) == Settings.EmbeddedBarcodeNumberSystemChar) && (SBar == "Y"))
                    {
                        SBar = "Y";
                    }
                    else
                    {
                        SBar = "N";
                    }
                }
                objReader.Close();

                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();

                return intID;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();
                return 0;
            }
        }

        public static int GetStockTakeSKUDetails(int SKU, ref string pSKU, ref string Desciption, ref double Cost, ref double PriceA, ref double PriceB, ref double PriceC,
                                                    ref double QtyOnHand, ref double BreakPackRatio, ref int LinkSKU)
        {
            int intID = 0;

            string strSQL = "";
            strSQL = " select ID,SKU,Description,Cost,PriceA,PriceB,PriceC,QtyOnHand,BreakPackRatio,LinkSKU from Product where ID = @PID ";

            SqlConnection objConnection = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objCommand = new SqlCommand(strSQL, objConnection);
            objCommand.Parameters.Add(new SqlParameter("@PID", System.Data.SqlDbType.Int));
            objCommand.Parameters["@PID"].Value = SKU;

            try
            {
                if (objConnection.State == System.Data.ConnectionState.Open) { objConnection.Close(); }
                objConnection.Open();

                SqlDataReader objReader = null;
                objReader = objCommand.ExecuteReader();
                if (objReader.Read())
                {
                    pSKU = objReader["SKU"].ToString().Trim();
                    intID = GeneralFunctions.fnInt32(objReader["ID"].ToString().Trim());
                    Desciption = objReader["Description"].ToString().Trim();
                    if (objReader["Cost"].ToString().Trim() != "") Cost = GeneralFunctions.fnDouble(objReader["Cost"].ToString().Trim()); else Cost = 0;
                    if (objReader["PriceA"].ToString().Trim() != "") PriceA = GeneralFunctions.fnDouble(objReader["PriceA"].ToString().Trim()); else PriceA = 0;
                    if (objReader["PriceB"].ToString().Trim() != "") PriceB = GeneralFunctions.fnDouble(objReader["PriceB"].ToString().Trim()); else PriceB = 0;
                    if (objReader["PriceC"].ToString().Trim() != "") PriceC = GeneralFunctions.fnDouble(objReader["PriceC"].ToString().Trim()); else PriceC = 0;
                    if (objReader["QtyOnHand"].ToString().Trim() != "") QtyOnHand = GeneralFunctions.fnDouble(objReader["QtyOnHand"].ToString().Trim()); else QtyOnHand = 0;
                    if (objReader["BreakPackRatio"].ToString().Trim() != "") BreakPackRatio = GeneralFunctions.fnDouble(objReader["BreakPackRatio"].ToString().Trim()); else BreakPackRatio = 0;
                    if (objReader["LinkSKU"].ToString().Trim() != "") LinkSKU = GeneralFunctions.fnInt32(objReader["LinkSKU"].ToString().Trim()); else LinkSKU = 0;
                }
                objReader.Close();

                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();

                return intID;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();
                return 0;
            }
        }

        public static int IfExistVendorPartNo(int ProductID, int VendorID)
        {
            int intID = 0;
            string strSQL = " Select count(*) as RecCount from VendPart where VendorID = @VendorID and ProductID = @ProductID ";

            SqlConnection objConnection = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objCommand = new SqlCommand(strSQL, objConnection);
            objCommand.Parameters.Add(new SqlParameter("@VendorID", System.Data.SqlDbType.Int));
            objCommand.Parameters["@VendorID"].Value = VendorID;
            objCommand.Parameters.Add(new SqlParameter("@ProductID", System.Data.SqlDbType.Int));
            objCommand.Parameters["@ProductID"].Value = ProductID;

            try
            {
                if (objConnection.State == System.Data.ConnectionState.Open) { objConnection.Close(); }
                objConnection.Open();

                SqlDataReader objReader = null;
                objReader = objCommand.ExecuteReader();
                if (objReader.Read())
                {
                    intID = GeneralFunctions.fnInt32(objReader["RecCount"].ToString().Trim());
                }
                objReader.Close();

                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();

                return intID;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();
                return 0;
            }
        }

        // Calcutate Tax for Tax Table

        public static double GetTaxFromTaxTable(int TxID, double TxPerc, double AppValue)
        {
            double aval = AppValue;
            if (AppValue < 0) aval = -aval;

            double val = 0;

            DataTable dtbl = new DataTable();
            PosDataObject.Tax objtx = new PosDataObject.Tax();
            objtx.Connection = SystemVariables.Conn;
            dtbl = objtx.ShowDetailRecord(TxID);
            if (dtbl.Rows.Count == 0)
            {
                val = GeneralFunctions.FormatDouble((TxPerc * aval) / 100);
            }
            else
            {
                bool exactval = false;
                double ttx = 0;
                bool startrange = false;
                bool endrange = false;
                int cnt = 0;
                foreach (DataRow dr in dtbl.Rows)
                {
                    cnt++;
                    if (GeneralFunctions.fnDouble(dr["BreakPoints"].ToString()) == aval)
                    {
                        exactval = true;
                        ttx = GeneralFunctions.fnDouble(dr["Tax"].ToString());
                        break;
                    }

                    if (aval > GeneralFunctions.fnDouble(dr["BreakPoints"].ToString()))
                    {
                        startrange = true;
                        ttx = GeneralFunctions.fnDouble(dr["Tax"].ToString());
                    }
                    else
                    {
                        if (startrange)
                        {
                            endrange = true;
                            break;
                        }
                    }
                }

                if ((exactval) || (startrange && endrange))
                {
                    val = ttx;
                }
                else
                {
                    if (!startrange && !endrange)
                    {
                        val = 0;
                    }
                    if (startrange && !endrange)
                    {
                        val = GeneralFunctions.FormatDouble((TxPerc * aval) / 100);
                    }
                }
            }
            if (AppValue < 0) val = -val;
            return val;
        }

        // No. of Records exist in a Database Table

        public static int GetRecordCount(string strTableName)
        {
            int intRecCount = 0;

            string strSQL = "select COUNT(*) AS RECCOUNT from " + strTableName.Trim();
            SqlConnection objConnection = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objCommand = new SqlCommand(strSQL, objConnection);
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Open) { objConnection.Close(); }
                objConnection.Open();

                SqlDataReader objReader = null;
                objReader = objCommand.ExecuteReader();
                if (objReader.Read())
                {
                    intRecCount = GeneralFunctions.fnInt32(objReader["RECCOUNT"].ToString());
                }
                objReader.Close();

                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();

                return intRecCount;
            }
            catch
            {
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();
                return 0;
            }
        }



        public static int GetRecordCountGC(string CentralFlag, string pStore)
        {
            int intresult = 0;
            string strSQL = "";

            if (CentralFlag == "Y")
                strSQL = " select count(*) as rcnt from GiftCert where IssueStore = @StoreCode ";
            if (CentralFlag == "N")
                strSQL = " select count(*) as rcnt from GiftCert";

            SqlConnection objConnection = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objCommand = new SqlCommand(strSQL, objConnection);
            SqlDataReader objSQLReader = null;
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Open) { objConnection.Close(); }
                objConnection.Open();
                
                if (CentralFlag == "Y")
                {
                    objCommand.Parameters.Add(new SqlParameter("@StoreCode", System.Data.SqlDbType.NVarChar));
                    objCommand.Parameters["@StoreCode"].Value = pStore;
                }

                objSQLReader = objCommand.ExecuteReader();

                while (objSQLReader.Read())
                {
                    intresult = fnInt32(objSQLReader["rcnt"].ToString());
                }
                objSQLReader.Close();

                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();
                return intresult;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                objSQLReader.Close();
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();
                return 0;
            }
        }

        //********Get Record count on the basos of id ***********************

        public static int GetStoreID(string strTableName,string storeCode)
        {
            int intRecCount = 0;

            string strSQL = "select ID from " + strTableName.Trim() + " Where storeCode="+storeCode.Trim();
            SqlConnection objConnection = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objCommand = new SqlCommand(strSQL, objConnection);
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Open) { objConnection.Close(); }
                objConnection.Open();

                SqlDataReader objReader = null;
                objReader = objCommand.ExecuteReader();
                if (objReader.Read())
                {
                    intRecCount = GeneralFunctions.fnInt32(objReader["ID"].ToString());
                }
                objReader.Close();

                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();

                return intRecCount;
            }
            catch
            {
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();
                return 0;
            }
        }

        //*************************************************************************

        public static int GetRecordCountForDelete(string strTableName, string strField, int intID)
        {
            int intRecCount = 0;

            string strSQL = " select COUNT(*) AS RECCOUNT from " + strTableName.Trim() + " where " + strField.Trim() + " = @ID ";
            SqlConnection objConnection = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objCommand = new SqlCommand(strSQL, objConnection);
            objCommand.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objCommand.Parameters["@ID"].Value = intID;

            try
            {
                if (objConnection.State == System.Data.ConnectionState.Open) { objConnection.Close(); }
                objConnection.Open();

                SqlDataReader objReader = null;
                objReader = objCommand.ExecuteReader();
                if (objReader.Read())
                {
                    intRecCount = GeneralFunctions.fnInt32(objReader["RECCOUNT"].ToString());
                }
                objReader.Close();

                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();

                return intRecCount;
            }
            catch
            {
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();
                return 0;
            }
        }


        public static int GetRecordCountForDeleteTemplate(int intID)
        {
            int intRecCount = 0;

            string strSQL = " select COUNT(*) AS RECCOUNT from localsetup where paramname like '%All Templates -%' and paramvalue = '" + intID.ToString() + "'";
            SqlConnection objConnection = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objCommand = new SqlCommand(strSQL, objConnection);
           

            try
            {
                if (objConnection.State == System.Data.ConnectionState.Open) { objConnection.Close(); }
                objConnection.Open();

                SqlDataReader objReader = null;
                objReader = objCommand.ExecuteReader();
                if (objReader.Read())
                {
                    intRecCount = GeneralFunctions.fnInt32(objReader["RECCOUNT"].ToString());
                }
                objReader.Close();

                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();

                return intRecCount;
            }
            catch
            {
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();
                return 0;
            }
        }


        public static int GetRefRecordCountFromScaleMapping(string strMappingType, int intID)
        {
            int intRecCount = 0;

            string strSQL = " select COUNT(*) AS RECCOUNT from Scale_Mapping where MappingType=@MTYPE and MappingID = @ID ";
            SqlConnection objConnection = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objCommand = new SqlCommand(strSQL, objConnection);
            objCommand.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objCommand.Parameters.Add(new SqlParameter("@MTYPE", System.Data.SqlDbType.VarChar));
            objCommand.Parameters["@ID"].Value = intID;
            objCommand.Parameters["@MTYPE"].Value = strMappingType;

            try
            {
                if (objConnection.State == System.Data.ConnectionState.Open) { objConnection.Close(); }
                objConnection.Open();

                SqlDataReader objReader = null;
                objReader = objCommand.ExecuteReader();
                if (objReader.Read())
                {
                    intRecCount = GeneralFunctions.fnInt32(objReader["RECCOUNT"].ToString());
                }
                objReader.Close();

                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();

                return intRecCount;
            }
            catch
            {
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();
                return 0;
            }
        }

        public static int GetRefRecordCountFromGeneralMapping(string strRefType, string strMappingType, int intID)
        {
            int intRecCount = 0;

            string strSQL = " select COUNT(*) AS RECCOUNT from GeneralMapping where ReferenceType = @RTYPE and MappingType=@MTYPE and ReferenceID = @ID ";
            SqlConnection objConnection = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objCommand = new SqlCommand(strSQL, objConnection);
            objCommand.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objCommand.Parameters.Add(new SqlParameter("@RTYPE", System.Data.SqlDbType.VarChar));
            objCommand.Parameters.Add(new SqlParameter("@MTYPE", System.Data.SqlDbType.VarChar));
            objCommand.Parameters["@ID"].Value = intID;
            objCommand.Parameters["@RTYPE"].Value = strRefType;
            objCommand.Parameters["@MTYPE"].Value = strMappingType;

            try
            {
                if (objConnection.State == System.Data.ConnectionState.Open) { objConnection.Close(); }
                objConnection.Open();

                SqlDataReader objReader = null;
                objReader = objCommand.ExecuteReader();
                if (objReader.Read())
                {
                    intRecCount = GeneralFunctions.fnInt32(objReader["RECCOUNT"].ToString());
                }
                objReader.Close();

                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();

                return intRecCount;
            }
            catch
            {
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();
                return 0;
            }
        }

        public static int GetTaxRecordCountForDelete(string strTableName, int intID, string pMapping)
        {
            int intRecCount = 0;

            string strSQL = " select COUNT(*) AS RECCOUNT from " + strTableName.Trim() + " where MappingID = @ID and MappingType = " + pMapping;
            SqlConnection objConnection = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objCommand = new SqlCommand(strSQL, objConnection);
            objCommand.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
            objCommand.Parameters["@ID"].Value = intID;

            try
            {
                if (objConnection.State == System.Data.ConnectionState.Open) { objConnection.Close(); }
                objConnection.Open();

                SqlDataReader objReader = null;
                objReader = objCommand.ExecuteReader();
                if (objReader.Read())
                {
                    intRecCount = GeneralFunctions.fnInt32(objReader["RECCOUNT"].ToString());
                }
                objReader.Close();

                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();

                return intRecCount;
            }
            catch
            {
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();
                return 0;
            }
        }

        public static bool ProceedFromItemImage(int pID)
        {
            if (Settings.AutoDisplayItemImage == "N")
            {
                return true;
            }
            else
            {
                System.Windows.Controls.Image pic = new System.Windows.Controls.Image();
                if (!GetPhotoFromTable(pic, pID, "Product"))
                {
                    return true;
                }
                else
                {
                    bool b = false;
                    frm_POSShopFromImageDlg frm = new frm_POSShopFromImageDlg();
                    try
                    {
                        frm.PhotoID = pID;
                        frm.ShowDialog();
                        if (frm.DialogResult == true)
                        {
                            b = true;
                        }
                       
                    }
                    finally
                    {
                        frm.Close();
                    }
                    return b;
                }
            }
        }

        // Get GwarePOS Application Version

        public static string GetAppVersionFromDB()
        {
            string strval = "";

            string strSQL = "select appversion from dbversion ";
            SqlConnection objConnection = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objCommand = new SqlCommand(strSQL, objConnection);
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Open) { objConnection.Close(); }
                objConnection.Open();

                SqlDataReader objReader = null;
                objReader = objCommand.ExecuteReader();
                if (objReader.Read())
                {
                    strval = objReader["appversion"].ToString();
                }
                objReader.Close();

                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();

                return strval;
            }
            catch
            {
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();
                return "";
            }
        }

        // Update GwarePOS Application Version

        public static string UpdateAppVersion()
        {
            string strval = "";

            string strSQL = "update dbversion set appversion = @prm ";
            SqlConnection objConnection = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objCommand = new SqlCommand(strSQL, objConnection);

            objCommand.Parameters.Add(new SqlParameter("@prm", System.Data.SqlDbType.VarChar));
            objCommand.Parameters["@prm"].Value = AppVersionInfo();

            try
            {
                if (objConnection.State == System.Data.ConnectionState.Open) { objConnection.Close(); }
                objConnection.Open();
                objCommand.ExecuteNonQuery();

                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();

                return strval;
            }
            catch
            {
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();
                return "";
            }
        }

        public static bool GetPhotoFromTable1(PictureBox PhotoBox, int intInputID, string ImageType)
        {
            bool retval = false;
            string strSQLComm = "";
            if (ImageType == "Customer")
                strSQLComm = "select CustomerPhoto from Customer where ID = @CODE ";
            else
                if (ImageType == "Employee")
                strSQLComm = "select EmployeePhoto from Employee where ID = @CODE ";
            else
                    if (ImageType == "Product")
                strSQLComm = "select ProductPhoto from Product where ID = @CODE ";
            else
                        if (ImageType == "Logo")
                strSQLComm = "select CompanyLogo from StoreInfo ";
            else
                            if (ImageType == "Customer_notes")
                strSQLComm = "select AttachFile from notes where ID = @CODE and RefType = 'Customer'";
            else
                                if (ImageType == "Employee_notes")
                strSQLComm = "select AttachFile from notes where ID = @CODE and RefType = 'Employee'";


            SqlConnection objSQLCon = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, objSQLCon);
            if (ImageType != "Logo")
            {
                objSQlComm.Parameters.Add(new SqlParameter("@CODE", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@CODE"].Value = intInputID;
            }

            try
            {
                if (objSQLCon.State == System.Data.ConnectionState.Closed) { objSQLCon.Open(); }
                SqlDataReader objSQLReader = null;
                byte[] content = (byte[])objSQlComm.ExecuteScalar();
                try
                {
                    MemoryStream stream = new MemoryStream(content);
                    PhotoBox.Image = Image.FromStream(stream);
                    stream.Close();
                    stream.Dispose();
                    retval = true;
                }
                catch
                {
                    //MessageBox.Show(ex.Message.ToString());
                    //MessageBox.Show(ex.StackTrace.ToString ()); 
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                objSQLCon.Close();
                objSQLCon.Dispose();
            }
            catch
            {
                objSQlComm.Dispose();
                objSQLCon.Close();
                objSQLCon.Dispose();
            }
            return retval;
        }

        public static bool GetPhotoFromTable(System.Windows.Controls.Image PhotoBox, int intInputID, string ImageType)
        {
            bool retval = false;
            string strSQLComm = "";
            if (ImageType == "Customer")
                strSQLComm = "select CustomerPhoto from Customer where ID = @CODE ";
            else if (ImageType == "Employee")
                strSQLComm = "select EmployeePhoto from Employee where ID = @CODE ";
            else if (ImageType == "Product")
                strSQLComm = "select ProductPhoto from Product where ID = @CODE ";
            else if (ImageType == "Logo")
                strSQLComm = "select CompanyLogo from StoreInfo ";
            else if (ImageType == "ScaleLogo")
                strSQLComm = "select ScaleLogo from StoreInfo ";
            else if (ImageType == "Customer_notes")
                strSQLComm = "select AttachFile from notes where ID = @CODE and RefType = 'Customer'";
            else if (ImageType == "Employee_notes")
                strSQLComm = "select AttachFile from notes where ID = @CODE and RefType = 'Employee'";


            SqlConnection objSQLCon = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, objSQLCon);
            if (!((ImageType == "Logo") || (ImageType == "ScaleLogo")))
            {
                objSQlComm.Parameters.Add(new SqlParameter("@CODE", System.Data.SqlDbType.Int));
                objSQlComm.Parameters["@CODE"].Value = intInputID;
            }

            try
            {
                if (objSQLCon.State == System.Data.ConnectionState.Closed) { objSQLCon.Open(); }
                //SqlDataReader objSQLReader = null;
                byte[] content = (byte[])objSQlComm.ExecuteScalar();
                try
                {
                    try
                    {
                        // assign byte array data into memory stream 
                        MemoryStream stream = new MemoryStream(content);

                        // set transparent bitmap with 32 X 32 size by memory stream data 
                        Bitmap b = new Bitmap(stream);
                        Bitmap output = new Bitmap(b, new System.Drawing.Size(32, 32));
                        output.MakeTransparent();

                        System.Windows.Media.Imaging.BitmapImage bi = new System.Windows.Media.Imaging.BitmapImage();
                        bi.BeginInit();
                        System.Drawing.Image tempImage = (System.Drawing.Image)output;
                        MemoryStream ms = new MemoryStream();
                        tempImage.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

                        stream.Seek(0, SeekOrigin.Begin);

                        bi.StreamSource = stream;

                        bi.EndInit();

                        PhotoBox.Source = bi;

                    }
                    catch (Exception ex)
                    {
                        PhotoBox.Source = null;
                    }
                    finally
                    {
                    }
                    //MemoryStream stream = new MemoryStream(content);
                    //ResMan.SetImage(PhotoBox, stream);
                    //stream.Close();
                    //stream.Dispose();
                    retval = true;
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message.ToString());
                    //MessageBox.Show(ex.StackTrace.ToString ()); 
                }
                //objSQLReader.Close();
                objSQlComm.Dispose();
                objSQLCon.Close();
                objSQLCon.Dispose();
            }
            catch
            {
                objSQlComm.Dispose();
                objSQLCon.Close();
                objSQLCon.Dispose();
            }
            return retval;
        }

        public static bool GetPhotoFromTableForReport(XRPictureBox PhotoBox, int intInputID, string ImageType)
        {
            bool retval = false;
            string strSQLComm = "";
            if (ImageType == "Customer")
                strSQLComm = "select CustomerPhoto from Customer where ID = @CODE";
            else if (ImageType == "Employee")
                strSQLComm = "select EmployeePhoto from Employee where ID = @CODE";
            else if (ImageType == "Product")
                strSQLComm = "select ProductPhoto from Product where ID = @CODE";
            else if (ImageType == "Graphic Art")
                strSQLComm = "select GraphicArt from GraphicArts where ID = @CODE";

            SqlConnection objSQLCon = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, objSQLCon);
            objSQlComm.Parameters.Add(new SqlParameter("@CODE", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@CODE"].Value = intInputID;

            try
            {
                if (objSQLCon.State == System.Data.ConnectionState.Closed) { objSQLCon.Open(); }
                SqlDataReader objSQLReader = null;
                byte[] content = (byte[])objSQlComm.ExecuteScalar();
                try
                {
                    MemoryStream stream = new MemoryStream(content);
                    PhotoBox.Image = Image.FromStream(stream);
                    stream.Close();
                    stream.Dispose();
                    retval = true;
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message.ToString());
                    //MessageBox.Show(ex.StackTrace.ToString ()); 
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                objSQLCon.Close();
                objSQLCon.Dispose();
            }
            catch
            {
                objSQlComm.Dispose();
                objSQLCon.Close();
                objSQLCon.Dispose();
            }
            return retval;
        }

        public static bool CheckIfUsedInOrder(string strwhat, int intchkID)
        {
            bool blflag = false;
            string strSQL = "";
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("logic", System.Type.GetType("System.String"));
            if (strwhat == "Discount")
                strSQL = "select discountlogic as logicstring from item ";
            if (strwhat == "Surcharge")
                strSQL = "select surchargelogic as logicstring from item ";
            if (strwhat == "Tax")
                strSQL = "select taxlogic as logicstring from item ";
            SqlConnection objConnection = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objCommand = new SqlCommand(strSQL, objConnection);
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Open) { objConnection.Close(); }
                objConnection.Open();

                SqlDataReader objReader = null;
                objReader = objCommand.ExecuteReader();
                if (objReader.Read())
                {
                    dtbl.Rows.Add(new object[] { objReader["logicstring"].ToString() });
                }

                if (dtbl.Rows.Count == 0) blflag = true;
                else
                {
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        string OrginalH = dr["logic"].ToString();
                        char FindH = ',';
                        int NumberOfOccurancesH = 0;

                        string CopyOrginalH = string.Copy(OrginalH);
                        int PlaceH = 0;

                        PlaceH = CopyOrginalH.IndexOf(FindH.ToString());
                        int i = 0;
                        while (PlaceH != -1)
                        {
                            string temp = "";
                            int length = 0;
                            temp = CopyOrginalH.Substring(length, PlaceH);
                            if (temp == intchkID.ToString())
                            {
                                blflag = true;
                                break;
                            }
                            length = temp.Length + 1;
                            CopyOrginalH = CopyOrginalH.Substring(PlaceH + 1);
                            PlaceH = CopyOrginalH.IndexOf(FindH.ToString());
                            i++;
                        }
                    }
                }
                dtbl.Dispose();
                objReader.Close();
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();

                return blflag;
            }
            catch
            {
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();
                return false;
            }
        }

        public static bool CheckMarkdownInMapping(int intchkID)
        {
            int rcnt = 0;
            bool blflag = false;
            string strSQL = "";
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("logic", System.Type.GetType("System.String"));
            strSQL = " select count(*) as RecCount from Scale_Markdown_Map where Markdown = @did and Markdown > 0 ";
            SqlConnection objConnection = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objCommand = new SqlCommand(strSQL, objConnection);
            objCommand.Parameters.Add(new SqlParameter("@did", System.Data.SqlDbType.Int));
            objCommand.Parameters["@did"].Value = intchkID;
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Open) { objConnection.Close(); }
                objConnection.Open();

                SqlDataReader objReader = null;
                objReader = objCommand.ExecuteReader();
                if (objReader.Read())
                {
                    rcnt = Convert.ToInt32(objReader["RecCount"].ToString());
                }

                if (rcnt > 0) blflag = true;

                dtbl.Dispose();
                objReader.Close();
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();

                return blflag;
            }
            catch
            {
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();
                return false;
            }

        }

        public static bool CheckMarkdownInPrint(int intchkID)
        {
            int rcnt = 0;
            bool blflag = false;
            string strSQL = "";
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("logic", System.Type.GetType("System.String"));
            strSQL = " select count(*) as RecCount from Scale_Markdown_Print where MarkdownRefID = @did and MarkdownRefID > 0 ";
            SqlConnection objConnection = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objCommand = new SqlCommand(strSQL, objConnection);
            objCommand.Parameters.Add(new SqlParameter("@did", System.Data.SqlDbType.Int));
            objCommand.Parameters["@did"].Value = intchkID;
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Open) { objConnection.Close(); }
                objConnection.Open();

                SqlDataReader objReader = null;
                objReader = objCommand.ExecuteReader();
                if (objReader.Read())
                {
                    rcnt = Convert.ToInt32(objReader["RecCount"].ToString());
                }

                if (rcnt > 0) blflag = true;

                dtbl.Dispose();
                objReader.Close();
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();

                return blflag;
            }
            catch
            {
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();
                return false;
            }

        }


        public static bool CheckItemDiscountInOrder(int intchkID)
        {
            int rcnt = 0;
            bool blflag = false;
            string strSQL = "";
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("logic", System.Type.GetType("System.String"));
            strSQL = " select count(*) as RecCount from item where discountid = @did and discountid > 0 ";
            SqlConnection objConnection = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objCommand = new SqlCommand(strSQL, objConnection);
            objCommand.Parameters.Add(new SqlParameter("@did", System.Data.SqlDbType.Int));
            objCommand.Parameters["@did"].Value = intchkID;
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Open) { objConnection.Close(); }
                objConnection.Open();

                SqlDataReader objReader = null;
                objReader = objCommand.ExecuteReader();
                if (objReader.Read())
                {
                    rcnt = Convert.ToInt32(objReader["RecCount"].ToString());
                }

                if (rcnt > 0) blflag = true;

                dtbl.Dispose();
                objReader.Close();
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();

                return blflag;
            }
            catch
            {
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();
                return false;
            }

        }

        public static bool CheckTicketDiscountInOrder(int intchkID)
        {
            int rcnt = 0;
            bool blflag = false;
            string strSQL = "";
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("logic", System.Type.GetType("System.String"));
            strSQL = " select count(*) as RecCount from item where productid = @did and producttype = 'C' ";
            SqlConnection objConnection = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objCommand = new SqlCommand(strSQL, objConnection);
            objCommand.Parameters.Add(new SqlParameter("@did", System.Data.SqlDbType.Int));
            objCommand.Parameters["@did"].Value = intchkID;
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Open) { objConnection.Close(); }
                objConnection.Open();

                SqlDataReader objReader = null;
                objReader = objCommand.ExecuteReader();
                if (objReader.Read())
                {
                    rcnt = Convert.ToInt32(objReader["RecCount"].ToString());
                }

                if (rcnt > 0) blflag = true;

                dtbl.Dispose();
                objReader.Close();
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();

                return blflag;
            }
            catch
            {
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();
                return false;
            }
        }

        public static bool CheckDiscountInCustomer(int intchkID)
        {
            int rcnt = 0;
            bool blflag = false;
            string strSQL = "";

            strSQL = " select count(*) as RecCount from customer where discountid = @did ";
            SqlConnection objConnection = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objCommand = new SqlCommand(strSQL, objConnection);
            objCommand.Parameters.Add(new SqlParameter("@did", System.Data.SqlDbType.Int));
            objCommand.Parameters["@did"].Value = intchkID;
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Open) { objConnection.Close(); }
                objConnection.Open();

                SqlDataReader objReader = null;
                objReader = objCommand.ExecuteReader();
                if (objReader.Read())
                {
                    rcnt = Convert.ToInt32(objReader["RecCount"].ToString());
                }

                if (rcnt > 0) blflag = true;

                objReader.Close();
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();

                return blflag;
            }
            catch
            {
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();
                return false;
            }
        }

        public static bool CheckFeesInOrder(int intchkID)
        {
            int rcnt = 0;
            bool blflag = false;
            string strSQL = "";
            strSQL = " select count(*) as RecCount from item where feesid = @did and feesid > 0 ";
            SqlConnection objConnection = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objCommand = new SqlCommand(strSQL, objConnection);
            objCommand.Parameters.Add(new SqlParameter("@did", System.Data.SqlDbType.Int));
            objCommand.Parameters["@did"].Value = intchkID;
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Open) { objConnection.Close(); }
                objConnection.Open();

                SqlDataReader objReader = null;
                objReader = objCommand.ExecuteReader();
                if (objReader.Read())
                {
                    rcnt = Convert.ToInt32(objReader["RecCount"].ToString());
                }

                if (rcnt > 0) blflag = true;

                objReader.Close();
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();

                return blflag;
            }
            catch
            {
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();
                return false;
            }

        }

        public static bool CheckFeesInSuspendOrder(int intchkID)
        {
            int rcnt = 0;
            bool blflag = false;
            string strSQL = "";
            strSQL = " select count(*) as RecCount from suspnded where feesid = @did and feesid > 0 ";
            SqlConnection objConnection = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objCommand = new SqlCommand(strSQL, objConnection);
            objCommand.Parameters.Add(new SqlParameter("@did", System.Data.SqlDbType.Int));
            objCommand.Parameters["@did"].Value = intchkID;
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Open) { objConnection.Close(); }
                objConnection.Open();

                SqlDataReader objReader = null;
                objReader = objCommand.ExecuteReader();
                if (objReader.Read())
                {
                    rcnt = Convert.ToInt32(objReader["RecCount"].ToString());
                }

                if (rcnt > 0) blflag = true;

                objReader.Close();
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();

                return blflag;
            }
            catch
            {
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();
                return false;
            }

        }

        public static bool CheckFeesInWorkOrder(int intchkID)
        {
            int rcnt = 0;
            bool blflag = false;
            string strSQL = "";
            strSQL = " select count(*) as RecCount from workorder where feesid = @did and feesid > 0 ";
            SqlConnection objConnection = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objCommand = new SqlCommand(strSQL, objConnection);
            objCommand.Parameters.Add(new SqlParameter("@did", System.Data.SqlDbType.Int));
            objCommand.Parameters["@did"].Value = intchkID;
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Open) { objConnection.Close(); }
                objConnection.Open();

                SqlDataReader objReader = null;
                objReader = objCommand.ExecuteReader();
                if (objReader.Read())
                {
                    rcnt = Convert.ToInt32(objReader["RecCount"].ToString());
                }

                if (rcnt > 0) blflag = true;

                objReader.Close();
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();

                return blflag;
            }
            catch
            {
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();
                return false;
            }

        }

        public static bool CheckSaleBatchInOrder(int intchkID)
        {
            int rcnt = 0;
            bool blflag = false;
            string strSQL = "";
            strSQL = " select count(*) as RecCount from item where salepriceid = @did and salepriceid > 0 ";
            SqlConnection objConnection = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objCommand = new SqlCommand(strSQL, objConnection);
            objCommand.Parameters.Add(new SqlParameter("@did", System.Data.SqlDbType.Int));
            objCommand.Parameters["@did"].Value = intchkID;
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Open) { objConnection.Close(); }
                objConnection.Open();

                SqlDataReader objReader = null;
                objReader = objCommand.ExecuteReader();
                if (objReader.Read())
                {
                    rcnt = Convert.ToInt32(objReader["RecCount"].ToString());
                }

                if (rcnt > 0) blflag = true;

                objReader.Close();
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();

                return blflag;
            }
            catch
            {
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();
                return false;
            }

        }

        public static void SetDetailedTransactionLog(string lgInstance, string lgDetails, string refID)
        {
            string strSQLComm = "";
            strSQLComm = "insert into logs(LogTime,LogDetails,LogInstance,RefID)values(@p1,@p2,@p3,@p4) ";

            SqlConnection objSQLCon = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, objSQLCon);

            objSQlComm.Parameters.Add(new SqlParameter("@p1", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@p1"].Value = DateTime.Now;
            objSQlComm.Parameters.Add(new SqlParameter("@p2", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@p2"].Value = lgDetails;
            objSQlComm.Parameters.Add(new SqlParameter("@p3", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@p3"].Value = lgInstance;
            objSQlComm.Parameters.Add(new SqlParameter("@p4", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@p4"].Value = refID;

            try
            {
                if (objSQLCon.State == System.Data.ConnectionState.Closed) { objSQLCon.Open(); }
                objSQlComm.ExecuteNonQuery();
                objSQlComm.Dispose();
                objSQLCon.Close();
                objSQLCon.Dispose();
            }
            catch (Exception er)
            {
                objSQlComm.Dispose();
                objSQLCon.Close();
                objSQLCon.Dispose();
            }
        }

        public static void SetTransactionLog(string lgInstance, string lgDetails)
        {
            string strSQLComm = "";
            strSQLComm = "insert into logs(LogTime,LogDetails,LogInstance)values(@p1,@p2,@p3) ";

            SqlConnection objSQLCon = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, objSQLCon);

            objSQlComm.Parameters.Add(new SqlParameter("@p1", System.Data.SqlDbType.DateTime));
            objSQlComm.Parameters["@p1"].Value = DateTime.Now;
            objSQlComm.Parameters.Add(new SqlParameter("@p2", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@p2"].Value = (lgDetails.Length < 257) ? lgDetails : lgDetails.Substring(0, 256);
            objSQlComm.Parameters.Add(new SqlParameter("@p3", System.Data.SqlDbType.NVarChar));
            objSQlComm.Parameters["@p3"].Value = lgInstance;

            try
            {
                if (objSQLCon.State == System.Data.ConnectionState.Closed) { objSQLCon.Open(); }
                objSQlComm.ExecuteNonQuery();
                objSQlComm.Dispose();
                objSQLCon.Close();
                objSQLCon.Dispose();
            }
            catch
            {
                objSQlComm.Dispose();
                objSQLCon.Close();
                objSQLCon.Dispose();
            }
        }

        public static int IsExistLocalSetupData(string pHost, string pParam)
        {
            int intRecCount = 0;

            string strSQL = " select COUNT(*) AS RECCOUNT from LocalSetup where HostName=@HostName and ParamName=@ParamName ";

            SqlConnection objConnection = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objCommand = new SqlCommand(strSQL, objConnection);

            objCommand.Parameters.Add(new SqlParameter("@HostName", System.Data.SqlDbType.NVarChar));
            objCommand.Parameters.Add(new SqlParameter("@ParamName", System.Data.SqlDbType.VarChar));
            objCommand.Parameters["@HostName"].Value = pHost;
            objCommand.Parameters["@ParamName"].Value = pParam;

            try
            {
                if (objConnection.State == System.Data.ConnectionState.Open) { objConnection.Close(); }
                objConnection.Open();

                SqlDataReader objReader = null;
                objReader = objCommand.ExecuteReader();
                if (objReader.Read())
                {
                    intRecCount = GeneralFunctions.fnInt32(objReader["RECCOUNT"].ToString());
                }
                objReader.Close();

                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();

                return intRecCount;
            }
            catch
            {
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();
                return 0;
            }
        }

        public static string GetScaleGraduationText()
        {
            string strval = "";
            string strSQL = " select isnull(Graduation,'') as GTxt from ScaleGraduation1 ";

            SqlConnection objConnection = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objCommand = new SqlCommand(strSQL, objConnection);

            try
            {
                if (objConnection.State == System.Data.ConnectionState.Open) { objConnection.Close(); }
                objConnection.Open();

                SqlDataReader objReader = null;
                objReader = objCommand.ExecuteReader();
                if (objReader.Read())
                {
                    strval = objReader["GTxt"].ToString();
                }
                objReader.Close();

                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();

                return strval;
            }
            catch
            {
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();
                return "";
            }
        }

        public static string GetLocalSetupData(string pHost, string pParam)
        {
            string strval = "";
            string strSQL = " select ParamValue from LocalSetup where HostName=@HostName and ParamName=@ParamName ";

            SqlConnection objConnection = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objCommand = new SqlCommand(strSQL, objConnection);
            objCommand.Parameters.Add(new SqlParameter("@HostName", System.Data.SqlDbType.NVarChar));
            objCommand.Parameters.Add(new SqlParameter("@ParamName", System.Data.SqlDbType.VarChar));

            objCommand.Parameters["@HostName"].Value = pHost;
            objCommand.Parameters["@ParamName"].Value = pParam;

            try
            {
                if (objConnection.State == System.Data.ConnectionState.Open) { objConnection.Close(); }
                objConnection.Open();

                SqlDataReader objReader = null;
                objReader = objCommand.ExecuteReader();
                if (objReader.Read())
                {
                    strval = objReader["ParamValue"].ToString();
                }
                objReader.Close();

                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();

                return strval;
            }
            catch
            {
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();
                return "";
            }
        }

        public static string GetLocalSetupDataSpecial(string pHost, string pParam)
        {
            string strval = "";
            string strSQL = " select ParamValue from LocalSetup where  ParamName=@ParamName ";

            SqlConnection objConnection = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objCommand = new SqlCommand(strSQL, objConnection);
            objCommand.Parameters.Add(new SqlParameter("@HostName", System.Data.SqlDbType.NVarChar));
            objCommand.Parameters.Add(new SqlParameter("@ParamName", System.Data.SqlDbType.VarChar));

            objCommand.Parameters["@HostName"].Value = pHost;
            objCommand.Parameters["@ParamName"].Value = pParam;

            try
            {
                if (objConnection.State == System.Data.ConnectionState.Open) { objConnection.Close(); }
                objConnection.Open();

                SqlDataReader objReader = null;
                objReader = objCommand.ExecuteReader();
                if (objReader.Read())
                {
                    strval = objReader["ParamValue"].ToString();
                }
                objReader.Close();

                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();

                return strval;
            }
            catch
            {
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();
                return "";
            }
        }

        // Markdown Printer
        public static string GetLocalPrinterForMarkdown(string pHost, string pParam, string pDept)
        {
            string strval = "(None)";
            string strSQL = " select ParamValue from LocalSetup where HostName=@HostName and ParamName=@ParamName and ParamValue like '" + pDept + "|%'  ";

            SqlConnection objConnection = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objCommand = new SqlCommand(strSQL, objConnection);
            objCommand.Parameters.Add(new SqlParameter("@HostName", System.Data.SqlDbType.NVarChar));
            objCommand.Parameters.Add(new SqlParameter("@ParamName", System.Data.SqlDbType.VarChar));

            objCommand.Parameters["@HostName"].Value = pHost;
            objCommand.Parameters["@ParamName"].Value = pParam;

            try
            {
                if (objConnection.State == System.Data.ConnectionState.Open) { objConnection.Close(); }
                objConnection.Open();

                SqlDataReader objReader = null;
                objReader = objCommand.ExecuteReader();
                if (objReader.Read())
                {
                    strval = objReader["ParamValue"].ToString().Substring(objReader["ParamValue"].ToString().IndexOf("|") + 1);
                }
                objReader.Close();

                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();

                return strval;
            }
            catch
            {
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();
                return "";
            }
        }


        public static string GetShiftDetail(int intEMPID)
        {
            DataSet dtSet = new DataSet();

            string strResult = "";
            string strSQLComm = "";

            strSQLComm = " select a.ShiftName, a.StartTime, a.EndTime from ShiftMaster a, Employee b where a.ID = b.EMPSHIFT and b.ID = @EID ";

            SqlConnection objSQLCon = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, objSQLCon);

            objSQlComm.Parameters.Add(new SqlParameter("@EID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@EID"].Value = intEMPID;

            SqlDataReader objSQLReader = null;

            try
            {
                if (objSQLCon.State == System.Data.ConnectionState.Closed) { objSQLCon.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();
                while (objSQLReader.Read())
                {

                    strResult = objSQLReader["ShiftName"].ToString() + SEPARATOR +
                                objSQLReader["StartTime"].ToString() + " - " + objSQLReader["EndTime"].ToString();

                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                objSQLCon.Close();
                objSQLCon.Dispose();
                return strResult;
            }
            catch (Exception Ex)
            {
                string csMsg = Ex.Message;
                objSQLReader.Close();
                objSQlComm.Dispose();
                objSQLCon.Close();
                objSQLCon.Dispose();
                return "";
            }
        }

        public static int GetShiftID(int intEMPID)
        {
            DataSet dtSet = new DataSet();

            int intResult = 0;
            string strSQLComm = "";

            strSQLComm = " select a.ID as ShiftID from ShiftMaster a, Employee b where a.ID = b.EMPSHIFT and b.ID = @EID ";

            SqlConnection objSQLCon = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, objSQLCon);

            objSQlComm.Parameters.Add(new SqlParameter("@EID", System.Data.SqlDbType.Int));
            objSQlComm.Parameters["@EID"].Value = intEMPID;

            SqlDataReader objSQLReader = null;

            try
            {
                if (objSQLCon.State == System.Data.ConnectionState.Closed) { objSQLCon.Open(); }

                objSQLReader = objSQlComm.ExecuteReader();
                while (objSQLReader.Read())
                {
                    intResult = GeneralFunctions.fnInt32(objSQLReader["ShiftID"].ToString());
                }
                objSQLReader.Close();
                objSQlComm.Dispose();
                objSQLCon.Close();
                objSQLCon.Dispose();
                return intResult;
            }
            catch (Exception Ex)
            {
                string csMsg = Ex.Message;
                objSQLReader.Close();
                objSQlComm.Dispose();
                objSQLCon.Close();
                objSQLCon.Dispose();
                return 0;
            }
        }

        public static int GetCloseOutID()
        {
            PosDataObject.Closeout objCloseOut = new PosDataObject.Closeout();
            objCloseOut.Connection = SystemVariables.Conn;
            objCloseOut.LoginUserID = SystemVariables.CurrentUserID;
            objCloseOut.TerminalName = Settings.TerminalName;
            return objCloseOut.FetchCloseoutID();
        }

        #endregion

        // Get Physical Path of the connected database
        public static string GetDBFilePath()
        {
            string strval = "";

            string strSQL = "select physical_name from sys.database_files where type = 0";
            SqlConnection objConnection = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objCommand = new SqlCommand(strSQL, objConnection);
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Open) { objConnection.Close(); }
                objConnection.Open();

                SqlDataReader objReader = null;
                objReader = objCommand.ExecuteReader();
                if (objReader.Read())
                {
                    strval = objReader["physical_name"].ToString();
                }
                objReader.Close();

                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();
                strval = strval.Remove(strval.Length - 16);

                return strval;
            }
            catch
            {
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();
                return "";
            }
        }

        // Get Current User Path for Mercury payment
        public static string FetchCurrentUserPath()
        {
            string p = "";
            p = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (p.EndsWith("\\"))
            {
                p = p + SystemVariables.BrandName + "\\Mercury";
            }
            else
            {
                p = p + "\\" + SystemVariables.BrandName + "\\Mercury";
            }
            return p;
        }

        // Get Current User Path for Element Express payment
        public static string FetchCurrentUserPath_Element()
        {
            string p = "";
            p = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (p.EndsWith("\\"))
            {
                p = p + SystemVariables.BrandName + "\\Element";
            }
            else
            {
                p = p + "\\" + SystemVariables.BrandName + "\\Element";
            }
            return p;
        }

        // Get Current User Path for Barcode Printing
        public static string FetchCurrentUserPath_BarCodePrinting()
        {
            string p = "";
            p = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (p.EndsWith("\\"))
            {
                p = p + SystemVariables.BrandName + "\\BarCode";
            }
            else
            {
                p = p + "\\" + SystemVariables.BrandName + "\\BarCode";
            }
            return p;
        }

        // Write BarCode Printer Command in a File

        public static void CreateBarCodePrintCommandInFile(bool sideprint, int pwidth, string pSKU, string pDesc, int pQty, string pAMT)
        {
            string currentuserpath = FetchCurrentUserPath_BarCodePrinting();
            if (!Directory.Exists(currentuserpath))
                Directory.CreateDirectory(currentuserpath);
            string fl = currentuserpath + "\\prncmd.txt";
            StreamWriter sw = new StreamWriter(fl);


            sw.WriteLine("! 0 100 90 " + pQty.ToString());
            sw.WriteLine("PITCH 100");
            if (pwidth > 0)
            {
                sw.WriteLine("WIDTH " + pwidth.ToString());
            }
            if (sideprint) sw.WriteLine("MULTIPLE 2");
            sw.WriteLine("STRING 8X8 7 0 " + pSKU);
            sw.WriteLine("STRING 8X8 7 10 " + pDesc);
            sw.WriteLine("BARCODE CODE128A 1 20 20 " + pSKU);
            sw.WriteLine("STRING 8X8 7 40 $" + pAMT);
            sw.WriteLine("END");
            sw.Close();
        }

        // Mercury Response in XML

        public static void CreateMercuryTransactionXML(string xmlstring, string tokenused)
        {
            string currentuserpath = FetchCurrentUserPath();
            if (!Directory.Exists(currentuserpath))
                Directory.CreateDirectory(currentuserpath);
            string fl = currentuserpath + "\\log.xml";
            StreamWriter sw = new StreamWriter(fl);
            sw.WriteLine("<!--  Token used : " + tokenused + "   -->");
            sw.Write(xmlstring);
            sw.Close();
        }

        // Mercury Response in XML

        public static void CreateMercuryTransactionXML1(string xmlstring, string tokenused, string xmlrequest)
        {
            string currentuserpath = FetchCurrentUserPath();
            if (!Directory.Exists(currentuserpath)) Directory.CreateDirectory(currentuserpath);
            string fl = currentuserpath + "\\log.xml";
            StreamWriter sw = new StreamWriter(fl);
            sw.Write(xmlrequest);
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("<!--  Token used : " + tokenused + "   -->");
            sw.Write(xmlstring);
            sw.Close();
        }

        #region Number to Word

        public static String changeNumericToWords(double numb)
        {

            String num = numb.ToString();

            return changeToWords(num, false);

        }

        public static String changeCurrencyToWords(String numb)
        {

            return changeToWords(numb, true);

        }

        public static String changeNumericToWords(String numb)
        {

            return changeToWords(numb, false);

        }

        public static String changeCurrencyToWords(double numb)
        {

            return changeToWords(numb.ToString(), true);

        }

        public static String changeToWords(String numb, bool isCurrency)
        {

            String val = "", wholeNo = numb, points = "", andStr = "", pointStr = "";

            String endStr = (isCurrency) ? Properties.Resources.Only : ("");

            try
            {

                int decimalPlace = numb.IndexOf(".");

                if (decimalPlace > 0)
                {

                    wholeNo = numb.Substring(0, decimalPlace);

                    points = numb.Substring(decimalPlace + 1);

                    if (Convert.ToInt32(points) > 0)
                    {

                        andStr = (isCurrency) ? Properties.Resources.and : Properties.Resources.point;// just to separate whole numbers from points/cents

                        endStr = (isCurrency) ? (Properties.Resources.Paisas + " " + endStr) : ("");

                        pointStr = translateCents(points);

                    }

                }

                val = String.Format("{0} {1}{2} {3}", translateWholeNumber(wholeNo).Trim(), andStr, pointStr, endStr);

            }

            catch {; }

            return val;

        }

        public static String translateWholeNumber(String number)
        {

            string word = "";

            try
            {

                bool beginsZero = false;//tests for 0XX

                bool isDone = false;//test if already translated

                double dblAmt = (Convert.ToDouble(number));

                //if ((dblAmt > 0) && number.StartsWith("0"))

                if (dblAmt > 0)
                {//test for zero or digit zero in a nuemric

                    beginsZero = number.StartsWith("0");

                    int numDigits = number.Length;

                    int pos = 0;//store digit grouping

                    String place = "";//digit grouping name:hundres,thousand,etc...

                    switch (numDigits)
                    {

                        case 1://ones' range

                            word = ones(number);

                            isDone = true;

                            break;

                        case 2://tens' range

                            word = tens(number);

                            isDone = true;

                            break;

                        case 3://hundreds' range

                            pos = (numDigits % 3) + 1;

                            place = " " + Properties.Resources.Hundred + " ";

                            break;

                        case 4://thousands' range

                        case 5:


                        case 6:



                        case 7://millions' range

                        case 8:

                        case 9:

                            pos = (numDigits % 7) + 1;

                            place = " " + Properties.Resources.Million + " ";

                            break;

                        case 10://Billions's range

                            pos = (numDigits % 10) + 1;

                            place = " " + Properties.Resources.Billion + " ";

                            break;

                        //add extra case options for anything above Billion...

                        default:

                            isDone = true;

                            break;

                    }

                    if (!isDone)
                    {//if transalation is not done, continue...(Recursion comes in now!!)

                        word = translateWholeNumber(number.Substring(0, pos)) + place + translateWholeNumber(number.Substring(pos));

                        //check for trailing zeros

                        if (beginsZero) word = " " + Properties.Resources.and + " " + word.Trim();

                    }

                    //ignore digit grouping names

                    if (word.Trim().Equals(place.Trim())) word = "";

                }

            }

            catch {; }

            return word.Trim();

        }

        public static String tens(String digit)
        {

            int digt = Convert.ToInt32(digit);

            String name = null;

            switch (digt)
            {

                case 10:

                    name = Properties.Resources.Ten;

                    break;

                case 11:

                    name = Properties.Resources.Eleven;

                    break;

                case 12:

                    name = Properties.Resources.Twelve;

                    break;

                case 13:

                    name = Properties.Resources.Thirteen;

                    break;

                case 14:

                    name = Properties.Resources.Fourteen;

                    break;

                case 15:

                    name = Properties.Resources.Fifteen;

                    break;

                case 16:

                    name = Properties.Resources.Sixteen;

                    break;

                case 17:

                    name = Properties.Resources.Seventeen;

                    break;

                case 18:

                    name = Properties.Resources.Eighteen;

                    break;

                case 19:

                    name = Properties.Resources.Nineteen;

                    break;

                case 20:

                    name = Properties.Resources.Twenty;

                    break;

                case 30:

                    name = Properties.Resources.Thirty;

                    break;

                case 40:

                    name = Properties.Resources.Fourty;

                    break;

                case 50:

                    name = Properties.Resources.Fifty;

                    break;

                case 60:

                    name = Properties.Resources.Sixty;

                    break;

                case 70:

                    name = Properties.Resources.Seventy;

                    break;

                case 80:

                    name = Properties.Resources.Eighty;

                    break;

                case 90:

                    name = Properties.Resources.Ninety;

                    break;

                default:

                    if (digt > 0)
                    {

                        name = tens(digit.Substring(0, 1) + "0") + " " + ones(digit.Substring(1));

                    }

                    break;

            }

            return name;

        }

        public static String ones(String digit)
        {

            int digt = Convert.ToInt32(digit);

            String name = "";

            switch (digt)
            {

                case 1:

                    name = Properties.Resources.One;

                    break;

                case 2:

                    name = Properties.Resources.Two;

                    break;

                case 3:

                    name = Properties.Resources.Three;

                    break;

                case 4:

                    name = Properties.Resources.Four;

                    break;

                case 5:

                    name = Properties.Resources.Five;

                    break;

                case 6:

                    name = Properties.Resources.Six;

                    break;

                case 7:

                    name = Properties.Resources.Seven;

                    break;

                case 8:

                    name = Properties.Resources.Eight;

                    break;

                case 9:

                    name = Properties.Resources.Nine;

                    break;

            }

            return name;

        }

        public static String translateCents(String cents)
        {

            String cts = "", digit = "", engOne = "";

            for (int i = 0; i < cents.Length; i++)
            {

                digit = cents[i].ToString();

                if (digit.Equals("0"))
                {

                    engOne = Properties.Resources.Zero;

                }

                else
                {

                    engOne = ones(digit);

                }

                cts += " " + engOne;

            }

            return cts;

        }


        #endregion

        #region TNP-CG Integration

        public static string GetRequestFileName(string trantype)
        {
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = SystemVariables.Conn;
            int intMaxInvNo = objPOS.FetchMaxInvoiceNo();
            return "R_" + trantype + "_" + intMaxInvNo.ToString() + "_" + Settings.TerminalName + "_" + DateTime.Now.ToString(SystemVariables.DateFormat + " hh:mm:ss") + ".xml";
        }

        public static void TNPCG_Response(string trantype, double amt, ref string resp, ref string resptxt)
        {
            CGtrantype = trantype;
            CGamt = amt;
            PosDataObject.POS objPOS = new PosDataObject.POS();
            objPOS.Connection = SystemVariables.Conn;
            CGinv = objPOS.FetchMaxInvoiceNo();

            CGrequestfile = "R_" + trantype + "_" + CGinv.ToString() + "_" + Settings.TerminalName + "_" + DateTime.Now.ToString("MMddyyyyhhmmss") + ".xml";
            CGanswerfile = "A_" + CGrequestfile.Remove(0, 2);

            CGmonitor = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (CGmonitor.EndsWith("\\")) CGmonitor = CGmonitor + "Precidia\\";
            else CGmonitor = CGmonitor + "\\Precidia\\";

            FileSystemWatcher watcher = new FileSystemWatcher(CGmonitor, "A_*.xml");
            watcher.EnableRaisingEvents = true;
            watcher.Created += new FileSystemEventHandler(watcher_Created);

            XmlDocument XDoc = new XmlDocument();

            // Create root node.
            XmlElement XElemRoot = XDoc.CreateElement("PLRequest");

            XDoc.AppendChild(XElemRoot);

            XmlElement XTemp = XDoc.CreateElement("Command");
            XTemp.InnerText = CGtrantype;
            XElemRoot.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("Id");
            XTemp.InnerText = CGinv.ToString();
            XElemRoot.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("Amount");
            XTemp.InnerText = CGamt.ToString("f");
            XElemRoot.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("Input");
            XTemp.InnerText = "GET";
            XElemRoot.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("ClientId");
            XTemp.InnerText = Settings.TerminalName;
            XElemRoot.AppendChild(XTemp);

            XDoc.Save(CGmonitor + CGrequestfile);


            resp = CGresp;
            resptxt = CGresptxt;
        }

        private static void watcher_Created(object source, FileSystemEventArgs e)
        {
            WaitToLoad(CGmonitor + CGrequestfile);
            WaitToLoad(CGmonitor + CGanswerfile);
            if (!blCG)
            {
                if (File.Exists(CGmonitor + CGanswerfile))
                {
                    XmlDocument XDoc1 = new XmlDocument();
                    XDoc1.Load(CGmonitor + CGanswerfile);
                    XmlNodeList nd = XDoc1.GetElementsByTagName("Result");
                    for (int i = 0; i < nd.Count; ++i)
                    {
                        CGresp = nd[i].InnerText;
                    }
                    XmlNodeList nd1 = XDoc1.GetElementsByTagName("ResultText");
                    for (int i = 0; i < nd1.Count; ++i)
                    {
                        CGresptxt = nd1[i].InnerText;
                    }
                    //MessageBox.Show(CGresp);
                    //MessageBox.Show(CGresptxt);
                    XDoc1 = null;
                    blCG = true;
                }
            }
        }

        private static void XMLFileProcessing()
        {
            XmlDocument XDoc = new XmlDocument();

            // Create root node.
            XmlElement XElemRoot = XDoc.CreateElement("PLRequest");

            XDoc.AppendChild(XElemRoot);

            XmlElement XTemp = XDoc.CreateElement("Command");
            XTemp.InnerText = CGtrantype;
            XElemRoot.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("Id");
            XTemp.InnerText = CGinv.ToString();
            XElemRoot.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("Amount");
            XTemp.InnerText = CGamt.ToString("f");
            XElemRoot.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("Input");
            XTemp.InnerText = "GET";
            XElemRoot.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("ClientId");
            XTemp.InnerText = Settings.TerminalName;
            XElemRoot.AppendChild(XTemp);

            XDoc.Save(CGmonitor + CGrequestfile);

        }

        public static void WaitToLoad(string fileName)
        {
            DateTime dtStart = DateTime.Now;
            TimeSpan ts = new TimeSpan(0);
            while (true)
            {
                try
                {
                    ts = DateTime.Now.Subtract
                    (dtStart);
                    FileStream fs = new FileStream
                    (fileName, FileMode.Open, FileAccess.Read, FileShare.None);
                    fs.Close();
                    return;
                }
                catch (FileNotFoundException)
                {
                    throw;
                }
                catch (IOException exc)
                {
                    Thread.Sleep(1000);
                    continue;
                }
            }
        }

        #endregion

        #region Print / Email Report

        public static void EmailReport(DevExpress.XtraReports.UI.XtraReport r, string attachfile, string attachtitle)
        {
            try
            {

                string emailattachpth = "";
                emailattachpth = Path.GetTempPath();
                if (emailattachpth.EndsWith("\\")) emailattachpth = emailattachpth + SystemVariables.BrandName + "\\Email\\";
                else emailattachpth = emailattachpth + "\\" + SystemVariables.BrandName + "\\Email\\";
                if (!Directory.Exists(emailattachpth)) Directory.CreateDirectory(emailattachpth);
                else
                {
                    Directory.Delete(@emailattachpth, true);
                    Directory.CreateDirectory(emailattachpth);
                }

                Attachment attch = null;

                bool bl = false;
                try
                {
                    string fpth = Path.Combine(emailattachpth, attachfile);
                    DevExpress.XtraPrinting.ExportOptions ex = new DevExpress.XtraPrinting.ExportOptions();
                    ex.PrintPreview.SaveMode = DevExpress.XtraPrinting.SaveMode.UsingDefaultPath;
                    ex.PrintPreview.ActionAfterExport = ActionAfterExport.None;

                    var reportPath = fpth;
                    var stream = new MemoryStream();
                    r.ExportToPdf(stream);

                    using (var fs = System.IO.File.Create(@reportPath))
                    {
                        stream.WriteTo(fs);
                    }
                    stream.Close();

                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                    smtp.Host = Settings.REHost;
                    smtp.Port = Settings.REPort;
                    smtp.EnableSsl = (Settings.RESSL == "Y");
                    smtp.Credentials = new System.Net.NetworkCredential(Settings.REUser, Settings.REPassword);

                    MailMessage msg = new MailMessage();
                    msg.To.Add(Settings.ReportEmail);
                    msg.From = new System.Net.Mail.MailAddress(Settings.REFromAddress, Settings.REFromName);

                    if (Settings.REReplyTo != "") msg.ReplyTo = new System.Net.Mail.MailAddress(Settings.REReplyTo);

                    msg.Subject = attachtitle;
                    msg.Body = Properties.Resources.pleaseFind + " " + attachtitle + ".";

                    foreach (string f in Directory.GetFiles(emailattachpth))
                    {
                        FileInfo fi = new FileInfo(f);
                        attch = new Attachment(fi.FullName);
                        msg.Attachments.Add(attch);
                    }

                    smtp.Send(msg);
                    bl = true;
                }
                catch
                {
                    bl = false;
                }
                if (attch != null) attch.Dispose();

                if (!bl)
                {
                    DocMessage.MsgInformation(Properties.Resources.ErrorwhilesendingEmail);
                }
                else
                {
                    DocMessage.MsgInformation(Properties.Resources.Emailsentsuccessfully);
                }
            }
            catch
            {
            }

        }

        public static bool EmailRepair(DevExpress.XtraReports.UI.XtraReport r, string emailattachpth, string pID, string cemail, string cnm)
        {
            Attachment attch = null;

            bool bl = false;
            try
            {
                string attachfile = "";
                attachfile = "Repair-" + pID + ".pdf";
                string fpth = Path.Combine(emailattachpth, attachfile);
                DevExpress.XtraPrinting.ExportOptions ex = new DevExpress.XtraPrinting.ExportOptions();
                ex.PrintPreview.SaveMode = DevExpress.XtraPrinting.SaveMode.UsingDefaultPath;
                ex.PrintPreview.ActionAfterExport = ActionAfterExport.None;

                var reportPath = fpth;
                var stream = new MemoryStream();
                r.ExportToPdf(stream);

                using (var fs = System.IO.File.Create(@reportPath))
                {
                    stream.WriteTo(fs);
                }
                stream.Close();

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                smtp.Host = Settings.REHost;
                smtp.Port = Settings.REPort;
                smtp.EnableSsl = (Settings.RESSL == "Y");
                smtp.Credentials = new System.Net.NetworkCredential(Settings.REUser, Settings.REPassword);

                MailMessage msg = new MailMessage();
                msg.To.Add(cemail);
                msg.From = new System.Net.Mail.MailAddress(Settings.REFromAddress, Settings.REFromName);
                if (Settings.REReplyTo != "") msg.ReplyToList.Add(Settings.REReplyTo);

                msg.Subject = Properties.Resources.EmailFrom + Settings.Company;
                msg.Body = cnm + "," + Environment.NewLine + Environment.NewLine +
                           Properties.Resources.PleaseFindAttachedRepairForm + Environment.NewLine + Environment.NewLine
                           + Settings.Company;

                foreach (string f in Directory.GetFiles(emailattachpth))
                {
                    FileInfo fi = new FileInfo(f);
                    attch = new Attachment(fi.FullName);
                    msg.Attachments.Add(attch);
                }

                smtp.Send(msg);
                bl = true;
            }
            catch
            {
                bl = false;
            }
            if (attch != null) attch.Dispose();
            return bl;
        }

        public static void PrintReport(DevExpress.XtraReports.UI.XtraReport r)
        {
            try
            {
                if ((Settings.ReportPrinterName != "") && (Settings.ReportPrinterName != Properties.Resources.None))
                    r.Print(Settings.ReportPrinterName);
                else
                    r.Print();
            }
            catch
            {
            }
        }


        #endregion

        #region Print Label

        public static async Task PrintItemLabel(OfflineRetailV2.UserControls.POSControl frmbrw, bool CallFromPOS)
        {
            int intRowID = -1;
            intRowID = frmbrw.PgridView1.FocusedRowHandle;
            if (intRowID == -1) return;
            if ((frmbrw.grdProduct.ItemsSource as DataTable).Rows.Count == 0) return;
            if (await GeneralFunctions.GetCellValue1(intRowID, frmbrw.grdProduct, frmbrw.colIMPrintLabel) == "N")
            {
                DocMessage.MsgInformation(Properties.Resources.Labelprintingsetupnotfoundagainstthisproduct + "\r\n" + Properties.Resources.PleaseCheckProductSetup);
                return;
            }
            int intQty = 0;
            intQty = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(intRowID, frmbrw.grdProduct, frmbrw.colIMQtyToPrint));
            if (!CallFromPOS)
            {
                if (intQty <= 0)
                {
                    intQty = 1;
                    //DocMessage.MsgInformation(Properties.Resources.Can not get the label printing quantity.\r\n Please Check Product Setup. ");
                    //return;
                }
            }
            else
            {
                intQty = 1;
            }

            OfflineRetailV2.UserControls.Administrator.frm_PrintBarcodeDlg frm_PrintBarcodeDlg = new OfflineRetailV2.UserControls.Administrator.frm_PrintBarcodeDlg();
            try
            {
                frm_PrintBarcodeDlg.isbatchprint = false;
                frm_PrintBarcodeDlg.SKU = await GeneralFunctions.GetCellValue1(intRowID, frmbrw.grdProduct, frmbrw.colIMSKU);
                frm_PrintBarcodeDlg.ProductDesc = await GeneralFunctions.GetCellValue1(intRowID, frmbrw.grdProduct, frmbrw.colIMDesc);
                frm_PrintBarcodeDlg.ProductDecimalPlace = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(intRowID, frmbrw.grdProduct, frmbrw.colIMDecimal));
                if (await GeneralFunctions.GetCellValue1(intRowID, frmbrw.grdProduct, frmbrw.colIMNoPriceLabel) == "Y")
                    frm_PrintBarcodeDlg.ProductPrice = "";
                else
                    frm_PrintBarcodeDlg.ProductPrice = (await GeneralFunctions.GetCellValue1(intRowID, frmbrw.grdProduct, frmbrw.colIMPrice)).Replace(SystemVariables.CurrencySymbol, "");

                frm_PrintBarcodeDlg.Qty = intQty;
                frm_PrintBarcodeDlg.LabelType = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(intRowID, frmbrw.grdProduct, frmbrw.colIMLabelType));
                frm_PrintBarcodeDlg.ShowDialog();
            }
            finally
            {
            }


        }

        public static async Task PrintItemLabel(OfflineRetailV2.UserControls.POSSection.frmProductBrwUC frmbrw, bool CallFromPOS)
        {

            int intRowID = -1;
            intRowID = frmbrw.gridView1.FocusedRowHandle;
            if (intRowID == -1) return;
            if ((frmbrw.gridControl1.ItemsSource as DataTable).Rows.Count == 0) return;
            if (await GeneralFunctions.GetCellValue1(intRowID, frmbrw.gridControl1, frmbrw.colPrintLabel) == "N")
            {
                DocMessage.MsgInformation(Properties.Resources.Labelprintingsetupnotfoundagainstthisproduct + "\r\n" + Properties.Resources.PleaseCheckProductSetup);
                return;
            }
            int intQty = 0;
            intQty = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(intRowID, frmbrw.gridControl1, frmbrw.colQtyToPrint));
            if (!CallFromPOS)
            {
                if (intQty <= 0)
                {
                    intQty = 1;
                    //DocMessage.MsgInformation(Properties.Resources.Can not get the label printing quantity.\r\n Please Check Product Setup. ");
                    //return;
                }
            }
            else
            {
                intQty = 1;
            }

            OfflineRetailV2.UserControls.Administrator.frm_PrintBarcodeDlg frm_PrintBarcodeDlg = new OfflineRetailV2.UserControls.Administrator.frm_PrintBarcodeDlg();
            try
            {
                frm_PrintBarcodeDlg.isbatchprint = false;
                frm_PrintBarcodeDlg.SKU = await GeneralFunctions.GetCellValue1(intRowID, frmbrw.gridControl1, frmbrw.colSKU);
                frm_PrintBarcodeDlg.ProductDesc = await GeneralFunctions.GetCellValue1(intRowID, frmbrw.gridControl1, frmbrw.colDesc);
                frm_PrintBarcodeDlg.ProductDecimalPlace = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(intRowID, frmbrw.gridControl1, frmbrw.colDecimal));
                if (await GeneralFunctions.GetCellValue1(intRowID, frmbrw.gridControl1, frmbrw.colNoPriceLabel) == "Y")
                    frm_PrintBarcodeDlg.ProductPrice = "";
                else
                    frm_PrintBarcodeDlg.ProductPrice = await GeneralFunctions.GetCellValue1(intRowID, frmbrw.gridControl1, frmbrw.colPrice);

                frm_PrintBarcodeDlg.Qty = intQty;
                frm_PrintBarcodeDlg.LabelType = GeneralFunctions.fnInt32(await GeneralFunctions.GetCellValue1(intRowID, frmbrw.gridControl1, frmbrw.colLabelType));
                frm_PrintBarcodeDlg.ShowDialog();
            }
            finally
            {
            }

        }

        #endregion

        // Get Lookup controls Column Value other than Display Field of Value Field
        public static void GetOtherColumnValuesOfLookup(LookUpEdit lk, string DisplayField, ref string val)
        {

            LookUpEdit dummylkup = new LookUpEdit();
            dummylkup.Properties.Columns.Assign(lk.Properties.Columns);
            dummylkup.Properties.ValueMember = lk.Properties.ValueMember;
            dummylkup.Properties.DisplayMember = DisplayField;
            dummylkup.Properties.NullText = "";
            dummylkup.Properties.DataSource = lk.Properties.DataSource as DataTable;
            dummylkup.Properties.ForceInitialize();

            if (lk.EditValue != null) dummylkup.EditValue = lk.EditValue.ToString();
            val = dummylkup.Text;
            dummylkup.Dispose();
        }

        // Allow only Integer in Text Edit Control

        public static void MaskIntegerTextEdit1(TextEdit edit, KeyPressEventArgs e)
        {
            if (edit != null)
            {
                if (edit.SelectionLength > 0)
                {
                    if ((Char.IsDigit(e.KeyChar) || e.KeyChar == 8))
                    {
                    }
                    else
                    {
                        e.Handled = true;
                    }
                    return;
                }
            }

            string strinput = edit.Text.ToString();

            if (strinput == "")
            {
                if (e.KeyChar == '0')
                {
                    e.Handled = true;
                    return;
                }
            }
            else
            {
                if ((edit.SelectionStart == 0) && (e.KeyChar == '0'))
                {
                    e.Handled = true;
                    return;
                }
            }

            int Decimals = 2;
            int inCaretPos = 0;
            int inDecimalPoint = strinput.IndexOf('.', 0, strinput.Length);
            if ((inDecimalPoint >= 0) && ((inDecimalPoint + Decimals) < strinput.Length)
                && (strinput != "0.00") && (e.KeyChar != 8) && (inCaretPos >
                (inDecimalPoint - 1)))
            {
                e.Handled = true;
                return;
            }
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                //Allow "." only once for decimal separator.
                if (e.KeyChar == '.')
                {
                    if (strinput.IndexOf('.', 0, strinput.Length) >= 0)
                    {
                        e.Handled = true;
                        return;
                    }
                }
                // Allow -ve only at the first position.
                if (e.KeyChar == '-')
                {
                    /*if (strinput.IndexOf('-', 0, strinput.Length) == 0)
                    {
                        e.Handled = true;
                        return;
                    }
                    else
                    {
                        strinput = "";
                    }*/
                    e.Handled = true;
                    return;

                }

                //allow only two decimal places
                int intPos = strinput.IndexOf('.', 0, strinput.Length);
                if ((intPos > 0))
                {
                    if (strinput.Length > intPos + 2)
                    {
                        if (Char.IsDigit(e.KeyChar))
                        {
                            e.Handled = true;
                            return;
                        }
                    }
                }
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

        }

        public static void MaskIntegerTextEdit(TextEdit edit, KeyPressEventArgs e)
        {
            if (edit != null)
            {
                if (edit.SelectionLength > 0)
                {
                    if ((Char.IsDigit(e.KeyChar) || e.KeyChar == 8))
                    {
                    }
                    else
                    {
                        e.Handled = true;
                    }
                    return;
                }
            }

            string strinput = edit.Text.ToString();



            int Decimals = 2;
            int inCaretPos = 0;
            int inDecimalPoint = strinput.IndexOf('.', 0, strinput.Length);
            if ((inDecimalPoint >= 0) && ((inDecimalPoint + Decimals) < strinput.Length)
                && (strinput != "0.00") && (e.KeyChar != 8) && (inCaretPos >
                (inDecimalPoint - 1)))
            {
                e.Handled = true;
                return;
            }
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                //Allow "." only once for decimal separator.
                if (e.KeyChar == '.')
                {
                    if (strinput.IndexOf('.', 0, strinput.Length) >= 0)
                    {
                        e.Handled = true;
                        return;
                    }
                }
                // Allow -ve only at the first position.
                if (e.KeyChar == '-')
                {
                    /*if (strinput.IndexOf('-', 0, strinput.Length) == 0)
                    {
                        e.Handled = true;
                        return;
                    }
                    else
                    {
                        strinput = "";
                    }*/
                    e.Handled = true;
                    return;

                }

                //allow only two decimal places
                int intPos = strinput.IndexOf('.', 0, strinput.Length);
                if ((intPos > 0))
                {
                    if (strinput.Length > intPos + 2)
                    {
                        if (Char.IsDigit(e.KeyChar))
                        {
                            e.Handled = true;
                            return;
                        }
                    }
                }
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

        }

        public static void MaskIntegerTextEdit(TextEdit edit, System.Windows.Input.KeyEventArgs e)
        {
            if (edit != null)
            {
                if (edit.SelectionLength > 0)
                {
                    if ((Char.IsDigit((char)System.Windows.Input.KeyInterop.VirtualKeyFromKey(e.Key)) ||
                        (char)System.Windows.Input.KeyInterop.VirtualKeyFromKey(e.Key) == 8))
                    {
                    }
                    else
                    {
                        e.Handled = true;
                    }
                    return;
                }
            }
            else
            {
                return;
            }

            string strinput = edit.Text.ToString();



            int Decimals = 2;
            int inCaretPos = 0;
            int inDecimalPoint = strinput.IndexOf('.', 0, strinput.Length);
            if ((inDecimalPoint >= 0) && ((inDecimalPoint + Decimals) < strinput.Length)
                && (strinput != "0.00") && ((char)System.Windows.Input.KeyInterop.VirtualKeyFromKey(e.Key) != 8) && (inCaretPos >
                (inDecimalPoint - 1)))
            {
                e.Handled = true;
                return;
            }
            if (Char.IsDigit((char)System.Windows.Input.KeyInterop.VirtualKeyFromKey(e.Key)) ||
                (char)System.Windows.Input.KeyInterop.VirtualKeyFromKey(e.Key) == 8)
            {
                //Allow "." only once for decimal separator.
                if ((char)System.Windows.Input.KeyInterop.VirtualKeyFromKey(e.Key) == '.')
                {
                    if (strinput.IndexOf('.', 0, strinput.Length) >= 0)
                    {
                        e.Handled = true;
                        return;
                    }
                }
                // Allow -ve only at the first position.
                if ((char)System.Windows.Input.KeyInterop.VirtualKeyFromKey(e.Key) == '-')
                {
                    /*if (strinput.IndexOf('-', 0, strinput.Length) == 0)
                    {
                        e.Handled = true;
                        return;
                    }
                    else
                    {
                        strinput = "";
                    }*/
                    e.Handled = true;
                    return;

                }

                //allow only two decimal places
                int intPos = strinput.IndexOf('.', 0, strinput.Length);
                if ((intPos > 0))
                {
                    if (strinput.Length > intPos + 2)
                    {
                        if (Char.IsDigit((char)System.Windows.Input.KeyInterop.VirtualKeyFromKey(e.Key)))
                        {
                            e.Handled = true;
                            return;
                        }
                    }
                }
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

        }

        public static void MaskDecimalTextEdit(TextEdit edit, KeyPressEventArgs e)
        {
            if (edit != null)
            {
                if (edit.SelectionLength > 0)
                {
                    if (Char.IsDigit(e.KeyChar) || e.KeyChar == 8 || e.KeyChar == 46)
                    {
                    }
                    else
                    {
                        e.Handled = true;
                    }
                    return;
                }
            }

            string strinput = edit.Text.ToString();
            int Decimals = 2;
            int inCaretPos = 0;
            int inDecimalPoint = strinput.IndexOf('.', 0, strinput.Length);
            if ((inDecimalPoint >= 0) && ((inDecimalPoint + Decimals) < strinput.Length)
                && (strinput != "0.00") && (e.KeyChar != 8) && (inCaretPos >
                (inDecimalPoint - 1)))
            {
                e.Handled = true;
                return;
            }
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == 8 || e.KeyChar == 46)
            {
                //Allow "." only once for decimal separator.
                if (e.KeyChar == '.')
                {
                    if (strinput.IndexOf('.', 0, strinput.Length) >= 0)
                    {
                        e.Handled = true;
                        return;
                    }
                }
                // Allow -ve only at the first position.
                if (e.KeyChar == '-')
                {
                    /*if (strinput.IndexOf('-', 0, strinput.Length) == 0)
                    {
                        e.Handled = true;
                        return;
                    }
                    else
                    {
                        strinput = "";
                    }*/
                    e.Handled = true;
                    return;

                }

                //allow only two decimal places
                int intPos = strinput.IndexOf('.', 0, strinput.Length);
                if ((intPos > 0))
                {
                    if (strinput.Length > intPos + 2)
                    {
                        if (Char.IsDigit(e.KeyChar))
                        {
                            e.Handled = true;
                            return;
                        }
                    }
                }
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

        }

        // Return specified length of characters from the end of a string

        public static string GetLast(string source, int tail_length)
        {
            if (tail_length >= source.Length)
            {
                return source;
            }
            else
            {
                var length = source.Length;
                var result = new String('X', length - tail_length) + source.Substring(length - tail_length);
                return result;
            }
        }

        #region Reporting / Printing Functions

        public static string GetReportFilePath(string repxFile)
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            string dirName = Path.GetDirectoryName(asm.Location);
            if (!File.Exists(Path.Combine(dirName + "\\Repair")))
                Directory.CreateDirectory(Path.Combine(dirName + "\\Repair"));
            return Path.Combine(dirName + "\\Repair", repxFile);
        }

        public static string GetReportPath(DevExpress.XtraReports.UI.XtraReport fReport, string ext)
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            string repName = fReport.Name;
            if (repName.Length == 0)
                repName = fReport.GetType().Name;
            string dirName = Path.GetDirectoryName(asm.Location);
            return Path.Combine(dirName + "\\Repair", repName + "." + ext);
        }

        protected static void ShowParameters(DevExpress.XtraReports.UI.XtraReport fReport)
        {
            if (fReport != null)
                fReport.PrintingSystem.ExecCommand(DevExpress.XtraPrinting.PrintingSystemCommand.Parameters, new object[] { true });
        }

        private static void ShowDesignerForm(Form designForm, Form parentForm)
        {
            //designForm.MinimumSize = parentForm.MinimumSize;
            //if (parentForm.WindowState == FormWindowState.Normal)
            //designForm.Bounds = parentForm.Bounds;
            //designForm.WindowState = parentForm.WindowState;
            parentForm.Visible = false;
            designForm.ShowDialog();
            parentForm.Visible = true;
        }

        public static void ReportDesigner(string repxFile, XtraReport repf, XtraForm thisForm)
        {
            XtraReport fReport = new XtraReport();

            if (File.Exists(GetReportFilePath(repxFile)))
                fReport = XtraReport.FromFile(GetReportFilePath(repxFile), true);
            else fReport = repf;

            string fileName = GetReportPath(fReport, "repx");
            string saveFileName = GetReportPath(fReport, "sav");
            if (!File.Exists(GetReportFilePath(repxFile)))
            {
                fReport.SaveLayout(fileName);
            }
            //fReport.PrintingSystem.ExecCommand(PrintingSystemCommand.StopPageBuilding);
            fReport.SaveLayout(saveFileName);
            using (XtraReport newReport = XtraReport.FromFile(saveFileName, true))
            {
                Translation.SetMultilingualTextInXtraReport(newReport);

                XRDesignFormExBase designForm = new CustomDesignForm();
                designForm.WindowState = FormWindowState.Maximized;
                designForm.OpenReport(newReport);
                designForm.FileName = fileName;
                ShowDesignerForm(designForm, thisForm.FindForm());
                if (designForm.FileName != fileName && File.Exists(designForm.FileName))
                    File.Copy(designForm.FileName, fileName, true);

                designForm.OpenReport((XtraReport)null);
                designForm.Dispose();
            }
            if (File.Exists(fileName))
            {
                fReport.LoadLayout(fileName);
            }

            //ShowParameters(fReport);
            File.Delete(saveFileName);
        }





        public static string GetLabelPrintingFilePath(string repxFile)
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            string dirName = Path.GetDirectoryName(asm.Location);
            if (!File.Exists(Path.Combine(dirName + "\\LabelPrinting")))
                Directory.CreateDirectory(Path.Combine(dirName + "\\LabelPrinting"));
            return Path.Combine(dirName + "\\LabelPrinting", repxFile);
        }

        public static string GetLabelPrintingPath(DevExpress.XtraReports.UI.XtraReport fReport, string ext)
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            string repName = fReport.Name;
            if (repName.Length == 0)
                repName = fReport.GetType().Name;
            string dirName = Path.GetDirectoryName(asm.Location);
            return Path.Combine(dirName + "\\LabelPrinting", repName + "." + ext);
        }


        public static void ReportDesignerForLabelPrinting(string repxFile, XtraReport repf, XtraForm thisForm)
        {
            XtraReport fReport = new XtraReport();

            if (File.Exists(GetLabelPrintingFilePath(repxFile)))
                fReport = XtraReport.FromFile(GetLabelPrintingFilePath(repxFile), true);
            else fReport = repf;

            string fileName = GetLabelPrintingPath(fReport, "repx");
            string saveFileName = GetLabelPrintingPath(fReport, "sav");
            if (!File.Exists(GetLabelPrintingFilePath(repxFile)))
            {
                fReport.SaveLayout(fileName);
            }
            //fReport.PrintingSystem.ExecCommand(PrintingSystemCommand.StopPageBuilding);
            fReport.SaveLayout(saveFileName);
            using (XtraReport newReport = XtraReport.FromFile(saveFileName, true))
            {
                Translation.SetMultilingualTextInXtraReport(newReport);

                XRDesignFormExBase designForm = new CustomDesignForm();
                designForm.WindowState = FormWindowState.Maximized;
                designForm.OpenReport(newReport);
                designForm.FileName = fileName;
                ShowDesignerForm(designForm, thisForm.FindForm());
                if (designForm.FileName != fileName && File.Exists(designForm.FileName))
                    File.Copy(designForm.FileName, fileName, true);

                designForm.OpenReport((XtraReport)null);
                designForm.Dispose();
            }
            if (File.Exists(fileName))
            {
                fReport.LoadLayout(fileName);
            }

            //ShowParameters(fReport);
            File.Delete(saveFileName);
        }



        #endregion

        #region Scale Comm

        public static string IsScaleCommPathExists()
        {
            string ret = "";

            string exepath = Path.GetPathRoot(Environment.SystemDirectory);
            if (!exepath.EndsWith("\\"))
            {
                exepath = exepath + "\\";
            }
            exepath = exepath + "Program Files\\" + SystemVariables.BrandName + "\\ScaleCom\\GWARE SCALE COMM.exe";

            if (File.Exists(exepath))
            {
                ret = exepath;
            }
            return ret;
        }

        public static void Call_SendToScales_Routine()
        {
            string path = "";
            path = IsScaleCommPathExists();
            if (path != "")
            {
                try
                {
                    /*DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(wait_scale));
                    System.Threading.Thread.Sleep(4000);
                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm();*/
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                    startInfo.FileName = path;
                    startInfo.Arguments = "SEND";
                    process.StartInfo = startInfo;
                    process.Start();
                }
                catch
                {
                }
            }
            else
            {
                //DocMessage.MsgInformation(Properties.Resources.Scale Communication not installed");
            }
        }

        #endregion

        // Check Modules that has been registered

        public static string RegisteredModules()
        {
            if ((Settings.RegPOSAccess == "Y") && (Settings.RegScaleAccess == "N") && (Settings.RegOrderingAccess == "N"))
            {
                return "POS";
            }
            else if ((Settings.RegPOSAccess == "N") && (Settings.RegScaleAccess == "Y") && (Settings.RegOrderingAccess == "N"))
            {
                return "SCALE";
            }
            else if ((Settings.RegPOSAccess == "N") && (Settings.RegScaleAccess == "N") && (Settings.RegOrderingAccess == "Y"))
            {
                return "ORDERING";
            }
            else if ((Settings.RegPOSAccess == "Y") && (Settings.RegScaleAccess == "Y") && (Settings.RegOrderingAccess == "N"))
            {
                return "POS, SCALE";
            }
            else if ((Settings.RegPOSAccess == "Y") && (Settings.RegScaleAccess == "N") && (Settings.RegOrderingAccess == "Y"))
            {
                return "POS, ORDERING";
            }
            else if ((Settings.RegPOSAccess == "N") && (Settings.RegScaleAccess == "Y") && (Settings.RegOrderingAccess == "Y"))
            {
                return "SCALE, ORDERING";
            }
            else
            {
                return "POS, SCALE, ORDERING";
            }
        }

        #region Scale Label Print

        public static string GetScaleLabelPath(string repxFile)
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            string dirName = Path.GetDirectoryName(asm.Location);
            if (!File.Exists(Path.Combine(dirName + "\\Default Labels")))
                Directory.CreateDirectory(Path.Combine(dirName + "\\Default Labels"));
            return Path.Combine(dirName + "\\Default Labels", repxFile);
        }

        public static string GetScaleLabelPath2(DevExpress.XtraReports.UI.XtraReport fReport, string ext)
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            string repName = fReport.Name;
            if (repName.Length == 0)
                repName = fReport.GetType().Name;
            string dirName = Path.GetDirectoryName(asm.Location);
            return Path.Combine(dirName + "\\Default Labels", repName + "." + ext);
        }

        public static string GetScaleLabelPath_Specific(string repxFile)
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            string dirName = Path.GetDirectoryName(asm.Location);
            if (!File.Exists(Path.Combine(dirName + "\\Default Labels\\Temp")))
                Directory.CreateDirectory(Path.Combine(dirName + "\\Default Labels\\Temp"));
            return Path.Combine(dirName + "\\Default Labels\\Temp", repxFile);
        }

        protected static void ShowScaleLabelParameters(DevExpress.XtraReports.UI.XtraReport fReport)
        {
            if (fReport != null)
                fReport.PrintingSystem.ExecCommand(DevExpress.XtraPrinting.PrintingSystemCommand.Parameters, new object[] { true });
        }

        private static void ShowDesignerFormForScaleLabel(Form designForm, Form parentForm)
        {
            //designForm.MinimumSize = parentForm.MinimumSize;
            //if (parentForm.WindowState == FormWindowState.Normal)
            //designForm.Bounds = parentForm.Bounds;
            //designForm.WindowState = parentForm.WindowState;
            parentForm.Visible = false;
            designForm.ShowDialog();
            parentForm.Visible = true;
        }




        public static void DefaultLabelDesigner(string repxFile, XtraReport repf, XtraForm thisForm)
        {
            XtraReport fReport = new XtraReport();

            if (File.Exists(GetScaleLabelPath(repxFile)))
                fReport = XtraReport.FromFile(GetScaleLabelPath(repxFile), true);
            else fReport = repf;

            string fileName = GetScaleLabelPath2(fReport, "repx");
            string saveFileName = GetScaleLabelPath2(fReport, "sav");
            if (!File.Exists(GetScaleLabelPath(repxFile)))
            {
                fReport.SaveLayout(fileName);
            }
            //fReport.PrintingSystem.ExecCommand(PrintingSystemCommand.StopPageBuilding);
            fReport.SaveLayout(saveFileName);
            using (XtraReport newReport = XtraReport.FromFile(saveFileName, true))
            {
                XRDesignFormExBase designForm = new CustomDesignForm();
                designForm.WindowState = FormWindowState.Maximized;
                designForm.OpenReport(newReport);
                designForm.FileName = fileName;
                ShowDesignerFormForScaleLabel(designForm, thisForm.FindForm());
                if (designForm.FileName != fileName && File.Exists(designForm.FileName))
                    File.Copy(designForm.FileName, fileName, true);

                designForm.OpenReport((XtraReport)null);
                designForm.Dispose();
            }
            if (File.Exists(fileName))
            {
                fReport.LoadLayout(fileName);
            }

            //ShowParameters(fReport);
            File.Delete(saveFileName);
        }

        public static void SpecificLabelDesigner(string repxFile, XtraReport repf, XtraForm thisForm, int RefID)
        {

            string DefaultPath = GetScaleLabelPath(repxFile);
            string TempPath = GetScaleLabelPath_Specific(repxFile);

            PosDataObject.Scales objs1 = new PosDataObject.Scales();
            objs1.Connection = SystemVariables.Conn;
            int cnt = objs1.IsExistLabelLayout(RefID);

            XtraReport fReport = new XtraReport();

            XtraReport newReport = new XtraReport();

            string fileName = "";

            File.Copy(DefaultPath, TempPath, true);
            fReport = XtraReport.FromFile(GetScaleLabelPath_Specific(repxFile), true);
            fileName = GetScaleLabelPath_Specific(repxFile);

            if (cnt == 0)
            {
                newReport = XtraReport.FromFile(fileName, true);
            }
            else
            {
                PosDataObject.Scales objs2 = new PosDataObject.Scales();
                objs2.Connection = SystemVariables.Conn;

                string s1 = objs2.GetLabelLayout(RefID);
                string s2 = @"<?xml version=""1.0"" encoding=""utf-8""?>" + s1;

                using (StreamWriter sw = new StreamWriter(new MemoryStream()))
                {
                    sw.Write(s2);
                    sw.Flush();
                    newReport = XtraReport.FromStream(sw.BaseStream, true);
                }

            }



            using (newReport)
            {
                XRDesignFormExBase designForm = new CustomDesignForm();
                designForm.WindowState = FormWindowState.Maximized;
                designForm.OpenReport(newReport);
                designForm.FileName = fileName;
                designForm.ShowDialog();
                ShowDesignerFormForScaleLabel(designForm, thisForm.FindForm());
                designForm.SaveReport(fileName);
                designForm.OpenReport((XtraReport)null);
                designForm.Dispose();
            }

            fReport.LoadLayout(fileName);

            MemoryStream stream = new MemoryStream();
            fReport.SaveLayoutToXml(stream);
            stream.Position = 0;

            using (StreamReader sr = new StreamReader(stream))
            {
                string s = sr.ReadToEnd().Replace("utf-8", "utf-16");

                if (cnt == 0)
                {
                    PosDataObject.Scales objs3 = new PosDataObject.Scales();
                    objs3.Connection = SystemVariables.Conn;
                    objs3.InsertLabelLayout(RefID, s);
                }
                else
                {
                    PosDataObject.Scales objs3 = new PosDataObject.Scales();
                    objs3.Connection = SystemVariables.Conn;
                    objs3.UpdateLabelLayout(RefID, s);
                }
            }
        }






        public static string LabelFormatPath()
        {

            string csConnPath = "";
            string strdirpath = "";

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var directory = new DirectoryInfo(documentsPath);
            csConnPath = directory.Parent.FullName;

            if (csConnPath.EndsWith("\\")) strdirpath = csConnPath + SystemVariables.BrandName + "\\Label";
            else strdirpath = csConnPath + "\\" + SystemVariables.BrandName + "\\Label";


            if (Directory.Exists(strdirpath))
            {
                return strdirpath;
            }
            else
            {
                Directory.CreateDirectory(strdirpath);
                return strdirpath;
            }
        }

        public static string CustomiseLabelFormatLayout(XtraReport refReport, string repxFile, int RefID)
        {
            string xml = "";
            string fileName = LabelFormatPath() + "\\" + repxFile;
            XtraReport fReport = new XtraReport();
            fReport = refReport;
            fReport.SaveLayout(fileName);

            XtraReport newReport = new XtraReport();

            PosDataObject.Scales objs2 = new PosDataObject.Scales();
            objs2.Connection = SystemVariables.Conn;
            string s1 = objs2.GetLabelFormatLayout(RefID);
            string s2 = @"<?xml version=""1.0"" encoding=""utf-8""?>" + s1;

            using (StreamWriter sw = new StreamWriter(new MemoryStream()))
            {
                sw.Write(s2);
                sw.Flush();
                newReport = XtraReport.FromStream(sw.BaseStream, true);
            }

            using (newReport)
            {
                XRDesignFormExBase designForm = new CustomDesignForm();
                designForm.WindowState = FormWindowState.Maximized;
                designForm.OpenReport(newReport);
                designForm.FileName = fileName;
                designForm.ShowDialog();
                designForm.SaveReport(fileName);
                designForm.OpenReport((XtraReport)null);
                designForm.Dispose();
            }

            fReport.LoadLayout(fileName);

            MemoryStream stream = new MemoryStream();
            fReport.SaveLayoutToXml(stream);
            stream.Position = 0;

            using (StreamReader sr = new StreamReader(stream))
            {
                xml = sr.ReadToEnd().Replace("utf-8", "utf-16");
            }

            File.Delete(fileName);
            return xml;
        }

        public static XtraReport GetLabelReport(int val)
        {
            XtraReport tReport = new XtraReport();
            if (val == 1) tReport = new OfflineRetailV2.Report.Scale.LabelTemplate();
            return tReport;
        }

        public static string GetLabelReport_repx(int val)
        {
            string pval = "";
            if (val == 1) pval = "repDefaultLabel1.repx";
            return pval;
        }

        public static void CopyDefaultLabelToCustomisedFolder(string sourcepath, string destpath)
        {
            DirectoryInfo dir = new DirectoryInfo(LabelFormatPath());
            foreach (FileInfo file in dir.GetFiles())
            {
                file.Delete();
            }

            File.Copy(sourcepath, destpath, true);
        }

        public static void DeleteFileForLabelFormatTransaction()
        {
            DirectoryInfo dir = new DirectoryInfo(LabelFormatPath());
            foreach (FileInfo file in dir.GetFiles())
            {
                file.Delete();
            }
        }

        /// <summary>
        /// Devexpress 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="returnPath"></param>
        public static void CustomiseLabelFormatLayout(string fileName, ref string returnPath)
        {
            XtraReport newReport = XtraReport.FromFile(fileName, true);

            using (newReport)
            {
                XRDesignFormExBase designForm = new CustomDesignForm();
                designForm.WindowState = FormWindowState.Maximized;
                designForm.OpenReport(newReport);
                designForm.FileName = fileName;

                designForm.ShowDialog();
                designForm.SaveReport(fileName);
                returnPath = designForm.FileName;
                designForm.OpenReport((XtraReport)null);
                designForm.Dispose();
            }
        }


        public static void CustomiseLabelFormatLayoutForLabelDotNet(string fileName, ref string returnPath)
        {
            alabel1 slabel = new alabel1();
            string x = fileName;
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream1 = File.Open(x, FileMode.Open, FileAccess.Read);
            slabel = (alabel.alabel1)formatter.Deserialize(stream1);
            slabel.ShowLabelDesigner();
            stream1.Close();
        }


        public static string DefaultLabelPath()
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            string dirName = Path.GetDirectoryName(asm.Location);
            if (!File.Exists(Path.Combine(dirName + "\\Default Labels")))
                Directory.CreateDirectory(Path.Combine(dirName + "\\Default Labels"));
            return dirName + "\\Default Labels";
        }


        public static string LabelPath()
        {
            string csConnPath = "";
            string strdirpath = "";

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var directory = new DirectoryInfo(documentsPath);
            csConnPath = directory.Parent.FullName;

            if (csConnPath.EndsWith("\\")) strdirpath = csConnPath + SystemVariables.BrandName + "\\Label";
            else strdirpath = csConnPath + "\\" + SystemVariables.BrandName + "\\Label";


            if (Directory.Exists(strdirpath))
            {
                return strdirpath;
            }
            else
            {
                Directory.CreateDirectory(strdirpath);
                return strdirpath;
            }
        }

        public static string LabelTemplatePath()
        {
            string csConnPath = "";
            string strdirpath = "";

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var directory = new DirectoryInfo(documentsPath);
            csConnPath = directory.Parent.FullName;

            if (csConnPath.EndsWith("\\")) strdirpath = csConnPath + SystemVariables.BrandName + "\\Label\\Template";
            else strdirpath = csConnPath + "\\" + SystemVariables.BrandName + "\\Label\\Template";


            if (Directory.Exists(strdirpath))
            {
                return strdirpath;
            }
            else
            {
                Directory.CreateDirectory(strdirpath);
                return strdirpath;
            }
        }

        public static string LabelTemplatePathWithType(string lbltype)
        {
            string csConnPath = "";
            string strdirpath = "";

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var directory = new DirectoryInfo(documentsPath);
            csConnPath = directory.Parent.FullName;

            if (csConnPath.EndsWith("\\")) strdirpath = csConnPath + SystemVariables.BrandName + "\\Label\\Template\\" + lbltype;
            else strdirpath = csConnPath + "\\" + SystemVariables.BrandName + "\\Label\\Template\\" + lbltype;


            if (Directory.Exists(strdirpath))
            {
                return strdirpath;
            }
            else
            {
                Directory.CreateDirectory(strdirpath);
                return strdirpath;
            }
        }

        public static string LabelTemplatePath(string labeltype)
        {
            string csConnPath = "";
            string strdirpath = "";

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var directory = new DirectoryInfo(documentsPath);
            csConnPath = directory.Parent.FullName;

            if (csConnPath.EndsWith("\\")) strdirpath = csConnPath + SystemVariables.BrandName + "\\Label\\Template\\" + labeltype;
            else strdirpath = csConnPath + "\\" + SystemVariables.BrandName + "\\Label\\Template\\" + labeltype;


            if (Directory.Exists(strdirpath))
            {
                return strdirpath;
            }
            else
            {
                Directory.CreateDirectory(strdirpath);
                return strdirpath;
            }
        }


        public static void SetInitialLabelTemplateFromApplication(string repx)
        {
            string fileName = LabelTemplatePath() + "\\" + repx;

            XtraReport fReport = new OfflineRetailV2.Report.Scale.LabelTemplate();

            fReport.SaveLayout(fileName);
        }

        public static void CopyLabelTemplate(string oldfile, string newfile)
        {
            string oldpath = LabelTemplatePath() + "\\" + oldfile;
            string newpath = LabelTemplatePath() + "\\" + newfile;
            try
            {
                File.Copy(oldpath, newpath, true);
            }
            catch
            {
            }
        }

        public static void SetNewLabelTemplate(string labeltype, string oldfile, string newfile)
        {
            string oldpath = LabelTemplatePath(labeltype) + "\\" + oldfile;
            string newpath = LabelPath() + "\\" + newfile;
            try
            {
                File.Copy(oldpath, newpath, true);
            }
            catch
            {
            }
        }

        public static void SetNewLabelTemplate(string oldfile, string newfile)
        {
            string oldpath = LabelTemplatePath() + "\\" + oldfile;
            string newpath = LabelPath() + "\\" + newfile;
            try
            {
                File.Copy(oldpath, newpath, true);
            }
            catch
            {
            }
        }

        public static bool SaveNewLabelTemplate(string oldfile, string newfile, string labeltype)
        {
            string newpath = LabelTemplatePath(labeltype) + "\\" + newfile;
            string oldpath = LabelPath() + "\\" + oldfile;
            try
            {
                File.Move(oldpath, newpath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool SaveLabelTemplate(string oldfile, string newfile, string oldlbltype, string newlbltype)
        {
            string oldpath = LabelTemplatePath(oldlbltype) + "\\" + oldfile;
            string newpath = LabelTemplatePath(newlbltype) + "\\" + newfile;
            try
            {
                File.Move(oldpath, newpath);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public static bool SaveLabelTemplateToDataBase(string FilePath, string TemplateName, string SaveType)
        {
            bool blReturn = false;
            byte[] file;
            using (var stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(stream))
                {
                    file = reader.ReadBytes((int)stream.Length);
                }
            }

            string strSQL = "";

            if (SaveType == "Add") strSQL = "insert into LabelTemplate(TemplateName,TemplateContent) values(@TemplateName,@TemplateContent) ";
            if (SaveType == "Edit") strSQL = "update LabelTemplate set TemplateContent = @TemplateContent where TemplateName = @TemplateName ";

            SqlConnection objConnection = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objCommand = new SqlCommand(strSQL, objConnection);

            objCommand.Parameters.Add(new SqlParameter("@TemplateName", System.Data.SqlDbType.VarChar));
            objCommand.Parameters.Add(new SqlParameter("@TemplateContent", System.Data.SqlDbType.VarBinary));

            objCommand.Parameters["@TemplateName"].Value = TemplateName;
            objCommand.Parameters["@TemplateContent"].Value = file;

            try
            {

                if (objConnection.State == System.Data.ConnectionState.Open) { objConnection.Close(); }
                objConnection.Open();
                objCommand.ExecuteNonQuery();
                blReturn = true;
            }
            catch
            {
                blReturn = false;
            }
            finally
            {

            }
            return blReturn;
        }


        public static void LabelFormatToFile(int RefID)
        {

            string varPathToNewLocation = LabelFormatPath() + "\\Template.dat";

            using (var varConnection = new SqlConnection(SystemVariables.ConnectionString))

            using (var sqlQuery = new SqlCommand(@"select LabelFormat from LabelFormats where FormatID = @ID ", varConnection))
            {
                sqlQuery.Parameters.AddWithValue("@ID", RefID);

                if (varConnection.State == System.Data.ConnectionState.Open) { varConnection.Close(); }
                varConnection.Open();
                using (var sqlQueryResult = sqlQuery.ExecuteReader())
                    if (sqlQueryResult != null)
                    {
                        sqlQueryResult.Read();
                        var blob = new Byte[(sqlQueryResult.GetBytes(0, 0, null, 0, int.MaxValue))];
                        sqlQueryResult.GetBytes(0, 0, blob, 0, blob.Length);
                        using (var fs = new FileStream(varPathToNewLocation, FileMode.Create, FileAccess.Write))
                            fs.Write(blob, 0, blob.Length);
                    }
                varConnection.Close();
                varConnection.Dispose();
            }
        }

        public static void CopyDefaultLabelInTemplateFolder()
        {
            try
            {
                string TemplatePath = LabelTemplatePath();
                string DefaultPath = DefaultLabelPath();

                foreach (string f in Directory.EnumerateFiles(DefaultPath))
                {

                    FileInfo fi = new FileInfo(f);
                    string FileName = fi.Name;
                    if (!File.Exists(TemplatePath + "\\" + FileName))
                    {
                        File.Copy(fi.FullName, TemplatePath + "\\" + FileName, false);
                    }
                    else
                    {
                        DateTime compareWriteTime1 = fi.LastWriteTime;
                        FileInfo fiuser = new FileInfo(TemplatePath + "\\" + FileName);
                        DateTime compareWriteTime2 = fiuser.LastWriteTime;

                        if (compareWriteTime1 != compareWriteTime2)
                        {
                            File.Copy(fi.FullName, TemplatePath + "\\" + FileName, true);
                        }
                    }
                }

                DirectoryInfo directory = new DirectoryInfo(DefaultPath);
                DirectoryInfo[] directories = directory.GetDirectories();

                foreach (DirectoryInfo folder in directories)
                {
                    string foldername = folder.Name;
                    string folderpath = TemplatePath + "\\" + foldername;
                    if (!Directory.Exists(folderpath))
                    {
                        Directory.CreateDirectory(folderpath);
                    }

                    foreach (FileInfo f in folder.GetFiles())
                    {
                        if (!File.Exists(folderpath + "\\" + f.Name))
                        {
                            File.Copy(f.FullName, folderpath + "\\" + f.Name, false);
                        }
                        else
                        {
                            DateTime compareWriteTime1 = f.LastWriteTime;
                            FileInfo fiuser = new FileInfo(folderpath + "\\" + f.Name);
                            DateTime compareWriteTime2 = fiuser.LastWriteTime;

                            if (compareWriteTime1 != compareWriteTime2)
                            {
                                File.Copy(f.FullName, folderpath + "\\" + f.Name, true);
                            }
                        }
                    }
                }

            }
            catch
            {
            }
        }


        #endregion

        #region Scale Button Height

        public static bool IfChangeScaleButtonHeight()
        {
            if (Screen.PrimaryScreen.Bounds.Width <= Settings.CutoffScreenResolutionForScale) return false;
            else return true;
        }

        #endregion

        #region Datacap

        public static string Datacap_CreditSale_Request_XML(double amt, int invno, bool forcedmanual)
        {
            XmlDocument XDoc = new XmlDocument();

            XmlElement XElemRoot = XDoc.CreateElement("TStream");
            XDoc.AppendChild(XElemRoot);

            XmlElement XElem = XDoc.CreateElement("Transaction");
            XElemRoot.AppendChild(XElem);

            XmlElement XTemp = XDoc.CreateElement("MerchantID");
            XTemp.InnerText = Settings.DatacapMID;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranType");
            XTemp.InnerText = "Credit";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranCode");
            XTemp.InnerText = "Sale";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("SecureDevice");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = (!forcedmanual) ? Settings.DatacapSecureDeviceID : "NONE";
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "NONE";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("ComPort");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = (!forcedmanual) ? Settings.DatacapCOMPort : "1";
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "1";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("InvoiceNo");
            XTemp.InnerText = invno.ToString();
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("RefNo");
            XTemp.InnerText = invno.ToString();
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("PartialAuth");
            XTemp.InnerText = "Allow";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("AcctNo");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = (!forcedmanual) ? "SecureDevice" : "Prompt";
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "Prompt";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("Purchase");
            XTemp.InnerText = amt.ToString("f2");
            XElem.AppendChild(XTemp);

            return XDoc.OuterXml;
        }

        public static string Datacap_GetSignature_Request_XML(bool forcedmanual)
        {
            XmlDocument XDoc = new XmlDocument();


            XmlElement XElemRoot = XDoc.CreateElement("TStream");
            XDoc.AppendChild(XElemRoot);

            XmlElement XElem = XDoc.CreateElement("Transaction");
            XElemRoot.AppendChild(XElem);

            XmlElement XTemp = XDoc.CreateElement("MerchantID");
            XTemp.InnerText = Settings.DatacapMID;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("OperatorID");
            XTemp.InnerText = "1";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("ComPort");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = (!forcedmanual) ? Settings.DatacapSignatureDeviceCOMPort : "1";
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "1";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranCode");
            XTemp.InnerText = "GetSignature";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("SecureDevice");
            XTemp.InnerText = Settings.DatacapSignatureDevice;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("AcctNo");
            XTemp.InnerText = "SecureDevice";
            XElem.AppendChild(XTemp);

            return XDoc.OuterXml;
        }

        public static string Datacap_CreditSale_Request_XML(double amt, int invno, string signdata, bool forcedmanual)
        {
            XmlDocument XDoc = new XmlDocument();

            XmlElement XElemRoot = XDoc.CreateElement("TStream");
            XDoc.AppendChild(XElemRoot);

            XmlElement XElem = XDoc.CreateElement("Transaction");
            XElemRoot.AppendChild(XElem);

            XmlElement XTemp = XDoc.CreateElement("MerchantID");
            XTemp.InnerText = Settings.DatacapMID;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranType");
            XTemp.InnerText = "Credit";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranCode");
            XTemp.InnerText = "Sale";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("SecureDevice");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = (!forcedmanual) ? Settings.DatacapSecureDeviceID : "NONE";
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "NONE";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("ComPort");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = (!forcedmanual) ? Settings.DatacapCOMPort : "1";
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "1";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("InvoiceNo");
            XTemp.InnerText = invno.ToString();
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("RefNo");
            XTemp.InnerText = invno.ToString();
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("PartialAuth");
            XTemp.InnerText = "Allow";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("AcctNo");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = (!forcedmanual) ? "SecureDevice" : "Prompt";
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "Prompt";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("Purchase");
            XTemp.InnerText = amt.ToString("f2");
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("Signature");
            XTemp.InnerText = signdata;
            XElem.AppendChild(XTemp);

            return XDoc.OuterXml;
        }

        public static string Datacap_DebitSale_Request_XML(double amt, int invno, double cbk)
        {
            XmlDocument XDoc = new XmlDocument();

            XmlElement XElemRoot = XDoc.CreateElement("TStream");
            XDoc.AppendChild(XElemRoot);

            XmlElement XElem = XDoc.CreateElement("Transaction");
            XElemRoot.AppendChild(XElem);

            XmlElement XTemp = XDoc.CreateElement("MerchantID");
            XTemp.InnerText = Settings.DatacapMID;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranType");
            XTemp.InnerText = "Debit";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranCode");
            XTemp.InnerText = "Sale";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("SecureDevice");
            XTemp.InnerText = Settings.DatacapSecureDeviceID;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("ComPort");
            XTemp.InnerText = Settings.DatacapCOMPort;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("InvoiceNo");
            XTemp.InnerText = invno.ToString();
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("RefNo");
            XTemp.InnerText = invno.ToString();
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("AcctNo");
            XTemp.InnerText = "SecureDevice";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("Purchase");
            XTemp.InnerText = amt.ToString("f2");
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("CashBack");
            XTemp.InnerText = cbk.ToString("f2");
            XElem.AppendChild(XTemp);

            return XDoc.OuterXml;
        }

        public static string Datacap_PrePaidSale_Request_XML(double amt, int invno)
        {
            XmlDocument XDoc = new XmlDocument();

            XmlElement XElemRoot = XDoc.CreateElement("TStream");
            XDoc.AppendChild(XElemRoot);

            XmlElement XElem = XDoc.CreateElement("Transaction");
            XElemRoot.AppendChild(XElem);

            XmlElement XTemp = XDoc.CreateElement("IpPort");
            XTemp.InnerText = "9100";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("MerchantID");
            XTemp.InnerText = Settings.DatacapMID;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranType");
            XTemp.InnerText = "PrePaid";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranCode");
            XTemp.InnerText = "Sale";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("SecureDevice");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = Settings.DatacapSecureDeviceID;
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "NONE";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("ComPort");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = Settings.DatacapCOMPort;
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "1";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("InvoiceNo");
            XTemp.InnerText = invno.ToString();
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("RefNo");
            XTemp.InnerText = invno.ToString();
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("AcctNo");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = "SecureDevice";
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "Prompt";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("Purchase");
            XTemp.InnerText = amt.ToString("f2");
            XElem.AppendChild(XTemp);

            return XDoc.OuterXml;
        }

        public static string Datacap_EBTCashSale_Request_XML(double amt, int invno, double cbk, bool forcedmanual)
        {
            XmlDocument XDoc = new XmlDocument();


            XmlElement XElemRoot = XDoc.CreateElement("TStream");
            XDoc.AppendChild(XElemRoot);

            XmlElement XElem = XDoc.CreateElement("Transaction");
            XElemRoot.AppendChild(XElem);

            XmlElement XTemp = XDoc.CreateElement("MerchantID");
            XTemp.InnerText = Settings.DatacapMID;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranType");
            XTemp.InnerText = "EBT";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("CardType");
            XTemp.InnerText = "Cash";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranCode");
            XTemp.InnerText = "Sale";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("SecureDevice");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = (!forcedmanual) ? Settings.DatacapSecureDeviceID : "NONE";
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "NONE";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("ComPort");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = (!forcedmanual) ? Settings.DatacapCOMPort : "1";
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "1";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("InvoiceNo");
            XTemp.InnerText = invno.ToString();
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("RefNo");
            XTemp.InnerText = invno.ToString();
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("AcctNo");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = (!forcedmanual) ? "SecureDevice" : "Prompt";
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "Prompt";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("Purchase");
            XTemp.InnerText = amt.ToString("f2");
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("CashBack");
            XTemp.InnerText = cbk.ToString("f2");
            XElem.AppendChild(XTemp);

            return XDoc.OuterXml;
        }

        public static string Datacap_EBTSale_Request_XML(double amt, int invno, bool forcedmanual)
        {
            XmlDocument XDoc = new XmlDocument();


            XmlElement XElemRoot = XDoc.CreateElement("TStream");
            XDoc.AppendChild(XElemRoot);

            XmlElement XElem = XDoc.CreateElement("Transaction");
            XElemRoot.AppendChild(XElem);

            XmlElement XTemp = XDoc.CreateElement("MerchantID");
            XTemp.InnerText = Settings.DatacapMID;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranType");
            XTemp.InnerText = "EBT";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("CardType");
            XTemp.InnerText = "Foodstamp";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranCode");
            XTemp.InnerText = "Sale";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("SecureDevice");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = (!forcedmanual) ? Settings.DatacapSecureDeviceID : "NONE";
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "NONE";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("ComPort");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = (!forcedmanual) ? Settings.DatacapCOMPort : "1";
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "1";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("InvoiceNo");
            XTemp.InnerText = invno.ToString();
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("RefNo");
            XTemp.InnerText = invno.ToString();
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("AcctNo");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = (!forcedmanual) ? "SecureDevice" : "Prompt";
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "Prompt";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("Purchase");
            XTemp.InnerText = amt.ToString("f2");
            XElem.AppendChild(XTemp);

            return XDoc.OuterXml;
        }

        public static string Datacap_CreditVoidSale_Request_XML(double amt, string invno, string resp_ref, string resp_athcode, string resp_acqref, bool forcedmanual)
        {
            XmlDocument XDoc = new XmlDocument();


            XmlElement XElemRoot = XDoc.CreateElement("TStream");
            XDoc.AppendChild(XElemRoot);

            XmlElement XElem = XDoc.CreateElement("Transaction");
            XElemRoot.AppendChild(XElem);

            XmlElement XTemp = XDoc.CreateElement("MerchantID");
            XTemp.InnerText = Settings.DatacapMID;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranType");
            XTemp.InnerText = "Credit";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranCode");
            XTemp.InnerText = "VoidSale";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("SecureDevice");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = (!forcedmanual) ? Settings.DatacapSecureDeviceID : "NONE";
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "NONE";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("ComPort");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = (!forcedmanual) ? Settings.DatacapCOMPort : "1";
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "1";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("InvoiceNo");
            XTemp.InnerText = invno;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("RefNo");
            XTemp.InnerText = resp_ref;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("AcctNo");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = (!forcedmanual) ? "SecureDevice" : "Prompt";
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "Prompt";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("Purchase");
            XTemp.InnerText = amt.ToString("f2");
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("AuthCode");
            XTemp.InnerText = resp_athcode;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("AcqRefData");
            XTemp.InnerText = resp_acqref;
            XElem.AppendChild(XTemp);

            return XDoc.OuterXml;
        }

        public static string Datacap_DebitVoidSale_Request_XML(double amt, string invno, string resp_ref, double csbk)
        {
            XmlDocument XDoc = new XmlDocument();


            XmlElement XElemRoot = XDoc.CreateElement("TStream");
            XDoc.AppendChild(XElemRoot);

            XmlElement XElem = XDoc.CreateElement("Transaction");
            XElemRoot.AppendChild(XElem);

            XmlElement XTemp = XDoc.CreateElement("MerchantID");
            XTemp.InnerText = Settings.DatacapMID;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranType");
            XTemp.InnerText = "Debit";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranCode");
            XTemp.InnerText = "VoidSale";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("SecureDevice");
            XTemp.InnerText = Settings.DatacapSecureDeviceID;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("ComPort");
            XTemp.InnerText = Settings.DatacapCOMPort;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("InvoiceNo");
            XTemp.InnerText = invno;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("RefNo");
            XTemp.InnerText = resp_ref;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("AcctNo");
            XTemp.InnerText = "SecureDevice";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("Purchase");
            XTemp.InnerText = amt.ToString("f2");
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("CashBack");
            XTemp.InnerText = csbk.ToString("f2");
            XElem.AppendChild(XTemp);


            return XDoc.OuterXml;
        }



        public static string Datacap_CreditReturn_Request_XML(double amt, int invno, bool forcedmanual)
        {
            XmlDocument XDoc = new XmlDocument();

            XmlElement XElemRoot = XDoc.CreateElement("TStream");
            XDoc.AppendChild(XElemRoot);

            XmlElement XElem = XDoc.CreateElement("Transaction");
            XElemRoot.AppendChild(XElem);

            XmlElement XTemp = XDoc.CreateElement("MerchantID");
            XTemp.InnerText = Settings.DatacapMID;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranType");
            XTemp.InnerText = "Credit";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranCode");
            XTemp.InnerText = "Return";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("SecureDevice");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = (!forcedmanual) ? Settings.DatacapSecureDeviceID : "NONE";
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "NONE";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("ComPort");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = (!forcedmanual) ? Settings.DatacapCOMPort : "1";
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "1";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("InvoiceNo");
            XTemp.InnerText = invno.ToString();
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("RefNo");
            XTemp.InnerText = invno.ToString();
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("AcctNo");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = (!forcedmanual) ? "SecureDevice" : "Prompt";
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "Prompt";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("Purchase");
            XTemp.InnerText = amt.ToString("f2");
            XElem.AppendChild(XTemp);

            return XDoc.OuterXml;
        }

        public static string Datacap_DebitReturn_Request_XML(double amt, int invno)
        {
            XmlDocument XDoc = new XmlDocument();

            XmlElement XElemRoot = XDoc.CreateElement("TStream");
            XDoc.AppendChild(XElemRoot);

            XmlElement XElem = XDoc.CreateElement("Transaction");
            XElemRoot.AppendChild(XElem);

            XmlElement XTemp = XDoc.CreateElement("MerchantID");
            XTemp.InnerText = Settings.DatacapMID;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranType");
            XTemp.InnerText = "Debit";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranCode");
            XTemp.InnerText = "Return";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("SecureDevice");
            XTemp.InnerText = Settings.DatacapSecureDeviceID;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("ComPort");
            XTemp.InnerText = Settings.DatacapCOMPort;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("InvoiceNo");
            XTemp.InnerText = invno.ToString();
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("RefNo");
            XTemp.InnerText = invno.ToString();
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("AcctNo");
            XTemp.InnerText = "SecureDevice";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("Purchase");
            XTemp.InnerText = amt.ToString("f2");
            XElem.AppendChild(XTemp);

            return XDoc.OuterXml;
        }

        public static string Datacap_EBTReturn_Request_XML(double amt, string invno, bool forcedmanual)
        {
            XmlDocument XDoc = new XmlDocument();

            XmlElement XElemRoot = XDoc.CreateElement("TStream");
            XDoc.AppendChild(XElemRoot);

            XmlElement XElem = XDoc.CreateElement("Transaction");
            XElemRoot.AppendChild(XElem);

            XmlElement XTemp = XDoc.CreateElement("MerchantID");
            XTemp.InnerText = Settings.DatacapMID;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranType");
            XTemp.InnerText = "EBT";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("CardType");
            XTemp.InnerText = "Foodstamp";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranCode");
            XTemp.InnerText = "Return";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("SecureDevice");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = (!forcedmanual) ? Settings.DatacapSecureDeviceID : "NONE";
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "NONE";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("ComPort");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = (!forcedmanual) ? Settings.DatacapCOMPort : "1";
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "1";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("InvoiceNo");
            XTemp.InnerText = invno;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("RefNo");
            XTemp.InnerText = invno.ToString();
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("AcctNo");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = (!forcedmanual) ? "SecureDevice" : "Prompt";
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "Prompt";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("Purchase");
            XTemp.InnerText = amt.ToString("f2");
            XElem.AppendChild(XTemp);

            return XDoc.OuterXml;
        }

        public static string Datacap_EBTCashReturn_Request_XML(double amt, string invno, bool forcedmanual)
        {
            XmlDocument XDoc = new XmlDocument();

            XmlElement XElemRoot = XDoc.CreateElement("TStream");
            XDoc.AppendChild(XElemRoot);

            XmlElement XElem = XDoc.CreateElement("Transaction");
            XElemRoot.AppendChild(XElem);

            XmlElement XTemp = XDoc.CreateElement("MerchantID");
            XTemp.InnerText = Settings.DatacapMID;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranType");
            XTemp.InnerText = "EBT";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("CardType");
            XTemp.InnerText = "Cash";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranCode");
            XTemp.InnerText = "Return";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("SecureDevice");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = (!forcedmanual) ? Settings.DatacapSecureDeviceID : "NONE";
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "NONE";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("ComPort");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = (!forcedmanual) ? Settings.DatacapCOMPort : "1";
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "1";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("InvoiceNo");
            XTemp.InnerText = invno;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("RefNo");
            XTemp.InnerText = invno.ToString();
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("AcctNo");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = (!forcedmanual) ? "SecureDevice" : "Prompt";
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "Prompt";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("Purchase");
            XTemp.InnerText = amt.ToString("f2");
            XElem.AppendChild(XTemp);

            return XDoc.OuterXml;
        }

        public static string Datacap_PrePaidBalance_Request_XML(int invno)
        {
            XmlDocument XDoc = new XmlDocument();

            XmlElement XElemRoot = XDoc.CreateElement("TStream");
            XDoc.AppendChild(XElemRoot);

            XmlElement XElem = XDoc.CreateElement("Transaction");
            XElemRoot.AppendChild(XElem);

            XmlElement XTemp = XDoc.CreateElement("IpPort");
            XTemp.InnerText = "9100";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("MerchantID");
            XTemp.InnerText = Settings.DatacapMID;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranType");
            XTemp.InnerText = "PrePaid";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranCode");
            XTemp.InnerText = "Balance";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("SecureDevice");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = Settings.DatacapSecureDeviceID;
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "NONE";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("ComPort");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = Settings.DatacapCOMPort;
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "1";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("InvoiceNo");
            XTemp.InnerText = "458";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("RefNo");
            XTemp.InnerText = "458";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("AcctNo");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = "SecureDevice";
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "Prompt";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("Purchase");
            XTemp.InnerText = "0.00";
            XElem.AppendChild(XTemp);

            return XDoc.OuterXml;
        }

        public static string Datacap_PrePaidIssue_Request_XML(double amt, int invno)
        {
            XmlDocument XDoc = new XmlDocument();

            XmlElement XElemRoot = XDoc.CreateElement("TStream");
            XDoc.AppendChild(XElemRoot);

            XmlElement XElem = XDoc.CreateElement("Transaction");
            XElemRoot.AppendChild(XElem);

            XmlElement XTemp = XDoc.CreateElement("IpPort");
            XTemp.InnerText = "9100";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("MerchantID");
            XTemp.InnerText = Settings.DatacapMID;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranType");
            XTemp.InnerText = "PrePaid";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranCode");
            XTemp.InnerText = "Issue";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("SecureDevice");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = Settings.DatacapSecureDeviceID;
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "NONE";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("ComPort");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = Settings.DatacapCOMPort;
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "1";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("InvoiceNo");
            XTemp.InnerText = "458";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("RefNo");
            XTemp.InnerText = "458";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("AcctNo");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = "SecureDevice";
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "Prompt";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("Purchase");
            XTemp.InnerText = amt.ToString("f2");
            XElem.AppendChild(XTemp);

            return XDoc.OuterXml;
        }

        public static string Datacap_PrePaidReload_Request_XML(double amt, int invno)
        {
            XmlDocument XDoc = new XmlDocument();

            XmlElement XElemRoot = XDoc.CreateElement("TStream");
            XDoc.AppendChild(XElemRoot);

            XmlElement XElem = XDoc.CreateElement("Transaction");
            XElemRoot.AppendChild(XElem);

            XmlElement XTemp = XDoc.CreateElement("IpPort");
            XTemp.InnerText = "9100";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("MerchantID");
            XTemp.InnerText = Settings.DatacapMID;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranType");
            XTemp.InnerText = "PrePaid";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranCode");
            XTemp.InnerText = "Reload";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("SecureDevice");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = Settings.DatacapSecureDeviceID;
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "NONE";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("ComPort");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = Settings.DatacapCOMPort;
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "1";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("InvoiceNo");
            XTemp.InnerText = "458";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("RefNo");
            XTemp.InnerText = "458";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("AcctNo");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = "SecureDevice";
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "Prompt";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("Purchase");
            XTemp.InnerText = amt.ToString("f2");
            XElem.AppendChild(XTemp);

            return XDoc.OuterXml;
        }

        public static string Datacap_PrePaidVoidSale_Request_XML(double amt, string invno, string resp_ref, string resp_athcode)
        {
            XmlDocument XDoc = new XmlDocument();

            XmlElement XElemRoot = XDoc.CreateElement("TStream");
            XDoc.AppendChild(XElemRoot);

            XmlElement XElem = XDoc.CreateElement("Transaction");
            XElemRoot.AppendChild(XElem);

            XmlElement XTemp = XDoc.CreateElement("IpPort");
            XTemp.InnerText = "9100";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("MerchantID");
            XTemp.InnerText = Settings.DatacapMID;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranType");
            XTemp.InnerText = "PrePaid";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranCode");
            XTemp.InnerText = "VoidSale";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("SecureDevice");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = Settings.DatacapSecureDeviceID;
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "NONE";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("ComPort");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = Settings.DatacapCOMPort;
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "1";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("InvoiceNo");
            XTemp.InnerText = invno;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("RefNo");
            XTemp.InnerText = resp_ref;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("AcctNo");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = "SecureDevice";
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "Prompt";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("Purchase");
            XTemp.InnerText = amt.ToString("f2");
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("AuthCode");
            XTemp.InnerText = resp_athcode;
            XElem.AppendChild(XTemp);

            return XDoc.OuterXml;
        }

        public static string Datacap_PrePaidVoidIssue_Request_XML(double amt, string invno, string resp_ref, string resp_athcode)
        {
            XmlDocument XDoc = new XmlDocument();

            XmlElement XElemRoot = XDoc.CreateElement("TStream");
            XDoc.AppendChild(XElemRoot);

            XmlElement XElem = XDoc.CreateElement("Transaction");
            XElemRoot.AppendChild(XElem);

            XmlElement XTemp = XDoc.CreateElement("IpPort");
            XTemp.InnerText = "9100";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("MerchantID");
            XTemp.InnerText = Settings.DatacapMID;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranType");
            XTemp.InnerText = "PrePaid";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranCode");
            XTemp.InnerText = "VoidIssue";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("SecureDevice");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = Settings.DatacapSecureDeviceID;
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "NONE";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("ComPort");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = Settings.DatacapCOMPort;
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "1";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("InvoiceNo");
            XTemp.InnerText = invno;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("RefNo");
            XTemp.InnerText = resp_ref;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("AcctNo");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = "SecureDevice";
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "Prompt";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("Purchase");
            XTemp.InnerText = amt.ToString("f2");
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("AuthCode");
            XTemp.InnerText = resp_athcode;
            XElem.AppendChild(XTemp);

            return XDoc.OuterXml;
        }

        public static string Datacap_PrePaidVoidReload_Request_XML(double amt, string invno, string resp_ref, string resp_athcode)
        {
            XmlDocument XDoc = new XmlDocument();

            XmlElement XElemRoot = XDoc.CreateElement("TStream");
            XDoc.AppendChild(XElemRoot);

            XmlElement XElem = XDoc.CreateElement("Transaction");
            XElemRoot.AppendChild(XElem);

            XmlElement XTemp = XDoc.CreateElement("IpPort");
            XTemp.InnerText = "9100";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("MerchantID");
            XTemp.InnerText = Settings.DatacapMID;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranType");
            XTemp.InnerText = "PrePaid";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranCode");
            XTemp.InnerText = "VoidReload";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("SecureDevice");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = Settings.DatacapSecureDeviceID;
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "NONE";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("ComPort");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = Settings.DatacapCOMPort;
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "1";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("InvoiceNo");
            XTemp.InnerText = invno;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("RefNo");
            XTemp.InnerText = resp_ref;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("AcctNo");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = "SecureDevice";
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "Prompt";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("Purchase");
            XTemp.InnerText = amt.ToString("f2");
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("AuthCode");
            XTemp.InnerText = resp_athcode;
            XElem.AppendChild(XTemp);

            return XDoc.OuterXml;
        }



        public static string Datacap_EBTBalance_Request_XML(string invno)
        {
            XmlDocument XDoc = new XmlDocument();

            XmlElement XElemRoot = XDoc.CreateElement("TStream");
            XDoc.AppendChild(XElemRoot);

            XmlElement XElem = XDoc.CreateElement("Transaction");
            XElemRoot.AppendChild(XElem);

            XmlElement XTemp = XDoc.CreateElement("MerchantID");
            XTemp.InnerText = Settings.DatacapMID;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranType");
            XTemp.InnerText = "EBT";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("CardType");
            XTemp.InnerText = "Foodstamp";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranCode");
            XTemp.InnerText = "Balance";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("SecureDevice");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = Settings.DatacapSecureDeviceID;
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "NONE";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("ComPort");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = Settings.DatacapCOMPort;
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "1";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("InvoiceNo");
            XTemp.InnerText = invno;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("RefNo");
            XTemp.InnerText = invno;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("AcctNo");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = "SecureDevice";
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "Prompt";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("Purchase");
            XTemp.InnerText = "0.00";
            XElem.AppendChild(XTemp);

            return XDoc.OuterXml;
        }

        public static string Datacap_EBTCashBalance_Request_XML(string invno)
        {
            XmlDocument XDoc = new XmlDocument();

            XmlElement XElemRoot = XDoc.CreateElement("TStream");
            XDoc.AppendChild(XElemRoot);

            XmlElement XElem = XDoc.CreateElement("Transaction");
            XElemRoot.AppendChild(XElem);

            XmlElement XTemp = XDoc.CreateElement("MerchantID");
            XTemp.InnerText = Settings.DatacapMID;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranType");
            XTemp.InnerText = "EBT";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("CardType");
            XTemp.InnerText = "Cash";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranCode");
            XTemp.InnerText = "Balance";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("SecureDevice");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = Settings.DatacapSecureDeviceID;
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "NONE";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("ComPort");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = Settings.DatacapCOMPort;
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "1";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("InvoiceNo");
            XTemp.InnerText = invno;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("RefNo");
            XTemp.InnerText = invno;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("AcctNo");
            if (Settings.DatacapCardEntryMode == 0) XTemp.InnerText = "SecureDevice";
            if (Settings.DatacapCardEntryMode == 1) XTemp.InnerText = "Prompt";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("Purchase");
            XTemp.InnerText = "0.00";
            XElem.AppendChild(XTemp);

            return XDoc.OuterXml;
        }

        public static void Datacap_Signature_Response(string pxml, ref string pstatus, ref string presponse, ref string psignature)
        {
            XmlDocument XDoc1 = new XmlDocument();
            XDoc1.LoadXml(pxml);
            try
            {
                pstatus = XDoc1.GetElementsByTagName("CmdStatus")[0].InnerText;
                presponse = XDoc1.GetElementsByTagName("TextResponse")[0].InnerText;
                psignature = XDoc1.GetElementsByTagName("Signature")[0].InnerText.ToString();
            }
            catch
            {
            }
        }

        public static void Datacap_General_Response(string p_xml, ref string p_status, ref string p_responsetext, ref string p_acct, ref string p_merch,
                                                    ref string p_trancode, ref string p_cardtype, ref string p_authcode, ref string p_invno, ref string p_refno,
                                                    ref string p_acqrefdata, ref string p_recordno, ref double pch_amt, ref double ath_amt, ref double cbk_amt,
                                                    ref double bal_amt)
        {
            XmlDocument XDoc1 = new XmlDocument();
            XDoc1.LoadXml(p_xml);
            try
            {
                p_status = XDoc1.GetElementsByTagName("CmdStatus")[0].InnerText;
            }
            catch
            {
            }
            try
            {
                p_responsetext = XDoc1.GetElementsByTagName("TextResponse")[0].InnerText;
            }
            catch
            {
            }

            if (p_status == "Approved")
            {
                try
                {
                    p_trancode = XDoc1.GetElementsByTagName("TranCode")[0].InnerText;
                }
                catch
                {
                }

                try
                {
                    p_acct = XDoc1.GetElementsByTagName("AcctNo")[0].InnerText;
                }
                catch
                {
                }

                try
                {
                    p_merch = XDoc1.GetElementsByTagName("MerchantID")[0].InnerText;
                }
                catch
                {
                }

                try
                {
                    p_cardtype = XDoc1.GetElementsByTagName("CardType")[0].InnerText;
                }
                catch
                {
                }

                try
                {
                    p_authcode = XDoc1.GetElementsByTagName("AuthCode")[0].InnerText;
                }
                catch
                {
                }

                try
                {
                    p_invno = XDoc1.GetElementsByTagName("InvoiceNo")[0].InnerText;
                }
                catch
                {
                }

                try
                {
                    p_refno = XDoc1.GetElementsByTagName("RefNo")[0].InnerText;
                }
                catch
                {
                }

                try
                {
                    p_acqrefdata = XDoc1.GetElementsByTagName("AcqRefData")[0].InnerText;
                }
                catch
                {
                }

                try
                {
                    p_recordno = XDoc1.GetElementsByTagName("RecordNo")[0].InnerText;
                }
                catch
                {
                }

                try
                {
                    pch_amt = fnDouble(XDoc1.GetElementsByTagName("Purchase")[0].InnerText);
                }
                catch
                {
                }

                try
                {
                    ath_amt = fnDouble(XDoc1.GetElementsByTagName("Authorize")[0].InnerText);
                }
                catch
                {
                }

                try
                {
                    cbk_amt = fnDouble(XDoc1.GetElementsByTagName("CashBack")[0].InnerText);
                }
                catch
                {
                }

                try
                {
                    bal_amt = fnDouble(XDoc1.GetElementsByTagName("Balance")[0].InnerText);
                }
                catch
                {
                }
            }
        }

        #endregion

        #region Datacap EMV

        public static string FetchDatacapEMVSequence()
        {
            string val = "";
            string strSQL = " select isnull(sequenceno,'0010010010') as sqno from setup  ";

            SqlConnection objConnection = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objCommand = new SqlCommand(strSQL, objConnection);

            try
            {
                if (objConnection.State == System.Data.ConnectionState.Open) { objConnection.Close(); }
                objConnection.Open();

                SqlDataReader objReader = null;
                objReader = objCommand.ExecuteReader();
                if (objReader.Read())
                {
                    val = objReader["sqno"].ToString().Trim();
                }
                objReader.Close();

                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();

                if (val == "")
                {
                    val = "0010010010";
                }
                return val;
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                strErrMsg = SQLDBException.Message;
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();
                return "0010010010";
            }
        }

        public static string GetEMVParamDownloadStatus(string xmlresponse)
        {
            string returnval = "";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlresponse);
            try
            {
                returnval = xmlDoc.GetElementsByTagName("TextResponse")[0].InnerText;
            }
            catch
            {
            }
            return returnval;
        }

        public static string GetEMVPadResetStatus(string xmlresponse)
        {
            string returnval = "";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlresponse);
            try
            {
                returnval = xmlDoc.GetElementsByTagName("CmdStatus")[0].InnerText;
            }
            catch
            {
            }
            return returnval;
        }

        public static string GetEMVPadResetTextResponse(string xmlresponse)
        {
            string returnval = "";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlresponse);
            try
            {
                returnval = xmlDoc.GetElementsByTagName("TextResponse")[0].InnerText;
            }
            catch
            {
            }
            return returnval;
        }

        public static void StoreResponseSequence(string xmlresponse)
        {
            string returnval = "";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlresponse);
            try
            {
                returnval = xmlDoc.GetElementsByTagName("SequenceNo")[0].InnerText;

                string strSQL = " update setup set sequenceno=@sqno ";

                SqlConnection objConnection = new SqlConnection(SystemVariables.ConnectionString);
                SqlCommand objCommand = new SqlCommand(strSQL, objConnection);

                objCommand.Parameters.Add(new SqlParameter("@sqno", System.Data.SqlDbType.VarChar));
                objCommand.Parameters["@sqno"].Value = returnval;

                if (objConnection.State == System.Data.ConnectionState.Open) { objConnection.Close(); }
                objConnection.Open();
                objCommand.ExecuteNonQuery();
            }
            catch
            {
            }
        }

        public static string PrepareEMVParamDownloadXML(string host, string port, string termnl, string merchntid, string oprid, string usrtrace, string secudevice,
            string comport)
        {
            XmlDocument XDoc = new XmlDocument();

            XmlElement XElemRoot = XDoc.CreateElement("TStream");
            XDoc.AppendChild(XElemRoot);

            XmlElement XElem = XDoc.CreateElement("Admin");
            XElemRoot.AppendChild(XElem);

            XmlElement XTemp = XDoc.CreateElement("HostOrIP");
            XTemp.InnerText = host;
            XElem.AppendChild(XTemp);

            if (port != "")
            {
                XTemp = XDoc.CreateElement("IPPort");
                XTemp.InnerText = port;
                XElem.AppendChild(XTemp);
            }

            XTemp = XDoc.CreateElement("MerchantID");
            XTemp.InnerText = merchntid;
            XElem.AppendChild(XTemp);

            if (termnl != "")
            {
                XTemp = XDoc.CreateElement("TerminalID");
                XTemp.InnerText = termnl;
                XElem.AppendChild(XTemp);
            }

            if (oprid != "")
            {
                XTemp = XDoc.CreateElement("OperatorID");
                XTemp.InnerText = oprid;
                XElem.AppendChild(XTemp);
            }

            if (usrtrace != "")
            {
                XTemp = XDoc.CreateElement("UserTrace");
                XTemp.InnerText = usrtrace;
                XElem.AppendChild(XTemp);
            }

            XTemp = XDoc.CreateElement("MerchantLanguage");
            XTemp.InnerText = "English";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("TranCode");
            XTemp.InnerText = "EMVParamDownload";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("SecureDevice");
            XTemp.InnerText = secudevice;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("ComPort");
            XTemp.InnerText = comport;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("SequenceNo");
            XTemp.InnerText = FetchDatacapEMVSequence();
            XElem.AppendChild(XTemp);

            return XDoc.OuterXml;
        }

        public static string PrepareEMVPadResetXML()
        {
            XmlDocument XDoc = new XmlDocument();

            XmlElement XElemRoot = XDoc.CreateElement("TStream");
            XDoc.AppendChild(XElemRoot);

            XmlElement XElem = XDoc.CreateElement("Transaction");
            XElemRoot.AppendChild(XElem);

            XmlElement XTemp = XDoc.CreateElement("HostOrIP");
            XTemp.InnerText = Settings.DatacapEMVServerIP;
            XElem.AppendChild(XTemp);

            if (Settings.DatacapEMVServerPort != "")
            {
                XTemp = XDoc.CreateElement("IPPort");
                XTemp.InnerText = Settings.DatacapEMVServerPort;
                XElem.AppendChild(XTemp);
            }

            XTemp = XDoc.CreateElement("MerchantID");
            XTemp.InnerText = Settings.DatacapEMVMID;
            XElem.AppendChild(XTemp);

            if (Settings.DatacapEMVTerminalID != "")
            {
                XTemp = XDoc.CreateElement("TerminalID");
                XTemp.InnerText = Settings.DatacapEMVTerminalID;
                XElem.AppendChild(XTemp);
            }

            if (Settings.DatacapEMVUserTrace != "")
            {
                XTemp = XDoc.CreateElement("UserTrace");
                XTemp.InnerText = Settings.DatacapEMVUserTrace;
                XElem.AppendChild(XTemp);
            }

            XTemp = XDoc.CreateElement("TranCode");
            XTemp.InnerText = "EMVPadReset";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("SecureDevice");
            XTemp.InnerText = Settings.DatacapEMVSecurityDevice;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("ComPort");
            XTemp.InnerText = Settings.DatacapEMVCOMPort;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("SequenceNo");
            XTemp.InnerText = FetchDatacapEMVSequence();
            XElem.AppendChild(XTemp);

            return XDoc.OuterXml;
        }

        public static string PrepareEMVDuplicateSaleXML(int pinv, double pAmt, bool cshBK, string manl)
        {
            XmlDocument XDoc = new XmlDocument();

            XmlElement XElemRoot = XDoc.CreateElement("TStream");
            XDoc.AppendChild(XElemRoot);

            XmlElement XElem = XDoc.CreateElement("Transaction");
            XElemRoot.AppendChild(XElem);

            XmlElement XTemp = XDoc.CreateElement("HostOrIP");
            XTemp.InnerText = Settings.DatacapEMVServerIP;
            XElem.AppendChild(XTemp);

            if (Settings.DatacapEMVServerPort != "")
            {
                XTemp = XDoc.CreateElement("IPPort");
                XTemp.InnerText = Settings.DatacapEMVServerPort;
                XElem.AppendChild(XTemp);
            }

            XTemp = XDoc.CreateElement("MerchantID");
            XTemp.InnerText = Settings.DatacapEMVMID;
            XElem.AppendChild(XTemp);

            if (Settings.DatacapEMVTerminalID != "")
            {
                XTemp = XDoc.CreateElement("TerminalID");
                XTemp.InnerText = Settings.DatacapEMVTerminalID;
                XElem.AppendChild(XTemp);
            }

            if (Settings.DatacapEMVOperatorID != "")
            {
                XTemp = XDoc.CreateElement("OperatorID");
                XTemp.InnerText = Settings.DatacapEMVOperatorID;
                XElem.AppendChild(XTemp);
            }

            if (Settings.DatacapEMVUserTrace != "")
            {
                XTemp = XDoc.CreateElement("UserTrace");
                XTemp.InnerText = Settings.DatacapEMVUserTrace;
                XElem.AppendChild(XTemp);
            }

            XTemp = XDoc.CreateElement("TranCode");
            XTemp.InnerText = "EMVSale";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("SecureDevice");
            XTemp.InnerText = Settings.DatacapEMVSecurityDevice;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("ComPort");
            XTemp.InnerText = Settings.DatacapEMVCOMPort;
            XElem.AppendChild(XTemp);

            if (manl == "Y")
            {
                XTemp = XDoc.CreateElement("AcctNo");
                XTemp.InnerText = "Prompt";
                XElem.AppendChild(XTemp);
            }

            XTemp = XDoc.CreateElement("InvoiceNo");
            XTemp.InnerText = pinv.ToString();
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("RefNo");
            XTemp.InnerText = pinv.ToString();
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("Purchase");
            XTemp.InnerText = pAmt.ToString("f2");
            XElem.AppendChild(XTemp);

            if (cshBK)
            {
                XTemp = XDoc.CreateElement("CashBack");
                XTemp.InnerText = "Prompt";
                XElem.AppendChild(XTemp);
            }

            XTemp = XDoc.CreateElement("Duplicate");
            XTemp.InnerText = "None";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("SequenceNo");
            XTemp.InnerText = FetchDatacapEMVSequence();
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("PartialAuth");
            XTemp.InnerText = "Allow";
            XElem.AppendChild(XTemp);

            if (Settings.DatacapEMVToken == "Y")
            {
                XTemp = XDoc.CreateElement("RecordNo");
                XTemp.InnerText = "RecordNumberRequested";
                XElem.AppendChild(XTemp);

                XTemp = XDoc.CreateElement("Frequency");
                XTemp.InnerText = "OneTime";
                XElem.AppendChild(XTemp);
            }

            return XDoc.OuterXml;
        }

        public static string PrepareEMVSaleXML(int pinv, double pAmt, bool cshBK, string manl)
        {
            XmlDocument XDoc = new XmlDocument();

            XmlElement XElemRoot = XDoc.CreateElement("TStream");
            XDoc.AppendChild(XElemRoot);

            XmlElement XElem = XDoc.CreateElement("Transaction");
            XElemRoot.AppendChild(XElem);

            XmlElement XTemp = XDoc.CreateElement("HostOrIP");
            XTemp.InnerText = Settings.DatacapEMVServerIP;
            XElem.AppendChild(XTemp);

            if (Settings.DatacapEMVServerPort != "")
            {
                XTemp = XDoc.CreateElement("IPPort");
                XTemp.InnerText = Settings.DatacapEMVServerPort;
                XElem.AppendChild(XTemp);
            }

            XTemp = XDoc.CreateElement("MerchantID");
            XTemp.InnerText = Settings.DatacapEMVMID;
            XElem.AppendChild(XTemp);

            if (Settings.DatacapEMVTerminalID != "")
            {
                XTemp = XDoc.CreateElement("TerminalID");
                XTemp.InnerText = Settings.DatacapEMVTerminalID;
                XElem.AppendChild(XTemp);
            }

            if (Settings.DatacapEMVOperatorID != "")
            {
                XTemp = XDoc.CreateElement("OperatorID");
                XTemp.InnerText = Settings.DatacapEMVOperatorID;
                XElem.AppendChild(XTemp);
            }

            if (Settings.DatacapEMVUserTrace != "")
            {
                XTemp = XDoc.CreateElement("UserTrace");
                XTemp.InnerText = Settings.DatacapEMVUserTrace;
                XElem.AppendChild(XTemp);
            }

            XTemp = XDoc.CreateElement("TranCode");
            XTemp.InnerText = "EMVSale";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("SecureDevice");
            XTemp.InnerText = Settings.DatacapEMVSecurityDevice;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("ComPort");
            XTemp.InnerText = Settings.DatacapEMVCOMPort;
            XElem.AppendChild(XTemp);

            if (manl == "Y")
            {
                XTemp = XDoc.CreateElement("AcctNo");
                XTemp.InnerText = "Prompt";
                XElem.AppendChild(XTemp);
            }

            XTemp = XDoc.CreateElement("InvoiceNo");
            XTemp.InnerText = pinv.ToString();
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("RefNo");
            XTemp.InnerText = pinv.ToString();
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("Purchase");
            XTemp.InnerText = pAmt.ToString("f2");
            XElem.AppendChild(XTemp);

            if (cshBK)
            {
                XTemp = XDoc.CreateElement("CashBack");
                XTemp.InnerText = "Prompt";
                XElem.AppendChild(XTemp);
            }

            XTemp = XDoc.CreateElement("SequenceNo");
            XTemp.InnerText = FetchDatacapEMVSequence();
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("PartialAuth");
            XTemp.InnerText = "Allow";
            XElem.AppendChild(XTemp);

            if (Settings.DatacapEMVToken == "Y")
            {
                XTemp = XDoc.CreateElement("RecordNo");
                XTemp.InnerText = "RecordNumberRequested";
                XElem.AppendChild(XTemp);

                XTemp = XDoc.CreateElement("Frequency");
                XTemp.InnerText = "OneTime";
                XElem.AppendChild(XTemp);
            }

            return XDoc.OuterXml;
        }

        public static string PrepareEMVVoidSaleXML(double amt, string invno, string resp_ref, string resp_athcode, string resp_acqref,
            string resp_processdata, string resp_recordno, bool cshBK, double cshbk_amt)
        {
            XmlDocument XDoc = new XmlDocument();

            XmlElement XElemRoot = XDoc.CreateElement("TStream");
            XDoc.AppendChild(XElemRoot);

            XmlElement XElem = XDoc.CreateElement("Transaction");
            XElemRoot.AppendChild(XElem);

            XmlElement XTemp = XDoc.CreateElement("HostOrIP");
            XTemp.InnerText = Settings.DatacapEMVServerIP;
            XElem.AppendChild(XTemp);

            if (Settings.DatacapEMVServerPort != "")
            {
                XTemp = XDoc.CreateElement("IPPort");
                XTemp.InnerText = Settings.DatacapEMVServerPort;
                XElem.AppendChild(XTemp);
            }

            XTemp = XDoc.CreateElement("MerchantID");
            XTemp.InnerText = Settings.DatacapEMVMID;
            XElem.AppendChild(XTemp);

            if (Settings.DatacapEMVTerminalID != "")
            {
                XTemp = XDoc.CreateElement("TerminalID");
                XTemp.InnerText = Settings.DatacapEMVTerminalID;
                XElem.AppendChild(XTemp);
            }

            if (Settings.DatacapEMVOperatorID != "")
            {
                XTemp = XDoc.CreateElement("OperatorID");
                XTemp.InnerText = Settings.DatacapEMVOperatorID;
                XElem.AppendChild(XTemp);
            }

            if (Settings.DatacapEMVUserTrace != "")
            {
                XTemp = XDoc.CreateElement("UserTrace");
                XTemp.InnerText = Settings.DatacapEMVUserTrace;
                XElem.AppendChild(XTemp);
            }

            if (Settings.DatacapEMVToken == "N")
            {
                XTemp = XDoc.CreateElement("TranCode");
                XTemp.InnerText = "EMVVoidSale";
                XElem.AppendChild(XTemp);
            }

            if (Settings.DatacapEMVToken == "Y")
            {
                XTemp = XDoc.CreateElement("TranType");
                XTemp.InnerText = "Credit";
                XElem.AppendChild(XTemp);

                XTemp = XDoc.CreateElement("TranCode");
                XTemp.InnerText = "VoidSaleByRecordNo";
                XElem.AppendChild(XTemp);
            }

            XTemp = XDoc.CreateElement("SecureDevice");
            XTemp.InnerText = Settings.DatacapEMVSecurityDevice;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("ComPort");
            XTemp.InnerText = Settings.DatacapEMVCOMPort;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("InvoiceNo");
            XTemp.InnerText = invno;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("RefNo");
            XTemp.InnerText = resp_ref;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("AuthCode");
            XTemp.InnerText = resp_athcode;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("Purchase");
            XTemp.InnerText = amt.ToString("f2");
            XElem.AppendChild(XTemp);

            if (cshBK)
            {
                XTemp = XDoc.CreateElement("CashBack");
                XTemp.InnerText = cshbk_amt.ToString("f2");
                XElem.AppendChild(XTemp);
            }

            XTemp = XDoc.CreateElement("SequenceNo");
            XTemp.InnerText = FetchDatacapEMVSequence();
            XElem.AppendChild(XTemp);

            if (Settings.DatacapEMVToken == "Y")
            {
                XTemp = XDoc.CreateElement("PartialAuth");
                XTemp.InnerText = "Allow";
                XElem.AppendChild(XTemp);

                XTemp = XDoc.CreateElement("RecordNo");
                XTemp.InnerText = resp_recordno;
                XElem.AppendChild(XTemp);

                XTemp = XDoc.CreateElement("Frequency");
                XTemp.InnerText = "OneTime";
                XElem.AppendChild(XTemp);
            }

            XTemp = XDoc.CreateElement("AcqRefData");
            XTemp.InnerText = resp_acqref;
            XElem.AppendChild(XTemp);

            if (resp_processdata != "")
            {
                XTemp = XDoc.CreateElement("ProcessData");
                XTemp.InnerText = resp_processdata;
                XElem.AppendChild(XTemp);
            }
            return XDoc.OuterXml;
        }


        public static string PrepareEMVVoidReturnXML(double amt, string invno, string resp_ref, string resp_athcode, string resp_acqref, string resp_processdata, string resp_recordno)
        {
            XmlDocument XDoc = new XmlDocument();

            XmlElement XElemRoot = XDoc.CreateElement("TStream");
            XDoc.AppendChild(XElemRoot);

            XmlElement XElem = XDoc.CreateElement("Transaction");
            XElemRoot.AppendChild(XElem);

            XmlElement XTemp = XDoc.CreateElement("HostOrIP");
            XTemp.InnerText = Settings.DatacapEMVServerIP;
            XElem.AppendChild(XTemp);

            if (Settings.DatacapEMVServerPort != "")
            {
                XTemp = XDoc.CreateElement("IPPort");
                XTemp.InnerText = Settings.DatacapEMVServerPort;
                XElem.AppendChild(XTemp);
            }

            XTemp = XDoc.CreateElement("MerchantID");
            XTemp.InnerText = Settings.DatacapEMVMID;
            XElem.AppendChild(XTemp);

            if (Settings.DatacapEMVTerminalID != "")
            {
                XTemp = XDoc.CreateElement("TerminalID");
                XTemp.InnerText = Settings.DatacapEMVTerminalID;
                XElem.AppendChild(XTemp);
            }

            if (Settings.DatacapEMVOperatorID != "")
            {
                XTemp = XDoc.CreateElement("OperatorID");
                XTemp.InnerText = Settings.DatacapEMVOperatorID;
                XElem.AppendChild(XTemp);
            }

            if (Settings.DatacapEMVUserTrace != "")
            {
                XTemp = XDoc.CreateElement("UserTrace");
                XTemp.InnerText = Settings.DatacapEMVUserTrace;
                XElem.AppendChild(XTemp);
            }

            if (Settings.DatacapEMVToken == "N")
            {
                XTemp = XDoc.CreateElement("TranCode");
                XTemp.InnerText = "EMVVoidReturn";
                XElem.AppendChild(XTemp);
            }

            if (Settings.DatacapEMVToken == "Y")
            {
                XTemp = XDoc.CreateElement("TranType");
                XTemp.InnerText = "Credit";
                XElem.AppendChild(XTemp);

                XTemp = XDoc.CreateElement("TranCode");
                XTemp.InnerText = "VoidReturnByRecordNo";
                XElem.AppendChild(XTemp);
            }

            XTemp = XDoc.CreateElement("SecureDevice");
            XTemp.InnerText = Settings.DatacapEMVSecurityDevice;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("ComPort");
            XTemp.InnerText = Settings.DatacapEMVCOMPort;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("InvoiceNo");
            XTemp.InnerText = invno;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("RefNo");
            XTemp.InnerText = resp_ref;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("AuthCode");
            XTemp.InnerText = resp_athcode;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("Purchase");
            XTemp.InnerText = amt.ToString("f2");
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("SequenceNo");
            XTemp.InnerText = FetchDatacapEMVSequence();
            XElem.AppendChild(XTemp);

            if (Settings.DatacapEMVToken == "Y")
            {
                XTemp = XDoc.CreateElement("PartialAuth");
                XTemp.InnerText = "Allow";
                XElem.AppendChild(XTemp);

                XTemp = XDoc.CreateElement("RecordNo");
                XTemp.InnerText = resp_recordno;
                XElem.AppendChild(XTemp);

                XTemp = XDoc.CreateElement("Frequency");
                XTemp.InnerText = "OneTime";
                XElem.AppendChild(XTemp);
            }

            XTemp = XDoc.CreateElement("AcqRefData");
            XTemp.InnerText = resp_acqref;
            XElem.AppendChild(XTemp);

            if (resp_processdata != "")
            {
                XTemp = XDoc.CreateElement("ProcessData");
                XTemp.InnerText = resp_processdata;
                XElem.AppendChild(XTemp);
            }
            return XDoc.OuterXml;
        }


        public static string PrepareEMVReturnXML(int pinv, double pAmt, string manl)
        {
            XmlDocument XDoc = new XmlDocument();

            XmlElement XElemRoot = XDoc.CreateElement("TStream");
            XDoc.AppendChild(XElemRoot);

            XmlElement XElem = XDoc.CreateElement("Transaction");
            XElemRoot.AppendChild(XElem);

            XmlElement XTemp = XDoc.CreateElement("HostOrIP");
            XTemp.InnerText = Settings.DatacapEMVServerIP;
            XElem.AppendChild(XTemp);

            if (Settings.DatacapEMVServerPort != "")
            {
                XTemp = XDoc.CreateElement("IPPort");
                XTemp.InnerText = Settings.DatacapEMVServerPort;
                XElem.AppendChild(XTemp);
            }

            XTemp = XDoc.CreateElement("MerchantID");
            XTemp.InnerText = Settings.DatacapEMVMID;
            XElem.AppendChild(XTemp);

            if (Settings.DatacapEMVTerminalID != "")
            {
                XTemp = XDoc.CreateElement("TerminalID");
                XTemp.InnerText = Settings.DatacapEMVTerminalID;
                XElem.AppendChild(XTemp);
            }

            if (Settings.DatacapEMVOperatorID != "")
            {
                XTemp = XDoc.CreateElement("OperatorID");
                XTemp.InnerText = Settings.DatacapEMVOperatorID;
                XElem.AppendChild(XTemp);
            }

            if (Settings.DatacapEMVUserTrace != "")
            {
                XTemp = XDoc.CreateElement("UserTrace");
                XTemp.InnerText = Settings.DatacapEMVUserTrace;
                XElem.AppendChild(XTemp);
            }

            XTemp = XDoc.CreateElement("TranCode");
            XTemp.InnerText = "EMVReturn";
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("SecureDevice");
            XTemp.InnerText = Settings.DatacapEMVSecurityDevice;
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("ComPort");
            XTemp.InnerText = Settings.DatacapEMVCOMPort;
            XElem.AppendChild(XTemp);

            if (manl == "Y")
            {
                XTemp = XDoc.CreateElement("AcctNo");
                XTemp.InnerText = "Prompt";
                XElem.AppendChild(XTemp);
            }

            XTemp = XDoc.CreateElement("InvoiceNo");
            XTemp.InnerText = pinv.ToString();
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("RefNo");
            XTemp.InnerText = pinv.ToString();
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("Purchase");
            XTemp.InnerText = pAmt.ToString("f2");
            XElem.AppendChild(XTemp);

            XTemp = XDoc.CreateElement("SequenceNo");
            XTemp.InnerText = FetchDatacapEMVSequence();
            XElem.AppendChild(XTemp);

            if (Settings.DatacapEMVToken == "Y")
            {
                XTemp = XDoc.CreateElement("RecordNo");
                XTemp.InnerText = "RecordNumberRequested";
                XElem.AppendChild(XTemp);

                XTemp = XDoc.CreateElement("Frequency");
                XTemp.InnerText = "OneTime";
                XElem.AppendChild(XTemp);
            }

            return XDoc.OuterXml;
        }



        public static void DatacapEMV_General_Response(string p_xml, ref string p_status, ref string p_responsetext, ref string p_acct, ref string p_merch,
                                                   ref string p_trancode, ref string p_cardtype, ref string p_authcode, ref string p_invno, ref string p_refno,
                                                   ref string p_acqrefdata, ref string p_recordno, ref double pch_amt, ref double ath_amt, ref double cbk_amt,
                                                   ref string p_processdata, ref string p_printdraft)
        {
            XmlDocument XDoc1 = new XmlDocument();
            XDoc1.LoadXml(p_xml);
            try
            {
                p_status = XDoc1.GetElementsByTagName("CmdStatus")[0].InnerText;
            }
            catch
            {
            }
            try
            {
                p_responsetext = XDoc1.GetElementsByTagName("TextResponse")[0].InnerText;
            }
            catch
            {
            }

            if (p_status == "Approved")
            {
                try
                {
                    p_trancode = XDoc1.GetElementsByTagName("TranCode")[0].InnerText;
                }
                catch
                {
                }

                try
                {
                    p_acct = XDoc1.GetElementsByTagName("AcctNo")[0].InnerText;
                }
                catch
                {
                }

                try
                {
                    p_merch = XDoc1.GetElementsByTagName("MerchantID")[0].InnerText;
                }
                catch
                {
                }

                try
                {
                    p_cardtype = XDoc1.GetElementsByTagName("CardType")[0].InnerText;
                }
                catch
                {
                }

                try
                {
                    p_authcode = XDoc1.GetElementsByTagName("AuthCode")[0].InnerText;
                }
                catch
                {
                }

                try
                {
                    p_invno = XDoc1.GetElementsByTagName("InvoiceNo")[0].InnerText;
                }
                catch
                {
                }

                try
                {
                    p_refno = XDoc1.GetElementsByTagName("RefNo")[0].InnerText;
                }
                catch
                {
                }

                try
                {
                    p_acqrefdata = XDoc1.GetElementsByTagName("AcqRefData")[0].InnerText;
                }
                catch
                {
                }

                try
                {
                    p_recordno = XDoc1.GetElementsByTagName("RecordNo")[0].InnerText;
                }
                catch
                {
                }

                try
                {
                    pch_amt = fnDouble(XDoc1.GetElementsByTagName("Purchase")[0].InnerText);
                }
                catch
                {
                }

                try
                {
                    ath_amt = fnDouble(XDoc1.GetElementsByTagName("Authorize")[0].InnerText);
                }
                catch
                {
                }

                try
                {
                    cbk_amt = fnDouble(XDoc1.GetElementsByTagName("CashBack")[0].InnerText);
                }
                catch
                {
                }

                try
                {
                    p_processdata = XDoc1.GetElementsByTagName("ProcessData")[0].InnerText;
                }
                catch
                {
                }
            }

            XmlNodeList printxml = XDoc1.GetElementsByTagName("PrintData");
            p_printdraft = printxml.Item(0).InnerXml;

        }

        #endregion

        #region POSLink

        /*

        public static POSLink.CommSetting GetPOSLinkCommSetup()
        {
            POSLink.CommSetting pgc = new POSLink.CommSetting();
            if (Settings.POSLinkCommType != "")
            {
                pgc.CommType = Settings.POSLinkCommType;
            }

            if (Settings.POSLinkDestIP != "")
            {
                pgc.DestIP = Settings.POSLinkDestIP;
            }

            if (Settings.POSLinkDestPort != "")
            {
                pgc.DestPort = Settings.POSLinkDestPort;
            }

            if (Settings.POSLinkSerialPort != "")
            {
                pgc.SerialPort = Settings.POSLinkSerialPort;
            }

            if (Settings.POSLinkTimeout != "")
            {
                pgc.TimeOut = Settings.POSLinkTimeout;
            }

            if (Settings.POSLinkBaudRate != "")
            {
                pgc.BaudRate = Settings.POSLinkBaudRate;
            }

            return pgc;
        }*/

        public static double GetPAXCashback(string responseXML)
        {
            double cashback = 0;

            XmlDocument XDoc1 = new XmlDocument();
            XDoc1.LoadXml("<ExtData>" + responseXML + "</ExtData>");
            try
            {
                foreach (XmlNode nd in XDoc1.DocumentElement.ChildNodes)
                {
                    if (nd.Name == "CashBackAmout")
                    {
                        cashback = fnDouble(nd.InnerText) / 100;
                        break;
                    }
                }

            }
            catch
            {
                cashback = 0;
            }
            return cashback;
        }


        #endregion


        // Check for Valid Live Weight (POS Module)
        public static bool IsValidScaleWeight_POS(string wt, bool bMsg)
        {
            double TempWeight = fnDouble(wt);
            if ((TempWeight < 0) || ((TempWeight > 0) && (Settings.MaxScaleWeight > 0) && (TempWeight > Settings.MaxScaleWeight)))
            {
                if (bMsg)
                    DocMessage.MsgInformation(Properties.Resources.Removeallweightsfromscaleandrezeroit);
                return false;
            }
            else
            {
                return true;
            }
        }

        // Check for Valid Live Weight (Scale Module)
        public static bool IsValidScaleWeight_Scale(string wt, bool bMsg)
        {
            double TempWeight = fnDouble(wt);
            if ((TempWeight < 0) || ((TempWeight > 0) && (Settings.MaxScaleWeight > 0) && (TempWeight > Settings.MaxScaleWeight)))
            {
                if (bMsg)
                    ResMan.MessageBox.Show(Properties.Resources.Removeallweightsfromscaleandrezeroit, "", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        // Previous Weight Format Hard Code
        public static string SetWeightAccuracy1(string whstring)
        {
            string FormatValue = "";
            decimal returnval = 0;
            decimal wh = fnDecimal(whstring);
            if (wh <= (decimal)Settings.Grad_S_Range2)
            {
                decimal intPart = Math.Truncate(wh);
                decimal decPart = wh - intPart;
                if (decPart > 0)
                {
                    int l = decPart.ToString().Length;
                    string ld = "";
                    try
                    {
                        ld = decPart.ToString().Substring(4, 1);
                    }
                    catch
                    {
                        returnval = wh;
                    }
                    if ((ld == "0") || (ld == "5"))
                    {
                        returnval = wh;
                    }
                    if (ld == "1")
                    {
                        returnval = intPart + decPart - (decimal)0.001;
                    }
                    if (ld == "2")
                    {
                        returnval = intPart + decPart + (decimal)0.003;
                    }
                    if (ld == "3")
                    {
                        returnval = intPart + decPart + (decimal)0.002;
                    }
                    if (ld == "4")
                    {
                        returnval = intPart + decPart + (decimal)0.001;
                    }
                    if (ld == "6")
                    {
                        returnval = intPart + decPart - (decimal)0.001;
                    }
                    if (ld == "7")
                    {
                        returnval = intPart + decPart + (decimal)0.003;
                    }
                    if (ld == "8")
                    {
                        returnval = intPart + decPart + (decimal)0.002;
                    }
                    if (ld == "9")
                    {
                        returnval = intPart + decPart + (decimal)0.001;
                    }
                }
                else
                {
                    returnval = wh;
                }

                FormatValue = String.Format("{0:0.000}", returnval);
            }
            else
            {
                decimal intPart = Math.Truncate(wh);
                decimal decPart = wh - intPart;
                if (decPart > 0)
                {
                    int l = decPart.ToString().Length;
                    string lf = "";
                    string ld = "";
                    try
                    {
                        lf = decPart.ToString().Substring(2, 1);
                        ld = decPart.ToString().Substring(3, 2);
                    }
                    catch
                    {
                        returnval = wh;
                    }
                    if ((ld == "00") || (ld == "10") || (ld == "20") || (ld == "30") || (ld == "40") || (ld == "50") || (ld == "60") || (ld == "70") || (ld == "80") || (ld == "90") || (ld == ""))
                    {
                        returnval = wh;
                    }
                    else
                    {
                        decimal lddbl = fnDecimal(ld);

                        if (lddbl < 10)
                        {
                            returnval = intPart + fnDecimal("." + lf) + (decimal)0.01;
                        }
                        if ((lddbl > 10) && (lddbl < 20))
                        {
                            returnval = intPart + fnDecimal("." + lf) + (decimal)0.02;
                        }
                        if ((lddbl > 20) && (lddbl < 30))
                        {
                            returnval = intPart + fnDecimal("." + lf) + (decimal)0.03;
                        }
                        if ((lddbl > 30) && (lddbl < 40))
                        {
                            returnval = intPart + fnDecimal("." + lf) + (decimal)0.04;
                        }
                        if ((lddbl > 40) && (lddbl < 50))
                        {
                            returnval = intPart + fnDecimal("." + lf) + (decimal)0.05;
                        }
                        if ((lddbl > 50) && (lddbl < 60))
                        {
                            returnval = intPart + fnDecimal("." + lf) + (decimal)0.06;
                        }
                        if ((lddbl > 60) && (lddbl < 70))
                        {
                            returnval = intPart + fnDecimal("." + lf) + (decimal)0.07;
                        }
                        if ((lddbl > 70) && (lddbl < 80))
                        {
                            returnval = intPart + fnDecimal("." + lf) + (decimal)0.08;
                        }
                        if ((lddbl > 80) && (lddbl < 90))
                        {
                            returnval = intPart + fnDecimal("." + lf) + (decimal)0.09;
                        }
                        if (lddbl > 90)
                        {
                            returnval = intPart + fnDecimal("." + lf) + (decimal)0.1;
                        }
                    }
                }
                else
                {
                    returnval = wh;
                }

                FormatValue = String.Format("{0:0.00}", returnval);
            }

            return FormatValue;
        }

        // Weight Format From Scale Graduation
        public static string SetWeightAccuracy(string whstring)
        {


            string FormatValue = "";
            decimal returnval = 0;
            decimal wh = fnDecimal(whstring);

            if ((Settings.Grad_S_Range1 == 0) && (Settings.Grad_S_Range2 == 0) && (Settings.Grad_D_Range1 == 0) && (Settings.Grad_D_Range2 == 0))
            {
                // No Setup
                FormatValue = String.Format("{0:0.000}", wh);
            }
            else
            {
                if (wh <= (decimal)Settings.Grad_S_Range2)
                {
                    if ((decimal)Settings.Grad_S_Graduation == 0)
                    {
                        returnval = wh;
                        //FormatValue = String.Format( Settings.S_Check2Digit == "Y" ? "{0:0.00}" : "{0:0.000}", wh);
                    }
                    else
                    {
                        decimal TempValue = fnDecimal(wh.ToString(Settings.S_Check2Digit == "Y" ? "f3" : "f4"));

                        decimal div = fnDecimal((TempValue % (decimal)Settings.Grad_S_Graduation).ToString(Settings.S_Check2Digit == "Y" ? "f3" : "f4"));

                        if (div != 0)
                        {
                            if (div < fnDecimal(((decimal)Settings.Grad_S_Graduation / 2).ToString(Settings.S_Check2Digit == "Y" ? "f3" : "f4")))
                            {
                                returnval = TempValue - div;
                            }
                            else
                            {
                                returnval = TempValue + ((decimal)Settings.Grad_S_Graduation - div);
                            }
                        }
                        else
                        {
                            returnval = TempValue;
                        }

                        if (returnval == (decimal)Settings.Grad_D_Range1)
                        {
                            returnval = returnval - (decimal)Settings.Grad_S_Graduation;
                        }
                        FormatValue = String.Format(Settings.S_Check2Digit == "Y" ? "{0:0.00}" : "{0:0.000}", returnval);
                    }
                }
                else
                {
                    if ((decimal)Settings.Grad_D_Graduation == 0)
                    {
                        returnval = wh;
                        //FormatValue = String.Format(Settings.D_Check2Digit == "Y" ? "{0:0.00}" : "{0:0.000}", wh);
                    }
                    else
                    {
                        decimal TempValue = fnDecimal(wh.ToString(Settings.D_Check2Digit == "Y" ? "f3" : "f4"));

                        decimal div = fnDecimal((TempValue % (decimal)Settings.Grad_D_Graduation).ToString(Settings.D_Check2Digit == "Y" ? "f3" : "f4"));

                        if (div != 0)
                        {
                            if (div < fnDecimal(((decimal)Settings.Grad_D_Graduation / 2).ToString(Settings.D_Check2Digit == "Y" ? "f3" : "f4")))
                            {
                                returnval = TempValue - div;
                            }
                            else
                            {
                                returnval = TempValue + ((decimal)Settings.Grad_D_Graduation - div);
                            }
                        }
                        else
                        {
                            returnval = TempValue;
                        }

                        FormatValue = String.Format(Settings.D_Check2Digit == "Y" ? "{0:0.00}" : "{0:0.000}", returnval);
                    }
                }
            }

            return FormatValue;
        }

        // Scale Tare Format
        public static string GetFormattedScaleGraduation(bool Format2, double valG)
        {
            if (Format2)
            {
                return String.Format("{0:0.00}", valG);
            }
            else
            {
                return String.Format("{0:0.000}", valG);
            }
        }

        // Smart Grocer Log Path
        public static string SmartGrocerDataLogPath()
        {
            string csConnPath = "";
            string strdirpath = "";

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var directory = new DirectoryInfo(documentsPath);
            csConnPath = directory.Parent.FullName;

            if (csConnPath.EndsWith("\\")) strdirpath = csConnPath + SystemVariables.BrandName + "\\SmartGrocer";
            else strdirpath = csConnPath + "\\" + SystemVariables.BrandName + "\\SmartGrocer";


            if (Directory.Exists(strdirpath))
            {
                return strdirpath;
            }
            else
            {
                Directory.CreateDirectory(strdirpath);
                return strdirpath;
            }
        }

        // Price Smart - Remove Decimal in Price
        public static string RemoveDecimal(string val)
        {
            if (Settings.PrintPriceSmartScaleLabel == "N") return val;
            else
            {
                if (val.Contains(".00"))
                {
                    return val.Replace(".00", "");
                }
                else
                {
                    return val;
                }
            }
        }

        #region FTP - General Functions

        public static int FtpSetupTest(string ftpH, string ftpU, string ftpP, string ftpTestFile, string ftpTempPath)
        {
            StreamWriter w = new StreamWriter(ftpTempPath + "\\" + ftpTestFile);
            w.WriteLine("Connect FTP ... "); //No Resource
            w.WriteLine("Upload File ... "); //No Resource
            w.WriteLine(" Testing OK "); //No Resource
            w.Close();
            int retval = 0;
            try
            {
                FtpWebRequest request = WebRequest.Create(new Uri("ftp://" + ftpH + "/" + ftpTestFile)) as FtpWebRequest;
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(ftpU, ftpP);
                byte[] fileContents = File.ReadAllBytes(ftpTempPath + "\\" + ftpTestFile);
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(fileContents, 0, fileContents.Length);
                requestStream.Close();
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                response.Close();
                retval = 1;
            }
            catch
            {
                retval = -1;
            }
            return retval;
        }

        public static void GetFTPFolderBrowse(string ftpH, string ftpU, string ftpP)
        {
            try
            {
                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(new Uri("ftp://" + ftpH + "/"));
                ftpRequest.Credentials = new NetworkCredential(ftpU, ftpP);
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());

                bool finished = false;
                string directoryData = string.Empty;
                StringBuilder remoteFiles = new StringBuilder();

                do
                {
                    directoryData = streamReader.ReadLine();

                    if (directoryData != null)
                    {
                        if (remoteFiles.Length > 0)
                        {
                            remoteFiles.Append("\n");
                        }

                        remoteFiles.AppendFormat("{0}", directoryData); //No Resource
                    }

                    else
                    {
                        finished = true;
                    }
                }
                while (!finished);


            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region General File Path

        public static string GetGeneralTempPath(string TempFolder)
        {
            string strTempPath = "";
            string strdirpath = "";
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var directory = new DirectoryInfo(documentsPath);
            strTempPath = directory.Parent.FullName;

            if (strTempPath.EndsWith("\\")) strdirpath = strTempPath + SystemVariables.BrandName + "\\Temp\\" + TempFolder;
            else strdirpath = strTempPath + "\\" + SystemVariables.BrandName + "\\Temp\\" + TempFolder;

            if (!Directory.Exists(strdirpath))
            {
                Directory.CreateDirectory(strdirpath);
            }

            return strdirpath;
        }

        #endregion

        // Get TCP/IP of the computer

        public static string GetTCPIp()
        {
            string IP = "";
            IPHostEntry host;
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    IP = ip.ToString();
                    break;
                }
            }

            return IP;
        }

        public static bool GetChangePermissionInScaleScreen(string sChangeSecurityCode)
        {
            if (SystemVariables.CurrentUserID <= 0)
            {
                return true;
            }
            else
            {
                bool b = false;
                if (sChangeSecurityCode == "15s1") b = SecurityPermission.AccessScale_ChangeUnitPrice;
                if (sChangeSecurityCode == "15s2") b = SecurityPermission.AccessScale_ChangeTare;
                if (sChangeSecurityCode == "15s3") b = SecurityPermission.AccessScale_ChangeByCount;
                if (sChangeSecurityCode == "15s4") b = SecurityPermission.AccessScale_ChangeProductLife;
                if (sChangeSecurityCode == "15s5") b = SecurityPermission.AccessScale_ChangeShelfLife;
                if (sChangeSecurityCode == "15s6") b = SecurityPermission.AcssScale_ManualWeightForNoScale;
                return b;
            }
        }

        #region Get Files from Local Directory for Fast Scale Label Printing

        public static Image GetScaleLabelLogoForFastPrinting()
        {
            string path = "";
            string dpath = "";
            dpath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);

            if (dpath.EndsWith("\\")) path = dpath + SystemVariables.BrandName + "\\Scale Label Printing\\Logo\\";
            else path = dpath + "\\" + SystemVariables.BrandName + "\\Scale Label Printing\\Logo\\";


            if (Directory.Exists(path))
            {
                try
                {
                    Image img = Image.FromFile(path + "scale_logo.jpg");
                    return img;
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static Image GetScaleGraphicsForFastPrinting(string refID)
        {
            string path = "";
            string dpath = "";
            dpath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);

            if (dpath.EndsWith("\\")) path = dpath + SystemVariables.BrandName + "\\Scale Label Printing\\Graphics\\";
            else path = dpath + "\\" + SystemVariables.BrandName + "\\Scale Label Printing\\Graphics\\";


            if (Directory.Exists(path))
            {
                try
                {
                    Image img = Image.FromFile(path + refID + ".jpg");
                    return img;
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static string GetScaleLabelFormatForFastPrinting(string refID)
        {
            string path = "";
            string dpath = "";
            dpath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);

            if (dpath.EndsWith("\\")) path = dpath + SystemVariables.BrandName + "\\Scale Label Printing\\Label Format\\";
            else path = dpath + "\\" + SystemVariables.BrandName + "\\Scale Label Printing\\Label Format\\";


            if (Directory.Exists(path))
            {
                return path + refID + ".dat";
            }
            else
            {
                return "";
            }
        }

        #endregion

        #region Save Files into Local Directory for Fast Scale Label Printing

        public static void SaveScaleLabelLogoInLocalFolder(Image img)
        {
            string path = "";
            string dpath = "";
            dpath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);

            if (dpath.EndsWith("\\")) path = dpath + SystemVariables.BrandName + "\\Scale Label Printing\\Logo\\";
            else path = dpath + "\\" + SystemVariables.BrandName + "\\Scale Label Printing\\Logo\\";


            if (Directory.Exists(path))
            {
                try
                {
                    img.Save(path + "scale_logo.jpg");
                    return;
                }
                catch
                {
                    return;
                }
            }
            else
            {
                Directory.CreateDirectory(path);
                img.Save(path + "scale_logo.jpg");
                return;
            }
        }

        public static void SaveScaleGraphicsInLocalFolder()
        {
            string path = "";
            string dpath = "";
            dpath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);

            if (dpath.EndsWith("\\")) path = dpath + SystemVariables.BrandName + "\\Scale Label Printing\\Graphics\\";
            else path = dpath + "\\" + SystemVariables.BrandName + "\\Scale Label Printing\\Graphics\\";


            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string strSQLComm = "select ID, GraphicArt from GraphicArts ";

            SqlConnection objSQLCon = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, objSQLCon);


            try
            {
                if (objSQLCon.State == System.Data.ConnectionState.Closed) { objSQLCon.Open(); }
                SqlDataReader objSQLReader = null;
                objSQLReader = objSQlComm.ExecuteReader();
                while (objSQLReader.Read())
                {
                    try
                    {
                        byte[] content = (byte[])objSQLReader["GraphicArt"];
                        MemoryStream stream = new MemoryStream(content);
                        Image img = Image.FromStream(stream);
                        img.Save(path + objSQLReader["ID"].ToString() + ".jpg");
                    }
                    catch
                    {
                    }
                }


                objSQLReader.Close();
                objSQlComm.Dispose();
                objSQLCon.Close();
                objSQLCon.Dispose();

            }
            catch
            {
                objSQlComm.Dispose();
                objSQLCon.Close();
                objSQLCon.Dispose();
            }
            return;



        }

        public static void SaveScaleLabelFormatInLocalFolder()
        {
            string path = "";
            string dpath = "";
            dpath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);

            if (dpath.EndsWith("\\")) path = dpath + SystemVariables.BrandName + "\\Scale Label Printing\\Label Format\\";
            else path = dpath + "\\" + SystemVariables.BrandName + "\\Scale Label Printing\\Label Format\\";


            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string strSQLComm = "select FormatID, LabelFormat from LabelFormats ";

            SqlConnection objSQLCon = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, objSQLCon);


            try
            {
                if (objSQLCon.State == System.Data.ConnectionState.Closed) { objSQLCon.Open(); }
                SqlDataReader objSQLReader = null;
                objSQLReader = objSQlComm.ExecuteReader();
                while (objSQLReader.Read())
                {
                    LabelFormatToFile(fnInt32(objSQLReader["FormatID"].ToString()), path + objSQLReader["FormatID"].ToString() + ".dat");
                }


                objSQLReader.Close();
                objSQlComm.Dispose();
                objSQLCon.Close();
                objSQLCon.Dispose();

            }
            catch
            {
                objSQlComm.Dispose();
                objSQLCon.Close();
                objSQLCon.Dispose();
            }
            return;



        }

        public static void LabelFormatToFile(int RefID, string RefPath)
        {

            string varPathToNewLocation = RefPath;

            using (var varConnection = new SqlConnection(SystemVariables.ConnectionString))

            using (var sqlQuery = new SqlCommand(@"select LabelFormat from LabelFormats where FormatID = @ID ", varConnection))
            {
                sqlQuery.Parameters.AddWithValue("@ID", RefID);

                if (varConnection.State == System.Data.ConnectionState.Open) { varConnection.Close(); }
                varConnection.Open();
                using (var sqlQueryResult = sqlQuery.ExecuteReader())
                    if (sqlQueryResult != null)
                    {
                        sqlQueryResult.Read();
                        var blob = new Byte[(sqlQueryResult.GetBytes(0, 0, null, 0, int.MaxValue))];
                        sqlQueryResult.GetBytes(0, 0, blob, 0, blob.Length);
                        using (var fs = new FileStream(varPathToNewLocation, FileMode.Create, FileAccess.Write))
                            fs.Write(blob, 0, blob.Length);
                    }
                varConnection.Close();
                varConnection.Dispose();
            }
        }

        #endregion

        #region Scale Functions
        /// Price Smart Currency Symbol Display
        public static string PriceSmartDisplay()
        {
            return Settings.PrintPriceSmartScaleLabel == "N" ? "$" : Settings.PriceSmartCurrency;
        }
        /// Second Monitor Display of Scale Screen Display Data 
        public static void SecondMonitorDisplayFromTextEditValue(string WeightType, string ProductName, TextEdit edtTare, TextEdit edtNetWeight, TextEdit edtUnitPrice,
                                                                    TextEdit edtTotalPrice)
        {
            SecondMonitor.AddScaleInfo(ProductName, edtTare.Text == "0" ? "" : edtTare.Text.Replace(" lb", ""), edtNetWeight.Text.Contains("oz") ? edtNetWeight.Text : edtNetWeight.Text.Replace(" lb", ""),
                         WeightType == "By Count" ? true : false, edtUnitPrice.Text.Replace(PriceSmartDisplay(), "").Replace("/lb", ""),
                         edtTotalPrice.Text.Replace(PriceSmartDisplay(), ""), WeightType == "Fixed Weight" ? true : false);

        }

        /// Unit Price Text Display in Scale Screen
        public static string DisplayUnitPriceText(string WeightType, double UnitPrice, int ByCount, string Unit)
        {
            string returnval = "";
            if (WeightType == "By Count") returnval = PriceSmartDisplay() + FormatDoubleForPrint(fnDouble((UnitPrice / ByCount).ToString()).ToString("0.0#")) + Unit;
            if (WeightType == "Fixed Weight") returnval = PriceSmartDisplay() + FormatDoubleForPrint(UnitPrice.ToString("0.0#")) + Unit;
            if (WeightType == "Random Weight") returnval = PriceSmartDisplay() + FormatDoubleForPrint(UnitPrice.ToString("0.0#")) + Unit;
            return returnval;
        }

        /// Total Price Text Display in Scale Screen
        public static string DisplayTotalPriceText(string WeightType, double UnitPrice, int ByCount)
        {
            string returnval = "";
            if (WeightType == "By Count") returnval = PriceSmartDisplay() + FormatDoubleForPrint((fnDouble((UnitPrice / ByCount).ToString()) * ByCount).ToString("0.0#"));
            if (WeightType == "Fixed Weight") returnval = PriceSmartDisplay() + FormatDoubleForPrint(FormatDoubleForPrint(UnitPrice.ToString("0.0#")));
            return returnval;
        }
        /// Total Price Text Display for Random Weighted Item in Scale Screen
        public static string DisplayRandomWeightTotalPriceText(double UnitPrice, TextEdit edtWeight)
        {
            return PriceSmartDisplay() + FormatDoubleForPrint((UnitPrice * fnDouble(edtWeight.Text.Replace(" lb", ""))).ToString("0.0#"));
        }

        /// Fetch Scale Logo from database
        public static bool GetScaleLogoFromTable(ref Image img)
        {
            bool retval = false;
            string strSQLComm = "";
            strSQLComm = "select ScaleLogo from StoreInfo ";

            SqlConnection objSQLCon = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objSQlComm = new SqlCommand(strSQLComm, objSQLCon);

            try
            {
                if (objSQLCon.State == System.Data.ConnectionState.Closed) { objSQLCon.Open(); }
                byte[] content = (byte[])objSQlComm.ExecuteScalar();
                try
                {
                    MemoryStream stream = new MemoryStream(content);
                    img = Image.FromStream(stream);

                    retval = true;
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message.ToString());
                    //MessageBox.Show(ex.StackTrace.ToString ()); 
                }
                objSQlComm.Dispose();
                objSQLCon.Close();
                objSQLCon.Dispose();
            }
            catch
            {
                objSQlComm.Dispose();
                objSQLCon.Close();
                objSQLCon.Dispose();
            }
            return retval;
        }

        /// Save Scale Label Prining Data into Local Folder for Fast Printing 
        public static void SaveScaleLabelDataForFastPrinting_OnScaleScreenLoad()
        {
            try
            {
                Image img = null;
                if (GetScaleLogoFromTable(ref img))
                {
                    if (img != null)
                    {
                        SaveScaleLabelLogoInLocalFolder(img);
                    }
                }

                SaveScaleGraphicsInLocalFolder();
                SaveScaleLabelFormatInLocalFolder();
            }
            catch
            {
            }
        }


        // Julian Date fromat from Integer
        public static string GetJulianDateFormat(int val)
        {
            int yr = DateTime.Today.Year;
            DateTime startdate = new DateTime(yr, 1, 1);
            TimeSpan ts = DateTime.Today.Date.Subtract(startdate);
            int dy = ts.Days + val;
            return dy.ToString().PadLeft(3, '0');
        }

        // Julian Date fromat from Datetime
        public static string GetJulianDateFormat(DateTime dt)
        {
            int yr = dt.Year;
            DateTime startdate = new DateTime(yr, 1, 1);
            TimeSpan ts = dt.Subtract(startdate);
            int dy = ts.Days;
            return dy.ToString().PadLeft(3, '0');
        }

        // Calculate Date Difference from Julian Format 
        public static string GetDateFromJulianFormat(string JF)
        {
            int val = GeneralFunctions.fnInt32(JF);
            int yr = DateTime.Today.Year;
            DateTime startdate = new DateTime(yr, 1, 1);
            TimeSpan ts = DateTime.Today.Date.Subtract(startdate);
            int dy = ts.Days;
            int diff = val - dy;
            return DateTime.Today.Date.AddDays(diff).ToString();
        }

        /// Shelf Life date formatting
        public static string FormatShelfLifeDate(bool bJulian, string sDateValue)
        {
            string returnval = "";
            if (bJulian)
            {
                if (Settings.ShelfLifeDateExtend == "Y") returnval = GetJulianDateFormat(fnInt32(sDateValue) + 1);
                if (Settings.ShelfLifeDateExtend == "N") returnval = GetJulianDateFormat(fnInt32(sDateValue));
            }
            else
            {
                if (fnInt32(sDateValue) == 0)
                {
                    if (Settings.ShelfLifeDateExtend == "Y") returnval = DateTime.Today.Date.AddDays(1).ToString(Settings.PrintPriceSmartScaleLabel == "N" ? "MMM d, yy" : "dd/MM/yy");
                    if (Settings.ShelfLifeDateExtend == "N") returnval = DateTime.Today.Date.ToString(Settings.PrintPriceSmartScaleLabel == "N" ? "MMM d, yy" : "dd/MM/yy");
                }
                else
                {
                    if (Settings.ShelfLifeDateExtend == "Y") returnval = DateTime.Today.AddDays(fnInt32(sDateValue)).ToString(Settings.PrintPriceSmartScaleLabel == "N" ? "MMM d, yy" : "dd/MM/yy");
                    if (Settings.ShelfLifeDateExtend == "N") returnval = DateTime.Today.AddDays(fnInt32(sDateValue) - 1).ToString(Settings.PrintPriceSmartScaleLabel == "N" ? "MMM d, yy" : "dd/MM/yy");
                }
            }
            return returnval;
        }
        /// Product Life date formatting
        public static string FormatProductLifeDate(bool bJulian, string sDateValue)
        {
            string returnval = "";
            if (bJulian)
            {
                returnval = GetJulianDateFormat(fnInt32(sDateValue));
            }
            else
            {
                if (fnInt32(sDateValue) == 0)
                {
                    returnval = DateTime.Today.Date.ToString(Settings.PrintPriceSmartScaleLabel == "N" ? "MMM d, yy" : "dd/MM/yy");
                }
                else
                {
                    returnval = DateTime.Today.AddDays(fnInt32(sDateValue) - 1).ToString(Settings.PrintPriceSmartScaleLabel == "N" ? "MMM d, yy" : "dd/MM/yy");
                }
            }
            return returnval;
        }

        /// Returns No. of digits after decimal
        public static int CountDigitsAfterDecimal(double value)
        {
            bool start = false;
            int count = 0;
            foreach (var s in value.ToString())
            {
                if (s == '.')
                {
                    start = true;
                }
                else if (start)
                {
                    count++;
                }
            }

            return count;
        }

        #endregion

        #region Markdown Price Display

        public static string DisplayMarkdownPriceForPrint(double dblVal)
        {
            return PriceSmartDisplay() + FormatDoubleForPrint(dblVal.ToString("0.0#"));
        }

        #endregion

        #region GPPrinter for die cart printing

        public static bool CheckForDiecutLabelPrinting(alabel1 albl)
        {
            if (Settings.GPPrinter_Use == "Y")
            {
                try
                {
                    float labelWidth = albl.LabWidth / 300;
                    float labelHeight = albl.LabHeight / 300;

                    float labelFooterHeight = (float)Settings.GPPrinter_LabelFooter;

                    string GPrinterWidth = (labelWidth * 25.4).ToString("f2");
                    string GPrinterHeight = ((labelHeight + labelFooterHeight) * 25.4).ToString("f2");
                    TSCLIB_DLL.openport(Settings.GPPrinter_PrinterName);
                    TSCLIB_DLL.setup(GPrinterWidth, GPrinterHeight, "4", "8", "0", "0", "0");
                    TSCLIB_DLL.closeport();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        #endregion

        #region Digi Scale Export

        // Create Digi Scale Export Path if not found

        public static string DigiScaleExportPath_InitialData()
        {
            string csConnPath = "";
            string strdirpath = "";

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var directory = new DirectoryInfo(documentsPath);
            csConnPath = directory.Parent.FullName;

            if (csConnPath.EndsWith("\\")) strdirpath = csConnPath + SystemVariables.BrandName + "\\Digi\\Initial Data";
            else strdirpath = csConnPath + "\\" + SystemVariables.BrandName + "\\Digi\\Initial Data";


            if (Directory.Exists(strdirpath))
            {
                return strdirpath;
            }
            else
            {
                Directory.CreateDirectory(strdirpath);
                return strdirpath;
            }
        }

        public static string DigiScaleExportPath_ChangedData()
        {
            string csConnPath = "";
            string strdirpath = "";

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var directory = new DirectoryInfo(documentsPath);
            csConnPath = directory.Parent.FullName;

            if (csConnPath.EndsWith("\\")) strdirpath = csConnPath + SystemVariables.BrandName + "\\Digi\\Changed Data";
            else strdirpath = csConnPath + "\\" + SystemVariables.BrandName + "\\Digi\\Changed Data";


            if (Directory.Exists(strdirpath))
            {
                return strdirpath;
            }
            else
            {
                Directory.CreateDirectory(strdirpath);
                return strdirpath;
            }
        }

        // Check if Digi Scale Export Path exists
        public static string DigiScaleExportPath_InitialDataExists()
        {
            string csConnPath = "";
            string strdirpath = "";

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var directory = new DirectoryInfo(documentsPath);
            csConnPath = directory.Parent.FullName;

            if (csConnPath.EndsWith("\\")) strdirpath = csConnPath + SystemVariables.BrandName + "\\Digi\\Initial Data";
            else strdirpath = csConnPath + "\\" + SystemVariables.BrandName + "\\Digi\\Initial Data";


            if (Directory.Exists(strdirpath))
            {
                return strdirpath;
            }
            else
            {

                return "";
            }
        }

        public static string DigiScaleExportPath_ChangedDataExists()
        {
            string csConnPath = "";
            string strdirpath = "";

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var directory = new DirectoryInfo(documentsPath);
            csConnPath = directory.Parent.FullName;

            if (csConnPath.EndsWith("\\")) strdirpath = csConnPath + SystemVariables.BrandName + "\\Digi\\Changed Data";
            else strdirpath = csConnPath + "\\" + SystemVariables.BrandName + "\\Digi\\Changed Data";


            if (Directory.Exists(strdirpath))
            {
                return strdirpath;
            }
            else
            {

                return "";
            }
        }

        #endregion

        #region Masking On DateFormat

        public static void PhoneMaskNew(DevExpress.XtraEditors.TextEdit txtPhone)
        {
            if (SystemVariables.Country == "USA")
            {
                txtPhone.Properties.Mask.EditMask = "(999)000-0000";
            }
            if (SystemVariables.Country == "UK")
            {
                txtPhone.Properties.Mask.EditMask = "00000 000000";
            }
            if (SystemVariables.Country == "CANADA")
            {
                txtPhone.Properties.Mask.EditMask = "000-000-0000";
            }
            if (SystemVariables.Country == "EURO")
            {
                txtPhone.Properties.Mask.EditMask = "00000 000000";
            }
        }

        #endregion

        public static string SendEmailOnAppointmentBooking(string ContactName, string ContactEmail, string AppointmentHeader, string AppointmentMessage)
        {
            bool boolFlag = false;


            string SMTP_HOST = "";
            string SMTP_PORT = "";
            string SMTP_USER = "";
            string SMTP_PSWD = "";
            string SMTP_SLL = "";

            string FROM_EMAIL = "";
            string FROM_NAME = "";

            string REPLY_TO = "";

            string BRAND_NAME = "";

            SMTP_HOST = Settings.REHost;
            SMTP_USER = Settings.REUser;
            SMTP_PSWD = Settings.REPassword;
            SMTP_PORT = Settings.REPort.ToString();
            SMTP_SLL = Settings.RESSL;
            FROM_EMAIL = Settings.REFromAddress;
            FROM_NAME = Settings.REFromName;
            REPLY_TO = Settings.REReplyTo;
            BRAND_NAME = SystemVariables.BrandName;

            if (SMTP_HOST == "")
            {
                return "";
            }
            else
            {

                try
                {
                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                    smtp.Host = SMTP_HOST;
                    smtp.Port = GeneralFunctions.fnInt32(SMTP_PORT);
                    smtp.EnableSsl = SMTP_SLL == "Y";
                    smtp.Credentials = new System.Net.NetworkCredential(SMTP_USER, SMTP_PSWD);
                    System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                    msg.From = (new System.Net.Mail.MailAddress(FROM_EMAIL, FROM_NAME));
                    msg.To.Add(new System.Net.Mail.MailAddress(ContactEmail, ContactName));
                    if (REPLY_TO != "")
                    {
                        msg.ReplyToList.Add(REPLY_TO);
                    }

                    msg.IsBodyHtml = true;
                    msg.Subject = AppointmentHeader;
                    msg.Body = AppointmentMessage + "<br/><br/><br/><br/><br/><br/><font size='2'>This email is generated from <b>" + BRAND_NAME + "</b></font>";
                    smtp.Send(msg);
                    msg.Dispose();
                    boolFlag = true;
                }
                catch
                {
                    boolFlag = false;
                }
                return boolFlag ? "success" : "fail";
            }

        }

        #region Set Currency and Date Format from Country Configaration

        public static void SetCurrencyNDateformat()
        {
            try
            {
                SystemVariables.DateFormat = Settings.DateFormat;
                SystemVariables.CurrencySymbol = Settings.CurrencySymbol;
            }
            catch
            {
                SystemVariables.DateFormat = "MM/dd/yyyy";
                SystemVariables.CurrencySymbol = "$";
            }
        }

        #endregion

        #region New Functions

        public static void SetRecordCountStatus(int TotalRecords, System.Windows.Controls.TextBlock block)
        {
            if (block == null) { return; }
            if (TotalRecords < 0) { block.Text = " "; }
            else if (TotalRecords == 0) { block.Text = "No Record"; }
            else if (TotalRecords == 1) { block.Text = TotalRecords.ToString() + " " + "Record"; }
            else if (TotalRecords > 1) { block.Text = TotalRecords.ToString() + " " + "Records"; }
        }

        public static void LoadPhotofromDB(string PhotoFor, int PhotoRef, System.Windows.Controls.Image img)
        {
            string strSQLComm = "";
            if (PhotoFor == "Product")
                strSQLComm = "SELECT ProductPhoto FROM Product WHERE ID = @ID";
            else if (PhotoFor == "Customer")
                strSQLComm = "SELECT CustomerPhoto FROM Customer WHERE ID = @ID";
            else if (PhotoFor == "Employee")
                strSQLComm = "SELECT EmployeePhoto FROM Employee WHERE ID = @ID";
            else if (PhotoFor == "Logo")
                strSQLComm = "SELECT CompanyLogo FROM StoreInfo";

            using (SqlConnection objSQLCon = new SqlConnection(SystemVariables.ConnectionString))
            using (SqlCommand objSQlComm = new SqlCommand(strSQLComm, objSQLCon))
            {
                if (PhotoFor != "Logo")
                {
                    objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int) { Value = PhotoRef });
                }

                try
                {
                    objSQLCon.Open();
                    object result = objSQlComm.ExecuteScalar();

                    if (result == DBNull.Value || result == null)
                    {
                        img.Source = null; // No image found, clear the UI image
                        return;
                    }

                    byte[] content = (byte[])result;
                    using (MemoryStream stream = new MemoryStream(content))
                    {
                        System.Windows.Media.Imaging.BitmapImage bi = new System.Windows.Media.Imaging.BitmapImage();
                        bi.BeginInit();
                        bi.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad;
                        bi.StreamSource = stream;
                        bi.EndInit();
                        bi.Freeze(); // Make it UI-thread safe

                        img.Source = bi;
                    }
                }
                catch (Exception ex)
                {
                    img.Source = null; // Set to null if an error occurs
                    Console.WriteLine($"Error loading image: {ex.Message}");
                }
            }
        }

        //public static void LoadPhotofromDB(string PhotoFor, int PhotoRef, System.Windows.Controls.Image img)
        //{

        //    string strSQLComm = "";

        //    if (PhotoFor == "Product")
        //    {
        //        strSQLComm = "select ProductPhoto from Product where ID = @ID";
        //    }
        //    if (PhotoFor == "Customer")
        //    {
        //        strSQLComm = "select CustomerPhoto from Customer where ID = @ID";
        //    }
        //    if (PhotoFor == "Employee")
        //    {
        //        strSQLComm = "select EmployeePhoto from Employee where ID = @ID";
        //    }

        //    if (PhotoFor == "Logo")
        //    {
        //        strSQLComm = "select CompanyLogo from StoreInfo ";
        //    }

        //    SqlConnection objSQLCon = new SqlConnection(SystemVariables.ConnectionString);
        //    SqlCommand objSQlComm = new SqlCommand(strSQLComm, objSQLCon);
        //    if (PhotoFor != "Logo")
        //    {
        //        objSQlComm.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
        //        objSQlComm.Parameters["@ID"].Value = PhotoRef;
        //    }
        //    try
        //    {
        //        if (objSQLCon.State == System.Data.ConnectionState.Closed) { objSQLCon.Open(); }
        //        byte[] content = (byte[])objSQlComm.ExecuteScalar();
        //        try
        //        {
        //            // assign byte array data into memory stream 
        //            MemoryStream stream = new MemoryStream(content);

        //            // set transparent bitmap with 32 X 32 size by memory stream data 
        //            Bitmap b = new Bitmap(stream);
        //            Bitmap output = new Bitmap(b, new System.Drawing.Size(32, 32));
        //            output.MakeTransparent();

        //            System.Windows.Media.Imaging.BitmapImage bi = new System.Windows.Media.Imaging.BitmapImage();
        //            bi.BeginInit();
        //            System.Drawing.Image tempImage = (System.Drawing.Image)output;
        //            MemoryStream ms = new MemoryStream();
        //            tempImage.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

        //            stream.Seek(0, SeekOrigin.Begin);

        //            bi.StreamSource = stream;

        //            bi.EndInit();

        //            img.Source = bi;

        //        }
        //        catch (Exception ex)
        //        {
        //            img.Source = null;
        //        }
        //        finally
        //        {
        //        }
        //        objSQlComm.Dispose();
        //        objSQLCon.Close();
        //        objSQLCon.Dispose();
        //    }
        //    catch
        //    {
        //        objSQlComm.Dispose();
        //        objSQLCon.Close();
        //        objSQLCon.Dispose();
        //    }
        //}

        public static byte[] ConvertBitmapSourceToByteArray(System.Windows.Media.ImageSource imageSource)
        {
            var image = imageSource as System.Windows.Media.Imaging.BitmapSource;
            byte[] data;
            System.Windows.Media.Imaging.BitmapEncoder encoder = new System.Windows.Media.Imaging.JpegBitmapEncoder();
            encoder.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(image));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }

        public static void PhoneMaskNew(DevExpress.Xpf.Editors.TextEdit txtPhone)
        {
            try
            {
                if (Settings.CountryName == "USA")
                    txtPhone.Mask = "(999)000-0000";
                else if (Settings.CountryName == "UK")
                    txtPhone.Mask = "00000 000000";
                else if (Settings.CountryName == "Europe")
                    txtPhone.Mask = "00000 000000";
                else if (Settings.CountryName == "Canada")
                    txtPhone.Mask = "000-000-0000";
                else
                    txtPhone.Mask = "";

            }
            catch
            {
                txtPhone.Mask = "";
            }

        }

        public static DataTable GetQuickTenderingCurrencies()
        {
            DataTable dtbl = new DataTable();

            dtbl.Columns.Add("CurrencyName", System.Type.GetType("System.String"));
            dtbl.Columns.Add("CurrencyValue", System.Type.GetType("System.Int32"));
            dtbl.Columns.Add("DisplayOrder", System.Type.GetType("System.Int32"));

            int iOrder = 0;

            if ((Settings.Currency10Name != "") && (Settings.Currency10QuickTender == "Y"))
            {
                iOrder++;
                dtbl.Rows.Add(new object[] { Settings.CurrencySymbol + "1000", 1000, iOrder });
            }

            if ((Settings.Currency9Name != "") && (Settings.Currency9QuickTender == "Y"))
            {
                iOrder++;
                dtbl.Rows.Add(new object[] { Settings.CurrencySymbol + "500", 500, iOrder });
            }

            if ((Settings.Currency8Name != "") && (Settings.Currency8QuickTender == "Y"))
            {
                iOrder++;
                dtbl.Rows.Add(new object[] { Settings.CurrencySymbol + "200", 200, iOrder });
            }

            if ((Settings.Currency7Name != "") && (Settings.Currency7QuickTender == "Y"))
            {
                iOrder++;
                dtbl.Rows.Add(new object[] { Settings.CurrencySymbol + "100", 100, iOrder });
            }

            if ((Settings.Currency6Name != "") && (Settings.Currency6QuickTender == "Y"))
            {
                iOrder++;
                dtbl.Rows.Add(new object[] { Settings.CurrencySymbol + "50", 50, iOrder });
            }

            if ((Settings.Currency5Name != "") && (Settings.Currency5QuickTender == "Y"))
            {
                iOrder++;
                dtbl.Rows.Add(new object[] { Settings.CurrencySymbol + "20", 20, iOrder });
            }

            if ((Settings.Currency4Name != "") && (Settings.Currency4QuickTender == "Y"))
            {
                iOrder++;
                dtbl.Rows.Add(new object[] { Settings.CurrencySymbol + "10", 10, iOrder });
            }

            if ((Settings.Currency3Name != "") && (Settings.Currency3QuickTender == "Y"))
            {
                iOrder++;
                dtbl.Rows.Add(new object[] { Settings.CurrencySymbol + "5", 5, iOrder });
            }

            if ((Settings.Currency2Name != "") && (Settings.Currency2QuickTender == "Y"))
            {
                iOrder++;
                dtbl.Rows.Add(new object[] { Settings.CurrencySymbol + "2", 2, iOrder });
            }

            if ((Settings.Currency1Name != "") && (Settings.Currency1QuickTender == "Y"))
            {
                iOrder++;
                dtbl.Rows.Add(new object[] { Settings.CurrencySymbol + "1", 1, iOrder });
            }

            return dtbl;
        }

        public static DataTable GetTenderingCurrencies()
        {
            DataTable dtbl = new DataTable();

            dtbl.Columns.Add("CurrencyName", System.Type.GetType("System.String"));
            dtbl.Columns.Add("CurrencyValue", System.Type.GetType("System.Int32"));
            dtbl.Columns.Add("DisplayOrder", System.Type.GetType("System.Int32"));

            int iOrder = 0;

            if ((Settings.Currency10Name != ""))
            {
                iOrder++;
                dtbl.Rows.Add(new object[] { Settings.CurrencySymbol + " 1000", 1000, iOrder });
            }

            if ((Settings.Currency9Name != ""))
            {
                iOrder++;
                dtbl.Rows.Add(new object[] { Settings.CurrencySymbol + "500", 500, iOrder });
            }

            if ((Settings.Currency8Name != ""))
            {
                iOrder++;
                dtbl.Rows.Add(new object[] { Settings.CurrencySymbol + "200", 200, iOrder });
            }

            if ((Settings.Currency7Name != ""))
            {
                iOrder++;
                dtbl.Rows.Add(new object[] { Settings.CurrencySymbol + "100", 100, iOrder });
            }

            if ((Settings.Currency6Name != ""))
            {
                iOrder++;
                dtbl.Rows.Add(new object[] { Settings.CurrencySymbol + "50", 50, iOrder });
            }

            if ((Settings.Currency5Name != ""))
            {
                iOrder++;
                dtbl.Rows.Add(new object[] { Settings.CurrencySymbol + "20", 20, iOrder });
            }

            if ((Settings.Currency4Name != ""))
            {
                iOrder++;
                dtbl.Rows.Add(new object[] { Settings.CurrencySymbol + "10", 10, iOrder });
            }

            if ((Settings.Currency3Name != ""))
            {
                iOrder++;
                dtbl.Rows.Add(new object[] { Settings.CurrencySymbol + "5", 5, iOrder });
            }

            if ((Settings.Currency2Name != ""))
            {
                iOrder++;
                dtbl.Rows.Add(new object[] { Settings.CurrencySymbol + "2", 2, iOrder });
            }

            if ((Settings.Currency1Name != ""))
            {
                iOrder++;
                dtbl.Rows.Add(new object[] { Settings.CurrencySymbol + "1", 1, iOrder });
            }

            return dtbl;
        }

        public static DataTable GetCurrencyDenominations()
        {
            DataTable dtbl = new DataTable();

            dtbl.Columns.Add("CurrencyName", System.Type.GetType("System.String"));
            dtbl.Columns.Add("CurrencyValue", System.Type.GetType("System.Double"));
            dtbl.Columns.Add("CoinOrCurrency", System.Type.GetType("System.Int32"));
            dtbl.Columns.Add("DisplayOrder", System.Type.GetType("System.Int32"));

            int icoin = 0;
            int inote = 0;
            if (Settings.Coin1Name != "")
            {
                icoin++;
                dtbl.Rows.Add(new object[] { Settings.Coin1Name, 0.01, 1, icoin });
            }

            if (Settings.Coin2Name != "")
            {
                icoin++;
                dtbl.Rows.Add(new object[] { Settings.Coin2Name, 0.02, 1, icoin });
            }

            if (Settings.Coin3Name != "")
            {
                icoin++;
                dtbl.Rows.Add(new object[] { Settings.Coin3Name, 0.05, 1, icoin });
            }

            if (Settings.Coin4Name != "")
            {
                icoin++;
                dtbl.Rows.Add(new object[] { Settings.Coin4Name, 0.1, 1, icoin });
            }

            if (Settings.Coin5Name != "")
            {
                icoin++;
                dtbl.Rows.Add(new object[] { Settings.Coin5Name, 0.2, 1, icoin });
            }

            if (Settings.Coin6Name != "")
            {
                icoin++;
                dtbl.Rows.Add(new object[] { Settings.Coin6Name, 0.25, 1, icoin });
            }

            if (Settings.Coin7Name != "")
            {
                icoin++;
                dtbl.Rows.Add(new object[] { Settings.Coin7Name, 0.5, 1, icoin });
            }

            if (Settings.Currency1Name != "")
            {
                inote++;
                dtbl.Rows.Add(new object[] { Settings.Currency1Name, 1, 2, inote });
            }

            if (Settings.Currency2Name != "")
            {
                inote++;
                dtbl.Rows.Add(new object[] { Settings.Currency2Name, 2, 2, inote });
            }

            if (Settings.Currency3Name != "")
            {
                inote++;
                dtbl.Rows.Add(new object[] { Settings.Currency3Name, 5, 2, inote });
            }

            if (Settings.Currency4Name != "")
            {
                inote++;
                dtbl.Rows.Add(new object[] { Settings.Currency4Name, 10, 2, inote });
            }

            if (Settings.Currency5Name != "")
            {
                inote++;
                dtbl.Rows.Add(new object[] { Settings.Currency5Name, 20, 2, inote });
            }

            if (Settings.Currency6Name != "")
            {
                inote++;
                dtbl.Rows.Add(new object[] { Settings.Currency6Name, 50, 2, inote });
            }

            if (Settings.Currency7Name != "")
            {
                inote++;
                dtbl.Rows.Add(new object[] { Settings.Currency7Name, 100, 2, inote });
            }

            if (Settings.Currency8Name != "")
            {
                inote++;
                dtbl.Rows.Add(new object[] { Settings.Currency8Name, 200, 2, inote });
            }

            if (Settings.Currency9Name != "")
            {
                inote++;
                dtbl.Rows.Add(new object[] { Settings.Currency9Name, 500, 2, inote });
            }

            if (Settings.Currency10Name != "")
            {
                inote++;
                dtbl.Rows.Add(new object[] { Settings.Currency10Name, 1000, 2, inote });
            }

            return dtbl;
        }

        public static class SendKeys
        {
            /// <summary>
            ///   Sends the specified key.
            /// </summary>
            /// <param name="key">The key.</param>
            public static void Send(Key key, string keyval)
            {
                if (Keyboard.PrimaryDevice != null)
                {


                    if (Keyboard.PrimaryDevice.ActiveSource != null)
                    {
                        //var e1 = new KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, key) { RoutedEvent = Keyboard.PreviewKeyDownEvent };
                        //char xxx = Convert.ToChar(e1.Key);
                        if (key == Key.Delete)
                        {
                            (Keyboard.FocusedElement as System.Windows.Controls.TextBox).SelectAll();
                            var e = new System.Windows.Input.KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, key) { RoutedEvent = Keyboard.KeyDownEvent };
                            InputManager.Current.ProcessInput(e);
                        }
                        else if (key == Key.Back)
                        {
                            var e1 = new System.Windows.Input.KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, key) { RoutedEvent = Keyboard.PreviewKeyDownEvent };
                            InputManager.Current.ProcessInput(e1);
                        }
                        else
                        {
                            TextCompositionManager.StartComposition(
                          new TextComposition(InputManager.Current, Keyboard.FocusedElement as System.Windows.Controls.TextBox, keyval));
                        }
                        /*var e1 = new KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, key) { RoutedEvent = Keyboard.KeyDownEvent };
                        bool bl = InputManager.Current.ProcessInput(e1);
                        if (bl)
                        {
                            (Keyboard.FocusedElement as TextBox).Text = (Keyboard.FocusedElement as TextBox).Text + keyval;
                            (Keyboard.FocusedElement as TextBox).CaretIndex = (Keyboard.FocusedElement as TextBox).Text.Length;
                        }*/
                    }
                }
            }

            public static void SendSpecial(Key key, string keyval)
            {
                if (Keyboard.PrimaryDevice != null)
                {


                    if (Keyboard.PrimaryDevice.ActiveSource != null)
                    {
                        //var e1 = new KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, key) { RoutedEvent = Keyboard.PreviewKeyDownEvent };
                        //char xxx = Convert.ToChar(e1.Key);
                        if (key == Key.Delete)
                        {
                            //(Keyboard.FocusedElement as DevExpress.Xpf.Editors.TextEdit).SelectAll();
                            // var e = new System.Windows.Input.KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, key) { RoutedEvent = Keyboard.KeyDownEvent };
                            //InputManager.Current.ProcessInput(e);
                            TextCompositionManager.StartComposition(
                          new TextComposition(InputManager.Current, Keyboard.FocusedElement as DevExpress.Xpf.Editors.TextEdit, "0.00"));
                        }
                        else if (key == Key.Back)
                        {
                            var e1 = new System.Windows.Input.KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, key) { RoutedEvent = Keyboard.PreviewKeyDownEvent };
                            InputManager.Current.ProcessInput(e1);
                        }
                        else
                        {
                           TextCompositionManager.StartComposition(
                          new TextComposition(InputManager.Current, Keyboard.FocusedElement as DevExpress.Xpf.Editors.TextEdit, keyval));


                            
                            //
                        }
                        /*var e1 = new KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, key) { RoutedEvent = Keyboard.KeyDownEvent };
                        bool bl = InputManager.Current.ProcessInput(e1);
                        if (bl)
                        {
                            (Keyboard.FocusedElement as TextBox).Text = (Keyboard.FocusedElement as TextBox).Text + keyval;
                            (Keyboard.FocusedElement as TextBox).CaretIndex = (Keyboard.FocusedElement as TextBox).Text.Length;
                        }*/
                    }
                }
            }


            public static void SendNumeric(Key key, string keyval)
            {
                if (Keyboard.PrimaryDevice != null)
                {


                    if (Keyboard.PrimaryDevice.ActiveSource != null)
                    {
                        //var e1 = new KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, key) { RoutedEvent = Keyboard.PreviewKeyDownEvent };
                        //char xxx = Convert.ToChar(e1.Key);
                        if (key == Key.Delete)
                        {
                            //(Keyboard.FocusedElement as DevExpress.Xpf.Editors.TextEdit).SelectAll();
                            // var e = new System.Windows.Input.KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, key) { RoutedEvent = Keyboard.KeyDownEvent };
                            //InputManager.Current.ProcessInput(e);
                            TextCompositionManager.StartComposition(
                          new TextComposition(InputManager.Current, Keyboard.FocusedElement as DevExpress.Xpf.Editors.TextEdit, "0.00"));
                        }
                        /*else if (key == Key.Back)
                        {
                            var e1 = new System.Windows.Input.KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, key) { RoutedEvent = Keyboard.PreviewKeyDownEvent };
                            InputManager.Current.ProcessInput(e1);
                        }*/
                        else
                        {
                            TextCompositionManager.StartComposition(
                           new TextComposition(InputManager.Current, Keyboard.FocusedElement as DevExpress.Xpf.Editors.TextEdit, keyval));



                            //
                        }
                        /*var e1 = new KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, key) { RoutedEvent = Keyboard.KeyDownEvent };
                        bool bl = InputManager.Current.ProcessInput(e1);
                        if (bl)
                        {
                            (Keyboard.FocusedElement as TextBox).Text = (Keyboard.FocusedElement as TextBox).Text + keyval;
                            (Keyboard.FocusedElement as TextBox).CaretIndex = (Keyboard.FocusedElement as TextBox).Text.Length;
                        }*/
                    }
                }
            }


            public static void SendFromNumberKeypad(Key key, string keyval,System.Windows.Controls.TextBox tb)
            {
                if (Keyboard.PrimaryDevice != null)
                {


                    if (Keyboard.PrimaryDevice.ActiveSource != null)
                    {
                        //var e1 = new KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, key) { RoutedEvent = Keyboard.PreviewKeyDownEvent };
                        //char xxx = Convert.ToChar(e1.Key);
                        if (key == Key.Delete)
                        {
                            //(Keyboard.FocusedElement as DevExpress.Xpf.Editors.TextEdit).SelectAll();
                            // var e = new System.Windows.Input.KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, key) { RoutedEvent = Keyboard.KeyDownEvent };
                            //InputManager.Current.ProcessInput(e);
                            TextCompositionManager.StartComposition(
                          new TextComposition(InputManager.Current, tb, "0.00"));
                        }
                        else if (key == Key.Back)
                        {
                            var e1 = new System.Windows.Input.KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, key) { RoutedEvent = Keyboard.PreviewKeyDownEvent };
                            InputManager.Current.ProcessInput(e1);
                        }
                        else
                        {
                            TextCompositionManager.StartComposition(
                          new TextComposition(InputManager.Current, tb, keyval));
                        }
                        /*var e1 = new KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, key) { RoutedEvent = Keyboard.KeyDownEvent };
                        bool bl = InputManager.Current.ProcessInput(e1);
                        if (bl)
                        {
                            (Keyboard.FocusedElement as TextBox).Text = (Keyboard.FocusedElement as TextBox).Text + keyval;
                            (Keyboard.FocusedElement as TextBox).CaretIndex = (Keyboard.FocusedElement as TextBox).Text.Length;
                        }*/
                    }
                }
            }
        }


        public static class SendKeysFullKybd
        {
            /// <summary>
            ///   Sends the specified key.
            /// </summary>
            /// <param name="key">The key.</param>
            public static void Send(Key key, string keyval, bool pswd)
            {
                if (Keyboard.PrimaryDevice != null)
                {


                    if (Keyboard.PrimaryDevice.ActiveSource != null)
                    {
                        //var e1 = new KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, key) { RoutedEvent = Keyboard.PreviewKeyDownEvent };
                        //char xxx = Convert.ToChar(e1.Key);
                        if (key == Key.Delete)
                        {
                            (Keyboard.FocusedElement as TextBox).SelectAll();
                            var e = new System.Windows.Input.KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, key) { RoutedEvent = Keyboard.KeyDownEvent };
                            InputManager.Current.ProcessInput(e);
                        }
                        else if (key == Key.Back)
                        {
                            var e1 = new System.Windows.Input.KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, key) { RoutedEvent = Keyboard.PreviewKeyDownEvent };
                            InputManager.Current.ProcessInput(e1);
                        }
                        else
                        {
                            if (!pswd)
                            {
                                TextCompositionManager.StartComposition(
                              new TextComposition(InputManager.Current, Keyboard.FocusedElement as System.Windows.Controls.TextBox, keyval));
                            }
                            else
                            {
                                TextCompositionManager.StartComposition(
                              new TextComposition(InputManager.Current, Keyboard.FocusedElement as System.Windows.Controls.PasswordBox, keyval));
                            }
                        }
                        /*var e1 = new KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, key) { RoutedEvent = Keyboard.KeyDownEvent };
                        bool bl = InputManager.Current.ProcessInput(e1);
                        if (bl)
                        {
                            (Keyboard.FocusedElement as TextBox).Text = (Keyboard.FocusedElement as TextBox).Text + keyval;
                            (Keyboard.FocusedElement as TextBox).CaretIndex = (Keyboard.FocusedElement as TextBox).Text.Length;
                        }*/
                    }
                }
            }
        }

        public static DataTable GetQuickTenderingCurrencies_TenderScreen()
        {
            DataTable dtbl = new DataTable();

            dtbl.Columns.Add("CurrencyName", System.Type.GetType("System.String"));
            dtbl.Columns.Add("CurrencyValue", System.Type.GetType("System.Int32"));
            dtbl.Columns.Add("DisplayOrder", System.Type.GetType("System.Int32"));

            int iOrder = 0;

            if ((Settings.Currency1Name != "") && (Settings.Currency1QuickTender == "Y"))
            {
                iOrder++;
                dtbl.Rows.Add(new object[] { Settings.CurrencySymbol + " 1", 1, iOrder });
            }

            if ((Settings.Currency2Name != "") && (Settings.Currency2QuickTender == "Y"))
            {
                iOrder++;
                dtbl.Rows.Add(new object[] { Settings.CurrencySymbol + " 2", 2, iOrder });
            }

            if ((Settings.Currency3Name != "") && (Settings.Currency3QuickTender == "Y"))
            {
                iOrder++;
                dtbl.Rows.Add(new object[] { Settings.CurrencySymbol + " 5", 5, iOrder });
            }

            if ((Settings.Currency4Name != "") && (Settings.Currency4QuickTender == "Y"))
            {
                iOrder++;
                dtbl.Rows.Add(new object[] { Settings.CurrencySymbol + " 10", 10, iOrder });
            }

            if ((Settings.Currency5Name != "") && (Settings.Currency5QuickTender == "Y"))
            {
                iOrder++;
                dtbl.Rows.Add(new object[] { Settings.CurrencySymbol + " 20", 20, iOrder });
            }

            if ((Settings.Currency6Name != "") && (Settings.Currency6QuickTender == "Y"))
            {
                iOrder++;
                dtbl.Rows.Add(new object[] { Settings.CurrencySymbol + " 50", 50, iOrder });
            }

            if ((Settings.Currency7Name != "") && (Settings.Currency7QuickTender == "Y"))
            {
                iOrder++;
                dtbl.Rows.Add(new object[] { Settings.CurrencySymbol + " 100", 100, iOrder });
            }

            if ((Settings.Currency8Name != "") && (Settings.Currency8QuickTender == "Y"))
            {
                iOrder++;
                dtbl.Rows.Add(new object[] { Settings.CurrencySymbol + " 200", 200, iOrder });
            }


            if ((Settings.Currency9Name != "") && (Settings.Currency9QuickTender == "Y"))
            {
                iOrder++;
                dtbl.Rows.Add(new object[] { Settings.CurrencySymbol + " 500", 500, iOrder });
            }

            if ((Settings.Currency10Name != "") && (Settings.Currency10QuickTender == "Y"))
            {
                iOrder++;
                dtbl.Rows.Add(new object[] { Settings.CurrencySymbol + "1000", 1000, iOrder });
            }

            return dtbl;
        }


        public static void SetWpfFont(System.Windows.Controls.TextBlock tb, string fFamily, string fSize, string B, string I)
        {
            if (fFamily == "") fFamily = "Open Sans";
            if (fSize == "") fSize = "11";
            if (fSize == "0") fSize = "11";
            if (B == "") B = "N";
            if (I == "") I = "N";

            tb.FontFamily = new System.Windows.Media.FontFamily(fFamily);
            tb.FontSize = fnDouble(fSize);

            if ((B == "N") && (I == "N"))
            {
                tb.FontStyle = FontStyles.Normal;
                tb.FontWeight = FontWeights.Normal;
            }
            else if ((B == "Y") && (I == "N"))
            {
                tb.FontStyle = FontStyles.Normal;
                tb.FontWeight = FontWeights.Bold;
            }
            else if ((B == "N") && (I == "Y"))
            {
                tb.FontStyle = FontStyles.Italic;
                tb.FontWeight = FontWeights.Normal;
            }
            else
            {
                tb.FontStyle = FontStyles.Italic;
                tb.FontWeight = FontWeights.Bold;
            }

        }


        public static bool ExecuteCopyConfigFile()
        {
            bool boolprocced = false;
            string userpath = "";
            userpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (userpath.EndsWith("\\"))
            {
                userpath = userpath + "XEPOSRetail2020";
            }
            else
            {
                userpath = userpath + "\\XEPOSRetail2020";
            }

            if (!Directory.Exists(userpath))
            {
                Directory.CreateDirectory(userpath);
            }

            string app_config_filepath = Assembly.GetExecutingAssembly().Location + ".config";

            DateTime app_config_file_LastUpdateTime = File.GetLastWriteTime(app_config_filepath);

            string user_config_filepath = userpath + "\\OfflineRetailV2.exe.config";

            if (!File.Exists(user_config_filepath))
            {
                try
                {
                    File.Copy(app_config_filepath, user_config_filepath, true);
                    boolprocced = true;
                }
                catch
                {

                }
            }
            else
            {
                DateTime user_config_file_LastUpdateTime = File.GetLastWriteTime(user_config_filepath);

                if (app_config_file_LastUpdateTime > user_config_file_LastUpdateTime)
                {
                    try
                    {
                        File.Copy(app_config_filepath, user_config_filepath, true);
                        boolprocced = true;
                    }
                    catch
                    {
                    }
                }
                else
                {
                    boolprocced = true;
                }
            }

            return boolprocced;
        }

        public static string GetUserConfigFile()
        {
            string userpath = "";
            userpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (userpath.EndsWith("\\"))
            {
                userpath = userpath + "XEPOSRetail2020";
            }
            else
            {
                userpath = userpath + "\\XEPOSRetail2020";
            }

            return userpath + "\\OfflineRetailV2.exe.config";
        }

        #endregion

        #region 

        public static System.Windows.Media.ImageSource GetImageStripForBrowseGrid()
        {
            return new System.Windows.Media.Imaging.BitmapImage(new Uri("/Resources/Images/indicator.png", UriKind.Relative));
        }


        public static byte[] GetImageAsByteArray()
        {
            var streamResourceInfo =
            System.Windows.Application.GetResourceStream(new Uri("OfflineRetailV2;component/Resources/Images/indicator.png",
            UriKind.Relative));
            byte[] image = { };
            if (streamResourceInfo != null)
            {
                var length = streamResourceInfo.Stream.Length;
                image = new byte[length];
                streamResourceInfo.Stream.Read(image, 0, (int)length);
            }
            return image;
        }

        public static byte[] GetReportImageAsByteArray()
        {
            var streamResourceInfo =
            System.Windows.Application.GetResourceStream(new Uri("OfflineRetailV2;component/Resources/Images/file-alt-blue.png",
            UriKind.Relative));
            byte[] image = { };
            if (streamResourceInfo != null)
            {
                var length = streamResourceInfo.Stream.Length;
                image = new byte[length];
                streamResourceInfo.Stream.Read(image, 0, (int)length);
            }
            return image;
        }

        #endregion


        #region User Customization

        public static int GetUserCustomizationCount()
        {
            int intRecCount = 0;

            string strSQL = " select count(*) as rcnt from usercustomization where userID = " + SystemVariables.CurrentUserID.ToString();
            SqlConnection objConnection = new SqlConnection(SystemVariables.ConnectionString);
            SqlCommand objCommand = new SqlCommand(strSQL, objConnection);
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Open) { objConnection.Close(); }
                objConnection.Open();

                SqlDataReader objReader = null;
                objReader = objCommand.ExecuteReader();
                if (objReader.Read())
                {
                    intRecCount = GeneralFunctions.fnInt32(objReader["rcnt"].ToString());
                }
                objReader.Close();

                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();

                return intRecCount;
            }
            catch
            {
                objConnection.Close();
                objConnection.Dispose();
                objCommand.Dispose();
                return 0;
            }
        }

        public static string AddUserCustomizationParameters(string val1)
        {
            string strSQLComm = " insert into usercustomization( UserID, POSFunctionButtonShowHideState)"
                                + " values ( @user,@param) ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlConnection objConnection = new SqlConnection(SystemVariables.ConnectionString);
            try
            {

                objSQlComm = new SqlCommand(strSQLComm, objConnection);
                if (objConnection.State == System.Data.ConnectionState.Closed) { objConnection.Open(); }

                objSQLTran = objConnection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@user", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@param", System.Data.SqlDbType.Char));

                objSQlComm.Parameters["@user"].Value = SystemVariables.CurrentUserID;
                objSQlComm.Parameters["@param"].Value = val1;

                objSQlComm.ExecuteNonQuery();

                objSQLTran.Commit();
                objSQlComm.Dispose();
                objConnection.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                objConnection.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
            finally
            {
                objConnection.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        public static string UpdateUserCustomizationParameters(string val1)
        {
            string strSQLComm = " update usercustomization set POSFunctionButtonShowHideState = @param where UserID = @user ";

            SqlCommand objSQlComm = null;
            SqlTransaction objSQLTran = null;
            SqlConnection objConnection = new SqlConnection(SystemVariables.ConnectionString);
            try
            {

                objSQlComm = new SqlCommand(strSQLComm, objConnection);
                if (objConnection.State == System.Data.ConnectionState.Closed) { objConnection.Open(); }

                objSQLTran = objConnection.BeginTransaction();
                objSQlComm.Transaction = objSQLTran;

                objSQlComm.Parameters.Add(new SqlParameter("@user", System.Data.SqlDbType.Int));
                objSQlComm.Parameters.Add(new SqlParameter("@param", System.Data.SqlDbType.Char));

                objSQlComm.Parameters["@user"].Value = SystemVariables.CurrentUserID;
                objSQlComm.Parameters["@param"].Value = val1;

                objSQlComm.ExecuteNonQuery();

                objSQLTran.Commit();
                objSQlComm.Dispose();
                objConnection.Close();
                return "";
            }
            catch (SqlException SQLDBException)
            {
                string strErrMsg = "";
                objSQLTran.Rollback();
                objConnection.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
                strErrMsg = SQLDBException.Message;
                return strErrMsg;
            }
            finally
            {
                objConnection.Close();
                objSQlComm.Dispose();
                objSQLTran.Dispose();
            }
        }

        #endregion


        public static string GetQBCCSVPath()
        {
            string csConnPath = "";
            string strfilename = "";
            string strdirpath = "";


            //csConnPath = Application.StartupPath;

            csConnPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (csConnPath.EndsWith("\\"))
            {
                strdirpath = csConnPath + "XEPOSRetail2020\\QB Cloud";
            }
            else
            {
                strdirpath = csConnPath + "\\XEPOSRetail2020\\QB Cloud";
            }
            if (Directory.Exists(strdirpath))
            {
                strfilename = strdirpath + "\\";
            }
            else
            {
                Directory.CreateDirectory(strdirpath);
                strfilename = strdirpath + "\\";
            }
            return strfilename;
        }

        public static bool ExecuteCopyLabelDesign()
        {
            bool boolprocced = false;
            string userpath = "";
            userpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (userpath.EndsWith("\\"))
            {
                userpath = userpath + "XEPOSRetail2020\\LabelPrinting";
            }
            else
            {
                userpath = userpath + "\\XEPOSRetail2020\\LabelPrinting";
            }

            string app_config_filepath = Assembly.GetExecutingAssembly().Location.Replace("OfflineRetailV2.exe", "");

            if (app_config_filepath.EndsWith("\\"))
            {
                app_config_filepath = app_config_filepath + "LabelPrinting";
            }
            else
            {
                app_config_filepath = app_config_filepath + "\\" + "LabelPrinting";
            }

            if (!Directory.Exists(userpath))
            {
                Directory.CreateDirectory(userpath);

                Directory.CreateDirectory(userpath + "\\OneUp");
            }

            foreach (var srcPath in Directory.GetFiles(app_config_filepath))
            {
                //Copy the file from sourcepath and place into mentioned target path, 
                //Overwrite the file if same file is exist in target path
                File.Copy(srcPath, srcPath.Replace(app_config_filepath, userpath), true);
            }





            /*else
            {
                DateTime user_config_file_LastUpdateTime = File.GetLastWriteTime(user_config_filepath);

                if (app_config_file_LastUpdateTime > user_config_file_LastUpdateTime)
                {
                    try
                    {
                        File.Copy(app_config_filepath, user_config_filepath, true);
                        boolprocced = true;
                    }
                    catch
                    {
                    }
                }
                else
                {
                    boolprocced = true;
                }
            }*/

            return boolprocced;
        }

        public static string GetLabelPrintingDefaultFile(string filename)
        {
            string userpath = "";
            userpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (userpath.EndsWith("\\"))
            {
                userpath = userpath + "XEPOSRetail2020\\LabelPrinting";
            }
            else
            {
                userpath = userpath + "\\XEPOSRetail2020\\LabelPrinting";
            }

            return userpath + "\\" + filename + ".repx";
        }

        public static string CheckLabelPrintingCustomisedFile(string templatetype, string templatename)
        {
            string userpath = "";
            userpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (userpath.EndsWith("\\"))
            {
                userpath = userpath + "XEPOSRetail2020\\LabelPrinting";
            }
            else
            {
                userpath = userpath + "\\XEPOSRetail2020\\LabelPrinting";
            }
            userpath = userpath + "\\" + templatetype;

            if (!Directory.Exists(userpath))
            {
                Directory.CreateDirectory(userpath);
                return "";

            }
            else
            {
                userpath =  userpath + "\\" + templatename + ".repx";

                if (File.Exists(userpath))
                {
                    return userpath;
                }
                else
                {
                    return "";
                }
            }
        }


        public static string GetLabelPrintingCustomisedFilePathForSave(string templatetype, string templatename)
        {
            string userpath = "";
            userpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (userpath.EndsWith("\\"))
            {
                userpath = userpath + "XEPOSRetail2020\\LabelPrinting";
            }
            else
            {
                userpath = userpath + "\\XEPOSRetail2020\\LabelPrinting";
            }
            userpath = userpath + "\\" + templatetype;

            if (!Directory.Exists(userpath))
            {
                Directory.CreateDirectory(userpath);
                
            }
            else
            {
                userpath = userpath + "\\" + templatename + ".repx";
            }

            return userpath;
        }


        public static bool ExecuteCopyBackground()
        {
            bool boolprocced = false;
            string userpath = "";
            userpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (userpath.EndsWith("\\"))
            {
                userpath = userpath + "XEPOSRetail2020\\Background";
            }
            else
            {
                userpath = userpath + "\\XEPOSRetail2020\\Background";
            }

            if (!Directory.Exists(userpath))
            {
                Directory.CreateDirectory(userpath);
            }

            string app_config_filepath = Assembly.GetExecutingAssembly().Location.Replace("OfflineRetailV2.exe", "");

            if (app_config_filepath.EndsWith("\\"))
            {
                app_config_filepath = app_config_filepath + "Background\\Background.png";
            }
            else
            {
                app_config_filepath = app_config_filepath + "\\" + "Background\\Background.png";
            }

            DateTime app_config_file_LastUpdateTime = File.GetLastWriteTime(app_config_filepath);

            string user_config_filepath = userpath + "\\Background.png";

            if (!File.Exists(user_config_filepath))
            {
                try
                {
                    File.Copy(app_config_filepath, user_config_filepath, true);
                    boolprocced = true;
                }
                catch
                {

                }
            }

            /*else
            {
                DateTime user_config_file_LastUpdateTime = File.GetLastWriteTime(user_config_filepath);

                if (app_config_file_LastUpdateTime > user_config_file_LastUpdateTime)
                {
                    try
                    {
                        File.Copy(app_config_filepath, user_config_filepath, true);
                        boolprocced = true;
                    }
                    catch
                    {
                    }
                }
                else
                {
                    boolprocced = true;
                }
            }*/

            return boolprocced;
        }

        public static string GetUserBackground()
        {
            string userpath = "";
            userpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (userpath.EndsWith("\\"))
            {
                userpath = userpath + "XEPOSRetail2020\\Background";
            }
            else
            {
                userpath = userpath + "\\XEPOSRetail2020\\Background";
            }

            return userpath + "\\Background.png";
        }

        public static System.Windows.Media.ImageSource ConvertImageToImageSource(System.Windows.Controls.Image image)
        {
            try
            {
                if (image != null)
                {
                    Bitmap bmpOut = null;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        PngBitmapEncoder encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create((BitmapSource)image.Source));
                        encoder.Save(ms);

                        using (Bitmap bmp = new Bitmap(ms))
                        {
                            bmpOut = new Bitmap(bmp);
                        }
                    }

                    var bitmap = new System.Windows.Media.Imaging.BitmapImage();
                    bitmap.BeginInit();
                    System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();

                    bmpOut.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                    memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
                    bitmap.StreamSource = memoryStream;
                    bitmap.EndInit();
                    return bitmap;
                }
            }
            catch { }
            return null;
        }

        public static string FormatDoubleWithCurrency(double dbl)
        {
            string val = "";
            if (Settings.DecimalPlace == 3)
                val = String.Format("{0:0.000}", dbl);
            else
                val = String.Format("{0:0.00}", dbl);

            if (val.StartsWith("-"))
            {
                string strTemp = val.Remove(0, 1);
                val = "(" + SystemVariables.CurrencySymbol + strTemp + ")";
            }
            else
            {
                val = SystemVariables.CurrencySymbol + val;
            }

            return val;
        }


        #region Xeconnect Log

        private static string LogFilePath(string logF)
        {
            string strfilename = "";
            string strdirpath = "";
            string userpath = "";
            userpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (userpath.EndsWith("\\"))
            {
                userpath = userpath + "XEPOSRetail2020\\XeconnectLog";
            }
            else
            {
                userpath = userpath + "\\XEPOSRetail2020\\XeconnectLog";
            }

            if (!Directory.Exists(userpath))
            {
                Directory.CreateDirectory(userpath);
                strfilename = userpath + "\\" + logF;
            }
            else
            {
                strfilename = userpath + "\\" + logF;
            }


           
            return strfilename;
        }


        public static void ExecuteXeconnectLog(string txt, int Err)
        {
            string logF = "xeconnectlog.txt";
            string logP = LogFilePath(logF);

            FileStream fileStrm;
            if (System.IO.File.Exists(logP)) fileStrm = new FileStream(logP, FileMode.Append, FileAccess.Write);
            else fileStrm = new FileStream(logP, FileMode.OpenOrCreate, FileAccess.Write);

            StreamWriter sw = new StreamWriter(fileStrm);
            if (Err == 0) sw.WriteLine(txt); else sw.WriteLine("Exception: " + Err.ToString() + "   " + txt);
            sw.Close();
            fileStrm.Close();
        }

        #endregion

        public static string GetUserFolderPath(string Subfolders)
        {
            string p = "";
            p = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            p = IncludesLeadingTrails(p) + "XEPOSRetail2020" + Subfolders;
            if (!Directory.Exists(p)) Directory.CreateDirectory(p);
            return p;
        }

        public static string IncludesLeadingTrails(string param)
        {
            if (param.EndsWith("\\")) return param; else return param + "\\";
        }

        public static string DownloadPath()
        {
            string filepath = "";
            filepath = GetUserFolderPath("\\Downloads\\");
            return filepath;
        }

        public static string VersionInfoForUpdate()
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                AssemblyName assemblyname = assembly.GetName();

                string strVersion = assemblyname.Version.Major.ToString() + "." +
                                    assemblyname.Version.Minor.ToString() + "." +
                                    assemblyname.Version.Build.ToString() + "." +
                                    assemblyname.Version.Revision.ToString();
                return strVersion;
            }
            catch
            {
                return "";
            }
        }

        public static string SendEmailOnAppointmentBookingNew(string ContactName, string ContactEmail, string AppointmentProvider, string AppointmentTime)
        {
            bool boolFlag = false;


            string SMTP_HOST = "";
            string SMTP_PORT = "";
            string SMTP_USER = "";
            string SMTP_PSWD = "";
            string SMTP_SLL = "";

            string FROM_EMAIL = "";
            string FROM_NAME = "";

            string REPLY_TO = "";

            string BRAND_NAME = "";

            SMTP_HOST = Settings.REHost;
            SMTP_USER = Settings.REUser;
            SMTP_PSWD = Settings.REPassword;
            SMTP_PORT = Settings.REPort.ToString();
            SMTP_SLL = Settings.RESSL;
            FROM_EMAIL = Settings.REFromAddress;
            FROM_NAME = Settings.REFromName;
            REPLY_TO = Settings.REReplyTo;
            BRAND_NAME = SystemVariables.BrandName;

            if (SMTP_HOST == "")
            {
                return "";
            }
            else
            {

                try
                {
                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                    smtp.Host = SMTP_HOST;
                    smtp.Port = GeneralFunctions.fnInt32(SMTP_PORT);
                    smtp.EnableSsl = SMTP_SLL == "Y";
                    smtp.Credentials = new System.Net.NetworkCredential(SMTP_USER, SMTP_PSWD);
                    System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                    msg.From = (new System.Net.Mail.MailAddress(FROM_EMAIL, FROM_NAME));
                    msg.To.Add(new System.Net.Mail.MailAddress(ContactEmail, ContactName));
                    if (REPLY_TO != "")
                    {
                        msg.ReplyToList.Add(REPLY_TO);
                    }

                    msg.IsBodyHtml = true;
                    msg.Subject = "Your Appointmnet is Confirmed";

                    string attachstr = "";
                    attachstr = attachstr + "<HTML><BODY>";

                    attachstr = attachstr + "<font size='5'>" + Settings.ReportHeader_Company + "</font><br/>";
                    attachstr = attachstr + "<font size='2'><i>" + Settings.ReportHeader_Address.Replace("\r\n", "<br/>") + "</i></font><br/><br/>";

                    attachstr = attachstr + "<font size='2'><strong>" + "Date &amp; Time" + "</strong></font><br/>";
                    attachstr = attachstr + "<font size='2'>" + AppointmentTime + "</font><br/><br/>";

                    attachstr = attachstr + "<font size='2'><strong>" + "Customer" + "</strong></font><br/>";
                    attachstr = attachstr + "<font size='2'>" + ContactName + "</font><br/><br/>";

                    attachstr = attachstr + "<font size='2'><strong>" + "Service Staff" + "</strong></font><br/>";
                    attachstr = attachstr + "<font size='2'>" + AppointmentProvider + "</font><br/><br/>";

                    if (Settings.AppointmentEmailBody != "")
                        attachstr = attachstr + "<font size='2'>" + Settings.AppointmentEmailBody.Replace("\r\n", "<br/>").Replace("&", "&amp;") + " </font><br/><br/>";




                    attachstr = attachstr + "<font size='2'>This email is generated from <b>" + BRAND_NAME + "</b></font>";

                    msg.Body = attachstr;

                    smtp.Send(msg);
                    msg.Dispose();
                    boolFlag = true;
                }
                catch
                {
                    boolFlag = false;
                }
                return boolFlag ? "success" : "fail";
            }

        }

        public static string CheckLengthOfString(string inputstr,int len)
        {
            if (inputstr.Length <= len)
            {
                return inputstr;
            }
            else
            {
                return inputstr.Substring(0, len);
            }
        }
    }
}
