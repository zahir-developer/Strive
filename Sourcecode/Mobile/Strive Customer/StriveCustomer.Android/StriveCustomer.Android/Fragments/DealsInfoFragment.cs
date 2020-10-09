using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
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
    public class DealsInfoFragment : MvxFragment<DealsViewModel>
    {
        List<string> infoListData;
        Context context;
        private Button backButton;
        private DealsFragment dealsFragment;
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
            var rootview = this.BindingInflate(Resource.Layout.DealsInfoFragment, null);
            backButton = rootview.FindViewById<Button>(Resource.Id.dealsBack);
            infoListData = new List<string>();
            for (int i = 1; i <= 3; i++)
            {
                infoListData.Add("Deal1" + i);
            }
            var dealsInfoRecyclerView = rootview.FindViewById<RecyclerView>(Resource.Id.dealsInfoList);
            dealsInfoRecyclerView.HasFixedSize = true;
            var layoutManager = new LinearLayoutManager(context);
            dealsInfoRecyclerView.SetLayoutManager(layoutManager);
            DealsInfoAdapter dealsInfoAdapter = new DealsInfoAdapter(infoListData,context);
            dealsFragment = new DealsFragment();
            dealsInfoRecyclerView.SetAdapter(dealsInfoAdapter);
            backButton.Click += BackButton_Click;
            return rootview;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            AppCompatActivity activity = (AppCompatActivity)Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, dealsFragment).Commit();
        }
    }
}