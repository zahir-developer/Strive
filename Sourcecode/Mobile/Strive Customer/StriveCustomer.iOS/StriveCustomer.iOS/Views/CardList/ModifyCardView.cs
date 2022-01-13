using System;
using Acr.UserDialogs;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;
using UIKit;

namespace StriveCustomer.iOS.Views.CardList
{
    public partial class ModifyCardView : MvxViewController<CardModifyViewModel>
    {
        public CardDetails Modifiedcard;
        public ModifyCardView() : base("ModifyCardView", null)
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
            if (CustomerCardInfo.isAddCard == true)
            {
                Title.Text = "Add Card";
                
            }
            else
            {
                Title.Text = "Edit Card";
                CardNumberentry.Text = CustomerCardInfo.cardNumber;
                ExpiryDateentry.Text = CustomerCardInfo.expiryDate;
            }
            SaveCardButton.Layer.CornerRadius = 5;
            CardDetialsView.Layer.CornerRadius = 5;

            
        }
        partial void SaveCard_BtnTouch(UIButton sender)
        {

            ViewModel.cardNumber = CardNumberentry.Text;
            ViewModel.expiryDate = ExpiryDateentry.Text;
            ViewModel.Navigatetoperosnalinfo();
        }
    }
}

