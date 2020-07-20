import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import * as moment from 'moment';
import { CashRegisterService } from 'src/app/shared/services/data-service/cash-register.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-cash-register',
  templateUrl: './cash-register.component.html',
  styleUrls: ['./cash-register.component.css']
})
export class CashinRegisterComponent implements OnInit {

  cashRegisterForm: FormGroup;
  cashDetails: any;
  isUpdate: boolean;
  totalPennie:number = 0;
  totalNickel:number = 0;
  totalDime:number = 0;
  totalQuater:number = 0;
  totalHalf:number = 0;
  totalCoin:number = 0;
  totalOnes: number = 0;
  totalFives: number = 0;
  totalTens: number = 0;
  totalTwenties: number = 0;
  totalFifties: number = 0;
  totalHunderds: number = 0;
  totalBill : number = 0;
  selectDate: any;
  totalCash: number = 0;

  constructor(private fb: FormBuilder, private registerService: CashRegisterService, private toastr: ToastrService) { }

  ngOnInit() {
    this.selectDate = moment(new Date()).format('YYYY-MM-DD');
    this.formInitialize();
    this.getCashRegister();
  }

  formInitialize() {
    this.cashRegisterForm = this.fb.group({
      coinPennies: ['',],
      coinNickels: ['',],
      coinDimes: ['',],
      coinQuaters: ['',],
      coinHalfDollars: ['',],
      billOnes: ['',],
      billFives: ['',],
      billTens: ['',],
      billTwenties: ['',],
      billFifties: ['',],
      billHundreds: ['',],
      pennieRolls: ['',],
      nickelRolls: ['',],
      dimeRolls: ['',],
      quaterRolls: ['',],
      highTemperature: ['',],
      rainPercentage: ['',],
      goal: ['',]
    });
  }

  getCashRegister() {
    const today = moment(new Date).format('YYYY-MM-DD');
    const cashRegisterType = "CASHIN";
    const locationId = 1;
    this.registerService.getCashRegisterByDate(cashRegisterType, locationId, today).subscribe(data => {
      if (data.status === "Success") {
        const cashIn = JSON.parse(data.resultData);
        this.cashDetails = cashIn.CashRegister;
        console.log(this.cashDetails);
        if (this.cashDetails.length != 0) {
          this.isUpdate = true;
          this.cashRegisterForm.patchValue({
            coinPennies: this.cashDetails[0].CashRegisterCoin.Pennies,
            coinNickels: this.cashDetails[0].CashRegisterCoin.Nickels,
            coinDimes: this.cashDetails[0].CashRegisterCoin.Dimes,
            coinQuaters: this.cashDetails[0].CashRegisterCoin.Quaters,
            coinHalfDollars: this.cashDetails[0].CashRegisterCoin.HalfDollars,
            billOnes: this.cashDetails[0].CashRegisterBill.Ones,
            billFives: this.cashDetails[0].CashRegisterBill.Fives,
            billTens: this.cashDetails[0].CashRegisterBill.Tens,
            billTwenties: this.cashDetails[0].CashRegisterBill.Twenties,
            billFifties: this.cashDetails[0].CashRegisterBill.Fifties,
            billHundreds: this.cashDetails[0].CashRegisterBill.Hundreds,
            pennieRolls: this.cashDetails[0].CashRegisterRoll.Pennies,
            nickelRolls: this.cashDetails[0].CashRegisterRoll.Nickels,
            dimeRolls: this.cashDetails[0].CashRegisterRoll.Dimes,
            quaterRolls: this.cashDetails[0].CashRegisterRoll.Quaters
          });
        }
      }
    });
  }

  submit() {
    const sourceObj = [];
    const coin = [{
      cashRegCoinId: this.isUpdate ? this.cashDetails[0].CashRegisterCoinId : 0,
      pennies: this.cashRegisterForm.value.coinPennies,
      nickels: this.cashRegisterForm.value.coinNickels,
      dimes: this.cashRegisterForm.value.coinDimes,
      quaters: this.cashRegisterForm.value.coinQuaters,
      halfDollars: this.cashRegisterForm.value.coinHalfDollars,
      dateEntered: moment(new Date).format('YYYY-MM-DD')
    }]
    const bill = [{
      cashRegBillId: this.isUpdate ? this.cashDetails[0].CashRegisterBillId : 0,
      ones: this.cashRegisterForm.value.billOnes,
      fives: this.cashRegisterForm.value.billFives,
      tens: this.cashRegisterForm.value.billTens,
      twenties: this.cashRegisterForm.value.billTwenties,
      fifties: this.cashRegisterForm.value.billFifties,
      hundreds: this.cashRegisterForm.value.billHundreds,
      dateEntered: moment(new Date).format('YYYY-MM-DD')
    }]
    const roll = [{
      cashRegRollId: this.isUpdate ? this.cashDetails[0].CashRegisterRollId : 0,
      pennies: this.cashRegisterForm.value.pennieRolls,
      nickels: this.cashRegisterForm.value.nickelRolls,
      dimes: this.cashRegisterForm.value.dimeRolls,
      quaters: this.cashRegisterForm.value.quaterRolls,
      halfDollars: 0,
      dateEntered: moment(new Date).format('YYYY-MM-DD')
    }]
    const other = [{
      cashRegOthersId: this.isUpdate ? this.cashDetails[0].CashRegisterOthersId : 0,
      creditCard1: 0,
      creditCard2: 0,
      creditCard3: 0,
      checks: 0,
      payouts: 0,
      dateEntered: moment(new Date).format('YYYY-MM-DD')
    }]
    const formObj = {
      cashRegisterId: this.isUpdate ? this.cashDetails[0].CashRegisterId : 0,
      cashRegisterType: 119,
      locationId: 1,
      drawerId: 0,
      userId: 1,
      enteredDateTime: moment(new Date).format('YYYY-MM-DD'),
      cashRegRollId: this.isUpdate ? this.cashDetails[0].CashRegisterRollId : 0,
      cashRegCoinId: this.isUpdate ? this.cashDetails[0].CashRegisterCoinId : 0,
      cashRegBillId: this.isUpdate ? this.cashDetails[0].CashRegisterBillId : 0,
      cashRegOthersId: this.isUpdate ? this.cashDetails[0].CashRegisterOthersId : 0,
      cashRegisterCoin: coin,
      CashRegisterBill: bill,
      CashRegisterRoll: roll,
      cashRegisterOther: other
    };
    sourceObj.push(formObj);
    console.log(sourceObj);
    this.registerService.saveCashRegister(sourceObj).subscribe(data => {
      if (data.status === "Success") {
        this.toastr.success('Record Saved Successfully!!', 'Success!');
      }
    });
    this.getCashRegister();
  }

  cancel() {
    //this.getCashRegister();
  }
  getTotalCoin(name:string , amt:number){    
    if(name === 'P'){
      this.totalPennie = 0;
      this.totalPennie += amt;
      this.totalPennie /= 100;
    }else if(name === 'N'){
      this.totalNickel = 0;
      this.totalNickel += 5*amt;
      this.totalNickel /= 100;
    }else if(name === 'D'){
      this.totalDime = 0;
      this.totalDime += 10*amt;
      this.totalDime /= 100;
    }else if(name === 'Q'){
      this.totalQuater = 0;
      this.totalQuater += 25*amt;
      this.totalQuater /= 100;
    }else if(name === 'H'){
      this.totalHalf = 0;
      this.totalHalf += 50*amt;
      this.totalHalf /= 100;
    }
    this.totalCoin = this.totalPennie + this.totalNickel + this.totalDime + this.totalQuater + this.totalHalf;
    this.getTotalCash();
  }  
  getTotalBill(name:number , amt:number){    
    amt = Number(amt);
    if(name === 1){
      this.totalOnes = 0;
      this.totalOnes += amt;
    }else if(name === 5){
      this.totalFives = 0;
      this.totalFives += 5*amt;
    }else if(name === 10){
      this.totalTens = 0;
      this.totalTens += 10*amt;
    }else if(name === 20){
      this.totalTwenties = 0;
      this.totalTwenties += 20*amt;
    }else if(name === 50){
      this.totalFifties = 0;
      this.totalFifties += 50*amt;
    }else if(name === 100){
      this.totalHunderds = 0;
      this.totalHunderds += 100*amt;
    }
    this.totalBill = this.totalOnes + this.totalFives + this.totalTens + this.totalTwenties + this.totalFifties + this.totalHunderds;
    this.getTotalCash();
  } 
  getTotalCash(){
    this.totalCash = this.totalCoin + this.totalBill;
  }

}