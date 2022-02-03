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
using Java.Lang;
using OperationCanceledException = System.OperationCanceledException;
using Exception = System.Exception;

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
        private LinearLayout bay_layout;
        private PlotView lineChart;
        private TextView NoRecord;
        private NestedScrollView BayDetailsScrollView;
        private static string SelectedLocName = "";
        private List<int> PreviousSelectedId = new List<int>();
        private int FirstLocId;
        Button locationBtn;
        List<Button> listBtn = new List<Button>();
        View layout;
        private static int selectedLocationId = 0;
        private HorizontalScrollView button_ScrollView;
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
            bay_layout = rootView.FindViewById<LinearLayout>(Resource.Id.BayDetails_LinearLayout);
            button_ScrollView = rootView.FindViewById<HorizontalScrollView>(Resource.Id.Button_ScrollView);
            NoRecord = rootView.FindViewById<TextView>(Resource.Id.norecord);
            BayDetailsScrollView = rootView.FindViewById<NestedScrollView>(Resource.Id.BayDetails_ScrollView);
            lineChart = rootView.FindViewById<PlotView>(Resource.Id.linechart);
            GetLocations();

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

            var Items = new Collection<Item>
            {
                new Item {Label = SelectedLocName, WashValue = int.Parse(ViewModel.statisticsData.WashesCount.ToString()), DetailValue = int.Parse(ViewModel.statisticsData.DetailCount.ToString()), EmployeeValue = int.Parse(ViewModel.statisticsData.EmployeeCount.ToString()), ScoreValue = double.Parse(ViewModel.statisticsData.Score.ToString())},
                
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
            try
            {
                await ViewModel.getStatistics(locationID);
                GetDashData(locationID);
                setChartView();
            }
            catch (Exception ex)
            {
                if (ex is OperationCanceledException)
                {
                    return;
                }
            }
           
        }

        private async void GetDashData(int locationID)
        {
            try
            {
                await ViewModel.getDashboardSchedule(locationID);
                bay_layout.RemoveAllViews();
                BayDetails();
            }
            catch (Exception ex)
            {
                if (ex is OperationCanceledException)
                {
                    return;
                }
            }
        }

        private async void GetLocations()
        {
            try
            {
                await ViewModel.GetAllLocationsCommand();
                PreviousSelectedId.Clear();
                listBtn.Clear();
                if (ViewModel.Locations != null && ViewModel.Locations.Location != null && ViewModel.Locations.Location.Count > 0)
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
                        // SelectedLocName = ViewModel.Locations.Location.First().LocationName;
                        // FirstLocId = ViewModel.Locations.Location.First().LocationId;                    
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
                        if (selectedLocationId == 0)
                        {
                            FirstLocId = ViewModel.Locations.Location.First().LocationId;
                            SelectedLocName = ViewModel.Locations.Location.First().LocationName;
                            if (BtnID == 778)
                            {
                                PreviousSelectedId.Add(BtnID);
                                locationBtn.Selected = true;
                            }
                        }
                        else
                        {
                            FirstLocId = selectedLocationId;
                            if (selectedLocationId == location.LocationId)
                            {
                                if (selectedLocationId == ViewModel.Locations.Location.Last().LocationId)
                                {
                                    Action myAction = () =>
                                    {
                                        button_ScrollView.FullScroll(FocusSearchDirection.Right);
                                    };
                                    button_ScrollView.PostDelayed(myAction, 1000);
                                }
                                SelectedLocName = location.LocationName;
                                // button_ScrollView.SmoothScrollTo(locationBtn.Top,locationBtn.Bottom);
                                PreviousSelectedId.Add(BtnID);
                                locationBtn.Selected = true;
                            }
                        }
                        locationBtn.Click += LocationBtn_Click;
                        row.AddView(locationBtn);
                    }
                    locationsLayout.AddView(row);
                    GetStatistics(FirstLocId);

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
            selectedLocationId = locationId;
            SelectedLocName = data.Text;
            dashhome_ViewPagerAdapter = new ViewPagerAdapter(ChildFragmentManager);
            dashhome_ViewPagerAdapter.AddFragment(servicesFragment, "Service");
            dashhome_ViewPagerAdapter.AddFragment(salesFragment, "Sales");
            dashhome_ViewPagerAdapter.AddFragment(revenueFragment, "Revenue");
            dashhome_ViewPager.Adapter = dashhome_ViewPagerAdapter;
            dashhome_TabLayout.SetupWithViewPager(dashhome_ViewPager);
            GetStatistics(locationId);
            //await ViewModel.getDashboardSchedule(locationId);
            //bay_layout.RemoveAllViews();
            //BayDetails();
        }  

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
                            
                            layout = LayoutInflater.From(Context).Inflate(Resource.Layout.BayDetails, bay_layout, false);

                            var bay_number = layout.FindViewById<TextView>(Resource.Id.bay_number);
                            var ticket_number = layout.FindViewById<TextView>(Resource.Id.ticket_number);
                            var bay_timein = layout.FindViewById<TextView>(Resource.Id.timein_TextView);
                            var bay_client = layout.FindViewById<TextView>(Resource.Id.client_TextView);
                            var bay_phone = layout.FindViewById<TextView>(Resource.Id.phones_TextView);
                            var bay_timeout = layout.FindViewById<TextView>(Resource.Id.timeout_TextView);
                            var bay_makemodelcolor = layout.FindViewById<TextView>(Resource.Id.makemodelcolor_TextView);
                            var bay_services = layout.FindViewById<TextView>(Resource.Id.serviceshome_TextView);
                            var bay_upcharges = layout.FindViewById<TextView>(Resource.Id.upchargeshome_TextView);

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