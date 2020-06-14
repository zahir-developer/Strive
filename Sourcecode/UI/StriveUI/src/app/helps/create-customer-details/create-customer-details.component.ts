
import { Component, OnInit, Input } from '@angular/core';
// import { SharedService } from './../../services/shared.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { ToastrService } from 'ngx-toastr';
import { CrudOperationService } from 'src/app/shared/services/crud-operation.service';
@Component({
  selector: 'app-create-customer-details',
  templateUrl: './create-customer-details.component.html',
  styleUrls: ['./create-customer-details.component.css']
})
export class CreateCustomerDetailsComponent implements OnInit {
  createCustomerForm: FormGroup;
  genderDropdown = [];
  @Input() selectedData?: any;
  submitted = false;
  constructor(
    private formBuilder: FormBuilder,
    private customerService: CrudOperationService,
    private toastr: ToastrService,
    // private sharedService: SharedService
  ) {
    // sharedService.setViewCustomerId(0);
    this.createCustomerForm = formBuilder.group({
      name: ['', Validators.required],
      phone: ['', [Validators.pattern('^[0-9]*')]],
      gender: ['', Validators.required],
      email: ['', Validators.email],
      age: ['', [Validators.min(0), Validators.max(100), Validators.pattern('^[0-9]*')]],
      address: ['', Validators.required],
      dob: ['', Validators.required]
    });
  }

  ngOnInit() {
    this.genderDropdown = [{ id: 'Female', value: 'Female' },
    { id: 'Male', value: 'Male' },
    { id: 'Other', value: 'Other' }];
    if (this.selectedData !== undefined) {
      this.createCustomerForm.reset();
      this.createCustomerForm.setValue({ name: this.selectedData.name });
    }
  }
  get f() { return this.createCustomerForm.controls; }
  createCustomer() {
    this.submitted = true;
    if (this.createCustomerForm.valid) {
      const formObj = {
        id: this.guid(),
        name: this.createCustomerForm.value.name,
        age: this.createCustomerForm.value.age,
        dob: this.createCustomerForm.value.dob,
        address: this.createCustomerForm.value.address,
        email: this.createCustomerForm.value.email,
        phone: this.createCustomerForm.value.phone,
        gender: this.createCustomerForm.value.gender

      };
      this.customerService.customerdetails.push(formObj);
      console.log(this.customerService.getCustomerDetails());
      this.toastr.success('This is a vaild form.', 'Success!');
    } else {
      this.toastr.success('This is not a valid form.', 'Alert!');
    }
  }
  guid() {
    function s4() {
      return Math.floor((1 + Math.random()) * 0x10000)
        .toString(16)
        .substring(1);
    }
    return s4() + s4() + '-' + s4() + '-' + s4() + '-' + s4() + '-' + s4() + s4() + s4();
  }
  reset() {
    this.createCustomerForm.reset();
  }
}
