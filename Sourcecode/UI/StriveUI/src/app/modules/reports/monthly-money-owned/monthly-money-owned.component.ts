import { Component, OnInit, ViewChild } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ExcelService } from 'src/app/shared/services/common-service/excel.service';
import { ReportsService } from 'src/app/shared/services/data-service/reports.service';
import * as _ from 'underscore';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { ToastrService } from 'ngx-toastr';
import { LocationDropdownComponent } from 'src/app/shared/components/location-dropdown/location-dropdown.component';
import { ExportFiletypeComponent } from 'src/app/shared/components/export-filetype/export-filetype.component';
import { YearPickerComponent } from 'src/app/shared/components/year-picker/year-picker.component';
import { MonthPickerComponent } from 'src/app/shared/components/month-picker/month-picker.component';
import { CurrencyPipe } from '@angular/common';

@Component({
  selector: 'app-monthly-money-owned',
  templateUrl: './monthly-money-owned.component.html'
})
export class MonthlyMoneyOwnedComponent implements OnInit {
  @ViewChild(LocationDropdownComponent) locationDropdownComponent: LocationDropdownComponent;
  @ViewChild(ExportFiletypeComponent) exportFiletypeComponent: ExportFiletypeComponent;
  @ViewChild(YearPickerComponent) yearPickerComponent: YearPickerComponent;
  @ViewChild(MonthPickerComponent) monthPickerComponent: MonthPickerComponent;
  ownedReportList: any = [];
  accountAmount = 0;
  driveUpRate = 0;
  totalHB = 0;
  totalMS = 0;
  totalOM = 0;
  total = 0;
  noOfCustomer = 0;
  averageWashRate = 0;
  totalOwnedHB = 0;
  totalOwnedOM = 0;
  fileType: any = '';
  date = new Date();
  month: number;
  year: number;
  uniqLocationName: any = [];
  locationName: any = [];
  locationTotalValue = [];
  totalOwnedValue = [];
  locationId: any;
  owedLocationName = [];
  clonedownedReportList = [];
  constructor(
    private excelService: ExcelService,
    private reportsService: ReportsService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private currencyPipe: CurrencyPipe
  ) { }

  ngOnInit(): void {
    this.month = this.date.getMonth() + 1;
    this.year = this.date.getFullYear();
    this.locationId = localStorage.getItem('empLocationId');
    this.getMoneyOwnedReportList();
  }
  getfileType(event) {
    this.fileType = +event.target.value;
  }

  getMoneyOwnedReportList() {
    const date = this.year + '-' + ('0' + (this.month)).slice(-2);
    this.spinner.show();
    this.reportsService.getMonthlyMoneyOwnedReport(date, this.locationId).subscribe(res => {
      if (res.status === 'Success') {
        this.spinner.hide()
        const monthlyReport = JSON.parse(res.resultData);
        this.uniqLocationName = [];
        this.ownedReportList = [];
        this.locationTotalValue = [];
        this.locationName = [];
        this.totalOwnedValue = [];
        this.owedLocationName = [];
        this.accountAmount = 0;
        this.total = 0;
        this.averageWashRate = 0;
        if (monthlyReport.GetMonthlyMoneyOwnedReport.length > 0) {
          let locName = '';
          const jobDate = _.pluck(monthlyReport.GetMonthlyMoneyOwnedReport, 'JobDate');
          const locationNameBasedonID = _.where(monthlyReport.GetMonthlyMoneyOwnedReport, { LocationId: +this.locationId });
          if (locationNameBasedonID.length > 0) {
            locName = locationNameBasedonID[0].LocationName;
          }
          const uniqDate = [...new Set(jobDate)];
          const location = _.pluck(monthlyReport.GetMonthlyMoneyOwnedReport, 'LocationName');
          this.uniqLocationName = [...new Set(location)];
          this.locationName = this.uniqLocationName;
          const locationName = [];
          let letters = '';
          this.uniqLocationName.forEach(item => {
            const firstLetter = item.split(' ');
            if (firstLetter.length > 1) {
              letters = firstLetter[0].charAt(0).toUpperCase() + firstLetter[1].charAt(0).toUpperCase();
            } else {
              letters = firstLetter[0].charAt(0).toUpperCase();
            }
            locationName.push({
              ShortName: letters,
              Name: item
            });
          });
          this.uniqLocationName = locationName;
          this.uniqLocationName.forEach(item => {
            monthlyReport.GetMonthlyMoneyOwnedReport.forEach(report => {
              if (item.Name === report.LocationName) {
                report.ShortName = item.ShortName;
              }
            });
          });
          this.uniqLocationName.forEach(uniq => {
            if (locName !== uniq.Name) {
              this.owedLocationName.push(uniq);
            }
          });
          this.moontlyOwnedGrid(monthlyReport.GetMonthlyMoneyOwnedReport, uniqDate);
        }
      }
      else {
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');

      }
    }, (err) => {
      this.spinner.hide();
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  moontlyOwnedGrid(report, date) {
    const gridRecord = [];
    const locationGrid = [];
    date.forEach(item => {
      let accountAmount = 0;
      let noOfCustomer = 0;
      let washRate = 0;
      let customerName = '';
      const jobDateRecord = report.filter(record => record.JobDate === item);
      if (jobDateRecord.length > 0) {
        jobDateRecord.forEach(elem => {
          accountAmount = accountAmount + elem.AccountAmount;
          noOfCustomer = jobDateRecord.length;
          washRate = washRate + elem.WashesAmount;
          customerName = elem.CustomerName;
        });
        const finalObj: any = {};
        const location = [];
        const locationObj: any = {};
        const locationAmount = [];
        this.uniqLocationName.forEach(loc => {
          const locationBasedRecord = jobDateRecord.filter(record => record.ShortName === loc.ShortName);
          locationObj[loc.ShortName] = locationBasedRecord.length;
          location.push({
            locationCount: locationBasedRecord.length,
            locationName: loc.ShortName
          });
        });
        this.owedLocationName.forEach(loc => {
          const locationBasedRecord = jobDateRecord.filter(record => record.ShortName === loc.ShortName);
          let totalOwned = 0;
          locationBasedRecord.forEach(owned => {
            totalOwned = totalOwned + (owned.TotalJobAmount ? owned.TotalJobAmount : 0);
          });
          locationAmount.push({
            locationName: loc.ShortName,
            locationAmount: totalOwned
          });
        });
        let totalLocation = 0;
        location.forEach(loc => {
          totalLocation = totalLocation + loc.locationCount;
        });
        finalObj.JobDate = item;
        finalObj.AccountAmount = accountAmount;
        finalObj.CustomerCount = noOfCustomer;
        finalObj.WashesAmount = washRate / jobDateRecord.length;
        finalObj.location = location;
        finalObj.CustomerName = customerName;
        finalObj.Total = totalLocation;
        finalObj.LocationAmount = locationAmount;
        gridRecord.push(finalObj);
      }
    });
    this.ownedReportList = gridRecord;
    this.ownedReportList.forEach(item => {
      this.accountAmount = this.accountAmount + item.AccountAmount;
      this.total = this.total + item.Total;
      this.averageWashRate = this.averageWashRate + item.WashesAmount;
    });
    const data = [];
    this.uniqLocationName.forEach(loc => {
      let totalValue = 0;
      this.ownedReportList.forEach(ele => {
        ele.location.forEach(sn => {
          if (sn.locationName === loc.ShortName) {
            totalValue = totalValue + sn.locationCount;
          }
        });
      });
      this.locationTotalValue.push({
        locationName: loc.ShortName,
        totalValue
      });
    });
    this.owedLocationName.forEach(loc => {
      let totalOwnedValue = 0;
      this.ownedReportList.forEach(ele => {
        ele.LocationAmount.forEach(sn => {
          if (sn.locationName === loc.ShortName) {
            totalOwnedValue = totalOwnedValue + sn.locationAmount;
          }
        });
      });
      this.totalOwnedValue.push({
        locationName: loc.ShortName,
        totalOwnedValue
      });
    });
    this.clonedownedReportList = this.ownedReportList.map(x => Object.assign({}, x));
  }

  onMonthChange(event) {
    this.month = event;
  }
  onYearChange(event) {
    this.year = event;
  }
  onLocationChange(event) {
    this.locationId = +event;
  }

  refresh() {
    this.date = new Date();
    this.month = this.date.getMonth() + 1;
    this.year = this.date.getFullYear();
    this.locationId = localStorage.getItem('empLocationId');
    this.locationDropdownComponent.locationId = +localStorage.getItem('empLocationId')
    this.exportFiletypeComponent.type = '';
    this.yearPickerComponent.getYear();
    this.monthPickerComponent.getMonth();
    this.getMoneyOwnedReportList();
  }

  export() {
    const fileType = this.fileType !== '' ? this.fileType : '';
    if (fileType === '' || fileType === 0) {
      return;
    } else if (this.ownedReportList.length === 0) {
      return;
    }
    switch (fileType) {
      case 1: {
        this.excelService.exportAsPDFFile('MonthlyMoneyreport', 'MoneyOwnedReport_' + this.date + '.pdf');
        break;
      }
      case 2: {
        const monthlyOwedReport = this.customReport(this.clonedownedReportList);
        console.log(monthlyOwedReport, 'report');
        this.excelService.exportAsCSVFile(monthlyOwedReport, 'MoneyOwnedReport_' + this.date);
        break;
      }
      case 3: {
        const monthlyOwedReport = this.customReport(this.clonedownedReportList);
        this.excelService.exportAsExcelFile(monthlyOwedReport, 'MoneyOwnedReport_' + this.date);
        break;
      }
      default: {
        return;
      }
    }
  }

  customReport(reports) {
    const moneyOwedReport = [];
    const locOwed = [];
    reports.forEach(item => {
      item.location.forEach(loc => {
          item[loc.locationName] = loc.locationCount;
      });
      item.LocationAmount.forEach(amount => {
          item['Total Owed For' + ' ' + amount.locationName] =  this.currencyPipe.transform(amount.locationAmount, 'USD');
      });
      moneyOwedReport.push(item);
    });
    moneyOwedReport.forEach( item => {
      delete item.location;
      delete item.LocationAmount;
    });
    return moneyOwedReport;
  }

  print() {
    const body = document.getElementById('MonthlyMoneyreport').innerHTML;  // @media print{body{ width: 950px; background-color: red;} }'

    const content = '<!DOCTYPE html><html><head><title>Daily Sales Report</title><link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css"/>'
      + '<link rel = "stylesheet" type = "text/css" media = "print"/><style type = "text/css">  @media print {@page {size: landscape;margin: 0mm 5mm 0mm 5mm;}}'
      + '</style><script src="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"></script></head><body><table><thead id="header"><tr><td>'
      + '</td></tr><tr><td><div class="fixed-top" "><div style="font-size:24px;margin-right:15px;text-align:center;margin-top:15px">' + 'Monthly Money Owed Report - ' + this.month + '/' + this.year + '</div></div></td></tr></thead><tbody><tr><td><div class="upperTeethData print-table-border"><div></div><div style="position:relative; top:100px">' + body + '</div></div></td></tr><tr><td>'
      + '<div class="lowerTeethData print-table-border"><div></div><div> </div></div></td></tr><tr><td><div class="casetype print-table-border"></div>'
      + '</td></tr></tbody><tfoot><tr><td><div class="fixed-bottom border-top" id="footer">' + '<div style="font-size:14px;margin-right:15px;float:left;">' +
      '</div></div></td></tr></tfoot></table><body></html>';
    const popupWin = window.open('', '_blank', 'scrollbars=1,width:100%;height:100%');
    popupWin.document.open();
    popupWin.document.write(content);
    popupWin.document.close(); // necessary for IE >= 10
    popupWin.focus(); // necessary for IE >= 10*/
    setTimeout(() => {
      popupWin.print();
      popupWin.close();
    }, 1000);
  }

}
