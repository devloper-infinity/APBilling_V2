using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
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
    public partial class SetReminderDates : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string GetAllReminderDates()
        {
            DataTable dt1 = new bllInvoice().GetAllReminderDates();
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
        public static int InsertIntoReminderDate(string Company, string Vendor, string ReminderDate)
        {
            int ReturnValue = 0;

            Hashtable htParam = new Hashtable();
            htParam.Add("Company", Company);
            htParam.Add("Vendor", Vendor);
            htParam.Add("ReminderDate", ReminderDate);
            htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));

            ReturnValue = new bllInvoice().InsertIntoReminderDate(htParam);
            if (ReturnValue > 0)
            {

            }
            return ReturnValue;
        }

        [WebMethod]
        public static int UpdateReminderDate(string Company, string Vendor, string ReminderDate, int ReminderDateID)
        {
            int ReturnValue = 0;

            Hashtable htParam = new Hashtable();
            htParam.Add("Company", Company);
            htParam.Add("Vendor", Vendor);
            htParam.Add("ReminderDate", ReminderDate);
            htParam.Add("ReminderDateID", ReminderDateID);
            htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));

            ReturnValue = new bllInvoice().UpdateReminderDate(htParam);
            if (ReturnValue > 0)
            {

            }
            return ReturnValue;
        }
    }
}