import { Component, OnInit } from '@angular/core';
import { TimeClockMaintenanceService } from 'src/app/shared/services/data-service/time-clock-maintenance.service';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import * as moment from 'moment';
import { DatePipe } from '@angular/common';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';

@Component({
  selector: 'app-time-clock-maintenance',
  templateUrl: './time-clock-maintenance.component.html',
  styleUrls: ['./time-clock-maintenance.component.css']
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
  sort = { column: 'EmployeeId', descending: true };
  sortColumn: { column: string; descending: boolean; };
  pageSizeList: number[];
  page: number;
  pageSize: number;
  constructor(
    private timeClockMaintenanceService: TimeClockMaintenanceService,
    private toastr: ToastrService,
    private confirmationService: ConfirmationUXBDialogService,
    private uiLoaderService: NgxUiLoaderService,
    private datePipe: DatePipe,
  ) { }

  ngOnInit(): void {
    this.page= ApplicationConfig.PaginationConfig.page;
    this.pageSize = ApplicationConfig.PaginationConfig.TableGridSize;
    this.pageSizeList = ApplicationConfig.PaginationConfig.Rows;
    this.selectedEmployee = '';
    this.isTimeClockWeekPage = false;
    this.weeklyDateAssign();
    // this.getEmployeeList();
    // this.getTimeClockEmployeeDetails();
  }
  paginate(event) {
    
    this.pageSize= +this.pageSize;
    this.page = event ;
    
    this.weeklyDateAssign()
  }
  paginatedropdown(event) {
    this.pageSize= +event.target.value;
    this.page =  this.page;
    
    this.weeklyDateAssign()
  }

  weeklyDateAssign() {
    const currentDate = new Date();
    const first = currentDate.getDate() - currentDate.getDay();
    const last = first + 6;
    this.startDate = new Date(currentDate.setDate(first));
    this.currentWeek = this.startDate;
    this.endDate = new Date(currentDate.setDate(last));
    // this.endDate = this.endDate.setDate( this.startDate.getDate() + 6);
    this.endDate = new Date(moment(this.endDate).format());
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
        if (this.timeClockEmployeeDetails.length === 0) {
          this.isTimeClockEmpty = true;
          console.log(this.timeClockEmployeeDetails);
        }
        else {
          this.collectionSize = Math.ceil(this.timeClockEmployeeDetails.length / this.pageSize) * 10;
          this.isTimeClockEmpty = false;
          console.log(this.timeClockEmployeeDetails);
        }
        //this.onValueChange
      }
      else {
        this.toastr.error('Communication Error', 'Error !!!');
      }
    });
  }

  deleteConfirm(obj) {
    if (this.isView) {
      return;
    }
    this.confirmationService.confirm('Delete Location', `Are you sure you want to delete this location? All related 
      information will be deleted and the location cannot be retrieved?`, 'Yes', 'No')
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

    this.timeClockMaintenanceService.deleteTimeClockEmployee(this.objDelete).subscribe(data => {
      if (data.status === 'Success') {
        this.toastr.success('Employee record deleted successfully!!', 'Success!');
        this.getTimeClockEmployeeDetails();
      }
      else {
        this.toastr.error('Communication Error', 'Error !!!');
      }
    });
  }

  employeeCheckInDetail(empDetail) {
    console.log(empDetail, 'detail');
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
    });
  }

  addEmployee() {
    const employeeObj = {
      timeClockId: 0,
      employeeId: this.selectedEmployee.EmployeeId,
      locationId: localStorage.getItem('empLocationId'),
      roleId: null,
      eventDate: moment(this.startDate).format(),
      inTime: null,
      outTime: null,
      eventType: 1,
      updatedFrom: 'string',
      status: true,
      comments: 'string',
      isActive: true,
      isDeleted: false
    };
    const employeeListObj = [];
    employeeListObj.push(employeeObj);
    const finalObj = {
      timeClock: employeeListObj
    };
    this.timeClockMaintenanceService.saveTimeClock(finalObj).subscribe(res => {
      if (res.status === 'Success') {
        this.toastr.success('Employee added successfully!!', 'Success!');
        this.selectedEmployee = '';
        this.getTimeClockEmployeeDetails();
      }
    });
  }

  selectEmploye(event) {
    console.log(event, 'event');
    this.selectedEmployee = event.value;
  }

  onValueChange(event) {
    console.log(event, 'start');
    if (event !== null) {
      if (event.length !== 0 && event.length !== null) {
        const dates = Math.floor((Date.UTC(event[1].getFullYear(), event[1].getMonth(), event[1].getDate()) - Date.UTC(event[0].getFullYear(), event[0].getMonth(), event[0].getDate())) / (1000 * 60 * 60 * 24));
        if (event[0].getDay() !== 0) {
          //console.log(event[0].getDay());          
          this.toastr.warning('Sunday should be the start of the week!!', 'Warning!');
          this.timeClockEmployeeDetails = [];
        } else if (dates !== 6) {
          this.toastr.warning('Only one week can be selected!', 'Warning!');
          this.timeClockEmployeeDetails = [];
        } else {
          this.startDate = event[0];
          this.endDate = event[1];
          if(this.startDate.toDateString() !== this.currentWeek.toDateString()){
            this.isView = true;
          }else{
            this.isView = false;
          }
          this.getTimeClockEmployeeDetails();
        }
      }
    }
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
