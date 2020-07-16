import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { CrudOperationService } from 'src/app/shared/services/crud-operation.service';
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
  selectedService:any;
  CommissionType:any;
  commissionTypeLabel : any;
  Status:any;
  isChecked:boolean;
  today : Date = new Date();
  submitted: boolean;
  ctypeLabel: any;
  constructor(private serviceSetup: ServiceSetupService,private getCode: GetCodeService, private fb: FormBuilder, private toastr: ToastrService,private crudService: CrudOperationService) { }

  ngOnInit() {
    this.Status=["Active","InActive"];
    this.CommissionType = ["Percentage","Flat Fee"];
    this.serviceSetupForm = this.fb.group({
      serviceType: ['', Validators.required],
      name: ['', Validators.required],
      cost: ['', Validators.required],
      commission: ['',],
      commissionType: ['',],
      upcharge: ['',],
      parentName: ['',],
      status: ['',],
      fee:['',]
    });
    this.ctypeLabel = 'none';
    this.getAllServiceType();
    this.getCommissionType();
    this.isChecked=false;
    this.submitted = false;
    console.log(this.selectedData);
    if (this.isEdit === true) {
      this.serviceSetupForm.reset();
      this.getServiceById();      
    }
  }

  get f(){
    return this.serviceSetupForm.controls;
  }

  getServiceById(){
    this.serviceSetup.getServiceSetupById(this.selectedData.ServiceId).subscribe(data =>{
      if(data.status === "Success"){
        const sType= JSON.parse(data.resultData);
        this.selectedService = sType.ServiceSetupById[0];
        this.serviceSetupForm.patchValue({
          serviceType: this.selectedService.ServiceType,
          name: this.selectedService.ServiceName,
          cost: this.selectedService.Cost,
          commission: this.selectedService.Commision,
          commissionType: this.selectedService.CommisionType,
          upcharge: this.selectedService.Upcharges,
          //parentName: this.selectedService.ParentName,
          status: this.selectedData.IsActive ? this.Status[0] : this.Status[1]
        });
        this.change(this.selectedService.Commision);
      }      
    });
  }

  getCommissionType(){
    this.getCode.getCodeByCategory("COMMISIONTYPE").subscribe(data =>{
      if(data.status === "Success"){
        const cType= JSON.parse(data.resultData);
        //this.prodType=pType.Codes;
        //console.log(cType);
      }else{
        this.toastr.error('Communication Error','Error!');
      }
    });
  }

  getCtype(data){
    this.ctypeLabel = data;
  }
  getAllServiceType()
  {
    this.serviceSetup.getServiceType().subscribe(data =>{
      if(data.status === "Success"){
        const sType= JSON.parse(data.resultData);
        this.serviceType = sType.ServiceType;
      }
    });
  }

  change(data){
    this.serviceSetupForm.value.commission = data;
    if(data === true){
      this.isChecked = true;
      this.serviceSetupForm.get('commissionType').setValidators([Validators.required]);
      this.serviceSetupForm.get('fee').setValidators([Validators.required]);
    }else{
      this.isChecked = false;
      this.serviceSetupForm.get('commissionType').clearValidators();
      this.serviceSetupForm.get('fee').clearValidators();
    }
  }
  submit() {
    this.submitted = true;
    if(this.serviceSetupForm.invalid){
      return;
    }

    const sourceObj = [];
    const formObj = {
      serviceType: this.serviceSetupForm.value.serviceType,
      serviceId: this.isEdit ? this.selectedService.ServiceId : 0,
      serviceName: this.serviceSetupForm.value.name,
      cost: this.serviceSetupForm.value.cost,
      commision: this.isChecked,
      commisionType: this.serviceSetupForm.value.commission == true ? this.serviceSetupForm.value.commissionType : 0,
      upcharges: (this.serviceSetupForm.value.upcharge == "" || this.serviceSetupForm.value.upcharge == null) ? 0.00 : this.serviceSetupForm.value.upcharge,
      //parentName: this.serviceSetupForm.value.parentName,
      parentServiceId:0,
      isActive: true,
      locationId:1,
      dateEntered: moment(this.today).format('YYYY-MM-DD')
    };
    sourceObj.push(formObj);
    console.log(sourceObj);
    this.serviceSetup.updateServiceSetup(sourceObj).subscribe(data => {
      if (data.status === 'Success') {
        if (this.isEdit === true) {
          this.toastr.success('Record Updated Successfully!!', 'Success!');
        } else {
          this.toastr.success('Record Saved Successfully!!', 'Success!');
        }
        this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
      }else{
        this.toastr.error('Communication Error','Error!');
        this.serviceSetupForm.reset();
        this.submitted=false;
      }
    });
  }
  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }
}
