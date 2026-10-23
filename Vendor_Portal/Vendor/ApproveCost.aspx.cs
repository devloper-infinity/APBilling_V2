using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Web.Script.Serialization;
using System.Web.Services;
using Vendor_Portal.App_Code.BLL;
using System.Text;

namespace Vendor_Portal.Vendor
{
    public partial class ApproveCost : System.Web.UI.Page
    {
        protected string ApproveCostCsrfToken { get; private set; }
        protected string ApproveCostExpiryUtc { get; private set; }
        protected bool ApproveCostAuthorized { get; private set; }
        protected bool ApproveCostAccessAllowed { get; private set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            ApproveCostCsrfToken = CostMasterOtpSecurity.EnsureCsrfToken();
            ApproveCostAccessAllowed = CostMasterOtpSecurity.HasPageAccess("APPROVE_COST");
            if (!ApproveCostAccessAllowed) return;
            DateTime? expiry = CostMasterOtpSecurity.GetAuthorizationExpiryUtc("APPROVE_COST");
            ApproveCostAuthorized = expiry.HasValue;
            ApproveCostExpiryUtc = expiry.HasValue ? expiry.Value.ToString("o") : "";
        }

        [WebMethod(EnableSession=true)] public static string RequestApproveCostOtp(string employeeCode) { return CostMasterOtpSecurity.SendOtp(employeeCode,"APPROVE_COST"); }
        [WebMethod(EnableSession=true)] public static object VerifyApproveCostOtp(string otp) { DateTime e=CostMasterOtpSecurity.VerifyOtp(otp,"APPROVE_COST"); return new { message="Verification successful. You can access Approve Cost for the next 15 minutes.", expiryUtc=e.ToString("o") }; }
        [WebMethod(EnableSession=true)] public static string RestartApproveCostVerification() { CostMasterOtpSecurity.RestartVerification("APPROVE_COST"); return "Enter your Employee Code to request a new OTP."; }

        [WebMethod(EnableSession=true)]
        public static string GetAllVendorRateConfiguration_ForApproval_Canopy()
        {
            CostMasterOtpSecurity.RequireCsrf(); CostMasterOtpSecurity.RequireAuthorization("APPROVE_COST");
            DataTable dt1 = new bllInvoice().GetAllVendorRateConfiguration_ForApproval("Canopy");

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

        [WebMethod(EnableSession=true)]
        public static string GetAllVendorRateConfiguration_ForApproval_IPS()
        {
            CostMasterOtpSecurity.RequireCsrf(); CostMasterOtpSecurity.RequireAuthorization("APPROVE_COST");
            DataTable dt1 = new bllInvoice().GetAllVendorRateConfiguration_ForApproval("IPS");

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

        [WebMethod(EnableSession=true)]
        public static int ApproveVendorRateConfigurastion(int RConfigurationID, string Remark)
        {
            CostMasterOtpSecurity.RequireCsrf(); CostMasterOtpSecurity.RequireAuthorization("APPROVE_COST");
            int returnvalue = 0;

            Hashtable htParam = new Hashtable();
            htParam.Add("RConfigurationID", RConfigurationID);
            htParam.Add("VerifyRemark", Remark);
            htParam.Add("VerifyBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));

            returnvalue = new bllInvoice().ApproveVendorRateConfigurastion(htParam);
            return returnvalue;
        }

    }
}
