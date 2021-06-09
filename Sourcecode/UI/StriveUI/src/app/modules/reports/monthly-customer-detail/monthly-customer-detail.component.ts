import { Component, OnInit, ViewChild } from '@angular/core';
import { ReportsService } from 'src/app/shared/services/data-service/reports.service';
import * as moment from 'moment';
import * as _ from 'underscore';
import { CurrencyPipe, DatePipe } from '@angular/common';
import { ExcelService } from 'src/app/shared/services/common-service/excel.service';
declare var $: any;
import { NgxSpinnerService } from 'ngx-spinner';
import { LocationDropdownComponent } from 'src/app/shared/components/location-dropdown/location-dropdown.component';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { ToastrService } from 'ngx-toastr';
import { ExportFiletypeComponent } from 'src/app/shared/components/export-filetype/export-filetype.component';
import { YearPickerComponent } from 'src/app/shared/components/year-picker/year-picker.component';
import { MonthPickerComponent } from 'src/app/shared/components/month-picker/month-picker.component';

@Component({
  selector: 'app-monthly-customer-detail',
  templateUrl: './monthly-customer-detail.component.html'
})
export class MonthlyCustomerDetailComponent implements OnInit {
  locationId: any;
  @ViewChild(LocationDropdownComponent) locationDropdownComponent: LocationDropdownComponent;
  @ViewChild(ExportFiletypeComponent) exportFiletypeComponent: ExportFiletypeComponent;
  @ViewChild(YearPickerComponent) yearPickerComponent: YearPickerComponent;
  @ViewChild(MonthPickerComponent) monthPickerComponent: MonthPickerComponent;
  date = new Date();
  month: number;
  year: number;
  selectedDate: string;
  customerDetailReport = [];
  unique = [];
  finalArr = [];
  fileType: number;
  fileTypeEvent: boolean = false;
  constructor(private reportService: ReportsService, private datePipe: DatePipe,
    private toastr: ToastrService,
    private excelService: ExcelService, private spinner: NgxSpinnerService, private currencyPipe: CurrencyPipe) { }

  ngOnInit(): void {
    this.locationId = localStorage.getItem('empLocationId');
    this.setCurrentMonth();
    this.getCustomerMonthlyDetailReport();
  }
  setCurrentMonth() {
    this.month = this.date.getMonth() + 1;
    this.year = this.date.getFullYear();
  }
  getCustomerMonthlyDetailReport() {
    this.finalArr = [];
    const obj = {
      locationId: +this.locationId,
      year: this.year,
      month: this.month
    };
    this.spinner.show();
    this.reportService.getCustomerMonthlyDetailReport(obj).subscribe(data => {
      if (data.status === 'Success') {
        this.spinner.hide()
        this.selectedDate = this.month + '/' + this.year;
        const customerDetailReport = JSON.parse(data.resultData);
        this.customerDetailReport = customerDetailReport?.GetCustomerMonthlyDetailReport ?
          customerDetailReport?.GetCustomerMonthlyDetailReport : [];
        this.customizeObject();
      } else {
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');

        this.selectedDate = this.month + '/' + this.year;
      }
    }, (err) => {
      this.selectedDate = this.month + '/' + this.year;
      this.spinner.hide();
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');

    });
  }
  customizeObject() {
    if (this.customerDetailReport.length > 0) {
      const ids = this.customerDetailReport.map(item => item.ClientId);
      this.unique = _.uniq(ids);
      this.unique.forEach(id => {
        const finalArr = this.customerDetailReport.filter(item => +item.ClientId === +id);
        this.finalArr.push({ userData: finalArr, name: finalArr[0].ClientName });
      });
    }
    this.createTable();
  }
  createTable() {
    let tableStr = '';
    this.finalArr.forEach((obj) => {
      const userData = obj.userData;
      let total = 0;
      let wash = 0;
      let totalMembership =0;

      userData.forEach((o, index) => {
        tableStr += '<tr>' + (index === 0 ? '<td rowspan="' + userData.length + '">' + obj.name + '</td>' : '') + '<td>' + o.TicketNumber + '</td><td>' + o.Color +
          '</td><td>' + o.Model + '</td><td>' + this.datePipe.transform(o.JobDate, 'MM/dd/yyyy') + '</td><td>' + (o.MemberShipName !== '' ?
            ('Membership - ' + o.MemberShipName) : 'DriveUp') +
          '</td><td>' + this.currencyPipe.transform(o.MembershipPrice, 'USD') + '</td><td>' + this.currencyPipe.transform(o.TicketAmount, 'USD') + '</td></tr>';
        total += o.TicketAmount;
        totalMembership += o.MembershipPrice
        wash += 1;
      });
      tableStr += '<tr><th>Washes</th><td>' + (wash ) + '</td><td></td><td></td><td></td><td></td colspan=2><th>Customer Total</th><th>'
        + this.currencyPipe.transform(total, 'USD') + '</th><th> Difference : '+ this.currencyPipe.transform(total - totalMembership, 'USD') + '</th></tr>';
    });
    $('#customerDetail tbody').html(tableStr);
  }
  getfileType(event) {
    this.fileTypeEvent = true;

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
    this.setCurrentMonth();
    this.locationId = +localStorage.getItem('empLocationId');
    this.locationDropdownComponent.locationId = +localStorage.getItem('empLocationId');
    this.exportFiletypeComponent.type = '';
    this.yearPickerComponent.getYear();
    this.monthPickerComponent.getMonth();
    this.getCustomerMonthlyDetailReport();
  }

  export() {
    const fileType = this.fileType !== undefined ? this.fileType : '';
    const locationName = this.locationDropdownComponent.locationName;
    if (fileType === '' || fileType === 0) {
      return;
    } else if (this.customerDetailReport.length === 0) {
      return;
    }
    switch (fileType) {
      case 1: {
        this.excelService.exportAsPDFFile('custDetailExport', 'customerDetailReport_' + this.selectedDate + '_' + locationName + '.pdf');
        break;
      }
      case 2: {
        const customerDetailReport = this.customizeObj(this.customerDetailReport);
        this.excelService.exportAsCSVFile(customerDetailReport, 'customerDetailReport_' + this.selectedDate + '_' + locationName);
        break;
      }
      case 3: {
        const customerDetailReport = this.customizeObj(this.customerDetailReport);
        this.excelService.exportAsExcelFile(customerDetailReport, 'customerDetailReport_' + this.selectedDate + '_' + locationName);
        break;
      }
      default: {
        return;
      }
    }
  }
  customizeObj(customerDetailReport) {
    if (customerDetailReport.length > 0) {
      const customerDetail = customerDetailReport.map(item => {
        return {
          ClientName: item.ClientName,
          TicketNumber: item.TicketNumber,
          Color: item.Color,
          Model: item.Model,
          Date: item.JobDate,
          MembershipOrDrive: item.MemberShipName !== '' ? item.MemberShipName : 'DriveUp',
          MembershipID: item.MemberShipId,
          MembershipAmount: item.MembershipPrice,
          TicketAmount: item.TicketAmount
        };
      });
      return customerDetail;
    }
  }

  print(): void {
    const body = document.getElementById('MonthlyCustomerDetailreport').innerHTML;

    const content = '<!DOCTYPE html><html><head><title>Hourly Wash Report</title><link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css"/>'
      + '<link rel = "stylesheet" type = "text/css" media = "print"/><style type = "text/css">   @media print{body{ width: 950px; background-color: red;} }'
      + '</style><script src="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"></script></head><body><table><thead id="header"><tr><td>'
      + '</td></tr><tr><td><div class="fixed-top" "><div style="font-size:14px;margin-right:15px;">' + '</div></div></td></tr></thead><tbody><tr><td><div class="upperTeethData print-table-border"><div></div><div style="position:relative; top:100px">' + body + '</div></div></td></tr><tr><td>'
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
