import { Component, OnInit } from '@angular/core';
import { DashboardService } from 'src/app/shared/services/data-service/dashboard.service';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  noOfWashes: any = 0;
  nOfDetail: any = 0;
  washEmployee: any = 0;
  currentCar: any = 0;
  foreCastedCar: any = 0;
  averageCarWashTime: any;
  location: any = [];
  constructor(
    public dashboardService: DashboardService,
    private messageService: MessageServiceToastr
  ) { }

  ngOnInit(): void {
    this.getLocationList();
    this.getDashboardCount();
  }

  getDashboardCount() {
    const obj = {
      id: localStorage.getItem('empLocationId'),
      date: new Date()
    };
    this.dashboardService.getDetailCount(obj).subscribe( res => {
      const dashboardCount = JSON.parse(res.resultData);
      console.log(dashboardCount, 'count');
      this.noOfWashes = dashboardCount.Dashboard.WashesCount.WashesCount;
      this.nOfDetail = dashboardCount.Dashboard.DetailsCount.DetailsCount;
      this.washEmployee = dashboardCount.Dashboard.EmployeeCount.EmployeeCount;
      this.currentCar = dashboardCount.Dashboard.Current.Current;
      this.foreCastedCar = dashboardCount.Dashboard.ForecastedCars.ForecastedCars;
      this.averageCarWashTime = dashboardCount.Dashboard.AverageWashTime.AverageWashTime;
    });
  }

  // Get All Location
  getLocationList() {
    this.dashboardService.getLocation().subscribe(res => {
      if (res.status === 'Success') {
        const location = JSON.parse(res.resultData);
        this.location = location.Location;
      } else {
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }

}
