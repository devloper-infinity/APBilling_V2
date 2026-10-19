using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vendor_Portal.OCR;
using Vendor_Portal.Models;
using System.IO;

namespace Vendor_Portal.Vendor
{
    public partial class NewVendor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



protected void btnScanInvoice_Click(object sender, EventArgs e)
    {
        if (!fuInvoice.HasFile)
            return;

        string folder = Server.MapPath("~/Temp/");

        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        string filePath = Path.Combine(folder, fuInvoice.FileName);

        fuInvoice.SaveAs(filePath);

        string pdfText = PdfReader.ReadPdf(filePath);

        InvoiceModel invoice = InvoiceParser.Parse(pdfText);

        txtInvoiceNo.Text = invoice.InvoiceNo;

        txtInvoiceDate.Text = invoice.InvoiceDate.HasValue
            ? invoice.InvoiceDate.Value.ToString("MM/dd/yyyy")
            : "";

        txtDueDate.Text = invoice.DueDate.HasValue
            ? invoice.DueDate.Value.ToString("MM/dd/yyyy")
            : "";

        txtAmount.Text = invoice.InvoiceAmount.ToString("0.00");

        txtBillingPeriod.Text = invoice.BillingPeriod;

        txtLoanCount.Text = invoice.LoanCount.ToString();
    }
}
}