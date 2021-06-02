import { Component, OnInit } from '@angular/core';
import { TimeClockMaintenanceService } from 'src/app/shared/services/data-service/time-clock-maintenance.service';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import * as moment from 'moment';
import { DatePipe } from '@angular/common';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-time-clock-maintenance',
  templateUrl: './time-clock-maintenance.component.html'
})
export class TimeClockMaintenanceComponent implements OnInit {

  timeClockEmployeeDetails = [];
  isLoading = true;

  collectionSize: number = 0;
  isTimeClockEmpty = false;
  timeClockEmployeeDetailDto =
    {
      locationId: 1,
      startDate: '2020-08-15',
      endDate: '2020-09-09'
    }
  objDelete: any;
  empClockInObj: any;
  isTimeClockWeekPage: boolean;
  daterangepickerModel: any;
  employeeList: any = [];
  selectedEmployee: any;
  startDate: any;
  currentWeek: any;
  endDate: any;
  dateRange: any = [];
  isView: boolean = false;

  pageSizeList: number[];
  page: number;
  pageSize: number;
  sortColumn: { sortBy: any; sortOrder: string; };
  constructor(
    private timeClockMaintenanceService: TimeClockMaintenanceService,
    private toastr: ToastrService,
    private confirmationService: ConfirmationUXBDialogService,
    private uiLoaderService: NgxUiLoaderService,
    private datePipe: DatePipe,
    private spinner : NgxSpinnerService,
  ) { }

  ngOnInit(): void {
    this.sortColumn ={
      sortBy: ApplicationConfig.Sorting.SortBy.TimeClock,
      sortOrder: ApplicationConfig.Sorting.SortOrder.TimeClock.order
     }
    this.page = ApplicationConfig.PaginationConfig.page;
    this.pageSize = ApplicationConfig.PaginationConfig.TableGridSize;
    this.pageSizeList = ApplicationConfig.PaginationConfig.Rows;
    this.selectedEmployee = '';
    this.isTimeClockWeekPage = false;
    this.weeklyDateAssign();

  }
  sort(property) {
    this.sortColumn ={
      sortBy: property,
      sortOrder: ApplicationConfig.Sorting.SortOrder.TimeClock.order
     }
     this.sorting(this.sortColumn)
     this.selectedCls(this.sortColumn)
   
  }
  sorting(sortColumn){
    let direction = sortColumn.sortOrder == 'ASC' ? 1 : -1;
  let property = sortColumn.sortBy;
    this.timeClockEmployeeDetails.sort(function (a, b) {
      if (a[property] < b[property]) {
        return -1 * direction;
      }
      else if (a[property] > b[property]) {
        return 1 * direction;
      }
      else {
        return 0;
      }
    });
  }
  changeSorting(property) {
      this.sortColumn ={
        sortBy: property,
        sortOrder: this.sortColumn.sortOrder == 'ASC' ? 'DESC' : 'ASC'
       }
   
       this.selectedCls(this.sortColumn)
  this.sorting(this.sortColumn)
      
    }
    selectedCls(column) {
      if (column ===  this.sortColumn.sortBy &&  this.sortColumn.sortOrder === 'DESC') {
        return 'fa-sort-desc';
      } else if (column ===  this.sortColumn.sortBy &&  this.sortColumn.sortOrder === 'ASC') {
        return 'fa-sort-asc';
      }
      return '';
    }
  paginate(event) {
    this.pageSize = +this.pageSize;
    this.page = event;
    this.weeklyDateAssign();
  }
  paginatedropdown(event) {
    this.pageSize = +event.target.value;
    this.page = this.page;
    this.weeklyDateAssign();
  }

  weeklyDateAssign() {
    const currentDate = new Date();
    const first = currentDate.getDate() - currentDate.getDay();
    const last = first + 6;
    this.startDate = new Date(currentDate.setDate(first));
    this.currentWeek = this.startDate;
    const lastDate = new Date();
    this.endDate = new Date(lastDate.setDate(last));
    this.daterangepickerModel = [this.startDate, this.endDate];
    this.getTimeClockEmployeeDetails();
  }


  getTimeClockEmployeeDetails() {
    const obj = this.timeClockEmployeeDetailDto;
    const finalObj = {
      locationId: localStorage.getItem('empLocationId'),
      startDate: this.datePipe.transform(this.startDate, 'yyyy-MM-dd'),
      endDate: this.datePipe.transform(this.endDate, 'yyyy-MM-dd')
    };
    this.isLoading = true;
    this.timeClockMaintenanceService.getTimeClockEmployeeDetails(finalObj).subscribe(data => {
      if (data.status === 'Success') {
        const timeClock = JSON.parse(data.resultData);
        this.timeClockEmployeeDetails = timeClock.Result.TimeClockEmployeeDetailViewModel !== null ?
          timeClock.Result.TimeClockEmployeeDetailViewModel : [];
        this.employeeList = timeClock.Result.EmployeeViewModel;
        this.sort(ApplicationConfig.Sorting.SortBy.TimeClock);

        if (this.timeClockEmployeeDetails.length === 0) {
          this.isTimeClockEmpty = true;
        }
        else {
          this.collectionSize = Math.ceil(this.timeClockEmployeeDetails.length / this.pageSize) * 10;
          this.isTimeClockEmpty = false;
        }
      }
      else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  deleteConfirm(obj) {
    if (this.isView) {
      return;
    }
    this.confirmationService.confirm('Delete Location', `Are you sure you want to delete this Time Clock? All related 
      information will be deleted and the Time Clock cannot be retrieved?`, 'Yes', 'No')
      .then((confirmed) => {
        if (confirmed === true) {
          this.deleteTimeClockEmployee(obj);
        }
      })
      .catch(() => { });
  }


  deleteTimeClockEmployee(obj) {
    this.objDelete =
    {
      locationId: obj.LocationId,
      employeeId: obj.EmployeeId
    };
this.spinner.show();
    this.timeClockMaintenanceService.deleteTimeClockEmployee(this.objDelete).subscribe(data => {
      if (data.status === 'Success') {
        this.spinner.hide();

        this.toastr.success(MessageConfig.Admin.TimeClock.Delete, 'Success!');
        this.sortColumn ={
          sortBy: ApplicationConfig.Sorting.SortBy.TimeClock,
          sortOrder: ApplicationConfig.Sorting.SortOrder.TimeClock.order
         }
        this.getTimeClockEmployeeDetails();
      }
      else {
        this.spinner.hide();

        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      this.spinner.hide();
    });
  }

  employeeCheckInDetail(empDetail) {
    this.isTimeClockWeekPage = true;
    this.empClockInObj = {
      employeeID: empDetail.EmployeeId,
      locationId: empDetail.LocationId,
      firstName: empDetail.FirstName,
      lastName: empDetail.LastName,
      startDate: this.startDate,
      endDate: this.endDate
    };
  }

  cancelCheckInPage() {
    this.isTimeClockWeekPage = false;
  }

  getEmployeeList() {
    this.timeClockMaintenanceService.getEmployeeList().subscribe(res => {
      if (res.status === 'Success') {
        const employee = JSON.parse(res.resultData);
        this.employeeList = employee.EmployeeList.Employee;
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  addEmployee() {
    if(this.selectedEmployee.EmployeeId == null){
return
    }
    const employeeObj = {
      timeClockId: 0,
      employeeId: this.selectedEmployee.EmployeeId,
      locationId: localStorage.getItem('empLocationId'),
      roleId: null,
      eventDate: moment(this.startDate).format(),
      inTime: null,
      outTime: null,
      eventType: null,
      updatedFrom: '',
      status: true,
      comments: '',
      isActive: true,
      isDeleted: false
    };
    const employeeListObj = [];
    employeeListObj.push(employeeObj);
    const finalObj = {
      timeClock: {timeClock:employeeListObj}
    };
    this.spinner.show();
    this.timeClockMaintenanceService.saveTimeClock(finalObj).subscribe(res => {
      if (res.status === 'Success') {
        this.spinner.hide();

        this.toastr.success(MessageConfig.Admin.TimeClock.Add, 'Success!');
        this.selectedEmployee = '';
        this.sortColumn ={
          sortBy: ApplicationConfig.Sorting.SortBy.TimeClock,
          sortOrder: ApplicationConfig.Sorting.SortOrder.TimeClock.order
         }
        this.getTimeClockEmployeeDetails();
      }
      else{
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');

      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      this.spinner.hide();
    });
  }

  selectEmploye(event) {
    this.selectedEmployee = event.value;
  }

  onValueChange(event) {
    if (event !== null) {
      if (event.length !== 0 && event.length !== null) {
        const dates = Math.floor((Date.UTC(event[1].getFullYear(), event[1].getMonth(), event[1].getDate()) - Date.UTC(event[0].getFullYear(), event[0].getMonth(), event[0].getDate())) / (1000 * 60 * 60 * 24));
        if (event[0].getDay() !== 0) {
          this.toastr.warning(MessageConfig.Admin.TimeClock.sunday, 'Warning!');
          this.timeClockEmployeeDetails = [];
        } else if (dates !== 6) {
          this.toastr.warning(MessageConfig.Admin.TimeClock.weekRange, 'Warning!');
          this.timeClockEmployeeDetails = [];
        } else {
          this.startDate = event[0];
          this.endDate = event[1];
          if (this.startDate.toDateString() !== this.currentWeek.toDateString()) {
            this.isView = true;
          } else {
            this.isView = false;
          }
          this.getTimeClockEmployeeDetails();
        }
      }
    }
  }

  


}
