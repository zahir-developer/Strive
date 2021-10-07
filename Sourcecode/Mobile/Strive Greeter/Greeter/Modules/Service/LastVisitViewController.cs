using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreGraphics;
using Greeter.Common;
using Greeter.DTOs;
using UIKit;

namespace Greeter.Modules.Service
{
    public partial class LastVisitViewController : BaseViewController
    {
        UILabel serviceDateLabel;
        UILabel serviceNameLabel;
        UILabel barcodeLabel;
        UILabel customerNameLabel;
        UILabel makeLabel;
        UILabel modelLabel;
        UILabel vechileColorLabel;
        UILabel additionalServicesLabel;
        UILabel detailPackageServicesLabel;
        UILabel startTimeLabel;
        UILabel endTimeLabel;
        UILabel notesLabel;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            SetupView();
            _ = GetAndUpdateSerivceDetailsToUI();

            //serviceDateLabel.Text = "25/10/2020";
            //serviceNameLabel.Text = "Mega Mammoth";
            //barcodeLabel.Text = "#ERP28";
            //makeLabel.Text = "Honda";
            //modelLabel.Text = "Civic";
            //vechileColorLabel.Text = "Metallic Blue";
            //additionalServicesLabel.Text = "Shampoo Seal";
            //detailPackageServicesLabel.Text = "None";
            //startTimeLabel.Text = "10:00 AM";
            //endTimeLabel.Text = "12:00 PM";
            //notesLabel.Text = "A motor vehicle service or tune-up is a series of maintenance procedures carried out at a set time interval or after the vehicle has traveled a certain distance";
        }

        async Task GetAndUpdateSerivceDetailsToUI()
        {
            var service = await GetLastService(vehicleId);
            UpdateDataToUI(service.LastServiceDetail.Services[0], service.LastServiceDetail.JobItmes);
        }

        void SetupView()
        {
            NavigationController.NavigationBar.Hidden = false;
            Title = "Last Service Visit";

            var backgroundImage = new UIImageView(UIImage.FromBundle(ImageNames.SPLASH_BG));
            backgroundImage.TranslatesAutoresizingMaskIntoConstraints = false;
            View.Add(backgroundImage);

            backgroundImage.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor).Active = true;
            backgroundImage.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor).Active = true;
            backgroundImage.TopAnchor.ConstraintEqualTo(View.TopAnchor).Active = true;
            backgroundImage.BottomAnchor.ConstraintEqualTo(View.BottomAnchor).Active = true;

            var backgroundView = new UIView(CGRect.Empty);
            backgroundView.TranslatesAutoresizingMaskIntoConstraints = false;
            backgroundView.BackgroundColor = UIColor.White;
            View.Add(backgroundView);

            backgroundView.LeadingAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.LeadingAnchor, 60).Active = true;
            backgroundView.TrailingAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TrailingAnchor, -60).Active = true;
            backgroundView.TopAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TopAnchor, 40).Active = true;
            backgroundView.BottomAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.BottomAnchor, -40).Active = true;

            var headerView = new UIView(CGRect.Empty);
            headerView.TranslatesAutoresizingMaskIntoConstraints = false;
            headerView.BackgroundColor = UIColor.FromRGB(230.0f / 255.0f, 255.0f / 255.0f, 252.0f / 255.0f);
            headerView.Layer.MasksToBounds = false;
            headerView.Layer.ShadowColor = UIColor.Gray.CGColor;
            headerView.Layer.ShadowOffset = new CGSize(0f, 3f);
            headerView.Layer.ShadowOpacity = 0.2f;
            backgroundView.Add(headerView);

            headerView.LeadingAnchor.ConstraintEqualTo(backgroundView.LeadingAnchor).Active = true;
            headerView.TrailingAnchor.ConstraintEqualTo(backgroundView.TrailingAnchor).Active = true;
            headerView.TopAnchor.ConstraintEqualTo(backgroundView.TopAnchor).Active = true;
            headerView.HeightAnchor.ConstraintEqualTo(80).Active = true;

            var serviceDateTitleLabel = new UILabel(CGRect.Empty);
            serviceDateTitleLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            serviceDateTitleLabel.TextColor = UIColor.Black;
            serviceDateTitleLabel.Font = UIFont.BoldSystemFontOfSize(18);
            serviceDateTitleLabel.TextAlignment = UITextAlignment.Center;
            serviceDateTitleLabel.Text = "Service Date";
            headerView.Add(serviceDateTitleLabel);

            serviceDateLabel = new UILabel(CGRect.Empty);
            serviceDateLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            serviceDateLabel.TextColor = UIColor.FromRGB(36.0f / 255.0f, 72.0f / 255.0f, 154.0f / 255.0f);
            serviceDateLabel.Font = UIFont.SystemFontOfSize(18, UIFontWeight.Semibold);
            serviceDateLabel.TextAlignment = UITextAlignment.Center;
            headerView.Add(serviceDateLabel);

            var serviceTitleLabel = new UILabel(CGRect.Empty);
            serviceTitleLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            serviceTitleLabel.TextColor = UIColor.Black;
            serviceTitleLabel.Font = UIFont.BoldSystemFontOfSize(18);
            serviceTitleLabel.TextAlignment = UITextAlignment.Center;
            serviceTitleLabel.Text = "Service Type";
            headerView.Add(serviceTitleLabel);

            serviceNameLabel = new UILabel(CGRect.Empty);
            serviceNameLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            serviceNameLabel.TextColor = UIColor.FromRGB(36.0f / 255.0f, 72.0f / 255.0f, 154.0f / 255.0f);
            serviceNameLabel.Font = UIFont.SystemFontOfSize(18, UIFontWeight.Semibold);
            serviceNameLabel.TextAlignment = UITextAlignment.Center;
            headerView.Add(serviceNameLabel);

            var verticalDivider = new UIView(CGRect.Empty);
            verticalDivider.TranslatesAutoresizingMaskIntoConstraints = false;
            verticalDivider.BackgroundColor = UIColor.FromRGB(112.0f / 255.0f, 112.0f / 255.0f, 112.0f / 255.0f);
            headerView.Add(verticalDivider);

            verticalDivider.WidthAnchor.ConstraintEqualTo(1).Active = true;
            verticalDivider.TopAnchor.ConstraintEqualTo(headerView.TopAnchor, 15).Active = true;
            verticalDivider.BottomAnchor.ConstraintEqualTo(headerView.BottomAnchor, -15).Active = true;
            verticalDivider.CenterXAnchor.ConstraintEqualTo(headerView.CenterXAnchor).Active = true;

            serviceDateTitleLabel.LeadingAnchor.ConstraintEqualTo(headerView.LeadingAnchor).Active = true;
            serviceDateTitleLabel.TrailingAnchor.ConstraintEqualTo(verticalDivider.LeadingAnchor).Active = true;
            serviceDateTitleLabel.TopAnchor.ConstraintEqualTo(headerView.TopAnchor, 15).Active = true;
            serviceDateTitleLabel.SetContentHuggingPriority(249, UILayoutConstraintAxis.Horizontal);
            serviceDateTitleLabel.SetContentCompressionResistancePriority(249, UILayoutConstraintAxis.Horizontal);

            serviceDateLabel.LeadingAnchor.ConstraintEqualTo(headerView.LeadingAnchor).Active = true;
            serviceDateLabel.TrailingAnchor.ConstraintEqualTo(verticalDivider.LeadingAnchor).Active = true;
            serviceDateLabel.TopAnchor.ConstraintEqualTo(serviceDateTitleLabel.BottomAnchor, 5).Active = true;
            serviceDateLabel.SetContentHuggingPriority(249, UILayoutConstraintAxis.Horizontal);

            serviceTitleLabel.LeadingAnchor.ConstraintEqualTo(verticalDivider.TrailingAnchor).Active = true;
            serviceTitleLabel.TrailingAnchor.ConstraintEqualTo(headerView.TrailingAnchor).Active = true;
            serviceTitleLabel.TopAnchor.ConstraintEqualTo(headerView.TopAnchor, 15).Active = true;
            serviceTitleLabel.SetContentHuggingPriority(249, UILayoutConstraintAxis.Horizontal);

            serviceNameLabel.LeadingAnchor.ConstraintEqualTo(verticalDivider.TrailingAnchor).Active = true;
            serviceNameLabel.TrailingAnchor.ConstraintEqualTo(headerView.TrailingAnchor).Active = true;
            serviceNameLabel.TopAnchor.ConstraintEqualTo(serviceTitleLabel.BottomAnchor, 5).Active = true;
            serviceNameLabel.SetContentHuggingPriority(249, UILayoutConstraintAxis.Horizontal);

            var scrollView = new UIScrollView(CGRect.Empty);
            scrollView.TranslatesAutoresizingMaskIntoConstraints = false;
            backgroundView.Add(scrollView);

            scrollView.LeadingAnchor.ConstraintEqualTo(backgroundView.LeadingAnchor).Active = true;
            scrollView.TrailingAnchor.ConstraintEqualTo(backgroundView.TrailingAnchor).Active = true;
            scrollView.TopAnchor.ConstraintEqualTo(headerView.BottomAnchor).Active = true;
            scrollView.BottomAnchor.ConstraintEqualTo(backgroundView.BottomAnchor).Active = true;

            var containerView = new UIView(CGRect.Empty);
            containerView.TranslatesAutoresizingMaskIntoConstraints = false;
            scrollView.Add(containerView);

            containerView.LeadingAnchor.ConstraintEqualTo(scrollView.LeadingAnchor).Active = true;
            containerView.TrailingAnchor.ConstraintEqualTo(scrollView.TrailingAnchor).Active = true;
            containerView.TopAnchor.ConstraintEqualTo(scrollView.TopAnchor).Active = true;
            containerView.BottomAnchor.ConstraintEqualTo(scrollView.BottomAnchor).Active = true;
            containerView.WidthAnchor.ConstraintEqualTo(scrollView.WidthAnchor).Active = true;

            var titleLabel = new UILabel(CGRect.Empty);
            titleLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            titleLabel.TextColor = UIColor.Black;
            titleLabel.Font = UIFont.BoldSystemFontOfSize(18);
            titleLabel.Text = "Service Details";
            containerView.Add(titleLabel);

            titleLabel.LeadingAnchor.ConstraintEqualTo(containerView.LeadingAnchor, 50).Active = true;
            titleLabel.TrailingAnchor.ConstraintEqualTo(containerView.TrailingAnchor, -50).Active = true;
            titleLabel.TopAnchor.ConstraintEqualTo(containerView.TopAnchor, 30).Active = true;
            titleLabel.HeightAnchor.ConstraintEqualTo(30).Active = true;

            var customerNameBackgroundView = new UIView(CGRect.Empty);
            customerNameBackgroundView.TranslatesAutoresizingMaskIntoConstraints = false;
            customerNameBackgroundView.BackgroundColor = UIColor.FromRGB(245.0f / 255.0f, 245.0f / 255.0f, 245.0f / 255.0f);
            containerView.Add(customerNameBackgroundView);

            var customerNameTitleLabel = new UILabel(CGRect.Empty);
            customerNameTitleLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            customerNameTitleLabel.TextColor = UIColor.FromRGB(39.0f / 255.0f, 68.0f / 255.0f, 110.0f / 255.0f);
            customerNameTitleLabel.Font = UIFont.SystemFontOfSize(18);
            customerNameTitleLabel.Text = "Customer Name:";
            customerNameBackgroundView.Add(customerNameTitleLabel);

            customerNameLabel = new UILabel(CGRect.Empty);
            customerNameLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            customerNameLabel.TextColor = UIColor.FromRGB(36.0f / 255.0f, 72.0f / 255.0f, 154.0f / 255.0f);
            customerNameLabel.Font = UIFont.SystemFontOfSize(18, UIFontWeight.Semibold);
            customerNameBackgroundView.Add(customerNameLabel);

            customerNameBackgroundView.LeadingAnchor.ConstraintEqualTo(containerView.LeadingAnchor, 50).Active = true;
            customerNameBackgroundView.TrailingAnchor.ConstraintEqualTo(containerView.TrailingAnchor, -50).Active = true;
            customerNameBackgroundView.HeightAnchor.ConstraintEqualTo(58).Active = true;
            customerNameBackgroundView.TopAnchor.ConstraintEqualTo(titleLabel.BottomAnchor, 15).Active = true;

            customerNameTitleLabel.LeadingAnchor.ConstraintEqualTo(customerNameBackgroundView.LeadingAnchor, 24).Active = true;
            customerNameTitleLabel.TrailingAnchor.ConstraintEqualTo(customerNameBackgroundView.CenterXAnchor).Active = true;
            customerNameTitleLabel.CenterYAnchor.ConstraintEqualTo(customerNameBackgroundView.CenterYAnchor).Active = true;

            customerNameLabel.LeadingAnchor.ConstraintEqualTo(customerNameBackgroundView.CenterXAnchor, 20).Active = true;
            customerNameLabel.TrailingAnchor.ConstraintEqualTo(customerNameBackgroundView.TrailingAnchor, -24).Active = true;
            customerNameLabel.CenterYAnchor.ConstraintEqualTo(customerNameBackgroundView.CenterYAnchor).Active = true;

            var barCodeBackgroundView = new UIView(CGRect.Empty);
            barCodeBackgroundView.TranslatesAutoresizingMaskIntoConstraints = false;
            barCodeBackgroundView.BackgroundColor = UIColor.FromRGB(245.0f / 255.0f, 245.0f / 255.0f, 245.0f / 255.0f);
            containerView.Add(barCodeBackgroundView);

            var barCodeTitleLabel = new UILabel(CGRect.Empty);
            barCodeTitleLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            barCodeTitleLabel.TextColor = UIColor.FromRGB(39.0f / 255.0f, 68.0f / 255.0f, 110.0f / 255.0f);
            barCodeTitleLabel.Font = UIFont.SystemFontOfSize(18);
            barCodeTitleLabel.Text = "Barcode:";
            barCodeBackgroundView.Add(barCodeTitleLabel);

            barcodeLabel = new UILabel(CGRect.Empty);
            barcodeLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            barcodeLabel.TextColor = UIColor.FromRGB(36.0f / 255.0f, 72.0f / 255.0f, 154.0f / 255.0f);
            barcodeLabel.Font = UIFont.SystemFontOfSize(18, UIFontWeight.Semibold);
            barCodeBackgroundView.Add(barcodeLabel);

            barCodeBackgroundView.LeadingAnchor.ConstraintEqualTo(containerView.LeadingAnchor, 50).Active = true;
            barCodeBackgroundView.TrailingAnchor.ConstraintEqualTo(containerView.TrailingAnchor, -50).Active = true;
            barCodeBackgroundView.HeightAnchor.ConstraintEqualTo(58).Active = true;
            barCodeBackgroundView.TopAnchor.ConstraintEqualTo(customerNameBackgroundView.BottomAnchor, 15).Active = true;

            barCodeTitleLabel.LeadingAnchor.ConstraintEqualTo(barCodeBackgroundView.LeadingAnchor, 24).Active = true;
            barCodeTitleLabel.TrailingAnchor.ConstraintEqualTo(barCodeBackgroundView.CenterXAnchor).Active = true;
            barCodeTitleLabel.CenterYAnchor.ConstraintEqualTo(barCodeBackgroundView.CenterYAnchor).Active = true;

            barcodeLabel.LeadingAnchor.ConstraintEqualTo(barCodeBackgroundView.CenterXAnchor, 20).Active = true;
            barcodeLabel.TrailingAnchor.ConstraintEqualTo(barCodeBackgroundView.TrailingAnchor, -24).Active = true;
            barcodeLabel.CenterYAnchor.ConstraintEqualTo(barCodeBackgroundView.CenterYAnchor).Active = true;

            var makeBackgroundView = new UIView(CGRect.Empty);
            makeBackgroundView.TranslatesAutoresizingMaskIntoConstraints = false;
            makeBackgroundView.BackgroundColor = UIColor.FromRGB(245.0f / 255.0f, 245.0f / 255.0f, 245.0f / 255.0f);
            containerView.Add(makeBackgroundView);

            var makeTitleLabel = new UILabel(CGRect.Empty);
            makeTitleLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            makeTitleLabel.TextColor = UIColor.FromRGB(39.0f / 255.0f, 68.0f / 255.0f, 110.0f / 255.0f);
            makeTitleLabel.Font = UIFont.SystemFontOfSize(18);
            makeTitleLabel.Text = "Make:";
            makeBackgroundView.Add(makeTitleLabel);

            makeLabel = new UILabel(CGRect.Empty);
            makeLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            makeLabel.TextColor = UIColor.FromRGB(36.0f / 255.0f, 72.0f / 255.0f, 154.0f / 255.0f);
            makeLabel.Font = UIFont.SystemFontOfSize(18, UIFontWeight.Semibold);
            makeBackgroundView.Add(makeLabel);

            makeBackgroundView.LeadingAnchor.ConstraintEqualTo(containerView.LeadingAnchor, 50).Active = true;
            makeBackgroundView.TrailingAnchor.ConstraintEqualTo(containerView.TrailingAnchor, -50).Active = true;
            makeBackgroundView.HeightAnchor.ConstraintEqualTo(58).Active = true;
            makeBackgroundView.TopAnchor.ConstraintEqualTo(barCodeBackgroundView.BottomAnchor, 5).Active = true;

            makeTitleLabel.LeadingAnchor.ConstraintEqualTo(makeBackgroundView.LeadingAnchor, 24).Active = true;
            makeTitleLabel.TrailingAnchor.ConstraintEqualTo(makeBackgroundView.CenterXAnchor).Active = true;
            makeTitleLabel.CenterYAnchor.ConstraintEqualTo(makeBackgroundView.CenterYAnchor).Active = true;

            makeLabel.LeadingAnchor.ConstraintEqualTo(makeBackgroundView.CenterXAnchor, 20).Active = true;
            makeLabel.TrailingAnchor.ConstraintEqualTo(makeBackgroundView.TrailingAnchor, -24).Active = true;
            makeLabel.CenterYAnchor.ConstraintEqualTo(makeBackgroundView.CenterYAnchor).Active = true;

            var modelBackgroundView = new UIView(CGRect.Empty);
            modelBackgroundView.TranslatesAutoresizingMaskIntoConstraints = false;
            modelBackgroundView.BackgroundColor = UIColor.FromRGB(245.0f / 255.0f, 245.0f / 255.0f, 245.0f / 255.0f);
            containerView.Add(modelBackgroundView);

            var modelTitleLabel = new UILabel(CGRect.Empty);
            modelTitleLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            modelTitleLabel.TextColor = UIColor.FromRGB(39.0f / 255.0f, 68.0f / 255.0f, 110.0f / 255.0f);
            modelTitleLabel.Font = UIFont.SystemFontOfSize(18);
            modelTitleLabel.Text = "Model:";
            modelBackgroundView.Add(modelTitleLabel);

            modelLabel = new UILabel(CGRect.Empty);
            modelLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            modelLabel.TextColor = UIColor.FromRGB(36.0f / 255.0f, 72.0f / 255.0f, 154.0f / 255.0f);
            modelLabel.Font = UIFont.SystemFontOfSize(18, UIFontWeight.Semibold);
            modelBackgroundView.Add(modelLabel);

            modelBackgroundView.LeadingAnchor.ConstraintEqualTo(containerView.LeadingAnchor, 50).Active = true;
            modelBackgroundView.TrailingAnchor.ConstraintEqualTo(containerView.TrailingAnchor, -50).Active = true;
            modelBackgroundView.HeightAnchor.ConstraintEqualTo(58).Active = true;
            modelBackgroundView.TopAnchor.ConstraintEqualTo(makeBackgroundView.BottomAnchor, 5).Active = true;

            modelTitleLabel.LeadingAnchor.ConstraintEqualTo(modelBackgroundView.LeadingAnchor, 24).Active = true;
            modelTitleLabel.TrailingAnchor.ConstraintEqualTo(modelBackgroundView.CenterXAnchor).Active = true;
            modelTitleLabel.CenterYAnchor.ConstraintEqualTo(modelBackgroundView.CenterYAnchor).Active = true;

            modelLabel.LeadingAnchor.ConstraintEqualTo(modelBackgroundView.CenterXAnchor, 20).Active = true;
            modelLabel.TrailingAnchor.ConstraintEqualTo(modelBackgroundView.TrailingAnchor, -24).Active = true;
            modelLabel.CenterYAnchor.ConstraintEqualTo(modelBackgroundView.CenterYAnchor).Active = true;

            var vechileColorBackgroundView = new UIView(CGRect.Empty);
            vechileColorBackgroundView.TranslatesAutoresizingMaskIntoConstraints = false;
            vechileColorBackgroundView.BackgroundColor = UIColor.FromRGB(245.0f / 255.0f, 245.0f / 255.0f, 245.0f / 255.0f);
            containerView.Add(vechileColorBackgroundView);

            var vechileColorTitleLabel = new UILabel(CGRect.Empty);
            vechileColorTitleLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            vechileColorTitleLabel.TextColor = UIColor.FromRGB(39.0f / 255.0f, 68.0f / 255.0f, 110.0f / 255.0f);
            vechileColorTitleLabel.Font = UIFont.SystemFontOfSize(18);
            vechileColorTitleLabel.Text = "Color:";
            vechileColorBackgroundView.Add(vechileColorTitleLabel);

            vechileColorLabel = new UILabel(CGRect.Empty);
            vechileColorLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            vechileColorLabel.TextColor = UIColor.FromRGB(36.0f / 255.0f, 72.0f / 255.0f, 154.0f / 255.0f);
            vechileColorLabel.Font = UIFont.SystemFontOfSize(18, UIFontWeight.Semibold);
            vechileColorBackgroundView.Add(vechileColorLabel);

            vechileColorBackgroundView.LeadingAnchor.ConstraintEqualTo(containerView.LeadingAnchor, 50).Active = true;
            vechileColorBackgroundView.TrailingAnchor.ConstraintEqualTo(containerView.TrailingAnchor, -50).Active = true;
            vechileColorBackgroundView.HeightAnchor.ConstraintEqualTo(58).Active = true;
            vechileColorBackgroundView.TopAnchor.ConstraintEqualTo(modelBackgroundView.BottomAnchor, 5).Active = true;

            vechileColorTitleLabel.LeadingAnchor.ConstraintEqualTo(vechileColorBackgroundView.LeadingAnchor, 24).Active = true;
            vechileColorTitleLabel.TrailingAnchor.ConstraintEqualTo(vechileColorBackgroundView.CenterXAnchor).Active = true;
            vechileColorTitleLabel.CenterYAnchor.ConstraintEqualTo(vechileColorBackgroundView.CenterYAnchor).Active = true;

            vechileColorLabel.LeadingAnchor.ConstraintEqualTo(vechileColorBackgroundView.CenterXAnchor, 20).Active = true;
            vechileColorLabel.TrailingAnchor.ConstraintEqualTo(vechileColorBackgroundView.TrailingAnchor, -24).Active = true;
            vechileColorLabel.CenterYAnchor.ConstraintEqualTo(vechileColorBackgroundView.CenterYAnchor).Active = true;

            var additionalServicesBackgroundView = new UIView(CGRect.Empty);
            additionalServicesBackgroundView.TranslatesAutoresizingMaskIntoConstraints = false;
            additionalServicesBackgroundView.BackgroundColor = UIColor.FromRGB(245.0f / 255.0f, 245.0f / 255.0f, 245.0f / 255.0f);
            containerView.Add(additionalServicesBackgroundView);

            var additionalServicesTitleLabel = new UILabel(CGRect.Empty);
            additionalServicesTitleLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            additionalServicesTitleLabel.TextColor = UIColor.FromRGB(39.0f / 255.0f, 68.0f / 255.0f, 110.0f / 255.0f);
            additionalServicesTitleLabel.Font = UIFont.SystemFontOfSize(18);
            additionalServicesTitleLabel.Text = "Additional Services:";
            additionalServicesBackgroundView.Add(additionalServicesTitleLabel);

            additionalServicesLabel = new UILabel(CGRect.Empty);
            additionalServicesLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            additionalServicesLabel.TextColor = UIColor.FromRGB(36.0f / 255.0f, 72.0f / 255.0f, 154.0f / 255.0f);
            additionalServicesLabel.Font = UIFont.SystemFontOfSize(18, UIFontWeight.Semibold);
            additionalServicesBackgroundView.Add(additionalServicesLabel);

            additionalServicesBackgroundView.LeadingAnchor.ConstraintEqualTo(containerView.LeadingAnchor, 50).Active = true;
            additionalServicesBackgroundView.TrailingAnchor.ConstraintEqualTo(containerView.TrailingAnchor, -50).Active = true;
            additionalServicesBackgroundView.HeightAnchor.ConstraintEqualTo(58).Active = true;
            additionalServicesBackgroundView.TopAnchor.ConstraintEqualTo(vechileColorBackgroundView.BottomAnchor, 5).Active = true;

            additionalServicesTitleLabel.LeadingAnchor.ConstraintEqualTo(additionalServicesBackgroundView.LeadingAnchor, 24).Active = true;
            additionalServicesTitleLabel.TrailingAnchor.ConstraintEqualTo(additionalServicesBackgroundView.CenterXAnchor).Active = true;
            additionalServicesTitleLabel.CenterYAnchor.ConstraintEqualTo(additionalServicesBackgroundView.CenterYAnchor).Active = true;

            additionalServicesLabel.LeadingAnchor.ConstraintEqualTo(additionalServicesBackgroundView.CenterXAnchor, 20).Active = true;
            additionalServicesLabel.TrailingAnchor.ConstraintEqualTo(additionalServicesBackgroundView.TrailingAnchor, -24).Active = true;
            additionalServicesLabel.CenterYAnchor.ConstraintEqualTo(additionalServicesBackgroundView.CenterYAnchor).Active = true;

            var detailPackageServicesBackgroundView = new UIView(CGRect.Empty);
            detailPackageServicesBackgroundView.TranslatesAutoresizingMaskIntoConstraints = false;
            detailPackageServicesBackgroundView.BackgroundColor = UIColor.FromRGB(245.0f / 255.0f, 245.0f / 255.0f, 245.0f / 255.0f);
            containerView.Add(detailPackageServicesBackgroundView);

            var detailPackageServicesTitleLabel = new UILabel(CGRect.Empty);
            detailPackageServicesTitleLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            detailPackageServicesTitleLabel.TextColor = UIColor.FromRGB(39.0f / 255.0f, 68.0f / 255.0f, 110.0f / 255.0f);
            detailPackageServicesTitleLabel.Font = UIFont.SystemFontOfSize(18);
            detailPackageServicesTitleLabel.Text = "Package Service:";
            detailPackageServicesBackgroundView.Add(detailPackageServicesTitleLabel);

            detailPackageServicesLabel = new UILabel(CGRect.Empty);
            detailPackageServicesLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            detailPackageServicesLabel.TextColor = UIColor.FromRGB(36.0f / 255.0f, 72.0f / 255.0f, 154.0f / 255.0f);
            detailPackageServicesLabel.Font = UIFont.SystemFontOfSize(18, UIFontWeight.Semibold);
            detailPackageServicesBackgroundView.Add(detailPackageServicesLabel);

            detailPackageServicesBackgroundView.LeadingAnchor.ConstraintEqualTo(containerView.LeadingAnchor, 50).Active = true;
            detailPackageServicesBackgroundView.TrailingAnchor.ConstraintEqualTo(containerView.TrailingAnchor, -50).Active = true;
            detailPackageServicesBackgroundView.HeightAnchor.ConstraintEqualTo(58).Active = true;
            detailPackageServicesBackgroundView.TopAnchor.ConstraintEqualTo(additionalServicesBackgroundView.BottomAnchor, 5).Active = true;

            detailPackageServicesTitleLabel.LeadingAnchor.ConstraintEqualTo(detailPackageServicesBackgroundView.LeadingAnchor, 24).Active = true;
            detailPackageServicesTitleLabel.TrailingAnchor.ConstraintEqualTo(detailPackageServicesBackgroundView.CenterXAnchor).Active = true;
            detailPackageServicesTitleLabel.CenterYAnchor.ConstraintEqualTo(detailPackageServicesBackgroundView.CenterYAnchor).Active = true;

            detailPackageServicesLabel.LeadingAnchor.ConstraintEqualTo(detailPackageServicesBackgroundView.CenterXAnchor, 20).Active = true;
            detailPackageServicesLabel.TrailingAnchor.ConstraintEqualTo(detailPackageServicesBackgroundView.TrailingAnchor, -24).Active = true;
            detailPackageServicesLabel.CenterYAnchor.ConstraintEqualTo(detailPackageServicesBackgroundView.CenterYAnchor).Active = true;

            var timeBackgroundView = new UIView(CGRect.Empty);
            timeBackgroundView.TranslatesAutoresizingMaskIntoConstraints = false;
            timeBackgroundView.BackgroundColor = UIColor.FromRGB(245.0f / 255.0f, 245.0f / 255.0f, 245.0f / 255.0f);
            containerView.Add(timeBackgroundView);

            var startTimeTitleLabel = new UILabel(CGRect.Empty);
            startTimeTitleLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            startTimeTitleLabel.TextColor = UIColor.FromRGB(39.0f / 255.0f, 68.0f / 255.0f, 110.0f / 255.0f);
            startTimeTitleLabel.Font = UIFont.SystemFontOfSize(18);
            startTimeTitleLabel.Text = "Start Time:";
            timeBackgroundView.Add(startTimeTitleLabel);

            startTimeLabel = new UILabel(CGRect.Empty);
            startTimeLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            startTimeLabel.TextColor = UIColor.FromRGB(36.0f / 255.0f, 72.0f / 255.0f, 154.0f / 255.0f);
            startTimeLabel.Font = UIFont.SystemFontOfSize(18, UIFontWeight.Semibold);
            timeBackgroundView.Add(startTimeLabel);

            var endTimeTitleLabel = new UILabel(CGRect.Empty);
            endTimeTitleLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            endTimeTitleLabel.TextColor = UIColor.FromRGB(39.0f / 255.0f, 68.0f / 255.0f, 110.0f / 255.0f);
            endTimeTitleLabel.Font = UIFont.SystemFontOfSize(18);
            endTimeTitleLabel.Text = "End Time:";
            timeBackgroundView.Add(endTimeTitleLabel);

            endTimeLabel = new UILabel(CGRect.Empty);
            endTimeLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            endTimeLabel.TextColor = UIColor.FromRGB(36.0f / 255.0f, 72.0f / 255.0f, 154.0f / 255.0f);
            endTimeLabel.Font = UIFont.SystemFontOfSize(18, UIFontWeight.Semibold);
            timeBackgroundView.Add(endTimeLabel);

            timeBackgroundView.LeadingAnchor.ConstraintEqualTo(containerView.LeadingAnchor, 50).Active = true;
            timeBackgroundView.TrailingAnchor.ConstraintEqualTo(containerView.TrailingAnchor, -50).Active = true;
            timeBackgroundView.HeightAnchor.ConstraintEqualTo(58).Active = true;
            timeBackgroundView.TopAnchor.ConstraintEqualTo(detailPackageServicesBackgroundView.BottomAnchor, 5).Active = true;

            startTimeTitleLabel.LeadingAnchor.ConstraintEqualTo(timeBackgroundView.LeadingAnchor, 24).Active = true;
            startTimeTitleLabel.CenterYAnchor.ConstraintEqualTo(timeBackgroundView.CenterYAnchor).Active = true;

            startTimeLabel.LeadingAnchor.ConstraintEqualTo(startTimeTitleLabel.TrailingAnchor, 5).Active = true;
            startTimeLabel.TrailingAnchor.ConstraintEqualTo(timeBackgroundView.CenterXAnchor, constant: 20).Active = true;
            startTimeLabel.CenterYAnchor.ConstraintEqualTo(timeBackgroundView.CenterYAnchor).Active = true;
            startTimeLabel.SetContentHuggingPriority(249, UILayoutConstraintAxis.Horizontal);

            endTimeTitleLabel.LeadingAnchor.ConstraintEqualTo(timeBackgroundView.CenterXAnchor).Active = true;
            endTimeTitleLabel.CenterYAnchor.ConstraintEqualTo(timeBackgroundView.CenterYAnchor).Active = true;

            endTimeLabel.LeadingAnchor.ConstraintEqualTo(endTimeTitleLabel.TrailingAnchor, 5).Active = true;
            endTimeLabel.TrailingAnchor.ConstraintEqualTo(timeBackgroundView.TrailingAnchor, -24).Active = true;
            endTimeLabel.CenterYAnchor.ConstraintEqualTo(timeBackgroundView.CenterYAnchor).Active = true;
            endTimeLabel.SetContentHuggingPriority(249, UILayoutConstraintAxis.Horizontal);

            var notesBackgroundView = new UIView(CGRect.Empty);
            notesBackgroundView.TranslatesAutoresizingMaskIntoConstraints = false;
            notesBackgroundView.BackgroundColor = UIColor.FromRGB(245.0f / 255.0f, 245.0f / 255.0f, 245.0f / 255.0f);
            containerView.Add(notesBackgroundView);

            var notesTitleLabel = new UILabel(CGRect.Empty);
            notesTitleLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            notesTitleLabel.TextColor = UIColor.FromRGB(39.0f / 255.0f, 68.0f / 255.0f, 110.0f / 255.0f);
            notesTitleLabel.Font = UIFont.SystemFontOfSize(18);
            notesTitleLabel.Text = "Completion Notes:";
            notesBackgroundView.Add(notesTitleLabel);

            notesLabel = new UILabel(CGRect.Empty);
            notesLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            notesLabel.TextColor = UIColor.FromRGB(36.0f / 255.0f, 72.0f / 255.0f, 154.0f / 255.0f);
            notesLabel.Font = UIFont.SystemFontOfSize(18, UIFontWeight.Semibold);
            notesLabel.Lines = -1;
            notesBackgroundView.Add(notesLabel);

            notesBackgroundView.LeadingAnchor.ConstraintEqualTo(containerView.LeadingAnchor, 50).Active = true;
            notesBackgroundView.TrailingAnchor.ConstraintEqualTo(containerView.TrailingAnchor, -50).Active = true;
            notesBackgroundView.TopAnchor.ConstraintEqualTo(timeBackgroundView.BottomAnchor, 5).Active = true;
            notesBackgroundView.BottomAnchor.ConstraintEqualTo(containerView.BottomAnchor, -35).Active = true;

            notesTitleLabel.LeadingAnchor.ConstraintEqualTo(notesBackgroundView.LeadingAnchor, 24).Active = true;
            notesTitleLabel.TrailingAnchor.ConstraintEqualTo(notesBackgroundView.TrailingAnchor, -24).Active = true;
            notesTitleLabel.TopAnchor.ConstraintEqualTo(notesBackgroundView.TopAnchor, 20).Active = true;

            notesLabel.LeadingAnchor.ConstraintEqualTo(notesBackgroundView.LeadingAnchor, 24).Active = true;
            notesLabel.TrailingAnchor.ConstraintEqualTo(notesBackgroundView.TrailingAnchor, -24).Active = true;
            notesLabel.TopAnchor.ConstraintEqualTo(notesTitleLabel.BottomAnchor, 5).Active = true;
            notesLabel.BottomAnchor.ConstraintEqualTo(notesBackgroundView.BottomAnchor, -20).Active = true;
        }

        void UpdateDataToUI(DTOs.Service service, List<LastServiceJobItem> lastServiceJobItems = null)
        {
            serviceDateLabel.Text = service.JobDate.ToString("MM/dd/yyyy");
            serviceNameLabel.Text = service.JobTypeName;
            barcodeLabel.Text = service?.Barcode ?? "-";
            makeLabel.Text = service.VehicleMake;
            modelLabel.Text = service.VehicleModel;
            vechileColorLabel.Text = service.VehicleColor;

            customerNameLabel.Text = service.CustName ?? "-";

            //additionalServicesLabel.Text = "None";

            startTimeLabel.Text = service.TimeIn.ToShortTimeString();
            endTimeLabel.Text = service.EstimatedTimeOut.ToShortTimeString();
            notesLabel.Text = service.ReviewNote ?? "-";

            if (service.JobTypeName.Equals("wash", System.StringComparison.OrdinalIgnoreCase))
            {
                detailPackageServicesLabel.Text = lastServiceJobItems.Where(x => x.ServiceType.Equals("Wash Package", System.StringComparison.OrdinalIgnoreCase)).FirstOrDefault().ServiceName;
            }
            else if (service.JobTypeName.Equals("detail", System.StringComparison.OrdinalIgnoreCase))
            {
                detailPackageServicesLabel.Text = lastServiceJobItems.Where(x => x.ServiceType.Equals("Detail Package", System.StringComparison.OrdinalIgnoreCase)).FirstOrDefault().ServiceName;
            }

            var additionalServices = lastServiceJobItems.Where(x => x.ServiceType.Equals("Additional Services", System.StringComparison.OrdinalIgnoreCase)).ToList();

            //FirstOrDefault().ServiceName;

            var addtionalServices = additionalServices.Select(x => x.ServiceName).ToArray();

            //if (additionalServices.Count == 1)
            //{

            //}

            string addtionalServiceNames = string.Join(", ", addtionalServices);

            additionalServicesLabel.Text =  !string.IsNullOrEmpty(addtionalServiceNames) ? addtionalServiceNames : "None";
        }
    }
}