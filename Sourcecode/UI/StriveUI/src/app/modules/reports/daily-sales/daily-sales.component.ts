import { Component, OnInit } from '@angular/core';
import { ReportsService } from 'src/app/shared/services/data-service/reports.service';
import { ExcelService } from 'src/app/shared/services/common-service/excel.service';
import * as moment from 'moment';
@Component({
  selector: 'app-daily-sales',
  templateUrl: './daily-sales.component.html',
  styleUrls: ['./daily-sales.component.css']
})
export class DailySalesComponent implements OnInit {
  locationId: any;
  fileType: number;
  dailySalesReport = [];
  date = new Date();
  constructor(private reportService: ReportsService, private excelService: ExcelService) { }

  ngOnInit(): void {
    this.locationId = localStorage.getItem('empLocationId');
    this.getDailySalesReport();
  }
  getDailySalesReport() {}
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
        this.excelService.exportAsPDFFile('dailySalesReport', 'customerDetailReport_' + moment(this.date).format('MM/dd/yyyy') + '.pdf');
        break;
      }
      case 2: {
        this.excelService.exportAsCSVFile(this.dailySalesReport, 'customerDetailReport_' + moment(this.date).format('MM/dd/yyyy'));
        break;
      }
      case 3: {
        this.excelService.exportAsExcelFile(this.dailySalesReport, 'customerDetailReport_' + moment(this.date).format('MM/dd/yyyy'));
        break;
      }
      default: {
        return;
      }
    }
  }
}
