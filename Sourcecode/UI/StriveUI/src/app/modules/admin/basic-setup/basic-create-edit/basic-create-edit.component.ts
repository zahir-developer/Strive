import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { LocationService } from 'src/app/shared/services/data-service/location.service';
import { log } from 'console';

@Component({
  selector: 'app-basic-create-edit',
  templateUrl: './basic-create-edit.component.html',
  styleUrls: ['./basic-create-edit.component.css']
})
export class BasicCreateEditComponent implements OnInit {
  basicSetupForm: FormGroup;
  State:any;
  Country:any;
  address:any;
  @Output() closeDialog = new EventEmitter();
  @Input() selectedData?: any;
  @Input() isEdit?: any;
  constructor(private fb: FormBuilder, private toastr: ToastrService,private locationService: LocationService) { }

  ngOnInit() {
    this.basicSetupForm = this.fb.group({
      locationId: ['', Validators.required],
      locationName: ['', Validators.required],
      locationAddress: ['', Validators.required],
      zipcode: ['', Validators.required],
      state: ['', Validators.required],
      country: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      email: ['', Validators.required],
      franchise: ['', Validators.required]
    });
    console.log(this.selectedData);
    if (this.selectedData !== undefined && this.selectedData.length !== 0) {
      this.basicSetupForm.reset();
      this.basicSetupForm.patchValue({
        locationId: this.selectedData.LocationId,
        locationName: this.selectedData.LocationName,
        locationAddress: this.selectedData.LocationDescription,
        // zipcode: this.selectedData.Zipcode,
        // state: this.selectedData.State,
        // country: this.selectedData.Country,
        // phoneNumber: this.selectedData.PhoneNumber,
        // email: this.selectedData.Email,
        franchise: this.selectedData.IsFranchise        
      });
    }
  }

  change(data){
    this.basicSetupForm.value.franchise = data;
  }
  submit() {
    console.log('submitted');
    const sourceObj = [];
    this.address=[{
      addressId:1,
      locationAddressId:this.basicSetupForm.value.locationId == "" ? "" : this.basicSetupForm.value.locationId,
      address1:this.basicSetupForm.value.locationAddress == "" ? "" :this.basicSetupForm.value.locationAddress,
      address2:"",
      phoneNumber2:"",
      isActive:true,
      zip: this.basicSetupForm.value.zipcode == "" ? "" : this.basicSetupForm.value.zipcode,
      state: this.basicSetupForm.value.state == "" ? 0 : this.basicSetupForm.value.state,
      city: 0,//this.basicSetupForm.value.country,
      phoneNumber: this.basicSetupForm.value.phoneNumber == "" ? "" : this.basicSetupForm.value.phoneNumber,
      email: this.basicSetupForm.value.email == "" ? "" : this.basicSetupForm.value.email
    }]
    const formObj = {
      locationId: this.basicSetupForm.value.locationId == "" ? "" : this.basicSetupForm.value.locationId,
      locationType:0,      
      locationName: this.basicSetupForm.value.locationName == "" ? "" : this.basicSetupForm.value.locationName,
      locationDescription: this.basicSetupForm.value.locationAddress == "" ? "" : this.basicSetupForm.value.locationAddress,
      isActive:true,
      taxRate:"",
      siteUrl:"",
      currency:0,
      facebook:"",
      twitter:"",
      instagram:"",
      wifiDetail:"",
      workHourThreshold:0,
      locationAddress:this.address,
      isFranchise: this.basicSetupForm.value.franchise == "" ? false : this.basicSetupForm.value.franchise
    };
    sourceObj.push(formObj);
    console.log(sourceObj);
    this.locationService.updateLocation(sourceObj).subscribe(data => {
      if (data.status === 'Success') {
        if (this.isEdit === true) {
          this.toastr.success('Record Updated Successfully!!', 'Success!');
        } else {
          this.toastr.success('Record Saved Successfully!!', 'Success!');
        }
        this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
      }
    });
  }
  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }
}
