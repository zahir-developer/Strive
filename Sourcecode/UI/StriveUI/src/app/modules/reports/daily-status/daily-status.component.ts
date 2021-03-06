import { Component, OnInit, ViewChild, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import { ReportsService } from 'src/app/shared/services/data-service/reports.service';
import { BsDaterangepickerDirective, BsDatepickerConfig } from 'ngx-bootstrap/datepicker/ngx-bootstrap-datepicker';
import { ExcelService } from 'src/app/shared/services/common-service/excel.service';
import * as moment from 'moment';
declare var $: any;
import { DatePipe } from '@angular/common';
import { NgxSpinnerService } from 'ngx-spinner';
import { LocationDropdownComponent } from 'src/app/shared/components/location-dropdown/location-dropdown.component';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-daily-status',
  templateUrl: './daily-status.component.html',
  styleUrls: ['./daily-status.component.css']
})
export class DailyStatusComponent implements OnInit, AfterViewInit {
  @ViewChild('dp', { static: false }) datepicker: BsDaterangepickerDirective;
  @ViewChild(LocationDropdownComponent) locationDropdownComponent: LocationDropdownComponent;
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
  dailyStatusWashInfo: any;
  fileTypeEvent: boolean = false;
  constructor(private reportService: ReportsService, private excelService: ExcelService, private cd: ChangeDetectorRef,
              private datePipe: DatePipe, private spinner: NgxSpinnerService,
              private toastr : ToastrService) {

  }

  ngOnInit(): void {
    this.locationId = localStorage.getItem('empLocationId');
    this.getDailyStatusReport();
    this.getDailyStatusWashReport();

    this.getDailyStatusDetailInfo();
    this.getClockDetail();
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
  getClockDetail() {
    const obj = {
      locationId: +this.locationId,
      date: moment(this.date).format('YYYY-MM-DD')
    };
    this.spinner.show();
    this.reportService.getDailyClockDetail(obj).subscribe(data => {
      if (data.status === 'Success') {
        this.spinner.hide();

        const clockDetail = JSON.parse(data.resultData);
        this.clockDetail = clockDetail?.Result?.TimeClockEmployeeDetails ? clockDetail?.Result?.TimeClockEmployeeDetails : [];
        this.clockDetailValue = clockDetail?.Result?.TimeClockDetails ? clockDetail?.Result?.TimeClockDetails : [];
        this.objConversion();
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
  objConversion() {
    this.clockDetail?.forEach(item => {
      let i = 1;
      this.clockDetailValue.forEach(data => {
        if (+data.EmployeeId === +item.EmployeeId) {
          item.EmployeeName = item.FirstName + ' ' + item.LastName;
          const Intime = 'Intime' + i;
          const Outtime = 'Outtime' + i;
          
          const RoleName = 'RoleName' + i;
          const TimeIn = 'TimeIn' + i;
          const TimeOut = 'TimeOut' + i;
          
          item[Intime] = data.InTime;
          item[Outtime] = data.OutTime;
          item[RoleName] = data.RoleName;
          item[TimeIn] = data.TimeIn;
          item[TimeOut] = data.TimeOut;
          
          item.count = i;
          i++;
        }
      });
    });
    this.generateTable();
  }
  generateTable() {
    let tableheader = '';
    let tableBody = '';
    this.washHours = 0;
    this.detailHours = 0;
    const count = Math.max(...this.clockDetail?.map(val => val.count));
    tableheader = `<tr><th scope="col">Employee Name</th><th scope="col">Wash Hours</th><th scope="col">Detail Hours</th>
    <th scope="col">Total Hours</th>`
    for (let i = 0; i < count; i++) {
      tableheader += `<th scope="col">In</th><th scope="col">Out</th><th scope="col">Role</th>`;
    }
    tableheader += `</tr>`;
    $('#table thead').html(tableheader);
    this.clockDetail.forEach(item => {
      tableBody += `<tr><td>` + item.EmployeeName + `</td><td>` + (item?.WashHours ? item?.WashHours : 0) + `</td><td>` +
        (item?.DetailHours ? item.DetailHours : 0) + `</td><td>` + (item?.WashHours + item?.DetailHours) + `</td>`;
      for (let i = 1; i <= count; i++) {
        const Intime = 'Intime' + i;
        const Outtime = 'Outtime' + i;
        const RoleName = 'RoleName' + i;

        const TimeIn = 'TimeIn' + i;
        const TimeOut = 'TimeOut' + i;
        // tableBody += `<td>` + (item[Intime] !== undefined ? this.datePipe.transform(item[Intime] , 'hh:mm:ss') : '') + `</td><td>`+ 
        // (item[Outtime] !== undefined ? this.datePipe.transform(item[Outtime] , 'hh:mm:ss') : '') + `</td><td>` +
        // (item[RoleName] !== undefined ? item[RoleName] : '') + `</td>`;

         tableBody += `<td>` + 
         item[TimeIn] + `</td><td>` +
         item[TimeOut] + `</td><td>` +
         item[RoleName] + `</td>`;
      }
      this.washHours += item.WashHours;
      this.detailHours += item.DetailHours;
      this.totalAmount += item.TotalAmount;
    });
    tableBody += `</tr>`;
    $('#table tbody').html(tableBody);
  }
  getDailyStatusReport() {
    this.washes = [];
    this.details = [];
    this.washTotal = 0;
    this.detailTotal = 0;
    const obj = {
      locationId: +this.locationId,
      date: moment(this.date).format('YYYY-MM-DD')
    };
    this.spinner.show();
    this.reportService.getDailyStatusReport(obj).subscribe(data => {
      if (data.status === 'Success') {
        this.spinner.hide();

        const dailyStatusReport = JSON.parse(data.resultData);
        this.dailyStatusReport = dailyStatusReport.GetDailyStatusReport;
        this.details = this.dailyStatusReport.filter(item => item.JobType === 'Detail');

        if (this.dailyStatusReport.length > 0) {
          this.washes = this.dailyStatusReport.filter(item => item.JobType === 'Wash');
          this.details = this.dailyStatusReport.filter(item => item.JobType === 'Detail');
          
          this.washTotal = this.calculateTotal(this.washes, 'wash');
          this.detailTotal = this.calculateTotal(this.details, 'detail');
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
  getDailyStatusWashReport() {
    this.washes = [];
    this.details = [];
    this.washTotal = 0;
    this.detailTotal = 0;
    const obj = {
      locationId: +this.locationId,
      date: moment(this.date).format('YYYY-MM-DD')
    };
    this.spinner.show();
    this.reportService.getDailyStatusWashReport(obj).subscribe(data => {
      if (data.status === 'Success') {
        this.spinner.hide();

        const dailyStatusReport = JSON.parse(data.resultData);
        this.dailyStatusReport = dailyStatusReport.GetDailyStatusReport;

        if (this.dailyStatusReport.length > 0) {
          this.washes = this.dailyStatusReport.filter(item => item.JobType === 'Wash');
          this.details = this.dailyStatusReport.filter(item => item.JobType === 'Detail');
         
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
  getDailyStatusDetailInfo() {
    const obj = {
      locationId: +this.locationId,
      date: moment(this.date).format('YYYY-MM-DD')
    };
    this.spinner.show();
    this.reportService.getDailyStatusDetailInfo(obj).subscribe(data => {
      if (data.status === 'Success') {
        this.spinner.hide();

        const dailyStatusDetailInfo = JSON.parse(data.resultData);
        this.dailyStatusDetailInfo = dailyStatusDetailInfo?.GetDailyStatusReport?.DailyStatusDetailInfo;
       this.dailyStatusWashInfo = dailyStatusDetailInfo?.GetDailyStatusReport?.DailyStatusWashInfo
        this.detailInfoTotal = this.calculateTotal(this.dailyStatusDetailInfo, 'detailInfo');
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
    $('#printReport').show();
    const fileType = this.fileType !== undefined ? this.fileType : '';
    const locationName = this.locationDropdownComponent.locationName;
    if (fileType === '' || fileType === 0) {
      $('#printReport').hide();
      return;
    } else {
      if (this.washes.length === 0 && this.details.length === 0 && this.clockDetail.length === 0) {
        $('#printReport').hide();
        return;
      }
    }
    switch (fileType) {
      case 1: {
        this.excelService.exportAsPDFFile('dailyStatusReport', 'DailyStatusReport_' +
        moment(this.date).format('MM/dd/yyyy') + '_' + locationName + '.pdf');
        break;
      }
      case 2: {
        let excelWashReport : any = [];
      
          if(this.washes.length > 0){
            for(let i = 0; i < this.washes.length; i ++){
              excelWashReport.push({
              'Number': this.washes[i].Number,
              'ServiceName': this.washes[i].ServiceName,
              'JobType' : this.washes[i].JobType
            })
            }
          }
          let excelDetailReport : any = [];
      
          if(this.details.length > 0){
            for(let i = 0; i < this.details.length; i ++){
              excelDetailReport.push({
              'Number': this.details[i].Number,
              'ServiceName': this.details[i].ServiceName,
              'JobType' : this.details[i].JobType
            })
            }
          }
          let employeeExportDetail : any = [];

          if(this.clockDetail.length > 0){
            for(let i = 0; i < this.clockDetail.length; i ++){
              employeeExportDetail.push({
              'Employee Name': this.clockDetail[i].EmployeeName,
              'Wash Hours': this.clockDetail[i].WashHours,
              'Detail Hours' : this.clockDetail[i].DetailHours,
              'Total Hours': this.clockDetail[i].WashHours + this.clockDetail[i].DetailHours,
              'In': this.clockDetail[i].Intime1 ? this.datePipe.transform( this.clockDetail[i].Intime1, 'hh:mm:ss') : '' ,
              'Out' :this.clockDetail[i].Outtime1 ? this.datePipe.transform( this.clockDetail[i].Outtime1, 'hh:mm:ss'): '' ,
              'Role' : this.clockDetail[i].RoleName1,


            })
            console.log(employeeExportDetail, 'export')
            }
          }
      
      
        this.excelService.exportAsCSVFile(excelWashReport, 'DailyWashStatusReport_' +
        moment(this.date).format('MM/DD/YYYY') + '_' + locationName);
        this.excelService.exportAsCSVFile(excelDetailReport, 'DailyDetailStatusReport_' +
        moment(this.date).format('MM/DD/YYYY') + '_' + locationName);
        this.excelService.exportAsCSVFile(employeeExportDetail, 'DailyEmployeeClockDetailsReport_' +
        moment(this.date).format('MM/DD/YYYY') + '_' + locationName);

        break;
      }
      case 3: {
         const obj = {
          locationId: +this.locationId,
          date: moment(this.date).format('YYYY-MM-DD'),
          cashRegisterType : "CLOSEOUT"

        };
        this.reportService.getDailyStatusExcelReport(obj).subscribe(data =>{
          if(data){
            this.download(data, 'excel', 'Daily Status Report');
           

            return data; 
               }
      })
      
        break;
      }
      default: {
        return;
      }
    }
    $('#printReport').hide();
  }

  download(data: any, type, fileName = 'Excel'){
    let format: string;
    format = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet';
    let a: HTMLAnchorElement;
    a = document.createElement('a');
    document.body.appendChild(a);
    const blob = new Blob([data], { type: format });
    const url = window.URL.createObjectURL(blob);
    a.href = url;
    a.download = fileName;
    a.click();
  }
  preview() {
    this.getDailyStatusReport();
    this.getDailyStatusWashReport();

    this.getDailyStatusDetailInfo();
    this.getClockDetail();
  }
  print() {
    $('#printReport').show();
    setTimeout(() => {
      $('#printReport').hide();
    }, 1000);
  }
  calculateTotal(obj, type) {
    return obj?.reduce((sum, i) => {
      return sum + (type === 'detailInfo' ? +i.Commission : +i.Number);
    }, 0)
  }
}
