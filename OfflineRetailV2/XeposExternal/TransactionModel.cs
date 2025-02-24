using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OfflineRetailV2.XeposExternal
{
    public class TransactionModel
    {
        public Guid Id { get; set; }

        public int InvoiceNumber { get; set; }

        public string TransactionId { get; set; }

        public string ProfileId { get; set; }

        public string StatusCode { get; set; }

        public string StatusMessage { get; set; }

        public string ApprovalCode { get; set; }

        public decimal Amount { get; set; }

        public string AVSResult_ActualResult { get; set; }

        public string AVSResult_PostalCodeResult { get; set; }

        public string BatchId { get; set; }

        public string CVResult { get; set; }

        public string TransactionType { get; set; }
        public DateTime CreatedOn { get; set; }
        public string TransType { get; set; }
        public int LayAwayNo { get; set; }
        public int LayAwayInvoiceNo { get; set; }

        

        public override string ToString()
        {
            var type = GetType();
            var properties = type.GetProperties();
            string ret = "";
            foreach (var property in properties)
                ret += "Name: " + property.Name + ", Value: " + property.GetValue(this, null) + "\n";
            return ret;
        }
    }

    public class TranscationInfo
    {
        public int InvoiceID { get; set; }
        public decimal TotalSale { get; set; }
        public string TransactionId { get; set; }
        public TranscationInfo(int invoiceID, decimal totalSale, string transactionId)
        {
            this.InvoiceID = invoiceID;
            this.TotalSale = totalSale;
            this.TransactionId = transactionId;
        }
    }

}