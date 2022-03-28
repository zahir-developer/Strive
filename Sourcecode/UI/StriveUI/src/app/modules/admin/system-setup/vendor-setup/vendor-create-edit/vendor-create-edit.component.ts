import { Component, OnInit, Output, EventEmitter, Input, ViewChild, AfterViewInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { StateDropdownComponent } from 'src/app/shared/components/state-dropdown/state-dropdown.component';
import { VendorService } from 'src/app/shared/services/data-service/vendor.service';
import * as moment from 'moment';
import { CityComponent } from 'src/app/shared/components/city/city.component';
import { CountryDropdownComponent } from 'src/app/shared/components/country-dropdown/country-dropdown.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';

@Component({
  selector: 'app-vendor-create-edit',
  templateUrl: './vendor-create-edit.component.html'
})
export class VendorCreateEditComponent implements OnInit {
  @ViewChild(CityComponent) cityComponent: CityComponent;
  @ViewChild(StateDropdownComponent) stateDropdownComponent: StateDropdownComponent;
  @ViewChild(CountryDropdownComponent) countryDropdownComponent: CountryDropdownComponent;
  vendorSetupForm: FormGroup;
  State: any;
  Country: any;
  city: any;
  @Output() closeDialog = new EventEmitter();
  @Input() selectedData?: any;
  @Input() isEdit?: any;
  submitted: boolean;
  address: any;
  selectedStateId: any;
  selectedCountryId: any;
  selectedCityId: any;
  employeeId: number;
  emailList = [];
  emailAddress = [];
  errorMessage: boolean;
  constructor(
    private fb: FormBuilder,
    private toastr: ToastrService,
    private vendorService: VendorService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit() {
    this.employeeId = +localStorage.getItem('empId');

    this.formInitialize();
    this.submitted = false;
    this.Country = 38;
    if (this.isEdit === true) {
      this.vendorSetupForm.reset();
      this.getVendorById();
    }
  }

  formInitialize() {
    this.vendorSetupForm = this.fb.group({
      vin: ['', Validators.required],
      vendorAlias: ['', Validators.required],
      name: ['', Validators.required],
      supplierAddress: [''],
      zipcode: ['', [Validators.required]],
      state: ['',],
      country: ['',],
      phoneNumber: ['', [Validators.minLength(14)]],
      email: [''],
      fax: ['',],
      website: ['']
    });
  }
  getVendorById() {
    const vendorAddress = this.selectedData;
    this.selectedStateId = vendorAddress.State;
    this.State = this.selectedStateId;
    this.selectedCountryId = vendorAddress.Country;
    this.Country = this.selectedCountryId;
    this.selectedCityId = vendorAddress.City;
    this.city = this.selectedCityId;
    this.vendorSetupForm.patchValue({
      vin: this.selectedData.VIN,
      vendorAlias: this.selectedData.VendorAlias,
      name: this.selectedData.VendorName,
      supplierAddress: this.selectedData.Address1,
      zipcode: this.selectedData.Zip,
      phoneNumber: this.selectedData.PhoneNumber,
      email: this.selectedData.Email,
      fax: this.selectedData.Fax,
      website: this.selectedData.websiteAddress
    });
  }

  get f() {
    return this.vendorSetupForm.controls;
  }
  testMail(event) {
    
    if(!this.validateEmail( this.vendorSetupForm.value.email)) {
       this.errorMessage =  true;
    }
    else{
      this.errorMessage =  false;

    }
  }
  
  validateEmail(email) {
     var re = /^((\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)\s*[;]{0,1}\s*)+$/
     return re.test(String(email).toLowerCase());
 }
  // Add/Update Vendor
  submit() {
    this.submitted = true;
    if (this.errorMessage ===  true) {
      return;
    }
    if (this.vendorSetupForm.invalid) {
    
      return;
    }
    if (this.stateDropdownComponent.stateValueSelection == false ) {
      this.toastr.error('State is Required', 'Error!');

      return;
    }
  
    const vendorObj = {
      vendorId: this.isEdit ? this.selectedData.VendorId : 0,
      vin: this.vendorSetupForm.value.vin,
      vendorName: this.vendorSetupForm.value.name,
      vendorAlias: this.vendorSetupForm.value.vendorAlias,
      isActive: true,
      isDeleted: false,
      createdBy: this.employeeId,
      createdDate: moment(new Date()).format('YYYY-MM-DD'),
      updatedBy: this.employeeId,
      updatedDate: moment(new Date()).format('YYYY-MM-DD'),
      websiteAddress: this.vendorSetupForm.value.website,
      accountNumber: ''
    };
    const addressObj = {
      vendorAddressId: this.isEdit ? this.selectedData.VendorAddressId : 0,
      vendorId: this.isEdit ? this.selectedData.VendorId : 0,
      address1: this.vendorSetupForm.value.supplierAddress,
      address2: '',
      phoneNumber: this.vendorSetupForm.value.phoneNumber,
      phoneNumber2: '',
      email: this.vendorSetupForm.value.email,
      city: this.city,
      state: this.State,
      zip: this.vendorSetupForm.value.zipcode,
      fax: this.vendorSetupForm.value.fax,
      country: this.countryDropdownComponent.country,
      isActive: true,
      isDeleted: false,
      createdBy: this.employeeId,
      createdDate: moment(new Date()).format('YYYY-MM-DD'),
      updatedBy: this.employeeId,
      updatedDate: moment(new Date()).format('YYYY-MM-DD')
    };
    
     
    
    const finalObj = {
      vendor: vendorObj,
      vendorAddress: addressObj
      
    };
    if (this.isEdit === false) {
      this.spinner.show();
      this.vendorService.saveVendor(finalObj).subscribe(res => {
        if (res.status === 'Success') {
          this.spinner.hide();

          this.toastr.success(MessageConfig.Admin.SystemSetup.Vendor.Add, 'Success!');
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        }
        else{
          this.spinner.hide();

        }
      }, (err) => {
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
    } else {
      this.spinner.show();
      this.vendorService.updateVendor(finalObj).subscribe(res => {
        if (res.status === 'Success') {
          this.spinner.hide();

          this.toastr.success(MessageConfig.Admin.SystemSetup.Vendor.Update, 'Success!');
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        }
        else{
          this.spinner.hide();

        }
      }, (err) => {
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
}
