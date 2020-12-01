using Strive.Core.Models.Employee.Collisions;
using Strive.Core.Models.Employee.Common;
using Strive.Core.Utils.Employee;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Employee.MyProfile.Collisions
{
    public class EditCollisionsViewModel : BaseViewModel
    {
        #region Properties

        public GetCollisions getCollisions { get; set; }
        public CommonCodes liabilityTypes { get; set; }
        public string CollisionDate { get; set; }
        public int CollisionID { get; set; }
        public string CollisionAmount { get; set; }
        public string CollisionNotes { get; set; }
        public string CollisionClientName { get; set; }
        public string CollisionVehicleId { get; set; }
        public bool collisionAdded { get; set; }

        #endregion Properties



        #region Commands

        public async Task GetLiabilityTypes()
        {
            var result = await AdminService.GetCommonCodes("LIABILITYTYPE");

            if (result == null)
            {
                liabilityTypes = null;
            }
            else
            {
                liabilityTypes = new CommonCodes();
                liabilityTypes.Codes = new List<Codes>();
                liabilityTypes = result;
            }
        }

        public async Task EditCollision()
        {
            if (String.IsNullOrEmpty(CollisionDate))
            {
                _userDialog.Alert("Please enter the date");
            }
            else if (String.IsNullOrEmpty(CollisionAmount))
            {
                _userDialog.Alert("Please enter the amount");
            }
            else if (String.IsNullOrEmpty(CollisionNotes))
            {
                _userDialog.Alert("Please enter the reason");
            }
            else
            {
                AddCollisions addCollisions = new AddCollisions();
                addCollisions.employeeLiability = new employeeLiability();
                addCollisions.employeeLiabilityDetail = new employeeLiabilityDetail();

                addCollisions.employeeLiability.createdBy = 0;
                addCollisions.employeeLiability.createdDate = CollisionDate;
                addCollisions.employeeLiability.employeeId = EmployeeTempData.EmployeeID;
                addCollisions.employeeLiability.isActive = true;
                addCollisions.employeeLiability.isDeleted = false;
                addCollisions.employeeLiability.liabilityDescription = CollisionNotes;
                addCollisions.employeeLiability.liabilityId = MyProfileTempData.LiabilityID;
                addCollisions.employeeLiability.liabilityType = CollisionID;
                addCollisions.employeeLiability.productId = 2;
                addCollisions.employeeLiability.status = 0;
                addCollisions.employeeLiability.totalAmount = float.Parse(CollisionAmount);
                addCollisions.employeeLiability.updatedBy = 0;
                addCollisions.employeeLiability.updatedDate = DateTime.Today.ToString("yyyy-M-dd");
                if (CollisionVehicleId == null)
                {
                    addCollisions.employeeLiability.vehicleId = "";
                }
                else
                {
                    addCollisions.employeeLiability.vehicleId = CollisionVehicleId;
                }

                addCollisions.employeeLiabilityDetail.amount = float.Parse(CollisionAmount);
                addCollisions.employeeLiabilityDetail.createdBy = 0;
                addCollisions.employeeLiabilityDetail.createdDate = CollisionDate;
                addCollisions.employeeLiabilityDetail.description = CollisionNotes;
                addCollisions.employeeLiabilityDetail.documentPath = "";
                addCollisions.employeeLiabilityDetail.isActive = true;
                addCollisions.employeeLiabilityDetail.isDeleted = false;
                addCollisions.employeeLiabilityDetail.liabilityDetailId = 0;
                addCollisions.employeeLiabilityDetail.liabilityDetailType = 1;
                addCollisions.employeeLiabilityDetail.liabilityId = MyProfileTempData.LiabilityID;
                addCollisions.employeeLiabilityDetail.paymentType = 1;
                addCollisions.employeeLiabilityDetail.updatedBy = 0;
                addCollisions.employeeLiabilityDetail.updatedDate = DateTime.Today.ToString("yyyy-M-dd");

                var result = await AdminService.UpdateCollisions(addCollisions);
                if (result == null)
                {
                    collisionAdded = false;
                }
                else
                {
                    collisionAdded = true;
                    _userDialog.Toast("Collision saved");
                }
            }
        }

        public async Task GetCollisions()
        {
            var result = await AdminService.GetCollisions(MyProfileTempData.LiabilityID);
            if(result == null)
            {
                getCollisions = null;
            }
            else
            {
                getCollisions = new GetCollisions();
                getCollisions.Collision = new Collision();
                getCollisions.Collision.Liability = new List<Liability>();
                getCollisions.Collision.LiabilityDetail = new List<LiabilityDetail>();
                getCollisions = result;
            }
        }

        #endregion Commands
    }
}
