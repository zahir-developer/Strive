import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { PayrollsService } from 'src/app/shared/services/data-service/payrolls.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { jsPDF } from 'jspdf';

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
    private datePipe: DatePipe
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
    this.payrollsService.getPayroll(48, '2020-09-27', '2020-09-28').subscribe(res => {
      if (res.status === 'Success') {
        const payRoll = JSON.parse(res.resultData);
        if (payRoll.Result.PayRollRateViewModel !== null) {
          this.payRollList = payRoll.Result.PayRollRateViewModel;
          this.payRollList.forEach(item => {
            item.isEditAdjustment = false;
          });
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
  }

  processPayrolls() {
    // const doc = new jsPDF();
    // doc.text('Hello world!', 1, 1);
    // doc.save('two-by-four.pdf');
    // doc.addHTML(this.content.nativeElement, function() {
    //    doc.save("obrz.pdf");
    // });
    // console.log(this.content.nativeElement);
    // doc.addHTML(this.content.nativeElement, function () {
    //   doc.save('obrz.pdf');
    // });
    const elementToPrint = document.getElementById('content'); // The html element to become a pdf
    const pdf = new jsPDF('p', 'pt', 'a4');
    pdf.addHTML(elementToPrint, () => {
      pdf.save('web.pdf');
    });
  }

}
