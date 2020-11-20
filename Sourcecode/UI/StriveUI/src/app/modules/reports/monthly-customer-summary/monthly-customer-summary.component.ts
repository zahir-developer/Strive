import { Component, OnInit, ViewChild } from '@angular/core';
import { ReportsService } from 'src/app/shared/services/data-service/reports.service';
import * as moment from 'moment';
import { ExcelService } from 'src/app/shared/services/common-service/excel.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { LocationDropdownComponent } from 'src/app/shared/components/location-dropdown/location-dropdown.component';
@Component({
  selector: 'app-monthly-customer-summary',
  templateUrl: './monthly-customer-summary.component.html',
  styleUrls: ['./monthly-customer-summary.component.css']
})
export class MonthlyCustomerSummaryComponent implements OnInit {
  @ViewChild(LocationDropdownComponent) locationDropdownComponent: LocationDropdownComponent;
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
    const locationName = this.locationDropdownComponent.locationName;
    const fileType = this.fileType !== undefined ? this.fileType : '';
    if (fileType === '' || fileType === 0) {
      return;
    } else if (this.customerSummaryReport.length === 0) {
      return;
    }
    switch (fileType) {
      case 1: {
        this.excelService.exportAsPDFFile('custSummaryExport', 'CustomerSummaryReport_' + this.selectedDate + '_' + locationName + '.pdf');
        break;
      }
      case 2: {
        const customerSummaryReport = this.customizeObj(this.customerSummaryReport);
        this.excelService.exportAsCSVFile(customerSummaryReport, 'CustomerSummaryReport_' + this.selectedDate + '_' + locationName);
        break;
      }
      case 3: {
        const customerSummaryReport = this.customizeObj(this.customerSummaryReport);
        this.excelService.exportAsExcelFile(customerSummaryReport, 'CustomerSummaryReport_' + this.selectedDate + '_' + locationName);
        break;
      }
      default: {
        return;
      }
    }
  }
  customizeObj(customerSummaryReport) {
    if (customerSummaryReport.length > 0) {
const customerSummary = customerSummaryReport.map(item => {
  return {
    Month: item.Month,
    NumberOfMembershipAccountCustomers: item.NumberOfMembershipAccounts,
    NumberOfCustomer: item.CustomerCount,
    NumberOfWashes: item.WashesCompletedCount,
    AverageNumberOfWashesPerCustomer: item.AverageNumberOfWashesPerCustomer,
    TotalNumberOfWashesPerCustomer: item.TotalNumberOfWashesPerCustomer,
    PercentageOfCustomersThatTurnedUp: item.PercentageOfCustomersThatTurnedUp
  };
});
return customerSummary;
    }
  }
}
