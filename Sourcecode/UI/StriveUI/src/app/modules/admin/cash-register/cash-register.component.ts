import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import * as moment from 'moment';
import { CashRegisterService } from 'src/app/shared/services/data-service/cash-register.service';

@Component({
  selector: 'app-cash-register',
  templateUrl: './cash-register.component.html',
  styleUrls: ['./cash-register.component.css']
})
export class CashRegisterComponent implements OnInit {

    cashRegisterForm : FormGroup;
    cashDetails: any;

  constructor(private fb: FormBuilder, private registerService: CashRegisterService) { }

  ngOnInit() {

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
    this.getCashRegister();
  }

  getCashRegister(){
    const today =moment(new Date).format('YYYY-MM-DD');
    this.registerService.getCashRegisterByDate(today).subscribe(data =>{
      if(data.status === "Success"){
        const closeOut = JSON.parse(data.resultData);
        this.cashDetails = closeOut.CashRegister;
        console.log(this.cashDetails.length);
        if(this.cashDetails.length != 0){
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

  submit(){

  }

  cancel(){
    
  }
  
}