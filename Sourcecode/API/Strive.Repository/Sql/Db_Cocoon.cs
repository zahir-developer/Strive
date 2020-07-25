using Cocoon.ORM;
using Dapper;
//using FastDeepCloner;
using System;
using System.Collections.Generic;
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

        private static Expression<Func<T, bool>> Exp<T>(int id, string fkField)
        {
            ParameterExpression argParam = Expression.Parameter(typeof(T), "s");
            Expression nameProperty = Expression.Property(argParam, fkField);
            Expression exp = Expression.Equal(nameProperty, Expression.Constant(id));
            return Expression.Lambda<Func<T, bool>>(exp, argParam);
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
