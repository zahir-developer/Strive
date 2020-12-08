import { Component, OnInit } from '@angular/core';
import * as moment from 'moment';
import { merge } from 'rxjs/operators';
import { ReportsService } from 'src/app/shared/services/data-service/reports.service';
import * as _ from 'underscore';
@Component({
  selector: 'app-hourly-wash',
  templateUrl: './hourly-wash.component.html',
  styleUrls: ['./hourly-wash.component.css']
})
export class HourlyWashComponent implements OnInit {
  locationId: any;
  fileType: number;
  todayDate = new Date();
  startDate: any;
  currentWeek: any;
  endDate: any;
  dateRange: any = [];
  daterangepickerModel: any;
  hourlyWashReport: any = [];
  hourlyWashReportDay: any;
  washModel: any = [];
  mergedList: any = [];
  SalesSummaryModel: any = [];
  washServiceName = [];
  salesDetails = [];
  totalWashCount = [];
  totalGiftCard = 0;
  totalAccount = 0;
  totalBC = 0;
  totalDeposits = 0;
  totalTips = 0;
  totalSales = 0;
  totalDifference = 0;
  totalActual = 0;
  constructor(
    private reportsService: ReportsService
  ) { }

  ngOnInit(): void {
    this.locationId = localStorage.getItem('empLocationId');
    this.weeklyDateAssign();
  }

  weeklyDateAssign() {
    const currentDate = new Date();
    const first = currentDate.getDate() - currentDate.getDay();
    const last = first + 6;
    this.startDate = new Date(currentDate.setDate(first));
    this.currentWeek = this.startDate;
    this.endDate = new Date(currentDate.setDate(last));
    this.endDate = this.endDate.setDate(this.startDate.getDate() + 6);
    this.endDate = new Date(moment(this.endDate).format());
    this.daterangepickerModel = [this.startDate, this.endDate];
  }

  getfileType(event) {
    this.fileType = +event.target.value;
  }

  onLocationChange(event) {
    this.locationId = +event;
  }

  onValueChange(event) {
    console.log(event, 'start');
    this.viewHourlyReport();
    if (event !== null) {
      this.startDate = event[0];
      this.endDate = event[1];
    }
  }

  refesh() {
    this.viewHourlyReport();
  }

  viewHourlyReport() {
    // 2034, '2020-11-16', '2020-11-17'
    const finalObj = {
      locationId: +this.locationId,
      fromDate: moment(this.startDate).format(),
      endDate: moment(this.endDate).format()
    };
    this.reportsService.getHourlyWashReport(finalObj).subscribe(res => {
      if (res.status === 'Success') {
        const hourlyRate = JSON.parse(res.resultData);
        if (hourlyRate.GetHourlyWashReport.LocationWashServiceViewModel !== null) {
          this.washModel = hourlyRate.GetHourlyWashReport.LocationWashServiceViewModel;
        }
        if (hourlyRate.GetHourlyWashReport.SalesSummaryViewModel !== null) {
          this.SalesSummaryModel = hourlyRate.GetHourlyWashReport.SalesSummaryViewModel;
        }
        if (hourlyRate.GetHourlyWashReport.WashHoursViewModel !== null) {
          this.hourlyWashReport = hourlyRate.GetHourlyWashReport.WashHoursViewModel;
        }

        if (this.washModel.length > 0) {
          const jobDate = _.pluck(this.washModel, 'JobDate');
          const uniqDate = [...new Set(jobDate)];
          const washName = _.pluck(this.washModel, 'ServiceName');
          this.washServiceName = [...new Set(washName)];
          // this.washModel.forEach( item => {
          //   this.washServiceName.push({
          //     date: item.JobDate,
          //     name: item.ServiceName
          //   });
          // });
          uniqDate.forEach(item => {
            const washService = _.where(this.washModel, { JobDate: item });
            const serviceName = [];
            if (washService.length > 0) {
              let washCount = 0;
              this.washServiceName.forEach(name => {
                const nameBased = washService.filter(ele => ele.ServiceName === name);
                nameBased.forEach(count => {
                  washCount = washCount + count.WashCount;
                });
                serviceName.push({
                  serviceName: name,
                  count: washCount
                });
              });
            }
            const sales = _.where(this.SalesSummaryModel, { JobDate: item });
            if (sales.length > 0) {
              sales.forEach(sale => {
                this.salesDetails.push({
                  JobDate: item,
                  serviceName,
                  GiftCard: sale.GiftCard,
                  Account: sale.Account,
                  BC: sale.Credit,
                  Deposits: 0,
                  Tips: 0,
                  Actual: 0,
                  Sales: 0,
                  Difference: 0,
                  Managers: ''
                });
              });
            } else {
              this.salesDetails.push({
                JobDate: item,
                serviceName,
                GiftCard: 0,
                Account: 0,
                BC: 0,
                Deposits: 0,
                Tips: 0,
                Actual: 0,
                Sales: 0,
                Difference: 0,
                Managers: ''
              });
            }
          });
          console.log(this.salesDetails, 'salesdetail');
          this.salesDetails.forEach(item => {
            this.totalDeposits = this.totalDeposits + item.Deposits;
            this.totalBC = this.totalBC + item.BC;
            this.totalAccount = this.totalAccount + item.Account;
            this.totalGiftCard = this.totalGiftCard + item.GiftCard;
            this.totalTips = this.totalTips + item.Tips;
            this.totalActual = this.totalActual + item.Actual;
            this.totalSales = this.totalSales + item.Sales;
            this.totalDifference = this.totalDifference + item.Difference;
          });
          this.washServiceName.forEach( name => {
            let totalValue = 0;
            this.salesDetails.forEach( ele => {
              ele.serviceName.forEach( count => {
                if (name === count.serviceName) {
                  totalValue = totalValue + count.count;
                }
              });
            });
            this.totalWashCount.push({
              serviceName: name,
              totalValue
            });
          });
        }
      }
    });
  }

}
