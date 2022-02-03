using System;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.IoC;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.ViewModels.Employee.MyProfile.Collisions;
using StriveEmployee.Android.Adapter.MyProfile.Collision;
using OperationCanceledException = System.OperationCanceledException;

namespace StriveEmployee.Android.Fragments.MyProfile.Collisions
{
    [MvxUnconventionalAttribute]
    public class CollisionsFragment : MvxFragment<CollisionsViewModel>
    {
        private RecyclerView collison_RecyclerView;
        private CollisionAdapter collision_Adapter;
        private MvxFragment selected_Fragment;
        //private AddCollisionsFragment addCollisionsFragment;        
        private ImageButton addCollision_ImageButton;
        private View rootView;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            rootView = this.BindingInflate(Resource.Layout.Collisions_Fragment, null);
            this.ViewModel = new CollisionsViewModel();           
            //addCollisionsFragment = new AddCollisionsFragment();
            //addCollision_ImageButton = rootView.FindViewById<ImageButton>(Resource.Id.addCollision_ImageButton);
            collison_RecyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.collison_RecyclerView);
            GetCollisionInfo(true);
            // addCollision_ImageButton.Click += AddCollision_ImageButton_Click;
            return rootView;
        }

        //private void AddCollision_ImageButton_Click(object sender, EventArgs e)
        //{
        //    AppCompatActivity activity = (AppCompatActivity)this.Context;
        //    addCollisionsFragment = new AddCollisionsFragment();
        //    activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame,addCollisionsFragment).Commit();
        //}

        public async void GetCollisionInfo(bool isInitialCall)
        {
            if (this.ViewModel == null)
            {
              this.ViewModel = new CollisionsViewModel();
            }
            try
            {
                ViewModel.isAndroidFlag = isInitialCall;
                await ViewModel.GetCollisionInfo();
                if (this.ViewModel.CollisionDetails != null && this.ViewModel.CollisionDetails.Employee.EmployeeCollision != null)
                {
                    collision_Adapter = new CollisionAdapter(Context, this.ViewModel.CollisionDetails.Employee.EmployeeCollision);
                    var layoutManager = new LinearLayoutManager(Context);
                    collison_RecyclerView.SetLayoutManager(layoutManager);
                    collison_RecyclerView.SetAdapter(collision_Adapter);
                }
            }
            catch (Exception ex)
            {
                if (ex is OperationCanceledException)
                {
                    return;
                }
            }
  
        }
        //public void NoData() 
        //{
        //    if (this.ViewModel.CollisionDetails == null)
        //    {
        //        Snackbar snackbar = Snackbar.Make(rootView, "No relatable data!", Snackbar.LengthShort);
        //        snackbar.Show();
        //    }       
         
        //}
    }
}