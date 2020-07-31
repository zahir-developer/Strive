using Dapper;
using Strive.BusinessEntities;
using Strive.Common;
using Strive.Repository;
using System;
using System.Collections.Generic;
using Strive.BusinessEntities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strive.BusinessEntities.Client;

namespace Strive.ResourceAccess
{
    public class VehicleRal
    {
        private Db db;

        public VehicleRal(ITenantHelper tenant)
        {
            var dbConnection = tenant.db();
            db = new Db(dbConnection);
        }

        public List<ClientVehicleView> GetVehicleDetails()
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<ClientVehicleView> lstResource = new List<ClientVehicleView>();
            var result = db.FetchRelation1<ClientVehicleView,ClientVehicle>(SPEnum.USPGETALLVEHICLE.ToString(), dynParams);
            return result;
        }
    }
}
