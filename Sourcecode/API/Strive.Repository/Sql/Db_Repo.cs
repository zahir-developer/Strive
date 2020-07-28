
namespace Strive.Repository
{
    public static class DbRepo
    {
        public static bool InsertPc<T>(T tview,string PrimaryField)
        {
            return true;
            //SqlServerBootstrap.Initialize();
            ////RepoDb.SqlServerInitializer();
            //using (var connection = new SqlConnection("").EnsureOpen())
            //{
                
   //             Type type = typeof(T);
   //             int primeId = 0;
   //             foreach (PropertyInfo prp in type.GetProperties())
   //             {
   //                 var model = prp.GetValue(tview, null);


   //                 if (primeId > 0)
   //                 {
   //                     //bool isList = false;
   //                     Type subModelType = model.GetType();

   //                     //if (subModelType.FullName.Contains("System.Collections.Generic.List"))
   //                     //{
   //                     //    isList = true;
   //                     //    tp = prp.PropertyType.GenericTypeArguments.First();
   //                     //}
   //                     //else
   //                     //{
   //                     //    tp = prp.PropertyType;
   //                     //}



   //                     //var subModel = prp.GetValue(tview, null);
                        
   //                     subModelType.GetProperty(PrimaryField).SetValue(model, primeId);
   //                     //var jString1 = JsonConvert.SerializeObject(model);
   //                     //var components = (IList)JsonConvert.DeserializeObject(jString1, typeof(List<>).MakeGenericType(new[] { subModelType }));
   //                     //(IList)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(prp.GetValue(tview, null)), typeof(List<>).MakeGenericType(new[] { type }));
   //                 }
   //                 var id = dbcon.Insert("[StriveCarSalon].tbl"+prp.Name, entity: model);
   //             }               
			}
          //  return true;
		}

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
