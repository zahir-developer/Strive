import { Component, OnInit } from '@angular/core';
import { DetailService } from '../../services/data-service/detail.service';

@Component({
  selector: 'app-no-of-details',
  templateUrl: './no-of-details.component.html'
})
export class NoOfDetailsComponent implements OnInit {
  detailCount: any;
  jobTypeId: any;
  constructor( private detail: DetailService) { }

  ngOnInit() {
    this.getJobType();
  }

  // Get Details Count
  getDashboardDetails() {
    const obj = {
      id: localStorage.getItem('empLocationId'),
      date: new Date(),
      jobType: this.jobTypeId
    };
    this.detail.getDetailCount(obj).subscribe( res => {
      const wash = JSON.parse(res.resultData);
      if (wash.Dashboard !== null) {
        this.detailCount = wash.Dashboard.DetailsCount;
      }
    });
  }

  getJobType() {
    this.detail.getJobType().subscribe(res => {
      if (res.status === 'Success') {
        const jobtype = JSON.parse(res.resultData);
        if (jobtype.GetJobType.length > 0) {
          jobtype.GetJobType.forEach(item => {
            if (item.valuedesc === 'Detail') {
              this.jobTypeId = item.valueid;
              this.getDashboardDetails();
            }
          });
        }
      }
    });
  }
}
