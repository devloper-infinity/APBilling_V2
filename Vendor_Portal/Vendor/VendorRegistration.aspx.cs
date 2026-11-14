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
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [System.Web.Services.WebMethod] 
        public static int InsertVendorRegistration(string vendorName,string companyName,string contactPerson,string contactNo,string emailID,
            string websiteURL,string address,string payToVendorName,string accountNumber,string remark,int vendorId,string VendorType)
        {
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


        [System.Web.Services.WebMethod]
        public static int AddVendorOtherInfo(int vendorRegistrationID, string startDate,string rateRevisionDate,string scopeOfWork,string remark,
        string isNDASigned,string agreementDate,string expiryDate,string signedByClient,string signedByInfinity,string workDocument,string agreementCopy)
        {
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
                    string originalFileName = parts[0]; // Actual File Name
                    //string uniqueFileName = DateTime.Now.Ticks.ToString() + "_" + originalFileName;

                    string base64Data = parts[1].Substring(parts[1].IndexOf(",") + 1);
                    byte[] fileBytes = Convert.FromBase64String(base64Data);

                    // Full Physical Server Path (e.g., C:\...\WorkDocument\filename.pdf)
                    string fullPath = workFolderPath + originalFileName;
                    System.IO.File.WriteAllBytes(fullPath, fileBytes);

                    // Database la full path + actual file name sathi (hava tar tu fat path save kru shaktoes)
                    savedWorkDocPath = fullPath; // Full Path
                }
                else
                {
                    savedWorkDocPath = workDocument; // Jar user ne file change keli nsel tar juna path tasaach rahato
                }

                // --- AgreementCopy File Save Logic ---
                string savedAgreementPath = string.Empty;
                if (!string.IsNullOrEmpty(agreementCopy) && agreementCopy.Contains("|"))
                {
                    string[] parts = agreementCopy.Split('|');
                    string originalFileName = parts[0]; // Actual File Name
                   // string uniqueFileName = DateTime.Now.Ticks.ToString() + "_" + originalFileName;

                    string base64Data = parts[1].Substring(parts[1].IndexOf(",") + 1);
                    byte[] fileBytes = Convert.FromBase64String(base64Data);

                    // Full Physical Server Path
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




        [System.Web.Services.WebMethod]
        public static int InsertVendorRate(int vendorRegistrationID, string fromDate, string rateType, string rate,string Process,int rateID,string BillingCycle)
        {
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


        [WebMethod]
        public static string GetVendorDetailsByID(int vendorRegistrationID)
        {
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


        [WebMethod]
        public static string GetRateConfiguration(int  VendorId)
        {
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


        [WebMethod]
        public static int ApproveVendorRate(int rateID, int vendorRegistrationID)
        {


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

    }
}