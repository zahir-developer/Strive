import { Component, OnInit } from '@angular/core';
import { TimeClockMaintenanceService } from 'src/app/shared/services/data-service/time-clock-maintenance.service';

@Component({
  selector: 'app-time-clock-maintenance',
  templateUrl: './time-clock-maintenance.component.html',
  styleUrls: ['./time-clock-maintenance.component.css']
})
export class TimeClockMaintenanceComponent implements OnInit {

  constructor(
    public timeClockMaintenanceService: TimeClockMaintenanceService
  ) { }

  ngOnInit(): void {
  }

}
