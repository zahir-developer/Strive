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
import { ExportFiletypeComponent } from 'src/app/shared/components/export-filetype/export-filetype.component';
@Component({
  selector: 'app-daily-status',
  templateUrl: './daily-status.component.html'
})
export class DailyStatusComponent implements OnInit, AfterViewInit {
  @ViewChild('dp', { static: false }) datepicker: BsDaterangepickerDirective;
  @ViewChild(LocationDropdownComponent) locationDropdownComponent: LocationDropdownComponent;
  @ViewChild(ExportFiletypeComponent) exportFiletypeComponent: ExportFiletypeComponent;
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
  totalWashHours = "";
  totalDetailHours = "";
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
    private toastr: ToastrService) {

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
    var curDate = new Date();
    var curr_date = curDate.getDate();

    const obj = {
      locationId: +this.locationId,
      date: moment(this.date).format('YYYY-MM-DD'),
      CurrentDate: this.date.getDate() == curr_date ?  this.datePipe.transform(new Date(), 'yyyy-MM-dd HH:mm:ss') : moment(this.date).format('YYYY-MM-DD') 

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
      else {
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
      tableBody += `<tr><td>` + item.EmployeeName + `</td><td>`;
      tableBody += (item?.WashHours ?
        (item?.WashHours)
        : "0") + `</td><td>`;

        tableBody += (item?.DetailHours ?
        (item?.DetailHours)
        : "0") + `</td><td>`;
/*
        var decimalTimeString = (item?.WashHours + item?.DetailHours).toFixed(2);
        const hrs = decimalTimeString.toString().split(".");
        var n = new Date(0,0);
        n.setSeconds(+hrs[0] * 60 * 60);
        if(hrs.length>=2){
        n.setSeconds(+hrs[1] * 60 );
        }*/
      
      tableBody += item?.TotalHours.toFixed(2)
      // (n.toTimeString().slice(0, 5)).toString().replace(".",":")
        + `</td>`;

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
          (item[TimeIn] != undefined ? item[TimeIn] : '') + `</td><td>` +
          (item[TimeOut] != undefined ? item[TimeOut] : '') + `</td><td>` +
          (item[RoleName] != undefined ? item[RoleName] : '') + `</td>`;
      }
      this.washHours += item.WashHours;
      this.detailHours += item.DetailHours;
      this.totalAmount += item.TotalAmount;
    });
    tableBody += `</tr>`;
    $('#table tbody').html(tableBody);
    this.totalWashHours = this.washHours > 0 ?
    this.washHours.toFixed(2).toString()
      : "0";


    this.totalDetailHours = this.detailHours > 0 ?
    this.detailHours.toFixed(2).toString()
      : "0";
   // this.totalWashHours = (this.washHours +this.detailHours).toString().replace(".",":");
/*
    var hrs =  this.washHours.toString().split(".");
    var n = new Date(0,0);
    n.setSeconds(+hrs[0] * 60 * 60);
    if(hrs.length>=2){
    n.setSeconds(+hrs[1] * 60 );
    }
  
   
    this.totalWashHours = this.washHours > 0 ?
    n.toTimeString().slice(0, 5).replace(".",":")
      : "0";

      hrs =  this.detailHours.toString().split(".");
      var n = new Date(0,0);
      n.setSeconds(+hrs[0] * 60 * 60);
      if(hrs.length>=2){
      n.setSeconds(+hrs[1] * 60 );
      }

    this.totalDetailHours = this.detailHours > 0 ?
    n.toTimeString().slice(0, 5).replace(".",":")
      : "0";*/
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
      else {
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
    var curDate = new Date();
    var curr_date = curDate.getDate();

    const obj = {
      locationId: +this.locationId,
      date: curr_date == this.date.getDate() ? this.datePipe.transform(this.date, 'yyyy-MM-dd HH:mm:ss') : this.datePipe.transform(this.date, 'yyyy-MM-dd')
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
      else {
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');

      }
    }, (err) => {
      this.spinner.hide();
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }
  getDailyStatusDetailInfo() {
    var curDate = new Date();
    var curr_date = curDate.getDate();

    const obj = {
      locationId: +this.locationId,
      date: this.date.getDate() == curr_date ? this.datePipe.transform(new Date(), 'yyyy-MM-dd HH:mm:ss') :  moment(this.date).format('YYYY-MM-DD') 
    };
    this.spinner.show();
    this.reportService.getDailyStatusDetailInfo(obj).subscribe(data => {
      if (data.status === 'Success') {
        this.spinner.hide();
        const dailyStatusDetailInfo = JSON.parse(data.resultData);
        this.dailyStatusDetailInfo = dailyStatusDetailInfo?.GetDailyStatusReport?.DailyStatusDetailInfo;
        this.dailyStatusWashInfo = dailyStatusDetailInfo?.GetDailyStatusReport?.DailyStatusWashInfo;
        this.detailInfoTotal = this.calculateTotal(this.dailyStatusDetailInfo, 'detailInfo');
      }
      else {
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
        let excelWashReport: any = [];

        if (this.washes.length > 0) {
          for (let i = 0; i < this.washes.length; i++) {
            excelWashReport.push({
              'Number': this.washes[i].Number,
              'ServiceName': this.washes[i].ServiceName,
              'JobType': this.washes[i].JobType
            })
          }
        }
        let excelDetailReport: any = [];

        if (this.details.length > 0) {
          for (let i = 0; i < this.details.length; i++) {
            excelDetailReport.push({
              'Number': this.details[i].Number,
              'ServiceName': this.details[i].ServiceName,
              'JobType': this.details[i].JobType
            })
          }
        }
        let employeeExportDetail: any = [];

        if (this.clockDetail.length > 0) {
          const count = Math.max(...this.clockDetail?.map(val => val.count));
          for (let i = 0; i < this.clockDetail.length; i++) {
            debugger;            
            employeeExportDetail.push({
             
                            'Employee Name': this.clockDetail[i].EmployeeName,
                            'Wash Hours': this.clockDetail[i].WashHours,
                            'Detail Hours': this.clockDetail[i].DetailHours,
                            'Total Hours': this.clockDetail[i].WashHours + this.clockDetail[i].DetailHours,
                            // 'In': this.clockDetail[i].TimeIn1 ? this.clockDetail[i].TimeIn1 : '',
                            // 'Out': this.clockDetail[i].TimeOut1 ? this.clockDetail[i].TimeOut1 : '',
                            // 'Role': this.clockDetail[i].RoleName1,              
                          });
                          
            // var detail ="";
            for (let j = 1; j <= count; j++) {
            const TimeIn = 'TimeIn' + j;
            const TimeOut = 'TimeOut' + j;
            const RoleName = 'RoleName' + j;
            
            employeeExportDetail[i][TimeIn] = this.clockDetail[i][TimeIn]!= undefined  ? this.clockDetail[i][TimeIn] : '', 

            employeeExportDetail[i][TimeOut] = this.clockDetail[i][TimeOut] != undefined ? this.clockDetail[i][TimeOut] : '',
            employeeExportDetail[i][RoleName]= this.clockDetail[i][RoleName] != undefined ? this.clockDetail[i][RoleName] : '' ,

            console.log(employeeExportDetail, 'export')
          }}
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
          cashRegisterType: "CLOSEOUT"

        };
        this.reportService.getDailyStatusExcelReport(obj).subscribe(data => {
          if (data) {
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

  download(data: any, type, fileName = 'Excel') {
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

  refresh() {
    this.locationId = localStorage.getItem('empLocationId');
    this.date = new Date();
    this.locationDropdownComponent.locationId = +localStorage.getItem('empLocationId')
    this.exportFiletypeComponent.type = '';
    this.getDailyStatusReport();
    this.getDailyStatusWashReport();
    this.getDailyStatusDetailInfo();
    this.getClockDetail();
  }
  print() {
    const body = document.getElementById('dailyStatusReport').innerHTML;  // @media print{body{ width: 950px; background-color: red;} }'

    const content = '<!DOCTYPE html><html><head><title></title><link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css"/>'
      + '<link rel = "stylesheet" type = "text/css" media = "print"/><style type = "text/css">  @media print {@page {size: landscape;margin: 5mm 5mm 0mm 5mm;}}'
      + '</style><script src="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"></script></head><body><table><thead id="header"><tr><td>'
      + '</td></tr><tr><td><div class="fixed-top" "><div style="font-size:14px;margin-right:15px;text-align:center">' + 'Daily status report' +'</div></div></td></tr></thead><tbody><tr><td><div class="upperTeethData print-table-border"><div></div><div style="position:relative; top:100px">' + body + '</div></div></td></tr><tr><td>'
      + '<div class="lowerTeethData print-table-border"><div></div><div> </div></div></td></tr><tr><td><div class="casetype print-table-border"></div>'
      + '</td></tr></tbody><tfoot><tr><td><div class="fixed-bottom border-top" id="footer">' + '<div style="font-size:14px;margin-right:15px;float:left;">' +
      '</div></div></td></tr></tfoot></table><body></html>';
    const popupWin = window.open('', '_blank', 'scrollbars=1,width:100%;height:100%');
    popupWin.document.open();
    popupWin.document.write(content);
    popupWin.document.close(); // necessary for IE >= 10
    popupWin.focus(); // necessary for IE >= 10*/
    setTimeout(() => {
      popupWin.print();
      popupWin.close();
    }, 1000);
  }
  calculateTotal(obj, type) {
    return obj?.reduce((sum, i) => {
      return sum + (type === 'detailInfo' ? +i.Commission : +i.Number);
    }, 0)
  }
}
