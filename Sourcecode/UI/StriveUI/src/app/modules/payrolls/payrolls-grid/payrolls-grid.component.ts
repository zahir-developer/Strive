import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { PayrollsService } from 'src/app/shared/services/data-service/payrolls.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { jsPDF } from 'jspdf';
import html2canvas from 'html2canvas';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { LoginComponent } from 'src/app/login/login.component';
import { LandingService } from 'src/app/shared/services/common-service/landing.service';
import { ExcelService } from 'src/app/shared/services/common-service/excel.service';
import * as moment from 'moment';

@Component({
  selector: 'app-payrolls-grid',
  templateUrl: './payrolls-grid.component.html'
})
export class PayrollsGridComponent implements OnInit {
  payrollDateForm: FormGroup;
  payRollList: any = [];
  payRollBackup: any = [];
  MaxDate = new Date()
  bsConfig: Partial<BsDatepickerConfig>;
  collectionSize = 0;
  isEditAdjustment: boolean;
  isPayrollEmpty = true;
  @ViewChild(LoginComponent) LoginComponent: LoginComponent;

  @ViewChild('content') content: ElementRef;
  pageSize: number;
  page: number;
  pageSizeList: number[];
  maxDate = new Date();
  minDate: any;
  isLoading: boolean;
  isEditRestriction: boolean = false;
  employeeId: string;
  sortColumn: { sortBy: string; sortOrder: string; };
  processLabel: string = "Process";
  fileExportType: { id: number; name: string; }[];
  fileType: any;
  date = moment(new Date()).format('MM/DD/YYYY');
  fileTypeEvent: boolean = false;
  location: any;
  locationId: number = 0;

  constructor(
    private payrollsService: PayrollsService,
    private fb: FormBuilder,
    private datePipe: DatePipe,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private landingservice: LandingService,
    private excelService: ExcelService
  ) { }

  ngOnInit(): void {
    this.employeeId = localStorage.getItem('empId');
    this.sortColumn = {
      sortBy: ApplicationConfig.Sorting.SortBy.PayRoll,
      sortOrder: ApplicationConfig.Sorting.SortOrder.PayRoll.order
    };
    this.isLoading = false;
    this.page = ApplicationConfig.PaginationConfig.page;
    this.pageSize = ApplicationConfig.PaginationConfig.TableGridSize;
    this.pageSizeList = ApplicationConfig.PaginationConfig.Rows;
    this.payrollDateForm = this.fb.group({
      fromDate: ['', Validators.required],
      toDate: ['', Validators.required],
    });
    this.location = JSON.parse(localStorage.getItem('empLocation'));
    this.locationId = +localStorage.getItem('empLocationId');
    this.isEditAdjustment = false;
    this.patchValue();
    this.fileExportType = [
      { id: 1, name: 'CSV (comma delimited)' },
      { id: 2, name: 'Excel 97 - 2003' },
    ];
  }
  landing() {
    this.landingservice.loadTheLandingPage();
  }
  getFileType(event) {
    this.fileTypeEvent = true;
    this.fileType = +event.target.value;
  }
  getlocation(event) {
    this.locationId = +event.target.value;
  }
  export() {
    this.payRollList
    const fileType = this.fileType !== undefined ? this.fileType : '';
    if (fileType === '' || fileType === 0) {
      return;
    } else if (this.payRollList.length === 0) {
      return;
    }
    let excelReport: any = [];
    if (this.payRollList.length > 0) {
      for (let i = 0; i < this.payRollList.length; i++) {
        excelReport.push({
          'Emp.ID': this.payRollList[i].EmployeeId,
          'Payee Name': this.payRollList[i].PayeeName,
          'Wash Hrs.': this.payRollList[i].TotalWashHours,
          'Detail Hrs.': this.payRollList[i].TotalDetailHours,
          'Rate': this.payRollList[i].WashRate,
          'Reg. pay': this.payRollList[i].WashAmount,
          'OT Hrs.': this.payRollList[i].OverTimeHours,
          'OT Pay': this.payRollList[i].OverTimePay,
          'Collision': this.payRollList[i].Collision,

          'Adjustment': this.payRollList[i].Adjustment,
          'Details com.': this.payRollList[i].DetailCommission,
          'CashTip': this.payRollList[i].CashTip,
          'DetailTip': this.payRollList[i].DetailTip,
          'CardTip': this.payRollList[i].CardTip,
          'WashTip': this.payRollList[i].WashTip,
          'Bonus': this.payRollList[i].Bonus,
          'Payee Total': this.payRollList[i].PayeeTotal,

        })
      }
    }
    if (this.fileType == 1) {
      this.excelService.exportAsCSVFile(excelReport, 'payrollReport_' + moment(this.date).format('MM/dd/yyyy'));
    }
    else {
      this.excelService.exportAsExcelFile(excelReport, 'payrollReport_' + moment(this.date).format('MM/dd/yyyy'));

    }

  }
  patchValue() {
    const curr = new Date(); // get current date
    const first = curr.getDate() - 15; // First day is the day of the month - the day of the week
    const last = curr.getDate(); // first + 13; // last day is the first day + 6
    const firstday = new Date(curr.setDate(first));
    const lastday = new Date();
    this.minDate = firstday;
    this.payrollDateForm.patchValue({
      fromDate: firstday,
      toDate: lastday
    });
    this.runReport();
  }
  _keyUp(e, payroll) {
    const pattern = /[0-9]/;
    let inputChar = payroll.Adjustment;
    if (!pattern.test(inputChar)) {
      payroll.Adjustment = '';
    }

  }
  paginate(event) {
    this.pageSize = +this.pageSize;
    this.page = event;
    this.runReport();
  }
  paginatedropdown(event) {
    this.pageSize = +event.target.value;
    this.page = 1;
    //this.runReport();
  }

  onLocationChange(event) {
    this.locationId = +event;
  }

  runReport() {
    const locationId = this.locationId;

    if (+locationId === 0) {
      this.toastr.warning(MessageConfig.PayRoll.SelectLocation, 'Warning!');
      return;
    }

    const startDate = this.datePipe.transform(this.payrollDateForm.value.fromDate, 'yyyy-MM-dd');
    const endDate = this.datePipe.transform(this.payrollDateForm.value.toDate, 'yyyy-MM-dd');
    this.spinner.show();
    this.payrollsService.getPayroll(locationId, startDate, endDate).subscribe(res => {
      if (res.status === 'Success') {
        this.spinner.hide();
        this.editRestriction();
        const payRoll = JSON.parse(res.resultData);
        if (payRoll.Result.PayRollRateViewModel) {
          this.payRollList = payRoll.Result.PayRollRateViewModel;
          this.payRollBackup = payRoll.Result.PayRollRateViewModel;
          const length = this.payRollList === null ? 0 : this.payRollList.length;
          this.collectionSize = Math.ceil(length / this.pageSize) * 10;
          this.sort(ApplicationConfig.Sorting.SortBy.PayRoll);
          this.isPayrollEmpty = false;
        } else {
          this.collectionSize = Math.ceil(length / this.pageSize) * 10;
          this.sort(ApplicationConfig.Sorting.SortBy.PayRoll);
          this.isPayrollEmpty = true;
          this.payRollList = null;
          this.payRollBackup = null;
        }
      }
      else {
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.spinner.hide();
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }
  editRestriction() {
    const empId = null;
    const startDate = this.datePipe.transform(this.payrollDateForm.value.fromDate, 'yyyy-MM-dd');
    const endDate = this.datePipe.transform(this.payrollDateForm.value.toDate, 'yyyy-MM-dd');
    const locationId = this.locationId;
    this.payrollsService.editRestriction(empId, startDate, endDate, locationId).subscribe(res => {
      const edit = JSON.parse(res.resultData);
      if (res.status === 'Success') {
        if (edit.Result === false) {

          this.isEditRestriction = false;
          this.processLabel = "Process";
        } else {
          this.isEditRestriction = true;
          this.processLabel = "Processed";

        }

      }
      else {

        this.toastr.error(MessageConfig.CommunicationError, 'Error!');

      }
    });
  }
  sort(property) {
    this.sortColumn = {
      sortBy: property,
      sortOrder: ApplicationConfig.Sorting.SortOrder.TimeClock.order
    }
    this.sorting(this.sortColumn)
    this.selectedCls(this.sortColumn)

  }
  sorting(sortColumn) {
    let direction = sortColumn.sortOrder == 'ASC' ? 1 : -1;
    let property = sortColumn.sortBy;
    this.payRollList.sort(function (a, b) {
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
    this.sortColumn = {
      sortBy: property,
      sortOrder: this.sortColumn.sortOrder == 'ASC' ? 'DESC' : 'ASC'
    }

    this.selectedCls(this.sortColumn)
    this.sorting(this.sortColumn)

  }
  selectedCls(column) {
    if (column === this.sortColumn.sortBy && this.sortColumn.sortOrder === 'DESC') {
      return 'fa-sort-desc';
    } else if (column === this.sortColumn.sortBy && this.sortColumn.sortOrder === 'ASC') {
      return 'fa-sort-asc';
    }
    return '';
  }
  onValueChange(event) {
    this.minDate = event;
  }

  editAdjustemt() {
    this.isEditAdjustment = true;
    this.payrollDateForm.disable();
  }

  CancelAdjustemt() {
    this.isEditAdjustment = false;
    this.payrollDateForm.enable();
  }

  saveAdjustemt() {
    this.runReport();
  }

  updateAdjustment() {
    const updateObj = [];
    this.payRollList.forEach(item => {
      const oldPayroll = this.payRollBackup.filter(s => s.EmployeeId === item.EmployeeId);
      if (oldPayroll !== null && oldPayroll !== undefined) {
        if (oldPayroll.Adjustment !== +item.Adjustment) {
          updateObj.push({
            id: item.EmployeeId,
            adjustment: +item.Adjustment,
            LocationId:this.locationId
          });
        }
      }
    });
    this.spinner.show();
    this.payrollsService.updateAdjustment(updateObj).subscribe(res => {
      if (res.status === 'Success') {
        this.spinner.hide();
        this.isEditAdjustment = false;
        this.payrollDateForm.enable();
        this.toastr.success(MessageConfig.PayRoll.Adjustment, 'Success!');
        this.runReport();
      }
      else {
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');

      }
    }, (err) => {
      this.spinner.hide();
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }
  addPayrollProcess() {
    const updatedObj = [];
    const obj = {
      "payrollProcess": {
        "payrollProcessId": 0,
        "fromDate": this.datePipe.transform(this.payrollDateForm.value.fromDate, 'yyyy-MM-dd'),
        "toDate": this.datePipe.transform(this.payrollDateForm.value.toDate, 'yyyy-MM-dd'),
        "isActive": true,
        "isDeleted": true,
        "createdBy": this.employeeId,
        "createdDate": new Date(),
        "updatedBy": this.employeeId,
        "updatedDate": new Date(),
        "locationId":this.locationId
      },
      "payrollEmployee": updatedObj
    }
    this.payRollList.forEach(item => {
      updatedObj.push({
        "payrollEmployeeId": item.EmployeeId,
        "employeeId": this.employeeId,
        "payrollProcessId": 0,
        "adjustment": +item.Adjustment,
        "isActive": true,
        "isDeleted": true,
        "createdBy": this.employeeId,
        "createdDate": new Date(),
        "updatedBy": this.employeeId,
        "updatedDate": new Date(),



      });
    });

    this.spinner.show();
    this.payrollsService.addPayRoll(obj).subscribe(res => {
      if (res.status === 'Success') {
        this.spinner.hide();

        this.isEditAdjustment = false;
        this.payrollDateForm.enable();

        this.toastr.success(MessageConfig.PayRoll.Process, 'Success!');
        this.runReport();
      }
      else {
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');

      }
    }, (err) => {
      this.spinner.hide();
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  processPayrolls() {
    const startDate = this.datePipe.transform(this.payrollDateForm.value.fromDate, 'yyyy-MM-dd');
    const endDate = this.datePipe.transform(this.payrollDateForm.value.toDate, 'yyyy-MM-dd');
    const data = document.getElementById('payrollPDF');
    html2canvas(data).then(canvas => {
      // Few necessary setting options
      const imgWidth = 208;
      const pageHeight = 295;
      const imgHeight = canvas.height * imgWidth / canvas.width;
      const heightLeft = imgHeight;
      const contentDataURL = canvas.toDataURL('image/png');
      const pdf = new jsPDF('p', 'mm', 'a4'); // A4 size page of PDF
      const position = 0;
      pdf.addImage(contentDataURL, 'PNG', 0, position, imgWidth, imgHeight);
      pdf.save('PayrollReport ' + startDate + ' - ' + endDate + '.pdf'); // Generated PDF
    });
  }

  newRunReport() {
    this.page = 1;
    this.runReport();
      }
}
