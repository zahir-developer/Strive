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
            if (tview == null)
                return true;

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

                            if (model is null || model.ToString() == "string" || model.GetType().BaseType == typeof(Enum))
                                continue;


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

        public bool SaveAll<T>(T tview, string PrimaryField)
        {
            if (tview == null)
                return true;

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
                        bool isGeneric = false;
                        foreach (PropertyInfo prp in type.GetProperties())
                        {
                            var model = prp.GetValue(tview, null);

                            if (model is null || model.GetType() == typeof(string)) continue;

                            Type subModelType = model.GetType();
                            if (subModelType.IsGenericType)
                            {
                                isGeneric = true;
                            }

                            if (primInsert)
                            {
                                subModelType = model.GetType();
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

                                List<object> insertList = new List<object>();
                                List<object> updateList = new List<object>();
                                foreach (var item in dynamicListObject)
                                {
                                    var prInfo = item.GetType().GetProperties().FirstOrDefault().GetValue(item, null) ?? 0;
                                    if (Convert.ToInt32(prInfo) == 0)
                                        insertList.Add(item);
                                    else
                                        updateList.Add(item);
                                }

                                if(insertList.Count > 0)
                                    insertId = (int)dbcon.InsertAll($"{sc}.tbl" + prp.Name, entities: (IEnumerable<object>)insertList, transaction: transaction);

                                if(updateList.Count > 0)
                                insertId = (int)dbcon.UpdateAll($"{sc}.tbl" + prp.Name, entities: (IEnumerable<object>)updateList, transaction: transaction);

                                isGeneric = false;

                            }
                            else
                            {
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

                            isGeneric = false;
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

        public int InsertPK<T>(T tview, string PrimaryField)
        {

            SqlServerBootstrap.Initialize();
            DbHelperMapper.Add(typeof(SqlConnection), new SqlServerDbHelperNew(), true);
            int primeId = 0;
            int pkId = 0;
            using (var dbcon = new SqlConnection(cs).EnsureOpen())
            {

                Type type = typeof(T);
                primeId = 0;
                bool primInsert = false;
                bool isGeneric = false;
                int insertId = 0;
                int genericInsertId = 0;
                using (var transaction = dbcon.BeginTransaction())
                {
                    try
                    {
                        foreach (PropertyInfo prp in type.GetProperties())
                        {
                            var model = prp.GetValue(tview, null);

                            if (model is null || model.GetType() == typeof(string) || model.GetType() == typeof(Guid) || model.GetType().BaseType == typeof(Enum)) continue;

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
                                if (dynamicListObject.Count > 0)
                                    genericInsertId = (int)dbcon.MergeAll($"{sc}.tbl" + prp.Name, entities: (IEnumerable<object>)dynamicListObject, transaction: transaction);
                                isGeneric = false;
                            }
                            else
                            {
                                insertId = (int)dbcon.Insert($"{sc}.tbl" + prp.Name, entity: model, transaction: transaction);
                                pkId = Convert.ToInt32(insertId);
                            }

                            primeId = (!primInsert) ? insertId : primeId;
                            primInsert = true;
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
            return primeId > 0 ? primeId : pkId;
        }


        public bool InsertPc<T>(T tview, string PrimaryField)
        {
            if (tview == null)
                return true;

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

                            if (model is null || model.ToString() == "string" || model.GetType() == typeof(int) || model.GetType().BaseType == typeof(Enum)) continue;

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
                                if (dynamicListObject.Count > 0)
                                    insertId = (int)dbcon.MergeAll($"{sc}.tbl" + prp.Name, entities: (IEnumerable<object>)dynamicListObject, transaction: transaction);
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

        public bool InsertInt64<T>(T tview, string PrimaryField)
        {

            SqlServerBootstrap.Initialize();
            DbHelperMapper.Add(typeof(SqlConnection), new SqlServerDbHelperNew(), true);

            using (var dbcon = new SqlConnection(cs).EnsureOpen())
            {

                Type type = typeof(T);
                long primeId = 0;
                bool primInsert = false;
                bool isGeneric = false;
                long insertId = 0;
                using (var transaction = dbcon.BeginTransaction())
                {
                    try
                    {
                        foreach (PropertyInfo prp in type.GetProperties())
                        {
                            var model = prp.GetValue(tview, null);

                            if (model is null || model.GetType() == typeof(string)) continue;

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
                                        var prop = subModelType.GetProperty(PrimaryField);
                                        if (prop != null)
                                            smt.GetProperty(PrimaryField)?.SetValue(m, primeId);
                                    }

                                    model = components;
                                }
                                else
                                {
                                    var prop = subModelType.GetProperty(PrimaryField);
                                    if (prop != null)
                                        subModelType.GetProperty(PrimaryField).SetValue(model, primeId);
                                }
                            }

                            if (isGeneric)
                            {
                                var dynamicListObject = (IList)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(model), typeof(List<>).MakeGenericType(new[] { model.GetType().GenericTypeArguments.First() }));
                                if (dynamicListObject.Count > 0)
                                    insertId = (long)dbcon.MergeAll($"{sc}.tbl" + prp.Name, entities: (IEnumerable<object>)dynamicListObject, transaction: transaction);
                                isGeneric = false;
                            }
                            else
                            {
                                var Id = dbcon.Insert($"{sc}.tbl" + prp.Name, entity: model, transaction: transaction);
                                insertId = Convert.ToInt64(Id);
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

        public bool UpdatePc<T>(T tview, string baseTable = null)
        {
            if (tview == null)
                return true;

            SqlServerBootstrap.Initialize();
            DbHelperMapper.Add(typeof(SqlConnection), new SqlServerDbHelperNew(), true);

            using (var dbcon = new SqlConnection(cs).EnsureOpen())
            {

                Type type = typeof(T);
                bool isGeneric = false;
                int insertId = 0;
                using (var transaction = dbcon.BeginTransaction())
                {
                    try
                    {
                        foreach (PropertyInfo prp in type.GetProperties())
                        {
                            var model = prp.GetValue(tview, null);

                            if (model is null || model.GetType() == typeof(string) || model.GetType().BaseType == typeof(Enum)) continue;

                            Type subModelType = model.GetType();

                            if (subModelType.IsGenericType)
                            {
                                isGeneric = true;
                            }

                            if (isGeneric)
                            {
                                var dynamicListObject = (IList)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(model), typeof(List<>).MakeGenericType(new[] { model.GetType().GenericTypeArguments.First() }));

                                /*if (dynamicListObject.Count > 0)
                                    insertId = (int)dbcon.MergeAll($"{sc}.tbl" + prp.Name, entities: (IEnumerable<object>)dynamicListObject, transaction: transaction);
                                isGeneric = false;
                                */
                                
                                List<object> insertList = new List<object>();
                                List<object> updateList = new List<object>();
                                foreach (var item in dynamicListObject)
                                {
                                    var prInfo = item.GetType().GetProperties().FirstOrDefault().GetValue(item, null) ?? 0;
                                    if (Convert.ToInt32(prInfo) == 0)
                                        insertList.Add(item);
                                    else
                                        updateList.Add(item);
                                }

                                if (insertList.Count > 0)
                                    insertId = (int)dbcon.InsertAll($"{sc}.tbl" + prp.Name, entities: (IEnumerable<object>)insertList, transaction: transaction);

                                if (updateList.Count > 0)
                                    insertId = (int)dbcon.UpdateAll($"{sc}.tbl" + prp.Name, entities: (IEnumerable<object>)updateList, transaction: transaction);

                                isGeneric = false;
                            }
                            else
                            {

                                //if (baseTable !=null ? prp.Name == ("BaySchedule")|| prp.Name ==( "JobDetail") : false)
                                var propValue = GetPropertyValue(model, prp.Name.Replace("ClientVehicle", "Vehicle") + "Id");
                                if (propValue == 0 && propValue != null)
                                {
                                    var Id = dbcon.Insert($"{sc}.tbl" + prp.Name, entity: model, transaction: transaction);
                                }
                                else
                                    insertId = (int)dbcon.Update($"{sc}.tbl" + prp.Name, entity: model, transaction: transaction);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                    transaction.Commit();
                }
            }
            return true;
        }

        public static int? GetPropertyValue(object model, string propertyName)
        {
            var value = model.GetType()?.GetProperty(propertyName)?.GetValue(model, null);

            if (value != null)
                return (int)value;
            else
                return null;
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
            if (tview == null)
                return true;

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
