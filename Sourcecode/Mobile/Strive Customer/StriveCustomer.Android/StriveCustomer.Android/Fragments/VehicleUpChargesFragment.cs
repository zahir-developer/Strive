using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.IoC;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;

namespace StriveCustomer.Android.Fragments
{
    [MvxUnconventionalAttribute]
    public class VehicleUpChargesFragment : MvxFragment<MyProfileInfoViewModel>
    {
        VehicleAdditionalServicesFragment additionalServicesFragment;
        MyProfileInfoViewModel mpvm = new MyProfileInfoViewModel();
        private RadioGroup upchargeOptions;
        private Dictionary<int, int> upchargeRadio;
        int someId = 12348880;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootview = inflater.Inflate(Resource.Layout.VehicleUpChargesFragment,null);
            additionalServicesFragment = new VehicleAdditionalServicesFragment();
            upchargeRadio = new Dictionary<int, int>();
            upchargeOptions = rootview.FindViewById<RadioGroup>(Resource.Id.upchargesOptions);
            upchargeOptions.CheckedChange += UpchargeOptions_CheckedChange;
            ServiceDetails();
            return rootview;
        }

        private void UpchargeOptions_CheckedChange(object sender, RadioGroup.CheckedChangeEventArgs e)
        {
            CustomerInfo.selectedUpCharge = upchargeRadio.FirstOrDefault(x => x.Value == e.CheckedId ).Key;
            AppCompatActivity activity = (AppCompatActivity)Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, additionalServicesFragment).Commit();
        }

        private async void ServiceDetails()
        {
            await mpvm.getServiceList(CustomerInfo.selectedMemberShip);
            await mpvm.getAllServiceList();
            if(CustomerInfo.filteredList != null)
            {
                foreach(var result in CustomerInfo.filteredList.ServicesWithPrice)
                {
                    if(string.Equals(result.ServiceTypeName, "Upcharges"))
                    {
                        RadioButton upChargeRadio = new RadioButton(Context);
                        upChargeRadio.Text = result.Upcharges;
                        upChargeRadio.SetPadding(5, 20, 400, 20);
                        upChargeRadio.SetButtonDrawable(Resource.Drawable.radioButton);
                        upChargeRadio.Id = someId;
                        if(!upchargeRadio.ContainsKey(result.ServiceId))
                        upchargeRadio.Add(result.ServiceId, someId);
                        upChargeRadio.SetTypeface(null, TypefaceStyle.Bold);
                        upChargeRadio.SetTextSize(ComplexUnitType.Sp, 15);
                        upChargeRadio.TextAlignment = TextAlignment.ViewEnd;
                        someId++;
                        upchargeOptions.AddView(upChargeRadio);
                    }
                   
                }
            }

        }
    }
}