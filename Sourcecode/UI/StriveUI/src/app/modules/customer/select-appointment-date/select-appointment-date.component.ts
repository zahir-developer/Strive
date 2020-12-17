import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { CustomerService } from 'src/app/shared/services/data-service/customer.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-select-appointment-date',
  templateUrl: './select-appointment-date.component.html',
  styleUrls: ['./select-appointment-date.component.css']
})
export class SelectAppointmentDateComponent implements OnInit {
  @Output() previewAppointment = new EventEmitter();
  @Output() locationPage = new EventEmitter();
  selectedDate: any; // = new Date();
  @Input() scheduleDetailObj?: any;
  activeSlot: any;
  timeSlot: any = [];
  WashTimeMinutes = 0;
  time = ['07:00', '07:30', '08:00', '08:30', '09:00', '09:30', '10:00', '10:30', '11:00', '11:30', '12:00', '12:30', '01:00', '01:30', '02:00', '02:30', '03:00', '03:30', '04:00', '04:30', '05:00', '05:30', '06:00', '06:30'];
  constructor(
    private customerService: CustomerService,
    private datePipe: DatePipe
  ) { }

  ngOnInit(): void {
    this.selectedDate = new Date();
    this.patchAppoimentValue();
  }

  next() {
    this.scheduleDetailObj.selectedDate = this.selectedDate;
    this.previewAppointment.emit();
  }

  cancel() {
    this.locationPage.emit();
  }

  getAvailablilityScheduleTime() {
    const finalObj = {
      locationId: this.scheduleDetailObj.locationObj.LocationId,
      date: this.selectedDate
    };
    this.customerService.getAvailablilityScheduleTime(finalObj).subscribe(res => {
      if (res.status === 'Success') {
        const slot = JSON.parse(res.resultData);
        this.timeSlot = slot.GetTimeInDetails.reduce((unique, o) => {
          if (!unique.some(obj => obj.TimeIn === o.TimeIn)) {
            unique.push(o);
          }
          return unique;
        }, []);
        console.log(this.timeSlot, 'slot');
      }
    });
  }

  patchAppoimentValue() {
    if (this.scheduleDetailObj.Slot !== undefined) {
      this.activeSlot = this.scheduleDetailObj.Slot.TimeIn;
      this.selectedDate = this.scheduleDetailObj.selectedDate;
    }
  }

  selectedTimeSlot(slot, i) {
    this.activeSlot = slot.TimeIn;
    this.scheduleDetailObj.Slot = slot;
    const time = slot.TimeIn.split(':');
    const hours = time[0];
    const minutes = time[1];
    this.selectedDate.setHours(hours);
    this.selectedDate.setMinutes(minutes);
    this.selectedDate.setSeconds('00');
    this.scheduleDetailObj.InTime = this.selectedDate;
    const outTime = this.selectedDate.setMinutes(this.selectedDate.getMinutes() + this.WashTimeMinutes);
    this.scheduleDetailObj.OutTime = this.datePipe.transform(outTime, 'MM/dd/yyyy HH:mm');
  }

  getScheduleDetailsByDate(date) {
    this.selectedDate = date;
    this.getAvailablilityScheduleTime();
    this.getWashTimeByLocationId();
  }

  getWashTimeByLocationId() {
    const locationID = this.scheduleDetailObj.locationObj.LocationId;
    this.customerService.getWashTimeByLocationId(locationID).subscribe(res => {
      if (res.status === 'Success') {
        const washTime = JSON.parse(res.resultData);
        this.WashTimeMinutes = washTime.Location.Location.WashTimeMinutes;
      }
    });
  }

}
