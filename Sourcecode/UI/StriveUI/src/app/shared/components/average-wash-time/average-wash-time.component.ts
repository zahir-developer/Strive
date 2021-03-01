import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../../services/data-service/customer.service';
import { WashService } from '../../services/data-service/wash.service';

@Component({
  selector: 'app-average-wash-time',
  templateUrl: './average-wash-time.component.html',
  styleUrls: ['./average-wash-time.component.css']
})
export class AverageWashTimeComponent implements OnInit {
  average: any;

  constructor(private wash: CustomerService) { }

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
    });
  }
}
