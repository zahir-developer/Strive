import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-hourly-wash',
  templateUrl: './hourly-wash.component.html',
  styleUrls: ['./hourly-wash.component.css']
})
export class HourlyWashComponent implements OnInit {
  locationId: any;
  fileType: number;
  todayDate = new Date();
  constructor() { }

  ngOnInit(): void {
    this.locationId = localStorage.getItem('empLocationId');
  }

  getfileType(event) {
    this.fileType = +event.target.value;
  }

  onLocationChange(event) {
    this.locationId = +event;
  }

}
