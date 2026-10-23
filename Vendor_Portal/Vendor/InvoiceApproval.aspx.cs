using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vendor_Portal.App_Code.BLL;

namespace Vendor_Portal.Vendor
{
    public partial class InvoiceApproval : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string GetAllIPSInvoiceForReconcile_ForApproval()
        {

            DataTable dt1 = new bllInvoice().GetInfinityInvoiceDetails_Approval_IPS(int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            if (dt1 != null)
            {
                foreach (DataRow dr in dt1.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt1.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }
            }
            JavaScriptSerializer ser = new JavaScriptSerializer();
            ser.MaxJsonLength = int.MaxValue;
            return ser.Serialize(rows);
        }
        [WebMethod]
        public static string GetAllIPSInvoiceForReconcile_ForApproval_canopy()
        {

            DataTable dt1 = new bllInvoice().GetInfinityInvoiceDetails_Approval_canopy(int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            if (dt1 != null)
            {
                foreach (DataRow dr in dt1.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt1.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }
            }
            JavaScriptSerializer ser = new JavaScriptSerializer();
            ser.MaxJsonLength = int.MaxValue;
            return ser.Serialize(rows);
        }
        [WebMethod]
        public static string GetAllInvoiceLoansDetailsForReport(int InvoiceID, string InvoiceType)
        {
            DataTable dt1 = new bllInvoice().GetAllInvoiceLoansDetailsForReport(InvoiceID, InvoiceType);
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            if (dt1 != null)
            {
                foreach (DataRow dr in dt1.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt1.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }
            }
            JavaScriptSerializer ser = new JavaScriptSerializer();
            ser.MaxJsonLength = int.MaxValue;
            return ser.Serialize(rows);
        }

        [WebMethod]
        public static int ApprovalInvoice(int InvoiceId, string Remark, string strCompany, string InvType,string status,int Approval_LoansDeducted,int Approval_AmountDeducted, string Approval_PaybleToVendor)
        {
            int ReturnValue = 0;
            var mc = new InvoiceApproval();


            //SendEmailToCMSir(InvoiceId, "IPS", "","");

            Hashtable htParam = new Hashtable();
            htParam.Add("InvoiceId", InvoiceId);
            htParam.Add("IsApproved", Convert.ToBoolean("True"));
            htParam.Add("Remark", Remark);
            htParam.Add("ApprovedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
            htParam.Add("Status", status);
            htParam.Add("LoansDeducted", Approval_LoansDeducted);
            htParam.Add("AmountDeducted", Approval_AmountDeducted);
            htParam.Add("PaybleToVendor", Approval_PaybleToVendor);


            if (strCompany == "Canopy")
            {
                ReturnValue = new bllInvoice().UpdateInvoiceReconciliation_Canopy(htParam);
                ReturnValue = InvoiceId;
                if (ReturnValue > 0)
                {
                    sendEmailApproval_Canopy(InvoiceId, strCompany, status);
                    try
                    {
                      //  SendEmailToCMSir(InvoiceId, "Canopy", "", "");
                    }
                    catch
                    { 
                    }
                }
            }
            else
            {
                ReturnValue = new bllInvoice().UpdateInvoiceReconciliation_IPS(htParam);
                ReturnValue = InvoiceId;
                if (ReturnValue > 0)
                {
                    SendApprovalEmail_IPS(InvoiceId, strCompany, InvType, status);
                    try
                    {
                       // SendEmailToCMSir(InvoiceId, "IPS", "", "");
                    }
                    catch { }
                }
            }

            return ReturnValue;
        }

        //[WebMethod]
        //public static void SendEmailToCMSir(int InvoiceId, string strCompany, string InvType, string strStatus)
        //{
        //    StringBuilder htmlBody = new StringBuilder();
        //    var mc = new InvoiceApproval();
        //    string Subject = string.Empty;
        //    string ProjectName = "Billing";
        //    string ProjectType = "";
        //    string strFilePath = string.Empty;
        //    DateTime dtime = DateTime.Today;
        //    string day = dtime.DayOfWeek.ToString();

            
        //    string ToAddress = "k.aarti@infinity-data.com";
        //    string ToCC = "";

        //    //string ToAddress = "cm@infinity-data.com,hetal@infinity-data.com";
        //    //string ToCC = "jim@infinity-data.com,anita@infinity-data.com,s.chandrakant@infinity-data.com";

        //    DataTable dt = new DataTable();
        //    DataTable dtSummary = new DataTable();
        //    DataTable dtAdvancePaymentSymmary = new DataTable();

        //    dtAdvancePaymentSymmary = new bllInvoice().GetAllInvoiceDetails(InvoiceId);
        //    string BillingDatePeriod = Convert.ToString(dtAdvancePaymentSymmary.Rows[0]["InvoiceType"]);
        //    strFilePath = Convert.ToString(dtAdvancePaymentSymmary.Rows[0]["strNewFilePath"]);
        //    string Path = strFilePath;
        //    string Path1 = strFilePath;

        //    Subject = Convert.ToString(dtAdvancePaymentSymmary.Rows[0]["strSubject"]);

        //   htmlBody.Append("<br />Dear Sir/Madam,<br />");
        //   htmlBody.Append("<br /><font color=brown face=Verdana size=2 ><b>For your approval to make payment to " + BillingDatePeriod + ". </b></font><br />");
        //   htmlBody.Append("</table>");
        //   htmlBody.Append("<br /><font color=brown face=Verdana size=2><b>Please find " + BillingDatePeriod + "  Summary Report.</b></font><br /><br />");
        //   if (dtAdvancePaymentSymmary.Rows.Count > 0)
        //    {
        //        if (BillingDatePeriod == "KEB")
        //        {
        //            htmlBody.Append("<table border=\"1\" bordercolor='Black' style='border:solid 1px black;border-collapse:collapse;padding:2px; font-size:11px;'><tr style='background-color:Skyblue; color:Black;'><td><center><b>Month</b></center></td><td><center><b>Year</b></center></td><td><center><b>Invoice No</b></center></td><td><center><b>Invoice Date</b></center></td><td><center><b>No Of Hours</b></center></td><td><center><b>Base Rate</b></center></td><td><center><b>Invoice Amount</b></center></td><td><center><b>Remark</b></center></td><td bgcolor='yellow'><center><b>Approved Remark</b></center></td><td bgcolor='yellow'><center><b>Approved By</b></center></td><td bgcolor='yellow'><center><b>Approved Date</b></center></td><td><center><b>Paid Date</b></center></td></tr>");

        //            for (int i = 0; i < dtAdvancePaymentSymmary.Rows.Count; i++)
        //            {
        //                htmlBody.Append("<tr style='background-color:White; color:black;'><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["Month"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["Year"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["InvoiceNo"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["InvoiceDate"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["NoOfLoans"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["ProjectId"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["Balance"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["Remark"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["ApprovedRemark"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["ApprovedBy"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["ApprovedDate"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["IsPaidDate"]) + "</b></center></td></tr>");
        //            }

        //        }
        //        else if (BillingDatePeriod == "LoanLogics")
        //        {
        //            htmlBody.Append("<table border=\"1\" bordercolor='Black' style='border:solid 1px black;border-collapse:collapse;padding:2px; font-size:11px;'><tr style='background-color:Skyblue; color:Black;'><td><center><b>Month</b></center></td><td><center><b>Year</b></center></td><td><center><b>Invoice No</b></center></td><td><center><b>Invoice Date</b></center></td><td><center><b>No Of Loans</b></center></td><td><center><b>Cost/Loan</b></center></td><td><center><b>Invoice Amount</b></center></td><td><center><b>Remark</b></center></td><td bgcolor='yellow'><center><b>Approved Remark</b></center></td><td bgcolor='yellow'><center><b>Approved By</b></center></td><td bgcolor='yellow'><center><b>Approved Date</b></center></td><td><center><b>Paid Date</b></center></td></tr>");
        //            for (int i = 0; i < dtAdvancePaymentSymmary.Rows.Count; i++)
        //            {
        //                htmlBody.Append("<tr style='background-color:White; color:black;'><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["Month"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["Year"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["InvoiceNo"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["InvoiceDate"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["NoOfLoans"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["PayToLM"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["Balance"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["Remark"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["ApprovedRemark"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["ApprovedBy"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["ApprovedDate"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["IsPaidDate"]) + "</b></center></td></tr>");
        //            }
        //        }

        //        else if (BillingDatePeriod.Contains("Stewart"))
        //        {
        //            htmlBody.Append("<table border=\"1\" bordercolor='Black' style='border:solid 1px black;border-collapse:collapse;padding:2px; font-size:11px;'><tr style='background-color:Skyblue; color:Black;'><td><center><b>Month</b></center></td><td><center><b>Year</b></center></td><td><center><b>Invoice No</b></center></td><td><center><b>Invoice Date</b></center></td><td><center><b>Invoice Amount</b></center></td><td><center><b>Difference Amount</b></center></td><td><center><b>Total Amount</b></center></td><td><center><b>Remark</b></center></td><td bgcolor='yellow'><center><b>Approved Remark</b></center></td><td bgcolor='yellow'><center><b>Approved By</b></center></td><td bgcolor='yellow'><center><b>Approved Date</b></center></td><td><center><b>Paid Date</b></center></td><td><center><b>Remark from A/c dept</b></center></td> </tr>");
        //            for (int i = 0; i < dtAdvancePaymentSymmary.Rows.Count; i++)
        //            {
        //                htmlBody.Append("<tr style='background-color:White; color:black;'><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["Month"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["Year"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["InvoiceNo"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["InvoiceDate"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["Balance"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["PayToLM"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["TotalPay"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["Remark"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["ApprovedRemark"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["ApprovedBy"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["ApprovedDate"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["IsPaidDate"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["PaymentRemark"]) + "</b></center></td></tr>");
        //            }
        //        }

        //        else if (BillingDatePeriod.Contains("LauraMac"))
        //        {
        //            htmlBody.Append("<table border=\"1\" bordercolor='Black' style='border:solid 1px black;border-collapse:collapse;padding:2px; font-size:12px;'><tr style='background-color:Skyblue; color:Black;'><td><center><b>Month</b></center></td><td><center><b>Year</b></center></td><td><center><b>Invoice No</b></center></td><td><center><b>Invoice Date</b></center></td><td><center><b>NoOfLoans</b></center></td><td><center><b>Invoice Amount</b></center></td><td><center><b>Per Order Cost</b></center></td><td><center><b>Remark</b></center></td><td><center><b>Deducted Loans count</ b></center></td><td><center><b>Deducted Amount</ b></center></td><td><center><b>Payble to LM</ b></center></td><td bgcolor='yellow'><center><b>Approved Remark</b></center></td><td bgcolor='yellow'><center><b>Approved By</b></center></td><td bgcolor='yellow'><center><b>Approved Date</b></center></td><td><center><b>Paid Date</b></center></td></tr>");
        //            for (int i = 0; i < dtAdvancePaymentSymmary.Rows.Count; i++)
        //            {
        //                htmlBody.Append("<tr style='background-color:White; color:black;'><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["Month"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["Year"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["InvoiceNo"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["InvoiceDate"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["NoOfLoans"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["Balance"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["IsReconcileRemark"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["Remark"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["ProjectId"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["IsApproved1Remark"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["PayToLM"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["ApprovedRemark"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["ApprovedBy"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["ApprovedDate"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["IsPaidDate"]) + "</b></center></td></tr>");
        //            }

        //        }
        //        else if (BillingDatePeriod.Contains("PACER"))
        //        {
        //            htmlBody.Append("<table border=\"1\" bordercolor='Black' style='border:solid 1px black;border-collapse:collapse;padding:3px; font-size:12px;'><tr style='background-color:Skyblue; color:Black;'><td><center><b>Month</b></center></td><td><center><b>Year</b></center></td><td><center><b>Invoice No</b></center></td><td><center><b>Invoice Date</b></center></td><td><center><b>Invoice Amount</b></center></td><td><center><b>Remark</b></center></td><td bgcolor='yellow'><center><b>Approved Remark</b></center></td><td bgcolor='yellow'><center><b>Approved By</b></center></td><td bgcolor='yellow'><center><b>Approved Date</b></center></td><td><center><b>Paid Date</b></center></td><td><center><b>Paid Remark(Charged to client invoice #)</b></center></td></tr>");
        //            for (int i = 0; i < dtAdvancePaymentSymmary.Rows.Count; i++)
        //            {
        //                htmlBody.Append("<tr style='background-color:White; color:black;'><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["Month"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["Year"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["InvoiceNo"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["InvoiceDate"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["Balance"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["Remark"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["ApprovedRemark"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["ApprovedBy"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["ApprovedDate"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["IsPaidDate"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["PaymentRemark"]) + "</b></center></td></tr>");
        //            }
        //        }

        //        else if (BillingDatePeriod.Contains("KCB"))
        //        {
        //            htmlBody.Append("<table border=\"1\" bordercolor='Black' style='border:solid 1px black;border-collapse:collapse;padding:3px; font-size:12px;'><tr style='background-color:Skyblue; color:Black;'><td><center><b>Month</b></center></td><td><center><b>Year</b></center></td><td><center><b>Invoice No</b></center></td><td><center><b>Invoice Date</b></center></td><td><center><b>Invoice Amount</b></center></td><td><center><b>Remark</b></center></td><td bgcolor='yellow'><center><b>Approved Remark</b></center></td><td bgcolor='yellow'><center><b>Approved By</b></center></td><td bgcolor='yellow'><center><b>Approved Date</b></center></td><td><center><b>Paid Date</b></center></td><td><center><b>Paid Remark(Charged to client invoice #)</b></center></td></tr>");
        //            for (int i = 0; i < dtAdvancePaymentSymmary.Rows.Count; i++)
        //            {
        //                htmlBody.Append("<tr style='background-color:White; color:black;'><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["Month"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["Year"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["InvoiceNo"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["InvoiceDate"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["Balance"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["Remark"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["ApprovedRemark"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["ApprovedBy"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["ApprovedDate"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["IsPaidDate"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["PaymentRemark"]) + "</b></center></td></tr>");
        //            }
        //        }

        //        else if (BillingDatePeriod.Contains("Attorney"))
        //        {
        //            htmlBody.Append("<table border=\"1\" bordercolor='Black' style='border:solid 1px black;border-collapse:collapse;padding:3px; font-size:12px;'><tr style='background-color:Skyblue; color:Black;'><td><center><b>Invoice No</b></center></td><td><center><b>Invoice Date</b></center></td><td><center><b>Description</b></center></td><td><center><b>Invoice Amount</b></center></td><td><center><b>Charged to client</ b></center></td><td><center><b> Paid to vendor</ b></center></td><td><center><b>Difference amount</ b></center></td><td><center><b>Paid Date</b></center></td><td><center><b>Operation's comments</b></center></td><td><center><b>Accounts comments</b></center></td><td bgcolor='yellow'><center><b>Approved Remark</b></center></td><td bgcolor='yellow'><center><b>Approved By</b></center></td><td bgcolor='yellow'><center><b>Approved Date</b></center></td></tr>");
        //            for (int i = 0; i < dtAdvancePaymentSymmary.Rows.Count; i++)
        //            {
        //                htmlBody.Append("<tr style='background-color:White; color:black;'><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["InvoiceNo"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["InvoiceDate"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["Description"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["BillAmount"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["PaidAmount"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["AccPaidAmount"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["DiffAmount"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["PaidDateNew"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["Remark"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["PaymentRemarkNew"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["IsApprovedRemark"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["ApprovedName"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["IsApprovedDate"]) + "</b></center></td></tr>");
        //            }
        //        }

        //        else if (BillingDatePeriod.Contains("Remote UW"))
        //        {
        //            htmlBody.Append("<table border=\"1\" bordercolor='Black' style='border:solid 1px black;border-collapse:collapse;padding:2px; font-size:11px;'><tr style='background-color:Skyblue; color:Black;'><td><center><b>Month</b></center></td><td><center><b>Year</b></center></td><td><center><b>Invoice No</b></center></td><td><center><b>Invoice Date</b></center></td><td><center><b>No Of Hours</b></center></td><td><center><b>Base Rate</b></center></td><td><center><b>Invoice Amount</b></center></td><td><center><b>Remark</b></center></td><td bgcolor='yellow'><center><b>Approved Remark</b></center></td><td bgcolor='yellow'><center><b>Approved By</b></center></td><td bgcolor='yellow'><center><b>Approved Date</b></center></td><td><center><b>Paid Date</b></center></td></tr>");

        //            for (int i = 0; i < dtAdvancePaymentSymmary.Rows.Count; i++)
        //            {
        //                htmlBody.Append("<tr style='background-color:White; color:black;'><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["Month"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["Year"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["InvoiceNo"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["InvoiceDate"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["NoOfLoans"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["ProjectId"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["Balance"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["Remark"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["ApprovedRemark"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["ApprovedBy"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["ApprovedDate"]) + "</b></center></td><td><center><b>" + Convert.ToString(dtAdvancePaymentSymmary.Rows[i]["IsPaidDate"]) + "</b></center></td></tr>");
        //            }

        //        }




        //        htmlBody.Append("</table>");
        //        //htmlBody.Append("<br /><font color=brown face=Verdana size=2><b>Details report is attached for your ready reference.</b></font><br /><br />");
        //        htmlBody.Append("<br /><br /><table width=\"650px\" style='font-size:13px;'><tr><td align=\"left\">Thanks,<br />Infinity </td></tr> <tr> <br /><td align=\"center\"><b>!!! This is software generated e-mail...Please do not reply. !!!</td></tr></table>");
        //        htmlBody.Append("<table width=\"600px\" style='font-size:10px;'><tr><td align=\"left\"></td></tr> <tr> <br /><td align=\"center\"><b>" + "***********************************************************************************************************************************************************************************************" + "</td></tr></table>");
        //        htmlBody.Append("<table width=\"600px\" style='font-size:10px;'><tr><td align=\"left\">" + "<b>CONFIDENTIALITY INFORMATION AND DISCLAIMER</b>" + "</td></tr> <tr> <br /><td align=\"center\"><b></td></tr></table>");
        //        htmlBody.Append("<table width=\"600px\" style='font-size:12px;'><tr><td align=\"left\">" + "This message contains information which may be confidential and privileged. Unless you are the addressee (or authorized to receive for the addressee), you may not use copy or disclose to anyone the message or any information contained in the message. If you have received the message in error, please advise the sender by reply e-mail and delete the message. Thank you." + "</td></tr><tr><td align=\"center\"><b></td></tr></table>");
        //        htmlBody.Append("<table width=\"600px\" style='font-size:10px;'><tr><td align=\"left\">" + "***********************************************************************************************************************************************************************************************" + " </td></tr> <tr> <br /><td align=\"center\"><b></td></tr></table>");
        //    }
        //    if (Subject.Contains ("Canopy"))
        //    {
        //        sendMailForOnlineTracking_Canopy(ToAddress, ToCC, "k.aarti@infinity-data.com", Subject, Path, htmlBody);
        //    }
        //    else
        //    {
        //        mc.sendEmailInvoiceApproval_IPS(ToAddress, ToCC, "k.aarti@infinity-data.com", Subject, Path, Path1, htmlBody);
        //    }
        //}


        [WebMethod]
        public static void SendApprovalEmail_IPS(int InvoiceId, string strCompany, string InvType,string strStatus)
        {
            var mc = new InvoiceApproval();
            StringBuilder htmlBody = new StringBuilder();
            string Subject = string.Empty;
            string ProjectName = "Billing";
            string ProjectType = "";

            DateTime dtime = DateTime.Today;
            string day = dtime.DayOfWeek.ToString();
            string Path = "";
            string ToAddress = "";
            string ToCC = "";

            if (InvType == "Attorney")
            {
                ToAddress = "jim@infinity-data.com,anita@infinity-data.com,k.adam@infinity-data.com";
                ToCC = "s.chandrakant@infinity-data.com";
            }

            else if (InvType == "Abstractor")
            {
                ToAddress = "jim@infinity-data.com,anita@infinity-data.com";
                ToCC = "s.chandrakant@infinity-data.com";
            }
            else
            {

                ToAddress = "jim@infinity-data.com,anita@infinity-data.com,k.adam@infinity-data.com";
                ToCC = "s.chandrakant@infinity-data.com,k.manoj@infinity-data.com";
            }

            //ToAddress = "b.shubhangi@infinityinternationals.us";ToCC = "p.kedar@infinityinternationals.us";

            string BillingDatePeriod = "";
            string strInvoiceType = "";
            DataTable dt = new DataTable();
            dt = new bllInvoice().GetAllPendingInvoice_IPS(InvoiceId);

            if (dt.Rows.Count > 0)
            {
                strInvoiceType = Convert.ToString(dt.Rows[0]["NewInvoiceType"]);
                //Subject = "Bill is approved for payment" + "-" + Convert.ToString(dt.Rows[0]["NewInvoiceType"]) + "-" + Convert.ToString(dt.Rows[0]["VendorInvoiceNumber"]);
                if (strStatus == "Approve")
                {
                    Subject = "Bill is approved for payment" + "-" + Convert.ToString(dt.Rows[0]["NewInvoiceType"]) + "-" + Convert.ToString(dt.Rows[0]["VendorInvoiceNumber"]);
                }
                else
                {
                    Subject = "Bill is Hold for payment" + "-" + Convert.ToString(dt.Rows[0]["NewInvoiceType"]) + "-" + Convert.ToString(dt.Rows[0]["VendorInvoiceNumber"]);
                }

                htmlBody.Append("<br />Dear Sir/Madam,<br />");
                htmlBody.Append("<br /><font color=brown face=Verdana size=2 ><b> Below vendor payments are approved." + BillingDatePeriod + ".</b></font><br />");
                htmlBody.Append("<br /><table border=\"1\" bordercolor='Black' style='border:solid 1px black;border-collapse:collapse;padding:3px; font-size:14px;'><tr style='background-color:#244b73; color:White;'><td colspan=15><center><b>" + ProjectName + "-Details " + BillingDatePeriod + "  </b></center></td></tr>");
                htmlBody.Append("<tr><td colspan='14' style='border:none; height:3px; background-color:White;'></td></tr>");

                if (strStatus == "Approve")
                {
                   htmlBody.Append("<tr style='background-color:#244b73; color:White;'><td><center><b>Invoice #</b></center></td><td><center><b>Invoice Type</b></center></td><td><center><b>Billing Period</b></center></td><td><center><b>Invoice Date</b></center></td><td><center><b>Due Date</b></center></td><td><center><b>No of Loans/Orders</b></center></td><td><center><b>Amount</b></center></td><td><center><b>Loan Deduct Count</b></center></td><td><center><b>Loan Deduct Amount</b></center></td><td><center><b>Payble To Vendor</b></center></td><td><center><b>Verification Remark</b></center></td><td><center><b>Invoice Added in System On </b></center></td><td><center><b>Approved Remark</b></center></td><td><center><b>Approved By</b></center></td><td><center><b>Approved Date</b></center></td></tr>");
                }
                else
                {
                    htmlBody.Append("<tr style='background-color:#244b73; color:White;'><td><center><b>Invoice #</b></center></td><td><center><b>Invoice Type</b></center></td><td><center><b>Billing Period</b></center></td><td><center><b>Invoice Date</b></center></td><td><center><b>Due Date</b></center></td><td><center><b>No of Loans/Orders</b></center></td><td><center><b>Amount</b></center></td><td><center><b>Loan Deduct Count</b></center></td><td><center><b>Loan Deduct Amount</b></center></td><td><center><b>Payble To Vendor</b></center></td><td><center><b>Verification Remark</b></center></td><td><center><b>Invoice Added in System On </b></center></td><td><center><b>Hold Remark</b></center></td><td><center><b>Hold By</b></center></td><td><center><b>Hold Date</b></center></td></tr>");
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (strStatus == "Approve")
                    {
                        htmlBody.Append("<tr style='background-color:White; color:black;'><td><center><b>" + Convert.ToString(dt.Rows[i]["VendorInvoiceNumber"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["NewInvoiceType"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["BillingPeriod"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["StatementDate"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["DueDate"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["NoOfLoans"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["InvoiceAmount"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["loandeductcount"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["loandeductamount"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["PayToVendor"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["Remark"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["AddedDate"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["ApprovedRemark"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["ApprovedBy"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["ApprovedDate"]) + "</b></center></td></tr>");
                    }

                    else
                    {
                        htmlBody.Append("<tr style='background-color:White; color:black;'><td><center><b>" + Convert.ToString(dt.Rows[i]["VendorInvoiceNumber"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["NewInvoiceType"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["BillingPeriod"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["StatementDate"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["DueDate"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["NoOfLoans"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["InvoiceAmount"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["loandeductcount"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["loandeductamount"]) + Convert.ToString(dt.Rows[i]["PayToVendor"]) + "</b></center></td><td><center><b>" + "</b></center></td><td><center><b>" +  Convert.ToString(dt.Rows[i]["Remark"]) + "</b><td><center><b>" + Convert.ToString(dt.Rows[i]["AddedDate"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["HoldRemark"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["HoldBy"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["HoldDate"]) + "</b></center></td></tr>");
                    }
                }

                htmlBody.Append("</table>");
                htmlBody.Append("<br /><br /><table width=\"650px\" style='font-size:13px;'><tr><td align=\"left\">Thanks,<br />Infinity </td></tr> <tr> <br /><td align=\"center\"><b>!!! This is software generated e-mail...Please do not reply. !!!</td></tr></table>");
                htmlBody.Append("<table width=\"600px\" style='font-size:10px;'><tr><td align=\"left\"></td></tr> <tr> <br /><td align=\"center\"><b>" + "***********************************************************************************************************************************************************************************************" + "</td></tr></table>");
                htmlBody.Append("<table width=\"600px\" style='font-size:10px;'><tr><td align=\"left\">" + "<b>CONFIDENTIALITY INFORMATION AND DISCLAIMER</b>" + "</td></tr> <tr> <br /><td align=\"center\"><b></td></tr></table>");
                htmlBody.Append("<table width=\"600px\" style='font-size:12px;'><tr><td align=\"left\">" + "This message contains information which may be confidential and privileged. Unless you are the addressee (or authorized to receive for the addressee), you may not use copy or disclose to anyone the message or any information contained in the message. If you have received the message in error, please advise the sender by reply e-mail and delete the message. Thank you." + "</td></tr><tr><td align=\"center\"><b></td></tr></table>");
                htmlBody.Append("<table width=\"600px\" style='font-size:10px;'><tr><td align=\"left\">" + "***********************************************************************************************************************************************************************************************" + " </td></tr> <tr> <br /><td align=\"center\"><b></td></tr></table>");

                mc.sendEmailInvoiceApproval_IPS(ToAddress, ToCC, "n.nilkanth@infinityinternationals.us,p.kedar@infinityinternationals.us,b.shubhangi@infinityinternationals.us", Subject, Path,"", htmlBody);
            }
            else
            {
                Subject = "Blank Email - Approved  Invoice : " + strCompany + " " + InvType + " " + InvoiceId;
                sendMailForOnlineTracking_Canopy("n.nilkanth@infinityinternationals.us,p.kedar@infinityinternationals.us,b.shubhangi@infinityinternationals.us", ToCC, "n.nilkanth@infinityinternationals.us,p.kedar@infinityinternationals.us", Subject, Path, htmlBody);
            }
        }

        [WebMethod]
        public bool sendEmailInvoiceApproval_IPS(string ToAddress, string ToCC, string ToBCC, string Subject, string Path, string Path1 ,StringBuilder htmlBody)
        {
            try
            {
                String Body = htmlBody.ToString();
                StringBuilder template = new StringBuilder();
                template.Append("<html><head></head><body>");
                //template.Append("<img src=\"http://www.infinity-data.com/images/TemplateHeader.png\" /><br />");
                template.Append(Body);
                //template.Append("<br /><img src=\"http://www.infinity-data.com/images/TemplateFooter.png\" />");
                template.Append("</body></html>");
                MailMessage mail = new MailMessage();

                mail.To.Add(ToAddress);
                //mail.To.Add("jim@infinity-data.com");
                //mail.To.Add("anita@infinity-data.com");

                if (ToCC != "")
                    mail.CC.Add(ToCC);
                if (ToBCC != "")
                    mail.Bcc.Add(ToBCC);
                mail.From = new MailAddress("ack@infinity-data.com", "IPS AP Billing", System.Text.Encoding.UTF8);
                mail.Subject = Subject;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = template.ToString();
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;

                string pass = new bllInvoice().GetPassword("ackdata");
                SmtpClient client = new SmtpClient();
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("ack@infinity-data.com", pass);
                client.Host = "smtp.office365.com";
                client.Port = 587;
                client.EnableSsl = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                try
                {
                    if (Path != "")
                    {
                        Attachment at = new Attachment(Path);
                        at.Name = "Invoice" + ".Doc";
                        mail.Attachments.Add(at);
                    }
                }
                catch { }

                try
                {
                    if (Path1 != "")
                    {
                        Attachment at1 = new Attachment(Path1);
                        at1.Name = "Invoice" + ".Pdf";
                        mail.Attachments.Add(at1);
                    }
                }
                catch { }

                try
                {
                    client.Send(mail);
                }
                catch (Exception ex)
                {
                    //AddException(ex.Message + '~' + pass);
                }
                htmlBody.Remove(0, htmlBody.Length);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [WebMethod]
        public static void sendEmailApproval_Canopy(int InvoiceId, string strcompany,string strStatus)
        {
            StringBuilder htmlBody = new StringBuilder();
            string Subject;
            string ProjectName = "Billing";
            string ProjectType = "";

            DateTime dtime = DateTime.Today;
            string day = dtime.DayOfWeek.ToString();
            string Path = "";

            string ToAddress = "";
            string ToCC = "";

            ToAddress = "jim@infinity-data.com,anita@infinity-data.com,k.adam@infinity-data.com";
            ToCC = "s.chandrakant@infinity-data.com";

            // ToAddress = "p.kedar@infinityinternationals.us"; ToCC = "";

            string BillingDatePeriod = "";
            string strInvoiceType = "";
            DataTable dt = new DataTable();
            dt = new bllInvoice().GetAllPendingInvoice_Canopy(InvoiceId);

            if (dt.Rows.Count > 0)
            {
                //Subject = "Bill is approved for payment" + "-" + Convert.ToString(dt.Rows[0]["NewInvoiceType"]) + "-" + Convert.ToString(dt.Rows[0]["VendorInvoiceNumber"]);

                if (strStatus == "Approve")
                {
                    Subject = "Bill is approved for payment" + "-" + Convert.ToString(dt.Rows[0]["NewInvoiceType"]) + "-" + Convert.ToString(dt.Rows[0]["VendorInvoiceNumber"]);
                }
                else
                {
                    Subject = "Bill is On Hold for payment" + "-" + Convert.ToString(dt.Rows[0]["NewInvoiceType"]) + "-" + Convert.ToString(dt.Rows[0]["VendorInvoiceNumber"]);
                }

                htmlBody.Append("<br />Dear Sir/Madam,<br />");

                htmlBody.Append("<br /><font color=brown face=Verdana size=2 ><b> Below vendor payments are approved." + BillingDatePeriod + ".</b></font><br />");

                htmlBody.Append("<br /><table border=\"1\" bordercolor='Black' style='border:solid 1px black;border-collapse:collapse;padding:3px; font-size:14px;'><tr style='background-color:#244b73; color:White;'><td colspan=15><center><b>" + ProjectName + "-Details " + BillingDatePeriod + "  </b></center></td></tr>");
                htmlBody.Append("<tr><td colspan='12' style='border:none; height:3px; background-color:White;'></td></tr>");
                if (strStatus == "Approve")
                {
                    htmlBody.Append("<tr style='background-color:#244b73; color:White;'><td><center><b>Invoice #</b></center></td><td><center><b>Invoice Type</b></center></td><td><center><b>Billing Period</b></center></td><td><center><b>Invoice Date</b></center></td><td><center><b>Due Date</b></center></td><td><center><b>No of Loans/Orders</b></center></td><td><center><b>Amount</b></center></td><td><center><b>Loan Deduct Count</b></center></td><td><center><b>Loan Deduct Amount</b></center></td><td><center><b>Payble To Vendor</b></center></td><td><center><b>Verification Remark</b></center></td><td><center><b>Invoice Added in System On </b></center></td><td><center><b>Approved Remark</b></center></td><td><center><b>Approved By</b></center></td><td><center><b>Approved Date</b></center></td></tr>");
                }

                else
                {
                     htmlBody.Append("<tr style='background-color:#244b73; color:White;'><td><center><b>Invoice #</b></center></td><td><center><b>Invoice Type</b></center></td><td><center><b>Billing Period</b></center></td><td><center><b>Invoice Date</b></center></td><td><center><b>Due Date</b></center></td><td><center><b>No of Loans/Orders</b></center></td><td><center><b>Amount</b></center></td><td><center><b>Loan Deduct Count</b></center></td><td><center><b>Loan Deduct Amount</b></center></td><td><center><b>Payble To Vendor</b></center></td><td><center><b>Verification Remark</b></center></td><td><center><b>Invoice Added in System On </b></center></td><td><center><b>Hold Remark</b></center></td><td><center><b>Hold By</b></center></td><td><center><b>Hold Date</b></center></td></tr>");
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (strStatus == "Approve")
                    {
                        htmlBody.Append("<tr style='background-color:White; color:black;'><td><center><b>" + Convert.ToString(dt.Rows[i]["VendorInvoiceNumber"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["NewInvoiceType"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["BillingPeriod"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["StatementDate"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["DueDate"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["NoOfLoans"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["InvoiceAmount"]) + Convert.ToString(dt.Rows[i]["loandeductcount"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["loandeductamount"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["PayToLM"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["Remark"]) + "</b><td><center><b>" + Convert.ToString(dt.Rows[i]["AddedDate"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["ApprovedRemark"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["ApprovedBy"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["ApprovedDate"]) + "</b></center></td></tr>");
                    }
                    else
                    {
                        htmlBody.Append("<tr style='background-color:White; color:black;'><td><center><b>" + Convert.ToString(dt.Rows[i]["VendorInvoiceNumber"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["NewInvoiceType"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["BillingPeriod"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["StatementDate"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["DueDate"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["NoOfLoans"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["InvoiceAmount"]) + Convert.ToString(dt.Rows[i]["loandeductcount"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["loandeductamount"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["PayToLM"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["Remark"]) + "</b><td><center><b>" + Convert.ToString(dt.Rows[i]["AddedDate"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["HoldRemark"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["HoldBy"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["HoldDate"]) + "</b></center></td></tr>");
                    }
                }

                htmlBody.Append("</table>");
                htmlBody.Append("<br /><br /><table width=\"650px\" style='font-size:13px;'><tr><td align=\"left\">Thanks,<br />Infinity </td></tr> <tr> <br /><td align=\"center\"><b>!!! This is software generated e-mail...Please do not reply. !!!</td></tr></table>");
                htmlBody.Append("<table width=\"600px\" style='font-size:10px;'><tr><td align=\"left\"></td></tr> <tr> <br /><td align=\"center\"><b>" + "***********************************************************************************************************************************************************************************************" + "</td></tr></table>");
                htmlBody.Append("<table width=\"600px\" style='font-size:10px;'><tr><td align=\"left\">" + "<b>CONFIDENTIALITY INFORMATION AND DISCLAIMER</b>" + "</td></tr> <tr> <br /><td align=\"center\"><b></td></tr></table>");
                htmlBody.Append("<table width=\"600px\" style='font-size:12px;'><tr><td align=\"left\">" + "This message contains information which may be confidential and privileged. Unless you are the addressee (or authorized to receive for the addressee), you may not use copy or disclose to anyone the message or any information contained in the message. If you have received the message in error, please advise the sender by reply e-mail and delete the message. Thank you." + "</td></tr><tr><td align=\"center\"><b></td></tr></table>");
                htmlBody.Append("<table width=\"600px\" style='font-size:10px;'><tr><td align=\"left\">" + "***********************************************************************************************************************************************************************************************" + " </td></tr> <tr> <br /><td align=\"center\"><b></td></tr></table>");

                sendMailForOnlineTracking_Canopy(ToAddress, ToCC, "n.nilkanth@infinityinternationals.us,p.kedar@infinityinternationals.us", Subject, Path, htmlBody);
            }
            else
            {
                Subject = "Blank Email - Approved  Invoice : " + strcompany + " " + InvoiceId;
                sendMailForOnlineTracking_Canopy("n.nilkanth@infinityinternationals.us,p.kedar@infinityinternationals.us,b.shubhangi@infinityinternationals.us", ToCC, "n.nilkanth@infinityinternationals.us,p.kedar@infinityinternationals.us", Subject, Path, htmlBody);
            }
        }

        [WebMethod]
        public static void sendMailForOnlineTracking_Canopy(string ToAddress, string ToCC, string ToBCC, string Subject, string Path, StringBuilder htmlBody)
        {
            try
            {
                String Body = htmlBody.ToString();
                StringBuilder template = new StringBuilder();
                template.Append("<html><head></head><body>");
                //template.Append("<img src=\"http://www.infinity-data.com/images/TemplateHeader.png\" /><br />");
                template.Append(Body);
                //template.Append("<br /><img src=\"http://www.infinity-data.com/images/TemplateFooter.png\" />");
                template.Append("</body></html>");
               
                MailMessage mail = new MailMessage();
                mail.To.Add(ToAddress);
                if (ToCC != "")
                    mail.CC.Add(ToCC);
                if (ToBCC != "")
                    mail.Bcc.Add(ToBCC);
                
                mail.From = new MailAddress("ack@infinity-data.com", "Canopy AP Billing", System.Text.Encoding.UTF8);
                mail.Subject = Subject;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = template.ToString();
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;

                string pass = new bllInvoice().GetPassword("ackdata");
                SmtpClient client = new SmtpClient();
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("ack@infinity-data.com", pass);
                client.Host = "smtp.office365.com";
                client.Port = 587;
                client.EnableSsl = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;


                try
                {
                    if (Path != "")
                    {
                        Attachment at = new Attachment(Path);
                        at.Name = "Invoice" + ".pdf";
                        mail.Attachments.Add(at);
                    }
                }
                catch { }

                try
                {
                    client.Send(mail);
                }
                catch (Exception ex)
                {
                    //AddException(ex.Message + '~' + pass);
                }
                htmlBody.Remove(0, htmlBody.Length);
                //return true;
            }
            catch (Exception ex)
            {
                //return false;
            }
        }




        [WebMethod]
        public static string DownloadInvoiceFile(int invoiceId, string InvoiceType,string Company)
        {
            try
            {
              
                DataTable dt = new bllInvoice().GetInvoiceFilePath(invoiceId.ToString(), InvoiceType);
                string filePath = string.Empty;

                if (dt != null && dt.Rows.Count > 0)
                {
                    filePath = dt.Rows[0]["FilePath"]?.ToString();
                }

                if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                {
                    byte[] fileBytes = File.ReadAllBytes(filePath);

                    string originalFileName = Path.GetFileName(filePath);
                    string fileExtension = Path.GetExtension(filePath);
                    string fileNameOnly = Path.GetFileNameWithoutExtension(filePath);

                    string safeCompany = string.IsNullOrEmpty(Company) ? "Company" : string.Join("_", Company.Split(Path.GetInvalidFileNameChars()));

                    string finalFileName = $"{safeCompany}_{fileNameOnly}{fileExtension}";

                    string base64String = Convert.ToBase64String(fileBytes);

                    var result = new
                    {
                        Success = true,
                        FileName = finalFileName,
                        Base64Data = base64String
                    };

                    JavaScriptSerializer ser = new JavaScriptSerializer();
                    return ser.Serialize(result);
                }
                else
                {
                    return new JavaScriptSerializer().Serialize(new { Success = false, Message = "Invoice file not found on server." });
                }
            }
            catch (Exception ex)
            {
                return new JavaScriptSerializer().Serialize(new { Success = false, Message = "Error: " + ex.Message });
            }
        }
    }
}