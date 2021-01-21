import { Component, OnInit, ViewChild } from '@angular/core';
import { DetailService } from 'src/app/shared/services/data-service/detail.service';
import { DatePipe } from '@angular/common';
import { TodayScheduleComponent } from '../today-schedule/today-schedule.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { NoOfDetailsComponent } from 'src/app/shared/components/no-of-details/no-of-details.component';
import { DatepickerDateCustomClasses } from 'ngx-bootstrap/datepicker';

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
  isView: boolean;
  dateCustomClasses: DatepickerDateCustomClasses[];
  time = ['07:00', '07:30', '08:00', '08:30', '09:00', '09:30', '10:00', '10:30', '11:00', '11:30', '12:00', '12:30', '13:00', '13:30', '14:00', '14:30', '15:00', '15:30', '16:00', '16:30', '17:00', '17:30', '18:00', '18:30'];
  @ViewChild(TodayScheduleComponent) todayScheduleComponent: TodayScheduleComponent;
  @ViewChild(NoOfDetailsComponent) noOfDetailsComponent: NoOfDetailsComponent;
  constructor(
    private detailService: DetailService,
    private datePipe: DatePipe,
    private spinner: NgxSpinnerService,
    private toastr: MessageServiceToastr
  ) { }

  ngOnInit(): void {
    this.actionType = '';
    this.showDialog = false;
    this.isEdit = false;
    this.isView = false;
    const now = new Date();
    const twoDaysAhead = new Date();
    twoDaysAhead.setDate(now.getDate() + 2);
    this.dateCustomClasses = [
      { date: now, classes: [] },
      { date: twoDaysAhead, classes: ['bg-warning'] }
    ];
    // this.getScheduleDetailsByDate(this.selectedDate);
    // this.getTodayDateScheduleList();
  }

  addNewDetail(schedule) {
    const currentDate = new Date();
    if (this.datePipe.transform(currentDate, 'dd-MM-yyyy') === this.datePipe.transform(this.selectedDate, 'dd-MM-yyyy')) {
      this.bayAddDetail(schedule);
    } else if (currentDate < this.selectedDate) {
      this.bayAddDetail(schedule);
    } else {
      this.toastr.showMessage({ severity: 'info', title: 'Info', body: 'New schedule is not allowed for passed dates.' });
      return;
    }
  }

  bayAddDetail(schedule) {
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
    const currentDate = new Date();
    if (this.datePipe.transform(currentDate, 'dd-MM-yyyy') === this.datePipe.transform(this.selectedDate, 'dd-MM-yyyy')) {
      this.isView = false;
    } else if (currentDate < this.selectedDate) {
      this.isView = false;
    } else {
      this.isView = true;
    }
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
    // document.documentElement.style.setProperty(`--primary-color`, '#FFFFFF');
    this.spinner.show();
    this.detailService.getScheduleDetailsByDate(finalObj).subscribe(res => {
      this.spinner.hide();
      if (res.status === 'Success') {
        const scheduleDetails = JSON.parse(res.resultData);
        console.log(scheduleDetails, 'details');
        const bayList = scheduleDetails.GetBaySchedulesDetails.BayList;
        const bayScheduleDetails = scheduleDetails.GetBaySchedulesDetails.BayScheduleDetails === null ? []
          : scheduleDetails.GetBaySchedulesDetails.BayScheduleDetails;
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
          item.time === '13:00' || item.time === '13:30' || item.time === '14:00' || item.time === '14:30') {
            this.afternoonBaySchedue.push(item);
          } else if ( item.time === '15:00' || item.time === '15:30' || item.time === '16:00' || item.time === '16:30' ||
          item.time === '17:00' || item.time === '17:30' || item.time === '18:00' || item.time === '18:30') {
            this.eveningBaySchedule.push(item);
          }
        });
        this.todayScheduleComponent.getTodayDateScheduleList();
      }
    }, (error) => {
      this.spinner.hide();
      this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
    });
  }

  closeModal() {
    this.showDialog = false;
    this.isEdit = false;
    this.isView = false;
    this.refreshDetailGrid();
  }

  refreshDetailGrid() {
    this.getScheduleDetailsByDate(this.selectedDate);
    this.todayScheduleComponent.getTodayDateScheduleList();
    this.noOfDetailsComponent.getDashboardDetails();
  }

}
