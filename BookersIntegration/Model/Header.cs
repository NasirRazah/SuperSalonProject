using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookersIntegration.Model
{
    public class Header
    {
        public string RecordType { get; set; }
        public string Filler1 { get; set; }
        public string FileTitle { get; set; }
        public string Filler2 { get; set; }
        public string DateDay { get; set; }
        public string Date1 { get; set; }
        public string DateMonth { get; set; }
        public string Date2 { get; set; }
        public string DateCentury { get; set; }
        public string DateYear { get; set; }
        public string Filler3 { get; set; }
        public string DepotTitle { get; set; }
        public string DepotNumber { get; set; }
        public string Filler4 { get; set; }
        public string EndOfLineMarker { get; set; }
    }

    public class Product
    {
        public string RecordType { get; set; }
        public string MIDASProductCode { get; set; }
        public string BarCode { get; set; }
        public string PackQuantity { get; set; }
        public string Price { get; set; }
        public double PriceDecimal
        {
            get
            {
                try
                {
                    double price = Convert.ToDouble(Price.Trim().Substring(0, 5) + "." + Price.Trim().Substring(5, 2));
                    return price;
                }
                catch (Exception ex)
                {
                }
                return 0.0;
            }
        }

        public string RecommendedRetail { get; set; }
        public string VATCode { get; set; }
        public string FullProductDescription { get; set; }
        public string TillProductDescription { get; set; }
        public string RetailBestSeller { get; set; }
        public string PriceMarkIndicator { get; set; }
        public string SupplierCode { get; set; }
        public string PackSize { get; set; }
        public string SplitIndicator { get; set; }
        public string ProductLinkCode { get; set; }
        public string SplitMidasCode { get; set; }
        public string DepartmentCode { get; set; }
        public string CategoryCode { get; set; }
        public string EndofLineMarker { get; set; }
    }

    public class PriceChange
    {
        public string RecordType { get; set; }
        public string MIDASProductCode { get; set; }
        public string BarCode { get; set; }
        public string PackQuantity { get; set; }
        public string Price { get; set; }
        public double PriceDecimal
        {
            get
            {
                try
                {
                    double price = Convert.ToDouble(Price.Trim().Substring(0, 5) + "." + Price.Trim().Substring(5, 2));
                    return price;
                }
                catch (Exception ex)
                {
                }
                return 0.0;
            }
        }
        public string RecommendedRetail { get; set; }
        public string ChangeEffectiveDate { get; set; }
        public string FullProductDescription { get; set; }
        public string Filler { get; set; }
        public string EndofLineMarker { get; set; }
    }

    public class Promotion
    {
        public string RecordType { get; set; }
        public string MIDASProductCode { get; set; }
        public string BarCode { get; set; }
        public string PackQuantity { get; set; }
        public string Price { get; set; }
        public double PriceDecimal
        {
            get
            {
                try
                {
                    double price = Convert.ToDouble(Price.Trim().Substring(0, 5) + "." + Price.Trim().Substring(5, 2));
                    return price;
                }
                catch (Exception ex)
                {
                }
                return 0.0;
            }
        }
        public string RecommendedRetail { get; set; }
        //public double RecommendedRetailDecimal
        //{
        //    get
        //    {
        //        try
        //        {
        //            double price = Convert.ToDouble(RecommendedRetail.Trim().Substring(0, 5) + "." + RecommendedRetail.Trim().Substring(5, 2));
        //            return price;
        //        }
        //        catch (Exception ex)
        //        {
        //        }
        //        return 0.0;
        //    }
        //}
        public string VATCode { get; set; }
        public string FullProductDescription { get; set; }
        public string TillProductDescription { get; set; }
        public string Filler { get; set; }
        public string SupplierCode { get; set; }
        public string PackSize { get; set; }
        public string SplitIndicator { get; set; }
        public string StartSellingDate { get; set; }
        public string EndSellingDate { get; set; }
        public string EndofLineMarker { get; set; }
    }

    public class ProductDelete
    {
        public string RecordType { get; set; }
        public string MIDASProductCode { get; set; }
        public string BarCode { get; set; }
        public string PackQuantity { get; set; }
        public string Filler1 { get; set; }
        public string FullProductDescription { get; set; }
        public string Filler2 { get; set; }
        public string PackSize { get; set; }
        public string Filler3 { get; set; }
        public string EndofLineMarker { get; set; }
    }

    public class Trailer
    {
        public string RecordType { get; set; }
        public string Filler1 { get; set; }
        public string Title { get; set; }
        public string Filler2 { get; set; }
        public string NewLineTitle { get; set; }
        public string Filler3 { get; set; }
        public string NewLineCount { get; set; }
        public string Filler4 { get; set; }
        public string PriceChangeTitle { get; set; }
        public string Filler5 { get; set; }
        public string PriceChangeCount { get; set; }
        public string Filler6 { get; set; }
        public string ProductFieldChangesTitle { get; set; }
        public string Filler7 { get; set; }
        public string ProductFieldChangesCount { get; set; }
        public string Filler8 { get; set; }
        public string PromotionsTitle { get; set; }
        public string Filler9 { get; set; }
        public string PromotionsCount { get; set; }
        public string Filler10 { get; set; }
        public string EndofLineMarker { get; set; }
    }

}
