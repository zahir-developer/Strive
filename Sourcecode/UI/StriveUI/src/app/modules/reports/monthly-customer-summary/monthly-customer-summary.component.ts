import { Component, OnInit } from '@angular/core';
import { ReportsService } from 'src/app/shared/services/data-service/reports.service';
import * as moment from 'moment';
import { ExcelService } from 'src/app/shared/services/common-service/excel.service';
import { NgxSpinnerService } from 'ngx-spinner';
@Component({
  selector: 'app-monthly-customer-summary',
  templateUrl: './monthly-customer-summary.component.html',
  styleUrls: ['./monthly-customer-summary.component.css']
})
export class MonthlyCustomerSummaryComponent implements OnInit {
date = new Date();
customerSummaryReport = [];
originaldata = [];
locationId = +localStorage.getItem('empLocationId');
  fileType: number;
  selectedDate: any;
  page = 1;
  pageSize = 50;
  collectionSize: number;
  constructor(private reportService: ReportsService, private excelService: ExcelService, private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.setMonth();
    this.getCustomerSummaryReport();
  }
  setMonth() {
    const currentYear = this.date.getFullYear();
    this.onYearChange(currentYear);
  }
  getCustomerSummaryReport() {
    const obj = {
      locationId: this.locationId,
      date: this.date,
    };
    this.spinner.show();
    this.reportService.getCustomerSummaryReport(obj).subscribe(data => {
      this.spinner.hide();
      if (data.status === 'Success') {
        this.selectedDate = this.date;
        const customerSummaryReport = JSON.parse(data.resultData);
        if (customerSummaryReport?.GetCustomerSummaryReport !== null) {
          const sumReport = customerSummaryReport?.GetCustomerSummaryReport ?
          customerSummaryReport?.GetCustomerSummaryReport : [];
          this.customerSummaryReport = sumReport;
          this.originaldata =  sumReport;
          this.collectionSize = Math.ceil(this.customerSummaryReport.length / this.pageSize) * 10;
        }
      }
    }, (err) => {
      this.spinner.hide();
    });
  }
  onYearChange(event) {
    this.date = event;
  }
  onLocationChange(event) {
    this.locationId = +event;
  }
  getfileType(event) {
    this.fileType = +event.target.value;
  }
  export() {
    const fileType = this.fileType !== undefined ? this.fileType : '';
    if (fileType === '' || fileType === 0) {
      return;
    } else if (this.customerSummaryReport.length === 0) {
      return;
    }
    switch (fileType) {
      case 1: {
        this.excelService.exportAsPDFFile('custSummaryExport', 'CustomerSummaryReport_' + this.selectedDate + '.pdf');
        break;
      }
      case 2: {
        this.excelService.exportAsCSVFile(this.customerSummaryReport, 'CustomerSummaryReport_' + this.selectedDate);
        break;
      }
      case 3: {
        this.excelService.exportAsExcelFile(this.customerSummaryReport, 'CustomerSummaryReport_' + this.selectedDate);
        break;
      }
      default: {
        return;
      }
    }
  }
}
