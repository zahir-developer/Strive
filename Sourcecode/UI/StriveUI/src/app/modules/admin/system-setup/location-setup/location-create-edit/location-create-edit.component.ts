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
      phoneNumber: ['',],
      email: ['',],
      franchise: ['',],
      workHourThreshold: ['',]
    });
  }

  getLocationById() {
    this.locationService.getLocationById(this.selectedData.LocationId).subscribe(data => {
      if (data.status === 'Success') {
        const location = JSON.parse(data.resultData);
        this.selectedLocation = location.Location[0];
        console.log(this.selectedLocation);
        const locationAddress = this.selectedLocation.LocationAddress[0];
        this.selectedStateId = locationAddress.State;
        this.selectedCountryId = locationAddress.Country;
        this.locationSetupForm.patchValue({
          locationName: this.selectedLocation.LocationName,
          locationAddress: this.selectedLocation.LocationAddress[0].Address1,
          locationAddress2: this.selectedLocation.LocationAddress[0].Address2,
          workHourThreshold: this.selectedLocation.WorkhourThreshold,
          zipcode: this.selectedLocation.LocationAddress[0].Zip,
          phoneNumber: this.selectedLocation.LocationAddress[0].PhoneNumber,
          email: this.selectedLocation.LocationAddress[0].Email,
          franchise: this.selectedLocation.IsFranchise
        });
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
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
    this.address = [{
      relationshipId: this.isEdit ? this.selectedLocation.LocationId : 0,
      locationAddressId: this.isEdit ? this.selectedLocation.LocationAddress[0].LocationAddressId : 0,
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
    }]
    const formObj = {
      locationId: this.isEdit ? this.selectedLocation.LocationId : 0,
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

