import { Component, OnInit, ViewChild } from '@angular/core';
import { LocationDropdownComponent } from 'src/app/shared/components/location-dropdown/location-dropdown.component';
import * as moment from 'moment';
import { ExportFiletypeComponent } from 'src/app/shared/components/export-filetype/export-filetype.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { ReportsService } from 'src/app/shared/services/data-service/reports.service';
import { ToastrService } from 'ngx-toastr';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { ExcelService } from 'src/app/shared/services/common-service/excel.service';
import { DatePipe } from '@angular/common';
declare var $: any;

@Component({
  selector: 'app-irregularity-report',
  templateUrl: './irregularity-report.component.html',
  styleUrls: ['./irregularity-report.component.css']
})
export class IrregularityReportComponent implements OnInit {

  @ViewChild(LocationDropdownComponent) locationDropdownComponent: LocationDropdownComponent;
  @ViewChild(ExportFiletypeComponent) exportFiletypeComponent: ExportFiletypeComponent;

  daterangepickerModel: any;
  todayDate = new Date();
  startDate: any;
  currentWeek: any;
  endDate: any;
  dateRange: any = [];
  locationId: any;
  fileTypeEvent: boolean = false;
  fileType: number;
  vehicleInfo = null;
  ticketInfo = null;
  couponsInfo = null;
  depositsInfo = null;
  totalIrregularityReport: any;

  constructor(private spinner: NgxSpinnerService, private excelService: ExcelService, private datePipe: DatePipe, private reportsService: ReportsService, private toastr: ToastrService) { }

  ngOnInit() {
    this.locationId = localStorage.getItem('empLocationId');
    this.weeklyDateAssign();
    this.viewRegularReport();
  }


  weeklyDateAssign() {
    const currentDate = new Date();
    const first = currentDate.getDate() - currentDate.getDay();
    const last = first + 7;
    this.startDate = new Date(currentDate.setDate(first));
    this.currentWeek = this.startDate;
    this.endDate = new Date(currentDate.setDate(last));
    this.endDate = this.endDate.setDate(this.startDate.getDate() + 7);
    this.endDate = new Date(moment(this.endDate).format());
    this.daterangepickerModel = [this.startDate, this.endDate];
  }


  onValueChange(event) {
    console.log(event, 'date view');
  }

  viewRegularReport() {
    const finalObj = {
      locationId: +this.locationId,
      fromDate: moment(this.startDate).format('YYYY-MM-DD'),
      endDate: moment(this.endDate).format('YYYY-MM-DD')
    };
    this.spinner.show();
    this.reportsService.getIrregularityReports(finalObj).subscribe(data => {
      if (data.status === 'Success') {
        this.spinner.hide();
        this.totalIrregularityReport = JSON.parse(data.resultData);
        this.vehicleInfo = this.totalIrregularityReport.GetIrregularitiesReport?.VehiclesInfo
        this.ticketInfo = this.totalIrregularityReport.GetIrregularitiesReport?.MissingTicket
        this.couponsInfo = this.totalIrregularityReport.GetIrregularitiesReport?.Coupon
        this.depositsInfo = this.totalIrregularityReport.GetIrregularitiesReport?.DepositOff
      }
    }, (err) => {
      console.log(err);
      this.spinner.hide();
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  onLocationChange(event) {
    this.locationId = +event;
  }

  refesh() {
    this.locationId = +localStorage.getItem('empLocationId');
    this.locationDropdownComponent.locationId = this.locationId;
    this.exportFiletypeComponent.type = '';
    this.weeklyDateAssign();
    this.viewRegularReport();
  }

  getfileType(event) {
    this.fileTypeEvent = true;
    this.fileType = +event.target.value;
  }



  export() {
    $('#printReport').show();
    const fileType = this.fileType !== undefined ? this.fileType : '';
    const locationName = this.locationDropdownComponent.locationName;
    if (fileType === '' || fileType === 0) {
      $('#printReport').hide();
      return;
    }
    switch (fileType) {
      case 1: {
        this.excelService.exportAsPDFFile('irregularityReport', 'IrregularityReport_' + this.datePipe.transform(this.todayDate, 'MM')
          + '/' + this.datePipe.transform(this.todayDate, 'yyyy')
          + '_' + locationName + '.pdf');
        $('#printReport').hide();
        break;
      }
      case 2: {
        $('#printReport').hide();
        this.excelService.exportAsCSVFile(this.vehicleInfo, 'VehicleInfoReport_' +
          this.datePipe.transform(this.todayDate, 'MM') + '/' + this.datePipe.transform(this.todayDate, 'yyyy') + '_' + locationName);
        this.excelService.exportAsCSVFile(this.ticketInfo, 'TicketInfoReport_' +
          this.datePipe.transform(this.todayDate, 'MM') + '/' + this.datePipe.transform(this.todayDate, 'yyyy') + '_' + locationName);
        this.excelService.exportAsCSVFile(this.couponsInfo, 'CouponInfoReport_' +
          this.datePipe.transform(this.todayDate, 'MM') + '/' + this.datePipe.transform(this.todayDate, 'yyyy') + '_' + locationName);
        this.excelService.exportAsCSVFile(this.depositsInfo, 'DepositInfoReport_' +
          this.datePipe.transform(this.todayDate, 'MM') + '/' + this.datePipe.transform(this.todayDate, 'yyyy') + '_' + locationName);
        break;
      }
      case 3: {
        $('#printReport').hide();
        const finalObj = {
          locationId: +this.locationId,
          fromDate: moment(this.startDate).format('YYYY-MM-DD'),
          endDate: moment(this.endDate).format('YYYY-MM-DD')
        };
        this.reportsService.getIrregularityExport(finalObj).subscribe(data => {
          if (data) {
            this.download(data, 'excel', 'Irregularity Reports');
            return data;
          }
        }, (err) => {
          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        });
        break;
      }
      default: {
        return;
      }
    }
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

  print() {
    $('#printReport').show();
    setTimeout(() => {
      $('#printReport').hide();
    }, 1000);
  }

  printReport() {
    $('#regularReports').show();
    const dataURL = document.getElementById('regularReports').innerHTML;
    const dateAndTimeStamp = moment(new Date()).format('M/d/YY, h:mm a');
    const content = '<!DOCTYPE html><html><head><title>Irregularity Report</title><link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css"/>'
      + '<link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css"/><style>'
      + '</style><script src="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"></script></head><body><table><thead id="header"><tr><td>'
      + '</td></tr><tr><td><div>' + '</div></td></tr></thead><tbody><tr><td><div class="upperTeethData print-table-border"><div></div><div>' + dataURL + '</div></div></td></tr><tr><td>'
      + '<div class="lowerTeethData print-table-border"><div></div><div> </div></div></td></tr><tr><td><div class="casetype print-table-border"></div>'
      + '</td></tr></tbody><tfoot><tr><td><div style="width:100%;" id="footer">' + '<div style="font-size:14px;margin-right:15px;float:right;">' + dateAndTimeStamp +
      '</div></div></td></tr></tfoot></table><body></html>';
    const popupWin = window.open('', '_blank', 'scrollbars=1,width:100%;height:100%');
    popupWin.document.open();
    popupWin.document.write(content);
    popupWin.document.close(); // necessary for IE >= 10
    popupWin.focus(); // necessary for IE >= 10*/
    setTimeout(() => {
      popupWin.print();
      popupWin.close();
    }, 2000);
    $('#regularReports').hide();
  }



}
