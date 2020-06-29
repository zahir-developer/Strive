import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { CrudOperationService } from 'src/app/shared/services/crud-operation.service';

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
  constructor(private fb: FormBuilder, private toastr: ToastrService,private crudService: CrudOperationService) { }

  ngOnInit() {

    this.serviceType=["Washes","Details","Additional Services","Upcharges","Air Fresheners","Discounts"];
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
    console.log(this.selectedData);
    if (this.selectedData !== undefined && this.selectedData.length !== 0) {
      this.serviceSetupForm.reset();
      this.serviceSetupForm.setValue({
        serviceType: this.selectedData.ServiceType,
        serviceId: this.selectedData.ServiceId,
        name: this.selectedData.Name,
        cost: this.selectedData.Cost,
        commission: this.selectedData.Commission,
        commissionType: this.selectedData.CommissionType,
        upcharge: this.selectedData.Upcharge,
        parentName: this.selectedData.ParentName,
        status: this.selectedData.Status        
      });
    }
  }

  change(data){
    this.serviceSetupForm.value.status = data;
  }
  submit() {
    console.log('submitted');
    const sourceObj = [];
    const formObj = {
      serviceType: this.serviceSetupForm.value.serviceType,
      serviceId: this.serviceSetupForm.value.serviceId,
      name: this.serviceSetupForm.value.name,
      cost: this.serviceSetupForm.value.cost,
      commission: this.serviceSetupForm.value.commission,
      commissionType: this.serviceSetupForm.value.commissionType,
      upcharge: this.serviceSetupForm.value.upcharge,
      parentName: this.serviceSetupForm.value.parentName,
      status: this.serviceSetupForm.value.status
    };
    sourceObj.push(formObj);
    this.crudService.servicesetupdetails.push(formObj);
        if (this.isEdit === true) {
          this.toastr.success('Record Updated Successfully!!', 'Success!');
        } else {
          this.toastr.success('Record Saved Successfully!!', 'Success!');
        }
  }
  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }
}
