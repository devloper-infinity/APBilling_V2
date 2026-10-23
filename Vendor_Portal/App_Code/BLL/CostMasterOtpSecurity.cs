using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Vendor_Portal.App_Code.DAL;

namespace Vendor_Portal.App_Code.BLL
{
    public static class CostMasterOtpSecurity
    {
        public const string ExpiredMessage = "Your Costing Master access has expired. Please verify again.";
        private const int OtpMinutes = 5, AccessMinutes = 15, MaxAttempts = 5, ResendSeconds = 60, MaxSendsPerHour = 5;

        public static int CurrentEmployeeId()
        {
            int id;
            if (HttpContext.Current == null || HttpContext.Current.User == null ||
                !HttpContext.Current.User.Identity.IsAuthenticated ||
                !int.TryParse(HttpContext.Current.User.Identity.Name, out id))
                throw new HttpException(401, "Authentication is required.");
            return id;
        }

        public static string SessionId()
        {
            if (HttpContext.Current.Session == null) throw new HttpException(401, "A valid session is required.");
            return HttpContext.Current.Session.SessionID;
        }

        public static string EnsureCsrfToken()
        {
            object existing = HttpContext.Current.Session["CostMasterCsrf"];
            if (existing != null) return existing.ToString();
            byte[] bytes = new byte[32];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create()) rng.GetBytes(bytes);
            string token = Convert.ToBase64String(bytes);
            HttpContext.Current.Session["CostMasterCsrf"] = token;
            return token;
        }

        public static void RequireCsrf()
        {
            string supplied = HttpContext.Current.Request.Headers["X-CostMaster-CSRF"];
            string expected = Convert.ToString(HttpContext.Current.Session["CostMasterCsrf"]);
            if (String.IsNullOrEmpty(expected) || !SlowEquals(Encoding.UTF8.GetBytes(expected), Encoding.UTF8.GetBytes(supplied ?? "")))
                throw new HttpException(403, "Invalid request token.");
        }

        public static DateTime? GetAuthorizationExpiryUtc(string securityArea = "COST_MASTER")
        {
            RequirePageAccess(securityArea);
            using (SqlConnection cn = new SqlConnection(SQLHelper.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(@"SELECT ExpiresUtc FROM dbo.CostMasterAuthorization
WHERE SecurityArea=@SecurityArea AND EmployeeID=@EmployeeID AND SessionID=@SessionID AND RevokedUtc IS NULL AND ExpiresUtc>SYSUTCDATETIME()", cn))
            {
                cmd.Parameters.Add("@SecurityArea", SqlDbType.NVarChar, 30).Value = securityArea;
                cmd.Parameters.Add("@EmployeeID", SqlDbType.BigInt).Value = CurrentEmployeeId();
                cmd.Parameters.Add("@SessionID", SqlDbType.NVarChar, 88).Value = SessionId();
                cn.Open(); object value = cmd.ExecuteScalar();
                return value == null || value == DBNull.Value ? (DateTime?)null : DateTime.SpecifyKind(Convert.ToDateTime(value), DateTimeKind.Utc);
            }
        }

        public static DateTime RequireAuthorization(string securityArea = "COST_MASTER")
        {
            DateTime? expiry = GetAuthorizationExpiryUtc(securityArea);
            if (!expiry.HasValue) throw new HttpException(403, ExpiredMessage);
            return expiry.Value;
        }

        public static bool HasPageAccess(string securityArea = "COST_MASTER")
        {
            using (SqlConnection cn = new SqlConnection(SQLHelper.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(@"SELECT COUNT(1) FROM dbo.CostMasterAccessMaster
WHERE SecurityArea=@SecurityArea AND EmployeeID=@EmployeeID AND IsActive=1", cn))
            {
                cmd.Parameters.Add("@SecurityArea", SqlDbType.NVarChar, 30).Value = securityArea;
                cmd.Parameters.Add("@EmployeeID", SqlDbType.BigInt).Value = CurrentEmployeeId();
                cn.Open(); return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        public static void RequirePageAccess(string securityArea = "COST_MASTER")
        {
            if (!HasPageAccess(securityArea)) throw new HttpException(403, "You are not authorized to access this page's data.");
        }

        public static string SendOtp(string employeeCode, string securityArea = "COST_MASTER")
        {
            RequireCsrf(); RequirePageAccess(securityArea);
            int employeeId = CurrentEmployeeId(); string sessionId = SessionId();
            string officialEmail;
            using (SqlConnection cn = new SqlConnection(SQLHelper.ConnectionString_ERP))
            using (SqlCommand cmd = new SqlCommand(@"SELECT OfficialEmailID FROM dbo.EmployeeInfo
WHERE EmployeeID=@EmployeeID AND Code=@Code AND ISNULL(IsDelete,0)=0", cn))
            {
                cmd.Parameters.Add("@EmployeeID", SqlDbType.BigInt).Value = employeeId;
                cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 50).Value = (employeeCode ?? "").Trim();
                cn.Open(); officialEmail = Convert.ToString(cmd.ExecuteScalar()).Trim();
            }
            if (String.IsNullOrWhiteSpace(officialEmail) || !IsValidEmail(officialEmail))
            {
                Audit(employeeId, sessionId, "OTP_REQUEST_REJECTED", false);
                throw new HttpException(400, "Employee Code does not match the signed-in employee or no official email is registered.");
            }

            DateTime now = DateTime.UtcNow;
            using (SqlConnection cn = new SqlConnection(SQLHelper.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(@"SELECT MAX(CreatedUtc), COUNT(*) FROM dbo.CostMasterOtpChallenge WITH (UPDLOCK,HOLDLOCK)
WHERE SecurityArea=@SecurityArea AND EmployeeID=@EmployeeID AND CreatedUtc>DATEADD(HOUR,-1,SYSUTCDATETIME())", cn))
            {
                cmd.Parameters.Add("@SecurityArea", SqlDbType.NVarChar, 30).Value = securityArea;
                cmd.Parameters.Add("@EmployeeID", SqlDbType.BigInt).Value = employeeId; cn.Open();
                using (SqlDataReader r = cmd.ExecuteReader()) { r.Read(); DateTime? last = r.IsDBNull(0) ? (DateTime?)null : r.GetDateTime(0); int count = r.GetInt32(1);
                    if ((last.HasValue && (now-last.Value).TotalSeconds<ResendSeconds) || count>=MaxSendsPerHour)
                        throw new HttpException(429, "Please wait before requesting another OTP."); }
            }

            string otp = SecureSixDigitCode(); byte[] salt = new byte[32];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create()) rng.GetBytes(salt);
            byte[] hash = HashOtp(otp, salt, employeeId, sessionId, securityArea);
            using (SqlConnection cn = new SqlConnection(SQLHelper.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(@"UPDATE dbo.CostMasterOtpChallenge SET ConsumedUtc=SYSUTCDATETIME() WHERE SecurityArea=@SecurityArea AND EmployeeID=@EmployeeID AND SessionID=@SessionID AND ConsumedUtc IS NULL;
INSERT dbo.CostMasterOtpChallenge(SecurityArea,EmployeeID,SessionID,OtpHash,OtpSalt,ExpiresUtc) VALUES(@SecurityArea,@EmployeeID,@SessionID,@Hash,@Salt,DATEADD(MINUTE,5,SYSUTCDATETIME()));", cn))
            {
                cmd.Parameters.Add("@SecurityArea", SqlDbType.NVarChar, 30).Value = securityArea;
                cmd.Parameters.Add("@EmployeeID", SqlDbType.BigInt).Value=employeeId; cmd.Parameters.Add("@SessionID",SqlDbType.NVarChar,88).Value=sessionId;
                cmd.Parameters.Add("@Hash",SqlDbType.VarBinary,32).Value=hash; cmd.Parameters.Add("@Salt",SqlDbType.VarBinary,32).Value=salt; cn.Open(); cmd.ExecuteNonQuery();
            }
            SendEmail(officialEmail, otp); Audit(employeeId,sessionId,"OTP_SENT",true);
            return "A verification code was sent to your registered official email.";
        }

        public static DateTime VerifyOtp(string otp, string securityArea = "COST_MASTER")
        {
            RequireCsrf(); RequirePageAccess(securityArea); int employeeId=CurrentEmployeeId(); string sessionId=SessionId();
            using (SqlConnection cn=new SqlConnection(SQLHelper.ConnectionString))
            {
                cn.Open(); using(SqlTransaction tx=cn.BeginTransaction(IsolationLevel.Serializable))
                using(SqlCommand cmd=new SqlCommand(@"SELECT TOP 1 ChallengeID,OtpHash,OtpSalt,AttemptCount,ExpiresUtc FROM dbo.CostMasterOtpChallenge WITH(UPDLOCK,HOLDLOCK)
WHERE SecurityArea=@SecurityArea AND EmployeeID=@EmployeeID AND SessionID=@SessionID AND ConsumedUtc IS NULL ORDER BY ChallengeID DESC",cn,tx))
                {
                    cmd.Parameters.Add("@SecurityArea",SqlDbType.NVarChar,30).Value=securityArea;
                    cmd.Parameters.Add("@EmployeeID",SqlDbType.BigInt).Value=employeeId; cmd.Parameters.Add("@SessionID",SqlDbType.NVarChar,88).Value=sessionId;
                    long challengeId; byte[] hash,salt; int attempts; DateTime expires;
                    using(SqlDataReader r=cmd.ExecuteReader()){if(!r.Read()) throw new HttpException(400,"Request a new OTP."); challengeId=r.GetInt64(0);hash=(byte[])r[1];salt=(byte[])r[2];attempts=r.GetInt32(3);expires=r.GetDateTime(4);}
                    if(attempts>=MaxAttempts || expires<=DateTime.UtcNow){Consume(cn,tx,challengeId);tx.Commit();throw new HttpException(400,"The OTP has expired or the maximum attempts were reached.");}
                    if(!SlowEquals(hash,HashOtp((otp??"").Trim(),salt,employeeId,sessionId,securityArea))){using(SqlCommand fail=new SqlCommand("UPDATE dbo.CostMasterOtpChallenge SET AttemptCount=AttemptCount+1 WHERE ChallengeID=@Id",cn,tx)){fail.Parameters.Add("@Id",SqlDbType.BigInt).Value=challengeId;fail.ExecuteNonQuery();} tx.Commit();Audit(employeeId,sessionId,"OTP_VERIFY_FAILED",false);throw new HttpException(400,"Invalid OTP.");}
                    Consume(cn,tx,challengeId);
                    using(SqlCommand grant=new SqlCommand(@"UPDATE dbo.CostMasterAuthorization SET RevokedUtc=SYSUTCDATETIME() WHERE SecurityArea=@SecurityArea AND EmployeeID=@EmployeeID AND SessionID=@SessionID AND RevokedUtc IS NULL;
INSERT dbo.CostMasterAuthorization(SecurityArea,EmployeeID,SessionID,ExpiresUtc) VALUES(@SecurityArea,@EmployeeID,@SessionID,DATEADD(MINUTE,15,SYSUTCDATETIME())); SELECT DATEADD(MINUTE,15,SYSUTCDATETIME());",cn,tx))
                    {grant.Parameters.Add("@SecurityArea",SqlDbType.NVarChar,30).Value=securityArea;grant.Parameters.Add("@EmployeeID",SqlDbType.BigInt).Value=employeeId;grant.Parameters.Add("@SessionID",SqlDbType.NVarChar,88).Value=sessionId;expires=Convert.ToDateTime(grant.ExecuteScalar());}
                    tx.Commit(); HttpContext.Current.Session["CostMasterAuthorizedEmployeeID"] = employeeId; Audit(employeeId,sessionId,"ACCESS_GRANTED",true); return DateTime.SpecifyKind(expires,DateTimeKind.Utc);
                }
            }
        }

        public static void RevokeCurrentSession()
        {
            if(HttpContext.Current == null || HttpContext.Current.Session == null) return;
            int id; if(!int.TryParse(HttpContext.Current.User.Identity.Name,out id)) return;
            RevokeSession(id, SessionId());
            HttpContext.Current.Session.Remove("CostMasterAuthorizedEmployeeID");
        }

        public static void RevokeSession(int employeeId, string sessionId)
        {
            if (employeeId <= 0 || String.IsNullOrEmpty(sessionId)) return;
            using(SqlConnection cn=new SqlConnection(SQLHelper.ConnectionString)) using(SqlCommand cmd=new SqlCommand(@"UPDATE dbo.CostMasterAuthorization SET RevokedUtc=SYSUTCDATETIME()
WHERE EmployeeID=@EmployeeID AND SessionID=@SessionID AND RevokedUtc IS NULL;
UPDATE dbo.CostMasterOtpChallenge SET ConsumedUtc=SYSUTCDATETIME()
WHERE EmployeeID=@EmployeeID AND SessionID=@SessionID AND ConsumedUtc IS NULL;",cn))
            {cmd.Parameters.Add("@EmployeeID",SqlDbType.BigInt).Value=employeeId;cmd.Parameters.Add("@SessionID",SqlDbType.NVarChar,88).Value=sessionId;cn.Open();cmd.ExecuteNonQuery();}
        }

        public static void RestartVerification(string securityArea = "COST_MASTER")
        {
            RequireCsrf();
            int employeeId = CurrentEmployeeId();
            string sessionId = SessionId();
            RevokeArea(employeeId, sessionId, securityArea);
            HttpContext.Current.Session.Remove("CostMasterAuthorizedEmployeeID");
            Audit(employeeId, sessionId, "ACCESS_REVOKED", true);
        }

        private static void Consume(SqlConnection cn,SqlTransaction tx,long id){using(SqlCommand c=new SqlCommand("UPDATE dbo.CostMasterOtpChallenge SET ConsumedUtc=SYSUTCDATETIME() WHERE ChallengeID=@Id",cn,tx)){c.Parameters.Add("@Id",SqlDbType.BigInt).Value=id;c.ExecuteNonQuery();}}
        private static void RevokeArea(int employeeId,string sessionId,string securityArea){using(SqlConnection cn=new SqlConnection(SQLHelper.ConnectionString))using(SqlCommand cmd=new SqlCommand(@"UPDATE dbo.CostMasterAuthorization SET RevokedUtc=SYSUTCDATETIME() WHERE SecurityArea=@A AND EmployeeID=@E AND SessionID=@S AND RevokedUtc IS NULL; UPDATE dbo.CostMasterOtpChallenge SET ConsumedUtc=SYSUTCDATETIME() WHERE SecurityArea=@A AND EmployeeID=@E AND SessionID=@S AND ConsumedUtc IS NULL",cn)){cmd.Parameters.Add("@A",SqlDbType.NVarChar,30).Value=securityArea;cmd.Parameters.Add("@E",SqlDbType.BigInt).Value=employeeId;cmd.Parameters.Add("@S",SqlDbType.NVarChar,88).Value=sessionId;cn.Open();cmd.ExecuteNonQuery();}}
        private static byte[] HashOtp(string otp,byte[] salt,int id,string session,string area){using(HMACSHA256 h=new HMACSHA256(salt)){return h.ComputeHash(Encoding.UTF8.GetBytes(area+"|"+id+"|"+session+"|"+otp));}}
        private static bool SlowEquals(byte[] a,byte[] b){if(a==null||b==null)return false;int diff=a.Length^b.Length;for(int i=0;i<Math.Min(a.Length,b.Length);i++)diff|=a[i]^b[i];return diff==0;}
        private static string SecureSixDigitCode(){byte[] b=new byte[4];using(RandomNumberGenerator r=RandomNumberGenerator.Create()){uint x;do{r.GetBytes(b);x=BitConverter.ToUInt32(b,0);}while(x>=UInt32.MaxValue-(UInt32.MaxValue%1000000));return (x%1000000).ToString("D6");}}
        private static bool IsValidEmail(string value){try{MailAddress m=new MailAddress(value);return String.Equals(m.Address,value,StringComparison.OrdinalIgnoreCase);}catch{return false;}}
        private static void SendEmail(string to,string otp){using(MailMessage mail=new MailMessage()){mail.To.Add(to);mail.From=new MailAddress("ack@infinity-data.com","IPS AP Billing",Encoding.UTF8);mail.Subject="Costing Master verification code";mail.Body="Your Costing Master verification code is <b>"+HttpUtility.HtmlEncode(otp)+"</b>. It expires in 5 minutes and can be used once.";mail.IsBodyHtml=true;using(SmtpClient client=new SmtpClient()){client.UseDefaultCredentials=false;client.Credentials=new NetworkCredential("ack@infinity-data.com",new bllInvoice().GetPassword("ackdata"));client.Host="smtp.office365.com";client.Port=587;client.EnableSsl=true;ServicePointManager.SecurityProtocol=SecurityProtocolType.Tls12;client.Send(mail);}}}
        private static void Audit(int id,string sid,string action,bool success){try{using(SqlConnection cn=new SqlConnection(SQLHelper.ConnectionString))using(SqlCommand cmd=new SqlCommand("INSERT dbo.CostMasterSecurityAudit(EmployeeID,SessionID,ActionName,WasSuccessful,SourceIP) VALUES(@E,@S,@A,@OK,@IP)",cn)){cmd.Parameters.Add("@E",SqlDbType.BigInt).Value=id;cmd.Parameters.Add("@S",SqlDbType.NVarChar,88).Value=sid;cmd.Parameters.Add("@A",SqlDbType.NVarChar,40).Value=action;cmd.Parameters.Add("@OK",SqlDbType.Bit).Value=success;cmd.Parameters.Add("@IP",SqlDbType.NVarChar,45).Value=(HttpContext.Current.Request.UserHostAddress??"");cn.Open();cmd.ExecuteNonQuery();}}catch{}}
    }
}
