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
    public partial class AddInvoiceRemoteUW : System.Web.UI.Page
    {
        static string NewFileName_RemoteInvoice = "";
        static string NewFileName_RemoteExcel = "";

        static string GUIDFile = "";
        static string MainPath = "";
        public static string Con_Canopy = "Data Source=23.111.175.186;Initial Catalog=Canopy-UWVendorBilling;User ID=sa;Password=#Cl0ud^$ecure4";
        public static string Con = "Data Source=23.111.175.186;Initial Catalog=Infinity-UWVendorBilling;User ID=sa;Password=#Cl0ud^$ecure4";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                MainPath = Server.MapPath(@"~\Invoice");
                HttpContext postedContext = HttpContext.Current;
                HttpPostedFile file = postedContext.Request.Files[0];

                string name = file.FileName;
                byte[] binaryWriteArray = new byte[file.InputStream.Length];
                file.InputStream.Read(binaryWriteArray, 0,
                (int)file.InputStream.Length);

                FileInfo file_Info = new FileInfo(file.FileName);
                string ext = file_Info.Extension;

                string file_Name = Guid.NewGuid().ToString() + "_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + ext;
                GUIDFile = file_Name;
                if (file.FileName.Contains("RemoteUWInvoice"))
                {
                    NewFileName_RemoteInvoice = Server.MapPath("..//TempFiles//" + "Invoice_" + file_Name);
                    FileStream objfilestream = new FileStream(NewFileName_RemoteInvoice, FileMode.Create, FileAccess.ReadWrite);
                    objfilestream.Write(binaryWriteArray, 0,
                    binaryWriteArray.Length);
                    objfilestream.Close();
                }
                else if (file.FileName.Contains("RemoteUW"))
                {
                    NewFileName_RemoteExcel = Server.MapPath("..//TempFiles//" + "RemoteUWExcel_" + file_Name);
                    FileStream objfilestream = new FileStream(NewFileName_RemoteExcel, FileMode.Create, FileAccess.ReadWrite);
                    objfilestream.Write(binaryWriteArray, 0,
                    binaryWriteArray.Length);
                    objfilestream.Close();
                }

            }
            catch (Exception ex)
            {

            }
        }

        [WebMethod]
        public static int InsertRemoteUWInvoice(string Month, string Year, string InvoiceDate, string DueDate, string LoanCount, string InvoiceAmount, string InvoiceNumber)
        {
            int returnvalue = 0;
            string InvoiceType = "Remote UW";
            Hashtable htParam = new Hashtable();
            htParam.Add("Month", Convert.ToDateTime(InvoiceDate).ToString("MMM"));
            htParam.Add("Year", Convert.ToDateTime(InvoiceDate).ToString("yyyy"));
            htParam.Add("StatementDate", Convert.ToDateTime(InvoiceDate).ToString("MM/dd/yyyy"));
            htParam.Add("DueDate", Convert.ToDateTime(DueDate).ToString("MM/dd/yyyy"));
            htParam.Add("Domain", "Underwriting");
            htParam.Add("Currency", "USD");
            htParam.Add("NoOfLoans", LoanCount);
            htParam.Add("VendorInvoiceNumber", InvoiceNumber);
            htParam.Add("TotalDue", InvoiceAmount);
            htParam.Add("VendorName", "Canopy Financial Technology");
            htParam.Add("InvoiceType", "Remote UW");


            if (NewFileName_RemoteInvoice != "")
            {
                string CodeDate = DateTime.Now.ToString("dd-MMM-yyyy-HHMMss");
                if (!Directory.Exists(MainPath))
                {
                    Directory.CreateDirectory(MainPath);
                }
                string SubPath = MainPath + "\\" + Convert.ToString(InvoiceType);
                if (!Directory.Exists(SubPath))
                {
                    Directory.CreateDirectory(SubPath);
                }
                string UniquePath = MainPath + "\\" + Convert.ToString(InvoiceType) + "\\" + Convert.ToString(CodeDate);
                if (!Directory.Exists(UniquePath))
                {
                    Directory.CreateDirectory(UniquePath);
                }
                File.Copy(NewFileName_RemoteInvoice, UniquePath + "\\" + GUIDFile);
                File.Delete(NewFileName_RemoteInvoice);
                htParam.Add("FilePath", UniquePath + "\\" + GUIDFile);

            }
            else
            {
                htParam.Add("FilePath", "");
            }

            htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
            htParam.Add("ProjectId", "0");
            htParam.Add("Delay", "No");
            htParam.Add("Remark", "Remote UW" + ' ' + Convert.ToDateTime(InvoiceDate).ToString("MM/dd/yyyy"));
            returnvalue = new bllInvoice().Insert_Infinity_InvoiceDetails_Canopy(htParam);
            return returnvalue;
        }

        [WebMethod]

        public static int InsertRemoteUWExcel(int InvoiceID, string Month, string Year)
        {
            int returnvalue = 0;

            if (NewFileName_RemoteExcel != "")
            {
                string fileName = NewFileName_RemoteExcel.Substring(NewFileName_RemoteExcel.LastIndexOf("\\") + 1);
                string Extn = fileName.Substring(fileName.LastIndexOf(".") + 1);
                if (Extn == "xls" | Extn == "xlsx")
                {

                    string ConExcel;
                    if (Extn.Contains("xlsx"))
                    {
                        ConExcel = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + NewFileName_RemoteExcel + "; Extended Properties=\"Excel 12.0;HDR=NO;IMEX=1;TypeGuessRows=0;ImportMixedTypes=Text\"";
                    }
                    else
                    {
                        ConExcel = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + NewFileName_RemoteExcel + "; Extended Properties=\"Excel 8.0;HDR=NO;IMEX=1;TypeGuessRows=0;ImportMixedTypes=Text\"";
                    }

                    DataSet dsExcel = new DataSet();
                    DataTable Dt = new DataTable("[Sheet1$]");
                    using (OleDbConnection myExcelConnection = new OleDbConnection(ConExcel))
                    {
                        string sqlExcel = "Select * from [Sheet1$]";
                        OleDbDataAdapter daExcel = new OleDbDataAdapter(sqlExcel, myExcelConnection);
                        daExcel.Fill(dsExcel);
                        daExcel.Dispose();
                        Dt = dsExcel.Tables[0];
                    }
                    try
                    {
                        DataTable dt1 = new DataTable();
                        dt1 = Dt;

                        DataTable dtProductSold = Dt;
                        Dt.Columns.Add("AddedBy", typeof(int));
                        Dt.Columns.Add("EmployeeId", typeof(int));
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            Dt.Rows[i]["AddedBy"] = int.Parse(HttpContext.Current.User.Identity.Name.ToString());
                            Dt.Rows[i]["EmployeeId"] = InvoiceID;
                            
                        }

                        DataRow row = Dt.Rows[0];
                        Dt.Rows.Remove(row);

                        SqlConnection con = new SqlConnection(Con_Canopy);
                        SqlBulkCopy objbulk = new SqlBulkCopy(Con_Canopy);
                        //assigning Destination table name  
                        objbulk.DestinationTableName = "Infinity_Remote_UWBilling";
                        //Mapping Table column  

                        objbulk.ColumnMappings.Add("F1", "BillingNumber");
                        objbulk.ColumnMappings.Add("F2", "DealNo");
                        objbulk.ColumnMappings.Add("F3", "Process");
                        objbulk.ColumnMappings.Add("F4", "UserName");
                        objbulk.ColumnMappings.Add("F5", "LoanNumber");
                        objbulk.ColumnMappings.Add("F6", "Funds");
                        objbulk.ColumnMappings.Add("F7", "CompleteDate");
                        objbulk.ColumnMappings.Add("F8", "FromDate");
                        objbulk.ColumnMappings.Add("F9", "ToDate");


                        //inserting bulk Records into DataBase   
                        objbulk.WriteToServer(Dt);
                        returnvalue = 1;
                    }
                    catch (Exception ex)
                    {
                        returnvalue = 0;
                    }
                }
                else
                {
                    returnvalue = -4;
                }

            }



            return returnvalue;
        }

        [WebMethod]
        public static string VerifyRemoteUW(int InvoiceID, string Month, string Year)
        {
            DataTable dt1 = new bllInvoice().GetCanopyRemoteUWForVerify(InvoiceID, Month, Year);
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