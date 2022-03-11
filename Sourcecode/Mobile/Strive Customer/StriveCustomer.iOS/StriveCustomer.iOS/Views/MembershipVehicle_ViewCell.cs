﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Foundation;
using Strive.Core.Models.Customer;
using Strive.Core.Models.Customer.Schedule;
using Strive.Core.Models.TimInventory;
using Strive.Core.ViewModels.Customer;
using StriveCustomer.iOS.UIUtils;
using UIKit;

namespace StriveCustomer.iOS.Views
{
    public partial class MembershipVehicle_ViewCell : UITableViewCell
    {
        public MembershipServiceList data;
        public static readonly NSString Key = new NSString("MembershipVehicle_ViewCell");
        public static readonly UINib Nib;
        public List<string> upchargeList;
        public static NSIndexPath selectedMembershipIndexPath;
        public static UIImageView selectedMembershipButton;

        static MembershipVehicle_ViewCell()
        {
            Nib = UINib.FromName("MembershipVehicle_ViewCell", NSBundle.MainBundle);
        }

        protected MembershipVehicle_ViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void SetData(MembershipServiceList list, NSIndexPath indexPath)
        {
            this.data = list;
            MembershipCell_ViewHeight.Constant = 12;
            
            Membership_VehicleLbl.Text = data.Membership[indexPath.Row].MembershipName;
            if (data.Membership[indexPath.Row].DiscountedPrice != null)
            {
                Membership_Discount.Text = "$" + (VehicleMembershipViewModel.isDiscoutAvailable ? data.Membership[indexPath.Row].DiscountedPrice.ToString() : data.Membership[indexPath.Row].Price.ToString());
            }
            else
            {
                Membership_Discount.Text = "$"+ data.Membership[indexPath.Row].Price.ToString();
            }
            if (data.Membership[indexPath.Row].MembershipId == MembershipDetails.selectedMembership)
            {
                Membership_CellBtn.Image = UIImage.FromBundle("icon-checked-round");                
                MembershipDetails.selectedMembershipDetail = data.Membership[indexPath.Row];
                selectedMembershipIndexPath = indexPath;
                selectedMembershipButton = Membership_CellBtn;
            }
            else
            {
                Membership_CellBtn.Image = UIImage.FromBundle("icon-unchecked-round");
            }
        }

        public void updateCell(NSIndexPath indexPath)
        {
            Membership_CellBtn.Image = UIImage.FromBundle("icon-checked-round");

        }

        public void deselectRow(NSIndexPath indexpath)
        {
            Membership_CellBtn.Image = UIImage.FromBundle("icon-unchecked-round");

        }
        public void deselectRowForInitialCellUpdate(NSIndexPath indexPath)
        {
             if(selectedMembershipIndexPath != null)
                selectedMembershipButton.Image = UIImage.FromBundle("icon-unchecked-round");

        }
        //Cell for Upcharges

        public void setUpchargeData(List<string> serviceList, NSIndexPath indexpath, MembershipVehicle_ViewCell cell)
        {
            this.upchargeList = serviceList;
            MembershipCell_ViewHeight.Constant = 0;
            Membership_Discount.Hidden = true;
            MonthlyCharge_lbl.Hidden = true;
            Membership_VehicleLbl.Text = upchargeList[indexpath.Row];

            //if ((MembershipDetails.filteredList.ServicesWithPrice.Count > 0))
            //{
            //    foreach (var item in MembershipDetails.filteredList.ServicesWithPrice)
            //    {
            //        if (item.Upcharges == upchargeList[indexpath.Row])
            //        {
            //            Membership_CellBtn.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
            //            MembershipDetails.selectedUpCharge = item.ServiceId;
            //        }
            //    }

            //    //cell.BackgroundColor = UIColor.Clear.FromHex(0xDCDCDC);
            //    //Membership_CellBtn.BackgroundColor = UIColor.Clear.FromHex(0xDCDCDC);
            //    //Membership_VehicleLbl.BackgroundColor = UIColor.Clear.FromHex(0xDCDCDC);
            //    //Membership_CellView.BackgroundColor = UIColor.Clear.FromHex(0xDCDCDC);
            //}
            if (MembershipDetails.modelUpcharge.upcharge.Count == 0)
            {
                if("None" == upchargeList[indexpath.Row])
                {
                    Membership_CellBtn.Image = UIImage.FromBundle("icon-checked-round");
                    MembershipDetails.isNoneSelected = true;
                }
                else
                {
                    Membership_CellBtn.Image = UIImage.FromBundle("icon-unchecked-round");
                }
            }
            else
            {
                //Membership_CellBtn.SetImage(UIImage.FromBundle("icon-unchecked-round"), UIControlState.Normal);
                if (upchargeList[indexpath.Row] == MembershipDetails.modelUpcharge.upcharge[0].Upcharges)
                {
                    Membership_CellBtn.Image = UIImage.FromBundle("icon-checked-round");
                    MembershipDetails.isNoneSelected = true;
                }
                else
                {
                    Membership_CellBtn.Image = UIImage.FromBundle("icon-unchecked-round");
                }
            }
        }

        public void setServicesData(ObservableCollection<AllServiceDetail> services, NSIndexPath indexPath, MembershipVehicle_ViewCell cell)
        {
            Membership_VehicleLbl.Text = services[indexPath.Row].ServiceName.Trim() + " - $" + services[indexPath.Row].Price+"/mo.";
            MembershipCell_ViewHeight.Constant = 0;
            Membership_Discount.Hidden = true;
            MonthlyCharge_lbl.Hidden = true;
            string service = services[indexPath.Row].ServiceName.Trim().Replace(" ", "");
            if (MembershipDetails.selectedMembershipDetail.Services != null)
            {
                string[] selectedServices = MembershipDetails.selectedMembershipDetail.Services.Split(",");

                if(selectedServices.Any(x => x.Trim().Replace(" ", "") == service))
                {
                    Membership_CellBtn.Image = UIImage.FromBundle("icon-checked-round");
                    cell.UserInteractionEnabled = false;
                    cell.BackgroundColor = UIColor.Clear.FromHex(0xDCDCDC);
                    Membership_CellBtn.BackgroundColor = UIColor.Clear.FromHex(0xDCDCDC);
                    Membership_VehicleLbl.BackgroundColor = UIColor.Clear.FromHex(0xDCDCDC);
                    Membership_CellView.BackgroundColor = UIColor.Clear.FromHex(0xDCDCDC);
                }
                else if (MembershipDetails.selectedAdditionalServices.Contains(services[indexPath.Row].ServiceId))
                {
                    Membership_CellBtn.Image = UIImage.FromBundle("icon-checked-round");
                }
                else
                {
                    Membership_CellBtn.Image = UIImage.FromBundle("icon-unchecked-round");
                    cell.UserInteractionEnabled = true;
                    cell.BackgroundColor = UIColor.Clear.FromHex(0xFFFFFF);
                    Membership_CellBtn.BackgroundColor = UIColor.Clear.FromHex(0xFFFFFF);
                    Membership_VehicleLbl.BackgroundColor = UIColor.Clear.FromHex(0xFFFFFF);
                    Membership_CellView.BackgroundColor = UIColor.Clear.FromHex(0xFFFFFF);
                }
               
            }
            else if (MembershipDetails.selectedAdditionalServices.Count != 0)
            {
                if (MembershipDetails.selectedAdditionalServices.Contains(services[indexPath.Row].ServiceId))
                {
                    Membership_CellBtn.Image = UIImage.FromBundle("icon-checked-round");
                }
                else
                {
                    Membership_CellBtn.Image = UIImage.FromBundle("icon-unchecked-round");
                }
            }

        }
        public void updateadditionalservices(NSIndexPath indexPath)
        {
            if (Membership_CellBtn.Image == UIImage.FromBundle("icon-checked-round"))
            {
                Membership_CellBtn.Image = UIImage.FromBundle("icon-unchecked-round");
            }
            else
            {
                Membership_CellBtn.Image = UIImage.FromBundle("icon-checked-round");
            }
        }
        public void updateServices(NSIndexPath indexPath)
        {
            
            if(Membership_CellBtn.Image == UIImage.FromBundle("icon-checked-round"))
            {
                Membership_CellBtn.Image = UIImage.FromBundle("icon-unchecked-round");
            }
            else
            {
                Membership_CellBtn.Image = UIImage.FromBundle("icon-checked-round");
                if (selectedMembershipIndexPath != null)
                {
                    deselectRowForInitialCellUpdate(selectedMembershipIndexPath);
                }
                selectedMembershipButton = Membership_CellBtn;
                selectedMembershipIndexPath = indexPath;
               
            }
        }
    }
}