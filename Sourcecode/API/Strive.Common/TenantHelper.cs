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
        IDbConnection dbAuth();
        void SetConnection(string con);

        void SetAuthDBConnection(string con);

        void SetTenantGuid(string tenantGuid);
    }

    public class TenantHelper : ITenantHelper
    {
        public string TenantConnectionStringTemplate = string.Empty;
        public string stringCurrentConnection = string.Empty;
        public string stringAuthConnection = string.Empty;
        public string stringTenantGuid = string.Empty;

        IDistributedCache _cache;
        public TenantHelper(IDistributedCache cache)
        {
            _cache = cache;
        }

        public void SetConnection(string con)
        {
            stringCurrentConnection = con;
        }

        public void SetTenantGuid(string tenantGuid)
        {
            stringTenantGuid = tenantGuid;
        }

        public void SetAuthDBConnection(string con)
        {
            stringAuthConnection = con;
        }

        public IDbConnection db()
        {
            return new SqlConnection(stringCurrentConnection);
        }

        public IDbConnection dbAuth()
        {
            return new SqlConnection(stringAuthConnection);
        }
    }
}
