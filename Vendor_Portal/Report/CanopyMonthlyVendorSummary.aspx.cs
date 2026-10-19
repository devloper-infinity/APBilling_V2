using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vendor_Portal.App_Code.BLL;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Data;
using System.IO;

namespace Vendor_Portal.Report
{
    public partial class CanopyMonthlyVendorSummary : System.Web.UI.Page
    {
        static string NewFileName = "";
        static string FileName = "";
        static string GUIDFile = "";
        static string FolderPath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    //string id = Request.QueryString["user"];

            //}

                //FolderPath = Server.MapPath(@"~\ProjectDocuments");
                //try
                //{
                //    HttpContext postedContext = HttpContext.Current;
                //    HttpPostedFile file = postedContext.Request.Files[0];

                //    string name = file.FileName;
                //    byte[] binaryWriteArray = new byte[file.InputStream.Length];
                //    file.InputStream.Read(binaryWriteArray, 0,
                //    (int)file.InputStream.Length);

                //    FileInfo file_Info = new FileInfo(file.FileName);
                //    string ext = file_Info.Extension;

                //    string file_Name = Guid.NewGuid().ToString() + "_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + ext;
                //    GUIDFile = file_Name;
                //    NewFileName = Server.MapPath("..//TempFiles//" + file_Name);
                //    FileStream objfilestream = new FileStream(NewFileName, FileMode.Create, FileAccess.ReadWrite);
                //    objfilestream.Write(binaryWriteArray, 0,
                //    binaryWriteArray.Length);
                //    objfilestream.Close();
                //}
                //catch { }
            }

        [WebMethod]
        public static string GetDataMonthlyVendorData_Canopy()
        {
            DataTable dt1 = new bllInvoice().GetDataMonthlyVendorData_Canopy("2026");
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
        public static string GetDataMonthlyVendorData_IPS()
        {
            DataTable dt1 = new bllInvoice().GetDataMonthlyVendorData_IPS("2026");
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