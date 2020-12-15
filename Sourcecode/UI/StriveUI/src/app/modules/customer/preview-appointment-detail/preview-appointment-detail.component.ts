import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-preview-appointment-detail',
  templateUrl: './preview-appointment-detail.component.html',
  styleUrls: ['./preview-appointment-detail.component.css']
})
export class PreviewAppointmentDetailComponent implements OnInit {
  @Output() confirmation = new EventEmitter();
  @Output() appointmentPage = new EventEmitter();
  constructor() { }

  ngOnInit(): void {
  }

  bookNow() {
    this.confirmation.emit();
  }

  cancel() {
    this.appointmentPage.emit();
  }

}
