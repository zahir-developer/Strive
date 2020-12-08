import { AfterViewInit, Component, OnInit, ViewChild, ChangeDetectorRef } from '@angular/core';
import { ReportsService } from 'src/app/shared/services/data-service/reports.service';
import { ExcelService } from 'src/app/shared/services/common-service/excel.service';
import * as moment from 'moment';
import { BsDaterangepickerDirective, BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { NgxSpinnerService } from 'ngx-spinner';
@Component({
  selector: 'app-daily-sales',
  templateUrl: './daily-sales.component.html',
  styleUrls: ['./daily-sales.component.css']
})
export class DailySalesComponent implements OnInit, AfterViewInit {
  @ViewChild('dp', { static: false }) datepicker: BsDaterangepickerDirective;
  bsConfig: Partial<BsDatepickerConfig>;
  maxDate = new Date();
  locationId: any;
  fileType: number;
  dailySalesReport = [];
  date = moment(new Date()).format('MM-DD-YYYY');
  isTableEmpty: boolean;
  page = 1;
  pageSize = 25;
  collectionSize: number = 0;

  constructor(private spinner: NgxSpinnerService, private toastr: MessageServiceToastr,
    private cd: ChangeDetectorRef, private reportService: ReportsService, private excelService: ExcelService) { }

  ngOnInit(): void {
    this.locationId = localStorage.getItem('empLocationId');
    this.getDailySalesReport();
  }
  ngAfterViewInit() {
    this.bsConfig = Object.assign({}, { maxDate: this.maxDate, dateInputFormat: 'MM-DD-YYYY', showWeekNumbers: false });
    this.datepicker.setConfig();
    this.cd.detectChanges();
  }
  getDailySalesReport() {
    const obj = {
      date: this.date
    };
    this.spinner.show();
    this.reportService.getDailySalesReport(obj).subscribe(data => {
      this.spinner.hide();
      if (data.status === 'Success') {
        const sales = JSON.parse(data.resultData);
        this.dailySalesReport = sales.GetDailySalesReport;
        if (this.dailySalesReport.length === 0) {
          this.isTableEmpty = true;
        } else {
          this.collectionSize = Math.ceil(this.dailySalesReport.length / this.pageSize) * 10;
          this.isTableEmpty = false;
        }
      } else {
        this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error!' });
      }
    }, (err) => {
      this.spinner.hide();
    });
  }

  onLocationChange(event) {
    this.locationId = +event;
  }
  getfileType(event) {
    this.fileType = +event.target.value;
  }
  export() {
    const fileType = this.fileType !== undefined ? this.fileType : '';
    if (fileType === '' || fileType === 0) {
      return;
    } else if (this.dailySalesReport.length === 0) {
      return;
    }
    switch (fileType) {
      case 1: {
        this.excelService.exportAsPDFFile('dailySalesReport', 'dailySalesReport_' + moment(this.date).format('MM/dd/yyyy') + '.pdf');
        break;
      }
      case 2: {
        this.excelService.exportAsCSVFile(this.dailySalesReport, 'dailySalesReport_' + moment(this.date).format('MM/dd/yyyy'));
        break;
      }
      case 3: {
        this.excelService.exportAsExcelFile(this.dailySalesReport, 'dailySalesReport_' + moment(this.date).format('MM/dd/yyyy'));
        break;
      }
      default: {
        return;
      }
    }
  }
  
  onValueChange(event) {
    let selectedDate = event;
    if (selectedDate !== null) {
      selectedDate = moment(event.toISOString()).format('MM-DD-YYYY');
      this.date = selectedDate;
    }
    this.getDailySalesReport();
  }
}
