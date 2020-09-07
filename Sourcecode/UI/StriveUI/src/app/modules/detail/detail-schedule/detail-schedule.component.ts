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
    this.getScheduleDetailsByDate(this.selectedDate);
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
    const scheduleDate = this.datePipe.transform(date, 'yyyy-MM-dd');
    console.log(scheduleDate, 'date');
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

}
