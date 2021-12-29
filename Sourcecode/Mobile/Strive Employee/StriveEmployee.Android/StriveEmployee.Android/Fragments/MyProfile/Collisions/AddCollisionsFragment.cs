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
using Strive.Core.Utils;
using Strive.Core.ViewModels.Employee.MyProfile.Collisions;

namespace StriveEmployee.Android.Fragments.MyProfile.Collisions
{
    public class AddCollisionsFragment : MvxFragment<AddCollisionsViewModel>
    {
        private Button save_Button;
        private Button back_Button;
        private Spinner collisionType_Spinner;
        private EditText collisionDate_EditText;
        private EditText collisionAmount_EditText;
        private EditText collisionNotes_EditText;
        private List<int> collisionID_List;
        private TextView addImage_TextView;
        private ArrayAdapter<string> codesAdapter;
        private List<string> codes;
        private AddCollisions_DialogFragment collisions_DialogFragment;
        private MyProfileFragment profileFragment;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.AddCollisions_Fragment, null);
            this.ViewModel = new AddCollisionsViewModel();

            back_Button = rootView.FindViewById<Button>(Resource.Id.addCollisionsBack_Button);
            save_Button = rootView.FindViewById<Button>(Resource.Id.addCollisionsSave_Button);
            collisionDate_EditText = rootView.FindViewById<EditText>(Resource.Id.collisionsDate_EditText);
            addImage_TextView = rootView.FindViewById<TextView>(Resource.Id.addImage_TextView);
            collisionType_Spinner = rootView.FindViewById<Spinner>(Resource.Id.collisionType_Spinner);
            collisionAmount_EditText = rootView.FindViewById<EditText>(Resource.Id.collisionAmount_EditText);
            collisionNotes_EditText = rootView.FindViewById<EditText>(Resource.Id.collisionNotes_EditText);
            collisionID_List = new List<int>();
            collisionType_Spinner.ItemSelected += CollisionType_Spinner_ItemSelected;
            addImage_TextView.PaintFlags = PaintFlags.UnderlineText;
            addImage_TextView.Click += AddImage_TextView_Click;
            collisionDate_EditText.Click += CollisionDate_EditText_Click;
            save_Button.Click += Save_Button_Click;
            back_Button.Click += Back_Button_Click;
            GetLiabilityTypes();

            return rootView;
        }

        private void Back_Button_Click(object sender, EventArgs e)
        {
            AppCompatActivity activity = (AppCompatActivity)this.Context;
            profileFragment = new MyProfileFragment();
            MyProfileInfoNeeds.selectedTab = 1;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, profileFragment).Commit();
        }

        private async void Save_Button_Click(object sender, EventArgs e)
        {
            this.ViewModel.CollisionAmount = collisionAmount_EditText.Text.ToString();
            this.ViewModel.CollisionNotes = collisionNotes_EditText.Text.ToString();
            await ViewModel.AddCollision();
        }

        private void CollisionDate_EditText_Click(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;
            DatePickerDialog dialog = new DatePickerDialog(Context, OnDateSet, today.Year, today.Month - 1, today.Day);
            dialog.DatePicker.MinDate = today.Millisecond;
            dialog.Show();
        }

        private void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            var date = DateUtils.ConvertDateTimeWithZ(e.Date.ToString());
            this.ViewModel.CollisionDate = date;
            collisionDate_EditText.Text = e.Date.ToString();
        }

        private void CollisionType_Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
           this.ViewModel.CollisionID =  collisionID_List.ElementAt(e.Position);
        }

        private async void GetLiabilityTypes()
        {
           await this.ViewModel.GetLiabilityTypes();
            if(this.ViewModel.liabilityTypes != null)
            {
                codes = new List<string>();
                foreach (var data in this.ViewModel.liabilityTypes.Codes)
                {
                    codes.Add(data.CodeValue);
                    collisionID_List.Add(data.CodeId);
                }
                codesAdapter = new ArrayAdapter<string>(Context, Resource.Layout.support_simple_spinner_dropdown_item, codes);
                collisionType_Spinner.Adapter = codesAdapter;
            }
        }

        private void AddImage_TextView_Click(object sender, EventArgs e)
        {
            AppCompatActivity activity = (AppCompatActivity)this.Context;
            collisions_DialogFragment = new AddCollisions_DialogFragment();
            collisions_DialogFragment.Show(activity.SupportFragmentManager,"AddCollisions_DialogFragment");
        }
    }
}