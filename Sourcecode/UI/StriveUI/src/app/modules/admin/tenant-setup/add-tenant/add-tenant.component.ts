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
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { ClientService } from 'src/app/shared/services/data-service/client.service';
import { NgbModalOptions, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { RecurringPaymentComponent } from 'src/app/shared/components/recurring-payment/recurring-payment.component';

@Component({
  selector: 'app-add-tenant',
  templateUrl: './add-tenant.component.html'
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
  isMobileAppSelectAll: boolean;
  stateList = [];
  cityList = [];
  stateId: any;
  cityId: any;
  adminModuleList = [];
  reportModuleList = [];
  isEmailAvailable: boolean;
  moduleScreenList = [];
  mobileAppList = [];
  constructor(
    private fb: FormBuilder,
    private toastr: ToastrService,
    private tenantSetupService: TenantSetupService,
    private spinner: NgxSpinnerService,
    private client: ClientService,
    private modalService: NgbModal
  ) { }

  ngOnInit(): void {
    this.submitted = false;
    this.isEmailAvailable = false;
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
      deactivation: ['']
    });
    this.getStateList();
    // this.getModuleList();
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
    if (event.target.checked) {
      this.moduleList.forEach(item => {
        item.IsChecked = true;
      });
      this.adminModuleList.forEach(item => {
        item.IsChecked = true;
      });
      this.reportModuleList.forEach(item => {
        item.IsChecked = true;
      });
    } else {
      this.moduleList.forEach(item => {
        item.IsChecked = false;
      });
      this.adminModuleList.forEach(item => {
        item.IsChecked = false;
      });
      this.reportModuleList.forEach(item => {
        item.IsChecked = false;
      });
    }
  }

  selectAllMobileApp(event) {
    if (event.target.checked) {
      this.mobileAppList.forEach(item => {
        item.IsChecked = true;
      });
    } else {
      this.mobileAppList.forEach(item => {
        item.IsChecked = false;
      });
    }
  }

  selectModule(module) {
    if (this.isEdit) {
      module.IsChecked = module.IsChecked ? false : true;
    } else {
      module.IsChecked = module.IsChecked ? false : true;
    }
    if (module.IsChecked) {
      this.moduleScreenList.forEach(item => {
        if (item.ModuleId === module.ModuleId) {
          item.IsChecked = true;
        }
      });
    } else {
      this.moduleScreenList.forEach(item => {
        if (item.ModuleId === module.ModuleId) {
          item.IsChecked = false;
        }
      });
    }
    const isAllModuleSelect = this.moduleList.filter(item => !item.IsChecked);
    if (isAllModuleSelect.length === 0) {
      this.isSelectAll = true;
    } else {
      this.isSelectAll = false;
    }
  }

  selectMobileApp(app) {
    app.IsChecked = app.IsChecked ? false : true;
    const isAllModuleSelect = this.mobileAppList.filter(item => !item.IsChecked);
    if (isAllModuleSelect.length === 0) {
      this.isMobileAppSelectAll = true;
    } else {
      this.isMobileAppSelectAll = false;
    }
  }

  selectModuleScreen(module) {
    module.IsChecked = module.IsChecked ? false : true;
  }

  getModuleList() {
    this.tenantSetupService.getModuleList().subscribe(res => {
      if (res.status === 'Success') {
        const modules = JSON.parse(res.resultData);
        console.log(modules, 'module');
        if (modules.AllModule !== null) {
          this.moduleList = modules.AllModule.Module;
          this.isSelectAll = true;
          this.moduleList.forEach(item => {
            item.IsChecked = true;
          });
          const modulesScreen = modules.AllModule.ModuleScreen;
          this.moduleScreenList = modulesScreen;

          console.log(modulesScreen);
          this.moduleScreenList.forEach(item => {
            item.IsChecked = true;
          });
          this.mobileAppList = modules.AllModule.MobileApp;
          this.isMobileAppSelectAll = true;
          this.mobileAppList.forEach(item => {
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
      zipcode: detail.zipCode
    });
    this.personalform.controls.email.disable();
    this.companyform.patchValue({  // moment(employeeInfo.HiredDate).toDate()
      company: detail.companyName,
      noOfLocation: +detail.maxLocation,
      dateOfSubscription: detail.subscriptionDate ? moment(detail.subscriptionDate).toDate() : '',
      paymentDate: detail.paymentDate ? moment(detail.paymentDate).toDate() : '',
      deactivation: detail.expiryDate ? moment(detail.expiryDate).toDate() : ''
    });

    const selectedState = this.stateList.filter(item => item.StateId === detail.state);
    if (selectedState.length > 0) {
      this.personalform.patchValue({
        stateId: selectedState[0]
      });
      this.selectedCity(selectedState[0]);
    }
    this.tenantModule.module.forEach(item => {
      if (item.isActive) {
        item.IsChecked = true;
      } else {
        item.IsChecked = false;
      }
    });
    const modules = [];
    this.tenantModule.module.forEach(item => {
      modules.push({
        ModuleId: item.moduleId,
        ModuleName: item.moduleName,
        IsActive: item.isActive,
        IsChecked: item.IsChecked,
        Description: item.description
      });
    });
    const isAllModuleSelect = this.tenantModule.module.filter(item => !item.IsChecked);
    if (isAllModuleSelect.length === 0) {
      this.isSelectAll = true;
    } else {
      this.isSelectAll = false;
    }
    this.moduleList = modules;
    this.tenantModule.moduleScreen.forEach(item => {
      if (item.isActive) {
        item.IsChecked = true;
      } else {
        item.IsChecked = false;
      }
    });
    const moduleScreen = [];
    this.tenantModule.moduleScreen.forEach(item => {
      moduleScreen.push({
        IsActive: item.isActive,
        ModuleId: item.moduleId,
        ModuleScreenId: item.moduleScreenId,
        ViewName: item.viewName,
        IsChecked: item.IsChecked,
        Description: item.description
      });
    });
    this.moduleScreenList = moduleScreen;
    const mobileApp = [];
    this.tenantModule.mobileApp.forEach(item => {
      if (item.isActive) {
        item.IsChecked = true;
      } else {
        item.IsChecked = false;
      }
    });
    this.tenantModule.mobileApp.forEach(item => {
      mobileApp.push({
        Description: item.description,
        IsActive: item.isActive,
        IsChecked: item.IsChecked,
        MobileAppId: item.mobileAppId,
        MobileAppName: item.mobileAppName
      });
    });
    const isAllMobileAppSelect = this.tenantModule.mobileApp.filter(item => !item.IsChecked);
    if (isAllMobileAppSelect.length === 0) {
      this.isMobileAppSelectAll = true;
    } else {
      this.isMobileAppSelectAll = false;
    }
    this.mobileAppList = mobileApp;
    // const adminScreen = [];
    // const reportScreen = [];
    // this.tenantModule.moduleScreen.forEach(item => {
    //   if (item.moduleId === adminId) {
    //     adminScreen.push({
    //       IsActive: item.isActive,
    //       ModuleId: item.moduleId,
    //       ModuleScreenId: item.moduleScreenId,
    //       ViewName: item.viewName,
    //       IsChecked: item.IsChecked
    //     });
    //   }
    //   if (item.moduleId === reportId) {
    //     reportScreen.push({
    //       IsActive: item.isActive,
    //       ModuleId: item.moduleId,
    //       ModuleScreenId: item.moduleScreenId,
    //       ViewName: item.viewName,
    //       IsChecked: item.IsChecked
    //     });
    //   }
    // });
    // this.adminModuleList = adminScreen;
    // this.reportModuleList = reportScreen;
  }

  selectedCity(event) {
    const stateId = event.StateId;
    this.tenantSetupService.getCityByStateId(stateId).subscribe(res => {
      if (res.status === 'Success') {
        const cites = JSON.parse(res.resultData);
        this.cityList = cites.cities;
        this.cityList = this.cityList.map(item => {
          return {
            CityId: item.CityId,
            CityName: item.CityName
          };
        });
        const selectedState = this.cityList.filter(item => item.CityId === this.tenantDetail.city);
        if (selectedState.length > 0) {
          const city: any = {
            CityId: selectedState[0].CityId,
            CityName: selectedState[0].CityName
          };
          this.personalform.patchValue({
            cityId: city
          });
        }
      }
    });
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
    // if (this.isEdit) {  // 
    // this.newModuleChanges.forEach(item => {
    //   if (item.IsChecked) {
    //     moduleObj.push({
    //       moduleId: item.ModuleId,
    //       moduleName: item.ModuleName,
    //       isActive: true
    //     });
    //   } else {
    //     moduleObj.push({
    //       moduleId: item.ModuleId,
    //       moduleName: item.ModuleName,
    //       isActive: false
    //     });
    //   }
    // });
    // } else {
    const mobileApp = [];
    this.mobileAppList.forEach(item => {
      mobileApp.push({
        mobileAppId: item.MobileAppId,
        mobileAppName: item.MobileAppName,
        description: item.Description,
        isActive: item.IsChecked
      });
    });
    this.moduleList.forEach(item => {
      const moduleScreen = [];
      console.log(this.moduleScreenList);

      this.moduleScreenList.forEach(screen => {
        console.log(screen, 'screen');

        // if (adminscreen.IsChecked) {
        if (item.ModuleId === screen.ModuleId) {
          moduleScreen.push({
            moduleScreenId: screen.ModuleScreenId,
            moduleId: screen.ModuleId,
            viewName: screen.ViewName,
            isActive: screen.IsChecked,
            description: screen.Description
          });
        }
        // }
      });
      const obj: any = {};
      obj.moduleId = item.ModuleId;
      obj.moduleName = item.ModuleName;
      obj.isActive = item.IsChecked;
      obj.description = item.Description;
      moduleObj.push({
        module: obj,
        moduleScreen,
        mobileApp
      });
    });
    // }

    const module = {
      module: moduleObj
    };
    this.personalform.controls.email.enable();
    const tenantObj = {
      tenantId: this.isEdit ? this.tenantDetail.tenantId : 0,
      clientId: this.isEdit ? this.tenantDetail.clientId : 0,
      firstName: this.personalform.value.firstName,
      lastName: this.personalform.value.lastName,
      address: this.personalform.value.address,
      state: this.personalform.value.stateId.StateId ? this.personalform.value.stateId.StateId : null,
      city: this.personalform.value.cityId.CityId ? this.personalform.value.cityId.CityId : null,
      zipCode: this.personalform.value.zipcode,
      tenantEmail: this.personalform.value.email,
      phoneNumber: this.personalform.value.phone,
      mobileNumber: this.personalform.value.mobile,
      subscriptionId: 0,
      tenantName: this.companyform.value.company,
      passwordHash: '',
      expiryDate: this.companyform.value.deactivation !== '' ? moment(this.companyform.value.deactivation).format() : null,
      subscriptionDate: moment(this.companyform.value.dateOfSubscription).format(),
      paymentDate: moment(this.companyform.value.paymentDate).format(),
      locations: +this.companyform.value.noOfLocation,
      isActive: true
    };
    const finalObj = {
      tenantViewModel: tenantObj,
      module: moduleObj
    };
    console.log(finalObj, 'final');
    if (this.isEdit) {
      this.spinner.show();
      this.tenantSetupService.updateTenant(finalObj).subscribe(res => {
        this.spinner.hide();
        if (res.status === 'Success') {
          this.toastr.success(MessageConfig.Admin.Tenant.Update, 'Success!');
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
          this.toastr.success(MessageConfig.Admin.Tenant.Add, 'Success!');
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
    this.tenantSetupService.getStateList().subscribe(res => {
      if (res.status === 'Success') {
        const states = JSON.parse(res.resultData);
        this.stateList = states.Allstate;
        this.getModuleList();
      }
    });
  }

  stateSelection(event) {
    console.log(event);
    const stateId = event.value.StateId;
    this.tenantSetupService.getCityByStateId(stateId).subscribe(res => {
      if (res.status === 'Success') {
        const cites = JSON.parse(res.resultData);
        this.cityList = cites.cities;
        console.log(cites);
        this.cityList = this.cityList.map(item => {
          return {
            CityId: item.CityId,
            CityName: item.CityName
          };
        });
      }
    });
  }

  clientEmailExist() {
    if (this.personalform.controls.email.errors !== null) {
      return;
    }
    this.client.ValidateTenantEmail(this.personalform.controls.email.value).subscribe(res => {
      if (res.status === 'Success') {
        const sameEmail = JSON.parse(res.resultData);
        if (sameEmail.EmailIdExist === true) {
          this.isEmailAvailable = true;
          this.toastr.warning(MessageConfig.Admin.Tenant.EmailAlreadyExists, 'Warning!');
        } else {
          this.isEmailAvailable = false;
          this.toastr.info(MessageConfig.Admin.Tenant.EmailAvailable, 'Info!');

        }
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  creditProcess() {
    const ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false,
      size: 'lg'
    };
    const modalRef = this.modalService.open(RecurringPaymentComponent, ngbModalOptions);
    modalRef.componentInstance.tenantName = this.personalform.value.firstName + " " + this.personalform.value.lastName;
   
  }



}
