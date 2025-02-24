using Newtonsoft.Json;
using OfflineRetailV2.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PosDataObject
{
    public class BookerPreIntegeration
    {
        private List<BookerTax> BookerTaxList { get; set; }
        private List<BookerDepartment> BookerDepartmentList { get; set; }
        private List<BookerCategory> BookerCategoryList { get; set; }
        private int LinkGL = 2;  //  Sales Tax Account in General Ledger
        public BookerPreIntegeration()
        {
            BookerTaxList = new List<BookerTax>();
            BookerDepartmentList = new List<BookerDepartment>();
            BookerCategoryList = new List<BookerCategory>();
        }

        public void ImportPreMigrationData()
        {
            SetTaxData();
            SetDepartmentData();
            SetCategoryData();


            AddTaxData();
            AddDepartmentData();
            AddCategoryData();
        }

        public void SetCategoryData()
        {
            BookerCategoryList.Add(new BookerCategory("01", "RETAIL BEVERAGES", "01"));
            BookerCategoryList.Add(new BookerCategory("02", "RETAIL SUGAR/SYRUP", "01"));
            BookerCategoryList.Add(new BookerCategory("03", "RETAIL BISCUITS", "01"));
            BookerCategoryList.Add(new BookerCategory("04", "GIFT HAMPERS", "01"));
            BookerCategoryList.Add(new BookerCategory("05", "RETAIL PRESERVES & SPREADS", "01"));
            BookerCategoryList.Add(new BookerCategory("06", "SOFT DRINKS - ON THE GO", "01"));
            BookerCategoryList.Add(new BookerCategory("07", "SOFT DRINKS - TAKE HOME", "01"));
            BookerCategoryList.Add(new BookerCategory("08", "RETAIL AMB/FRUIT/DESSERT/MIL", "01"));
            BookerCategoryList.Add(new BookerCategory("09", "RETAIL CANNED VEGETABLES", "01"));
            BookerCategoryList.Add(new BookerCategory("10", "MAKRO SALES DUMP CODE - PRD", "01"));
            BookerCategoryList.Add(new BookerCategory("11", "RETAIL AMB/MEAT/FISH/RDY MEA", "01"));
            BookerCategoryList.Add(new BookerCategory("12", "CRISPS/SNACKS", "01"));
            BookerCategoryList.Add(new BookerCategory("13", "BABY PRODUCTS", "01"));
            BookerCategoryList.Add(new BookerCategory("14", "RETAIL SOUPS", "01"));
            BookerCategoryList.Add(new BookerCategory("15", "RETAIL BREAKFAST CEREALS", "01"));
            BookerCategoryList.Add(new BookerCategory("16", "RETAIL DRY VEG CEREAL PASTA", "01"));
            BookerCategoryList.Add(new BookerCategory("17", "RETAIL BAKING INGREDIENTS", "01"));
            BookerCategoryList.Add(new BookerCategory("18", "RETAIL COOKING SAUCE/PASTE", "01"));
            BookerCategoryList.Add(new BookerCategory("19", "RETAIL PICKLE/SCE/CONDIMENT", "01"));
            BookerCategoryList.Add(new BookerCategory("20", "RETAIL SAVOURY INGREDIENTS", "01"));
            BookerCategoryList.Add(new BookerCategory("21", "UNUSED", "01"));
            BookerCategoryList.Add(new BookerCategory("22", "UNUSED", "01"));
            BookerCategoryList.Add(new BookerCategory("23", "ETHNIC", "01"));
            BookerCategoryList.Add(new BookerCategory("24", "RETAIL LAUNDRY/WUL/CLEANING", "01"));
            BookerCategoryList.Add(new BookerCategory("25", "UNUSED", "01"));
            BookerCategoryList.Add(new BookerCategory("26", "PAPER PRODUCTS", "01"));
            BookerCategoryList.Add(new BookerCategory("27", "UNUSED", "01"));
            BookerCategoryList.Add(new BookerCategory("28", "HEALTH & BEAUTY", "01"));
            BookerCategoryList.Add(new BookerCategory("29", "PET PRODUCTS", "01"));

            // 2

            BookerCategoryList.Add(new BookerCategory("30", "BUTTERS/SPREADS & FATS", "02"));
            BookerCategoryList.Add(new BookerCategory("31", "CHEESE", "02"));
            BookerCategoryList.Add(new BookerCategory("32", "MILKS, CREAMS & JUICES", "02"));
            BookerCategoryList.Add(new BookerCategory("33", "UNUSED", "02"));
            BookerCategoryList.Add(new BookerCategory("34", "COOKED DELI MEATS", "02"));
            BookerCategoryList.Add(new BookerCategory("35", "BREAD AND CAKES", "02"));
            BookerCategoryList.Add(new BookerCategory("36", "PASTRY/BREAD/PASTA", "02"));
            BookerCategoryList.Add(new BookerCategory("37", "YOGHURTS & DESSERTS", "02"));
            // 03

            BookerCategoryList.Add(new BookerCategory("38", "CATER BEVERAGES", "03"));
            BookerCategoryList.Add(new BookerCategory("39", "CATERING SUGAR/SYRUP", "03"));
            BookerCategoryList.Add(new BookerCategory("40", "CATER SOFT DRINKS/JUICE", "03"));
            BookerCategoryList.Add(new BookerCategory("41", "CATER CANNED VEGETABLES/FISH", "03"));
            BookerCategoryList.Add(new BookerCategory("42", "CATER SOUPS", "03"));
            BookerCategoryList.Add(new BookerCategory("43", "CATER LAUNDRY", "03"));
            BookerCategoryList.Add(new BookerCategory("44", "CATER AMBIENT FRUIT/DESSERTS", "03"));
            BookerCategoryList.Add(new BookerCategory("45", "CATER MEAT/READY MEALS", "03"));
            BookerCategoryList.Add(new BookerCategory("46", "CATER PIE FILLING/PRESERVES", "03"));
            BookerCategoryList.Add(new BookerCategory("47", "CATER BISCUITS", "03"));
            BookerCategoryList.Add(new BookerCategory("48", "CATER BREAKFAST CEREALS", "03"));
            BookerCategoryList.Add(new BookerCategory("49", "CATER BAKING INGREDIENTS", "03"));
            BookerCategoryList.Add(new BookerCategory("50", "CATER COOKING SAUCE/PASTE", "03"));
            BookerCategoryList.Add(new BookerCategory("51", "CATER PICKLE/SAUCE/CONDIMENT", "03"));
            BookerCategoryList.Add(new BookerCategory("52", "CATER SAVOURY INGREDIENTS", "03"));
            BookerCategoryList.Add(new BookerCategory("53", "CATER HOUSEHOLD CLEANERS", "03"));
            BookerCategoryList.Add(new BookerCategory("54", "CATER DRY VEG/CEREALS/PASTA", "03"));
            BookerCategoryList.Add(new BookerCategory("55", "CATER DESSERT MILK WHITENERS", "03"));

            // 04
          // "0", "FROZEN FOOD", "04"));
            BookerCategoryList.Add(new BookerCategory("56", "FROZEN VEGETABLES", "04"));
            BookerCategoryList.Add(new BookerCategory("57", "FROZEN MEAT/POULTRY", "04"));
            BookerCategoryList.Add(new BookerCategory("58", "FROZEN READY MEALS", "04"));
            BookerCategoryList.Add(new BookerCategory("59", "FROZEN PASTRY PRODUCTS", "04"));
            BookerCategoryList.Add(new BookerCategory("60", "FROZEN FISH", "04"));
            BookerCategoryList.Add(new BookerCategory("61", "ICE CREAM", "04"));
            BookerCategoryList.Add(new BookerCategory("62", "FROZEN DESSERTS", "04"));
        
            
            
            //  0", "CONFECTIONERY", "05"));
            BookerCategoryList.Add(new BookerCategory("63", "CHOCOLATE ON THE GO", "05"));
            BookerCategoryList.Add(new BookerCategory("64", "CHOCOLATE TAKE HOME", "05"));
            BookerCategoryList.Add(new BookerCategory("65", "MINTS/GUMS & MEDICATED", "05"));
            BookerCategoryList.Add(new BookerCategory("66", "SPECIAL OCCASION", "05"));
            BookerCategoryList.Add(new BookerCategory("67", "CATERING", "05"));
            BookerCategoryList.Add(new BookerCategory("68", "SUGAR ON THE GO", "05"));
            BookerCategoryList.Add(new BookerCategory("69", "SUGAR TAKE HOME", "05"));
            BookerCategoryList.Add(new BookerCategory("70", "UNUSED", "05"));
            BookerCategoryList.Add(new BookerCategory("71", "SEASONAL", "05"));
         
            
            //  0", "CIGARETTES", "06"));
            BookerCategoryList.Add(new BookerCategory("72", "CIGARETTES", "06"));
            BookerCategoryList.Add(new BookerCategory("73", "CIGARS/OTHER TOBACCOS", "06"));
            BookerCategoryList.Add(new BookerCategory("74", "TOBACCO REQUISITES", "06"));
      
            
            
            // WINES SPIRITS BEERS", "07"));
            BookerCategoryList.Add(new BookerCategory("75", "WINE", "07"));
            BookerCategoryList.Add(new BookerCategory("76", "FORTIFIED/VERMOUTHS/APERITIF", "07"));
            BookerCategoryList.Add(new BookerCategory("77", "ALCOHOLIC RTDS", "07"));
            BookerCategoryList.Add(new BookerCategory("78", "SPIRITS", "07"));
            BookerCategoryList.Add(new BookerCategory("79", "BEERS", "07"));
            BookerCategoryList.Add(new BookerCategory("80", "CIDER", "07"));


            //  MEAT", "08"));
            BookerCategoryList.Add(new BookerCategory("81", "BEEF", "08"));
            BookerCategoryList.Add(new BookerCategory("82", "PORK", "08"));
            BookerCategoryList.Add(new BookerCategory("83", "BACON", "08"));
            BookerCategoryList.Add(new BookerCategory("84", "LAMB", "08"));
            BookerCategoryList.Add(new BookerCategory("85", "POULTRY", "08"));
            BookerCategoryList.Add(new BookerCategory("86", "MEAT/GAME/FISH/EGGS", "08"));
            BookerCategoryList.Add(new BookerCategory("87", "FRESH FISH", "08"));


            //  0", "FRUIT & VEG", "09"));
            BookerCategoryList.Add(new BookerCategory("88", "FRESH FRUIT", "09"));
            BookerCategoryList.Add(new BookerCategory("89", "FRESH VEGETABLES", "09"));

           //  "NON-FOOD", "10"));

            BookerCategoryList.Add(new BookerCategory("90", "FOOD AND DRINK DISPOSABLES", "10"));
            BookerCategoryList.Add(new BookerCategory("91", "COOKING AND KITCHEN EQUIPME", "10"));
            BookerCategoryList.Add(new BookerCategory("92", "TABLE TOP & BAR PRODUCTS", "10"));
            BookerCategoryList.Add(new BookerCategory("93", "GENERAL MAINTENANCE & WORK", "10"));
            BookerCategoryList.Add(new BookerCategory("94", "WORK PLACE & JANITORIAL", "10"));
            BookerCategoryList.Add(new BookerCategory("95", "HYGIENE SYSTEMS", "10"));
            BookerCategoryList.Add(new BookerCategory("96", "RETAILER RESALE", "10"));
            BookerCategoryList.Add(new BookerCategory("97", "OFFICE SUPPLIES & STATIONERY", "10"));
            BookerCategoryList.Add(new BookerCategory("98", "ACCOMODATIONS & FURNISHINGS", "10"));
            BookerCategoryList.Add(new BookerCategory("99", "OUTDOOR, LEISURE & SEASONAL", "10"));

        }
        public void SetDepartmentData()
        {
            BookerDepartmentList.Add(new BookerDepartment("01", "RETAIL GROCERY"));
            BookerDepartmentList.Add(new BookerDepartment("02", "CHILLED"));
            BookerDepartmentList.Add(new BookerDepartment("03", "CATERING GROCERY"));
            BookerDepartmentList.Add(new BookerDepartment("04", "FROZEN FOOD"));
            BookerDepartmentList.Add(new BookerDepartment("05", "CONFECTIONERY"));
            BookerDepartmentList.Add(new BookerDepartment("06", "CIGARETTES"));
            BookerDepartmentList.Add(new BookerDepartment("07", "WINES SPIRITS BEERS"));
            BookerDepartmentList.Add(new BookerDepartment("08", "MEAT"));
            BookerDepartmentList.Add(new BookerDepartment("09", "FRUIT & VEG"));
            BookerDepartmentList.Add(new BookerDepartment("10", "NON-FOOD"));
        }
        public void SetTaxData()
        {
            BookerTaxList.Add(new BookerTax("01", "Sales Tax 0%", 0.0));
            BookerTaxList.Add(new BookerTax("02", "Sales Tax 20%", 20.00));
            BookerTaxList.Add(new BookerTax("03", "Sales Tax 5%", 5.00));
        }
        public void AddTaxData()
        {
            foreach (var bookerTax in BookerTaxList)
            {
                try
                {

                    PosDataObject.Product objProduct = new Product();
                    objProduct.Connection = SystemVariables.Conn;
                    objProduct.LoginUserID = SystemVariables.CurrentUserID;
                    DataTable dtbl = objProduct.ShowBookersTax(bookerTax.BookerID);

                    if (dtbl.Rows.Count != 0)    //  already exists, break whole insertion, as other records have also been inserted
                        continue;//break;
                
                    PosDataObject.Tax objTax = new PosDataObject.Tax();
                    objTax.Connection = SystemVariables.Conn;
                    objTax.LoginUserID = SystemVariables.CurrentUserID;

                    //objTax.SplitDataTable = null;
                    objTax.BookerID = bookerTax.BookerID;
                    objTax.TaxName = bookerTax.TaxName.Trim();
                    objTax.TaxType = 0;    //  Percent
                    objTax.TaxRate = GeneralFunctions.fnDouble(bookerTax.TaxRate);
                    objTax.ErrorMsg = "";
                    objTax.Active = "Yes";
                    objTax.Mode = "Add";

                    objTax.LinkGL = 2;
                    if (Settings.CloseoutExport == "Y")
                        objTax.LinkGL = LinkGL;
                
                    objTax.BeginTransaction();
                    objTax.InsertTax();
                    objTax.EndTransaction();
                }
                catch (Exception ex)
                {
                    var requestParams = JsonConvert.SerializeObject(bookerTax);
                    GeneralFunctions.SetDetailedTransactionLog("Error Booker Import (AddTaxData)", ex.Message + "\n" + requestParams, "");
                }
            }
        }
        public void AddDepartmentData()
        {
            foreach (var bookerDepartment in BookerDepartmentList)
            {
                try
                {
                    PosDataObject.Department objDepartment = new PosDataObject.Department();
                    objDepartment.Connection = SystemVariables.Conn;
                    objDepartment.LoginUserID = SystemVariables.CurrentUserID;

                    DataTable dtbl = objDepartment.ShowBookerRecord(bookerDepartment.BookerID.Trim());
                    if (dtbl.Rows.Count != 0)    //  already exists, break whole insertion, as other records have also been inserted
                        continue;//break;

                    objDepartment.GeneralCode = bookerDepartment.BookerID.Trim();
                    objDepartment.BookerDeptID = bookerDepartment.BookerID.Trim();
                    objDepartment.GeneralDesc = bookerDepartment.DepartmentName.Trim();
                    objDepartment.ID = 0;

                    objDepartment.FoodStamp = "N";
                    objDepartment.ScaleFlag = "N";
                    objDepartment.ScaleScreenVisible = "N";
                    objDepartment.ScaleDisplayOrder = 0;

                    objDepartment.LinkSalesGL = objDepartment.LinkCostGL = objDepartment.LinkPayableGL = objDepartment.LinkInventoryGL = 0;
                    if (Settings.CloseoutExport == "Y")
                        objDepartment.LinkSalesGL = objDepartment.LinkCostGL = objDepartment.LinkPayableGL = objDepartment.LinkInventoryGL = LinkGL;
                
                
                    objDepartment.InsertData();
                }
                catch (Exception ex)
                {
                    var requestParams = JsonConvert.SerializeObject(bookerDepartment);
                    GeneralFunctions.SetDetailedTransactionLog("Error Booker Import (AddDepartmentData)", ex.Message + "\n" + requestParams, "");
                }
            }
        }

        public void AddCategoryData()
        {
            foreach (var bookerCategory in BookerCategoryList)
            {
                try
                {
                    string strError = "";
                    PosDataObject.Category objCategory = new PosDataObject.Category();
                    objCategory.Connection = SystemVariables.Conn;
                    objCategory.LoginUserID = SystemVariables.CurrentUserID;

                    DataTable dtbl = objCategory.ShowBookerRecord(bookerCategory.CategoryID.Trim());
                    if (dtbl.Rows.Count != 0)    //  already exists, break whole insertion, as other records have also been inserted
                        continue;//break;

                    objCategory.GeneralCode = bookerCategory.CategoryID.Trim();
                    objCategory.BookerCategoryID = bookerCategory.CategoryID.Trim();
                    objCategory.BookerDeptID = bookerCategory.DepartmentID.Trim();
                    objCategory.GeneralDesc = bookerCategory.CategoryName.Trim();
                    objCategory.ID = 0;

                    objCategory.FoodStamp = "N";
                    objCategory.PosDisplayOrder = GeneralFunctions.fnInt32(objCategory.MaxPosDisplayOrder() + 1);
                    objCategory.MaxItemsforPOS = 10;
                
                    objCategory.POSBackground = "Color";
                    objCategory.POSScreenColor = "#00000000";
                    objCategory.POSScreenStyle = "";
                
                    objCategory.POSFontType = Settings.DefaultCategoryFontType;
                    objCategory.POSFontSize = GeneralFunctions.fnInt32(Settings.DefaultCategoryFontSize.Trim());
                    objCategory.POSFontColor = "#00000000";
                    objCategory.POSItemFontColor = "#00000000";
                
                    objCategory.FoodStamp = "N";
                    objCategory.IsBold = "N";
                    objCategory.IsItalics = "N";
                    objCategory.AddToPOSScreen = "Y";
                    objCategory.ScaleBarCode = "N";
                    objCategory.FoodStampEligibleForProduct = "N";
                    objCategory.PrintBarCode = "N";
                    objCategory.NoPriceOnLabel = "N";
                    objCategory.AllowZeroStock = "Y";
                    objCategory.DisplayStockinPOS = "Y";
                    objCategory.ProductStatus = "Y";
                    objCategory.LabelType = -1;
                    objCategory.NonDiscountable = "N";
                
                    objCategory.RepairPromptForTag = "N";
                    objCategory.MinimumAge = 0;
                    objCategory.QtyToPrint = 0;

                    objCategory.ImportFrom = "Booker Service";
                    objCategory.TaxDataTable = null;

                    objCategory.PostData();
                }
                catch (Exception ex)
                {
                    var requestParams = JsonConvert.SerializeObject(bookerCategory);
                    GeneralFunctions.SetDetailedTransactionLog("Error Booker Import (AddCategoryData)", ex.Message + "\n" + requestParams, "");
                }

            }

        }

    }

    public class BookerTax
    {
        public string BookerID { get; set; }
        public string TaxName { get; set; }
        public double TaxRate { get; set; }
        public BookerTax(string bookerID, string taxName, double taxRate)
        {
            this.BookerID = bookerID;
            this.TaxName = taxName;
            this.TaxRate = taxRate;
        }
    }
    public class BookerDepartment
    {
        public string BookerID { get; set; }
        public string DepartmentName { get; set; }
        public BookerDepartment(string bookerID, string departmentName)
        {
            this.BookerID = bookerID;
            this.DepartmentName = departmentName;
        }
    }
    public class BookerCategory
    {
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string DepartmentID { get; set; }
        public BookerCategory(string bookerID, string categoryName, string deptBookerID)
        {
            this.CategoryID = bookerID;
            this.CategoryName = categoryName;
            this.DepartmentID = deptBookerID;
        }
    }


}
