
using Microsoft.Data.SqlClient;
using RepoDb;
using RepoDb.SqlServer;
using System;
using System.Linq;
using System.Reflection;

namespace Strive.RepositoryCqrs
{
    public static class DbRepo
    {
        public static bool InsertPc<T>(T tview, string PrimaryField, string cs, string sc)
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
            

        public static bool UpdatePc<T>(T tview, string PrimaryField, string cs, string sc)
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
