import { Component, OnInit, Output, Input, EventEmitter } from '@angular/core';
import { VehicleService } from 'src/app/shared/services/data-service/vehicle.service';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-vehicle-create-edit',
  templateUrl: './vehicle-create-edit.component.html',
  styleUrls: ['./vehicle-create-edit.component.css']
})
export class VehicleCreateEditComponent implements OnInit {
  vehicleForm: FormGroup;
  @Output() closeDialog = new EventEmitter();
  @Input() selectedData?: any;
  @Input() isEdit?: any;
  make:any;
  model:any;
  color:any;
  upcharge:any;
  constructor(private fb: FormBuilder, private toastr: ToastrService, private vehicle: VehicleService) { }

  ngOnInit() {
    this.formInitialize();
    this.make = [{ id: 0, Value: "Make1" }, { id: 1, Value: "Make2" }];
    this.model = [{ id: 0, Value: "Model1" }, { id: 1, Value: "Model2" }];
    this.color = [{ id: 0, Value: "Color1" }, { id: 1, Value: "Color2" }];
    this.upcharge = [{ id: 0, Value: "None" }, { id: 1, Value: "Upcharge1" }, { id: 2, Value: "Upcharge2" }];
    if (this.isEdit === true) {
      this.vehicleForm.reset();
      //this.getLocationById();
    }
  }

  formInitialize() {
    this.vehicleForm = this.fb.group({
      fName: ['',],
      lName: ['',],
      address: ['',],
      zipcode: ['',],
      state: ['',],
      city: ['',],
      phone1: ['',],
      email: ['',],
      phone2: ['',],
      creditAccount: ['',],
      noEmail: ['',],
      score: ['',],
      status: ['',],
      notes: ['',],
      checkOut: ['',]
    });
  }

  // getLocationById() {
  //   const locationAddress = this.selectedData.LocationAddress;
  //   this.selectedStateId = locationAddress.State;
  //   this.State = this.selectedStateId;
  //   this.selectedCountryId = locationAddress.Country;
  //   this.Country = this.selectedCountryId;
  //   this.vehicleForm.patchValue({
  //     locationName: this.selectedData.LocationName,
  //     locationAddress: this.selectedData.LocationAddress.Address1,
  //     locationAddress2: this.selectedData.LocationAddress.Address2,
  //     workHourThreshold: this.selectedData.WorkhourThreshold,
  //     zipcode: this.selectedData.LocationAddress.Zip,
  //     phoneNumber: this.selectedData.LocationAddress.PhoneNumber,
  //     email: this.selectedData.LocationAddress.Email,
  //     franchise: this.selectedData.IsFranchise
  //   });
  // }

  change(data) {
    this.vehicleForm.value.franchise = data;
  }

  submit() {   
    const formObj = {
      locationId: this.isEdit ? this.selectedData.LocationId : 0,
      locationType: 1,
      locationName: this.vehicleForm.value.locationName,
      locationDescription: "",
      isActive: true,
      taxRate: "",
      siteUrl: "",
      currency: 0,
      facebook: "",
      twitter: "",
      instagram: "",
      wifiDetail: "",
      workHourThreshold: this.vehicleForm.value.workHourThreshold,
      isFranchise: this.vehicleForm.value.franchise == "" ? false : this.vehicleForm.value.franchise
    };
    // this.locationService.updateLocation(sourceObj).subscribe(data => {
    //   if (data.status === 'Success') {
    //     if (this.isEdit === true) {
    //       this.toastr.success('Record Updated Successfully!!', 'Success!');
    //     } else {
    //       this.toastr.success('Record Saved Successfully!!', 'Success!');
    //     }
    //     this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
    //   } else {
    //     this.toastr.error('Communication Error', 'Error!');
    //     this.vehicleForm.reset();
    //     this.submitted = false;
    //   }
    // });
  }
  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved'});
  }
}

