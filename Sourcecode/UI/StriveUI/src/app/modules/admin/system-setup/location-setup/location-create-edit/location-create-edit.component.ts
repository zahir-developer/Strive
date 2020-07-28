import { Component, OnInit, Output, EventEmitter, Input, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { LocationService } from 'src/app/shared/services/data-service/location.service';
import { StateDropdownComponent } from 'src/app/shared/components/state-dropdown/state-dropdown.component';

@Component({
  selector: 'app-location-create-edit',
  templateUrl: './location-create-edit.component.html',
  styleUrls: ['./location-create-edit.component.css']
})
export class LocationCreateEditComponent implements OnInit {
  @ViewChild(StateDropdownComponent) stateDropdownComponent: StateDropdownComponent;
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
  constructor(private fb: FormBuilder, private toastr: ToastrService, private locationService: LocationService) { }

  ngOnInit() {
    this.formInitialize();
    this.submitted = false;
    if (this.isEdit === true) {
      this.locationSetupForm.reset();
      this.getLocationById();
    }
  }

  formInitialize() {
    this.locationSetupForm = this.fb.group({
      locationAddress2: ['', Validators.required],
      locationName: ['', Validators.required],
      locationAddress: ['', Validators.required],
      zipcode: ['', Validators.required],
      state: ['',],
      country: ['',],
      phoneNumber: ['', [Validators.minLength(14)]],
      email: ['',],
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
    this.locationSetupForm.patchValue({
      locationName: this.selectedData.LocationName,
      locationAddress: this.selectedData.LocationAddress.Address1,
      locationAddress2: this.selectedData.LocationAddress.Address2,
      workHourThreshold: this.selectedData.WorkhourThreshold,
      zipcode: this.selectedData.LocationAddress.Zip,
      phoneNumber: this.selectedData.LocationAddress.PhoneNumber,
      email: this.selectedData.LocationAddress.Email,
      franchise: this.selectedData.IsFranchise
    });
  }

  change(data) {
    this.locationSetupForm.value.franchise = data;
  }

  get f() {
    return this.locationSetupForm.controls;
  }

  submit() {
    this.submitted = true;
    this.stateDropdownComponent.submitted = true;
    if (this.locationSetupForm.invalid) {
      return;
    }
    const sourceObj = [];
    this.address = {
      relationshipId: this.isEdit ? this.selectedData.LocationId : 0,
      locationAddressId: this.isEdit ? this.selectedData.LocationAddress.LocationAddressId : 0,
      address1: this.locationSetupForm.value.locationAddress,
      address2: this.locationSetupForm.value.locationAddress2,
      phoneNumber2: "",
      isActive: true,
      zip: this.locationSetupForm.value.zipcode,
      state: this.State,
      city: 1,
      country: this.Country,
      phoneNumber: this.locationSetupForm.value.phoneNumber,
      email: this.locationSetupForm.value.email
    }
    const formObj = {
      locationId: this.isEdit ? this.selectedData.LocationId : 0,
      locationType: 1,
      locationName: this.locationSetupForm.value.locationName,
      locationDescription: "",
      isActive: true,
      taxRate: "",
      siteUrl: "",
      currency: 0,
      facebook: "",
      twitter: "",
      instagram: "",
      wifiDetail: "",
      workHourThreshold: this.locationSetupForm.value.workHourThreshold,
      locationAddress: this.address,
      isFranchise: this.locationSetupForm.value.franchise == "" ? false : this.locationSetupForm.value.franchise
    };
    sourceObj.push(formObj);
    this.locationService.updateLocation(sourceObj).subscribe(data => {
      if (data.status === 'Success') {
        if (this.isEdit === true) {
          this.toastr.success('Record Updated Successfully!!', 'Success!');
        } else {
          this.toastr.success('Record Saved Successfully!!', 'Success!');
        }
        this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
      } else {
        this.toastr.error('Communication Error', 'Error!');
        this.locationSetupForm.reset();
        this.submitted = false;
      }
    });
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
}

