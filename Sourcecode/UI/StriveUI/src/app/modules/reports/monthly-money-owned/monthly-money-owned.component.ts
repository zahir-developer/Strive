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
import { elementAt } from 'rxjs/operators';

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

  locationId: any;
  clonedownedReportList = [];
  totalResult: any;
  moneyOwnedReport: any;
  locationShort: any;
  headerLocationShort: any;
  newMoneyOwnedReports = [];
  selectedLocation: any;
  headerShortGroup: any;
  headerOwned: any;
  totalAmount = 0;
  totalAverage = "";
  totalAvg = 0
  membership = 0;
  driveUp = 0
  shortCharValue: any;
  ownedCharValue: any;
  monthlyOwedReport = [];





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
    } else if (this.newMoneyOwnedReports.length === 0) {
      return;
    }
    switch (fileType) {
      case 1: {
        console.log('step1');
        this.excelService.exportAsPDFFile('MonthlyMoneyreport', 'MoneyOwnedReport_' + this.date + '.pdf');
        break;
      }
      case 2: {
        console.log('step2');
        this.customReport(this.newMoneyOwnedReports);
        this.excelService.exportAsCSVFile(this.monthlyOwedReport, 'MoneyOwnedReport_' + this.date);
        break;
      }
      case 3: {
        console.log('step3');
        this.customReport(this.newMoneyOwnedReports);
        this.excelService.exportAsExcelFile(this.monthlyOwedReport, 'MoneyOwnedReport_' + this.date);
        break;
      }
      default: {
        return;
      }
    }
  }

  customReport(reports) {
    this.monthlyOwedReport = [];
    reports.forEach(ele => {
      const list = {
        FirstName: ele.FirstName,
        LastName: ele.LastName,
        Membership: ele.MembershipAmount,
        DriveUpRate: ele.TotalJobAmount,
        Total: ele.total,
        Average: ele.Average
      }
      var result1 = ele.mulipleLocWash.reduce(function (result, field, index) {
        result[ele.locationTitle[index]] = field;
        return result;
      }, {})

      var result2 = ele.ownedValue.reduce(function (result, field, index) {
        result[ele.ownedTitle[index]] = field;
        return result;
      }, {})

      let remoteJob = {
        ...list, ...result1, ...result2
      };
      this.monthlyOwedReport.push(remoteJob)
    });
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


  getMoneyOwnedReportList() {

    const date = this.year + '-' + ('0' + (this.month)).slice(-2);
    this.spinner.show();
    this.reportsService.getMonthlyMoneyOwnedReport(date, this.locationId).subscribe(res => {
      if (res.status === 'Success') {
        this.spinner.hide()
        this.totalResult = JSON.parse(res.resultData);
        this.moneyOwnedReport = this.totalResult.GetMonthlyMoneyOwedReport;

        if (this.moneyOwnedReport.Location !== null && this.moneyOwnedReport.Location.length !== 0 &&
          this.moneyOwnedReport.MoneyOwedReport !== null && this.moneyOwnedReport.MoneyOwedReport.length !== 0) {

          this.moneyOwnedReport.Location.forEach(loc => {
            this.moneyOwnedReport.MoneyOwedReport.forEach(owned => {

              if (owned.LocationId === loc.LocationId) {
                const spaceAvoid = owned.LocationName.replace(/\s+/g, ' ')
                const charSplit = spaceAvoid.split(' ')
                if (charSplit.length > 1) {
                  this.locationShort = charSplit[0].charAt(0).toUpperCase() + charSplit[1].charAt(0).toUpperCase();
                } else {
                  this.locationShort = charSplit[0].charAt(0).toUpperCase();
                }
                loc.shortName = this.locationShort;
                owned.shortName = this.locationShort;
              }
            });
          });
        } else {
          this.moneyOwnedReport.Location = []
          this.moneyOwnedReport.MoneyOwedReport = []
          this.newMoneyOwnedReports = []
          this.headerOwned = []
        }

        const locationShortName = _.pluck(this.moneyOwnedReport?.Location, 'shortName');
        //this.headerLocationShort = [...new Set(locationShortName)]
        this.headerLocationShort = [];
        this.moneyOwnedReport?.Location.forEach(s =>
          {
            
            const spaceAvoid = s.LocationName.replace('  ', ' ');
                const charSplit = spaceAvoid.split(' ');
                var locShortName = "";
                if (charSplit.length > 1) {
                  locShortName = charSplit[0].charAt(0).toUpperCase() + charSplit[1].charAt(0).toUpperCase();
                } else {
                  locShortName = charSplit[0].charAt(0).toUpperCase();
                }

                if(locShortName !== "")
                {
                  this.headerLocationShort.push(locShortName);
                }
          });

        if (this.moneyOwnedReport.MoneyOwedReport !== null && this.moneyOwnedReport.MoneyOwedReport.length !== 0) {

          this.moneyOwnedReport.MoneyOwedReport.forEach(owned => {
            owned.shortLocationValue = [];
            this.headerLocationShort.forEach(element => {
              if (owned.shortName === element) {
                owned.shortLocationValue.push(owned.WashCount)
              } else {
                owned.shortLocationValue.push(0)
              }
            });
          });

          this.newMoneyOwnedReports = [];
          this.moneyOwnedReport.MoneyOwedReport.forEach(owned => {
            var newObject = {
              "Average": owned.Average,
              "ClientId": owned.ClientId,
              "VehicleId": owned.VehicleId,
              "FirstName": owned.FirstName,
              "Barcode": owned.Barcode,
              "LastName": owned.LastName,
              "LocationId": owned.LocationId,
              "LocationName": owned.LocationName,
              "MembershipAmount": owned.MembershipAmount,
              "MoneyOwed": owned.MoneyOwed,
              "TotalJobAmount": owned.TotalJobAmount,
              "TotalWashCount": owned.TotalWashCount,
              "WashCount": owned.WashCount,
              "shortLocationValue": owned.shortLocationValue,
              "mulipleLocWash": [],
              "locationTitle": this.headerLocationShort,
              "shortName": owned.shortName
            }
            this.moneyOwnedReport.MoneyOwedReport.forEach(innerOwned => {
              if (innerOwned.VehicleId === owned.VehicleId) {
                newObject.mulipleLocWash.push(innerOwned.shortLocationValue)
              }
            });
            this.newMoneyOwnedReports.push(newObject)
          });

          this.newMoneyOwnedReports.forEach(owned => {
            owned.mulipleLocWash = owned.mulipleLocWash[0].map((x, idx) => owned.mulipleLocWash.reduce((sum, curr) => sum + curr[idx], 0));
          });

          const removeDuplicateClient = this.newMoneyOwnedReports.map(e => e.VehicleId).map((e, i, fin) => fin.indexOf(e) === i && i)
            .filter(e => this.newMoneyOwnedReports[e]).map(e => this.newMoneyOwnedReports[e])

          this.newMoneyOwnedReports = removeDuplicateClient;


          this.newMoneyOwnedReports.forEach(owned => {
            owned.ownedTitle = [];
            owned.ownedValue = [];
            owned.total = 0;
            owned.totalAmount = 0;
            if (this.headerLocationShort.length !== 0) {
              this.headerLocationShort.forEach(element => {
                owned.ownedTitle.push("Owed for " + element);
              });
            }

            if (owned.mulipleLocWash.length !== 0) {
              for (let i = 0; i < owned.mulipleLocWash.length; i++) {
                let owed = 0;

                if(owned.mulipleLocWash[i] === 1)
                owed = owned.MoneyOwed;
                else
                owed = owned.mulipleLocWash[i] * owned.Average;
                
                owned.ownedValue.push(owed);
                owned.total += owned.mulipleLocWash[i];
              }
            }
          });


          const location = +this.locationId;
          if (location) {
            const locationNameBasedonID = _.where(this.moneyOwnedReport?.Location, { LocationId: +this.locationId });
            if (locationNameBasedonID !== null && locationNameBasedonID !== undefined) {
              this.selectedLocation = locationNameBasedonID[0]?.shortName
            }
          }

          this.totalAmount = 0;
          this.totalAvg = 0;
          this.membership = 0;
          this.driveUp = 0;
          this.newMoneyOwnedReports.forEach(element => {
            const index = element.locationTitle.findIndex(ele => ele === this.selectedLocation);
            element.ownedTitle.splice(index, 1);
            element.ownedValue.splice(index, 1);
            this.headerShortGroup = element.locationTitle
            this.headerOwned = element.ownedTitle;
            this.totalAmount += element.total
            this.totalAvg += element.Average
            this.membership += element.MembershipAmount
            this.driveUp += element.TotalJobAmount
          });

          this.totalAverage = this.totalAvg.toFixed(2);
          
          let shortArray = [];
          let ownedArray = [];
          this.newMoneyOwnedReports.forEach(element => {
            shortArray.push(element.mulipleLocWash)
            const sums = shortArray[0].map((x, idx) => shortArray.reduce((sum, curr) => sum + curr[idx], 0));
            this.shortCharValue = sums
            ownedArray.push(element.ownedValue)
            const owned = ownedArray[0].map((x, idx) => ownedArray.reduce((sum, curr) => sum + curr[idx], 0));
            this.ownedCharValue = owned
          });
        }
        console.log(this.newMoneyOwnedReports, 'filter results');
      } else {
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, err => {
      console.log(err);
      this.spinner.hide();
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

}
