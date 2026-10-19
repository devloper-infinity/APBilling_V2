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
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string GetAllVendorRateConfiguration_ForApproval_Canopy()
        {
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

        [WebMethod]
        public static string GetAllVendorRateConfiguration_ForApproval_IPS()
        {
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

        [WebMethod]
        public static int ApproveVendorRateConfigurastion(int RConfigurationID, string Remark)
        {
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