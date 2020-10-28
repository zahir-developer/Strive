import { Component, OnInit } from '@angular/core';
import { DashboardService } from 'src/app/shared/services/data-service/dashboard.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-today-schedule-dashboard',
  templateUrl: './today-schedule-dashboard.component.html',
  styleUrls: ['./today-schedule-dashboard.component.css']
})
export class TodayScheduleDashboardComponent implements OnInit {
  todayScheduleDetail: any = [];
  currentDate = new Date();
  constructor(
    public dashboardService: DashboardService,
    private datePipe: DatePipe
    ) { }

  ngOnInit(): void {
    this.getScheduleDetailsByDate();
  }

  getScheduleDetailsByDate() {
    const todayDate = this.datePipe.transform(this.currentDate, 'yyyy-MM-dd');
    const locationId = localStorage.getItem('empLocationId');
    this.dashboardService.getTodayDateScheduleList(todayDate, locationId).subscribe( res => {
      if (res.status === 'Success') {
        const scheduleDetails = JSON.parse(res.resultData);
        console.log(scheduleDetails, 'todayList');
        if (scheduleDetails.DetailsGrid.BayJobDetailViewModel !== null) {
          this.todayScheduleDetail = scheduleDetails.DetailsGrid.BayJobDetailViewModel;
        }
      }
    });
  }

}
