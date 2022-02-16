using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using StriveEmployee.Android.Adapter.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StriveEmployee.Android.Fragments.Schedule
{
    public class ScheduleCheckListFragment : MvxFragment
    {

        private RecyclerView checkList_RecyclerView;
        private ScheduleCheckListAdapter checkListAdapter;
        Context context;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.Schedule_ChecklistFragment, null);
            checkList_RecyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.checkList_RecyclerView);
            GetCheckListData();

            return rootView;
        }

        private void GetCheckListData()
        {
            checkListAdapter = new ScheduleCheckListAdapter(new String[2] { "item 1","10 am" });
            var layoutManager = new LinearLayoutManager(context);
            checkList_RecyclerView.SetLayoutManager(layoutManager);
            checkList_RecyclerView.SetAdapter(checkListAdapter);
        }
    }
}