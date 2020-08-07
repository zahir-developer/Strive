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
using Strive.BusinessEntities.ViewModel;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.Code;
using Strive.BusinessEntities.DTO.Vehicle;

namespace Strive.ResourceAccess
{
    public class VehicleRal : RalBase
    {
        private Db _db;

        public VehicleRal(ITenantHelper tenant) : base(tenant) { }

        public List<VehicleViewModel> GetAllVehicle()
        {
            return db.Fetch<VehicleViewModel>(SPEnum.USPGETVEHICLE.ToString(), null);
        }
        public List<VehicleMembershipModel> GetVehicleMembership()
        {
            return db.Fetch<VehicleMembershipModel>(SPEnum.uspGetVihicleMembership.ToString(), null);
        }
        public bool UpdateVehicleMembership(Membership Membership)
        {
            return dbRepo.Update(Membership);
        }

        public bool SaveVehicle(VehicleDto client)
        {
            return dbRepo.InsertPc(client, "ClientId");
        }

        public bool DeleteVehicleById(int vehicleId)
        {
            _prm.Add("VehicleId", vehicleId);
            db.Save(SPEnum.USPDELETECLIENTVEHICLE.ToString(), _prm);
            return true;
        }
        public VehicleViewModel GetVehicleById(int clientId)
        {
            _prm.Add("ClientId", clientId);
             return db.FetchSingle<VehicleViewModel>(SPEnum.USPGETVEHICLE.ToString(), _prm);
        }
        public VehicleDetailViewModel GetVehicleId(int vehicleId)
        {
            _prm.Add("VehicleId", vehicleId);
            return db.FetchSingle<VehicleDetailViewModel>(SPEnum.uspGetVehicleById.ToString(), _prm);
        }
        public List<Code> GetVehicleColour()
        {
            return new CommonRal(_tenant).GetCodeByCategory(GlobalCodes.VEHICLECOLOR);
        }
        public List<Code> GetCodeTypeModel()
        {
            return new CommonRal(_tenant).GetCodeByCategory(GlobalCodes.VEHICLEMODEL);
        }
        public List<Code> GetCodeModel()
        {
            return new CommonRal(_tenant).GetCodeByCategory(GlobalCodes.VEHICLEMANUFACTURER);
        }
        public List<Code> GetCodeUpcharge()
        {
            return new CommonRal(_tenant).GetCodeByCategory(GlobalCodes.UPCHARGE);
        }
        public List<Code> GetCodeMake()
        {
            return new CommonRal(_tenant).GetCodeByCategory(GlobalCodes.UPCHARGE);
        }

    }
}
