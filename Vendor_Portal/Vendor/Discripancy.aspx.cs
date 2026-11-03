using System;
using System.Collections;
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
    public partial class Discripancy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        [WebMethod]
        public static string GetDiscripancyInvoice()
        {
            DataTable dt1 = new bllInvoice().GetDiscripancyInvoice();
            return ConvertDatatableToJson(dt1);
        }

        [WebMethod]
        public static string GetLoansForReconcillation(int InvoiceID, string Status, string InvType, string TableName)
        {
            DataTable dt = new bllInvoice().GetLoansForReconcillation(InvoiceID, Status, InvType, TableName);
            return ConvertDatatableToJson(dt);
        }

        [WebMethod]
        public static bool UpdateDiscrepancyRemark(long InvoiceID, long UniqueID, string InvoiceType, string TableName, string UserRemark)
        {
            if (InvoiceID <= 0 || UniqueID <= 0 || string.IsNullOrWhiteSpace(InvoiceType) || InvoiceType.Length > 500 ||
                string.IsNullOrWhiteSpace(TableName) || TableName.Length > 500 || UserRemark == null || UserRemark.Length > 5000)
            {
                return false;
            }

            int addedBy;
            if (HttpContext.Current == null || HttpContext.Current.User == null || HttpContext.Current.User.Identity == null ||
                !int.TryParse(HttpContext.Current.User.Identity.Name, out addedBy))
            {
                return false;
            }

            Hashtable htParam = new Hashtable();
            htParam.Add("InvoiceID", InvoiceID);
            htParam.Add("UniqueID", UniqueID);
            htParam.Add("InvoiceType", InvoiceType);
            htParam.Add("TableName", TableName);
            htParam.Add("UserRemark", UserRemark);
            htParam.Add("AddedBy", addedBy);

            return new bllInvoice().UpdateReconcileRemark(htParam) != 0;
        }

        public static string ConvertDatatableToJson(DataTable dt)
        {
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
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
