using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using StriveEmployee.Android.Fragments;
using StriveEmployee.Android.Listeners;

namespace StriveEmployee.Android.Adapter
{
    public class MessengerRecentContactsRecycleHolder : RecyclerView.ViewHolder, View.IOnClickListener, View.IOnLongClickListener
    {
        public Button recentContact_ImageView;
        public TextView recentContactName_TextView;
        public TextView recentContactLastText_TextView;
        public TextView recentContactMessageTime_TextView;
        public MessengerRecentContactsRecycleHolder(View recentContact) : base(recentContact)
        {

            recentContact_ImageView = recentContact.FindViewById<Button>(Resource.Id.recentContact_ImageView);
            recentContactName_TextView = recentContact.FindViewById<TextView>(Resource.Id.recentContactName_TextView);
            recentContactLastText_TextView = recentContact.FindViewById<TextView>(Resource.Id.recentContactLastText_TextView);
            recentContactMessageTime_TextView = recentContact.FindViewById<TextView>(Resource.Id.recentContactMessageTime_TextView);

        }
        public void OnClick(View v)
        {
            
        }

        public bool OnLongClick(View v)
        {
            return false;
        }
    }
    public class MessengerRecentContactsAdapter : RecyclerView.Adapter, IItemClickListener
    {

        Context context;
        private MessengerRecentContactsRecycleHolder recentContactsRecycleHolder;
        private List<RecentContactsSampleData> recentContactsSampleDatas = new List<RecentContactsSampleData>();
        public MessengerRecentContactsAdapter(Context context, List<RecentContactsSampleData> sampleData)
        {
            this.context = context;
            this.recentContactsSampleDatas = sampleData;
        }

        public override int ItemCount
        {
            get
            {
                return recentContactsSampleDatas.Count;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            recentContactsRecycleHolder = holder as MessengerRecentContactsRecycleHolder;
            var avatarInitials = recentContactsSampleDatas[position].ContactName.Split(" ");
            char[] firstInitial = avatarInitials[0].ToCharArray();
            char[] secondInitial = avatarInitials[1].ToCharArray();

            recentContactsRecycleHolder.recentContact_ImageView.Text = firstInitial.ElementAt(0).ToString() + secondInitial.ElementAt(0).ToString();
            recentContactsRecycleHolder.recentContactName_TextView.Text = recentContactsSampleDatas[position].ContactName;
            recentContactsRecycleHolder.recentContactLastText_TextView.Text = recentContactsSampleDatas[position].LastMessage;
            recentContactsRecycleHolder.recentContactMessageTime_TextView.Text = recentContactsSampleDatas[position].MessageTime;
        }

        public void OnClick(View itemView, int position, bool isLongClick)
        {
            
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater layoutInflater = LayoutInflater.From(parent.Context);
            View itemView = layoutInflater.Inflate(Resource.Layout.MessengerRecentContactItemView, parent, false);
            return new MessengerRecentContactsRecycleHolder(itemView);
        }
    }
}