using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreGraphics;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;
using StriveCustomer.iOS.UIUtils;
using StriveCustomer.iOS.Views.CardList;
using UIKit;

namespace StriveCustomer.iOS.Views
{
    public partial class EditVehicleListView : MvxViewController<VehicleInfoDisplayViewModel>
    {
        private VehicleInfoDisplayViewModel CardInfoViewModel;

        public EditVehicleListView() : base("EditVehicleListView", null)
        {
        }

        public  override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            InitialSetup();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private void InitialSetup()
        {
            CardInfoViewModel = new VehicleInfoDisplayViewModel();
            NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes()
            {
                Font = DesignUtils.OpenSansBoldFifteen(),
                ForegroundColor = UIColor.Clear.FromHex(0x24489A),
            };
            NavigationItem.Title = "Vehicle";
            CardDetails_TableView.Hidden = false;
            NoData.Hidden = false;
            EditVehicle_ParentView.Layer.CornerRadius = 5;
            CardDetails_TableView.RegisterNibForCellReuse(CardListViewCell.Nib, CardListViewCell.Key);
            CardDetails_TableView.BackgroundColor = UIColor.Clear;
            CardDetails_TableView.ReloadData();
            getSelectVehicleInfo();
            GetCardList();

        }
        private async void GetCardList()
        {
            await ViewModel.GetCustomerCardList();

            if (ViewModel.response != null)
            {
                if (ViewModel.noData)
                {
                    CardDetails_TableView.Hidden = true;
                    NoData.Hidden = false;

                }
                if(ViewModel.isData)
                {
                    CardDetails_TableView.Hidden = false;
                    NoData.Hidden = true;
                }
            }

            if (!(this.ViewModel.response.Status.Count == 0) || !(this.ViewModel.response == null))
            {
                var CardTableSource = new CardListTableSource(this.ViewModel);
                CardDetails_TableView.Source = CardTableSource;
                CardDetails_TableView.TableFooterView = new UIView(CGRect.Empty);
                CardDetails_TableView.DelaysContentTouches = false;
                CardDetails_TableView.ReloadData();
            }

        }
        private async void getSelectVehicleInfo()
        {
            CheckMembership.hasExistingMembership = false;
            CustomerVehiclesInformation.membershipDetails = null;

            await this.ViewModel.GetSelectedVehicleInfo();
            await this.ViewModel.GetCompleteVehicleDetails();

            MembershipDetails.colorNumber = this.ViewModel.clientVehicleDetail.Status.ColorId;
            MembershipDetails.modelNumber = this.ViewModel.clientVehicleDetail.Status.VehicleModelId ?? 0;
            MembershipDetails.vehicleMakeNumber = this.ViewModel.clientVehicleDetail.Status.VehicleMakeId;
            MembershipDetails.barCode = this.ViewModel.clientVehicleDetail.Status.Barcode;
            MembershipDetails.vehicleMfr = this.ViewModel.clientVehicleDetail.Status.VehicleMakeId;

            if (this.ViewModel.selectedVehicleInfo != null || this.ViewModel.selectedVehicleInfo.Status.Count > 0)
            {
                EditVehicleName.Text = this.ViewModel.selectedVehicleInfo.Status.FirstOrDefault().VehicleColor + " " +
                                   this.ViewModel.selectedVehicleInfo.Status.FirstOrDefault().VehicleMfr + " " +
                                   this.ViewModel.selectedVehicleInfo.Status.FirstOrDefault().VehicleModel;                
                EditBarCode_Value.Text = this.ViewModel.selectedVehicleInfo.Status.FirstOrDefault().Barcode ?? "";
                EditVehicleMake_Value.Text = this.ViewModel.selectedVehicleInfo.Status.FirstOrDefault().VehicleMfr ?? "";
                EditVehicleModel_Value.Text = this.ViewModel.selectedVehicleInfo.Status.FirstOrDefault().VehicleModel ?? "";
                EditVehicleColor_Value.Text = this.ViewModel.selectedVehicleInfo.Status.FirstOrDefault().VehicleColor ?? "";

                if (CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership != null)
                {
                    CheckMembership.hasExistingMembership = true;
                    EditVehicleMembership_Value.Text = "Yes";

                    var rightBtn = new UIButton(UIButtonType.Custom);
                    rightBtn.SetTitle("Next", UIControlState.Normal);
                    rightBtn.SetTitleColor(UIColor.FromRGB(0, 110, 202), UIControlState.Normal);

                    var rightBarBtn = new UIBarButtonItem(rightBtn);
                    NavigationItem.SetRightBarButtonItems(new UIBarButtonItem[] { rightBarBtn }, false);
                    rightBtn.TouchUpInside += (sender, e) =>
                    {
                        ViewModel.NavToMembershipDetail();
                    };
                }
                else
                {
                    CheckMembership.hasExistingMembership = false;
                    EditVehicleMembership_Value.Text = "No";                    
                }
            }
        }

        partial void EditVehicleList_BtnTouch(UIButton sender)
        {
            NavToEditVehicle();
        }

        private async void NavToEditVehicle()
        {
            var result = await this.ViewModel.MembershipExists();
            if (result)
            {
                ViewModel.NavToVehicleMembership();
                
            }
        }

        partial void Touch_CardDetails_AddBtn(UIButton sender)
        {
            ViewModel.NavigatetoAddCard();

        }
        
    }

    public class CheckMembership
    {
        public static bool hasExistingMembership { get; set; }
    }
}