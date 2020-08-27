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
using Strive.BusinessEntities.Model;
using Strive.BusinessEntities.ViewModel;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.Code;
using Strive.BusinessEntities.DTO.Vehicle;


namespace Strive.ResourceAccess
{
    public class VehicleRal : RalBase
    {
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

        public bool UpdateClientVehicle(ClientVehicle ClientVehicle)
        {
            return dbRepo.Update(ClientVehicle);
        }

        public bool SaveClientVehicle(VehicleDto client)
        {
            return dbRepo.InsertPc(client, "ClientId");
        }

        public bool DeleteVehicleById(int vehicleId)
        {
            _prm.Add("VehicleId", vehicleId);
            db.Save(SPEnum.USPDELETECLIENTVEHICLE.ToString(), _prm);
            return true;
        }
        public ClientVehicleListViewModel GetVehicleByClientId(int clientId)
        {
            _prm.Add("ClientId", clientId);
             return db.FetchMultiResult<ClientVehicleListViewModel>(SPEnum.USPGETVEHICLE.ToString(), _prm);
        }
        public VehicleDetailViewModel GetVehicleId(int vehicleId)
        {
            _prm.Add("VehicleId", vehicleId);
            return db.FetchSingle<VehicleDetailViewModel>(SPEnum.uspGetVehicleById.ToString(), _prm);
        }
        public List<VehicleColourViewModel> GetVehicleCodes()
        {
            return db.Fetch<VehicleColourViewModel>(SPEnum.uspGetVehicleCodes.ToString(), _prm);
        }
        public bool SaveClientVehicleMembership(ClientVehicleMembershipModel ClientVehicleMembershipModel)
        {
            return dbRepo.SaveAll(ClientVehicleMembershipModel, "ClientMembershipId");
        }

        public bool SaveVehicle(ClientVehicleModel clientVehicle)
        {
            return dbRepo.UpdatePc(clientVehicle);
        }
        public VehicleMembershipViewModel GetVehicleMembershipDetailsByVehicleId(int id)
        {
            _prm.Add("VehicleId", id);
            return db.FetchMultiResult<VehicleMembershipViewModel>(SPEnum.USPGETVEHICLEMEMBERSHIPBYVEHICLEID.ToString(), _prm);
        }
        public MembershipAndServiceViewModel GetMembershipDetailsByVehicleId(int id)
        {
            _prm.Add("VehicleId", id);
            return db.FetchMultiResult<MembershipAndServiceViewModel>(SPEnum.USPGETMEMBERSHIPSERVICEBYVEHICLEID.ToString(), _prm);
        }
    }
}
