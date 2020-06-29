import { Component, OnInit } from '@angular/core';
import { CrudOperationService } from 'src/app/shared/services/crud-operation.service';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { ConfirmationService } from 'primeng/api';
//import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';

@Component({
  selector: 'app-service-setup-list',
  templateUrl: './service-setup-list.component.html',
  styleUrls: ['./service-setup-list.component.css']
})
export class ServiceSetupListComponent implements OnInit {
  serviceSetupDetails = [];
  showDialog = false;
  selectedData: any;
  headerData: string;
  isEdit: boolean;
  constructor(private crudService: CrudOperationService,private fb: FormBuilder,private confirmationService: ConfirmationService) { }

  ngOnInit() {
    this.getAllserviceSetupDetails();
  }
  getAllserviceSetupDetails() {
    this.serviceSetupDetails=this.crudService.getServiceSetupDetails();
  }
edit(data) {
this.selectedData = data;
this.showDialog = true;
}
delete(data) {
  const index = this.serviceSetupDetails.map(x => x.id).indexOf(data.id);
  if (index > -1) {
    this.confirmationService.confirm({
      header: 'Delete',
      message: 'Do you want to continue?',
      acceptLabel: 'Yes',
      rejectLabel: 'Cancel',
      accept: () => {
        this.serviceSetupDetails.splice(index, 1);
      },
      reject: () => {
      }
    });    
  }
}
closePopupEmit(event) {
  if(event.status === 'saved') {
    this.getAllserviceSetupDetails();
  }
  this.showDialog = event.isOpenPopup;
}
add( data, serviceDetails?) {
  if (data === 'add') {
    this.headerData = 'Create Setup';
    this.selectedData = serviceDetails;
    this.isEdit = false;
    this.showDialog = true;
  } else {
    this.headerData = 'Edit Setup';
    this.selectedData = serviceDetails;
    this.isEdit = true;
    this.showDialog = true;
  }
}
}
