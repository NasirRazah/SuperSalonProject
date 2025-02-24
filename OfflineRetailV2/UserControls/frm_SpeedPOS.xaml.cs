/*
        purpose : Main Screen for POS Module
        USER CLASS : Opening Screen for POS Module
 *      note : using product type of cart itemse
 *      
 *      "U" - Unit of Mesure Item 
 *      
 *      "M" - Matrix Item
 *      "W" - Weighted Item
 *      "K" - Kit ItemLoad
 *      "E" - Serialized Item
 *      "F" - Fuel Item
 *      "T" - Tagged Item
 *      "G" - Gift Certificate 
 *      "A" - Account Payment
 *      "C" - Coupon
 *      "H" - Fees on Ticket
 *      "X" - Mercury / Precidia / Datacap / POSLink Gift Card issue
 *      "B" - Blank Item Type ( Not stored in Database )
 *      "O" - Bottle Refund
 *      "S" - Employee Service
 *      "Z" - Special Mix n Match
 *      "Y" - Buy 'n Get Free

 */

using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Grid;
using DevExpress.XtraReports.UI;

using Microsoft.PointOfService;

using OfflineRetailV2.Data;
using OfflineRetailV2.UserControls.POSSection;
using OfflineRetailV2.UserControls.Administrator;
using POSControls;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Xml;
using DevExpress.Xpf.RichEdit;
using System.Globalization;
using DevExpress.Xpf.Printing;
using System.Text.RegularExpressions;
using System.Windows.Markup;
using DevExpress.Office.Utils;
using System.Collections.Generic;
using DevExpress.Xpf.Core.DragAndDrop;
using System.Windows.Documents;


namespace OfflineRetailV2.UserControls
{
    /// <summary>
    /// Interaction logic for POSControl.xaml
    /// </summary>
    public partial class frm_SpeedPOS : System.Windows.Controls.UserControl
    {

        

        bool userControlHasFocus = false;


        FullKeyboard fkybrd;
        bool IsAboutFullKybrdOpen = false;

        private static readonly object padlock = new object();
        
        private bool boolLoadAllItem = false;
        System.Windows.Controls.Button btnsupnd = new System.Windows.Controls.Button();
        System.Windows.Controls.Button btnsvrepiar = new System.Windows.Controls.Button();
        private bool bool_btnsupnd = false;
        private bool bool_btnsvrepiar = false;
        public DispatcherTimer timerdisplay;

        private DataTable dtblFunctionButton = null;

        private int intSetNavBarSetup = 0;


        private int NewPrintCopy = 1;
        private string NewPrinterName = "";
        private int NewTemplateID = 0;
        private string NewTemplateSize = "";
        private bool FindNewTemplate = false;
        private int T_Width = 270;
        private DataTable NewTemplateLinkData = null;

        private bool boolPaidOut = false;

        public frm_SpeedPOS()
        {
            InitializeComponent();

           
        }

       

       





      

        


       

      
     

      


       

      

       

       

      

     

      

       

      

       
        



        
       

       
       
      




       



       

       
      




       

        
    }



    

}
