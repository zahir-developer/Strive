
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using RepoDb;
using RepoDb.SqlServer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Strive.RepositoryCqrs
{
    public class DbRepo
    {
        private string cs;
        private string sc;

        public DbRepo(string cs, string schemaName)
        {
            this.cs = cs;
            this.sc = schemaName;
        }

        public bool Insert<T>(T tview)
        {

            SqlServerBootstrap.Initialize();
            DbHelperMapper.Add(typeof(SqlConnection), new SqlServerDbHelperNew(), true);

            using (var dbcon = new SqlConnection(cs).EnsureOpen())
            {
                Type type = typeof(T);
                using (var transaction = dbcon.BeginTransaction())
                {
                    try
                    {
                        var insertId = (int)dbcon.Insert($"{sc}.tbl" + type.Name, entity: tview, transaction: transaction);
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                    transaction.Commit();
                }
            }
            return true;
        }


        public bool SavePc<T>(T tview, string PrimaryField)
        {
            SqlServerBootstrap.Initialize();
            DbHelperMapper.Add(typeof(SqlConnection), new SqlServerDbHelperNew(), true);

            using (var dbcon = new SqlConnection(cs).EnsureOpen())
            {

                Type type = typeof(T);
                int primeId = 0;
                bool primInsert = false;
                int insertId = 0;
                using (var transaction = dbcon.BeginTransaction())
                {
                    try
                    {
                        foreach (PropertyInfo prp in type.GetProperties())
                        {
                            var model = prp.GetValue(tview, null);


                            if (primInsert)
                            {
                                Type subModelType = model.GetType();
                                subModelType.GetProperty(PrimaryField).SetValue(model, primeId);
                            }

                            var prInfo = model.GetType().GetProperties().FirstOrDefault().GetValue(model, null) ?? 0;
                            if (Convert.ToInt32(prInfo) > 0)
                            {
                                var Updated = (int)dbcon.Update($"{sc}.tbl" + prp.Name, entity: model, transaction: transaction);
                            }
                            else
                            {
                                insertId = (int)dbcon.Insert($"{sc}.tbl" + prp.Name, entity: model, transaction: transaction);
                                primeId = (!primInsert) ? insertId : primeId;
                                primInsert = true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                    transaction.Commit();
                }
            }
            return true;
        }

        public int Save<T>(T tview, string PrimaryField)
        {
            SqlServerBootstrap.Initialize();
            DbHelperMapper.Add(typeof(SqlConnection), new SqlServerDbHelperNew(), true);
            int Id = 0;
            using (var dbcon = new SqlConnection(cs).EnsureOpen())
            {

                Type type = typeof(T);
                int primeId = 0;
                bool primInsert = false;

                using (var transaction = dbcon.BeginTransaction())
                {
                    try
                    {
                        foreach (PropertyInfo prp in type.GetProperties())
                        {
                            var model = prp.GetValue(tview, null);


                            if (primInsert)
                            {
                                Type subModelType = model.GetType();
                                subModelType.GetProperty(PrimaryField).SetValue(model, primeId);
                            }

                            var prInfo = model.GetType().GetProperties().FirstOrDefault().GetValue(model, null) ?? 0;
                            if (Convert.ToInt32(prInfo) > 0)
                            {
                                Id = (int)dbcon.Update($"{sc}.tbl" + prp.Name, entity: model, transaction: transaction);
                            }
                            else
                            {
                                Id = (int)dbcon.Insert($"{sc}.tbl" + prp.Name, entity: model, transaction: transaction);
                                primeId = (!primInsert) ? Id : primeId;
                                primInsert = true;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                    transaction.Commit();
                }
            }
            return Id;
        }

        public bool Update<T>(T tview)
        {

            SqlServerBootstrap.Initialize();
            DbHelperMapper.Add(typeof(SqlConnection), new SqlServerDbHelperNew(), true);

            using (var dbcon = new SqlConnection(cs).EnsureOpen())
            {
                Type type = typeof(T);
                using (var transaction = dbcon.BeginTransaction())
                {
                    try
                    {
                        var id = dbcon.Update($"{sc}.tbl" + type.Name, entity: tview, transaction: transaction);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                    transaction.Commit();
                }
            }
            return true;
        }


        public bool InsertPc<T>(T tview, string PrimaryField)
        {

            SqlServerBootstrap.Initialize();
            DbHelperMapper.Add(typeof(SqlConnection), new SqlServerDbHelperNew(), true);

            using (var dbcon = new SqlConnection(cs).EnsureOpen())
            {

                Type type = typeof(T);
                int primeId = 0;
                bool primInsert = false;
                bool isGeneric = false;
                int insertId = 0;
                using (var transaction = dbcon.BeginTransaction())
                {
                    try
                    {
                        foreach (PropertyInfo prp in type.GetProperties())
                        {
                            var model = prp.GetValue(tview, null);

                            if (model is null) continue;

                            Type subModelType = model.GetType();

                            if (subModelType.IsGenericType)
                            {
                                isGeneric = true;
                            }
                            if (primInsert)
                            {
                                
                                if (subModelType.IsGenericType)
                                {
                                    isGeneric = true;
                                    var jString = JsonConvert.SerializeObject(model);
                                    var components = (IList)JsonConvert.DeserializeObject(jString, typeof(List<>).MakeGenericType(new[] { model.GetType().GenericTypeArguments.First() }));

                                    foreach (var m in components)
                                    {
                                        var smt = m.GetType();
                                        smt.GetProperty(PrimaryField).SetValue(m, primeId);
                                    }

                                    model = components;
                                }
                                else
                                {
                                    subModelType.GetProperty(PrimaryField).SetValue(model, primeId);
                                }
                            }

                            if (isGeneric)
                            {
                                var dynamicListObject = (IList)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(model), typeof(List<>).MakeGenericType(new[] { model.GetType().GenericTypeArguments.First() }));
                                insertId = (int)dbcon.InsertAll($"{sc}.tbl" + prp.Name, entities: (IEnumerable<object>)dynamicListObject, transaction: transaction);
                                isGeneric = false;
                            }
                            else
                            {
                                insertId = (int)dbcon.Insert($"{sc}.tbl" + prp.Name, entity: model, transaction: transaction);
                            }

                            primeId = (!primInsert) ? insertId : primeId;
                            primInsert = true;
                        }
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                    transaction.Commit();
                }
            }
            return true;
        }


        public bool UpdatePc<T>(T tview)
        {

            SqlServerBootstrap.Initialize();
            DbHelperMapper.Add(typeof(SqlConnection), new SqlServerDbHelperNew(), true);

            using (var dbcon = new SqlConnection(cs).EnsureOpen())
            {
                Type type = typeof(T);

                using (var transaction = dbcon.BeginTransaction())
                {
                    try
                    {
                        foreach (PropertyInfo prp in type.GetProperties())
                        {
                            var model = prp.GetValue(tview, null);

                            if (model is null) continue;

                            var id = dbcon.Update($"{sc}.tbl" + prp.Name, entity: model, transaction: transaction);
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                    transaction.Commit();
                }
            }
            return true;
        }

        public int Add<T>(T tview)
        {

            SqlServerBootstrap.Initialize();
            DbHelperMapper.Add(typeof(SqlConnection), new SqlServerDbHelperNew(), true);
            int insertId;
            using (var dbcon = new SqlConnection(cs).EnsureOpen())
            {
                Type type = typeof(T);
                using (var transaction = dbcon.BeginTransaction())
                {
                    try
                    {
                        insertId = (int)dbcon.Insert($"{sc}.tbl" + type.Name, entity: tview, transaction: transaction);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                    transaction.Commit();
                }
            }
            return insertId;
        }

        public bool InsertAll<T>(T tview, string PrimaryField)
        {

            SqlServerBootstrap.Initialize();
            DbHelperMapper.Add(typeof(SqlConnection), new SqlServerDbHelperNew(), true);

            using (var dbcon = new SqlConnection(cs).EnsureOpen())
            {

                Type type = typeof(T);
                int primeId = 0;
                bool primInsert = false;
                using (var transaction = dbcon.BeginTransaction())
                {
                    try
                    {
                        foreach (PropertyInfo prp in type.GetProperties())
                        {
                            var model = prp.GetValue(tview, null);

                            if (model.GetType().Name.Contains("List"))
                            {
                                var list = (List<object>)model;

                                var id = (int)dbcon.InsertAll($"{sc}.tbl" + prp.Name, entities: list, transaction: transaction);
                            }

                            if (primInsert)
                            {
                                Type subModelType = model.GetType();
                                subModelType.GetProperty(PrimaryField).SetValue(model, primeId);
                            }

                            var insertId = (int)dbcon.Insert($"{sc}.tbl" + prp.Name, entity: model, transaction: transaction);
                            primeId = (!primInsert) ? insertId : primeId;
                            primInsert = true;
                        }
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                    transaction.Commit();
                }
            }
            return true;
        }



        //public static void Delete<T>(string primeField, int id, string cs, string schemaName) where T : class, new()
        //{
        //    SqlServerBootstrap.Initialize();
        //    DbHelperMapper.Add(typeof(SqlConnection), new SqlServerDbHelperNew(), true);

        //    using (var dbcon = new SqlConnection(cs).EnsureOpen())
        //    {
        //        dbcon.Update("tbl" + ClassMappedNameCache.Get<T>(), tdata);
        //        //dbcon.Update("tbl" + ClassMappedNameCache.Get<T>(), new { primeField.ToString() = id, IsDeleted = true });
        //    }
        //}

        //public static T Get<T>(int id)
        //{
        //    SqlServerBootstrap.Initialize();
        //    DbHelperMapper.Add(typeof(SqlConnection), new SqlServerDbHelperNew(), true);

        //    using (var dbcon = new SqlConnection(cs).EnsureOpen())
        //    {
        //        dbcon.
        //    }
        //}

        //public static Child ConvertToChildObject(this PropertyInfo propertyInfo, object parent)
        //{
        //    var source = propertyInfo.GetValue(parent, null);
        //    var destination = Activator.CreateInstance(propertyInfo.PropertyType);

        //    foreach (PropertyInfo prop in destination.GetType().GetProperties().ToList())
        //    {
        //        var value = source.GetType().GetProperty(prop.Name).GetValue(source, null);
        //        prop.SetValue(destination, value, null);
        //    }

        //    return (Child)destination;
        //}

        //    public static TPropertyType ConvertToChildObject<TInstanceType, TPropertyType>(this PropertyInfo propertyInfo, TInstanceType instance)
        //where TInstanceType : class, new()
        //    {
        //        if (instance == null)
        //            instance = Activator.CreateInstance<TInstanceType>();

        //        //var p = (Child)propertyInfo;
        //        return (TPropertyType)propertyInfo.GetValue(instance);

        //    }
        // }
    }
}
