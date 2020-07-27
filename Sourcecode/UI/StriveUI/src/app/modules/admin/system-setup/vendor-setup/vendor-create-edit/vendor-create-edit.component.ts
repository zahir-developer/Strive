import { Component, OnInit, Output, EventEmitter, Input, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { StateDropdownComponent } from 'src/app/shared/components/state-dropdown/state-dropdown.component';
import { VendorService } from 'src/app/shared/services/data-service/vendor.service';

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
  selectedVendor: any;
  selectedStateId: any;
  selectedCountryId: any;
  constructor(private fb: FormBuilder, private toastr: ToastrService, private vendorService: VendorService) { }

  ngOnInit() {
    this.formInitialize();
    this.submitted = false;
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
      phoneNumber: ['',],
      email: ['', Validators.email],
      fax: ['',]
    });
  }
  getVendorById() {
    this.vendorService.getVendorById(this.selectedData.VendorId).subscribe(data => {
      if (data.status === 'Success') {
        const vendor = JSON.parse(data.resultData);
        this.selectedVendor = vendor.VendorDetail[0];
        const vendorAddress = this.selectedVendor.VendorAddress[0];
        this.selectedStateId = vendorAddress.State;
        // this.selectedCountryId = vendorAddress.Country;
        this.vendorSetupForm.patchValue({
          vin: this.selectedVendor.VIN,
          vendorAlias: this.selectedVendor.VendorAlias,
          name: this.selectedVendor.VendorName,
          supplierAddress: this.selectedVendor.VendorAddress[0].Address1,
          zipcode: this.selectedVendor.VendorAddress[0].Zip,
          state: this.selectedVendor.VendorAddress[0].State,
          phoneNumber: this.selectedVendor.VendorAddress[0].PhoneNumber,
          email: this.selectedVendor.VendorAddress[0].Email,
          fax: this.selectedVendor.VendorAddress[0].Fax
        });
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }

  get f() {
    return this.vendorSetupForm.controls;
  }

  submit() {
    this.submitted = true;
    if (this.vendorSetupForm.invalid) {
      return;
    }
    this.address = [{
      relationshipId: this.isEdit ? this.selectedVendor.VendorId : 0,
      vendorAddressId: this.isEdit ? this.selectedVendor.VendorAddress[0].VendorAddressId : 0,
      address1: this.vendorSetupForm.value.supplierAddress,
      address2: "",
      phoneNumber2: "",
      isActive: true,
      zip: this.vendorSetupForm.value.zipcode,
      state: this.State,
      city: 1,
      Country: this.Country,
      phoneNumber: this.vendorSetupForm.value.phoneNumber,
      email: this.vendorSetupForm.value.email,
      fax: this.vendorSetupForm.value.fax
    }]
    const sourceObj = [];
    const formObj = {
      vendorId: this.isEdit ? this.selectedVendor.VendorId : 0,
      vin: this.vendorSetupForm.value.vin,
      vendorAlias: this.vendorSetupForm.value.vendorAlias,
      vendorName: this.vendorSetupForm.value.name,
      isActive: true,
      vendorAddress: this.address,
    };
    sourceObj.push(formObj);
    this.vendorService.updateVendor(sourceObj).subscribe(data => {
      if (data.status === 'Success') {
        if (this.isEdit === true) {
          this.toastr.success('Record Updated Successfully!!', 'Success!');
        } else {
          this.toastr.success('Record Saved Successfully!!', 'Success!');
        }
        this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
      } else {
        this.toastr.error('Communication Error', 'Error!');
        this.vendorSetupForm.reset();
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
