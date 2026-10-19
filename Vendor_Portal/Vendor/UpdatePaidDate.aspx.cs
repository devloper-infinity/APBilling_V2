using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using Vendor_Portal.App_Code.BLL;
using System.Text;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Net;
using System.Security.Authentication;

namespace Vendor_Portal.Vendor
{
    public partial class UpdatePaidDate : System.Web.UI.Page
    {
        static SqlConnection con = new SqlConnection("Data Source=23.111.175.186;Initial Catalog=InfinityERP;Persist Security Info=True;User ID=sa;Password=#Cl0ud^$ecure4; Pooling=true; Min Pool Size=1; Max Pool Size=10; Connect Timeout=200; Packet Size=8192");

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string GetInfinityInvoicesForUpdatePaidDate(string Company)
        {
            DataTable dt1 = new bllInvoice().GetInfinityInvoicesForUpdatePaidDate(int.Parse(HttpContext.Current.User.Identity.Name.ToString()), Company);
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
        public static int UpdateRemark(string InvoiceIDs, string Remark, string PaymentDate, string Company, string UTR, string ToAddress, string ToCC, string ToBCC)
        {
            int ReturnValue = 0;
            try
            {
                string str_split = InvoiceIDs.Substring(2);
                string[] IDs = str_split.Split(',');

                foreach (var sub_str in IDs)
                {
                    Hashtable htParam = new Hashtable();
                    htParam.Add("InvoiceID", sub_str);
                    htParam.Add("PaymentDate", PaymentDate);
                    htParam.Add("Remark", Remark);
                    htParam.Add("UTR", UTR);
                    htParam.Add("Company", Company);
                    htParam.Add("UpdatedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));

                    ReturnValue =  new bllInvoice().UpdateRemark(htParam);

                    if (ReturnValue > 0)
                    {
                        SendEmail_Body_UpdateDate(Convert.ToInt32(sub_str), Company, ToAddress, ToCC, ToBCC);
                        //SendEmail_Body_UpdateDate(10419, "Canopy", "", "", "");
                        
                    }
                }
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                SqlCommand cmd1 = new SqlCommand("AddExeceptionMessage", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Message", ex.Message);
                cmd1.CommandTimeout = 0;
                cmd1.ExecuteNonQuery();
                con.Close();
            }
            return ReturnValue;
        }

        [WebMethod]
        public static void SendEmail_Body_UpdateDate(int InvoiceID, string Company, string ToAddress, string ToCC, string ToBCC)
        {
            try
            {
                string PayTo = string.Empty;
                string AccNo = string.Empty;
                string Subject = string.Empty;
                string PaymentFrom = string.Empty;
                string FromMailAddress = string.Empty;

                System.Text.StringBuilder htmlBody = new StringBuilder();
                DataTable dt = null;

                if (Company == "IPS")
                {
                    PaymentFrom = "Infinity IPS Inc";
                    dt = new bllInvoice().GetInvoiceDetailsForUpdateDateByID_IPS(InvoiceID);

                }
                else if (Company == "Canopy")
                {
                    PaymentFrom = "Canopy";
                    dt = new bllInvoice().GetInvoiceDetailsForUpdateDateByID_Canopy(InvoiceID);
                }

                if (dt.Rows.Count > 0)
                {
                    ToAddress = Convert.ToString(dt.Rows[0]["EmailID"]);
                    ToCC = Convert.ToString(dt.Rows[0]["ToCC"]);
                    ToBCC = Convert.ToString(dt.Rows[0]["ToBCC"]);
                    FromMailAddress = Convert.ToString(dt.Rows[0]["FromMailAddress"]);

                    if (Company == "IPS")
                    {

                        Subject = "Issued Payment Intimation Infinity " + Company + " " + Convert.ToString(dt.Rows[0]["InvoiceType"]) + " - Invoice # " + Convert.ToString(dt.Rows[0]["InvoiceNo"]);
                    }

                    else
                    {
                        Subject = "Issued Payment Intimation " + Company + " " + Convert.ToString(dt.Rows[0]["InvoiceType"]) + " - Invoice # " + Convert.ToString(dt.Rows[0]["InvoiceNo"]);
                    }



                    htmlBody.Append("<table width=\"500px\" ><tr><td align=\"left\"><br /><b>Dear Sir/Madam,</b><br /><br /></td></tr>");
                    htmlBody.Append("<table style='border:solid 1px Gainsboro;' width=\"500px\"><tr><td>");
                    htmlBody.Append("<table width=\"500px\"><tr><td>");
                    htmlBody.Append("<tr><td align=\"left\"><b>Remittance Information</b></td></tr>");
                    htmlBody.Append("<tr><td align=\"left\">Payment From :</td><td>" + PaymentFrom + "</td></tr>");
                    htmlBody.Append("<tr><td>Payment To :</td><td>" + Convert.ToString(dt.Rows[0]["InvoiceType"]) + "</td></tr>");
                    htmlBody.Append("<tr><td>Payment Method :</td><td>ACH</td></tr>");
                    htmlBody.Append("<tr><td>Payment Amount :</td><td>" + Convert.ToString(dt.Rows[0]["BalanceNew"]) + "</td></tr>");
                    htmlBody.Append("<tr><td>Payment Date :</td><td>" + Convert.ToString(dt.Rows[0]["PaidDate"]) + "</td></tr>");
                    htmlBody.Append("<tr><td width=\"230px\">Payment Reference Number : </td><td>" + Convert.ToString(dt.Rows[0]["UTRNo"]) + "</td></tr></table>");

                    htmlBody.Append("<br /><b>Invoice Details</b>");

                    htmlBody.Append("<table style='border-collapse:collapse;' border=\"1\" bordercolor='Black' width=\"500px\"><tr style='background-color:Gainsboro; color:Black;border:solid 1px black;'><td style='padding - left:10px; padding - right:10px;'><center><b>Invoice Number</b></center></td><td style='padding - left:10px; padding - right:10px;'><center><b>Invoice Date</b></center></td><td style='padding - left:10px; padding - right:10px;'><center><b>Invoice Amount</b></center></td><td style='padding - left:10px; padding - right:10px;'><center><b>Payment Amount</b></center></td><td style='padding - left:10px; padding - right:10px;'><center><b>Notes</b></center></td></tr>");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        htmlBody.Append("<tr style='background-color:White; color:black;border:solid 1px black;'><td><center>" + Convert.ToString(dt.Rows[i]["InvoiceNo"]) + "</center></td><td><center>" + Convert.ToString(dt.Rows[i]["InvoiceDate"]) + "</center></td><td><center>" + Convert.ToString(dt.Rows[i]["Balance"]) + " " + Convert.ToString(dt.Rows[i]["Currency"]) + "</center></td><td><center>" + Convert.ToString(dt.Rows[i]["Balance"]) + " " + Convert.ToString(dt.Rows[i]["Currency"]) + "</center></td><td><center>" + Convert.ToString(dt.Rows[i]["Notes"]) + "</center></td></tr>");
                    }

                    htmlBody.Append("</table>");

                    //htmlBody.Append("<table width=\"500px\" ><tr><td align=\"left\"><br />Infinity IPS. has initiated payment for the listed invoices and is scheduled to credit the bank <br /> account ending in " + Convert.ToString(dt.Rows[0]["AccNo"]) + "on the Payment Date.</td></tr><tr>");
                    htmlBody.Append("<table width=\"500px\" ><tr><td align=\"left\"><br />Infinity IPS. has initiated payment for the listed invoices and is scheduled to credit the bank <br /> account ending in " + Convert.ToString(dt.Rows[0]["AccNo"]) + " within two working days.</td></tr><tr>");
                    htmlBody.Append("<br />For any issues or questions related to this payment, please contact the Accounts Payable <br /> department of Infinity IPS at ap@infinity-data.com");
                    htmlBody.Append("</table>");
                    htmlBody.Append("<br /><br /><table width=\"500px\"><tr><td align=\"left\">Thank you</td></tr></table>");
                    htmlBody.Append("<br /><br /><br /></td></tr></table>");

                    sendEmail_UpdateDate(ToAddress, ToCC, ToBCC, Subject, FromMailAddress, Company, htmlBody);
                }
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                SqlCommand cmd1 = new SqlCommand("AddExeceptionMessage", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Message", ex.Message);
                cmd1.CommandTimeout = 0;
                cmd1.ExecuteNonQuery();
                con.Close();
            }
        }

        [WebMethod]
        public static bool sendEmail_UpdateDate(string ToAddress, string ToCC, string ToBCC, string Subject,string FromMailAddress, string Company, StringBuilder htmlBody)
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

                mail.From = new MailAddress(FromMailAddress, "Infinity IPS", System.Text.Encoding.UTF8);
                //mail.From = new MailAddress("paymentintimation@canopytpr.com", "Canopy", System.Text.Encoding.UTF8);
                mail.Subject = Subject;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = template.ToString();
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;

                string pass = new bllInvoice().GetPassword(Company);
                mail.Priority = System.Net.Mail.MailPriority.High;

                SmtpClient client = new SmtpClient();
                client.Credentials = new System.Net.NetworkCredential(FromMailAddress, pass);
                //client.Credentials = new System.Net.NetworkCredential("paymentintimation@canopytpr.com", "pi@02022026");
                client.Host = "smtp.office365.com";
                client.UseDefaultCredentials = false;
                client.Port = 587;
                client.EnableSsl = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;



                try
                {
                     client.Send(mail);
                }
                catch (Exception ex)
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    SqlCommand cmd1 = new SqlCommand("AddExeceptionMessage", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@Message", ex.Message);
                    cmd1.CommandTimeout = 0;
                    cmd1.ExecuteNonQuery();
                    con.Close();
                }
                htmlBody.Remove(0, htmlBody.Length);
                return true;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                SqlCommand cmd1 = new SqlCommand("AddExeceptionMessage", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Message", ex.Message);
                cmd1.CommandTimeout = 0;
                cmd1.ExecuteNonQuery();
                con.Close();
                return false;
            }
        }
    }
}