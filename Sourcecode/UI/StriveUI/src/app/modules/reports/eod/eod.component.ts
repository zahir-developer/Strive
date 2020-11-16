import { Component, OnInit, ViewChild, ChangeDetectorRef, AfterViewInit } from '@angular/core';
import { BsDatepickerConfig, BsDaterangepickerDirective } from 'ngx-bootstrap/datepicker';
import { ReportsService } from 'src/app/shared/services/data-service/reports.service';
import { ExcelService } from 'src/app/shared/services/common-service/excel.service';
import * as moment from 'moment';

@Component({
  selector: 'app-eod',
  templateUrl: './eod.component.html',
  styleUrls: ['./eod.component.css']
})
export class EodComponent implements OnInit, AfterViewInit {
  @ViewChild('dp', { static: false }) datepicker: BsDaterangepickerDirective;
  bsConfig: Partial<BsDatepickerConfig>;
  date = new Date();
  maxDate = new Date();
  selectDate: any;
  cashRegisterCoins: any;
  cashRegisterBills: any;
  cashRegisterRolls: any;
  locationId: any;
  constructor(
    private cd: ChangeDetectorRef,
    private reportService: ReportsService,
    private excelService: ExcelService
    ) { }

  ngOnInit(): void {
    this.locationId = localStorage.getItem('empLocationId');
    this.selectDate = moment(new Date()).format('MM-DD-YYYY');
  }

  ngAfterViewInit() {
    this.bsConfig = Object.assign({}, { maxDate: this.maxDate, dateInputFormat: 'MM/DD/YYYY' });
    this.datepicker.setConfig();
    this.cd.detectChanges();
  }

  onValueChange(event) {
    if (event !== null) {
      this.selectDate = event;
      this.getCashRegister();
    }
  }

  getCashRegister() {
    const date = moment(this.selectDate).format('YYYY-MM-DD');
    const cashRegisterType = 'CASHIN';
    const locationId = +localStorage.getItem('empLocationId');
    this.reportService.getCashRegisterByDate(cashRegisterType, locationId, date).subscribe( res => {
      if (res.status === 'Success') {
        const cashIn = JSON.parse(res.resultData);
        console.log(cashIn, 'cashIn');
        if (cashIn.CashRegister.CashRegisterCoins !== null) {
          this.cashRegisterCoins = {
            Pennies: cashIn.CashRegister.CashRegisterCoins.Pennies / 100,
            Nickels: ( 5 * cashIn.CashRegister.CashRegisterCoins.Nickels) / 100,
            Dimes: (10 * cashIn.CashRegister.CashRegisterCoins.Dimes) / 100,
            Quarters: ( 25 * cashIn.CashRegister.CashRegisterCoins.Quarters) / 100,
            HalfDollars: ( 50 * cashIn.CashRegister.CashRegisterCoins.HalfDollars) / 100
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
            s5: ( 5 * cashIn.CashRegister.CashRegisterBills.s5),
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
            Nickels: cashIn.CashRegister.CashRegisterRolls.Nickels !== null ? ( 5 * cashIn.CashRegister.CashRegisterRolls.Nickels) / 100
            : 0,
            Dimes: cashIn.CashRegister.CashRegisterRolls.Dimes !== null ? (10 * cashIn.CashRegister.CashRegisterRolls.Dimes) / 100 : 0,
            Quarters: cashIn.CashRegister.CashRegisterRolls.Quarters !== null ? ( 25 * cashIn.CashRegister.CashRegisterRolls.Quarters) / 100
            : 0,
            HalfDollars: cashIn.CashRegister.CashRegisterRolls.HalfDollars !== null ?
             ( 50 * cashIn.CashRegister.CashRegisterRolls.HalfDollars) / 100 : 0
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
    });
  }

  onLocationChange(event) {
    this.locationId = +event;
  }

}
