using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class AddInvoiceLauraMacIPS : System.Web.UI.Page
    {
        static string GUIDFile = "";
        static string GUIDFilePDF = "";
        static string MainPath = "";
        public static string Con_Canopy = "Data Source=23.111.175.186;Initial Catalog=Canopy-UWVendorBilling;User ID=sa;Password=#Cl0ud^$ecure4";
        public static string Con = "Data Source=23.111.175.186;Initial Catalog=Infinity-UWVendorBilling;User ID=sa;Password=#Cl0ud^$ecure4";
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Upload Files

            MainPath = Server.MapPath(@"~\Invoice");
            //Regular Attachment
            try
            {
                HttpContext postedContext = HttpContext.Current;
                HttpPostedFile file = postedContext.Request.Files[0];

                string name = file.FileName;
                byte[] binaryWriteArray = new byte[file.InputStream.Length];
                file.InputStream.Read(binaryWriteArray, 0,
                (int)file.InputStream.Length);

                FileInfo file_Info = new FileInfo(file.FileName);
                string ext = file_Info.Extension;

                if (ext == ".pdf")
                {

                }

                else
                {

                }

                string file_Name = Guid.NewGuid().ToString() + "_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + ext;
                GUIDFile = file_Name;



               
               if (file.FileName.Contains("StewartInvoice_"))
                {
                  //  NewFileName_StewartInvoice = Server.MapPath("..//TempFiles//" + "Invoice_" + file_Name);
                    //FileStream objfilestream = new FileStream(NewFileName_StewartInvoice, FileMode.Create, FileAccess.ReadWrite);
                    //objfilestream.Write(binaryWriteArray, 0, binaryWriteArray.Length);
                    //objfilestream.Close();
                }
                else if (file.FileName.Contains("StewartExcel_"))
                {
                    //NewFileName_StewartExcel = Server.MapPath("..//TempFiles//" + "StewartExcel_" + file_Name);
                  //  FileStream objfilestream = new FileStream(NewFileName_StewartExcel, FileMode.Create, FileAccess.ReadWrite);
                    //objfilestream.Write(binaryWriteArray, 0, binaryWriteArray.Length);
                    //objfilestream.Close();
                }

                else
                {
                  //  NewFileName = Server.MapPath("..//TempFiles//" + file_Name);
                   // FileStream objfilestream = new FileStream(NewFileName, FileMode.Create, FileAccess.ReadWrite);
                    //objfilestream.Write(binaryWriteArray, 0,
                    //binaryWriteArray.Length);
                    //objfilestream.Close();
                }

            }
            catch { }

            #endregion
        }

        [WebMethod]
        public static string VerifyLauraMac(int InvoiceID, string Month, string Year)
        {
            DataTable dt1 = new bllInvoice().GetLauraMacDetailsForVerify(InvoiceID, Month, Year);
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