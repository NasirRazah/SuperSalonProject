using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pos.XeposExternal
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
}