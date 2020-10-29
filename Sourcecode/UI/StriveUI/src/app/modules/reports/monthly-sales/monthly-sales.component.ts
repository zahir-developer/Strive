import { Component, OnInit, ViewChild, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import { ReportsService } from 'src/app/shared/services/data-service/reports.service';
import { BsDaterangepickerDirective, BsDatepickerConfig } from 'ngx-bootstrap/datepicker/ngx-bootstrap-datepicker';
import { ExcelService } from 'src/app/shared/services/common-service/excel.service';
import * as moment from 'moment';
// import * as jsPDF from 'jspdf';
// import 'jspdf-autotable';
// declare let jsPDF;
@Component({
  selector: 'app-monthly-sales',
  templateUrl: './monthly-sales.component.html',
  styleUrls: ['./monthly-sales.component.css']
})
export class MonthlySalesComponent implements OnInit, AfterViewInit {
  // @ViewChild('dp', { static: false }) datepicker: BsDaterangepickerDirective;
  // bsConfig: Partial<BsDatepickerConfig>;
  monthlySalesReport = [];
  employees = [];
  empCount = 1;
  originaldata = [];
  empName = '';
  total = 0;
  date = new Date();
  maxDate = new Date();
  fileExportType = [];
  fileType: number;
  fromDate = new Date();
  endDate = new Date();
  locationId = +localStorage.getItem('empLocationId');
  page = 1;
  pageSize = 50;
  collectionSize: number;
  constructor(private reportService: ReportsService, private cd: ChangeDetectorRef,
    private excelService: ExcelService) { }

  ngOnInit(): void {
    this.setMonth();
    this.getMonthlySalesReport();
    this.filetype();
  }
  setMonth() {
    const currentMonth = this.fromDate.getMonth() + 1;
    this.onMonthChange(currentMonth);
  }
  filetype() {
    this.fileExportType = [{ id: 0, name: 'select' },
    { id: 1, name: 'Acrobat (PDF) File' },
    { id: 2, name: 'CSV (comma delimited)' },
    { id: 3, name: 'Excel 97 - 2003' },
    { id: 4, name: 'Rich Text Format ' },
    { id: 5, name: 'TIFF File' },
    { id: 6, name: 'Web Archive' },
    { id: 7, name: 'XPS Document' }];
  }
  ngAfterViewInit() {
    // this.bsConfig = Object.assign({}, { maxDate: this.maxDate, dateInputFormat: 'MM-DD-YYYY' });
    // this.datepicker.setConfig();
    // this.cd.detectChanges();
  }
  getMonthlySalesReport() {
    const obj = {
      locationId: this.locationId,
      fromDate: this.fromDate,
      endDate: this.endDate
    };
    this.reportService.getMonthlySalesReport(obj).subscribe(data => {
      console.log(data);
      if (data.status === 'Success') {
        const monthlySalesReport = JSON.parse(data.resultData);
        if (monthlySalesReport?.GetMonthlySalesReport !== null) {
          this.employees = monthlySalesReport?.GetMonthlySalesReport?.EmployeeViewModel ?
            monthlySalesReport?.GetMonthlySalesReport?.EmployeeViewModel : [];
          this.monthlySalesReport = monthlySalesReport?.GetMonthlySalesReport?.MonthlySalesReportViewModel ?
          monthlySalesReport?.GetMonthlySalesReport?.MonthlySalesReportViewModel : [];
          this.originaldata = monthlySalesReport?.GetMonthlySalesReport?.MonthlySalesReportViewModel;
          this.collectionSize = Math.ceil(this.monthlySalesReport.length / this.pageSize) * 10;
          this.employeeListFilter(this.empCount);
        }
      }
    });
  }
  count(action) {
    if (action === 'add') {
      this.empCount = (this.empCount < this.employees?.length) ? (this.empCount + 1) :
      this.employees.length !== 0 ? this.employees.length : 1 ;
      this.employeeListFilter(this.empCount);
    } else {
      this.empCount = (this.empCount > 1) ? (this.empCount - 1) : 1;
      this.employeeListFilter(this.empCount);
    }
  }
  employeeListFilter(count) {
    this.monthlySalesReport = this.originaldata;
    if (this.employees.length > 0) {
      this.empName = this.employees[count - 1]?.EmployeeName;
      this.monthlySalesReport = this.monthlySalesReport.filter(emp => emp.EmployeeId === this.employees[count - 1].EmployeeId);
      this.collectionSize = Math.ceil(this.monthlySalesReport.length / this.pageSize) * 10;
      this.calculatePrice();
    }
  }
  calculatePrice() {
    this.total = this.monthlySalesReport.reduce((sum, i) => {
      return sum + (+i.Total);
    }, 0);
  }
  onValueChange(event) {
    this.fromDate = event;
    this.endDate = event;
    if (event !== null) {
      this.setMonth();
      this.getMonthlySalesReport();
    }
  }
  getFileType(event) {
    this.fileType = +event.target.value;
  }
  export() {
    const fileType = this.fileType !== undefined ? this.fileType : '';
    if (fileType === '' || fileType === 0) {
      return;
    }
    switch (fileType) {
      case 1: {
        this.excelService.exportAsPDFFile('MonthlySalesreport', 'MonthlySalesReport.pdf');
        break;
      }
      case 2: {
        this.excelService.exportAsCSVFile(this.monthlySalesReport, 'monthly-sales');
        break;
      }
      case 3: {
        this.excelService.exportAsExcelFile(this.monthlySalesReport, 'monthly-sales');
        break;
      }
      default: {
        return;
      }
    }
  }
  onMonthChange(event) {
    this.fromDate.setMonth(event - 1);
    this.endDate.setMonth(event - 1);
    this.fromDate = moment(this.fromDate).startOf('month').toDate();
    this.endDate = moment(this.endDate).endOf('month').toDate();
  }
  onYearChange(event) {
    this.fromDate.setFullYear(event);
    this.endDate.setFullYear(event);
  }
  onLocationChange(event) {
    this.locationId = +event;
  }
}
