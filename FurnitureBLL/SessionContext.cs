using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using FurnitureDO;
using FurnitureDAL;
using System.Xml;
using System.Net.Mail;
using System.Net;

namespace FurnitureBLL
{
    public class SessionContext
    {
        private AISessionDO aiSession = null;

        public AISessionDO AISession
        {
            get
            {
                if (aiSession == null)
                {
                    aiSession = new AISessionDO();
                }
                return aiSession;
            }
        }

        public static SessionContext Current
        {
            get
            {
                SessionContext sessionContext
                    = (SessionContext)HttpContext.Current.Session["_userSession"];

                if (sessionContext == null)
                {
                    sessionContext = new SessionContext();
                }

                return sessionContext;
            }
        }

        #region User Authentication

        public AISessionDO Login(string _userId, string _password, string _SessionId)
        {
            aiSession = null;
            AISessionDO session = new LoginDAL().Authenticate(_userId, _password, this.AISession, _SessionId);
            HttpContext.Current.Session["_userSession"] = this;

            return session;
        }
        #endregion

        public AISessionDO UserB(LoginDetailDO objLogindetails,bool IsGoogleLogin,string _sessionId)
        {
            LoginDO obj = new LoginDO();
            //string data = string.Empty;         
            aiSession = null;
            AISessionDO session = new LoginDAL().UserDB(objLogindetails, this.AISession, IsGoogleLogin, _sessionId);
            HttpContext.Current.Session["_userSession"] = this;
            return session;
        }


        public AISessionDO Googlesignin(GoogleSignIn objgooglesignin)
        {
            LoginDO obj = new LoginDO();
            AISessionDO session = new LoginDAL().GoogleSignin(objgooglesignin, this.AISession);
            HttpContext.Current.Session["_userSession"] = this;
            return session;
        }

        private static string GenerateNewRandom()
        {
            Random Ran = new Random();
            string r = Ran.Next(000000, 999999).ToString("D6");

            return r;
        }

        public string SendMail(string UserName, string Mail)
        {

            XmlDocument xml = new XmlDocument();
            string OTP = GenerateNewRandom();

            string fromMail = "sushiltechnex1@gmail.com";
            string fromPassword = "mabfiepyfiuisavm";
            string BorderColor = "RED";
            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Vikas Furniture";
            message.To.Add(new MailAddress(Mail));
            message.Body = "<html><body> <div>  <h1>Dear " + UserName + " </h1> <br/> " +
               "<p style=\"font-size:50px;><br/> Please enter Your" +
               " OTP is valid for 10 minutes</p><br/>" + "Your OTP Is " + "<b><style=\"color:red;\">"+ OTP + "</b>" + " <br/><br/><br/>" +
               "Thanks & Regards," + "<br/>" +
               UserName + "</body></html> ";

            //"<div style=\"width:710px; border:8px solid"; margin:0px; padding:0px;\">";





            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);



            return Mail;
        }

    }
}
