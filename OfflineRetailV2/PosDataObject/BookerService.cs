using BookersIntegration.Service;
using Newtonsoft.Json;
using OfflineRetailV2;
using OfflineRetailV2.Data;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Forms;

namespace PosDataObject
{
    public enum BookerRecordStatus
    {
        Successful,
        Failed,
        Duplicate,
        Not_Found
    }

    public class BookerService
    {
        //  fixed values
        private const string ProductInsert = "06";
        private const string ProductAmendment = "Nodata"; //  "17";

        PosDataObject.Category objCategory = null;
        PosDataObject.Department objDepartment = null;

        BookerHelper bookerHelper = null;


        public BookerService()
        {
            objCategory = new PosDataObject.Category();
            objDepartment = new PosDataObject.Department();

            objCategory.Connection = SystemVariables.Conn;
            objCategory.LoginUserID = SystemVariables.CurrentUserID;
            objDepartment.Connection = SystemVariables.Conn;
            objDepartment.LoginUserID = SystemVariables.CurrentUserID;


            //bookerHelper = new BookerHelper(@"D:\All Code\XEPOS Code\EPOSScrapper\EPOSScrapper\CGN34800.eup");
            //bookerHelper = new BookerHelper(@"D:\All Code\XEPOS Code\EPOSScrapper\EPOSScrapper\HBC26600.EUP");
          //  bookerHelper = new BookerHelper(@"D:\All Code\XEPOS Code\booker integratio\Specs\fwd_fwd_booker_gateway_link\HAG26600.EUP");
        }

        private bool ProcessAllFiles()
        {
            string fileString = String.Empty;
            try
            {
                //var startupPath= SystemVariables.BookersStartUpPath;
                //var startupPath = ConfigurationManager.AppSettings["BookersStartUpDirectory"];
                var startupPath = ConfigSettings.GetBookerDirectory();
                if (Directory.Exists(startupPath) == false)
                {
                    throw new Exception ("Not a valid Directory ==> " + startupPath);
                }
                string[] filePaths = Directory.GetFiles(startupPath);
                foreach (string filePath in filePaths)
                {
                    //  FileInfo fi = new FileInfo(f);
                    //  var path = fi.FullName;
                    fileString = filePath;
                      bookerHelper = new BookerHelper(filePath);

                    StartMigration(filePath);
                }
            }
            catch (Exception ex)
            {
                GeneralFunctions.SetDetailedTransactionLog("Error Booker Import ReadFiles (ProcessAllFiles)", fileString + "==>" + ex.Message, Path.GetFileName(fileString));
                return false;
            }
            return true;

        }

        public bool StartProcess()
        {
            PosDataObject.BookerPreIntegeration bookerIntegeration = new PosDataObject.BookerPreIntegeration();
            bookerIntegeration.ImportPreMigrationData();
            return ProcessAllFiles();

            // StartMigration();

        }

        private void StartMigration(string filePath)
        {
            try
            {
                AddProductsData(filePath);
                UpdatePriceChangeData(filePath);
                AddPromotions(filePath);
                DeleteProductData(filePath);
            }
            catch (Exception ex)
            {
                GeneralFunctions.SetDetailedTransactionLog("Error Booker Import (StartMigration)", filePath + "==>" + ex.Message, Path.GetFileName(filePath));
            }
        }

        public void UpdatePriceChangeData(string filePath)
        {
            Cursor.Current = Cursors.WaitCursor;
            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            objProduct.LoginUserID = SystemVariables.CurrentUserID;

            int totalPriceUpdatedCounter = 0;
            int totalFailedCounter = 0;
            int totalNotFoundCounter = 0;

            foreach (var priceChangeData in bookerHelper.PriceChangeDataList)
            {

                try
                {
                    int intReturn = objProduct.UpdateBookerProductPrice(priceChangeData.BarCode.Trim(), priceChangeData.PriceDecimal);
                    if (intReturn == 0)
                        totalPriceUpdatedCounter++;
                    else if (intReturn == 1)
                        totalNotFoundCounter++;
                    else if (intReturn == -1)
                        totalFailedCounter++;
                }
                catch (Exception ex)
                {
                    var requestParams = JsonConvert.SerializeObject(priceChangeData);
                    GeneralFunctions.SetDetailedTransactionLog("Error Booker Import (UpdatePriceChangeData)", filePath + " ==> " + ex.Message + "\n" + requestParams, Path.GetFileName(filePath));
                }
            }


            Cursor.Current = Cursors.Default;
            string resultMsg = "Total Records = " + bookerHelper.PriceChangeDataList.Count + "\nPrice Updated = " + totalPriceUpdatedCounter +
                "\nNot Found = " + totalNotFoundCounter + "\nFailed = " + totalFailedCounter;
            //new MessageBoxWindow().Show(resultMsg, "Booker Import", MessageBoxButton.OK, MessageBoxImage.Information);
            GeneralFunctions.SetDetailedTransactionLog("Booker Import (UpdatePriceChangeData)", filePath + " ==> " + resultMsg, Path.GetFileName(filePath));

        }

        public void DeleteProductData(string filePath)
        {
            Cursor.Current = Cursors.WaitCursor;

            PosDataObject.Product objProduct = new PosDataObject.Product();
            objProduct.Connection = SystemVariables.Conn;
            objProduct.LoginUserID = SystemVariables.CurrentUserID;

            int totalDeletedCounter = 0;
            int totalFailedCounter = 0;
            int totalNotFoundCounter = 0;
            foreach (var productDeleteData in bookerHelper.ProductDeleteDataList)
            {

                try
                {
                    int intReturn = objProduct.DeleteBookerRecord(productDeleteData.BarCode.Trim());
                    if (intReturn == 0)
                        totalDeletedCounter++;
                    else if (intReturn == 1)
                        totalNotFoundCounter++;
                    else if (intReturn == -1)
                        totalFailedCounter++;
                }
                catch (Exception ex)
                {
                    var requestParams = JsonConvert.SerializeObject(productDeleteData);
                    GeneralFunctions.SetDetailedTransactionLog("Error Booker Import (DeleteProductData)", filePath + " ==> " + ex.Message + "\n" + requestParams, Path.GetFileName(filePath));

                }
            }


            Cursor.Current = Cursors.Default;
            string resultMsg = "Total Products = " + bookerHelper.ProductDeleteDataList.Count + "\nDeleted = " + totalDeletedCounter +
                "\nNot Found = " + totalNotFoundCounter + "\nFailed = " + totalFailedCounter;

            GeneralFunctions.SetDetailedTransactionLog("Booker Import (DeleteProductData)", filePath + " ==> " + resultMsg, Path.GetFileName(filePath));

        }

        public void AddProductsData(string filePath)
        {
            int insertSuccessCounter = 0;
            int insertFailedCounter = 0;
            int insertDuplicateCounter = 0;

            int updateSuccessCounter = 0;
            int updateFailedCounter = 0;
            int updateNotFoundCounter = 0;
            //string duplicateTypes = "";
            int updateProductsCounter = 0;
            bookerHelper.ProductDataList = bookerHelper.ProductDataList.OrderBy(o => o.RecordType).ToList();   //  it will sort the list, first insertion products, then to update 
            //Cursor.Current
            Cursor.Current = Cursors.WaitCursor;

            foreach (var productData in bookerHelper.ProductDataList)
            {
                try
                {
                    PosDataObject.Product objProduct = SetProductDefaults();

                    objProduct.BookerProductID = productData.MIDASProductCode.Trim();
                    objProduct.SKU = productData.BarCode.Trim();
                    objProduct.Description = productData.FullProductDescription.Trim();
                    objProduct.DepartmentID = GetDeptID(productData.DepartmentCode.Trim());
                    objProduct.CategoryID = GetCategoryID(productData.CategoryCode.Trim());
                    objProduct.PriceA = objProduct.Cost = SetPriceFromRecommendedRetailString(productData.RecommendedRetail); //productData.PriceDecimal;
                    objProduct.QtyOnHand = 0;
                    //objProduct.QtyOnHand = Convert.ToInt32(productData.PackQuantity.Trim());
                    objProduct.UOM = productData.PackSize.Trim();

                    SetTaxDetails(objProduct, productData.VATCode);


                    //objProduct.RecommendedRetail = productData.RecommendedRetail;
                    //objProduct.VATCode = productData.VATCode;
                    //objProduct.TillProductDescription = productData.TillProductDescription;
                    //objProduct.RetailBestSeller = productData.RetailBestSeller;
                    //objProduct.SupplierCode = productData.SupplierCode;
                    //objProduct.LinkSKU = productData.ProductLinkCode;
                    //objProduct.SplitMidasCode = productData.SplitMidasCode;


                    objProduct.ImportFrom = "Booker Service";

                    objProduct.AddtoPOSCategoryScreen = "N";
                    objProduct.DiscountedCost = 0;
                    objProduct.ExpiryDate = Convert.ToDateTime(null);
                    BookerRecordStatus status = InsertProduct(objProduct, productData.RecordType);
                    if (productData.RecordType == ProductInsert)
                    {
                        if (status == BookerRecordStatus.Successful)
                            insertSuccessCounter++;
                        else if (status == BookerRecordStatus.Failed)
                            insertFailedCounter++;
                        else if (status == BookerRecordStatus.Duplicate)
                            insertDuplicateCounter++;
                    }
                    else
                    {
                        updateProductsCounter++;

                        if (status == BookerRecordStatus.Successful)
                            updateSuccessCounter++;
                        else if (status == BookerRecordStatus.Failed)
                            updateFailedCounter++;
                        else if (status == BookerRecordStatus.Not_Found)
                            updateNotFoundCounter++;
                        else if (status == BookerRecordStatus.Duplicate)
                        {

                        }
                    }

                }
                catch (Exception ex)
                {
                    var requestParams = JsonConvert.SerializeObject(productData);
                    GeneralFunctions.SetDetailedTransactionLog("Error Booker Import (AddProductsData)", filePath + " ==> " + ex.Message + "\n" + requestParams, Path.GetFileName(filePath));
                }

            }
            Cursor.Current = Cursors.Default;

            string resultMsg = "Total Records: " + (bookerHelper.ProductDataList.Count - updateProductsCounter) + "\nInserted: " +
                insertSuccessCounter + "\nDuplicate: " + insertDuplicateCounter + "\nFailed: " + insertFailedCounter;
            GeneralFunctions.SetDetailedTransactionLog("Booker Import (AddProductsData (Insert))", filePath + " ==> " + resultMsg, Path.GetFileName(filePath));

            resultMsg = "Total Records: " + updateProductsCounter + "\nUpdated: " +
                updateSuccessCounter + "\nNot Found: " + updateNotFoundCounter + "\nFailed: " + updateFailedCounter;
            GeneralFunctions.SetDetailedTransactionLog("Booker Import (AddProductsData (Ammendment))", filePath + " ==> " + resultMsg, Path.GetFileName(filePath));
            //new MessageBoxWindow().Show(resultMsg, "Booker Import", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        
        private double SetPriceFromRecommendedRetailString(string strValue)
        {
            double retVal = 0;
            string strBeforeDecimal = strValue.Substring(0, 5).TrimStart('0');
            strBeforeDecimal = strBeforeDecimal.Length > 0 ? strBeforeDecimal : "0";
            string strAfterDecimal = strValue.Substring(5, 2).TrimStart('0');

            retVal = GeneralFunctions.fnDouble(strBeforeDecimal + "." + strAfterDecimal);
            return retVal;
        }
       

        public void AddPromotions(string filePath)
        {
            Cursor.Current = Cursors.WaitCursor;

            int totalInsertedCounter = 0;
            int totalFailedCounter = 0;
            int totalDuplicateCounter = 0;
            int totalProductNotFoundCounter = 0;

            foreach (var promotionData in bookerHelper.PromotionDataList)
            {
                try
                {
                    int itemID = 0;
                    DataTable dtbl = FetchProductDataTable(promotionData.BarCode.Trim(), promotionData.FullProductDescription.Trim(), out itemID);
                    if (dtbl == null || dtbl.Rows.Count == 0)
                    {
                        totalProductNotFoundCounter++;
                        continue;
                    }

                    PosDataObject.Discounts objClass = new PosDataObject.Discounts();
                    objClass.Connection = SystemVariables.Conn;
                    objClass.LoginUserID = SystemVariables.CurrentUserID;
                    objClass.DiscountName = promotionData.TillProductDescription.Trim();
                    objClass.DiscountDescription = promotionData.FullProductDescription.Trim();

                    objClass.LimitedStartDate = Convert.ToDateTime(promotionData.StartSellingDate.Substring(0, 4) + "-" + promotionData.StartSellingDate.Substring(4, 2) + "-" +
                        promotionData.StartSellingDate.Substring(6, 2));
                    objClass.LimitedEndDate = Convert.ToDateTime(promotionData.EndSellingDate.Substring(0, 4) + "-" + promotionData.EndSellingDate.Substring(4, 2) + "-" +
                        promotionData.EndSellingDate.Substring(6, 2));

                    if (IsDuplicatePromotion(itemID, objClass.LimitedStartDate, objClass.LimitedEndDate))
                    {
                        totalDuplicateCounter++;
                        continue;
                    }

                    objClass.SplitDataTable = dtbl;

                    objClass.DiscountStatus = "Y";
                    objClass.DiscountType = "P";
                    objClass.DiscountAmount = 0;
                    objClass.DiscountPercentage = 0;
                    objClass.DiscountCategory = "A";
                    objClass.BasedOn = 0;
                    objClass.DiscountPlusQty = Convert.ToInt32(promotionData.PackQuantity);
                    objClass.DiscountFamilyID = -1;
                    objClass.LimitedPeriodCheck = "Y";


                    objClass.AbsolutePrice = promotionData.PriceDecimal;

                    objClass.ID = 0;

                    objClass.BeginTransaction();
                    bool ret = objClass.ProcessMixNmatchSetup();
                    if (ret)
                        totalInsertedCounter++;
                    else
                        totalFailedCounter++;
                    objClass.EndTransaction();

                }
                catch (Exception ex)
                {
                    var requestParams = JsonConvert.SerializeObject(promotionData);
                    GeneralFunctions.SetDetailedTransactionLog("Error Booker Import (AddPromotions)", filePath + " ==> " + ex.Message + "\n" + requestParams, Path.GetFileName (filePath));
                }
            }


            Cursor.Current = Cursors.Default;
            string resultMsg = "Total Promotions = " + bookerHelper.PromotionDataList.Count + "\nInserted = " + totalInsertedCounter +
                "\nNot Found Products = " + totalProductNotFoundCounter + "\nDuplicate = " + totalDuplicateCounter + "\nFailed = " + totalFailedCounter;

            GeneralFunctions.SetDetailedTransactionLog("Booker Import (AddPromotions)", filePath + " ==> " + resultMsg, Path.GetFileName(filePath));

        }

        private int GetCategoryID(string bookerCategoryID)
        {
            DataTable dtbl = objCategory.ShowBookerRecord(bookerCategoryID);
            if (dtbl.Rows.Count != 0)
                return Convert.ToInt32(dtbl.Rows[0]["ID"].ToString());
            return Convert.ToInt32(bookerCategoryID);
        }

        private int GetDeptID(string bookerDeptID)
        {
            DataTable dtbl = objDepartment.ShowBookerRecord(bookerDeptID);
            if (dtbl.Rows.Count != 0)
                return Convert.ToInt32(dtbl.Rows[0]["ID"].ToString());
            return Convert.ToInt32(bookerDeptID);
        }
        private BookerRecordStatus InsertProduct(Product objProduct, string recordType)
        {
            BookerRecordStatus bookerRecordStatus;
            //  1st check of already inserted records
            int id = 0;
            bool productExists = IsProductExists(objProduct, out id);

            if (recordType == ProductAmendment)
            {

            }
            string strError = "";
            if (recordType == ProductInsert && productExists == false)  //  insert
            {
                strError = objProduct.PostData_WPF(true);
                if (strError == "")
                    bookerRecordStatus = BookerRecordStatus.Successful;
                else
                    bookerRecordStatus = BookerRecordStatus.Failed;

            }
            else if (recordType == ProductAmendment && productExists)   //  update (ammendment call)
            {
                //  update the product in db

                bookerRecordStatus = BookerRecordStatus.Successful;
                objProduct.ID = id;
                strError = objProduct.PostData_WPF(true);
                if (strError == "")
                    bookerRecordStatus = BookerRecordStatus.Successful;
                else
                    bookerRecordStatus = BookerRecordStatus.Failed;
            }
            else if (productExists)     //  call is for insert and Product already exists
            {
                bookerRecordStatus = BookerRecordStatus.Duplicate;
            }
            else if (recordType == ProductAmendment && productExists == false)  //  call is for update and product does not exists
            {
                bookerRecordStatus = BookerRecordStatus.Not_Found;
            }
            else    //  it coulb be an update call, and product doesn,t exists in db
            {
                bookerRecordStatus = BookerRecordStatus.Failed;
            }

            return bookerRecordStatus;
        }

        private bool IsProductExists(Product objProduct, out int id)
        {
            id = 0;
            DataTable dtblProduct = objProduct.ShowBookerRecord(objProduct.SKU);
            if (dtblProduct.Rows.Count > 0)
            {
                id = Convert.ToInt32(dtblProduct.Rows[0]["ID"].ToString());
                return true;
            }
            return false;
        }


        private void SetTaxDetails(Product objProduct, string VATCode)
        {
            objProduct.TaxDataTable = null;
            objProduct.RentTaxDataTable = null;

            DataTable dtblTax = objProduct.ShowBookersTax(VATCode);

            DataTable dtblBookerTax = new DataTable();
            dtblBookerTax.Columns.Add("TaxID", System.Type.GetType("System.String"));

            if (dtblTax.Rows.Count != 0)
                dtblBookerTax.Rows.Add(new object[] { dtblTax.Rows[0]["ID"] });

            objProduct.TaxDataTable = dtblBookerTax;

        }

        private DataTable FetchProductDataTable(string sku, string itemDesc, out int itemID)
        {
            DataTable dtbl = new DataTable();

            PosDataObject.Product objP = new PosDataObject.Product();
            objP.Connection = SystemVariables.Conn;
            objP.SKU = sku;

            if (IsProductExists(objP, out itemID))
            {
                dtbl.Columns.Add("ID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("SKU", System.Type.GetType("System.String"));
                dtbl.Columns.Add("Product", System.Type.GetType("System.String"));
                dtbl.Columns.Add("BrandID", System.Type.GetType("System.String"));
                dtbl.Columns.Add("chk", System.Type.GetType("System.Boolean"));
                dtbl.Columns.Add("Image", System.Type.GetType("System.Byte[]"));

                dtbl.Rows.Add(new object[] { itemID.ToString(), sku, itemDesc, "0", true, GeneralFunctions.GetImageAsByteArray() });
            }
            return dtbl;
        }

        private bool IsDuplicatePromotion(int itemID, DateTime startDate, DateTime endDate)
        {
            PosDataObject.Discounts objD = new PosDataObject.Discounts();
            objD.Connection = SystemVariables.Conn;

            objD.ID = itemID;
            objD.LimitedStartDate = startDate;
            objD.LimitedEndDate = endDate;

            return objD.isDuplicateBookersPromotion();
        }

        private Product SetProductDefaults()
        {
            PosDataObject.Product objProduct = new PosDataObject.Product();

            objProduct.Connection = SystemVariables.Conn;
            objProduct.LoginUserID = SystemVariables.CurrentUserID;

            objProduct.UPC = "";
            objProduct.SKU2 = "";
            objProduct.SKU3 = "";
            objProduct.BinLocation = "";
            objProduct.PrimaryVendorID = "";
            objProduct.BookingExpFlag = "N";

            objProduct.Tare = 0.00;
            objProduct.Tare2 = 0.00;

            objProduct.BrandID = 0;
            objProduct.PromptForPrice = "N";
            objProduct.AddtoPOSScreen = "N";
            objProduct.ScaleBarCode = "N";
            objProduct.FoodStampEligible = "N";
            objProduct.PrintBarCode = "N";

            objProduct.NoPriceOnLabel = "N";
            objProduct.AllowZeroStock = "Y";
            objProduct.DisplayStockinPOS = "Y";
            objProduct.ProductStatus = "Y";
            objProduct.TaggedInInvoice = "N";
            objProduct.NonDiscountable = "N";
            objProduct.AddtoScaleScreen = "N";


            objProduct.Cost = 0.0;
            objProduct.LastCost = 0.0;
            objProduct.PriceB = 0.0;
            objProduct.PriceC = 0.0;
            objProduct.ReorderQty = 0.0;
            objProduct.NormalQty = 0.0;
            objProduct.QtyOnLayaway = 0.0;
            objProduct.MinimumAge = 0;
            objProduct.QtyToPrint = 0;
            objProduct.ProductNotes = "";
            objProduct.Notes2 = "";
            objProduct.Points = 0;
            objProduct.CaseQty = 1;
            objProduct.Season = "";
            objProduct.CaseUPC = "";

            objProduct.RentalPerMinute = 0.0;
            objProduct.RentalPerHour = 0.0;
            objProduct.RentalPerHalfDay = 0.0;
            objProduct.RentalPerDay = 0.0;
            objProduct.RentalPerWeek = 0.0;
            objProduct.RentalPerMonth = 0.0;
            objProduct.RentalDeposit = 0.0;
            objProduct.RentalPrompt = "N";

            objProduct.RentalMinHour = 0.0;
            objProduct.RentalMinAmount = 0.0;

            objProduct.RepairCharge = 0.0;
            objProduct.RepairPromptForCharge = "N";
            objProduct.RepairPromptForTag = "N";

            objProduct.MinimumServiceTime = 0;
            objProduct.ProductType = "P";

            objProduct.ChangeProductType = "";

            //objProduct.cmbCategory.EditValue = "";
            //objProduct.SetCategoryStyle();

            objProduct.POSBackground = "Color";
            objProduct.POSScreenColor = "#00000000";
            objProduct.POSScreenStyle = "";
            objProduct.POSFontType = "Tahoma";
            objProduct.POSFontSize = 9;
            objProduct.POSFontColor = "#00000000";

            objProduct.IsBold = "N";
            objProduct.IsItalics = "N";

            objProduct.ScaleBackground = "Skin";
            objProduct.ScaleScreenColor = "0";
            objProduct.ScaleScreenStyle = "DevExpress Skin";
            objProduct.ScaleFontType = "Arial";
            objProduct.ScaleFontSize = 8;
            objProduct.ScaleFontColor = "0";
            objProduct.ScaleIsBold = "N";
            objProduct.ScaleIsItalics = "N";


            objProduct.LabelType = -1;
            objProduct.PhotoFilePath = "";
            objProduct.ProductPhoto = null;
            objProduct.ID = 0;
            objProduct.BreakPackRatio = 0;
            objProduct.LinkSKU = 0;

            objProduct.AddJournal = false;
            objProduct.ProductDecimalPlace = 2;
            objProduct.ChangeInventory = false;
            objProduct.PrintLabelQty = 0;

            objProduct.PrevPriceA = 0;
            objProduct.SplitWeight = 0;
            objProduct.UOM = "";
            objProduct.ShopifyImageExportFlag = "N";

            return objProduct;

        }

    }
}
