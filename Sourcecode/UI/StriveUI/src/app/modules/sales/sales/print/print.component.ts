import { Component, OnInit, Input, ViewEncapsulation } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-print',
  templateUrl: './print.component.html',
  styleUrls: ['./print.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class PrintComponent implements OnInit {
  addressLine1: any;
  addressLine2: any;
  city: any;
  phone: any;
  invoiceDate: any;
  invoiceTime: any;
  @Input() itemList: any = []; 
  @Input() printTicketNumber: any;
  @Input() cashBack: any;
  today = new Date();
  constructor(private activeModal: NgbActiveModal) { }

  ngOnInit(): void {
    this.addressLine1 = 'Old Milton Mammoth Detail Salon';
    this.addressLine2 = '2145, Old Milton Parkaway';
    this.city = 'Alpharetta, GA 30004';
    this.phone = '(770)521-0599';
    this.invoiceDate = '9/18/2020';
    this.invoiceTime = '9:26 AM';
  }
cancel() {
  this.activeModal.close();
}
}
