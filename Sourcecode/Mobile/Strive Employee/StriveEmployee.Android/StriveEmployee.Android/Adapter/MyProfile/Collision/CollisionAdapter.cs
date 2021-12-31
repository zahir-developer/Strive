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
using Android.Views;
using Android.Widget;
using Strive.Core.Models.Employee.PersonalDetails;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Employee.MyProfile.Collisions;
using StriveEmployee.Android.Fragments.MyProfile.Collisions;
using StriveEmployee.Android.Listeners;

namespace StriveEmployee.Android.Adapter.MyProfile.Collision
{
    public class CollisionViewHolder : RecyclerView.ViewHolder, View.IOnClickListener
    {
        public ImageButton collisionDelete_Button;
        public ImageButton collisionEdit_Button;
        public IItemClickListener IItemClickListener;
        public TextView collisionName_TextView;
        public TextView collisionDate_TextView;
        public TextView collisionCost_TextView;
        public CollisionViewHolder(View itemView) : base(itemView)
        {
            collisionName_TextView = itemView.FindViewById<TextView>(Resource.Id.collisionName_TextView);
            collisionDate_TextView = itemView.FindViewById<TextView>(Resource.Id.collisionDate_TextView);
            collisionCost_TextView = itemView.FindViewById<TextView>(Resource.Id.collisionCost_TextView);
            collisionDelete_Button = itemView.FindViewById<ImageButton>(Resource.Id.collisionDelete_Button);
            collisionEdit_Button = itemView.FindViewById<ImageButton>(Resource.Id.collisionEdit_Button);
            collisionDelete_Button.Click += CollisionDelete_Button_Click;
            collisionEdit_Button.Click += CollisionEdit_Button_Click;
            itemView.SetOnClickListener(this);
        }
        public void SetItemClickListener(IItemClickListener itemClickListener)
        {
            this.IItemClickListener = itemClickListener;
        }
        private void CollisionEdit_Button_Click(object sender, EventArgs e)
        {
            ClickActions.Selected_Action = ClickActions.Edit_Action;
            IItemClickListener.OnClick(null, AdapterPosition, false);
        }

        private void CollisionDelete_Button_Click(object sender, EventArgs e)
        {
            ClickActions.Selected_Action = ClickActions.Delete_Action;
            IItemClickListener.OnClick(null, AdapterPosition, false);
        }

        public void OnClick(View v)
        {
            IItemClickListener.OnClick(null, AdapterPosition, false);
        }
    }
    public class CollisionAdapter : RecyclerView.Adapter, IItemClickListener
    {

        Context context;
        private CollisionViewHolder collisionViewHolder;
        private List<EmployeeCollision> employeeCollisions = new List<EmployeeCollision>();
        private CollisionsViewModel collisionsViewModel;
        private EditCollisionsFragment editCollisionsFragment;
        public CollisionAdapter(Context context, List<EmployeeCollision> employeeCollisions)
        {
            this.context = context;
            this.employeeCollisions = employeeCollisions;
        }

        public override int ItemCount
        {
            get 
            {
                return employeeCollisions.Count;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            collisionViewHolder = holder as CollisionViewHolder;
            collisionViewHolder.SetItemClickListener(this);
            collisionViewHolder.collisionName_TextView.Text = employeeCollisions[position].LiabilityType;
           if(!String.IsNullOrEmpty(employeeCollisions[position].CreatedDate))
            {
                var date = employeeCollisions[position].CreatedDate.Split("T");
                collisionViewHolder.collisionDate_TextView.Text = date[0];
            }
            collisionViewHolder.collisionCost_TextView.Text = "$"+employeeCollisions[position].Amount.ToString();
        }

        public async void OnClick(View itemView, int position, bool isLongClick)
        {
            if(ClickActions.Selected_Action == 0)
            {
                MyProfileTempData.LiabilityID = employeeCollisions.ElementAt(position).LiabilityId;
                AppCompatActivity activity = (AppCompatActivity)this.context;
                editCollisionsFragment = new EditCollisionsFragment();
                activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, editCollisionsFragment).Commit();
            }
            else
            {
                collisionsViewModel = new CollisionsViewModel();
                var collisionID = employeeCollisions.ElementAt(position).LiabilityId;
                await collisionsViewModel.DeleteCollisions(collisionID);
                if(collisionsViewModel.confirm)
                {
                    employeeCollisions.RemoveAt(position);
                    NotifyItemRemoved(position);
                    NotifyItemRangeChanged(position, employeeCollisions.Count);
                }
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View itemView = inflater.Inflate(Resource.Layout.CollisionsItemView, parent, false);
            return new CollisionViewHolder(itemView);
        }
    }
    public static class ClickActions
    {
        public static int Edit_Action = 0;
        public static int Delete_Action = 1;
        public static int Selected_Action { get; set; }
    }
}