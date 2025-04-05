using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDAL
{
    public sealed class Util
    {
        public static int GetInteger(object obj)
        {
            return (obj == null || obj == DBNull.Value || obj.ToString() == "") ? 0 : Convert.ToInt32(obj);
        }

        public static int GetInteger(bool b)
        {
            return b ? 1 : 0;
        }

        public static string GetString(object obj)
        {
            return (obj == null || obj == DBNull.Value || obj.ToString() == "") ? string.Empty : obj.ToString().Trim();
        }

        public static DateTime GetDateTime(object str)
        {
            return (str == null || str == DBNull.Value || str.ToString() == "") ? DateTime.MinValue : Convert.ToDateTime(str);
        }

        public static Single GetSingle(object str)
        {
            return (str == null || str == DBNull.Value || str.ToString() == "") ? Convert.ToSingle(0.0) : Convert.ToSingle(str);
        }

        public static Decimal GetDecimal(object str)
        {
            return (str == null || str == DBNull.Value || str.ToString() == "") ? Convert.ToDecimal(0.0) : Convert.ToDecimal(str);
        }

        public static bool GetBoolean(object str)
        {
            return (str == null || str == DBNull.Value || str.ToString() == "") ? false : Convert.ToBoolean(str);
        }

        public static bool GetBoolean(int i)
        {
            return i == 0 ? false : true;
        }

  

        

   
      



        public static string EncryptData(string OtherDetail)
        {
            string strmsg = string.Empty;
            byte[] encode = new byte[OtherDetail.Length];
            encode = Encoding.UTF8.GetBytes(OtherDetail);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }


       

   


    }
}
