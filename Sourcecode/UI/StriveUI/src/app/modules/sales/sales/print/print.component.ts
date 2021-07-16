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
  time: any;
  constructor(private activeModal: NgbActiveModal) { }

  ngOnInit(): void {
    this.addressLine1 =  localStorage.getItem('streetGroup');
    this.city = localStorage.getItem('cityGroup');
    this.today = new Date();
    this.time = this.today.getHours() + ":" + this.today.getMinutes() + ":" + this.today.getSeconds();
  }
cancel() {
  this.activeModal.close();
}
}
