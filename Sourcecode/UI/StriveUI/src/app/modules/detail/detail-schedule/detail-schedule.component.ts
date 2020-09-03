import { Component, OnInit } from '@angular/core';
import { DetailService } from 'src/app/shared/services/data-service/detail.service';

@Component({
  selector: 'app-detail-schedule',
  templateUrl: './detail-schedule.component.html',
  styleUrls: ['./detail-schedule.component.css']
})
export class DetailScheduleComponent implements OnInit {
  showDialog: boolean;
  selectedData: any;
  isEdit: boolean;
  constructor(
    private detailService: DetailService
  ) { }

  ngOnInit(): void {
    this.showDialog = false;
    this.isEdit = false;
    this.getScheduleDetailsByDate();
  }

  addNewDetail() {
    this.showDialog = true;
  }

  closeDialog(event) {
    this.isEdit = event.isOpenPopup;
    this.showDialog = event.isOpenPopup;
  }

  getDetailByID() {
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

  getScheduleDetailsByDate() {
    this.detailService.getScheduleDetailsByDate('2020-08-20').subscribe( res => {
      if (res.status === 'Success') {
        const scheduleDetails = JSON.parse(res.resultData);
        console.log(scheduleDetails, 'details');
      }
    });
  }

}
