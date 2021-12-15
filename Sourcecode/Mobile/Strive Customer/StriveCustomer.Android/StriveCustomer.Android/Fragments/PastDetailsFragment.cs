using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.IoC;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;
using StriveCustomer.Android.Adapter;

namespace StriveCustomer.Android.Fragments
{
    [MvxUnconventionalAttribute]
    public class PastDetailsFragment : MvxFragment<PastDetailViewModel>
    {
        Context context;
        int previousID = 0;
        float totalCost;
        string previousDates;
        List<float> TotalCost;
        private Dictionary<float, int> TotalServiceCosts;
        private PastClientServices pastClientServices;
        private RecyclerView detailsRecyclerView;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootview = this.BindingInflate(Resource.Layout.PastDetailsScreenFragment, null);
            this.ViewModel = new PastDetailViewModel();
            CustomerInfo.TotalCost = new List<float>();
            pastClientServices = new PastClientServices();
            pastClientServices.PastClientDetails = new List<PastClientDetails>();
            CustomerInfo.pastClientServices = new PastClientServices();
            CustomerInfo.pastClientServices.PastClientDetails = new List<PastClientDetails>();
            TotalServiceCosts = new Dictionary<float, int>();
            GetPastDetails();
            detailsRecyclerView = rootview.FindViewById<RecyclerView>(Resource.Id.pastDetailsList);
            detailsRecyclerView.HasFixedSize = true;
            var layoutManager = new LinearLayoutManager(context);
            detailsRecyclerView.SetLayoutManager(layoutManager);
            
            
            return rootview;
        }
        public async void GetPastDetails()
        {
            totalCost = 0;
            int previousID = 0;
            string previousVehicle;
            if (this.ViewModel == null)
            {
                ViewModel = new PastDetailViewModel();
            }
            var result = await this.ViewModel.GetPastDetailsServices();
            PastDetailsCompleteDetails.pastClientServices = result;
            if (result != null && result.PastClientDetails.Count != 0)
            {
                var count = 0;
                previousID = result.PastClientDetails[0].VehicleId;
                previousDates = result.PastClientDetails[0].DetailVisitDate;
                previousVehicle = result.PastClientDetails[0].Color +" "+ result.PastClientDetails[0].Make + " " + result.PastClientDetails[0].Model;
                foreach (var data in result.PastClientDetails)
                {
                    if (String.Equals(data.DetailOrAdditionalService, "Details") && string.Equals(data.DetailVisitDate,previousDates))
                    {
                        CustomerInfo.pastClientServices.PastClientDetails.Add(data);
                        totalCost = totalCost + float.Parse(data.Cost);
                    }
                    else if(String.Equals(data.DetailOrAdditionalService, "Additional Services") && string.Equals(data.DetailVisitDate, previousDates))
                    {
                        totalCost = totalCost + float.Parse(data.Cost);

                    }
                    else if(String.Equals(data.DetailOrAdditionalService, "Details") && !string.Equals(data.DetailVisitDate, previousDates))
                    {
                        CustomerInfo.TotalCost.Add(totalCost);
                        totalCost = 0;
                        CustomerInfo.pastClientServices.PastClientDetails.Add(data);
                        totalCost = totalCost + float.Parse(data.Cost);
                    }
                    count++;
                    if(count == result.PastClientDetails.Count)
                    {
                        CustomerInfo.TotalCost.Add(totalCost);
                        totalCost = 0;
                    }
                    previousID = data.VehicleId;
                    previousDates = data.DetailVisitDate;
                }
                var counts = 0;
                foreach (var data in CustomerInfo.pastClientServices.PastClientDetails)
                {
                    if(data.VehicleId != previousID || counts == 0)
                    {
                        pastClientServices.PastClientDetails.Add(data);
                        previousID = data.VehicleId;
                    }
                    counts++;
                }
                PastDetailsAdapter pastDetailsAdapter = new PastDetailsAdapter(pastClientServices, context);
                detailsRecyclerView.SetAdapter(pastDetailsAdapter);
            }
        }
    }
    public class PastDetailsCompleteDetails
    {
        public static PastClientServices pastClientServices { get; set; }
    }
}
