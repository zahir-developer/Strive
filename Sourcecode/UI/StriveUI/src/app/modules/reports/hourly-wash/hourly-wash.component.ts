import { Component, OnInit } from '@angular/core';
import * as moment from 'moment';
import { merge } from 'rxjs/operators';
import { ReportsService } from 'src/app/shared/services/data-service/reports.service';
import * as _ from "underscore";
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
  hourlyWashReportDay: any;
  washModel: any = []; 
  mergedList: any = [];
  SalesSummaryModel: any = [];
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
        console.log(hourlyRate.GetHourlyWashReport.LocationWashServiceViewModel)
this.washModel = hourlyRate.GetHourlyWashReport.LocationWashServiceViewModel;
this.SalesSummaryModel = hourlyRate.GetHourlyWashReport.SalesSummaryViewModel;

// this.mergedList= _.map(this.washModel, function(member){
//   return _.extend(member, _.omit(_.findWhere(this.SalesSummaryModel, {id: moment(member.JobDate).format('MM-DD-YYYY')}), 'JobDate'));
// });
// console.log(this.SalesSummaryModel , 'newsales')
// for(var i = 0; i < this.washModel.length; i++){
//   for(var j = 0; j < this.SalesSummaryModel.length; i++){


//   this.mergedList.push({
//     JobDate:this.washModel[i].JobDate,
//     LocationId: this.washModel[i].LocationId,
//     LocationName: this.washModel[i].LocationName,
//     ServiceId: this.washModel[i].ServiceId,
//     ServiceName: this.washModel[i].ServiceName,
//     WashCount: this.washModel[i].WashCount,
//     Account: this.SalesSummaryModel[j].Account,
//     Balance: this.SalesSummaryModel[j].Balance,
//     Cash:this.SalesSummaryModel[j].Cash,
//     CashBack: this.SalesSummaryModel[j].CashBack,
//     Credit: this.SalesSummaryModel[j].Credit,
//     Discount: this.SalesSummaryModel[j].Discount,
//     GiftCard:this.SalesSummaryModel[j].GiftCard,
//     GrandTotal: this.SalesSummaryModel[j].GrandTotal,
//     TotalPaid: this.SalesSummaryModel[j].TotalPaid
  
//   })

//   console.log(this.mergedList , 'merged')
// }
// }

        console.log(hourlyRate, 'hourly');
        if (hourlyRate.GetHourlyWashReport.WashHoursViewModel !== null) {
          this.hourlyWashReport = hourlyRate.GetHourlyWashReport.WashHoursViewModel;
         
        }
      
      }

    });
  }

}
