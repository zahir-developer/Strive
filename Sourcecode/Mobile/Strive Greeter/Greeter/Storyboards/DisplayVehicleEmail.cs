using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Foundation;
using Greeter.Common;
using Greeter.DTOs;
using UIKit;

namespace Greeter.Storyboards
{
    public partial class DisplayVehicleEmail : BaseViewController
    {
        public DisplayVehicleEmail(IntPtr handle) : base(handle)
        {
        }
        UIPickerView pv = new UIPickerView();
        VehicleByEmailResponse vehicleByEmailResponse;
        public string CustomBarcode;
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            BtnCancel.TouchUpInside += delegate {
                CloseController();
            };
            
            BtnCreate.TouchUpInside += delegate {

                AddNewBarcode();
	        };
            // Perform any additional setup after loading the view, typically from a nib.
        }
        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        void CloseController()
        {
            DismissViewController(true, () => { });
        }

        public static bool isValidEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        public async void AddNewBarcode()
        {
            if(TftEmail.Text != null)
            {
                if (isValidEmail(TftEmail.Text))
                {
                    ServiceViewController.Clientemail = TftEmail.Text;
                    var result = await SingleTon.VehicleApiService.GetVehicleByEmailId(TftEmail.Text);
                    vehicleByEmailResponse = result;
                    var alert = UIAlertController.Create("Select Vehicle", "", UIAlertControllerStyle.ActionSheet);
                    alert.View.TintColor = UIColor.Black;
                    if (vehicleByEmailResponse.Status.Count != 0)
                    {

                        foreach (var item in vehicleByEmailResponse.Status)
                        {

                            alert.AddAction(UIAlertAction.Create(item.VehicleMfr + " / " + item.VehicleModel + " / " + item.VehicleColor, UIAlertActionStyle.Default, (action) =>
                            {
                                //var vc = (ServiceQuestionViewController)GetViewController(GetHomeStorybpard(), nameof(ServiceQuestionViewController));
                                ////vc.View = this.View;
                                //vc.ClientID = item.ClientID;
                                //vc.VehicleID = item.VehicleID;
                                //vc.Barcode = CustomBarcode;
                                //vc.MakeID = item.VehicleMakeId;
                                //vc.ModelID = item.ModelID;
                                //vc.UpchargeID = item.UpchargeID;
                                //vc.Model = item.VehicleModel;
                                //vc.ColorID = item.VehicleColorId;
                                //vc.CustName = item.FirstName + item.LastName;
                                //vc.ClientEmail = ""; //item.Email;
                                //vc.ShopPhoneNumber = ServiceViewController.shopPhoneNumber;
                                //vc.IsNewBarcode = true;
                                //if (ServiceViewController.IsWash)
                                //{
                                //    vc.ServiceType = ServiceType.Wash;
                                //}
                                //else
                                //{
                                //    vc.ServiceType = ServiceType.Detail;
                                //}
                                ServiceViewController.EmailStatus = item;
                                NSNotificationCenter.DefaultCenter.PostNotificationName(new NSString("com.strive.greeter.createclicked"), null);
                                DismissViewController(true, () => { });
                                //NavigateToWithAnim(vc);
                            }));
                        }


                        alert.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, null));

                        UIPopoverPresentationController presentationPopover = alert.PopoverPresentationController;
                        if (presentationPopover != null)
                        {
                            presentationPopover.SourceView = TftEmail;
                            presentationPopover.PermittedArrowDirections = UIPopoverArrowDirection.Up;
                        }
                        PresentViewController(alert, true, null);
                    }
                    else
                    {
                        ShowAlertMsg("This Email id has no associated vehicles");
                    }
                }
                else
                {
                    ShowAlertMsg("Enter a Valid Email ID");
                }

               
            }
            else
            {
                ShowAlertMsg("The Email Cannot be Empty");
            }
        }
        

    }
}

