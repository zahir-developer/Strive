import { Component, OnInit, ViewChild, AfterViewInit, ChangeDetectorRef, ViewEncapsulation } from '@angular/core';
import { ReportsService } from 'src/app/shared/services/data-service/reports.service';
import { BsDaterangepickerDirective, BsDatepickerConfig } from 'ngx-bootstrap/datepicker/ngx-bootstrap-datepicker';
import { ExcelService } from 'src/app/shared/services/common-service/excel.service';
import * as moment from 'moment';
import { NgxSpinnerService } from 'ngx-spinner';
import { LocationDropdownComponent } from 'src/app/shared/components/location-dropdown/location-dropdown.component';
declare var $: any;
// import * as jsPDF from 'jspdf';
// import 'jspdf-autotable';
// declare let jsPDF;
@Component({
  selector: 'app-monthly-sales',
  templateUrl: './monthly-sales.component.html',
  styleUrls: ['./monthly-sales.component.css']
})
export class MonthlySalesComponent implements OnInit, AfterViewInit {
  @ViewChild(LocationDropdownComponent) locationDropdownComponent: LocationDropdownComponent;
  monthlySalesReport = [];
  selectedDate : any;
  employees = [];
  showNavigation = true;
  showLocation = false;
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
    private excelService: ExcelService, private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.setMonth();
    this.getMonthlySalesReport();
  }
  setMonth() {
    const currentMonth = this.fromDate.getMonth() + 1;
    this.onMonthChange(currentMonth);
  }
  ngAfterViewInit() {
  }
  getMonthlySalesReport() {
    const obj = {
      locationId: this.locationId,
      fromDate: this.fromDate,
      endDate: this.endDate
    };
    this.spinner.show();
    this.reportService.getMonthlySalesReport(obj).subscribe(data => {
      this.spinner.hide();
      if (data.status === 'Success') {
        this.selectedDate = moment(this.fromDate).format('MM/YYYY');
        const monthlySalesReport = JSON.parse(data.resultData);
        if (monthlySalesReport?.GetMonthlySalesReport !== null) {
          this.employees = monthlySalesReport?.GetMonthlySalesReport?.EmployeeViewModel ?
            monthlySalesReport?.GetMonthlySalesReport?.EmployeeViewModel : [];
          this.monthlySalesReport = monthlySalesReport?.GetMonthlySalesReport?.MonthlySalesReportViewModel ?
          monthlySalesReport?.GetMonthlySalesReport?.MonthlySalesReportViewModel : [];
          this.originaldata = monthlySalesReport?.GetMonthlySalesReport?.MonthlySalesReportViewModel ?
          monthlySalesReport?.GetMonthlySalesReport?.MonthlySalesReportViewModel : [];
          this.collectionSize = Math.ceil(this.monthlySalesReport.length / this.pageSize) * 10;
          this.employeeListFilter(this.empCount);
        }
      }
    }, (err) => { this.spinner.hide(); });
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
    this.empName = '';
    if (this.employees.length > 0) {
      this.empName = this.employees[count - 1]?.EmployeeName;
      this.monthlySalesReport = this.monthlySalesReport.filter(emp => emp.EmployeeId === this.employees[count - 1].EmployeeId);
      this.collectionSize = Math.ceil(this.monthlySalesReport.length / this.pageSize) * 10;
      // this.calculatePrice();
    }
    this.calculatePrice();
  }
  calculatePrice() {
    this.total = this.monthlySalesReport?.reduce((sum, i) => {
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
  export() {
    const fileType = this.fileType !== undefined ? this.fileType : '';
    const locationName = this.locationDropdownComponent.locationName;
    if (fileType === '' || fileType === 0) {
      return;
    }
    if (this.monthlySalesReport.length === 0) {
      return;
    }
    switch (fileType) {
      case 1: {
        this.excelService.exportAsPDFFile('monthlySalesExport', 'MonthlySalesReport_' + this.selectedDate + '_' + locationName + '.pdf');
        break;
      }
      case 2: {
        const monthlySalesReport = this.customizeObj(this.monthlySalesReport);
        this.excelService.exportAsCSVFile(monthlySalesReport, 'MonthlySalesReport_' + this.selectedDate + '_' + locationName);
        break;
      }
      case 3: {
        const monthlySalesReport = this.customizeObj(this.monthlySalesReport);
        this.excelService.exportAsExcelFile(monthlySalesReport, 'MonthlySalesReport_' + this.selectedDate + '_' + locationName);
        break;
      }
      default: {
        return;
      }
    }
  }
  customizeObj(monthlySalesReport) {
    if (monthlySalesReport?.length > 0) {
const monthlySales = monthlySalesReport.map(item => {
  return {
    Number: item?.Number,
    Description: item?.Description,
    Price: item?.Price,
    Total: item?.Total
  };
});
return monthlySales;
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
  getfileType(event) {
    this.fileType = +event.target.value;
  }
 
}
