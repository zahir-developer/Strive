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
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.ViewModels.Employee.MyProfile.Collisions;

namespace StriveEmployee.Android.Fragments.MyProfile.Collisions
{
    public class AddCollisionsFragment : MvxFragment<AddCollisionsViewModel>
    {
        private TextView addImage_TextView;
        private AddCollisions_DialogFragment collisions_DialogFragment;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.AddCollisions_Fragment, null);

            addImage_TextView = rootView.FindViewById<TextView>(Resource.Id.addImage_TextView);
            addImage_TextView.PaintFlags = PaintFlags.UnderlineText;
            addImage_TextView.Click += AddImage_TextView_Click;
            return rootView;
        }

        private void AddImage_TextView_Click(object sender, EventArgs e)
        {
            AppCompatActivity activity = (AppCompatActivity)this.Context;
            collisions_DialogFragment = new AddCollisions_DialogFragment();
            collisions_DialogFragment.Show(activity.SupportFragmentManager,"AddCollisions_DialogFragment");
        }
    }
}