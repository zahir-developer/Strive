import { Component, OnInit, ViewChild } from '@angular/core';
import { ReportsService } from 'src/app/shared/services/data-service/reports.service';
import * as moment from 'moment';
import * as _ from 'underscore';
import { DatePipe } from '@angular/common';
import { ExcelService } from 'src/app/shared/services/common-service/excel.service';
declare var $: any;
import { NgxSpinnerService } from 'ngx-spinner';
import { LocationDropdownComponent } from 'src/app/shared/components/location-dropdown/location-dropdown.component';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-monthly-customer-detail',
  templateUrl: './monthly-customer-detail.component.html',
  styleUrls: ['./monthly-customer-detail.component.css']
})
export class MonthlyCustomerDetailComponent implements OnInit {
  locationId: any;
  @ViewChild(LocationDropdownComponent) locationDropdownComponent: LocationDropdownComponent;
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
    private toastr : ToastrService,
              private excelService: ExcelService, private spinner: NgxSpinnerService) { }

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
      this.spinner.hide();
      if (data.status === 'Success') {
        this.selectedDate = this.month + '/' + this.year;
        const customerDetailReport = JSON.parse(data.resultData);
        this.customerDetailReport = customerDetailReport?.GetCustomerMonthlyDetailReport ?
          customerDetailReport?.GetCustomerMonthlyDetailReport : [];
        this.customizeObject();
      } else {
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

      userData.forEach((o, index) => {
        tableStr += '<tr>' + (index === 0 ? '<td rowspan="' + userData.length + '">' + obj.name + '</td>' : '') + '<td>' + o.TicketNumber + '</td><td>' + o.Color +
          '</td><td>' + o.Model + '</td><td>' + this.datePipe.transform(o.JobDate, 'MM/dd/yyyy') + '</td><td>' + (o.MemberShipName !== '' ?
          ('Membership - ' + o.MemberShipName) : 'DriveUp')  +
          '</td><td>' + o.MembershipPrice.toFixed(2) + '</td><td>' + o.TicketAmount.toFixed(2) + '</td></tr>';
        total += o.TicketAmount;
        wash += index;
      });
      tableStr += '<tr><th>Washes</th><td>' + (wash + 1) + '</td><td></td><td></td><td></td><td></td colspan=2><th>Customer Total</th><th>'
      + total.toFixed(2) +'</th><th> Difference : 10</th></tr>';
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
        this.excelService.exportAsPDFFile('custDetailExport', 'customerDetailReport_' + this.selectedDate  +  '_' + locationName + '.pdf');
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
    MembershipOrDrive: item.MemberShipName !== '' ? item.MemberShipName: 'DriveUp',
    MembershipID: item.MemberShipId,
    MembershipAmount: item.MembershipPrice,
    TicketAmount: item.TicketAmount
  };
});
return customerDetail;
    }
  }
}
