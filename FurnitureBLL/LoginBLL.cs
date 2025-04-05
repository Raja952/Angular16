using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Xml;
using System.Xml.Linq;
using FurnitureDO;
using FurnitureDAL;
using System.Xml.Xsl;
using System.Web.Hosting;
using System.IO;
using System.Web;
using System.Security.Cryptography;

namespace FurnitureBLL
{
    public class LoginBLL
    {

        public string GetNewUrlPage(string UrName)
        {
            string NewUrl = string.Empty;
            string Image = string.Empty;
            string Years = string.Empty;
            string title = string.Empty;
            string CompanyName = string.Empty;
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(HostingEnvironment.ApplicationPhysicalPath + "\\App_Data\\Setting.xml");
            string LoginData = new LoginBLL().GetApplicableLoginUrl(xdoc, UrName);
            int AppURL = Convert.ToInt32(LoginData.Split('^')[0]);
            bool OnlyPromo = Convert.ToBoolean(LoginData.Split('^')[1]);

            NewUrl = Convert.ToString(LoginData.Split('^')[2]);
            Image = Convert.ToString(LoginData.Split('^')[3]);
            CompanyName = Convert.ToString(LoginData.Split('^')[4]);
            Years = Convert.ToString(LoginData.Split('^')[5]);
            title = Convert.ToString(LoginData.Split('^')[6]);

            return NewUrl + "@" + AppURL + "~" + Image + "~" + CompanyName + "~" + Years + "~" + title + "~" + OnlyPromo;
        }

        public string GetApplicableLoginUrl(XmlDocument docx, string Url)
        {
            int ApplicableUrl = 0;
            bool OnlyPromo = false;
            string strNewUrl = "";
            string strImage = "";
            string strCompanyName = "";
            string strYears = "";
            string strTitle = "";
            XmlNamespaceManager nsmanager = new XmlNamespaceManager(docx.NameTable);
            nsmanager.AddNamespace("", "");
            XmlNodeList nodelist = docx.SelectNodes(string.Format("//AppUrls/appurl"), nsmanager);
            foreach (XmlNode node in nodelist)
            {
                XmlNode XnodeUrlName = node.SelectSingleNode(string.Format("Url"), nsmanager);
                string UrlName = XnodeUrlName.InnerText.Trim();
                if (Url == UrlName)
                {
                    XmlNode XId = node.SelectSingleNode(string.Format("@id"), nsmanager);
                    XmlNode OnlyPromo1 = node.SelectSingleNode(string.Format("@OnlyPromo"), nsmanager);
                    XmlNode NewUrl = node.SelectSingleNode(string.Format("@NewUrl"), nsmanager);
                    XmlNode Image = node.SelectSingleNode(string.Format("@Image"), nsmanager);
                    XmlNode CompanyName = node.SelectSingleNode(string.Format("@CompanyName"), nsmanager);
                    XmlNode Years = node.SelectSingleNode(string.Format("@Years"), nsmanager);
                    XmlNode title = node.SelectSingleNode(string.Format("@title"), nsmanager);
                    ApplicableUrl = Convert.ToInt32(XId.InnerText);
                    OnlyPromo = Convert.ToBoolean(OnlyPromo1.InnerText);
                    strNewUrl = Convert.ToString(NewUrl.InnerText);
                    strImage = Convert.ToString(Image.InnerText);
                    strCompanyName = Convert.ToString(CompanyName.InnerText);
                    strYears = Convert.ToString(Years.InnerText);
                    strTitle = Convert.ToString(title.InnerText);
                    break;
                }
                else
                { ApplicableUrl = 0; }
            }
            return ApplicableUrl + "^" + OnlyPromo + "^" + strNewUrl + "^" + strImage + "^" + strCompanyName + "^" + strYears + "^" + strTitle;
        }


        public void GoogleLoginSign(GoogleSignIn objGoogleSign)
        {
            LoginDetailDO objGoogleLoginDetails = new LoginDetailDO();
            new LoginBLL().InsertGoogleSignin(objGoogleSign);

            //AISessionDO objreg = new LoginBLL().GoogleSignin(objGoogleSign);

            // return objGoogleLoginDetails;
        }



        public string UserRegistration(RegisterDO objRes)
        {
            string str = string.Empty;
            new LoginDAL().UserRegistration(objRes);
            new LoginBLL().SendMail(objRes.Name, objRes.Email, "");
            return str;
        }

        public RegisterDO checkLoginBLL(RegisterDO objRes)
        {
            RegisterDO objreg = new RegisterDO();


            objreg = new LoginDAL().CheckLoginDAL(objRes);

            return objreg;
        }

        public void InsertGoogleSignin(GoogleSignIn objgooglesignin)
        {
            new LoginDAL().InsertGoogleSignin(objgooglesignin);
        }


        public AISessionDO GoogleSignin(GoogleSignIn objgooglesignin)
        {
            SessionContext sessionContext = SessionContext.Current;

            //AISessionDO session = new SessionContext().UserDB(UserName, Password);
            return sessionContext.Googlesignin(objgooglesignin);

        }

        public AISessionDO Authenticate(string _userId, string _password)
        {
            SessionContext sessionContext = SessionContext.Current;
            string _sessionId = null;

            //Direct Login to NewSystem
            Random randNum = new Random();
            _sessionId = GetMd5Sum(randNum.Next().ToString());
            _sessionId = StringLength(_sessionId, 5);



            HttpContext.Current.Session["LogSessionId"] = _sessionId;
            return sessionContext.Login(_userId, _password, _sessionId);

        }
        private static string GenerateNewRandom()
        {
            Random Ran = new Random();
            string r = Ran.Next(000000, 999999).ToString("D6");

            return r;
        }


        public LoginDetailDO GetLoginBLL(LoginDetailDO objLogindetails, bool IsGoogleLogin, string _sessionId)
        {
            LoginDetailDO objLoginDetails = new LoginDetailDO();
            ////AISessionDO objreg = new LoginBLL().UserB(objLogindetails,  IsGoogleLogin, _sessionId);
            //objLoginDetails.IsOTP = objreg.IsOTPValid;
            //if (objreg.IsOTPValid == true)
            //{
            //    string OTP = GenerateNewRandom();
            //    objreg.OTP = OTP;
            //    //objLoginDetails.ISMailSend = new LoginBLL().SendMail(objLogindetails.UserName, SessionContext.Current.AISession.Email, OTP);
            //}



            return objLoginDetails;
        }

        public int GetUserId(string UserName, string EmailId)
        {
            return new LoginDAL().GetUserId(UserName, EmailId);

        }

        public bool ForgetPassword(string UserName, string EmailId)
        {

            return new LoginBLL().ForgetPasswordMail(UserName, EmailId); ;
        }

        public bool resetPasswordBLL(string oldpassword, string password, string cpassword, int UserId)
        {
            return new LoginDAL().resetPasswordDAL(oldpassword, password, cpassword, UserId);
        }


        public string VerifyOTP(LoginDetailDO objLogindetails)
        {
            string IsVerify = string.Empty;
            LoginDetailDO objOtpdetails = new LoginDetailDO();

            try
            {
                objOtpdetails = objLogindetails;
                //ObjectCache cache = MemoryCache.Default;
                //string Key = OTP + "~" + SessionContext.Current.AISession.SessionId + "~" + SessionContext.Current.AISession.UserMail;
                // string strStoredOTP = (string)cache.Get(Key);

                if (String.Compare(SessionContext.Current.AISession.OTP.Trim(), objLogindetails.OTP.Trim()) == 0)
                {
                    IsVerify = "true";
                    SessionContext.Current.AISession.SessionConnected = true;
                }
                else
                {
                    IsVerify = "false";
                }
            }
            catch (Exception) { }

            return IsVerify;
        }

        public bool SendMail(string UserName, string Mail, string OTP)
        {
            XmlDocument xml = new XmlDocument();

            string fromMail = "sushiltechnex1@gmail.com";
            string fromPassword = "mabfiepyfiuisavm";
            MailMessage message = new MailMessage();
            XmlDocument xmlDoc = new XmlDocument();

            string body = "";
            // Load the XSLT file
            XslCompiledTransform xslt = new XslCompiledTransform();
            XsltArgumentList argList = new XsltArgumentList();
            //xslt.Load(HostingEnvironment.MapPath("~/SendOTP.xslt"));
            string loadxslt = HostingEnvironment.ApplicationPhysicalPath + "XSLTFiles\\SendOTP.xslt";
            xslt.Load(loadxslt);
            argList.AddParam("UserName", string.Empty, UserName);
            argList.AddParam("OTP", string.Empty, OTP);
            StringWriter sw = new StringWriter();
            xslt.Transform(xmlDoc, argList, sw);

            body = sw.ToString();
            message.From = new MailAddress(fromMail);
            message.Subject = "One Time Password";
            message.To.Add(new MailAddress(Mail));

            return SendMailOTP(message, body, fromMail, fromPassword); ;
        }

        #region Send Maill
        private bool SendMailOTP(MailMessage mail, string strMailBody, string fromMail, string fromPassword)
        {
            bool IsSent = false;
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(strMailBody.ToString(), null, "text/html");
            mail.AlternateViews.Add(htmlView);
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromMail, fromPassword),
                    EnableSsl = true,
                };

                smtpClient.Send(mail);
                IsSent = true;
            }
            catch (Exception ex)
            {
                IsSent = false;
            }
            return IsSent;
        }
        #endregion


        //public bool SendMailOld(string UserName, string Mail, bool OTPValue)
        //{
        //    XmlDocument xml = new XmlDocument();
        //    bool MailSended = false;
        //    string OTP = GenerateNewRandom();
        //    string fromMail = "sushiltechnex1@gmail.com";
        //    string fromPassword = "mabfiepyfiuisavm";
        //    MailMessage message = new MailMessage();

        //    try
        //    {
        //        message.From = new MailAddress(fromMail);
        //        message.Subject = "Vikas Furnitur";
        //        message.To.Add(new MailAddress(Mail));
        //        message.Body = "<html><body> <div>  <h1>Dear " + UserName + " </h1> <br/> " +
        //                "<p style=\"font-size:50px;><br/> Please enter Your" +
        //                " OTP is valid for 10 minutes</p><br/>" + "Your OTP Is " + "<b><style=\"color:red;\">" + OTP + "</b>" + " <br/><br/><br/>" +
        //                "Thanks & Regards," + "<br/>" +
        //                UserName + "</body></html> ";

        //        message.IsBodyHtml = true;
        //        var smtpClient = new SmtpClient("smtp.gmail.com")
        //        {
        //            Port = 587,
        //            Credentials = new NetworkCredential(fromMail, fromPassword),
        //            EnableSsl = true,
        //        };

        //        smtpClient.Send(message);
        //        MailSended = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MailSended = false;
        //    }
        //    return MailSended;
        //}

        public bool ForgetPasswordMail(string UserName, string Mail)
        {
            bool Mailsended = false;
            string hostName = HttpContext.Current.Request.Url.Host;
            string resetUrl = $"http://{hostName}/Furniture/Home/ResetPasswordLink?Userid={Mail}&Username={UserName}";
            string Loadxml = HostingEnvironment.ApplicationPhysicalPath + "App_Data\\Email.xml";

            XmlDocument zoopmail = new XmlDocument();

            try
            {
                // Load the XML document
                zoopmail.Load(Loadxml);

                // Extract values using XPath
                string fromMail = zoopmail.SelectSingleNode("/ZoopEmailSessing/FromMailId").InnerText;
                string fromPassword = zoopmail.SelectSingleNode("/ZoopEmailSessing/Password").InnerText;

                // Email message setup
                MailMessage message = new MailMessage
                {
                    From = new MailAddress(fromMail),
                    Subject = "Reset Your Password",
                    IsBodyHtml = true
                };

                message.To.Add(new MailAddress(Mail));

                // Professional Email Body
                message.Body = $@"
        <html>
        <head>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    background-color: #f8f1e4;
                    padding: 20px;
                }}
                .email-container {{
                    max-width: 600px;
                    background: #fffaf0;
                    margin: auto;
                    padding: 20px;
                    border-radius: 8px;
                    box-shadow: 0px 0px 15px rgba(0, 0, 0, 0.1);
                    text-align: center;
                    border: 2px solid #d4af37;
                }}
                .header {{
                    font-size: 24px;
                    color: #8b6508;
                    font-weight: bold;
                }}
                .message {{
                    font-size: 16px;
                    color: #5c3c00;
                    margin: 20px 0;
                }}
                .reset-button {{
                    display: inline-block;
                    padding: 12px 20px;
                    margin-top: 10px;
                    font-size: 18px;
                    color: #fff;
                    background: #d4af37;
                    text-decoration: none;
                    border-radius: 5px;
                    font-weight: bold;
                    box-shadow: 2px 2px 5px rgba(0, 0, 0, 0.2);
                }}
                .reset-button:hover {{
                    background: #b89624;
                }}
                .footer {{
                    font-size: 14px;
                    color: #776043;
                    margin-top: 20px;
                }}
            </style>
        </head>
        <body>
            <div class='email-container'>
                <h2 class='header'>Password Reset Request</h2>
                <p class='message'>Hello <b>{UserName}</b>,</p>
                <p class='message'>We received a request to reset your password. Click the button below to proceed:</p>
                <a href='{resetUrl}' class='reset-button'>Reset Password</a>
                <p class='message'>If you did not request this, please ignore this email.</p>
                <p class='footer'>Thanks,<br/>Vikas Furniture</p>
            </div>
        </body>
        </html>";

                // SMTP Client Setup
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromMail, fromPassword),
                    EnableSsl = true
                };

                smtpClient.Send(message);
                Mailsended = true;
            }
            catch (Exception ex)
            {
                Mailsended = false;
            }

            return Mailsended;
        }

        public string ReadFurnitureFiles(string Loadxml)
        {
            string data = string.Empty;
            XmlDocument objxmlNode = new XmlDocument();
            List<Student> studentdetails = new List<Student>();


            objxmlNode.Load(Loadxml);

            XmlNode RootElement = objxmlNode.SelectSingleNode("employees");

            if (RootElement != null)
            {

                XmlNodeList objStudentdt = objxmlNode.SelectNodes("employee");

                for (var i = 0; i < objStudentdt.Count; i++)
                {
                    Student student = new Student();
                    student.ID = objStudentdt[i].SelectNodes("ID").Count;


                }

            }
            //XDocument doc = XDocument.Load(Loadxml);

            //// Query the XML to get employee information
            //var studentd = from employee in doc.Descendants("employee")
            //                select new Student
            //                {
            //                    ID = (int)employee.Element("id"),
            //                    Name = (string)employee.Element("name"),
            //                    Department = (string)employee.Element("department"),
            //                    Salary = (int)employee.Element("salary")
            //                };


            return data;

        }



        static public string GetMd5Sum(string str)
        {

            Encoder enc = System.Text.Encoding.Unicode.GetEncoder();

            byte[] unicodeText = new byte[str.Length * 2];

            enc.GetBytes(str.ToCharArray(), 0, str.Length, unicodeText, 0, true);

            MD5 md5 = new MD5CryptoServiceProvider();

            byte[] result = md5.ComputeHash(unicodeText);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < result.Length; i++)
            {
                sb.Append(result[i].ToString("X2"));
            }

            return sb.ToString();

        }

        public static string StringLength(string str, int length)
        {
            if (!string.IsNullOrEmpty(str))
                if (str.Length > length)
                {
                    str = str.Substring(0, length);
                }

            return str;
        }

    }
}
