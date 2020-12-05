import { Component, OnInit, Input } from '@angular/core';
import { DetailService } from 'src/app/shared/services/data-service/detail.service';
import { DatePipe } from '@angular/common';
import * as _ from 'underscore';

@Component({
  selector: 'app-today-schedule',
  templateUrl: './today-schedule.component.html',
  styleUrls: ['./today-schedule.component.css']
})
export class TodayScheduleComponent implements OnInit {
  bayDetail: any = [];
  currentDate = new Date();
  @Input() selectedDate?: any;
  isView: boolean;
  actionType: string;
  selectedData: any;
  isEdit: boolean;
  showDialog: boolean;
  constructor(
    private detailService: DetailService,
    private datePipe: DatePipe
  ) { }

  ngOnInit(): void {
    this.getTodayDateScheduleList();
  }

  getTodayDateScheduleList() {
    const todayDate = this.datePipe.transform(this.selectedDate, 'yyyy-MM-dd');
    const locationId = localStorage.getItem('empLocationId');
    const finalObj = {
      jobDate: todayDate,
      locationId
    };
    this.detailService.getTodayDateScheduleList(todayDate, locationId).subscribe(res => {
      if (res.status === 'Success') {
        const scheduleDetails = JSON.parse(res.resultData);
        console.log(scheduleDetails, 'todayList');
        const detailGrid = scheduleDetails.DetailsGrid;
        // this.bayDetail = detailGrid.BayDetailViewModel;
        const bayJobDetail = [];
        if (detailGrid.BayJobDetailViewModel !== null) {
          detailGrid.BayDetailViewModel.forEach(item => {
            const isData = _.where(detailGrid.BayJobDetailViewModel, { BayId: item.BayId });
            if (isData.length > 0) {
              const services = [];
              const detailService = _.where(isData, { ServiceTypeName: 'Details' });
              const outsideService = _.where(isData, { ServiceTypeName: 'Outside Services' });
              detailService.forEach(service => {
                // if (outsideService.length > 0) {
                const sameJobId = _.where(outsideService, { JobId: service.JobId });
                // outsideService.forEach(outside => {
                // if (service.JobId === outside.JobId) {
                services.push({
                  BayId: service.BayId,
                  BayName: service.BayName,
                  ClientName: service.ClientName,
                  EstimatedTimeOut: service.EstimatedTimeOut,
                  JobId: service.JobId,
                  PhoneNumber: service.PhoneNumber,
                  ServiceName: service.ServiceName,
                  TicketNumber: service.TicketNumber,
                  TimeIn: service.TimeIn,
                  OutsideService: sameJobId.length > 0 ? sameJobId[0].ServiceName : 'None'
                });
                // }
                // });
                // } else {
                //   services.push({
                //     BayId: service.BayId,
                //     BayName: service.BayName,
                //     ClientName: service.ClientName,
                //     EstimatedTimeOut: service.EstimatedTimeOut,
                //     JobId: service.JobId,
                //     PhoneNumber: service.PhoneNumber,
                //     ServiceName: service.ServiceName,
                //     TicketNumber: service.TicketNumber,
                //     TimeIn: service.TimeIn,
                //     OutsideService: 'None'
                //   });
                // }
              });
              console.log(services, 'servcei');
              bayJobDetail.push({
                BayId: item.BayId,
                BayDetail: services
              });
            } else {
              bayJobDetail.push({
                BayId: item.BayId,
                BayDetail: isData
              });
            }
          });
        } else if (detailGrid.BayJobDetailViewModel === null) {
          detailGrid.BayDetailViewModel.forEach(item => {
            bayJobDetail.push({
              BayId: item.BayId,
              BayDetail: []
            });
          });
        }

        bayJobDetail.forEach(bay => {
          let isJobID = false;
          bay.BayDetail.forEach(detail => {
            if (detail.JobId === null) {
              isJobID = true;
            } else {
              isJobID = false;
            }
          });
          if (isJobID) {
            bay.totalCount = 0;
            bay.BayDetail = [];
          } else {
            bay.totalCount = bay.BayDetail.length;
          }
        });
        console.log(bayJobDetail, 'bayjb');
        this.bayDetail = bayJobDetail;
      }
    });
  }

  getDetailByID(bay) {
    console.log(bay,bay.JobId);
    const currentDate = new Date();
    if (this.datePipe.transform(currentDate, 'dd-MM-yyyy') === this.datePipe.transform(this.selectedDate, 'dd-MM-yyyy')) {
      this.isView = false;
    } else if (currentDate < this.selectedDate) {
      this.isView = false;
    } else {
      this.isView = true;
    }
    this.actionType = 'Edit Detail';
    this.detailService.getDetailById(bay.JobId).subscribe(res => {
      if (res.status === 'Success') {
        const details = JSON.parse(res.resultData);
        console.log(details, 'details');
        this.selectedData = details.DetailsForDetailId;
        this.isEdit = true;
        this.showDialog = true;
      }
    });
  }

  closeModal() {
    this.showDialog = false;
    this.isEdit = false;
    this.isView = false;    
    this.getTodayDateScheduleList();
  }
  closeDialog(event) {
    this.isEdit = event.isOpenPopup;
    this.showDialog = event.isOpenPopup;
  }

}
