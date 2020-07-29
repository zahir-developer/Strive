import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import * as moment from 'moment';
import { CashRegisterService } from 'src/app/shared/services/data-service/cash-register.service';
import { ToastrService } from 'ngx-toastr';
import { WeatherService } from 'src/app/shared/services/common-service/weather.service';

@Component({
  selector: 'app-cash-register',
  templateUrl: './cash-register.component.html',
  styleUrls: ['./cash-register.component.css']
})
export class CashinRegisterComponent implements OnInit {

  cashRegisterCoinForm: FormGroup;
  cashDetails: any;
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
  cashRegisterForm: FormGroup;
  weatherDetails: any;
  toggleTab: number;

  constructor(private fb: FormBuilder, private registerService: CashRegisterService, private toastr: ToastrService, private weatherService: WeatherService) { }

  ngOnInit() {
    this.selectDate = moment(new Date()).format('YYYY-MM-DD');
    this.formInitialize();
    this.getWeatherDetails();
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
    this.cashRegisterForm = this.fb.group({
      goal: ['',]
    });
    this.toggleTab = 0;
    this.totalCoin = this.totalPennie = this.totalQuater = this.totalNickel = this.totalDime = this.totalHalf = 0;
    this.totalRoll = this.totalPennieRoll = this.totalQuaterRoll = this.totalNickelRoll = this.totalDimeRoll = 0;
    this.totalBill = this.totalOnes = this.totalFives = this.totalTens = this.totalTwenties = this.totalFifties = this.totalHunderds = 0;
    this.totalCash = 0;
    this.getCashRegister();
  }

  getCashRegister() {
    const today = moment(new Date()).format('YYYY-MM-DD');
    const cashRegisterType = 'CASHIN';
    const locationId = 1;
    this.registerService.getCashRegisterByDate(cashRegisterType, locationId, today).subscribe(data => {
      if (data.status === 'Success') {
        const cashIn = JSON.parse(data.resultData);
        this.cashDetails = cashIn.CashRegister;
        if (this.cashDetails.length != 0) {
          this.isUpdate = true;
          this.cashRegisterCoinForm.patchValue({
            coinPennies: this.cashDetails[0].CashRegisterCoin.Pennies,
            coinNickels: this.cashDetails[0].CashRegisterCoin.Nickels,
            coinDimes: this.cashDetails[0].CashRegisterCoin.Dimes,
            coinQuaters: this.cashDetails[0].CashRegisterCoin.Quarters,
            coinHalfDollars: this.cashDetails[0].CashRegisterCoin.HalfDollars,
          });
          this.totalPennie = this.cashDetails[0].CashRegisterCoin.Pennies / 100;
          this.totalNickel = (5 * this.cashDetails[0].CashRegisterCoin.Nickels) / 100;
          this.totalDime = (10 * this.cashDetails[0].CashRegisterCoin.Dimes) / 100;
          this.totalQuater = (25 * this.cashDetails[0].CashRegisterCoin.Quarters) / 100;
          this.totalHalf = (50 * this.cashDetails[0].CashRegisterCoin.HalfDollars) / 100;
          this.totalCoin = this.totalPennie + this.totalNickel + this.totalDime + this.totalQuater + this.totalHalf;
          this.cashRegisterBillForm.patchValue({
            billOnes: this.cashDetails[0].CashRegisterBill.Ones,
            billFives: this.cashDetails[0].CashRegisterBill.Fives,
            billTens: this.cashDetails[0].CashRegisterBill.Tens,
            billTwenties: this.cashDetails[0].CashRegisterBill.Twenties,
            billFifties: this.cashDetails[0].CashRegisterBill.Fifties,
            billHundreds: this.cashDetails[0].CashRegisterBill.Hundreds,
          });
          this.totalOnes = this.cashDetails[0].CashRegisterBill.Ones;
          this.totalFives = (5 * this.cashDetails[0].CashRegisterBill.Fives);
          this.totalTens = (10 * this.cashDetails[0].CashRegisterBill.Tens);
          this.totalTwenties = (20 * this.cashDetails[0].CashRegisterBill.Twenties);
          this.totalFifties = (50 * this.cashDetails[0].CashRegisterBill.Fifties);
          this.totalHunderds = (100 * this.cashDetails[0].CashRegisterBill.Hundreds);
          this.totalBill = this.totalOnes + this.totalFives + this.totalTens + this.totalTwenties + this.totalFifties + this.totalHunderds;
          this.cashRegisterRollForm.patchValue({
            pennieRolls: this.cashDetails[0].CashRegisterRoll.Pennies,
            nickelRolls: this.cashDetails[0].CashRegisterRoll.Nickels,
            dimeRolls: this.cashDetails[0].CashRegisterRoll.Dimes,
            quaterRolls: this.cashDetails[0].CashRegisterRoll.Quarters
          });
          this.totalPennieRoll = (50 * this.cashDetails[0].CashRegisterRoll.Pennies) / 100;
          this.totalNickelRoll = (40 * 5 * this.cashDetails[0].CashRegisterRoll.Nickels) / 100;
          this.totalDimeRoll = (50 * 10 * this.cashDetails[0].CashRegisterRoll.Dimes) / 100;
          this.totalQuaterRoll = (40 * 25 * this.cashDetails[0].CashRegisterRoll.Quarters) / 100;
          this.totalRoll = this.totalPennieRoll + this.totalNickelRoll + this.totalDimeRoll + this.totalQuaterRoll;
          setTimeout(() => {
            this.cashRegisterForm.patchValue({
              goal: this.weatherDetails?.TargetBusiness
            });
          }, 1200);
          this.getTotalCash();
        }
      }
    });
  }

  getWeatherDetails = () => {
    this.weatherService.data.subscribe((data: any) => {
      if (data !== undefined) {
        this.weatherDetails = data;
      }
    });
  }

  submit() {
    const coin = {
      cashRegisterCoinId: this.isUpdate ? this.cashDetails[0].CashRegisterCoin.CashRegisterCoinId : 0,
      pennies: this.cashRegisterCoinForm.value.coinPennies,
      nickels: this.cashRegisterCoinForm.value.coinNickels,
      dimes: this.cashRegisterCoinForm.value.coinDimes,
      quarters: this.cashRegisterCoinForm.value.coinQuaters,
      halfDollars: this.cashRegisterCoinForm.value.coinHalfDollars,
      dateEntered: moment(new Date()).format('YYYY-MM-DD')
    }
    const bill = {
      cashRegisterBillId: this.isUpdate ? this.cashDetails[0].CashRegisterBill.CashRegisterBillId : 0,
      ones: this.cashRegisterBillForm.value.billOnes,
      fives: this.cashRegisterBillForm.value.billFives,
      tens: this.cashRegisterBillForm.value.billTens,
      twenties: this.cashRegisterBillForm.value.billTwenties,
      fifties: this.cashRegisterBillForm.value.billFifties,
      hundreds: this.cashRegisterBillForm.value.billHundreds,
      dateEntered: moment(new Date()).format('YYYY-MM-DD')
    }
    const roll = {
      cashRegisterRollId: this.isUpdate ? this.cashDetails[0].CashRegisterRoll.CashRegisterRollId : 0,
      pennies: this.cashRegisterRollForm.value.pennieRolls,
      nickels: this.cashRegisterRollForm.value.nickelRolls,
      dimes: this.cashRegisterRollForm.value.dimeRolls,
      quarters: this.cashRegisterRollForm.value.quaterRolls,
      halfDollars: 0,
      dateEntered: moment(new Date()).format('YYYY-MM-DD')
    }
    const other = {
      cashRegisterOtherId: this.isUpdate ? this.cashDetails[0].CashRegisterOther.CashRegisterOtherId : 0,
      creditCard1: 0,
      creditCard2: 0,
      creditCard3: 0,
      checks: 0,
      payouts: 0,
      dateEntered: moment(new Date()).format('YYYY-MM-DD')
    }
    const formObj = {
      cashRegisterId: this.isUpdate ? this.cashDetails[0].CashRegisterId : 0,
      cashRegisterType: 119,
      locationId: 1,
      drawerId: 1,
      userId: 1,
      enteredDateTime: moment(new Date()).format('YYYY-MM-DD'),
      cashRegisterRollId: this.isUpdate ? this.cashDetails[0].CashRegisterRollId : 0,
      cashRegisterCoinId: this.isUpdate ? this.cashDetails[0].CashRegisterCoinId : 0,
      cashRegisterBillId: this.isUpdate ? this.cashDetails[0].CashRegisterBillId : 0,
      cashRegisterOtherId: this.isUpdate ? this.cashDetails[0].CashRegisterOtherId : 0,
      cashRegisterCoin: coin,
      CashRegisterBill: bill,
      CashRegisterRoll: roll,
      cashRegisterOther: other
    };
    const weatherObj = {
      weatherId: 0,
      locationId: 1,
      weather: Math.floor(this.weatherDetails?.temporature).toString(),
      rainProbability: Math.floor(this.weatherDetails?.rainPercentage).toString(),
      predictedBusiness: '-',
      targetBusiness: this.cashRegisterForm.value.goal,
      createdDate: moment(new Date()).format('YYYY-MM-DD')
    };
    this.registerService.saveCashRegister(formObj, 'CASHIN').subscribe(data => {
      if (data.status === 'Success') {
        this.weatherService.UpdateWeather(weatherObj).subscribe(response => {
          if (response.status === 'Success') {
            this.toggleTab = 0;
            if(this.isUpdate){
              this.toastr.success('Record Updated Successfully!!', 'Success!');
            }else{
              this.toastr.success('Record Saved Successfully!!', 'Success!');
            }            
            this.weatherService.getWeather();
            this.getCashRegister();
          } else {
            this.toastr.error('Weather Communication Error', 'Error!');
          }
        });
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
    if (this.toggleTab === 0) {
      this.toggleTab = 1;
    } else {
      this.toggleTab = 0;
    }
    console.log(this.toggleTab);
  }

  cancel() {
    if (this.toggleTab === 0) {
      this.toggleTab = 1;
    } else {
      this.toggleTab = 0;
    }
    console.log(this.toggleTab);
  }

  next() {
    if (this.toggleTab === 0) {
      this.toggleTab = 1;
    } else {
      this.toggleTab = 0;
    }
    console.log(this.toggleTab);
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
    }
    this.totalRoll = this.totalPennieRoll + this.totalNickelRoll + this.totalDimeRoll + this.totalQuaterRoll;
    this.getTotalCash();
  }
  getTotalCash() {
    this.totalCash = this.totalCoin + this.totalBill + this.totalRoll;
  }
}