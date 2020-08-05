using Cocoon.ORM;
using Dapper;
//using FastDeepCloner;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;

namespace Strive.Repository
{
    public partial class Db
    {
        public List<T> GetListByFkId<T>(int id, string fkField)
        {
            return new CocoonORM(dbcon.ConnectionString).GetList<T>(where: Exp<T>(id, fkField)).ToList();
        }
        public T GetSingleByFkId<T>(int id, string fkField)
        {
            return new CocoonORM(dbcon.ConnectionString).GetList<T>(where: Exp1<T>(id, fkField)).ToList().FirstOrDefault();
        }

        public List<T> DoSearch<T>(string searchTerm)
        {
            return null;
            //return new CocoonORM(dbcon.ConnectionString).GetList<T>(where: Exp<T>(id, fkField)).ToList();
        }




        private static Expression<Func<T, bool>> Exp<T>(int id, string fkField)
        {
            ParameterExpression argParam = Expression.Parameter(typeof(T), "s");
            Expression nameProperty = Expression.Property(argParam, fkField);

            Expression exp = Expression.Equal(nameProperty, Expression.Constant(id));

            return Expression.Lambda<Func<T, bool>>(exp, argParam);
        }

        private static Expression<Func<T, bool>> Exp1<T>(int id, string fkField)
        {
            //var model = new T();

            ParameterExpression argParam = Expression.Parameter(typeof(T), "s");
            Expression nameProperty = Expression.Property(argParam, fkField);
            Expression exp = Expression.Equal(nameProperty, Expression.Constant(id));

            ParameterExpression argParam1 = Expression.Parameter(typeof(T), "active");
            Expression nameProperty1 = Expression.Property(argParam1, "IsActive");
            Expression exp1 = Expression.Equal(nameProperty1, Expression.Constant(true));


            ParameterExpression argParam2 = Expression.Parameter(typeof(T), "deleted");
            Expression nameProperty2 = Expression.Property(argParam2, "IsDeleted");
            Expression exp2 = Expression.Equal(nameProperty2, Expression.Constant(false));

            Expression<Func<T, bool>> predicates = Expression.Lambda<Func<T, bool>>(exp, argParam);// c => model != null;//start with an always true/false predicate to get started

            predicates = predicates.And(Expression.Lambda<Func<T, bool>>(exp1, argParam1));
            predicates = predicates.And(Expression.Lambda<Func<T, bool>>(exp2, argParam2));


            //List<ParameterExpression> lstParam = new List<ParameterExpression>();


            //ParameterExpression argParam = Expression.Parameter(typeof(T), "s");
            //Expression nameProperty = Expression.Property(argParam, fkField);

            //ParameterExpression argParam1 = Expression.Parameter(typeof(T), "active");
            //Expression nameProperty1 = Expression.Property(argParam1, "IsActive");

            //ParameterExpression argParam2 = Expression.Parameter(typeof(T), "deleted");
            //Expression nameProperty2 = Expression.Property(argParam2, "IsDeleted");

            //Expression exp = Expression.Equal(nameProperty, Expression.Constant(id));        
            //Expression exp1 = Expression.Equal(nameProperty1, Expression.Constant(true));
            //Expression exp2 = Expression.Equal(nameProperty2, Expression.Constant(false));

            //Expression expFinal = Expression.IsTrue(exp)



            //lstParam.Add(argParam);
            //lstParam.Add(argParam1);
            //lstParam.Add(argParam2);

            ////return Expression.Lambda<Func<T, bool>>(exp, argParam);
            return predicates;
        }


        public void Save(string sp, DynamicParameters dynParams)
        {
            CommandDefinition cmd = new CommandDefinition(sp, dynParams, commandType: CommandType.StoredProcedure);
            Save(cmd);
        }

        //dynamic expando = new ExpandoObject();

        //var prop = DeepCloner.GetFastDeepClonerProperties(typeof(T));
        //foreach(var prp in prop)
        //{
        //    AddProperty(expando, "Language", "English");
        //    //prp.Attributes.Find(x=> typeof(x) == typeof(Column))
        //    //prp.Attributes.Add(new Column());
        //}



        //public static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
        //{
        //    // ExpandoObject supports IDictionary so we can extend it like this
        //    var expandoDict = expando as IDictionary<string, object>;
        //    if (expandoDict.ContainsKey(propertyName))
        //        expandoDict[propertyName] = propertyValue;
        //    else
        //        expandoDict.Add(propertyName, propertyValue);
        //}
    }
}
