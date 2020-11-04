import { Component, OnInit } from '@angular/core';
import { DashboardService } from 'src/app/shared/services/data-service/dashboard.service';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { NgbModalOptions, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FilterDashboardComponent } from './filter-dashboard/filter-dashboard.component';

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
  showDialog: boolean;
  dashboardStatistics: any;
  constructor(
    public dashboardService: DashboardService,
    private messageService: MessageServiceToastr,
    private modalService: NgbModal
  ) { }

  ngOnInit(): void {
    this.showDialog = false;
    this.getLocationList();
    this.getDashboardStatistics();
  }

  getDashboardCount() {
    const obj = {
      id: localStorage.getItem('empLocationId'),
      date: new Date()
    };
    this.dashboardService.getDetailCount(obj).subscribe( res => {
      const dashboardCount = JSON.parse(res.resultData);
      this.noOfWashes = dashboardCount.Dashboard.WashesCount.WashesCount;
      this.nOfDetail = dashboardCount.Dashboard.DetailsCount.DetailsCount;
      this.washEmployee = dashboardCount.Dashboard.EmployeeCount.EmployeeCount;
      this.currentCar = dashboardCount.Dashboard.Current.Current;
      this.foreCastedCar = dashboardCount.Dashboard.ForecastedCars.ForecastedCars;
      this.averageCarWashTime = dashboardCount.Dashboard.AverageWashTime.AverageWashTime;
    });
  }

  getDashboardStatistics() {
    const locationId = localStorage.getItem('empLocationId');
    this.dashboardService.getDashboardStatistics(locationId).subscribe( res => {
      const dashboardCount = JSON.parse(res.resultData);
      this.dashboardStatistics = dashboardCount.GetDashboardStatisticsForLocationId;
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

  filterDashboard() {
    this.showDialog = true;
    const ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false,
      size: 'lg'
    };
    const modalRef = this.modalService.open(FilterDashboardComponent, ngbModalOptions);
  }

}
