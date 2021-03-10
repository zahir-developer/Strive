import { Component, OnInit } from '@angular/core';
import { ServiceSetupService } from 'src/app/shared/services/data-service/service-setup.service';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageConfig } from 'src/app/shared/services/messageConfig';

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
  totalRowCount = 0;
  isLoading: boolean;
  sortColumn: { sortBy: string; SortOrder: string; };
  sort: { sortBy: string; SortOrder: string; };

  
  constructor(
    private serviceSetup: ServiceSetupService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private confirmationService: ConfirmationUXBDialogService
  ) { }

  ngOnInit() {
    this.isLoading = false;
    this.sortColumn = { sortBy: ApplicationConfig.Sorting.SortBy.ServiceSetup ,SortOrder: ApplicationConfig.Sorting.SortOrder.ServiceSetup.ASC };
    this.page = ApplicationConfig.PaginationConfig.page;
    this.pageSize = ApplicationConfig.PaginationConfig.TableGridSize;
    this.pageSizeList = ApplicationConfig.PaginationConfig.Rows;
    this.Status = [{ id: false, Value: 'InActive' }, { id: true, Value: 'Active' }, { id: '', Value: 'All' }];
    this.searchStatus = true;
    this.getAllserviceSetupDetails();
  }

  // Get All Services
  getAllserviceSetupDetails() {
    const serviceObj = {
      locationId: +localStorage.getItem('empLocationId'),
      pageNo: this.page,
      pageSize: this.pageSize,
      query: this.search !== '' ? this.search : null,
      sortOrder: this.sortColumn.SortOrder,
      sortBy: this.sortColumn.sortBy,
      status: this.searchStatus === '' ? null : this.searchStatus
    };
    this.isLoading = true;
    this.serviceSetup.getServiceSetup(serviceObj).subscribe(data => {
      this.isLoading = false;
      if (data.status === 'Success') {
        this.totalRowCount = 0;
        this.serviceSetupDetails = [];
        const serviceDetails = JSON.parse(data.resultData);
        if (serviceDetails.ServiceSetup.getAllServiceViewModel !== null) {
          this.serviceSetupDetails = serviceDetails.ServiceSetup.getAllServiceViewModel;
          if (this.serviceSetupDetails.length === 0) {
            this.isTableEmpty = true;
          } else {
            this.totalRowCount = serviceDetails.ServiceSetup.Count.Count;
            this.collectionSize = Math.ceil(this.totalRowCount / this.pageSize) * 10;
            this.isTableEmpty = false;
          }
        }
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.isLoading = false;
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
      status: this.searchStatus === '' ? null : this.searchStatus
    };
    this.isLoading = true;
    this.serviceSetup.ServiceSearch(obj).subscribe(data => {
      this.isLoading = false;
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
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.isLoading = false;
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
        this.toastr.success(MessageConfig.Admin.SystemSetup.ServiceSetup.Delete, 'Success!');
        this.getAllserviceSetupDetails();
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    });
  }

  changeSorting(sortBy) {
    this.changeSortingDescending(sortBy, this.sort);
    this.sortColumn = this.sort;
    this.getAllserviceSetupDetails();
  }

  changeSortingDescending(sortBy, sortingInfo) {
    if (sortingInfo.sortBy === sortBy) {
      sortingInfo.sortOrder = !sortingInfo.sortOrder;
    } else {
      sortingInfo.sortBy = sortBy;
      sortingInfo.sortOrder =  sortingInfo.sortOrder;
    }
    return sortingInfo;
  }

  sortedColumnCls(sortBy, sortingInfo) {
    if (sortBy === sortingInfo.sortBy && sortingInfo.sortOrder) {
      return 'fa-sort-desc';
    } else if (sortBy === sortingInfo.sortBy && !sortingInfo.sortOrder) {
      return 'fa-sort-asc';
    }
    return '';
  }

  selectedCls(sortBy) {
    return this.sortedColumnCls(sortBy, this.sort);
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

