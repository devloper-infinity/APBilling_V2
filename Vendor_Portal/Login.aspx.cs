using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Security.Cryptography;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vendor_Portal.App_Code.BLL;
using Vendor_Portal.App_Code.EL;

namespace Vendor_Portal
{
    public partial class Login : System.Web.UI.Page
    {
        bllLogin bllLogin = new bllLogin();
        string Password = "";
        public string localIP;
        private string returnUrl
        {
            get
            {
                if (ViewState["returnUrl"] == null)
                    ViewState["returnUrl"] = "";
                return (string)ViewState["returnUrl"];
            }
            set
            {
                ViewState["returnUrl"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var remoteIpAddress = Request.UserHostAddress;
            try
            {
                returnUrl = Request.QueryString["ReturnUrl"];
            }
            catch { }

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (string.IsNullOrEmpty(returnUrl))
                {
                    string restFlag = Convert.ToString(Session["resetFlg"]);

                    if (restFlag == "False" || restFlag == "false")
                    {
                        if (HttpContext.Current.User.IsInRole("Admin"))
                        {
                            
                            Session["User"] = "Admin";

                            string userName = HttpContext.Current.User.Identity.Name.ToLower();

                            if (userName == "12" || userName == "9961" || userName == "8128" || userName == "6959" || userName == "5")
                            {
                                Response.Redirect("~/Report/VendorReportCanopy.aspx");
                            }
                            else
                            {
                                Response.Redirect("~/Vendor/Dashboard.aspx");
                            }

                            //if (HttpContext.Current.User.IsInRole("Admin"))
                            //    Response.Redirect("~/Vendor/Dashboard.aspx");
                        }
                    }
                    else
                    {
                        if (HttpContext.Current.User.IsInRole("Admin"))
                        {
                            string userName = HttpContext.Current.User.Identity.Name.ToLower();

                            if (userName == "12" || userName == "9961" || userName == "8128" || userName == "6959" || userName == "5")
                            {
                                Response.Redirect("~/Report/VendorReportCanopy.aspx");
                            }
                            else
                            {
                                Response.Redirect("~/Vendor/Dashboard.aspx");
                            }

                            //if (HttpContext.Current.User.IsInRole("Admin"))
                            //    Response.Redirect("~/Vendor/Dashboard.aspx");
                        }

                        else
                        {
                            FormsAuthentication.SignOut();
                            Response.Redirect("~/Logout.aspx");
                        }
                    }
                }
                Response.Redirect(returnUrl);
            }


        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {

                string userID = Request.Form["login_username"];
                string pwd = Request.Form["login_password"];


                if (chkRemember.Checked == true)
                {
                    Response.Cookies["userid"].Value = userID;
                    Response.Cookies["pwd"].Value = pwd;
                    Response.Cookies["userid"].Expires = DateTime.Now.AddMinutes(30);
                    Response.Cookies["pwd"].Expires = DateTime.Now.AddMinutes(30);
                }

                else
                {
                    Response.Cookies["userid"].Expires = DateTime.Now.AddMinutes(-1);

                    Response.Cookies["pwd"].Expires = DateTime.Now.AddMinutes(-1);
                }

                //********** Block User Login **********//
                DataTable dt = bllLogin.BlockUserLogin(userID);
                string encPassword = bllLogin.Encrypt(pwd);
                int ReturnValue = 0;

                int ReturnValue2 = bllLogin.ValidateUser(Filter.SQLInjectionFilter(userID), Filter.SQLInjectionFilter(encPassword));
                if (ReturnValue2 == 0)
                {
                    ReturnValue = bllLogin.ValidateUser(Filter.SQLInjectionFilter(userID), Filter.SQLInjectionFilter(pwd));
                }
                else
                {
                    ReturnValue = ReturnValue2;
                }
                //ReturnValue = bllLogin.ValidateUser(Filter.SQLInjectionFilter(userID), Filter.SQLInjectionFilter(pwd));

                if (ReturnValue == -1)
                {
                    dvError.Style.Add("display", "");
                    dvError.Attributes.Add("class", "alert alert-danger background-danger");
                    dvError.InnerHtml = "User does not exists";
                }
                else if (ReturnValue == 0)
                {
                    dvError.Style.Add("display", "");
                    dvError.Attributes.Add("class", "alert alert-danger background-danger");
                    dvError.InnerHtml = "Invalid Password";
                }

                else if (dt.Rows.Count > 0)
                {
                    dvError.Style.Add("display", "");
                    dvError.Attributes.Add("class", "alert alert-danger background-danger");
                    dvError.InnerHtml = "<b>Your login has been blocked. <br/>Please contact your reporting manager.</b>";
                }
                else
                {
                    DataTable usr = bllLogin.GetUserById(ReturnValue, Filter.SQLInjectionFilter(userID), Filter.SQLInjectionFilter(encPassword));
                    if (usr.Rows.Count <= 0)
                        usr = bllLogin.GetUserById(ReturnValue, Filter.SQLInjectionFilter(userID), Filter.SQLInjectionFilter(pwd));
                    if (usr.Rows.Count == 0)
                    {
                        ShowError("Unable to load the user profile.");
                        return;
                    }

                    Session["PendingMfaEmployeeId"] = Convert.ToString(usr.Rows[0]["EmployeeId"]);
                    Session["PendingMfaRole"] = Convert.ToString(usr.Rows[0]["Role"]);
                    Session["PendingMfaRemember"] = chkRemember.Checked;
                    Session["PendingPasswordReset"] = pwd.IndexOf("INFINITY", StringComparison.OrdinalIgnoreCase) >= 0;

                    if (IsMfaEnabled())
                    {
                        pnlCredentials.Visible = false;
                        pnlMfa.Visible = true;
                        return;
                    }

                    CompleteAuthentication();
                }
            }
            catch (Exception)
            {
                ShowError("We could not sign you in. Please try again.");
            }
        }

        protected void btnVerifyMfa_Click(object sender, EventArgs e)
        {
            pnlCredentials.Visible = false;
            pnlMfa.Visible = true;

            string code = Request.Form["mfa_code"];
            int attempts = Convert.ToInt32(Session["MfaAttempts"] ?? 0);
            if (attempts >= 5)
            {
                ClearPendingAuthentication();
                pnlCredentials.Visible = true;
                pnlMfa.Visible = false;
                ShowError("Too many verification attempts. Please sign in again.");
                return;
            }

            Session["MfaAttempts"] = attempts + 1;
            string mfaSecret = Environment.GetEnvironmentVariable("AP_BILLING_MFA_SECRET") ?? ConfigurationManager.AppSettings["MfaSharedSecret"];
            if (!ValidateTotp(code, mfaSecret))
            {
                ShowError("The authentication code is invalid or has expired.");
                return;
            }

            CompleteAuthentication();
        }

        protected void btnCancelMfa_Click(object sender, EventArgs e)
        {
            ClearPendingAuthentication();
            pnlCredentials.Visible = true;
            pnlMfa.Visible = false;
        }

        private bool IsMfaEnabled()
        {
            bool enabled;
            return bool.TryParse(ConfigurationManager.AppSettings["MfaEnabled"], out enabled) && enabled;
        }

        private void CompleteAuthentication()
        {
            string employeeId = Convert.ToString(Session["PendingMfaEmployeeId"]);
            string role = Convert.ToString(Session["PendingMfaRole"]);
            bool remember = Convert.ToBoolean(Session["PendingMfaRemember"] ?? false);
            bool resetPassword = Convert.ToBoolean(Session["PendingPasswordReset"] ?? false);
            if (string.IsNullOrWhiteSpace(employeeId))
            {
                ShowError("Your sign-in request expired. Please sign in again.");
                pnlCredentials.Visible = true;
                pnlMfa.Visible = false;
                return;
            }

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, employeeId, DateTime.Now,
                DateTime.Now.AddMinutes(30), remember, role, FormsAuthentication.FormsCookiePath);
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            authCookie.HttpOnly = true;
            authCookie.Secure = Request.IsSecureConnection;
            if (ticket.IsPersistent) authCookie.Expires = ticket.Expiration;
            Response.Cookies.Add(authCookie);
            ClearPendingAuthentication();

            if (resetPassword) Response.Redirect("~/ResetPassword.aspx");
            Response.Redirect(string.IsNullOrEmpty(returnUrl) ? "~/Login.aspx" : "~/Login.aspx?ReturnUrl=" + Server.UrlEncode(returnUrl), true);
        }

        private void ClearPendingAuthentication()
        {
            Session.Remove("PendingMfaEmployeeId");
            Session.Remove("PendingMfaRole");
            Session.Remove("PendingMfaRemember");
            Session.Remove("PendingPasswordReset");
            Session.Remove("MfaAttempts");
        }

        private void ShowError(string message)
        {
            dvError.Style["display"] = "block";
            dvError.Attributes["class"] = "alert alert-danger";
            dvError.InnerText = message;
        }

        private static bool ValidateTotp(string code, string base32Secret)
        {
            if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(base32Secret)) return false;
            byte[] key;
            try { key = DecodeBase32(base32Secret); }
            catch (FormatException) { return false; }
            long counter = DateTimeOffset.UtcNow.ToUnixTimeSeconds() / 30;
            for (long offset = -1; offset <= 1; offset++)
                if (GenerateTotp(key, counter + offset) == code.Trim()) return true;
            return false;
        }

        private static string GenerateTotp(byte[] key, long counter)
        {
            byte[] data = BitConverter.GetBytes(counter);
            if (BitConverter.IsLittleEndian) Array.Reverse(data);
            using (HMACSHA1 hmac = new HMACSHA1(key))
            {
                byte[] hash = hmac.ComputeHash(data);
                int offset = hash[hash.Length - 1] & 0x0f;
                int binary = ((hash[offset] & 0x7f) << 24) | ((hash[offset + 1] & 0xff) << 16) |
                             ((hash[offset + 2] & 0xff) << 8) | (hash[offset + 3] & 0xff);
                return (binary % 1000000).ToString("D6");
            }
        }

        private static byte[] DecodeBase32(string value)
        {
            string input = value.Trim().Replace(" ", "").TrimEnd('=').ToUpperInvariant();
            byte[] output = new byte[input.Length * 5 / 8];
            int buffer = 0, bitsLeft = 0, index = 0;
            foreach (char c in input)
            {
                int val = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567".IndexOf(c);
                if (val < 0) throw new FormatException("Invalid Base32 value.");
                buffer = (buffer << 5) | val;
                bitsLeft += 5;
                if (bitsLeft >= 8) { output[index++] = (byte)(buffer >> (bitsLeft - 8)); bitsLeft -= 8; }
            }
            return output;
        }
    }
}
