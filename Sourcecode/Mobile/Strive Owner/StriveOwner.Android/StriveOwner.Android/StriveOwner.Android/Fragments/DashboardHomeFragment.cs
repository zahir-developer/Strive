using System;
using System.Linq;
using Android.Graphics;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.Utils.Owner;
using Strive.Core.ViewModels.Owner;
using StriveOwner.Android.Adapter;
using Android.Support.V4.Widget;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.Android;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace StriveOwner.Android.Resources.Fragments
{
    public class DashboardHomeFragment : MvxFragment<HomeViewModel>
    {
       // private TextView TempTextView;
        private LinearLayout locationsLayout;
        private TabLayout dashhome_TabLayout;
        private ViewPager dashhome_ViewPager;
        private ViewPagerAdapter dashhome_ViewPagerAdapter;
        private ServicesFragment servicesFragment;
        private SalesFragment salesFragment;
        private RevenueFragment revenueFragment;
        //private ImageView bay1_expand;
        //private TextView bay1_timein;
        //private TextView bay1_client;
        //private TextView bay1_phone;
        //private TextView bay1_timeout;
        //private TextView bay1_makemodelcolor;
        //private TextView bay1_services;
        //private TextView bay1_upcharges;
        //private ImageView bay2_expand;
        //private TextView bay2_timein;
        //private TextView bay2_client;
        //private TextView bay2_phone;
        //private TextView bay2_timeout;
        //private TextView bay2_makemodelcolor;
        //private TextView bay2_services;
        //private TextView bay2_upcharges;
        //private ImageView bay3_expand;
        //private TextView bay3_timein;
        //private TextView bay3_client;
        //private TextView bay3_phone;
        //private TextView bay3_timeout;
        //private TextView bay3_makemodelcolor;
        //private TextView bay3_services;
        //private TextView bay3_upcharges;
        private LinearLayout bay_layout;
        private PlotView lineChart;
        private TextView NoRecord;
        private NestedScrollView BayDetailsScrollView;
        private string SelectedLocName;
        private List<int> PreviousSelectedId = new List<int>();
        private int FirstLocId;
        Button locationBtn;
        List<Button> listBtn = new List<Button>();
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.DashboardHome_Fragment, null);
            this.ViewModel = new HomeViewModel();

            servicesFragment = new ServicesFragment();
            salesFragment = new SalesFragment();
            revenueFragment = new RevenueFragment();
            //TempTextView = rootView.FindViewById<TextView>(Resource.Id.weather_Temperature);
            ///TempTextView.Text = "/";
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
            NoRecord = rootView.FindViewById<TextView>(Resource.Id.norecord);
            BayDetailsScrollView = rootView.FindViewById<NestedScrollView>(Resource.Id.BayDetails_ScrollView);
            lineChart = rootView.FindViewById<PlotView>(Resource.Id.linechart);
            OwnerTempData.LocationID = 14;
            GetLocations();
            //OwnerTempData.LocationID = 1;
            //GetStatistics(OwnerTempData.LocationID);
            // GetDashData(OwnerTempData.LocationID);
            //GetChartDetails();

            return rootView;
        }
        public void setChartView()
        {
            var model = new PlotModel()
            {
                PlotType = PlotType.XY,
                LegendSymbolLength = 5,
                LegendPlacement = LegendPlacement.Outside,
                LegendOrientation = LegendOrientation.Vertical,
            };

            CategoryAxis xaxis = new CategoryAxis();
            xaxis.Position = AxisPosition.Bottom;
            xaxis.Labels.Add("Mon, 4/24");
            xaxis.Labels.Add("Tue, 4/25");
            xaxis.Labels.Add("Wed, 4/26");
            xaxis.Labels.Add("Thu, 4/27");
            xaxis.Labels.Add("Mon, 4/24");
            xaxis.Labels.Add("Tue, 4/25");
            xaxis.AbsoluteMinimum = -.5;
            xaxis.AbsoluteMaximum = 6;
            xaxis.Zoom(0, 4.5);
            xaxis.Angle = 45;

            LinearAxis yaxis = new LinearAxis();
            yaxis.Position = AxisPosition.Left;
            yaxis.AbsoluteMinimum = 0;
            yaxis.MinimumPadding = 0;
            yaxis.AbsoluteMaximum = 100;

            ColumnSeries s1 = new ColumnSeries();
            s1.IsStacked = true;
            s1.Items.Add(new ColumnItem(20));
            s1.Items.Add(new ColumnItem(60));
            s1.Items.Add(new ColumnItem(40));
            s1.Items.Add(new ColumnItem(50));
            s1.Items.Add(new ColumnItem(20));
            s1.Items.Add(new ColumnItem(60));
            s1.ColumnWidth = 20;
            s1.FillColor = OxyColor.FromRgb(255, 0, 0);

            ColumnSeries s2 = new ColumnSeries();
            s2.IsStacked = true;
            s2.Items.Add(new ColumnItem(50));
            s2.Items.Add(new ColumnItem(30));
            s2.Items.Add(new ColumnItem(10));
            s2.Items.Add(new ColumnItem(20));
            s2.ColumnWidth = 20;

            //if (servicesFragment.getScore() == "")
            //{
            //    servicesFragment.getScore() = "0";
            //}

            var Items = new Collection<Item>
            {
                new Item {Label = SelectedLocName, WashValue = int.Parse(servicesFragment.getWashCount().ToString()), DetailValue = int.Parse(servicesFragment.getDetailsCount().ToString()), EmployeeValue = int.Parse(servicesFragment.getEmployeeCount().ToString()), ScoreValue = double.Parse(servicesFragment.getScoreCount().ToString())},
                
                //new Item {Label = "Old Milton", Value1 = 4, Value2 = 2, Value3 = 1},
                //new Item {Label = "Holcomb Bridge", Value1 = 1, Value2 = 4, Value3 = 2}
            };

            model.Axes.Add(new CategoryAxis { ItemsSource = Items, LabelField = "Label", AbsoluteMinimum = -0.5 });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, MinimumPadding = 0, AbsoluteMinimum = 0 });
            model.Series.Add(new ColumnSeries { Title = "Washes", ItemsSource = Items, ValueField = "Value1", ColumnWidth = 20 });
            model.Series.Add(new ColumnSeries { Title = "Details", ItemsSource = Items, ValueField = "Value2", ColumnWidth = 20 });
            model.Series.Add(new ColumnSeries { Title = "Employees", ItemsSource = Items, ValueField = "Value3", ColumnWidth = 20 });
            model.Series.Add(new ColumnSeries { Title = "Score", ItemsSource = Items, ValueField = "Value4", ColumnWidth = 20 });

            this.lineChart.Model = model;
            //lineChart.fr = new CGRect(0, 0, this.View.Frame.Width + 10, this.View.Frame.Height);
            //return model;
        }        

        private async void GetStatistics(int locationID)
        {
            await ViewModel.getStatistics(locationID);
            GetDashData(locationID);
            setChartView();
        }

        private async void GetDashData(int locationID)
        {
            await ViewModel.getDashboardSchedule(locationID);
            //GetLocations();
        }
        
        private async void GetLocations()
        {
            await ViewModel.GetAllLocationsCommand();
            PreviousSelectedId.Clear();
            listBtn.Clear();
            if (ViewModel.Locations.Location.Count > 0 && ViewModel.Locations != null && ViewModel.Locations.Location != null)
            {
                var BtnID = 777;
                locationsLayout.Orientation = Orientation.Vertical;
                LinearLayout row = new LinearLayout(this.Context);
                var layoutParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
                var btnParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
                row.LayoutParameters = layoutParams;
                foreach (var location in ViewModel.Locations.Location)
                {
                    locationBtn = new Button(this.Context);
                    listBtn.Add(locationBtn);
                    locationBtn.SetBackgroundResource(Resource.Drawable.RoundEdge_Button);
                    SelectedLocName = ViewModel.Locations.Location.First().LocationName;
                    FirstLocId = ViewModel.Locations.Location.First().LocationId;
                    var nameSplits = location.LocationName.Split(" ");
                    foreach (var name in nameSplits)
                    {
                        if (!string.Equals(name.ToUpper(), "DETAIL") || !string.Equals(name.ToUpper(), "SALON") || !string.Equals(name.ToUpper(), "MAMMOTH"))
                            locationBtn.Text += name + " ";
                    }
                    btnParams.SetMargins(5, 5, 5, 5);
                    locationBtn.LayoutParameters = btnParams;
                    BtnID += 1;
                    locationBtn.SetTextColor(Color.ParseColor("#000000"));
                    locationBtn.Id = BtnID;
                    locationBtn.Tag = location.LocationId;
                    if (BtnID == 778)
                    {
                        PreviousSelectedId.Add(BtnID);
                        locationBtn.Selected = true;
                    }
                    locationBtn.Click += LocationBtn_Click;
                    row.AddView(locationBtn);
                }
                locationsLayout.AddView(row);
                GetStatistics(FirstLocId);
                BayDetails();
                //hidebay1Details();
                //hidebay2Details();
                //hidebay3Details();
            }
        }
        private async void LocationBtn_Click(object sender, EventArgs e)
        {
            var data = (Button)sender;
            PreviousSelectedId.Add(data.Id);
            for (int i = 0; i < PreviousSelectedId.Count; i++)
            {
                if (data.Id != PreviousSelectedId[i])
                {
                    int index = listBtn.FindIndex(a => a.Id == PreviousSelectedId[i]);
                    listBtn[index].Selected = false;
                    PreviousSelectedId.RemoveAt(i);
                }
            }
            data.Selected = true;
            var locationId = Convert.ToInt32(data.Tag);
            OwnerTempData.LocationID = locationId;
            SelectedLocName = data.Text;
            dashhome_ViewPagerAdapter = new ViewPagerAdapter(ChildFragmentManager);
            dashhome_ViewPagerAdapter.AddFragment(servicesFragment, "Service");
            dashhome_ViewPagerAdapter.AddFragment(salesFragment, "Sales");
            dashhome_ViewPagerAdapter.AddFragment(revenueFragment, "Revenue");
            dashhome_ViewPager.Adapter = dashhome_ViewPagerAdapter;
            dashhome_TabLayout.SetupWithViewPager(dashhome_ViewPager);
            GetStatistics(locationId);
            await ViewModel.getDashboardSchedule(locationId);
            BayDetails();
        }        


       

        //private void hidebay1Details()
        //{
        //    if (this.ViewModel.dbSchedule != null && this.ViewModel.dbSchedule.DetailsGrid != null && this.ViewModel.dbSchedule.DetailsGrid.BayDetailViewModel != null && this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel != null)
        //    {
        //        bay1_timein.Text = this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[0].TimeIn;
        //        bay1_client.Text = this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[0].ClientName;
        //        bay1_phone.Text = this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[0].PhoneNumber;
        //        bay1_timeout.Text = this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[0].EstimatedTimeOut;
        //        bay1_makemodelcolor.Text = this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[0].VehicleMake + this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[0].VehicleModel + this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[0].VehicleColor;
        //        bay1_services.Text = this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[0].ServiceTypeName;
        //        bay1_upcharges.Text = "$" + " " + this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[0].Upcharge.ToString();
        //    }

        //}
        //private void hidebay2Details()
        //{
        //    if (this.ViewModel.dbSchedule != null && this.ViewModel.dbSchedule.DetailsGrid != null && this.ViewModel.dbSchedule.DetailsGrid.BayDetailViewModel != null && this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel != null)
        //    {
        //        bay2_timein.Text = this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[1].TimeIn;
        //        bay2_client.Text = this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[1].ClientName;
        //        bay2_phone.Text = this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[1].PhoneNumber;
        //        bay2_timeout.Text = this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[1].EstimatedTimeOut;
        //        bay2_makemodelcolor.Text = this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[1].VehicleMake + this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[1].VehicleModel + this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[1].VehicleColor;
        //        bay2_services.Text = this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[1].ServiceTypeName;
        //        bay2_upcharges.Text = "$" + " " + this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[1].Upcharge.ToString();
        //    }
        //}
        //private void hidebay3Details()
        //{
        //    if (this.ViewModel.dbSchedule != null && this.ViewModel.dbSchedule.DetailsGrid != null && this.ViewModel.dbSchedule.DetailsGrid.BayDetailViewModel != null && this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel != null)
        //    {
        //        bay3_timein.Text = this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[2].TimeIn;
        //        bay3_client.Text = this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[2].ClientName;
        //        bay3_phone.Text = this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[2].PhoneNumber;
        //        bay3_timeout.Text = this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[2].EstimatedTimeOut;
        //        bay3_makemodelcolor.Text = this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[2].VehicleMake + this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[2].VehicleModel + this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[2].VehicleColor;
        //        bay3_services.Text = this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[2].ServiceTypeName;
        //        bay3_upcharges.Text = "$" + " " + this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel[2].Upcharge.ToString();
        //    }
        //}

        private void BayDetails()
        {

            if (this.ViewModel.dbSchedule != null && this.ViewModel.dbSchedule.DetailsGrid != null && this.ViewModel.dbSchedule.DetailsGrid.BayDetailViewModel != null && this.ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel != null)
            {
                BayDetailsScrollView.Visibility = ViewStates.Visible;
                NoRecord.Visibility = ViewStates.Gone;



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
                            var bay_phone = layout.FindViewById<TextView>(Resource.Id.phones_TextView);

                            var bay_timeout = layout.FindViewById<TextView>(Resource.Id.timeout_TextView);
                            var bay_makemodelcolor = layout.FindViewById<TextView>(Resource.Id.makemodelcolor_TextView);
                            var bay_services = layout.FindViewById<TextView>(Resource.Id.timein_TextView);
                            var bay_upcharges = layout.FindViewById<TextView>(Resource.Id.timein_TextView);

                            bay_number.Text = data.BayName;
                            ticket_number.Text = "Ticket#: " + data.TicketNumber;

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
            else
            {
                NoRecord.Visibility = ViewStates.Visible;
                BayDetailsScrollView.Visibility = ViewStates.Gone;
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
    internal class Item
    {
        public string Label { get; set; }
        public int WashValue { get; set; }
        public int DetailValue { get; set; }
        public int EmployeeValue { get; set; }
        public double ScoreValue { get; set; }
    }
}