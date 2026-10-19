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

namespace Vendor_Portal.Report
{
    public partial class TrackingSheetReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        [WebMethod]
        public static string GetTrackingSheetCreditData(string category, string fromDate, string toDate)
        {
            DataTable dt1 = null;
            if (category == "Credit")
            {
                dt1 = new bllInvoice().GetTrackingSheetCreditData(fromDate, toDate);
            }
            else
            {
                dt1 = new bllInvoice().GetTrackingSheetServiceData(fromDate, toDate);

            }
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();

            if (dt1 != null)
            {
                foreach (DataRow dr in dt1.Rows)
                {
                    Dictionary<string, object> row = new Dictionary<string, object>();
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