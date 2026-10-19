using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vendor_Portal.Vendor
{
    public partial class Vendor : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int LoginID = int.Parse(HttpContext.Current.User.Identity.Name.ToString());

            //Master

            if (LoginID == 5 || LoginID == 12 || LoginID == 235 || LoginID == 8128 || LoginID == 9961 || LoginID == 6959)
            {
                li_MasterSection.Visible = true;
                li_UpdateClientInvoice.Visible = true;
                li_UpdatePaidDate.Visible = true;
                li_UpdatePaidDateOther.Visible = true;
                li_CanopyMGTReport.Visible = true;
                li_IPSMGTReport.Visible = true;
                li2.Visible = true;
                li3.Visible = true;
                li1.Visible = true;
            }

            else if (LoginID == 9858 || LoginID == 9852)  //IT
            {
                li_MasterSection.Visible = false;
                li_UpdateClientInvoice.Visible = false;
                li_UpdatePaidDate.Visible = false;
                li_UpdatePaidDateOther.Visible = false;
                li_CanopyMGTReport.Visible = false;
                li_IPSMGTReport.Visible = false;
                li1.Visible = true;
                li2.Visible = true;
                li3.Visible = false;
            }

            else
            {
                li_MasterSection.Visible = false;
                li_UpdateClientInvoice.Visible = false;
                li_UpdatePaidDate.Visible = false;
                li_UpdatePaidDateOther.Visible = false;
                li_CanopyMGTReport.Visible = false;
                li_IPSMGTReport.Visible = false;
                li1.Visible = true;
                li2.Visible = false;
                li3.Visible = false;
            }
                        
            }
        }
}