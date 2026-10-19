using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vendor_Portal
{
    public partial class MIS : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            int LoginID = int.Parse(HttpContext.Current.User.Identity.Name.ToString());
            if (LoginID == 5 || LoginID == 12 || LoginID == 235 || LoginID == 8128)
            {
                li_MasterSection.Visible = true;
                li_UpdateClientInvoice.Visible = true;
                li_UpdatePaidDate.Visible = true;
                li_UpdatePaidDateOther.Visible = true;
                li_CanopyMGTReport.Visible = true;
                li_IPSMGTReport.Visible = true;
            }

            else if (LoginID == 291) //PPL
            {
               // li_InvApproval.Visible = true;
                //li_AddInv.Visible = false;
                //li_InvoiceSection.Visible = false;
                li_MasterSection.Visible = false;
                li_RemoteUW.Visible = false;
               // li_OtherInvoices.Visible = false;
               // li_InvReconci.Visible = false;
                li_IPSReport.Visible = false;
                li_CanopyReport.Visible = false;
               // li_ERPExcelFormat.Visible = false;
                li_UpdateClientInvoice.Visible = false;
                li_UpdatePaidDate.Visible = false;
                li_UpdatePaidDateOther.Visible = false;
                li_CanopyMGTReport.Visible = false;
                li_IPSMGTReport.Visible = false;
                //li_AddInvSales.Visible = false;
                //li_AddInvSales.Visible = false;




            }

            else if (LoginID == 9771) //GYN
            {
               // li_AddInv.Visible = true;
               // li_InvApproval.Visible = false;
               // li_InvoiceSection.Visible = false;
                li_MasterSection.Visible = false;
                li_RemoteUW.Visible = false;
                //li_OtherInvoices.Visible = false;
                //li_InvReconci.Visible = false;
                li_IPSReport.Visible = false;
                li_CanopyReport.Visible = false;
              //  li_ERPExcelFormat.Visible = false;
                li_UpdateClientInvoice.Visible = false;
                li_UpdatePaidDate.Visible = false;
                li_UpdatePaidDateOther.Visible = false;
                li_CanopyMGTReport.Visible = false;
                li_IPSMGTReport.Visible = false;
                //li_AddInvSales.Visible = false;
                //li_AddInvSales.Visible = true;
            }

            else if (LoginID == 9251) //MPV
            {
                li_UpdateClientInvoice.Visible = true;
                li_UpdatePaidDate.Visible = true;
                li_UpdatePaidDateOther.Visible = true;

                //li_AddInv.Visible = false;
                //li_InvApproval.Visible = false;
                //li_InvoiceSection.Visible = false;
                li_MasterSection.Visible = false;
                li_RemoteUW.Visible = false;
               // li_OtherInvoices.Visible = false;
               // li_InvReconci.Visible = false;
                li_IPSReport.Visible = false;
                li_CanopyReport.Visible = false;
               // li_ERPExcelFormat.Visible = false;
                li_CanopyMGTReport.Visible = false;
                li_IPSMGTReport.Visible = false;
            }

            else if (LoginID == 394 || LoginID == 9858 || LoginID == 9852) //IT
            {
               // li_AddInv.Visible = true;
               // li_InvApproval.Visible = true;
               // li_InvoiceSection.Visible = true;
                li_MasterSection.Visible = false;
                li_RemoteUW.Visible = false;
               // li_OtherInvoices.Visible = false;
               // li_InvReconci.Visible = false;
                li_IPSReport.Visible = false;
                li_CanopyReport.Visible = false;
              //  li_ERPExcelFormat.Visible = false;

                li_UpdateClientInvoice.Visible = false;
                li_UpdatePaidDate.Visible = false;
                li_UpdatePaidDateOther.Visible = false;
                li_CanopyMGTReport.Visible = false;
                li_IPSMGTReport.Visible = false;
            }

            else if (LoginID == 9961)
            {
                //li_InvoiceSection.Visible = true;
               // li_AddInv.Visible = true;
              //  li_InvApproval.Visible = true;
                li_MasterSection.Visible = false;
                li_RemoteUW.Visible = false;
                //li_OtherInvoices.Visible = true;
                //li_InvReconci.Visible = true;
                li_IPSReport.Visible = false;
                li_CanopyReport.Visible = true;
               // li_ERPExcelFormat.Visible = false;
                li_UpdateClientInvoice.Visible = false;
                li_UpdatePaidDate.Visible = false;
                li_UpdatePaidDateOther.Visible = false;
                li_CanopyMGTReport.Visible = true;
                li_IPSMGTReport.Visible = false;
            }

            else if (LoginID == 6959 || LoginID == 292 || LoginID == 277 || LoginID == 216)  // Canopy for 
            {
               // li_InvoiceSection.Visible = true;
               // li_AddInv.Visible = true;
               // li_InvApproval.Visible = true;
                li_MasterSection.Visible = false;
                li_RemoteUW.Visible = true;
              //  li_OtherInvoices.Visible = true;
                //li_InvReconci.Visible = true;
                li_IPSReport.Visible = false;
                li_CanopyReport.Visible = false;
               // li_ERPExcelFormat.Visible = true;
                li_UpdateClientInvoice.Visible = false;
                li_UpdatePaidDate.Visible = false;
                li_UpdatePaidDateOther.Visible = false;
                li_CanopyMGTReport.Visible = false;
                li_IPSMGTReport.Visible = false;
            }

            else
            {

               // li_AddInv.Visible = false;
               // li_InvApproval.Visible = false;
               // li_InvoiceSection.Visible = false;
                li_MasterSection.Visible = false;
                li_RemoteUW.Visible = false;
               // li_OtherInvoices.Visible = false;
               // li_InvReconci.Visible = false;
                li_IPSReport.Visible = false;
                li_CanopyReport.Visible = false;
               // li_ERPExcelFormat.Visible = false;
                li_UpdateClientInvoice.Visible = false;
                li_UpdatePaidDate.Visible = false;
                li_UpdatePaidDateOther.Visible = false;
                li_CanopyMGTReport.Visible = false;
                li_IPSMGTReport.Visible = false;
            }


        }
    }
}