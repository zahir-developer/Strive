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
  constructor(
    private detailService: DetailService
  ) { }

  ngOnInit(): void {
    this.showDialog = false;
  }

  addNewDetail() {
    this.showDialog = true;
  }

  closeDialog(event) {
    this.showDialog = event.isOpenPopup;
  }

  getDetailByID() {
    this.detailService.getDetailById(65).subscribe( res => {
      if (res.status === 'Success') {
        const details = JSON.parse(res.resultData);
        console.log(details, 'details');
        this.selectedData = details.DetailsForDetailId;
        this.showDialog = true;
      }
    });
  }

}
