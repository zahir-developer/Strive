﻿using Strive.BusinessEntities;
using Strive.BusinessEntities.Code;
using Strive.BusinessEntities.DTO.Vehicle;
using Strive.BusinessEntities.MembershipSetup;
using Strive.BusinessEntities.ViewModel;
using Strive.Common;
using Strive.Repository;
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

        public List<VehicleViewModel> GetAllVehicle(VehicleSearchDto name)
        {
            _prm.Add("@SearchName", name.SearchName);
            return db.Fetch<VehicleViewModel>(EnumSP.Vehicle.USPGETVEHICLE.ToString(), _prm);
        }
        public List<VehicleMembershipModel> GetVehicleMembership()
        {
            return db.Fetch<VehicleMembershipModel>(EnumSP.Vehicle.USPGETVEHICLEMEMBERSHIP.ToString(), null);
        }
        public bool UpdateVehicleMembership(BusinessEntities.Model.Membership Membership)
        {
            return dbRepo.Update(Membership);
        }

        public bool AddVehicle(VehicleDto ClientVehicle)
        {
            return dbRepo.InsertPc(ClientVehicle,"VehicleId");
        }

        public bool SaveClientVehicle(VehicleDto client)
        {
            return dbRepo.InsertPc(client, "ClientId");
        }

        public bool DeleteVehicleById(int vehicleId)
        {
            _prm.Add("VehicleId", vehicleId);
            db.Save(EnumSP.Vehicle.USPDELETECLIENTVEHICLE.ToString(), _prm);
            return true;
        }
        public List<VehicleByClientViewModel> GetVehicleByClientId(int clientId)
        {
            _prm.Add("ClientId", clientId);
             return db.Fetch<VehicleByClientViewModel>(EnumSP.Vehicle.USPGETVEHICLEDETAILBYCLIENTID.ToString(), _prm);
        }
        public VehicleDetailViewModel GetVehicleId(int vehicleId)
        {
            _prm.Add("VehicleId", vehicleId);
            return db.FetchSingle<VehicleDetailViewModel>(EnumSP.Vehicle.USPGETVEHICLEBYID.ToString(), _prm);
        }
        public List<VehicleColourViewModel> GetVehicleCodes()
        {
            return db.Fetch<VehicleColourViewModel>(EnumSP.Vehicle.uspGetVehicleCodes.ToString(), _prm);
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
            return db.FetchMultiResult<VehicleMembershipViewModel>(EnumSP.Vehicle.USPGETVEHICLEMEMBERSHIPBYVEHICLEID.ToString(), _prm);
        }
        public MembershipAndServiceViewModel GetMembershipDetailsByVehicleId(int id)
        {
            _prm.Add("VehicleId", id);
            return db.FetchMultiResult<MembershipAndServiceViewModel>(EnumSP.Membership.USPGETMEMBERSHIPSERVICEBYVEHICLEID.ToString(), _prm);
        }
        public List<PastDetailsViewModel> GetPastDetails(int clientId)
        {
            _prm.Add("ClientId", clientId);
            return db.Fetch<PastDetailsViewModel>(EnumSP.Vehicle.USPGETPASTDETAILSBYCLIENTID.ToString(), _prm);
        }

        public int AddVehicleImage(VehicleImageDto vehicleImage)
        {
            return dbRepo.InsertPK(vehicleImage, "VehicleImageId");
        }

    }
}
