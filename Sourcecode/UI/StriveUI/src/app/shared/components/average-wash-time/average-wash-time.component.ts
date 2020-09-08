import { Component, OnInit } from '@angular/core';
import { WashService } from '../../services/data-service/wash.service';

@Component({
  selector: 'app-average-wash-time',
  templateUrl: './average-wash-time.component.html',
  styleUrls: ['./average-wash-time.component.css']
})
export class AverageWashTimeComponent implements OnInit {
  average: any;

  constructor(private wash: WashService) { }

  ngOnInit() {
    this.getDashboardDetails();
  }

  // Get Averege Wash Time
  getDashboardDetails = () => {
    this.wash.data.subscribe((data: any) => {
      if (data.AverageWashTime !== undefined) {
        this.average = data.AverageWashTime.AverageWashTime;
      }
    });
  }
}
