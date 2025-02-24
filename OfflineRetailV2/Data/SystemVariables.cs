// Sam Park @ Mental Code Master 2019
//-----------------------------------------------------------
using DevExpress.XtraBars;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Resources;
using System.Windows.Forms;

namespace OfflineRetailV2.Data
{
    public class SystemVariables
    {

        private static SqlConnection gConn;
        private static string gConnString;

        private static SqlConnection grmConn;
        private static string grmConnString;

        private static SqlConnection grmBookingConn;
        private static string grmBookingConnString;

        private static SqlConnection gMLConn;
        private static string gMLConnString;

        private static string gLogonCode;
        private static string gLogonName;
        private static DateTime gLogonTime;
        private static string gServerName;
        private static string gDatabaseName;
        private static string gUserName;
        private static string gPassword;
        private static string gLogonUserCode;
        private static string gLogonUserName;
        private static int gLogonUserID;
        private static StatusBarPanel sbp_RecordStatusPanel = null;
        private static StatusBarPanel sbp_RecordStatusPanelofEmp = null;

        private static BarStaticItem bsi_RecordStatusItem = null;
        private static BarStaticItem bsi_RecordStatusItemofEmp = null;

        public const string InvalidLogon = "5758431";
        public const string ValidLogon = "4192542";

        public const int FIXEDGRIDROWS = 20;

        private static string gDIRpath;
        private static string gGMRpath;
        private static string gAPHpath;

        private static int gCloseOutID;

        private static int intSecondMonitorAppID;

        private static string gBrandName;
        private static string gBrandShortName;
        private static string gCurrencySymbol;
        private static string gDateFormat;
        private static string gDateFormatWithTime;
        private static int gPageAdjustmentForPrint;

        private static ResourceManager gRM;

        private static string gCountry;

        private static string gSelectedTheme;

        private static DataTable dtblMenu;

        public SystemVariables()
        {
        }
        //public static string BookersStartUpPath { get; set; } = @"C:\Bookers\In\";

        public static DataTable Menu
        {
            get { return dtblMenu; }
            set { dtblMenu = value; }
        }

        public static ResourceManager RM
        {
            get { return gRM; }
            set { gRM = value; }
        }

        public static SqlConnection MLConn
        {
            get { return gMLConn; }
            set { gMLConn = value; }
        }

        public static string MLConnectionString
        {
            get { return gMLConnString; }
            set { gMLConnString = value; }
        }

        public static SqlConnection Conn
        {
            get { return gConn; }
            set { gConn = value; }
        }

        public static string BrandName
        {
            get { return gBrandName; }
            set { gBrandName = value; }
        }

        public static string BrandShortName
        {
            get { return gBrandShortName; }
            set { gBrandShortName = value; }
        }

        public static string CurrencySymbol
        {
            get { return gCurrencySymbol; }
            set { gCurrencySymbol = value; }
        }

        public static string DateFormat
        {
            get { return gDateFormat; }
            set { gDateFormat = value; }
        }

        public static string DateFormatWithTime
        {
            get { return gDateFormatWithTime; }
            set { gDateFormatWithTime = value; }
        }

        public static int PageAdjustmentForPrint
        {
            get { return gPageAdjustmentForPrint; }
            set { gPageAdjustmentForPrint = value; }
        }

        public static string ConnectionString
        {
            get { return gConnString; }
            set { gConnString = value; }
        }

        public static SqlConnection rmConn
        {
            get { return grmConn; }
            set { grmConn = value; }
        }

        public static string rmConnectionString
        {
            get { return grmConnString; }
            set { grmConnString = value; }
        }

        public static SqlConnection rmBookingConn
        {
            get { return grmBookingConn; }
            set { grmBookingConn = value; }
        }

        public static string rmBookingConnectionString
        {
            get { return grmBookingConnString; }
            set { grmBookingConnString = value; }
        }

        public static int SecondMonitorAppID
        {
            get { return intSecondMonitorAppID; }
            set { intSecondMonitorAppID = value; }
        }

        public static int CloseOutID
        {
            get { return gCloseOutID; }
            set { gCloseOutID = value; }
        }

        public static string LogonCode
        {
            get { return gLogonCode; }
            set { gLogonCode = value; }
        }

        public static string LogonName
        {
            get { return gLogonName; }
            set { gLogonName = value; }
        }

        public static DateTime LogonTime
        {
            get { return gLogonTime; }
            set { gLogonTime = value; }
        }

        public static string DatabaseServerName
        {
            get { return gServerName; }
            set { gServerName = value; }
        }

        public static string DatabaseName
        {
            get { return gDatabaseName; }
            set { gDatabaseName = value; }
        }

        public static string DatabaseUserName
        {
            get { return gUserName; }
            set { gUserName = value; }
        }

        public static string DatabasePassword
        {
            get { return gPassword; }
            set { gPassword = value; }
        }

        public static string CurrentUserName
        {
            get { return gLogonUserName; }
            set { gLogonUserName = value; }
        }

        public static string CurrentUserCode
        {
            get { return gLogonUserCode; }
            set { gLogonUserCode = value; }
        }

        public static int CurrentUserID
        {
            get { return gLogonUserID; }
            set { gLogonUserID = value; }
        }

        public static StatusBarPanel RecordStatusPanel
        {
            get { return sbp_RecordStatusPanel; }
            set { sbp_RecordStatusPanel = value; }
        }

        public static StatusBarPanel RecordStatusPanelofEmp
        {
            get { return sbp_RecordStatusPanelofEmp; }
            set { sbp_RecordStatusPanelofEmp = value; }
        }

        public static BarStaticItem RecordStatusItem
        {
            get { return bsi_RecordStatusItem; }
            set { bsi_RecordStatusItem = value; }
        }

        public static BarStaticItem RecordStatusItemofEmp
        {
            get { return bsi_RecordStatusItemofEmp; }
            set { bsi_RecordStatusItemofEmp = value; }
        }

        public static string DIRpath
        {
            get { return gDIRpath; }
            set { gDIRpath = value; }
        }

        public static string GMRpath
        {
            get { return gGMRpath; }
            set { gGMRpath = value; }
        }

        public static string APHpath
        {
            get { return gAPHpath; }
            set { gAPHpath = value; }
        }

        public static string Country
        {
            get { return gCountry; }
            set { gCountry = value; }
        }

        public static string SelectedTheme
        {
            get { return gSelectedTheme; }
            set { gSelectedTheme = value; }
        }
    }
}