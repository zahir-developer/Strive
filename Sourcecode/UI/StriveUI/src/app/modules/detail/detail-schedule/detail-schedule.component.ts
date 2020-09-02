import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-detail-schedule',
  templateUrl: './detail-schedule.component.html',
  styleUrls: ['./detail-schedule.component.css']
})
export class DetailScheduleComponent implements OnInit {
  showDialog: boolean;
  constructor() { }

  ngOnInit(): void {
    this.showDialog = false;
  }

  addNewDetail() {
    this.showDialog = true;
  }

}
