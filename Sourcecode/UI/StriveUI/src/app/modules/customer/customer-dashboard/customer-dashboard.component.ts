import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { CustomerService } from 'src/app/shared/services/data-service/customer.service';
import * as moment from 'moment';

@Component({
  selector: 'app-customer-dashboard',
  templateUrl: './customer-dashboard.component.html',
  styleUrls: ['./customer-dashboard.component.css']
})
export class CustomerDashboardComponent implements OnInit {
  @Output() selectServcie = new EventEmitter();
  vechicleList: any = [];
  @Input() scheduleDetailObj?: any;
  serviceList: any = [];
  constructor(private customerService: CustomerService) { }

  ngOnInit(): void {
    this.getDailySalesReport();
    this.getVehicleListByClientId();
  }

  getDailySalesReport() {
    const finalObj = {
      date: moment(new Date()).format('MM/DD/YYYY'),
      locationId: 0
    };
    this.customerService.getDailySalesReport(finalObj).subscribe( res => {
      if (res.status === 'Success') {
        const sales = JSON.parse(res.resultData);
        this.serviceList = sales.GetDailySalesReport;
        console.log(sales, 'customer');
      }
    });
  }

  schedule(vechicle) {
    this.scheduleDetailObj.vechicleDetail = vechicle;
    this.selectServcie.emit(vechicle);
  }

  getVehicleListByClientId() {
    this.customerService.getVehicleByClientId(115).subscribe( res => {
      if (res.status === 'Success') {
        const vechicle = JSON.parse(res.resultData);
        this.vechicleList = vechicle.Status;
        console.log(vechicle, 'vechicle');
      }
    });
  }

}
