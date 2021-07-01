using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.ViewModels.Customer;
using StriveCustomer.Android.Adapter;
using StriveCustomer.Android.DemoData;
using StriveCustomer.Android.Services;
using ZXing.Mobile;

namespace StriveCustomer.Android.Fragments
{
    public class DealsFragment : MvxFragment<DealsViewModel>
    {

        private Button qrCode;
        Context context;
        List<DealsDemoData> dealsDemoDatas;
        public RecyclerView dealsRecyclerView;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootview = this.BindingInflate(Resource.Layout.DealsScreenFragment, null);
            qrCode = rootview.FindViewById<Button>(Resource.Id.qrCodeScan);
            this.ViewModel = new DealsViewModel();

            dealsRecyclerView = rootview.FindViewById<RecyclerView>(Resource.Id.dealsList);
            dealsRecyclerView.HasFixedSize = true;
            var layoutManager = new LinearLayoutManager(context);
            dealsRecyclerView.SetLayoutManager(layoutManager);
            qrCode.Click += QrCode_Click;
            GetDeals();
            
            return rootview;
        }
        private async void QrCode_Click(object sender, EventArgs e)
        {
            if (ActivityCompat.CheckSelfPermission(this.Context,Manifest.Permission.Camera) == Permission.Granted)
            {
                MobileBarcodeScanner.Initialize(Activity.Application);
                var scanner = new ZXing.Mobile.MobileBarcodeScanner();
                var result = await scanner.Scan();
                if (result != null)
                    Console.WriteLine("Scanned Barcode: " + result.Text);
            }
            else
            {
               await AndroidPermissions.checkCameraPermission(this);
            }
        }
        public async void GetDeals()
        {
            await this.ViewModel.GetAllDealsCommand();
            DealsAdapter dealsAdapter = new DealsAdapter(this.ViewModel.Deals, context);
            dealsRecyclerView.SetAdapter(dealsAdapter);
        }
    }
}