using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace FurnitureDAL
{

    public struct SqlParameters
    {
        public string parameterName;
        public SqlDbType dbType;
        public int size;
        public object value;
        public ParameterDirection direction;
        public static SqlParameters Add(string parameterName, SqlDbType dbType, int size, object value, ParameterDirection parameterDirection)
        {
            SqlParameters sqlParameters = new SqlParameters();
            sqlParameters.parameterName = parameterName;
            sqlParameters.dbType = dbType;
            sqlParameters.size = size;
            sqlParameters.value = value;
            sqlParameters.direction = parameterDirection;
            return sqlParameters;
        }
    }
    public sealed class SqlManager
    {
        #region Properties
        public static int ApprovalString { get; set; }
        public static int CompanyCode { get; set; }
        #endregion

        #region Connection String

        #endregion

        public static SqlConnection GetconnectionString(int ApprovalString, int CompanyCode)
        {
            string ConnString = "";
            switch (ApprovalString)
            {
                case 1:
                    ConnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                    break;
                case 2:
                    ConnString = ConfigurationManager.ConnectionStrings["ConnectionStringFurniture"].ConnectionString;
                    break;
                default:
                    ConnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                    break;


            }



            return new SqlConnection(ConnString);


        }


        #region Prepare Command
        private static void PrepareCommand(SqlCommand sqlCommand, SqlTransaction sqlTransaction, CommandType commandType, string commandText, List<SqlParameters> commandParameters, int dBPathId, int CompCode)
        {
            // sqlCommand.Connection = connection;

            sqlCommand.Connection = GetconnectionString(dBPathId, CompCode);
            if (sqlCommand.Connection.State != ConnectionState.Open)
            {
                sqlCommand.Connection.Open();
            }

            sqlCommand.CommandText = commandText;
            sqlCommand.CommandTimeout = 60;
            if (sqlTransaction != null)
            {
                sqlCommand.Transaction = sqlTransaction;
            }

            sqlCommand.CommandType = commandType;
            if (commandParameters != null)
            {
                foreach (SqlParameters paramNode in commandParameters)
                {
                    SqlParameter sqlParameter = new SqlParameter(paramNode.parameterName, paramNode.dbType, paramNode.size);
                    sqlParameter.Value = (sqlParameter.Direction == ParameterDirection.InputOutput) && (sqlParameter.Value == null) ? DBNull.Value : paramNode.value;
                    sqlCommand.Parameters.Add(sqlParameter);
                }

            }

            return;
        }
        #endregion

        #region Execute Reader
        public static SqlDataReader ExecuteReader(CommandType commandType, string commandText, List<SqlParameters> commandParameters, int dBPathId, int CompCode)
        {
            InitiliazeVariable(dBPathId, CompCode);

            SqlConnection Connection = GetconnectionString(dBPathId, CompCode);
            using (Connection)
            {
                SqlDataReader sqlDataReader;
                SqlCommand sqlCommand = new SqlCommand();
                PrepareCommand(sqlCommand, (SqlTransaction)null, commandType, commandText, commandParameters, dBPathId, CompCode);

                try
                {
                    sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    sqlCommand.Dispose();
                }
                return sqlDataReader;
            }
        }
        #endregion

        #region Execute NonQuery
        public static int ExecuteNonQuery(CommandType commandType, string commandText, List<SqlParameters> commandParameters, int dBPathId, int CompCode)
        {
            InitiliazeVariable(dBPathId, CompCode);

            SqlConnection Connection = GetconnectionString(dBPathId, CompCode);
            using (Connection)
            {
                int retval;
                SqlCommand sqlCommand = new SqlCommand();
                PrepareCommand(sqlCommand, (SqlTransaction)null, commandType, commandText, commandParameters, dBPathId, CompCode);

                try
                {
                    retval = sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    sqlCommand.Connection.Close();
                    sqlCommand.Dispose();
                }
                return retval;
            }
        }
        #endregion

        #region Excecute Scalar
        public static object ExecuteScalar(CommandType commandType, string commandText, List<SqlParameters> commandParameters, int dBPathId, int CompCode)
        {
            InitiliazeVariable(dBPathId, CompCode);

            SqlConnection Connection = GetconnectionString(dBPathId, CompCode);
            using (Connection)
            {
                object retval;
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, (SqlTransaction)null, commandType, commandText, commandParameters, dBPathId, CompCode);

                try
                {
                    retval = cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    cmd.Connection.Close();
                    cmd.Dispose();
                }
                return retval;
            }
        }
        #endregion

        #region ExecuteDataSet
        public static DataSet ExecuteDataSet(CommandType commandType, string commandText, List<SqlParameters> commandParameters, int dBPathId, int CompCode)
        {
            InitiliazeVariable(dBPathId, CompCode);

            SqlConnection Connection = GetconnectionString(dBPathId, CompCode);
            using (Connection)
            {
                SqlCommand sqlCommand = new SqlCommand();
                PrepareCommand(sqlCommand, (SqlTransaction)null, commandType, commandText, commandParameters, dBPathId, CompCode);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataSet dataSet = new DataSet();

                try
                {
                    sqlDataAdapter.Fill(dataSet);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    sqlCommand.Connection.Close();
                    sqlCommand.Dispose();
                }

                return dataSet;
            }
        }
        #endregion

        #region ExecuteDataTable
        public static DataTable ExecuteDataTable(CommandType commandType, string commandText, List<SqlParameters> commandParameters, int dBPathId, int CompCode)
        {
            InitiliazeVariable(dBPathId, CompCode);

            SqlConnection Connection = GetconnectionString(dBPathId, CompCode);
            using (Connection)
            {
                SqlCommand sqlCommand = new SqlCommand();
                PrepareCommand(sqlCommand, (SqlTransaction)null, commandType, commandText, commandParameters, dBPathId, CompCode);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();

                try
                {
                    sqlDataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    sqlCommand.Connection.Close();
                    sqlCommand.Dispose();
                }
                return dataTable;
            }
        }
        #endregion

        #region Common Methods
        public static void InitiliazeVariable(int approvalString, int CompCode)
        {
            ApprovalString = approvalString;
            CompanyCode = CompCode;
        }
        #endregion



    }
}
