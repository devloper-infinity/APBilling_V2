using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vendor_Portal.App_Code.BLL;
using ClosedXML.Excel;

namespace Vendor_Portal.Vendor
{
    public partial class AddInvoiceSales : System.Web.UI.Page
    {
        static string NewFileName_RemoteInvoice = "";
        static string NewFileName_RemoteExcel = "";

        static string GUIDFile = "";
        static string GUIDFilePDF = "";
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
        public static int InsertRemoteUWSalesInvoice(string Month, string Year, string InvoiceDate, string DueDate, string LoanCount, string InvoiceAmount, string InvoiceNumber,string InvoiceType,string Rate)
        {
            var mc_Sales = new AddInvoiceSales();
            int returnvalue = 0;
            //Convert.ToDateTime(InvoiceDate).ToString("MMM")
            //string SInvoiceType = "Sales";
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
            htParam.Add("InvoiceType", "Sales");
           

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
                File.Copy(NewFileName_RemoteInvoice, UniquePath + "\\" + GUIDFilePDF);
                File.Delete(NewFileName_RemoteInvoice);
                htParam.Add("FilePath", UniquePath + "\\" + GUIDFilePDF);

            }
            else
            {
                htParam.Add("FilePath", "");
            }

            htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
            htParam.Add("Delay", InvoiceType);
            htParam.Add("ProjectId", Rate);
            htParam.Add("Remark", "Sales-Invoice :"+ InvoiceType +  '~' + Convert.ToDateTime(InvoiceDate).ToString("MM/dd/yyyy"));


            returnvalue = new bllInvoice().Insert_Infinity_InvoiceDetails(htParam);
            
            if (returnvalue > 0)
            {
                mc_Sales.sendEmailAddNewInvoice_Sales(returnvalue, InvoiceType);
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

            //ToAddress = "p.kedar @infinityinternationals.us";
            //ToCC = "";

            ToAddress = "mdk@infinity-data.com,anita@infinity-data.com";
            ToCC = "jim@infinity-data.com";

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

                htmlBody.Append("<br /><table border=\"1\" bordercolor='Black' style='border:solid 1px black;border-collapse:collapse;padding:3px; font-size:14px;'><tr style='background-color:Skyblue; color:Black;'><td colspan=9><center><b>" + ProjectName + "-Details " + BillingDatePeriod + "  </b></center></td></tr>");
                htmlBody.Append("<tr style='background-color:Skyblue; color:Black;'><td><center><b>Invoice #</b></center></td><td><center><b>Invoice Type</b></center></td><td><center><b>Billing Period</b></center></td><td><center><b>Invoice Date</b></center></td><td><center><b>Due Date</b></center></td><td><center><b>No of Loans/Orders</b></center></td><td><center><b>Amount</b></center></td><td><center><b>Verification Remark</b></center></td><td><center><b>Invoice Added in System On </b></center></td></tr>");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    htmlBody.Append("<tr style='background-color:White; color:black;'><td><center><b>" + Convert.ToString(dt.Rows[i]["VendorInvoiceNumber"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["NewInvoiceType"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["BillingPeriod"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["StatementDate"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["DueDate"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["NoOfLoans"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["InvoiceAmount"]) + "</b></center></td><td><center><b>" + Convert.ToString(dt.Rows[i]["Remark"]) + "</b><td><center><b>" + Convert.ToString(dt.Rows[i]["AddedDate"]) + "</b></center></td></tr>");
                }

                htmlBody.Append("</table>");

                htmlBody.Append("<br /><table cellspacing='7px' cellpadding='3px' width='700px' style='font-family: Verdana; font-size: 12px; border-collapse: collapse;'><tr><td align=\"left\" width=600>Please <a href='http://192.168.11.11/Vendor/Login.aspx" + BillingDatePeriod + "'>click here</a> For Approval</td></tr></table><br />");


                htmlBody.Append("<br /><br /><table width=\"650px\" style='font-size:13px;'><tr><td align=\"left\">Thanks,<br />Infinity </td></tr> <tr> <br /><td align=\"center\"><b>!!! This is software generated e-mail...Please do not reply. !!!</td></tr></table>");
                htmlBody.Append("<table width=\"600px\" style='font-size:10px;'><tr><td align=\"left\"></td></tr> <tr> <br /><td align=\"center\"><b>" + "***********************************************************************************************************************************************************************************************" + "</td></tr></table>");
                htmlBody.Append("<table width=\"600px\" style='font-size:10px;'><tr><td align=\"left\">" + "<b>CONFIDENTIALITY INFORMATION AND DISCLAIMER</b>" + "</td></tr> <tr> <br /><td align=\"center\"><b></td></tr></table>");
                htmlBody.Append("<table width=\"600px\" style='font-size:12px;'><tr><td align=\"left\">" + "This message contains information which may be confidential and privileged. Unless you are the addressee (or authorized to receive for the addressee), you may not use copy or disclose to anyone the message or any information contained in the message. If you have received the message in error, please advise the sender by reply e-mail and delete the message. Thank you." + "</td></tr><tr><td align=\"center\"><b></td></tr></table>");
                htmlBody.Append("<table width=\"600px\" style='font-size:10px;'><tr><td align=\"left\">" + "***********************************************************************************************************************************************************************************************" + " </td></tr> <tr> <br /><td align=\"center\"><b></td></tr></table>");
                // sendMailForOnlineTracking_New(ToAddress, ToCC, "n.nilkanth@infinityinternationals.us,p.kedar@infinityinternationals.us", Subject, Path, htmlBody);
                mc_Sales.sendEmailInvoiceApproval_IPS(ToAddress, ToCC, "n.nilkanth@infinityinternationals.us,p.kedar@infinityinternationals.us,b.shubhangi@infinityinternationals.us", Subject, Path, htmlBody);
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
                mail.From = new MailAddress("ack@infinityinternationals.us", "IPS AP Billing", System.Text.Encoding.UTF8);
                mail.Subject = Subject;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = template.ToString();
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;

                string pass = new bllInvoice().GetPassword("ack");
                mail.Priority = System.Net.Mail.MailPriority.High;
                SmtpClient client = new SmtpClient();
                client.Credentials = new System.Net.NetworkCredential("ack@infinityinternationals.us", pass);
                client.Host = "smtpcorp.netcore.co.in";

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