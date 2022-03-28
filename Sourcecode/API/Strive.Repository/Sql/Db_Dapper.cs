using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Slapper;
using System.Collections;
using Newtonsoft.Json;

namespace Strive.Repository
{
    public partial class Db
    {
        IDbConnection dbcon;

        public Db(IDbConnection con)
        {
            dbcon = con;
        }

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

        public void Save(string sp, DynamicParameters dynParams)
        {
            CommandDefinition cmd = new CommandDefinition(sp, dynParams, commandType: CommandType.StoredProcedure);
            Save(cmd);
        }

        public object SaveGetId(CommandDefinition cmd)
        {
            object id = null;// default;
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

        public T Get<T>(string spName, DynamicParameters dynParam)
        {
            try
            {
                return dbcon.QuerySingleOrDefault<T>(spName, dynParam, commandType: CommandType.StoredProcedure, commandTimeout: 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public object SaveParentChild(List<(CommandDefinition, object)> lstCmd)
        {
            List<(string, int)> lstId = new List<(string, int)>();
            try
            {
                using (dbcon)
                {
                    if (dbcon?.State != ConnectionState.Open)
                    {
                        dbcon.Open();
                    }
                    using (var tran = dbcon.BeginTransaction())
                    {
                        try
                        {
                            foreach (var cmd in lstCmd)
                            {
                                if (cmd.Item2.GetType() == typeof(string))
                                {
                                    var newCmd = new CommandDefinition(cmd.Item1.CommandText, cmd.Item1.Parameters, tran, null, cmd.Item1.CommandType);
                                    lstId.Add((cmd.Item2.ToString(), (int)dbcon.ExecuteScalar(newCmd)));
                                }
                            }

                            var parent = lstCmd.Where(x => x.Item2.GetType() != typeof(string)).FirstOrDefault();
                            DataTable dt = (DataTable)parent.Item2;

                            foreach (var res in lstId)
                            {
                                //var ddt = ((dynamic)((Dapper.DynamicParameters)parent.Item1.Parameters).Get<dynamic>("tvpCashRegister")).table;

                                dt.Rows[0][res.Item1] = res.Item2;
                            }
                            DynamicParameters dyn = new DynamicParameters();
                            dyn.Add("@" + dt.TableName, dt.AsTableValuedParameter(dt.TableName));
                            var newCmd1 = new CommandDefinition(parent.Item1.CommandText, dyn, tran, null, parent.Item1.CommandType);
                            dbcon.Execute(newCmd1);

                            tran.Commit();
                        }
                        catch
                        {
                            tran.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
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


        public List<T> FetchRelation1<T, T1>(string spName, DynamicParameters dynParam)
        {
            dynamic fetchlist = dbcon.Query<dynamic>(spName, dynParam, commandType: CommandType.StoredProcedure);
            AutoMapper.Configuration.AddIdentifiers(typeof(T), new List<string> { typeof(T).Name.Replace("View", string.Empty) + "Id" });
            AutoMapper.Configuration.AddIdentifiers(typeof(T1), new List<string> { typeof(T1).Name + "Id" });
            var result = (AutoMapper.MapDynamic<T>(fetchlist) as IEnumerable<T>).ToList();
            return result;
        }

        public List<T> FetchRelation2<T, T1, T2>(string spName, DynamicParameters dynParam)
        {
            dynamic fetchlist = dbcon.Query<dynamic>(spName, dynParam, commandType: CommandType.StoredProcedure);
            AutoMapper.Configuration.AddIdentifiers(typeof(T), new List<string> { typeof(T).Name.Replace("View", string.Empty) + "Id" });
            AutoMapper.Configuration.AddIdentifiers(typeof(T1), new List<string> { typeof(T1).Name + "Id" });
            AutoMapper.Configuration.AddIdentifiers(typeof(T2), new List<string> { typeof(T2).Name + "Id" });
            var result = (AutoMapper.MapDynamic<T>(fetchlist) as IEnumerable<T>).ToList();
            return result;
        }

        public List<T> FetchRelation3<T, T1, T2, T3>(string spName, DynamicParameters dynParam)
        {
            dynamic fetchlist = dbcon.Query<dynamic>(spName, dynParam, commandType: CommandType.StoredProcedure);
            AutoMapper.Configuration.AddIdentifiers(typeof(T), new List<string> { typeof(T).Name.Replace("View", string.Empty) + "Id" });
            AutoMapper.Configuration.AddIdentifiers(typeof(T1), new List<string> { typeof(T1).Name + "Id" });
            AutoMapper.Configuration.AddIdentifiers(typeof(T2), new List<string> { typeof(T2).Name + "Id" });
            AutoMapper.Configuration.AddIdentifiers(typeof(T3), new List<string> { typeof(T3).Name + "Id" });
            var result = (AutoMapper.MapDynamic<T>(fetchlist) as IEnumerable<T>).ToList();
            return result;
        }

        public List<T> FetchRelation4<T, T1, T2, T3, T4>(string spName, DynamicParameters dynParam)
        {
            dynamic fetchlist = dbcon.Query<dynamic>(spName, dynParam, commandType: CommandType.StoredProcedure);
            AutoMapper.Configuration.AddIdentifiers(typeof(T), new List<string> { typeof(T).Name.Replace("View", string.Empty) + "Id" });
            AutoMapper.Configuration.AddIdentifiers(typeof(T1), new List<string> { typeof(T1).Name + "Id" });
            AutoMapper.Configuration.AddIdentifiers(typeof(T2), new List<string> { typeof(T2).Name + "Id" });
            AutoMapper.Configuration.AddIdentifiers(typeof(T3), new List<string> { typeof(T3).Name + "Id" });
            AutoMapper.Configuration.AddIdentifiers(typeof(T4), new List<string> { typeof(T4).Name + "Id" });
            var result = (AutoMapper.MapDynamic<T>(fetchlist) as IEnumerable<T>).ToList();
            return result;
        }


        public List<T> FetchWithRelation<T, T1>(string spName, string internalproperty = null)
            where T : class, new()
            where T1 : class, new()
        {
            List<T> lstpropdetails = new List<T>();


            var dict = new Dictionary<int, T>();
            lstpropdetails = dbcon.Query<T, T1, T>(spName, (t, t1) =>
                 {

                     T1 t1obj = new T1();
                     T tobj = (T)t;

                     t1obj = t1;
                     PropertyInfo prop = tobj.GetType().GetProperty(t1.GetType().Name);
                     var businessObjectPropValue = prop.GetValue(tobj, null);

                     prop.SetValue(tobj, t1, null);
                     return t;


                 },
                //param, 
                splitOn: internalproperty,
                commandType: CommandType.StoredProcedure)
              //.GroupBy(o => o.Propertycode)
              //.Select(group =>
              //{
              //    var combinedProperty = group.First();
              //    combinedProperty.Segments = group.Select(prp => prp.Segments.Single()).ToList();
              //    return combinedProperty;
              //})
              .ToList();

            return lstpropdetails;
        }



        public List<T> Fetch<T>(string spName, DynamicParameters dynParam)
        {
            List<T> fetchlist = new List<T>();
            try
            {
                var list = dbcon.Query<T>(spName, dynParam, commandType: CommandType.StoredProcedure);
                fetchlist = (List<T>)list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return fetchlist;
        }

        public T FetchSingle<T>(string spName, DynamicParameters dynParam)
        {
            T fetchlist;
            try
            {
                fetchlist = dbcon.QuerySingleOrDefault<T>(spName, dynParam, commandType: CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return fetchlist;
        }


        public T FetchMultiResult<T>(string spName, DynamicParameters dynParams) where T: new()
        {
            T tnew = new T();
            Type type = typeof(T);

            using (var multi = dbcon.QueryMultiple(spName, dynParams, commandType: CommandType.StoredProcedure))
            {
                foreach (PropertyInfo prp in type.GetProperties())
                {
                    FetchMultiResultSet(tnew, multi, prp);
                }
            }
            return tnew;
        }

        private void FetchMultiResultSet<T>(T tnew, SqlMapper.GridReader multi, PropertyInfo prp)
        {
            bool isList = false;
            Type tp;
            if (prp.PropertyType.FullName.Contains("System.Collections.Generic.List"))
            {
                isList = true;
                tp = prp.PropertyType.GenericTypeArguments.First();
            }
            else
            {
                tp = prp.PropertyType;
            }

            var source = multi.Read<dynamic>();
            var jString1 = JsonConvert.SerializeObject(source);
            var components = (IList)JsonConvert.DeserializeObject(jString1, typeof(List<>).MakeGenericType(new[] { tp }));

            if (components is IList && components.Count > 0)
            {
                var dbValue = isList ? components : components[0];
                prp.SetValue(tnew, dbValue, null);
            }
            else
            {
                prp.SetValue(tnew, null, null);
            }
        }

        public T FetchFirstResult<T>(string spName, DynamicParameters dynParam)
        {
            return dbcon.QueryFirst<T>(spName, dynParam, commandType: CommandType.StoredProcedure);
        }

        public int Execute<T>(string spName, DynamicParameters dynParam)
        {
            var result = dbcon.Execute(spName, dynParam, commandType: CommandType.StoredProcedure);

            return result;
        }
        public T QuerySingleOrDefault<T>(string spName, DynamicParameters dynParm)
        {
            return dbcon.QuerySingleOrDefault<T>(spName, dynParm, commandType: CommandType.StoredProcedure);
        }
    }
}
