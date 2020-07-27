using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Strive.Repository
{
    public partial class Db
    {
        public T Get<T>(int id) where T : class
        {
            return SqlMapperExtensions.Get<T>(dbcon, id);
        }

        public bool Update<T>(T list) where T : class
        {
            return SqlMapperExtensions.Update(dbcon, list);
        }

        public long Insert<T>(T list) where T : class
        {
            return SqlMapperExtensions.Insert<T>(dbcon, list);
        }

        public List<T> GetAll<T>() where T : class
        {
            return SqlMapperExtensions.GetAll<T>(dbcon).ToList();
        }
    }
}
