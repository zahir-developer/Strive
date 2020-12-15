import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-select-appointment-date',
  templateUrl: './select-appointment-date.component.html',
  styleUrls: ['./select-appointment-date.component.css']
})
export class SelectAppointmentDateComponent implements OnInit {
  @Output() previewAppointment = new EventEmitter();
  @Output() locationPage = new EventEmitter();
  constructor() { }

  ngOnInit(): void {
  }

  next() {
    this.previewAppointment.emit();
  }

  cancel() {
    this.locationPage.emit();
  }

}
