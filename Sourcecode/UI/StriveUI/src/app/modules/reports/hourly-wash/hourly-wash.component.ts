import { Component, OnInit } from '@angular/core';
import * as moment from 'moment';
import { ReportsService } from 'src/app/shared/services/data-service/reports.service';

@Component({
  selector: 'app-hourly-wash',
  templateUrl: './hourly-wash.component.html',
  styleUrls: ['./hourly-wash.component.css']
})
export class HourlyWashComponent implements OnInit {
  locationId: any;
  fileType: number;
  todayDate = new Date();
  startDate: any;
  currentWeek: any;
  endDate: any;
  dateRange: any = [];
  daterangepickerModel: any;
  hourlyWashReport: any = [];
  constructor(
    private reportsService: ReportsService
  ) { }

  ngOnInit(): void {
    this.locationId = localStorage.getItem('empLocationId');
    this.weeklyDateAssign();
  }

  weeklyDateAssign() {
    const currentDate = new Date();
    const first = currentDate.getDate() - currentDate.getDay();
    const last = first + 6;
    this.startDate = new Date(currentDate.setDate(first));
    this.currentWeek = this.startDate;
    this.endDate = new Date(currentDate.setDate(last));
    this.endDate = this.endDate.setDate(this.startDate.getDate() + 6);
    this.endDate = new Date(moment(this.endDate).format());
    this.daterangepickerModel = [this.startDate, this.endDate];
  }

  getfileType(event) {
    this.fileType = +event.target.value;
  }

  onLocationChange(event) {
    this.locationId = +event;
  }

  onValueChange(event) {
    console.log(event, 'start');
    if (event !== null) {
      this.startDate = event[0];
      this.endDate = event[1];
    }
  }

  viewHourlyReport() {
    // 2034, '2020-11-16', '2020-11-17'
    const finalObj = {
      locationId: 2034, // +this.locationId,
      fromDate: '2020-11-16',   // moment(this.startDate).format(),
      endDate: '2020-11-17'  // moment(this.endDate).format()
    };
    this.reportsService.getHourlyWashReport(finalObj).subscribe(res => {
      if (res.status === 'Success') {
        const hourlyRate = JSON.parse(res.resultData);
        console.log(hourlyRate, 'hourly');
        if (hourlyRate.GetHourlyWashReport.WashHoursViewModel !== null) {
          this.hourlyWashReport = hourlyRate.GetHourlyWashReport.WashHoursViewModel;
        }
      }
    });
  }

}
