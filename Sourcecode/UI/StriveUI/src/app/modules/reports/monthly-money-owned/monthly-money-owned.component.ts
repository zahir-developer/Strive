import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ExcelService } from 'src/app/shared/services/common-service/excel.service';
import { ReportsService } from 'src/app/shared/services/data-service/reports.service';
import * as _ from 'underscore';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { ToastrService } from 'ngx-toastr';

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
  noOfCustomer = 0;
  averageWashRate = 0;
  totalOwnedHB = 0;
  totalOwnedOM = 0;
  fileType: any = '';
  date = new Date();
  month: number;
  year: number;
  uniqLocationName: any = [];
  locationName: any = [];
  locationTotalValue = [];
  totalOwnedValue = [];
  locationId: any;
  owedLocationName = [];
  constructor(
    private excelService: ExcelService,
    private reportsService: ReportsService,
    private spinner: NgxSpinnerService,
    private toastr : ToastrService
  ) { }

  ngOnInit(): void {
    this.month = this.date.getMonth() + 1;
    this.year = this.date.getFullYear();
    this.locationId = localStorage.getItem('empLocationId');
    this.getMoneyOwnedReportList();
  }
  getfileType(event) {
    this.fileType = +event.target.value;
  }

  getMoneyOwnedReportList() {
    const date = this.year + '-' + ('0' + (this.month)).slice(-2);
    this.spinner.show();
    this.reportsService.getMonthlyMoneyOwnedReport(date, this.locationId).subscribe(res => {
      if (res.status === 'Success') {
        this.spinner.hide()
        const monthlyReport = JSON.parse(res.resultData);
        this.uniqLocationName = [];
        this.ownedReportList = [];
        this.locationTotalValue = [];
        this.locationName = [];
        this.totalOwnedValue = [];
        this.owedLocationName = [];
        this.accountAmount = 0;
        this.total = 0;
        this.averageWashRate = 0;
        if (monthlyReport.GetMonthlyMoneyOwnedReport.length > 0) {
          let locName = '';
          const jobDate = _.pluck(monthlyReport.GetMonthlyMoneyOwnedReport, 'JobDate');
          const locationNameBasedonID = _.where(monthlyReport.GetMonthlyMoneyOwnedReport, { LocationId: +this.locationId });
          if (locationNameBasedonID.length > 0) {
            locName = locationNameBasedonID[0].LocationName;
          }
          const uniqDate = [...new Set(jobDate)];
          const location = _.pluck(monthlyReport.GetMonthlyMoneyOwnedReport, 'LocationName');
          this.uniqLocationName = [...new Set(location)];
          this.locationName = this.uniqLocationName;
          const locationName = [];
          let letters = '';
          this.uniqLocationName.forEach(item => {
            const firstLetter = item.split(' ');
            if (firstLetter.length > 1) {
              letters = firstLetter[0].charAt(0).toUpperCase() + firstLetter[1].charAt(0).toUpperCase();
            } else {
              letters = firstLetter[0].charAt(0).toUpperCase();
            }
            locationName.push({
              ShortName: letters,
              Name: item
            });
          });
          this.uniqLocationName = locationName;
          this.uniqLocationName.forEach(item => {
            monthlyReport.GetMonthlyMoneyOwnedReport.forEach(report => {
              if (item.Name === report.LocationName) {
                report.ShortName = item.ShortName;
              }
            });
          });
          this.uniqLocationName.forEach( uniq => {
            if (locName !== uniq.Name) {
              this.owedLocationName.push(uniq);
            }
          });
          this.moontlyOwnedGrid(monthlyReport.GetMonthlyMoneyOwnedReport, uniqDate);
        }
      }
      else{
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');

      }
    }, (err) => {
      this.spinner.hide();
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  moontlyOwnedGrid(report, date) {
    const gridRecord = [];
    const locationGrid = [];
    date.forEach(item => {
      let accountAmount = 0;
      let noOfCustomer = 0;
      let washRate = 0;
      let customerName = '';
      const jobDateRecord = report.filter(record => record.JobDate === item);
      if (jobDateRecord.length > 0) {
        jobDateRecord.forEach(elem => {
          accountAmount = accountAmount + elem.AccountAmount;
          noOfCustomer = jobDateRecord.length;
          washRate = washRate + elem.WashesAmount;
          customerName = elem.CustomerName;
        });
        const finalObj: any = {};
        const location = [];
        const locationObj: any = {};
        const locationAmount = [];
        this.uniqLocationName.forEach(loc => {
          const locationBasedRecord = jobDateRecord.filter(record => record.ShortName === loc.ShortName);
          locationObj[loc.ShortName] = locationBasedRecord.length;
          location.push({
            locationCount: locationBasedRecord.length,
            locationName: loc.ShortName
          });
        });
        this.owedLocationName.forEach( loc => {
          const locationBasedRecord = jobDateRecord.filter(record => record.ShortName === loc.ShortName);
          let totalOwned = 0;
          locationBasedRecord.forEach(owned => {
            totalOwned = totalOwned + (owned.TotalJobAmount ? owned.TotalJobAmount : 0);
          });
          locationAmount.push({
            locationName: loc.ShortName,
            locationAmount: totalOwned
          });
        });
        let totalLocation = 0;
        location.forEach(loc => {
          totalLocation = totalLocation + loc.locationCount;
        });
        finalObj.JobDate = item;
        finalObj.AccountAmount = accountAmount;
        finalObj.CustomerCount = noOfCustomer;
        finalObj.WashesAmount = washRate / jobDateRecord.length;
        finalObj.location = location;
        finalObj.CustomerName = customerName;
        finalObj.Total = totalLocation;
        finalObj.LocationAmount = locationAmount;
        gridRecord.push(finalObj);
      }
    });
    this.ownedReportList = gridRecord;
    this.ownedReportList.forEach(item => {
      this.accountAmount = this.accountAmount + item.AccountAmount;
      this.total = this.total + item.Total;
      this.averageWashRate = this.averageWashRate + item.WashesAmount;
    });
    const data = [];
    this.uniqLocationName.forEach(loc => {
      let totalValue = 0;
      this.ownedReportList.forEach(ele => {
        ele.location.forEach(sn => {
          if (sn.locationName === loc.ShortName) {
            totalValue = totalValue + sn.locationCount;
          }
        });
      });
      this.locationTotalValue.push({
        locationName: loc.ShortName,
        totalValue
      });
    });
    this.owedLocationName.forEach( loc => {
      let totalOwnedValue = 0;
      this.ownedReportList.forEach(ele => {
        ele.LocationAmount.forEach(sn => {
          if (sn.locationName === loc.ShortName) {
            totalOwnedValue = totalOwnedValue + sn.locationAmount;
          }
        });
      });
      this.totalOwnedValue.push({
        locationName: loc.ShortName,
        totalOwnedValue
      });
    });
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

  export() {
    const fileType = this.fileType !== '' ? this.fileType : '';
    if (fileType === '' || fileType === 0) {
      return;
    } else if (this.ownedReportList.length === 0) {
      return;
    }
    switch (fileType) {
      case 1: {
        this.excelService.exportAsPDFFile('MonthlyMoneyreport', 'MoneyOwnedReport_' + this.date + '.pdf');
        break;
      }
      case 2: {
        this.excelService.exportAsCSVFile(this.ownedReportList, 'MoneyOwnedReport_' + this.date);
        break;
      }
      case 3: {
        this.excelService.exportAsExcelFile(this.ownedReportList, 'MoneyOwnedReport_' + this.date);
        break;
      }
      default: {
        return;
      }
    }
  }

}
