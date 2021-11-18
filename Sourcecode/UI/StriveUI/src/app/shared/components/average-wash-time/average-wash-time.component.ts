import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../../services/data-service/customer.service';
import { WashService } from '../../services/data-service/wash.service';
import { ToastrService } from 'ngx-toastr';
import { MessageConfig } from '../../services/messageConfig';

@Component({
  selector: 'app-average-wash-time',
  templateUrl: './average-wash-time.component.html'
})
export class AverageWashTimeComponent implements OnInit {
  average: any;

  constructor(private wash: CustomerService,
    private toastr: ToastrService) { }

  ngOnInit() {
    this.getDashboardDetails();
  }

  // Get Averege Wash Time
  getDashboardDetails = () => {
    const id = +localStorage.getItem('empLocationId');
    this.wash.getWashTimeByLocationId(id).subscribe((data: any) => {
      if (data.status === 'Success') {
        const washTime = JSON.parse(data.resultData);
        this.average = washTime.Location.Location.WashTimeMinutes;
      }
    }
    , (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }
}
