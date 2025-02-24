using BookersIntegration.Helper;
using BookersIntegration.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookersIntegration.Service
{
    public class BookerHelper
    {
        public string FilePath { get; set; }

        private List<FileFormatModel> HeaderList;
        private List<FileFormatModel> ProductList;
        private List<FileFormatModel> PriceChangeList;
        private List<FileFormatModel> PromotionList;
        private List<FileFormatModel> ProductDeleteList;
        private List<FileFormatModel> TrailerList;


        public List<Header> HeaderDataList;
        public List<Product> ProductDataList;
        public List<PriceChange> PriceChangeDataList;
        public List<Promotion> PromotionDataList;
        public List<ProductDelete> ProductDeleteDataList;
        public List<Trailer> TrailerDataList;

        public BookerHelper(string filePath)
        {
          

            // configure settings
            ReadFormat();

            if (filePath != "")
            {
                //  FilePath = @"D:\All Code\XEPOS Code\booker integratio\Specs\fwd_fwd_booker_gateway_link\HBG26600.ERF";
                FilePath = filePath;
                //Read data from file
                ExtractData();
            }
        }

       

        private void ReadFormat()
        {
            SetHeaderList();
            SetProductList();
            SetPriceChangeList();
            SetPromotionList();
            SetProductDeleteList();
            SetTrailerList();
        }

        #region Extraction

        private string ReadFile()
        {
            string fileText = File.ReadAllText(FilePath);
            //Console.WriteLine(fileText);
            return fileText;
        }

        public void ExtractData()
        {
            string fileText = ReadFile();
            var items = fileText.Split(new string[] { "\n", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            HeaderDataList = new List<Header>();
            ProductDataList = new List<Product>();
            PriceChangeDataList = new List<PriceChange>();
            PromotionDataList = new List<Promotion>();
            ProductDeleteDataList = new List<ProductDelete>();
            TrailerDataList = new List<Trailer>();

            foreach (var item in items)
            {
                if (item.Substring(0, 2) == "01")
                    HeaderDataList.AddRange(CommonMethod.ConvertToList<Header>(GetItemData(item, HeaderList)));
                if (item.Substring(0, 2) == "06" || item.Substring(0, 2) == "17")
                    ProductDataList.AddRange(CommonMethod.ConvertToList<Product>(GetItemData(item, ProductList)));
                if (item.Substring(0, 2) == "49")
                    ProductDeleteDataList.AddRange(CommonMethod.ConvertToList<ProductDelete>(GetItemData(item, ProductDeleteList)));
                if (item.Substring(0, 2) == "03")
                    PriceChangeDataList.AddRange(CommonMethod.ConvertToList<PriceChange>(GetItemData(item, PriceChangeList)));
                if (item.Substring(0, 2) == "18")
                    PromotionDataList.AddRange(CommonMethod.ConvertToList<Promotion>(GetItemData(item, PromotionList)));
                if (item.Substring(0, 2) == "98")
                    TrailerDataList.AddRange(CommonMethod.ConvertToList<Trailer>(GetItemData(item, TrailerList)));
            }
        }


        #endregion


        #region New Data Extraction

        private DataTable GetItemData(string itemInfo, List<FileFormatModel> FileFormatModelList)
        {
            DataTable dt = new DataTable();
            foreach (var item in FileFormatModelList)
                if (dt.Columns.Contains(item.Name) == false)
                    dt.Columns.Add(item.Name);

            var dr = dt.NewRow();
            dt.Rows.Add(dr);
            foreach (var item in FileFormatModelList)
                dt.Rows[0][item.Name] = itemInfo.GetString(item.Length, item.TotalSize);
            return dt;
        }

        #endregion

        #region All Scrapping Info

        private void SetHeaderList()
        {
            HeaderList = new List<FileFormatModel>();

            HeaderList.Add(new FileFormatModel() { Name = "RecordType", StartIndex = 0, EndIndex = 1, Length = 2, TotalSize = 2 });
            HeaderList.Add(new FileFormatModel() { Name = "Filler1", StartIndex = 0, EndIndex = 0, Length = 1, TotalSize = 3 });
            HeaderList.Add(new FileFormatModel() { Name = "FileTitle", StartIndex = 0, EndIndex = 0, Length = 27, TotalSize = 30 });
            HeaderList.Add(new FileFormatModel() { Name = "Filler2", StartIndex = 0, EndIndex = 0, Length = 1, TotalSize = 31 });
            HeaderList.Add(new FileFormatModel() { Name = "DateDay", StartIndex = 0, EndIndex = 0, Length = 2, TotalSize = 33 });
            HeaderList.Add(new FileFormatModel() { Name = "Date1", StartIndex = 0, EndIndex = 0, Length = 1, TotalSize = 34 });
            HeaderList.Add(new FileFormatModel() { Name = "DateMonth", StartIndex = 0, EndIndex = 0, Length = 2, TotalSize = 36 });
            HeaderList.Add(new FileFormatModel() { Name = "Date2", StartIndex = 0, EndIndex = 0, Length = 1, TotalSize = 38 });
            HeaderList.Add(new FileFormatModel() { Name = "DateCentury", StartIndex = 0, EndIndex = 0, Length = 2, TotalSize = 40 });
            HeaderList.Add(new FileFormatModel() { Name = "DateYear", StartIndex = 0, EndIndex = 0, Length = 2, TotalSize = 42 });
            HeaderList.Add(new FileFormatModel() { Name = "Filler3", StartIndex = 0, EndIndex = 0, Length = 1, TotalSize = 43 });
            HeaderList.Add(new FileFormatModel() { Name = "DepotTitle", StartIndex = 0, EndIndex = 0, Length = 8, TotalSize = 50 });
            HeaderList.Add(new FileFormatModel() { Name = "DepotNumber", StartIndex = 0, EndIndex = 0, Length = 3, TotalSize = 53 });
            HeaderList.Add(new FileFormatModel() { Name = "Filler4", StartIndex = 0, EndIndex = 0, Length = 67, TotalSize = 120 });
            HeaderList.Add(new FileFormatModel() { Name = "EndOfLineMarker", StartIndex = 0, EndIndex = 0, Length = 1, TotalSize = 121 });

            int TotalSize = 0;
            for (int i = 0; i < HeaderList.Count; i++)
            {
                TotalSize += HeaderList[i].Length;
                HeaderList[i].TotalSize = TotalSize;
            }

            /*
Record Type 	X(02)	2
Filler	    X(01)	1
File Title	    X(27)	27
Filler	    X(01)	1
Run Date	    	10
Date Day	    9(02)	2
Date /	    X(01)	1
Date Month	    9(02)	2
Date /	    X(01)	1
Date Century    	9(02)	2
Date Year	    9(02)	2
Filler	     X(01)	1
Depot Title     	X(08)	8
Depot Number	    9(03)	3
Filler	        X(67)	67
End of Line Marker	    X(01)	1

             */
        }
        private void SetProductList()
        {
            ProductList = new List<FileFormatModel>();

            ProductList.Add(new FileFormatModel() { Name = "RecordType", StartIndex = 0, EndIndex = 0, Length = 2 });
            ProductList.Add(new FileFormatModel() { Name = "MIDASProductCode", StartIndex = 0, EndIndex = 0, Length = 7 });
            ProductList.Add(new FileFormatModel() { Name = "BarCode", StartIndex = 0, EndIndex = 0, Length = 13 });
            ProductList.Add(new FileFormatModel() { Name = "PackQuantity", StartIndex = 0, EndIndex = 0, Length = 5 });
            ProductList.Add(new FileFormatModel() { Name = "Price", StartIndex = 0, EndIndex = 0, Length = 7 });
            ProductList.Add(new FileFormatModel() { Name = "RecommendedRetail", StartIndex = 0, EndIndex = 0, Length = 7 });
            ProductList.Add(new FileFormatModel() { Name = "VATCode", StartIndex = 0, EndIndex = 0, Length = 2 });
            ProductList.Add(new FileFormatModel() { Name = "FullProductDescription", StartIndex = 0, EndIndex = 0, Length = 30 });
            ProductList.Add(new FileFormatModel() { Name = "TillProductDescription", StartIndex = 0, EndIndex = 0, Length = 12 });
            ProductList.Add(new FileFormatModel() { Name = "RetailBestSeller", StartIndex = 0, EndIndex = 0, Length = 1 });
            ProductList.Add(new FileFormatModel() { Name = "PriceMarkIndicator", StartIndex = 0, EndIndex = 0, Length = 1 });
            ProductList.Add(new FileFormatModel() { Name = "SupplierCode", StartIndex = 0, EndIndex = 0, Length = 4 });
            ProductList.Add(new FileFormatModel() { Name = "PackSize", StartIndex = 0, EndIndex = 0, Length = 12 });
            ProductList.Add(new FileFormatModel() { Name = "SplitIndicator", StartIndex = 0, EndIndex = 0, Length = 1 });
            ProductList.Add(new FileFormatModel() { Name = "ProductLinkCode", StartIndex = 0, EndIndex = 0, Length = 6 });
            ProductList.Add(new FileFormatModel() { Name = "SplitMidasCode", StartIndex = 0, EndIndex = 0, Length = 6 });
            ProductList.Add(new FileFormatModel() { Name = "DepartmentCode", StartIndex = 0, EndIndex = 0, Length = 2 });
            ProductList.Add(new FileFormatModel() { Name = "CategoryCode", StartIndex = 0, EndIndex = 0, Length = 2 });
            ProductList.Add(new FileFormatModel() { Name = "EndofLineMarker", StartIndex = 0, EndIndex = 0, Length = 1 });

            int TotalSize = 0;
            for (int i = 0; i < ProductList.Count; i++)
            {
                TotalSize += ProductList[i].Length;
                ProductList[i].TotalSize = TotalSize;
            }

            /*
              Record Type	X(02)	2
MIDAS Product Code	X(07)	7
Bar Code	X(13)	13
Pack Quantity	9(05)	5
Price	9(05)V99	7
Recommended Retail	9(05)V99	7
VAT Code	9(02)	2
Full Product Description 	X(30)	30
Till Product Description 	X(12)	12
Retail Best Seller	9(01)	1
Price Mark Indicator	9(01)	1
Supplier Code	X(04)	4
Pack Size	X(12)	12
Split Indicator	X(01)	1
Product Link Code	9(06)	6
Split Midas Code	9(06)	6
Department Code	9(02)	2
Category Code	9(02)	2
End of Line Marker	X(01)	1

             */
        }
        private void SetPriceChangeList()
        {
            PriceChangeList = new List<FileFormatModel>();

            PriceChangeList.Add(new FileFormatModel() { Name = "RecordType", StartIndex = 0, EndIndex = 0, Length = 2 });
            PriceChangeList.Add(new FileFormatModel() { Name = "MIDASProductCode", StartIndex = 0, EndIndex = 0, Length = 7 });
            PriceChangeList.Add(new FileFormatModel() { Name = "BarCode", StartIndex = 0, EndIndex = 0, Length = 13 });
            PriceChangeList.Add(new FileFormatModel() { Name = "PackQuantity", StartIndex = 0, EndIndex = 0, Length = 5 });
            PriceChangeList.Add(new FileFormatModel() { Name = "Price", StartIndex = 0, EndIndex = 0, Length = 7 });
            PriceChangeList.Add(new FileFormatModel() { Name = "RecommendedRetail", StartIndex = 0, EndIndex = 0, Length = 7 });
            PriceChangeList.Add(new FileFormatModel() { Name = "ChangeEffectiveDate", StartIndex = 0, EndIndex = 0, Length = 8 });
            PriceChangeList.Add(new FileFormatModel() { Name = "FullProductDescription", StartIndex = 0, EndIndex = 0, Length = 30 });
            PriceChangeList.Add(new FileFormatModel() { Name = "Filler", StartIndex = 0, EndIndex = 0, Length = 41 });
            PriceChangeList.Add(new FileFormatModel() { Name = "EndofLineMarker", StartIndex = 0, EndIndex = 0, Length = 1 });

            int TotalSize = 0;
            for (int i = 0; i < PriceChangeList.Count; i++)
            {
                TotalSize += PriceChangeList[i].Length;
                PriceChangeList[i].TotalSize = TotalSize;
            }

            /*
            Record Type	X(02)	2
MIDAS Product Code	X(07)	7
Bar Code	X(13)	13
Pack Quantity	9(05)	5
Price	9(05)V99	7
Recommended Retail	9(05)V99	7
Change Effective Date	9(08)	8
Full Product Description 	X(30)	30
Filler	X(41)	41
End of Line Marker	X(01)	1

            */
        }
        private void SetPromotionList()
        {
            PromotionList = new List<FileFormatModel>();

            PromotionList.Add(new FileFormatModel() { Name = "RecordType", StartIndex = 0, EndIndex = 0, Length = 2 });
            PromotionList.Add(new FileFormatModel() { Name = "MIDASProductCode", StartIndex = 0, EndIndex = 0, Length = 7 });
            PromotionList.Add(new FileFormatModel() { Name = "BarCode", StartIndex = 0, EndIndex = 0, Length = 13 });
            PromotionList.Add(new FileFormatModel() { Name = "PackQuantity", StartIndex = 0, EndIndex = 0, Length = 5 });
            PromotionList.Add(new FileFormatModel() { Name = "Price", StartIndex = 0, EndIndex = 0, Length = 7 });
            PromotionList.Add(new FileFormatModel() { Name = "RecommendedRetail", StartIndex = 0, EndIndex = 0, Length = 7 });
            PromotionList.Add(new FileFormatModel() { Name = "VATCode", StartIndex = 0, EndIndex = 0, Length = 2 });
            PromotionList.Add(new FileFormatModel() { Name = "FullProductDescription", StartIndex = 0, EndIndex = 0, Length = 30 });
            PromotionList.Add(new FileFormatModel() { Name = "TillProductDescription", StartIndex = 0, EndIndex = 0, Length = 12 });
            PromotionList.Add(new FileFormatModel() { Name = "Filler", StartIndex = 0, EndIndex = 0, Length = 2 });
            PromotionList.Add(new FileFormatModel() { Name = "SupplierCode", StartIndex = 0, EndIndex = 0, Length = 4 });
            PromotionList.Add(new FileFormatModel() { Name = "PackSize", StartIndex = 0, EndIndex = 0, Length = 12 });
            PromotionList.Add(new FileFormatModel() { Name = "SplitIndicator", StartIndex = 0, EndIndex = 0, Length = 1 });
            PromotionList.Add(new FileFormatModel() { Name = "StartSellingDate", StartIndex = 0, EndIndex = 0, Length = 8 });
            PromotionList.Add(new FileFormatModel() { Name = "EndSellingDate", StartIndex = 0, EndIndex = 0, Length = 8 });
            PromotionList.Add(new FileFormatModel() { Name = "EndofLineMarker", StartIndex = 0, EndIndex = 0, Length = 1 });

            int TotalSize = 0;
            for (int i = 0; i < PromotionList.Count; i++)
            {
                TotalSize += PromotionList[i].Length;
                PromotionList[i].TotalSize = TotalSize;
            }

            /*
            Record Type	X(02)	2
MIDAS Product Code	X(07)	7
Bar Code	X(13)	13
Pack Quantity	9(05)	5
Price	9(05)V99	7
Recommended Retail	9(05)V99	7
Change Effective Date	9(08)	8
Full Product Description 	X(30)	30
Filler	X(41)	41
End of Line Marker	X(01)	1
              */
        }
        private void SetProductDeleteList()
        {
            ProductDeleteList = new List<FileFormatModel>();

            ProductDeleteList.Add(new FileFormatModel() { Name = "RecordType", StartIndex = 0, EndIndex = 0, Length = 2 });
            ProductDeleteList.Add(new FileFormatModel() { Name = "MIDASProductCode", StartIndex = 0, EndIndex = 0, Length = 7 });
            ProductDeleteList.Add(new FileFormatModel() { Name = "BarCode", StartIndex = 0, EndIndex = 0, Length = 13 });
            ProductDeleteList.Add(new FileFormatModel() { Name = "PackQuantity", StartIndex = 0, EndIndex = 0, Length = 5 });
            ProductDeleteList.Add(new FileFormatModel() { Name = "Filler1", StartIndex = 0, EndIndex = 0, Length = 16 });
            ProductDeleteList.Add(new FileFormatModel() { Name = "FullProductDescription", StartIndex = 0, EndIndex = 0, Length = 30 });
            ProductDeleteList.Add(new FileFormatModel() { Name = "Filler2", StartIndex = 0, EndIndex = 0, Length = 18 });
            ProductDeleteList.Add(new FileFormatModel() { Name = "PackSize", StartIndex = 0, EndIndex = 0, Length = 12 });
            ProductDeleteList.Add(new FileFormatModel() { Name = "Filler3", StartIndex = 0, EndIndex = 0, Length = 17 });
            ProductDeleteList.Add(new FileFormatModel() { Name = "EndofLineMarker", StartIndex = 0, EndIndex = 0, Length = 1 });

            int TotalSize = 0;
            for (int i = 0; i < ProductDeleteList.Count; i++)
            {
                TotalSize += ProductDeleteList[i].Length;
                ProductDeleteList[i].TotalSize = TotalSize;
            }

            /*

            Record Type	X(02)	2
MIDAS Product Code	X(07)	7
Bar Code	X(13)	13
Pack Quantity	9(05)	5
Price	9(05)V99	7
Recommended Retail	9(05)V99	7
VAT Code	9(02)	2
Full Product Description 	X(30)	30
Till Product Description 	X(12)	12
Filler	X(02)	2
Supplier Code	X(04)	4
Pack Size	X(12)	12
Split Indicator	X(01)	1
Start Selling Date	9(08)	8
End Selling Date	9(08)	8
End of Line Marker	X(01)	1

            */
        }
        private void SetTrailerList()
        {
            TrailerList = new List<FileFormatModel>();

            TrailerList.Add(new FileFormatModel() { Name = "RecordType", StartIndex = 0, EndIndex = 0, Length = 2 });
            TrailerList.Add(new FileFormatModel() { Name = "Filler1", StartIndex = 0, EndIndex = 0, Length = 1 });
            TrailerList.Add(new FileFormatModel() { Name = "Title", StartIndex = 0, EndIndex = 0, Length = 14 });
            TrailerList.Add(new FileFormatModel() { Name = "Filler2", StartIndex = 0, EndIndex = 0, Length = 1 });
            TrailerList.Add(new FileFormatModel() { Name = "NewLineTitle", StartIndex = 0, EndIndex = 0, Length = 20 });
            TrailerList.Add(new FileFormatModel() { Name = "Filler3", StartIndex = 0, EndIndex = 0, Length = 1 });
            TrailerList.Add(new FileFormatModel() { Name = "NewLineCount", StartIndex = 0, EndIndex = 0, Length = 5 });
            TrailerList.Add(new FileFormatModel() { Name = "Filler4", StartIndex = 0, EndIndex = 0, Length = 1 });
            TrailerList.Add(new FileFormatModel() { Name = "PriceChangeTitle", StartIndex = 0, EndIndex = 0, Length = 19 });
            TrailerList.Add(new FileFormatModel() { Name = "Filler5", StartIndex = 0, EndIndex = 0, Length = 1 });
            TrailerList.Add(new FileFormatModel() { Name = "PriceChangeCount", StartIndex = 0, EndIndex = 0, Length = 5 });
            TrailerList.Add(new FileFormatModel() { Name = "Filler6", StartIndex = 0, EndIndex = 0, Length = 1 });
            TrailerList.Add(new FileFormatModel() { Name = "ProductFieldChangesTitle", StartIndex = 0, EndIndex = 0, Length = 19 });
            TrailerList.Add(new FileFormatModel() { Name = "Filler7", StartIndex = 0, EndIndex = 0, Length = 1 });
            TrailerList.Add(new FileFormatModel() { Name = "ProductFieldChangesCount", StartIndex = 0, EndIndex = 0, Length = 5 });
            TrailerList.Add(new FileFormatModel() { Name = "Filler8", StartIndex = 0, EndIndex = 0, Length = 1 });
            TrailerList.Add(new FileFormatModel() { Name = "PromotionsTitle", StartIndex = 0, EndIndex = 0, Length = 16 });
            TrailerList.Add(new FileFormatModel() { Name = "Filler9", StartIndex = 0, EndIndex = 0, Length = 1 });
            TrailerList.Add(new FileFormatModel() { Name = "PromotionsCount", StartIndex = 0, EndIndex = 0, Length = 5 });
            TrailerList.Add(new FileFormatModel() { Name = "Filler10", StartIndex = 0, EndIndex = 0, Length = 1 });
            TrailerList.Add(new FileFormatModel() { Name = "EndofLineMarker", StartIndex = 0, EndIndex = 0, Length = 1 });

            int TotalSize = 0;
            for (int i = 0; i < TrailerList.Count; i++)
            {
                TotalSize += TrailerList[i].Length;
                TrailerList[i].TotalSize = TotalSize;
            }

            /*
            Record Type	X(02)	2
Filler	X(01)	1
Title	X(14)	14
Filler	X(01)	1
New Line Title	X(20)	20
Filler	X(01)	1
New Line Count	9(05)	5
Filler	X(01)	1
Price Change Title	X(19)	19
Filler	X(01)	1
Price Change Count	9(05)	5
Filler	X(01)	1
Product Field Changes Title	X(19)	19
Filler	X(01)	1
Product Field Changes Count	9(05)	5
Filler	X(01)	1
Promotions Title	X(16)	16
Filler	X(01)	1
Promotions Count	9(05)	5
Filler	X(01)	1
End of Line Marker	X(01)	1

            */
        }

        #endregion 
    }
}
