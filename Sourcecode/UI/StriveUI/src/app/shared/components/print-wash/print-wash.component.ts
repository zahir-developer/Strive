import { Component, OnInit, Input } from '@angular/core';

declare function setup_web_print(): any;

declare function sendData(data): any;

@Component({
  selector: 'app-print-wash',
  templateUrl: './print-wash.component.html'
})
export class PrintWashComponent implements OnInit {
  @Input() selectedData?: any;
  @Input() module?: any;
  constructor() { }

  ngOnInit(): void {
    if (this.module !== 'detail') {
      this.selectedData.Details = this.selectedData.Washes[0];
      this.selectedData.DetailsItem = this.selectedData.WashItem;
    }
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
    const header = document.getElementById('print-header').innerHTML;
    const body = document.getElementById('print-body').innerHTML;
    const footer = document.getElementById('print-footer').innerHTML;
    const printContent = '<!DOCTYPE html><html><head><title>Print Vehicle Detail</title><link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css"/>'
      + '<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css"/><style>'
      + '</style><script src="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"></script></head><body><table><thead><tr><td><div class="row" style="width:100%;" id="header"><div class="col-12" style="font-size:14px;margin-right:15px;">' + header
      + '</div></div></td></tr></thead><tbody><tr><td>' + body + '</td></tr></tbody><tfoot><tr><td><div class="fixed-bottom border-top"  style="width:100%;" id="footer"><div style="font-size:14px;margin-right:15px;float:left;">' + footer + '</div></div></td></tr></tfoot></table><body></html>';

    const content = '<!DOCTYPE html><html><head><title>Wash Details</title><link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css"/>'
      + '<link rel = "stylesheet" type = "text/css" media = "print"/><style type = "text/css">   @media print{body{ width: 950px; background-color: red;} }'
      + '</style><script src="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"></script></head><body><table><thead id="header"><tr><td>'
      + '</td></tr><tr><td><div class="fixed-top" "><div style="font-size:14px;margin-right:15px;">' + header + '</div></div></td></tr></thead><tbody><tr><td><div class="upperTeethData print-table-border"><div></div><div style="position:relative; top:100px">' + body + '</div></div></td></tr><tr><td>'
      + '<div class="lowerTeethData print-table-border"><div></div><div> </div></div></td></tr><tr><td><div class="casetype print-table-border"></div>'
      + '</td></tr></tbody><tfoot><tr><td><div class="fixed-bottom border-top" id="footer">' + '<div style="font-size:14px;margin-right:15px;float:left;">' + footer +
      '</div></div></td></tr></tfoot></table><body></html>';
    const popupWin = window.open('', '_blank', 'scrollbars=1,width:100%;height:100%');
    popupWin.document.open();
    popupWin.document.write(content);
    popupWin.document.close(); // necessary for IE >= 10
    popupWin.focus(); // necessary for IE >= 10*/
    setTimeout(() => {
      popupWin.print();
      popupWin.close();
    }, 1000);
  }


}
