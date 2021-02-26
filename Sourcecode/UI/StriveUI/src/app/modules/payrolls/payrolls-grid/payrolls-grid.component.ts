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

@Component({
  selector: 'app-payrolls-grid',
  templateUrl: './payrolls-grid.component.html',
  styleUrls: ['./payrolls-grid.component.css']
})
export class PayrollsGridComponent implements OnInit {
  payrollDateForm: FormGroup;
  payRollList: any = [];
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
  isEditRestriction: boolean;
  employeeId: string;
  constructor(
    private payrollsService: PayrollsService,
    private fb: FormBuilder,
    private datePipe: DatePipe,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService
    ,private landingservice:LandingService
  ) { }

  ngOnInit(): void {
    this.employeeId = localStorage.getItem('empId');

    this.isLoading = false;
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
landing(){
  this.landingservice.loadTheLandingPage()
}
  patchValue() {
    const curr = new Date(); // get current date
    const first = curr.getDate() - 13; // First day is the day of the month - the day of the week
    const last = curr.getDate();//first + 13; // last day is the first day + 6
    const firstday = new Date(curr.setDate(first));
    const lastday = new Date(curr.setDate(last));
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
    this.page = this.page;
    this.runReport();
  }
  runReport() {
    const locationId = localStorage.getItem('empLocationId');
    const startDate = this.datePipe.transform(this.payrollDateForm.value.fromDate, 'yyyy-MM-dd');
    const endDate = this.datePipe.transform(this.payrollDateForm.value.toDate, 'yyyy-MM-dd');
    this.spinner.show();
    this.payrollsService.getPayroll(locationId, startDate, endDate).subscribe(res => {
      this.spinner.hide();
      if (res.status === 'Success') {
        this.editRestriction();
        const payRoll = JSON.parse(res.resultData);
        this.payRollList = payRoll.Result.PayRollRateViewModel;
        var length = this.payRollList === null ? 0 : this.payRollList.length;
        this.collectionSize = Math.ceil(length / this.pageSize) * 10;
        this.isPayrollEmpty = false;
        this.isPayrollEmpty = payRoll.Result.PayRollRateViewModel === null ? true : false;
      }
    }, (err) => {
      this.spinner.hide();
    });
  }
  editRestriction() {
   
       const empId =  null;
       const startDate = this.datePipe.transform(this.payrollDateForm.value.fromDate, 'yyyy-MM-dd');
    const endDate = this.datePipe.transform(this.payrollDateForm.value.toDate, 'yyyy-MM-dd');  
    this.payrollsService.editRestriction(empId,startDate,endDate).subscribe( res => {
      const edit = JSON.parse(res.resultData);
      if (res.status === 'Success') {
        if (edit.Result === 'false') {
        
          this.isEditRestriction = edit.Result;
          
        } else{
          this.isEditRestriction = edit.Result;        }
        
      }
      else{

        this.toastr.error(MessageConfig.CommunicationError, 'Error!');

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
    const updateObj = [];
    this.payRollList.forEach(item => {
      updateObj.push({
        id: item.EmployeeId,
        adjustment: +item.Adjustment
      });
    });
    this.spinner.show();
    this.payrollsService.updateAdjustment(updateObj).subscribe(res => {
      this.spinner.hide();
      if (res.status === 'Success') {
        this.isEditAdjustment = false;
        this.payrollDateForm.enable();
        this.toastr.success(MessageConfig.PayRoll.Adjustment, 'Success!');
        this.runReport();
      }
    }, (err) => {
      this.spinner.hide();
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
        },
        "payrollEmployee": updatedObj
    }
    this.payRollList.forEach( item => {
      updatedObj.push({
        
        
          "payrollEmployeeId": item.EmployeeId,
          "employeeId": this.employeeId,
          "payrollProcessId": 0,
          "adjustment": +item.Adjustment,
          "isActive": true,
          "isDeleted": true,
          "createdBy": this.employeeId,
          "createdDate":new Date(),
          "updatedBy": this.employeeId,
          "updatedDate": new Date(),
        
      
    
      });
    });
 
  
    this.payrollsService.addPayRoll(obj).subscribe( res => {
      if (res.status === 'Success') {
        
        this.isEditAdjustment = false;
        this.payrollDateForm.enable();
        
        this.toastr.success(MessageConfig.PayRoll.Process, 'Success!');
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
