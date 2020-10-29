import { Component, OnInit } from '@angular/core';
import { ReportsService } from 'src/app/shared/services/data-service/reports.service';
import * as moment from 'moment';
@Component({
  selector: 'app-monthly-customer-summary',
  templateUrl: './monthly-customer-summary.component.html',
  styleUrls: ['./monthly-customer-summary.component.css']
})
export class MonthlyCustomerSummaryComponent implements OnInit {
fromDate = new Date();
endDate = new Date();
customerSummaryReport = [];
originaldata = [];
locationId = +localStorage.getItem('empLocationId');
  constructor(private reportService: ReportsService) { }

  ngOnInit(): void {
    this.setMonth();
    this.getCustomerSummaryReport();
  }
  setMonth() {
    const currentMonth = this.fromDate.getMonth() + 1;
    this.onMonthChange(currentMonth);
  }
  getCustomerSummaryReport() {
    const obj = {
      locationId: this.locationId,
      date: this.fromDate,
    };
    this.reportService.getCustomerSummaryReport(obj).subscribe(data => {
      if (data.status === 'Success') {
        const customerSummaryReport = JSON.parse(data.resultData);
        if (customerSummaryReport?.GetCustomerSummaryReport !== null) {
          this.customerSummaryReport = customerSummaryReport?.GetCustomerSummaryReport ?
          customerSummaryReport?.GetCustomerSummaryReport : [];
          this.originaldata =  customerSummaryReport?.GetCustomerSummaryReport;
        }
      }
      console.log(data, 'customer');
    });
  }
  onMonthChange(event) {
    this.fromDate.setMonth(event - 1);
    this.endDate.setMonth(event - 1);
    this.fromDate = moment(this.fromDate).startOf('month').toDate();
    this.endDate = moment(this.endDate).endOf('month').toDate();
  }
  onYearChange(event) {
    this.fromDate.setFullYear(event);
    this.endDate.setFullYear(event);
  }
  onLocationChange(event) {
    this.locationId = +event;
  }
}
