import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { PayrollsService } from 'src/app/shared/services/data-service/payrolls.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { jsPDF } from 'jspdf';
import html2canvas from 'html2canvas';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { HomeNavService } from 'src/app/shared/common-service/home-nav.service';


@Component({
  selector: 'app-payrolls-grid',
  templateUrl: './payrolls-grid.component.html',
  styleUrls: ['./payrolls-grid.component.css']
})
export class PayrollsGridComponent implements OnInit {
  payrollDateForm: FormGroup;
  payRollList: any = [];
  page = 1;
  pageSize = 5;
  collectionSize = 0;
  isEditAdjustment: boolean;
  @ViewChild('content') content: ElementRef;
  constructor(
    private payrollsService: PayrollsService,
    private fb: FormBuilder,
    private datePipe: DatePipe,    
    private messageService: MessageServiceToastr, private homeNavigation: HomeNavService
  ) { }

  ngOnInit(): void {
    this.payrollDateForm = this.fb.group({
      fromDate: ['', Validators.required],
      toDate: ['', Validators.required]
    });
    this.isEditAdjustment = false;
    this.patchValue();
  }

  patchValue() {
    const curr = new Date(); // get current date
    const first = curr.getDate() - curr.getDay(); // First day is the day of the month - the day of the week
    const last = first + 6; // last day is the first day + 6

    const firstday = new Date(curr.setDate(first));
    const lastday = new Date(curr.setDate(last));
    this.payrollDateForm.patchValue({
      fromDate: firstday,
      toDate: lastday
    });
    this.runReport();
  }

  runReport() {
    const locationId = localStorage.getItem('empLocationId');
    const startDate = this.datePipe.transform(this.payrollDateForm.value.fromDate, 'yyyy-MM-dd');
    const endDate = this.datePipe.transform(this.payrollDateForm.value.toDate, 'yyyy-MM-dd');
    this.payrollsService.getPayroll(locationId, startDate, endDate).subscribe(res => {
      if (res.status === 'Success') {
        const payRoll = JSON.parse(res.resultData);
        if (payRoll.Result.PayRollRateViewModel !== null) {
          this.payRollList = payRoll.Result.PayRollRateViewModel;
          // this.payRollList.forEach(item => {
          //   item.isEditAdjustment = false;
          // });
          this.collectionSize = Math.ceil(this.payRollList.length / this.pageSize) * 10;
        }
      }
    });
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
    this.payRollList.forEach( item => {
      updateObj.push({
        id: item.EmployeeId,
        adjustment: +item.Adjustment
      });
    });
    this.payrollsService.updateAdjustment(updateObj).subscribe( res => {
      if (res.status === 'Success') {
        
        this.isEditAdjustment = false;
        this.payrollDateForm.enable();
        
        this.messageService.showMessage({ severity: 'success', title: 'Success', body: 'Updated Successfully' });
        this.runReport();
      }
    });
  }

  processPayrolls() {
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
      pdf.save('MYPdf.pdf'); // Generated PDF
    });
  }

  loadLandingPage() {
    this.homeNavigation.loadLandingPage();
  }

}
