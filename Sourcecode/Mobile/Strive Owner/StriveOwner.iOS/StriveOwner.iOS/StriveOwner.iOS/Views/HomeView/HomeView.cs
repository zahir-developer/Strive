using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CoreGraphics;
using MvvmCross.Platforms.Ios.Views;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Owner;
using StriveOwner.iOS.UIUtils;
using UIKit;

namespace StriveOwner.iOS.Views.HomeView
{
    public partial class HomeView : MvxViewController<HomeViewModel>
    {
        public Locations Locations;
        public int SelectedLocationId = 0;
        public string SelectedLocName;
        bool useRefreshControl = false;
        UIRefreshControl RefreshControl;
        public int newSegment = 3;

        public HomeView() : base("HomeView", null)
        {
        }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();
            DoInitialSetup();
            // Perform any additional setup after loading the view, typically from a nib.
            await RefreshAsync();
            AddRefreshControl();

            DashboardInnerView.Add(RefreshControl);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private async void DoInitialSetup()
        {
            NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes()
            {
                Font = DesignUtils.OpenSansBoldFifteen(),
                ForegroundColor = UIColor.Clear.FromHex(0x24489A),
            };
            NavigationItem.Title = "Dashboard";

            DashboardParentView.Layer.CornerRadius = 5;
            DashboardInnerView.Layer.CornerRadius = 5;

            ScheduleTableView.RegisterNibForCellReuse(DBSchedule_Cell.Nib, DBSchedule_Cell.Key);
            ScheduleTableView.BackgroundColor = UIColor.Clear;
            ScheduleTableView.ReloadData();

            Locations = await ViewModel.GetAllLocationsCommand();
            Locations.Location.Reverse();
            setSegment();
        }

        async Task RefreshAsync()
        {
            // only activate the refresh control if the feature is available  
            if (useRefreshControl)
                RefreshControl.BeginRefreshing();

            if (useRefreshControl)
                RefreshControl.EndRefreshing();

            
        }

        #region * iOS Specific Code  
        // This method will add the UIRefreshControl to the table view if  
        // it is available, ie, we are running on iOS 6+  
        void AddRefreshControl()
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(6, 0))
            {
                // the refresh control is available, let's add it  
                RefreshControl = new UIRefreshControl();
                RefreshControl.ValueChanged += async (sender, e) =>
                {
                    getStatisticsData(SelectedLocationId);
                    //LocationSegment.SelectedSegment = 0;
                    dashboardService_Seg.SelectedSegment = 0;
                    await RefreshAsync();
                };
                useRefreshControl = true;
            }
        }
        #endregion
        private async void getStatisticsData(int locationId)
        {            
            await ViewModel.getStatistics(locationId);
            await ViewModel.getDashboardSchedule(locationId);           
            setData();
            setScheduleData();
            setChartView();
        }

        private void setChartView()
        {
            LineChart_Button.SetImage(UIImage.FromBundle("Image-5"), UIControlState.Normal);
            ScatterChart_Button.SetImage(UIImage.FromBundle("Image-8"), UIControlState.Normal);
            BarChart_Button.SetImage(UIImage.FromBundle("Barchart"), UIControlState.Normal);
            PieChart_Button.SetImage(UIImage.FromBundle("Image-6"), UIControlState.Normal);
            var model = new PlotModel()
            {
                PlotType = PlotType.XY,
                LegendSymbolLength = 5,
                LegendPlacement = LegendPlacement.Outside,
                LegendOrientation = LegendOrientation.Vertical,
                Title = SelectedLocName
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




            washline.Points.Add(new DataPoint(0, Double.Parse(washCount.Text)));
            //washline.Points.Add(new DataPoint(1, Double.Parse(washCount.Text) + 1));
            //washline.Points.Add(new DataPoint(2, Double.Parse(washCount.Text) + 2));
            //washline.Points.Add(new DataPoint(3, Double.Parse(washCount.Text) + 3));
            detailline.Points.Add(new DataPoint(0, Double.Parse(detailCount.Text)));
            //detailline.Points.Add(new DataPoint(1, Double.Parse(detailCount.Text) + 1));
            //detailline.Points.Add(new DataPoint(2, Double.Parse(detailCount.Text) + 2));
            //detailline.Points.Add(new DataPoint(3, Double.Parse(detailCount.Text) + 3));
            employeeline.Points.Add(new DataPoint(0, Double.Parse(employeeCount.Text)));
            //employeeline.Points.Add(new DataPoint(1, Double.Parse(employeeCount.Text) + 1));
            //employeeline.Points.Add(new DataPoint(2, Double.Parse(employeeCount.Text) + 2));
            //employeeline.Points.Add(new DataPoint(3, Double.Parse(employeeCount.Text) + 3));
            scoreline.Points.Add(new DataPoint(0, Double.Parse(scoreCount.Text)));
            //scoreline.Points.Add(new DataPoint(1, Double.Parse(scoreCount.Text) + 1));
            //scoreline.Points.Add(new DataPoint(2, Double.Parse(scoreCount.Text) + 2));
            //scoreline.Points.Add(new DataPoint(3, Double.Parse(scoreCount.Text) + 3));

            model.Series.Add(washline);
            model.Series.Add(detailline);
            model.Series.Add(employeeline);
            model.Series.Add(scoreline);
            plotView.Model = model;
            plotView.Frame = new CGRect(0, 0, this.View.Frame.Width + 10, this.View.Frame.Height);
        }

        private void setSegment()
        {
            if (Locations.Location.Count > 3)
            {
                var index = 0;

                while (index != 3)
                {
                    LocationSegment.SetTitle(Locations.Location[index].LocationName, index);
                    index++;
                }

                //var newSegment = 3;
                while (newSegment != Locations.Location.Count)
                {
                    LocationSegment.InsertSegment(Locations.Location[newSegment].LocationName, newSegment, true);
                    LocationSegment.SetTitle(Locations.Location[newSegment].LocationName, newSegment);
                    newSegment++;
                }
            }
            else
            {
                var index = 0;
                foreach (var item in Locations.Location)
                {
                    LocationSegment.SetTitle(item.LocationName, index);
                    index++;
                }
            }
            getStatisticsData(Locations.Location[0].LocationId);
            SelectedLocName = Locations.Location[0].LocationName;
        }

        private void setData()
        {           
            if (ViewModel.statisticsData != null)
            {
                WashLbl.Text = "No of Washes";
                DetailLbl.Text = "No of Details";
                washEmployeeLbl.Text = "Wash Employees";
                scoreLbl.Text = "Score";
                current_ForecastedLbl.Text = "Current/Forecasted Cars";
                carWashtimelbl.Text = "Average Car Wash Time";

                washCount.Text = ViewModel.statisticsData.WashesCount.ToString();
                detailCount.Text = ViewModel.statisticsData.DetailCount.ToString();
                employeeCount.Text = ViewModel.statisticsData.EmployeeCount.ToString();
                scoreCount.Text = ViewModel.statisticsData.Score.ToString();
                forecastedCount.Text = ViewModel.statisticsData.Currents.ToString() +"/"+ ViewModel.statisticsData.ForecastedCar.ToString();
                carWashTimeCount.Text = ViewModel.statisticsData.WashTime.ToString();

                washImage.Image = UIImage.FromBundle("Dashboard-Washes");
                detailImage.Image = UIImage.FromBundle("Dashboard-Details");
                washEmployeeImage.Image = UIImage.FromBundle("Dashboard-WashEmployees");
                scoreImage.Image = UIImage.FromBundle("Dashboard-Score");
                forecastedImage.Image = UIImage.FromBundle("Dashboard-Cars");
                carWashTimeImage.Image = UIImage.FromBundle("Dashboard-WashTime");
            }
        }

        private void setScheduleData()
        {
            if (ViewModel.dbSchedule != null && ViewModel.dbSchedule.DetailsGrid.BayJobDetailViewModel != null)
            {              
                var scheduleSource = new DBSchedule_DataSource(ScheduleTableView,ViewModel.dbSchedule);
                ScheduleTableView.Source = scheduleSource;
                ScheduleTableView.TableFooterView = new UIView(CGRect.Empty);
                ScheduleTableView.DelaysContentTouches = false;
                ScheduleTableView.ReloadData();
                detailScheduleStatus.Hidden = true;
                ScheduleTableView.Hidden = false;
            }
            else
            {                
                detailScheduleStatus.Hidden = false;
                ScheduleTableView.Hidden = true;
            }
        }
        partial void DashboardServices_Touch(UISegmentedControl sender)
        {
            var index = dashboardService_Seg.SelectedSegment;
            
            if(index == 0)
            {
                setData();
            }
            else if(index == 1)
            {
                if (ViewModel.statisticsData != null)
                {
                    WashLbl.Text = "Wash Sales";
                    DetailLbl.Text = "Detail Sales";
                    washEmployeeLbl.Text = "Extra Services Sales";
                    scoreLbl.Text = "Merchandise Sales";
                    current_ForecastedLbl.Text = "Total Sales";
                    carWashtimelbl.Text = "Monthly Client Sales";

                    washCount.Text = ViewModel.statisticsData.WashSales.ToString();
                    detailCount.Text = ViewModel.statisticsData.DetailSales.ToString();
                    employeeCount.Text = ViewModel.statisticsData.ExtraServiceSales.ToString();
                    scoreCount.Text = ViewModel.statisticsData.MerchandizeSales.ToString();
                    forecastedCount.Text = ViewModel.statisticsData.TotalSales.ToString();
                    carWashTimeCount.Text = ViewModel.statisticsData.MonthlyClientSales.ToString();

                    washImage.Image = UIImage.FromBundle("DB-WashSales");
                    detailImage.Image = UIImage.FromBundle("DB-DetailSales");
                    washEmployeeImage.Image = UIImage.FromBundle("DB-ExtraServiceSales");
                    scoreImage.Image = UIImage.FromBundle("DB-MerchandiseSales");
                    forecastedImage.Image = UIImage.FromBundle("DB-ExtraServiceSales");
                    carWashTimeImage.Image = UIImage.FromBundle("DB-MonthlyClientSales");
                }                    
            }
            else if(index == 2)
            {
                if (ViewModel.statisticsData != null)
                {
                    WashLbl.Text = "Ave Wash $ per car";
                    DetailLbl.Text = "Ave Detail $ per car";
                    washEmployeeLbl.Text = "Ave Extra Service $ per car";
                    scoreLbl.Text = "Ave Total per car All";
                    current_ForecastedLbl.Text = "Labor cost per car";
                    carWashtimelbl.Text = "Detail cost per car";

                    washCount.Text = ViewModel.statisticsData.AverageWashPerCar.ToString();
                    detailCount.Text = ViewModel.statisticsData.AverageDetailPerCar.ToString();
                    employeeCount.Text = ViewModel.statisticsData.AverageExtraServicePerCar.ToString();
                    scoreCount.Text = ViewModel.statisticsData.AverageTotalPerCar.ToString();
                    forecastedCount.Text = ViewModel.statisticsData.LabourCostPerCarMinusDetail.ToString();
                    carWashTimeCount.Text = ViewModel.statisticsData.DetailCostPerCar.ToString();

                    washImage.Image = UIImage.FromBundle("Ave-Wash");
                    detailImage.Image = UIImage.FromBundle("Ave-Detail");
                    washEmployeeImage.Image = UIImage.FromBundle("Ave-ExtraService");
                    scoreImage.Image = UIImage.FromBundle("Ave-TotalPerCar");
                    forecastedImage.Image = UIImage.FromBundle("LaborCost");
                    carWashTimeImage.Image = UIImage.FromBundle("DetailCost");
                }               
            }
        }

        partial void locationSegment_Touch(UISegmentedControl sender)
        {
            var selectedIndex = LocationSegment.SelectedSegment;
            SelectedLocName = LocationSegment.TitleAt(selectedIndex);

            var selectedLoc = Locations.Location.Find(x => x.LocationName == SelectedLocName);
            SelectedLocationId = selectedLoc.LocationId;
            getStatisticsData(selectedLoc.LocationId);           
        }

        partial void ScatterChart_ButtonTouch(UIButton sender)
        {
            ScatterChart_Button.SetImage(UIImage.FromBundle("Image-9"), UIControlState.Normal);
            LineChart_Button.SetImage(UIImage.FromBundle("Image-4"), UIControlState.Normal);
            BarChart_Button.SetImage(UIImage.FromBundle("Barchart"), UIControlState.Normal);
            PieChart_Button.SetImage(UIImage.FromBundle("Image-6"), UIControlState.Normal);
            var model = new PlotModel()
            {
                PlotType = PlotType.XY,
                LegendSymbolLength = 5,
                LegendPlacement = LegendPlacement.Outside,
                LegendOrientation = LegendOrientation.Vertical,
                Title = SelectedLocName
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

            var washpoint = new ScatterPoint(Double.Parse(washCount.Text), 0);
            var detailpoint = new ScatterPoint(Double.Parse(detailCount.Text), 0);
            var employeepoint = new ScatterPoint(Double.Parse(employeeCount.Text), 0);
            var scorepoint = new ScatterPoint(Double.Parse(scoreCount.Text),0);
            
            washseries.Points.Add(washpoint);
            detailseries.Points.Add(detailpoint);
            employeeseries.Points.Add(employeepoint);
            scoreseries.Points.Add(scorepoint);
            model.Series.Add(washseries);
            model.Series.Add(detailseries);
            model.Series.Add(employeeseries);
            model.Series.Add(scoreseries);
            plotView.Model = model;
            plotView.Frame = new CGRect(0, 0, this.View.Frame.Width + 10, this.View.Frame.Height);


        }
        partial void LineChart_ButtonTouch(UIButton sender)
        {
            setChartView();
        }
        partial void BarChart_ButtonTouch(UIButton sender)
        {
            LineChart_Button.SetImage(UIImage.FromBundle("Image-4"), UIControlState.Normal);
            ScatterChart_Button.SetImage(UIImage.FromBundle("Image-8"), UIControlState.Normal);
            BarChart_Button.SetImage(UIImage.FromBundle("Barchart_active"), UIControlState.Normal);
            PieChart_Button.SetImage(UIImage.FromBundle("Image-6"), UIControlState.Normal);
            setBarChartView();
        }
        partial void PieChart_ButtonTouch(UIButton sender)
        {
            PieChart_Button.SetImage(UIImage.FromBundle("Image-7"), UIControlState.Normal);
            LineChart_Button.SetImage(UIImage.FromBundle("Image-4"), UIControlState.Normal);
            ScatterChart_Button.SetImage(UIImage.FromBundle("Image-8"), UIControlState.Normal);
            BarChart_Button.SetImage(UIImage.FromBundle("Barchart"), UIControlState.Normal);
           
            var model = new PlotModel()
            {
                Title = SelectedLocName
            };

            var seriespie = new PieSeries() {
                StrokeThickness = 2.0,
                InsideLabelPosition = 1.05,
                OutsideLabelFormat = "",
                TickHorizontalLength = 0.00,
                TickRadialLength = 0.00,
                AngleSpan = 360, 
            };

            seriespie.Slices.Add(new PieSlice("Wash", Double.Parse(washCount.Text)) { IsExploded = false}); 
            seriespie.Slices.Add(new PieSlice("Detail", Double.Parse(detailCount.Text)) { IsExploded = false});
            seriespie.Slices.Add(new PieSlice("Employee", Double.Parse(employeeCount.Text)) { IsExploded = false});
            seriespie.Slices.Add(new PieSlice("Score", Double.Parse(scoreCount.Text)) { IsExploded = false });
            

            model.Series.Add(seriespie);
            plotView.Model = model;
            
            plotView.Frame = new CGRect(0, 0, this.View.Frame.Width + 10, this.View.Frame.Height);
        }

        public void setBarChartView()
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

            if (scoreCount.Text == "")
            {
                scoreCount.Text = "0";
            }

            var Items = new Collection<Item>
            {                
                new Item {Label = SelectedLocName, WashValue = int.Parse(washCount.Text.ToString()), DetailValue = int.Parse(detailCount.Text.ToString()), EmployeeValue = int.Parse(employeeCount.Text.ToString()), ScoreValue = double.Parse(scoreCount.Text.ToString())},
                
                //new Item {Label = "Old Milton", Value1 = 4, Value2 = 2, Value3 = 1},
                //new Item {Label = "Holcomb Bridge", Value1 = 1, Value2 = 4, Value3 = 2}
            };

            model.Axes.Add(new CategoryAxis { ItemsSource = Items, LabelField = "Label", AbsoluteMinimum = -0.5 });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, MinimumPadding = 0, AbsoluteMinimum = 0 });
            model.Series.Add(new ColumnSeries { Title = "Washes", ItemsSource = Items, ValueField = "WashValue", ColumnWidth = 20 });
            model.Series.Add(new ColumnSeries { Title = "Details", ItemsSource = Items, ValueField = "DetailValue", ColumnWidth = 20 });
            model.Series.Add(new ColumnSeries { Title = "Employees", ItemsSource = Items, ValueField = "EmployeeValue", ColumnWidth = 20 });
            model.Series.Add(new ColumnSeries { Title = "Score", ItemsSource = Items, ValueField = "ScoreValue", ColumnWidth = 20 });

            this.plotView.Model = model;
            plotView.Frame = new CGRect(0, 0, this.View.Frame.Width + 10, this.View.Frame.Height);            
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

