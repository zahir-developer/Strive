using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Strive.BusinessEntities;

namespace Strive.BusinessLogic
{
    public class Strivebase
    {
        public readonly IDistributedCache Strivecache;
        public Strivebase(IDistributedCache cache)
        {
            Strivecache = cache;
        }

        public void SetTenantSchematoCache(TenantSchema tenantSchema)
        {
            string strTenantSchema = Strivecache.GetString(tenantSchema.UserGuid);
            if (string.IsNullOrEmpty(strTenantSchema))
            {
                //Strivecache.SetString(tenantSchema.UserGuid, JsonConvert.SerializeObject(tenantSchema));
                Strivecache.SetString(tenantSchema.Schemaname, JsonConvert.SerializeObject(tenantSchema));
            }
        }

        protected string GetRefreshToken(string userGuid)
        {
            string refreshToken = Strivecache.GetString(userGuid);
            return refreshToken;
        }

        protected void DeleteRefreshToken(string userGuid, string refreshToken)
        {
            Strivecache.Remove(userGuid);
        }

        protected void SaveRefreshToken(string userGuid, string refreshToken)
        {
            Strivecache.SetString(userGuid, refreshToken);
        }

        protected string GetTenantConnectionString(TenantSchema tenantSchema, string conString)
        {
            conString = conString.Replace("[UserName]", tenantSchema.Username);
            conString = conString.Replace("[Password]", tenantSchema.Password);
            
            return conString;
        }

    }
}