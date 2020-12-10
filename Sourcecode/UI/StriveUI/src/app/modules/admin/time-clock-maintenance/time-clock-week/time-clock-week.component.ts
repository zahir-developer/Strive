import { Component, OnInit, Input, Output, EventEmitter, ViewEncapsulation } from '@angular/core';
import { TimeClockMaintenanceService } from 'src/app/shared/services/data-service/time-clock-maintenance.service';
import * as _ from 'underscore';
import * as moment from 'moment';
import { DatePipe } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
declare var $ : any;
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
  replicateClockList: any = [];
  weekDays = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
  roleList = [];
  @Input() empClockInObj?: any;
  @Input() isView?: any;
  @Output() cancelCheckInPage = new EventEmitter();
  weekStartDate: any;
  weekLastDate: any;
  constructor(
    public timeClockMaintenanceService: TimeClockMaintenanceService,
    private datePipe: DatePipe,
    private toastr: ToastrService,
    private messageService: MessageServiceToastr,
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
          //this.totalHoursCalculation();
        }
        if (weekDetails.Result.TimeClock !== null) {
          this.weekDays.forEach(day => {
            const days = _.where(weekDetails.Result.TimeClock, { Day: day });
            const dayDetails = [];
            if (days.length > 0) {
              days.forEach(item => {
                dayDetails.push({
                  EventDate: item.EventDate,
                  InTime: item.InTime ?  moment(item.InTime).format('HH:mm'):'',
                  OutTime: item.OutTime ?  moment(item.OutTime).format('HH:mm'):'',
                  RoleId: item.RoleId,
                  TimeClockId: item.TimeClockId,
                  TotalHours: moment(item.TotalHours).format('HH:mm'),
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
          console.log(this.timeClockList, 'timeclocklist');
          this.totalHoursCalculation();
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
          this.replicateClockList = this.timeClockList;
          this.totalHoursCalculation();
        }
      }
    });
  }

  getAllRoles() {
    this.timeClockMaintenanceService.getAllRoles().subscribe(res => {
      if (res.status === 'Success') {
        const roles = JSON.parse(res.resultData);
        console.log(roles);
        this.roleList = roles.EmployeeRoles;
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
    let i=0;
    this.timeClockList.forEach(item => {
      this.replicateClockList.push({
        day: item.day,
        date: item.date,
        checkInDetail:[]
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
    console.log(this.timeClockList,this.replicateClockList);
  }

  saveWeeklyhours() {
    let checkIn = [];
    let negativeHrs = [];
    var count = 0;
    let replication = false;
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
      this.messageService.showMessage({ severity: 'warning', title: 'Warning', body: 'Similar Timing in same Day' });
      return;
    }
    if (checkIn.length !== 0) {
      this.messageService.showMessage({ severity: 'warning', title: 'Warning', body: 'Total Hours should not be 0' });
      return;
    } else if (negativeHrs.length !== 0) {
      this.messageService.showMessage({ severity: 'warning', title: 'Warning', body: 'Total Hours should not be negative' });
      return;
    }
    console.log(this.replicateClockList, 'finalobj');
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
        weekDetailObj.push({
          timeClockId: time.TimeClockId,
          employeeId: time.employeeId,
          locationId: time.locationId,
          roleId: (time.RoleId !== null && time.RoleId !== '') ? +time.RoleId : null,
          eventDate: time.EventDate,
          inTime: time.InTime? moment(inEventDate).format() : '',  // this.datePipe.transform(time.InTime, 'HH:mm'),
          outTime:time.OutTime ?  moment(outEventDate).format(): '', // this.datePipe.transform(time.OutTime, 'HH:mm'),
          eventType: 1,
          updatedFrom: 'string',
          status: true,
          comments: 'string',
          isActive: true,
          isDeleted: time.isDeleted
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

  inTime(event, currentTime) {
  
    console.log(event, 'intime');
    if (currentTime.OutTime !== "") {
      const inTime = (currentTime.InTime.split(':'));
      const outTime = (currentTime.OutTime.split(':'));
      const checkinHours = +inTime[0];
      const checkoutHours = +outTime[0];
      const checkinmins = +inTime[1];
      const checkoutmins = +outTime[1];
    const HOURS = checkoutHours - checkinHours
      const MINUTES = checkoutmins - checkinmins;
      const hrs = Math.abs(HOURS)
      const mins = Math.abs(MINUTES)

       const minutesMath = MINUTES
      var m = minutesMath % 60;
      // const h = hr
      // const hrs = h < 0 ? -h : h;
      if (m < 0) {
        m = 60 - (-m);
      }
      

    //  const HHMM1 :any=( (h < 10 && h >= 0 ? "0" : "")  + hrs.toString()) + ":" + (m < 10 ? "0" : "") + m.toString();
     const  HHMM   = ( (hrs < 10 && hrs >= 0 ? "0" : "")  + hrs.toString()) + ':' + (mins < 10 ? "0" : "") + mins.toString();
    
    currentTime.TotalHours = HHMM;

      this.totalHoursCalculation();
    }
  }

  outTime(event, currentTime) {
    console.log(event, currentTime);
    if (currentTime.InTime !== "") {
      // const inTime = new Date(currentTime.InTime);
      // const outTime = new Date(currentTime.OutTime);
      // const inTimeMins = inTime.getHours() * 60 + inTime.getMinutes();
      // const outTimeMins = outTime.getHours() * 60 + outTime.getMinutes();
      // const MINUTES = (outTimeMins - inTimeMins);
      // var m = (MINUTES % 60);
      // const h = (MINUTES - m) / 60;
      // const hrs = h < 0 ? -h : h;
      // if (m < 0) {
      //   m = 60 - (-m);
      // }
      // const HHMM = (h < 10 && h >= 0 ? "0" : "") + (h < 0 ? "-0" : "") + hrs.toString() + ":" + (m < 10 ? "0" : "") + m.toString();
      // currentTime.TotalHours = HHMM;
      const inTime = (currentTime.InTime.split(':'));
      const outTime = (currentTime.OutTime.split(':'));
      const checkinHours = +inTime[0];
      const checkoutHours = +outTime[0];
      const checkinmins = +inTime[1];
      const checkoutmins = +outTime[1];
    const HOURS = checkoutHours - checkinHours
      const MINUTES = checkoutmins - checkinmins;
      const hrs = Math.abs(HOURS)
      const mins = Math.abs(MINUTES)

       const minutesMath = MINUTES
      var m = minutesMath % 60;
      // const h = hr
      // const hrs = h < 0 ? -h : h;
      if (m < 0) {
        m = 60 - (-m);
      }
      

    //  const HHMM1 :any=( (h < 10 && h >= 0 ? "0" : "")  + hrs.toString()) + ":" + (m < 10 ? "0" : "") + m.toString();
     const  HHMM   = ( (hrs < 10 && hrs >= 0 ? "0" : "")  + hrs.toString()) + ':' + (mins < 10 ? "0" : "") + mins.toString();
    
    currentTime.TotalHours = HHMM;
      this.totalHoursCalculation();

    }
  }

  backToTimeClockPage() {
    this.cancelCheckInPage.emit();
  }

  totalHoursCalculation() {
    let washHour = 0;
    let detailHour = 0;
    this.replicateClockList.forEach(item => {
      item.checkInDetail.forEach(checkIn => {
        if (this.roleList.filter(role => +role.RoleMasterId === +checkIn.RoleId)[0]?.RoleName === 'Wash') {
          let n = checkIn.TotalHours.search(":");
          let h = checkIn.TotalHours.substring(0, n);
          let m = checkIn.TotalHours.substring(n + 1, n + 3);
          let hrs = +h;
          let min = (+m / 60).toFixed(2);
          let totalHrs = hrs + (+min);
          washHour += totalHrs;
        } else if (this.roleList.filter(role => +role.RoleMasterId === +checkIn.RoleId)[0]?.RoleName === 'Detailer') {
          let n = checkIn.TotalHours.search(":");
          let h = checkIn.TotalHours.substring(0, n);
          let m = checkIn.TotalHours.substring(n + 1, n + 3);
          let hrs = +h;
          let min = (+m / 60).toFixed(2);
          let totalHrs = hrs + (+min);
          detailHour += totalHrs;
        }
      });
    });
    this.totalWeekDetail.TotalDetailHours = detailHour;
    this.totalWeekDetail.TotalWashHours = washHour <= 40 ? washHour : 40;
    this.totalWeekDetail.OverTimeHours = washHour > 40 ? (washHour-40) : 0;
    this.totalWeekDetail.WashAmount = this.totalWeekDetail.TotalWashHours * this.totalWeekDetail.WashRate;
    this.totalWeekDetail.DetailAmount = this.totalWeekDetail.TotalDetailHours * this.totalWeekDetail.DetailRate;
    this.totalWeekDetail.OverTimePay = this.totalWeekDetail.OverTimeHours * (this.totalWeekDetail.WashRate * 1.5);
    this.totalWeekDetail.GrandTotal = (this.totalWeekDetail.WashAmount + this.totalWeekDetail.DetailAmount +
      this.totalWeekDetail.OverTimePay) - this.totalWeekDetail.CollisionAmount;

  }

  timeCheck(data) {
    console.log(data);
  }

}
