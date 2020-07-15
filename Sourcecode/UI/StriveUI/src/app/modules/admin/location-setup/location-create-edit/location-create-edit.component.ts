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
  submitted: boolean;
  constructor(private fb: FormBuilder, private toastr: ToastrService,private locationService: LocationService) { }

  ngOnInit() {
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
      workHourThreshold:['',]
    });
    this.submitted = false;
    this.State = ["state1","state2","state3"];
    this.Country = ["USA"];
    this.locationSetupForm.controls['country'].patchValue(this.Country);
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
        console.log(this.selectedLocation);
        this.locationSetupForm.patchValue({
          // locationId: this.selectedLocation.LocationId,
          locationName: this.selectedLocation.LocationName,
          locationAddress: this.selectedLocation.LocationDescription,
          workHourThreshold:this.selectedLocation.WorkHourThreshold,
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

  get f(){
    return this.locationSetupForm.controls;
  }

  submit() {
    this.submitted = true;
    if(this.locationSetupForm.invalid){
      return;
    }
    const sourceObj = [];
    this.address=[{
      relationshipId:1,
      locationAddressId: 1,
      address1: this.locationSetupForm.value.locationAddress,
      address2: this.locationSetupForm.value.locationAddress2,
      phoneNumber2:"",
      isActive:true,
      zip: this.locationSetupForm.value.zipcode,
      state: 1,//this.locationSetupForm.value.state == "" ? 0 : this.locationSetupForm.value.state,
      city: 1,//this.locationSetupForm.value.country,
      country : 1,
      phoneNumber: this.locationSetupForm.value.phoneNumber,
      email: this.locationSetupForm.value.email
    }]
    const formObj = {
      locationId : this.isEdit ? this.selectedLocation.LocationId : 1,
      locationType:1,      
      locationName: this.locationSetupForm.value.locationName,
      locationDescription: "",
      isActive:true,
      taxRate:"",
      siteUrl:"",
      currency:0,
      facebook:"",
      twitter:"",
      instagram:"",
      wifiDetail:"",
      workHourThreshold:this.locationSetupForm.value.workHourThreshold,
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
      }else{
        this.toastr.error('Communication Error','Error!');
        this.locationSetupForm.reset();
        this.submitted=false;
      }
    });
  }
  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }
}
