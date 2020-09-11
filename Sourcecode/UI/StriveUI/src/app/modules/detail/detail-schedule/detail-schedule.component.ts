import { Component, OnInit, ViewChild } from '@angular/core';
import { DetailService } from 'src/app/shared/services/data-service/detail.service';
import { DatePipe } from '@angular/common';
import { TodayScheduleComponent } from '../today-schedule/today-schedule.component';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-detail-schedule',
  templateUrl: './detail-schedule.component.html',
  styleUrls: ['./detail-schedule.component.css']
})
export class DetailScheduleComponent implements OnInit {
  showDialog: boolean;
  selectedData: any;
  isEdit: boolean;
  selectedDate = new Date();
  scheduledBayGrid = [];
  bayScheduleObj: any = {};
  bayDetail: any = [];
  morningBaySchedule: any = [];
  afternoonBaySchedue: any = [];
  eveningBaySchedule: any = [];
  actionType: string;
  time = ['07:00', '07:30', '08:00', '08:30', '09:00', '09:30', '10:00', '10:30', '11:00', '11:30', '12:00', '12:30', '01:00', '01:30', '02:00', '02:30', '03:00', '03:30', '04:00', '04:30', '05:00', '05:30', '06:00', '06:30'];
  @ViewChild(TodayScheduleComponent) todayScheduleComponent: TodayScheduleComponent;
  constructor(
    private detailService: DetailService,
    private datePipe: DatePipe,
    private spinner: NgxSpinnerService,
  ) { }

  ngOnInit(): void {
    this.actionType = '';
    this.showDialog = false;
    this.isEdit = false;
    // this.getScheduleDetailsByDate(this.selectedDate);
    // this.getTodayDateScheduleList();
  }

  addNewDetail(schedule) {
    console.log(schedule);
    this.bayScheduleObj = {
      time: schedule.time,
      date: this.selectedDate,
      bayId: schedule.bayId
    };
    this.actionType = 'New Detail';
    this.isEdit = false;
    this.showDialog = true;
  }

  closeDialog(event) {
    this.isEdit = event.isOpenPopup;
    this.showDialog = event.isOpenPopup;
  }

  getDetailByID(bay) {
    this.actionType = 'Edit Detail';
    console.log(this.datePipe.transform(this.selectedDate, 'yyyy-MM-dd'), 'date changing');
    this.detailService.getDetailById(bay.jobId).subscribe(res => {
      if (res.status === 'Success') {
        const details = JSON.parse(res.resultData);
        console.log(details, 'details');
        this.selectedData = details.DetailsForDetailId;
        this.isEdit = true;
        this.showDialog = true;
      }
    });
  }

  getScheduleDetailsByDate(date) {
    this.morningBaySchedule = [];
    this.afternoonBaySchedue = [];
    this.eveningBaySchedule = [];
    this.selectedDate = date;
    const locationId = localStorage.getItem('empLocationId');
    const scheduleDate = this.datePipe.transform(date, 'yyyy-MM-dd');
    const finalObj = {
      jobDate: scheduleDate,
      locationId
    };
    this.spinner.show();
    this.detailService.getScheduleDetailsByDate(scheduleDate, locationId).subscribe(res => {
      this.spinner.hide();
      if (res.status === 'Success') {
        const scheduleDetails = JSON.parse(res.resultData);
        console.log(scheduleDetails, 'details');
        const bayList = scheduleDetails.ScheduleDetailsForDate.BayList;
        const bayScheduleDetails = scheduleDetails.ScheduleDetailsForDate.BayScheduleDetails === null ? []
          : scheduleDetails.ScheduleDetailsForDate.BayScheduleDetails;
        let baySchedule = [];
        const baySheduled = [];
        this.time.forEach(item => {
          baySchedule = [];
          const time = bayScheduleDetails.filter(elem => elem.ScheduleInTime === item);
          if (time.length > 0) {
            bayList.forEach(bay => {
              const bayID = time.filter(elem => elem.BayId === bay.BayId);
              if (bayID.length > 0) {
                baySchedule.push({
                  bayId: bayID[0].BayId,
                  isSchedule: true,
                  jobId: bayID[0].JobId,
                  time: bayID[0].ScheduleInTime
                });
              } else {
                baySchedule.push({
                  bayId: bay.BayId,
                  isSchedule: false,
                  time: item
                });
              }
            });
          } else {
            bayList.forEach(bay => {
              baySchedule.push({
                bayId: bay.BayId,
                isSchedule: false,
                time: item
              });
            });
          }
          baySheduled.push({
            time: item,
            bay: baySchedule
          });
        });
        console.log(baySheduled, 'bayLogic');
        baySheduled.forEach(item => {
          if (item.time === '07:00' || item.time === '07:30' || item.time === '08:00' || item.time === '08:30' ||
          item.time === '09:00' || item.time === '09:30' || item.time === '10:00' || item.time === '10:30') {
            this.morningBaySchedule.push(item);
          } else if ( item.time === '11:00' || item.time ===  '11:30' || item.time === '12:00' || item.time === '12:30' ||
          item.time === '01:00' || item.time === '01:30' || item.time === '02:00' || item.time === '02:30') {
            this.afternoonBaySchedue.push(item);
          } else if ( item.time === '03:00' || item.time === '03:30' || item.time === '04:00' || item.time === '04:30' ||
          item.time === '05:00' || item.time === '05:30' || item.time === '06:00' || item.time === '06:30') {
            this.eveningBaySchedule.push(item);
          }
        });
      }
    });
  }

  closeModal() {
    this.showDialog = false;
    this.isEdit = false;
  }

  refreshDetailGrid() {
    this.getScheduleDetailsByDate(this.selectedDate);
    this.todayScheduleComponent.getTodayDateScheduleList();
  }

}
