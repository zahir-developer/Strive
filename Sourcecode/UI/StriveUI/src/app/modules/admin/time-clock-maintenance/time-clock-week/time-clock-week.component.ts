import { Component, OnInit, Input, Output, EventEmitter, ViewEncapsulation } from '@angular/core';
import { TimeClockMaintenanceService } from 'src/app/shared/services/data-service/time-clock-maintenance.service';
import * as _ from 'underscore';
import * as moment from 'moment';
import { DatePipe } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
declare var $: any;
@Component({
  selector: 'app-time-clock-week',
  templateUrl: './time-clock-week.component.html',
  styleUrls: ['./time-clock-week.component.css'],
  encapsulation: ViewEncapsulation.None
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
  washHours: any;
  replicateClockList: any = [];
  weekDays = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
  roleList = [];
  @Input() empClockInObj?: any;
  @Input() isView?: any;
  @Output() cancelCheckInPage = new EventEmitter();
  weekStartDate: any;
  weekLastDate: any;
  inCorrectTotalHours: boolean = false;
  constructor(
    public timeClockMaintenanceService: TimeClockMaintenanceService,
    private datePipe: DatePipe,
    private toastr: ToastrService,
    private messageService: MessageServiceToastr,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
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
    this.spinner.show();
    this.timeClockMaintenanceService.getTimeClockWeekDetails(inputParams).subscribe(res => {
      if (res.status === 'Success') {
        this.spinner.hide();

        const weekDetails = JSON.parse(res.resultData);
        if (weekDetails.Result.TimeClockWeek !== null) {
          this.totalWeekDetail = weekDetails.Result.TimeClockWeek;
        }
        if (weekDetails.Result.TimeClock !== null) {
          this.weekDays.forEach(day => {
            const days = _.where(weekDetails.Result.TimeClock, { Day: day });
            const dayDetails = [];
            if (days.length > 0) {
              days.forEach(item => {
                const inTimeHour = item.InTime.split('+');
                const inTime = new Date(inTimeHour[0]);
                const outTimeHour = item.OutTime.split('+');
                const outTime = new Date(outTimeHour[0]);
                const hours = item.TotalHours.split('+');
                const totalHours = new Date(hours[0]);
                dayDetails.push({
                  EventDate: item.EventDate,
                  InTime: item.InTime ? moment(inTime).format('HH:mm') : '',
                  OutTime: item.OutTime ? moment(outTime).format('HH:mm') : '',
                  RoleId: item.RoleId,
                  TimeClockId: item.TimeClockId,
                  TotalHours: moment(totalHours).format('HH:mm'),
                  employeeId: this.empClockInObj.employeeID,
                  locationId: this.empClockInObj.locationId,
                  isDeleted: false
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
          this.replicateClockList = this.timeClockList;
          this.totalHoursCalculation();
        } else {
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
          this.timeClockList = weekDetails;
          this.replicateClockList = this.timeClockList;
          this.totalHoursCalculation();
        }
      }
      else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');

        this.spinner.hide();

      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      this.spinner.hide();
    });
  }

  getAllRoles() {
    let id = this.empClockInObj.employeeID
    this.timeClockMaintenanceService.getRolesbyEmployeeId(id).subscribe(res => {
      if (res.status === 'Success') {
        const roles = JSON.parse(res.resultData);
        this.roleList = roles.EmployeeRole.EmployeeRoles;
        this.getTimeClockWeekDetails();
      }
    });
  }

  addTimeList(week) {
    week.checkInDetail.push({
      EventDate: week.date ? week.date : week.checkInDetail[0].EventDate,
      InTime: '',
      OutTime: '',
      RoleId: '',
      TimeClockId: 0,
      TotalHours: '',
      employeeId: this.empClockInObj.employeeID,
      locationId: this.empClockInObj.locationId,
      isDeleted: false
    });
  }

  deleteConfirm(currentTime) {
    if (this.isView) {
      return;
    }
    this.timeClockList.filter(i => i.date === moment(currentTime.EventDate).format("YYYY-MM-DD")).forEach(element => {
      if (element.checkInDetail !== 0) {
        element.checkInDetail.forEach(ele => {
          if (ele.TimeClockId !== 0 && ele.TimeClockId === currentTime.TimeClockId) {
            ele.isDeleted = true;
          } else if (ele.TimeClockId === 0 && ele === currentTime) {
            ele.isDeleted = true;
          }
        });
      }
    });
    this.replicateClockList = [];
    let i = 0;
    this.timeClockList.forEach(item => {
      this.replicateClockList.push({
        day: item.day,
        date: item.date,
        checkInDetail: []
      });
      if (item.checkInDetail.length !== 0) {
        item.checkInDetail.forEach(time => {
          if (time.isDeleted === false) {
            this.replicateClockList[i].checkInDetail.push(time);
          }
        });
      }
      i++;
    });
    this.totalHoursCalculation();
  }

  saveWeeklyhours() {
    let checkIn = [];
    let negativeHrs = [];
    var count = 0;
    let replication = false;


    if (this.inCorrectTotalHours === true) {
      this.toastr.error(MessageConfig.Admin.TimeClock.HourFormat, 'Error!');
      return;
    }
    this.timeClockList.forEach(element => {
      if (element.checkInDetail !== 0) {
        element.checkInDetail.forEach(ele => {
          if (ele.TotalHours === "00:00") {
            checkIn.push(ele);
          }
          if (new Date(ele.InTime).toUTCString() > new Date(ele.OutTime).toUTCString()) {
            negativeHrs.push(ele);
          }
          element.checkInDetail.forEach(i => {
            if ((new Date(ele.InTime).toUTCString() > new Date(i.InTime).toUTCString()
              && new Date(ele.InTime).toUTCString() < new Date(i.OutTime).toUTCString())
              || (new Date(ele.OutTime).toUTCString() > new Date(i.InTime).toUTCString()
                && new Date(ele.OutTime).toUTCString() < new Date(i.OutTime).toUTCString())) {
              count += 1;
            }
          });
          if (count > 0) {
            count = 0;
            replication = true;
          }
        });
      }
    });
    if (replication) {
      this.toastr.warning(MessageConfig.Admin.TimeClock.sameDay, 'Warning!');
      return;
    }
    if (checkIn.length !== 0) {
      this.toastr.warning(MessageConfig.Admin.TimeClock.totalHour, 'Warning!');

      return;
    } else if (negativeHrs.length !== 0) {
      this.toastr.warning(MessageConfig.Admin.TimeClock.totalHourNegative, 'Warning!');

      return;
    }
    const weekDetailObj = [];
    this.timeClockList.forEach(item => {
      item.checkInDetail.forEach(time => {
        const inEventDate = new Date(time.EventDate);
        const outEventDate = new Date(time.EventDate);
        const inTime = time.InTime.split(':');
        const outTime = time.OutTime.split(':');
        const inTimeHours = +inTime[0];
        const inTimeMins = +inTime[1];
        const outTimeHours = +outTime[0];
        const outTimeMins = +outTime[1];
        inEventDate.setHours(inTimeHours);
        inEventDate.setMinutes(inTimeMins);
        outEventDate.setHours(outTimeHours);
        outEventDate.setMinutes(outTimeMins);
        const inTimeFormat = this.datePipe.transform(inEventDate, 'MM/dd/yyyy HH:mm');
        const outTimeFormat = this.datePipe.transform(outEventDate, 'MM/dd/yyyy HH:mm');
        weekDetailObj.push({
          timeClockId: time.TimeClockId,
          employeeId: time.employeeId,
          locationId: time.locationId,
          roleId: (time.RoleId !== null && time.RoleId !== '') ? +time.RoleId : null,
          eventDate: time.EventDate, // time.EventDate,
          inTime: time.InTime ? inTimeFormat : null,
          outTime: time.OutTime ? outTimeFormat : null,
          eventType: null,
          updatedFrom: '',
          status: true,
          comments: '',
          isActive: true,
          isDeleted: time.isDeleted
        });
      });
    });
    const finalObj = {
      timeClock: { timeClock: weekDetailObj },
      TimeClockWeekDetailDto: {

        employeeId: this.empClockInObj.employeeID,
        locationId: this.empClockInObj.locationId,
        startDate: this.datePipe.transform(this.empClockInObj.startDate, 'yyyy-MM-dd'),
        endDate: this.datePipe.transform(this.empClockInObj.endDate, 'yyyy-MM-dd'),
        employeeName: this.empClockInObj.firstName + ' ' + this.empClockInObj.lastName
      }
    };
    this.spinner.show();
    this.timeClockMaintenanceService.saveTimeClock(finalObj).subscribe(res => {
      if (res.status === 'Success') {
        this.spinner.hide();

        this.toastr.success(MessageConfig.Admin.TimeClock.Add, 'Success!');
        this.backToTimeClockPage();
      }
      else {
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');

      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      this.spinner.hide();
    });
  }

  inTime(event, currentTime) {
    if (currentTime.OutTime !== "") {

      const DateMonthInTime = currentTime.EventDate + ' ' + currentTime.InTime;
      const DateMonthOutTime = currentTime.EventDate + ' ' + currentTime.OutTime;
      const inTimeHours = moment(DateMonthInTime, 'YYYY-MM-DD HH mm').format('YYYY-MM-DD hh:mm A');
      const outFormat = moment(DateMonthOutTime, 'YYYY-MM-DD HH mm').format('YYYY-MM-DD hh:mm A');
      const inTime = new Date(inTimeHours);
      const outTime = new Date(outFormat);
      const inTimeMins = inTime.getHours() * 60 + inTime.getMinutes();
      const outTimeMins = outTime.getHours() * 60 + outTime.getMinutes();
      const MINUTES = (outTimeMins - inTimeMins);
      var m = (MINUTES % 60);
      const h = (MINUTES - m) / 60;
      const hrs = h < 0 ? -h : h;
      if (m < 0) {
        m = 60 - (-m);
      }
      const HHMM = (h < 10 && h >= 0 ? "0" : "") + (h < 0 ? "-0" : "") + hrs.toString() + ":" + (m < 10 ? "0" : "") + m.toString();
      let hm = HHMM.slice(0, 1)
      if (hm == '-') {
        currentTime.TotalHours = 0;
        this.inCorrectTotalHours = true;

        this.toastr.error(MessageConfig.Admin.TimeClock.HourFormat, 'Error!');

      }
      else {
        currentTime.TotalHours = HHMM;
        this.inCorrectTotalHours = false;
      }
      this.totalHoursCalculation();
    }
  }

  outTime(event, currentTime) {
    if (currentTime.InTime !== "") {
      const DateMonthInTime = currentTime.EventDate + ' ' + currentTime.InTime;
      const DateMonthOutTime = currentTime.EventDate + ' ' + currentTime.OutTime;

      const inTimeHours = moment(DateMonthInTime, 'YYYY-MM-DD HH mm').format('YYYY-MM-DD hh:mm A');
      const outFormat = moment(DateMonthOutTime, 'YYYY-MM-DD HH mm').format('YYYY-MM-DD hh:mm A');
      const inTime = new Date(inTimeHours);
      const outTime = new Date(outFormat);
      const inTimeMins = inTime.getHours() * 60 + inTime.getMinutes();
      const outTimeMins = outTime.getHours() * 60 + outTime.getMinutes();
      const MINUTES = (outTimeMins - inTimeMins);
      var m = (MINUTES % 60);
      const h = (MINUTES - m) / 60;
      const hrs = h < 0 ? -h : h;
      if (m < 0) {
        m = 60 - (-m);
      }
      const HHMM = (h < 10 && h >= 0 ? "0" : "") + (h < 0 ? "-0" : "") + hrs.toString() + ":" + (m < 10 ? "0" : "") + m.toString();

      let hm = HHMM.slice(0, 1)
      if (hm == '-') {
        currentTime.TotalHours = 0;
        this.inCorrectTotalHours = true;

        this.toastr.error(MessageConfig.Admin.TimeClock.HourFormat, 'Error!');

      }
      else {
        currentTime.TotalHours = HHMM;
        this.inCorrectTotalHours = false;
      }
      this.totalHoursCalculation();

    }
  }

  backToTimeClockPage() {
    this.cancelCheckInPage.emit();
  }

  totalHoursCalculation() {
    let washHour: any = 0;
    let detailHour: any = 0;
    let washMins = 0;
    let detailsMins = 0;
    this.replicateClockList.forEach(item => {
      item.checkInDetail.forEach(checkIn => {
        if (this.roleList.filter(role => +role.RoleMasterId === +checkIn.RoleId)[0]?.RoleName === 'Washer') {
          let n = checkIn.TotalHours.search(":");
          let h = checkIn.TotalHours.substring(0, n);
          let m = checkIn.TotalHours.substring(n + 1, n + 3);
          let hrs = +h;
          let min = (+m);
          let hr = (hrs) * 60;
          let totalMins = hr + min;
          washMins += totalMins;
          var minutes = (washMins % 60);
          const hours = (washMins - minutes) / 60;
          const HHMM = (hours < 10 ? "0" : "") + hours.toString() + ":" + (minutes < 10 ? "0" : "") + minutes.toString();

          let totalHrs = HHMM;
          washHour = totalHrs;

        } else if (this.roleList.filter(role => +role.RoleMasterId === +checkIn.RoleId)[0]?.RoleName === 'Detailer') {
          let n = checkIn.TotalHours.search(":");
          let h = checkIn.TotalHours.substring(0, n);
          let m = checkIn.TotalHours.substring(n + 1, n + 3);
          let hrs = +h;
          let min = (+m);
          let hr = (hrs) * 60;
          let totalMins = hr + min;
          detailsMins += totalMins;
          var minutes = (detailsMins % 60);
          const hours = (detailsMins - minutes) / 60;
          const HHMM = (hours < 10 ? "0" : "") + hours.toString() + ":" + (minutes < 10 ? "0" : "") + minutes.toString();

          let totalHrs = HHMM;
          detailHour = totalHrs;

        }
      });
    });


    this.totalWeekDetail.TotalDetailHours = detailHour;
   // this.washHours = washHour.split(':');
    this.totalWeekDetail.TotalWashHours = washHour;//this.washHours[0] <= 40 ? washHour : '40:00';

  }



}
