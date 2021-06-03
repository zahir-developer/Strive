using System;
using System.Collections.ObjectModel;
using System.Linq;
using Foundation;
using Strive.Core.Models.Customer.Schedule;
using Strive.Core.Models.TimInventory;
using Strive.Core.Utils.TimInventory;
using StriveTimInventory.iOS.UIUtils;
using UIKit;

namespace StriveTimInventory.iOS.Views.MembershipView
{
    public partial class ClientTableViewCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("ClientTableViewCell`");
        public static readonly UINib Nib;

        static ClientTableViewCell()
        {
            Nib = UINib.FromName("ClientTableViewCell", NSBundle.MainBundle);
        }

        protected ClientTableViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void SetMembershipList(MembershipServices item,int index,int SelectedIndex, ClientTableViewCell cell)
        {
            ItemTitle.Text = item.MembershipName;
            DeSelectMembershipcell();
            cell.BackgroundColor = UIColor.White;
            cell.UserInteractionEnabled = true;
            if (SelectedIndex == index && index !=0)
            {
                SelectMembershipcell();
            }
            if(MembershipData.MembershipDetailView != null)
            {
                var SelectedMembership = MembershipData.MembershipServiceList.Membership.Where(m => m.MembershipId == MembershipData.MembershipDetailView.ClientVehicleMembership.MembershipId).FirstOrDefault();
                if ((SelectedMembership != null) && (SelectedMembership == item))
                {
                    cell.BackgroundColor = UIColor.Clear.FromHex(0xDCDCDC);
                    cell.UserInteractionEnabled = false;
                }
            }
        }

        public void SetClientDetail(ClientInfo item)
        {
            ItemTitle.Text = item.FirstName + " " + item.LastName;
            ItemIcon.Image = UIImage.FromBundle("member-inactive");
        }

        public void SelectMembershipcell()
        {
            ItemTitle.TextColor = UIColor.Clear.FromHex(0x1DC9B7);
            ItemIcon.Image = UIImage.FromBundle("icon-checked");
        }

        public void DeSelectMembershipcell()
        {
            ItemTitle.TextColor = UIColor.Black;
            ItemIcon.Image = UIImage.FromBundle("icon-unchecked");
        }

        public void SetUpchargeList(string item)
        {
            ItemTitle.Text = item;
            ItemIcon.Image = UIImage.FromBundle("icon-unchecked");
        }

        public void SetExtraServiceList(AllServiceDetail item,ObservableCollection<AllServiceDetail> list, ObservableCollection<AllServiceDetail> GrayedList, ClientTableViewCell cell)
        {
            ItemTitle.Text = item.ServiceName;
            DeSelectMembershipcell();
            cell.BackgroundColor = UIColor.White;
            cell.UserInteractionEnabled = true;
            if (list.Contains(item))
            {
                SelectMembershipcell();
            }
            //var GrayedItem = GrayedList.Where(s => s.ServiceId == item.ServiceId).FirstOrDefault();
            //if(GrayedItem != null)
            //{
            //    SelectMembershipcell();
            //    cell.BackgroundColor = UIColor.Clear.FromHex(0xF9F9F9);
            //    cell.UserInteractionEnabled = false;
            //}
        }

        public void SetVehicleList(VehicleDetail vehicle)
        {
            ItemTitle.Font = DesignUtils.OpenSansSemiBoldTwenty();
            ItemTitle.Text = vehicle.VehicleColor + " " + vehicle.VehicleMfr + " " + vehicle.VehicleModel;
            
            if(vehicle.IsMembership)
            {
                ItemIcon.Image = UIImage.FromBundle("member-active");
            }
            else
            {
                ItemIcon.Image = UIImage.FromBundle("member-inactive");
            }
        }
    }
}
