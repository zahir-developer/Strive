import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import * as moment from 'moment';
import { ServiceSetupService } from 'src/app/shared/services/data-service/service-setup.service';
import { GetCodeService } from 'src/app/shared/services/data-service/getcode.service';

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
  commissionTypeLabel: any;
  Status: any;
  parent: any;
  isChecked: boolean;
  today: Date = new Date();
  submitted: boolean;
  ctypeLabel: any;
  constructor(private serviceSetup: ServiceSetupService, private getCode: GetCodeService, private fb: FormBuilder, private toastr: ToastrService) { }

  ngOnInit() {
    this.Status = [{id : 0,Value :"Active"}, {id :1 , Value:"InActive"}];
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
      cost: ['', Validators.required],
      commission: ['',],
      commissionType: ['',],
      upcharge: ['',],
      parentName: ['',],
      status: ['',],
      fee: ['',]
    });
    this.serviceSetupForm.patchValue({status : 0});
  }

  get f() {
    return this.serviceSetupForm.controls;
  }

  getServiceById() {
    this.serviceSetup.getServiceSetupById(this.selectedData.ServiceId).subscribe(data => {
      if (data.status === "Success") {
        const sType = JSON.parse(data.resultData);
        this.selectedService = sType.ServiceSetupById[0];
        this.serviceSetupForm.patchValue({
          serviceType: this.selectedService.ServiceType,
          name: this.selectedService.ServiceName,
          cost: this.selectedService.Cost,
          commission: this.selectedService.Commision,
          commissionType: this.selectedService.CommisionType,
          fee: this.selectedService.CommissionCost,
          upcharge: this.selectedService.Upcharges,
          parentName: this.selectedService.ParentServiceId,
          status: this.selectedService.IsActive ? 0 : 1
        });
        this.change(this.selectedService.Commision);
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }

  getCommissionType() {
    this.getCode.getCodeByCategory("COMMISIONTYPE").subscribe(data => {
      if (data.status === "Success") {
        const cType = JSON.parse(data.resultData);
        this.CommissionType = cType.Codes;
        this.getParentType();
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }

  getParentType() {
    this.serviceSetup.getServiceSetup().subscribe(data => {
      if (data.status === 'Success') {
        const serviceDetails = JSON.parse(data.resultData);
        this.parent = serviceDetails.ServiceSetup;
        this.getAllServiceType();
      } else {
        this.toastr.error('Communication Error', 'Error!');
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
  getAllServiceType() {
    this.getCode.getCodeByCategory("SERVICETYPE").subscribe(data => {
      if (data.status === "Success") {
        const cType = JSON.parse(data.resultData);
        this.serviceType = cType.Codes;
        if (this.isEdit === true) {
          this.serviceSetupForm.reset();
          this.getServiceById();
        }
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }

  change(data) {
    this.serviceSetupForm.value.commission = data;
    if (data === true) {
      this.isChecked = true;
      this.serviceSetupForm.get('commissionType').setValidators([Validators.required]);
      this.getCtype(this.selectedService.CommisionType);
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
  submit() {
    this.submitted = true;
    if (this.serviceSetupForm.invalid) {
      return;
    }
    console.log(this.serviceSetupForm.value.status);
    const sourceObj = [];
    const formObj = {
      serviceType: this.serviceSetupForm.value.serviceType,
      serviceId: this.isEdit ? this.selectedService.ServiceId : 0,
      serviceName: this.serviceSetupForm.value.name,
      cost: this.serviceSetupForm.value.cost,
      commision: this.isChecked,
      commisionType: this.isChecked == true ? this.serviceSetupForm.value.commissionType : 0,
      upcharges: (this.serviceSetupForm.value.upcharge == "" || this.serviceSetupForm.value.upcharge == null) ? 0.00 : this.serviceSetupForm.value.upcharge,
      parentServiceId: this.serviceSetupForm.value.parentName === "" ? 0 : this.serviceSetupForm.value.parentName,
      isActive: this.serviceSetupForm.value.status == 0 ? true : false,
      locationId: 1,
      commissionCost: this.isChecked === true ? this.serviceSetupForm.value.fee : 0,
      dateEntered: moment(this.today).format('YYYY-MM-DD')
    };
    sourceObj.push(formObj);
    this.serviceSetup.updateServiceSetup(sourceObj).subscribe(data => {
      if (data.status === 'Success') {
        if (this.isEdit === true) {
          this.toastr.success('Record Updated Successfully!!', 'Success!');
        } else {
          this.toastr.success('Record Saved Successfully!!', 'Success!');
        }
        this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
      } else {
        this.toastr.error('Communication Error', 'Error!');
        this.serviceSetupForm.reset();
        this.submitted = false;
      }
    });
  }
  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }
}
