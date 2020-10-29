import { Component, OnInit, Input } from '@angular/core';
import { ChartOptions, ChartType, ChartDataSets } from 'chart.js';
import { Label } from 'ng2-charts';
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
  // public barChartData: ChartDataSets[] = [   = ['2006', '2007', '2008', '2009', '2010', '2011', '2012'];
  //   { data: [65, 59, 80, 81, 56, 55, 40], label: 'Series A' },
  //   { data: [28, 48, 40, 19, 86, 27, 90], label: 'Series B' },
  //   { data: [18, 78, 30, 89, 80, 37, 30], label: 'Series c' }
  // ];
  @Input() location?: any;
  isBarChart: boolean;
  isLineChart: boolean;
  isPieChart: boolean;
  isDotLineChart: boolean;
  chartDetail: any = [];
  dataPoints: any = ['wash', 'detail', 'employee'];
  selectedLocationIds = [];
  constructor() { }

  ngOnInit(): void {
    this.isBarChart = true;
    this.isLineChart = false;
    this.isPieChart = false;
    this.isDotLineChart = false;
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
        employee: 25
      },
      {
        LocationId: 2044,
        locationName: 'CHECK',
        wash: 55,
        detail: 45,
        employee: 30
      },
      {
        LocationId: 2034,
        locationName: 'Main street',
        wash: 35,
        detail: 25,
        employee: 10
      },
      {
        LocationId: 2033,
        locationName: 'Old Milton',
        wash: 65,
        detail: 55,
        employee: 30
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
    this.chartDetail.forEach( item => {
      item.isSelected = true;
    });
    const chartData = [];
    this.dataPoints.forEach(item => {
      const points = _.pluck(this.chartDetail, item);
      chartData.push({
        data: points,
        label: item
      });
    });
    this.barChartData  = chartData;
    this.barChartLabels = locatioName;
    console.log(this.barChartData, 'barchart');
  }

  selectedLocation(loc) {
    const chartData = [];
    const locatioName = [];
    const locationId = this.selectedLocationIds.filter( item => item !== loc.LocationId);
    this.chartDetail.forEach( item => {
      if ( item.LocationId ===  loc.LocationId) {
        item.isSelected = !item.isSelected;
      }
    });
    this.chartDetail.forEach( item => {
      if (item.isSelected) {
        locatioName.push(item.locationName);
      }
    });
    const filteredLoction = this.chartDetail.filter( item => item.isSelected === true);
    this.dataPoints.forEach( item => {
      const points = _.pluck(filteredLoction, item);
      chartData.push({
        data: points,
        label: item
      });
    });
    this.barChartData  = chartData;
    this.barChartLabels = locatioName;
  }

}
