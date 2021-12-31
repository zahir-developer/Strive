using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.Utils.Owner;
using Strive.Core.ViewModels.Owner;
using StriveOwner.Android.Adapter;

namespace StriveOwner.Android.Resources.Fragments
{
    public class DashboardHomeFragment : MvxFragment<HomeViewModel>
    {
        private TextView TempTextView;
        private LinearLayout locationsLayout;
        private TabLayout dashhome_TabLayout;
        private ViewPager dashhome_ViewPager;
        private ViewPagerAdapter dashhome_ViewPagerAdapter;
        private ServicesFragment servicesFragment;
        private SalesFragment salesFragment;
        private RevenueFragment revenueFragment;
        private ImageView bay1_expand;
        private TextView bay1_timein;
        private TextView bay1_client;
        private TextView bay1_phone;
        private TextView bay1_timeout;
        private TextView bay1_makemodelcolor;
        private TextView bay1_services;
        private TextView bay1_upcharges;
        private ImageView bay2_expand;
        private TextView bay2_timein;
        private TextView bay2_client;
        private TextView bay2_phone;
        private TextView bay2_timeout;
        private TextView bay2_makemodelcolor;
        private TextView bay2_services;
        private TextView bay2_upcharges;
        private ImageView bay3_expand;
        private TextView bay3_timein;
        private TextView bay3_client;
        private TextView bay3_phone;
        private TextView bay3_timeout;
        private TextView bay3_makemodelcolor;
        private TextView bay3_services;
        private TextView bay3_upcharges;
        private LinearLayout bay_layout;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.DashboardHome_Fragment, null);
            this.ViewModel = new HomeViewModel();

            servicesFragment = new ServicesFragment();
            salesFragment = new SalesFragment();
            revenueFragment = new RevenueFragment();
            TempTextView = rootView.FindViewById<TextView>(Resource.Id.weather_Temperature);
            TempTextView.Text = "/";
            locationsLayout = rootView.FindViewById<LinearLayout>(Resource.Id.addinglocationbuttons);
            dashhome_TabLayout = rootView.FindViewById<TabLayout>(Resource.Id.dashhome_TabLayout);
            dashhome_ViewPager = rootView.FindViewById<ViewPager>(Resource.Id.dashhome_ViewPager);
            //bay1_expand = rootView.FindViewById<ImageView>(Resource.Id.bay1_button);
            //bay1_timein = rootView.FindViewById<TextView>(Resource.Id.timein_TextView_1);
            //bay1_client = rootView.FindViewById<TextView>(Resource.Id.client_TextView_1);
            //bay1_phone = rootView.FindViewById<TextView>(Resource.Id.phones_TextView_1);
            //bay1_timeout = rootView.FindViewById<TextView>(Resource.Id.timeout_TextView_1);
            //bay1_makemodelcolor = rootView.FindViewById<TextView>(Resource.Id.makemodelcolor_TextView_1);
            //bay1_services = rootView.FindViewById<TextView>(Resource.Id.serviceshome_TextView_1);
            //bay1_upcharges = rootView.FindViewById<TextView>(Resource.Id.upchargeshome_TextView_1);
            //bay2_expand = rootView.FindViewById<ImageView>(Resource.Id.bay2_button);
            //bay2_timein = rootView.FindViewById<TextView>(Resource.Id.timein_TextView_2);
            //bay2_client = rootView.FindViewById<TextView>(Resource.Id.client_TextView_2);
            //bay2_phone = rootView.FindViewById<TextView>(Resource.Id.phones_TextView_2);
            //bay2_timeout = rootView.FindViewById<TextView>(Resource.Id.timeout_TextView_2);
            //bay2_makemodelcolor = rootView.FindViewById<TextView>(Resource.Id.makemodelcolor_TextView_2);
            //bay2_services = rootView.FindViewById<TextView>(Resource.Id.serviceshome_TextView_2);
            //bay2_upcharges = rootView.FindViewById<TextView>(Resource.Id.upchargeshome_TextView_2);
            //bay3_expand = rootView.FindViewById<ImageView>(Resource.Id.bay3_button);
            //bay3_timein = rootView.FindViewById<TextView>(Resource.Id.timein_TextView_3);
            //bay3_client = rootView.FindViewById<TextView>(Resource.Id.client_TextView_3);
            //bay3_phone = rootView.FindViewById<TextView>(Resource.Id.phones_TextView_3);
            //bay3_timeout = rootView.FindViewById<TextView>(Resource.Id.timeout_TextView_3);
            //bay3_makemodelcolor = rootView.FindViewById<TextView>(Resource.Id.makemodelcolor_TextView_3);
            //bay3_services = rootView.FindViewById<TextView>(Resource.Id.serviceshome_TextView_3);
            //bay3_upcharges = rootView.FindViewById<TextView>(Resource.Id.upchargeshome_TextView_3);
            bay_layout = rootView.FindViewById<LinearLayout>(Resource.Id.BayDetails_LinearLayout);
            OwnerTempData.LocationID = 1;
            GetStatistics(OwnerTempData.LocationID);
            GetDashData(OwnerTempData.LocationID);
            
           

            return rootView;
        }

        private async void GetStatistics(int locationID)
        {
            await ViewModel.getStatistics(locationID);
        }

        private async void GetDashData(int locationID)
        {
            await ViewModel.getDashboardSchedule(locationID);
            GetLocations();
        }

        private async void GetLocations()
        {
            await ViewModel.GetAllLocationsCommand();
            if(ViewModel.Locations.Location.Count > 0 && ViewModel.Locations != null && ViewModel.Locations.Location != null)
            {
                var BtnID = 777;
                locationsLayout.Orientation = Orientation.Vertical;
                LinearLayout row = new LinearLayout(this.Context);
                var layoutParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
                var btnParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
                row.LayoutParameters = layoutParams;
                foreach (var location in ViewModel.Locations.Location)
                {
                    Button locationBtn = new Button(this.Context);
                    locationBtn.SetBackgroundResource(Resource.Drawable.RoundEdge_Button);
                    var nameSplits = location.LocationName.Split(" ");
                    foreach (var name in nameSplits)
                    {
                        if(!string.Equals(name.ToUpper(), "DETAIL") || !string.Equals(name.ToUpper(), "SALON") || !string.Equals(name.ToUpper(), "MAMMOTH") )
                        locationBtn.Text += name + " ";
                    }
                    
                    btnParams.SetMargins(5, 5, 5, 5);
                    locationBtn.LayoutParameters = btnParams;                  
                    BtnID += 1;
                    locationBtn.SetTextColor(Color.ParseColor("#ffffff"));
                    locationBtn.Id = BtnID;
                    locationBtn.Tag = location.LocationId;
                    locationBtn.Click += LocationBtn_Click;
                    row.AddView(locationBtn);
                }
                locationsLayout.AddView(row);
                BayDetails();
                //hidebay1Details();
                //hidebay2Details();
                //hidebay3Details();
            }

        }

        private void LocationBtn_Click(object sender, EventArgs e)
        {
            var data = (Button)sender;
            var locationId = Convert.ToInt32(data.Tag);
            OwnerTempData.LocationID = locationId;
            dashhome_ViewPagerAdapter = new ViewPagerAdapter(ChildFragmentManager);
            dashhome_ViewPagerAdapter.AddFragment(servicesFragment, "Service");
            dashhome_ViewPagerAdapter.AddFragment(salesFragment, "Sales");
            dashhome_ViewPagerAdapter.AddFragment(revenueFragment, "Revenue");
            dashhome_ViewPager.Adapter = dashhome_ViewPagerAdapter;
            dashhome_TabLayout.SetupWithViewPager(dashhome_ViewPager);
            GetStatistics(locationId);
        }

        private void hidebay1Details()
        {
            if(this.ViewModel.dbSchedule != null && this.ViewModel.dbSchedule.DetailsGrid != null && this.ViewModel.dbSchedule.DetailsGrid.BayDetailViewModel != null && this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel != null)
            {
                bay1_timein.Text = this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[0].TimeIn;
                bay1_client.Text = this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[0].ClientName;
                bay1_phone.Text = this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[0].PhoneNumber;
                bay1_timeout.Text = this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[0].EstimatedTimeOut;
                bay1_makemodelcolor.Text = this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[0].VehicleMake + this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[0].VehicleModel + this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[0].VehicleColor;
                bay1_services.Text = this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[0].ServiceTypeName;
                bay1_upcharges.Text = "$" + " " + this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[0].Upcharge.ToString();
            }
         
        }
        private void hidebay2Details()
        {
            if (this.ViewModel.dbSchedule != null && this.ViewModel.dbSchedule.DetailsGrid != null && this.ViewModel.dbSchedule.DetailsGrid.BayDetailViewModel != null && this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel != null)
            {
                bay2_timein.Text = this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[1].TimeIn;
                bay2_client.Text = this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[1].ClientName;
                bay2_phone.Text = this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[1].PhoneNumber;
                bay2_timeout.Text = this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[1].EstimatedTimeOut;
                bay2_makemodelcolor.Text = this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[1].VehicleMake + this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[1].VehicleModel + this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[1].VehicleColor;
                bay2_services.Text = this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[1].ServiceTypeName;
                bay2_upcharges.Text = "$" + " " + this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[1].Upcharge.ToString();
            }           
        }
        private void hidebay3Details()
        {
            if (this.ViewModel.dbSchedule != null && this.ViewModel.dbSchedule.DetailsGrid != null && this.ViewModel.dbSchedule.DetailsGrid.BayDetailViewModel != null && this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel != null)
            {
                bay3_timein.Text = this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[2].TimeIn;
                bay3_client.Text = this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[2].ClientName;
                bay3_phone.Text = this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[2].PhoneNumber;
                bay3_timeout.Text = this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[2].EstimatedTimeOut;
                bay3_makemodelcolor.Text = this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[2].VehicleMake + this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[2].VehicleModel + this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[2].VehicleColor;
                bay3_services.Text = this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[2].ServiceTypeName;
                bay3_upcharges.Text = "$" + " " + this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[2].Upcharge.ToString();
            }           
        }

        private void BayDetails() 
        {
            
            if (this.ViewModel.dbSchedule != null && this.ViewModel.dbSchedule.DetailsGrid != null && this.ViewModel.dbSchedule.DetailsGrid.BayDetailViewModel != null && this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel != null) 
            {
                if (this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel.Count > 0)
                {
                    foreach (var data in ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel)
                    {
                        if (Context != null) 
                        {
                           var layout = LayoutInflater.From(Context).Inflate(Resource.Layout.BayDetails, bay_layout, false);

                            var bay_number = layout.FindViewById<TextView>(Resource.Id.bay_number);
                            var ticket_number = layout.FindViewById<TextView>(Resource.Id.ticket_number);
                            var bay_timein = layout.FindViewById<TextView>(Resource.Id.timein_TextView);
                            var bay_client = layout.FindViewById<TextView>(Resource.Id.client_TextView);
                            var bay_phone  = layout.FindViewById<TextView>(Resource.Id.phones_TextView);
                            var bay_timeout = layout.FindViewById<TextView>(Resource.Id.timeout_TextView);
                            var bay_makemodelcolor = layout.FindViewById<TextView>(Resource.Id.makemodelcolor_TextView);
                            var bay_services = layout.FindViewById<TextView>(Resource.Id.timein_TextView);
                            var bay_upcharges = layout.FindViewById<TextView>(Resource.Id.timein_TextView);

                            bay_number.Text = data.BayName;
                            ticket_number.Text = data.TicketNumber;
                            bay_timein.Text = data.TimeIn;
                            bay_client.Text = data.ClientName;
                            bay_phone.Text = data.PhoneNumber;
                            bay_timeout.Text = data.EstimatedTimeOut;
                            bay_makemodelcolor.Text = data.VehicleMake + data.VehicleModel + data.VehicleColor;
                            bay_services.Text = data.ServiceTypeName;
                            bay_upcharges.Text = "$" + " " + data.Upcharge.ToString();

                            bay_layout.AddView(layout);

                        }             
                    
                    }
                
                }
            }           
                
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            dashhome_ViewPagerAdapter = new ViewPagerAdapter(ChildFragmentManager);
            dashhome_ViewPagerAdapter.AddFragment(servicesFragment, "Service");
            dashhome_ViewPagerAdapter.AddFragment(salesFragment, "Sales");
            dashhome_ViewPagerAdapter.AddFragment(revenueFragment, "Revenue");
            dashhome_ViewPager.Adapter = dashhome_ViewPagerAdapter;
            dashhome_TabLayout.SetupWithViewPager(dashhome_ViewPager);
        }
        public override void OnPause()
        {
            base.OnPause();            
        }
        public override void OnDetach()
        {
            base.OnDetach();
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}