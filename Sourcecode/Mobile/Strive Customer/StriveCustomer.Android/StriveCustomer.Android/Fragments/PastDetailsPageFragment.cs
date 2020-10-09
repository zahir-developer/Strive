using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.IoC;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;

namespace StriveCustomer.Android.Fragments
{
    [MvxUnconventionalAttribute]
    public class PastDetailsPageFragment : MvxFragment<PastDetailViewModel>
    {
        PastClientDetails pastClient;
        float TotalCost;
        public PastDetailsPageFragment(PastClientDetails pastClient, float TotalCost)
        {
            this.pastClient = new PastClientDetails();
            this.TotalCost = TotalCost;
            this.pastClient = pastClient;
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootview = this.BindingInflate(Resource.Layout.PastDetailsPageFragment, null);
            
            
            rootview.FindViewById<TextView>(Resource.Id.carModelName).Text = pastClient.Color + " " + pastClient.Make + " " + pastClient.Model;
            rootview.FindViewById<TextView>(Resource.Id.barcodeNumber).Text = pastClient.Barcode;
            rootview.FindViewById<TextView>(Resource.Id.carMake).Text = pastClient.Make;
            rootview.FindViewById<TextView>(Resource.Id.carModel).Text = pastClient.Model;
            rootview.FindViewById<TextView>(Resource.Id.carColor).Text = pastClient.Color;
            var splits = pastClient.DetailVisitDate.Split('T');
            rootview.FindViewById<TextView>(Resource.Id.carVisitDate).Text = splits[0];
            rootview.FindViewById<TextView>(Resource.Id.packageName).Text = pastClient.ServiceName;
            foreach (var data in PastDetailsCompleteDetails.pastClientServices.PastClientDetails)
            {
               if(pastClient.DetailVisitDate == data.DetailVisitDate && pastClient.VehicleId == data.VehicleId)
                {
                    if(string.Equals(data.DetailOrAdditionalService, "Additional Services"))
                    {
                        rootview.FindViewById<TextView>(Resource.Id.additionalService).Text = "Yes";
                    }
                    else
                    {
                        rootview.FindViewById<TextView>(Resource.Id.additionalService).Text = "No";
                    }
                }
            }
                
            rootview.FindViewById<TextView>(Resource.Id.washPrice).Text = TotalCost.ToString();

            return rootview;
        }
    }
}
