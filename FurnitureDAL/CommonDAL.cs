using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurnitureDO;

namespace FurnitureDAL
{
    public class CommonDAL
    {

        public string InsertCheckOutDetails(CheckOutDO objCheckOutData)
        {
            List<SqlParameters> param = new List<SqlParameters>();

            param.Add(SqlParameters.Add("@UserId", SqlDbType.Int, 4, objCheckOutData.UserId, ParameterDirection.Input));
            param.Add(SqlParameters.Add("@SessionId", SqlDbType.VarChar, 100, objCheckOutData.SessionId, ParameterDirection.Input));
            param.Add(SqlParameters.Add("@Amount", SqlDbType.Decimal, 5, objCheckOutData.Amount, ParameterDirection.Input));
            param.Add(SqlParameters.Add("@Description", SqlDbType.VarChar, 50, objCheckOutData.Description, ParameterDirection.Input));
            param.Add(SqlParameters.Add("@ImageURL", SqlDbType.VarChar, 1000, objCheckOutData.ImageUrl, ParameterDirection.Input));
            param.Add(SqlParameters.Add("@Id", SqlDbType.Int, 4, objCheckOutData.Id, ParameterDirection.Input));
            param.Add(SqlParameters.Add("@Name", SqlDbType.VarChar, 255, objCheckOutData.Name, ParameterDirection.Input));
            param.Add(SqlParameters.Add("@Price", SqlDbType.Decimal, 5, objCheckOutData.Price, ParameterDirection.Input));
            param.Add(SqlParameters.Add("@OriginalPrice", SqlDbType.Decimal, 5, objCheckOutData.OriginalPrice, ParameterDirection.Input));
            param.Add(SqlParameters.Add("@Discount", SqlDbType.Int, 4, objCheckOutData.Discount, ParameterDirection.Input));
            param.Add(SqlParameters.Add("@Quantity", SqlDbType.Int, 4, objCheckOutData.Quantity, ParameterDirection.Input));
            param.Add(SqlParameters.Add("@MaxQuantity", SqlDbType.Int, 4, objCheckOutData.MaxQuantity, ParameterDirection.Input));

            // Execute and return the result from the stored procedure
            var result = SqlManager.ExecuteScalar(CommandType.StoredProcedure, "Usp_InsertCartItem", param, 1, 1);
            return result.ToString();  // Result could be "Item already exists in the cart" or "Item added to the cart"
        }

        public string RemoveCheckOutDetails(CheckOutDO objCheckOutData)
        {
            List<SqlParameters> param = new List<SqlParameters>();

            param.Add(SqlParameters.Add("@UserId", SqlDbType.Int, 4, objCheckOutData.UserId, ParameterDirection.Input));
            param.Add(SqlParameters.Add("@SessionId", SqlDbType.VarChar, 100, objCheckOutData.SessionId, ParameterDirection.Input));
            param.Add(SqlParameters.Add("@Id", SqlDbType.Int, 4, objCheckOutData.Id, ParameterDirection.Input));

            // Execute and return the result from the stored procedure
            var result = SqlManager.ExecuteScalar(CommandType.StoredProcedure, "Usp_RemoveCartItem", param, 1, 1);
            return result.ToString();  // Result could be "Item removed from the cart" or "Item not found in the cart"
        }



        //public void InsertCheckOutDetails(CheckOutDO objCheckOutData)
        //{
        //    List<SqlParameters> param = new List<SqlParameters>();

        //    param.Add(SqlParameters.Add("@UserId", SqlDbType.Int, 4, objCheckOutData.UserId, ParameterDirection.Input));
        //    param.Add(SqlParameters.Add("@SessionId", SqlDbType.VarChar, 100, objCheckOutData.SessionId, ParameterDirection.Input));
        //    param.Add(SqlParameters.Add("@Amount", SqlDbType.Decimal, 5, objCheckOutData.Amount, ParameterDirection.Input));
        //    param.Add(SqlParameters.Add("@Description", SqlDbType.VarChar, 50, objCheckOutData.Description, ParameterDirection.Input));
        //    param.Add(SqlParameters.Add("@ImageURL", SqlDbType.VarChar, 1000, objCheckOutData.ImageUrl, ParameterDirection.Input));

        //    // Additional parameters for missing attributes
        //    param.Add(SqlParameters.Add("@Id", SqlDbType.Int, 4, objCheckOutData.Id, ParameterDirection.Input));            // Id (integer)
        //    param.Add(SqlParameters.Add("@Name", SqlDbType.VarChar, 255, objCheckOutData.Name, ParameterDirection.Input));    // Name (string)
        //    param.Add(SqlParameters.Add("@Price", SqlDbType.Decimal, 5, objCheckOutData.Price, ParameterDirection.Input));    // Price (decimal)
        //    param.Add(SqlParameters.Add("@OriginalPrice", SqlDbType.Decimal, 5, objCheckOutData.OriginalPrice, ParameterDirection.Input)); // OriginalPrice (decimal)
        //    param.Add(SqlParameters.Add("@Discount", SqlDbType.Int, 4, objCheckOutData.Discount, ParameterDirection.Input));   // Discount (integer)
        //    param.Add(SqlParameters.Add("@Quantity", SqlDbType.Int, 4, objCheckOutData.Quantity, ParameterDirection.Input));   // Quantity (integer)
        //    param.Add(SqlParameters.Add("@MaxQuantity", SqlDbType.Int, 4, objCheckOutData.MaxQuantity, ParameterDirection.Input)); // MaxQuantity (integer)

        //    SqlManager.ExecuteNonQuery(CommandType.StoredProcedure, "Usp_InsertCartItem", param, 1, 1);

        //}

        public List<ItemDO> GetCartDetails(int UserId)
        {
            List<ItemDO> memberdetailsLST = new List<ItemDO>();
            List<SqlParameters> param = new List<SqlParameters>();
            param.Add(SqlParameters.Add("@UserId", SqlDbType.Int, 4, UserId, ParameterDirection.Input));

            using (SqlDataReader reader = SqlManager.ExecuteReader(CommandType.StoredProcedure, "Usp_GetCartDetails", param, 1, 1))
            {
                if (reader.HasRows)
                {
                    memberdetailsLST = BuildGetCartDetails(reader);
                }

                reader.Close();
            }
            return memberdetailsLST;
        }
        private List<ItemDO> BuildGetCartDetails(SqlDataReader reader)
        {
            List<ItemDO> cartDetails = new List<ItemDO>();

            try
            {
                while (reader.Read())
                {
                    cartDetails.Add(new ItemDO
                    {
                        Id = reader.IsDBNull(reader.GetOrdinal("Id")) ? 0 : reader.GetInt32(reader.GetOrdinal("Id")),
                        UserId = reader.IsDBNull(reader.GetOrdinal("UserId")) ? 0 : reader.GetInt32(reader.GetOrdinal("UserId")),
                        SessionId = reader.IsDBNull(reader.GetOrdinal("SessionId")) ? null : reader.GetString(reader.GetOrdinal("SessionId")),
                        Amount = reader.IsDBNull(reader.GetOrdinal("Amount")) ? 0 : reader.GetDecimal(reader.GetOrdinal("Amount")),
                        Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString(reader.GetOrdinal("Name")),
                        Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                        ImageUrl = reader.IsDBNull(reader.GetOrdinal("ImageUrl")) ? null : reader.GetString(reader.GetOrdinal("ImageUrl")),
                        Price = reader.IsDBNull(reader.GetOrdinal("Price")) ? 0 : reader.GetDecimal(reader.GetOrdinal("Price")),
                        OriginalPrice = reader.IsDBNull(reader.GetOrdinal("OriginalPrice")) ? 0 : reader.GetDecimal(reader.GetOrdinal("OriginalPrice")),
                        Discount = reader.IsDBNull(reader.GetOrdinal("Discount")) ? 0 : reader.GetInt32(reader.GetOrdinal("Discount")),
                        Quantity = reader.IsDBNull(reader.GetOrdinal("Quantity")) ? 0 : reader.GetInt32(reader.GetOrdinal("Quantity")),
                        MaxQuantity = reader.IsDBNull(reader.GetOrdinal("MaxQuantity")) ? 0 : reader.GetInt32(reader.GetOrdinal("MaxQuantity")),

                        Date = (DateTime)(reader.IsDBNull(reader.GetOrdinal("CheckoutDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("CheckoutDate")))
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                throw; // Rethrow for debugging purposes
            }

            return cartDetails;
        }



    }
}
