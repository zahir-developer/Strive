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

        public Result GetAllVehicle(VehicleSearchDto name)
        {
            return ResultWrap(new VehicleRal(_tenant).GetAllVehicle, name, "Vehicle");
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
            foreach (var img in ClientVehicle.VehicleImage)
            {
              

                string imageName = new DocumentBpl(_cache, _tenant).Upload(GlobalUpload.DocumentType.VEHICLEIMAGE, img.Base64, img.ImageName);
                
                img.OriginalImageName = img.ImageName;
                img.ImageName = imageName;
                img.FilePath = new DocumentBpl(_cache, _tenant).GetUploadFolderPath(GlobalUpload.DocumentType.VEHICLEIMAGE) + imageName;
               

            }

            return ResultWrap(new VehicleRal(_tenant).AddVehicle, ClientVehicle, "Status");
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

        //public int AddImage(VehicleImageDto vehicleImage)
        //{

        //    foreach (var img in vehicleImage.VehicleImage)
        //        string imageName = Upload(vehicleImage.DocumentType, vehicleImage.VehicleImage.Base64, vehicleImage.VehicleImage.ImageName);

        //    vehicleImage.VehicleImage.OriginalImageName = vehicleImage.VehicleImage.ImageName;
        //    vehicleImage.VehicleImage.ImageName = imageName;
        //    vehicleImage.VehicleImage.FilePath = new DocumentBpl(_cache, _tenant).GetUploadFolderPath(vehicleImage.DocumentType) + imageName;

        //    var result = new VehicleRal(_tenant).AddVehicleImage(vehicleImage);

        //    if (!(result > 0))
        //    {
        //        DeleteImage(vehicleImage.DocumentType, imageName);
        //    }

        //    return result;
        //}
    }
}
