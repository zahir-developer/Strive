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
import { GetCodeService } from 'src/app/shared/services/data-service/getcode.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { DatePipe } from '@angular/common';
import { CodeValueService } from 'src/app/shared/common-service/code-value.service';

@Component({
  selector: 'app-cash-register',
  templateUrl: './cash-register.component.html'
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
  Todaydate: any;
  date = moment(new Date()).format('MM/DD/YYYY');
  employeeId: string;
  documentTypeId: any;
  CahRegisterId: any;
  storeStatusList = [];
  storeTimeIn = '';
  storeStatus: any = '';
  storeTimeOut = '';
  submitted = false;
  isTimechange: boolean;
  constructor(private fb: FormBuilder, private registerService: CashRegisterService, private getCode: GetCodeService,
    private toastr: ToastrService, private weatherService: WeatherService,
    private cd: ChangeDetectorRef, private spinner: NgxSpinnerService, private datePipe: DatePipe,
    private codeValueService: CodeValueService) { }

  ngOnInit() {
    this.selectDate = moment(new Date()).format('MM/DD/YYYY');
    this.locationId = localStorage.getItem('empLocationId');
    this.employeeId = localStorage.getItem('empId');
    this.isTimechange = false;
    this.getDocumentType();
    this.drawerId = localStorage.getItem('drawerId');
    this.formInitialize();
    const locationId = +this.locationId;
    this.Todaydate = moment(new Date()).format('YYYY-MM-DD');
    this.getStoreStatusList();
    this.getTargetBusinessData(locationId, this.Todaydate);
    this.getWeatherDetails();
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
    this.cashRegisterForm = this.fb.group({
      goal: ['',]
    });
    this.toggleTab = 0;
    this.totalCoin = this.totalPennie = this.totalQuater = this.totalNickel = this.totalDime = this.totalHalf = 0;
    this.totalRoll = this.totalPennieRoll = this.totalQuaterRoll = this.totalNickelRoll = this.totalDimeRoll = 0;
    this.totalBill = this.totalOnes = this.totalFives = this.totalTens = this.totalTwenties = this.totalFifties = this.totalHunderds = 0;
    this.totalCash = 0;
    // this.getCashRegister();
  }

  // Get targetBusinessData
  getTargetBusinessData(locationId, date) {
    this.weatherService.getTargetBusinessData(locationId, date).subscribe(data => {
      if (data) {
        this.targetBusiness = JSON.parse(data.resultData);
        if (this.targetBusiness.WeatherPrediction.WeatherPredictionToday !== null) {
          this.cashRegisterForm.patchValue({
            goal: this.targetBusiness?.WeatherPrediction?.WeatherPredictionToday.TargetBusiness
          });
        }
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  // Get CashInRegister By Date
  getCashRegister() {
    const today = moment(new Date()).format('MM-DD-YYYY');
    const date = this.selectDate;
    const cashRegisterType = 'CASHIN';
    const locationId = +localStorage.getItem('empLocationId');
    this.cashRegisterForm.reset();
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
    this.registerService.getCashRegisterByDate(cashRegisterType, locationId, date).subscribe(data => {
      if (data.status === 'Success') {
        this.spinner.hide();

        const cashIn = JSON.parse(data.resultData);
        this.cashDetails = cashIn.CashRegister;
        if (this.cashDetails.CashRegister !== null) {
          this.isUpdate = true;
          const storeStatus = this.storeStatusList.filter(item => item.CodeId === this.cashDetails.CashRegister.StoreOpenCloseStatus);
          let isStatus = false;
          if (storeStatus.length > 0) {
            if (storeStatus[0].CodeValue === 'Open') {
              isStatus = true;
            } else {
              isStatus = false;
            }
          }
          this.storeStatus = this.cashDetails.CashRegister.StoreOpenCloseStatus !== null ?
            +this.cashDetails.CashRegister.StoreOpenCloseStatus : '';
          this.storeTimeIn = isStatus ? this.cashDetails.CashRegister.StoreTimeIn !== null ?
            moment(this.cashDetails.CashRegister.StoreTimeIn).format('HH:mm') : '' : this.cashDetails.CashRegister.StoreTimeOut !== null ?
              moment(this.cashDetails.CashRegister.StoreTimeOut).format('HH:mm') : '';
          // this.storeTimeIn  = this.cashDetails.CashRegister.StoreTimeOut !== null ?
          //   moment(this.cashDetails.CashRegister.StoreTimeOut).format('HH:mm') : '';
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
              goal: this.targetBusiness?.WeatherPrediction?.WeatherPredictionToday.TargetBusiness
            });
          }, 1200);
          this.getTotalCash();
        }
        else {
          this.isUpdate = false;
          this.cashRegisterForm.reset();
          this.cashRegisterCoinForm.reset();
          this.cashRegisterBillForm.reset();
          this.cashRegisterRollForm.reset();
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

  // Get WeatherDetails
  getWeatherDetails = () => {
    this.spinner.show();
    this.weatherService.data.subscribe((data: any) => {
      if (data !== undefined) {
        this.spinner.hide();
        //this.weatherDetails = JSON.parse(data.resultData);
        this.weatherDetails = data;
      } else {
        this.spinner.hide();

      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      this.spinner.hide();
    }
    );
  }
  getDocumentType() {
    const cashRegisterVaue = this.codeValueService.getCodeValueByType(ApplicationConfig.Category.cashRegister);
    if (cashRegisterVaue.length > 0) {
      this.CahRegisterId = cashRegisterVaue.filter(i => i.CodeValue === ApplicationConfig.CodeValue.CashIn)[0].CodeId;
    } else {
      this.getCode.getCodeByCategory(ApplicationConfig.Category.cashRegister).subscribe(data => {
        if (data.status === 'Success') {
          const dType = JSON.parse(data.resultData);
          this.CahRegisterId = dType.Codes.filter(i => i.CodeValue === ApplicationConfig.CodeValue.CashIn)[0].CodeId;
        } else {
          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        }
      }
        , (err) => {
          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        }
      );
    }
  }
  // Add/Update CashInRegister
  submit() {
    this.submitted = true;
    if (this.storeStatus === '' || this.storeTimeIn === '') {
      return;
    }
    const coin = {
      cashRegCoinId: this.isUpdate ? this.cashDetails.CashRegisterCoins.CashRegCoinId : 0,
      cashRegisterId: this.isUpdate ? this.cashDetails.CashRegister.CashRegisterId : 0,
      pennies: this.cashRegisterCoinForm.value.coinPennies == null ? 0 : this.cashRegisterCoinForm.value.coinPennies,
      nickels: this.cashRegisterCoinForm.value.coinNickels == null ? 0 : this.cashRegisterCoinForm.value.coinNickels,
      dimes: this.cashRegisterCoinForm.value.coinDimes == null ? 0 : this.cashRegisterCoinForm.value.coinDimes,
      quarters: this.cashRegisterCoinForm.value.coinQuaters == null ? 0 : this.cashRegisterCoinForm.value.coinQuaters,
      halfDollars: this.cashRegisterCoinForm.value.coinHalfDollars == null ? 0 : this.cashRegisterCoinForm.value.coinHalfDollars,
      isActive: true,
      isDeleted: false,
      createdBy: this.employeeId,
      createdDate: new Date(),
      updatedBy: this.employeeId,
      updatedDate: new Date(),
    };
    const bill = {
      cashRegBillId: this.isUpdate ? this.cashDetails.CashRegisterBills.CashRegBillId : 0,
      cashRegisterId: this.isUpdate ? this.cashDetails.CashRegister.CashRegisterId : 0,
      s1: this.cashRegisterBillForm.value.billOnes == null ? 0 : this.cashRegisterBillForm.value.billOnes,
      s5: this.cashRegisterBillForm.value.billFives == null ? 0 : this.cashRegisterBillForm.value.billFives,
      s10: this.cashRegisterBillForm.value.billTens == null ? 0 : this.cashRegisterBillForm.value.billTens,
      s20: this.cashRegisterBillForm.value.billTwenties == null ? 0 : this.cashRegisterBillForm.value.billTwenties,
      s50: this.cashRegisterBillForm.value.billFifties == null ? 0 : this.cashRegisterBillForm.value.billFifties,
      s100: this.cashRegisterBillForm.value.billHundreds == null ? 0 : this.cashRegisterBillForm.value.billHundreds,
      isActive: true,
      isDeleted: false,
      createdBy: this.employeeId,
      createdDate: new Date(),
      updatedBy: this.employeeId,
      updatedDate: new Date(),
    };
    const roll = {
      cashRegRollId: this.isUpdate ? this.cashDetails.CashRegisterRolls.CashRegRollId : 0,
      cashRegisterId: this.isUpdate ? this.cashDetails.CashRegister.CashRegisterId : 0,
      pennies: this.cashRegisterRollForm.value.pennieRolls == null ? 0 : this.cashRegisterRollForm.value.pennieRolls,
      nickels: this.cashRegisterRollForm.value.nickelRolls == null ? 0 : this.cashRegisterRollForm.value.nickelRolls,
      dimes: this.cashRegisterRollForm.value.dimeRolls == null ? 0 : this.cashRegisterRollForm.value.dimeRolls,
      quarters: this.cashRegisterRollForm.value.quaterRolls == null ? 0 : this.cashRegisterRollForm.value.quaterRolls,
      halfDollars: 0,
      isActive: true,
      isDeleted: false,
      createdBy: this.employeeId,
      createdDate: new Date(),
      updatedBy: this.employeeId,
      updatedDate: new Date(),
    };
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
      createdBy: this.employeeId,
      createdDate: new Date(),
      updatedBy: this.employeeId,
      updatedDate: new Date(),
    };
    let checkinTime = '';
    let checkoutTime = '';
    if (this.isUpdate && !this.isTimechange) {
      const intime = this.storeTimeIn.split(':');
      const inhour = intime[0];
      const inminutes = intime[1];
      const inTime: any = new Date(this.date);
      inTime.setHours(inhour);
      inTime.setMinutes(inminutes);
      inTime.setSeconds('00');
      checkinTime = this.datePipe.transform(inTime, 'MM/dd/yyyy HH:mm');
    } else {
      checkinTime = this.datePipe.transform(this.storeTimeIn, 'MM/dd/yyyy HH:mm');
      checkoutTime = this.storeTimeOut;
    }
    const storeStatus = this.storeStatusList.filter(item => item.CodeId === +this.storeStatus);
    let isStatus = false;
    if (storeStatus.length > 0) {
      if (storeStatus[0].CodeValue === 'Open') {
        isStatus = true;
      } else {
        isStatus = false;
      }
    }
    const cashregister = {
      cashRegisterId: this.isUpdate ? this.cashDetails.CashRegister.CashRegisterId : 0,
      cashRegisterType: this.CahRegisterId,
      locationId: +this.locationId,
      drawerId: +this.drawerId,
      cashRegisterDate: moment(this.date).format('YYYY-MM-DD'),
      isActive: true,
      isDeleted: false,
      createdBy: this.employeeId,
      createdDate: new Date(),
      updatedBy: this.employeeId,
      updatedDate: new Date(),
      storeTimeIn: isStatus ? checkinTime !== '' ? checkinTime : null : null,
      storeTimeOut: !isStatus ? checkinTime !== '' ? checkinTime : null : null,
      storeOpenCloseStatus: this.storeStatus === '' ? null : +this.storeStatus,
      totalAmount: this.totalCash
    };
    const formObj = {
      cashregister,
      cashRegisterCoins: coin,
      cashRegisterBills: bill,
      cashRegisterRolls: roll,
      cashregisterOthers: other
    }
    const weatherObj = {
      weatherId: this.targetBusiness?.WeatherPrediction?.WeatherPredictionToday?.WeatherId,
      locationId: +this.locationId,
      weather: (this.weatherDetails?.currentWeather?.temporature) ? Math.floor(this.weatherDetails?.currentWeather?.temporature).toString() : null,
      rainProbability: (this.weatherDetails?.currentWeather?.rainPercentage) ? Math.floor(this.weatherDetails?.currentWeather?.rainPercentage).toString() : null,
      predictedBusiness: '-',
      targetBusiness: this.cashRegisterForm.controls.goal.value,
      createdDate: moment(new Date()).format('YYYY-MM-DD')
    };
    this.spinner.show();
    this.registerService.saveCashRegister(formObj, 'CASHIN').subscribe(data => {
      this.submitted = false;
      this.isTimechange = false;
      if (data.status === 'Success') {
        this.spinner.hide();
        this.toastr.success(MessageConfig.Admin.CashRegister.Update, 'Success!');
        this.weatherService.UpdateWeather(weatherObj).subscribe(response => {
          if (response.status === 'Success') {
            this.spinner.hide();

            this.toggleTab = 0;

            this.weatherService.getWeather();
            this.getTargetBusinessData(this.locationId, this.Todaydate);
            this.getCashRegister();
          } else {
            this.spinner.hide();

          }
        });
      } else {
        this.spinner.hide();

        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }
      , (err) => {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        this.spinner.hide();
      }
    );
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

  getStoreStatusList() {
    const storeStatusVaue = this.codeValueService.getCodeValueByType(ApplicationConfig.Category.cashRegister);
    console.log(storeStatusVaue, 'cached value ');
    if (storeStatusVaue.length > 0) {
      this.storeStatusList = storeStatusVaue;
    } else {
      this.getCode.getCodeByCategory(ApplicationConfig.Category.storeStatus).subscribe(data => {
        if (data.status === 'Success') {
          const dType = JSON.parse(data.resultData);
          this.storeStatusList = dType.Codes;
          // this.storeStatusList = this.storeStatusList.filter(item => item.CodeValue === ApplicationConfig.storestatus.open);
        } else {
          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        }
      }
        , (err) => {
          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        }
      );
    }
  }

  inTime(event) {
    this.isTimechange = true;
    const time = event.split(':');
    const hour = time[0];
    const minutes = time[1];
    const checkinTime: any = new Date(this.date);
    checkinTime.setHours(hour);
    checkinTime.setMinutes(minutes);
    checkinTime.setSeconds('00');
    this.storeTimeIn = checkinTime;
  }

  outTime(event) {
    const time = event.split(':');
    const hour = time[0];
    const minutes = time[1];
    const checkoutTime: any = new Date(this.date);
    checkoutTime.setHours(hour);
    checkoutTime.setMinutes(minutes);
    checkoutTime.setSeconds('00');
    this.storeTimeOut = checkoutTime;
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
      const locationId = +this.locationId;
      this.toggleTab = 0;

      this.getWeatherDetails();
      this.getTargetBusinessData(this.locationId, this.selectDate);


      if (moment(today).isSame(selectedDate)) {
        this.cashRegisterCoinForm.enable();
        this.cashRegisterBillForm.enable();
        this.cashRegisterRollForm.enable();
        this.cashRegisterForm.enable();

      } else if (moment(today).isAfter(selectedDate)) {
        this.cashRegisterCoinForm.disable();
        this.cashRegisterBillForm.disable();
        this.cashRegisterRollForm.disable();
        this.cashRegisterForm.disable();
      } else {
        this.cashRegisterCoinForm.enable();
        this.cashRegisterBillForm.enable();
        this.cashRegisterRollForm.enable();
        this.cashRegisterForm.enable();
      }


    }
    this.getCashRegister();
  }
}
