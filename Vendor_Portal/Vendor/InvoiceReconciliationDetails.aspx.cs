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
    public partial class InvoiceReconciliationDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string GetLauraMacDataForReconcile(int InvoiceID, string Month, string Year)
        {
            DataTable dt1 = new bllInvoice().GetLauraMacDataForReconcile(0, Month, Year);
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
        public static string GetStewartDataForReconcile(string Month, string Year)
        {
            DataTable dt1 = new bllInvoice().GetStewartDataForReconcile(Month, Year);
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
        public static int VerifyLoansLauraMac(int BillingID, string UserRemark)
        {
            int returnvalue = 0;
            Hashtable htParam = new Hashtable();
            htParam.Add("BillingId", BillingID);
            htParam.Add("UserRemark", UserRemark);
            htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
            returnvalue = new bllInvoice().VerifyLoanNo_LauraMac(htParam);
            return returnvalue;
        }

        [WebMethod]
        public static int VerifyLoans(int BillingID, string DisputeValue, string UserRemark)
        {
            int returnvalue = 0;
            Hashtable htParam = new Hashtable();
            htParam.Add("BillingId", BillingID);
            htParam.Add("DisputeValue", DisputeValue);
            htParam.Add("UserRemark", UserRemark);
            htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
            returnvalue = new bllInvoice().VerifyLoanNo(htParam);
            return returnvalue;
        }

        [WebMethod]
        public static int VerifyCompliance(string Vendor, int InvoiceID, string Remark)
        {
            int returnValue = 0;

            Hashtable htParam = new Hashtable();
            htParam.Add("InvoiceID", InvoiceID);
            htParam.Add("Remark", Remark);
            htParam.Add("VerifyBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
            returnValue = new bllInvoice().VerifyCompliance(htParam);
            return returnValue;
        }


        [WebMethod]
        public static int ReconcileLoan_Generalised(string Vendor, string Month, string Year, string AllInvoiceID, string AllRemark)
        {
            int returnValue = 0;

            string ID_split = AllInvoiceID.Substring(10);
            string[] P_ID = ID_split.Split('|');

            string Remark_split = AllRemark.Substring(10);
            string[] Remark = Remark_split.Split('|');

            for(int i= 0; i < Remark.Length; i++)
            {
                Hashtable htParam = new Hashtable();
                htParam.Add("Vendor", Vendor);
                htParam.Add("InvoiceID", P_ID[i]);
                htParam.Add("Month", Month);
                htParam.Add("Year", Year);
                htParam.Add("Remark", Remark[i]);
                htParam.Add("VerifyBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
                returnValue = new bllInvoice().ReconcileLoan_Generalised(htParam);
            }
            return returnValue;
        }

        [WebMethod]
        public static string GetComplianceRecordForReconsile(int InvoiceID, string Month, string Year)
        {
            DataTable dt1 = new bllInvoice().GetComplianceRecordForReconsile(InvoiceID, Month, Year);
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
        public static string GetVendorRecordForReconcile_Generilised(string VendorType, int InvoiceID, string Month, string Year)
        {
            DataTable dt1 = new bllInvoice().GetVendorRecordForReconcile_Generilised(VendorType, InvoiceID, Month, Year);
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