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
    totalPennieRoll:number = 0;
    totalNickelRoll:number = 0;
    totalDimeRoll:number = 0;
    totalQuaterRoll:number = 0;
    totalRoll:number = 0;
    selectDate: any;
    totalCash: number = 0;

  constructor(private fb: FormBuilder, private registerService: CashRegisterService,private toastr: ToastrService) { }

  ngOnInit() {
    this.selectDate = moment(new Date()).format('YYYY-MM-DD');
    this.formInitialize();    
    //this.getCloseOutRegister();   
  }
  formInitialize() {
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
      highTemperature: ['',],
      rainPercentage: ['',],
      goal: ['',]
    });
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
      cashRegisterType: 120,
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
    this.getCloseOutRegister();
  }

  cancel(){
    //this.getCloseOutRegister();
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
  getTotalRoll(name:string , amt:number){    
    if(name === 'P'){
      this.totalPennieRoll = 0;
      this.totalPennieRoll += 50*amt;
      this.totalPennieRoll /= 100;
    }else if(name === 'N'){
      this.totalNickelRoll = 0;
      this.totalNickelRoll += (5*amt)*40;
      this.totalNickelRoll /= 100;
    }else if(name === 'D'){
      this.totalDimeRoll = 0;
      this.totalDimeRoll += (10*amt)*50;
      this.totalDimeRoll /= 100;
    }else if(name === 'Q'){
      this.totalQuaterRoll = 0;
      this.totalQuaterRoll += (25*amt)*40;
      this.totalQuaterRoll /= 100;
    }this.totalRoll = this.totalPennieRoll + this.totalNickelRoll + this.totalDimeRoll + this.totalQuaterRoll;
    this.getTotalCash();
  } 
  getTotalCash(){
    this.totalCash = this.totalCoin + this.totalBill + this.totalRoll;
  }
}