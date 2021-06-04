import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { CustomerService } from 'src/app/shared/services/data-service/customer.service';
import * as moment from 'moment';
import { DatePipe } from '@angular/common';
import { DashboardService } from 'src/app/shared/services/data-service/dashboard.service';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { DetailService } from 'src/app/shared/services/data-service/detail.service';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageConfig } from 'src/app/shared/services/messageConfig';

@Component({
  selector: 'app-customer-dashboard',
  templateUrl: './customer-dashboard.component.html'
})
export class CustomerDashboardComponent implements OnInit {
  @Output() selectServcie = new EventEmitter();
  vechicleList: any = [];
  @Input() scheduleDetailObj?: any;
  serviceList: any = [];
  todayScheduleDetail: any = [];
  @Output() editSchedule = new EventEmitter();
  clonedVechicleList = [];
  sort = { column: 'JobDate', descending: true };
  sortColumn: { column: string; descending: boolean; };
  pastSort = { column: 'JobDate', descending: true };
  pastSortColumn: { column: string; descending: boolean; };
  pastScheduleDetail = [];
  clonedPastScheduleDetail = [];
  clientID: any;
  page = 1;
  pageSize = 10;
  collectionSize: number = 0;
  constructor(
    private customerService: CustomerService,
    private datePipe: DatePipe,
    public dashboardService: DashboardService,
    private confirmationService: ConfirmationUXBDialogService,
    private detailService: DetailService,
    private toastr: MessageServiceToastr,
    private route: ActivatedRoute,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    const paramsData = this.route.snapshot.queryParamMap.get('clientId');
    if (paramsData !== null) {
      this.clientID = paramsData;
    }
    else
    {
      this.clientID = +localStorage.getItem('clientId');
    }
    
    this.getScheduleDetail();
    this.getVehicleListByClientId();
  }

  schedule(vechicle) {
    this.scheduleDetailObj.vechicleDetail = vechicle;
    this.selectServcie.emit(vechicle);
  }

  getVehicleListByClientId() {
    const clientID = this.clientID ? this.clientID : 0;
    this.customerService.getVehicleByClientId(clientID).subscribe(res => {
      if (res.status === 'Success') {
        const vechicle = JSON.parse(res.resultData);
        this.vechicleList = vechicle.Status;
        this.vechicleList.forEach(item => {
          item.VechicleName = item.VehicleMfr + ' ' + item.VehicleModel + ' ' + item.VehicleColor;
        });
        this.clonedVechicleList = this.vechicleList.map(x => Object.assign({}, x));
      }
    }, (err) => {
      this.toastr.showMessage({ severity: 'error', title: 'Error!', body: MessageConfig.CommunicationError });
    });
  }

  searchVechicleList(text) {
    if (text.length > 0) {
      this.vechicleList = this.clonedVechicleList.filter(item => item.VechicleName.toLowerCase().includes(text));
    } else {
      this.vechicleList = [];
      this.vechicleList = this.clonedVechicleList;
    }
  }

  searchHistory(text) {
    if (text.length > 0) {
      this.pastScheduleDetail = this.clonedPastScheduleDetail.filter(item => item.searchData.toLowerCase().includes(text));
    } else {
      this.pastScheduleDetail = [];
      this.pastScheduleDetail = this.clonedPastScheduleDetail;
    }
  }

  getScheduleDetail() {
    const currentDate = new Date();
    const todayDate = null;
    const locationId = null;
    const clientID = this.clientID ? this.clientID : 0;
    this.spinner.show();
    this.dashboardService.getTodayDateScheduleList(todayDate, locationId, clientID).subscribe(res => {
      this.spinner.hide();
      if (res.status === 'Success') {
        const scheduleDetails = JSON.parse(res.resultData);
        this.pastScheduleDetail = [];
        this.todayScheduleDetail = [];
        if (scheduleDetails.DetailsGrid.BayJobDetailViewModel !== null) {
          scheduleDetails.DetailsGrid.BayJobDetailViewModel.forEach(item => {
            if (this.datePipe.transform(currentDate, 'dd-MM-yyyy') === this.datePipe.transform(item.JobDate, 'dd-MM-yyyy')) {
              this.todayScheduleDetail.push(item);
              if (this.todayScheduleDetail?.length > 0) {
                for (let i = 0; i < this.todayScheduleDetail.length; i++) {
                  this.todayScheduleDetail[i].VehicleModel === 'None' ?
                   this.todayScheduleDetail[i].VehicleModel = 'Unk' : this.todayScheduleDetail[i].VehicleModel;
                }
              }
            } else if (currentDate < new Date(item.JobDate)) {
              this.todayScheduleDetail.push(item);
            } else {
              this.pastScheduleDetail.push(item);
            }
          });
          if (this.pastScheduleDetail.length > 0) {
            this.pastScheduleDetail.forEach(item => {
              item.searchData = item.TicketNumber + ' ' + this.datePipe.transform(item.JobDate, 'MM/dd/yyyy') + ' ' + item.LocationName
                + ' ' + item.VehicleMake + ' ' + item.VehicleModel + ' ' + item.VehicleColor + ' ' + item.ServiceTypeName;
            });
            this.clonedPastScheduleDetail = this.pastScheduleDetail.map(x => Object.assign({}, x));
          }
          this.collectionSize = Math.ceil(this.pastScheduleDetail.length / this.pageSize) * 10;
        }
      }
      else {
      }
    }, (err) => {
      this.spinner.hide();
      this.toastr.showMessage({ severity: 'error', title: 'Error!', body: MessageConfig.CommunicationError });
    });
  }

  deleteDetail(detail) {
    this.confirmationService.confirm('Delete Employee', `Are you sure you want to delete`, 'Confirm', 'Cancel')
      .then((confirmed) => {
        if (confirmed === true) {
          this.confirmDelete(detail.JobId);
        }
      })
      .catch(() => { });
  }

  confirmDelete(jobID) {
    this.spinner.show();
    this.detailService.deleteDetail(jobID).subscribe(res => {
      if (res.status === 'Success') {
        this.spinner.hide();

        this.getScheduleDetail();
        this.toastr.showMessage({ severity: 'success', title: 'Success', body: MessageConfig.Customer.Delete });
      }
      else {
        this.toastr.showMessage({ severity: 'error', title: 'Error!', body: MessageConfig.CommunicationError });

        this.spinner.hide();

      }
    }, (err) => {
      this.toastr.showMessage({ severity: 'error', title: 'Error!', body: MessageConfig.CommunicationError });
      this.spinner.hide();
    });
  }

  updateSchedule(service) {
    this.editSchedule.emit(service.JobId);
  }

  changeSorting(column) {
    this.changeSortingDescending(column, this.sort);
    this.sortColumn = this.sort;
  }

  changePastSorting(column) {
    this.changePastSortingDescending(column, this.pastSort);
    this.pastSortColumn = this.pastSort;
  }

  changeSortingDescending(column, sortingInfo) {
    if (sortingInfo.column === column) {
      sortingInfo.descending = !sortingInfo.descending;
    } else {
      sortingInfo.column = column;
      sortingInfo.descending = false;
    }
    return sortingInfo;
  }

  changePastSortingDescending(column, sortingInfo) {
    if (sortingInfo.column === column) {
      sortingInfo.descending = !sortingInfo.descending;
    } else {
      sortingInfo.column = column;
      sortingInfo.descending = false;
    }
    return sortingInfo;
  }

  sortedColumnCls(column, sortingInfo) {
    if (column === sortingInfo.column && sortingInfo.descending) {
      return 'fa-sort-desc';
    } else if (column === sortingInfo.column && !sortingInfo.descending) {
      return 'fa-sort-asc';
    }
    return '';
  }

  sortedPastColumnCls(column, sortingInfo) {
    if (column === sortingInfo.column && sortingInfo.descending) {
      return 'fa-sort-desc';
    } else if (column === sortingInfo.column && !sortingInfo.descending) {
      return 'fa-sort-asc';
    }
    return '';
  }

  selectedCls(column) {
    return this.sortedColumnCls(column, this.sort);
  }

  selectedPastCls(column) {
    return this.sortedPastColumnCls(column, this.pastSort);
  }

}
