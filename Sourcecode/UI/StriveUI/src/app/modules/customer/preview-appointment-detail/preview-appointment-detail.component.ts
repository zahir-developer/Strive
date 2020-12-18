import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';

@Component({
  selector: 'app-preview-appointment-detail',
  templateUrl: './preview-appointment-detail.component.html',
  styleUrls: ['./preview-appointment-detail.component.css']
})
export class PreviewAppointmentDetailComponent implements OnInit {
  @Output() confirmation = new EventEmitter();
  @Output() appointmentPage = new EventEmitter();
  @Input() scheduleDetailObj?: any;
  constructor() { }

  ngOnInit(): void {
    console.log(this.scheduleDetailObj, 'schedule');
  }

  bookNow() {
    this.confirmation.emit();
  }

  cancel() {
    this.appointmentPage.emit();
  }

}
