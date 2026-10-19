using System;

namespace Vendor_Portal.Models
{
    public class InvoiceModel
    {
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal InvoiceAmount { get; set; }
        public string BillingPeriod { get; set; }
        public int LoanCount { get; set; }
    }
}