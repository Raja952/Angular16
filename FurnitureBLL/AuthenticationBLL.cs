using FurnitureDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FurnitureBLL
{
    public class AuthenticationBLL
    {
        //public LoginDetailDO Authentication(string _userId, string _password, string NewsessionId)
        //{
        //    SessionContext sessionContext = SessionContext.Current;
        //    string _sessionId = null;
        //    if (NewsessionId == null || NewsessionId == string.Empty)
        //    {
        //        //Direct Login to NewSystem
        //        Random randNum = new Random();
        //        _sessionId = GetMd5Sum(randNum.Next().ToString());
        //        _sessionId = StringLength(_sessionId, 5);
        //    }
        //    else
        //    {
        //        //From OldSystem To NewSystem
        //        _sessionId = NewsessionId;
        //    }

        //    HttpContext.Current.Session["LogSessionId"] = _sessionId;


        //    return sessionContext.Login(_userId, _password, _sessionId, 1);
        //}

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
