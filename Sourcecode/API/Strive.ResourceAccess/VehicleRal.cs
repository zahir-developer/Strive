using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO.Vehicle;
using Strive.BusinessEntities.ViewModel;
using Strive.Common;
using System.Collections.Generic;
using Strive.BusinessEntities.Model;
using Strive.BusinessEntities.DTO;
using System;

namespace Strive.ResourceAccess
{
    public class VehicleRal : RalBase
    {
        public VehicleRal(ITenantHelper tenant) : base(tenant) { }

        public VehicleCountViewModel GetAllVehicle(SearchDto searchDto)
        {
            _prm.Add("@PageNo", searchDto.PageNo);
            _prm.Add("@PageSize", searchDto.PageSize);
            _prm.Add("@Query", searchDto.Query);
            _prm.Add("@SortOrder", searchDto.SortOrder);
            _prm.Add("@SortBy", searchDto.SortBy);
            return db.FetchMultiResult<VehicleCountViewModel>(SPEnum.USPGETVEHICLE.ToString(), _prm);
        }
        public List<VehicleMembershipModel> GetVehicleMembership()
        {
            return db.Fetch<VehicleMembershipModel>(SPEnum.USPGETVEHICLEMEMBERSHIP.ToString(), null);
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
            db.Save(SPEnum.USPDELETECLIENTVEHICLE.ToString(), _prm);
            return true;
        }
        public List<VehicleByClientViewModel> GetVehicleByClientId(int clientId)
        {
            _prm.Add("ClientId", clientId);
             return db.Fetch<VehicleByClientViewModel>(SPEnum.USPGETVEHICLEDETAILBYCLIENTID.ToString(), _prm);
        }
        public VehicleDetailViewModel GetVehicleId(int vehicleId)
        {
            _prm.Add("VehicleId", vehicleId);
            return db.FetchSingle<VehicleDetailViewModel>(SPEnum.USPGETVEHICLEBYID.ToString(), _prm);
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
        public List<PastDetailsViewModel> GetPastDetails(int clientId)
        {
            _prm.Add("ClientId", clientId);
            return db.Fetch<PastDetailsViewModel>(SPEnum.USPGETPASTDETAILSBYCLIENTID.ToString(), _prm);
        }

        public int AddVehicleImage(VehicleImageDto vehicleImage)
        {
            return dbRepo.InsertPK(vehicleImage, "VehicleImageId");
        }

        public List<VehicleImageViewModel> GetAllVehicleThumbnail(int vehicleId)
        {
            _prm.Add("vehicleId", vehicleId);
            return db.Fetch<VehicleImageViewModel>(SPEnum.USPGETALLVEHICLEIMAGEBYID.ToString(), _prm);
        }
        public VehicleImageViewModel GetVehicleImageById(int vehicleImageId)
        {
            _prm.Add("vehicleImageId", vehicleImageId);
            
            return db.FetchSingle<VehicleImageViewModel>(SPEnum.USPGETVEHICLEIMAGEBYID.ToString(), _prm);
        }

        public bool DeleteVehicleImage(int vehicleImageId)
        {
            _prm.Add("VehicleImageId", vehicleImageId);
            db.Save(SPEnum.USPDELETECLIENTVEHICLEIMAGE.ToString(), _prm);
            return true;
        }

    }
}
