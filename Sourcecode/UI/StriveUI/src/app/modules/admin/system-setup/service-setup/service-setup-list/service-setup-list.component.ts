import { Component, OnInit } from '@angular/core';
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
  isLoading = true;
  search: any = '';
  searchStatus: any;
  page = 1;
  pageSize = 5;
  collectionSize: number = 0;
  Status: any;
  constructor(private serviceSetup: ServiceSetupService, private toastr: ToastrService, private confirmationService: ConfirmationUXBDialogService) { }

  ngOnInit() {
    this.Status = [{id : 0,Value :"InActive"}, {id :1 , Value:"Active"}, {id :2 , Value:"All"}];
    this.searchStatus = 2;
    this.getAllserviceSetupDetails();
  }

  // Get All Services
  getAllserviceSetupDetails() {
    this.isLoading = true;
    this.serviceSetup.getServiceSetup().subscribe(data => {
      this.isLoading = false;
      if (data.status === 'Success') {
        const serviceDetails = JSON.parse(data.resultData);
        this.serviceSetupDetails = serviceDetails.ServiceSetup;
        if (this.serviceSetupDetails.length === 0) {
          this.isTableEmpty = true;
        } else {
          this.collectionSize = Math.ceil(this.serviceSetupDetails.length/this.pageSize) * 10;
          this.isTableEmpty = false;
        }
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }

  serviceSearch(){
    this.page = 1;
    const obj ={
      serviceSearch: this.search,
      status: Number(this.searchStatus)
   }
   this.serviceSetup.ServiceSearch(obj).subscribe(data => {
     if (data.status === 'Success') {
       const location = JSON.parse(data.resultData);
       this.serviceSetupDetails = location.ServiceSearch;
       if (this.serviceSetupDetails.length === 0) {
         this.isTableEmpty = true;
       } else {
         this.collectionSize = Math.ceil(this.serviceSetupDetails.length / this.pageSize) * 10;
         this.isTableEmpty = false;
       }
     } else {
       this.toastr.error('Communication Error', 'Error!');
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
      .catch(() => { });
  }

  // Delete Service
  confirmDelete(data) {
    this.serviceSetup.deleteServiceSetup(data.ServiceId).subscribe(res => {
      if (res.status === "Success") {
        this.toastr.success('Record Deleted Successfully!!', 'Success!');
        this.getAllserviceSetupDetails();
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }

  closePopupEmit(event) {
    if (event.status === 'saved') {
      this.getAllserviceSetupDetails();
    }
    this.showDialog = event.isOpenPopup;
  }
  add(data, serviceDetails?) {
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
