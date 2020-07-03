import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { CrudOperationService } from 'src/app/shared/services/crud-operation.service';

@Component({
  selector: 'app-vendor-create-edit',
  templateUrl: './vendor-create-edit.component.html',
  styleUrls: ['./vendor-create-edit.component.css']
})
export class VendorCreateEditComponent implements OnInit {
  vendorSetupForm: FormGroup;
  State:any;
  Country:any;
  @Output() closeDialog = new EventEmitter();
  @Input() selectedData?: any;
  @Input() isEdit?: any;
  submitted: boolean;
  constructor(private fb: FormBuilder, private toastr: ToastrService,private crudService: CrudOperationService) { }

  ngOnInit() {
    this.vendorSetupForm = this.fb.group({
      supplierId: ['', Validators.required],
      vin: ['', Validators.required],
      vendorAlias: [''],
      name: ['', Validators.required],
      supplierAddress: ['', Validators.required],
      zipcode: ['', [Validators.required]],
      state: ['', Validators.required],
      country: ['',],
      phoneNumber: ['',],
      email: ['', Validators.email],
      fax: ['',]
    });
    this.submitted = false;
    this.State = ["state1","state2","state3"];
    this.Country = ["USA"];
    this.vendorSetupForm.controls['country'].patchValue(this.Country);
    this.vendorSetupForm.controls['supplierId'].patchValue(1);
    this.vendorSetupForm.controls['supplierId'].disable();
    console.log(this.selectedData);
    if (this.selectedData !== undefined && this.selectedData.length !== 0) {
      this.vendorSetupForm.reset();
      this.vendorSetupForm.patchValue({
        supplierId: this.selectedData.SupplierId,
        vin: this.selectedData.Vin,
        vendorAlias: this.selectedData.VendorAlias,
        name: this.selectedData.Name,
        supplierAddress: this.selectedData.SupplierAddress,
        zipcode: this.selectedData.Zipcode,
        state: this.selectedData.State,
        country: this.selectedData.Country,
        phoneNumber: this.selectedData.PhoneNumber,
        email: this.selectedData.Email,
        fax: this.selectedData.Fax        
      });
    }
  }

  get f(){
    return this.vendorSetupForm.controls;
  }
  
  submit() {  
    this.submitted = true;
    if(this.vendorSetupForm.invalid){
      return;
    }  
    const sourceObj = [];
    const formObj = {
      supplierId: this.vendorSetupForm.value.supplierId,
      vin: this.vendorSetupForm.value.vin,
      vendorAlias: this.vendorSetupForm.value.vendorAlias,
      name: this.vendorSetupForm.value.name,
      supplierAddress: this.vendorSetupForm.value.supplierAddress,
      zipcode: this.vendorSetupForm.value.zipcode,
      state: this.vendorSetupForm.value.state,
      country: this.vendorSetupForm.value.country,
      phoneNumber: this.vendorSetupForm.value.phoneNumber,
      email: this.vendorSetupForm.value.email,
      fax: this.vendorSetupForm.value.fax
    };
    sourceObj.push(formObj);
    this.crudService.vendorsetupdetails.push(formObj);
        if (this.isEdit === true) {
          this.toastr.success('Record Updated Successfully!!', 'Success!');
        } else {
          this.toastr.success('Record Saved Successfully!!', 'Success!');
        }
  }
  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }
}
