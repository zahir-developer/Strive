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
            //List<ClientVehicleView> lstResource = new List<ClientVehicleView>();
            var result = _db.FetchRelation1<ClientVehicleView, ClientVehicle>(SPEnum.USPGETALLVEHICLE.ToString(), dynParams);
            return result;
        }

        public bool UpdateVehicle(Strive.BusinessEntities.Client.ClientVehicle lstUpdateVehicle)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@ClientVehicleId", lstUpdateVehicle.ClientVehicleId);
            dynamicParameters.Add("@ClientId", lstUpdateVehicle.ClientId);
            dynamicParameters.Add("@LocationId", lstUpdateVehicle.LocationId);
            dynamicParameters.Add("@VehicleNumber", lstUpdateVehicle.VehicleNumber);
            dynamicParameters.Add("@VehicleMake", lstUpdateVehicle.VehicleMake);
            dynamicParameters.Add("@VehicleModel", lstUpdateVehicle.VehicleModel);
            dynamicParameters.Add("@VehicleModelNo", lstUpdateVehicle.VehicleModelNo);
            dynamicParameters.Add("@VehicleYear", lstUpdateVehicle.VehicleYear);
            dynamicParameters.Add("@VehicleColor", lstUpdateVehicle.VehicleColor);
            dynamicParameters.Add("@Upcharge", lstUpdateVehicle.Upcharge);
            dynamicParameters.Add("@Barcode", lstUpdateVehicle.Barcode);
            dynamicParameters.Add("@Notes", lstUpdateVehicle.Notes);
            dynamicParameters.Add("@CreatedDate", lstUpdateVehicle.CreatedDate);
            CommandDefinition commandDefinition = new CommandDefinition(SPEnum.USPUPDATEVEHICLE.ToString(), dynamicParameters, commandType: CommandType.StoredProcedure);
            _db.Save(commandDefinition);
            return true;
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
