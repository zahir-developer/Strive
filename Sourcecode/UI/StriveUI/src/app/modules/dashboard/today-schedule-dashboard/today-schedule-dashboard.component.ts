import { Component, OnInit, Input } from '@angular/core';
import { DashboardService } from 'src/app/shared/services/data-service/dashboard.service';
import { DatePipe } from '@angular/common';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-today-schedule-dashboard',
  templateUrl: './today-schedule-dashboard.component.html'
})
export class TodayScheduleDashboardComponent implements OnInit {
  todayScheduleDetail: any = [];
  currentDate = new Date();
  @Input() locationId?: any;
  constructor(
    public dashboardService: DashboardService,
    private datePipe: DatePipe,
    private toastr: ToastrService
    ) { }

  ngOnInit(): void {
    this.getScheduleDetailsByDate();
  }

  getScheduleDetailsByDate() {
    const todayDate = this.datePipe.transform(this.currentDate, 'yyyy-MM-dd');
    const locationId = this.locationId === 0 ? null : this.locationId;
    const clientID = null;
    this.dashboardService.getTodayDateScheduleList(todayDate, locationId, clientID).subscribe( res => {
      if (res.status === 'Success') {
        const scheduleDetails = JSON.parse(res.resultData);
        if (scheduleDetails.DetailsGrid.BayJobDetailViewModel !== null) {
          this.todayScheduleDetail = scheduleDetails.DetailsGrid.BayJobDetailViewModel;
          if (this.todayScheduleDetail?.length > 0) {
            for (let i = 0; i < this.todayScheduleDetail.length; i++) {
              this.todayScheduleDetail[i].VehicleModel == 'None' ? this.todayScheduleDetail[i].VehicleModel =  'Unk' : this.todayScheduleDetail[i].VehicleModel ;
            }
          }
        }
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    })
  }

}
