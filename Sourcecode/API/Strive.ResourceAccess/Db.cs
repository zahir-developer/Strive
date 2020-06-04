using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.ResourceAccess
{
    public class Db
    {
        IDbConnection dbcon = null;
        public void Save(string connectionString, CommandDefinition cmd)
        {
            try
            {
                using (dbcon = new SqlConnection(connectionString))
                {
                    if (dbcon?.State != ConnectionState.Open)
                    {
                        dbcon.Open();
                    }

                    dbcon.Execute(cmd);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object SaveGetId(string connectionString, CommandDefinition cmd)
        {
            object id = default;
            try
            {
                using (dbcon = new SqlConnection(connectionString))
                {
                    if (dbcon?.State != ConnectionState.Open)
                    {
                        dbcon.Open();
                    }

                    id = dbcon.ExecuteScalar(cmd);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }


        public dynamic FetchTwoResults<T, T1>(string connectionString, string spName, DynamicParameters dynParam)
        {
            List<T> Result1 = null;
            List<T1> Result2 = null;
            try
            {
                using (dbcon = new SqlConnection(connectionString))
                {
                    if (dbcon?.State != ConnectionState.Open)
                    {
                        dbcon.Open();
                    }

                    using (var fetchlist1 = dbcon.QueryMultiple(spName, dynParam, commandType: CommandType.StoredProcedure, commandTimeout: 0))
                    {
                        Result1 = fetchlist1.Read<T>().ToList();
                        Result2 = fetchlist1.Read<T1>().ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (Result1, Result2);
        }

        public (List<T>, List<T1>) FetchDualResults<T, T1>(string connectionString, string spName, DynamicParameters dynParam)
        {
            IEnumerable<T> Result1;
            IEnumerable<T1> Result2;
            using (dbcon = new SqlConnection(connectionString))
            {
                if (dbcon?.State != ConnectionState.Open)
                {
                    dbcon.Open();
                }

                var Result = dbcon.QueryMultiple(spName, dynParam, commandType: CommandType.StoredProcedure);
                Result1 = Result.Read<T>();
                Result2 = Result.Read<T1>();
            }
            return (Result1.ToList(), Result2.ToList());
        }

        public List<T> Fetch<T>(string connectionString, string spName, DynamicParameters dynParam)
        {
            List<T> fetchlist;
            try
            {

                using (dbcon = new SqlConnection(connectionString))
                {
                    if (dbcon?.State != ConnectionState.Open)
                    {
                        dbcon.Open();
                    }

                    fetchlist = (List<T>)dbcon.Query<T>(spName, dynParam, commandType: CommandType.StoredProcedure);

                }
           }
            catch (Exception ex)
            {
                throw ex;
            }
            return fetchlist;
        }


        public DataTable FetchTable(string connectionString, string spName, List<SqlParameter> parameterList)
        {
            DataTable fetchTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();

                    SqlCommand command = new SqlCommand(spName, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    foreach (SqlParameter parameter in parameterList)
                    {
                        command.Parameters.Add(parameter);
                    }

                    adapter.SelectCommand = command;
                    adapter.Fill(fetchTable);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return fetchTable;
        }
    }
}
