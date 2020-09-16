import { Component, OnInit } from '@angular/core';
import { TimeClockMaintenanceService } from 'src/app/shared/services/data-service/time-clock-maintenance.service';
import * as _ from 'underscore';
import * as moment from 'moment';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-time-clock-week',
  templateUrl: './time-clock-week.component.html',
  styleUrls: ['./time-clock-week.component.css']
})
export class TimeClockWeekComponent implements OnInit {
  totalWeekDetail: any = {
    CollisionAmount: 0,
    DetailAmount: 0,
    DetailRate: 0,
    GrandTotal: 0,
    OverTimeHours: 0,
    OverTimePay: 0,
    TotalDetailHours: 0,
    TotalWashHours: 0,
    WashAmount: 0,
    WashRate: 0
  };
  timeClockList: any = [];
  weekDays = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
  roleList = [];
  constructor(
    public timeClockMaintenanceService: TimeClockMaintenanceService,
    private datePipe: DatePipe
  ) { }

  ngOnInit(): void {
    this.getAllRoles();
  }

  getTimeClockWeekDetails() {
    const employeeId = 1;
    const locationId = 1;
    const startDate = '2020-08-09';
    const endDate = '2020-08-15';
    const inputParams = {
      employeeId,
      locationId,
      startDate,
      endDate
    };
    this.timeClockMaintenanceService.getTimeClockWeekDetails(inputParams).subscribe(res => {
      if (res.status === 'Success') {
        const weekDetails = JSON.parse(res.resultData);
        console.log(weekDetails, 'weekDetails');
        if (weekDetails.Result.TimeClockWeek !== null) {
          this.totalWeekDetail = weekDetails.Result.TimeClockWeek;
        }
        if (weekDetails.Result.TimeClock !== null) {
          this.weekDays.forEach(day => {
            const days = _.where(weekDetails.Result.TimeClock, { Day: day });
            const dayDetails = [];
            if (days.length > 0) {
              days.forEach(item => {
                dayDetails.push({
                  EventDate: item.EventDate,
                  InTime: item.InTime,
                  OutTime: item.OutTime,
                  RoleId: item.RoleId,
                  TimeClockId: item.TimeClockId,
                  TotalHours: this.datePipe.transform(item.TotalHours, 'HH:mm')
                });
              });
            }
            this.timeClockList.push({
              day,
              checkInDetail: dayDetails
            });
          });
          console.log(this.timeClockList, 'timeclocklist');
        }
      }
    });
  }

  getAllRoles() {
    this.timeClockMaintenanceService.getAllRoles('EMPLOYEEROLE').subscribe(res => {
      if (res.status === 'Success') {
        const roles = JSON.parse(res.resultData);
        console.log(roles);
        this.roleList = roles.Codes;
        this.getTimeClockWeekDetails();
      }
    });
  }

  addTimeList(week) {
    console.log(week);
    week.checkInDetail.push({
      EventDate: '',
      InTime: '',
      OutTime: '',
      RoleId: '',
      TimeClockId: 0,
      TotalHours: ''
    });
  }

  saveWeeklyhours() {
    console.log(this.timeClockList, 'finalobj');
    const weekDetailObj = [];
    this.timeClockList.forEach(item => {
      item.checkInDetail.forEach(time => {
        weekDetailObj.push({
          timeClockId: time.TimeClockId,
          employeeId: 1,
          locationId: 1,
          roleId: +time.RoleId,
          eventDate: time.EventDate,
          inTime: this.datePipe.transform(time.InTime, 'HH:mm'),
          outTime: this.datePipe.transform(time.OutTime, 'HH:mm'),
          eventType: 1,
          updatedFrom: 'string',
          status: true,
          comments: 'string',
          isActive: true,
          isDeleted: false
        });
      });
    });
    const finalObj = {
      timeClock: weekDetailObj
    };
    console.log(finalObj, 'finalObj');
    // this.timeClockMaintenanceService.saveTimeClock(finalObj).subscribe(res => {
    //   if (res.status === 'Success') {

    //   }
    // });
  }

  inTime(event) {
    console.log(event, 'intime');
  }

  outTime(event, currentTime) {
    console.log(event, currentTime);
    const inTime = currentTime.InTime;
    const outTime = currentTime.OutTime;
    const totalHours = this.datePipe.transform((outTime - inTime), 'HH:mm');
    currentTime.TotalHours = totalHours;
  }

}
