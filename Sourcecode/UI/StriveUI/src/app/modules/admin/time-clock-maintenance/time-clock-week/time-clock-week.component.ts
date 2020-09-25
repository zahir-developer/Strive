import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { TimeClockMaintenanceService } from 'src/app/shared/services/data-service/time-clock-maintenance.service';
import * as _ from 'underscore';
import * as moment from 'moment';
import { DatePipe } from '@angular/common';
import { ToastrService } from 'ngx-toastr';

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
  @Input() empClockInObj?: any;
  @Output() cancelCheckInPage = new EventEmitter();
  weekStartDate: any;
  weekLastDate: any;
  constructor(
    public timeClockMaintenanceService: TimeClockMaintenanceService,
    private datePipe: DatePipe,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    console.log(this.empClockInObj, 'empObj');
    this.weekStartDate = new Date(this.empClockInObj.startDate);
    this.weekLastDate = this.empClockInObj.endDate;
    this.getAllRoles();
  }

  getTimeClockWeekDetails() {
    const employeeId = this.empClockInObj.employeeID;
    const locationId = this.empClockInObj.locationId;
    const startDate = this.datePipe.transform(this.empClockInObj.startDate, 'yyyy-MM-dd');
    const endDate = this.datePipe.transform(this.empClockInObj.endDate, 'yyyy-MM-dd');
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
                  TotalHours: this.datePipe.transform(item.TotalHours, 'H'),
                  employeeId: this.empClockInObj.employeeID,
                  locationId: this.empClockInObj.locationId
                });
              });
            }
            const weeklyDays = new Date(this.weekStartDate.setDate(this.weekStartDate.getDate() + 1));
            const tempDate = new Date(weeklyDays.setDate(weeklyDays.getDate() - 1));
            this.timeClockList.push({
              day,
              date: this.datePipe.transform(tempDate, 'yyyy-MM-dd'),
              checkInDetail: dayDetails
            });
          });
          console.log(this.timeClockList, 'timeclocklist');
        } else {
          // const daysCount = this.empClockInObj.endDate.getDate() - this.empClockInObj.startDate.getDate();
          console.log("daysCount", 'day');
          const weekDetails = [];
          weekDetails.push({
            day: this.datePipe.transform(this.weekStartDate, 'EEEE'),
            date: this.datePipe.transform(this.weekStartDate, 'yyyy-MM-dd')
          });
          for (let i = 0; i < 6; i++) {
            const weeklyDays = new Date(this.weekStartDate.setDate(this.weekStartDate.getDate() + 1));
            weekDetails.push({
              day: this.datePipe.transform(weeklyDays, 'EEEE'),
              date: this.datePipe.transform(weeklyDays, 'yyyy-MM-dd')
            });
          }
          weekDetails.forEach(item => {
            const checkIn = [];
            item.checkInDetail = checkIn;
          });
          console.log(weekDetails, 'weekDetails');
          this.timeClockList = weekDetails;
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
      EventDate: week.date ? week.date : week.checkInDetail[0].EventDate,
      InTime: '',
      OutTime: '',
      RoleId: '',
      TimeClockId: 0,
      TotalHours: '',
      employeeId: this.empClockInObj.employeeID,
      locationId: this.empClockInObj.locationId
    });
  }

  saveWeeklyhours() {
    console.log(this.timeClockList, 'finalobj');
    const weekDetailObj = [];
    this.timeClockList.forEach(item => {
      item.checkInDetail.forEach(time => {
        weekDetailObj.push({
          timeClockId: time.TimeClockId,
          employeeId: time.employeeId,
          locationId: time.locationId,
          roleId: (time.RoleId !== null && time.RoleId !== '') ? +time.RoleId : null,
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
    this.timeClockMaintenanceService.saveTimeClock(finalObj).subscribe(res => {
      if (res.status === 'Success') {
        this.toastr.success('Time Clock record added successfully!!', 'Success!');
        this.backToTimeClockPage();
      }
    });
  }

  inTime(event) {
    console.log(event, 'intime');
  }

  outTime(event, currentTime) {
    console.log(event, currentTime);
    const inTime = currentTime.InTime;
    const outTime = currentTime.OutTime;
    const hours = new Date();
    const hourDifference = hours.setHours(outTime.getHours() - inTime.getHours());
    const totalHours = this.datePipe.transform(hourDifference, 'H');
    currentTime.TotalHours = totalHours;
  }

  backToTimeClockPage() {
    this.cancelCheckInPage.emit();
  }

  totalHoursCalculation(data) {
    let washHour = 0;
    let detailHour = 0;
    this.timeClockList.forEach( item => {
      item.checkInDetail.forEach( checkIn => {
        if (this.roleList.filter(role => +role.CodeId === +checkIn.RoleId)[0].CodeValue === 'Wash') {
          washHour +=  +checkIn.TotalHours;
        } else if (this.roleList.filter(role => +role.CodeId === +checkIn.RoleId)[0].CodeValue === 'Detailer') {
          detailHour +=  +checkIn.TotalHours;
        }
      });
    });
    this.totalWeekDetail.TotalDetailHours = detailHour;
    this.totalWeekDetail.TotalWashHours = washHour;
    this.totalWeekDetail.WashAmount = this.totalWeekDetail.TotalWashHours * this.totalWeekDetail.WashRate;
    this.totalWeekDetail.DetailAmount = this.totalWeekDetail.TotalDetailHours * this.totalWeekDetail.DetailRate;
    this.totalWeekDetail.GrandTotal = this.totalWeekDetail.WashAmount + this.totalWeekDetail.DetailAmount +
    this.totalWeekDetail.OverTimePay + this.totalWeekDetail.CollisionAmount;
  }

}
