import { Component, OnInit } from '@angular/core';
import { TimeClockMaintenanceService } from 'src/app/shared/services/data-service/time-clock-maintenance.service';

@Component({
  selector: 'app-time-clock-maintenance',
  templateUrl: './time-clock-maintenance.component.html',
  styleUrls: ['./time-clock-maintenance.component.css']
})
export class TimeClockMaintenanceComponent implements OnInit {
  totalWeekDetail: any = {
    CollisionAmount: 0,
    DetailAmount: 0,
    DetailRate: 0,
    GrandTotal: 0,
    OverTimeHours: 0,
    OverTimePay: 0,
    TotalDetailHours: 0,
    TotalWashHours: 0,
    WashAmount: 0,
    WashRate: 0
  };
  timeClockList: any;
  weekDays = [ 'Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday' ];
  constructor(
    public timeClockMaintenanceService: TimeClockMaintenanceService
  ) { }

  ngOnInit(): void {
    this.getTimeClockWeekDetails();
  }

  getTimeClockWeekDetails() {
    const employeeId = 1;
    const locationId = 1;
    const startDate = '2020-08-09';
    const endDate = '2020-08-15';
    const inputParams = {
      employeeId,
      locationId,
      startDate,
      endDate
    };
    this.timeClockMaintenanceService.getTimeClockWeekDetails(inputParams).subscribe(res => {
      if (res.status === 'Success') {
        const weekDetails = JSON.parse(res.resultData);
        console.log(weekDetails, 'weekDetails');
        if (weekDetails.Result.TimeClockWeek !== null) {
          this.totalWeekDetail = weekDetails.Result.TimeClockWeek;
        }
        if (weekDetails.Result.TimeClock !== null) {
          this.weekDays.forEach( day => {
            const days = _.where(weekDetails.Result.TimeClock, { Day: day });
            if (days.length > 0) {
              const dayDetails = [];
              let count = 0;
              days.forEach( item => {
                count = count + 1;
                dayDetails.push(item);
              });
            }
          });
        }
      }
    });
  }

}
