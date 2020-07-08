import { Component, OnInit } from '@angular/core';
import { CrudOperationService } from 'src/app/shared/services/crud-operation.service';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { ConfirmationService } from 'primeng/api';
import { ServiceSetupService } from 'src/app/shared/services/data-service/service-setup.service';
import { ToastrService } from 'ngx-toastr';
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
  constructor(private serviceSetup: ServiceSetupService,private toastr: ToastrService,private fb: FormBuilder,private confirmationService: ConfirmationService) { }

  ngOnInit() {
    this.getAllserviceSetupDetails();
  }
  getAllserviceSetupDetails() {
    this.serviceSetup.getServiceSetup().subscribe(data =>{
      if (data.status === 'Success') {
        const serviceDetails = JSON.parse(data.resultData);
        this.serviceSetupDetails = serviceDetails.ServiceSetup;
      }
    });
  }
edit(data) {
this.selectedData = data;
this.showDialog = true;
}
delete(data) {
    //  this.confirmationService.confirm({
    //   header: 'Delete',
    //   message: 'Do you want to continue?',
    //   acceptLabel: 'Yes',
    //   rejectLabel: 'Cancel',
    //   accept: () => {
          this.serviceSetup.deleteServiceSetup(data.ServiceId).subscribe(res =>{
            if(res.status === "Success"){
              this.toastr.success('Record Deleted Successfully!!', 'Success!');
              this.getAllserviceSetupDetails();
            }
          });
          
    //   },
    //   reject: () => {
    //   }
    // });    
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
