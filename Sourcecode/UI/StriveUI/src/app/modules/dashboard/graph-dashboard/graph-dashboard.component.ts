import { Component, OnInit, Input } from '@angular/core';
import { ChartOptions, ChartType, ChartDataSets } from 'chart.js';
import { Label, Color } from 'ng2-charts';
import * as _ from 'underscore';

@Component({
  selector: 'app-graph-dashboard',
  templateUrl: './graph-dashboard.component.html',
  styleUrls: ['./graph-dashboard.component.css']
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

  getDataPoints() {
    this.dataPoints = [
      {
        id: 1,
        name: 'wash',
        color: '#fd397a',
        isSelected: true
      },
      {
        id: 2,
        name: 'detail',
        color: '#f3c200',
        isSelected: true
      },
      {
        id: 3,
        name: 'employee',
        color: '#24489A',
        isSelected: true
      },
      {
        id: 4,
        name: 'score',
        color: '#24CAFF',
        isSelected: true
      },
      {
        id: 5,
        name: 'washTime',
        color: '#5968DD',
        isSelected: false
      }
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
    this.chartDetail = [
      {
        LocationId: 2045,
        locationName: 'Strive New Salon',
        wash: 45,
        detail: 35,
        employee: 25,
        score: 56
      },
      {
        LocationId: 2044,
        locationName: 'CHECK',
        wash: 55,
        detail: 45,
        employee: 30,
        score: 87
      },
      {
        LocationId: 2034,
        locationName: 'Main street',
        wash: 35,
        detail: 25,
        employee: 15,
        score: 45
      },
      {
        LocationId: 2033,
        locationName: 'Old Milton',
        wash: 65,
        detail: 55,
        employee: 30,
        score: 67
      }
    ];
    const locatioName = [];
    const wash = [];
    const detail = [];
    const employee = [];
    this.chartDetail.forEach(item => {
      locatioName.push(item.locationName);
      this.selectedLocationIds.push(item.LocationId);
    });
    this.chartDetail.forEach(item => {
      item.isSelected = true;
    });
    const chartData = [];
    this.dataPoints.forEach(item => {
      if (item.isSelected) {
        const points = _.pluck(this.chartDetail, item.name);
        chartData.push({
          data: points,
          label: item.name
        });
      }
    });
    this.barChartData = chartData;
    this.barChartLabels = locatioName;
  }

  selectedLocation(loc) {
    const chartData = [];
    const locatioName = [];
    const locationId = this.selectedLocationIds.filter(item => item !== loc.LocationId);
    this.chartDetail.forEach(item => {
      if (item.LocationId === loc.LocationId) {
        item.isSelected = !item.isSelected;
      }
    });
    this.chartDetail.forEach(item => {
      if (item.isSelected) {
        locatioName.push(item.locationName);
      }
    });
    const filteredLoction = this.chartDetail.filter(item => item.isSelected === true);
    this.dataPoints.forEach(item => {
      if (item.isSelected) {
        const points = _.pluck(filteredLoction, item.name);
        chartData.push({
          data: points,
          label: item.name
        });
      }
    });
    this.barChartData = chartData;
    this.barChartLabels = locatioName;
  }

  selectedDatapoints(data) {
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
        const points = _.pluck(this.chartDetail, item.name);
        chartData.push({
          data: points,
          label: item.name
        });
      }
    });
    this.barChartData = chartData;
  }

}
