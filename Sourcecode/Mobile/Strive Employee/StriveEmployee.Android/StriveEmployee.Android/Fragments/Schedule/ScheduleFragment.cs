using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using Strive.Core.ViewModels.Employee.Schedule;
using StriveEmployee.Android.Adapter.Schedule;

namespace StriveEmployee.Android.Fragments.Schedule
{
    public class ScheduleFragment : MvxFragment<ScheduleViewModel>
    {
        private RecyclerView scheduleInfo;
        private ScheduleAdapter scheduleAdapter;
        private Button backButton;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.Schedule_Fragment, null);
            scheduleInfo = rootView.FindViewById<RecyclerView>(Resource.Id.scheduleInfo);
            backButton = rootView.FindViewById<Button>(Resource.Id.schedule_BackButton);
            this.ViewModel = new ScheduleViewModel();

            backButton.Click += BackButton_Click;
            GetScheduleList();
            return rootView;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            MessengerFragment messengerFragment = new MessengerFragment();
            FragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, messengerFragment).Commit();
        }

        public async void GetScheduleList()
        {
            await this.ViewModel.GetScheduleList();
            scheduleAdapter = new ScheduleAdapter(Context, this.ViewModel.scheduleList);
            var layoutManager = new LinearLayoutManager(Context);
            scheduleInfo.SetLayoutManager(layoutManager);
            scheduleInfo.SetAdapter(scheduleAdapter);

        }
    }
}