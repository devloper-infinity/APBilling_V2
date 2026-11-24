using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vendor_Portal.App_Code.BLL;

namespace Vendor_Portal.Vendor
{
    public partial class VendorRegistrationMaster : System.Web.UI.Page
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
        [WebMethod(EnableSession = true)] public static object VerifyApproveCostOtp(string otp) { DateTime e = CostMasterOtpSecurity.VerifyOtp(otp, "VENDORREGISTRATION_MASTER"); return new { message = "Verification successful. You can access Vendor Registration Master Page for the next 15 minutes.", expiryUtc = e.ToString("o") }; }
        [WebMethod(EnableSession = true)] public static string RestartApproveCostVerification() { CostMasterOtpSecurity.RestartVerification("VENDORREGISTRATION_MASTER"); return "Enter your Employee Code to request a new OTP."; }

        [WebMethod(EnableSession = true)]
        public static string GetAllRegisteredVendors()
        {
            CostMasterOtpSecurity.RequireCsrf(); CostMasterOtpSecurity.RequireAuthorization("VENDORREGISTRATION_MASTER");

            DataTable dt1 = new bllInvoice().GetAllRegisteredVendors();
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