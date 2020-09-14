import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-print-wash',
  templateUrl: './print-wash.component.html',
  styleUrls: ['./print-wash.component.css']
})
export class PrintWashComponent implements OnInit {
  @Input() selectedData?: any;
  constructor() { }

  ngOnInit(): void {
    console.log(this.selectedData, 'print');
  }

  print(): void {
    let printContents;
    let popupWin;
    printContents = document.getElementById('print-section').innerHTML;
    popupWin = window.open('', '_blank', 'top=0,left=0,height=100%,width=auto');
    popupWin.document.open();
    popupWin.document.write(`
      <html>
        <head>
          <title>Print tab</title>
          <style>
          //........Customized style.......
          </style>
        </head>
    <body onload="window.print();window.close()">${printContents}</body>
      </html>`
    );
    popupWin.document.close();
}

}
