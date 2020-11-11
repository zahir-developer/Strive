import { Component, OnInit } from '@angular/core';
import { ExcelService } from 'src/app/shared/services/common-service/excel.service';
import { ReportsService } from 'src/app/shared/services/data-service/reports.service';
import * as moment from 'moment';
@Component({
  selector: 'app-monthly-tip',
  templateUrl: './monthly-tip.component.html',
  styleUrls: ['./monthly-tip.component.css']
})
export class MonthlyTipComponent implements OnInit {
  fromDate = new Date();
  endDate = new Date();
  fileType: any;
  monthlyTip = [];
  date = new Date();
  month: number;
  year: number;
  locationId = +localStorage.getItem('empLocationId');
  page = 1;
  pageSize = 25;
  collectionSize: number;
  totalTip = 0;
  tipAmount: number;
  totalHours = 0;
  constructor(private excelService: ExcelService, private reportService: ReportsService) { }

  ngOnInit(): void {
    this.month = this.date.getMonth() + 1;
    this.year = this.date.getFullYear();
    this.getMonthlyTipReport();
  }
  getMonthlyTipReport() {
    const obj = {
      locationId: +this.locationId,
      year: this.year,
      month: +this.month,
      date: null
    };
    this.totalTip = 0;
    this.reportService.getMonthlyDailyTipReport(obj).subscribe(res => {
      if (res.status === 'Success') {
        const dailytip = JSON.parse(res.resultData);
        console.log(dailytip);
        this.monthlyTip = dailytip.GetEmployeeTipReport;
        this.monthlyTip.forEach(item => {
          this.totalTip = this.totalTip + item.Tip;
        });
        this.collectionSize = Math.ceil(this.monthlyTip.length / this.pageSize) * 10;
      }
    });
  }
  export() {
    const fileType = this.fileType !== undefined ? this.fileType : '';
    if (fileType === '' || fileType === 0) {
      return;
    }
    switch (fileType) {
      case 1: {
        this.excelService.exportAsPDFFile('Monthlyreport', 'MonthlyTipReport_' + this.month + '/' + this.year + '.pdf');
        break;
      }
      case 2: {
        this.excelService.exportAsCSVFile(this.monthlyTip, 'MonthlyTipReport_' + this.month + '/' + this.year);
        break;
      }
      case 3: {
        this.excelService.exportAsExcelFile(this.monthlyTip, 'MonthlyTipReport_' + this.month + '/' + this.year);
        break;
      }
      default: {
        return;
      }
    }
  }
  onMonthChange(event) {
    this.month = event;
  }
  onYearChange(event) {
    this.year = event;
  }
  onLocationChange(event) {
    this.locationId = +event;
  }
  getfileType(event) {
    this.fileType = +event.target.value;
  }

  preview() {
    this.getMonthlyTipReport();
  }

  submit() {
    this.totalHours = 0;
    this.totalTip = 0;
    if (this.tipAmount !== 0) {
      this.monthlyTip.forEach(s => {
        this.totalHours = this.totalHours + s.HoursPerDay;
      });

      const hourTip = +this.tipAmount / this.totalHours;
      this.monthlyTip.forEach(item => {
        item.Tip = (item.HoursPerDay * hourTip).toFixed(2);
        this.totalTip += +item.Tip;
      });
    }
  }

}
