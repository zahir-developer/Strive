import { Component, OnInit } from '@angular/core';
import { ExcelService } from 'src/app/shared/services/common-service/excel.service';

@Component({
  selector: 'app-monthly-money-owned',
  templateUrl: './monthly-money-owned.component.html',
  styleUrls: ['./monthly-money-owned.component.css']
})
export class MonthlyMoneyOwnedComponent implements OnInit {
  ownedReportList: any = [];
  accountAmount = 0;
  driveUpRate = 0;
  totalHB = 0;
  totalMS = 0;
  totalOM = 0;
  total = 0;
  averageWashRate = 0;
  totalOwnedHB = 0;
  totalOwnedOM = 0;
  fileType: any = '';
  constructor(
    private excelService: ExcelService
  ) { }

  ngOnInit(): void {
    this.getMoneyOwnedReportList();
  }
  getfileType(event) {
    this.fileType = +event.target.value;
  }

  getMoneyOwnedReportList() {
    this.ownedReportList = [
      {
        date: '12-11-2020',
        amount: 10,
        driveUpRate: 9,
        customer: 2,
        firstName: 'steve',
        lastName: 'jobs',
        hb: 10,
        ms: 4,
        om: 3,
        total: 78,
        washRate: 67,
        totalOwnedHB: 34,
        totalOwnedOM: 56
      },
      {
        date: '12-11-2020',
        amount: 10,
        driveUpRate: 9,
        customer: 2,
        firstName: 'steve',
        lastName: 'jobs',
        hb: 10,
        ms: 4,
        om: 3,
        total: 78,
        washRate: 67,
        totalOwnedHB: 34,
        totalOwnedOM: 56
      }
    ];
    this.ownedReportList.forEach(item => {
      this.accountAmount = this.accountAmount + item.amount;
      this.driveUpRate = this.driveUpRate + item.driveUpRate;
      this.totalHB = this.totalHB + item.hb;
      this.totalMS = this.totalMS + item.ms;
      this.totalOM = this.totalOM + item.om;
      this.total = this.total + item.total;
      this.averageWashRate = this.averageWashRate + item.washRate;
      this.totalOwnedHB = this.totalOwnedHB + item.totalOwnedHB;
      this.totalOwnedOM = this.totalOwnedOM + item.totalOwnedOM;
    });
  }

  export() {
    const fileType = this.fileType !== '' ? this.fileType : '';
    if (fileType === '' || fileType === 0) {
      return;
    } else if (this.ownedReportList.length === 0) {
      return;
    }
    switch (fileType) {
      case 1: {
        this.excelService.exportAsPDFFile('MonthlyMoneyreport', 'MoneyOwnedReport.pdf');
        break;
      }
      case 2: {
        this.excelService.exportAsCSVFile(this.ownedReportList, 'MoneyOwnedReport');
        break;
      }
      case 3: {
        this.excelService.exportAsExcelFile(this.ownedReportList, 'MoneyOwnedReport');
        break;
      }
      default: {
        return;
      }
    }
  }

}
