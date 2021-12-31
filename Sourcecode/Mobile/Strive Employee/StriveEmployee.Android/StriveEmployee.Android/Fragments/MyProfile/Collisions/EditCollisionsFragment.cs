using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.Utils;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Employee.MyProfile.Collisions;

namespace StriveEmployee.Android.Fragments.MyProfile.Collisions
{
    public class EditCollisionsFragment : MvxFragment<EditCollisionsViewModel>
    {
        private Button back_Button;
        private Button save_Button;
        private EditText editCollisionsDate_EditText;
        private EditText editCollisionAmount_EditText;
        private EditText editCollisionNotes_EditText;
        private Spinner editCollisionType_Spinner;
        private MyProfileFragment profileFragment;
        private List<int> collisionID_List;
        private List<string> codes;
        private ArrayAdapter<string> codesAdapter;
        private MyProfileFragment myProfileFragment;
        private string dates;
        public int position { get; set; }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.EditCollisions_Fragment,null);
            this.ViewModel = new EditCollisionsViewModel();          

            back_Button = rootView.FindViewById<Button>(Resource.Id.editCollisionsBack_Button);
            save_Button = rootView.FindViewById<Button>(Resource.Id.editCollisionsSave_Button);
            editCollisionType_Spinner = rootView.FindViewById<Spinner>(Resource.Id.editCollisionType_Spinner);
            editCollisionsDate_EditText = rootView.FindViewById<EditText>(Resource.Id.editCollisionsDate_EditText);
            editCollisionAmount_EditText = rootView.FindViewById<EditText>(Resource.Id.editCollisionAmount_EditText);
            editCollisionNotes_EditText = rootView.FindViewById<EditText>(Resource.Id.editCollisionNotes_EditText);
            back_Button.Click += Back_Button_Click;
            save_Button.Click += Save_Button_Click;
            editCollisionsDate_EditText.Click += CollisionDate_EditText_Click;
            editCollisionType_Spinner.ItemSelected += EditCollisionType_Spinner_ItemSelected;
            GetLiabilityTypes();
            return rootView;
        }

        private void EditCollisionType_Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            this.ViewModel.CollisionID = collisionID_List.ElementAt(e.Position);
        }

        private async void Save_Button_Click(object sender, EventArgs e)
        {
            this.ViewModel.CollisionDate = dates;
            this.ViewModel.CollisionAmount = editCollisionAmount_EditText.Text;
            this.ViewModel.CollisionNotes = editCollisionNotes_EditText.Text;
            MyProfileTempData.LiabilityID = this.ViewModel.getCollisions.Collision.LiabilityDetail.First().LiabilityId;

            await this.ViewModel.EditCollision();
            if(this.ViewModel.collisionAdded)
            {
                AppCompatActivity activity = (AppCompatActivity)this.Context;
                MyProfileInfoNeeds.selectedTab = 1;
                myProfileFragment = new MyProfileFragment();
                activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, myProfileFragment).Commit(); 
            }
        }

        private async Task GetCollisions()
        {
            await this.ViewModel.GetCollisions();
            if(this.ViewModel.getCollisions != null)
            {
                var id = this.ViewModel.getCollisions.Collision.LiabilityDetail.First().LiabilityId;
                position = this.ViewModel.liabilityTypes.Codes.FindIndex(x => x.CodeId == id);
                if(!string.IsNullOrEmpty(this.ViewModel.getCollisions.Collision.Liability.First().CreatedDate))
                {
                    dates = this.ViewModel.getCollisions.Collision.Liability.First().CreatedDate;
                    var date = this.ViewModel.getCollisions.Collision.Liability.First().CreatedDate.Split('T');
                    editCollisionsDate_EditText.Text = date[0];
                }                
                editCollisionAmount_EditText.Text =this.ViewModel.getCollisions.Collision.LiabilityDetail.First().Amount.ToString();
                editCollisionNotes_EditText.Text = this.ViewModel.getCollisions.Collision.LiabilityDetail.First().Description;
            }
        }

        private async void GetLiabilityTypes()
        {
            await this.ViewModel.GetLiabilityTypes();
            if(this.ViewModel.liabilityTypes != null)
            {
                codes = new List<string>();
                collisionID_List = new List<int>();

                foreach (var data in this.ViewModel.liabilityTypes.Codes)
                {
                    codes.Add(data.CodeValue);
                    collisionID_List.Add(data.CodeId);
                }
                codesAdapter = new ArrayAdapter<string>(Context, Resource.Layout.support_simple_spinner_dropdown_item, codes);
                editCollisionType_Spinner.Adapter = codesAdapter;
                await GetCollisions();
                editCollisionType_Spinner.SetSelection(position);
            }
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
            dates = DateUtils.ConvertDateTimeWithZ(e.Date.ToString());
            this.ViewModel.CollisionDate = dates;
            editCollisionsDate_EditText.Text = e.Date.ToString();
        }
        private void Back_Button_Click(object sender, EventArgs e)
        {            
            AppCompatActivity activity = (AppCompatActivity)Context;
            MyProfileInfoNeeds.selectedTab = 1;
            profileFragment = new MyProfileFragment();
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, profileFragment).Commit();
        }
    }
}