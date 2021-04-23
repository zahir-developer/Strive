import { ViewChild } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { CityComponent } from 'src/app/shared/components/city/city.component';
import { StateDropdownComponent } from 'src/app/shared/components/state-dropdown/state-dropdown.component';
import { MessageConfig } from 'src/app/shared/services/messageConfig';

@Component({
  selector: 'app-add-tenant',
  templateUrl: './add-tenant.component.html',
  styleUrls: ['./add-tenant.component.css']
})
export class AddTenantComponent implements OnInit {
  @ViewChild(StateDropdownComponent) stateDropdownComponent: StateDropdownComponent;
  @ViewChild(CityComponent) cityComponent: CityComponent;
  State: any;
  city: any;
  personalform: FormGroup;
  companyform: FormGroup;
  moduleList = [];
  submitted: boolean;
  constructor(
    private fb: FormBuilder,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.submitted = false;
    this.personalform = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      address: ['', Validators.required],
      zipcode: [''],
      email: [''],
      mobile: [''],
      phone: ['']
    });
    this.companyform = this.fb.group({
      company: ['', Validators.required],
      dateOfSubscription: ['', Validators.required],
      noOfLocation: [''],
      paymentDate: ['', Validators.required],
      deactivation: ['', Validators.required]
    });
    this.getModuleList();
  }

  getSelectedStateId(event) {
    this.State = event;
    this.cityComponent.getCity(event);
  }

  selectCity(event) {
    this.city = event;
  }

  getModuleList() {
    this.moduleList = [
      {
        tenantModuleId: 0,
        moduleId: 1,
        isChecked: false,
        moduleName: 'Washes'
      },
      {
        tenantModuleId: 0,
        moduleId: 2,
        isChecked: false,
        moduleName: 'Details'
      },
      {
        tenantModuleId: 0,
        moduleId: 3,
        isChecked: false,
        moduleName: 'Checkout'
      },
      {
        tenantModuleId: 0,
        moduleId: 4,
        isChecked: false,
        moduleName: 'Sales'
      },
      {
        tenantModuleId: 0,
        moduleId: 5,
        isChecked: false,
        moduleName: 'Payroll'
      }
    ];
  }

  get f() {
    return this.personalform.controls;
  }

  get g() {
    return this.companyform.controls;
  }

  addTenant() {
    this.submitted = true;
    // this.stateDropdownComponent.submitted = true;
    // this.cityComponent.submitted = true;
    // if (this.stateDropdownComponent.stateValueSelection === false ) {
    //   return;
    // }
    // if (this.cityComponent.selectValueCity === false ) {
    //   return;
    // }

    if (this.personalform.invalid || this.companyform.invalid) {
      this.toastr.warning(MessageConfig.Mandatory, 'Warning!');
      return;
    }
  }



}
