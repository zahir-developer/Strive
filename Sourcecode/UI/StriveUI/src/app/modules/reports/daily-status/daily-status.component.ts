import { Component, OnInit, ViewChild, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import { ReportsService } from 'src/app/shared/services/data-service/reports.service';
import { BsDaterangepickerDirective, BsDatepickerConfig } from 'ngx-bootstrap/datepicker/ngx-bootstrap-datepicker';
import { ExcelService } from 'src/app/shared/services/common-service/excel.service';
import * as moment from 'moment';
@Component({
  selector: 'app-daily-status',
  templateUrl: './daily-status.component.html',
  styleUrls: ['./daily-status.component.css']
})
export class DailyStatusComponent implements OnInit, AfterViewInit {
  @ViewChild('dp', { static: false }) datepicker: BsDaterangepickerDirective;
  bsConfig: Partial<BsDatepickerConfig>;
  locationId: any;
  date = new Date();
  maxDate = new Date();
  dailyStatusReport = [];
  fileType: number;
  washes = [];
  details = [];
  washTotal  = 0;
  detailTotal = 0;
  detailInfoTotal = 0;
  dailyStatusDetailInfo = [];
  clockDetail = [];
  constructor(private reportService: ReportsService, private excelService: ExcelService, private cd: ChangeDetectorRef) {

   }

  ngOnInit(): void {
    this.locationId = localStorage.getItem('empLocationId');
    this.getDailyStatusReport();
    this.getDailyStatusDetailInfo();
    this.getClockDetail();
  }
  getfileType(event) {
    this.fileType = +event.target.value;
  }
  ngAfterViewInit() {
    this.bsConfig = Object.assign({}, { maxDate: this.maxDate, dateInputFormat: 'MM/DD/YYYY' });
    this.datepicker.setConfig();
    this.cd.detectChanges();
  }
  getClockDetail() {
    const obj = {
      locationId: +this.locationId,
      date: moment(this.date).format('YYYY-MM-DD')
    };
    this.reportService.getDailyClockDetail(obj).subscribe(data => {
      if (data.status === 'Success') {
const clockDetail = JSON.parse(data.resultData);
console.log(clockDetail);
this.clockDetail = clockDetail?.GetDailyClockDetail;
      }
    }, (err) => {

    });
  }
  getDailyStatusReport()  {
    const obj = {
      locationId: +this.locationId,
      date: moment(this.date).format('YYYY-MM-DD')
    };
    this.reportService.getDailyStatusReport(obj).subscribe(data => {
      if (data.status === 'Success') {
const dailyStatusReport = JSON.parse(data.resultData);
console.log(dailyStatusReport);
this.dailyStatusReport = dailyStatusReport.GetDailyStatusReport;
if (this.dailyStatusReport.length > 0) {
  this.washes = this.dailyStatusReport.filter(item => item.JobType === 'Wash');
  this.details = this.dailyStatusReport.filter(item => item.JobType === 'Detail');
  this.washTotal = this.calculateTotal(this.washes, 'wash');
  this.detailTotal = this.calculateTotal(this.details, 'detail');
}
      }
    }, (err) => {

    });
  }
  getDailyStatusDetailInfo() {
    const obj = {
      locationId: +this.locationId,
      date: moment(this.date).format('YYYY-MM-DD')
    };
    this.reportService.getDailyStatusDetailInfo(obj).subscribe(data => {
      if (data.status === 'Success') {
const dailyStatusDetailInfo = JSON.parse(data.resultData);
console.log(dailyStatusDetailInfo);
this.dailyStatusDetailInfo = dailyStatusDetailInfo.GetDailyStatusReport;
this.detailInfoTotal = this.calculateTotal(this.dailyStatusDetailInfo, 'detailInfo');
      }
    }, (err) => {
 
  });
}
  onLocationChange(event) {
    this.locationId = +event;
  }
  onValueChange(event) {
    if (event !== null) {
      this.date = event;
    }
  }
  export() {
    const fileType = this.fileType !== undefined ? this.fileType : '';
    if (fileType === '' || fileType === 0) {
      return;
    } else if (this.dailyStatusReport.length === 0) {
      return;
    }
    switch (fileType) {
      case 1: {
        this.excelService.exportAsPDFFile('dailyStatusExport', 'customerDetailReport_' + moment(this.date).format('MM/dd/yyyy') + '.pdf');
        break;
      }
      case 2: {
        this.excelService.exportAsCSVFile(this.dailyStatusReport, 'customerDetailReport_' + moment(this.date).format('MM/dd/yyyy'));
        break;
      }
      case 3: {
        this.excelService.exportAsExcelFile(this.dailyStatusReport, 'customerDetailReport_' + moment(this.date).format('MM/dd/yyyy'));
        break;
      }
      default: {
        return;
      }
    }
  }
  preview() {
    this.getDailyStatusReport();
    this.getDailyStatusDetailInfo();
    this.getClockDetail();
  }
  calculateTotal(obj, type) {
      return obj.reduce((sum, i) => {
        return sum + (type === 'detailInfo' ? +i.Commision : +i.Number);
      }, 0);
  }
}
