import { Component, OnInit, Input } from '@angular/core';
import { WashService } from 'src/app/shared/services/data-service/wash.service';
import * as _ from 'underscore';

@Component({
  selector: 'app-print-customer-copy',
  templateUrl: './print-customer-copy.component.html',
  styleUrls: ['./print-customer-copy.component.css']
})
export class PrintCustomerCopyComponent implements OnInit {
  @Input() selectedData?: any;
  serviceEnum: any;
  detailService: any;
  airfreshService: any;
  upchargeService: any;
  constructor(
    private wash: WashService,
  ) { }

  ngOnInit(): void {
    this.detailService = 'None';
    this.airfreshService = 'None';
    this.upchargeService = 'None';
    this.getServiceType();
  }

  getServiceType() {
    this.wash.getServiceType('SERVICETYPE').subscribe(data => {
      if (data.status === 'Success') {
        const sType = JSON.parse(data.resultData);
        this.serviceEnum = sType.Codes;
        this.selectedData.DetailsItem.forEach(item => {
          const serviceType = _.where(this.serviceEnum, { CodeId: item.ServiceTypeId });
          if (serviceType.length > 0) {
            if (serviceType[0].CodeValue === 'Details') {
              this.detailService = item.ServiceName + '$' + item.Cost;
            } else if (serviceType[0].CodeValue === 'Upcharges') {
              this.upchargeService = item.ServiceName + '$' + item.Cost;
            } else if (serviceType[0].CodeValue === 'Air Fresheners') {
              this.airfreshService = item.ServiceName;
            }
          }
        });
      }
    });
  }

  print(): void {
    const content = document.getElementById('print-section').innerHTML;
    const printContent = '<!DOCTYPE html><html><head><link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css"/>'
    + '<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css"/><style>@media print {@page {size: portrait;}}'
    + '</style><script src="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"></script></head><body>' + content + '<body></html>';

    const popupWin = window.open('', '_blank', 'scrollbars=1,width:100%;height:100%');
    popupWin.document.open();
    popupWin.document.write(printContent);
    popupWin.document.close(); // necessary for IE >= 10
    popupWin.focus(); // necessary for IE >= 10*/
    setTimeout(() => {
      popupWin.print();
      popupWin.close();
    }, 2000);
  }

}
