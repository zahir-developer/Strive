import { Component, OnInit, ViewChild, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import { ReportsService } from 'src/app/shared/services/data-service/reports.service';
import { BsDaterangepickerDirective, BsDatepickerConfig } from 'ngx-bootstrap/datepicker/ngx-bootstrap-datepicker';
import { ExcelService } from 'src/app/shared/services/common-service/excel.service';
import * as moment from 'moment';
declare var $: any;
import { DatePipe } from '@angular/common';
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
  washTotal = 0;
  detailTotal = 0;
  washHours = 0;
  detailHours = 0;
  totalAmount = 0;
  detailInfoTotal = 0;
  dailyStatusDetailInfo = [];
  clockDetail = [];
  clockDetailValue = [];
  constructor(private reportService: ReportsService, private excelService: ExcelService, private cd: ChangeDetectorRef,
              private datePipe: DatePipe) {

  }

  ngOnInit(): void {
    this.locationId = localStorage.getItem('empLocationId');
    this.getDailyStatusReport();
    this.getDailyStatusDetailInfo();
    this.getClockDetail();
    // <tr><td>` + item.EmployeeName + `</td><td>` + (item.WashHours ? item.WashHours : '') + `</td><td>` +
    //     (item.DetailHours ? item.DetailHours : '') + `</td><td>` + (item.TotalHours ? item.TotalHours : '') + `</td><td>`
    //     + (item.HoursPerDay ? item.HoursPerDay : '') + `</td><td>` + (item.TotalAmount ? item.TotalAmount : '') + `</td>`;
    this.clockDetail = [{
      Checked: 'On',
      EmployeeName: 'NewEmployeestring',
      EmployeeId: 1,
      WashHours: 12,
      DetailHours: 11,
      TotalHours: 33,
      TotalAmount: 120,
      EventDate: '2020-11-17T00:00:00',
      HoursPerDay: 22,
    },
    {
      Checked: 'On',
      EmployeeName: 'NewEmployeestring',
      EmployeeId: 2,
      WashHours: 12,
      DetailHours: 11,
      TotalHours: 33,
      TotalAmount: 120,
      EventDate: '2020-11-17T00:00:00',
      HoursPerDay: 22,
    }];
    this.clockDetailValue = [{
      Checked: 'On',
      EmployeeName: 'NewEmployeestring',
      EmployeeId: 1,
      EventDate: '2020-11-17T00:00:00',
      HoursPerDay: 22,
      RoleName: 'Washer',
      InTime: '2020-11-17T01:00:00+05:30',
      OutTime: '2020-11-17T23:00:00+05:30'
    },
    {
      Checked: 'On',
      EmployeeName: 'NewEmployeestring',
      EmployeeId: 1,
      EventDate: '2020-11-17T00:00:00',
      HoursPerDay: 22,
      RoleName: 'Detailer',
      InTime: '2020-11-17T01:00:00+05:30',
      OutTime: '2020-11-17T23:00:00+05:30'
    },
    {
      Checked: 'On',
      EmployeeName: 'NewEmployeestring',
      EmployeeId: 2,
      EventDate: '2020-11-17T00:00:00',
      HoursPerDay: 22,
      InTime: '2020-11-17T01:00:00+05:30',
      OutTime: '2020-11-17T23:00:00+05:30'
    }
    ];
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
        // this.clockDetail = clockDetail?.GetDailyClockDetail;
        this.objConversion();
      }
    }, (err) => {

    });
  }
  objConversion() {
    this.clockDetail.forEach(item => {
      let i = 1;
      this.clockDetailValue.forEach(data => {
        if (+data.EmployeeId === +item.EmployeeId) {
          const Intime = 'Intime' + i;
          const Outtime = 'Outtime' + i;
          item[Intime] = data.InTime;
          item[Outtime] = data.OutTime;
          item.RoleName = data.RoleName;
          item.count = i;
          i++;
        }
      });
    });
    this.generateTable();
    console.log(this.clockDetail);
  }
  generateTable() {
    let tableheader = '';
    let tableBody = '';
    const count = Math.max(...this.clockDetail.map(val => val.count));
    tableheader = `<tr><th scope="col">Employee Name</th><th scope="col">Wash Hours</th><th scope="col">Detail Hours</th>
    <th scope="col">Total Hours</th><th scope="col">Rate/Hour</th><th scope="col">Total Amount</th>`
    for (let i = 0; i < count; i++) {
      tableheader += `<th scope="col">In</th><th scope="col">Out</th><th scope="col">Role</th>`;
    }
    tableheader += `</tr>`;
    $('#table thead').html(tableheader);
    this.clockDetail.forEach(item => {
      tableBody += `<tr><td>` + item.EmployeeName + `</td><td>` + (item.WashHours ? item.WashHours : '') + `</td><td>` +
        (item.DetailHours ? item.DetailHours : '') + `</td><td>` + (item.TotalHours ? item.TotalHours : '') + `</td><td>`
        + (item.HoursPerDay ? item.HoursPerDay : '') + `</td><td>` + (item.TotalAmount ? item.TotalAmount : '') + `</td>`;
      for (let i = 1; i <= count; i++) {
        const Intime = 'Intime' + i;
        const Outtime = 'Outtime' + i;
        tableBody += `<td>` + (item[Intime] !== undefined ? this.datePipe.transform(item[Intime] , 'hh:mm:ss') : '') + `</td><td>`
        + (item[Outtime] !== undefined ? this.datePipe.transform(item[Outtime] , 'hh:mm:ss') : '') + `</td><td>` +
        (item.RoleName ? item.RoleName : '') + `</td>`;
      }
      this.washHours += item.WashHours;
      this.detailHours += item.DetailHours;
      this.totalAmount += item.TotalAmount;
    });
    tableBody += `</tr>`;
    $('#table tbody').html(tableBody);
  }
  getDailyStatusReport() {
    const obj = {
      locationId: +this.locationId,
      date: moment(this.date).format('YYYY-MM-DD')
    };
    this.reportService.getDailyStatusReport(obj).subscribe(data => {
      if (data.status === 'Success') {
        const dailyStatusReport = JSON.parse(data.resultData);
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
