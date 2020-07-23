import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import * as moment from 'moment';
import { CashRegisterService } from 'src/app/shared/services/data-service/cash-register.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-closeout-register',
  templateUrl: './closeout-register.component.html',
  styleUrls: ['./closeout-register.component.css']
})
export class CloseoutRegisterComponent implements OnInit {

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


  constructor(private fb: FormBuilder, private registerService: CashRegisterService, private toastr: ToastrService) { }

  ngOnInit() {
    this.selectDate = moment(new Date()).format('YYYY-MM-DD');
    this.formInitialize();
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
      cardAmount: ['',]
    });
    this.totalCoin = 0;
    this.totalRoll = 0;
    this.totalBill = 0;
    this.totalCash = 0;
    this.getCloseOutRegister();
  }

  getCloseOutRegister() {
    const today = moment(new Date).format('YYYY-MM-DD');
    const cashRegisterType = "CLOSEOUT";
    const locationId = 1;
    this.registerService.getCashRegisterByDate(cashRegisterType, locationId, today).subscribe(data => {
      if (data.status === "Success") {
        const closeOut = JSON.parse(data.resultData);
        this.closeOutDetails = closeOut.CashRegister;
        if (this.closeOutDetails.length != 0) {
          this.isUpdate = true;
          this.cashRegisterCoinForm.patchValue({
            coinPennies: this.closeOutDetails[0].CashRegisterCoin.Pennies,
            coinNickels: this.closeOutDetails[0].CashRegisterCoin.Nickels,
            coinDimes: this.closeOutDetails[0].CashRegisterCoin.Dimes,
            coinQuaters: this.closeOutDetails[0].CashRegisterCoin.Quarters,
            coinHalfDollars: this.closeOutDetails[0].CashRegisterCoin.HalfDollars,
          });
          this.totalPennie = this.closeOutDetails[0].CashRegisterCoin.Pennies / 100;
          this.totalNickel = (5 * this.closeOutDetails[0].CashRegisterCoin.Nickels) / 100;
          this.totalDime = (10 * this.closeOutDetails[0].CashRegisterCoin.Dimes) / 100;
          this.totalQuater = (25 * this.closeOutDetails[0].CashRegisterCoin.Quarters) / 100;
          this.totalHalf = (50 * this.closeOutDetails[0].CashRegisterCoin.HalfDollars) / 100;
          this.totalCoin = this.totalPennie + this.totalNickel + this.totalDime + this.totalQuater + this.totalHalf;
          this.cashRegisterBillForm.patchValue({
            billOnes: this.closeOutDetails[0].CashRegisterBill.Ones,
            billFives: this.closeOutDetails[0].CashRegisterBill.Fives,
            billTens: this.closeOutDetails[0].CashRegisterBill.Tens,
            billTwenties: this.closeOutDetails[0].CashRegisterBill.Twenties,
            billFifties: this.closeOutDetails[0].CashRegisterBill.Fifties,
            billHundreds: this.closeOutDetails[0].CashRegisterBill.Hundreds,
          });
          this.totalOnes = this.closeOutDetails[0].CashRegisterBill.Ones;
          this.totalFives = (5 * this.closeOutDetails[0].CashRegisterBill.Fives);
          this.totalTens = (10 * this.closeOutDetails[0].CashRegisterBill.Tens);
          this.totalTwenties = (20 * this.closeOutDetails[0].CashRegisterBill.Twenties);
          this.totalFifties = (50 * this.closeOutDetails[0].CashRegisterBill.Fifties);
          this.totalHunderds = (100 * this.closeOutDetails[0].CashRegisterBill.Hundreds);
          this.totalBill = this.totalOnes + this.totalFives + this.totalTens + this.totalTwenties + this.totalFifties + this.totalHunderds;
          this.cashRegisterRollForm.patchValue({
            pennieRolls: this.closeOutDetails[0].CashRegisterRoll.Pennies,
            nickelRolls: this.closeOutDetails[0].CashRegisterRoll.Nickels,
            dimeRolls: this.closeOutDetails[0].CashRegisterRoll.Dimes,
            quaterRolls: this.closeOutDetails[0].CashRegisterRoll.Quarters
          });
          this.totalPennieRoll = (50 * this.closeOutDetails[0].CashRegisterRoll.Pennies) / 100;
          this.totalNickelRoll = (40 * 5 * this.closeOutDetails[0].CashRegisterRoll.Nickels) / 100;
          this.totalDimeRoll = (50 * 10 * this.closeOutDetails[0].CashRegisterRoll.Dimes) / 100;
          this.totalQuaterRoll = (40 * 25 * this.closeOutDetails[0].CashRegisterRoll.Quarters) / 100;
          this.totalRoll = this.totalPennieRoll + this.totalNickelRoll + this.totalDimeRoll + this.totalQuaterRoll;
          this.getTotalCash();
          this.closeoutRegisterForm.patchValue({
            cardAmount: this.closeOutDetails[0].cashRegisterOther.CreditCard1
          });
        }
      }
    });
  }

  submit() {
    const coin = {
      cashRegCoinId: this.isUpdate ? this.closeOutDetails[0].CashRegisterCoin.CashRegCoinId : 0,
      pennies: this.cashRegisterCoinForm.value.coinPennies,
      nickels: this.cashRegisterCoinForm.value.coinNickels,
      dimes: this.cashRegisterCoinForm.value.coinDimes,
      quarters: this.cashRegisterCoinForm.value.coinQuaters,
      halfDollars: this.cashRegisterCoinForm.value.coinHalfDollars,
      dateEntered: moment(new Date()).format('YYYY-MM-DD')
    }
    const bill = {
      cashRegBillId: this.isUpdate ? this.closeOutDetails[0].CashRegisterBill.CashRegBillId : 0,
      ones: this.cashRegisterBillForm.value.billOnes,
      fives: this.cashRegisterBillForm.value.billFives,
      tens: this.cashRegisterBillForm.value.billTens,
      twenties: this.cashRegisterBillForm.value.billTwenties,
      fifties: this.cashRegisterBillForm.value.billFifties,
      hundreds: this.cashRegisterBillForm.value.billHundreds,
      dateEntered: moment(new Date()).format('YYYY-MM-DD')
    }
    const roll = {
      cashRegRollId: this.isUpdate ? this.closeOutDetails[0].CashRegisterRoll.CashRegRollId : 0,
      pennies: this.cashRegisterRollForm.value.pennieRolls,
      nickels: this.cashRegisterRollForm.value.nickelRolls,
      dimes: this.cashRegisterRollForm.value.dimeRolls,
      quarters: this.cashRegisterRollForm.value.quaterRolls,
      halfDollars: 0,
      dateEntered: moment(new Date()).format('YYYY-MM-DD')
    }
    const other = {
      cashRegOtherId: this.isUpdate ? this.closeOutDetails[0].CashRegisterOther.CashRegOtherId : 0,
      creditCard1: this.closeoutRegisterForm.value.cardAmount,
      creditCard2: 0,
      creditCard3: 0,
      checks: 0,
      payouts: 0,
      dateEntered: moment(new Date()).format('YYYY-MM-DD')
    }
    const formObj = {
      cashRegisterId: this.isUpdate ? this.closeOutDetails[0].CashRegisterId : 0,
      cashRegisterType: 120,
      locationId: 1,
      drawerId: 1,
      userId: 1,
      enteredDateTime: moment(new Date()).format('YYYY-MM-DD'),
      cashRegisterRollId: this.isUpdate ? this.closeOutDetails[0].CashRegisterRollId : 0,
      cashRegisterCoinId: this.isUpdate ? this.closeOutDetails[0].CashRegisterCoinId : 0,
      cashRegisterBillId: this.isUpdate ? this.closeOutDetails[0].CashRegisterBillId : 0,
      cashRegisterOtherId: this.isUpdate ? this.closeOutDetails[0].CashRegisterOtherId : 0,
      cashRegisterCoin: coin,
      CashRegisterBill: bill,
      CashRegisterRoll: roll,
      cashRegisterOther: other
    };
    this.registerService.saveCashRegister(formObj, "CLOSEOUT").subscribe(data => {
      if (data.status === "Success") {
        this.toastr.success('Record Saved Successfully!!', 'Success!');
        this.getCloseOutRegister();
      } else {
        this.toastr.error('Weather Communication Error', 'Error!');
      }
    });    
  }

  cancel() {
  }
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
  getTotalCash() {
    this.totalCash = this.totalCoin + this.totalBill + this.totalRoll;
  }
}
