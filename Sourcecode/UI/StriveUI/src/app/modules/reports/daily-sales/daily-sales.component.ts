import { AfterViewInit, Component, OnInit, ViewChild, ChangeDetectorRef } from '@angular/core';
import { ReportsService } from 'src/app/shared/services/data-service/reports.service';
import { ExcelService } from 'src/app/shared/services/common-service/excel.service';
import * as moment from 'moment';
import { BsDaterangepickerDirective, BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { ToastrService } from 'ngx-toastr';
import { ExportFiletypeComponent } from 'src/app/shared/components/export-filetype/export-filetype.component';
import { LocationDropdownComponent } from 'src/app/shared/components/location-dropdown/location-dropdown.component';
@Component({
  selector: 'app-daily-sales',
  templateUrl: './daily-sales.component.html'
})
export class DailySalesComponent implements OnInit, AfterViewInit {
  @ViewChild('dp', { static: false }) datepicker: BsDaterangepickerDirective;
  @ViewChild(LocationDropdownComponent) locationDropdownComponent: LocationDropdownComponent;
  @ViewChild(ExportFiletypeComponent) exportFiletypeComponent: ExportFiletypeComponent;
  bsConfig: Partial<BsDatepickerConfig>;
  maxDate = new Date();
  locationId: any;
  fileType: number;
  dailySalesReport = [];
  date = moment(new Date()).format('MM/DD/YYYY');
  isTableEmpty: boolean;
  page = 1;
  pageSize = 25;
  collectionSize: number = 0;
  fileTypeEvent: boolean = false;

  constructor(private spinner: NgxSpinnerService, private toastr: ToastrService,
    private cd: ChangeDetectorRef, private reportService: ReportsService, private excelService: ExcelService) { }

  ngOnInit(): void {
    this.locationId = localStorage.getItem('empLocationId');
    this.getDailySalesReport();
  }
  ngAfterViewInit() {
    this.bsConfig = Object.assign({}, { maxDate: this.maxDate, dateInputFormat: 'MM/DD/YYYY', showWeekNumbers: false });
    this.datepicker.setConfig();
    this.cd.detectChanges();
  }
  getDailySalesReport() {
    const obj = {
      locationId: this.locationId,
      date: this.date
    };
    this.spinner.show();
    this.reportService.getDailySalesReport(obj).subscribe(data => {
      if (data.status === 'Success') {
        this.spinner.hide();
        const sales = JSON.parse(data.resultData);
        this.dailySalesReport = sales.GetDailySalesReport;
        if (this.dailySalesReport.length === 0) {
          this.isTableEmpty = true;
        } else {
          if (this.dailySalesReport?.length > 0) {
            for (let i = 0; i < this.dailySalesReport.length; i++) {
              this.dailySalesReport[i].Model == 'None' ? this.dailySalesReport[i].Model = 'Unk' : this.dailySalesReport[i].Model;
            }
          }
          this.collectionSize = Math.ceil(this.dailySalesReport.length / this.pageSize) * 10;
          this.isTableEmpty = false;
        }
      } else {
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
  getfileType(event) {
    this.fileTypeEvent = true;

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
        const report = this.salesReport(this.dailySalesReport);
        this.excelService.exportAsCSVFile(report, 'dailySalesReport_' + moment(this.date).format('MM/dd/yyyy'));
        break;
      }
      case 3: {
        const report = this.salesReport(this.dailySalesReport);
        this.excelService.exportAsExcelFile(report, 'dailySalesReport_' + moment(this.date).format('MM/dd/yyyy'));
        break;
      }
      default: {
        return;
      }
    }
  }

  salesReport(record) {
    const sales = [];
    record.filter( item => {
      sales.push({
        TicketNumber: item.TicketNumber,
        TimeIn: item.TimeIn,
        TimeOut: item.TimeOut,
        Est: item.Est,
        Deviation: item.Deviation,
        ServiceName: item.ServiceName,
        ServiceType: item.ServiceType,
        MerchandiseItemsPurchased: item.MerchandiseItemsPurchased,
        Barcode: item.Barcode,
        Make: item.Make,
        Color: item.Color,
        Model: item.Model,
        CustomerName: item.CustomerName,
        PhoneNumber: item.PhoneNumber,
        Amount: item.Amount,
        Type: item.Type
      });
    });
    return sales;
  }

  onValueChange(event) {
    let selectedDate = event;
    if (selectedDate !== null) {
      selectedDate = moment(event.toISOString()).format('MM/DD/YYYY');
      this.date = selectedDate;
    }
    this.getDailySalesReport();
  }

  refresh() {
    this.locationId = +localStorage.getItem('empLocationId');
    this.date = moment(new Date()).format('MM/DD/YYYY');
    this.locationDropdownComponent.locationId = +localStorage.getItem('empLocationId')
    this.exportFiletypeComponent.type = '';
    this.getDailySalesReport();
  }

  print(): void {
    const body = document.getElementById('dailySalesReport').innerHTML;  // @media print{body{ width: 950px; background-color: red;} }'

    const content = '<!DOCTYPE html><html><head><title>Daily Sales Report</title><link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css"/>'
      + '<link rel = "stylesheet" type = "text/css" media = "print"/><style type = "text/css">  @media print {@page {size: landscape;margin: 0mm 5mm 0mm 5mm;}}'
      + '</style><script src="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"></script></head><body><table><thead id="header"><tr><td>'
      + '</td></tr><tr><td><div class="fixed-top" "><div style="font-size:44px;margin-right:15px;text-align:center">' + 'Daily Sales Report' + '</div></div></td></tr></thead><tbody><tr><td><div class="upperTeethData print-table-border"><div></div><div style="position:relative; top:100px">' + body + '</div></div></td></tr><tr><td>'
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
}
