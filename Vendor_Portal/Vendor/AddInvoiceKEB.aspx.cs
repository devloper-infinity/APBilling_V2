using ClosedXML.Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vendor_Portal.App_Code.BLL;

namespace Vendor_Portal.Vendor
{
    public partial class AddInvoiceKEB : System.Web.UI.Page
    {
        static string NewFileName_RemoteInvoice = "";
        static string NewFileName_RemoteExcel = "";

        static string GUIDFile = "";
        static string GUIDFile1 = "";
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

                string file_Name = string.Empty;
                string file_Name1 = string.Empty;


                if(ext == ".pdf" || ext == ".doc" || ext == ".docx")
                {
                    file_Name = Guid.NewGuid().ToString() + "_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + ext;
                    GUIDFile1 = file_Name;

                    NewFileName_RemoteInvoice = Server.MapPath("..//TempFiles//" + "Invoice_" + file_Name);
                    FileStream objfilestream = new FileStream(NewFileName_RemoteInvoice, FileMode.Create, FileAccess.ReadWrite);
                    objfilestream.Write(binaryWriteArray, 0,
                    binaryWriteArray.Length);
                    objfilestream.Close();
                }

                else
                {
                    file_Name1 = Guid.NewGuid().ToString() + "_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + ext;
                    GUIDFile = file_Name1;

                    NewFileName_RemoteExcel = Server.MapPath("..//TempFiles//" + "RemoteUWExcel_" + file_Name1);
                    FileStream objfilestream = new FileStream(NewFileName_RemoteExcel, FileMode.Create, FileAccess.ReadWrite);
                    objfilestream.Write(binaryWriteArray, 0,
                    binaryWriteArray.Length);
                    objfilestream.Close();

                }


                //string file_Name = Guid.NewGuid().ToString() + "_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + ext;
                //if (ext == ".pdf")
                //{
                //    GUIDFilePDF = file_Name;
                //}
                //else
                //{
                //    GUIDFile = file_Name;
                //}
                //if (file.FileName.Contains("RemoteUWInvoice"))
                //{
                //    NewFileName_RemoteInvoice = Server.MapPath("..//TempFiles//" + "Invoice_" + file_Name);
                //    FileStream objfilestream = new FileStream(NewFileName_RemoteInvoice, FileMode.Create, FileAccess.ReadWrite);
                //    objfilestream.Write(binaryWriteArray, 0,
                //    binaryWriteArray.Length);
                //    objfilestream.Close();
                //}
                //else if (file.FileName.Contains("RemoteUW"))
                //{
                //    NewFileName_RemoteExcel = Server.MapPath("..//TempFiles//" + "RemoteUWExcel_" + file_Name);
                //    FileStream objfilestream = new FileStream(NewFileName_RemoteExcel, FileMode.Create, FileAccess.ReadWrite);
                //    objfilestream.Write(binaryWriteArray, 0,
                //    binaryWriteArray.Length);
                //    objfilestream.Close();
                //}

            }
            catch (Exception ex)
            {

            }
        }

        [WebMethod]
        public static int InsertRemoteUWKEBInvoice(string Month, string Year, string InvoiceDate, string DueDate, string LoanCount, string InvoiceAmount, string InvoiceNumber,string InvoiceType)
        {
            try
            {
          
            var mc_KEB = new AddInvoiceKEB();
            int returnvalue = 0;
            //Convert.ToDateTime(InvoiceDate).ToString("MMM")
            //string SInvoiceType = "KEB";
            Hashtable htParam = new Hashtable();
            htParam.Add("Month", Month);
            htParam.Add("Year", Year);
            htParam.Add("StatementDate", Convert.ToDateTime(InvoiceDate).ToString("MM/dd/yyyy"));
            htParam.Add("DueDate", Convert.ToDateTime(DueDate).ToString("MM/dd/yyyy"));
            htParam.Add("Domain", "Underwriting");
            htParam.Add("Currency", "USD");
            htParam.Add("NoOfLoans", LoanCount);
            htParam.Add("VendorInvoiceNumber", InvoiceNumber);

            
            htParam.Add("TotalDue", InvoiceAmount);
            htParam.Add("VendorName", "IPS");
            if (InvoiceType == "KEB")
            {
                htParam.Add("InvoiceType", "KEB");
            }
            else if (InvoiceType == "TrueResources")
            {
              htParam.Add("InvoiceType", "True Resources");
            }
            else if (InvoiceType == "SmartHire")
            {
                htParam.Add("InvoiceType", "Smart Hire");
            }
            else if (InvoiceType == "Compliance")
            {
                htParam.Add("InvoiceType", "Compliance");
            }
            else if (InvoiceType == "LauraMac")
            {
                htParam.Add("InvoiceType", "LauraMac");
            }
            else if (InvoiceType == "LoanLogics")
            {
                htParam.Add("InvoiceType", "LoanLogics");
            }
            else if (InvoiceType == "KCB")
            {
                htParam.Add("InvoiceType", "KCB");
            }
            else if (InvoiceType == "Pacer")
            {
                htParam.Add("InvoiceType", "Pacer");
            }
            else if (InvoiceType == "Magna")
            {
                htParam.Add("InvoiceType", "Magna 5");
            }
                else if (InvoiceType == "Attorney")
                {
                    htParam.Add("InvoiceType", "Attorney");
                }
                else
           {
             htParam.Add("InvoiceType", "Remote UW");
           }

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
                File.Copy(NewFileName_RemoteInvoice, UniquePath + "\\" + GUIDFile1);
                File.Delete(NewFileName_RemoteInvoice);
                htParam.Add("FilePath", UniquePath + "\\" + GUIDFile1);

            }
            else
            {
                htParam.Add("FilePath", "");
            }

            htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
            if (InvoiceType == "TrueResource")
            {
                htParam.Add("ProjectId", "55");
            }
            else
            {
                htParam.Add("ProjectId", "35");
            }
            htParam.Add("Delay", InvoiceType);
            if (InvoiceType == "KEB")
            {
                htParam.Add("Remark", "KEB" + '~' + Convert.ToDateTime(InvoiceDate).ToString("MM/dd/yyyy"));
            }
            else if (InvoiceType == "TrueResources")
            {
              htParam.Add("Remark", "TrueResource" + ' ' + InvoiceType + '~' + Convert.ToDateTime(InvoiceDate).ToString("MM/dd/yyyy"));
            }
            else if (InvoiceType == "SmartHire")
            {
                htParam.Add("Remark", "SmartHire" + ' ' + InvoiceType + '~' + Convert.ToDateTime(InvoiceDate).ToString("MM/dd/yyyy"));
            }
            else if (InvoiceType == "Compliance")
            {
                htParam.Add("Remark", "ComplianceEase" + '~' + Convert.ToDateTime(InvoiceDate).ToString("MM/dd/yyyy"));
            }
            else if (InvoiceType == "LauraMac")
            {
                htParam.Add("Remark", "LauraMac" + '~' + Convert.ToDateTime(InvoiceDate).ToString("MM/dd/yyyy"));
            }
            else if (InvoiceType == "LoanLogics")
            {
                htParam.Add("Remark", "LoanLogics" + '~' + Convert.ToDateTime(InvoiceDate).ToString("MM/dd/yyyy"));
            }
            else if (InvoiceType == "KCB")
            {
                htParam.Add("Remark", "KCB" + '~' + Convert.ToDateTime(InvoiceDate).ToString("MM/dd/yyyy"));
            }
            else if (InvoiceType == "Pacer")
            {
                htParam.Add("Remark", "Pacer" + '~' + Convert.ToDateTime(InvoiceDate).ToString("MM/dd/yyyy"));
            }
            else if (InvoiceType == "Magna")
            {
                htParam.Add("Remark", "Magna5" + '~' + Convert.ToDateTime(InvoiceDate).ToString("MM/dd/yyyy"));
            }
                else if (InvoiceType == "Attorney")
                {
                    htParam.Add("Remark", "Attorney" + '~' + Convert.ToDateTime(InvoiceDate).ToString("MM/dd/yyyy"));
                }

                else
            {
             htParam.Add("Remark", "Remote UW" + ' ' + InvoiceType + '~' + Convert.ToDateTime(InvoiceDate).ToString("MM/dd/yyyy"));
             }

            returnvalue = new bllInvoice().Insert_Infinity_InvoiceDetails(htParam);


            if (returnvalue > 0)
            {
                mc_KEB.sendEmailAddNewInvoice_KEB(returnvalue, InvoiceType);
            }
            return returnvalue;
            }
            catch (Exception ex)
            {
                System.IO.File.WriteAllText(HttpContext.Current.Server.MapPath("~/ErrorLog.txt"), ex.Message + " | " + ex.StackTrace);
                throw new Exception(ex.Message);
            }
        }


        public static DataTable ReadExcelToDataTable(string filePath)
        {
            var dt = new DataTable();

            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1);
                bool firstRow = true;

                foreach (var row in worksheet.RowsUsed())
                {
                    if (firstRow)
                    {
                        // Add columns
                        foreach (var cell in row.CellsUsed())
                            dt.Columns.Add(cell.Value.ToString());
                        firstRow = false;
                    }
                    else
                    {
                        // Add rows
                        dt.Rows.Add();
                        int i = 0;
                        foreach (var cell in row.CellsUsed())
                        {
                            dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                            i++;
                        }
                    }
                }
            }

            return dt;
        }




        [WebMethod]
        public static int InsertRemoteUWCanopuKEBExcel(int InvoiceID, string Month, string Year)
        {
            int returnvalue = 0;

            if (NewFileName_RemoteExcel != "")
            {
                string fileName = NewFileName_RemoteExcel.Substring(NewFileName_RemoteExcel.LastIndexOf("\\") + 1);
                string Extn = fileName.Substring(fileName.LastIndexOf(".") + 1);
                if (Extn == "xls" | Extn == "xlsx")
                {

                   
                    var Dt = ReadExcelToDataTable(NewFileName_RemoteExcel);
                    try
                    {
                        DataTable dt1 = new DataTable();
                        dt1 = Dt;
                        DataTable dtProductSold = Dt;
                        Dt.Columns.Add("AddedBy", typeof(int));
                        Dt.Columns.Add("InvoiceId", typeof(int));
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            Dt.Rows[i]["AddedBy"] = int.Parse(HttpContext.Current.User.Identity.Name.ToString());
                            Dt.Rows[i]["InvoiceId"] = InvoiceID;
                        }



                        DataRow row = Dt.Rows[0];
                        //Dt.Rows.Remove(row);

                        SqlConnection con1 = new SqlConnection(Con);
                        SqlBulkCopy objbulk = new SqlBulkCopy(Con);
                        //assigning Destination table name  
                        objbulk.DestinationTableName = "Infinity_Remote_UWBilling1099";
                        //Mapping Table column  

                        objbulk.ColumnMappings.Add("InvoiceNo", "InvoiceNo");
                        objbulk.ColumnMappings.Add("EmployeeName", "EmployeeName");
                        objbulk.ColumnMappings.Add("ProjectNo", "ProjectNo");
                        objbulk.ColumnMappings.Add("DealNo", "DealNo");
                        objbulk.ColumnMappings.Add("Process", "Process");
                        objbulk.ColumnMappings.Add("PeriodWorked", "PeriodWorked");
                        objbulk.ColumnMappings.Add("FileProcessed", "FileProcessed");
                        objbulk.ColumnMappings.Add("Loan#", "Loan#");
                        objbulk.ColumnMappings.Add("WorkingDuration", "WorkingDuration");
                        objbulk.ColumnMappings.Add("ContractorRate", "ContractorRate");
                        objbulk.ColumnMappings.Add("InvoiceAmount", "InvoiceAmount");
                        objbulk.ColumnMappings.Add("RatePerLoan", "RatePerLoan");
                        objbulk.ColumnMappings.Add("OprRemark", "OprRemark");
                        objbulk.ColumnMappings.Add("AddedBy", "AddedBy");
                        objbulk.ColumnMappings.Add("InvoiceId", "InvoiceId");

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
        public static int InsertRemoteUWKEBExcel(int InvoiceID, string Month, string Year)
        {
            int returnvalue = 0;

            if (NewFileName_RemoteExcel != "")
            {
                string fileName = NewFileName_RemoteExcel.Substring(NewFileName_RemoteExcel.LastIndexOf("\\") + 1);
                string Extn = fileName.Substring(fileName.LastIndexOf(".") + 1);
                if (Extn == "xls" | Extn == "xlsx")
                {

                    //string ConExcel;
                    //if (Extn.Contains("xlsx"))
                    //{
                    //    ConExcel = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + NewFileName_RemoteExcel + "; Extended Properties=\"Excel 12.0;HDR=NO;IMEX=1;TypeGuessRows=0;ImportMixedTypes=Text\"";
                    //}
                    //else
                    //{
                    //    ConExcel = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + NewFileName_RemoteExcel + "; Extended Properties=\"Excel 8.0;HDR=NO;IMEX=1;TypeGuessRows=0;ImportMixedTypes=Text\"";
                    //}

                    //DataSet dsExcel = new DataSet();
                    //DataTable Dt = new DataTable("[Sheet1$]");
                    //using (OleDbConnection myExcelConnection = new OleDbConnection(ConExcel))
                    //{
                    //    string sqlExcel = "Select * from [Sheet1$]";
                    //    OleDbDataAdapter daExcel = new OleDbDataAdapter(sqlExcel, myExcelConnection);
                    //    daExcel.Fill(dsExcel);
                    //    daExcel.Dispose();
                    //    Dt = dsExcel.Tables[0];
                    //}
                    //DataTable Dt;
                    var Dt = ReadExcelToDataTable(NewFileName_RemoteExcel);
                    try
                    {
                        DataTable dt1 = new DataTable();
                        dt1 = Dt;
                        DataTable dtProductSold = Dt;
                        Dt.Columns.Add("AddedBy", typeof(int));
                        Dt.Columns.Add("InvoiceID", typeof(int));
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            Dt.Rows[i]["AddedBy"] = int.Parse(HttpContext.Current.User.Identity.Name.ToString());
                            Dt.Rows[i]["InvoiceID"] = InvoiceID;
                        }


                       
                  

                        SqlConnection con = new SqlConnection(Con_Canopy);
                        SqlBulkCopy objbulk = new SqlBulkCopy(Con_Canopy);
                        //assigning Destination table name  
                        objbulk.DestinationTableName = "KEB";
                        //Mapping Table column  

                        objbulk.ColumnMappings.Add("Month", "Month");
                        objbulk.ColumnMappings.Add("Year", "Year");
                        objbulk.ColumnMappings.Add("Date", "Date");
                        objbulk.ColumnMappings.Add("StartTime", "StartTime");
                        objbulk.ColumnMappings.Add("EndTime", "EndTime");
                        objbulk.ColumnMappings.Add("Breaktime", "Breaktime");
                        objbulk.ColumnMappings.Add("TotalTime", "TotalTime");
                        objbulk.ColumnMappings.Add("ClientNo", "ClientNo");
                        objbulk.ColumnMappings.Add("DealNo", "DealNo");
                        objbulk.ColumnMappings.Add("TaskName", "TaskName");
                        objbulk.ColumnMappings.Add("Target", "Target");
                        objbulk.ColumnMappings.Add("LoansReviewed", "LoansReviewed");
                        objbulk.ColumnMappings.Add("LoansCorrection", "LoansCorrectionMade");
                        objbulk.ColumnMappings.Add("NoErrorsFiles", "NoErrorsFiles");
                        objbulk.ColumnMappings.Add("AddedBy", "AddedBy");
                        objbulk.ColumnMappings.Add("InvoiceID", "InvoiceID");

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
        public static int InsertRemoteUWComplianceExcel(int InvoiceID, string Month, string Year)
        {
            int returnvalue = 0;

            if (NewFileName_RemoteExcel != "")
            {
                string fileName = NewFileName_RemoteExcel.Substring(NewFileName_RemoteExcel.LastIndexOf("\\") + 1);
                string Extn = fileName.Substring(fileName.LastIndexOf(".") + 1);
                if (Extn == "xls" | Extn == "xlsx")
                {

                    var Dt = ReadExcelToDataTable(NewFileName_RemoteExcel);
                    try
                    {
                        DataTable dt1 = new DataTable();
                        dt1 = Dt;
                        DataTable dtProductSold = Dt;
                        Dt.Columns.Add("Month", typeof(String));
                        Dt.Columns.Add("Year", typeof(String));
                        Dt.Columns.Add("AddedBy", typeof(int));
                        Dt.Columns.Add("InvoiceID", typeof(int));
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            Dt.Rows[i]["Month"] = Month;
                            Dt.Rows[i]["Year"] = Year;
                            Dt.Rows[i]["AddedBy"] = int.Parse(HttpContext.Current.User.Identity.Name.ToString());
                            Dt.Rows[i]["InvoiceID"] = InvoiceID;
                        }


                      

                        SqlConnection con = new SqlConnection(Con);
                        SqlBulkCopy objbulk = new SqlBulkCopy(Con);
                        //assigning Destination table name  
                        objbulk.DestinationTableName = "ComplianceEase";
                        //Mapping Table column  

                        objbulk.ColumnMappings.Add("ClientID", "ClientID");
                        objbulk.ColumnMappings.Add("AuditTimeStamp", "AuditTimeStamp");
                        objbulk.ColumnMappings.Add("CompanyName", "CompanyName");
                        objbulk.ColumnMappings.Add("UserName", "UserName");
                        objbulk.ColumnMappings.Add("Product", "Product");
                        objbulk.ColumnMappings.Add("DisclosureType", "DisclosureType");
                        objbulk.ColumnMappings.Add("NoOfDisclosures", "NoOfDisclosures");
                        objbulk.ColumnMappings.Add("CreditType", "CreditType");
                        objbulk.ColumnMappings.Add("AuditType", "AuditType");
                        objbulk.ColumnMappings.Add("CEID", "CEID");
                        objbulk.ColumnMappings.Add("VerNo", "VerNo");
                        objbulk.ColumnMappings.Add("CABillable", "CABillable");
                        objbulk.ColumnMappings.Add("TRIDMonitorBillable", "TRIDMonitorBillable");
                        objbulk.ColumnMappings.Add("Riskindicator", "Riskindicator");
                        objbulk.ColumnMappings.Add("State", "State");
                        objbulk.ColumnMappings.Add("LoanNo", "LoanNo");
                        objbulk.ColumnMappings.Add("Lender", "Lender");
                        objbulk.ColumnMappings.Add("BorrowerName", "BorrowerName");
                        objbulk.ColumnMappings.Add("NoteAmount", "NoteAmount");
                        objbulk.ColumnMappings.Add("Lien", "Lien");
                        objbulk.ColumnMappings.Add("Source", "Source");
                        objbulk.ColumnMappings.Add("Month", "Month");
                        objbulk.ColumnMappings.Add("Year", "Year");
                        objbulk.ColumnMappings.Add("AddedBy", "AddedBy");
                        objbulk.ColumnMappings.Add("InvoiceID", "InvoiceID");

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
        public static int InsertLauraMacExcel(int InvoiceID, string Month, string Year)
        {
            int returnvalue = 0;

            if (NewFileName_RemoteExcel != "")
            {
                string fileName = NewFileName_RemoteExcel.Substring(NewFileName_RemoteExcel.LastIndexOf("\\") + 1);
                string Extn = fileName.Substring(fileName.LastIndexOf(".") + 1);
                if (Extn == "xls" | Extn == "xlsx")
                {

                    var Dt = ReadExcelToDataTable(NewFileName_RemoteExcel);
                    try
                    {
                        DataTable dt1 = new DataTable();
                        dt1 = Dt;
                        DataTable dtProductSold = Dt;
                        Dt.Columns.Add("Month", typeof(String));
                        Dt.Columns.Add("Year", typeof(String));
                        Dt.Columns.Add("AddedBy", typeof(int));
                        Dt.Columns.Add("InvoiceID", typeof(int));
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            Dt.Rows[i]["Month"] = Month;
                            Dt.Rows[i]["Year"] = Year;
                            Dt.Rows[i]["AddedBy"] = int.Parse(HttpContext.Current.User.Identity.Name.ToString());
                            Dt.Rows[i]["InvoiceID"] = InvoiceID;
                        }


                       


                        SqlConnection con = new SqlConnection(Con_Canopy);
                        SqlBulkCopy objbulk = new SqlBulkCopy(Con_Canopy);

                        //assigning Destination table name  
                        objbulk.DestinationTableName = "LauraMac";

                        //Mapping Table column  
                        objbulk.ColumnMappings.Add("LoanNo", "LoanNo");
                        objbulk.ColumnMappings.Add("InvoiceActivatedDate", "InvoiceActivatedDate");
                        objbulk.ColumnMappings.Add("Month", "Month");
                        objbulk.ColumnMappings.Add("Year", "Year");
                        objbulk.ColumnMappings.Add("AddedBy", "AddedBy");
                        objbulk.ColumnMappings.Add("InvoiceID", "InvoiceID");

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
        public static int InsertLoanLogicsExcel(int InvoiceID, string Month, string Year)
        {
            int returnvalue = 0;

            if (NewFileName_RemoteExcel != "")
            {
                string fileName = NewFileName_RemoteExcel.Substring(NewFileName_RemoteExcel.LastIndexOf("\\") + 1);
                string Extn = fileName.Substring(fileName.LastIndexOf(".") + 1);
                if (Extn == "xls" | Extn == "xlsx")
                {

                    var Dt = ReadExcelToDataTable(NewFileName_RemoteExcel);
                    try
                    {
                        DataTable dt1 = new DataTable();
                        dt1 = Dt;
                        DataTable dtProductSold = Dt;
                        Dt.Columns.Add("Month", typeof(String));
                        Dt.Columns.Add("Year", typeof(String));
                        Dt.Columns.Add("AddedBy", typeof(int));
                        Dt.Columns.Add("InvoiceID", typeof(int));
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            Dt.Rows[i]["Month"] = Month;
                            Dt.Rows[i]["Year"] = Year;
                            Dt.Rows[i]["AddedBy"] = int.Parse(HttpContext.Current.User.Identity.Name.ToString());
                            Dt.Rows[i]["InvoiceID"] = InvoiceID;
                        }


                    

                        SqlConnection con = new SqlConnection(Con_Canopy);
                        SqlBulkCopy objbulk = new SqlBulkCopy(Con_Canopy);
                        //assigning Destination table name  
                        objbulk.DestinationTableName = "LoanLogics";
                        //Mapping Table column  


                        objbulk.ColumnMappings.Add("LoanNumber", "LoanNumber");
                        objbulk.ColumnMappings.Add("ProjectMonth", "ProjectMonth");
                        objbulk.ColumnMappings.Add("LMLoanNo", "LMLoanNo");
                        objbulk.ColumnMappings.Add("PageCount", "PageCount");
                        objbulk.ColumnMappings.Add("DocCount", "DocCount");
                        objbulk.ColumnMappings.Add("DeliveryDate", "DeliveryDate");
                        objbulk.ColumnMappings.Add("FeeType", "FeeType");
                        objbulk.ColumnMappings.Add("Quantity", "Quantity");
                        objbulk.ColumnMappings.Add("Amount", "Amount");
                        objbulk.ColumnMappings.Add("Month", "Month");
                        objbulk.ColumnMappings.Add("Year", "Year");
                        objbulk.ColumnMappings.Add("AddedBy", "AddedBy");
                        objbulk.ColumnMappings.Add("InvoiceID", "InvoiceID");

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
        public static int InsertKCBExcel(int InvoiceID, string Month, string Year)
        {
            int returnvalue = 0;

            if (NewFileName_RemoteExcel != "")
            {
                string fileName = NewFileName_RemoteExcel.Substring(NewFileName_RemoteExcel.LastIndexOf("\\") + 1);
                string Extn = fileName.Substring(fileName.LastIndexOf(".") + 1);
                if (Extn == "xls" | Extn == "xlsx")
                {

                    var Dt = ReadExcelToDataTable(NewFileName_RemoteExcel);
                    try
                    {
                        DataTable dt1 = new DataTable();
                        dt1 = Dt;
                        DataTable dtProductSold = Dt;
                        Dt.Columns.Add("Month", typeof(String));
                        Dt.Columns.Add("Year", typeof(String));
                       // Dt.Columns.Add("AddedBy", typeof(int));
                        Dt.Columns.Add("InvoiceID", typeof(int));
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            Dt.Rows[i]["Month"] = Month;
                            Dt.Rows[i]["Year"] = Year;
                            //Dt.Rows[i]["AddedBy"] = int.Parse(HttpContext.Current.User.Identity.Name.ToString());
                            Dt.Rows[i]["InvoiceID"] = InvoiceID;
                        }


                        SqlConnection con = new SqlConnection(Con);
                        SqlBulkCopy objbulk = new SqlBulkCopy(Con);
                        //assigning Destination table name  
                        objbulk.DestinationTableName = "CreditSoftPull";
                        //Mapping Table column  


                        objbulk.ColumnMappings.Add("CustomerName", "CustomerName");
                        objbulk.ColumnMappings.Add("CustomerNumber", "CustomerNumber");
                        objbulk.ColumnMappings.Add("FileNo", "FileNo");
                        objbulk.ColumnMappings.Add("RefNo", "RefNo");
                        objbulk.ColumnMappings.Add("FirstName", "FirstName");
                        objbulk.ColumnMappings.Add("LastName", "LastName");
                        objbulk.ColumnMappings.Add("Product", "Product");
                        objbulk.ColumnMappings.Add("User", "User");
                        objbulk.ColumnMappings.Add("Date", "Date");
                        objbulk.ColumnMappings.Add("Description", "Description");
                        objbulk.ColumnMappings.Add("Payments", "Payments");
                        objbulk.ColumnMappings.Add("Charges", "Charges");
                        objbulk.ColumnMappings.Add("SysRemark", "SysRemark");

                        objbulk.ColumnMappings.Add("Month", "Month");
                        objbulk.ColumnMappings.Add("Year", "Year");
                        //objbulk.ColumnMappings.Add("AddedBy", "AddedBy");
                        objbulk.ColumnMappings.Add("InvoiceID", "InvoiceID");


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
        public static string InsertPacerExcel(int InvoiceID, string Month, string Year)
        {
            string returnvalue = "0";

            if (NewFileName_RemoteExcel != "")
            {
                string fileName = NewFileName_RemoteExcel.Substring(NewFileName_RemoteExcel.LastIndexOf("\\") + 1);
                string Extn = fileName.Substring(fileName.LastIndexOf(".") + 1);
                if (Extn == "xls" | Extn == "xlsx")
                {

                    var Dt = ReadExcelToDataTable(NewFileName_RemoteExcel);
                    try
                    {
                        DataTable dt1 = new DataTable();
                        dt1 = Dt;
                        DataTable dtProductSold = Dt;
                        Dt.Columns.Add("Month", typeof(String));
                        Dt.Columns.Add("Year", typeof(String));
                        Dt.Columns.Add("AddedBy", typeof(int));
                        Dt.Columns.Add("InvoiceID", typeof(int));
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            Dt.Rows[i]["Month"] = Month;
                            Dt.Rows[i]["Year"] = Year;
                            Dt.Rows[i]["AddedBy"] = int.Parse(HttpContext.Current.User.Identity.Name.ToString());
                            Dt.Rows[i]["InvoiceID"] = InvoiceID;
                        }


                        SqlConnection con = new SqlConnection(Con);
                        SqlBulkCopy objbulk = new SqlBulkCopy(Con);
                        //assigning Destination table name  
                        objbulk.DestinationTableName = "PacerDetails";
                        //Mapping Table column  

                        objbulk.ColumnMappings.Clear();

                        objbulk.ColumnMappings.Add("Login", "Login");
                        objbulk.ColumnMappings.Add("Court", "Court");
                        objbulk.ColumnMappings.Add("Date", "Date");
                        objbulk.ColumnMappings.Add("ClientCode", "ClientCode");
                        objbulk.ColumnMappings.Add("Pages", "Pages");
                        objbulk.ColumnMappings.Add("Audio", "Audio");
                        objbulk.ColumnMappings.Add("Cost", "Cost");


                        objbulk.ColumnMappings.Add("Month", "Month");
                        objbulk.ColumnMappings.Add("Year", "Year");
                        objbulk.ColumnMappings.Add("AddedBy", "AddedBy");
                        objbulk.ColumnMappings.Add("InvoiceID", "InvoiceID");


                        //inserting bulk Records into DataBase   
                        objbulk.WriteToServer(Dt);
                        returnvalue = "1";
                    }
                    catch (Exception ex)
                    {
                        returnvalue = "Error: " + ex.Message;
                    }
                }
                else
                {
                    returnvalue = "-4";
                }

            }

            return returnvalue;
        }




        [WebMethod]
        public static int InsertAttorneyExcel(int InvoiceID, string Month, string Year)
        {
            int returnvalue = 0;

            if (NewFileName_RemoteExcel != "")
            {
                string fileName = NewFileName_RemoteExcel.Substring(NewFileName_RemoteExcel.LastIndexOf("\\") + 1);
                string Extn = fileName.Substring(fileName.LastIndexOf(".") + 1);
                if (Extn == "xls" | Extn == "xlsx")
                {

                    var Dt = ReadExcelToDataTable(NewFileName_RemoteExcel);
                    try
                    {
                        DataTable dt1 = new DataTable();
                        dt1 = Dt;
                        DataTable dtProductSold = Dt;
                  
                        Dt.Columns.Add("AddedBy", typeof(int));
                        Dt.Columns.Add("InvoiceID", typeof(int));
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                         
                            Dt.Rows[i]["AddedBy"] = int.Parse(HttpContext.Current.User.Identity.Name.ToString());
                            Dt.Rows[i]["InvoiceID"] = InvoiceID;
                        }





                        SqlConnection con = new SqlConnection(Con);
                        SqlBulkCopy objbulk = new SqlBulkCopy(Con);

                        //assigning Destination table name  
                        objbulk.DestinationTableName = "AttorneyInvoiceDetails";

                        //Mapping Table column  
                        objbulk.ColumnMappings.Add("InvoiceDate", "InvoiceDate");
                        objbulk.ColumnMappings.Add("InvoiceNo", "InvoiceNo");
                        objbulk.ColumnMappings.Add("VendorName", "VendorName");
                        objbulk.ColumnMappings.Add("Date", "Date");
                        objbulk.ColumnMappings.Add("TimeKeeper", "TimeKeeper");
                        objbulk.ColumnMappings.Add("Hours", "Hours");
                        objbulk.ColumnMappings.Add("Amount", "Amount");
                        objbulk.ColumnMappings.Add("Description", "Description");
                        objbulk.ColumnMappings.Add("OprRemark", "OprRemark");

                      
                        objbulk.ColumnMappings.Add("AddedBy", "AddedBy");
                        objbulk.ColumnMappings.Add("InvoiceID", "InvoiceID");

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
        public static string VerifyRemoteUW_Canopy(int InvoiceID, string Month, string Year)
        {
            DataTable dt1 = new bllInvoice().GetIPSRemoteUWKEBForVerify(InvoiceID, Month, Year);
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
        public static string VerifyRemoteUWKEB(int InvoiceID, string Month, string Year)
        {
            DataTable dt1 = new bllInvoice().GetIPSRemoteUWKEBForVerify(InvoiceID, Month, Year);
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

        public void sendEmailAddNewInvoice_KEB(int InvoiceId, string strInvoiceType)
        {
            var mc_KEB = new AddInvoiceKEB();
            StringBuilder htmlBody = new StringBuilder();
            string Subject;
            string ProjectName = "Billing";
            string ProjectType = "";

            DateTime dtime = DateTime.Today;

            string Path = "";
            string ToAddress = string.Empty;
            string ToCC = string.Empty;

            ToAddress = "k.aarti@infinity-data.com";
            ToCC = "k.aarti@infinity-data.com";

            string BillingDatePeriod = "";

            DataTable dt = new DataTable();
            DataTable dtSummary = new DataTable();
            DataTable dtAdvancePaymentSymmary = new DataTable();

            dt = new bllInvoice().GetAllPendingInvoice_IPS(InvoiceId);

            if (dt.Rows.Count > 0)
            {
                Subject = "IPS - New Invoice" + "-" + Convert.ToString(dt.Rows[0]["NewInvoiceType"]) + "-" + Convert.ToString(dt.Rows[0]["VendorInvoiceNumber"]) + " : Pending for Approval";
                htmlBody.Append("<br />Dear Sir/Madam,<br />");
                htmlBody.Append("<br /><font color=brown face=Verdana size=2 ><b>New invoice has been added in system with below details" + BillingDatePeriod + ".</b></font><br />");

                htmlBody.Append("<br /><table border=\"1\" bordercolor='Black' style='border:solid 1px black; border-collapse:collapse; padding:5px; font-size:12px; font-family:Verdana;'>");
                htmlBody.Append("<tr><td colspan='9' style='background-color:#244b73; color:White; text-align:center; padding:10px; font-size:14px;'><b>" + ProjectName + "-Details " + BillingDatePeriod + "  </b></td></tr>");
                htmlBody.Append("<tr><td colspan='9' style='border:none; height:2px; background-color:White;'></td></tr>");

                htmlBody.Append("<tr style='background-color:#244b73; color:White; font-weight:bold;'>");
                htmlBody.Append("<td style='padding:8px; text-align:center;'><b>Invoice #</b></td>");
                htmlBody.Append("<td style='padding:8px; text-align:center;'><b>Invoice Type</b></td>");
                htmlBody.Append("<td style='padding:8px; text-align:center;'><b>Billing Period</b></td>");
                htmlBody.Append("<td style='padding:8px; text-align:center;'><b>Invoice Date</b></td>");
                htmlBody.Append("<td style='padding:8px; text-align:center;'><b>Due Date</b></td>");
                htmlBody.Append("<td style='padding:8px; text-align:center;'><b>No of Loans/Orders</b></td>");
                htmlBody.Append("<td style='padding:8px; text-align:center;'><b>Amount</b></td>");
                htmlBody.Append("<td style='padding:8px; text-align:center;'><b>Verification Remark</b></td>");
                htmlBody.Append("<td style='padding:8px; text-align:center;'><b>Invoice Added in System On</b></td>");
                htmlBody.Append("</tr>");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string rowBgColor = (i % 2 == 0) ? "White" : "#e4edf7";

                    htmlBody.Append("<tr style='background-color:" + rowBgColor + "; color:black;'>");
                    htmlBody.Append("<td style='padding:6px; text-align:center;'><b>" + Convert.ToString(dt.Rows[i]["VendorInvoiceNumber"]) + "</b></td>");
                    htmlBody.Append("<td style='padding:6px; text-align:center;'>" + Convert.ToString(dt.Rows[i]["NewInvoiceType"]) + "</td>");
                    htmlBody.Append("<td style='padding:6px; text-align:center;'>" + Convert.ToString(dt.Rows[i]["BillingPeriod"]) + "</td>");
                    htmlBody.Append("<td style='padding:6px; text-align:center;'>" + Convert.ToString(dt.Rows[i]["StatementDate"]) + "</td>");
                    htmlBody.Append("<td style='padding:6px; text-align:center;'>" + Convert.ToString(dt.Rows[i]["DueDate"]) + "</td>");
                    htmlBody.Append("<td style='padding:6px; text-align:center;'>" + Convert.ToString(dt.Rows[i]["NoOfLoans"]) + "</td>");
                    htmlBody.Append("<td style='padding:6px; text-align:right; padding-right:10px;'>" + Convert.ToString(dt.Rows[i]["InvoiceAmount"]) + "</td>");
                    htmlBody.Append("<td style='padding:6px; text-align:center;'>" + Convert.ToString(dt.Rows[i]["Remark"]) + "</td>");
                    htmlBody.Append("<td style='padding:6px; text-align:center;'>" + Convert.ToString(dt.Rows[i]["AddedDate"]) + "</td>");
                    htmlBody.Append("</tr>");
                }

                htmlBody.Append("</table>");

                htmlBody.Append("<br /><table cellspacing='7px' cellpadding='3px' width='700px' style='font-family: Verdana; font-size: 12px; border-collapse: collapse;'><tr><td align=\"left\" width=600>Please <a href='http://192.168.11.11/Vendor/Login.aspx" + BillingDatePeriod + "'>click here</a> For Approval</td></tr></table><br />");

                htmlBody.Append("<br /><br /><table width=\"650px\" style='font-size:13px;'><tr><td align=\"left\">Thanks,<br />Infinity </td></tr> <tr> <br /><td align=\"center\"><b>!!! This is software generated e-mail...Please do not reply. !!!</td></tr></table>");
                htmlBody.Append("<table width=\"600px\" style='font-size:10px;'><tr><td align=\"left\"></td></tr> <tr> <br /><td align=\"center\"><b>" + "***********************************************************************************************************************************************************************************************" + "</td></tr></table>");
                htmlBody.Append("<table width=\"600px\" style='font-size:10px;'><tr><td align=\"left\">" + "<b>CONFIDENTIALITY INFORMATION AND DISCLAIMER</b>" + "</td></tr> <tr> <br /><td align=\"center\"><b></td></tr></table>");
                htmlBody.Append("<table width=\"600px\" style='font-size:12px;'><tr><td align=\"left\">" + "This message contains information which may be confidential and privileged. Unless you are the addressee (or authorized to receive for the addressee), you may not use copy or disclose to anyone the message or any information contained in the message. If you have received the message in error, please advise the sender by reply e-mail and delete the message. Thank you." + "</td></tr><tr><td align=\"center\"><b></td></tr></table>");
                htmlBody.Append("<table width=\"600px\" style='font-size:10px;'><tr><td align=\"left\">" + "***********************************************************************************************************************************************************************************************" + " </td></tr> <tr> <br /><td align=\"center\"><b></td></tr></table>");
                // sendMailForOnlineTracking_New(ToAddress, ToCC, "n.nilkanth@infinityinternationals.us,p.kedar@infinityinternationals.us", Subject, Path, htmlBody);
                mc_KEB.sendEmailInvoiceApproval_IPS(ToAddress, ToCC, "k.aarti@infinity-data.com", Subject, Path, htmlBody);
            }



        }

        [WebMethod]
        public bool sendEmailInvoiceApproval_IPS(string ToAddress, string ToCC, string ToBCC, string Subject, string Path, StringBuilder htmlBody)
        {
            try
            {
                String Body = htmlBody.ToString();
                StringBuilder template = new StringBuilder();
                template.Append("<html><head></head><body>");
                //template.Append("<img src=\"http://www.infinity-data.com/images/TemplateHeader.png\" /><br />");
                template.Append(Body);
                //template.Append("<br /><img src=\"http://www.infinity-data.com/images/TemplateFooter.png\" />");
                template.Append("</body></html>");
                MailMessage mail = new MailMessage();

                mail.To.Add(ToAddress);


                if (ToCC != "")
                    mail.CC.Add(ToCC);
                if (ToBCC != "")
                    mail.Bcc.Add(ToBCC);
                mail.From = new MailAddress("ack@infinity-data.com", "IPS AP Billing", System.Text.Encoding.UTF8);
                mail.Subject = Subject;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = template.ToString();
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;

                string pass = new bllInvoice().GetPassword("ackdata");

                SmtpClient client = new SmtpClient();
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("ack@infinity-data.com", pass);
                client.Host = "smtp.office365.com";
                client.Port = 587;
                client.EnableSsl = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                try
                {
                    client.Send(mail);
                }
                catch (Exception ex)
                {
                    //AddException(ex.Message + '~' + pass);
                }
                htmlBody.Remove(0, htmlBody.Length);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}