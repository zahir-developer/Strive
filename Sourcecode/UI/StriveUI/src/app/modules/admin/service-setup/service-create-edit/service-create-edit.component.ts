import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { CrudOperationService } from 'src/app/shared/services/crud-operation.service';
import * as moment from 'moment';
import { ServiceSetupService } from 'src/app/shared/services/data-service/service-setup.service';

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
  CommissionType:any;
  Status:any;
  isChecked:boolean;
  today : Date = new Date();
  constructor(private serviceSetup: ServiceSetupService,private fb: FormBuilder, private toastr: ToastrService,private crudService: CrudOperationService) { }

  ngOnInit() {
    //this.today = new Date(this.today.getFullYear(),this.today.getMonth(), 10);
    //this.serviceType=["Washes","Details","Additional Services","Upcharges","Air Fresheners","Discounts"];
    this.serviceSetupForm = this.fb.group({
      serviceType: ['', Validators.required],
      serviceId: ['', Validators.required],
      name: ['', Validators.required],
      cost: ['', Validators.required],
      commission: ['', Validators.required],
      commissionType: ['', Validators.required],
      upcharge: ['', Validators.required],
      parentName: ['', Validators.required],
      status: ['', Validators.required]
    });
    this.getAllServiceType();
    this.isChecked=false;
    console.log(this.selectedData);
    if (this.selectedData !== undefined && this.selectedData.length !== 0) {
      this.serviceSetupForm.reset();
      this.serviceSetupForm.patchValue({
        serviceType: this.selectedData.ServiceType,
        serviceId: this.selectedData.ServiceId,
        name: this.selectedData.ServiceName,
        cost: this.selectedData.Cost
        // commission: this.selectedData.Commission,
        // commissionType: this.selectedData.CommissionType,
        // upcharge: this.selectedData.Upcharge,
        // parentName: this.selectedData.ParentName,
        // status: this.selectedData.Status        
      });
    }
  }

  getAllServiceType()
  {
    this.serviceSetup.getServiceType().subscribe(data =>{
      if(data.status === "Success"){
        const sType= JSON.parse(data.resultData);
        this.serviceType = sType.ServiceType;
      }
    })
  }

  change(data){
    this.serviceSetupForm.value.commission = data;
    if(data === true){
      this.isChecked = true;
    }else{
      this.isChecked = false;
    }
  }
  submit() {
    console.log('submitted');
    const sourceObj = [];
    const formObj = {
      serviceType: this.serviceSetupForm.value.serviceType,
      serviceId: this.serviceSetupForm.value.serviceId,
      serviceName: this.serviceSetupForm.value.name,
      cost: this.serviceSetupForm.value.cost,
      commission: this.isChecked,
      commissionType: this.serviceSetupForm.value.commission == true ? this.serviceSetupForm.value.commissionType : 0,
      upcharges: (this.serviceSetupForm.value.upcharge == "" || this.serviceSetupForm.value.upcharge == null) ? 0.00 : this.serviceSetupForm.value.upcharge,
      //parentName: this.serviceSetupForm.value.parentName,
      parentServiceId:0,
      isActive:true,
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
      }
    });
  }
  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }
}
