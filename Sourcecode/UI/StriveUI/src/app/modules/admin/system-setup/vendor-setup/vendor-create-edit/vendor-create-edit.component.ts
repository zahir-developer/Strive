import { Component, OnInit, Output, EventEmitter, Input, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { StateDropdownComponent } from 'src/app/shared/components/state-dropdown/state-dropdown.component';
import { VendorService } from 'src/app/shared/services/data-service/vendor.service';
import * as moment from 'moment';

@Component({
  selector: 'app-vendor-create-edit',
  templateUrl: './vendor-create-edit.component.html',
  styleUrls: ['./vendor-create-edit.component.css']
})
export class VendorCreateEditComponent implements OnInit {
  @ViewChild(StateDropdownComponent) stateDropdownComponent: StateDropdownComponent;
  vendorSetupForm: FormGroup;
  State: any;
  Country: any;
  @Output() closeDialog = new EventEmitter();
  @Input() selectedData?: any;
  @Input() isEdit?: any;
  submitted: boolean;
  address: any;
  selectedStateId: any;
  selectedCountryId: any;
  constructor(private fb: FormBuilder, private toastr: ToastrService, private vendorService: VendorService) { }

  ngOnInit() {
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
      vendorAlias: [''],
      name: ['', Validators.required],
      supplierAddress: ['', Validators.required],
      zipcode: ['', [Validators.required]],
      state: ['',],
      country: ['',],
      phoneNumber: ['', [Validators.minLength(14)]],
      email: ['', Validators.email],
      fax: ['',]
    });
  }
  getVendorById() {
    const vendorAddress = this.selectedData;
    this.selectedStateId = vendorAddress.State;
    this.State = this.selectedStateId;
    this.selectedCountryId = vendorAddress.Country;
    this.Country = this.selectedCountryId;
    this.vendorSetupForm.patchValue({
      vin: this.selectedData.VIN,
      vendorAlias: this.selectedData.VendorAlias,
      name: this.selectedData.VendorName,
      supplierAddress: this.selectedData.Address1,
      zipcode: this.selectedData.Zip,
      phoneNumber: this.selectedData.PhoneNumber,
      email: this.selectedData.Email,
      fax: this.selectedData.Fax
    });
  }

  get f() {
    return this.vendorSetupForm.controls;
  }

  // Add/Update Vendor
  submit() {
    this.submitted = true;
    if (this.vendorSetupForm.invalid) {
      return;
    }
    const vendorObj = {
      vendorId: this.isEdit ? this.selectedData.VendorId : 0,
      vin: this.vendorSetupForm.value.vin,
      vendorName: this.vendorSetupForm.value.name,
      vendorAlias: this.vendorSetupForm.value.vendorAlias,
      isActive: true,
      isDeleted: false,
      createdBy: 0,
      createdDate: moment(new Date()).format('YYYY-MM-DD'),
      updatedBy: 0,
      updatedDate: moment(new Date()).format('YYYY-MM-DD'),
      websiteAddress: 'string',
      accountNumber: 'string'
    };
    const addressObj = {
      vendorAddressId: 0,
      vendorId: this.isEdit ? this.selectedData.VendorId : 0,
      address1: this.vendorSetupForm.value.supplierAddress,
      address2: 'string',
      phoneNumber: this.vendorSetupForm.value.phoneNumber,
      phoneNumber2: 'string',
      email: this.vendorSetupForm.value.email,
      city: 0,
      state: this.Country,
      zip: this.vendorSetupForm.value.zipcode,
      fax: this.vendorSetupForm.value.fax,
      country: this.State,
      isActive: true,
      isDeleted: false,
      createdBy: 0,
      createdDate: moment(new Date()).format('YYYY-MM-DD'),
      updatedBy: 0,
      updatedDate: moment(new Date()).format('YYYY-MM-DD')
    };
    const finalObj = {
      vendor: vendorObj,
      vendorAddress: addressObj
    };
    if (this.isEdit === false) {
      this.vendorService.saveVendor(finalObj).subscribe(res => {
        if (res.status === 'Success') {
          this.toastr.success('Record Saved Successfully!!', 'Success!');
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        }
      });
    } else {
      this.vendorService.updateVendor(finalObj).subscribe(res => {
        if (res.status === 'Success') {
          this.toastr.success('Record Updated Successfully!!', 'Success!');
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
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
}
