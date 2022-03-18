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
        public bool AddDriveUpVehicle(int? locationId, string barcode, int? make, int? model, int? color, int? createdBy)
        {
            _prm.Add("Locationid", locationId);
            _prm.Add("Barcode", barcode);
            _prm.Add("Make", make);
            _prm.Add("Model", model);
            _prm.Add("Color", color);
            _prm.Add("CreatedBy", createdBy);

            db.Save(EnumSP.Vehicle.USPADDDRIVEUPVEHICLE.ToString(), _prm);

            return true;
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
        public List<VehicleByEmailViewModel> GetVehicleByEmailId(string emailId)
        {
            _prm.Add("EmailId", emailId);
            return db.Fetch<VehicleByEmailViewModel>(SPEnum.USPGETVEHICLEDETAILBYEMAILID.ToString(), _prm);
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

        public bool UpdateVehicleNumberSequence(int? vehicleId, int clientId)
        {
            if (clientId != 0)
            {
                _prm.Add("@ClientId", clientId);
                _prm.Add("@VehicleId", vehicleId.GetValueOrDefault());
                db.Save(EnumSP.Vehicle.USPUPDATEVEHICLENUMBERSEQUENCE.ToString(), _prm);
                return true;
            }
            else
                return false;
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


        public VehicleImageViewModel GetVehicleIssueImageById(int vehicleIssueImageId)
        {
            _prm.Add("vehicleIssueImageId", vehicleIssueImageId);
            return db.FetchSingle<VehicleImageViewModel>(SPEnum.USPGETVEHICLEISSUEIMAGEBYID.ToString(), _prm);
        }

        public bool DeleteVehicleIssue(int vehicleIssueId)
        {
            _prm.Add("VehicleIssueId", vehicleIssueId);
            db.Save(SPEnum.USPDELETEVEHICLEISSUE.ToString(), _prm);
            return true;
        }

        public bool DeleteVehicleImage(int vehicleImageId)
        {
            _prm.Add("VehicleImageId", vehicleImageId);
            db.Save(SPEnum.USPDELETECLIENTVEHICLEIMAGE.ToString(), _prm);
            return true;
        }

        public bool GetMembershipDiscountStatus(int clientId, int vehicleId)
        {
            _prm.Add("ClientId", clientId);
            _prm.Add("VehicleId", vehicleId);
            var result = db.FetchSingle<MembershipDiscountViewModel>(SPEnum.USPGETMEMBERSHIPDISCOUNT.ToString(), _prm);
            if(result.IsDiscount == true)
                return true;
            else
                return false;
           
        }

        public bool updateMembershipVehicleDiscount(int clientId, int vehicleId, string action)
        {
            _prm.Add("clientId", clientId);
            _prm.Add("vehicleId", vehicleId);
            _prm.Add("Action", action);
            db.Save(EnumSP.Vehicle.USPUPDATEMEMBERSHIPVEHICLEDISCOUNT.ToString(), _prm);
            return true;
        }

        public bool VehicleMembershipDelete(VehicleMembershipDeleteDto deleteDto)
        {
            _prm.Add("ClientMembershipId", deleteDto.ClientMembershipId);
            db.Save(EnumSP.Vehicle.USPDELETEVEHICLEMEMBERSHIP.ToString(), _prm);

            return true;
        }

        public bool AddVehicleIssue(VehicleIssueDto vehicleIssueDto)
        {
            return dbRepo.SaveAll<VehicleIssueDto>(vehicleIssueDto, "VehicleIssueId");
        }

        public VehicleIssueImageViewModel GetAllVehicleIssueImage(int vehicleId)
        {
            _prm.Add("vehicleId", vehicleId);
            return db.FetchMultiResult<VehicleIssueImageViewModel>(SPEnum.USPGETALLVEHICLEISSUE.ToString(), _prm);
        }

        public List<VehicleImageViewModel> GetAllVehicleImage(int vehicleIssueId)
        {
            _prm.Add("vehicleIssueId", vehicleIssueId);
            return db.Fetch<VehicleImageViewModel> (SPEnum.USPGETALLVEHICLEISSUEIMAGE.ToString(), _prm);
        }

    }
}
