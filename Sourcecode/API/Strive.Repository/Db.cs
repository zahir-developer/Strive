using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Repository
{
    public class Db : BaseRepository
    {
        public void Save(CommandDefinition cmd)
        {
            try
            {
                dbcon.Execute(cmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object SaveGetId(CommandDefinition cmd)
        {
            object id = default;
            try
            {
                   id = dbcon.ExecuteScalar(cmd);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return id;
        }


        public dynamic FetchTwoResults<T, T1>(string spName, DynamicParameters dynParam)
        {
            List<T> Result1 = null;
            List<T1> Result2 = null;
            try
            {

                    using (var fetchlist1 = dbcon.QueryMultiple(spName, dynParam, commandType: CommandType.StoredProcedure, commandTimeout: 0))
                    {
                        Result1 = fetchlist1.Read<T>().ToList();
                        Result2 = fetchlist1.Read<T1>().ToList();
                    }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (Result1, Result2);
        }

        public (List<T>, List<T1>) FetchDualResults<T, T1>(string spName, DynamicParameters dynParam)
        {
            IEnumerable<T> Result1;
            IEnumerable<T1> Result2;

                var Result = dbcon.QueryMultiple(spName, dynParam, commandType: CommandType.StoredProcedure);
                Result1 = Result.Read<T>();
                Result2 = Result.Read<T1>();

            return (Result1.ToList(), Result2.ToList());
        }

        public List<T> Fetch<T>(string spName, DynamicParameters dynParam)
        {
            List<T> fetchlist;
            try
            {
                fetchlist = (List<T>)dbcon.Query<T>(spName, dynParam, commandType: CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return fetchlist;
        }

    }
}
