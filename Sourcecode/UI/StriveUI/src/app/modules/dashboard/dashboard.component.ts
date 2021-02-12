import { Component, OnInit, ViewChild } from '@angular/core';
import { DashboardService } from 'src/app/shared/services/data-service/dashboard.service';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { NgbModalOptions, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FilterDashboardComponent } from './filter-dashboard/filter-dashboard.component';
import * as moment from 'moment';
declare var $: any;

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
  dashboardStatistics: any = [];
  averageDetailPerCar = 0;
  averageExtraServicePerCar = 0;
  averageTotalPerCar = 0;
  averageWashPerCar = 0;
  currents = 0;
  detailCostPerCar = 0;
  detailCount = 0;
  detailSales = 0;
  employeeCount = 0;
  extraServiceSales = 0;
  forecastedCar = 0;
  labourCostPerCarMinusDetail = 0;
  merchandizeSales = 0;
  monthlyClientSales = 0;
  score = 0;
  totalSales = 0;
  washSales = 0;
  washTime = 0;
  washesCount = 0;
  firstSectionTogggle: boolean;
  secondSectionToggle: boolean;
  thirdSectionToggle: boolean;
  fromDate: any;
  toDate: any;
  locationId = 0;
  constructor(
    public dashboardService: DashboardService,
    private messageService: MessageServiceToastr,
    private modalService: NgbModal
  ) { }

  ngOnInit(): void {
    this.showDialog = false;
    this.firstSectionTogggle = false;
    this.secondSectionToggle = false;
    this.thirdSectionToggle = false;
    this.fromDate = moment(new Date()).format();
    this.toDate = moment(new Date()).format();
    this.getLocationList();
    this.getDashboardStatistics(0);
  }

  getDashboardCount() {
    const obj = {
      id: localStorage.getItem('empLocationId'),
      date: new Date()
    };
    this.dashboardService.getDetailCount(obj).subscribe(res => {
      const dashboardCount = JSON.parse(res.resultData);
      this.noOfWashes = dashboardCount.Dashboard.WashesCount.WashesCount;
      this.nOfDetail = dashboardCount.Dashboard.DetailsCount.DetailsCount;
      this.washEmployee = dashboardCount.Dashboard.EmployeeCount.EmployeeCount;
      this.currentCar = dashboardCount.Dashboard.Current.Current;
      this.foreCastedCar = dashboardCount.Dashboard.ForecastedCars.ForecastedCars;
      this.averageCarWashTime = dashboardCount.Dashboard.AverageWashTime.AverageWashTime;
    });
  }

  getDashboardStatistics(locationID) {
    // const locationId = localStorage.getItem('empLocationId');
    this.locationId = locationID;
    this.dashboardStatistics = [];
    this.resetValue();
    const finalObj = {
      locationId: locationID,
      fromDate: this.fromDate,
      toDate: this.toDate
    };
    this.dashboardService.getDashboardStatistics(finalObj).subscribe(res => {
      if (res.status === 'Success') {
        const dashboardCount = JSON.parse(res.resultData);
        console.log(dashboardCount, 'dashboard');
        this.dashboardStatistics = dashboardCount.GetDashboardStatisticsForLocationId;
        this.dashboardStatistics.forEach(item => {
          this.washesCount = this.washesCount + item.WashesCount;
          this.detailCount = this.detailCount + item.DetailCount;
          this.employeeCount = this.employeeCount + item.EmployeeCount;
          this.washTime = item.WashTime;
          this.score = this.score + item.Score;
          this.currents = this.currents + item.Currents;
          this.forecastedCar = this.forecastedCar + item.ForecastedCar;
          this.washSales = this.washSales + item.WashSales;
          this.detailSales = this.detailSales + item.DetailSales;
          this.extraServiceSales = this.extraServiceSales + item.ExtraServiceSales;
          this.merchandizeSales = this.merchandizeSales + item.MerchandizeSales;
          this.totalSales = this.totalSales + item.TotalSales;
          this.monthlyClientSales = this.monthlyClientSales + item.MonthlyClientSales;
          this.averageWashPerCar = this.averageWashPerCar + item.AverageWashPerCar;
          this.averageDetailPerCar = this.averageDetailPerCar + item.AverageDetailPerCar;
          this.averageExtraServicePerCar = this.averageExtraServicePerCar + item.AverageExtraServicePerCar;
          this.averageTotalPerCar = this.averageTotalPerCar + item.AverageTotalPerCar;
          this.labourCostPerCarMinusDetail = this.labourCostPerCarMinusDetail + item.LabourCostPerCarMinusDetail;
          this.detailCostPerCar = this.detailCostPerCar + item.DetailCostPerCar;
        });
      } else {
        //this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }

  resetValue() {
    this.washesCount = 0;
    this.detailCount = 0;
    this.employeeCount = 0;
    this.score = 0;
    this.currents = 0;
    this.forecastedCar = 0;
    this.washSales = 0;
    this.detailSales = 0;
    this.extraServiceSales = 0;
    this.merchandizeSales = 0;
    this.totalSales = 0;
    this.monthlyClientSales = 0;
    this.averageWashPerCar = 0;
    this.averageDetailPerCar = 0;
    this.averageExtraServicePerCar = 0;
    this.averageTotalPerCar = 0;
    this.labourCostPerCarMinusDetail = 0;
    this.detailCostPerCar = 0;
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
      size: '100px'
    };
    const modalRef = this.modalService.open(FilterDashboardComponent, ngbModalOptions);
    modalRef.componentInstance.filterDashboard.subscribe((receivedEntry) => {
      if (receivedEntry) {
        this.fromDate = receivedEntry.fromDate;
        this.toDate = receivedEntry.toDate;
        this.getDashboardStatistics(this.locationId);
      }
    });
  }

  mainStreet() {
    this.firstSectionTogggle = !this.firstSectionTogggle;
  }

  mainStreets() {
    this.secondSectionToggle = !this.secondSectionToggle;
  }

  mainStreetAvg() {
    this.thirdSectionToggle = !this.thirdSectionToggle;
  }

}
