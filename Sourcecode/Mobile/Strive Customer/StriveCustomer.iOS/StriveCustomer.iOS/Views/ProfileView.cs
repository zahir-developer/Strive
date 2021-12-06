using System;
using System.Collections.Generic;
using System.Linq;
using Acr.UserDialogs;
using CoreGraphics;
using MvvmCross.Navigation;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;
using StriveCustomer.iOS.UIUtils;
using UIKit;

namespace StriveCustomer.iOS.Views
{
    public partial class ProfileView : MvxViewController<MyProfileInfoViewModel>
    {
        private PastDetailViewModel pastViewModel;
        private PersonalInfoViewModel personalInfoViewModel;
        private VehicleInfoViewModel vehicleViewModel;
        float totalCost;
        string previousDates;
        public PastClientServices pastClientServices;
        private readonly IMvxNavigationService navigationService;

        public ProfileView() : base("ProfileView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
                       
            InitialSetup();            

            // Perform any additional setup after loading the view, typically from a nib.
        }
        
        private void InitialSetup()
        {
            pastViewModel = new PastDetailViewModel();
            personalInfoViewModel = new PersonalInfoViewModel();
            vehicleViewModel = new VehicleInfoViewModel();
            CustomerInfo.TotalCost = new List<float>();
            pastClientServices = new PastClientServices();
            pastClientServices.PastClientDetails = new List<PastClientDetails>();
            CustomerInfo.pastClientServices = new PastClientServices();
            CustomerInfo.pastClientServices.PastClientDetails = new List<PastClientDetails>();

            NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes()
            {
                Font = DesignUtils.OpenSansBoldFifteen(),
                ForegroundColor = UIColor.Clear.FromHex(0x24489A),
            };
            NavigationItem.Title = "Account";

            var leftBtn = new UIButton(UIButtonType.Custom);
            leftBtn.SetTitle("Logout", UIControlState.Normal);
            leftBtn.SetTitleColor(UIColor.FromRGB(0, 110, 202), UIControlState.Normal);

            var leftBarBtn = new UIBarButtonItem(leftBtn);
            NavigationItem.SetLeftBarButtonItems(new UIBarButtonItem[] { leftBarBtn }, false);
            leftBtn.TouchUpInside += (sender, e) =>
            {
                ViewModel.LogoutCommand();
            };

            PastDetail_Segment.Hidden = true;
            PersonalInfo_Segment.Hidden = false;
            VehicleList_Segment.Hidden = true;
            NavigationItem.HidesBackButton = true;
            ProfileParent_View.Layer.CornerRadius = 5;
            PersonalInfo_Segment.Layer.CornerRadius = 5;
            PastDetail_Segment.Layer.CornerRadius = 5;
            VehicleList_Segment.Layer.CornerRadius = 5;

            if(CustomerInfo.actionType == 1)
            {
                SegmentControl.SelectedSegment = 1;

                VehicleList_Segment.Hidden = false;
                PersonalInfo_Segment.Hidden = true;
                PastDetail_Segment.Hidden = true;
                VehicleList_AddBtn.Layer.CornerRadius = 5;

                VehicleList_TableView.RegisterNibForCellReuse(VehicleListViewCell.Nib, VehicleListViewCell.Key);
                VehicleList_TableView.BackgroundColor = UIColor.Clear;
                VehicleList_TableView.ReloadData();

                GetVehicleList();
            }

            getPersonalInfo();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        partial void ProfileSegment_SelectedTab(UISegmentedControl sender)
        {
            var index = SegmentControl.SelectedSegment;

            if (index == 0)
            {
                PersonalInfo_Segment.Hidden = false;
                PastDetail_Segment.Hidden = true;
                VehicleList_Segment.Hidden = true;

                getPersonalInfo();
            }
            else if (index == 1)
            {
                VehicleList_Segment.Hidden = false;
                PersonalInfo_Segment.Hidden = true;
                PastDetail_Segment.Hidden = true;
                VehicleList_AddBtn.Layer.CornerRadius = 5;

                VehicleList_TableView.RegisterNibForCellReuse(VehicleListViewCell.Nib, VehicleListViewCell.Key);
                VehicleList_TableView.BackgroundColor = UIColor.Clear;
                VehicleList_TableView.ReloadData();

                GetVehicleList();
            }
            //else if (index == 2)
            //{
            //    PersonalInfo_Segment.Hidden = true;
            //    PastDetail_Segment.Hidden = false;
            //    VehicleList_Segment.Hidden = true;

            //    PastDetailTableView.RegisterNibForCellReuse(PastDetailViewCell.Nib, PastDetailViewCell.Key);
            //    PastDetailTableView.BackgroundColor = UIColor.Clear;
            //    PastDetailTableView.ReloadData();
            //    if (pastClientServices.PastClientDetails.Count == 0)
            //    {
            //        getPastDetails();
            //    }
            //}
        }

        private async void getPersonalInfo()
        {
            await personalInfoViewModel.GetClientById();

            if (personalInfoViewModel.customerInfo.Status.Count > 0)
            {
                FullName_Value.Text = personalInfoViewModel.customerInfo.Status.FirstOrDefault().FirstName +" "+ personalInfoViewModel.customerInfo.Status.FirstOrDefault().MiddleName +" "+ personalInfoViewModel.customerInfo.Status.FirstOrDefault().LastName;
                ContactNo_Value.Text = personalInfoViewModel.customerInfo.Status.FirstOrDefault().PhoneNumber;
                Address_Value.Text = personalInfoViewModel.customerInfo.Status.FirstOrDefault().Address1;
                ZipCode_Value.Text = personalInfoViewModel.customerInfo.Status.FirstOrDefault().Zip;
                PhoneNo_Value.Text = personalInfoViewModel.customerInfo.Status.FirstOrDefault().PhoneNumber2;
                Email_Value.Text = personalInfoViewModel.customerInfo.Status.FirstOrDefault().Email;

                MyProfileCustomerInfo.FullName = personalInfoViewModel.customerInfo.Status.FirstOrDefault().FirstName +" "+ personalInfoViewModel.customerInfo.Status.FirstOrDefault().MiddleName + " " + personalInfoViewModel.customerInfo.Status.FirstOrDefault().LastName;
                MyProfileCustomerInfo.ContactNumber = personalInfoViewModel.customerInfo.Status.FirstOrDefault().PhoneNumber;
                MyProfileCustomerInfo.Email = personalInfoViewModel.customerInfo.Status.FirstOrDefault().Email;
                MyProfileCustomerInfo.SecondaryContactNumber = personalInfoViewModel.customerInfo.Status.FirstOrDefault().PhoneNumber2;
                MyProfileCustomerInfo.Address = personalInfoViewModel.customerInfo.Status.FirstOrDefault().Address1;
                MyProfileCustomerInfo.ZipCode = personalInfoViewModel.customerInfo.Status.FirstOrDefault().Zip;
            }
        }

        //private async void getPastDetails()
        //{
        //    totalCost = 0;
        //    int previousID = 0;
        //    string previousVehicle;
        //    var result = await this.pastViewModel.GetPastDetailsServices();
        //    PastDetailsCompleteDetails.pastClientServices = result;
        //    if (result != null && result.PastClientDetails.Count != 0)
        //    {
        //        var count = 0;
        //        previousID = result.PastClientDetails[0].VehicleId;
        //        previousDates = result.PastClientDetails[0].DetailVisitDate;
        //        previousVehicle = result.PastClientDetails[0].Color + " " + result.PastClientDetails[0].Make + " " + result.PastClientDetails[0].Model;
        //        foreach (var data in result.PastClientDetails)
        //        {
        //            if (String.Equals(data.DetailOrAdditionalService, "Details") && string.Equals(data.DetailVisitDate, previousDates))
        //            {
        //                CustomerInfo.pastClientServices.PastClientDetails.Add(data);
        //                totalCost = totalCost + float.Parse(data.Cost);
        //            }
        //            else if (String.Equals(data.DetailOrAdditionalService, "Additional Services") && string.Equals(data.DetailVisitDate, previousDates))
        //            {
        //                CustomerInfo.pastClientServices.PastClientDetails.Add(data);
        //                totalCost = totalCost + float.Parse(data.Cost);

        //            }
        //            else if (String.Equals(data.DetailOrAdditionalService, "Details") && !string.Equals(data.DetailVisitDate, previousDates))
        //            {
        //                CustomerInfo.TotalCost.Add(totalCost);
        //                totalCost = 0;
        //                CustomerInfo.pastClientServices.PastClientDetails.Add(data);
        //                totalCost = totalCost + float.Parse(data.Cost);
        //            }
        //            count++;
        //            if (count == result.PastClientDetails.Count)
        //            {
        //                CustomerInfo.TotalCost.Add(totalCost);
        //                totalCost = 0;
        //            }
        //            previousID = data.VehicleId;
        //            previousDates = data.DetailVisitDate;
        //        }
        //        var counts = 0;
        //        foreach (var data in CustomerInfo.pastClientServices.PastClientDetails)
        //        {
        //            if (data.VehicleId != previousID || counts == 0)
        //            {
        //                pastClientServices.PastClientDetails.Add(data);
        //                previousID = data.VehicleId;
        //            }
        //            counts++;
        //        }

        //        var PastDetailTableSource = new PastDetailTableSource(this, pastClientServices);
        //        PastDetailTableView.Source = PastDetailTableSource;
        //        PastDetailTableView.TableFooterView = new UIView(CGRect.Empty);
        //        PastDetailTableView.DelaysContentTouches = false;
        //        PastDetailTableView.ReloadData();
        //    }
        //}

        partial void EditProfile_Touch(UIButton sender)
        {
            personalInfoViewModel.NavToEditPersonalInfo();
        }
        
        public async void GetVehicleList()
        {
            await this.vehicleViewModel.GetCustomerVehicleList();
            
            if (!(this.vehicleViewModel.vehicleLists.Status.Count == 0) || !(this.vehicleViewModel.vehicleLists == null))
            {                
                var VehicleTableSource = new VehicleListTableSource(this.vehicleViewModel);
                VehicleList_TableView.Source = VehicleTableSource;
                VehicleList_TableView.TableFooterView = new UIView(CGRect.Empty);
                VehicleList_TableView.DelaysContentTouches = false;
                VehicleList_TableView.ReloadData();
            } 
        }

        partial void Touch_VehicleList_AddBtn(UIButton sender)
        {
            CheckMembership.hasExistingMembership = false;
            CustomerVehiclesInformation.membershipDetails = null;
            MembershipDetails.clearMembershipData();
            vehicleViewModel.NavToAddVehicle();
        }                
    }

    public class PastDetailsCompleteDetails
    {
        public static PastClientServices pastClientServices { get; set; }
    }
}