using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
        [WebMethod(EnableSession = true)] public static object VerifyApproveCostOtp(string otp) { DateTime e = CostMasterOtpSecurity.VerifyOtp(otp, "VENDORREGISTRATION_MASTER"); return new { message = "Verification successful. You can access Vendor Registration Page for the next 20 minutes.", expiryUtc = e.ToString("o") }; }
        [WebMethod(EnableSession = true)] public static string RestartApproveCostVerification() { CostMasterOtpSecurity.RestartVerification("VENDORREGISTRATION_MASTER"); return "Enter your Employee Code to request a new OTP."; }

        [WebMethod(EnableSession = true)]
        public static int InsertVendorRegistration(string vendorName,string companyName,string contactPerson,string contactNo,string emailID,
            string websiteURL,string address,string payToVendorName,string accountNumber,string remark,int vendorId,string VendorType)
        {
            CostMasterOtpSecurity.RequireCsrf(); CostMasterOtpSecurity.RequireAuthorization("VENDORREGISTRATION_MASTER");
            int returnValue = 0;
            Hashtable htParam = new Hashtable();

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

            htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));

            try
            {
                returnValue = new bllInvoice().InsertVendorRegistration(htParam);
            }
            catch (Exception ex)
            {
                returnValue = 0;
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



        [System.Web.Services.WebMethod]
        public static List<object> GetVendorList()
        {
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




        [System.Web.Services.WebMethod]
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



        [System.Web.Services.WebMethod]
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

    }
}