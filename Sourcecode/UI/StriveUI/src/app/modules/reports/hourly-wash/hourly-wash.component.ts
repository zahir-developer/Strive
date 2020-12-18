import { Component, OnInit, ViewChild } from '@angular/core';
import * as moment from 'moment';
import { merge } from 'rxjs/operators';
import { ReportsService } from 'src/app/shared/services/data-service/reports.service';
import * as _ from 'underscore';
import { LocationDropdownComponent } from 'src/app/shared/components/location-dropdown/location-dropdown.component';
import { ExcelService } from 'src/app/shared/services/common-service/excel.service';
import { DatePipe } from '@angular/common';
declare var $: any;
@Component({
  selector: 'app-hourly-wash',
  templateUrl: './hourly-wash.component.html',
  styleUrls: ['./hourly-wash.component.css']
})
export class HourlyWashComponent implements OnInit {
  @ViewChild(LocationDropdownComponent) locationDropdownComponent: LocationDropdownComponent;
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
  fileTypeEvent: boolean = false;
  hourlyWashManager: any = [];
  constructor(
    private reportsService: ReportsService,
    private excelService: ExcelService,
    private datePipe: DatePipe
  ) { }

  ngOnInit(): void {
    this.locationId = localStorage.getItem('empLocationId');
    this.weeklyDateAssign();
    this.viewHourlyReport();
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
    this.fileTypeEvent = true;

    this.fileType = +event.target.value;
  }

  onLocationChange(event) {
    this.locationId = +event;
  }

  onValueChange(event) {
    console.log(event, 'start');
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
        this.salesDetails = [];
        this.totalWashCount = [];
        this.washModel = [];
        this.SalesSummaryModel = [];
        this.hourlyWashReport = [];
        this.washServiceName = [];
        if (hourlyRate.GetHourlyWashReport.LocationWashServiceViewModel !== null) {
          this.washModel = hourlyRate.GetHourlyWashReport.LocationWashServiceViewModel;
        }
        if (hourlyRate.GetHourlyWashReport.SalesSummaryViewModel !== null) {
          this.SalesSummaryModel = hourlyRate.GetHourlyWashReport.SalesSummaryViewModel;
        }
        if (hourlyRate.GetHourlyWashReport.WashHoursViewModel !== null) {
          this.hourlyWashReport = hourlyRate.GetHourlyWashReport.WashHoursViewModel;
          this.hourlyWashReport = this.customizeObj(this.hourlyWashReport);
        }

        if (hourlyRate.GetHourlyWashReport.HourlyWashEmployeeViewModel !== null) {
          this.hourlyWashManager = hourlyRate.GetHourlyWashReport.HourlyWashEmployeeViewModel;
        }

        if (this.washModel.length > 0) {
          const jobDate = _.pluck(this.washModel, 'JobDate');
          const uniqDate = [...new Set(jobDate)];
          const washName = _.pluck(this.washModel, 'ServiceName');
          this.washServiceName = [...new Set(washName)];
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
          this.salesDetails.forEach( item => {
            const manager = _.where(this.hourlyWashManager, { EventDate: item.JobDate });
            if (manager.length > 0) {
              item.Managers = manager[0].FirstName + '' + manager[0].LastName;
            }
          });
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
          this.washServiceName.forEach(name => {
            let totalValue = 0;
            this.salesDetails.forEach(ele => {
              ele.serviceName.forEach(count => {
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

  export() {
    $('#printReport').show();
    const fileType = this.fileType !== undefined ? this.fileType : '';
    const locationName = this.locationDropdownComponent.locationName;
    if (fileType === '' || fileType === 0) {
      $('#printReport').hide();
      return;
    }
    switch (fileType) {
      case 1: {
        this.excelService.exportAsPDFFile('hourlyWashReport', 'HourlyWashReport_' + this.datePipe.transform(this.todayDate, 'MM')
          + '/' + this.datePipe.transform(this.todayDate, 'yyyy')
          + '_' + locationName + '.pdf');
        $('#printReport').hide();
        break;
      }
      case 2: {
        $('#printReport').hide();
        const hourlySalesDetail = this.salesObj(this.salesDetails);
        this.excelService.exportAsCSVFile(this.hourlyWashReport, 'HourlyWashReport_' +
          this.datePipe.transform(this.todayDate, 'MM') + '/' + this.datePipe.transform(this.todayDate, 'yyyy') + '_' + locationName);
        this.excelService.exportAsCSVFile(hourlySalesDetail, 'HourlySalesDetail' +
          this.datePipe.transform(this.todayDate, 'MM') + '/' + this.datePipe.transform(this.todayDate, 'yyyy') + '_' + locationName);
        break;
      }
      case 3: {
        $('#printReport').hide();
        const hourlySalesDetail = this.salesObj(this.salesDetails);
        this.excelService.exportAsCSVFile(this.hourlyWashReport, 'HourlyWashReport_' +
          this.datePipe.transform(this.todayDate, 'MM') + '/' + this.datePipe.transform(this.todayDate, 'yyyy') + '_' + locationName);
        this.excelService.exportAsCSVFile(hourlySalesDetail, 'HourlySalesDetail' +
          this.datePipe.transform(this.todayDate, 'MM') + '/' + this.datePipe.transform(this.todayDate, 'yyyy') + '_' + locationName);
        break;
      }
      default: {
        return;
      }
    }
  }

  customizeObj(hourlyWash) {
    if (hourlyWash.length > 0) {
      const wash = hourlyWash.map(item => {
        return {
          JobDate: this.datePipe.transform(item.JobDate, 'MM-dd-yyyy'),
          Day: this.datePipe.transform(item.JobDate, 'EEE'),
          _1PM: +item._1PM,
          _2PM: +item._2PM,
          _3PM: +item._3PM,
          _4PM: +item._4PM,
          _5PM: +item._5PM,
          _6PM: +item._6PM,
          _7AM: +item._7AM,
          _7PM: +item._7PM,
          _8AM: +item._8AM,
          _9AM: +item._9AM,
          _9PM: +item._9PM,
          _10AM: +item._10AM,
          _11AM: +item._11AM,
          _12AM: +item._12AM,
          TotalWashCount: (+item._1PM) + (+item._2PM) + (+item._3PM) + (+item._4PM) + (+item._5PM) +
            (+item._6PM) + (+item._7AM) + (+item._7PM) + (+item._8AM)
            + (+item._9AM) + (+item._9PM) + (+item._10AM) + (+item._11AM) + (+item._12AM)
        };
      });
      return wash;
    } else {
      return [];
    }
  }

  salesObj(salesdetail) {
    if (salesdetail.length > 0) {
      const sale = [];
      salesdetail.forEach(item => {
        item.serviceName.forEach(name => {
          sale.push({
            Account: item.Account,
            Actual: 0,
            BC: item.BC,
            Deposits: 0,
            Difference: 0,
            GiftCard: item.GiftCard,
            JobDate: this.datePipe.transform(item.JobDate, 'MM-dd-yyyy'),
            Managers: '',
            Sales: 0,
            Tips: 0,
            [name.serviceName]: name.count
          });
        });
      });
      return sale;
    }
  }

  print() {
    $('#printReport').show();
    setTimeout(() => {
      $('#printReport').hide();
    }, 1000);
  }

}
