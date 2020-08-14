import { Component, OnInit } from '@angular/core';
import { WashService } from '../../services/data-service/wash.service';

@Component({
  selector: 'app-no-of-details',
  templateUrl: './no-of-details.component.html',
  styleUrls: ['./no-of-details.component.css']
})
export class NoOfDetailsComponent implements OnInit {
  detailCount: any;

  constructor( private wash: WashService) { }

  ngOnInit() {
    this.getDashboardDetails();
  }
  getDashboardDetails = () => {
    this.wash.data.subscribe((data: any) => {
      if (data.DetailsCount !== undefined) {
        this.detailCount = data.DetailsCount.DetailsCount;
      }
    });
  }
}
