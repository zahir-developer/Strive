using System;
using System.Drawing;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.Customer;
using UIKit;

namespace StriveCustomer.iOS.Views
{
    public partial class EditProfileInfo : MvxViewController                                                                                                                                                                                                                                                                                                                                                                                 <PersonalInfoEditViewModel>
    {
        private PersonalInfoEditViewModel ViewModel;
        public EditProfileInfo() : base("EditProfileInfo", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitialSetUp();
            // Perform any additional setup after loading the view, typically from a nib.
        }
        
        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private void InitialSetUp()
        {
            var Tap = new UITapGestureRecognizer(() => View.EndEditing(true));
            Tap.CancelsTouchesInView = false;
            View.AddGestureRecognizer(Tap);

            EditProfile_View.Layer.CornerRadius = 5;
            SaveEditProfile_Btn.Layer.CornerRadius = 5;
            EditProfile_scrollView.Layer.CornerRadius = 5;

            ViewModel = new PersonalInfoEditViewModel();    
        }

        partial void FullName_TouchBegin(UITextField sender)
        {
            EditFullName_Hint.Hidden = false;
            EditFullName_Hint.TextColor = UIColor.FromRGB(0, 202, 184);
        }

        partial void FullName_TouchEnd(UITextField sender)
        {
            EditFullName_Hint.Hidden = true;
        }

        partial void ContactNo_TouchBegin(UITextField sender)
        {
            EditContactNo_Hint.Hidden = false;
            EditContactNo_Hint.TextColor = UIColor.FromRGB(0, 202, 184);
        }

        partial void ContactNo_TouchEnd(UITextField sender)
        {
            EditContactNo_Hint.Hidden = true;
        }

        partial void Address_TouchBegin(UITextField sender)
        {
            EditAddress_Hint.Hidden = false;
            EditAddress_Hint.TextColor = UIColor.FromRGB(0, 202, 184);
        }

        partial void Address_TouchEnd(UITextField sender)
        {
            EditAddress_Hint.Hidden = true;
        }

        partial void ZipCode_TouchBegin(UITextField sender)
        {
            EditZipCode_Hint.Hidden = false;
            EditZipCode_Hint.TextColor = UIColor.FromRGB(0, 202, 184);
        }

        partial void ZipCode_TouchEnd(UITextField sender)
        {
            EditZipCode_Hint.Hidden = true;
        }

        partial void SecPhNo_TouchBegin(UITextField sender)
        {
            EditSecPh_Hint.Hidden = false;
            EditSecPh_Hint.TextColor = UIColor.FromRGB(0, 202, 184);
        }

        partial void SecPhNo_TouchEnd(UITextField sender)
        {
            EditSecPh_Hint.Hidden = true;
        }

        partial void EditEmail_TouchBegin(UITextField sender)
        {
            EditEmail_Hint.Hidden = false;
            EditEmail_Hint.TextColor = UIColor.FromRGB(0, 202, 184);
        }

        partial void EditEmail_TouchEnd(UITextField sender)
        {
            EditEmail_Hint.Hidden = true;
        }

        partial void SaveProfile_BtnTouch(UIButton sender)
        {
            ViewModel.FullName = FullName_EditField.Text;
            ViewModel.ContactNumber = ContactNo_EditField.Text;
            ViewModel.Address = Address_EditField.Text;
            ViewModel.ZipCode = ZipCode_EditField.Text;
            ViewModel.SecondaryContactNumber = SecPhone_EditField.Text;
            ViewModel.Email = Email_EditField.Text;

            saveProfile();            
        }

        private async void saveProfile()
        {
            var result = await ViewModel.saveClientInfoCommand();

            if (result)
            {
                ViewModel.NavigateToProfile();
            }
        }
    }
}

