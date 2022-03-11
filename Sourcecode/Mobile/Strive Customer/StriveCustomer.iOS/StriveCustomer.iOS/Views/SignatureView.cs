using System;
using System.Drawing;
using System.IO;
using System.Text;
using CoreGraphics;
using Foundation;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;
using UIKit;
using Xamarin.Controls;

namespace StriveCustomer.iOS.Views
{
    public partial class SignatureView : MvxViewController<MembershipSignatureViewModel>
    {
        public static SignaturePadView signature { get; set; }
        public static UIImage Signatureimage;
        public static UIImage Base64ContractImage;
        public SignatureView() : base("SignatureView", null)
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
            // TODO
            //var rightBtn = new UIButton(UIButtonType.Custom);
            //rightBtn.SetTitle("Next", UIControlState.Normal);
            //rightBtn.SetTitleColor(UIColor.FromRGB(0, 110, 202), UIControlState.Normal);

            //var rightBarBtn = new UIBarButtonItem(rightBtn);
            //NavigationItem.SetRightBarButtonItems(new UIBarButtonItem[] { rightBarBtn }, false);
            //rightBtn.TouchUpInside += (sender, e) =>
            //{
                //AgreeTerms();
            //};


            signature = new SignaturePadView(signPadView.Frame)
            {
                StrokeWidth = 3f,
                StrokeColor = UIColor.Black,
                SignatureLineColor = UIColor.Black,
                BackgroundColor = UIColor.White
            };

            signPadView.Layer.BorderColor = UIColor.FromRGBA(255, 255, 255, 0).CGColor;
            signPadView.Layer.BorderWidth = 1f;

            signPadView.Layer.CornerRadius = 5;
            CancelBtn_Sign.Layer.CornerRadius = 5;
            DoneBtn_Sign.Layer.CornerRadius = 5;
            SignatureParentView.Layer.CornerRadius = 5;
            FinalContract.Hidden = true;        
            signature.SignaturePrompt.Text = "";
            signature.SignaturePrompt.TextColor = UIColor.Clear;
            signature.Caption.TextColor = UIColor.Clear;
            signature.Caption.Text = "";
            
            UIImage image = signature.GetImage();
                        
            signPadView.AddSubview(signature);

            LoadSignature();
        }
        
        private async void Agree()
        {
            Signatureimage = UIViewExtensions.AsImage(signPadView);
            FinalContract.Hidden = false;
            //UIImage CroppedContract = CropImage.cropImage(TermsView.contract,0,120,410,590);
            //UIImage CroppedSignature = CropImage.cropImage(Signatureimage,35,180,310,300);
            // combine all the images related to the contract
            Contract.Image =TermsView.contract ;
            TermsConfirmView.Image = TermsView.TermsConfirmView;
            SignatureImg.Image = signature.GetImage();  //Signatureimage;
            Base64ContractImage = UIViewExtensions.AsImage(FinalContract);
            // Conversion to base 64 string
            PaymentViewModel.Base64ContractString = Base64ContractImage.AsJPEG(0.15f).GetBase64EncodedString(NSDataBase64EncodingOptions.None);
            //CancelMembership

            
            var result = await ViewModel.AgreeMembership();
            if (result)
            {
                ViewModel.NextCommand();
            }
        }
       
        private void LoadSignature()
        {
            if (SignatureClass.signaturePoints != null)
            {
                signature.LoadPoints(SignatureClass.signaturePoints);
            }
            else
            {
                signature.Clear();
            }
        }

        partial void CancelBtn_SignTouch(UIButton sender)
        {
            CancelMembership();
        }

        partial void DoneBtn_SignTouch(UIButton sender)
        {
            SignatureClass.signaturePoints = signature.Points;

            if(SignatureClass.signaturePoints == null || !(SignatureClass.signaturePoints.Length > 100))
            {
                ViewModel.NoSignatureError();
            }
            else
            {
                Agree();
                signature.Clear();
                SignatureClass.signaturePoints = null;
            }
        }

        private async void CancelMembership()
        {
            var result = await ViewModel.CancelMembership();

            if (result)
            {
                string make = MembershipDetails.vehicleMakeName;
                string model = MembershipDetails.modelName;
                string color = MembershipDetails.colorName;
                MembershipDetails.clearMembershipData();                
                signature.Clear();
                SignatureClass.signaturePoints = null;
                MembershipDetails.vehicleMakeName = make;
                MembershipDetails.modelName = model;
                MembershipDetails.colorName = color;
                await ViewModel.NavToMembership();
            }            
        }
        
    }

    public static class SignatureClass
    {
        public static CGPoint[] signaturePoints { get; set; }        
    }

    public static class CropImage
    {
        public static UIImage cropImage(this UIImage sourceImage, int crop_x, int crop_y, int width, int height)
        {
            var imgSize = sourceImage.Size;
            UIGraphics.BeginImageContext(new SizeF(width, height));
            var context = UIGraphics.GetCurrentContext();
            var clippedRect = new RectangleF(0, 0, width, height);
            context.ClipToRect(clippedRect);
            var drawRect = new CGRect(-crop_x, -crop_y, imgSize.Width, imgSize.Height);
            sourceImage.Draw(drawRect);
            var modifiedImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return modifiedImage;
        }
        
    }
}