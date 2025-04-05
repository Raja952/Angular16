using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using FurnitureDO;
using System.Data.SqlClient;

namespace FurnitureDAL
{
    public class MemberDAL
    {
        public void Getmemberdetails(MemberDO objMemberDetails)
        {
            List<MemberDO> memberdetailsLST = new List<MemberDO>();
            List<SqlParameters> param = new List<SqlParameters>();
            param.Add(SqlParameters.Add("@Name", SqlDbType.VarChar, 50, objMemberDetails.Name, ParameterDirection.Input));
            param.Add(SqlParameters.Add("@Email", SqlDbType.VarChar, 50, objMemberDetails.Email, ParameterDirection.Input));
            param.Add(SqlParameters.Add("@Password", SqlDbType.VarChar, 10, objMemberDetails.Password, ParameterDirection.Input));
            param.Add(SqlParameters.Add("@Address", SqlDbType.VarChar, 100, objMemberDetails.Address, ParameterDirection.Input));
            param.Add(SqlParameters.Add("@Phone", SqlDbType.VarChar, 10, objMemberDetails.Phone, ParameterDirection.Input));

            SqlDataReader reader = SqlManager.ExecuteReader(CommandType.StoredProcedure, "usp_InsertMemberDetails", param, 1, 1);

            ;
        }

        public List<MemberDO> BuildGetMemberCreditDetail(SqlDataReader dr)
        {
            List<MemberDO> objMemberDetailsDO = new List<MemberDO>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    objMemberDetailsDO.Add(new MemberDO()
                    {
                        FName = Util.GetString(dr["cFirstName"]),
                        LName = Util.GetString(dr["cLastName"]),
                        Name = Util.GetString(dr["cFullName"]),
                        Email = Util.GetString(dr["cEmail"]),
                        Address = Util.GetString(dr["cAddress"]),
                        Date = Util.GetString(dr["dDateRegister"]),

                    });
                }
            }
            return objMemberDetailsDO.ToList();

        }


        public List<MemberDetails> Getmemberdetails(int MemberID)
        {
            List<MemberDetails> memberdetailsLST = new List<MemberDetails>();
            List<SqlParameters> param = new List<SqlParameters>();
            param.Add(SqlParameters.Add("@MemberId", SqlDbType.VarChar, 50, MemberID, ParameterDirection.Input));
            //param.Add(SqlParameters.Add("@Email", SqlDbType.VarChar, 50, objMemberDetails.Email, ParameterDirection.Input));
            //param.Add(SqlParameters.Add("@Password", SqlDbType.VarChar, 10, objMemberDetails.Password, ParameterDirection.Input));
            //param.Add(SqlParameters.Add("@Address", SqlDbType.VarChar, 100, objMemberDetails.Address, ParameterDirection.Input));
            //param.Add(SqlParameters.Add("@Phone", SqlDbType.VarChar, 10, objMemberDetails.Phone, ParameterDirection.Input));
            using (SqlDataReader reader = SqlManager.ExecuteReader(CommandType.StoredProcedure, "Usp_GetMemberDetails_v1", param, 2, 1))
            {
                if (reader.HasRows)
                {
                    reader.Read();

                    memberdetailsLST = BuildGetMemberDetail(reader);
                }

                reader.Close();
            }
            return memberdetailsLST;

        }



        private List<MemberDetails> BuildGetMemberDetail(SqlDataReader dr)
        {
            List<MemberDetails> memberDetailsList = new List<MemberDetails>();

            while (dr.Read())
            {
                memberDetailsList.Add(new MemberDetails()
                {
                    OrderName = dr["order_name"]?.ToString(),
                    PhoneNo = dr["order_phone"]?.ToString(),
                    Address = dr["order_address"]?.ToString(),
                    Productname = dr["products"]?.ToString(),
                    Status = dr["order_status"]?.ToString(),
                    Amount = dr["order_sub_status"] != DBNull.Value ? Convert.ToDecimal(dr["order_sub_status"]) : 0
                });
            }

            return memberDetailsList;
        }


        //private List<MemberDetails> BuildGetMemberDetail(SqlDataReader dr)
        //{
        //    List<MemberDetails> objMemberDetailsDO = new List<MemberDetails>();

        //    while (dr.Read()) // No need to check HasRows separately
        //    {
        //        objMemberDetailsDO.Add(new MemberDetails()
        //        {
        //            OrderName = dr["order_name"]?.ToString(),
        //            PhoneNo = dr["order_phone"]?.ToString(),
        //            Address = dr["order_address"]?.ToString(),
        //            Productname = dr["products"]?.ToString(),
        //            Status = dr["order_status"]?.ToString(),
        //            Amount = dr["order_sub_status"] != DBNull.Value ? Convert.ToDecimal(dr["order_sub_status"]) : 0
        //        });
        //    }

        //    return objMemberDetailsDO;
        //}



        //private List<MemberDetails> BuildGetMemberDetail(SqlDataReader dr)
        //{
        //    List<MemberDetails> objMemberDetailsDO = new List<MemberDetails>();
        //    if (dr.HasRows)
        //    {
        //        while (dr.Read())
        //        {
        //            objMemberDetailsDO.Add(new MemberDetails()
        //            {
        //                OrderName = Util.GetString(dr["cOrderName"]),
        //                PhoneNo = Util.GetString(dr["nPhone_No"]),
        //                Address = Util.GetString(dr["cAddress"]),
        //                Productname = Util.GetString(dr["cProductName"]),
        //                Status = Util.GetString(dr["cStatus"]),
        //                Amount = Util.GetDecimal(dr["nAmount"]),

        //            });
        //        }
        //    }
        //    return objMemberDetailsDO.ToList();

        //}

        public void InsertMemberDetails(MemberDO objMemberDetails)
        {
            List<MemberDO> memberdetailsLST = new List<MemberDO>();
            List<SqlParameters> param = new List<SqlParameters>();
            param.Add(SqlParameters.Add("@Name", SqlDbType.VarChar, 50, objMemberDetails.Name, ParameterDirection.Input));
            param.Add(SqlParameters.Add("@Email", SqlDbType.VarChar, 50, objMemberDetails.Email, ParameterDirection.Input));
            param.Add(SqlParameters.Add("@Password", SqlDbType.VarChar, 10, objMemberDetails.Password, ParameterDirection.Input));
            param.Add(SqlParameters.Add("@Address", SqlDbType.VarChar, 100, objMemberDetails.Address, ParameterDirection.Input));
            param.Add(SqlParameters.Add("@Phone", SqlDbType.VarChar, 10, objMemberDetails.Phone, ParameterDirection.Input));
            SqlDataReader reader = SqlManager.ExecuteReader(CommandType.StoredProcedure, "usp_InsertMemberDetails", param, 1, 1);

        }


    }
}
