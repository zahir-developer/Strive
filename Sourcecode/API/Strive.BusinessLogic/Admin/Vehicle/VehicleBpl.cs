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
            foreach (var vehicleImage in ClientVehicle.VehicleImage)
            {
                UploadVehicleImage(vehicleImage);
            }

            return ResultWrap(new VehicleRal(_tenant).AddVehicle, ClientVehicle, "Status");
        }

        private void UploadVehicleImage(VehicleImage vehicleImage)
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

        public Result DeleteVehicle(int vehicleId)
        {
            return ResultWrap(new VehicleRal(_tenant).DeleteVehicleById, vehicleId, "Status");
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
            if (vehicleMembership.ClientVehicle.VehicleImage != null)
            {
                foreach (var vehicleImage in vehicleMembership.ClientVehicle.VehicleImage)
                {
                    UploadVehicleImage(vehicleImage);
                }
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

        public Result GetAllVehicleThumbnail(int vehicleId)
        {
            var vehicleThumnail = new VehicleRal(_tenant).GetAllVehicleThumbnail(vehicleId);

            var documentBpl = new DocumentBpl(_cache, _tenant);
            foreach(var vehicle in vehicleThumnail)
            {
                vehicle.Base64Thumbnail = documentBpl.GetBase64(GlobalUpload.DocumentType.VEHICLEIMAGE, vehicle.ThumbnailFileName);
            }

            return ResultWrap(vehicleThumnail, "VehicleThumbnails");
        }

        public Result GetVehicleImageById(int vehicleImageId)
        {
            var vehicleImage = new VehicleRal(_tenant).GetVehicleImageById(vehicleImageId);
            
          
            vehicleImage.Base64Thumbnail= new DocumentBpl(_cache, _tenant).GetBase64(GlobalUpload.DocumentType.VEHICLEIMAGE, vehicleImage.ImageName);
            

            return ResultWrap(vehicleImage, "VehicleThumbnails");
        }

        public Result DeleteVehicleImage(int vehicleImageId)
        {
            return ResultWrap(new VehicleRal(_tenant).DeleteVehicleImage, vehicleImageId, "Status");
        }

    }
}
