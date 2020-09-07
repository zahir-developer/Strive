import { Component, OnInit } from '@angular/core';
import { DetailService } from 'src/app/shared/services/data-service/detail.service';
import { DatePipe } from '@angular/common';

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
  constructor(
    private detailService: DetailService,
    private datePipe: DatePipe
  ) { }

  ngOnInit(): void {
    this.showDialog = false;
    this.isEdit = false;
    // this.getScheduleDetailsByDate(this.selectedDate);
  }

  addNewDetail() {
    this.showDialog = true;
  }

  closeDialog(event) {
    this.isEdit = event.isOpenPopup;
    this.showDialog = event.isOpenPopup;
  }

  getDetailByID() {
    console.log(this.datePipe.transform(this.selectedDate, 'yyyy-MM-dd'), 'date changing');
    this.detailService.getDetailById(65).subscribe( res => {
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
    const data = this.sampleJson();
    let firstRow = [];
    let secondRow = [];
    const thirdRow = [];
    const fourthRow = [];
    const fifthRow = [];
    const sixthRow = [];
    const seventhRow = [];
    const eightRow = [];
    data.forEach( item => {
      if (item.time === '07:00' || item.time === '11:00' || item.time === '03:00') {
        if (item.time === '07:00') {

        }
        firstRow.push(item);
      } else if (item.time === '07:30' || item.time === '11:30' || item.time === '03:30') {
        secondRow.push(item);
      }
    });
    firstRow.forEach( item => {
      
    });
    secondRow = secondRow.map( item =>  {
      return {
        firstColumn: item.time,
        firstBaySchedule: item.baySchedule
      };
    });
    console.log(firstRow, secondRow, 'data');
    const scheduleDate = this.datePipe.transform(date, 'yyyy-MM-dd');
    this.detailService.getScheduleDetailsByDate('2020-08-20').subscribe( res => {
      if (res.status === 'Success') {
        const scheduleDetails = JSON.parse(res.resultData);
        console.log(scheduleDetails, 'details');
      }
    });
  }

  closeModal() {
    this.showDialog = false;
    this.isEdit = false;
  }

  sampleJson() {
    const data = [
      {
        time: '07:00',
        baySchedule: [
          {
            bayId: 1,
            isSchedule : false,
          },
          {
            bayId: 2,
            isSchedule: true
          }
        ]
      },
      {
        time: '07:30',
        baySchedule: [
          {
            bayId: 1,
            isSchedule : false,
          },
          {
            bayId: 2,
            isSchedule: true
          }
        ]
      },
      {
        time: '11:00',
        baySchedule: [
          {
            bayId: 1,
            isSchedule : false,
          },
          {
            bayId: 2,
            isSchedule: true
          }
        ]
      },
      {
        time: '11:30',
        baySchedule: [
          {
            bayId: 1,
            isSchedule : false,
          },
          {
            bayId: 2,
            isSchedule: true
          }
        ]
      },
      {
        time: '03:00',
        baySchedule: [
          {
            bayId: 1,
            isSchedule : false,
          },
          {
            bayId: 2,
            isSchedule: true
          }
        ]
      },
      {
        time: '03:30',
        baySchedule: [
          {
            bayId: 1,
            isSchedule : false,
          },
          {
            bayId: 2,
            isSchedule: true
          }
        ]
      }
    ];
    return data;
  }

}
