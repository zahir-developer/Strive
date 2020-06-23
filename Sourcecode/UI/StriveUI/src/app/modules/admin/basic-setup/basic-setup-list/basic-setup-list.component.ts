import { Component, OnInit } from '@angular/core';
import { CrudOperationService } from 'src/app/shared/services/crud-operation.service';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { ConfirmationService } from 'primeng/api';
//import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';

@Component({
  selector: 'app-basic-setup-list',
  templateUrl: './basic-setup-list.component.html',
  styleUrls: ['./basic-setup-list.component.css']
})
export class BasicSetupListComponent implements OnInit {
  basicSetupDetails = [];
  basicSetupForm: FormGroup;
  showDialog = false;
  selectedData: any;
  headerData: string;
  isEdit: boolean;
  constructor(private crudService: CrudOperationService,private fb: FormBuilder,private confirmationService: ConfirmationService) { }

  ngOnInit() {
    this.basicSetupForm = this.fb.group({
      workHour: ['', Validators.required,Validators.maxLength(2)]
    });
    this.getAllBasicSetupDetails();
  }
  getAllBasicSetupDetails() {
    this.basicSetupDetails=this.crudService.getBasicSetupDetails();
  }
edit(data) {
this.selectedData = data;
this.showDialog = true;
}
delete(data) {
  const index = this.basicSetupDetails.map(x => x.id).indexOf(data.id);
  if (index > -1) {
    this.confirmationService.confirm({
      header: 'Delete',
      message: 'Do you want to continue?',
      acceptLabel: 'Yes',
      rejectLabel: 'Cancel',
      accept: () => {
        this.basicSetupDetails.splice(index, 1);
      },
      reject: () => {
      }
    });    
  }
}
closePopupEmit(event) {
  if(event.status === 'saved') {
    this.getAllBasicSetupDetails();
  }
  this.showDialog = event.isOpenPopup;
}
add( data, basicDetails?) {
  if (data === 'add') {
    this.headerData = 'Create Setup';
    this.selectedData = basicDetails;
    this.isEdit = false;
    this.showDialog = true;
  } else {
    this.headerData = 'Edit Setup';
    this.selectedData = basicDetails;
    this.isEdit = true;
    this.showDialog = true;
  }
}
}
