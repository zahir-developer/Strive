import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { CustomerService } from 'src/app/shared/services/data-service/customer.service';
import * as moment from 'moment';

@Component({
  selector: 'app-customer-dashboard',
  templateUrl: './customer-dashboard.component.html',
  styleUrls: ['./customer-dashboard.component.css']
})
export class CustomerDashboardComponent implements OnInit {
  @Output() selectServcie = new EventEmitter();
  constructor(private customerService: CustomerService) { }

  ngOnInit(): void {
    this.getDailySalesReport();
  }

  getDailySalesReport() {
    const finalObj = {
      date: moment(new Date()).format('MM/DD/YYYY'),
      locationId: 0
    };
    this.customerService.getDailySalesReport(finalObj).subscribe( res => {
      if (res.status === 'Success') {
        const sales = JSON.parse(res.resultData);
        console.log(sales, 'customer');
      }
    });
  }

  schedule() {
    this.selectServcie.emit();
  }

}
