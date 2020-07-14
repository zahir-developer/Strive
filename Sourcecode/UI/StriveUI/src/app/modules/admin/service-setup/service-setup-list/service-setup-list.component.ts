import { Component, OnInit } from '@angular/core';
import { CrudOperationService } from 'src/app/shared/services/crud-operation.service';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { ConfirmationService } from 'primeng/api';
import { ServiceSetupService } from 'src/app/shared/services/data-service/service-setup.service';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
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
  isTableEmpty: boolean;
  constructor(private serviceSetup: ServiceSetupService,private toastr: ToastrService,private fb: FormBuilder,private confirmationService: ConfirmationUXBDialogService) { }

  ngOnInit() {
    this.isTableEmpty = true;
    this.getAllserviceSetupDetails();
    console.log(this.serviceSetupDetails.length);
    
  }
  getAllserviceSetupDetails() {
    this.serviceSetup.getServiceSetup().subscribe(data =>{
      if (data.status === 'Success') {
        const serviceDetails = JSON.parse(data.resultData);
        this.serviceSetupDetails = serviceDetails.ServiceSetup;
        console.log(this.serviceSetupDetails);
        if(this.serviceSetupDetails.length === 0){
          this.isTableEmpty = true;
        }else{
          this.isTableEmpty = false;
        }
      }
    });
  }
edit(data) {
this.selectedData = data;
this.showDialog = true;
}
delete(data) {
  this.confirmationService.confirm('Delete Service', `Are you sure you want to delete this service? All related 
  information will be deleted and the service cannot be retrieved?`, 'Yes', 'No')
    .then((confirmed) => {
      if (confirmed === true) {
        this.confirmDelete(data);
      }
    })
    .catch(() => {});
}
confirmDelete(data){
  this.serviceSetup.deleteServiceSetup(data.ServiceId).subscribe(res =>{
    if(res.status === "Success"){
      this.toastr.success('Record Deleted Successfully!!', 'Success!');
      this.getAllserviceSetupDetails();
    }else{
      this.toastr.error('Communication Error','Error!');
    }
  });
}

closePopupEmit(event) {
  if(event.status === 'saved') {
    this.getAllserviceSetupDetails();
  }
  this.showDialog = event.isOpenPopup;
}
add( data, serviceDetails?) {
  if (data === 'add') {
    this.headerData = 'Add New Service';
    this.selectedData = serviceDetails;
    this.isEdit = false;
    this.showDialog = true;
  } else {
    this.headerData = 'Edit Service';
    this.selectedData = serviceDetails;
    this.isEdit = true;
    this.showDialog = true;
  }
}
}
