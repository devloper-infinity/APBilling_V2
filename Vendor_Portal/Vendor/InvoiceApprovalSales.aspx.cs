using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
    public partial class InvoiceApprovalSales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static string GetInfinity_SalesInvoiceDetails_Approval()
        {

            DataTable dt1 = new bllInvoice().GetInfinity_SalesInvoiceDetails_Approval();
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
        public static int SalesApprovalInvoice(int InvoiceId, string Remark, string strCompany, string InvType, string status, string Approval_PaybleToVendor)
        {
            int ReturnValue = 0;
            var mc = new InvoiceApproval();

            Hashtable htParam = new Hashtable();
            htParam.Add("InvoiceId", InvoiceId);
            htParam.Add("IsApproved", Convert.ToBoolean("True"));
            htParam.Add("Remark", Remark);
            htParam.Add("ApprovedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
            htParam.Add("Status", status);
            htParam.Add("PaybleToVendor", Approval_PaybleToVendor);

           ReturnValue = new bllInvoice().ApprovedInvoice_Sales(htParam);

           if (ReturnValue == 0)
           {
              return 0;
           }

            ReturnValue = InvoiceId;
            if (ReturnValue > 0)
            {
                SendApprovalEmail_Sales(InvoiceId, strCompany, status);        
            }
            return ReturnValue;
        }


        [WebMethod]
        public static void SendApprovalEmail_Sales(int InvoiceId, string strCompany,  string strStatus)
        {
            StringBuilder htmlBody = new StringBuilder();
            string Subject = string.Empty;
            string ProjectName = "Billing";
            
            // Mail recipients
            string ToAddress = "k.aarti@infinity-data.com";
            string ToCC = "p.kedar@infinity-data.com"; 
            string ToBCC = "k.aarti@infinity-data.com";

            string BillingDatePeriod = "";
            DataTable dt = new bllInvoice().GetAllPendingInvoice_IPS(InvoiceId);

            bool isApproved = string.Equals(strStatus, "Approve", StringComparison.OrdinalIgnoreCase);

            if (dt != null && dt.Rows.Count > 0)
            {
                string invoiceType = Convert.ToString(dt.Rows[0]["NewInvoiceType"]);
                string vendorInvNo = Convert.ToString(dt.Rows[0]["VendorInvoiceNumber"]);
                string ToCompany = Convert.ToString(dt.Rows[0]["ToCompany"]);
                string VendorName = Convert.ToString(dt.Rows[0]["VendorName"]);
                string VendorType = Convert.ToString(dt.Rows[0]["VendorType"]);

                string statusText = isApproved ? "Bill is approved for payment" : "Bill is Hold for payment";

                Subject = $"{ToCompany} - {statusText}-{invoiceType}-{vendorInvNo}-{VendorName}-{VendorType}";

                string statusIntro = isApproved
                    ? "Below vendor payments are approved."
                    : "Below vendor payments are on hold.";
                int colCount = isApproved ? 12 : 11;
                htmlBody.Append("<br />Dear Sir/Madam,<br />");
                htmlBody.Append("<br /><font color=brown face=Verdana size=2><b> " + statusIntro + BillingDatePeriod + "</b></font><br />");
                htmlBody.Append("<br /><table border=\"1\" bordercolor='Black' style='border:solid 1px black;border-collapse:collapse;padding:3px; font-size:14px;'>");
                htmlBody.Append("<tr style='background-color:#244b73; color:White;'><td colspan='" + colCount + "'><center><b>" + ProjectName + "-Details " + BillingDatePeriod + "</b></center></td></tr>");
                htmlBody.Append("<tr><td colspan='" + colCount + "' style='border:none; height:3px; background-color:White;'></td></tr>");

                if (isApproved)
                {
                    htmlBody.Append("<tr style='background-color:#244b73; color:White;'>" +
                        "<td><center><b>Invoice #</b></center></td>" +
                        "<td><center><b>Invoice Type</b></center></td>" +
                        "<td><center><b>Billing Period</b></center></td>" +
                        "<td><center><b>Invoice Date</b></center></td>" +
                        "<td><center><b>Due Date</b></center></td>" +
                        "<td><center><b>Amount</b></center></td>" +   
                        "<td><center><b>Payble To Vendor</b></center></td>" +
                        "<td><center><b>Verification Remark</b></center></td>" +
                        "<td><center><b>Invoice Added in System On </b></center></td>" +
                        "<td><center><b>Approved Remark</b></center></td>" +
                        "<td><center><b>Approved By</b></center></td>" +
                        "<td><center><b>Approved Date</b></center></td></tr>");

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        htmlBody.Append("<tr style='background-color:White; color:black;'>" +
                            "<td><center><b>" + Convert.ToString(dr["VendorInvoiceNumber"]) + "</b></center></td>" +
                            "<td><center><b>" + Convert.ToString(dr["NewInvoiceType"]) + "</b></center></td>" +
                            "<td><center><b>" + Convert.ToString(dr["BillingPeriod"]) + "</b></center></td>" +
                            "<td><center><b>" + Convert.ToString(dr["StatementDate"]) + "</b></center></td>" +
                            "<td><center><b>" + Convert.ToString(dr["DueDate"]) + "</b></center></td>" +
                            "<td><center><b>" + Convert.ToString(dr["InvoiceAmount"]) + "</b></center></td>" +      
                            "<td><center><b>" + Convert.ToString(dr["PayToVendor"]) + "</b></center></td>" +
                            "<td><center><b>" + Convert.ToString(dr["Remark"]) + "</b></center></td>" +
                            "<td><center><b>" + Convert.ToString(dr["AddedDate"]) + "</b></center></td>" +
                            "<td><center><b>" + Convert.ToString(dr["ApprovedRemark"]) + "</b></center></td>" +
                            "<td><center><b>" + Convert.ToString(dr["ApprovedBy"]) + "</b></center></td>" +
                            "<td><center><b>" + Convert.ToString(dr["ApprovedDate"]) + "</b></center></td></tr>");
                    }
                }
                else
                {
                    htmlBody.Append("<tr style='background-color:#244b73; color:White;'>" +
                        "<td><center><b>Invoice #</b></center></td>" +
                        "<td><center><b>Invoice Type</b></center></td>" +
                        "<td><center><b>Billing Period</b></center></td>" +
                        "<td><center><b>Invoice Date</b></center></td>" +
                        "<td><center><b>Due Date</b></center></td>" +
                        "<td><center><b>Amount</b></center></td>" +
                        "<td><center><b>Payble To Vendor</b></center></td>" +
                        "<td><center><b>Verification Remark</b></center></td>" +
                        "<td><center><b>Invoice Added in System On </b></center></td>" +
                        "<td><center><b>Hold Remark</b></center></td>" +
                        "<td><center><b>Hold Date</b></center></td></tr>");

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        htmlBody.Append("<tr style='background-color:White; color:black;'>" +
                            "<td><center><b>" + Convert.ToString(dr["VendorInvoiceNumber"]) + "</b></center></td>" +
                            "<td><center><b>" + Convert.ToString(dr["NewInvoiceType"]) + "</b></center></td>" +
                            "<td><center><b>" + Convert.ToString(dr["BillingPeriod"]) + "</b></center></td>" +
                            "<td><center><b>" + Convert.ToString(dr["StatementDate"]) + "</b></center></td>" +
                            "<td><center><b>" + Convert.ToString(dr["DueDate"]) + "</b></center></td>" +                      
                            "<td><center><b>" + Convert.ToString(dr["InvoiceAmount"]) + "</b></center></td>" +     
                            "<td><center><b>" + Convert.ToString(dr["PayToVendor"]) + "</b></center></td>" +
                            "<td><center><b>" + Convert.ToString(dr["Remark"]) + "</b></center></td>" +
                            "<td><center><b>" + Convert.ToString(dr["AddedDate"]) + "</b></center></td>" +
                            "<td><center><b>" + Convert.ToString(dr["IsHoldRemark"]) + "</b></center></td>" +  
                            "<td><center><b>" + Convert.ToString(dr["IsHoldDate"]) + "</b></center></td></tr>");
                    }
                }

                htmlBody.Append("</table>");
                htmlBody.Append("<br /><br /><table width=\"650px\" style='font-size:13px;'><tr><td align=\"left\">Thanks,<br />Infinity </td></tr> <tr> <br /><td align=\"center\"><b>!!! This is software generated e-mail...Please do not reply. !!!</b></td></tr></table>");
                htmlBody.Append("<table width=\"600px\" style='font-size:10px;'><tr><td align=\"center\"><b>***********************************************************************************************************************************************************************************************</b></td></tr></table>");
                htmlBody.Append("<table width=\"600px\" style='font-size:10px;'><tr><td align=\"left\"><b>CONFIDENTIALITY INFORMATION AND DISCLAIMER</b></td></tr></table>");
                htmlBody.Append("<table width=\"600px\" style='font-size:12px;'><tr><td align=\"left\">This message contains information which may be confidential and privileged. Unless you are the addressee (or authorized to receive for the addressee), you may not use copy or disclose to anyone the message or any information contained in the message. If you have received the message in error, please advise the sender by reply e-mail and delete the message. Thank you.</td></tr></table>");
                htmlBody.Append("<table width=\"600px\" style='font-size:10px;'><tr><td align=\"center\"><b>***********************************************************************************************************************************************************************************************</b></td></tr></table>");

                sendMailForOnlineTracking_Sales(ToAddress, ToCC, ToBCC, Subject, htmlBody);
            }
            else
            {
                Subject = "Blank Email - Approved  Invoice : " + strCompany + " " + "InvType" + " " + InvoiceId;
                sendMailForOnlineTracking_Sales(ToAddress, ToCC, ToBCC, Subject, htmlBody);
            }
        }

        public static void sendMailForOnlineTracking_Sales(string ToAddress, string ToCC, string ToBCC, string Subject, StringBuilder htmlBody)
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
                    client.Send(mail);
                }
                catch (Exception ex)
                {
                    //AddException(ex.Message + '~' + pass);
                }
                htmlBody.Remove(0, htmlBody.Length);
               
            }
            catch (Exception)
            {
            }
        }
    

}
}