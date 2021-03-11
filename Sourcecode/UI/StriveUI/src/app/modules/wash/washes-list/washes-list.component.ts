import { Component, OnInit, ViewChild, ChangeDetectorRef } from '@angular/core';
import { VehicleService } from 'src/app/shared/services/data-service/vehicle.service';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { WashService } from 'src/app/shared/services/data-service/wash.service';
import { Router } from '@angular/router';
import { datepickerAnimation } from 'ngx-bootstrap/datepicker/datepicker-animations';
import { DatePipe } from '@angular/common';
import * as moment from 'moment';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { LandingService } from 'src/app/shared/services/common-service/landing.service';
import { DashboardStaticsComponent } from 'src/app/shared/components/dashboard-statics/dashboard-statics.component';
import { DetailService } from 'src/app/shared/services/data-service/detail.service';
import { BsDaterangepickerDirective, BsDatepickerConfig } from 'ngx-bootstrap/datepicker';

@Component({
  selector: 'app-washes-list',
  templateUrl: './washes-list.component.html',
  styleUrls: ['./washes-list.component.css']
})
export class WashesListComponent implements OnInit {
  washDetails = [];
  showDialog = false;
  selectedData: any;
  @ViewChild('dp', { static: false }) datepicker: BsDaterangepickerDirective;
  headerData: string;
  isEdit: boolean;
  isTableEmpty: boolean;
  isView: boolean;
  collectionSize: number = 0;
  dashboardDetails: any;
  locationId = +localStorage.getItem('empLocationId');
  TimeInFormat: any;
  washListDetails = [];

  pageSizeList: number[];
  page: number;
  pageSize: number;
  search: any = null;

  jobTypeId: any;
  maxDate = new Date()
  @ViewChild(DashboardStaticsComponent) dashboardStaticsComponent: DashboardStaticsComponent;
  daterangepickerModel: any;
  currentWeek: any;
  startDate: any;
  endDate: any;
  sortColumn: { sortBy: string; sortOrder: string; };
  constructor(private washes: WashService, private toastr: ToastrService,
    private datePipe: DatePipe, private spinner: NgxSpinnerService,
    private confirmationService: ConfirmationUXBDialogService, private router: Router
    , private landingservice: LandingService, private detailService: DetailService,
    private cd: ChangeDetectorRef,) { }

  ngOnInit() {
    this.sortColumn = { sortBy: ApplicationConfig.Sorting.SortBy.Washes, sortOrder: ApplicationConfig.Sorting.SortOrder.Washes.order };

    const currentDate = new Date();
    const first = currentDate.getDate();
    const last = first - 7;
    this.startDate = new Date(currentDate.setDate(last));
    this.currentWeek = this.startDate;
    const lastDate = new Date();
    this.endDate = new Date(lastDate);
    this.daterangepickerModel = [this.startDate, this.endDate];
    this.page = ApplicationConfig.PaginationConfig.page;
    this.pageSize = ApplicationConfig.PaginationConfig.TableGridSize;
    this.pageSizeList = ApplicationConfig.PaginationConfig.Rows;
    const obj = {
      id: this.locationId,
      date: new Date()
    };
    // this.washes.getDashBoard(obj);
    this.getAllWashDetails();
  }
  landing() {
    this.landingservice.loadTheLandingPage();
  }
  paginate(event) {

    this.pageSize = +this.pageSize;
    this.page = event;

    this.getAllWashDetails();
  }
  paginatedropdown(event) {
    this.pageSize = +event.target.value;
    this.page = this.page;
    this.getAllWashDetails();
  }
  onValueChange(event) {
    if (event !== null) {
      this.startDate = event[0];
      this.endDate = event[1];
      this.getAllWashDetails();
    }
    else {
      this.startDate = null;
      this.endDate = null;
    }
  }
  // Get All Washes
  getAllWashDetails() {
    const obj = {
      LocationId: this.locationId,
      PageNo: this.page,
      PageSize: this.pageSize,
      Query: this.search == "" ? null : this.search,
      SortOrder: this.sortColumn.sortOrder,
      SortBy: this.sortColumn.sortBy,
      StartDate: this.startDate,
      EndDate: this.endDate
    };
    this.spinner.show();
    this.washes.getAllWashes(obj).subscribe(data => {
      this.spinner.hide();
      if (data.status === 'Success') {
        const wash = JSON.parse(data.resultData);
        this.getJobType();
        if (wash.Washes !== null) {
          this.washDetails = wash?.Washes?.AllWashesViewModel;
          const totalRowCount = wash?.Washes?.Count?.Count;
          if (this.washDetails?.length > 0) {
            for (let i = 0; i < this.washDetails.length; i++) {
              let hh = this.washDetails[i].TimeIn.substring(13, 11);
              let m = this.washDetails[i].TimeIn.substring(16, 14);
              var s = this.washDetails[i].TimeIn.substring(19, 17);
              let min = m;
              let sec = s;
              var dd;
              var hr;
              if (hh > 12) {
                hr = hh - 12;
                dd = "PM";
              }
              else {
                hr = hh;
                dd = "AM"
              }
              if (hh == 0) {
                hh = 12;
              }
              let inTimeFormat = hr + ":" + min + ":" + sec + dd;
              let outhh = this.washDetails[i]?.EstimatedTimeOut ? this.washDetails[i].EstimatedTimeOut.substring(13, 11) : null;
              let outm = this.washDetails[i]?.EstimatedTimeOut ? this.washDetails[i].EstimatedTimeOut.substring(16, 14) : null;
              var outs = this.washDetails[i]?.EstimatedTimeOut ? this.washDetails[i].EstimatedTimeOut.substring(19, 17) : null;
              let outmin = outm;

              let outsec = outs;
              var outdd;
              var outhr;
              if (outhh > 12) {
                outhr = outhh - 12;
                outdd = "PM";
              }
              else {
                outhr = outhh
                outdd = "AM"
              }
              if (outhh == 0) {
                outhh = 12;
              }
              let outTimeFormat = outhr + ":" + outmin + ":" + outsec + outdd;
              this.washDetails.forEach(item => {
                item.EstimatedTimeOutFormat = outTimeFormat,
                  item.TimeInFormat = inTimeFormat;
              });
            }
          }
          if (this.washDetails?.length === 0 || this.washDetails == null) {
            this.isTableEmpty = true;
          } else {
            this.collectionSize = Math.ceil(totalRowCount / this.pageSize) * 10;
            this.isTableEmpty = false;
          }
        }
      } else {
        this.isTableEmpty === false
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.spinner.hide();
    });
  }
  edit(data) {
    this.selectedData = data;
    this.showDialog = true;
  }
  delete(data) {
    this.confirmationService.confirm('Delete Wash', `Are you sure you want to delete this Wash? All related 
    information will be deleted and the Wash cannot be retrieved?`, 'Yes', 'No')
      .then((confirmed) => {
        if (confirmed === true) {
          this.confirmDelete(data);
        }
      })
      .catch(() => { });
  }

  // Delete Wash
  confirmDelete(data) {
    this.washes.deleteWash(data.JobId).subscribe(res => {
      if (res.status === 'Success') {
        this.toastr.success(MessageConfig.Wash.Delete, 'Success!');
        this.sortColumn = { sortBy: ApplicationConfig.Sorting.SortBy.Washes, sortOrder: ApplicationConfig.Sorting.SortOrder.Washes.order };

        this.getAllWashDetails();
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    });
  }
  closePopupEmit(event) {
    if (event.status === 'saved') {
      this.sortColumn = { sortBy: ApplicationConfig.Sorting.SortBy.Washes, sortOrder: ApplicationConfig.Sorting.SortOrder.Washes.order };

      const obj = {
        id: this.locationId,
        date: new Date()
      };
      this.washes.getDashBoard(obj);
      this.getAllWashDetails();
    }
    this.showDialog = event.isOpenPopup;
  }

  add(data, washDetails?) {
    if (data === 'add') {
      this.headerData = 'Add New Service';
      this.selectedData = washDetails;
      this.isEdit = false;
      this.isView = false;
      this.showDialog = true;
    } else {
      this.getWashById(data, washDetails);
    }
  }

  // Get Wash By Id
  getWashById(label, washDet) {
    this.spinner.show();
    this.washes.getWashById(washDet.JobId).subscribe(data => {
      if (data.status === 'Success') {
        const wash = JSON.parse(data.resultData);
        if (label === 'edit') {
          this.headerData = 'Edit Service';
          this.selectedData = wash.WashesDetail;
          this.isEdit = true;
          this.isView = false;
          this.showDialog = true;
          this.spinner.hide();
        } else {
          this.headerData = 'View Service';
          this.selectedData = wash.WashesDetail;
          this.isEdit = true;
          this.isView = true;
          this.showDialog = true;
          this.spinner.hide();
        }
      } else {
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    });
  }

  pay(wash) {
    this.router.navigate(['/sales'], { queryParams: { ticketNumber: wash.TicketNumber } });
  }

  changeSorting(column) {
    this.sortColumn = {
      sortBy: column,
      sortOrder: this.sortColumn.sortOrder === 'ASC' ? 'DESC' : 'ASC'
    };
    this.selectedCls(this.sortColumn);
    this.getAllWashDetails();
  }



  selectedCls(column) {
    if (column === this.sortColumn.sortBy && this.sortColumn.sortOrder === 'DESC') {
      return 'fa-sort-desc';
    } else if (column === this.sortColumn.sortBy && this.sortColumn.sortOrder === 'ASC') {
      return 'fa-sort-asc';
    }
    return '';
  }
  getJobType() {
    this.detailService.getJobType().subscribe(res => {
      if (res.status === 'Success') {
        const jobtype = JSON.parse(res.resultData);
        if (jobtype.GetJobType.length > 0) {
          jobtype.GetJobType.forEach(item => {
            if (item.valuedesc === 'Wash') {
              this.jobTypeId = item.valueid;
              if (this.dashboardStaticsComponent?.jobTypeId) {
                this.dashboardStaticsComponent.jobTypeId = this.jobTypeId;
                this.dashboardStaticsComponent.getDashboardDetails();

              }
            }
          });
        }
      }
    });
  }

}
