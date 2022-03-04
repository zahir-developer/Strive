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
using OperationCanceledException = System.OperationCanceledException;
using Exception = System.Exception;
using Double = System.Double;
using Acr.UserDialogs;

namespace StriveOwner.Android.Resources.Fragments
{
    public class DashboardHomeFragment : MvxFragment<HomeViewModel>, ViewPager.IOnPageChangeListener, SwipeRefreshLayout.IOnRefreshListener
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
        public static int selectedLocationId = 0;
        private HorizontalScrollView button_ScrollView;
        private ImageButton lineChartView;
        private ImageButton barChartView;
        private ImageButton pieChartView;
        private ImageButton scatterChartView;
        private static bool isLineChartSelected, isBarChartSelected
            , isPieChartSelected, isScatterChartSelected;
        SwipeRefreshLayout swipeRefreshLayout;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.DashboardHome_Fragment, null);
            ViewModel = new HomeViewModel();
           
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
            lineChartView = rootView.FindViewById<ImageButton>(Resource.Id.lineChart);
            barChartView = rootView.FindViewById<ImageButton>(Resource.Id.barChart);
            pieChartView = rootView.FindViewById<ImageButton>(Resource.Id.pieChart);
            scatterChartView = rootView.FindViewById<ImageButton>(Resource.Id.scatterChart);
            swipeRefreshLayout = rootView.FindViewById<SwipeRefreshLayout>(Resource.Id.swipeRefreshLayout);
            swipeRefreshLayout.SetOnRefreshListener(this);
            GetLocations();
            servicesFragment = new ServicesFragment();
            salesFragment = new SalesFragment();
            revenueFragment = new RevenueFragment();

            lineChartView.Click += LineChartView_Click;
            barChartView.Click += BarChartView_Click;
            pieChartView.Click += PieChartView_Click;
            scatterChartView.Click += ScatterChartView_Click;
           
            return rootView;
        }

        private void ScatterChartView_Click(object sender, EventArgs e)
        {
            isScatterChartSelected = true;
            isPieChartSelected = false;
            isBarChartSelected = false;
            isLineChartSelected = false;
            SetScatterChart();  
        }

        private void PieChartView_Click(object sender, EventArgs e)
        {
            isPieChartSelected = true;
            isBarChartSelected = false;
            isLineChartSelected = false;
            isScatterChartSelected = false;
            SetPieChart();
        }

        private void BarChartView_Click(object sender, EventArgs e)
        {
            isBarChartSelected = true;
            isLineChartSelected = false;
            isScatterChartSelected = false;
            isPieChartSelected = false;
           SetBarChart();
        }

        private void LineChartView_Click(object sender, EventArgs e)
        {
            isLineChartSelected = true;
            isScatterChartSelected = false;
            isPieChartSelected = false;
            isBarChartSelected = false;
            SetLineChart();
        }
        private void SetPieChart()
        {
            lineChartView.SetImageResource(Resource.Drawable.ic_chartline);
            barChartView.SetImageResource(Resource.Drawable.ic_chartbar);
            pieChartView.SetImageResource(Resource.Drawable.selected_piechart);
            scatterChartView.SetImageResource(Resource.Drawable.ic_chartmetro);

            var model = new PlotModel()
            {
                Title = SelectedLocName,
                TitleFontSize = 15
            };

            var seriespie = new PieSeries()
            {
                StrokeThickness = 2.0,
                InsideLabelFormat = "",
                InsideLabelPosition = 0.00,
                OutsideLabelFormat = "{1}",
                TickHorizontalLength = 8,
                TickRadialLength = 0.00,
                AngleSpan = 360,
                AngleIncrement = 1.0,
                FontSize = 12,

            };
            seriespie.Slices.Add(new PieSlice("Wash", Double.Parse(ViewModel.statisticsData.WashesCount.ToString())) { IsExploded = false });
            seriespie.Slices.Add(new PieSlice("Detail", Double.Parse(ViewModel.statisticsData.DetailCount.ToString())) { IsExploded = false });
            seriespie.Slices.Add(new PieSlice("Employee", Double.Parse(ViewModel.statisticsData.EmployeeCount.ToString())) { IsExploded = false });
            seriespie.Slices.Add(new PieSlice("Score", Double.Parse(ViewModel.statisticsData.Score.ToString())) { IsExploded = false });


            model.Series.Add(seriespie);
            lineChart.Model = model;
        }
        private void SetScatterChart()
        {
            lineChartView.SetImageResource(Resource.Drawable.ic_chartline);
            barChartView.SetImageResource(Resource.Drawable.ic_chartbar);
            pieChartView.SetImageResource(Resource.Drawable.ic_chartpie);
            scatterChartView.SetImageResource(Resource.Drawable.selected_scatterchart);
            var model = new PlotModel()
            {
                PlotType = PlotType.XY,
                LegendSymbolLength = 5,
                LegendPlacement = LegendPlacement.Outside,
                LegendOrientation = LegendOrientation.Vertical,
                Title = SelectedLocName,
                TitleFontSize = 15
            };
            var washseries = new ScatterSeries()
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 3,
                Title = "Wash"
            };
            var detailseries = new ScatterSeries()
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 3,
                Title = "Detail"
            };
            var employeeseries = new ScatterSeries()
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 3,
                Title = "Employee"
            };
            var scoreseries = new ScatterSeries()
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 3,
                Title = "Score"
            };

            var washpoint = new ScatterPoint(Double.Parse(ViewModel.statisticsData.WashesCount.ToString()), 0);
            var detailpoint = new ScatterPoint(Double.Parse(ViewModel.statisticsData.DetailCount.ToString()), 0);
            var employeepoint = new ScatterPoint(Double.Parse(ViewModel.statisticsData.EmployeeCount.ToString()), 0);
            var scorepoint = new ScatterPoint(Double.Parse(ViewModel.statisticsData.Score.ToString()), 0);

            washseries.Points.Add(washpoint);
            detailseries.Points.Add(detailpoint);
            employeeseries.Points.Add(employeepoint);
            scoreseries.Points.Add(scorepoint);
            model.Series.Add(washseries);
            model.Series.Add(detailseries);
            model.Series.Add(employeeseries);
            model.Series.Add(scoreseries);
            lineChart.Model = model;

        }
        public void SetBarChart()
        {
            lineChartView.SetImageResource(Resource.Drawable.ic_chartline);
            barChartView.SetImageResource(Resource.Drawable.selected_barchart);
            pieChartView.SetImageResource(Resource.Drawable.ic_chartpie);
            scatterChartView.SetImageResource(Resource.Drawable.ic_chartmetro);
            var model = new PlotModel()
            {
                PlotType = PlotType.XY,
                LegendSymbolLength = 5,
                LegendPlacement = LegendPlacement.Outside,
                LegendOrientation = LegendOrientation.Vertical,
                Title = SelectedLocName,
                TitleFontSize = 15
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
                new Item {Label = "", WashValue = int.Parse(ViewModel.statisticsData.WashesCount.ToString()), DetailValue = int.Parse(ViewModel.statisticsData.DetailCount.ToString()), EmployeeValue = int.Parse(ViewModel.statisticsData.EmployeeCount.ToString()), ScoreValue = double.Parse(ViewModel.statisticsData.Score.ToString())},
                
                //new Item {Label = "Old Milton", Value1 = 4, Value2 = 2, Value3 = 1},
                //new Item {Label = "Holcomb Bridge", Value1 = 1, Value2 = 4, Value3 = 2}
            };

            model.Axes.Add(new CategoryAxis { ItemsSource = Items, LabelField = "Label", AbsoluteMinimum = -0.5 });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, MinimumPadding = 0, AbsoluteMinimum = 0 });
            model.Series.Add(new ColumnSeries { Title = "Washes", ItemsSource = Items, ValueField = "WashValue", ColumnWidth = 20 });
            model.Series.Add(new ColumnSeries { Title = "Details", ItemsSource = Items, ValueField = "DetailValue", ColumnWidth = 20 });
            model.Series.Add(new ColumnSeries { Title = "Employees", ItemsSource = Items, ValueField = "EmployeeValue", ColumnWidth = 20 });
            model.Series.Add(new ColumnSeries { Title = "Score", ItemsSource = Items, ValueField = "ScoreValue", ColumnWidth = 20 });

            lineChart.Model = model;
        }
        //private void SetBarChart()
        //{
        //    var model = new PlotModel()
        //    {
        //        PlotType = PlotType.XY,
        //        LegendSymbolLength = 5,
        //        LegendPlacement = LegendPlacement.Outside,
        //        LegendOrientation = LegendOrientation.Vertical,
        //        Title = SelectedLocName
        //    };

        //    CategoryAxis xaxis = new CategoryAxis();
        //    xaxis.Position = AxisPosition.Bottom;
        //    xaxis.Labels.Add("Mon, 4/24");
        //    xaxis.Labels.Add("Tue, 4/25");
        //    xaxis.Labels.Add("Wed, 4/26");
        //    xaxis.Labels.Add("Thu, 4/27");
        //    xaxis.Labels.Add("Mon, 4/24");
        //    xaxis.Labels.Add("Tue, 4/25");
        //    xaxis.AbsoluteMinimum = -.5;
        //    xaxis.AbsoluteMaximum = 6;
        //    xaxis.Zoom(0, 4.5);
        //    xaxis.Angle = 45;

        //    LinearAxis yaxis = new LinearAxis();
        //    yaxis.Position = AxisPosition.Left;
        //    yaxis.AbsoluteMinimum = 0;
        //    yaxis.MinimumPadding = 0;
        //    yaxis.AbsoluteMaximum = 100;

        //    ColumnSeries s1 = new ColumnSeries();
        //    s1.IsStacked = true;
        //    s1.Items.Add(new ColumnItem(20));
        //    s1.Items.Add(new ColumnItem(60));
        //    s1.Items.Add(new ColumnItem(40));
        //    s1.Items.Add(new ColumnItem(50));
        //    s1.Items.Add(new ColumnItem(20));
        //    s1.Items.Add(new ColumnItem(60));
        //    s1.ColumnWidth = 20;
        //    s1.FillColor = OxyColor.FromRgb(255, 0, 0);

        //    ColumnSeries s2 = new ColumnSeries();
        //    s2.IsStacked = true;
        //    s2.Items.Add(new ColumnItem(50));
        //    s2.Items.Add(new ColumnItem(30));
        //    s2.Items.Add(new ColumnItem(10));
        //    s2.Items.Add(new ColumnItem(20));
        //    s2.ColumnWidth = 20;

        //    var Items = new Collection<Item>
        //    {
        //        new Item {WashValue = int.Parse(ViewModel.statisticsData.WashesCount.ToString()), DetailValue = int.Parse(ViewModel.statisticsData.DetailCount.ToString()), EmployeeValue = int.Parse(ViewModel.statisticsData.EmployeeCount.ToString()), ScoreValue = double.Parse(ViewModel.statisticsData.Score.ToString())},

        //        //new Item {Label = "Old Milton", Value1 = 4, Value2 = 2, Value3 = 1},
        //        //new Item {Label = "Holcomb Bridge", Value1 = 1, Value2 = 4, Value3 = 2}
        //    };

        //    model.Axes.Add(new CategoryAxis { ItemsSource = Items, LabelField = "Label", AbsoluteMinimum = -0.5 });
        //    model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, MinimumPadding = 0, AbsoluteMinimum = 0 });
        //    model.Series.Add(new ColumnSeries { Title = "Washes", ItemsSource = Items, ValueField = "Value1", ColumnWidth = 20 });
        //    model.Series.Add(new ColumnSeries { Title = "Details", ItemsSource = Items, ValueField = "Value2", ColumnWidth = 20 });
        //    model.Series.Add(new ColumnSeries { Title = "Employees", ItemsSource = Items, ValueField = "Value3", ColumnWidth = 20 });
        //    model.Series.Add(new ColumnSeries { Title = "Score", ItemsSource = Items, ValueField = "Value4", ColumnWidth = 20 });

        //    lineChart.Model = model;
        //    //lineChart.fr = new CGRect(0, 0, View.Frame.Width + 10, View.Frame.Height);
        //    //return model;
        //}
        private void SetLineChart()
        {
            lineChartView.SetImageResource(Resource.Drawable.selected_linechart);
            barChartView.SetImageResource(Resource.Drawable.ic_chartbar);
            pieChartView.SetImageResource(Resource.Drawable.ic_chartpie);
            scatterChartView.SetImageResource(Resource.Drawable.ic_chartmetro);
            var model = new PlotModel()
            {
                PlotType = PlotType.XY,
                LegendSymbolLength = 5,
                LegendPlacement = LegendPlacement.Outside,
                LegendOrientation = LegendOrientation.Vertical,
                Title = SelectedLocName,
                TitleFontSize = 15
            };

            var washline = new LineSeries()
            {

                MarkerSize = 3,
                MarkerType = MarkerType.Circle,
                Title = "Wash"
            };
            var detailline = new LineSeries()
            {

                MarkerSize = 3,
                MarkerType = MarkerType.Circle,
                Title = "Detail"
            };
            var employeeline = new LineSeries()
            {

                MarkerSize = 3,
                MarkerType = MarkerType.Circle,
                Title = "Employee"
            };
            var scoreline = new LineSeries()
            {

                MarkerSize = 3,
                MarkerType = MarkerType.Circle,
                Title = "Score"
            };

            CategoryAxis xaxis = new CategoryAxis();
            xaxis.Position = AxisPosition.Bottom;
            xaxis.AbsoluteMinimum = -.5;
            xaxis.AbsoluteMaximum = 6;
            xaxis.Zoom(0, 3);
            xaxis.Angle = 45;

             washline.Points.Add(new DataPoint(0, Double.Parse(ViewModel.statisticsData.WashesCount.ToString())));
            detailline.Points.Add(new DataPoint(0, Double.Parse(ViewModel.statisticsData.DetailCount.ToString())));
            employeeline.Points.Add(new DataPoint(0, Double.Parse(ViewModel.statisticsData.EmployeeCount.ToString())));
            scoreline.Points.Add(new DataPoint(0, Double.Parse(ViewModel.statisticsData.Score.ToString())));
         
            model.Series.Add(washline);
            model.Series.Add(detailline);
            model.Series.Add(employeeline);
            model.Series.Add(scoreline);
            lineChart.Model = model;
        }
        private async void GetStatistics(int locationID)
        {
            try
            {
                await ViewModel.getStatistics(locationID);
                GetDashData(locationID);
                // setChartView();
                if (isLineChartSelected)
                {
                    SetLineChart();
                }
                else if (isPieChartSelected)
                {
                    SetPieChart();
                }
                else if (isBarChartSelected)
                {
                    SetBarChart();
                }
                else if (isScatterChartSelected)
                {
                    SetScatterChart();
                }
                else
                {
                    SetLineChart();
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

        private async void GetDashData(int locationID)
        {
            try
            {
                await ViewModel.getDashboardSchedule(locationID);
                bay_layout.RemoveAllViews();
                BayDetails();
                UpdateTabs();
            }
            catch (Exception ex)
            {
                if (ex is OperationCanceledException)
                {
                    return;
                }
            }
        }
        private void UpdateTabs()
        {
            servicesFragment.getServiceData(ViewModel.statisticsData);
            salesFragment.getSalesData(ViewModel.statisticsData);
            revenueFragment.getRevenueData(ViewModel.statisticsData);
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
                    ViewModel.Locations.Location.Reverse();
                    var BtnID = 777;
                    locationsLayout.Orientation = Orientation.Vertical;
                    LinearLayout row = new LinearLayout(Context);
                    var layoutParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
                    var btnParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
                    row.LayoutParameters = layoutParams;
                    foreach (var location in ViewModel.Locations.Location)
                    {
                        locationBtn = new Button(Context);
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
                            OwnerTempData.LocationID = FirstLocId;
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
            isBarChartSelected = false;
            isPieChartSelected = false;
            isScatterChartSelected = false;
            GetStatistics(locationId);
            //await ViewModel.getDashboardSchedule(locationId);
            //bay_layout.RemoveAllViews();
            //BayDetails();
        }  

        private void BayDetails()
        {

            if (ViewModel.dbSchedule != null && ViewModel.dbSchedule.DetailsGrid != null && ViewModel.dbSchedule.DetailsGrid.JobViewModel != null)
            {
                BayDetailsScrollView.Visibility = ViewStates.Visible;
                NoRecord.Visibility = ViewStates.Gone;            
                
                if (ViewModel.dbSchedule.DetailsGrid.JobViewModel.Count > 0)
                {
                    foreach (var data in ViewModel.dbSchedule.DetailsGrid.JobViewModel)
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

                            //bay_number.Text = data.BayName;
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
            dashhome_ViewPager.SetCurrentItem(DashboardInfoNeeds.selectedTab, false);
            dashhome_ViewPager.AddOnPageChangeListener(this);
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
        public void OnPageScrollStateChanged(int state)
        {

        }

        public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
        {

        }

        public void OnPageSelected(int position)
        {
            if (position == 0)
            {
                if(servicesFragment !=null && ViewModel !=null && ViewModel.statisticsData != null)
                {
                    servicesFragment.getServiceData(ViewModel.statisticsData);

                }
            }
            if (position == 1)
            {
                if (salesFragment !=null && ViewModel != null && ViewModel.statisticsData != null)
                {
                    salesFragment.getSalesData(ViewModel.statisticsData);

                }
            }
            if (position == 2)
            {
                if (revenueFragment != null && ViewModel != null && ViewModel.statisticsData != null)
                {
                    revenueFragment.getRevenueData(ViewModel.statisticsData);

                }

            }
        }

        public void OnRefresh()
        {
            GetStatistics(OwnerTempData.LocationID);
            swipeRefreshLayout.Refreshing = false;
        }
    }
    public static class DashboardInfoNeeds
    {
        public static int selectedTab { get; set; } = 0;
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