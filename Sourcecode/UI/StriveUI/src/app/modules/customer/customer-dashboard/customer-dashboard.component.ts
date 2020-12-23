import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { CustomerService } from 'src/app/shared/services/data-service/customer.service';
import * as moment from 'moment';
import { DatePipe } from '@angular/common';
import { DashboardService } from 'src/app/shared/services/data-service/dashboard.service';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { DetailService } from 'src/app/shared/services/data-service/detail.service';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';

@Component({
  selector: 'app-customer-dashboard',
  templateUrl: './customer-dashboard.component.html',
  styleUrls: ['./customer-dashboard.component.css']
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
  pastScheduleDetail = [];
  clonedPastScheduleDetail = [];
  constructor(
    private customerService: CustomerService,
    private datePipe: DatePipe,
    public dashboardService: DashboardService,
    private confirmationService: ConfirmationUXBDialogService,
    private detailService: DetailService,
    private toastr: MessageServiceToastr
  ) { }

  ngOnInit(): void {
    this.getScheduleDetail();
    this.getVehicleListByClientId();
  }

  getDailySalesReport() {
    const finalObj = {
      date: moment(new Date()).format('MM/DD/YYYY'),
      locationId: 0
    };
    this.customerService.getDailySalesReport(finalObj).subscribe(res => {
      if (res.status === 'Success') {
        const sales = JSON.parse(res.resultData);
        this.serviceList = sales.GetDailySalesReport;
        console.log(sales, 'customer');
      }
    });
  }

  schedule(vechicle) {
    this.scheduleDetailObj.vechicleDetail = vechicle;
    this.selectServcie.emit(vechicle);
  }

  getVehicleListByClientId() {
    this.customerService.getVehicleByClientId(115).subscribe(res => {
      if (res.status === 'Success') {
        const vechicle = JSON.parse(res.resultData);
        this.vechicleList = vechicle.Status;
        this.vechicleList.forEach(item => {
          item.VechicleName = item.VehicleMfr + ' ' + item.VehicleModel + ' ' + item.VehicleColor;
        });
        this.clonedVechicleList = this.vechicleList.map(x => Object.assign({}, x));
        console.log(vechicle, 'vechicle');
      }
    });
  }

  searchVechicleList(text) {
    console.log(text);
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
    const todayDate = null; // this.datePipe.transform(currentDate, 'yyyy-MM-dd');
    const locationId = null; // 2033;
    const clientID = 115;
    this.dashboardService.getTodayDateScheduleList(todayDate, locationId, clientID).subscribe(res => {
      if (res.status === 'Success') {
        const scheduleDetails = JSON.parse(res.resultData);
        this.pastScheduleDetail = [];
        this.todayScheduleDetail = [];
        if (scheduleDetails.DetailsGrid.BayJobDetailViewModel !== null) {
          // this.todayScheduleDetail = scheduleDetails.DetailsGrid.BayJobDetailViewModel;
          scheduleDetails.DetailsGrid.BayJobDetailViewModel.forEach(item => {
            if (this.datePipe.transform(currentDate, 'dd-MM-yyyy') === this.datePipe.transform(item.JobDate, 'dd-MM-yyyy')) {
              this.todayScheduleDetail.push(item);
            } else if (currentDate < new Date(item.JobDate)) {
              this.todayScheduleDetail.push(item);
            } else {
              this.pastScheduleDetail.push(item);
            }
          });
          console.log(this.todayScheduleDetail, this.pastScheduleDetail, 'scheduleDetails');
          if (this.pastScheduleDetail.length > 0) {
            this.pastScheduleDetail.forEach(item => {
              item.searchData = item.TicketNumber + ' ' + this.datePipe.transform(item.JobDate, 'MM/dd/yyyy') + ' ' + item.LocationName
                + ' ' + item.VehicleMake + ' ' + item.VehicleModel + ' ' + item.VehicleColor + ' ' + item.ServiceTypeName;
            });
            this.clonedPastScheduleDetail = this.pastScheduleDetail.map(x => Object.assign({}, x));
          }
        }
      }
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
    this.detailService.deleteDetail(jobID).subscribe(res => {
      if (res.status === 'Success') {
        this.getScheduleDetail();
        this.toastr.showMessage({ severity: 'success', title: 'Success', body: 'Record deleted Successfully!!' });
      }
    });
  }

  updateSchedule(service) {
    this.editSchedule.emit(service.JobId);
  }

  changeSorting(column) {
    this.changeSortingDescending(column, this.sort);
    this.sortColumn = this.sort;
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
  sortedColumnCls(column, sortingInfo) {
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

}
