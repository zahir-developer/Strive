import { Component, Input, OnInit } from '@angular/core';
import { DetailService } from '../../services/data-service/detail.service';

@Component({
  selector: 'app-dashboard-statics',
  templateUrl: './dashboard-statics.component.html',
  styleUrls: ['./dashboard-statics.component.css']
})
export class DashboardStaticsComponent implements OnInit {
  @Input() jobTypeId?: any;
  detailCount: any;
  washCount: any;
  employeeCount: any;
  score: any;
  current: any;
  forecastedCar: any;
  averageTime: any;
  constructor(
    private detail: DetailService
  ) { }

  ngOnInit(): void {
  }

  // Get Details Count
  getDashboardDetails() {
    const obj = {
      id: localStorage.getItem('empLocationId'),
      date: new Date(),
      jobType: this.jobTypeId
    };
    this.detail.getDetailCount(obj).subscribe( res => {
      const wash = JSON.parse(res.resultData);
      if (wash.Dashboard !== null) {
        this.detailCount = wash.Dashboard.DetailsCount;
        this.washCount = wash.Dashboard.WashesCount;
        this.employeeCount = wash.Dashboard.EmployeeCount;
        this.score = wash.Dashboard.Score;
        this.current = wash.Dashboard.Current;
        this.forecastedCar = wash.Dashboard.ForecastedCars;
        this.averageTime = wash.Dashboard.AverageWashTime;
      }
    });
  }

}
