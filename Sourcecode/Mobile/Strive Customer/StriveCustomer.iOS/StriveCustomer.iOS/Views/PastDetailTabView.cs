using System;
using Acr.UserDialogs;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.Models.Customer;
using Strive.Core.Utils;
using Strive.Core.ViewModels.Customer;
using UIKit;

namespace StriveCustomer.iOS.Views
{
    public partial class PastDetailTabView : UIViewController
    {
        private int pastDetailDates = 0;
        private PastClientDetails segment1;
        private PastClientDetails segment2;
        private PastClientDetails segment3;
        public PastDetailTabView() : base("PastDetailTabView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            PastDetailTab_ParentView.Layer.CornerRadius = 5;
            getData();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private void getData()
        {            
            var data = CustomerInfo.SelectedVehiclePastDetails;
            var count = 0;

            foreach (var result in CustomerInfo.pastClientServices.PastClientDetails)
            {
                if (data == result.VehicleId)
                {
                    pastDetailDates++;
                    if (pastDetailDates == 1)
                    {
                        segment1 = result;

                        setView(result, result.Cost);
                        var splits = result.DetailVisitDate.Split('T');
                        var dateTime = DateUtils.GetDateFromString(splits[0]);
                        var finalDate = dateTime.ToString("MM/dd/yyyy");
                        PastDetailTab_SegmentCtrl.SetTitle(finalDate, 0);
                    }
                    if (pastDetailDates == 2)
                    {
                        segment2 = result;
                        
                        var splits = result.DetailVisitDate.Split('T');
                        var dateTime = DateUtils.GetDateFromString(splits[0]);
                        var finalDate = dateTime.ToString("MM/dd/yyyy");
                        PastDetailTab_SegmentCtrl.SetTitle(finalDate, 1);
                    }
                    if (pastDetailDates == 3)
                    {
                        segment3 = result;
                        
                        var splits = result.DetailVisitDate.Split('T');
                        var dateTime = DateUtils.GetDateFromString(splits[0]);
                        var finalDate = dateTime.ToString("MM/dd/yyyy");
                        PastDetailTab_SegmentCtrl.SetTitle(finalDate, 2);
                    }                    
                }               
                               
                count++;
            }
            if (pastDetailDates == 1)
            {
                PastDetailTab_SegmentCtrl.RemoveSegmentAtIndex(2, true);
                PastDetailTab_SegmentCtrl.RemoveSegmentAtIndex(1, true);
            }
            else if (pastDetailDates == 2)
            {
                PastDetailTab_SegmentCtrl.RemoveSegmentAtIndex(2, true);
            }
        }

        private void setView(PastClientDetails details, string totalCost)
        {
            UserDialogs.Instance.Loading("Loading", null, null, true, MaskType.Black);
            var CarName = details.Color + " " + details.Make + " " + details.Model;
            CarNameValue.Text = CarName;
            BarcodeValue.Text = details.Barcode;
            MakeValue.Text = details.Make;
            ModelValue.Text = details.Model;
            ColorValue.Text = details.Color;
            var splits = details.DetailVisitDate.Split('T');
            var dateTime = DateUtils.GetDateFromString(splits[0]);
            var finalDate = dateTime.ToString("MM/dd/yyyy");
            VisitDateValue.Text = finalDate;
            PackageNameValue.Text = details.ServiceName;

            foreach (var data in PastDetailsCompleteDetails.pastClientServices.PastClientDetails)
            {
                if (details.DetailVisitDate == data.DetailVisitDate && details.VehicleId == data.VehicleId)
                {
                    if (string.Equals(data.DetailOrAdditionalService, "Additional Services"))
                    {
                        AdditionalServiceValue.Text = "Yes";
                    }
                    else
                    {
                        AdditionalServiceValue.Text = "No";
                    }
                }
            }

            PriceValue.Text = totalCost.ToString();
            UserDialogs.Instance.Loading("Loading", null, null, true, MaskType.Black).Hide();
        }

        partial void DateSegment_PD(UISegmentedControl sender)
        {           
            var index = PastDetailTab_SegmentCtrl.SelectedSegment;
            if (index == 0)
            {
                setView(segment1, segment1.Cost);
            }
            else if (index == 1)
            {               
                setView(segment2, segment2.Cost);
            }
            else if (index == 2)
            {
                setView(segment3, segment3.Cost);
            }              
        }
    }
}