import { Component, OnInit, ViewChild, ChangeDetectorRef, AfterViewInit } from '@angular/core';
import { BsDatepickerConfig, BsDaterangepickerDirective } from 'ngx-bootstrap/datepicker';
import { ReportsService } from 'src/app/shared/services/data-service/reports.service';
import { ExcelService } from 'src/app/shared/services/common-service/excel.service';
import * as moment from 'moment';
import { LocationDropdownComponent } from 'src/app/shared/components/location-dropdown/location-dropdown.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
declare var $: any;

@Component({
  selector: 'app-eod',
  templateUrl: './eod.component.html',
  styleUrls: ['./eod.component.css']
})
export class EodComponent implements OnInit, AfterViewInit {
  @ViewChild(LocationDropdownComponent) locationDropdownComponent: LocationDropdownComponent;
  @ViewChild('dp', { static: false }) datepicker: BsDaterangepickerDirective;
  bsConfig: Partial<BsDatepickerConfig>;
  date = new Date();
  maxDate = new Date();
  selectDate: any;
  cashRegisterCoins: any;
  cashRegisterBills: any;
  cashRegisterRolls: any;
  locationId: any;
  dailyStatusReport = [];
  fileType: number;
  washes = [];
  details = [];
  washTotal = 0;
  detailTotal = 0;
  detailInfoTotal = 0;
  dailyStatusDetailInfo = [];
  clockDetail = [];
  salesReport: any;
  empTotalHours = 0;
  isPrintReport: boolean;
  washReport = [];
  detailReport = [];
  serviceTotal = 0;
  fileTypeEvent: boolean = false;
  constructor(
    private cd: ChangeDetectorRef,
    private reportService: ReportsService,
    private excelService: ExcelService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.isPrintReport = false;
    this.locationId = localStorage.getItem('empLocationId');
    this.selectDate = moment(new Date()).format('MM-DD-YYYY');
    this.getEodSalesReport();
    this.getDailyStatusReport();
    this.getDailyStatusDetailInfo();
    this.getClockDetail();
    this.getCashRegister();
  }

  ngAfterViewInit() {
    this.bsConfig = Object.assign({}, { maxDate: this.maxDate, dateInputFormat: 'MM/DD/YYYY', showWeekNumbers: false  });
    this.datepicker.setConfig();
    this.cd.detectChanges();
  }

  onValueChange(event) {
    if (event !== null) {
      this.selectDate = event;
      this.getCashRegister();
    }
  }

  preview() {
    this.getEodSalesReport();
    this.getDailyStatusReport();
    this.getDailyStatusDetailInfo();
    this.getClockDetail();
  }

  getCashRegister() {
    const date = moment(this.selectDate).format('YYYY-MM-DD');
    const cashRegisterType = 'CLOSEOUT';
    const locationId = +localStorage.getItem('empLocationId');
    this.reportService.getCashRegisterByDate(cashRegisterType, locationId, date).subscribe(res => {
      if (res.status === 'Success') {
        const cashIn = JSON.parse(res.resultData);
        if (cashIn.CashRegister.CashRegisterCoins !== null) {
          this.cashRegisterCoins = {
            Pennies: cashIn.CashRegister.CashRegisterCoins.Pennies / 100,
            Nickels: (5 * cashIn.CashRegister.CashRegisterCoins.Nickels) / 100,
            Dimes: (10 * cashIn.CashRegister.CashRegisterCoins.Dimes) / 100,
            Quarters: (25 * cashIn.CashRegister.CashRegisterCoins.Quarters) / 100,
            HalfDollars: (50 * cashIn.CashRegister.CashRegisterCoins.HalfDollars) / 100
          };
        } else {
          this.cashRegisterCoins = {
            Pennies: 0,
            Nickels: 0,
            Dimes: 0,
            Quarters: 0,
            HalfDollars: 0
          };
        }
        if (cashIn.CashRegister.CashRegisterBills !== null) {
          this.cashRegisterBills = {
            s1: cashIn.CashRegister.CashRegisterBills.s1,
            s5: (5 * cashIn.CashRegister.CashRegisterBills.s5),
            s10: (10 * cashIn.CashRegister.CashRegisterBills.s10),
            s20: (20 * cashIn.CashRegister.CashRegisterBills.s20),
            s50: (50 * cashIn.CashRegister.CashRegisterBills.s50),
            s100: (100 * cashIn.CashRegister.CashRegisterBills.s100)
          };
        } else {
          this.cashRegisterBills = {
            s1: 0,
            s5: 0,
            s10: 0,
            s20: 0,
            s50: 0,
            s100: 0
          };
        }
        if (cashIn.CashRegister.CashRegisterRolls !== null) {
          this.cashRegisterRolls = {
            Pennies: cashIn.CashRegister.CashRegisterRolls.Pennies !== null ? cashIn.CashRegister.CashRegisterRolls.Pennies / 100 : 0,
            Nickels: cashIn.CashRegister.CashRegisterRolls.Nickels !== null ? (5 * cashIn.CashRegister.CashRegisterRolls.Nickels) / 100
              : 0,
            Dimes: cashIn.CashRegister.CashRegisterRolls.Dimes !== null ? (10 * cashIn.CashRegister.CashRegisterRolls.Dimes) / 100 : 0,
            Quarters: cashIn.CashRegister.CashRegisterRolls.Quarters !== null ? (25 * cashIn.CashRegister.CashRegisterRolls.Quarters) / 100
              : 0,
            HalfDollars: cashIn.CashRegister.CashRegisterRolls.HalfDollars !== null ?
              (50 * cashIn.CashRegister.CashRegisterRolls.HalfDollars) / 100 : 0
          };
        } else {
          this.cashRegisterRolls = {
            Pennies: 0,
            Nickels: 0,
            Dimes: 0,
            Quarters: 0,
            HalfDollars: 0
          };
        }
      }
    }, (err) => {
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
    $('#printReport').show();
    const fileType = this.fileType !== undefined ? this.fileType : '';
    const locationName = this.locationDropdownComponent.locationName;
    if (fileType === '' || fileType === 0) {
      return;
    } else if (this.dailyStatusReport.length === 0) {
      return;
    }
    switch (fileType) {
      case 1: {
        this.excelService.exportAsPDFFile('EodStatusReport', 'EodStatusReport_' + moment(this.date).format('MM/dd/yyyy')
        + '_' + locationName + '.pdf');
        break;
      }
      case 2: {
        this.excelService.exportAsCSVFile(this.washes, 'EodWashStatusReport_' + 
        moment(this.date).format('MM/dd/yyyy') + '_' + locationName);
        this.excelService.exportAsCSVFile(this.details, 'EodDetailStatusReport_' +
        moment(this.date).format('MM/dd/yyyy') + '_' + locationName);
        this.excelService.exportAsCSVFile(this.clockDetail, 'EodEmployeeClockDetailsReport_' +
        moment(this.date).format('MM/DD/YYYY') + '_' + locationName);
        break;
      }
      case 3: {
        const obj = {
          locationId: +this.locationId,
          date: moment(this.date).format('YYYY-MM-DD'),
          cashRegisterType : "CLOSEOUT"

        };
        this.reportService.getEODexcelReport(obj).subscribe(data =>{
          if(data){
            this.download(data, 'excel', 'EOD Report');
           

            return data; 
               }
          

        }, (err) => {
          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
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
  print() {
    $('#printReport').show();
    setTimeout(() => {
      $('#printReport').hide();
    }, 1000);
  }

  getDailyStatusReport() {
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
        if (this.dailyStatusReport.length > 0) {
          this.washes = [];
          this.details = []
          this.washes = this.dailyStatusReport.filter(item => item.JobType === 'Wash');
          this.details = this.dailyStatusReport.filter(item => item.JobType === 'Detail');
          this.washTotal = this.calculateTotal(this.washes, 'wash');
          this.detailTotal = this.calculateTotal(this.details, 'detail');
          this.serviceTotal = this.washTotal + this.detailTotal;
    
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
    this.reportService.getDailyStatusDetailInfo(obj).subscribe(data => {
      if (data.status === 'Success') {
        const dailyStatusDetailInfo = JSON.parse(data.resultData);
        this.dailyStatusDetailInfo = dailyStatusDetailInfo?.GetDailyStatusReport?.DailyStatusDetailInfo;
        this.detailInfoTotal = this.calculateTotal(this.dailyStatusDetailInfo, 'detailInfo');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  refresh() {
    this.preview();
  }

  getClockDetail() {
    const obj = {
      locationId: +this.locationId,
      date: moment(this.date).format('YYYY-MM-DD')
    };
    this.clockDetail = [];

    this.reportService.getTimeClockEmpHoursDetail(obj).subscribe(data => {
      if (data.status === 'Success') {

        const clockDetail = JSON.parse(data.resultData);
        if (clockDetail.Result.TimeClockEmployeeDetails !== null) {
       
          this.clockDetail = clockDetail.Result.TimeClockEmployeeDetails;
          this.clockDetail.forEach(item => {
            this.empTotalHours = this.empTotalHours + item.HoursPerDay;
          });
        }
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  calculateTotal(obj, type) {
    return obj?.reduce((sum, i) => {
      return sum + (type === 'detailInfo' ? +i.Commission : +i.Number);
    }, 0);
  }

  getEodSalesReport() {
    const saleObj = {
      locationId: this.locationId,
      fromDate: moment(this.selectDate).format('YYYY-MM-DD'),
      endDate: moment(this.selectDate).format('YYYY-MM-DD')
    };
    this.reportService.getEodSaleReport(saleObj).subscribe(res => {
      if (res.status === 'Success') {
        const saleReport = JSON.parse(res.resultData);
        this.salesReport = saleReport.GetEODSalesReport.EODSalesDetails;
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

}
