using System;
using System.Collections.ObjectModel;
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
        public string SelectedLocName;

        public HomeView() : base("HomeView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            DoInitialSetup();
            // Perform any additional setup after loading the view, typically from a nib.
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
            setSegment();
        }
         
        private async void getStatisticsData(int locationId)
        {            
            await ViewModel.getStatistics(locationId);
            await ViewModel.getDashboardSchedule(locationId);           
            setData();
            setScheduleData();
            setChartView();
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

                var newSegment = 3;
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
                    forecastedImage.Image = UIImage.FromBundle("DB-TotalSales");
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

            getStatisticsData(selectedLoc.LocationId);           
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
            model.Series.Add(new ColumnSeries { Title = "Washes", ItemsSource = Items, ValueField = "Value1", ColumnWidth = 20 });
            model.Series.Add(new ColumnSeries { Title = "Details", ItemsSource = Items, ValueField = "Value2", ColumnWidth = 20 });
            model.Series.Add(new ColumnSeries { Title = "Employees", ItemsSource = Items, ValueField = "Value3", ColumnWidth = 20 });
            model.Series.Add(new ColumnSeries { Title = "Score", ItemsSource = Items, ValueField = "Value4", ColumnWidth = 20 });

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

