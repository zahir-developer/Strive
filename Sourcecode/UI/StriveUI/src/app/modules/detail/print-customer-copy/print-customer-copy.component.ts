import { Component, OnInit, Input } from '@angular/core';
import { WashService } from 'src/app/shared/services/data-service/wash.service';
import * as _ from 'underscore';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { ToastrService } from 'ngx-toastr';
import { CodeValueService } from 'src/app/shared/common-service/code-value.service';

@Component({
  selector: 'app-print-customer-copy',
  templateUrl: './print-customer-copy.component.html'
})
export class PrintCustomerCopyComponent implements OnInit {
  @Input() selectedData?: any;
  serviceEnum: any;
  detailService: any;
  airfreshService: any;
  upchargeService: any;
  constructor(
    private wash: WashService,
    private toastr: ToastrService,
    private codeService: CodeValueService
  ) { }

  ngOnInit(): void {
    this.detailService = 'None';
    this.airfreshService = 'None';
    this.upchargeService = 'None';
    this.getServiceType();
  }

  getServiceType() {
    const serviceTypeValue = this.codeService.getCodeValueByType(ApplicationConfig.CodeValue.serviceType);
    if (serviceTypeValue.length > 0) {
      this.serviceEnum = serviceTypeValue.Codes;
      this.bindingService();
    } else {
      this.wash.getServiceType('SERVICETYPE').subscribe(data => {
        if (data.status === 'Success') {
          const sType = JSON.parse(data.resultData);
          this.serviceEnum = sType.Codes;
          this.bindingService();
        }
      }, (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
    }
  }

  bindingService() {
    this.selectedData?.DetailsItem.forEach(item => {
      const serviceType = _.where(this.serviceEnum, { CodeId: item.ServiceTypeId });
      if (serviceType.length > 0) {
        if (serviceType[0].CodeValue === ApplicationConfig.Enum.ServiceType.DetailPackage) {
          this.detailService = item.ServiceName + '$' + item.Cost;
        } else if (serviceType[0].CodeValue === ApplicationConfig.Enum.ServiceType.Upcharges) {
          this.upchargeService = item.ServiceName + '$' + item.Cost;
        } else if (serviceType[0].CodeValue === ApplicationConfig.Enum.ServiceType.AirFresheners) {
          this.airfreshService = item.ServiceName;
        }
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
