import { Component, OnInit } from '@angular/core';
import { CrudOperationService } from 'src/app/shared/services/crud-operation.service';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { ConfirmationService } from 'primeng/api';
//import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';

@Component({
  selector: 'app-vendor-setup-list',
  templateUrl: './vendor-setup-list.component.html',
  styleUrls: ['./vendor-setup-list.component.css']
})
export class VendorSetupListComponent implements OnInit {
  vendorSetupDetails = [];
  showDialog = false;
  selectedData: any;
  headerData: string;
  isEdit: boolean;
  isTableEmpty:boolean;
  constructor(private crudService: CrudOperationService,private fb: FormBuilder,private confirmationService: ConfirmationService) { }

  ngOnInit() {
    this.getAllvendorSetupDetails();
    if(this.vendorSetupDetails.length === 0){
      this.isTableEmpty = true;
    }else{
      this.isTableEmpty = false;
    }
  }
  getAllvendorSetupDetails() {
    this.vendorSetupDetails=this.crudService.getVendorSetupDetails();
  }
edit(data) {
this.selectedData = data;
this.showDialog = true;
}
delete(data) {
  const index = this.vendorSetupDetails.map(x => x.id).indexOf(data.id);
  if (index > -1) {
    this.confirmationService.confirm({
      header: 'Delete',
      message: 'Do you want to continue?',
      acceptLabel: 'Yes',
      rejectLabel: 'Cancel',
      accept: () => {
        this.vendorSetupDetails.splice(index, 1);
      },
      reject: () => {
      }
    });    
  }
}
closePopupEmit(event) {
  if(event.status === 'saved') {
    this.getAllvendorSetupDetails();
  }
  this.showDialog = event.isOpenPopup;
}
add( data, vendorDetails?) {
  if (data === 'add') {
    this.headerData = 'Add Vendor Setup';
    this.selectedData = vendorDetails;
    this.isEdit = false;
    this.showDialog = true;
  } else {
    this.headerData = 'Edit Vendor';
    this.selectedData = vendorDetails;
    this.isEdit = true;
    this.showDialog = true;
  }
}
}
