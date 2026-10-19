using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Vendor_Portal.App_Code.DAL;

namespace Vendor_Portal.App_Code.BLL
{
    public class bllInvoice
    {
        dalInvoice dalInvoice = new dalInvoice();

        #region Infinity
        public DataTable GetInfinityInvoiceDetails(int EmployeeId)
        {
            return dalInvoice.GetInfinityInvoiceDetails(EmployeeId);
        }
        public int Insert_Infinity_InvoiceDetails(Hashtable htParam)
        {
            return dalInvoice.Insert_Infinity_InvoiceDetails(htParam);
        }

        public int UpdateInvoiceReconciliation_Canopy(Hashtable htParam)
        {
            return dalInvoice.UpdateInvoiceReconciliation_Canopy(htParam);
        }

        public int UpdateInvoiceReconciliation_IPS(Hashtable htParam)
        {
            return dalInvoice.UpdateInvoiceReconciliation(htParam);
        }
        public int InsertInvoiceDetails(Hashtable htParam)
        {
            return dalInvoice.InsertInvoiceDetails(htParam);
        }
        public int DeleteScienna(string Month, string Year)
        {
            return dalInvoice.DeleteScienna(Month, Year);
        }
        public int DeleteSciennaLabour(string Month, string Year)
        {
            return dalInvoice.DeleteSciennaLabour(Month, Year);
        }
        public DataTable GetSciennaDetailsAfterImport(int InvoiceID, string Month, string Year)
        {
            return dalInvoice.GetSciennaDetailsAfterImport(InvoiceID, Month, Year);
        }
        public DataTable GetSciennaLaborDetailsAfterImport(int InvoiceID, string Month, string Year)
        {
            return dalInvoice.GetSciennaLaborDetailsAfterImport(InvoiceID, Month, Year);
        }
        public DataTable GetSciennaLoanDetailsAfterImport()
        {
            return dalInvoice.GetSciennaLoanDetailsAfterImport();
        }
        public DataTable GetInfinityInvoiceDetails_Report(int EmployeeId)
        {
            return dalInvoice.GetInfinityInvoiceDetails_Report(EmployeeId);
        }

        public DataTable GetInfinityInvoiceDetailsPayment_Report(int EmployeeId)
        {
            return dalInvoice.GetInfinityInvoiceDetailsPayment_Report(EmployeeId);
        }

        public DataTable GetInfinityInvoiceDetailsPaymentCanopy_Report(int EmployeeId)
        {
            return dalInvoice.GetInfinityInvoiceDetailsPaymentCanopy_Report(EmployeeId);
        }



        public DataTable GetInfinityInvoiceDetailsCanopy_Report(int EmployeeId)
        {
            return dalInvoice.GetInfinityInvoiceDetailsCanopy_Report(EmployeeId);
        }
        public DataTable GetAllInvoiceLoansDetailsForReport(int InvoiceID, string InvType)
        {
            return dalInvoice.GetAllInvoiceLoansDetailsForReport(InvoiceID, InvType);
        }

        public DataTable GetAllInvoiceForUpdatePaidDateAsPerPayees(int InvoiceID, string InvType)
        {
            return dalInvoice.GetAllInvoiceForUpdatePaidDateAsPerPayees(InvoiceID, InvType);
        }

        public DataTable GetLauraMacBillingWorkOrdersDetails()
        {
            return dalInvoice.GetLauraMacBillingWorkOrdersDetails();
        }
        public DataTable GetAllStewartIA_Summary()
        {
            return dalInvoice.GetAllStewartIA_Summary();
        }
        public DataTable GetAllLoanLogic_Summary()
        {
            return dalInvoice.GetAllLoanLogic_Summary();
        }

        public DataTable GetDataForMarginCanopy_LoanWise(string FromDate, string ToDate)
        {
            return dalInvoice.GetDataForMarginCanopy_LoanWise(FromDate, ToDate);
        }

        public DataTable GetDataMonthlyVendorData_Canopy(string Year)
        {
            return dalInvoice.GetDataMonthlyVendorData_Canopy(Year);
        }

        public DataTable GetDataMonthlyVendorData_IPS(string Year)
        {
            return dalInvoice.GetDataMonthlyVendorData_IPS(Year);
        }

        public DataTable GetAllLoanLogic_Details()
        {
            return dalInvoice.GetAllLoanLogic_Details();
        }
        public DataTable GetAllCanopyMagnaRecords()
        {
            return dalInvoice.GetAllCanopyMagnaRecords();
        }

        public DataTable GetAllAttorneyDetails_InvoiceSummary()
        {
            return dalInvoice.GetAllAttorneyDetails_InvoiceSummary();
        }

        public DataTable GetAllAttorneyDetails()
        {
            return dalInvoice.GetAllAttorneyDetails();
        }

        public DataTable GetAllAttorneyDetails_BillingSummary()
        {
            return dalInvoice.GetAllAttorneyDetails_BillingSummary();

        }

        public DataTable GetDataForMargin_DealWise(string FromDate, string ToDate)
        {
            return dalInvoice.GetDataForMargin_DealWise(FromDate, ToDate);
        }

        public DataTable GetDataForMargin_ProjectWise(string FromDate, string ToDate)
        {
            return dalInvoice.GetDataForMargin_ProjectWise(FromDate, ToDate);
        }

        public DataTable GetDataForMargin_TypeWise(string FromDate, string ToDate)
        {
            return dalInvoice.GetDataForMargin_TypeWise(FromDate, ToDate);
        }

        public DataTable GetDataForMargin_LoanWise(string FromDate, string ToDate)
        {
            return dalInvoice.GetDataForMargin_LoanWise(FromDate, ToDate);
        }

        public DataSet GetProjectHeadersWithData_DS(string FormDate, string ToDate)
        {
            return dalInvoice.GetProjectHeadersWithData_DS(FormDate, ToDate);
        }


        public DataTable GetComplainceEaseLoanwiseDetails(string FormDate, string ToDate)
        {
            return dalInvoice.GetComplainceEaseLoanwiseDetails(FormDate, ToDate);
        }

        public DataTable GetComplainceEaseDealwiseDetails(string FormDate, string ToDate)
        {
            return dalInvoice.GetComplainceEaseDealwiseDetails(FormDate, ToDate);
        }


        public DataTable GetComplainceEaseProjectwiseDetails(string FormDate, string ToDate)
        {
            return dalInvoice.GetComplainceEaseProjectwiseDetails(FormDate, ToDate);
        }

        public DataTable GetComplainceEaseTypewiseDetails(string FormDate, string ToDate)
        {
            return dalInvoice.GetComplainceEaseTypewiseDetails(FormDate, ToDate);
        }

        public DataTable GetComplainceEaseSummary(string FormDate, string ToDate)
        {
            return dalInvoice.GetComplainceEaseSummary(FormDate, ToDate);
        }
        public DataTable GetAllStewartIA_Details()
        {
            return dalInvoice.GetAllStewartIA_Details();
        }
        public DataTable GetLauraMacBillingScriptChargesDetails(string FormDate, string ToDate)
        {
            return dalInvoice.GetLauraMacBillingScriptChargesDetails(FormDate, ToDate);
        }

        public DataTable GetLauraMacBillingLoanWiseDetails(string FormDate, string ToDate)
        {
            return dalInvoice.GetLauraMacBillingLoanWiseDetails(FormDate, ToDate);
        }

        public DataTable GetAllLauraMac()
        {
            return dalInvoice.GetAllLauraMac();
        }

        public DataTable GetInfinity_InvoiceDetails_Approval_Other(int EmployeeID, string Company, string InvType)
        {
            return dalInvoice.GetInfinity_InvoiceDetails_Approval_Other(EmployeeID, Company, InvType);
        }



        public DataTable GetTrackingSheetCreditData(string FromDate, string ToDate)
        {
            return dalInvoice.GetTrackingSheetCreditData(FromDate, ToDate);
        }

        public DataTable GetTrackingSheetServiceData(string FromDate, string ToDate)
        {
            return dalInvoice.GetTrackingSheetServiceData(FromDate, ToDate);
        }



        #endregion Infinity

        #region Canopy

        public int Insert_Infinity_InvoiceDetails_Canopy(Hashtable htParam)
        {
            return dalInvoice.Insert_Infinity_InvoiceDetails_Canopy(htParam);
        }
        public DataTable GetCanopyRemoteUWForVerify(int InvoiceID, string Month, string Year)
        {
            return dalInvoice.GetCanopyRemoteUWForVerify(InvoiceID, Month, Year);
        }
        public int DeleteStewartLoan(string Month, string Year)
        {
            return dalInvoice.DeleteStewartLoan(Month, Year);
        }
        public int InsertInvoiceDetails_Canopy(Hashtable htParam)
        {
            return dalInvoice.InsertInvoiceDetails_Canopy(htParam);
        }
        public DataTable GetStewartDetailsForVerify(int InvoiceID, string Month, string Year)
        {
            return dalInvoice.GetStewartDetailsForVerify(InvoiceID, Month, Year);
        }

        public DataTable GetComplianceEaseDetailsForVerify(int InvoiceID, string Month, string Year)
        {
            return dalInvoice.GetComplianceEaseDetailsForVerify(InvoiceID, Month, Year);
        }

        public DataTable GetAllAP670PaidInvoices()
        {
            return dalInvoice.GetAllAP670PaidInvoices();
        }
        public DataTable GetAllAP670UnPaidInvoices()
        {
            return dalInvoice.GetAllAP670UnPaidInvoices();
        }

        public DataTable GetAllCreditSoftPull_Details()
        {
            return dalInvoice.GetAllCreditSoftPull_Details();
        }
        public DataTable GetAllCreditSoftPull_Summary()
        {
            return dalInvoice.GetAllCreditSoftPull_Summary();
        }
        public DataTable GetLauraMacDetailsForVerify(int InvoiceID, string Month, string Year)
        {
            return dalInvoice.GetLauraMacAfterImport(InvoiceID, Month, Year);
        }
        public DataTable GetInfinityInvoiceDetails_Canopy(int EmployeeId)
        {
            return dalInvoice.GetInfinityInvoiceDetails_Canopy(EmployeeId);
        }


        public DataTable GetInfinityInvoiceDetails_IPS(int EmployeeId)
        {
            return dalInvoice.GetInfinityInvoiceDetails_IPS(EmployeeId);
        }

        public DataTable GetInfinityInvoiceDetails_Approval_IPS(int EmployeeId)
        {
            return dalInvoice.GetInfinityInvoiceDetails_Approval_IPS(EmployeeId);
        }
        public DataTable GetInfinityInvoiceDetails_Approval_canopy(int EmployeeId)
        {
            return dalInvoice.GetInfinityInvoiceDetails_Approval_canopy(EmployeeId);
        }
        public DataTable GetInfinityInvoicesForUpdatePaidDate(int EmployeeId, string Company)
        {
            return dalInvoice.GetInfinityInvoicesForUpdatePaidDate(EmployeeId, Company);
        }
        public DataTable GetInvoicesForUpdateClientInvoiceNo(int EmployeeId, string Company)
        {
            return dalInvoice.GetInvoicesForUpdateClientInvoiceNo(EmployeeId, Company);
        }


        public DataTable GetInvoiceDetailsForUpdateDateByID_IPS(int InvoiceID)
        {
            return dalInvoice.GetInvoiceDetailsForUpdateDateByID_IPS(InvoiceID);
        }


        public DataTable GetAllInvoiceForUpdatePaidDateOtherPayee(int PayeeID, int InvoiceId)
        {
            return dalInvoice.GetAllInvoiceForUpdatePaidDateOtherPayee(PayeeID, InvoiceId);
        }

        public DataTable GetInvoiceDetailsForUpdateDateByOtherPayee_Canopy(int PayeeID, int InvoiceId)
        {
            return dalInvoice.GetInvoiceDetailsForUpdateDateByOtherPayee_Canopy(PayeeID, InvoiceId);
        }


        public DataTable GetInvoiceDetailsForUpdateDateByID_Canopy(int InvoiceID)
        {
            return dalInvoice.GetInvoiceDetailsForUpdateDateByID_Canopy(InvoiceID);
        }

        public DataTable GetAllPendingInvoice_Canopy(int InvoiceId)
        {
            return dalInvoice.GetAllPendingPaymentInvoices_Canopy(InvoiceId);
        }

        public DataTable GetAllInvoiceLoansDetailsReport(int InvoiceID, string InvType)
        {
            return dalInvoice.GetAllInvoiceLoansDetailsForReport(InvoiceID, InvType);
        }

        public DataTable GetAllPendingInvoice_IPS(int InvoiceId)
        {
            return dalInvoice.GetAllPendingPaymentInvoices(InvoiceId);
        }
        public DataTable GetAllInvoiceDetails(int InvoiceId)
        {
            return dalInvoice.GetAllInvoiceDetails(InvoiceId);
            
        }

        public DataTable GetStewartDataForReconcile(string Month, string Year)
        {
            return dalInvoice.GetStewartDataForReconcile(Month, Year);
        }

        public DataTable GetComplianceRecordForReconsile(int InvoiceID, string Month, string Year)
        {
            return dalInvoice.GetComplianceRecordForReconsile(InvoiceID, Month, Year);
        }

        public DataTable GetLauraMacDataForReconcile(int InvoiceID, string Month, string Year)
        {
            return dalInvoice.GetLauraMacDataForReconcile(InvoiceID, Month, Year);
        }
        public int VerifyLoanNo(Hashtable htParam)
        {
            return dalInvoice.VerifyLoanNo(htParam);
        }

        public int VerifyLoanNo_LauraMac(Hashtable htParam)
        {
            return dalInvoice.VerifyLauraMac(htParam);
        }

        public int VerifyCompliance(Hashtable htParam)
        {
            return dalInvoice.VerifyCompliance(htParam);
        }

        public int ReconcileLoan_Generalised(Hashtable htParam)
        {
            return dalInvoice.ReconcileLoan_Generalised(htParam);
        }

        public int InsertIntoReminderDate(Hashtable htParam)
        {
            return dalInvoice.InsertIntoReminderDate(htParam);
        }


        public int UpdateReminderDate(Hashtable htParam)
        {
            return dalInvoice.UpdateReminderDate(htParam);
        }

        public DataTable GetAllReminderDates()
        {
            return dalInvoice.GetAllReminderDates();
        }

        public DataTable GetVendorRecordForReconcile_Generilised(string VendorType, int InvoiceID, string Month, string Year)
        {
            return dalInvoice.GetVendorRecordForReconcile_Generilised(VendorType, InvoiceID, Month, Year);
        }

        public DataTable GetIPSRemoteUWLoanLogicsForVerify(int InvoiceID, string Month, string Year)
        {
            return dalInvoice.GetIPSRemoteUWLoanLogicsForVerifyint(InvoiceID, Month, Year);
        }

        public DataTable GetIPSRemoteUWLoanLogicsForVerify_IPS(int InvoiceID, string Month, string Year)
        {
            return dalInvoice.GetIPSRemoteUWLoanLogicsForVerifyint_IPS(InvoiceID, Month, Year);
        }

        public DataTable GetIPSRemoteUWLaminrForVerify(int InvoiceID, string Month, string Year)
        {
            return dalInvoice.GetIPSRemoteUWLaminrForVerify(InvoiceID, Month, Year);
        }
        
        public DataTable GetIPSRemoteUWAttorneyForVerify(int InvoiceID, string Month, string Year)
        {
            return dalInvoice.GetIPSRemoteUWLaminrForVerify(InvoiceID, Month, Year);
        }


        #endregion Canopy

        #region Cost Master


        public DataTable GetAllVendorAsPerCompany(string Company)
        {
            return dalInvoice.GetAllVendorAsPerCompany(Company);
        }

        public DataTable GetAllVendorRateConfiguration(string Company)
        {
            return dalInvoice.GetAllVendorRateConfiguration(Company);
        }

        public int InsertVendorRateConfigurationForbilling(Hashtable htParam)
        {
            return dalInvoice.InsertVendorRateConfigurationForbilling(htParam);
        }

        public DataTable GetAllVendorRateConfiguration_ForApproval(string Company)
        {
            return dalInvoice.GetAllVendorRateConfiguration_ForApproval(Company);
        }

        public int ApproveVendorRateConfigurastion(Hashtable htParam)
        {
            return dalInvoice.ApproveVendorRateConfigurastion(htParam);
        }
        #endregion

        public int UpdateRemark(Hashtable htParam)
        {
            return dalInvoice.UpdateRemark(htParam);
        }

        public int UpdatePaidDateOthers(Hashtable htParam)
        {
            return dalInvoice.UpdatePaidDateOthers(htParam);
        }

        public int UpdateClientInvoice(Hashtable htParam)
        {
            return dalInvoice.UpdateClientInvoice(htParam);
        }

        public string GetPassword(string Username)
        {
            return dalInvoice.GetPassword(Username);
        }

        public DataTable GetDashborddata(string Month, string Year,int EmployeeId)
        {
            return dalInvoice.GetDashbordSummary(Month, Year, EmployeeId);
        }

        public DataTable GetSciennaSummary(string Month, string Year, int EmployeeId)
        {
            return dalInvoice.GetSciennaSummary(Month, Year, EmployeeId);
        }
        public DataTable GetIPSRemoteUW670ForVerify(int InvoiceID, string Month, string Year)
        {
            return dalInvoice.GetIPSRemoteUW670ForVerify(InvoiceID, Month, Year);
        }
        public DataTable GetIPSRemoteUWKCBForVerify(int InvoiceID, string Month, string Year)
        {
            return dalInvoice.GetIPSRemoteUWKBCForVerify(InvoiceID, Month, Year);
        }

        public DataTable GetIPSRemoteUWPacerForVerify(int InvoiceID, string Month, string Year)
        {
            return dalInvoice.GetIPSPacerForVerify(InvoiceID, Month, Year);
        }
        public DataTable GetIPSRemoteUWKEBForVerify(int InvoiceID, string Month, string Year)
        {
            return dalInvoice.GetIPSRemoteUWKEBForVerify(InvoiceID, Month, Year);
        }



        public DataTable GetSummary(string Month, string Year, string Company)
        {
            return dalInvoice.GetSummary(Month, Year, Company);
        }

        public DataTable GetDetails(string Month, string Year, string Company)
        {
            return dalInvoice.GetDetails(Month, Year, Company);
        }
        public DataSet GetLoadLoanLogic(string Month, string Year, string Company)
        {
            return dalInvoice.GetLoadLoanLogic(Month, Year, Company);
        }
        public DataTable GetInvoiceDetails()
        {
            return dalInvoice.GetInvoiceDetails();
        }
   
        public DataTable GetCompanySummary()
        {
            return dalInvoice.GetCompanySummary();
        }

        //****************Arti Changes (YTU)*************************//

        public DataTable GetInvoiceFilePath(string invoiceId, string Company)
        {
            return dalInvoice.GetInvoiceFilePath(invoiceId, Company);
        }

    }




}