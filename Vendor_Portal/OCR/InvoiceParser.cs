using System;
using System.Text.RegularExpressions;
using Vendor_Portal.Models;

namespace Vendor_Portal.OCR
{
    public class InvoiceParser
    {
        public static InvoiceModel Parse(string text)
        {
            InvoiceModel model = new InvoiceModel();

            Match m;

            m = Regex.Match(text,
                @"Invoice\s*(No|Number|#)\s*[:\-]?\s*(.+)",
                RegexOptions.IgnoreCase);

            if (m.Success)
                model.InvoiceNo = m.Groups[2].Value.Trim();

            m = Regex.Match(text,
                @"Invoice\s*Date\s*[:\-]?\s*(.+)",
                RegexOptions.IgnoreCase);

            if (m.Success)
            {
                DateTime dt;

                if (DateTime.TryParse(m.Groups[1].Value.Trim(), out dt))
                    model.InvoiceDate = dt;
            }

            m = Regex.Match(text,
                @"Due\s*Date\s*[:\-]?\s*(.+)",
                RegexOptions.IgnoreCase);

            if (m.Success)
            {
                DateTime dt;

                if (DateTime.TryParse(m.Groups[1].Value.Trim(), out dt))
                    model.DueDate = dt;
            }

            m = Regex.Match(text,
                @"\$?\s*([\d,]+\.\d{2})");

            if (m.Success)
            {
                model.InvoiceAmount =
                    Convert.ToDecimal(
                        m.Groups[1].Value.Replace(",", ""));
            }

            m = Regex.Match(text,
                @"Billing\s*Period\s*[:\-]?\s*(.+)",
                RegexOptions.IgnoreCase);

            if (m.Success)
                model.BillingPeriod = m.Groups[1].Value.Trim();

            m = Regex.Match(text,
                @"(Total\s*Loans|Loan\s*Count)\s*[:\-]?\s*(\d+)",
                RegexOptions.IgnoreCase);

            if (m.Success)
                model.LoanCount = Convert.ToInt32(m.Groups[2].Value);

            return model;
        }
    }
}