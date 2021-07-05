import { Component, OnInit, Input } from '@angular/core';
import { ChartOptions, ChartType, ChartDataSets } from 'chart.js';
import { Label, Color } from 'ng2-charts';
import * as _ from 'underscore';

@Component({
  selector: 'app-graph-dashboard',
  templateUrl: './graph-dashboard.component.html'
})
export class GraphDashboardComponent implements OnInit {
  public barChartOptions: ChartOptions = {
    responsive: true,
    // We use these empty structures as placeholders for dynamic theming.
    scales: {
      xAxes: [{
        gridLines: {
          display: false
        }
      }], yAxes: [{
        gridLines: {
          display: false
        }
      }]
    }
  };
  public barChartLabels: Label[];
  public barChartType: ChartType = 'bar';
  public barChartLegend = false;

  public barChartData: ChartDataSets[];
  @Input() location?: any;
  @Input() dashboardStatistics?: any;
  isBarChart: boolean;
  isLineChart: boolean;
  isPieChart: boolean;
  isDotLineChart: boolean;
  chartDetail: any = [];
  dataPoints: any;
  selectedLocationIds = [];
  public barChartColors: Color[];
  constructor() { }

  ngOnInit(): void {
    this.isBarChart = true;
    this.isLineChart = false;
    this.isPieChart = false;
    this.isDotLineChart = false;
    this.getDataPoints();
    this.getChartDetail();
  }

  dotLineChart() {
    this.barChartType = 'line';
    this.isDotLineChart = true;
    this.isLineChart = false;
    this.isPieChart = false;
    this.isBarChart = false;
    this.barChartOptions = {
      responsive: true,
      // We use these empty structures as placeholders for dynamic theming.
      scales: {
        xAxes: [{
          gridLines: {
            display: false
          }
        }], yAxes: [{
          gridLines: {
            display: false
          },
          ticks: {
            min: 0,
            stepSize: 10,
          }
        }]
      },
      elements: {
        line: {
          fill: false
        }
      }
    };
  }

  barChart() {
    this.barChartType = 'bar';
    this.isDotLineChart = false;
    this.isLineChart = false;
    this.isPieChart = false;
    this.isBarChart = true;
  }

  pieChart() {
    this.barChartType = 'pie';
    this.isDotLineChart = false;
    this.isLineChart = false;
    this.isPieChart = true;
    this.isBarChart = false;
  }

  lineChart() {
    this.barChartType = 'line';
    this.isDotLineChart = false;
    this.isLineChart = true;
    this.isPieChart = false;
    this.isBarChart = false;
    this.barChartOptions = {
      responsive: true,
      // We use these empty structures as placeholders for dynamic theming.
      scales: {
        xAxes: [{
          gridLines: {
            display: false
          }
        }], yAxes: [{
          gridLines: {
            display: false
          },
          ticks: {
            min: 0,
            stepSize: 10,
          }
        }]
      }
    };
  }

  getChartDetail() {
    this.barChartData = [];
    this.barChartLabels = [];
    const locatioName = [];
    const wash = [];
    const detail = [];
    const employee = [];
    this.dashboardStatistics.forEach( item => {
      locatioName.push(item.LocationName);
    });
    this.dashboardStatistics.forEach(item => {
      item.isSelected = true;
    });
    const chartData = [];
    this.dataPoints.forEach(item => {
      if (item.isSelected) {
        const points = _.pluck(this.dashboardStatistics, item.name);
        chartData.push({
          data: points,
          label: item.displayName
        });
      }
    });
    this.barChartData = chartData;
    this.barChartLabels = locatioName;
  }

  selectedLocation(loc) {
    this.barChartData = [];
    this.barChartLabels = [];
    const chartData = [];
    const locatioName = [];
    const locationId = this.selectedLocationIds.filter(item => item !== loc.LocationId);
    this.dashboardStatistics.forEach(item => {
      if (item.LocationId === loc.LocationId) {
        item.isSelected = !item.isSelected;
      }
    });
    this.dashboardStatistics.forEach(item => {
      if (item.isSelected) {
        locatioName.push(item.LocationName);
      }
    });
    const filteredLoction = this.dashboardStatistics.filter(item => item.isSelected === true);
    this.dataPoints.forEach(item => {
      if (item.isSelected) {
        const points = _.pluck(filteredLoction, item.name);
        chartData.push({
          data: points,
          label: item.displayName
        });
      }
    });
    this.barChartData = chartData;
    this.barChartLabels = locatioName;
  }

  selectedDatapoints(data) {
    this.barChartData = [];
    this.dataPoints.forEach(item => {
      if (item.id === data.id) {
        item.isSelected = !item.isSelected;
      }
    });
    const backgroundColor = [];
    this.dataPoints.forEach(item => {
      if (item.isSelected) {
        backgroundColor.push({
          backgroundColor: item.color
        });
      }
    });
    this.barChartColors = backgroundColor;
    const chartData = [];
    this.dataPoints.forEach(item => {
      if (item.isSelected) {
        const points = _.pluck(this.dashboardStatistics, item.name);
        chartData.push({
          data: points,
          label: item.displayName
        });
      }
    });
    this.barChartData = chartData;
  }

  getDataPoints() {
    this.dataPoints = [
      {
        id: 1,
        name: 'WashesCount',
        color: '#fd397a',
        isSelected: true,
        displayName: 'Washes'
      },
      {
        id: 2,
        name: 'DetailCount',
        color: '#f3c200',
        isSelected: true,
        displayName: 'Details'
      },
      {
        id: 3,
        name: 'EmployeeCount',
        color: '#24489A',
        isSelected: true,
        displayName: 'Employees'
      },
      {
        id: 4,
        name: 'Score',
        color: '#24CAFF',
        isSelected: true,
        displayName: 'Score'
      },
      {
        id: 5,
        name: 'ForecastedCar',
        color: '#5968DD',
        isSelected: false,
        displayName: 'Currents/Forecasted Car'
      },
      {
        id: 6,
        name: 'WashSales',
        color: '#fd397a',
        isSelected: false,
        displayName: 'Wash Sales'
      },
      {
        id: 7,
        name: 'DetailSales',
        color: '#FFB822',
        isSelected: false,
        displayName: 'Detail Sales'
      },
      {
        id: 8,
        name: 'ExtraServiceSales',
        color: '#24489A',
        isSelected: false,
        displayName: 'Extra Service Sales'
      },
      {
        id: 9,
        name: 'MerchandizeSales',
        color: '#24CAFF',
        isSelected: false,
        displayName: 'Merchandize Sales'
      },
      {
        id: 10,
        name: 'TotalSales',
        color: '#1DC9B7',
        isSelected: false,
        displayName: 'Total Sales'
      },
      {
        id: 11,
        name: 'MembershipClientSales',
        color: '#5968DD',
        isSelected: false,
        displayName: 'Membership Client Sales'
      },
      {
        id: 12,
        name: 'AverageWashPerCar',
        color: '#FD397A',
        isSelected: false,
        displayName: 'Average Wash Per Car'
      },
      {
        id: 13,
        name: 'AverageDetailPerCar',
        color: '#FFB822',
        isSelected: false,
        displayName: 'Average Detail Per Car'
      },
      {
        id: 14,
        name: 'AverageExtraServicePerCar',
        color: '#24489A',
        isSelected: false,
        displayName: 'Average ExtraService Per Car'
      },
      {
        id: 15,
        name: 'AverageTotalPerCar',
        color: '#24CAFF',
        isSelected: false,
        displayName: 'Average Total Per Car'
      },
      {
        id: 16,
        name: 'LabourCostPerCarMinusDetail',
        color: '#1DC9B7',
        isSelected: false,
        displayName: 'Labour Cost Per Car'
      },
      {
        id: 17,
        name: 'DetailCostPerCar',
        color: '#5968DD',
        isSelected: false,
        displayName: 'Detail Cost Per Car'
      },
    ];
    const backgroundColor = [];
    this.dataPoints.forEach(item => {
      if (item.isSelected) {
        backgroundColor.push({
          backgroundColor: item.color
        });
      }
    });
    this.barChartColors = backgroundColor;
  }

}
