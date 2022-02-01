import { Component, OnInit, Output, EventEmitter, Input, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { LocationService } from 'src/app/shared/services/data-service/location.service';
import { StateDropdownComponent } from 'src/app/shared/components/state-dropdown/state-dropdown.component';
import * as moment from 'moment';
import { CityComponent } from 'src/app/shared/components/city/city.component';
import { CountryDropdownComponent } from 'src/app/shared/components/country-dropdown/country-dropdown.component';
import { TabsetComponent } from 'ngx-bootstrap/tabs';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';

@Component({
  selector: 'app-location-create-edit',
  templateUrl: './location-create-edit.component.html'
})
export class LocationCreateEditComponent implements OnInit {
  @ViewChild(StateDropdownComponent) stateDropdownComponent: StateDropdownComponent;
  @ViewChild(CityComponent) cityComponent: CityComponent;
  @ViewChild(CountryDropdownComponent) countryDropdownComponent: CountryDropdownComponent;
  @ViewChild('staticTabs', { static: false }) staticTabs: TabsetComponent;
  locationSetupForm: FormGroup;
  State: any;
  Country: any;
  address: any;
  selectedLocation: any;
  @Output() closeDialog = new EventEmitter();
  @Input() selectedData?: any;
  @Input() isEdit?: any;
  submitted: boolean;
  selectedStateId: any;
  selectedCountryId: any;
  city: any;
  selectedCityId: any;
  offset1On = false;
  offset1 = '';
  offsetA = '';
  offsetB = '';
  offsetC = '';
  offsetD = '';
  offsetE = '';
  offsetF = '';

  //Merchat Detail

  merchantId = '';
  merchantUserName = '';
  merchantPassword = '';
  Url = '';
  emailPattern: '^([\w+-.%]+@[\w-.]+\.[A-Za-z]{2,4},*[\W]*)+$'
  isOffset: boolean;
  employeeId: number;
  errorMessage: boolean = false;
  emailList = [];
  emailRemovedList = [];
  emailAddress = [];
  constructor(
    private fb: FormBuilder,
    private toastr: ToastrService,
    private locationService: LocationService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit() {
    this.employeeId = +localStorage.getItem('empId');
    this.isOffset = false;
    this.formInitialize();
    this.submitted = false;
    this.Country = null;
    if (this.isEdit === true) {
      this.locationSetupForm.reset();
      this.getLocationById();
    }
  }

  formInitialize() {
    this.locationSetupForm = this.fb.group({
      locationAddress2: [''],
      locationName: ['', Validators.required],
      locationAddress: ['', Validators.required],
      zipcode: ['', Validators.required],
      state: ['',],
      country: ['',],
      phoneNumber: ['', [Validators.minLength(14)]],
      email: '',
      franchise: ['',],
      workHourThreshold: ['',]
    });
  }


  addEmail() {
    if (this.emailList.length >= ApplicationConfig.EmailSize.location) {
      this.toastr.error(MessageConfig.Admin.SystemSetup.BasicSetup.Email, 'Error!');
      return;
    }

    var re = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
    if (!re.test(this.locationSetupForm.value.email) && this.locationSetupForm.value.email !== '') {
      this.toastr.error(MessageConfig.Admin.SystemSetup.BasicSetup.InvalidEmail, 'Error!');
      return;
    }

    if (this.locationSetupForm.value.email !== '') {
      this.emailList.push({
        locationEmailId: 0,
        locationId: this.isEdit ? this.selectedData.Location.LocationId : 0,
        EmailAddress: this.locationSetupForm.value.email,
        isActive: true,
        isDeleted: false,
        createdBy: this.employeeId,
        createdDate: moment(new Date()).format('YYYY-MM-DD'),
        updatedBy: this.employeeId,
        updatedDate: moment(new Date()).format('YYYY-MM-DD')
      });
      this.emailList.forEach((item, index) => {
        item.id = index;
      });
      this.locationSetupForm.controls.email.reset();
    }
  }
  removeEmail(email) {
    if (email.locationEmailId === 0) {
      this.emailList = this.emailList.filter(item => item.id !== email.id);
    } else {
      this.emailRemovedList.push(email);
      this.emailList = this.emailList.filter(item => item.LocationEmailId !== email.LocationEmailId);
    }
  }
  getLocationById() {
    const locationAddress = this.selectedData.LocationAddress;
    this.selectedStateId = locationAddress.State;
    this.State = this.selectedStateId;
    this.selectedCountryId = locationAddress.Country;
    this.Country = this.selectedCountryId;
    this.selectedCityId = locationAddress.City;
    this.city = this.selectedCityId;
    this.locationSetupForm.patchValue({
      locationName: this.selectedData.Location.LocationName,
      locationAddress: this.selectedData.LocationAddress.Address1,
      locationAddress2: this.selectedData.LocationAddress.Address2,
      workHourThreshold: this.selectedData.Location.WorkhourThreshold,
      zipcode: this.selectedData.LocationAddress.Zip,
      phoneNumber: this.selectedData.LocationAddress.PhoneNumber,
      email: this.selectedData.LocationAddress.Email,
      franchise: this.selectedData.Location.IsFranchise
    });
    if (this.selectedData.LocationEmail) {
      this.emailList = this.selectedData.LocationEmail;
    }
    if (this.selectedData.LocationOffset !== null) {
      this.offset1On = this.selectedData.LocationOffset.OffSet1On;
      if (this.offset1On) {
        this.isOffset = true;
      } else {
        this.isOffset = false;
      }
      this.offset1 = this.selectedData.LocationOffset.OffSet1;
      this.offsetA = this.selectedData.LocationOffset.OffSetA;
      this.offsetB = this.selectedData.LocationOffset.OffSetB;
      this.offsetC = this.selectedData.LocationOffset.OffSetC;
      this.offsetD = this.selectedData.LocationOffset.OffSetD;
      this.offsetE = this.selectedData.LocationOffset.OffSetE;
      this.offsetF = this.selectedData.LocationOffset.OffSetF;
    }
    if (this.selectedData.MerchantDetail !== null) {
      this.merchantId = this.selectedData?.MerchantDetail.MID;
      this.merchantUserName = this.selectedData?.MerchantDetail.UserName;
      this.merchantPassword = this.selectedData?.MerchantDetail.Password;
      this.Url = this.selectedData?.MerchantDetail.Url;
    }
  }

  change(data) {
    this.locationSetupForm.value.franchise = data;
  }

  handleChange(event) {
    console.log(event, 'event');
    if (event.checked) {
      this.isOffset = true;
    } else {
      this.isOffset = false;
    }
  }

  get f() {
    return this.locationSetupForm.controls;
  }

  // Add / Update location 
  submit() {
    this.submitted = true;
    this.stateDropdownComponent.submitted = true;
    this.cityComponent.submitted = true;
    this.countryDropdownComponent.submitted = true;
    if (this.cityComponent.selectValueCity == false) {
      this.selectTab(0);
      return;
    }
    if (this.locationSetupForm.invalid) {
      this.selectTab(0);
      return;
    }

    this.emailList.forEach((item, index) => {
      item.id = index;
      this.emailAddress.push({
        locationEmailId: item.LocationEmailId,
        locationId: this.isEdit ? this.selectedData.Location.LocationId : 0,
        emailAddress: item.EmailAddress,
        isActive: true,
        isDeleted: false,
        createdBy: this.employeeId,
        createdDate: moment(new Date()).format('YYYY-MM-DD'),
        updatedBy: this.employeeId,
        updatedDate: moment(new Date()).format('YYYY-MM-DD'),
      });
    });

    this.emailRemovedList.forEach((item, index) => {
      this.emailAddress.push({
        locationEmailId: item.LocationEmailId,
        locationId: this.isEdit ? this.selectedData.Location.LocationId : 0,
        emailAddress: item.EmailAddress,
        isActive: true,
        isDeleted: true,
        createdBy: this.employeeId,
        createdDate: moment(new Date()).format('YYYY-MM-DD'),
        updatedBy: this.employeeId,
        updatedDate: moment(new Date()).format('YYYY-MM-DD'),
      });
    });

    const sourceObj = [];
    this.address = {
      locationAddressId: this.isEdit ? this.selectedData.LocationAddress.LocationAddressId : 0,
      locationId: this.isEdit ? this.selectedData.Location.LocationId : 0,
      address1: this.locationSetupForm.value.locationAddress,
      address2: this.locationSetupForm.value.locationAddress2,
      phoneNumber: this.locationSetupForm.value.phoneNumber,
      phoneNumber2: '',
      email: null,
      city: this.city,
      state: this.State,
      zip: this.locationSetupForm.value.zipcode,
      country: this.countryDropdownComponent.country,
      longitude: this.isEdit ? this.selectedData.LocationAddress.Longitude : 0,
      latitude: this.isEdit ? this.selectedData.LocationAddress.Latitude : 0,
      weatherLocationId: 0,
      isActive: true,
      isDeleted: false,
      createdBy: this.employeeId,
      createdDate: moment(new Date()).format('YYYY-MM-DD'),
      updatedBy: this.employeeId,
      updatedDate: moment(new Date()).format('YYYY-MM-DD'),
      cityName : this.cityComponent.city.name,
      stateName : this.stateDropdownComponent.state.name

    };
    const formObj = {
      locationId: this.isEdit ? this.selectedData.Location.LocationId : 0,
      locationType: null,
      locationName: this.locationSetupForm.value.locationName,
      locationDescription: '',
      isFranchise: this.locationSetupForm.value.franchise === '' ? false : this.locationSetupForm.value.franchise,
      taxRate: null,
      siteUrl: null,
      currency: null,
      facebook: null,
      twitter: null,
      instagram: null,
      wifiDetail: null,
      washTimeMinutes: this.isEdit ? this.selectedData.Location.WashTimeMinutes : 0,
      workhourThreshold: this.locationSetupForm.value.workHourThreshold,
      startTime: null,
      endTime: null,
      isActive: true,
      isDeleted: false,
      createdBy: this.employeeId,
      createdDate: moment(new Date()).format('YYYY-MM-DD'),
      updatedBy: this.employeeId,
      updatedDate: moment(new Date()).format('YYYY-MM-DD')
    };
    const locationOffset = {
      locationOffSetId: this.isEdit ? this.selectedData.LocationOffset === null ? 0 :
        this.selectedData.LocationOffset.LocationOffSetId : 0,
      locationId: this.isEdit ? this.selectedData.Location.LocationId : 0,
      offSet1: this.offset1,
      offSetA: this.offsetA,
      offSetB: this.offsetB,
      offSetC: this.offsetC,
      offSetD: this.offsetD,
      offSetE: this.offsetE,
      offSetF: this.offsetF,
      offSet1On: this.offset1On,
      isActive: true,
      isDeleted: false
    };
    const drawer = {
      drawerId: 0,
      drawerName: null,
      locationId: this.isEdit ? this.selectedData.Location.LocationId : 0,
      isActive: true,
      isDeleted: false,
      createdBy: this.employeeId,
      createdDate: moment(new Date()).format('YYYY-MM-DD'),
      updatedBy: this.employeeId,
      updatedDate: moment(new Date()).format('YYYY-MM-DD')
    };

    const merchantDetail = {
      merchantDetailId: this.isEdit ? this.selectedData?.MerchantDetail?.MerchantDetailId : 0,
      locationId: this.isEdit ? this.selectedData.Location.LocationId : 0,
      mid: this.merchantId,
      userName: this.merchantUserName,
      password: this.merchantPassword,
      isActive: true,
      isDeleted: false,
      createdBy: this.employeeId,
      createdDate: moment(new Date()).format('YYYY-MM-DD'),
      updatedBy: this.employeeId,
      updatedDate: moment(new Date()).format('YYYY-MM-DD'),
      url: this.Url
    }

    const finalObj = {
      location: formObj,
      locationAddress: this.address,
      locationOffset,
      locationEmail: this.emailAddress,
      merchantDetail: merchantDetail
    };

    console.log(finalObj);
    
    if (this.isEdit === false) {
      this.spinner.show();
      this.locationService.saveLocation(finalObj).subscribe(data => {
        if (data.status === 'Success') {
          this.spinner.hide();

          this.toastr.success(MessageConfig.Admin.SystemSetup.BasicSetup.Add, 'Success!');
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        } else {
          this.spinner.hide();

          this.toastr.error('Communication Error', 'Error!');
          this.locationSetupForm.reset();
          this.submitted = false;
        }
      },
        (err) => {
          this.spinner.hide();
          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        });
    } else {
      this.spinner.show();
      this.locationService.updateLocation(finalObj).subscribe(res => {
        if (res.status === 'Success') {
          this.spinner.hide();

          this.toastr.success(MessageConfig.Admin.SystemSetup.BasicSetup.Update, 'Success!');
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        } else {
          this.spinner.hide();

          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
          this.locationSetupForm.reset();
          this.submitted = false;
        }
      },
        (err) => {
          this.spinner.hide();
          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        });
    }
  }
  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }
  getSelectedStateId(event) {
    this.State = event;
    this.cityComponent.getCity(event);
  }
  getSelectedCountryId(event) {
    this.Country = event.target.value;

  }

  selectCity(event) {
    this.city = event;
  }

  selectTab(tabId: number) {
    this.staticTabs.tabs[tabId].active = true;
  }
}

