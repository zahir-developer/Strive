using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Strive.Common
{
    public interface ITenantHelper
    {
        IDbConnection db();
        void SetConnection(string con);
    }

    public class TenantHelper : ITenantHelper
    {
        public string stringCurrentConnection = string.Empty;
        IDistributedCache _cache;
        public TenantHelper(IDistributedCache cache)
        {
            _cache = cache;
        }

        public void SetConnection(string con)
        {
            stringCurrentConnection = con;
        }

        public IDbConnection db()
        {
            return new SqlConnection(stringCurrentConnection);
        }

    }
}
