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
        string ClientId { get; set; }
        string SchemaName { get; set; }
        string DocumentUploadFolder { get; set; }
        string DocumentFormat { get; set; }
        string ProductImageFolder { get; set; }
        string ProductImageFormat { get; set; }
        int ImageThumbHeight { get; set; }
        int ImageThumbWidth { get; set; }
        string LogoImageFolder { get; set; }
        string LogoImageFormat { get; set; }
        int LogoThumbHeight { get; set; }
        int LogoThumbWidth { get; set; }
        string TenatId { get; set; }
        string TermsAndCondition { get; set; }
        string EmployeeHandbook { get; set; }
        string GeneralDocumentFolder { get; set; }
        string HtmlTemplates { get; set; }

        string VehicleImageFolder { get; set; }
        string ApplicationUrl { get; set; }
        string MobileUrl { get; set; }
         string OSMUri { get; set; }
         string UserAgent { get; set; }


        #region CardConnect

        string CCUrl { get; set; }
        string CCUserName { get; set; }
        string CCPassword { get; set; }
        string MID { get; set; }

        #endregion

        IDbConnection db();
        IDbConnection dbAuth();
        IDbConnection dbAuthAdmin();
        void SetConnection(string con);
        void SetAuthDBConnection(string con);
        void SetAuthAdminDBConnection(string con);
        void SetTenantGuid(string tenantGuid);
    }

    public class TenantHelper : ITenantHelper
    {
        public string TenantConnectionStringTemplate = string.Empty;
        public string stringCurrentConnection = string.Empty;
        public string stringAuthConnection = string.Empty;
        public string stringAuthAdminConnection { get; set; }
        public string stringTenantGuid = string.Empty;


        IDistributedCache _cache;

        public int TokenExpiryMintues { get; set; }
        public string TenantGuid { get; set; }

        public string SMTPClient { get; set; }
        public string SMTPPassword { get; set; }
        public string Port { get; set; }
        public string FromMailAddress { get; set; }
        public string EmployeeId { get; set; }
        public string ClientId { get; set; }
        public string SchemaName { get; set; }
        public string DocumentUploadFolder { get; set; }
        public string DocumentFormat { get; set; }
        public string ProductImageFolder { get; set; }
        public string ProductImageFormat { get; set; }
        public int ImageThumbHeight { get; set; }
        public int ImageThumbWidth { get; set; }
        public string LogoImageFolder { get; set; }
        public string LogoImageFormat { get; set; }
        public int LogoThumbHeight { get; set; }
        public int LogoThumbWidth { get; set; }
        public string TenatId { get; set; }
        public string TermsAndCondition { get; set; }
        public string EmployeeHandbook { get; set; }
        public string GeneralDocumentFolder { get; set; }

        public string HtmlTemplates { get; set; }

        public string VehicleImageFolder { get; set; }
        public string ApplicationUrl { get; set; }
        public string MobileUrl { get; set; }
        public string OSMUri { get; set; }
        public string UserAgent { get; set; }

        #region

        public string CCUrl { get; set; }

        public string CCUserName { get; set; }

        public string CCPassword { get; set; }

        public string MID { get; set; }

        #endregion
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

        public void SetAuthAdminDBConnection(string con)
        {
            stringAuthAdminConnection = con;
        }

        public IDbConnection db()
        {
            return new SqlConnection(stringCurrentConnection);
        }

        public IDbConnection dbAuth()
        {
            return new SqlConnection(stringAuthConnection);
        }

        public IDbConnection dbAuthAdmin()
        {
            return new SqlConnection(stringAuthAdminConnection);
        }
    }
}
