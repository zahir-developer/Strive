import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';

@Component({
  selector: 'app-select-appointment-date',
  templateUrl: './select-appointment-date.component.html',
  styleUrls: ['./select-appointment-date.component.css']
})
export class SelectAppointmentDateComponent implements OnInit {
  @Output() previewAppointment = new EventEmitter();
  @Output() locationPage = new EventEmitter();
  selectedDate = new Date();
  @Input() scheduleDetailObj?: any;
  activeSlot: any;
  time = ['07:00', '07:30', '08:00', '08:30', '09:00', '09:30', '10:00', '10:30', '11:00', '11:30', '12:00', '12:30', '01:00', '01:30', '02:00', '02:30', '03:00', '03:30', '04:00', '04:30', '05:00', '05:30', '06:00', '06:30'];
  constructor() { }

  ngOnInit(): void {
  }

  next() {
    this.previewAppointment.emit();
  }

  cancel() {
    this.locationPage.emit();
  }

  selectedTimeSlot(slot, i) {
    console.log(slot, 'slot');
    this.activeSlot = i;
    this.scheduleDetailObj.Slot = slot;
  }

  getScheduleDetailsByDate(date) {
    this.scheduleDetailObj.selectedDate = date;
    this.selectedDate = date;
  }

}
