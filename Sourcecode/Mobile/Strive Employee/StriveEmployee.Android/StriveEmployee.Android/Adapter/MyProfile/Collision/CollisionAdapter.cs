using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Strive.Core.Models.Employee.PersonalDetails;

namespace StriveEmployee.Android.Adapter.MyProfile.Collision
{
    public class CollisionViewHolder : RecyclerView.ViewHolder
    {
        public TextView collisionName_TextView;
        public TextView collisionDate_TextView;
        public TextView collisionCost_TextView;
        public CollisionViewHolder(View itemView) : base(itemView)
        {
            collisionName_TextView = itemView.FindViewById<TextView>(Resource.Id.collisionName_TextView);
            collisionDate_TextView = itemView.FindViewById<TextView>(Resource.Id.collisionDate_TextView);
            collisionCost_TextView = itemView.FindViewById<TextView>(Resource.Id.collisionCost_TextView);
        }
    }
    public class CollisionAdapter : RecyclerView.Adapter
    {

        Context context;
        private CollisionViewHolder collisionViewHolder;
        private List<EmployeeCollision> employeeCollisions = new List<EmployeeCollision>();
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

            collisionViewHolder.collisionName_TextView.Text = employeeCollisions[position].LiabilityType;
           if(!String.IsNullOrEmpty(employeeCollisions[position].CreatedDate))
            {
                var date = employeeCollisions[position].CreatedDate.Split("T");
                collisionViewHolder.collisionDate_TextView.Text = date[0];
            }
            collisionViewHolder.collisionCost_TextView.Text = employeeCollisions[position].Amount.ToString();
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View itemView = inflater.Inflate(Resource.Layout.CollisionsItemView, parent, false);
            return new CollisionViewHolder(itemView);
        }
    }
}