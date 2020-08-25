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
using Strive.Core.ViewModels.Customer;
using StriveCustomer.Android.Adapter;

namespace StriveCustomer.Android.Fragments
{
    [MvxUnconventionalAttribute]
    public class PastDetailsFragment : MvxFragment<PastDetailViewModel>
    {
        public List<string> pastDetailsData;
        Context context;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootview = this.BindingInflate(Resource.Layout.PastDetailsScreenFragment, null);
            pastDetailsData = new List<string>();
            for (int i = 1; i <= 5; i++)
            {
                pastDetailsData.Add("Clickme" + i);
            }
            var detailsRecyclerView = rootview.FindViewById<RecyclerView>(Resource.Id.pastDetailsList);
            detailsRecyclerView.HasFixedSize = true;
            var layoutManager = new LinearLayoutManager(context);
            detailsRecyclerView.SetLayoutManager(layoutManager);
            PastDetailsAdapter pastDetailsAdapter = new PastDetailsAdapter(pastDetailsData,context);
            detailsRecyclerView.SetAdapter(pastDetailsAdapter);
            return rootview;
        }
    }
}