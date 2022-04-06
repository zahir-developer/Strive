// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;
using Foundation;
using Greeter.Cells;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Extensions;
using Greeter.Sources;
using Newtonsoft.Json;
using UIKit;

namespace Greeter.Storyboards
{
    public partial class IssuesViewController : BaseViewController
    {
        // Data
        const string SCREEN_TITLE = "Issues";
        

        //Data to set
        public long ClientID;
        public long VehicleID;
        public string Barcode;
        public string Email;
        public long MakeID;
        public long ModelID;
        public long UpchargeID;
        public string Model;
        public int ColorID;
        public string MakeName;

        VehicleIssueResponse vehicleIssueResponse;
       
        public IssuesViewController(IntPtr handle) : base(handle)
        {
           
        }
        
       
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            lblBarcode.Text = Barcode;
            lblDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            lblMake.Text = MakeName;
            lblModel.Text = Model;
            ImageContainer.Hidden = true;

            CloseButton.TouchUpInside += CloseButton_TouchUpInside;
            InitialiseUI();
            
            GetData();
            NSNotificationCenter.DefaultCenter.AddObserver(new NSString("com.strive.greeter.delete_clicked"), notify: (notification) => {
                if (notification.UserInfo is null)
                    return;

                var issueid = notification.UserInfo["issueid"] as NSString;
                var id = JsonConvert.DeserializeObject<string>(issueid);

                InvokeOnMainThread(() => {
                    deleteissue(int.Parse(id));
                });
            });
            NSNotificationCenter.DefaultCenter.AddObserver(new NSString("com.strive.greeter.View_Clicked"), notify: (notification) => {
                if (notification.UserInfo is null)
                    return;

                var issueimageid = notification.UserInfo["IssueImageId"] as NSString;
                var id = JsonConvert.DeserializeObject<string>(issueimageid);

                InvokeOnMainThread(() => {
                    ViewIssueImage(int.Parse(id));
                });
            });

            NSNotificationCenter.DefaultCenter.AddObserver(new NSString("com.strive.greeter.reload_Add"), notify: (notification) =>
            {
                InvokeOnMainThread(() =>
                {
                    this.ReloadInputViews();
                    GetData();
                });
            });
            btnAddIssue.TouchUpInside += delegate
            {
                PresentAddIssuePopUp();
            };


        }

        private void CloseButton_TouchUpInside(object sender, EventArgs e)
        {
            ImageContainer.Hidden = true;
        }

        private void BtnAddIssueTouchUpInside(object sender, EventArgs e)
        {
            PresentAddIssuePopUp();
        }

        void PresentAddIssuePopUp()
        {
            AddIssueViewController.vehicleId = VehicleID;
            View.Dispose();
            UIViewController vc = GetViewController(GetHomeStorybpard(), nameof(AddIssueViewController));
            vc.ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext;
            
            PresentViewController(vc, true, null);
        }

        void InitialiseUI()
        {
            
            NavigationController.NavigationBar.Hidden = false;
            viewHeader.AddHearderViewShadow();
            tvIssues.RegisterNibForCellReuse(IssueCell.Nib, IssueCell.Key);
            
        }
        
        async void GetData()
        {
            
            ShowActivityIndicator();
            var testresponse = await SingleTon.VehicleApiService.GetVehicleIssue(VehicleID); //VehicleID
             vehicleIssueResponse = new VehicleIssueResponse();
            vehicleIssueResponse = testresponse;
            UpdateDataToUI();
            HideActivityIndicator();
            
        }

        public void UpdateDataToUI()
        {
            if (vehicleIssueResponse.VehicleIssueThumbnail != null)
            {
                Title = SCREEN_TITLE;
                tvIssues.Source = new IssuesSource(this.vehicleIssueResponse);
                tvIssues.ReloadData();
                
            }
            else
            {
                tvIssues.Hidden = true;
            }
            
        }

        public  async void deleteissue(int issueid)
        {
            //ShowActivityIndicator();
            var result = await SingleTon.VehicleApiService.DeleteVehicleIssue(issueid);
            //HideActivityIndicator();
            if (result.StatusCode == 200 )
            {

                //var index = vehicleIssueResponse.VehicleIssueThumbnail.VehicleIssue.Find(X => X.VehicleIssueid == issueid);
                //vehicleIssueResponse.VehicleIssueThumbnail.VehicleIssue.Remove(index);
                //tvIssues.ReloadData();
                //ViewDidLoad();
                GetData();
                ShowAlertMsg("Issue Deleted Successfuly");
                Console.WriteLine("Issue Deleted Successfuly");
            }
            else
            {
                ShowAlertMsg("Vehicle Not Deleted");
                Console.WriteLine("Issue Not Deleted");
            }
        }
        public async void ViewIssueImage(int imageid)
        {
            var result = await SingleTon.VehicleApiService.GetVehicleIssueImageById(imageid);
            if(result.VehicleImage.VehicleIssueImageId == imageid)
            {
                byte[] encodedDataAsBytes = Convert.FromBase64String(result.VehicleImage.Base64);
                NSData data = NSData.FromArray(encodedDataAsBytes);
                var uiImage = UIImage.LoadFromData(data);
                IssueImageView.Image = uiImage;
                ImageContainer.Hidden = false;
            }
          
        }

        //string sample = "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";
    
}
}
