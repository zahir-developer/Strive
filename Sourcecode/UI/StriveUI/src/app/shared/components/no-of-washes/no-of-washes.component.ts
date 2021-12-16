import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { DetailService } from '../../services/data-service/detail.service';
import { WashService } from '../../services/data-service/wash.service';
import { MessageConfig } from '../../services/messageConfig';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-no-of-washes',
  templateUrl: './no-of-washes.component.html'
})
export class NoOfWashesComponent implements OnInit {
  washCount: any;
  jobTypeId: any;
  constructor(
    private wash: WashService,
    private detail: DetailService,
    private toastr :ToastrService
    ) { }

  ngOnInit() {
    this.getJobType();
  }

  // Get Wash Count
  getDashboardDetails = () => {
    const obj = {
      id: +localStorage.getItem('empLocationId'),
      date: new Date(),
      jobType: this.jobTypeId
    };
    this.wash.getDashBoard(obj);
    this.wash.dashBoardData.subscribe((data: any) => {
        this.washCount = data.WashesCount;
    });
  }

  getJobType() {
    this.detail.getJobType().subscribe(res => {
      if (res.status === 'Success') {
        const jobtype = JSON.parse(res.resultData);
        if (jobtype.GetJobType.length > 0) {
          jobtype.GetJobType.forEach(item => {
            if (item.valuedesc === 'Wash') {
              this.jobTypeId = item.valueid;
              this.getDashboardDetails();
            }
          });
        }
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }
}
