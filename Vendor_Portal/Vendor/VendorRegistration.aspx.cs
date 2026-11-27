using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
    public partial class VendorRegistration : System.Web.UI.Page
    {
        protected string ApproveCostCsrfToken { get; private set; }
        protected string ApproveCostExpiryUtc { get; private set; }
        protected bool ApproveCostAuthorized { get; private set; }
        protected bool ApproveCostAccessAllowed { get; private set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            ApproveCostCsrfToken = CostMasterOtpSecurity.EnsureCsrfToken();
            ApproveCostAccessAllowed = CostMasterOtpSecurity.HasPageAccess("VENDORREGISTRATION_MASTER");
            if (!ApproveCostAccessAllowed) return;
            DateTime? expiry = CostMasterOtpSecurity.GetAuthorizationExpiryUtc("VENDORREGISTRATION_MASTER");
            ApproveCostAuthorized = expiry.HasValue;
            ApproveCostExpiryUtc = expiry.HasValue ? expiry.Value.ToString("o") : "";
        }
        [WebMethod(EnableSession = true)] public static string RequestApproveCostOtp(string employeeCode) { return CostMasterOtpSecurity.SendOtp(employeeCode, "VENDORREGISTRATION_MASTER"); }
        [WebMethod(EnableSession = true)] public static object VerifyApproveCostOtp(string otp) { DateTime e = CostMasterOtpSecurity.VerifyOtp(otp, "VENDORREGISTRATION_MASTER"); return new { message = "Verification successful. You can access Vendor Registration Page for the next 15 minutes.", expiryUtc = e.ToString("o") }; }
        [WebMethod(EnableSession = true)] public static string RestartApproveCostVerification() { CostMasterOtpSecurity.RestartVerification("VENDORREGISTRATION_MASTER"); return "Enter your Employee Code to request a new OTP."; }

        [WebMethod(EnableSession = true)]
        public static int InsertVendorRegistration(string vendorName,string companyName,string contactPerson,string contactNo,string emailID,
            string websiteURL,string address,string payToVendorName,string accountNumber,string remark,int vendorId,string VendorType,string VendorDomain,string VendorPaymentMethod,string VendorRoutingNo, string taxSSNEIN,
            string taxID, string paymentDocument, string w9FormDocument)
        {
           CostMasterOtpSecurity.RequireCsrf(); CostMasterOtpSecurity.RequireAuthorization("VENDORREGISTRATION_MASTER");
            int returnValue = 0;
            var mc_VendorRegistration = new VendorRegistration();
            Hashtable htParam = new Hashtable();


            string paymentFolderPath = HttpContext.Current.Server.MapPath("~/VendorDocument/PaymentDocument/");
            if (!System.IO.Directory.Exists(paymentFolderPath))
            {
                System.IO.Directory.CreateDirectory(paymentFolderPath);
            }

            string w9FolderPath = HttpContext.Current.Server.MapPath("~/VendorDocument/W9FormDocument/");
            if (!System.IO.Directory.Exists(w9FolderPath))
            {
                System.IO.Directory.CreateDirectory(w9FolderPath);
            }

            // --- PaymentDocument File Save Logic ---
            string savedPaymentDocPath = string.Empty;
            if (!string.IsNullOrEmpty(paymentDocument) && paymentDocument.Contains("|"))
            {
                string[] parts = paymentDocument.Split('|');
                string originalFileName = System.IO.Path.GetFileName(parts[0]);
                string base64Data = parts[1].Substring(parts[1].IndexOf(",") + 1);
                byte[] fileBytes = Convert.FromBase64String(base64Data);

                string fullPath = System.IO.Path.Combine(paymentFolderPath, originalFileName);
                System.IO.File.WriteAllBytes(fullPath, fileBytes);
                savedPaymentDocPath = fullPath;
            }
            else
            {
                savedPaymentDocPath = paymentDocument;
            }

            // --- W9FormDocument File Save Logic ---
            string savedW9DocPath = string.Empty;
            if (!string.IsNullOrEmpty(w9FormDocument) && w9FormDocument.Contains("|"))
            {
                string[] parts = w9FormDocument.Split('|');
                string originalFileName = System.IO.Path.GetFileName(parts[0]);
                string base64Data = parts[1].Substring(parts[1].IndexOf(",") + 1);
                byte[] fileBytes = Convert.FromBase64String(base64Data);

                string fullPath = System.IO.Path.Combine(w9FolderPath, originalFileName);
                System.IO.File.WriteAllBytes(fullPath, fileBytes);
                savedW9DocPath = fullPath;
            }
            else
            {
                savedW9DocPath = w9FormDocument;
            }

            htParam.Add("VendorName", vendorName);
            htParam.Add("CompanyName", companyName);
            htParam.Add("ContactPerson", contactPerson);
            htParam.Add("ContactNo", contactNo);
            htParam.Add("EmailID", emailID);
            htParam.Add("WebsiteURL", websiteURL);
            htParam.Add("Address", address);
            htParam.Add("PayToVendorName", payToVendorName);
            htParam.Add("AccountNumber", accountNumber);
            htParam.Add("Remark", remark);
            htParam.Add("vendorId", vendorId);
            htParam.Add("VendorType", VendorType);
            htParam.Add("VendorDomain", VendorDomain);
            htParam.Add("VendorPaymentMethod", VendorPaymentMethod);
            htParam.Add("VendorRoutingNo", VendorRoutingNo);
            htParam.Add("TaxSSNEIN", taxSSNEIN);
            htParam.Add("TaxID", taxID);
            htParam.Add("PaymentDocument", string.IsNullOrEmpty(savedPaymentDocPath) ? DBNull.Value : (object)savedPaymentDocPath);
            htParam.Add("W9FormDocument", string.IsNullOrEmpty(savedW9DocPath) ? DBNull.Value : (object)savedW9DocPath);
            htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));

            try
            {
                returnValue = new bllInvoice().InsertVendorRegistration(htParam);
            }
            catch (Exception ex)
            {
                returnValue = 0;
            }
            if (returnValue > 0 && vendorId == 0) 
            {
                mc_VendorRegistration.sendEmailAddNewVendor(returnValue);
            }
            return returnValue;
        }


        [WebMethod(EnableSession = true)]
        public static int AddVendorOtherInfo(int vendorRegistrationID, string startDate,string rateRevisionDate,string scopeOfWork,string remark,
        string isNDASigned,string agreementDate,string expiryDate,string signedByClient,string signedByInfinity,string workDocument,string agreementCopy)
        {
          CostMasterOtpSecurity.RequireCsrf(); CostMasterOtpSecurity.RequireAuthorization("VENDORREGISTRATION_MASTER");
            int returnValue = 0;
            Hashtable htParam = new Hashtable();
            try
            {
                string workFolderPath = HttpContext.Current.Server.MapPath("~/VendorDocument/WorkDocument/");
                if (!System.IO.Directory.Exists(workFolderPath))
                {
                    System.IO.Directory.CreateDirectory(workFolderPath);
                }

                string agreementFolderPath = HttpContext.Current.Server.MapPath("~/VendorDocument/AgreementCopy/");
                if (!System.IO.Directory.Exists(agreementFolderPath))
                {
                    System.IO.Directory.CreateDirectory(agreementFolderPath);
                }

                // --- WorkDocument File Save Logic ---
                string savedWorkDocPath = string.Empty;
                if (!string.IsNullOrEmpty(workDocument) && workDocument.Contains("|"))
                {
                    string[] parts = workDocument.Split('|');
                    string originalFileName = parts[0]; 

                    string base64Data = parts[1].Substring(parts[1].IndexOf(",") + 1);
                    byte[] fileBytes = Convert.FromBase64String(base64Data);

                    string fullPath = workFolderPath + originalFileName;
                    System.IO.File.WriteAllBytes(fullPath, fileBytes);

                    savedWorkDocPath = fullPath; 
                }
                else
                {
                    savedWorkDocPath = workDocument; 
                }

                // --- AgreementCopy File Save Logic ---
                string savedAgreementPath = string.Empty;
                if (!string.IsNullOrEmpty(agreementCopy) && agreementCopy.Contains("|"))
                {
                    string[] parts = agreementCopy.Split('|');
                    string originalFileName = parts[0]; 

                    string base64Data = parts[1].Substring(parts[1].IndexOf(",") + 1);
                    byte[] fileBytes = Convert.FromBase64String(base64Data);

                    string fullPath = agreementFolderPath + originalFileName;
                    System.IO.File.WriteAllBytes(fullPath, fileBytes);

                    savedAgreementPath = fullPath; // Full Path
                }
                else
                {
                    savedAgreementPath = agreementCopy;
                }
                htParam.Add("vendorRegistrationID", vendorRegistrationID);
                htParam.Add("StartDate", string.IsNullOrEmpty(startDate) ? DBNull.Value : (object)Convert.ToDateTime(startDate));
                htParam.Add("RateRevisionDate", string.IsNullOrEmpty(rateRevisionDate) ? DBNull.Value : (object)Convert.ToDateTime(rateRevisionDate));
                htParam.Add("ScopeOfWork", scopeOfWork);
                htParam.Add("OtherInfoRemark", remark);
                htParam.Add("IsNDASigned", isNDASigned);
                if (isNDASigned == "1")
                {
                    htParam.Add("AgreementDate", string.IsNullOrEmpty(agreementDate) ? DBNull.Value : (object)Convert.ToDateTime(agreementDate));
                    htParam.Add("ExpiryDate", string.IsNullOrEmpty(expiryDate) ? DBNull.Value : (object)Convert.ToDateTime(expiryDate));
                    htParam.Add("SignedByClient", signedByClient);
                    htParam.Add("SignedByInfinity", signedByInfinity);
                }
                else
                {
                    htParam.Add("AgreementDate", DBNull.Value);
                    htParam.Add("ExpiryDate", DBNull.Value);
                    htParam.Add("SignedByClient", DBNull.Value);
                    htParam.Add("SignedByInfinity", DBNull.Value);
                }
                htParam.Add("WorkDocument", string.IsNullOrEmpty(savedWorkDocPath) ? DBNull.Value : (object)savedWorkDocPath);
                htParam.Add("AgreementCopy", string.IsNullOrEmpty(savedAgreementPath) ? DBNull.Value : (object)savedAgreementPath);
                htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
                returnValue = new bllInvoice().AddVendorOtherInfo(htParam);  
            }
            catch (Exception ex)
            {
                returnValue = 0;
            }
            return returnValue;
        }




        [WebMethod(EnableSession = true)]
        public static int InsertVendorRate(int vendorRegistrationID, string fromDate, string rateType, string rate,string Process,int rateID,string BillingCycle,int EmployeeID,int savedEmployeeId)
        {
            CostMasterOtpSecurity.RequireCsrf(); CostMasterOtpSecurity.RequireAuthorization("VENDORREGISTRATION_MASTER");
            int returnValue = 0;
            Hashtable htParam = new Hashtable();
            try
            {
                htParam.Add("VendorRegistrationID", vendorRegistrationID);
                htParam.Add("FromDate", fromDate);
                htParam.Add("RateType", rateType);
                htParam.Add("Rate", rate);
                htParam.Add("Process", Process);
                htParam.Add("rateID", rateID);
                htParam.Add("BillingCycle", BillingCycle);
                htParam.Add("EmployeeID", EmployeeID);
                htParam.Add("RateConfigEmpId", savedEmployeeId);


                htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));

                returnValue = new bllInvoice().InsertVendorRate(htParam);
            }
            catch (Exception ex)
            {
                returnValue = 0; 
            }
            return returnValue;
        }



        [WebMethod(EnableSession = true)]
        public static List<object> GetVendorList()
        {
            CostMasterOtpSecurity.RequireCsrf(); CostMasterOtpSecurity.RequireAuthorization("VENDORREGISTRATION_MASTER");

            List<object> vendorList = new List<object>();

            bllInvoice inv = new bllInvoice();
            DataTable dt = inv.GetVendors();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    vendorList.Add(new
                    {
                        VendorRegistrationID = dr["VendorRegistrationID"],
                        VendorName = dr["VendorName"]
                    });
                }
            }

            return vendorList;
        }


        [WebMethod(EnableSession = true)]
        public static string GetVendorDetailsByID(int vendorRegistrationID)
        {
            CostMasterOtpSecurity.RequireCsrf(); CostMasterOtpSecurity.RequireAuthorization("VENDORREGISTRATION_MASTER");
            DataTable dt1 = new bllInvoice().GetVendorDetailsByID(vendorRegistrationID);
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


        [WebMethod(EnableSession = true)]
        public static string GetRateConfiguration(int  VendorId)
        {
           CostMasterOtpSecurity.RequireCsrf(); CostMasterOtpSecurity.RequireAuthorization("VENDORREGISTRATION_MASTER");
            DataTable dt1 = new bllInvoice().GetRateConfiguration(VendorId);
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


        [WebMethod(EnableSession = true)]
        public static int ApproveVendorRate(int rateID, int vendorRegistrationID)
        {

            CostMasterOtpSecurity.RequireCsrf(); CostMasterOtpSecurity.RequireAuthorization("VENDORREGISTRATION_MASTER");
            int returnValue = 0;
            Hashtable htParam = new Hashtable();
            try
            {
                htParam.Add("VendorRegistrationID", vendorRegistrationID);
                htParam.Add("rateID", rateID);
                htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));

                returnValue = new bllInvoice().ApproveVendorRate(htParam);
            }
            catch (Exception ex)
            {
                returnValue = 0;
            }
            return returnValue;
        }




        [WebMethod(EnableSession = true)]
        public static List<object> GetEmployeeForRateConfig()
        {
            List<object> EmployeeList = new List<object>();

            bllInvoice inv = new bllInvoice();
            DataTable dt = inv.GetEmployeeForRateConfig();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    EmployeeList.Add(new
                    {
                        EmployeeID = dr["EmployeeID"],
                        PsuedoName = dr["PsuedoName"]
                    });
                }
            }

            return EmployeeList;
        }



        [WebMethod(EnableSession = true)]
        public static List<object> GetRateConfigProcess()
        {
            List<object> ProcessList = new List<object>();

            bllInvoice inv = new bllInvoice();
            DataTable dt = inv.GetRateConfigProcess();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ProcessList.Add(new
                    {
                        ProcessId = dr["ProcessId"],
                        Process = dr["Process"]
                    });
                }
            }

            return ProcessList;
        }



        public void sendEmailAddNewVendor(int InvoiceId)
        {
            var mc_VendorRegistration = new VendorRegistration();
            StringBuilder htmlBody = new StringBuilder();
            string Subject;
            string ProjectName = "Vendor";
            string ProjectType = "";

            DateTime dtime = DateTime.Today;

            string Path = "";
            string ToAddress = string.Empty;
            string ToCC = string.Empty;

            ToAddress = "k.aarti@infinity-data.com";
            ToCC = "p.kedar@infinity-data.com";

            string BillingDatePeriod = "";

            DataTable dt = new DataTable();
            DataTable dtSummary = new DataTable();
            DataTable dtAdvancePaymentSymmary = new DataTable();

            dt = new bllInvoice().GetVendorDetailsByID(InvoiceId);

            if (dt.Rows.Count > 0)
            {
                string ToCompany = dt.Columns.Contains("CompanyName")
                   ? Convert.ToString(dt.Rows[0]["CompanyName"])
                   : Convert.ToString(dt.Rows[0]["CompanyName"]);

                string VendorName = dt.Columns.Contains("VendorName")
                  ? Convert.ToString(dt.Rows[0]["VendorName"])
                  : Convert.ToString(dt.Rows[0]["VendorName"]);

                string VendorType = dt.Columns.Contains("VendorType")
                  ? Convert.ToString(dt.Rows[0]["VendorType"])
                  : Convert.ToString(dt.Rows[0]["VendorType"]);

    
                Subject = $"{ToCompany} - New Vendor Registered-{VendorName}-{VendorType}";
                htmlBody.Append("<br />Dear Sir/Madam,<br />");
                htmlBody.Append("<br /><font color=brown face=Verdana size=2 ><b>New Vendor Registered in system with below details.</b></font><br />");

                htmlBody.Append("<br /><table border=\"1\" bordercolor='Black' style='border:solid 1px black;border-collapse:collapse;padding:3px; font-size:14px;'><tr style='background-color:#244b73; color:White;'><td colspan=13><center><b>" + ProjectName + "-Details </b></center></td></tr>");
                htmlBody.Append("<tr style='background-color:#244b73; color:White;'>" +
                    "<td><center><b>Vendor Type</b></center></td>" +
                    "<td><center><b>Vendor Name</b></center></td>" +
                    "<td><center><b>Company Name</b></center></td>" +
                    "<td><center><b>Contact Person</b></center></td>" +
                    "<td><center><b>Contact No</b></center></td>" +
                    "<td><center><b>Email ID</b></center></td>" +
                    "<td><center><b>Website URL</b></center></td>" +
                    "<td><center><b>Address</b></center></td>" +
                    "<td><center><b>Pay To Vendor Name</b></center></td>" +                  
                    "<td><center><b>Remark</b></center></td>" +
                    "<td><center><b>Domain</b></center></td>" +
                    "<td><center><b>Tax SSN EIN</b></center></td>" +
                    "<td><center><b>Tax ID</b></center></td>" +
                   
                    "</tr>");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    htmlBody.Append("<tr style='background-color:White; color:black;'>" +
                        "<td><center><b>" + Convert.ToString(dt.Rows[i]["VendorType"]) + "</b></center></td>" +
                        "<td><center><b>" + Convert.ToString(dt.Rows[i]["VendorName"]) + "</b></center></td>" +
                        "<td><center><b>" + Convert.ToString(dt.Rows[i]["CompanyName"]) + "</b></center></td>" +
                        "<td><center><b>" + Convert.ToString(dt.Rows[i]["ContactPerson"]) + "</b></center></td>" +
                        "<td><center><b>" + Convert.ToString(dt.Rows[i]["ContactNo"]) + "</b></center></td>" +
                        "<td><center><b>" + Convert.ToString(dt.Rows[i]["EmailID"]) + "</b></center></td>" +
                        "<td><center><b>" + Convert.ToString(dt.Rows[i]["WebsiteURL"]) + "</b></center></td>" +
                        "<td><center><b>" + Convert.ToString(dt.Rows[i]["Address"]) + "</b></center></td>" +
                        "<td><center><b>" + Convert.ToString(dt.Rows[i]["PayToVendorName"]) + "</b></center></td>" +
                        "<td><center><b>" + Convert.ToString(dt.Rows[i]["Remark"]) + "</b></center></td>" +
                        "<td><center><b>" + Convert.ToString(dt.Rows[i]["Domain"]) + "</b></center></td>" +
                        "<td><center><b>" + Convert.ToString(dt.Rows[i]["TaxSSNEIN"]) + "</b></center></td>" +
                        "<td><center><b>" + Convert.ToString(dt.Rows[i]["TaxID"]) + "</b></center></td>" +
                        "</tr>");
                }

                htmlBody.Append("</table>");

          

                htmlBody.Append("<br /><br /><table width=\"650px\" style='font-size:13px;'><tr><td align=\"left\">Thanks,<br />Infinity </td></tr> <tr> <br /><td align=\"center\"><b>!!! This is software generated e-mail...Please do not reply. !!!</td></tr></table>");
                htmlBody.Append("<table width=\"600px\" style='font-size:10px;'><tr><td align=\"left\"></td></tr> <tr> <br /><td align=\"center\"><b>" + "***********************************************************************************************************************************************************************************************" + "</td></tr></table>");
                htmlBody.Append("<table width=\"600px\" style='font-size:10px;'><tr><td align=\"left\">" + "<b>CONFIDENTIALITY INFORMATION AND DISCLAIMER</b>" + "</td></tr> <tr> <br /><td align=\"center\"><b></td></tr></table>");
                htmlBody.Append("<table width=\"600px\" style='font-size:12px;'><tr><td align=\"left\">" + "This message contains information which may be confidential and privileged. Unless you are the addressee (or authorized to receive for the addressee), you may not use copy or disclose to anyone the message or any information contained in the message. If you have received the message in error, please advise the sender by reply e-mail and delete the message. Thank you." + "</td></tr><tr><td align=\"center\"><b></td></tr></table>");
                htmlBody.Append("<table width=\"600px\" style='font-size:10px;'><tr><td align=\"left\">" + "***********************************************************************************************************************************************************************************************" + " </td></tr> <tr> <br /><td align=\"center\"><b></td></tr></table>");

                mc_VendorRegistration.sendEmailVendorRegistration(ToAddress, ToCC, "k.aarti@infinity-data.com", Subject, Path, htmlBody);
            }
        }

        [WebMethod]
        public bool sendEmailVendorRegistration(string ToAddress, string ToCC, string ToBCC, string Subject, string Path, StringBuilder htmlBody)
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
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }



        [WebMethod(EnableSession = true)]
        public static string GetVendorsDocumentHistory(int VendorId)
        {
            CostMasterOtpSecurity.RequireCsrf(); CostMasterOtpSecurity.RequireAuthorization("VENDORREGISTRATION_MASTER");
            DataTable dt1 = new bllInvoice().GetVendorsDocumentHistory(VendorId);
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
    }
}