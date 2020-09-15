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
        int TokenExpiryMintues { get; set; }
        string TenantGuid { get; set; }
        string SMTPClient { get; set; }
        string SMTPPassword { get; set; }
        string Port { get; set; }
        string FromMailAddress { get; set; }
        string EmployeeId { get; set; }
        string SchemaName { get; set; }
        string DocumentUploadFolder { get; set; }
        string DocumentFormat { get; set; }
        string ProductImageFolder { get; set; }
        string ProductImageFormat { get; set; }
        int ProductThumbHeight { get; set; }
        int ProductThumbWidth { get; set; }
        string TenatId { get; set; }

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

        public int TokenExpiryMintues { get; set; }
        public string TenantGuid { get; set; }

        public string SMTPClient { get; set; }
        public string SMTPPassword { get; set; }
        public string Port { get; set; }
        public string FromMailAddress { get; set; }
        public string EmployeeId { get; set; }
        public string SchemaName { get; set; }
        public string DocumentUploadFolder { get; set; }
        public string DocumentFormat{ get; set; }
        public string ProductImageFolder { get; set; }
        public string ProductImageFormat { get; set; }
        public int ProductThumbHeight { get; set; }
        public int ProductThumbWidth { get; set; }
        public string TenatId { get; set; }

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
            TenantGuid = tenantGuid;
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
