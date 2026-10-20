using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vendor_Portal.App_Code.DAL;

namespace Vendor_Portal.Vendor
{
    public partial class VendorAccountDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static int InsertVendorAccountDetails(string VendorCompany, string VendorAccount, string VendorAccountPaymentType, string VendorAccount_PayTo, string VendorAccount_AcNo, string VendorAccount_EmailId)
        {
            int returnvalue = 0;
            Hashtable htParam = new Hashtable();

            htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
            htParam.Add("VendorCompany", VendorCompany);
            htParam.Add("VendorAccount", VendorAccount);
            htParam.Add("VendorAccountPaymentType", VendorAccountPaymentType);
            htParam.Add("VendorAccount_PayTo", VendorAccount_PayTo);
            htParam.Add("VendorAccount_AcNo", VendorAccount_AcNo);
            htParam.Add("VendorAccount_EmailId", VendorAccount_EmailId);

            returnvalue = new bllInvoice().InsertVendorAccountDetails(htParam);
            return returnvalue;
        }

        [WebMethod]
        public static string GetAllVendorAccountDetails_Canopy()
        {
            DataTable dt1 = new bllInvoice().GetAllVendorAccountDetails_Canopy("Canopy");

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
        public static string GetAllVendorAccountDetails_IPS()
        {
            DataTable dt1 = new bllInvoice().GetAllVendorAccountDetails_IPS("IPS");

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
    }
}