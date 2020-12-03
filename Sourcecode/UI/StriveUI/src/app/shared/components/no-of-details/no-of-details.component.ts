import { Component, OnInit } from '@angular/core';
import { DetailService } from '../../services/data-service/detail.service';

@Component({
  selector: 'app-no-of-details',
  templateUrl: './no-of-details.component.html',
  styleUrls: ['./no-of-details.component.css']
})
export class NoOfDetailsComponent implements OnInit {
  detailCount: any;

  constructor( private detail: DetailService) { }

  ngOnInit() {
    this.getDashboardDetails();
  }

  // Get Details Count
  getDashboardDetails() {
    const obj = {
      id: localStorage.getItem('empLocationId'),
      date: new Date()
    };
    this.detail.getDetailCount(obj).subscribe( res => {
      const wash = JSON.parse(res.resultData);
      if (wash.Dashboard !== null) {
        this.detailCount = wash.Dashboard.DetailsCount;
      }
    });
  }
}
