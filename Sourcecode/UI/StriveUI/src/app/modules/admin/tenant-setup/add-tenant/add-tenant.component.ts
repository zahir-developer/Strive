import { EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { CityComponent } from 'src/app/shared/components/city/city.component';
import { StateDropdownComponent } from 'src/app/shared/components/state-dropdown/state-dropdown.component';
import { TenantSetupService } from 'src/app/shared/services/data-service/tenant-setup.service';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import * as moment from 'moment';
import { NgxSpinnerService } from 'ngx-spinner';

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
  @Output() closePopup = new EventEmitter();
  @Output() reloadGrid = new EventEmitter();
  @Input() isEdit?: any;
  @Input() tenantDetail?: any;
  @Input() tenantModule?: any;
  errorMessage: boolean;
  newModuleChanges = [];
  constructor(
    private fb: FormBuilder,
    private toastr: ToastrService,
    private tenantSetupService: TenantSetupService,
    private spinner: NgxSpinnerService
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


  get f() {
    return this.personalform.controls;
  }

  get g() {
    return this.companyform.controls;
  }

  selectAll(event) {
    if (this.isEdit) {
      if (event.target.checked) {

      }
    } else {
      if (event.target.checked) {
        this.moduleList.forEach(item => {
          item.IsChecked = true;
        });
      } else {
        this.moduleList.forEach(item => {
          item.IsChecked = false;
        });
      }
    }
  }

  selectModule(module) {
    if (this.isEdit) {
      const modules = this.moduleList.filter(item => item.ModuleId === module.ModuleId);
      if (modules.length > 0) {
        modules[0].IsChecked = modules[0].IsChecked ? false : true;
        this.newModuleChanges.push(modules[0]);
      } else {
        module.IsChecked = module.IsChecked ? false : true;
        this.newModuleChanges.push(modules[0]);
      }
    } else {
      module.IsChecked = module.IsChecked ? false : true;
    }
  }

  getModuleList() {
    this.spinner.show();
    this.tenantSetupService.getModuleList().subscribe(res => {
      this.spinner.hide();
      if (res.status === 'Success') {
        const modules = JSON.parse(res.resultData);
        console.log(modules, 'module');
        if (modules.AllModule !== null) {
          this.moduleList = modules.AllModule;
          this.moduleList.forEach(item => {
            item.IsChecked = false;
          });
        }
        if (this.isEdit) {
          this.setValue();
        }
      }
    }, (err) => {
      this.spinner.hide();
    });
  }

  setValue() {
    const detail = this.tenantDetail;
    this.personalform.patchValue({
      firstName: detail.clientName,
      address: '',
      email: detail.clientEmail,
      mobile: detail.mobileNumber
    });
    this.companyform.patchValue({  // moment(employeeInfo.HiredDate).toDate()
      company: detail.companyName,
      dateOfSubscription: detail.subscriptionDate ? moment(detail.subscriptionDate).toDate() : '',
      paymentDate: detail.paymentDate ? moment(detail.paymentDate).toDate() : '',
      deactivation: detail.expiryDate ? moment(detail.expiryDate).toDate() : ''
    });
    this.tenantModule.forEach(item => {
      if (item.isActive) {
        item.IsChecked = true;
      } else {
        item.IsChecked = false;
      }
    });
    const modules = [];
    this.tenantModule.forEach(item => {
      modules.push({
        ModuleId: item.moduleId,
        ModuleName: item.moduleName,
        IsActive: item.isActive,
        IsChecked: item.IsChecked
      });
    });
    this.moduleList = modules;
    // this.tenantModule.forEach( item => {
    //   this.moduleList.forEach( mod => {
    //     if (mod.ModuleId === item.moduleId && item.isActive) {
    //       mod.IsChecked = true;
    //     } else if (mod.ModuleId === item.moduleId && !item.isActive) {
    //       mod.IsChecked = false;
    //     }
    //   });
    // });
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

    if (this.errorMessage === true) {
      return;
    }

    const moduleObj = [];
    if (this.isEdit) {
      this.newModuleChanges.forEach( item => {
        if (item.IsChecked) {
          moduleObj.push({
            moduleId: item.ModuleId,
            moduleName: item.ModuleName,
            isActive: true
          });
        } else {
          moduleObj.push({
            moduleId: item.ModuleId,
            moduleName: item.ModuleName,
            isActive: false
          });
        }
      });
    } else {
      this.moduleList.forEach(item => {
        if (item.IsChecked) {
          moduleObj.push({
            moduleId: item.ModuleId,
            moduleName: item.ModuleName,
            isActive: true
          });
        }
      });
    }

    const module = {
      module: moduleObj
    };
    const tenantObj = {
      tenantId: this.isEdit ? this.tenantDetail.tenantId : 0,
      clientId: this.isEdit ? this.tenantDetail.clientId : 0,
      firstName: this.personalform.value.firstName,
      address: this.personalform.value.address,
      tenantEmail: this.personalform.value.email,
      phoneNumber: this.personalform.value.phone,
      mobileNumber: this.personalform.value.mobile,
      subscriptionId: 0,
      tenantName: this.companyform.value.company,
      passwordHash: '',
      expiryDate: moment(this.companyform.value.deactivation).format(),
      subscriptionDate: moment(this.companyform.value.dateOfSubscription).format(),
      paymentDate: moment(this.companyform.value.paymentDate).format(),
      locations: +this.companyform.value.noOfLocation
    };
    const finalObj = {
      tenantViewModel: tenantObj,
      tenantModuleViewModel: module
    };
    if (this.isEdit) {
      this.tenantSetupService.updateTenant(finalObj).subscribe(res =>  {
        if (res.status === 'Success') {
          this.navigate();
        }
      });
    } else {
      this.tenantSetupService.addTenant(finalObj).subscribe(res => {
        if (res.status === 'Success') {
          this.navigate();
        }
      });
    }
  }

  cancel() {
    this.closePopup.emit();
  }

  navigate() {
    this.reloadGrid.emit();
  }

  testMail(event) {
    if (!this.validateEmail(this.personalform.value.email)) {
      this.errorMessage = true;
    }
    else {
      this.errorMessage = false;
    }
  }

  validateEmail(email) {
    const re = /^((\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)\s*[;]{0,1}\s*)+$/;
    return re.test(String(email).toLowerCase());
  }



}
