import { Component, OnInit, ViewChild, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import { BsDaterangepickerDirective, BsDatepickerConfig } from 'ngx-bootstrap/datepicker/ngx-bootstrap-datepicker';
import { ReportsService } from 'src/app/shared/services/data-service/reports.service';
import { ExcelService } from 'src/app/shared/services/common-service/excel.service';
import * as moment from 'moment';
import { LocationDropdownComponent } from 'src/app/shared/components/location-dropdown/location-dropdown.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { ToastrService } from 'ngx-toastr';
import { ExportFiletypeComponent } from 'src/app/shared/components/export-filetype/export-filetype.component';

@Component({
  selector: 'app-daily-tip',
  templateUrl: './daily-tip.component.html'
})
export class DailyTipComponent implements OnInit, AfterViewInit {
  @ViewChild(LocationDropdownComponent) locationDropdownComponent: LocationDropdownComponent;
  @ViewChild(ExportFiletypeComponent) exportFiletypeComponent: ExportFiletypeComponent;
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
  fileTypeEvent: boolean = false;
  tips: number =0;
  constructor(private cd: ChangeDetectorRef, private reportService: ReportsService,
    private excelService: ExcelService, private spinner: NgxSpinnerService,
    private toastr :ToastrService) { }

  ngOnInit(): void {
    this.tipAmount = 0;
    this.locationId = localStorage.getItem('empLocationId');
    this.getDailyTipReport();
  }
  getfileType(event) {
    this.fileTypeEvent = true;
    this.fileType = +event.target.value;
  }
  ngAfterViewInit() {
    this.bsConfig = Object.assign({}, { maxDate: this.maxDate, dateInputFormat: 'MM/DD/YYYY', showWeekNumbers: false });
    this.datepicker.setConfig();
    this.cd.detectChanges();
  }
  getDailyTipReport() {
    this.tipAmount = 0;
    this.tips = 0;
    const month = this.date.getMonth() + 1;
    const year = this.date.getFullYear();
    const obj = {
      locationId: +this.locationId,
      date: moment(this.date).format('MM/DD/YYYY'),
      month,
      year
    };
    this.spinner.show();
    this.totalTip = 0;
    this.reportService.getMonthlyDailyTipReport(obj).subscribe(data => {
      if (data.status === 'Success') {
        this.spinner.hide();

        const dailytip = JSON.parse(data.resultData);
        this.dailyTip = dailytip.GetEmployeeTipReport;
        this.dailyTip.forEach(item => {
          this.totalTip = this.totalTip + item.Tip;
        });
        this.collectionSize = Math.ceil(this.dailyTip.length / this.pageSize) * 10;
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
    const locationName = this.locationDropdownComponent.locationName;
    if (fileType === '' || fileType === 0) {
      return;
    }
    switch (fileType) {
      case 1: {
        document.getElementById('tipTotal').style.visibility = 'hidden';
        document.getElementById('tipSubmit').style.visibility = 'hidden';
        setTimeout(() => {
          document.getElementById('tipTotal').style.visibility = 'visible';
          document.getElementById('tipSubmit').style.visibility = 'visible';
        }, 3000);
        this.excelService.exportAsPDFFile('dailyreport', 'DailyTipReport' + moment(this.date).format('MM/DD/YYYY') + '_' + locationName + '.pdf');
        break;
      }
      case 2: {
        const dailyTip = this.customizeObj(this.dailyTip);
        this.excelService.exportAsCSVFile(dailyTip, 'DailyTipReport_' + moment(this.date).format('MM/DD/YYYY') + '_' + locationName);
        break;
      }
      case 3: {
        const dailyTip = this.customizeObj(this.dailyTip);
        this.excelService.exportAsExcelFile(dailyTip, 'DailyTipReport_' + moment(this.date).format('MM/DD/YYYY') + '_' + locationName);
        break;
      }
      default: {
        return;
      }
    }
  }
  customizeObj(dailyTip) {
    if (dailyTip.length > 0) {
      const dTip = dailyTip.map(item => {
        return {
          Payee: item.EmployeeName,
          Hours: item.HoursPerDay,
          Tip: item.Tip
        };
      });
      return dTip;
    }
  }
  submit() {
    this.totalHours = 0;
    this.totalTip = 0;
    if (this.tipAmount !== 0) {
      this.tips = this.tipAmount;
      this.dailyTip.forEach(s => { this.totalHours = this.totalHours + s.HoursPerDay; });
      const hourTip = +this.tipAmount / this.totalHours;
      this.dailyTip.forEach(item => {
        item.Tip = (item.HoursPerDay * hourTip).toFixed(2);
        this.totalTip += +item.Tip;
      });
    }
  }

  refresh() {
    this.locationId = localStorage.getItem('empLocationId');
    this.date = new Date();
    this.locationDropdownComponent.locationId = +localStorage.getItem('empLocationId')
    this.exportFiletypeComponent.type = '';
    this.getDailyTipReport();
  }
}

