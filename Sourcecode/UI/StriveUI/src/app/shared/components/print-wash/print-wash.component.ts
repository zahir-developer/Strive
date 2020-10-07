import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-print-wash',
  templateUrl: './print-wash.component.html',
  styleUrls: ['./print-wash.component.css']
})
export class PrintWashComponent implements OnInit {
  @Input() selectedData?: any;
  @Input() module?: any;
  constructor() { }

  ngOnInit(): void {
    console.log(this.selectedData, 'print');
    if (this.module !== 'detail') {
      this.selectedData.Details = this.selectedData.Washes[0];
      this.selectedData.DetailsItem = this.selectedData.WashItem;
    }
  }

  print(): void {
    const header = document.getElementById('print-header').innerHTML;
    const body = document.getElementById('print-body').innerHTML;
    const footer = document.getElementById('print-footer').innerHTML;
    const printContent = '<!DOCTYPE html><html><head><title>Print Vehicle Detail</title><link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css"/>'
      + '<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css"/><style>@media print {@page {size: portrait;}}'
      + '</style><script src="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"></script></head><body><table><thead><tr><td><div class="fixed-top" style="width:100%;" id="header"><div style="font-size:14px;margin-right:15px;">' + header +
      + '</div></div></td></tr></thead><tbody><tr><td>' + body + '</td></tr></tbody><tfoot><tr><td><div class="fixed-bottom border-top"  style="width:100%;" id="footer"><div style="font-size:14px;margin-right:15px;float:left;">' + footer + '</div></div></td></tr></tfoot></table><body></html>';

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
