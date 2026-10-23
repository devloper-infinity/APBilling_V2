using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Web.Services;
using Vendor_Portal.App_Code.BLL;

namespace Vendor_Portal.Vendor
{
    public partial class CostMaster : System.Web.UI.Page
    {
        protected string CostMasterCsrfToken { get; private set; }
        protected string CostMasterExpiryUtc { get; private set; }
        protected bool CostMasterAuthorized { get; private set; }
        protected bool CostMasterAccessAllowed { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            CostMasterCsrfToken = CostMasterOtpSecurity.EnsureCsrfToken();
            CostMasterAccessAllowed = CostMasterOtpSecurity.HasPageAccess();
            if (!CostMasterAccessAllowed) return;
            DateTime? expiry = CostMasterOtpSecurity.GetAuthorizationExpiryUtc();
            CostMasterAuthorized = expiry.HasValue;
            CostMasterExpiryUtc = expiry.HasValue ? expiry.Value.ToString("o") : "";
        }

        [WebMethod(EnableSession = true)]
        public static string RequestCostMasterOtp(string employeeCode) { return CostMasterOtpSecurity.SendOtp(employeeCode); }

        [WebMethod(EnableSession = true)]
        public static object VerifyCostMasterOtp(string otp)
        {
            DateTime expiry = CostMasterOtpSecurity.VerifyOtp(otp);
            return new { message = "Verification successful. You can access Costing Master for the next 15 minutes.", expiryUtc = expiry.ToString("o") };
        }

        [WebMethod(EnableSession = true)]
        public static object GetCostMasterAuthorizationStatus()
        {
            CostMasterOtpSecurity.RequireCsrf(); CostMasterOtpSecurity.RequirePageAccess();
            DateTime? expiry = CostMasterOtpSecurity.GetAuthorizationExpiryUtc();
            return new { authorized = expiry.HasValue, expiryUtc = expiry.HasValue ? expiry.Value.ToString("o") : "", message = expiry.HasValue ? "" : CostMasterOtpSecurity.ExpiredMessage };
        }

        [WebMethod(EnableSession = true)]
        public static string RestartCostMasterVerification()
        {
            CostMasterOtpSecurity.RestartVerification();
            return "Enter your Employee Code to request a new OTP.";
        }

        [WebMethod(EnableSession = true)]
        public static string GetAllVendorRateConfiguration_Canopy()
        {
            CostMasterOtpSecurity.RequireCsrf(); CostMasterOtpSecurity.RequireAuthorization();
            DataTable dt1 = new bllInvoice().GetAllVendorRateConfiguration("Canopy");

            List<Dictionary<string, object>> columns = new List<Dictionary<string, object>>();
            Dictionary<string, object> column;

            if (dt1 != null)
            {
                foreach (DataColumn dc in dt1.Columns)
                {
                    column = new Dictionary<string, object>();
                }
            }

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
        public static string GetAllVendorRateConfiguration_IPS()
        {
            CostMasterOtpSecurity.RequireCsrf(); CostMasterOtpSecurity.RequireAuthorization();
            DataTable dt1 = new bllInvoice().GetAllVendorRateConfiguration("IPS");

            List<Dictionary<string, object>> columns = new List<Dictionary<string, object>>();
            Dictionary<string, object> column;

            if (dt1 != null)
            {
                foreach (DataColumn dc in dt1.Columns)
                {
                    column = new Dictionary<string, object>();
                }
            }

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
        public static string GetAllVendorAsPerCompany(string Company)
        {
            CostMasterOtpSecurity.RequireAuthorization();
            DataTable dt1 = new bllInvoice().GetAllVendorAsPerCompany(Company);

            List<Dictionary<string, object>> columns = new List<Dictionary<string, object>>();
            Dictionary<string, object> column;

            if (dt1 != null)
            {
                foreach (DataColumn dc in dt1.Columns)
                {
                    column = new Dictionary<string, object>();
                }
            }

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
        public static int InsertVendorRateConfigurationForbilling(string strCompany,string VendorName, string EffectiveDate, string RType, string BaseRate)
        {
            CostMasterOtpSecurity.RequireCsrf(); CostMasterOtpSecurity.RequireAuthorization();
            int returnvalue = 0;
            Hashtable htParam = new Hashtable();
            htParam.Add("Company", strCompany);
            htParam.Add("VendorName", VendorName);
            htParam.Add("EffectiveDate", EffectiveDate);
            htParam.Add("RType", RType);
            htParam.Add("Rate", BaseRate);
            htParam.Add("TRate", 0);

            htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));

            returnvalue = new bllInvoice().InsertVendorRateConfigurationForbilling(htParam);
            return returnvalue;
        }
    }
}
