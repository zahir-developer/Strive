import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import * as moment from 'moment';
import { ServiceSetupService } from 'src/app/shared/services/data-service/service-setup.service';
import { GetCodeService } from 'src/app/shared/services/data-service/getcode.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { CodeValueService } from 'src/app/shared/common-service/code-value.service';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';
import { IDropdownSettings } from 'ng-multiselect-dropdown';

@Component({
  selector: 'app-service-create-edit',
  templateUrl: './service-create-edit.component.html'
})
export class ServiceCreateEditComponent implements OnInit {
  serviceSetupForm: FormGroup;
  @Output() closeDialog = new EventEmitter();
  @Input() selectedData?: any;
  @Input() isEdit?: any;
  serviceType: any;
  selectedService: any;
  CommissionType: any;
  isCommisstionShow = true;
  commissionTypeLabel: any;
  Status: any;
  parent: any;
  isChecked: boolean;
  today: Date = new Date();
  submitted: boolean;
  ctypeLabel: any;
  isUpcharge = false;
  isAdditional = false;
  isDetails: boolean;
  costErrMsg: boolean = false;
  discountType: any;
  priceLabel: string = 'Price';
  isDiscounts: boolean;
  discountServiceType: any;
  employeeId: number;
  priceErrMsg: boolean;
  serviceEnum: any;
  additional: any;
  location: any;
  dropdownSettings: IDropdownSettings = {};
  locationId: any = [];
  serviceSetupList: any = [];
  codeCategory: any;
  isCeramic: boolean = false;
  isWash: boolean;
  washUpcharge: boolean;
  detailUpcharge: boolean;
  CategoryName: any;
  Category: any[];



  constructor(
    private serviceSetup: ServiceSetupService,
    private getCode: GetCodeService,
    private fb: FormBuilder,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private codeValueService: CodeValueService,
    private employeeService: EmployeeService
  ) { }

  ngOnInit() {
    this.employeeId = +localStorage.getItem('empId');
    this.Status = [{ id: 0, Value: "Active" }, { id: 1, Value: "InActive" }];
    this.ctypeLabel = 'none';
    this.isChecked = false;
    this.submitted = false;
    this.getLocation();
    this.formInitialize();
    this.getCommissionType();
    this.getCategory();
  
   
         if (this.isEdit === true) {
           
      this.serviceSetupForm.controls.upcharge.disable();
    }
    else{
      this.serviceSetupForm.patchValue({
        serviceCategory: this.codeCategory[0]
      }) 
    
      this.serviceSetupForm.controls.upcharge.disable();
  
    }
  }

  formInitialize() {
    this.serviceSetupForm = this.fb.group({
      serviceType: ['', Validators.required],
      name: ['', Validators.required],
      description: [''],
      price: ['', Validators.required],
      cost: [''],
      commission: ['',],
      commissionType: ['',],
      discountType: ['',],
      discountServiceType: ['',],
      upcharge: [''],
      parentName: ['',],
      status: ['',],
      fee: ['',],
      suggested: [''],
      serviceCategory: [''],
      isCeramic: [''],
      location: [[], Validators.required]
    });
    this.serviceSetupForm.patchValue({ status: 0 });
  }

  get f() {
    return this.serviceSetupForm.controls;
  }
  locationDropDown() {
    this.location = this.location.map(item => {
      return {
        id: item.LocationId,
        name: item.LocationName
      };
    });
    this.dropDownSetting();
  }
  getLocation() {
    this.employeeService.getLocation().subscribe(res => {
      if (res.status === 'Success') {
        const location = JSON.parse(res.resultData);
        this.location = location.Location;
        this.location = this.location.map(item => {
          return {
            item_id: item.LocationId,
            item_text: item.LocationName
          };
        });
        this.dropDownSetting();
      }
    });
  }

  dropDownSetting() {
    this.dropdownSettings = {
      singleSelection: ApplicationConfig.dropdownSettings.singleSelection,
      defaultOpen: ApplicationConfig.dropdownSettings.defaultOpen,
      idField: ApplicationConfig.dropdownSettings.idField,
      textField: ApplicationConfig.dropdownSettings.textField,
      itemsShowLimit: ApplicationConfig.dropdownSettings.itemsShowLimit,
      enableCheckAll: ApplicationConfig.dropdownSettings.enableCheckAll,
      allowSearchFilter: ApplicationConfig.dropdownSettings.allowSearchFilter
    };
  }

  // Get Service By Id
  getServiceById() {
    this.spinner.show();
    this.serviceSetup.getServiceSetupById(this.selectedData.ServiceId).subscribe(data => {
      if (data.status === "Success") {
        this.spinner.hide();
        const sType = JSON.parse(data.resultData);
        this.selectedService = sType.ServiceSetup;
        if (this.selectedService.Upcharges === '') {
          this.serviceSetupForm.get('upcharge').clearValidators();
          this.serviceSetupForm.get('upcharge').updateValueAndValidity();
        }
        let name = '';
        this.location.forEach(item => {
          if (+item.item_id === +this.selectedService.LocationId) {
            name = item.item_text;
          }
        });
        const locObj = {
          item_id: this.selectedService.LocationId,
          item_text: name
        };
        const selectedLocation = [];
        this.Category = []
        this.CategoryName  = ''
        selectedLocation.push(locObj);
        this.dropDownSetting();
        this.codeCategory.forEach(element => {
          if(this.selectedService?.ServiceCategory == element.CodeId){
        this.Category = element;
          }
        });
        this.serviceSetupForm.patchValue({
          
          serviceType: this.selectedService?.ServiceTypeId,
          name: this.selectedService?.ServiceName,
          description: this.selectedService?.Description,
          cost: this.selectedService?.Cost,
          price: this.selectedService?.Price,
          commission: this.selectedService?.Commision,
          serviceCategory:  this.Category,
          isCeramic: this.selectedService?.IsCeramic,
          commissionType: this.selectedService?.CommissionTypeId,
          fee: this.selectedService?.CommissionCost,
          discountType: this.selectedService?.DiscountType,
          upcharge: this.selectedService?.Upcharges,
          discountServiceType: this.selectedService?.DiscountServiceType,
          parentName: this.selectedService?.ParentServiceId,
          status: this.selectedService.IsActive ? 0 : 1,
          location: selectedLocation
        });
        this.change(this.selectedService.Commision);
        this.checkService(this.selectedService.ServiceTypeId);
      } else {
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
      if (this.selectedService?.ServiceType === ApplicationConfig.Enum.ServiceType.DetailUpcharge ||
        this.selectedService?.ServiceType === ApplicationConfig.Enum.ServiceType.DetailCeramicUpcharge ||
        this.selectedService?.ServiceType === ApplicationConfig.Enum.ServiceType.WashUpcharge) {
        this.isUpcharge = true;
      } else {
        this.isUpcharge = false;
        this.serviceSetupForm.get('upcharge').clearValidators();
        this.serviceSetupForm.get('upcharge').updateValueAndValidity();
      }
      if (this.selectedService?.ServiceType === ApplicationConfig.Enum.ServiceType.AdditonalServices) {
        this.isAdditional = true;
      } else {
        this.isAdditional = false;
      }
    }, (err) => {
      this.spinner.hide();
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  // Get CommisionType
  getCommissionType() {
    this.getCode.getCodeByCategory(ApplicationConfig.Category.CommisionType).subscribe(data => {
      if (data.status === "Success") {
        const cType = JSON.parse(data.resultData);
        this.CommissionType = cType.Codes;
        this.discountType = cType.Codes;
        this.getAllServiceType();

      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  // Get ParentType
  getParentType() {
    this.serviceEnum = this.serviceType;
    this.parent = this.serviceEnum.filter(i => i.CodeValue === ApplicationConfig.Enum.ServiceType.AdditonalServices)[0]?.CodeId;
    this.getAllServices();
    // const serviceTypeValue = this.codeValueService.getCodeValueByType(ApplicationConfig.CodeValueByType.serviceType);
    // if (serviceTypeValue.length > 0) {
    //   this.serviceEnum = serviceTypeValue;
    //   const serviceDetails = this.serviceEnum;
    //   if (serviceDetails !== null) {

    //   }

    // } else {
    //   this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    // }
  }
  getAllServices() {
    const locID = +localStorage.getItem('empLocationId');
    this.serviceSetup.getAllServiceDetail(locID).subscribe(res => {
      if (res.status === 'Success') {
        const serviceDetails = JSON.parse(res.resultData);
        if (serviceDetails.AllServiceDetail !== null) {
          this.additional = serviceDetails.AllServiceDetail.filter(item =>
            Number(item.ServiceTypeId) === this.parent);
        }
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });

  }

  getCtype(data) {
    const label = this.CommissionType.filter(item => item.CodeId === Number(data));
    if (label.length !== 0) {
      this.ctypeLabel = label[0].CodeValue;
      this.serviceSetupForm.get('fee').setValidators([Validators.required]);
    }
  }

  // Get ServiceType
  getAllServiceType() {
    const check = this.codeValueService.getCodeValueByType(ApplicationConfig.CodeValueByType.serviceType);
    this.getCode.getCodeByCategory(ApplicationConfig.Category.serviceType).subscribe(data => {
      if (data.status === "Success") {
        const cType = JSON.parse(data.resultData);
        this.serviceType = cType.Codes;
        this.discountServiceType = cType.Codes.filter(item => item.CodeValue !== ApplicationConfig.Enum.ServiceType.ServiceDiscounts);
        this.getParentType();
        if (this.isEdit === true) {
          this.serviceSetupForm.reset();
          this.getServiceById();
        }
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  checkService(typeID) {
    const serviceType = this.serviceType.filter(item => +item.CodeId === +typeID);
    if (serviceType.length > 0) {
      const type = serviceType[0].CodeValue;
      if (type === ApplicationConfig.Enum.ServiceType.DetailUpcharge ||
        type === ApplicationConfig.Enum.ServiceType.DetailCeramicUpcharge || type === ApplicationConfig.Enum.ServiceType.WashUpcharge) {
        this.isUpcharge = true;
        this.categoryName()

      } else {
        this.isUpcharge = false;
        this.serviceSetupForm.get('upcharge').clearValidators();
        this.serviceSetupForm.get('upcharge').updateValueAndValidity();
      }
      if (type === ApplicationConfig.Enum.ServiceType.AdditonalServices) {
        this.isAdditional = true;
      } else {
        this.isAdditional = false;
      }
      if (type === ApplicationConfig.Enum.ServiceType.DetailPackage) {
        this.isDetails = true;
      } else {
        this.isDetails = false;
      }
      if (type === ApplicationConfig.Enum.ServiceType.WashUpcharge) {
        this.washUpcharge = true;
        
      } else {
        this.washUpcharge = false;
      }
      if (type === ApplicationConfig.Enum.ServiceType.DetailUpcharge) {
        this.detailUpcharge = true;
      } else {
        this.detailUpcharge = false;
      }
      if (type === ApplicationConfig.Enum.ServiceType.ServiceDiscounts) {
        this.isDiscounts = true;
        this.serviceSetupForm.get('discountType').setValidators([Validators.required]);
        this.serviceSetupForm.get('discountServiceType').setValidators([Validators.required]);

      } else {
        this.isDiscounts = false;
        this.serviceSetupForm.get('discountServiceType').clearValidators();
        this.serviceSetupForm.get('discountServiceType').updateValueAndValidity();

        this.serviceSetupForm.get('discountType').clearValidators()
        this.serviceSetupForm.get('discountType').updateValueAndValidity();

        
      }
      if (type === ApplicationConfig.Enum.ServiceType.WashPackage) {
        this.isWash = true;
        this.isCommisstionShow = false;
      } else {
        this.isWash = false;

        this.isCommisstionShow = true;
      }
    }

  }

  change(data) {
    this.serviceSetupForm.value.commission = data;
    if (data === true) {
      this.isChecked = true;
      this.serviceSetupForm.get('commissionType').setValidators([Validators.required]);
      if (this.isEdit === true) {
        this.getCtype(this.selectedService?.CommissionTypeId);
      }
    } else {
      this.isChecked = false;
      this.ctypeLabel = 'none';
      this.serviceSetupForm.get('commissionType').clearValidators();
      this.serviceSetupForm.get('fee').clearValidators();
      this.serviceSetupForm.get('fee').reset();
      this.serviceSetupForm.get('commissionType').reset();
      this.serviceSetupForm.get('fee').reset();
    }
  }
  changeCeramic(data) {
    this.serviceSetupForm.value.commission = data;
    if (data === true) {
      this.isCeramic = true;
      
    } else {
      this.isCeramic = false;
    
    }
  }
categoryName(){
  const upcharge =  this.serviceSetupForm.controls['serviceCategory'].value.CodeValue 
   
 const price = this.serviceSetupForm.controls['price'].value !== "" ? this.serviceSetupForm.controls['price'].value : '0'
   this.serviceSetupForm.patchValue({
    upcharge:  upcharge + ' -  $' + price
   })
}
  // Add/Update Service
  submit() {
    debugger;

    this.submitted = true;
    if (this.serviceSetupForm.invalid) {

      if (this.serviceSetupForm.value.price !== "") {
        if (Number(this.serviceSetupForm.value.price) <= 0) {
          this.priceErrMsg = true;
          return;
        } else {
          this.priceErrMsg = false;
        }
      }
      return;
    }
 
      this.serviceSetupForm.get('upcharge').enable();
     
    if (this.serviceSetupForm.value.location) {
      this.serviceSetupForm.value.location.map(item => {
        this.serviceSetupList.push({
            serviceType: this.serviceSetupForm.value.serviceType,
            serviceId: this.isEdit ? this.selectedService.ServiceId : 0,
            serviceName: this.serviceSetupForm.value.name,
            description: this.serviceSetupForm.value.description,
            cost: this.serviceSetupForm.value.cost,
            price: this.serviceSetupForm.value.price,
            commision: this.isChecked,
            commisionType: this.isChecked === true ? this.serviceSetupForm.value.commissionType : null,
            upcharges:this.serviceSetupForm.value.upcharge,
            parentServiceId: this.serviceSetupForm.value.parentName === '' ? 0 : this.serviceSetupForm.value.parentName,
            isActive: this.serviceSetupForm.value.status == 0 ? true : false,
            locationId: item.item_id,
            commissionCost: this.isChecked === true ? +this.serviceSetupForm.value.fee : null,
            serviceCategory : this.serviceSetupForm.value.serviceCategory.CodeId,
            isCeramic : this.isCeramic,
            isDeleted: false,
            createdBy: this.employeeId,
            createdDate: this.isEdit ? this.selectedService.CreatedDate : new Date(),
            updatedBy: this.employeeId,
            updatedDate: new Date(),
            discountServiceType: this.serviceSetupForm.value.discountServiceType,
            discountType: this.serviceSetupForm.value.discountType,
          });
      });
    }
    if (this.isEdit === true) {
      const formObj = this.serviceSetupList[0];
      this.spinner.show();
      this.serviceSetup.updateServiceSetup(formObj).subscribe(data => {
        if (data.status === 'Success') {
          this.spinner.hide();
          this.toastr.success(MessageConfig.Admin.SystemSetup.ServiceSetup.Update, 'Success!');
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        } else {
          this.spinner.hide();
          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
          this.serviceSetupForm.reset();
          this.submitted = false;
        }
      }, (err) => {
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      });
    } else {
      const formObj = {
        service: this.serviceSetupList
      };
      this.spinner.show();
      this.serviceSetup.addServiceSetup(formObj).subscribe(data => {
        if (data.status === 'Success') {
          this.spinner.hide();
          this.toastr.success(MessageConfig.Admin.SystemSetup.ServiceSetup.Add, 'Success!');
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        } else {
          this.spinner.hide();
          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
          this.serviceSetupForm.reset();
          this.submitted = false;
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

   // Get Category
   getCategory() {
    const sizeCodes = this.codeValueService.getCodeValueByType(ApplicationConfig.Category.ServiceCategory);
    if (sizeCodes.length > 0) {
      this.codeCategory = sizeCodes;
      console.log(this.codeCategory)
    }
  }
}
