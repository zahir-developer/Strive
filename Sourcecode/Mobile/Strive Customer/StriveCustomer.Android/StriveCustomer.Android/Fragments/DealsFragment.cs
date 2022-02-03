using System;
using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.ViewModels.Customer;
using StriveCustomer.Android.Adapter;
using StriveCustomer.Android.DemoData;
using OperationCanceledException = System.OperationCanceledException;

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
            this.ViewModel = new DealsViewModel();

            dealsRecyclerView = rootview.FindViewById<RecyclerView>(Resource.Id.dealsList);
            dealsRecyclerView.HasFixedSize = true;
            var layoutManager = new LinearLayoutManager(context);
            dealsRecyclerView.SetLayoutManager(layoutManager);
            GetDeals();
            
            return rootview;
        }
        //private async void QrCode_Click(object sender, EventArgs e)
        //{
        //    if (ActivityCompat.CheckSelfPermission(this.Context, Manifest.Permission.Camera) == Permission.Granted)
        //    {
        //        MobileBarcodeScanner.Initialize(Activity.Application);
        //        var scanner = new ZXing.Mobile.MobileBarcodeScanner();
        //        var result = await scanner.Scan();
        //        if (result != null)
        //            Console.WriteLine("Scanned Barcode: " + result.Text);
        //    }
        //    else
        //    {
        //        await AndroidPermissions.checkCameraPermission(this);
        //    }
        //}
        public async void GetDeals()
        {
            try
            {
                await this.ViewModel.GetAllDealsCommand();
                DealsAdapter dealsAdapter = new DealsAdapter(this.ViewModel.Deals, context, ViewModel);
                dealsRecyclerView.SetAdapter(dealsAdapter);

            }
            catch (Exception ex)
            {
                if (ex is OperationCanceledException)
                {
                    return;
                }
            }
        }
        
    }
    

}