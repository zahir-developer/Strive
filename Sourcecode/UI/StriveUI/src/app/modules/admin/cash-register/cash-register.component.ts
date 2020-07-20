import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import * as moment from 'moment';
import { CashRegisterService } from 'src/app/shared/services/data-service/cash-register.service';
import { ToastrService } from 'ngx-toastr';
import { log } from 'console';
declare var $: any;

@Component({
  selector: 'app-cash-register',
  templateUrl: './cash-register.component.html',
  styleUrls: ['./cash-register.component.css']
})
export class CashinRegisterComponent implements OnInit {

  cashRegisterForm: FormGroup;
  cashDetails: any;
  isUpdate: boolean;
  totalPennie:number;
  totalNickel:number;
  totalDime:number;
  totalQuater:number;
  totalHalf:number;
  totalCoin:Number = 0;

  constructor(private fb: FormBuilder, private registerService: CashRegisterService, private toastr: ToastrService) { }

  ngOnInit() {
    // $(document).ready(function () {
    //   var date = new Date();
    //   var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    //   var end = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    //   $('#datepicker').datepicker({
    //     format: "dd-mm-yyyy",
    //     todayHighlight: true,
    //     startDate: today,
    //     autoclose: true
    //   });
    //   $('#datepicker').datepicker('setDate', today);
    // });

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
    //this.default();
    //this.getCashRegister();
  }

  default() {
    this.cashRegisterForm.patchValue({
      coinPennies: 0,
      coinNickels: 0,
      coinDimes: 0,
      coinQuaters: 0,
      coinHalfDollars: 0,
      billOnes: 0,
      billFives: 0,
      billTens: 0,
      billTwenties: 0,
      billFifties: 0,
      billHundreds: 0,
      pennieRolls: 0,
      nickelRolls: 0,
      dimeRolls: 0,
      quaterRolls: 0,
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
        else {
          this.default();
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

  getTotalPennie(amt:number){
    this.totalPennie = 0;
    this.totalPennie += amt;
    this.totalPennie /= 100;
    this.getTotalCoin();
  }
  getTotalNickel(amt:number){
    this.totalNickel = 0;
    this.totalNickel += 5*amt;
    this.totalNickel /= 100;
    this.getTotalCoin();
  }
  getTotalDime(amt:number){
    this.totalDime = 0;
    this.totalDime += 10*amt;
    this.totalDime /= 100;
    this.getTotalCoin();
  }
  getTotalQuater(amt:number){
    this.totalQuater = 0;
    this.totalQuater += 25*amt;
    this.totalQuater /= 100;
    this.getTotalCoin();
  }
  getTotalHalf(amt:number){
    this.totalHalf = 0;
    this.totalHalf += 50*amt;
    this.totalHalf /= 100;
    this.getTotalCoin();
  }
  getTotalCoin(){
    this.totalCoin = this.totalPennie + this.totalNickel + this.totalQuater + this.totalDime + this.totalHalf;
  }

}