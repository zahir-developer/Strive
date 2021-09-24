import { Component, OnInit, ViewChild, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import * as moment from 'moment';
import { CashRegisterService } from 'src/app/shared/services/data-service/cash-register.service';
import { ToastrService } from 'ngx-toastr';
import { BsDaterangepickerDirective, BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { GetCodeService } from 'src/app/shared/services/data-service/getcode.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { DatePipe } from '@angular/common';
import { CodeValueService } from 'src/app/shared/common-service/code-value.service';

@Component({
  selector: 'app-closeout-register',
  templateUrl: './closeout-register.component.html',
  styles: [`
  .ngx-timepicker {
    border-bottom: none !important;
  }
  `]
})
export class CloseoutRegisterComponent implements OnInit, AfterViewInit {
  @ViewChild('dp', { static: false }) datepicker: BsDaterangepickerDirective;
  bsConfig: Partial<BsDatepickerConfig>;
  maxDate = new Date();
  cashRegisterCoinForm: FormGroup;
  closeOutDetails: any;
  isUpdate: boolean;
  totalPennie: number;
  totalNickel: number;
  totalDime: number;
  totalQuater: number;
  totalHalf: number;
  totalCoin: number;
  totalOnes: number;
  totalFives: number;
  totalTens: number;
  totalTwenties: number;
  totalFifties: number;
  totalHunderds: number;
  totalBill: number;
  totalPennieRoll: number;
  totalNickelRoll: number;
  totalDimeRoll: number;
  totalQuaterRoll: number;
  totalRoll: number;
  selectDate: any;
  totalCash: number;
  cashRegisterBillForm: FormGroup;
  cashRegisterRollForm: FormGroup;
  closeoutRegisterForm: FormGroup;
  date = moment(new Date()).format('MM/DD/YYYY');
  CloseRegisterId: any;
  storeStatusList = [];
  storeTimeIn = '';
  storeStatus = '';
  storeTimeOut = '';
  drawerId: any;
  submitted = false;
  isTimechange: boolean;
  tips: any;
  TipsDto: any;
  washTips: any;
  detailTips: any;
  detailerTip: any;
  today: Date;
  locationId: number;
  cashTipsEnable: boolean;
  constructor(
    private fb: FormBuilder, private registerService: CashRegisterService, private getCode: GetCodeService, private toastr: ToastrService,
    private cd: ChangeDetectorRef, private spinner: NgxSpinnerService, private datePipe: DatePipe,
    private codeValueService: CodeValueService) { }

  ngOnInit() {
    this.isTimechange = false;
    this.getDocumentType();
    this.getStoreStatusList();
    this.selectDate = moment(new Date()).format('MM/DD/YYYY');
    this.drawerId = localStorage.getItem('drawerId');
    this.formInitialize();
  }
  ngAfterViewInit() {
    this.bsConfig = Object.assign({}, { maxDate: this.maxDate, dateInputFormat: 'MM/DD/YYYY', showWeekNumbers: false });
    this.datepicker.setConfig();
    this.cd.detectChanges();
  }
  formInitialize() {
    this.cashRegisterCoinForm = this.fb.group({
      coinPennies: ['',],
      coinNickels: ['',],
      coinDimes: ['',],
      coinQuaters: ['',],
      coinHalfDollars: ['',]
    });
    this.cashRegisterBillForm = this.fb.group({
      billOnes: ['',],
      billFives: ['',],
      billTens: ['',],
      billTwenties: ['',],
      billFifties: ['',],
      billHundreds: ['',],
    });
    this.cashRegisterRollForm = this.fb.group({
      pennieRolls: ['',],
      nickelRolls: ['',],
      dimeRolls: ['',],
      quaterRolls: ['',],
    });
    this.closeoutRegisterForm = this.fb.group({
      cardAmount: ['',],

    });
    this.totalCoin = this.totalPennie = this.totalQuater = this.totalNickel = this.totalDime = this.totalHalf = 0;
    this.totalRoll = this.totalPennieRoll = this.totalQuaterRoll = this.totalNickelRoll = this.totalDimeRoll = 0;
    this.totalBill = this.totalOnes = this.totalFives = this.totalTens = this.totalTwenties = this.totalFifties = this.totalHunderds = 0;
    this.totalCash = 0;
    // this.getCloseOutRegister();
  }

  // Get CloseOutRegister By Date
  getCloseOutRegister() {
    const today = moment(this.selectDate).format('YYYY-MM-DD');
    const cashRegisterType = "CLOSEOUT";
    const locationId = +localStorage.getItem('empLocationId');
    this.closeoutRegisterForm.reset();
    this.cashRegisterCoinForm.reset();
    this.cashRegisterBillForm.reset();
    this.cashRegisterRollForm.reset();
    this.totalCoin = 0;
    this.totalBill = 0;
    this.totalRoll = 0;
    this.totalCash = 0;
    this.storeTimeOut = '';
    this.storeTimeIn = '';
    this.storeStatus = '';
    this.spinner.show();
    this.registerService.getCashRegisterByDate(cashRegisterType, locationId, today).subscribe(data => {
      if (data.status === "Success") {
        this.spinner.hide();

        const closeOut = JSON.parse(data.resultData);
        this.closeOutDetails = closeOut.CashRegister;
        this.getTotalCash();
        this.closeoutRegisterForm.patchValue({
          cardAmount: this.closeOutDetails.CardAmount.CardAmount ?
            this.closeOutDetails.CardAmount.CardAmount : '',
        });
        if (this.closeOutDetails.CashRegister !== null) {
          this.isUpdate = true;
          this.tips = this.closeOutDetails.CashRegister.Tips
          // this.storeStatus = this.closeOutDetails.CashRegister.StoreOpenCloseStatus !== null ?
          //   this.closeOutDetails.CashRegister.StoreOpenCloseStatus : '';
          // this.storeTimeIn = this.closeOutDetails.CashRegister.StoreTimeIn !== null ?
          //   moment(this.closeOutDetails.CashRegister.StoreTimeIn).format('HH:mm') : '';
          // this.storeTimeOut = this.closeOutDetails.CashRegister.StoreTimeOut !== null ?
          //   moment(this.closeOutDetails.CashRegister.StoreTimeOut).format('HH:mm') : '';
          this.cashRegisterCoinForm.patchValue({
            coinPennies: this.closeOutDetails.CashRegisterCoins.Pennies,
            coinNickels: this.closeOutDetails.CashRegisterCoins.Nickels,
            coinDimes: this.closeOutDetails.CashRegisterCoins.Dimes,
            coinQuaters: this.closeOutDetails.CashRegisterCoins.Quarters,
            coinHalfDollars: this.closeOutDetails.CashRegisterCoins.HalfDollars,
          });
          this.totalPennie = this.closeOutDetails.CashRegisterCoins.Pennies / 100;
          this.totalNickel = (5 * this.closeOutDetails.CashRegisterCoins.Nickels) / 100;
          this.totalDime = (10 * this.closeOutDetails.CashRegisterCoins.Dimes) / 100;
          this.totalQuater = (25 * this.closeOutDetails.CashRegisterCoins.Quarters) / 100;
          this.totalHalf = (50 * this.closeOutDetails.CashRegisterCoins.HalfDollars) / 100;
          this.totalCoin = this.totalPennie + this.totalNickel + this.totalDime + this.totalQuater + this.totalHalf;
          this.cashRegisterBillForm.patchValue({
            billOnes: this.closeOutDetails.CashRegisterBills.s1,
            billFives: this.closeOutDetails.CashRegisterBills.s5,
            billTens: this.closeOutDetails.CashRegisterBills.s10,
            billTwenties: this.closeOutDetails.CashRegisterBills.s20,
            billFifties: this.closeOutDetails.CashRegisterBills.s50,
            billHundreds: this.closeOutDetails.CashRegisterBills.s100,
          });
          this.totalOnes = this.closeOutDetails.CashRegisterBills.s1;
          this.totalFives = (5 * this.closeOutDetails.CashRegisterBills.s5);
          this.totalTens = (10 * this.closeOutDetails.CashRegisterBills.s10);
          this.totalTwenties = (20 * this.closeOutDetails.CashRegisterBills.s20);
          this.totalFifties = (50 * this.closeOutDetails.CashRegisterBills.s50);
          this.totalHunderds = (100 * this.closeOutDetails.CashRegisterBills.s100);
          this.totalBill = this.totalOnes + this.totalFives + this.totalTens + this.totalTwenties + this.totalFifties + this.totalHunderds;
          this.cashRegisterRollForm.patchValue({
            pennieRolls: this.closeOutDetails.CashRegisterRolls.Pennies,
            nickelRolls: this.closeOutDetails.CashRegisterRolls.Nickels,
            dimeRolls: this.closeOutDetails.CashRegisterRolls.Dimes,
            quaterRolls: this.closeOutDetails.CashRegisterRolls.Quarters
          });
          this.totalPennieRoll = (50 * this.closeOutDetails.CashRegisterRolls.Pennies) / 100;
          this.totalNickelRoll = (40 * 5 * this.closeOutDetails.CashRegisterRolls.Nickels) / 100;
          this.totalDimeRoll = (50 * 10 * this.closeOutDetails.CashRegisterRolls.Dimes) / 100;
          this.totalQuaterRoll = (40 * 25 * this.closeOutDetails.CashRegisterRolls.Quarters) / 100;
          this.totalRoll = this.totalPennieRoll + this.totalNickelRoll + this.totalDimeRoll + this.totalQuaterRoll;
         
          
         
          this.closeoutRegisterForm.controls.cardAmount.disable();
        } else if (this.closeOutDetails.CashRegister === null || this.closeOutDetails.CashRegisterCoins === null
          || this.closeOutDetails.CashRegisterRolls === null || this.closeOutDetails.CashRegisterBills === null) {
          this.cashRegisterCoinForm.enable();
          this.cashRegisterBillForm.enable();
          this.cashRegisterRollForm.enable();
          this.closeoutRegisterForm.enable();
          this.cashTipsEnable = true;

          this.isUpdate = false;
        } else {
          this.isUpdate = false;
          this.closeoutRegisterForm.reset();
          this.cashRegisterCoinForm.reset();
          this.cashRegisterBillForm.reset();
          this.cashRegisterRollForm.reset();
          this.tips = ''
        }
      }
      else {
        this.spinner.hide();

      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      this.spinner.hide();
    });
  }
  getDocumentType() {
    const closeRegisterVaue = this.codeValueService.getCodeValueByType(ApplicationConfig.Category.cashRegister);
    console.log(closeRegisterVaue, 'cached Value');
    if (closeRegisterVaue.length > 0) {
      this.CloseRegisterId = closeRegisterVaue.filter(i => i.CodeValue === ApplicationConfig.CodeValue.CloseOut)[0].CodeId;
    } else {
      this.getCode.getCodeByCategory(ApplicationConfig.Category.cashRegister).subscribe(data => {
        if (data.status === "Success") {
          const dType = JSON.parse(data.resultData);
          this.CloseRegisterId = dType.Codes.filter(i => i.CodeValue === ApplicationConfig.CodeValue.CloseOut)[0].CodeId;
        } else {
          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        }
      }, (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
      );
    }
  }

  // Add/Update CloseOutRegister
  submit() {
    this.submitted = true;
    // if (this.storeStatus === '' || this.storeTimeOut === '') {
    //   return;
    // }
    const coin = {
      cashRegCoinId: this.isUpdate ? this.closeOutDetails.CashRegisterCoins.CashRegCoinId : 0,
      cashRegisterId: this.isUpdate ? this.closeOutDetails.CashRegister.CashRegisterId : 0,
      pennies: this.cashRegisterCoinForm.value.coinPennies,
      nickels: this.cashRegisterCoinForm.value.coinNickels,
      dimes: this.cashRegisterCoinForm.value.coinDimes,
      quarters: this.cashRegisterCoinForm.value.coinQuaters,
      halfDollars: this.cashRegisterCoinForm.value.coinHalfDollars,
      isActive: true,
      isDeleted: false,
      createdBy: +localStorage.getItem('empId'),
      createdDate: new Date(),
      updatedBy: +localStorage.getItem('empId'),
      updatedDate: new Date(),
    };
    const bill = {
      cashRegBillId: this.isUpdate ? this.closeOutDetails.CashRegisterBills.CashRegBillId : 0,
      cashRegisterId: this.isUpdate ? this.closeOutDetails.CashRegister.CashRegisterId : 0,
      s1: this.cashRegisterBillForm.value.billOnes,
      s5: this.cashRegisterBillForm.value.billFives,
      s10: this.cashRegisterBillForm.value.billTens,
      s20: this.cashRegisterBillForm.value.billTwenties,
      s50: this.cashRegisterBillForm.value.billFifties,
      s100: this.cashRegisterBillForm.value.billHundreds,
      isActive: true,
      isDeleted: false,
      createdBy: +localStorage.getItem('empId'),
      createdDate: new Date(),
      updatedBy: +localStorage.getItem('empId'),
      updatedDate: new Date(),
    }
    const roll = {
      cashRegRollId: this.isUpdate ? this.closeOutDetails.CashRegisterRolls.CashRegRollId : 0,
      cashRegisterId: this.isUpdate ? this.closeOutDetails.CashRegister.CashRegisterId : 0,
      pennies: this.cashRegisterRollForm.value.pennieRolls,
      nickels: this.cashRegisterRollForm.value.nickelRolls,
      dimes: this.cashRegisterRollForm.value.dimeRolls,
      quarters: this.cashRegisterRollForm.value.quaterRolls,
      halfDollars: 0,
      isActive: true,
      isDeleted: false,
      createdBy: +localStorage.getItem('empId'),
      createdDate: new Date(),
      updatedBy: +localStorage.getItem('empId'),
      updatedDate: new Date(),
    }
    const other = {
      cashRegOtherId: this.isUpdate ? this.closeOutDetails.CashRegisterOthers.CashRegOtherId : 0,
      cashRegisterId: this.isUpdate ? this.closeOutDetails.CashRegister.CashRegisterId : 0,
      creditCard1: this.closeoutRegisterForm.value.cardAmount,
      creditCard2: 0,
      creditCard3: 0,
      checks: 0,
      payouts: 0,
      isActive: true,
      isDeleted: false,
      createdBy: +localStorage.getItem('empId'),
      createdDate: new Date(),
      updatedBy: +localStorage.getItem('empId'),
      updatedDate: new Date(),
    }

    const cashregister = {
      cashRegisterId: this.isUpdate ? this.closeOutDetails.CashRegister.CashRegisterId : 0,
      cashRegisterType: this.CloseRegisterId,
      locationId: +localStorage.getItem('empLocationId'),
      drawerId: +this.drawerId,
      cashRegisterDate: moment(new Date()).format('YYYY-MM-DD'),
      isActive: true,
      isDeleted: false,
      createdBy: +localStorage.getItem('empId'),
      createdDate: new Date(),
      updatedBy: +localStorage.getItem('empId'),
      updatedDate: new Date(),
      storeTimeIn: null,
      Tips: this.tips,
      totalAmount: this.totalCash,
      storeTimeOut: null, // checkoutTime,
      storeOpenCloseStatus: null // this.storeStatus === '' ? null : +this.storeStatus
    };
    const formObj = {
      cashregister,
      cashRegisterCoins: coin,
      cashRegisterBills: bill,
      cashRegisterRolls: roll,
      cashregisterOthers: other,
    }
    this.spinner.show();
    this.registerService.saveCashRegister(formObj, "CLOSEOUT").subscribe(data => {
      this.submitted = false;
      if (data.status === "Success") {
        this.toastr.success(MessageConfig.Admin.CloseRegister.Update, 'Success!');
        this.spinner.hide();
        this.getCloseOutRegister();
      } else {
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');

      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      this.submitted = false;
      this.spinner.hide();
    });
  }

  cancel() {
  }

  // Calculate TotalCoins
  getTotalCoin(name: string, amt: number) {
    if (name === 'P') {
      this.totalPennie = 0;
      this.totalPennie += amt;
      this.totalPennie /= 100;
    } else if (name === 'N') {
      this.totalNickel = 0;
      this.totalNickel += 5 * amt;
      this.totalNickel /= 100;
    } else if (name === 'D') {
      this.totalDime = 0;
      this.totalDime += 10 * amt;
      this.totalDime /= 100;
    } else if (name === 'Q') {
      this.totalQuater = 0;
      this.totalQuater += 25 * amt;
      this.totalQuater /= 100;
    } else if (name === 'H') {
      this.totalHalf = 0;
      this.totalHalf += 50 * amt;
      this.totalHalf /= 100;
    }
    this.totalCoin = this.totalPennie + this.totalNickel + this.totalDime + this.totalQuater + this.totalHalf;
    this.getTotalCash();
  }

  // Calculate TotalBills
  getTotalBill(name: number, amt: number) {
    amt = Number(amt);
    if (name === 1) {
      this.totalOnes = 0;
      this.totalOnes += amt;
    } else if (name === 5) {
      this.totalFives = 0;
      this.totalFives += 5 * amt;
    } else if (name === 10) {
      this.totalTens = 0;
      this.totalTens += 10 * amt;
    } else if (name === 20) {
      this.totalTwenties = 0;
      this.totalTwenties += 20 * amt;
    } else if (name === 50) {
      this.totalFifties = 0;
      this.totalFifties += 50 * amt;
    } else if (name === 100) {
      this.totalHunderds = 0;
      this.totalHunderds += 100 * amt;
    }
    this.totalBill = this.totalOnes + this.totalFives + this.totalTens + this.totalTwenties + this.totalFifties + this.totalHunderds;
    this.getTotalCash();
  }

  // Calculate TotalRolls
  getTotalRoll(name: string, amt: number) {
    if (name === 'P') {
      this.totalPennieRoll = 0;
      this.totalPennieRoll += 50 * amt;
      this.totalPennieRoll /= 100;
    } else if (name === 'N') {
      this.totalNickelRoll = 0;
      this.totalNickelRoll += (5 * amt) * 40;
      this.totalNickelRoll /= 100;
    } else if (name === 'D') {
      this.totalDimeRoll = 0;
      this.totalDimeRoll += (10 * amt) * 50;
      this.totalDimeRoll /= 100;
    } else if (name === 'Q') {
      this.totalQuaterRoll = 0;
      this.totalQuaterRoll += (25 * amt) * 40;
      this.totalQuaterRoll /= 100;
    } this.totalRoll = this.totalPennieRoll + this.totalNickelRoll + this.totalDimeRoll + this.totalQuaterRoll;
    this.getTotalCash();
  }

  tipsEvent(event) {
    this.tips = +event;
    this.getTotalCash();

  }
  // Calculate TotalCash
  getTotalCash() {

    if (this.tips === undefined || this.tips === null) {
      this.tips = 0;
    }

    this.totalCash = this.tips + this.totalCoin + this.totalBill + this.totalRoll;
  }
  onValueChange(event) {
    let selectedDate = event;
    let today;
    if (selectedDate !== null) {
      selectedDate = moment(event.toISOString()).format('YYYY-MM-DD');
      this.selectDate = selectedDate;
      today = moment(new Date().toISOString()).format('YYYY-MM-DD');
      if (moment(today).isSame(selectedDate)) {
        this.cashTipsEnable = true;
        this.cashRegisterCoinForm.enable();
        this.cashRegisterBillForm.enable();
        this.cashRegisterRollForm.enable();
        this.closeoutRegisterForm.enable();
      } else if (moment(today).isAfter(selectedDate)) {
        this.cashRegisterCoinForm.disable();
        this.cashRegisterBillForm.disable();
        this.cashRegisterRollForm.disable();
        this.cashTipsEnable = false;
        this.closeoutRegisterForm.disable();
      } else {
        this.cashRegisterCoinForm.enable();
        this.cashRegisterBillForm.enable();
        this.cashRegisterRollForm.enable();
        this.closeoutRegisterForm.enable();
        this.cashTipsEnable = true;
      }
    }
    this.getCloseOutRegister();
    this.getTips(this.selectDate);

  }
  getTips(selectDate) {
    const tipdetailDto = {
      locationId: +localStorage.getItem('empLocationId'),
      date: selectDate ? selectDate : new Date()
    }
    this.registerService.getTips(tipdetailDto).subscribe(data => {
      if (data.status === 'Success') {
        const dType = JSON.parse(data.resultData);
        this.TipsDto = dType.CashRegister;
        this.washTips = this.TipsDto.washTip.WashTips;
        this.detailTips = this.TipsDto.washTip.DetailTips;
        this.detailerTip = this.TipsDto.detailerTips;
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    }

    );
  }
  getStoreStatusList() {
    const storeStatusVaue = this.codeValueService.getCodeValueByType(ApplicationConfig.Category.cashRegister);
    console.log(storeStatusVaue, 'cached value ');
    if (storeStatusVaue.length > 0) {
      this.storeStatusList = storeStatusVaue;
      this.storeStatusList = this.storeStatusList.filter(item => item.CodeValue !== ApplicationConfig.storestatus.open);
    } else {
      this.getCode.getCodeByCategory(ApplicationConfig.Category.storeStatus).subscribe(data => {
        if (data.status === 'Success') {
          const dType = JSON.parse(data.resultData);
          this.storeStatusList = dType.Codes;
          this.storeStatusList = this.storeStatusList.filter(item => item.CodeValue !== ApplicationConfig.storestatus.open);
        } else {
          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        }
      }, (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
    }
  }

}
