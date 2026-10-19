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
using System.Collections;
using System.IO;
using System.Text;
using System.Net.Mail;

namespace Vendor_Portal.Vendor
{
    public partial class UpdateClientInvoice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string GetInfinityInvoicesForUpdatePaidDate(string Company)
        {
            DataTable dt1 = new bllInvoice().GetInvoicesForUpdateClientInvoiceNo(int.Parse(HttpContext.Current.User.Identity.Name.ToString()), Company);
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
        public static int UpdateClientInvoiceNo(string InvoiceIDs,  string Company, string InvType, string ClientInvNo)
        {
            int ReturnValue = 0;
            try
            {
                string str_split = InvoiceIDs.Substring(2);
                string[] IDs = str_split.Split(',');

                foreach (var sub_str in IDs)
                {
                    Hashtable htParam = new Hashtable();
                    htParam.Add("InvoiceID", sub_str);
                    htParam.Add("InvType", InvType);
                    htParam.Add("ClientInvNo", ClientInvNo);
                    htParam.Add("Company", Company);
                    htParam.Add("UpdatedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));

                    ReturnValue = new bllInvoice().UpdateClientInvoice(htParam);

                    if (ReturnValue > 0)
                    {
                       
                    }
                }
            }
            catch (Exception ex)
            {
        
            }
            return ReturnValue;
        }
    }
}