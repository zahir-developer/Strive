using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Strive.BusinessEntities;

namespace Strive.BusinessLogic
{
    public class Strivebase
    {
        public IDistributedCache Strivecache;
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

        public (string, string)? GetTenantSchematoCache(string key)
        {
            string strTenantSchema = Strivecache.GetString(key);
            if (string.IsNullOrEmpty(strTenantSchema)) return null;
            var tenantSchema = JsonConvert.DeserializeObject<TenantSchema>(strTenantSchema);
            string strConnectionString = GetTenantConnectionString(tenantSchema);
            return (strConnectionString, tenantSchema.Schemaname);
        }

        public string GetTenantConnectionString(TenantSchema tenantSchema)
        {
            return $"Server=14.141.185.75;Initial Catalog=StriveAuthDb;MultipleActiveResultSets=true;User ID={tenantSchema.Username};Password={tenantSchema.Password}";
        }

    }
}