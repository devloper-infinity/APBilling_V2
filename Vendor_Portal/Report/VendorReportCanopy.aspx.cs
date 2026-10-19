using ClosedXML.Excel;
using DocumentFormat.OpenXml.ExtendedProperties;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.Services;
using Vendor_Portal.App_Code.BLL;

namespace Vendor_Portal.Report
{
    public partial class VendorReportCanopy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string GetAllStewartIA_Summary()
        {
            DataTable dt = new bllInvoice().GetAllStewartIA_Summary();
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

        [WebMethod]
        public static string GetAllStewartIA_Details()
        {
            DataTable dt = new bllInvoice().GetAllStewartIA_Details();
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
        //GetCompanySummary

        [WebMethod]
        public static string GetCompanySummary()
        {
            DataTable dt = new bllInvoice().GetCompanySummary();

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




        [WebMethod]
        public static string GetInvoiceDetails()
        {
            DataTable dt = new bllInvoice().GetInvoiceDetails();

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

        [WebMethod]
        public static string GetSummary(string Vendor, string Year,string Company)
        {
            DataTable dt = new bllInvoice().GetSummary(Vendor, Year, Company);

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

        [WebMethod]
        public static string GetDetails(string Vendor, string Year, string Company)
        {
            DataTable dt = new bllInvoice().GetDetails(Vendor, Year, Company);
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
        [WebMethod]
        public static string GetLoadLoanLogic(string Vendor, string Year, string Company)
        {
            DataSet ds = new bllInvoice().GetLoadLoanLogic(Vendor, Year, Company);     
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            if (ds != null && ds.Tables.Count >= 2)
            {
                DataTable dt1 = ds.Tables[0];
                DataTable dt2 = ds.Tables[1];
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

                if (dt2 != null)
                {
                    foreach (DataRow dr in dt2.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt2.Columns)
                        {
                            row.Add(col.ColumnName, dr[col]);
                        }
                        rows.Add(row);
                    }
                }
            }
            JavaScriptSerializer ser = new JavaScriptSerializer();
            ser.MaxJsonLength = int.MaxValue;
            return ser.Serialize(rows);
        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            string Vendor = hdnVendor.Value;
            string Year = hdnYear.Value;
            string Company = hdnCompany.Value;

            DataTable dtSummary = new bllInvoice().GetSummary(Vendor, Year, Company);
            DataTable dtDetails = new bllInvoice().GetDetails(Vendor, Year, Company);
            DataTable dtLoan1 = new DataTable();
            DataTable dtLoan2 = new DataTable();

            DataSet dsLoan = new bllInvoice().GetLoadLoanLogic(Vendor, Year, Company);

            if (dsLoan != null && dsLoan.Tables.Count >= 2)
            {
                dtLoan1 = dsLoan.Tables[0];
                dtLoan2 = dsLoan.Tables[1];
            }
           
            ExportExcel(dtSummary, dtDetails, dtLoan1, dtLoan2);
        }
        public void ExportExcel(DataTable dtSummary, DataTable dtDetails, DataTable dtLoan1, DataTable dtLoan2)
    {
        using (XLWorkbook wb = new XLWorkbook())
        {
                bool hasSummary = dtSummary != null && dtSummary.Rows.Count > 0;
                bool hasDetails = dtDetails != null && dtDetails.Rows.Count > 0;
                bool hasLoanLogic1 = dtLoan1 != null && dtLoan1.Rows.Count > 0;
                bool hasLoanLogic2 = dtLoan2 != null && dtLoan2.Rows.Count > 0;

                if (!hasSummary && !hasDetails && !hasLoanLogic1 && !hasLoanLogic2) return;
                //---------------- Summary ----------------//
                if (hasSummary)
                {
                    var ws1 = wb.Worksheets.Add(dtSummary, "Summary");

                    ws1.Tables.First().Theme = XLTableTheme.TableStyleMedium2;

                    ws1.Columns().AdjustToContents();

                    ws1.SheetView.FreezeRows(1);
                }

                //---------------- Details ----------------//
                if (hasDetails)
                {
                    var ws2 = wb.Worksheets.Add(dtDetails, "Details");

                    ws2.Tables.First().Theme = XLTableTheme.TableStyleMedium2;

                    ws2.Columns().AdjustToContents();

                    ws2.SheetView.FreezeRows(1);
                }
                if (hasLoanLogic1)
                {
                    var ws2 = wb.Worksheets.Add(dtLoan1, "Details_1");

                    ws2.Tables.First().Theme = XLTableTheme.TableStyleMedium2;

                    ws2.Columns().AdjustToContents();

                    ws2.SheetView.FreezeRows(1);
                }
                if (hasLoanLogic2)
                {
                    var ws2 = wb.Worksheets.Add(dtLoan2, "Details_2");

                    ws2.Tables.First().Theme = XLTableTheme.TableStyleMedium2;

                    ws2.Columns().AdjustToContents();

                    ws2.SheetView.FreezeRows(1);
                }
                //---------------- Download ----------------//

                Response.Clear();

            Response.Buffer = true;

            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            Response.AddHeader("content-disposition",
                "attachment;filename=VendorReport.xlsx");

            using (MemoryStream ms = new MemoryStream())
            {
                wb.SaveAs(ms);

                ms.WriteTo(Response.OutputStream);
            }

            Response.Flush();

            Response.End();
        }
    }

}
}