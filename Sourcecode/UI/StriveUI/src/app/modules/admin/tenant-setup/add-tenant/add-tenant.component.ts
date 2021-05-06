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
  isSelectAll: boolean;
  stateList = [];
  cityList = [];
  stateId: any;
  cityId: any;
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
      email: ['', [Validators.required, Validators.email]],
      mobile: [''],
      phone: [''],
      stateId: [''],
      cityId: ['']
    });
    this.companyform = this.fb.group({
      company: ['', Validators.required],
      dateOfSubscription: ['', Validators.required],
      noOfLocation: [''],
      paymentDate: ['', Validators.required],
      deactivation: ['', Validators.required]
    });
    this.getStateList();
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
    const isAllModuleSelect = this.moduleList.filter(item => !item.IsChecked);
    if (isAllModuleSelect.length === 0) {
      this.isSelectAll = true;
    } else {
      this.isSelectAll = false;
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
          this.isSelectAll = true;
          this.moduleList.forEach(item => {
            item.IsChecked = true;
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
      firstName: detail.firstName,
      lastName: detail.lastName,
      address: detail.address,
      email: detail.clientEmail,
      mobile: detail.mobileNumber,
      phone: detail.phoneNumber,
      zipcode: detail.zipCode,
      noOfLocation: +detail.maxLocation
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
    const isAllModuleSelect = this.tenantModule.filter(item => !item.IsChecked);
    if (isAllModuleSelect.length === 0) {
      this.isSelectAll = true;
    } else {
      this.isSelectAll = false;
    }
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


    const moduleObj = [];
    if (this.isEdit) {
      this.newModuleChanges.forEach(item => {
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
      lastName: this.personalform.value.lastName,
      address: this.personalform.value.address,
      state: this.personalform.value.stateId.StateId ? this.personalform.value.stateId.StateId : 0,
      city: this.personalform.value.cityId.CityId ? this.personalform.value.cityId.CityId : 0,
      zipCode: this.personalform.value.zipcode,
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
      this.spinner.show();
      this.tenantSetupService.updateTenant(finalObj).subscribe(res => {
        this.spinner.hide();
        if (res.status === 'Success') {
          this.toastr.success(MessageConfig.Admin.SystemSetup.TenantSetup.Update, 'Success!');
          this.navigate();
        }
      }, (err) => {
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
    } else {
      this.spinner.show();
      this.tenantSetupService.addTenant(finalObj).subscribe(res => {
        this.spinner.hide();
        if (res.status === 'Success') {
          this.toastr.success(MessageConfig.Admin.SystemSetup.TenantSetup.Add, 'Success!');
          this.navigate();
        }
      }, (err) => {
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
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

  getStateList() {
    this.tenantSetupService.getStateList().subscribe( res => {
      if (res.status === 'Success') {
        const states = JSON.parse(res.resultData);
        this.stateList = states.Allstate;
      }
    });
  }

  stateSelection(event) {
    console.log(event);
    const stateId = event.value.StateId;
    this.tenantSetupService.getCityByStateId(stateId).subscribe( res => {
      if (res.status === 'Success') {
        const cites = JSON.parse(res.resultData);
        this.cityList = cites.cities;
        console.log(cites);
      }
    });
  }



}
