using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vendor_Portal.Vendor
{
    public partial class ApproveVendorAccountDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static int ApproveVendorAllAccountDetails(int VendorPayToID, string Remark)
        {
            int returnvalue = 0;


            Hashtable htParam = new Hashtable();
            htParam.Add("VendorPayToID", VendorPayToID);
            htParam.Add("VerifyRemark", Remark);
            htParam.Add("VerifyBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));

            returnvalue = new bllInvoice().ApproveVendorAllAccountDetails(htParam);
            return returnvalue;
        }
    }
}