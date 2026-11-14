//using DocumentFormat.OpenXml.VariantTypes;
using DocumentFormat.OpenXml.ExtendedProperties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
//using System.IdentityModel.Protocols.WSTrust;

//using System.Linq;
using System.Web;
using Vendor_Portal.Vendor;

namespace Vendor_Portal.App_Code.DAL
{
    public class dalInvoice
    {

        #region Infinity
        public DataTable GetInfinityInvoiceDetails(int EmployeeId)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetInfinity_InvoiceDetails");
            SQLHelper.AddParamToSQLCmd(cmd, "@EmployeeId", System.Data.SqlDbType.BigInt, 10, System.Data.ParameterDirection.Input, EmployeeId);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }

        public int UpdateInvoiceReconciliation_Canopy(Hashtable htParam)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_UpdateInvoice_Approved_Canopy_New");
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, htParam["InvoiceId"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@IsApproved", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, htParam["IsApproved"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Remark", System.Data.SqlDbType.NVarChar, 4000, System.Data.ParameterDirection.Input, htParam["Remark"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ApprovedBy", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, htParam["ApprovedBy"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Status", System.Data.SqlDbType.NVarChar, 4000, System.Data.ParameterDirection.Input, htParam["Status"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@LoansDeducted", System.Data.SqlDbType.NVarChar, 4000, System.Data.ParameterDirection.Input, htParam["LoansDeducted"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@AmountDeducted", System.Data.SqlDbType.NVarChar,4000, System.Data.ParameterDirection.Input, htParam["AmountDeducted"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@PaybleToVendor", System.Data.SqlDbType.NVarChar, 4000, System.Data.ParameterDirection.Input, htParam["PaybleToVendor"]);

            SQLHelper.AddParamToSQLCmd(cmd, "@ReturnValue", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.ReturnValue, null);
            SQLHelper.ExecuteNonQueryCmd(cmd);
            int ReturnValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
            cmd.Dispose();
            return ReturnValue;
        }

        public DataTable GetAllAttorneyDetails()
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetAllAttorneyDetails_MIS");
            DataTable dt = SQLHelper.ExecuteDataTableCmd_Sal(cmd);
            return dt;
        }

        public DataTable GetAllAttorneyDetails_InvoiceSummary()
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetAllAttorneySummary_MIS");
            DataTable dt = SQLHelper.ExecuteDataTableCmd_Sal(cmd);
            return dt;
        }

        public DataTable GetAllAttorneyDetails_BillingSummary()
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetAllPendingPaymentInvoices_New");
            DataTable dt = SQLHelper.ExecuteDataTableCmd_Sal(cmd);
            return dt;
        }

        public int UpdateInvoiceReconciliation(Hashtable htParam)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_UpdateInvoice_Approved_New");
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, htParam["InvoiceId"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@IsApproved", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, htParam["IsApproved"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Remark", System.Data.SqlDbType.NVarChar, 4000, System.Data.ParameterDirection.Input, htParam["Remark"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ApprovedBy", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, htParam["ApprovedBy"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Status", System.Data.SqlDbType.NVarChar, 4000, System.Data.ParameterDirection.Input, htParam["Status"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@LoansDeducted", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, htParam["LoansDeducted"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@AmountDeducted", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, htParam["AmountDeducted"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@PaybleToVendor", System.Data.SqlDbType.NVarChar, 4000, System.Data.ParameterDirection.Input, htParam["PaybleToVendor"]);

            SQLHelper.AddParamToSQLCmd(cmd, "@ReturnValue", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.ReturnValue, null);
            SQLHelper.ExecuteNonQueryCmd(cmd);
            int ReturnValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
            cmd.Dispose();
            return ReturnValue;
        }

        public int Insert_Infinity_InvoiceDetails(Hashtable htParam)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_Insert_Infinity_InvoiceDetails");
            SQLHelper.AddParamToSQLCmd(cmd, "@Month", System.Data.SqlDbType.NVarChar, 500, System.Data.ParameterDirection.Input, htParam["Month"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Year", System.Data.SqlDbType.NVarChar, 500, System.Data.ParameterDirection.Input, htParam["Year"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@StatementDate", System.Data.SqlDbType.NVarChar, 500, System.Data.ParameterDirection.Input, htParam["StatementDate"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@DueDate", System.Data.SqlDbType.NVarChar, 500, System.Data.ParameterDirection.Input, htParam["DueDate"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Domain", System.Data.SqlDbType.NVarChar, 500, System.Data.ParameterDirection.Input, htParam["Domain"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Currency", System.Data.SqlDbType.NVarChar, 500, System.Data.ParameterDirection.Input, htParam["Currency"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@NoOfLoans", System.Data.SqlDbType.NVarChar, 500, System.Data.ParameterDirection.Input, htParam["NoOfLoans"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@VendorInvoiceNumber", System.Data.SqlDbType.NVarChar, 500, System.Data.ParameterDirection.Input, htParam["VendorInvoiceNumber"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@TotalDue", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, htParam["TotalDue"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@VendorName", System.Data.SqlDbType.NVarChar, 500, System.Data.ParameterDirection.Input, htParam["VendorName"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceType", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, htParam["InvoiceType"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@FilePath", System.Data.SqlDbType.NVarChar, 5000, System.Data.ParameterDirection.Input, htParam["FilePath"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@AddedBy", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, htParam["AddedBy"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ProjectId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, htParam["ProjectId"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Delay", System.Data.SqlDbType.NVarChar, 5000, System.Data.ParameterDirection.Input, htParam["Delay"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Remark", System.Data.SqlDbType.NVarChar, 5000, System.Data.ParameterDirection.Input, htParam["Remark"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ReturnValue", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.ReturnValue, null);
            SQLHelper.ExecuteNonQueryCmd(cmd);
            int ReturnValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
            cmd.Dispose();
            return ReturnValue;
        }

        public int InsertInvoiceDetails(Hashtable htParam)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_InsertInvoiceDetails");
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, htParam["InvoiceID"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@BillingType", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, htParam["BillingType"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Frequency", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, htParam["Frequency"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@StatementDate", System.Data.SqlDbType.NVarChar, 500, System.Data.ParameterDirection.Input, htParam["StatementDate"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@VendorAccountNumber", System.Data.SqlDbType.NVarChar, 500, System.Data.ParameterDirection.Input, htParam["VendorAccountNumber"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@VendorInvoiceNumber", System.Data.SqlDbType.NVarChar, 500, System.Data.ParameterDirection.Input, htParam["VendorInvoiceNumber"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@TotalDue", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, htParam["TotalDue"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@VendorName", System.Data.SqlDbType.NVarChar, 500, System.Data.ParameterDirection.Input, htParam["VendorName"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@VendorAddress", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, htParam["VendorAddress"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Type", System.Data.SqlDbType.NVarChar, 10, System.Data.ParameterDirection.Input, htParam["Type"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceType", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, htParam["InvoiceType"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@FilePath", System.Data.SqlDbType.NVarChar, 5000, System.Data.ParameterDirection.Input, htParam["FilePath"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@AddedBy", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, htParam["AddedBy"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@LoanFilePath", System.Data.SqlDbType.NVarChar, 5000, System.Data.ParameterDirection.Input, htParam["LoanFilePath"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ReturnValue", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.ReturnValue, null);
            SQLHelper.ExecuteNonQueryCmd(cmd);
            int ReturnValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
            cmd.Dispose();
            return ReturnValue;
        }

        public int DeleteScienna(string Month, string Year)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_DeleteScienna");
            SQLHelper.AddParamToSQLCmd(cmd, "@Month", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, Month);
            SQLHelper.AddParamToSQLCmd(cmd, "@Year", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, Year);
            SQLHelper.AddParamToSQLCmd(cmd, "@ReturnValue", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.ReturnValue, null);
            SQLHelper.ExecuteNonQueryCmd(cmd);
            int ReturnValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
            cmd.Dispose();
            return ReturnValue;
        }
        public int DeleteSciennaLabour(string Month, string Year)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_DeleteSciennaLabour");
            SQLHelper.AddParamToSQLCmd(cmd, "@Month", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, Month);
            SQLHelper.AddParamToSQLCmd(cmd, "@Year", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, Year);
            SQLHelper.AddParamToSQLCmd(cmd, "@ReturnValue", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.ReturnValue, null);
            SQLHelper.ExecuteNonQueryCmd(cmd);
            int ReturnValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
            cmd.Dispose();
            return ReturnValue;
        }
        public DataTable GetSciennaDetailsAfterImport(int InvoiceID, string Month, string Year)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetSciennaDetailsAfterImport");
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, InvoiceID);
            SQLHelper.AddParamToSQLCmd(cmd, "@Month", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Month);
            SQLHelper.AddParamToSQLCmd(cmd, "@Year", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Year);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }

        public DataTable GetSciennaLaborDetailsAfterImport(int InvoiceID, string Month, string Year)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetSciennaLaborDetailsAfterImport");
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, InvoiceID);
            SQLHelper.AddParamToSQLCmd(cmd, "@Month", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Month);
            SQLHelper.AddParamToSQLCmd(cmd, "@Year", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Year);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }
        public DataTable GetSciennaLoanDetailsAfterImport()
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetSciennaLoanDetailsAfterImport_1");
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }
        public DataTable GetInfinityInvoiceDetails_Report(int EmployeeId)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetInfinity_InvoiceDetails_Report");
            SQLHelper.AddParamToSQLCmd(cmd, "@EmployeeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, EmployeeId);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }

        public DataTable GetInfinityInvoiceDetailsPayment_Report(int EmployeeId)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_PaymentIntimation");
            SQLHelper.AddParamToSQLCmd(cmd, "@EmployeeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, EmployeeId);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }

        public DataTable GetInfinityInvoiceDetailsPaymentCanopy_Report(int EmployeeId)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_PaymentIntimation_Canopy");
            SQLHelper.AddParamToSQLCmd(cmd, "@EmployeeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, EmployeeId);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }



        public DataTable GetInfinityInvoiceDetailsCanopy_Report(int EmployeeId)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetInfinity_InvoiceDetailsCanopy_Report");
            SQLHelper.AddParamToSQLCmd(cmd, "@EmployeeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, EmployeeId);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }
        public DataTable GetAllInvoiceLoansDetailsForReport(int InvoiceID, string InvType)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetAllInvoiceLoansDetailsForReport");
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, InvoiceID);
            SQLHelper.AddParamToSQLCmd(cmd, "@InvType", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, InvType);
            DataTable dt = SQLHelper.ExecuteDataTableCmd_VendorBilling(cmd);
            return dt;
        }
        public DataTable GetAllLauraMac()
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetAllLauraMac_MIS");
            DataTable dt = SQLHelper.ExecuteDataTableCmd_Sal(cmd);
            return dt;
        }

        public DataTable GetLauraMacBillingLoanWiseDetails(string FormDate, string ToDate)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetLauraMacBillingLoanWiseDetails_tab5");
            SQLHelper.AddParamToSQLCmd(cmd, "@CurrentDate", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, FormDate);
            SQLHelper.AddParamToSQLCmd(cmd, "@ToDate", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, ToDate);
            DataTable dt = SQLHelper.ExecuteDataTableCmd_Sal(cmd);
            return dt;
        }

        public DataTable GetAllStewartIA_Summary()
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetAllStewartIASummary_MIS");
            DataTable dt = SQLHelper.ExecuteDataTableCmd_Sal(cmd);
            return dt;
        }

        public DataTable GetAllLoanLogic_Details()
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetAllLoanLogicsDetails_MIS");
            DataTable dt = SQLHelper.ExecuteDataTableCmd_Sal(cmd);
            return dt;
        }

        public DataTable GetAllLoanLogic_Summary()
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetAllLoanLogicsSummary_MIS");
            DataTable dt = SQLHelper.ExecuteDataTableCmd_Sal(cmd);
            return dt;
        }

        public DataTable GetDataForMarginCanopy_LoanWise(string FromDate, string ToDate)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetNegativeMarginReport_DataSet_New_Canopy_Details");
            SQLHelper.AddParamToSQLCmd(cmd, "@FromDate", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, FromDate);
            SQLHelper.AddParamToSQLCmd(cmd, "@ToDate", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, ToDate);
            DataTable dt = SQLHelper.ExecuteDataTableCmd_Sal(cmd);
            return dt;
        }


        public DataTable GetDataMonthlyVendorData_Canopy(string Year)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_getMonthlyVendorSummary_Canopy");
            SQLHelper.AddParamToSQLCmd(cmd, "@Year", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, Year);
            DataTable dt = SQLHelper.ExecuteDataTableCmd_Sal(cmd);
            return dt;
        }

        public DataTable GetDataMonthlyVendorData_IPS(string Year)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_getMonthlyVendorSummary");
            SQLHelper.AddParamToSQLCmd(cmd, "@Year", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, Year);
            DataTable dt = SQLHelper.ExecuteDataTableCmd_Sal(cmd);
            return dt;
        }

        public DataTable GetAllCanopyMagnaRecords()
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetAllMagna_MIS");
            DataTable dt = SQLHelper.ExecuteDataTableCmd_Sal(cmd);
            return dt;
        }

        public DataSet GetProjectHeadersWithData_DS(string FormDate, string ToDate)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_getComplianceBillingReportCanopy");
            SQLHelper.AddParamToSQLCmd(cmd, "@FromDate", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, FormDate);
            SQLHelper.AddParamToSQLCmd(cmd, "@ToDate", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, ToDate);
            DataSet dt = SQLHelper.ExecuteDataSetCmd_Sal(cmd);
            return dt;
        }

        public DataTable GetDataForMargin_DealWise(string FromDate, string ToDate)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetNegativeMarginReport_NewMIS_Dealwise");
            SQLHelper.AddParamToSQLCmd(cmd, "@FromDate", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, FromDate);
            SQLHelper.AddParamToSQLCmd(cmd, "@ToDate", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, ToDate);
            DataTable dt = SQLHelper.ExecuteDataTableCmd_ERP(cmd);
            return dt;
        }

        public DataTable GetDataForMargin_ProjectWise(string FromDate, string ToDate)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetNegativeMarginReport_NewMIS_Projectwise");
            SQLHelper.AddParamToSQLCmd(cmd, "@FromDate", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, FromDate);
            SQLHelper.AddParamToSQLCmd(cmd, "@ToDate", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, ToDate);
            DataTable dt = SQLHelper.ExecuteDataTableCmd_ERP(cmd);
            return dt;
        }

        public DataTable GetDataForMargin_TypeWise(string FromDate, string ToDate)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetNegativeMarginReport_NewMIS_Typewise");
            SQLHelper.AddParamToSQLCmd(cmd, "@FromDate", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, FromDate);
            SQLHelper.AddParamToSQLCmd(cmd, "@ToDate", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, ToDate);
            DataTable dt = SQLHelper.ExecuteDataTableCmd_ERP(cmd);
            return dt;
        }

        public DataTable GetDataForMargin_LoanWise(string FromDate, string ToDate)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetNegativeMarginReport_NewMIS_Details");
            SQLHelper.AddParamToSQLCmd(cmd, "@FromDate", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, FromDate);
            SQLHelper.AddParamToSQLCmd(cmd, "@ToDate", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, ToDate);
            DataTable dt = SQLHelper.ExecuteDataTableCmd_ERP(cmd);
            return dt;
        }
        public DataTable GetComplainceEaseLoanwiseDetails(string FormDate, string ToDate)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_CanopyCompalianceLoanwise");
            SQLHelper.AddParamToSQLCmd(cmd, "@FromDate", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, FormDate);
            SQLHelper.AddParamToSQLCmd(cmd, "@ToDate", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, ToDate);
            DataTable dt = SQLHelper.ExecuteDataTableCmd_Sal(cmd);
            return dt;
        }

        public DataTable GetComplainceEaseDealwiseDetails(string FormDate, string ToDate)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_CanopyCompalianceDealwise");
            SQLHelper.AddParamToSQLCmd(cmd, "@FromDate", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, FormDate);
            SQLHelper.AddParamToSQLCmd(cmd, "@ToDate", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, ToDate);
            DataTable dt = SQLHelper.ExecuteDataTableCmd_Sal(cmd);
            return dt;
        }

        public DataTable GetComplainceEaseProjectwiseDetails(string FormDate, string ToDate)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_CanopyCompalianceProjectwise");
            SQLHelper.AddParamToSQLCmd(cmd, "@FromDate", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, FormDate);
            SQLHelper.AddParamToSQLCmd(cmd, "@ToDate", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, ToDate);
            DataTable dt = SQLHelper.ExecuteDataTableCmd_Sal(cmd);
            return dt;
        }

        public DataTable GetComplainceEaseTypewiseDetails(string FormDate, string ToDate)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_CanopyCompalianceTypewise");
            SQLHelper.AddParamToSQLCmd(cmd, "@FromDate", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, FormDate);
            SQLHelper.AddParamToSQLCmd(cmd, "@ToDate", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, ToDate);
            DataTable dt = SQLHelper.ExecuteDataTableCmd_Sal(cmd);
            return dt;
        }

        public DataTable GetComplainceEaseSummary(string FormDate, string ToDate)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_CanopyCompalianceSummary");
            SQLHelper.AddParamToSQLCmd(cmd, "@FromDate", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, FormDate);
            SQLHelper.AddParamToSQLCmd(cmd, "@ToDate", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, ToDate);
            DataTable dt = SQLHelper.ExecuteDataTableCmd_Sal(cmd);
            return dt;
        }

        public DataTable GetAllStewartIA_Details()
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetAllStewartIADetails_MIS");
            DataTable dt = SQLHelper.ExecuteDataTableCmd_Sal(cmd);
            return dt;
        }

        public DataTable GetLauraMacBillingScriptChargesDetails(string FormDate, string ToDate)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetLauraMacBillingScriptChargesDetails_tab6");
            SQLHelper.AddParamToSQLCmd(cmd, "@CurrentDate", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, FormDate);
            SQLHelper.AddParamToSQLCmd(cmd, "@ToDate", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, ToDate);
            DataTable dt = SQLHelper.ExecuteDataTableCmd_Sal(cmd);
            return dt;
        }

        public DataTable GetLauraMacBillingWorkOrdersDetails()
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetLauraMacBillingWorkOrdersDetails");
            DataTable dt = SQLHelper.ExecuteDataTableCmd_Sal(cmd);
            return dt;
        }
        public DataTable GetAllInvoiceForUpdatePaidDateAsPerPayees(int InvoiceID, string InvType)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetAllInvoiceForUpdatePaidDateAsPerPayees");
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, InvoiceID);
            SQLHelper.AddParamToSQLCmd(cmd, "@InvType", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, InvType);
            DataTable dt = SQLHelper.ExecuteDataTableCmd_VendorBilling(cmd);
            return dt;
        }

        public DataTable GetInfinity_InvoiceDetails_Approval_Other(int EmployeeID, string Company, string InvType)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetInfinity_InvoiceDetails_Approval_Other");
            SQLHelper.AddParamToSQLCmd(cmd, "@EmployeeID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, EmployeeID);
            SQLHelper.AddParamToSQLCmd(cmd, "@Company", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, Company);
            SQLHelper.AddParamToSQLCmd(cmd, "@InvType", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, InvType);
            DataTable dt = SQLHelper.ExecuteDataTableCmd_VendorBilling(cmd);
            return dt;
        }


        public DataTable GetTrackingSheetCreditData(string FromDate, string ToDate)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetTrackingSheetCreditData_IPS");
            SQLHelper.AddParamToSQLCmd(cmd, "@FromDate", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, FromDate);
            SQLHelper.AddParamToSQLCmd(cmd, "@ToDate", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, ToDate);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }

        public DataTable GetTrackingSheetServiceData(string FromDate, string ToDate)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetTrackingSheetServiceData_IPS");
            SQLHelper.AddParamToSQLCmd(cmd, "@FromDate", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, FromDate);
            SQLHelper.AddParamToSQLCmd(cmd, "@ToDate", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, ToDate);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }
        #endregion Infinity

        #region Canopy
        public System.Data.DataTable GetCanopyRemoteUWForVerify(int InvoiceID, string Month, string Year)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetCanopyRemoteUWLoansVerify_ForVerify");
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, InvoiceID);
            SQLHelper.AddParamToSQLCmd(cmd, "@Month", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Month);
            SQLHelper.AddParamToSQLCmd(cmd, "@Year", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Year);
            System.Data.DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }

        public int Insert_Infinity_InvoiceDetails_Canopy(Hashtable htParam)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_Insert_Infinity_InvoiceDetails_Canopy");
            SQLHelper.AddParamToSQLCmd(cmd, "@Month", System.Data.SqlDbType.NVarChar, 500, System.Data.ParameterDirection.Input, htParam["Month"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Year", System.Data.SqlDbType.NVarChar, 500, System.Data.ParameterDirection.Input, htParam["Year"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@StatementDate", System.Data.SqlDbType.NVarChar, 500, System.Data.ParameterDirection.Input, htParam["StatementDate"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@DueDate", System.Data.SqlDbType.NVarChar, 500, System.Data.ParameterDirection.Input, htParam["DueDate"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Domain", System.Data.SqlDbType.NVarChar, 500, System.Data.ParameterDirection.Input, htParam["Domain"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Currency", System.Data.SqlDbType.NVarChar, 500, System.Data.ParameterDirection.Input, htParam["Currency"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@NoOfLoans", System.Data.SqlDbType.NVarChar, 500, System.Data.ParameterDirection.Input, htParam["NoOfLoans"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@VendorInvoiceNumber", System.Data.SqlDbType.NVarChar, 500, System.Data.ParameterDirection.Input, htParam["VendorInvoiceNumber"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@TotalDue", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, htParam["TotalDue"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@VendorName", System.Data.SqlDbType.NVarChar, 500, System.Data.ParameterDirection.Input, htParam["VendorName"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceType", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, htParam["InvoiceType"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@FilePath", System.Data.SqlDbType.NVarChar, 5000, System.Data.ParameterDirection.Input, htParam["FilePath"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@AddedBy", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, htParam["AddedBy"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ProjectId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, htParam["ProjectId"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Delay", System.Data.SqlDbType.NVarChar, 5000, System.Data.ParameterDirection.Input, htParam["Delay"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Remark", System.Data.SqlDbType.NVarChar, 5000, System.Data.ParameterDirection.Input, htParam["Remark"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ReturnValue", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.ReturnValue, null);
            SQLHelper.ExecuteNonQueryCmd(cmd);
            int ReturnValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
            cmd.Dispose();
            return ReturnValue;
        }
        public int DeleteStewartLoan(string Month, string Year)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_DeleteAVMLoan");
            SQLHelper.AddParamToSQLCmd(cmd, "@Month", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, Month);
            SQLHelper.AddParamToSQLCmd(cmd, "@Year", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, Year);
            SQLHelper.AddParamToSQLCmd(cmd, "@ReturnValue", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.ReturnValue, null);
            SQLHelper.ExecuteNonQueryCmd(cmd);
            int ReturnValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
            cmd.Dispose();
            return ReturnValue;
        }

        public int InsertInvoiceDetails_Canopy(Hashtable htParam)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_InsertInvoiceDetails_Canopy");
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, htParam["InvoiceID"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Month", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, htParam["Month"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Year", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, htParam["Year"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@BillingType", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, htParam["BillingType"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Frequency", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, htParam["Frequency"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@StatementDate", System.Data.SqlDbType.NVarChar, 500, System.Data.ParameterDirection.Input, htParam["StatementDate"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@VendorAccountNumber", System.Data.SqlDbType.NVarChar, 500, System.Data.ParameterDirection.Input, htParam["VendorAccountNumber"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@VendorInvoiceNumber", System.Data.SqlDbType.NVarChar, 500, System.Data.ParameterDirection.Input, htParam["VendorInvoiceNumber"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@TotalDue", System.Data.SqlDbType.Decimal, 0, System.Data.ParameterDirection.Input, htParam["TotalDue"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@VendorName", System.Data.SqlDbType.NVarChar, 500, System.Data.ParameterDirection.Input, htParam["VendorName"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@VendorAddress", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, htParam["VendorAddress"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Type", System.Data.SqlDbType.NVarChar, 10, System.Data.ParameterDirection.Input, htParam["Type"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceType", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, htParam["InvoiceType"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@FilePath", System.Data.SqlDbType.NVarChar, 5000, System.Data.ParameterDirection.Input, htParam["FilePath"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@AddedBy", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, htParam["AddedBy"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@LoanFilePath", System.Data.SqlDbType.NVarChar, 5000, System.Data.ParameterDirection.Input, htParam["LoanFilePath"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ReturnValue", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.ReturnValue, null);
            SQLHelper.ExecuteNonQueryCmd(cmd);
            int ReturnValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
            cmd.Dispose();
            return ReturnValue;
        }

        public DataTable GetStewartDetailsForVerify(int InvoiceID, string Month, string Year)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetStewartIADetails_ForVerify");
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, InvoiceID);
            SQLHelper.AddParamToSQLCmd(cmd, "@Month", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Month);
            SQLHelper.AddParamToSQLCmd(cmd, "@Year", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Year);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }
        public DataTable GetAllCreditSoftPull_Summary()
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetAllCreditSoftSummary_MIS");
            DataTable dt = SQLHelper.ExecuteDataTableCmd_Sal(cmd);
            return dt;
        }
        public DataTable GetAllAP670PaidInvoices()
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetAllPendingPaymentInvoices_670_Paid");//usp_GetAllPendingPaymentInvoices_670
            DataTable dt = SQLHelper.ExecuteDataTableCmd_Sal(cmd);
            return dt;
        }
        public DataTable GetAllAP670UnPaidInvoices()
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetAllPendingPaymentInvoices_670");
            DataTable dt = SQLHelper.ExecuteDataTableCmd_Sal(cmd);
            return dt;
        }
        public DataTable GetAllCreditSoftPull_Details()
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetAllCreditSoftDetails_MIS");
            DataTable dt = SQLHelper.ExecuteDataTableCmd_Sal(cmd);
            return dt;
        }
        public DataTable GetComplianceEaseDetailsForVerify(int InvoiceID, string Month, string Year)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetComplianceEaseDetails_ForVerify_Canopy");
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, InvoiceID);
            SQLHelper.AddParamToSQLCmd(cmd, "@Month", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Month);
            SQLHelper.AddParamToSQLCmd(cmd, "@Year", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Year);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }
        public DataTable GetLauraMacAfterImport(int InvoiceID, string Month, string Year)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetLauraMacDetails_ForVerify");
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, InvoiceID);
            SQLHelper.AddParamToSQLCmd(cmd, "@Month", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Month);
            SQLHelper.AddParamToSQLCmd(cmd, "@Year", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Year);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }

        public DataTable GetInfinityInvoiceDetails_Canopy(int InvoiceID)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetInfinity_InvoiceDetails_Approval_canopy");
            SQLHelper.AddParamToSQLCmd(cmd, "@EmployeeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, InvoiceID);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }

        public DataTable GetInfinityInvoiceDetails_IPS(int InvoiceID)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetInfinity_InvoiceDetails_Approval_IPS");
            SQLHelper.AddParamToSQLCmd(cmd, "@EmployeeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, InvoiceID);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }

        public DataTable GetInfinityInvoiceDetails_Approval_canopy(int EmployeeId)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetInfinity_InvoiceDetails_Approval_Canopy_New");
            SQLHelper.AddParamToSQLCmd(cmd, "@EmployeeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, EmployeeId);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }

        public DataTable GetInfinityInvoiceDetails_Approval_IPS(int EmployeeId)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetInfinity_InvoiceDetails_Approval_IPS_New");
            SQLHelper.AddParamToSQLCmd(cmd, "@EmployeeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, EmployeeId);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }

        public DataTable GetInfinityInvoicesForUpdatePaidDate(int EmployeeId, string Company)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetInvoicesForUpdatePaidDate_ByCompany");//usp_GetInfinity_InvoiceDetails_Approval_MIS_UpdatePaid
            SQLHelper.AddParamToSQLCmd(cmd, "@EmployeeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, EmployeeId);
            SQLHelper.AddParamToSQLCmd(cmd, "@Company", System.Data.SqlDbType.NVarChar, 10, System.Data.ParameterDirection.Input, Company);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }

        public DataTable GetInvoicesForUpdateClientInvoiceNo(int EmployeeId, string Company)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetInvoicesForUpdateClientInvoiceNo_ByCompany");//usp_GetInfinity_InvoiceDetails_Approval_MIS_UpdatePaid
            SQLHelper.AddParamToSQLCmd(cmd, "@EmployeeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, EmployeeId);
            SQLHelper.AddParamToSQLCmd(cmd, "@Company", System.Data.SqlDbType.NVarChar, 10, System.Data.ParameterDirection.Input, Company);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }

        public DataTable GetInvoiceDetailsForUpdateDateByID_IPS(int InvoiceID)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetInvoiceDetailsForUpdateDateByID_IPS");
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, InvoiceID);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }

        public DataTable GetAllInvoiceForUpdatePaidDateOtherPayee(int PayeeID, int InvoiceId)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetAllInvoiceForUpdatePaidDateOtherPayee_New");
            SQLHelper.AddParamToSQLCmd(cmd, "@PayeeID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, PayeeID);
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, InvoiceId);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }


        public DataTable GetInvoiceDetailsForUpdateDateByOtherPayee_Canopy(int PayeeID, int InvoiceId)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetInvoiceDetailsForUpdateDateByOtherPayee_Canopy");
            SQLHelper.AddParamToSQLCmd(cmd, "@PayeeID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, PayeeID);
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, InvoiceId);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }


        public DataTable GetInvoiceDetailsForUpdateDateByID_Canopy(int InvoiceID)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetInvoiceDetailsForUpdateDateByID_Canopy");
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, InvoiceID);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }

        public DataTable GetAllPendingPaymentInvoices_Canopy(int InvoiceId)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetAllInvoices_ForEmail_Canopy_Approved");
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, InvoiceId);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }
        public DataTable GetAllInvoiceLoansDetailsForReport_New(int InvoiceID, string InvType)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetAllInvoiceLoansDetailsForReport");
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, InvoiceID);
            SQLHelper.AddParamToSQLCmd(cmd, "@InvType", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, InvType);
            DataTable dt = SQLHelper.ExecuteDataTableCmd_VendorBilling(cmd);
            return dt;
        }
        public DataTable GetAllPendingPaymentInvoices(int InvoiceId)
        {
            //SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetAllPendingPaymentInvoices_ForEmail_New");
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetAllInvoices_ForEmail");
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, InvoiceId);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }

        public DataTable GetAllInvoiceDetails(int InvoiceId)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetAllInvoiceDetails");
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, InvoiceId);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }



        public DataTable GetComplianceRecordForReconsile(int InvoiceID, string Month, string Year)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetComplianceRecordForReconsile");
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, InvoiceID);
            SQLHelper.AddParamToSQLCmd(cmd, "@Month", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Month);
            SQLHelper.AddParamToSQLCmd(cmd, "@Year", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Year);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }

        public DataTable GetStewartDataForReconcile(string Month, string Year)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetStewartIADetails_ForVerify_InvoiceApproval_1");
            SQLHelper.AddParamToSQLCmd(cmd, "@Month", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Month);
            SQLHelper.AddParamToSQLCmd(cmd, "@Year", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Year);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }

        public DataTable GetLauraMacDataForReconcile(int InvoiceID, string Month, string Year)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetSciennaDetailsAfterImport_LauraMac");
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, InvoiceID);
            SQLHelper.AddParamToSQLCmd(cmd, "@Month", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Month);
            SQLHelper.AddParamToSQLCmd(cmd, "@Year", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Year);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }

        //GetIPSRemoteUWLoanLogicsForVerifyint

        public DataTable GetIPSRemoteUWLoanLogicsForVerifyint(int InvoiceID, string Month, string Year)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetLoanLogicsDetails_ForVerify");
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, InvoiceID);
            SQLHelper.AddParamToSQLCmd(cmd, "@Month", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Month);
            SQLHelper.AddParamToSQLCmd(cmd, "@Year", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Year);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }

        public DataTable GetIPSRemoteUWLoanLogicsForVerifyint_IPS(int InvoiceID, string Month, string Year)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetLoanLogicsDetails_ForVerify_IPS");
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, InvoiceID);
            SQLHelper.AddParamToSQLCmd(cmd, "@Month", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Month);
            SQLHelper.AddParamToSQLCmd(cmd, "@Year", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Year);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }


        public DataTable GetIPSRemoteUWLaminrForVerify(int InvoiceID, string Month, string Year)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetLaminrsDetails_ForVerify");
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, InvoiceID);
            SQLHelper.AddParamToSQLCmd(cmd, "@Month", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Month);
            SQLHelper.AddParamToSQLCmd(cmd, "@Year", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Year);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }

        public DataTable GetDiscripancyInvoice()
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetDiscripancyInvoices");
               DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }


        public DataTable GetLoansForReconcillation(int InvoiceID, string Status, string InvType, string TableName)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetLoansForReconcillation");
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceID", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, InvoiceID);
            SQLHelper.AddParamToSQLCmd(cmd, "@Status", System.Data.SqlDbType.NVarChar, 300, System.Data.ParameterDirection.Input, Status);
            SQLHelper.AddParamToSQLCmd(cmd, "@InvType", System.Data.SqlDbType.NVarChar, 300, System.Data.ParameterDirection.Input, InvType);
            SQLHelper.AddParamToSQLCmd(cmd, "@TableName", System.Data.SqlDbType.NVarChar, 300, System.Data.ParameterDirection.Input, TableName);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }

        public DataTable GetIPSRemoteUWAttorneyForVerify(int InvoiceID, string Month, string Year)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetAttorneyDetails_ForVerify");
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, InvoiceID);
            SQLHelper.AddParamToSQLCmd(cmd, "@Month", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Month);
            SQLHelper.AddParamToSQLCmd(cmd, "@Year", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Year);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }


        public int UpdateReconcileRemark(Hashtable htParam)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_UpdateReconcileRemark");
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceID", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, htParam["InvoiceID"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@UniqueID", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, htParam["UniqueID"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceType", System.Data.SqlDbType.NVarChar, 500, System.Data.ParameterDirection.Input, htParam["InvoiceType"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@TableName", System.Data.SqlDbType.NVarChar, 500, System.Data.ParameterDirection.Input, htParam["TableName"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@UserRemark", System.Data.SqlDbType.NVarChar, -1, System.Data.ParameterDirection.Input, htParam["UserRemark"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@AddedBy", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, htParam["AddedBy"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ReturnValue", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.ReturnValue, null);
            SQLHelper.ExecuteNonQueryCmd(cmd);
            int ReturnValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
            cmd.Dispose();
            return ReturnValue;
        }


        public int VerifyLoanNo(Hashtable htParam)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_VerifyLoanNo_Stewart_1");
            SQLHelper.AddParamToSQLCmd(cmd, "@BillingId", System.Data.SqlDbType.NVarChar, 400, System.Data.ParameterDirection.Input, htParam["BillingId"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@DisputeValue", System.Data.SqlDbType.NVarChar, 5000, System.Data.ParameterDirection.Input, htParam["DisputeValue"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@UserRemark", System.Data.SqlDbType.NVarChar, 5000, System.Data.ParameterDirection.Input, htParam["UserRemark"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@AddedBy", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, htParam["AddedBy"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ReturnValue", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.ReturnValue, null);
            SQLHelper.ExecuteNonQueryCmd(cmd);
            int ReturnValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
            cmd.Dispose();
            return ReturnValue;
        }

        public int VerifyLauraMac(Hashtable htParam)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_VerifyLauraMac");
            SQLHelper.AddParamToSQLCmd(cmd, "@BillingId", System.Data.SqlDbType.NVarChar, 400, System.Data.ParameterDirection.Input, htParam["BillingId"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@UserRemark", System.Data.SqlDbType.NVarChar, 5000, System.Data.ParameterDirection.Input, htParam["UserRemark"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@AddedBy", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, htParam["AddedBy"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ReturnValue", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.ReturnValue, null);
            SQLHelper.ExecuteNonQueryCmd(cmd);
            int ReturnValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
            cmd.Dispose();
            return ReturnValue;
        }

        public int VerifyCompliance(Hashtable htParam)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_VerifyCompliance");
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, htParam["InvoiceID"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@VerifyBy", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, htParam["VerifyBy"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Remark", System.Data.SqlDbType.NVarChar, 5000, System.Data.ParameterDirection.Input, htParam["Remark"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ReturnValue", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.ReturnValue, null);
            SQLHelper.ExecuteNonQueryCmd(cmd);
            int ReturnValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
            cmd.Dispose();
            return ReturnValue;
        }

        public int ReconcileLoan_Generalised(Hashtable htParam)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_ReconcileLoan_Generalised");
            SQLHelper.AddParamToSQLCmd(cmd, "@Vendor", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, htParam["Vendor"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Month", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, htParam["Month"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Year", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, htParam["Year"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Remark", System.Data.SqlDbType.NVarChar, 5000, System.Data.ParameterDirection.Input, htParam["Remark"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@VerifyBy", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, htParam["VerifyBy"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, htParam["InvoiceID"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ReturnValue", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.ReturnValue, null);
            SQLHelper.ExecuteNonQueryCmd(cmd);
            int ReturnValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
            cmd.Dispose();
            return ReturnValue;
        }

        public int InsertIntoReminderDate(Hashtable htParam)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_InsertIntoReminderDate");
            SQLHelper.AddParamToSQLCmd(cmd, "@ReminderDate", System.Data.SqlDbType.NVarChar, 20, System.Data.ParameterDirection.Input, htParam["ReminderDate"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Company", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, htParam["Company"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Vendor", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, htParam["Vendor"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@AddedBy", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, htParam["AddedBy"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ReturnValue", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.ReturnValue, null);
            SQLHelper.ExecuteNonQueryCmd(cmd);
            int ReturnValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
            cmd.Dispose();
            return ReturnValue;
        }


        public int UpdateReminderDate(Hashtable htParam)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_UpdateReminderDate");
            SQLHelper.AddParamToSQLCmd(cmd, "@ReminderDateID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, htParam["ReminderDateID"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ReminderDate", System.Data.SqlDbType.NVarChar, 20, System.Data.ParameterDirection.Input, htParam["ReminderDate"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Company", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, htParam["Company"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Vendor", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, htParam["Vendor"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@AddedBy", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, htParam["AddedBy"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ReturnValue", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.ReturnValue, null);
            SQLHelper.ExecuteNonQueryCmd(cmd);
            int ReturnValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
            cmd.Dispose();
            return ReturnValue;
        }

        public DataTable GetAllReminderDates()
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetAllReminderDates");
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }


        public DataTable GetVendorRecordForReconcile_Generilised(string VendorType, int InvoiceID, string Month, string Year)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetVendorRecordForReconcile_Generilised");
            SQLHelper.AddParamToSQLCmd(cmd, "@VendorType", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, VendorType);
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceID", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, InvoiceID);
            SQLHelper.AddParamToSQLCmd(cmd, "@Month", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, Month);
            SQLHelper.AddParamToSQLCmd(cmd, "@Year", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, Year);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }

        #endregion Canopy

        #region Cost Master


        public DataTable GetAllVendorAsPerCompany(string Company)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetAllVendorAsPerCompany");
            SQLHelper.AddParamToSQLCmd(cmd, "@Company", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, Company);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }


        public DataTable GetAllVendorRateConfiguration(string Company)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_ViewVendorRateConfiguration");
            SQLHelper.AddParamToSQLCmd(cmd, "@Company", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, Company);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }

        public int InsertVendorRateConfigurationForbilling(Hashtable htParam)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_InsertVendorRateConfigurastion");
            SQLHelper.AddParamToSQLCmd(cmd, "@Company", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, htParam["Company"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@EffectiveDate", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, htParam["EffectiveDate"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Vendor", System.Data.SqlDbType.NVarChar, 1000, System.Data.ParameterDirection.Input, htParam["VendorName"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@RType", System.Data.SqlDbType.NVarChar, 1000, System.Data.ParameterDirection.Input, htParam["RType"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Rate", System.Data.SqlDbType.Decimal, 100, System.Data.ParameterDirection.Input, htParam["Rate"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@TRate", System.Data.SqlDbType.Decimal, 100, System.Data.ParameterDirection.Input, htParam["TRate"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@AddedBy", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, htParam["AddedBy"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ReturnValue", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.ReturnValue, null);
            SQLHelper.ExecuteNonQueryCmd(cmd);

            int ReturnValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
            return ReturnValue;
        }


        public DataTable GetAllVendorRateConfiguration_ForApproval(string Company)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_ViewVendorRateConfiguration_ForApproval");
            SQLHelper.AddParamToSQLCmd(cmd, "@Company", System.Data.SqlDbType.NVarChar, 20, System.Data.ParameterDirection.Input, Company);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }


        public int ApproveVendorRateConfigurastion(Hashtable htParam)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_ApproveVendorRateConfigurastion");
            SQLHelper.AddParamToSQLCmd(cmd, "@RConfigurationID", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, htParam["RConfigurationID"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@VerifyRemark", System.Data.SqlDbType.NVarChar, 1000, System.Data.ParameterDirection.Input, htParam["VerifyRemark"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@VerifyBy", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, htParam["VerifyBy"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ReturnValue", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.ReturnValue, null);
            SQLHelper.ExecuteNonQueryCmd(cmd);

            int ReturnValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
            return ReturnValue;
        }


        #endregion


        public int UpdateRemark(Hashtable htParam)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_UpdateInvociePaidDate");//usp_UpdateInvociePaymentDate
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, htParam["InvoiceID"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@PaymentDate", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, htParam["PaymentDate"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Company", System.Data.SqlDbType.NVarChar, 10, System.Data.ParameterDirection.Input, htParam["Company"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Remark", System.Data.SqlDbType.NVarChar, 4000, System.Data.ParameterDirection.Input, htParam["Remark"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@UTR", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, htParam["UTR"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@UpdatedBy", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, htParam["UpdatedBy"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ReturnValue", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.ReturnValue, null);
            SQLHelper.ExecuteDataTableCmd_VendorBilling(cmd);
            int ReturnValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
            return ReturnValue;
        }

        public int UpdatePaidDateOthers(Hashtable htParam)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_UpdatePaidDateOthers");//usp_UpdateInvociePaymentDate
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, htParam["InvoiceID"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@PayeeID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, htParam["PayeeID"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@PaymentDate", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, htParam["PaymentDate"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Company", System.Data.SqlDbType.NVarChar, 10, System.Data.ParameterDirection.Input, htParam["Company"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Vendor", System.Data.SqlDbType.NVarChar, 10, System.Data.ParameterDirection.Input, htParam["Vendor"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Remark", System.Data.SqlDbType.NVarChar, 4000, System.Data.ParameterDirection.Input, htParam["Remark"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@UTRNo", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, htParam["UTR"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@TotalAmount", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, htParam["TotalAmount"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@AddedBy", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, htParam["AddedBy"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ReturnValue", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.ReturnValue, null);
            SQLHelper.ExecuteDataTableCmd_VendorBilling(cmd);
            int ReturnValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
            return ReturnValue;
        }
        public int UpdateClientInvoice(Hashtable htParam)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_UpdateClientInvoice");//usp_UpdateInvociePaymentDate
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, htParam["InvoiceID"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Company", System.Data.SqlDbType.NVarChar, 10, System.Data.ParameterDirection.Input, htParam["Company"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@InvType", System.Data.SqlDbType.NVarChar, 4000, System.Data.ParameterDirection.Input, htParam["InvType"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ClientInvNo", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, htParam["ClientInvNo"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ReturnValue", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.ReturnValue, null);
            SQLHelper.ExecuteDataTableCmd_VendorBilling(cmd);
            int ReturnValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
            return ReturnValue;
        }

        public string GetPassword(string Username)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetEmailPassword");
            SQLHelper.AddParamToSQLCmd(cmd, "@Username", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, Username);
            string Password = (string)SQLHelper.ExecuteScalarCmd(cmd);
            return Password;
        }
        //


        public System.Data.DataTable GetSciennaSummary(string Month, string Year, int EmployeeId)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetSciennaSummary");
            SQLHelper.AddParamToSQLCmd(cmd, "@Month", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Month);
            SQLHelper.AddParamToSQLCmd(cmd, "@Year", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Year);
            SQLHelper.AddParamToSQLCmd(cmd, "@EmployeeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, EmployeeId);
            System.Data.DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }
        public System.Data.DataTable GetDashbordSummary(string Month, string Year, int EmployeeId)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetPendingAP_IPS_Dashboard");
            SQLHelper.AddParamToSQLCmd(cmd, "@Month", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Month);
            SQLHelper.AddParamToSQLCmd(cmd, "@Year", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Year);
            SQLHelper.AddParamToSQLCmd(cmd, "@EmployeeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, EmployeeId);
            System.Data.DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }

        public System.Data.DataTable GetIPSPacerForVerify(int InvoiceID, string Month, string Year)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetIPSPacerDetails_ForVerify");
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, InvoiceID);
            SQLHelper.AddParamToSQLCmd(cmd, "@Month", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Month);
            SQLHelper.AddParamToSQLCmd(cmd, "@Year", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Year);
            System.Data.DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }

        public System.Data.DataTable GetIPSRemoteUWKEBForVerify(int InvoiceID, string Month, string Year)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetIPSRemoteUWKEB_ForVerify");
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, InvoiceID);
            SQLHelper.AddParamToSQLCmd(cmd, "@Month", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Month);
            SQLHelper.AddParamToSQLCmd(cmd, "@Year", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Year);
            System.Data.DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }

        public System.Data.DataTable GetIPSRemoteUWKBCForVerify(int InvoiceID, string Month, string Year)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetIPSRemoteUWKCB_ForVerify");
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, InvoiceID);
            SQLHelper.AddParamToSQLCmd(cmd, "@Month", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Month);
            SQLHelper.AddParamToSQLCmd(cmd, "@Year", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Year);
            System.Data.DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }

        public System.Data.DataTable GetIPSRemoteUW670ForVerify(int InvoiceID, string Month, string Year)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetIPSRemoteUW670_ForVerify");
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, InvoiceID);
            SQLHelper.AddParamToSQLCmd(cmd, "@Month", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Month);
            SQLHelper.AddParamToSQLCmd(cmd, "@Year", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Year);
            System.Data.DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }


        public System.Data.DataTable GetSummary(string Vendor, string Year, string Company)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetAllLoanLogics_MIS_IPS_Email");
            SQLHelper.AddParamToSQLCmd(cmd, "@InvType", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Vendor);
            SQLHelper.AddParamToSQLCmd(cmd, "@Company", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Company);
            SQLHelper.AddParamToSQLCmd(cmd, "@Type", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, "Other");
            System.Data.DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }

        public System.Data.DataTable GetInvoiceDetails()
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "USP_GetInvoiceDetails");
            System.Data.DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }
        //GetCompanySummary

        public System.Data.DataTable GetCompanySummary()
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "USP_GetCompanySummary");
            System.Data.DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }




        public System.Data.DataTable GetDetails(string Vendor, string Year, string Company)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetAllLoanLogics_MIS_IPS_Details_New");
            SQLHelper.AddParamToSQLCmd(cmd, "@InvType", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Vendor);
            SQLHelper.AddParamToSQLCmd(cmd, "@Company", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Company);
            SQLHelper.AddParamToSQLCmd(cmd, "@Type", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, "Other");
            System.Data.DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }

        public System.Data.DataSet GetLoadLoanLogic(string Vendor, string Year, string Company)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetAllLoanLogics_MIS_IPS_Details_Smart_Summary");
            SQLHelper.AddParamToSQLCmd(cmd, "@InvType", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Vendor);
            SQLHelper.AddParamToSQLCmd(cmd, "@Company", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, Company);
            SQLHelper.AddParamToSQLCmd(cmd, "@Type", System.Data.SqlDbType.NVarChar, 30, System.Data.ParameterDirection.Input, "Project");
            System.Data.DataSet ds = SQLHelper.ExecuteDataSetCmd(cmd);
            return ds;
        }


        //****************Arti Changes (YTU)*************************//

        public DataTable GetInvoiceFilePath(string invoiceId, string Company)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetInvoiceFilePath_YTU");
            SQLHelper.AddParamToSQLCmd(cmd, "@InvoiceID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, invoiceId);
            SQLHelper.AddParamToSQLCmd(cmd, "@Company", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, Company);

            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }
        public int InsertVendorAccountDetails(Hashtable htParam)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_InsertVendorPayToDetails_YTU");
            SQLHelper.AddParamToSQLCmd(cmd, "@Company", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, htParam["VendorCompany"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Vendor", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, htParam["VendorAccount"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@PayTo", System.Data.SqlDbType.NVarChar, 500, System.Data.ParameterDirection.Input, htParam["VendorAccount_PayTo"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@AccNoLastFourDigit", System.Data.SqlDbType.NChar, 10, System.Data.ParameterDirection.Input, htParam["VendorAccount_AcNo"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@EmailID", System.Data.SqlDbType.NVarChar, -1, System.Data.ParameterDirection.Input, htParam["VendorAccount_EmailId"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@AddedBy", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, htParam["AddedBy"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ReturnValue", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.ReturnValue, null);
            SQLHelper.ExecuteNonQueryCmd(cmd);
            int ReturnValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
            return ReturnValue;
        }

        public DataTable GetAllVendorAccountDetails_Canopy(string Company)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetAllVendorAccountDetails_Canopy");
            SQLHelper.AddParamToSQLCmd(cmd, "@Company", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, Company);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }
        public DataTable GetAllVendorAccountDetails_IPS(string Company)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetAllVendorAccountDetails_IPS");
            SQLHelper.AddParamToSQLCmd(cmd, "@Company", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, Company);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }

        public int ApproveVendorAllAccountDetails(Hashtable htParam)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_ApproveVendorAccountDetails_YTU");
            SQLHelper.AddParamToSQLCmd(cmd, "@VendorPayToID", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, htParam["VendorPayToID"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ApprovedRemark", System.Data.SqlDbType.NVarChar, 1000, System.Data.ParameterDirection.Input, htParam["VerifyRemark"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ApprovedBy", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, htParam["VerifyBy"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ReturnValue", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.ReturnValue, null);
            SQLHelper.ExecuteNonQueryCmd(cmd);
            int ReturnValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
            return ReturnValue;
        }

        public System.Data.DataTable GetAllRegisteredVendors()
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "GetAllRegisteredVendors_YTU");
            System.Data.DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }



        public int InsertVendorRegistration(Hashtable htParam)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_InsertVendorRegistration_YTU");
            SQLHelper.AddParamToSQLCmd(cmd, "@VendorName", System.Data.SqlDbType.VarChar, 150, System.Data.ParameterDirection.Input, htParam["VendorName"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@CompanyName", System.Data.SqlDbType.VarChar, 150, System.Data.ParameterDirection.Input, htParam["CompanyName"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ContactPerson", System.Data.SqlDbType.VarChar, 100, System.Data.ParameterDirection.Input, htParam["ContactPerson"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ContactNo", System.Data.SqlDbType.VarChar, 20, System.Data.ParameterDirection.Input, htParam["ContactNo"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@EmailID", System.Data.SqlDbType.VarChar, 100, System.Data.ParameterDirection.Input, htParam["EmailID"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@WebsiteURL", System.Data.SqlDbType.VarChar, 255, System.Data.ParameterDirection.Input, htParam["WebsiteURL"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Address", System.Data.SqlDbType.VarChar, -1, System.Data.ParameterDirection.Input, htParam["Address"]); 
            SQLHelper.AddParamToSQLCmd(cmd, "@PayToVendorName", System.Data.SqlDbType.VarChar, 150, System.Data.ParameterDirection.Input, htParam["PayToVendorName"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@AccountNumber", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, htParam["AccountNumber"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Remark", System.Data.SqlDbType.VarChar, -1, System.Data.ParameterDirection.Input, htParam["Remark"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@VendorID", System.Data.SqlDbType.VarChar, 150, System.Data.ParameterDirection.Input, htParam["vendorId"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@AddedBy", System.Data.SqlDbType.VarChar, 100, System.Data.ParameterDirection.Input, htParam["AddedBy"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@VendorType", System.Data.SqlDbType.VarChar, 150, System.Data.ParameterDirection.Input, htParam["VendorType"]);

            SQLHelper.AddParamToSQLCmd(cmd, "@NewVendorID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Output, null);
            SQLHelper.ExecuteNonQueryCmd(cmd);
            int returnValue = Convert.ToInt32(cmd.Parameters["@NewVendorID"].Value);
            return returnValue;
        }


        public int AddVendorOtherInfo(Hashtable htParam)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_UpdateVendorOtherInfo_YTU");
            SQLHelper.AddParamToSQLCmd(cmd, "@VendorRegistrationID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, htParam["vendorRegistrationID"]); 
            SQLHelper.AddParamToSQLCmd(cmd, "@StartDate", System.Data.SqlDbType.Date, 0, System.Data.ParameterDirection.Input, htParam["StartDate"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@RateRevisionDate", System.Data.SqlDbType.Date, 0, System.Data.ParameterDirection.Input, htParam["RateRevisionDate"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@WorkDocument", System.Data.SqlDbType.VarChar, -1, System.Data.ParameterDirection.Input, htParam["WorkDocument"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ScopeOfWork", System.Data.SqlDbType.NVarChar, -1, System.Data.ParameterDirection.Input, htParam["ScopeOfWork"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@OtherInfoRemark", System.Data.SqlDbType.NVarChar, -1, System.Data.ParameterDirection.Input, htParam["OtherInfoRemark"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@IsNDASigned", System.Data.SqlDbType.VarChar, 10, System.Data.ParameterDirection.Input, htParam["IsNDASigned"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@AgreementDate", System.Data.SqlDbType.VarChar, 100, System.Data.ParameterDirection.Input, htParam["AgreementDate"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ExpiryDate", System.Data.SqlDbType.VarChar, 100, System.Data.ParameterDirection.Input, htParam["ExpiryDate"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@SignedByClient", System.Data.SqlDbType.VarChar, 100, System.Data.ParameterDirection.Input, htParam["SignedByClient"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@SignedByInfinity", System.Data.SqlDbType.VarChar, 100, System.Data.ParameterDirection.Input, htParam["SignedByInfinity"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@AgreementCopy", System.Data.SqlDbType.VarChar, 255, System.Data.ParameterDirection.Input, htParam["AgreementCopy"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@UpdatedID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Output, null);
            SQLHelper.ExecuteNonQueryCmd(cmd);
            int returnValue = Convert.ToInt32(cmd.Parameters["@UpdatedID"].Value);
            return returnValue;
        }

        public int InsertVendorRate(Hashtable htParam)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_InsertVendorRate_YTU");
            SQLHelper.AddParamToSQLCmd(cmd, "@VendorRegistrationID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, htParam["VendorRegistrationID"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@FromDate", System.Data.SqlDbType.Date, 0, System.Data.ParameterDirection.Input, htParam["FromDate"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@RateType", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, htParam["RateType"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Rate", System.Data.SqlDbType.Decimal, 18, System.Data.ParameterDirection.Input, htParam["Rate"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Process", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, htParam["Process"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@BillingCycle", System.Data.SqlDbType.VarChar, 100, System.Data.ParameterDirection.Input, htParam["BillingCycle"]);

            SQLHelper.AddParamToSQLCmd(cmd, "@AddedBy", System.Data.SqlDbType.VarChar, 100, System.Data.ParameterDirection.Input, htParam["AddedBy"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@rateID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, htParam["rateID"]);

            SQLHelper.AddParamToSQLCmd(cmd, "@newRateId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Output, null);
            SQLHelper.ExecuteNonQueryCmd(cmd);
            int returnValue = Convert.ToInt32(cmd.Parameters["@newRateId"].Value);
            return returnValue;
        }


        public static DataTable GetVendors()
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetVendor_YTU");
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }

        public DataTable GetVendorDetailsByID(int vendorRegistrationID)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetVendorDetailsByID_YTU");
            SQLHelper.AddParamToSQLCmd(cmd, "@vendorRegistrationID", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, vendorRegistrationID);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }

        public static DataTable GetRateConfiguration(int VendorId)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetRateConfiguration_YTU");
            SQLHelper.AddParamToSQLCmd(cmd, "@VendorId", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, VendorId);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }

        public int ApproveVendorRate(Hashtable htParam)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_ApprovedVendorRate_YTU");
            SQLHelper.AddParamToSQLCmd(cmd, "@VendorRegistrationID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, htParam["VendorRegistrationID"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@rateID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, htParam["rateID"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@AddedBy", System.Data.SqlDbType.Int,4, System.Data.ParameterDirection.Input, htParam["AddedBy"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@newRateId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Output, null);
            SQLHelper.ExecuteNonQueryCmd(cmd);
            int returnValue = Convert.ToInt32(cmd.Parameters["@newRateId"].Value);
            return returnValue;
        }

        public System.Data.DataTable GetVendorInfoForPopUp()
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetVendorInfoForPopUp_YTU");
            System.Data.DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }

        public System.Data.DataTable GetVendorRateRevisionDateInfoForPopup()
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetVendorRateRevisionDateInfoForPopup_YTU");
            System.Data.DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }

        public System.Data.DataTable GetVendorRateStatusInfoForPopUp()
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetVendorRateStatusInfoForPopUp_YTU");
            System.Data.DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }

        public int InsertVendorNotificationLog(Hashtable htParam)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_InsertVendorNotificationLog_YTU");
            // ActionByUserID la NVarChar kela karan table madhe varchar(100) ahe
            SQLHelper.AddParamToSQLCmd(cmd, "@ActionByUserID", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, htParam["ActionByUserID"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@VendorId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, htParam["VendorId"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@NotificationType", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, htParam["notificationType"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@IsRead", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, htParam["IsRead"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@newReadId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Output, null);

            SQLHelper.ExecuteNonQueryCmd(cmd);

            object outVal = cmd.Parameters["@newReadId"].Value;
            int returnValue = (outVal != null && outVal != DBNull.Value) ? Convert.ToInt32(outVal) : 0;
            return returnValue;
        }
    }
}
