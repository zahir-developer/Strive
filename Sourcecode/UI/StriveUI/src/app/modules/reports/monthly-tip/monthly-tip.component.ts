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
  pageSize = 5;
  collectionSize: number;
  constructor(private excelService: ExcelService, private reportService: ReportsService) { }

  ngOnInit(): void {
    this.monthlyTip = [{ Payee: 12, Hours: 10, Tip: 20 },
    { Payee: 13, Hours: 10, Tip: 20 },
    { Payee: 14, Hours: 10, Tip: 20 },
    { Payee: 11, Hours: 10, Tip: 20 },
    { Payee: 11, Hours: 10, Tip: 20 },
    { Payee: 11, Hours: 10, Tip: 20 },
    { Payee: 11, Hours: 10, Tip: 20 },
    { Payee: 11, Hours: 10, Tip: 20 },
    { Payee: 11, Hours: 10, Tip: 20 },
    { Payee: 20, Hours: 10, Tip: 20 }];
    this.collectionSize = Math.ceil(this.monthlyTip.length / this.pageSize) * 10;
    this.getMonthlyTipReport();
  }
  getMonthlyTipReport() {
    const obj = {
      locationId: +this.locationId,
      year: this.year,
      month: this.month
    };
    this.reportService.getMonthlyTipReport(obj).subscribe(res => {
      if (res.status === 'Success') { }
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

}
