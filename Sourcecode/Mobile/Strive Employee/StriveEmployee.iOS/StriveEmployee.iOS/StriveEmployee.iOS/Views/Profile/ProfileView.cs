using System;
using MvvmCross.Platforms.Ios.Views;
using UIKit;
using StriveEmployee.iOS.UIUtils;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Employee.MyProfile;
using Strive.Core.ViewModels.Employee.MyProfile.Collisions;
using CoreGraphics;
using Strive.Core.ViewModels.Employee.MyProfile.Documents;
using Foundation;
using System.Drawing;
using Xamarin.Essentials;
using System.Net;

namespace StriveEmployee.iOS.Views
{
    public partial class ProfileView : MvxViewController<EmployeeInfoViewModel>
    {
        private CollisionsViewModel collisionView;
        private DocumentsViewModel documentsView;
        public ProfileView view;
        public ProfileView() : base("ProfileView", null)
        {
        }

        public override void ViewDidLoad()
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
            view = new ProfileView();
            collisionView = new CollisionsViewModel();
            documentsView = new DocumentsViewModel();
            NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes()
            {
                Font = DesignUtils.OpenSansBoldFifteen(),
                ForegroundColor = UIColor.Clear.FromHex(0x24489A),
            };
            NavigationItem.Title = "My Profile";

            var leftBtn = new UIButton(UIButtonType.Custom);
            leftBtn.SetTitle("Logout", UIControlState.Normal);
            leftBtn.SetTitleColor(UIColor.FromRGB(0, 110, 202), UIControlState.Normal);

            var leftBarBtn = new UIBarButtonItem(leftBtn);
            NavigationItem.SetLeftBarButtonItems(new UIBarButtonItem[] { leftBarBtn }, false);
            leftBtn.TouchUpInside += (sender, e) =>
            {
                ViewModel.LogoutCommand();
            };

            EmployeeProfile_View.Layer.CornerRadius = 5;
            EmployeeInfo_Segment.Layer.CornerRadius = 5;
            EmployeeCollision_View.Layer.CornerRadius = 5;
            EmployeeDocument_View.Layer.CornerRadius = 5;

            EmployeeCollision_View.Hidden = true;
            EmployeeDocument_View.Hidden = true;
            EmployeeInfo_Segment.Hidden = false;

            GetEmployeeDetails();
        }
        partial void Emp_Segment_Touch(UISegmentedControl sender)
        {
            var index = EmpAccount_Seg_Ctrl.SelectedSegment;

            if(index == 0)
            {
                EmployeeCollision_View.Hidden = true;
                EmployeeDocument_View.Hidden = true;
                EmployeeInfo_Segment.Hidden = false;

                GetEmployeeDetails();
            }
            else if(index == 1)
            {
                EmployeeInfo_Segment.Hidden = true;
                EmployeeCollision_View.Hidden = false;
                EmployeeDocument_View.Hidden = true;

                Collision_TableView.RegisterNibForCellReuse(CollisionCell.Nib, CollisionCell.Key);
                Collision_TableView.BackgroundColor = UIColor.Clear;
                Collision_TableView.ReloadData();

                GetCollisionInfo();
            }
            else if(index == 2)
            {
                EmployeeInfo_Segment.Hidden = true;
                EmployeeCollision_View.Hidden = true;
                EmployeeDocument_View.Hidden = false;

                Documents_TableView.RegisterNibForCellReuse(DocumentsCell.Nib, DocumentsCell.Key);
                Documents_TableView.BackgroundColor = UIColor.Clear;
                Documents_TableView.ReloadData();

                GetDocumentInfo();
            }
        }
        private async void GetEmployeeDetails()
        {
            await ViewModel.GetGender();
            await ViewModel.GetImmigrationStatus();
            await ViewModel.GetPersonalEmployeeInfo();
            //if ((this.ViewModel.PersonalDetails.Employee != null && this.ViewModel.PersonalDetails.Employee.EmployeeInfo != null) || this.ViewModel.PersonalDetails.Employee.EmployeeCollision != null
            //    || this.ViewModel.PersonalDetails.Employee.EmployeeDocument != null || this.ViewModel.PersonalDetails.Employee.EmployeeLocations != null || this.ViewModel.PersonalDetails.Employee.EmployeeRoles != null)
            if ((this.ViewModel.PersonalDetails.Employee != null && this.ViewModel.PersonalDetails.Employee.EmployeeInfo != null))
            {
                SetEmployeeData();
                Emp_Firstname.Text = ViewModel.PersonalDetails.Employee.EmployeeInfo.Firstname;
                Emp_Lastname.Text = ViewModel.PersonalDetails.Employee.EmployeeInfo.LastName;
                if (this.ViewModel.PersonalDetails.Employee.EmployeeInfo.Gender != null && this.ViewModel.PersonalDetails.Employee.EmployeeInfo.Gender != 0)
                {
                    Emp_Gender.Text = ViewModel.gender.Codes.Find(x => x.CodeId == ViewModel.PersonalDetails.Employee.EmployeeInfo.Gender).CodeValue;
                }
                Emp_ContactNo.Text = ViewModel.PersonalDetails.Employee.EmployeeInfo.PhoneNumber;
                Emp_SSN.Text = ViewModel.PersonalDetails.Employee.EmployeeInfo.SSNo;
                foreach (var data in this.ViewModel.ImmigrationStatus.Codes)
                {
                    if (data.CodeId == this.ViewModel.PersonalDetails.Employee.EmployeeInfo.ImmigrationStatus)
                    {
                        Emp_Imm_Status.Text = data.CodeValue;
                    }
                }                    
                Emp_Address.Text = ViewModel.PersonalDetails.Employee.EmployeeInfo.Address1;
                Emp_LoginId.Text = ViewModel.PersonalDetails.Employee.EmployeeInfo.Email;

                var date = this.ViewModel.PersonalDetails.Employee.EmployeeInfo.HiredDate.Split("T");
                Emp_HireDate.Text = date[0];
                if (this.ViewModel.PersonalDetails.Employee.EmployeeInfo.Status)
                {
                    Emp_Status.Text = "Active";
                }
                else
                {
                    Emp_Status.Text = "InActive";
                }                    
            }
        }
        private void SetEmployeeData()
        {
            EmployeePersonalDetails.FirstName = ViewModel.PersonalDetails.Employee.EmployeeInfo.Firstname;
            EmployeePersonalDetails.LastName = ViewModel.PersonalDetails.Employee.EmployeeInfo.LastName;
            EmployeePersonalDetails.GenderCodeID = ViewModel.PersonalDetails.Employee.EmployeeInfo.Gender;
            EmployeePersonalDetails.ContactNumber = ViewModel.PersonalDetails.Employee.EmployeeInfo.PhoneNumber;
            EmployeePersonalDetails.SSN = ViewModel.PersonalDetails.Employee.EmployeeInfo.SSNo;
            EmployeePersonalDetails.ImmigrationCodeID = ViewModel.PersonalDetails.Employee.EmployeeInfo.ImmigrationStatus;
            EmployeePersonalDetails.Address = ViewModel.PersonalDetails.Employee.EmployeeInfo.Address1;
            EmployeeLoginDetails.LoginID = ViewModel.PersonalDetails.Employee.EmployeeInfo.Email;
            EmployeeLoginDetails.DateofHire = ViewModel.PersonalDetails.Employee.EmployeeInfo.HiredDate;
            EmployeePersonalDetails.AddressID = ViewModel.PersonalDetails.Employee.EmployeeInfo.EmployeeAddressId;
            EmployeeLoginDetails.DetailID = ViewModel.PersonalDetails.Employee.EmployeeInfo.EmployeeDetailId;
            EmployeeLoginDetails.WashRate = ViewModel.PersonalDetails.Employee.EmployeeInfo.WashRate;
            EmployeeLoginDetails.AuthID = ViewModel.PersonalDetails.Employee.EmployeeInfo.AuthId;
            EmployeeLoginDetails.Exemptions = ViewModel.PersonalDetails.Employee.EmployeeInfo.Exemptions.ToString();
        }

        private async void GetCollisionInfo()
        {
            await collisionView.GetCollisionInfo();
            if (collisionView.CollisionDetails != null && collisionView.CollisionDetails.Employee.EmployeeCollision != null)
            {
                var collisionSource = new CollisionDataSource(collisionView.CollisionDetails.Employee.EmployeeCollision);
                Collision_TableView.Source = collisionSource;
                Collision_TableView.TableFooterView = new UIView(CGRect.Empty);
                Collision_TableView.DelaysContentTouches = false;
                Collision_TableView.ReloadData();
            }
        }

        private async void GetDocumentInfo()
        {
            await documentsView.GetDocumentInfo();
            if (documentsView.DocumentDetails != null && documentsView.DocumentDetails.Employee.EmployeeDocument != null)
            {
                var documentSource = new DocumentDataSource(documentsView.DocumentDetails.Employee.EmployeeDocument, documentsView, view);
                Documents_TableView.Source = documentSource;
                Documents_TableView.TableFooterView = new UIView(CGRect.Empty);
                Documents_TableView.DelaysContentTouches = false;
                Documents_TableView.ReloadData();
            }            
        }

        public void loadPdf(string filename)
        {
            //var webView = new UIWebView(View.Bounds);
            //View.AddSubview(webView);

            //webView.LoadRequest(new NSUrlRequest(new NSUrl(filename, false)));
            //webView.ScalesPageToFit = true;

            //UIApplication.SharedApplication.OpenUrl(new NSUrl(filename));

            //var viewer = UIDocumentInteractionController.FromUrl(NSUrl.FromFilename(filename));
            //viewer.PresentOpenInMenu(new RectangleF(0, -260, 320, 320), this.View, true);

            //Browser.OpenAsync(filename);

            //var imageview = new UIImageView(View.Bounds);

            //imageview.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor).Active = true;
            //imageview.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor).Active = true;
            //imageview.TopAnchor.ConstraintEqualTo(View.TopAnchor).Active = true;
            //imageview.BottomAnchor.ConstraintEqualTo(View.BottomAnchor).Active = true;

            //var imageBytes = Convert.FromBase64String(filename);
            //var imageData = NSData.FromArray(imageBytes);
            //var uiImage = UIImage.LoadFromData(imageData);

            //imageview.Image = uiImage;

            //View.AddSubview(imageview);

            //var webClient = new WebClient();
            //var url = new Uri("_codex.txt");
            //webClient.DownloadDataAsync(url);
        }
    }
}

