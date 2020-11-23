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
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Strive.Core.ViewModels.Employee.MyProfile.Collisions;
using StriveEmployee.Android.Adapter.MyProfile.Collision;

namespace StriveEmployee.Android.Fragments.MyProfile.Collisions
{
    [MvxFragmentPresentationAttribute]
    public class CollisionsFragment : MvxFragment<CollisionsViewModel>
    {
        private RecyclerView collison_RecyclerView;
        private CollisionAdapter collision_Adapter;
        private ImageButton addCollision_Buttton;
        private MvxFragment selected_Fragment;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.Collisions_Fragment, null);
            this.ViewModel = new CollisionsViewModel();

            addCollision_Buttton = rootView.FindViewById<ImageButton>(Resource.Id.addCollisions_ImageButton);
            collison_RecyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.collison_RecyclerView);
            addCollision_Buttton.Click += AddCollision_Buttton_Click;
            GetCollisionInfo();
            return rootView;
        }

        private void AddCollision_Buttton_Click(object sender, EventArgs e)
        {
            selected_Fragment = new AddCollisionsFragment();
            AppCompatActivity activity = (AppCompatActivity)this.Context;
           activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, selected_Fragment).Commit();
        }

        private async void GetCollisionInfo()
        {
            await this.ViewModel.GetCollisionInfo();
            if(this.ViewModel.CollisionDetails != null)
            {
                collision_Adapter = new CollisionAdapter(Context, this.ViewModel.CollisionDetails.Employee.EmployeeCollision);
                var layoutManager = new LinearLayoutManager(Context);
                collison_RecyclerView.SetLayoutManager(layoutManager);
                collison_RecyclerView.SetAdapter(collision_Adapter);
            }
        }
    }
}