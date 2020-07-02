import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { LocationService } from 'src/app/shared/services/data-service/location.service';
import { log } from 'console';

@Component({
  selector: 'app-location-create-edit',
  templateUrl: './location-create-edit.component.html',
  styleUrls: ['./location-create-edit.component.css']
})
export class LocationCreateEditComponent implements OnInit {
  locationSetupForm: FormGroup;
  State:any;
  Country:any;
  address:any;
  selectedLocation:any;
  @Output() closeDialog = new EventEmitter();
  @Input() selectedData?: any;
  @Input() isEdit?: any;
  constructor(private fb: FormBuilder, private toastr: ToastrService,private locationService: LocationService) { }

  ngOnInit() {
    this.locationSetupForm = this.fb.group({
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
    if (this.isEdit === true) {
      this.locationSetupForm.reset();
      this.getLocationById();      
    }
  }

  getLocationById(){
    this.locationService.getLocationById(this.selectedData.LocationId).subscribe(data => {
      if (data.status === 'Success') {
        const location = JSON.parse(data.resultData);
        this.selectedLocation = location.Location[0];
        this.locationSetupForm.patchValue({
          locationId: this.selectedLocation.LocationId,
          locationName: this.selectedLocation.LocationName,
          locationAddress: this.selectedLocation.LocationDescription,
          // zipcode: this.selectedLocation.LocationAddress[0].Zip,
          // state: this.selectedLocation.LocationAddress[0].State,
          //country: this.selectedData.Country,
          // phoneNumber: this.selectedLocation.LocationAddress[0].PhoneNumber,
          // email: this.selectedLocation.LocationAddress[0].Email,
          franchise: this.selectedLocation.IsFranchise        
        });
      }
    });
  }

  change(data){
    this.locationSetupForm.value.franchise = data;
  }
  submit() {
    console.log('submitted');
    const sourceObj = [];
    this.address=[{
      addressId:1,
      locationAddressId:this.locationSetupForm.value.locationId == "" ? "" : this.locationSetupForm.value.locationId,
      address1:this.locationSetupForm.value.locationAddress == "" ? "" :this.locationSetupForm.value.locationAddress,
      address2:"",
      phoneNumber2:"",
      isActive:true,
      zip: this.locationSetupForm.value.zipcode == "" ? "" : this.locationSetupForm.value.zipcode,
      state: this.locationSetupForm.value.state == "" ? 0 : this.locationSetupForm.value.state,
      city: 0,//this.locationSetupForm.value.country,
      phoneNumber: this.locationSetupForm.value.phoneNumber == "" ? "" : this.locationSetupForm.value.phoneNumber,
      email: this.locationSetupForm.value.email == "" ? "" : this.locationSetupForm.value.email
    }]
    const formObj = {
      locationId: this.locationSetupForm.value.locationId == "" ? "" : this.locationSetupForm.value.locationId,
      locationType:0,      
      locationName: this.locationSetupForm.value.locationName == "" ? "" : this.locationSetupForm.value.locationName,
      locationDescription: this.locationSetupForm.value.locationAddress == "" ? "" : this.locationSetupForm.value.locationAddress,
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
      isFranchise: this.locationSetupForm.value.franchise == "" ? false : this.locationSetupForm.value.franchise
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
