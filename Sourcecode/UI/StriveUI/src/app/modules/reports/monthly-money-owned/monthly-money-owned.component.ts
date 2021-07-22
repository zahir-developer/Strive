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
  uniqLocationName: any = [];
  locationName: any = [];
  locationTotalValue = [];
  totalOwnedValue = [];
  locationId: any;
  owedLocationName = [];
  clonedownedReportList = [];

  // new object declare

  totalResult: any;
  moneyOwnedReport: any;
  locationGroup = [];
  locationShortName: any;
  filterLocation = [];
  headerLocationRow = [];
  headerLocationOwed = [];
  allOwned: any;
  selectedLocation: any;
  totalCal = 0;
  averageCal = 0;
  membershipCal = 0;
  driveupCal = 0;
  shortCharValue: any;
  ownedCharValue: any;



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
        item['Total Owed For' + ' ' + amount.locationName] = this.currencyPipe.transform(amount.locationAmount, 'USD');
      });
      moneyOwedReport.push(item);
    });
    moneyOwedReport.forEach(item => {
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


  getMoneyOwnedReportList() {

    const date = this.year + '-' + ('0' + (this.month)).slice(-2);
    this.spinner.show();
    this.reportsService.getMonthlyMoneyOwnedReport(date, this.locationId).subscribe(res => {
      if (res.status === 'Success') {
        this.spinner.hide()
        this.totalResult = JSON.parse(res.resultData);
        console.log(this.totalResult);
        this.moneyOwnedReport = this.totalResult.GetMonthlyMoneyOwedReport;
        console.log(this.moneyOwnedReport, 'real response');


        this.headerLocationRow = [];
        this.headerLocationOwed = [];
        let shortLetter = '';
        this.moneyOwnedReport?.Location.forEach(loc => {
          const list = loc.LocationName.replace(/\s+/g, ' ')
          const ShortName = list.split(' ')
          if (ShortName.length > 1) {
            shortLetter = ShortName[0].charAt(0).toUpperCase() + ShortName[1].charAt(0).toUpperCase();
          } else {
            shortLetter = ShortName[0].charAt(0).toUpperCase();
          }
          loc.shortName = shortLetter;

          this.headerLocationRow.push(shortLetter);

          this.headerLocationOwed.push("Total Owned For" + " " + shortLetter);
        });


        this.moneyOwnedReport?.Client.forEach(client => {

          client.moneyOwedData = [];
          client.joblocationCount = [];
          client.OwedlocationAmount = [];
          client.MembershipAmount = 0;
          client.TotalWashCount = 0;
          client.Average = 0;
          client.TotalJobAmount = 0;

          const moneyOwed = this.moneyOwnedReport.MoneyOwedReport.filter(s => s.ClientId === client.ClientId);

          
          moneyOwed.forEach(item => {
           const filterLocation =  this.moneyOwnedReport.Location.filter(list =>list.locationId !== item.locationId)
          });
          

          if (moneyOwed?.length > 0) {
            client.MembershipAmount = moneyOwed[0]?.MembershipAmount;
            client.TotalWashCount = moneyOwed[0]?.TotalWashCount;
            client.Average = moneyOwed[0]?.Average;
            client.TotalJobAmount = moneyOwed[0]?.TotalJobAmount;
          }

          client.joblocationCount = [];
          client.OwedlocationAmount = [];
          moneyOwed.forEach(moneyOwed => {
            
            for(let loc of this.moneyOwnedReport.Location)
            {
              if (moneyOwed.locationId === loc.locationId) {
                console.log(moneyOwed.locationId === loc.locationId);
                //Wash Count based on location
                client.joblocationCount.push(moneyOwed.WashCount);
                
                //Money Owed based on location
                client.OwedlocationAmount.push(moneyOwed.MoneyOwed);
                break;
              }
              else {
                client.joblocationCount.push(0);
                 //Money Owed based on location
                client.OwedlocationAmount.push(0);
                break;
              }
            }
          });

          client.joblocationCount

        });
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
