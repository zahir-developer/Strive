import {
  Component, OnInit, ViewChild, AfterViewInit, ChangeDetectionStrategy,
  ChangeDetectorRef
} from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import * as moment from 'moment';
import { CashRegisterService } from 'src/app/shared/services/data-service/cash-register.service';
import { ToastrService } from 'ngx-toastr';
import { WeatherService } from 'src/app/shared/services/common-service/weather.service';
import { BsDaterangepickerDirective, BsDatepickerConfig } from 'ngx-bootstrap/datepicker';

@Component({
  selector: 'app-cash-register',
  templateUrl: './cash-register.component.html',
  styleUrls: ['./cash-register.component.css']
})
export class CashinRegisterComponent implements OnInit, AfterViewInit {
  @ViewChild('dp', { static: false }) datepicker: BsDaterangepickerDirective;
  bsConfig: Partial<BsDatepickerConfig>;
  maxDate = new Date();
  locationId: any;
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
  targetBusiness: any;
  drawerId: any;
  date = moment(new Date()).format('MM-DD-YYYY');
  constructor(private fb: FormBuilder, private registerService: CashRegisterService,
    private toastr: ToastrService, private weatherService: WeatherService, private cd: ChangeDetectorRef) { }

  ngOnInit() {
    this.selectDate = moment(new Date()).format('MM-DD-YYYY');
    this.locationId = localStorage.getItem('empLocationId');
    this.drawerId = localStorage.getItem('drawerId');
    this.formInitialize();
    this.getTargetBusinessData();
    this.getWeatherDetails();
  }
  ngAfterViewInit() {
    this.bsConfig = Object.assign({}, { maxDate: this.maxDate, dateInputFormat: 'MM-DD-YYYY' });
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

  // Get targetBusinessData
  getTargetBusinessData() {
    const locationId = +this.locationId;
    const date = moment(new Date()).format('YYYY-MM-DD');
    this.weatherService.getTargetBusinessData(locationId, date).subscribe(data => {
      if (data && data.resultData) {
        this.targetBusiness = JSON.parse(data.resultData);
      }
    });
  }

  // Get CashInRegister By Date
  getCashRegister() {
    const today = moment(new Date()).format('MM-DD-YYYY');
    const date = this.selectDate;
    const cashRegisterType = 'CASHIN';
    const locationId = +localStorage.getItem('empLocationId');
    this.registerService.getCashRegisterByDate(cashRegisterType, locationId, date).subscribe(data => {
      if (data.status === 'Success') {
        const cashIn = JSON.parse(data.resultData);
        this.cashDetails = cashIn.CashRegister;
        if (this.cashDetails.CashRegister !== null) {
          this.isUpdate = true;
          this.cashRegisterCoinForm.patchValue({
            coinPennies: this.cashDetails.CashRegisterCoins.Pennies,
            coinNickels: this.cashDetails.CashRegisterCoins.Nickels,
            coinDimes: this.cashDetails.CashRegisterCoins.Dimes,
            coinQuaters: this.cashDetails.CashRegisterCoins.Quarters,
            coinHalfDollars: this.cashDetails.CashRegisterCoins.HalfDollars,
          });
          this.totalPennie = this.cashDetails.CashRegisterCoins.Pennies / 100;
          this.totalNickel = (5 * this.cashDetails.CashRegisterCoins.Nickels) / 100;
          this.totalDime = (10 * this.cashDetails.CashRegisterCoins.Dimes) / 100;
          this.totalQuater = (25 * this.cashDetails.CashRegisterCoins.Quarters) / 100;
          this.totalHalf = (50 * this.cashDetails.CashRegisterCoins.HalfDollars) / 100;
          this.totalCoin = this.totalPennie + this.totalNickel + this.totalDime + this.totalQuater + this.totalHalf;
          this.cashRegisterBillForm.patchValue({
            billOnes: this.cashDetails.CashRegisterBills.s1,
            billFives: this.cashDetails.CashRegisterBills.s5,
            billTens: this.cashDetails.CashRegisterBills.s10,
            billTwenties: this.cashDetails.CashRegisterBills.s20,
            billFifties: this.cashDetails.CashRegisterBills.s50,
            billHundreds: this.cashDetails.CashRegisterBills.s100,
          });
          this.totalOnes = this.cashDetails.CashRegisterBills.s1;
          this.totalFives = (5 * this.cashDetails.CashRegisterBills.s5);
          this.totalTens = (10 * this.cashDetails.CashRegisterBills.s10);
          this.totalTwenties = (20 * this.cashDetails.CashRegisterBills.s20);
          this.totalFifties = (50 * this.cashDetails.CashRegisterBills.s50);
          this.totalHunderds = (100 * this.cashDetails.CashRegisterBills.s100);
          this.totalBill = this.totalOnes + this.totalFives + this.totalTens + this.totalTwenties + this.totalFifties + this.totalHunderds;
          this.cashRegisterRollForm.patchValue({
            pennieRolls: this.cashDetails.CashRegisterRolls.Pennies,
            nickelRolls: this.cashDetails.CashRegisterRolls.Nickels,
            dimeRolls: this.cashDetails.CashRegisterRolls.Dimes,
            quaterRolls: this.cashDetails.CashRegisterRolls.Quarters
          });
          this.totalPennieRoll = (50 * this.cashDetails.CashRegisterRolls.Pennies) / 100;
          this.totalNickelRoll = (40 * 5 * this.cashDetails.CashRegisterRolls.Nickels) / 100;
          this.totalDimeRoll = (50 * 10 * this.cashDetails.CashRegisterRolls.Dimes) / 100;
          this.totalQuaterRoll = (40 * 25 * this.cashDetails.CashRegisterRolls.Quarters) / 100;
          this.totalRoll = this.totalPennieRoll + this.totalNickelRoll + this.totalDimeRoll + this.totalQuaterRoll;
          setTimeout(() => {
            this.cashRegisterForm.patchValue({
              goal: this.targetBusiness?.WeatherPrediction?.TargetBusiness
            });
          }, 1200);
          this.getTotalCash();
        } else{
          this.isUpdate = false;
          this.cashRegisterForm.reset();
          this.cashRegisterCoinForm.reset();
          this.cashRegisterBillForm.reset();
          this.cashRegisterRollForm.reset();
        }
      }
    });
  }

  // Get WeatherDetails
  getWeatherDetails = () => {
    this.weatherService.data.subscribe((data: any) => {
      if (data !== undefined) {
        this.weatherDetails = data;
      }
    });
  }

  // Add/Update CashInRegister
  submit() {
    const coin = {
      cashRegCoinId: this.isUpdate ? this.cashDetails.CashRegisterCoins.CashRegCoinId : 0,
      cashRegisterId: this.isUpdate ? this.cashDetails.CashRegister.CashRegisterId : 0,
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
      cashRegBillId: this.isUpdate ? this.cashDetails.CashRegisterBills.CashRegBillId : 0,
      cashRegisterId: this.isUpdate ? this.cashDetails.CashRegister.CashRegisterId : 0,
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
      cashRegRollId: this.isUpdate ? this.cashDetails.CashRegisterRolls.CashRegRollId : 0,
      cashRegisterId: this.isUpdate ? this.cashDetails.CashRegister.CashRegisterId : 0,
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
      cashRegOtherId: this.isUpdate ? this.cashDetails.CashRegisterOthers.CashRegOtherId : 0,
      cashRegisterId: this.isUpdate ? this.cashDetails.CashRegister.CashRegisterId : 0,
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
      cashRegisterId: this.isUpdate ? this.cashDetails.CashRegister.CashRegisterId : 0,
      cashRegisterType: 119,
      locationId: +this.locationId,
      drawerId: +this.drawerId,
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
    // const weatherObj = {
    //   weatherId: 0,
    //   locationId: 1,
    //   weather: Math.floor(this.weatherDetails?.temporature).toString(),
    //   rainProbability: Math.floor(this.weatherDetails?.rainPercentage).toString(),
    //   predictedBusiness: '-',
    //   targetBusiness: this.cashRegisterForm.controls.goal.value,
    //   createdDate: moment(new Date()).format('YYYY-MM-DD')
    // };
    const weatherObj = {
      weatherId: 0,
      locationId: +this.locationId,
      // weather: Math.floor(this.targetBusiness?.WeatherPrediction?.Weather).toString(),
      // rainProbability: Math.floor(this.targetBusiness?.WeatherPrediction?.RainProbability).toString(),
    weather: (this.weatherDetails?.currentWeather.temporature) ?  Math.floor(this.weatherDetails?.currentWeather.temporature).toString() : null,
      rainProbability: (this.weatherDetails?.currentWeather.rainPercentage) ? Math.floor(this.weatherDetails?.currentWeather.rainPercentage).toString() : null,
      predictedBusiness: '-',
      targetBusiness: this.cashRegisterForm.controls.goal.value,
      createdDate: moment(new Date()).format('YYYY-MM-DD')
    };
    this.registerService.saveCashRegister(formObj, 'CASHIN').subscribe(data => {
      if (data.status === 'Success') {
        this.weatherService.UpdateWeather(weatherObj).subscribe(response => {
          if (response.status === 'Success') {
            this.toggleTab = 0;
            if (this.isUpdate) {
              this.toastr.success('Record Updated Successfully!!', 'Success!');
            } else {
              this.toastr.success('Record Saved Successfully!!', 'Success!');
            }
            this.weatherService.getWeather();
            this.getTargetBusinessData();
            this.getCashRegister();
          } else {
            this.toastr.error('Weather Communication Error', 'Error!');
          }
        });
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
    this.toggleTab = 0;
  }

  cancel() {
    this.toggleTab = 0;
  }

  next() {
    this.toggleTab = 1;
  }

  toggle() {
    if (this.toggleTab === 0) {
      this.toggleTab = 1;
    } else {
      this.toggleTab = 0;
    }
  }

  // Calcualte TotalCoins
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

  // Calcualte TotalBills
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

  // Calcualte TotalRolls
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

  // Calculate TotalCash
  getTotalCash() {
    this.totalCash = this.totalCoin + this.totalBill + this.totalRoll;
  }
  onValueChange(event) {
    let selectedDate = event;
    let today;
    if (selectedDate !== null) {
      selectedDate = moment(event.toISOString()).format('YYYY-MM-DD');
      this.selectDate = selectedDate;
      today = moment(new Date().toISOString()).format('YYYY-MM-DD');

      if(this.selectDate){
        this.getTargetBusinessData();
        this.getWeatherDetails();
      }
     
      if (moment(today).isSame(selectedDate)) {
        this.cashRegisterCoinForm.enable();
        this.cashRegisterBillForm.enable();
        this.cashRegisterRollForm.enable();
        
      } else if (moment(today).isAfter(selectedDate)) {
        this.cashRegisterCoinForm.disable();
        this.cashRegisterBillForm.disable();
        this.cashRegisterRollForm.disable();
      } else {
        this.cashRegisterCoinForm.enable();
        this.cashRegisterBillForm.enable();
        this.cashRegisterRollForm.enable();
      }


    }
    this.getCashRegister();
  }
}