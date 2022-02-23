using Microsoft.Extensions.Caching.Distributed;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.Vehicle;
using Strive.BusinessEntities.Model;
using Strive.BusinessEntities.ViewModel;
using Strive.BusinessLogic.Document;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.IO;
using System.Net;

namespace Strive.BusinessLogic.Vehicle
{
    public class VehicleBpl : Strivebase, IVehicleBpl
    {
        public VehicleBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper) { }

        public Result GetAllVehicle(SearchDto searchDto)
        {
            return ResultWrap(new VehicleRal(_tenant).GetAllVehicle, searchDto, "Vehicle");
        }
        public Result GetVehicleMembership()
        {
            return ResultWrap(new VehicleRal(_tenant).GetVehicleMembership, "VehicleMembership");
        }
        public Result UpdateVehicleMembership(Membership Membership)
        {
            return ResultWrap(new VehicleRal(_tenant).UpdateVehicleMembership, Membership, "Status");
        }
        public Result AddVehicle(VehicleDto ClientVehicle)
        {
            return ResultWrap(new VehicleRal(_tenant).AddVehicle, ClientVehicle, "Status");
        }

        private void UploadVehicleImage(VehicleIssueImage vehicleImage)
        {
            var docBpl = new DocumentBpl(_cache, _tenant);
            string imageName = docBpl.Upload(GlobalUpload.DocumentType.VEHICLEIMAGE, vehicleImage.Base64, vehicleImage.ImageName);

            vehicleImage.OriginalImageName = vehicleImage.ImageName;
            vehicleImage.ImageName = imageName;
            vehicleImage.FilePath = new DocumentBpl(_cache, _tenant).GetUploadFolderPath(GlobalUpload.DocumentType.VEHICLEIMAGE) + imageName;

            try
            {
                vehicleImage.ThumbnailFileName = docBpl.SaveThumbnail(GlobalUpload.DocumentType.VEHICLEIMAGE, _tenant.ImageThumbWidth, _tenant.ImageThumbHeight, vehicleImage.Base64, imageName);
            }
            catch (Exception ex)
            {

            }
        }

        public Result SaveClientVehicle(VehicleDto vehicle)
        {
            try
            {
                return ResultWrap(new VehicleRal(_tenant).SaveClientVehicle, vehicle, "Status");
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        public Result DeleteVehicle(int id, int? clientId)
        {
            var vehRal = new VehicleRal(_tenant);

            bool result = false;
            if (vehRal.DeleteVehicleById(id))
            {
                if (vehRal.UpdateVehicleNumberSequence(id, clientId.GetValueOrDefault()))
                {
                    result = true;
                }
                else
                    result = false;
            }
            else
                result = false;

            return ResultWrap(result, "Status");
        }
        
        public Result GetVehicleByClientId(int clientId)
        {
            return ResultWrap(new VehicleRal(_tenant).GetVehicleByClientId, clientId, "Status");
        }
        public Result GetVehicleId(int vehicleId)
        {
            return ResultWrap(new VehicleRal(_tenant).GetVehicleId, vehicleId, "Status");
        }
        public Result GetVehicleCodes()
        {
            return ResultWrap(new VehicleRal(_tenant).GetVehicleCodes, "VehicleDetails");
        }
        public Result SaveClientVehicleMembership(ClientVehicleMembershipDetailModel vehicleMembership)
        {
            if (vehicleMembership.ClientVehicleMembershipModel != null)
            {
                var cardNumber = vehicleMembership.ClientVehicleMembershipModel.ClientVehicleMembershipDetails.CardNumber;
                if (cardNumber.Length > 0)
                {
                    var lastDigits = cardNumber.Substring(cardNumber.Length - 4, 4);

                    var requiredMask = new String('X', cardNumber.Length - lastDigits.Length);

                    vehicleMembership.ClientVehicleMembershipModel.ClientVehicleMembershipDetails.CardNumber = string.Concat(requiredMask, lastDigits);
                }
                if (vehicleMembership.ClientVehicleMembershipModel.ClientVehicleMembershipDetails != null)
                {
                    var startDate = Convert.ToDateTime(vehicleMembership.ClientVehicleMembershipModel.ClientVehicleMembershipDetails.StartDate);
                    vehicleMembership.ClientVehicleMembershipModel.ClientVehicleMembershipDetails.StartDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0);
                }
            }

            if (vehicleMembership.ClientVehicle.VehicleImage != null)
            {
                foreach (var vehicleImage in vehicleMembership.ClientVehicle.VehicleImage)
                {
                    UploadVehicleImage(vehicleImage);
                }
            }

            if (vehicleMembership.DeletedClientMembershipId != null && vehicleMembership.DeletedClientMembershipId.GetValueOrDefault(0) > 0)
            {

                if (vehicleMembership.ClientVehicleMembershipModel == null)
                {
                    var membershipVehicleDiscount = new VehicleRal(_tenant).updateMembershipVehicleDiscount(
                        vehicleMembership.ClientVehicle.ClientVehicle.ClientId.GetValueOrDefault(),
                        vehicleMembership.ClientVehicle.ClientVehicle.VehicleId, "ChangeMembership");
                }

                var clientMembershipDelete = new MembershipSetupRal(_tenant).DeleteVehicleMembershipById(vehicleMembership.DeletedClientMembershipId.GetValueOrDefault());

            }

            var saveVehicle = new VehicleRal(_tenant).SaveVehicle(vehicleMembership.ClientVehicle);
            if (!saveVehicle)
                return ResultWrap<ClientVehicle>(false, "Result", "Failed to save vehicle details.");

            return ResultWrap(new VehicleRal(_tenant).SaveClientVehicleMembership, vehicleMembership.ClientVehicleMembershipModel, "Status");
        }
        public Result GetVehicleMembershipDetailsByVehicleId(int id)
        {
            return ResultWrap(new VehicleRal(_tenant).GetVehicleMembershipDetailsByVehicleId, id, "VehicleMembershipDetails");
        }
        public Result GetMembershipDetailsByVehicleId(int id)
        {
            return ResultWrap(new VehicleRal(_tenant).GetMembershipDetailsByVehicleId, id, "MembershipDetailsForVehicleId");
        }
        public Result GetPastDetails(int clientId)
        {
            return ResultWrap(new VehicleRal(_tenant).GetPastDetails, clientId, "PastClientDetails");
        }

        public Result DeleteVehicleImage(int vehicleImageId)
        {
            return ResultWrap(new VehicleRal(_tenant).DeleteVehicleImage, vehicleImageId, "Status");
        }

        public Result DeleteVehicleIssue(int vehicleIssueId)
        {
            var documentBpl = new DocumentBpl(_cache, _tenant);

            var vehicleRal = new VehicleRal(_tenant);

            var vehicleImages = vehicleRal.GetAllVehicleImage(vehicleIssueId);

            foreach (var vImage in vehicleImages)
            {
                documentBpl.DeleteBlob(GlobalUpload.DocumentType.VEHICLEIMAGE, vImage.ImageName);
                documentBpl.DeleteBlob(GlobalUpload.DocumentType.VEHICLEIMAGE, vImage.ThumbnailFileName);
            }

            return ResultWrap(vehicleRal.DeleteVehicleIssue, vehicleIssueId, "Status");
        }



        public Result GetMembershipDiscountStatus(int clientId, int vehicleId)
        {
            return ResultWrap(new VehicleRal(_tenant).GetMembershipDiscountStatus, clientId, vehicleId, "Status");
        }

        public Result DeleteVehicleMembership(VehicleMembershipDeleteDto deleteDto)
        {
            if (deleteDto != null)
            {
                var membershipVehicleDiscount = new VehicleRal(_tenant).updateMembershipVehicleDiscount(
                    deleteDto.ClientId,
                    deleteDto.VehicleId, "ChangeMembership");
            }

            return ResultWrap(new VehicleRal(_tenant).VehicleMembershipDelete, deleteDto, "Status");
        }

        public Result AddVehicleIssue(VehicleIssueDto vehicleIssueDto)
        {
            foreach (var vehicleImage in vehicleIssueDto.VehicleIssueImage)
            {
                UploadVehicleImage(vehicleImage);
            }

            return ResultWrap(new VehicleRal(_tenant).AddVehicleIssue, vehicleIssueDto, "Status");
        }

        public Result GetAllVehicleIssueImage(int vehicleId)
        {
            var vehicleThumnail = new VehicleRal(_tenant).GetAllVehicleIssueImage(vehicleId);

            var documentBpl = new DocumentBpl(_cache, _tenant);
            foreach (var vehicle in vehicleThumnail?.VehicleIssueImage)
            {
                vehicle.Base64Thumbnail = documentBpl.GetBase64(GlobalUpload.DocumentType.VEHICLEIMAGE, vehicle.ThumbnailFileName);
            }

            return ResultWrap(vehicleThumnail, "VehicleIssueThumbnail");
        }

        public Result GetVehicleIssueImageById(int vehicleIssueImageId)
        {
            var vehicleImage = new VehicleRal(_tenant).GetVehicleIssueImageById(vehicleIssueImageId);

            if (vehicleImage != null)
                vehicleImage.Base64 = new DocumentBpl(_cache, _tenant).GetBase64(GlobalUpload.DocumentType.VEHICLEIMAGE, vehicleImage.ImageName);

            return ResultWrap(vehicleImage, "VehicleImage");
        }

        public Result UpdateVehicleNumberSequence(int? vehicleId, int clientId)
        {
            var result = new VehicleRal(_tenant).UpdateVehicleNumberSequence(vehicleId, clientId);

            return ResultWrap(result, "Status");
        }


    }
}
