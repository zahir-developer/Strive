import { Component, OnInit, ViewChild, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import { BsDaterangepickerDirective, BsDatepickerConfig } from 'ngx-bootstrap/datepicker/ngx-bootstrap-datepicker';
import { ReportsService } from 'src/app/shared/services/data-service/reports.service';
import { ExcelService } from 'src/app/shared/services/common-service/excel.service';

@Component({
  selector: 'app-daily-tip',
  templateUrl: './daily-tip.component.html',
  styleUrls: ['./daily-tip.component.css']
})
export class DailyTipComponent implements OnInit, AfterViewInit {
  @ViewChild('dp', { static: false }) datepicker: BsDaterangepickerDirective;
  bsConfig: Partial<BsDatepickerConfig>;
  date = new Date();
  maxDate = new Date();
  locationId: any;
  dailyTip = [];
  fileType: any;
  page = 1;
  pageSize = 25;
  collectionSize: number;
  totalTip = 0;
  tipAmount: number;
  totalHours: number = 0;
  constructor(private cd: ChangeDetectorRef, private reportService: ReportsService,
    private excelService: ExcelService) { }

  ngOnInit(): void {
    this.locationId = localStorage.getItem('empLocationId');
    // // this.getDailyTipReport();
  }
  getfileType(event) {
    this.fileType = +event.target.value;
  }
  ngAfterViewInit() {
    this.bsConfig = Object.assign({}, { maxDate: this.maxDate, dateInputFormat: 'MM/DD/YYYY' });
    this.datepicker.setConfig();
    this.cd.detectChanges();
  }
  getDailyTipReport() {
    const month = this.date.getMonth() + 1;
    const year = this.date.getFullYear();
    const obj = {
      locationId: +this.locationId,
      date: this.date,
      month,
      year
    };
    this.totalTip = 0;
    this.reportService.getMonthlyDailyTipReport(obj).subscribe(data => {
      if (data.status === 'Success') {
        const dailytip = JSON.parse(data.resultData);
        console.log(dailytip);
        this.dailyTip = dailytip.GetEmployeeTipReport;
        this.dailyTip.forEach(item => {
          this.totalTip = this.totalTip + item.Tip;
        });
        this.collectionSize = Math.ceil(this.dailyTip.length / this.pageSize) * 10;
      }
    });
  }
  onLocationChange(event) {
    this.locationId = +event;
  }
  onValueChange(event) {
    if (event !== null) {
      this.date = event;
      this.getDailyTipReport();
    }
  }
  export() {
    const fileType = this.fileType !== undefined ? this.fileType : '';
    if (fileType === '' || fileType === 0) {
      return;
    }
    switch (fileType) {
      case 1: {
        this.excelService.exportAsPDFFile('Dailyreport', 'DailyTipReport' + this.date + '.pdf');
        break;
      }
      case 2: {
        this.excelService.exportAsCSVFile(this.dailyTip, 'DailyTipReport_' + this.date);
        break;
      }
      case 3: {
        this.excelService.exportAsExcelFile(this.dailyTip, 'DailyTipReport_' + this.date);
        break;
      }
      default: {
        return;
      }
    }
  }

  submit() {
    this.totalHours = 0;
    this.totalTip = 0;
    if (this.tipAmount !== 0) {
      this.dailyTip.forEach(s => { this.totalHours = this.totalHours + s.HoursPerDay });

      const hourTip = +this.tipAmount / this.totalHours;
      this.dailyTip.forEach(item => {
        item.Tip = (item.HoursPerDay * hourTip).toFixed(2);
        this.totalTip += +item.Tip;
      });
    }
  }
}
