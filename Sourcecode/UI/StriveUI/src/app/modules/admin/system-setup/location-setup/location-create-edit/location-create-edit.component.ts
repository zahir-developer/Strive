import { Component, OnInit, Output, EventEmitter, Input, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { LocationService } from 'src/app/shared/services/data-service/location.service';
import { StateDropdownComponent } from 'src/app/shared/components/state-dropdown/state-dropdown.component';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import * as moment from 'moment';
import { CityComponent } from 'src/app/shared/components/city/city.component';

@Component({
  selector: 'app-location-create-edit',
  templateUrl: './location-create-edit.component.html',
  styleUrls: ['./location-create-edit.component.css']
})
export class LocationCreateEditComponent implements OnInit {
  @ViewChild(StateDropdownComponent) stateDropdownComponent: StateDropdownComponent;
  @ViewChild(CityComponent) cityComponent: CityComponent;
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
  constructor(private fb: FormBuilder, private toastr: ToastrService, private locationService: LocationService,
    private uiLoaderService: NgxUiLoaderService) { }

  ngOnInit() {
    this.formInitialize();
    this.submitted = false;
    this.Country = 38;
    console.log(this.selectedData);
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
      email: ['', Validators.email],
      franchise: ['',],
      workHourThreshold: ['',]
    });
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
  }

  change(data) {
    this.locationSetupForm.value.franchise = data;
  }

  get f() {
    return this.locationSetupForm.controls;
  }

  // Add / Update location 
  submit() {
    this.submitted = true;
    this.stateDropdownComponent.submitted = true;
    this.cityComponent.submitted = true;
    if (this.cityComponent.country === '') {
      return;
    }
    if (this.locationSetupForm.invalid) {
      return;
    }
    const sourceObj = [];
    this.address = {
      locationAddressId: this.isEdit ? this.selectedData.LocationAddress.LocationAddressId : 0,
      locationId: this.isEdit ? this.selectedData.Location.LocationId : 0,
      address1: this.locationSetupForm.value.locationAddress,
      address2: this.locationSetupForm.value.locationAddress2,
      phoneNumber: this.locationSetupForm.value.phoneNumber,
      phoneNumber2: '',
      email: this.locationSetupForm.value.email,
      city: this.city,
      state: this.State,
      zip: this.locationSetupForm.value.zipcode,
      country: this.Country,
      longitude: 0,
      latitude: 0,
      weatherLocationId: 0,
      isActive: true,
      isDeleted: false,
      createdBy: 0,
      createdDate: moment(new Date()).format('YYYY-MM-DD'),
      updatedBy: 0,
      updatedDate: moment(new Date()).format('YYYY-MM-DD')
    };
    const formObj = {
      locationId: this.isEdit ? this.selectedData.Location.LocationId : 0,
      locationType: 1,
      locationName: this.locationSetupForm.value.locationName,
      locationDescription: '',
      isFranchise: this.locationSetupForm.value.franchise === '' ? false : this.locationSetupForm.value.franchise,
      taxRate: '',
      siteUrl: '',
      currency: 0,
      facebook: '',
      twitter: '',
      instagram: '',
      wifiDetail: '',
      washTimeMinutes: this.isEdit ? this.selectedData.Location.WashTimeMinutes : 0,
      workhourThreshold: this.locationSetupForm.value.workHourThreshold,
      startTime: '',
      endTime: '',
      isActive: true,
      isDeleted: false,
      createdBy: 0,
      createdDate: moment(new Date()).format('YYYY-MM-DD'),
      updatedBy: 0,
      updatedDate: moment(new Date()).format('YYYY-MM-DD')
    };
    const drawer = {
    drawerId: 0,
    drawerName: "",
    locationId: this.isEdit ? this.selectedData.Location.LocationId : 0,
    isActive: true,
    isDeleted: false,
    createdBy: 0,
    createdDate: moment(new Date()).format('YYYY-MM-DD'),
    updatedBy: 0,
    updatedDate: moment(new Date()).format('YYYY-MM-DD')
    };
    const finalObj = {
      location: formObj,
      locationAddress: this.address,
      drawer: drawer
    };
    if (this.isEdit === false) {
      this.locationService.saveLocation(finalObj).subscribe(data => {
        if (data.status === 'Success') {
          this.toastr.success('Record Saved Successfully!!', 'Success!');
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        } else {
          this.toastr.error('Communication Error', 'Error!');
          this.locationSetupForm.reset();
          this.submitted = false;
        }
      });
    } else {
      this.locationService.updateLocation(finalObj).subscribe(res => {
        if (res.status === 'Success') {
          this.toastr.success('Record Saved Successfully!!', 'Success!');
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        } else {
          this.toastr.error('Communication Error', 'Error!');
          this.locationSetupForm.reset();
          this.submitted = false;
        }
      });
    }
  }
  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }
  getSelectedStateId(event) {
    this.State = event.target.value;
  }
  getSelectedCountryId(event) {
    this.Country = event.target.value;

  }

  selectCity(id) {
    this.city = id;
  }
}

