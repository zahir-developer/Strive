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
    this.selectDate = moment(new Date()).format('MM-DD-YYYY');
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
    this.totalCoin = this.totalPennie = this.totalQuater = this.totalNickel = this.totalDime = this.totalHalf = 0;
    this.totalRoll = this.totalPennieRoll = this.totalQuaterRoll = this.totalNickelRoll = this.totalDimeRoll = 0;
    this.totalBill = this.totalOnes = this.totalFives = this.totalTens = this.totalTwenties = this.totalFifties = this.totalHunderds = 0;
    this.totalCash = 0;
    this.getCloseOutRegister();
  }

  // Get CloseOutRegister By Date
  getCloseOutRegister() {
    const today = moment(new Date).format('YYYY-MM-DD');
    const cashRegisterType = "CLOSEOUT";
    const locationId = 1;
    this.registerService.getCashRegisterByDate(cashRegisterType, locationId, today).subscribe(data => {
      if (data.status === "Success") {
        const closeOut = JSON.parse(data.resultData);
        this.closeOutDetails = closeOut.CashRegister;
        if (this.closeOutDetails.CashRegister !== null) {
          this.isUpdate = true;
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
          this.getTotalCash();
          this.closeoutRegisterForm.patchValue({
            cardAmount: this.closeOutDetails.CashRegisterOthers.CreditCard1
          });
        }
      }
    });
  }

  // Add/Update CloseOutRegister
  submit() {
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
      createdBy: 1,
      createdDate: new Date(),
      updatedBy: 1,
      updatedDate: new Date(),
    }
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
      createdBy: 1,
      createdDate: new Date(),
      updatedBy: 1,
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
      createdBy: 1,
      createdDate: new Date(),
      updatedBy: 1,
      updatedDate: new Date(),
    }
    const other = {
      cashRegOtherId: this.isUpdate ? this.closeOutDetails.CashRegisterOthers.CashRegOtherId : 0,
      cashRegisterId: this.isUpdate ? this.closeOutDetails.CashRegister.CashRegisterId : 0,
      creditCard1: 0,
      creditCard2: 0,
      creditCard3: 0,
      checks: 0,
      payouts: 0,
      isActive: true,      
      isDeleted: false,
      createdBy: 1,
      createdDate: new Date(),
      updatedBy: 1,
      updatedDate: new Date(),
    }
    const cashregister = {
      cashRegisterId: this.isUpdate ? this.closeOutDetails.CashRegister.CashRegisterId : 0,
      cashRegisterType: 120,
      locationId: 1,
      drawerId: 1,
      cashRegisterDate: moment(new Date()).format('YYYY-MM-DD'),
      isActive: true,      
      isDeleted: false,
      createdBy: 1,
      createdDate: new Date(),
      updatedBy: 1,
      updatedDate: new Date(),
    };
    const formObj = {
      cashregister: cashregister,
      cashRegisterCoins: coin,
      cashRegisterBills: bill,
      cashRegisterRolls: roll,
      cashregisterOthers: other
    }
    this.registerService.saveCashRegister(formObj, "CLOSEOUT").subscribe(data => {
      if (data.status === "Success") {
        if(this.isUpdate){
          this.toastr.success('Record Updated Successfully!!', 'Success!');
        }else{
          this.toastr.success('Record Saved Successfully!!', 'Success!');
        }        
        this.getCloseOutRegister();
      } else {
        this.toastr.error('Weather Communication Error', 'Error!');
      }
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

  // Calculate TotalCash
  getTotalCash() {
    this.totalCash = this.totalCoin + this.totalBill + this.totalRoll;
  }
}
