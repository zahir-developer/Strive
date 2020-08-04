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
using System.Data;
using Strive.BusinessEntities.MembershipSetup;

namespace Strive.ResourceAccess
{
    public class VehicleRal
    {
        private Db _db;

        public VehicleRal(ITenantHelper tenant)
        {
            var dbConnection = tenant.db();
            _db = new Db(dbConnection);
        }

        public List<ClientVehicleView> GetVehicleDetails()
        {
            DynamicParameters dynParams = new DynamicParameters();
            var result = _db.FetchRelation1<ClientVehicleView, ClientVehicle>(SPEnum.USPGETALLVEHICLE.ToString(), dynParams);
            return result;
        }
        public int SaveVehicle(List<ClientVehicle> products)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@tvpVehicle", products.ToDataTable().AsTableValuedParameter());
            return _db.Execute<ClientVehicle>("uspSaveVehicle".ToString(), parameters);
        }

        public bool DeleteVehicleById(int id)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@tblClientVehicleId", id.toInt());
            CommandDefinition commandDefinition = new CommandDefinition(SPEnum.USPDELETECLIENTVEHICLE.ToString(), dynamicParameters, commandType: CommandType.StoredProcedure);
            _db.Save(commandDefinition);
            return true;
        }

        public List<ClientVehicleView> GetVehicleById(int id)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@tblClientId", id.toInt());
            var result = _db.FetchRelation1<ClientVehicleView, ClientVehicle>(SPEnum.USPGETVEHICLEBYID.ToString(), dynamicParameters);
            return result;
        }
       
    }
}
