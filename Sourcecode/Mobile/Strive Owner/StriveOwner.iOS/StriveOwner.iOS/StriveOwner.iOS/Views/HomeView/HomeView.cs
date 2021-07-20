using System;
using CoreGraphics;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.Owner;
using StriveOwner.iOS.UIUtils;
using UIKit;

namespace StriveOwner.iOS.Views.HomeView
{
    public partial class HomeView : MvxViewController<HomeViewModel>
    {
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

        private void DoInitialSetup()
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

            getStatisticsData();
        }
         
        private async void getStatisticsData()
        {
            await ViewModel.getStatistics();
            await ViewModel.getDashboardSchedule();
            setData();
            setScheduleData();
        }

        private void setData()
        {
            if(ViewModel.statisticsData != null)
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
                forecastedCount.Text = ViewModel.statisticsData.ForecastedCar.ToString();
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
            if (ViewModel.dbSchedule != null)
            {
                var scheduleSource = new DBSchedule_DataSource(ViewModel.dbSchedule);
                ScheduleTableView.Source = scheduleSource;
                ScheduleTableView.TableFooterView = new UIView(CGRect.Empty);
                ScheduleTableView.DelaysContentTouches = false;
                ScheduleTableView.ReloadData();
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
    }
}

