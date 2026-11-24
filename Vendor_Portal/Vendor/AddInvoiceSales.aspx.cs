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
    public partial class AddInvoiceSales : System.Web.UI.Page
    {
        static string NewFileName_RemoteInvoice = "";
        static string NewFileName_RemoteExcel = "";

        static string GUIDFile = "";
        static string GUIDFilePDF = "";
        static string MainPath = "";
        public static string Con_Canopy = "Data Source=192.168.11.11,8989;Initial Catalog=Canopy-UWVendorBilling;User ID=sa;Password=idt15central";
        public static string Con = "Data Source=192.168.11.11,8989;Initial Catalog=Infinity-UWVendorBilling;User ID=sa;Password=idt15central";
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
                if (ext == ".pdf")
                {
                    GUIDFilePDF = file_Name;
                }
                else
                {
                    GUIDFile = file_Name;
                }
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
        public static int InsertRemoteUWSalesInvoice(
    
     string InvoiceDate,
     string DueDate,
    
     string InvoiceAmount,
     string InvoiceNumber,
     //string InvoiceType,
     
     string VendorName,
     string VendorType,
     string ToCompany,
     string EmailAddress,
     string ServicePeriodFrom,
     string ServicePeriodTo,
     string InvoiceCategory,
     string InvoiceDescription,
     string PaymentTerms,
     string PaymentMethod,
     string BankAccount,
     string PaymentFromCompany)
        {
            var mc_Sales = new AddInvoiceSales();
            int returnvalue = 0;

            Hashtable htParam = new Hashtable();
            htParam.Add("Month", "");
            htParam.Add("Year", "");
            htParam.Add("StatementDate", Convert.ToDateTime(InvoiceDate).ToString("MM/dd/yyyy"));
            htParam.Add("DueDate", Convert.ToDateTime(DueDate).ToString("MM/dd/yyyy"));
            htParam.Add("Domain", "Underwriting");
            htParam.Add("Currency", "USD");
            htParam.Add("NoOfLoans", "");
            htParam.Add("VendorInvoiceNumber", InvoiceNumber);
            htParam.Add("TotalDue", InvoiceAmount);
            htParam.Add("VendorName", "IPS"); 
            htParam.Add("InvoiceType", "Sales");

            // New parameters
            htParam.Add("SeletedVendorName", VendorName);

            htParam.Add("VendorType", VendorType ?? string.Empty);
            htParam.Add("ToCompany", ToCompany ?? string.Empty);
            htParam.Add("EmailAddress", EmailAddress ?? string.Empty);
            htParam.Add("ServicePeriodFrom", string.IsNullOrWhiteSpace(ServicePeriodFrom) ? (object)DBNull.Value : Convert.ToDateTime(ServicePeriodFrom).ToString("MM/dd/yyyy"));
            htParam.Add("ServicePeriodTo", string.IsNullOrWhiteSpace(ServicePeriodTo) ? (object)DBNull.Value : Convert.ToDateTime(ServicePeriodTo).ToString("MM/dd/yyyy"));
            htParam.Add("InvoiceCategory", InvoiceCategory ?? string.Empty);
            htParam.Add("InvoiceDescription", InvoiceDescription ?? string.Empty);
            htParam.Add("PaymentTerms", PaymentTerms ?? string.Empty);
            htParam.Add("PaymentMethod", PaymentMethod ?? string.Empty);
            htParam.Add("BankAccount", BankAccount ?? string.Empty);
            htParam.Add("PaymentFromCompany", PaymentFromCompany ?? string.Empty);

            if (NewFileName_RemoteInvoice != "")
            {
                string CodeDate = DateTime.Now.ToString("dd-MMM-yyyy-HHMMss");
                if (!Directory.Exists(MainPath))
                {
                    Directory.CreateDirectory(MainPath);
                }
                string SubPath = MainPath + "\\" + Convert.ToString("Sales");
                if (!Directory.Exists(SubPath))
                {
                    Directory.CreateDirectory(SubPath);
                }
                string UniquePath = MainPath + "\\" + Convert.ToString("Sales") + "\\" + Convert.ToString(CodeDate);
                if (!Directory.Exists(UniquePath))
                {
                    Directory.CreateDirectory(UniquePath);
                }
                File.Copy(NewFileName_RemoteInvoice, UniquePath + "\\" + GUIDFilePDF);
                File.Delete(NewFileName_RemoteInvoice);
                htParam.Add("FilePath", UniquePath + "\\" + GUIDFilePDF);
            }
            else
            {
                htParam.Add("FilePath", "");
            }

            htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
            htParam.Add("Delay", "Sales");
            htParam.Add("ProjectId", 0);
            htParam.Add("Remark", "Sales-Invoice :" + "Sales" + '~' + Convert.ToDateTime(InvoiceDate).ToString("MM/dd/yyyy"));

            returnvalue = new bllInvoice().Insert_Infinity_InvoiceDetails(htParam);

            if (returnvalue > 0)
            {
                mc_Sales.sendEmailAddNewInvoice_Sales(returnvalue, "Sales");
            }
            return returnvalue;
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
        public static string GetVendorTypes(int vendorRegistrationID)
        {
            DataTable dt1 = new bllInvoice().GetVendorTypes(vendorRegistrationID);
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
        public static int InsertRemoteUWCanopuSalesExcel(int InvoiceID, string Month, string Year)
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
                            Dt.Rows[i]["Employeeid"] = InvoiceID;
                        }



                        DataRow row = Dt.Rows[0];
                        Dt.Rows.Remove(row);

                        SqlConnection con = new SqlConnection(Con_Canopy);
                        SqlBulkCopy objbulk = new SqlBulkCopy(Con_Canopy);
                        //assigning Destination table name  
                        objbulk.DestinationTableName = "Infinity_Remote_UWBilling";
                        //Mapping Table column  

                        objbulk.ColumnMappings.Add("BillingNumber", "BillingNumber");
                        objbulk.ColumnMappings.Add("DealNo", "DealNo");
                        objbulk.ColumnMappings.Add("Date", "Date");
                        objbulk.ColumnMappings.Add("Process", "Process");
                        objbulk.ColumnMappings.Add("UserName", "UserName");
                        objbulk.ColumnMappings.Add("LoanNumber", "LoanNumber");
                        objbulk.ColumnMappings.Add("Funds", "Funds");
                        objbulk.ColumnMappings.Add("CompleteDate", "CompleteDate");
                        objbulk.ColumnMappings.Add("FromDate", "FromDate");
                        objbulk.ColumnMappings.Add("ToDate", "ToDate");
                        objbulk.ColumnMappings.Add("AddedBy", "AddedBy");
                        objbulk.ColumnMappings.Add("Employeeid", "InvoiceID");

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
        public static int InsertRemoteUWSalesExcel(int InvoiceID, string Month, string Year)
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


                       
                        DataRow row = Dt.Rows[0];
                        Dt.Rows.Remove(row);

                        SqlConnection con = new SqlConnection(Con_Canopy);
                        SqlBulkCopy objbulk = new SqlBulkCopy(Con_Canopy);
                        //assigning Destination table name  
                        objbulk.DestinationTableName = "Sales";
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



        //[WebMethod]
        //public static string VerifyRemoteUW_Canopy(int InvoiceID, string Month, string Year)
        //{
        //    DataTable dt1 = new bllInvoice().GetIPSRemoteUWSalesForVerify(InvoiceID, Month, Year);
        //    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        //    Dictionary<string, object> row;
        //    if (dt1 != null)
        //    {
        //        foreach (DataRow dr in dt1.Rows)
        //        {
        //            row = new Dictionary<string, object>();
        //            foreach (DataColumn col in dt1.Columns)
        //            {
        //                row.Add(col.ColumnName, dr[col]);
        //            }
        //            rows.Add(row);
        //        }
        //    }
        //    JavaScriptSerializer ser = new JavaScriptSerializer();
        //    ser.MaxJsonLength = int.MaxValue;
        //    return ser.Serialize(rows);
        //}


        //[WebMethod]
        //public static string VerifyRemoteUWSales(int InvoiceID, string Month, string Year)
        //{
        //    DataTable dt1 = new bllInvoice().GetIPSRemoteUWSalesForVerify(InvoiceID, Month, Year);
        //    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        //    Dictionary<string, object> row;
        //    if (dt1 != null)
        //    {
        //        foreach (DataRow dr in dt1.Rows)
        //        {
        //            row = new Dictionary<string, object>();
        //            foreach (DataColumn col in dt1.Columns)
        //            {
        //                row.Add(col.ColumnName, dr[col]);
        //            }
        //            rows.Add(row);
        //        }
        //    }
        //    JavaScriptSerializer ser = new JavaScriptSerializer();
        //    ser.MaxJsonLength = int.MaxValue;
        //    return ser.Serialize(rows);
        //}

        public void sendEmailAddNewInvoice_Sales(int InvoiceId, string strInvoiceType)
        {
            var mc_Sales = new AddInvoiceSales();
            StringBuilder htmlBody = new StringBuilder();
            string Subject;
            string ProjectName = "Billing";
            string ProjectType = "";

            DateTime dtime = DateTime.Today;

            string Path = "";
            string ToAddress = string.Empty;
            string ToCC = string.Empty;

            ToAddress = "k.aarti@infinity-data.com";
            ToCC = "p.kedar@infinity-data.com";

            string BillingDatePeriod = "";

            DataTable dt = new DataTable();
            DataTable dtSummary = new DataTable();
            DataTable dtAdvancePaymentSymmary = new DataTable();

            dt = new bllInvoice().GetAllPendingInvoice_IPS(InvoiceId);

            if (dt.Rows.Count > 0)
            {
                string invNo = dt.Columns.Contains("InvoiceNo")
                    ? Convert.ToString(dt.Rows[0]["InvoiceNo"])
                    : Convert.ToString(dt.Rows[0]["VendorInvoiceNumber"]);

                string invType = dt.Columns.Contains("InvoiceType")
                    ? Convert.ToString(dt.Rows[0]["InvoiceType"])
                    : Convert.ToString(dt.Rows[0]["NewInvoiceType"]);

                Subject = "IPS - New Invoice" + "-" + invType + "-" + invNo + " : Pending for Approval";
                htmlBody.Append("<br />Dear Sir/Madam,<br />");
                htmlBody.Append("<br /><font color=brown face=Verdana size=2 ><b>New invoice has been added in system with below details" + BillingDatePeriod + ".</b></font><br />");

                htmlBody.Append("<br /><table border=\"1\" bordercolor='Black' style='border:solid 1px black;border-collapse:collapse;padding:3px; font-size:14px;'><tr style='background-color:#244b73; color:White;'><td colspan=18><center><b>" + ProjectName + "-Details " + BillingDatePeriod + "  </b></center></td></tr>");
                htmlBody.Append("<tr style='background-color:#244b73; color:White;'>" +
                    "<td><center><b>Vendor Name</b></center></td>" +
                    "<td><center><b>Vendor Type</b></center></td>" +
                    "<td><center><b>To Company</b></center></td>" +
                    "<td><center><b>Email Address</b></center></td>" +
                    "<td><center><b>Invoice No</b></center></td>" +
                    "<td><center><b>Invoice Date</b></center></td>" +
                    "<td><center><b>Service Period From</b></center></td>" +
                    "<td><center><b>Service Period To</b></center></td>" +
                    "<td><center><b>Invoice Category</b></center></td>" +
                    "<td><center><b>Invoice Description</b></center></td>" +
                    "<td><center><b>Invoice Amount</b></center></td>" +
                    "<td><center><b>Due Date</b></center></td>" +
                    "<td><center><b>Payment Terms</b></center></td>" +
                    "<td><center><b>Payment Method</b></center></td>" +
                    "<td><center><b>Bank Account</b></center></td>" +
                    "<td><center><b>Payment From Company</b></center></td>" +
                    "<td><center><b>Verification Remark</b></center></td>" +
                    "<td><center><b>Invoice Added in System On </b></center></td>" +
                    "</tr>");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string rowInvoiceNo = dt.Columns.Contains("InvoiceNo")
                        ? Convert.ToString(dt.Rows[i]["InvoiceNo"])
                        : Convert.ToString(dt.Rows[i]["VendorInvoiceNumber"]);

                    string rowInvoiceDate = dt.Columns.Contains("InvoiceDate")
                        ? Convert.ToString(dt.Rows[i]["InvoiceDate"])
                        : Convert.ToString(dt.Rows[i]["StatementDate"]);

                    string rowAddedDate = dt.Columns.Contains("Addeddate")
                        ? Convert.ToString(dt.Rows[i]["Addeddate"])
                        : Convert.ToString(dt.Rows[i]["AddedDate"]);

                    htmlBody.Append("<tr style='background-color:White; color:black;'>" +
                        "<td><center><b>" + Convert.ToString(dt.Rows[i]["VendorName"]) + "</b></center></td>" +
                        "<td><center><b>" + Convert.ToString(dt.Rows[i]["VendorType"]) + "</b></center></td>" +
                        "<td><center><b>" + Convert.ToString(dt.Rows[i]["ToCompany"]) + "</b></center></td>" +
                        "<td><center><b>" + Convert.ToString(dt.Rows[i]["EmailAddress"]) + "</b></center></td>" +
                        "<td><center><b>" + rowInvoiceNo + "</b></center></td>" +
                        "<td><center><b>" + rowInvoiceDate + "</b></center></td>" +
                        "<td><center><b>" + (string.IsNullOrWhiteSpace(Convert.ToString(dt.Rows[i]["ServicePeriodFrom"])) ? "" : Convert.ToDateTime(dt.Rows[i]["ServicePeriodFrom"]).ToString("MM/dd/yyyy")) + "</b></center></td>" +
                        "<td><center><b>" + (string.IsNullOrWhiteSpace(Convert.ToString(dt.Rows[i]["ServicePeriodTo"])) ? "" : Convert.ToDateTime(dt.Rows[i]["ServicePeriodTo"]).ToString("MM/dd/yyyy")) + "</b></center></td>" +
                        "<td><center><b>" + Convert.ToString(dt.Rows[i]["InvoiceCategory"]) + "</b></center></td>" +
                        "<td><center><b>" + Convert.ToString(dt.Rows[i]["InvoiceDescription"]) + "</b></center></td>" +
                        "<td><center><b>" + Convert.ToString(dt.Rows[i]["InvoiceAmount"]) + "</b></center></td>" +
                        "<td><center><b>" + Convert.ToString(dt.Rows[i]["DueDate"]) + "</b></center></td>" +
                        "<td><center><b>" + Convert.ToString(dt.Rows[i]["PaymentTerms"]) + "</b></center></td>" +
                        "<td><center><b>" + Convert.ToString(dt.Rows[i]["PaymentMethod"]) + "</b></center></td>" +
                        "<td><center><b>" + Convert.ToString(dt.Rows[i]["BankAccount"]) + "</b></center></td>" +
                        "<td><center><b>" + Convert.ToString(dt.Rows[i]["PaymentFromCompany"]) + "</b></center></td>" +
                        "<td><center><b>" + Convert.ToString(dt.Rows[i]["Remark"]) + "</b></center></td>" +
                        "<td><center><b>" + rowAddedDate + "</b></center></td>" +
                        "</tr>");
                }

                htmlBody.Append("</table>");

                htmlBody.Append("<br /><table cellspacing='7px' cellpadding='3px' width='700px' style='font-family: Verdana; font-size: 12px; border-collapse: collapse;'><tr><td align=\"left\" width=600>Please <a href='http://192.168.11.11/Vendor/Login.aspx" + BillingDatePeriod + "'>click here</a> For Approval</td></tr></table><br />");

                htmlBody.Append("<br /><br /><table width=\"650px\" style='font-size:13px;'><tr><td align=\"left\">Thanks,<br />Infinity </td></tr> <tr> <br /><td align=\"center\"><b>!!! This is software generated e-mail...Please do not reply. !!!</td></tr></table>");
                htmlBody.Append("<table width=\"600px\" style='font-size:10px;'><tr><td align=\"left\"></td></tr> <tr> <br /><td align=\"center\"><b>" + "***********************************************************************************************************************************************************************************************" + "</td></tr></table>");
                htmlBody.Append("<table width=\"600px\" style='font-size:10px;'><tr><td align=\"left\">" + "<b>CONFIDENTIALITY INFORMATION AND DISCLAIMER</b>" + "</td></tr> <tr> <br /><td align=\"center\"><b></td></tr></table>");
                htmlBody.Append("<table width=\"600px\" style='font-size:12px;'><tr><td align=\"left\">" + "This message contains information which may be confidential and privileged. Unless you are the addressee (or authorized to receive for the addressee), you may not use copy or disclose to anyone the message or any information contained in the message. If you have received the message in error, please advise the sender by reply e-mail and delete the message. Thank you." + "</td></tr><tr><td align=\"center\"><b></td></tr></table>");
                htmlBody.Append("<table width=\"600px\" style='font-size:10px;'><tr><td align=\"left\">" + "***********************************************************************************************************************************************************************************************" + " </td></tr> <tr> <br /><td align=\"center\"><b></td></tr></table>");

                mc_Sales.sendEmailInvoiceApproval_IPS(ToAddress, ToCC, "k.aarti@infinity-data.com", Subject, Path, htmlBody);
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



        [System.Web.Services.WebMethod]
        public static List<object> GetSalesVendors()
        {
            List<object> vendorList = new List<object>();

            bllInvoice inv = new bllInvoice();
            DataTable dt = inv.GetSalesVendors();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    vendorList.Add(new
                    {
                        VendorRegistrationID = dr["VendorRegistrationID"],
                        VendorName = dr["VendorName"]
                    });
                }
            }

            return vendorList;
        }
    }
}