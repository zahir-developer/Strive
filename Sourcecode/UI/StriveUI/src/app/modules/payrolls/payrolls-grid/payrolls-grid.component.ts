import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { PayrollsService } from 'src/app/shared/services/data-service/payrolls.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { jsPDF } from 'jspdf';
import html2canvas from 'html2canvas';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';

@Component({
  selector: 'app-payrolls-grid',
  templateUrl: './payrolls-grid.component.html',
  styleUrls: ['./payrolls-grid.component.css']
})
export class PayrollsGridComponent implements OnInit {
  payrollDateForm: FormGroup;
  payRollList: any = [];
  bsConfig: Partial<BsDatepickerConfig>;
  collectionSize = 0;
  isEditAdjustment: boolean;
  isPayrollEmpty = true;
  @ViewChild('content') content: ElementRef;
  pageSize: number;
  page: number;
  pageSizeList: number[];
  maxDate = new Date();
  minDate: any;
  constructor(
    private payrollsService: PayrollsService,
    private fb: FormBuilder,
    private datePipe: DatePipe,
    private messageService: MessageServiceToastr,
  ) { }

  ngOnInit(): void {
    this.page = ApplicationConfig.PaginationConfig.page;
    this.pageSize = ApplicationConfig.PaginationConfig.TableGridSize;
    this.pageSizeList = ApplicationConfig.PaginationConfig.Rows;
    this.payrollDateForm = this.fb.group({
      fromDate: ['', Validators.required],
      toDate: ['', Validators.required]
    });
    this.isEditAdjustment = false;
    this.patchValue();
  }

  patchValue() {
    const curr = new Date(); // get current date
    const first = curr.getDate() - 13; // First day is the day of the month - the day of the week
    const last = curr.getDate(); // first + 13;  // last day is the first day + 6
    const firstday = new Date(curr.setDate(first));
    const lastday = new Date();  // curr.setDate(last)
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
      payroll.Adjustment = ''
    };

  }
  paginate(event) {

    this.pageSize = +this.pageSize;
    this.page = event;

    this.runReport()
  }
  paginatedropdown(event) {
    this.pageSize = +event.target.value;
    this.page = this.page;

    this.runReport()
  }
  runReport() {
    const locationId = localStorage.getItem('empLocationId');
    const startDate = this.datePipe.transform(this.payrollDateForm.value.fromDate, 'yyyy-MM-dd');
    const endDate = this.datePipe.transform(this.payrollDateForm.value.toDate, 'yyyy-MM-dd');
    this.payrollsService.getPayroll(locationId, startDate, endDate).subscribe(res => {
      if (res.status === 'Success') {
        const payRoll = JSON.parse(res.resultData);
        //if (payRoll.Result.PayRollRateViewModel !== null) {
        this.payRollList = payRoll.Result.PayRollRateViewModel;
        // this.payRollList.forEach(item => {
        //   item.isEditAdjustment = false;
        // });
        var length = this.payRollList === null ? 0 : this.payRollList.length;
        this.collectionSize = Math.ceil(length / this.pageSize) * 10;
        this.isPayrollEmpty = false;
        //}
        //else
        //{
        this.isPayrollEmpty = payRoll.Result.PayRollRateViewModel === null ? true : false;
        //}
      }
    });
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
    console.log(this.payRollList, 'edit');
    const updateObj = [];
    this.payRollList.forEach(item => {
      updateObj.push({
        id: item.EmployeeId,
        adjustment: +item.Adjustment
      });
    });
    this.payrollsService.updateAdjustment(updateObj).subscribe(res => {
      if (res.status === 'Success') {

        this.isEditAdjustment = false;
        this.payrollDateForm.enable();

        this.messageService.showMessage({ severity: 'success', title: 'Success', body: 'Updated Successfully' });
        this.runReport();
      }
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

}
