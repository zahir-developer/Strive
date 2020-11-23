import { Component, OnInit } from '@angular/core';
import { ReportsService } from 'src/app/shared/services/data-service/reports.service';
import { ExcelService } from 'src/app/shared/services/common-service/excel.service';
import * as moment from 'moment';
import { CheckoutService } from 'src/app/shared/services/data-service/checkout.service';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
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
  isTableEmpty: boolean;
  page = 1;
  pageSize = 25;
  collectionSize: number = 0;

  constructor(private checkout: CheckoutService, private toastr: MessageServiceToastr,private reportService: ReportsService, private excelService: ExcelService) { }

  ngOnInit(): void {
    this.locationId = localStorage.getItem('empLocationId');
    this.getDailySalesReport();
  }
  getDailySalesReport() {
  this.checkout.getUncheckedVehicleDetails().subscribe(data => {
    if (data.status === 'Success') {
      const uncheck = JSON.parse(data.resultData);
      this.dailySalesReport = uncheck.GetCheckedInVehicleDetails;
      if (this.dailySalesReport.length === 0) {
        this.isTableEmpty = true;
      } else {
        this.collectionSize = Math.ceil(this.dailySalesReport.length / this.pageSize) * 10;
        this.isTableEmpty = false;
      }
    } else {
      this.toastr.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error!' });
    }
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
}
