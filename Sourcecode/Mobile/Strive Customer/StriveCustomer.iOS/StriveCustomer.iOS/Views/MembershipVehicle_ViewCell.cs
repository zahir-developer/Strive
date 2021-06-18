using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            
            Membership_VehicleLbl.Text = data.Membership[indexPath.Row].MembershipName;
            if(data.Membership[indexPath.Row].MembershipId == MembershipDetails.selectedMembership)
            {
                Membership_CellBtn.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
            }
            else
            {
                Membership_CellBtn.SetImage(UIImage.FromBundle("icon-unchecked-round"), UIControlState.Normal);
            }                         
        }

        public void updateCell(NSIndexPath indexPath)
        {
            Membership_CellBtn.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
        }

        public void deselectRow(NSIndexPath indexpath)
        {
            Membership_CellBtn.SetImage(UIImage.FromBundle("icon-unchecked-round"), UIControlState.Normal);
        }

        //Cell for Upcharges

        public void setUpchargeData(List<string> serviceList, NSIndexPath indexpath, MembershipVehicle_ViewCell cell)
        {
            this.upchargeList = serviceList;

            Membership_VehicleLbl.Text = upchargeList[indexpath.Row];
            if (upchargeList[indexpath.Row] == MembershipDetails.filteredList.ServicesWithPrice[0].Upcharges)
            {
                //Membership_CellBtn.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                cell.BackgroundColor = UIColor.Clear.FromHex(0xDCDCDC);
                Membership_CellBtn.BackgroundColor = UIColor.Clear.FromHex(0xDCDCDC);
                Membership_VehicleLbl.BackgroundColor = UIColor.Clear.FromHex(0xDCDCDC);
                Membership_CellView.BackgroundColor = UIColor.Clear.FromHex(0xDCDCDC);
            }
            else
            {
                Membership_CellBtn.SetImage(UIImage.FromBundle("icon-unchecked-round"), UIControlState.Normal);
            }
        }

        public void setServicesData(ObservableCollection<AllServiceDetail> services, NSIndexPath indexPath, MembershipVehicle_ViewCell cell)
        {
            Membership_VehicleLbl.Text = services[indexPath.Row].ServiceName;
            string service = Membership_VehicleLbl.Text.Replace(" ", "");

            string[] selectedServices = MembershipDetails.selectedMembershipDetail.Services.Split(",");

            foreach(var itm in selectedServices)
            {
                if (itm == service)
                {
                    Membership_CellBtn.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                    cell.BackgroundColor = UIColor.Clear.FromHex(0xDCDCDC);
                    Membership_CellBtn.BackgroundColor = UIColor.Clear.FromHex(0xDCDCDC);
                    Membership_VehicleLbl.BackgroundColor = UIColor.Clear.FromHex(0xDCDCDC);
                    Membership_CellView.BackgroundColor = UIColor.Clear.FromHex(0xDCDCDC);
                }
                else
                {
                    Membership_CellBtn.SetImage(UIImage.FromBundle("icon-unchecked-round"), UIControlState.Normal);
                }
            }                
           
        }

        public void updateServices(NSIndexPath indexPath)
        {
            if(Membership_CellBtn.CurrentImage == UIImage.FromBundle("icon-unchecked-round"))
            {
                Membership_CellBtn.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
            }
            else
            {
                Membership_CellBtn.SetImage(UIImage.FromBundle("icon-unchecked-round"), UIControlState.Normal);
            }
        }
    }
}