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
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.ViewModels.Customer;
using StriveCustomer.Android.Adapter;

namespace StriveCustomer.Android.Fragments
{
    public class DealsFragment : MvxFragment<DealsViewModel>
    {
        public List<string> listData;
        Context context;
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
            listData = new List<string>();
            for(int i = 1;i<=10;i++)
            {
                listData.Add("Clickme"+i);
            }
            var dealsRecyclerView = rootview.FindViewById<RecyclerView>(Resource.Id.dealsList);
            dealsRecyclerView.HasFixedSize = true;
            var layoutManager = new LinearLayoutManager(context);
            dealsRecyclerView.SetLayoutManager(layoutManager);
            DealsAdapter dealsAdapter = new DealsAdapter(listData,context);
            dealsRecyclerView.SetAdapter(dealsAdapter);
            return rootview;
        }
    }
}