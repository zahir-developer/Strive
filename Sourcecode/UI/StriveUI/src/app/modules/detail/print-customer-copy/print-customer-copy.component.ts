import { Component, OnInit, Input } from '@angular/core';
import { WashService } from 'src/app/shared/services/data-service/wash.service';
import * as _ from 'underscore';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { ToastrService } from 'ngx-toastr';
import { CodeValueService } from 'src/app/shared/common-service/code-value.service';

declare function setup_web_print(): any;

declare function sendData(data): any;

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
  minutes:any;
  hours:any;
  days:any;
  constructor(
    private wash: WashService,
    private toastr: ToastrService,
    private codeService: CodeValueService
  ) { }

  ngOnInit(): void {
    this.minutes = 0;
    this.days = 0;
    this.hours = 0;
    this.detailService = [];
    this.airfreshService = 'None';
    this.upchargeService = 'None';
    this.getServiceType();
  }

  getServiceType() {
    const serviceTypeValue = this.codeService.getCodeValueByType(ApplicationConfig.CodeValue.serviceType);
    if (serviceTypeValue.length > 0) {
      this.serviceEnum = serviceTypeValue;
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
    var TimeOut:any = new Date(this.selectedData.Details.EstimatedTimeOut) ;
    var TimeIn:any = new Date(this.selectedData.Details.TimeIn);
    var diff :any = TimeOut-TimeIn;
    this.minutes = Math.floor((diff/1000)/60);

    this.selectedData?.DetailsItem.forEach(item => {
      const serviceType = _.where(this.serviceEnum, { CodeId: item.ServiceTypeId });
      if (serviceType.length > 0) {
        if (serviceType[0].CodeValue === ApplicationConfig.Enum.ServiceType.DetailPackage || serviceType[0].CodeValue === ApplicationConfig.Enum.ServiceType.OutsideServices || serviceType[0].CodeValue === ApplicationConfig.Enum.ServiceType.AdditonalServices) {
          this.detailService.push(item.ServiceName + ' $' + item.Cost);
        } else if (serviceType[0].CodeValue.includes(ApplicationConfig.Enum.ServiceType.Upcharges) || serviceType[0].CodeValue === ApplicationConfig.Enum.ServiceType.DetailUpcharge || serviceType[0].CodeValue === ApplicationConfig.Enum.ServiceType.WashUpcharge) {
          this.upchargeService = item.ServiceName + ' $' + item.Cost;
        } else if (serviceType[0].CodeValue === ApplicationConfig.Enum.ServiceType.AirFresheners) {
          this.airfreshService = item.ServiceName;
        }
      }
    });
  }

  zebraPrint(data) {
    var content = "^XA^A1N,20^FO50,50^FD1/17/2022 9:27:37 PM^FS^A1N,20^FO320,50^FDEmail Receipt^FS^A1N,30^FO50,90^FDIn:17/01/2022, 09:27 PM^FS^A0N,30,30^FO50,130^FDOut:09:27 PM^FS^A1N,30^FO50,170^FDClient:Drive Up^FS^A1N,20^FO50,220^FDVehicle:Ambassador^FS^A1N,20^FO50,250^GB700,3,3^FS^A1N,30^FO550,90^FD 7327112021 ^FS^A1N,30^FO495,170^FD(234)235 - 3453^FS^A1N,20^FO440,220^FDAMC^FS^A1N,20^FO690,220^FDRed^FS^A1N,30^FO50,280^GB20,20,1^FS^A1N,30^FO80,280^FDMega Mammoth^FS^A1N,30^FO80,400^FDTicket Number:593201^FS^XZ";
    if (data !== "") {
      content = data;
    }
    sendData(content);
  }

  printInit()
  {
    setup_web_print();
  }


  print(): void {
    const content = document.getElementById('print-section').innerHTML;
    const printContent = '<!DOCTYPE html><html><head><link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css"/>'
      + '<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css"/><style>@media print {@page {size: portrait;}}'
      + '</style><script src="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"></script><title>Customer Copy</title></head><body>' + content + '<body></html>';

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
