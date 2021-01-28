import { Component, OnInit } from '@angular/core';
import { ServiceSetupService } from 'src/app/shared/services/data-service/service-setup.service';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { NgxSpinnerService } from 'ngx-spinner';
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
  search: any = '';
  searchStatus: any;
  collectionSize: number = 0;
  Status: any;
  page: number;
  pageSize: number;
  pageSizeList: number[];
  isDesc: boolean = false;
  column: string = 'ServiceName';
  totalRowCount = 0;
  isLoading: boolean;
  constructor(private serviceSetup: ServiceSetupService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService, private confirmationService: ConfirmationUXBDialogService) { }

  ngOnInit() {
    this.page = ApplicationConfig.PaginationConfig.page;
    this.pageSize = ApplicationConfig.PaginationConfig.TableGridSize;
    this.pageSizeList = ApplicationConfig.PaginationConfig.Rows;
    this.Status = [{ id: 0, Value: 'InActive' }, { id: 1, Value: 'Active' }, { id: 2, Value: 'All' }];
    this.searchStatus = 1;
    this.getAllserviceSetupDetails();
  }

  // Get All Services
  getAllserviceSetupDetails() {
    const serviceObj = {
      locationId: +localStorage.getItem('empLocationId'),
      pageNo: this.page,
      pageSize: this.pageSize,
      query: this.search !== '' ? this.search : null,
      sortOrder: null,
      sortBy: null,
      status: this.searchStatus !== 2 ? this.searchStatus === 1 ? true : false : null
    };
    this.serviceSetup.getServiceSetup(serviceObj).subscribe(data => {
      if (data.status === 'Success') {
        this.totalRowCount = 0;
        this.serviceSetupDetails = [];
        const serviceDetails = JSON.parse(data.resultData);
        if (serviceDetails.ServiceSetup.getAllServiceViewModel !== null) {
          this.serviceSetupDetails = serviceDetails.ServiceSetup.getAllServiceViewModel;
          if (this.serviceSetupDetails.length === 0) {
            this.isTableEmpty = true;
          } else {
            this.sort('ServiceName');
            this.totalRowCount = serviceDetails.ServiceSetup.Count.Count;
            this.collectionSize = Math.ceil(this.totalRowCount / this.pageSize) * 10;
            this.isTableEmpty = false;
          }
        }
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }
  paginate(event) {
    this.pageSize = +this.pageSize;
    this.page = event;
    this.getAllserviceSetupDetails();
  }
  paginatedropdown(event) {
    this.pageSize = +event.target.value;
    this.page = this.page;
    this.getAllserviceSetupDetails();
  }

  serviceSearch() {
    this.page = 1;
    const obj = {
      serviceSearch: this.search,
      status: this.searchStatus === '' ? 2 : Number(this.searchStatus)
    };
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
      if (res.status === 'Success') {
        this.toastr.success('Record Deleted Successfully!!', 'Success!');
        this.getAllserviceSetupDetails();
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }
  sort(property) {
    this.isDesc = !this.isDesc; // change the direction
    this.column = property;
    let direction = this.isDesc ? 1 : -1;
    this.serviceSetupDetails.sort(function (a, b) {
      if (a[property] < b[property]) {
        return -1 * direction;
      }
      else if (a[property] > b[property]) {
        return 1 * direction;
      }
      else {
        return 0;
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

