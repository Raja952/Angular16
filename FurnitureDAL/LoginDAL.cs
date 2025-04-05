using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using FurnitureDO;

namespace FurnitureDAL
{
    public class LoginDAL
    {

        public string UserRegistration(RegisterDO objRes)
        {
            int CompCode = 1;
            string ret = string.Empty;
            try
            {

                List<SqlParameters> param = new List<SqlParameters>();
                param.Add(SqlParameters.Add("@Name", SqlDbType.VarChar, 100, objRes.Name, ParameterDirection.Input));
                param.Add(SqlParameters.Add("@Email", SqlDbType.VarChar, 100, objRes.Email, ParameterDirection.Input));
                param.Add(SqlParameters.Add("@Password", SqlDbType.VarChar, 100, objRes.Password, ParameterDirection.Input));
                param.Add(SqlParameters.Add("@Address", SqlDbType.VarChar, 500, objRes.Address, ParameterDirection.Input));
                param.Add(SqlParameters.Add("@PhoneNo", SqlDbType.VarChar, 20, objRes.Phone, ParameterDirection.Input));

                CompCode = SqlManager.ExecuteNonQuery(CommandType.StoredProcedure, "usp_RegisterUser", param, 1, CompCode);
            }
            catch (Exception ex) { 
            
            }
            return CompCode.ToString();
        }
        //Old way Login

        public RegisterDO CheckLoginDAL(RegisterDO objRes)
        {
            RegisterDO objreg = new RegisterDO();
            int CompCode = 1;
            string ret = string.Empty;
            List<SqlParameters> param = new List<SqlParameters>();
            param.Add(SqlParameters.Add("@Email", SqlDbType.VarChar, 100, objRes.Email, ParameterDirection.Input));
            param.Add(SqlParameters.Add("@Password", SqlDbType.VarChar, 100, objRes.Password, ParameterDirection.Input));


            using (SqlDataReader reader = SqlManager.ExecuteReader(CommandType.StoredProcedure, "Usp_CheckLogin", param, 1, CompCode))
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    objRes.value = Util.GetInteger(reader["Value"]);
                    objreg = objRes;

                }
                reader.Close();
            }
            return objreg;
        }

        //New Way To Login


        public AISessionDO UserDB1(string username, string password, AISessionDO session)
        {
            int CompCode = 1;
            string ret = string.Empty;
            //AISessionDO session = new AISessionDO();

            List<SqlParameters> param = new List<SqlParameters>();
            param.Add(SqlParameters.Add("@UserName", SqlDbType.VarChar, 50, username, ParameterDirection.Input));
            param.Add(SqlParameters.Add("@Password", SqlDbType.VarChar, 50, password, ParameterDirection.Input));

            using (SqlDataReader reader = SqlManager.ExecuteReader(CommandType.StoredProcedure, "Usp_GetLogin", param, 1, CompCode))
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    string lookupPassword = Util.GetString(reader["cPassword"]);
                    // case sensitive comparision of password
                    if (0 == string.Compare(lookupPassword, password, true))
                    {
                        session = BuildReaderNew(reader, session);
                    }
                }
                reader.Close();
            }
            return session;

        }

        private AISessionDO BuildReaderNew(SqlDataReader reader, AISessionDO session)
        {
            session.UserId = Util.GetInteger(reader["nUserId"]);
            session.Email = Util.GetString(reader["cUserName"]);
            session.Password = Util.GetString(reader["cPassword"]);
            return session;
        }



        public int GetUserId(string UserName, string EnailId)
        {
            int UserId = 0;

            List<SqlParameters> param = new List<SqlParameters>();
            param.Add(SqlParameters.Add("@UserName", SqlDbType.VarChar, 50, UserName, ParameterDirection.Input));
            param.Add(SqlParameters.Add("@EmailId", SqlDbType.VarChar, 50, EnailId, ParameterDirection.Input));
            //SqlManager.ExecuteNonQuery(CommandType.StoredProcedure, "Usp_Login", param, 1, CompCode);
            using (SqlDataReader reader = SqlManager.ExecuteReader(CommandType.StoredProcedure, "Usp_GetUserId", param, 1, 1))
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    string DBUserName = Util.GetString(reader["cUserName"]);
                    string DBEmail = Util.GetString(reader["cEmail"]);
                    // case sensitive comparision of password
                    if ((0 == string.Compare(UserName, DBUserName, true)) && (0 == string.Compare(EnailId, DBEmail, true)))
                    {
                        UserId = Util.GetInteger(reader["nUserId"]);
                    }
                    else
                    {
                        UserId = 0;
                    }
                }
                reader.Close();
            }
            return UserId;
        }

        public bool resetPasswordDAL(string oldpassword, string NewPasswd, string CPassword, int UserId)
        {
            int result = 0;
            List<SqlParameters> param = new List<SqlParameters>();
            param.Add(SqlParameters.Add("@UserId", SqlDbType.Int, 4, UserId, ParameterDirection.Input));
            param.Add(SqlParameters.Add("@OldPassword", SqlDbType.VarChar, 50, oldpassword, ParameterDirection.Input));
            param.Add(SqlParameters.Add("@Password", SqlDbType.VarChar, 50, NewPasswd, ParameterDirection.Input));
            param.Add(SqlParameters.Add("@CPassword", SqlDbType.VarChar, 50, CPassword, ParameterDirection.Input));

            result = SqlManager.ExecuteNonQuery(CommandType.StoredProcedure, "Usp_ResetPassword", param, 1, 1);
            return Util.GetBoolean(result);
        }

        public string UserDB1(string username, string password)
        {
            int CompCode = 1;
            string ret = string.Empty;

            List<SqlParameters> param = new List<SqlParameters>();
            param.Add(SqlParameters.Add("@UserName", SqlDbType.VarChar, 50, username, ParameterDirection.Input));
            param.Add(SqlParameters.Add("@Password", SqlDbType.VarChar, 50, password, ParameterDirection.Input));
            //SqlManager.ExecuteNonQuery(CommandType.StoredProcedure, "Usp_Login", param, 1, CompCode);
            using (SqlDataReader reader = SqlManager.ExecuteReader(CommandType.StoredProcedure, "Usp_GetLogin", param, 1, CompCode))
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    string lookupPassword = Util.GetString(reader["cPassword"]);
                    // case sensitive comparision of password
                    if (0 == string.Compare(lookupPassword, password, true))
                    {
                        ret = "1";
                    }
                    else
                    {
                        ret = "0";
                    }
                }
                reader.Close();
            }
            return ret;

        }

        public void InsertGoogleSignin(GoogleSignIn objGoogleSignin)
        {

            List<SqlParameters> param = new List<SqlParameters>();
            param.Add(SqlParameters.Add("@Name", SqlDbType.VarChar, 100, objGoogleSignin.Name, ParameterDirection.Input));
            param.Add(SqlParameters.Add("@VerifiedEmail", SqlDbType.VarChar, 100, objGoogleSignin.verified_email, ParameterDirection.Input));
            param.Add(SqlParameters.Add("@Email", SqlDbType.VarChar, 100, objGoogleSignin.Email, ParameterDirection.Input));
            param.Add(SqlParameters.Add("@FullName", SqlDbType.VarChar, 300, objGoogleSignin.FullName, ParameterDirection.Input));
            param.Add(SqlParameters.Add("@SurName", SqlDbType.VarChar, 50, objGoogleSignin.SurName, ParameterDirection.Input));
            param.Add(SqlParameters.Add("@Picture", SqlDbType.VarChar, 1000, objGoogleSignin.Picture, ParameterDirection.Input));

            SqlManager.ExecuteNonQuery(CommandType.StoredProcedure, "Usp_InsertGoogleDetails", param, 1, 1);

        }


        public AISessionDO GoogleSignin(GoogleSignIn objGoogleSignin, AISessionDO session)
        {
            int CompCode = 1;
            string ret = string.Empty;

            // AISessionDO session = new AISessionDO();

            List<SqlParameters> param = new List<SqlParameters>();
            param.Add(SqlParameters.Add("@UserName", SqlDbType.VarChar, 50, objGoogleSignin.Email, ParameterDirection.Input));
            param.Add(SqlParameters.Add("@Password", SqlDbType.VarChar, 50, "Google_" + objGoogleSignin.Name, ParameterDirection.Input));

            using (SqlDataReader reader = SqlManager.ExecuteReader(CommandType.StoredProcedure, "Usp_GetGoogleLogin", param, 1, CompCode))
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    string lookupPassword = Util.GetString(reader["cPassword"]);
                    // case sensitive comparision of password
                    if (0 == string.Compare(lookupPassword, "Google_" + Convert.ToString(objGoogleSignin.Name), true))
                    {
                        session = BuildReader(reader, session, "");
                    }
                }
                reader.Close();
            }
            return session;

        }

        public AISessionDO Authenticate(string _userId, string _password, AISessionDO session, string _SessionId)
        {
            List<SqlParameters> param = new List<SqlParameters>();
            param.Add(SqlParameters.Add("@UserName", SqlDbType.VarChar, 50, _userId, ParameterDirection.Input));
            param.Add(SqlParameters.Add("@Password", SqlDbType.VarChar, 50, _password, ParameterDirection.Input));
            param.Add(SqlParameters.Add("@IsGoogle", SqlDbType.Bit, 1, false, ParameterDirection.Input));

            using (SqlDataReader reader = SqlManager.ExecuteReader(CommandType.StoredProcedure, "Usp_GetLogin", param, 1, 1)) // 1 is compcode
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    string lookupPassword = Util.GetString(reader["cPassword"]);
                    // case sensitive comparision of password
                    if (0 == string.Compare(lookupPassword, _password, true))
                    {
                        session = BuildReader(reader, session, _SessionId);
                    }
                }
                reader.Close();
            }
            return session;
        }



        public AISessionDO UserDB(LoginDetailDO objLogindetails, AISessionDO session, bool IsGoogleLogin, string _sessionId)
        {
            int CompCode = 1;
            string ret = string.Empty;
            //AISessionDO session = new AISessionDO();

            List<SqlParameters> param = new List<SqlParameters>();
            param.Add(SqlParameters.Add("@UserName", SqlDbType.VarChar, 50, objLogindetails.UserName, ParameterDirection.Input));
            param.Add(SqlParameters.Add("@Password", SqlDbType.VarChar, 50, objLogindetails.Password, ParameterDirection.Input));
            param.Add(SqlParameters.Add("@IsGoogle", SqlDbType.Bit, 1, IsGoogleLogin, ParameterDirection.Input));
            //SqlManager.ExecuteNonQuery(CommandType.StoredProcedure, "Usp_Login", param, 1, CompCode);
            using (SqlDataReader reader = SqlManager.ExecuteReader(CommandType.StoredProcedure, "Usp_GetLogin", param, 1, CompCode))
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    string lookupPassword = Util.GetString(reader["cPassword"]);
                    // case sensitive comparision of password
                    if (0 == string.Compare(lookupPassword, objLogindetails.Password, true))
                    {
                        session = BuildReader(reader, session, _sessionId);
                    }
                }
                reader.Close();
            }
            return session;

        }

        private AISessionDO BuildReader(SqlDataReader reader, AISessionDO session, string _sessionId)
        {
            session.SessionId = Util.GetString(_sessionId);
            session.UserId = Util.GetInteger(reader["nUserId"]);
            session.UserName = Util.GetString(reader["cUserName"]);
            session.Email = Util.GetString(reader["cEmailId"]);
            session.IsOTPValid = Util.GetBoolean(reader["bLoginOTP"]);
            if (session.IsOTPValid == true)
            {
                session.SessionConnected = false; // It Will set After Otp enter and login
            }
            else
            {
                session.SessionConnected = true; // if OTP Not Applicable Than Direct Login
            }

            return session;
        }
    }
}
