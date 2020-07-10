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

    closeoutRegisterForm : FormGroup;
    closeOutDetails: any;

  constructor(private fb: FormBuilder, private registerService: CashRegisterService,private toastr: ToastrService) { }

  ngOnInit() {

    this.closeoutRegisterForm = this.fb.group({
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
        cardAmount: ['',]
    });
    this.closeoutRegisterForm.patchValue({
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
    this.getCloseOutRegister();
    
  }

  getCloseOutRegister(){
    const today =moment(new Date).format('YYYY-MM-DD');
    const cashRegisterType = "CASHOUT";
    const locationId = 1;
    this.registerService.getCashRegisterByDate(cashRegisterType,locationId,today).subscribe(data =>{
      if(data.status === "Success"){
        const closeOut = JSON.parse(data.resultData);
        this.closeOutDetails = closeOut.CashRegister;
        console.log(this.closeOutDetails.length);
        if(this.closeOutDetails.length != 0){
          this.closeoutRegisterForm.patchValue({
            coinPennies: this.closeOutDetails[0].CashRegisterCoin.Pennies,
            coinNickels: this.closeOutDetails[0].CashRegisterCoin.Nickels,
            coinDimes: this.closeOutDetails[0].CashRegisterCoin.Dimes,
            coinQuaters: this.closeOutDetails[0].CashRegisterCoin.Quaters,
            coinHalfDollars: this.closeOutDetails[0].CashRegisterCoin.HalfDollars,
            billOnes: this.closeOutDetails[0].CashRegisterBill.Ones,
            billFives: this.closeOutDetails[0].CashRegisterBill.Fives,
            billTens: this.closeOutDetails[0].CashRegisterBill.Tens,
            billTwenties: this.closeOutDetails[0].CashRegisterBill.Twenties,
            billFifties: this.closeOutDetails[0].CashRegisterBill.Fifties,
            billHundreds: this.closeOutDetails[0].CashRegisterBill.Hundreds,
            pennieRolls: this.closeOutDetails[0].CashRegisterRoll.Pennies,
            nickelRolls: this.closeOutDetails[0].CashRegisterRoll.Nickels,
            dimeRolls: this.closeOutDetails[0].CashRegisterRoll.Dimes,
            quaterRolls: this.closeOutDetails[0].CashRegisterRoll.Quaters
          });
        }
      }
    });
  }

  submit(){
    const sourceObj = [];
    const coin = [{
      cashRegCoinId : 0,
      pennies: this.closeoutRegisterForm.value.coinPennies,
      nickels: this.closeoutRegisterForm.value.coinNickels,
      dimes: this.closeoutRegisterForm.value.coinDimes,
      quaters: this.closeoutRegisterForm.value.coinQuaters,
      halfDollars: this.closeoutRegisterForm.value.coinHalfDollars,
      dateEntered: new Date()
    }] 
    const bill = [{
      cashRegBillId: 0,
      ones: this.closeoutRegisterForm.value.billOnes,
      fives: this.closeoutRegisterForm.value.billFives,
      tens: this.closeoutRegisterForm.value.billTens,
      twenties: this.closeoutRegisterForm.value.billTwenties,
      fifties: this.closeoutRegisterForm.value.billFifties,
      hundreds: this.closeoutRegisterForm.value.billHundreds,
      dateEntered: new Date()
    }]
    const roll =[{
      cashRegRollId : 0,
      pennies: this.closeoutRegisterForm.value.pennieRolls,
      nickels: this.closeoutRegisterForm.value.nickelRolls,
      dimes: this.closeoutRegisterForm.value.dimeRolls,
      quaters: this.closeoutRegisterForm.value.quaterRolls,
      halfDollars: 0,
      dateEntered: new Date()
    }]
    const other = [{
      cashRegOthersId : 0,
      creditCard1: 0,
      creditCard2: 0,
      creditCard3: 0,
      checks : 0,
      payouts: 0,
      dateEntered : new Date()
    }]
    const formObj = {
      cashRegisterId: 0,
      cashRegisterType: 2,
      locationId: 1,
      drawerId: 0,
      userId: 1,
      enteredDateTime: new Date() ,
      cashRegRollId: 0,
      cashRegCoinId: 0,
      cashRegBillId: 0,
      cashRegOthersId: 0,
      cashRegisterCoin: coin,
      CashRegisterBill: bill,
      CashRegisterRoll: roll,
      cashRegisterOther: other
    };
    sourceObj.push(formObj);
    console.log(sourceObj);
    this.registerService.saveCashRegister(sourceObj).subscribe(data =>{
      if(data.status === "Success")
      {
        this.toastr.success('Record Saved Successfully!!', 'Success!');
      }
    });
  }

  cancel(){
    
  }
  
}