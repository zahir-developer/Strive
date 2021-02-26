import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import * as moment from 'moment';
import { ServiceSetupService } from 'src/app/shared/services/data-service/service-setup.service';
import { GetCodeService } from 'src/app/shared/services/data-service/getcode.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { CodeValueService } from 'src/app/shared/common-service/code-value.service';

@Component({
  selector: 'app-service-create-edit',
  templateUrl: './service-create-edit.component.html',
  styleUrls: ['./service-create-edit.component.css']
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



  constructor(
    private serviceSetup: ServiceSetupService,
    private getCode: GetCodeService,
    private fb: FormBuilder,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private codeValueService: CodeValueService
    ) { }

  ngOnInit() {
    this.employeeId = +localStorage.getItem('empId');
    this.Status = [{ id: 0, Value: "Active" }, { id: 1, Value: "Inactive" }];
    this.formInitialize();
    this.ctypeLabel = 'none';
    this.getCommissionType();
    this.isChecked = false;
    this.submitted = false;
  }

  formInitialize() {
    this.serviceSetupForm = this.fb.group({
      serviceType: ['', Validators.required],
      name: ['', Validators.required],
      description: [''],
      price:  ['', Validators.required],
     cost: ['', Validators.required],
      commission: ['',],
      commissionType: ['',],
      discountType: ['',],
      discountServiceType: ['',],
      upcharge: ['', Validators.required],
      parentName: ['',],
      status: ['',],
      fee: ['',],
      suggested: ['']
    });
    this.serviceSetupForm.patchValue({ status: 0 });
  }

  get f() {
    return this.serviceSetupForm.controls;
  }

  // Get Service By Id
  getServiceById() {
    this.serviceSetup.getServiceSetupById(this.selectedData.ServiceId).subscribe(data => {
      if (data.status === "Success") {
        const sType = JSON.parse(data.resultData);
        this.selectedService = sType.ServiceSetup;
        if (this.selectedService.Upcharges === '') {
          this.serviceSetupForm.get('upcharge').clearValidators();
          this.serviceSetupForm.get('upcharge').updateValueAndValidity();
        }
        this.serviceSetupForm.patchValue({
          serviceType: this.selectedService?.ServiceTypeId,
          name: this.selectedService?.ServiceName,
          description: this.selectedService?.Description,
          cost: this.selectedService?.Cost,
          price: this.selectedService?.Price,

          commission: this.selectedService?.Commision,
          commissionType: this.selectedService?.CommissionTypeId,
          fee: this.selectedService?.CommissionCost,
          discountType: this.selectedService?.DiscountType,
          upcharge: this.selectedService?.Upcharges,
          discountServiceType: this.selectedService?.DiscountServiceType,
          parentName: this.selectedService?.ParentServiceId,
          status: this.selectedService.IsActive ? 0 : 1
        });
        this.change(this.selectedService.Commision);
        this.checkService(this.selectedService.ServiceType);
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    });
  }

  // Get CommisionType
  getCommissionType() {
    this.getCode.getCodeByCategory("COMMISIONTYPE").subscribe(data => {
      if (data.status === "Success") {
        const cType = JSON.parse(data.resultData);
        this.CommissionType = cType.Codes;
        this.discountType = cType.Codes;
        this.getAllServiceType();
        // this.getParentType();
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    });
  }

  // Get ParentType
  getParentType() {
    const serviceObj = {
      locationId: null,
      pageNo: 1,
      pageSize: 10,
      query: null,
      sortOrder: null,
      sortBy: null,
      status: true
    };
    this.serviceSetup.getServiceSetup(serviceObj).subscribe(data => {
      if (data.status === 'Success') {
        const serviceDetails = JSON.parse(data.resultData);
        if (serviceDetails.ServiceSetup.getAllServiceViewModel !== null) {
          this.parent = serviceDetails.ServiceSetup.getAllServiceViewModel.filter(
            item => Number(item.ServiceTypeId) === 17 && item.IsActive === true);
          this.parent = this.parent.filter(item => Number(item.ParentServiceId) === 0);
          this.getAllServiceType();
        }
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
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
    const check = this.codeValueService.getCodeValueByType('ServiceType');
    console.log(check, 'cache-value');
    this.getCode.getCodeByCategory("SERVICETYPE").subscribe(data => {
      if (data.status === "Success") {
        const cType = JSON.parse(data.resultData);
        this.serviceType = cType.Codes;
        this.discountServiceType = cType.Codes;
        if (this.isEdit === true) {
          this.serviceSetupForm.reset();
          this.getServiceById();
        }
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    });
  }

  checkService(typeID) {
    const serviceType = this.serviceType.filter(item => +item.CodeId === +typeID);
    if (serviceType.length > 0) {
      const type = serviceType[0].CodeValue;
      if (type === 'Detail-Upcharge' || type === 'Detail-CeramicUpcharge' || type === 'Wash-Upcharge') {
        this.isUpcharge = true;
      } else {
        this.isUpcharge = false;
        this.serviceSetupForm.get('upcharge').clearValidators();
        this.serviceSetupForm.get('upcharge').updateValueAndValidity();
      }
      if (type === 'Additonal Services') {
        this.isAdditional = true;
      } else {
        this.isAdditional = false;
      }
      if (type === 'Details') {
        this.isDetails = true;
      } else {
        this.isDetails = false;
      }
      if (type === 'Service Discounts') {
        this.isDiscounts = true;
        this.serviceSetupForm.get('discountType').setValidators([Validators.required]);
        this.serviceSetupForm.get('discountServiceType').setValidators([Validators.required]);

      } else {
        this.serviceSetupForm.get('discountServiceType').clearValidators();
        this.serviceSetupForm.get('discountType').setValidators([Validators.required]);
        this.isDiscounts = false;
      }
      if (type === 'Wash Package') { 
        this.isCommisstionShow = false;
      } else {
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

  // Add/Update Service
  submit() {
    this.submitted = true;
    if (this.serviceSetupForm.invalid) {
      if (this.serviceSetupForm.value.cost !== "") {
        if (Number(this.serviceSetupForm.value.cost) <= 0) {
          this.costErrMsg = true;
          return;
        } else {
          this.costErrMsg = false;
        }
      }
      if(this.serviceSetupForm.value.price !== ""){        
        if(Number(this.serviceSetupForm.value.price) <= 0){
          this.priceErrMsg = true;
          return;
        }else{
          this.priceErrMsg = false;
        }
      }
      return;
    }
    const formObj = {
      serviceType: this.serviceSetupForm.value.serviceType,
      serviceId: this.isEdit ? this.selectedService.ServiceId : 0,
      serviceName: this.serviceSetupForm.value.name,
      description: this.serviceSetupForm.value.description,
      cost: this.serviceSetupForm.value.cost,
      price: this.serviceSetupForm.value.price,
       commision: this.isChecked,
      commisionType: this.isChecked == true ? this.serviceSetupForm.value.commissionType : null,
      upcharges: this.serviceSetupForm.value.upcharge,
      parentServiceId: this.serviceSetupForm.value.parentName === "" ? 0 : this.serviceSetupForm.value.parentName,
      isActive: this.serviceSetupForm.value.status == 0 ? true : false,
      locationId: +localStorage.getItem('empLocationId'),
      commissionCost: this.isChecked === true ? +this.serviceSetupForm.value.fee : null,
      isDeleted: false,
      createdBy: this.employeeId,
      createdDate: this.isEdit ? this.selectedService.CreatedDate : new Date(),
      updatedBy: this.employeeId,
      updatedDate: new Date(),
      discountServiceType: this.serviceSetupForm.value.discountServiceType,
      discountType: this.serviceSetupForm.value.discountType,
    };
    if (this.isEdit === true) {
      this.spinner.show();
      this.serviceSetup.updateServiceSetup(formObj).subscribe(data => {
        this.spinner.hide();
        if (data.status === 'Success') {
          this.toastr.success(MessageConfig.Admin.SystemSetup.ServiceSetup.Update, 'Success!');
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        } else {
          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
          this.serviceSetupForm.reset();
          this.submitted = false;
        }
      }, (err) => {
        this.spinner.hide();
      });
    } else {
      this.spinner.show();
      this.serviceSetup.addServiceSetup(formObj).subscribe(data => {
        this.spinner.hide();
        if (data.status === 'Success') {
          this.toastr.success(MessageConfig.Admin.SystemSetup.ServiceSetup.Add, 'Success!');
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        } else {
          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
          this.serviceSetupForm.reset();
          this.submitted = false;
        }
      }, (err) => {
        this.spinner.hide();
      });
    }
  }
  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }
}
