import { Component, OnInit } from '@angular/core';
import { ReportsService } from 'src/app/shared/services/data-service/reports.service';
import * as moment from 'moment';
import * as _ from 'underscore';
@Component({
  selector: 'app-monthly-customer-detail',
  templateUrl: './monthly-customer-detail.component.html',
  styleUrls: ['./monthly-customer-detail.component.css']
})
export class MonthlyCustomerDetailComponent implements OnInit {
  locationId: any;
  date = new Date();
  month: number;
  year: number;
  selectedDate: string;
  customerDetailReport = [];
  unique = [];
  constructor(private reportService: ReportsService) { }

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
    const obj = {
      locationId: +this.locationId,
      year: this.year,
      month: this.month
    };
    this.reportService.getCustomerMonthlyDetailReport(obj).subscribe(data => {
      console.log(data);
      if (data.status === 'Success') {
        this.selectedDate = moment(this.date).format('YYYY');
        const customerDetailReport = JSON.parse(data.resultData);
        this.customerDetailReport = customerDetailReport?.GetCustomerMonthlyDetailReport ?
          customerDetailReport?.GetCustomerMonthlyDetailReport : [];
        this.customizeObject();
      }
    });
  }
  customizeObject() {
    if (this.customerDetailReport.length > 0) {
      const ids = this.customerDetailReport.map(item => item.ClientId);
      this.unique = _.uniq(ids);
      console.log(this.unique);
    }
  }
  getfileType(event) {

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
}
