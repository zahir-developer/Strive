import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-add-schedule',
  templateUrl: './add-schedule.component.html'
})
export class AddScheduleComponent implements OnInit {
  startTime: Date = new Date();
  constructor() { }

  ngOnInit(): void {
  }

}
