import { Component, OnInit, ViewChild } from '@angular/core';
import * as moment from 'moment';
import { merge } from 'rxjs/operators';
import { ReportsService } from 'src/app/shared/services/data-service/reports.service';
import * as _ from 'underscore';
import { LocationDropdownComponent } from 'src/app/shared/components/location-dropdown/location-dropdown.component';
import { ExcelService } from 'src/app/shared/services/common-service/excel.service';
import { DatePipe } from '@angular/common';
import { jsPDF } from 'jspdf';
import html2canvas from 'html2canvas';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { ExportFiletypeComponent } from 'src/app/shared/components/export-filetype/export-filetype.component';
declare var $: any;
@Component({
  selector: 'app-hourly-wash',
  templateUrl: './hourly-wash.component.html'
})
export class HourlyWashComponent implements OnInit {
  @ViewChild(LocationDropdownComponent) locationDropdownComponent: LocationDropdownComponent;
  @ViewChild(ExportFiletypeComponent) exportFiletypeComponent: ExportFiletypeComponent;
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
  dateAndTimeStamp = moment(new Date()).format('M/d/YY, h:mm a');
  constructor(
    private reportsService: ReportsService,
    private excelService: ExcelService,
    private datePipe: DatePipe,
    private spinner: NgxSpinnerService,
    private toastr :ToastrService
  ) { }

  ngOnInit(): void {
    this.locationId = localStorage.getItem('empLocationId');
    this.weeklyDateAssign();
    this.viewHourlyReport();
  }

  weeklyDateAssign() {
    const currentDate = new Date();
    const first = currentDate.getDate() - currentDate.getDay();
    const last = first + 7;
    this.startDate = new Date(currentDate.setDate(first));
    this.currentWeek = this.startDate;
    this.endDate = new Date(currentDate.setDate(last));
    this.endDate = this.endDate.setDate(this.startDate.getDate() + 7);
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
    if (event !== null) {
      this.startDate = event[0];
      this.endDate = event[1];
    }
  }

  refesh() {
    this.locationId = +localStorage.getItem('empLocationId');
    this.locationDropdownComponent.locationId = this.locationId;
    this.exportFiletypeComponent.type = '';
    this.weeklyDateAssign();
    this.viewHourlyReport();
  }

  viewHourlyReport() {
    const finalObj = {
      locationId: +this.locationId,
      fromDate: moment(this.startDate).format(),
      endDate: moment(this.endDate).format()
    };
    this.spinner.show();
    this.reportsService.getHourlyWashReport(finalObj).subscribe(res => {
      if (res.status === 'Success') {
        this.spinner.hide()
        const hourlyRate = JSON.parse(res.resultData);
        this.salesDetails = [];
        this.totalWashCount = [];
        this.washModel = [];
        this.SalesSummaryModel = [];
        this.hourlyWashReport = [];
        this.washServiceName = [];
        this.totalDeposits = 0;
        this.totalBC = 0;
        this.totalAccount = 0;
        this.totalGiftCard = 0;
        this.totalTips = 0;
        this.totalActual = 0;
        this.totalSales = 0;
        this.totalDifference = 0;
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
                  count: washCount,
                  date: item
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
                  Sales: sale.Total,
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
          this.salesDetails.forEach(item => {
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
          this.salesDetails.forEach( item => {
            const manager = _.where(this.hourlyWashManager, { JobDate: item.JobDate });
            if (manager.length > 0) {
              item.Managers = manager[0].FirstName + '' + manager[0].LastName;
            }
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
      else{
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');

      }
    }, (err) => {
      this.spinner.hide();
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
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
        // const hourlySalesDetail = this.salesObj(this.salesDetails);
        // this.excelService.exportAsCSVFile(this.hourlyWashReport, 'HourlyWashReport_' +
        //   this.datePipe.transform(this.todayDate, 'MM') + '/' + this.datePipe.transform(this.todayDate, 'yyyy') + '_' + locationName);
        // this.excelService.exportAsCSVFile(hourlySalesDetail, 'HourlySalesDetail' +
        //   this.datePipe.transform(this.todayDate, 'MM') + '/' + this.datePipe.transform(this.todayDate, 'yyyy') + '_' + locationName);
        const finalObj = {
          locationId: +this.locationId,
          fromDate: moment(this.startDate).format(),
          endDate: moment(this.endDate).format()
        };

        this.reportsService.getHourlyWashExport(finalObj).subscribe(data => {
          if (data) {
            this.download(data, 'excel', 'Hourly Wash Report');
            return data;
          }
        }, (err) => {
          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        })
        break;
      }
      default: {
        return;
      }
    }
  }

  download(data: any, type, fileName = 'Excel') {
    let format: string;
    format = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet';
    let a: HTMLAnchorElement;
    a = document.createElement('a');
    document.body.appendChild(a);
    const blob = new Blob([data], { type: format });
    const url = window.URL.createObjectURL(blob);
    a.href = url;
    a.download = fileName;
    a.click();
  }
  
  customizeObj(hourlyWash) {
    if (hourlyWash.length > 0) {
      const wash = hourlyWash.map(item => {
        return {
          JobDate: this.datePipe.transform(item.JobDate, 'MM-dd-yyyy'),
          Temperature : +item.Temperature,
          Rain : +item.Rain,
          Score : +item.TotalWashHours == 0 ? 0 : (((+item._1PMTotal) + (+item._2PMTotal) + (+item._3PMTotal) + (+item._4PMTotal) + (+item._5PMTotal) +
          (+item._6PMTotal) + (+item._7AMTotal) + (+item._7PMTotal) + (+item._8AMTotal)
          + (+item._9AMTotal) + (+item._10AMTotal) + (+item._11AMTotal) + (+item._12AMTotal))/item.TotalWashHours).toFixed(2),
          Goal : +item.Goal,
          NoOfWashes : +item.NoOfWashes,
          TotalWashHours : +item.TotalWashHours,
          Day: this.datePipe.transform(item.JobDate, 'EEE'),
          _7AM: +item._7AM,
          _8AM: +item._8AM,
          _9AM: +item._9AM,
          _10AM: +item._10AM,
          _11AM: +item._11AM,
          _12PM: +item._12AM,
          _1PM: +item._1PM,
          _2PM: +item._2PM,
          _3PM: +item._3PM,
          _4PM: +item._4PM,
          _5PM: +item._5PM,
          _6PM: +item._6PM,
          _7PM: +item._7PM,
          TotalWashCount: (+item._1PMTotal) + (+item._2PMTotal) + (+item._3PMTotal) + (+item._4PMTotal) + (+item._5PMTotal) +
            (+item._6PMTotal) + (+item._7AMTotal) + (+item._7PMTotal) + (+item._8AMTotal)
            + (+item._9AMTotal) + (+item._10AMTotal) + (+item._11AMTotal) + (+item._12AMTotal),
            _7AMTotal: +item._7AMTotal,
            _8AMTotal: +item._8AMTotal,
            _9AMTotal: +item._9AMTotal,
            _10AMTotal: +item._10AMTotal,
            _11AMTotal: +item._11AMTotal,
            _12PMTotal: +item._12AMTotal,
            _1PMTotal: +item._1PMTotal,
            _2PMTotal: +item._2PMTotal,
            _3PMTotal: +item._3PMTotal,
            _4PMTotal: +item._4PMTotal,
            _5PMTotal: +item._5PMTotal,
            _6PMTotal: +item._6PMTotal,
            _7PMTotal: +item._7PMTotal,
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
      const saleName = [];
      salesdetail.forEach(item => {
        item.serviceName.forEach(name => {
          saleName.push(name);
        });
        sale.push({
          JobDate: this.datePipe.transform(item.JobDate, 'MM-dd-yyyy'),
          Day: this.datePipe.transform(item.JobDate, 'EEE'),
          Account: item.Account,
          Actual: 0,
          BC: item.BC,
          Deposits: 0,
          Difference: 0,
          GiftCard: item.GiftCard,
          Managers: '',
          Sales: item.Sales,
          Tips: 0,
        });
      });
      sale.forEach(item => {
        const serviceName = saleName.filter(name => item.JobDate === this.datePipe.transform(name.date, 'MM-dd-yyyy'));
        if (serviceName.length > 0) {
          serviceName.forEach(ele => {
            item[ele.serviceName] = ele.count;
          });
        }
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

  printReport() {
    $('#hourlyWash').show();
    const dataURL = document.getElementById('hourlyWash').innerHTML;
    const dateAndTimeStamp = moment(new Date()).format('M/d/YY, h:mm a');
    const content = '<!DOCTYPE html><html><head><title>Hourly Wash Report</title><link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css"/>'
      + '<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css"/><style>'
      + '</style><script src="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"></script></head><body><table><thead id="header"><tr><td>'
      + '</td></tr><tr><td><div>'  + '</div></td></tr></thead><tbody><tr><td><div class="upperTeethData print-table-border"><div></div><div>' + dataURL + '</div></div></td></tr><tr><td>'
      + '<div class="lowerTeethData print-table-border"><div></div><div> </div></div></td></tr><tr><td><div class="casetype print-table-border"></div>'
      + '</td></tr></tbody><tfoot><tr><td><div style="width:100%;" id="footer">' + '<div style="font-size:14px;margin-right:15px;float:right;">' + dateAndTimeStamp +
      '</div></div></td></tr></tfoot></table><body></html>';
    const popupWin = window.open('', '_blank', 'scrollbars=1,width:100%;height:100%');
    popupWin.document.open();
    popupWin.document.write(content);
    popupWin.document.close(); // necessary for IE >= 10
    popupWin.focus(); // necessary for IE >= 10*/
    setTimeout(() => {
      popupWin.print();
      popupWin.close();
    }, 2000);
    $('#hourlyWash').hide();
  }





}
