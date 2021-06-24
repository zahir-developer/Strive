import { Component, OnInit } from '@angular/core';
import { ServiceSetupService } from 'src/app/shared/services/data-service/service-setup.service';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-service-setup-list',
  templateUrl: './service-setup-list.component.html'
})
export class ServiceSetupListComponent implements OnInit {
  showDialog = false;
  selectedData: any;
  headerData: string;
  isEdit: boolean;
  isTableEmpty: boolean;
  searchStatus: any;
  collectionSize: number = 0;
  Status: any;
  page: number;
  pageSize: number;
  pageSizeList: number[];
  column = ApplicationConfig.Sorting.SortBy.ServiceSetup;
  totalRowCount = 0;
  isLoading: boolean;
  sortColumn: { sortBy: string; sortOrder: string; };
  public serviceSetupDetails: string[] = [];
  public search: string;
  searchUpdate = new Subject<string>();
  
  constructor(
    private serviceSetup: ServiceSetupService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private confirmationService: ConfirmationUXBDialogService
  ) {
    // Debounce search.
    this.searchUpdate.pipe(
      debounceTime(ApplicationConfig.debounceTime.sec),
      distinctUntilChanged())
      .subscribe(value => {
        this.getAllserviceSetupDetails();
      });
  }


  ngOnInit() {
    this.isLoading = false;
    this.sortColumn = {
      sortBy: ApplicationConfig.Sorting.SortBy.ServiceSetup, sortOrder: ApplicationConfig.Sorting.SortOrder.ServiceSetup.order };
    this.page = ApplicationConfig.PaginationConfig.page;
    this.pageSize = ApplicationConfig.PaginationConfig.TableGridSize;
    this.pageSizeList = ApplicationConfig.PaginationConfig.Rows;
    this.Status = [{ id: false, Value: 'InActive' }, { id: true, Value: 'Active' }, { id: '', Value: 'All' }];
    this.searchStatus = true;
    this.getAllserviceSetupDetails();
  }
  
  getAllserviceSetup(){
    this.page = 1;
    this.pageSize = ApplicationConfig.PaginationConfig.TableGridSize;
    this.getAllserviceSetupDetails();

  }

  // Get All Services
  getAllserviceSetupDetails() {
    const serviceObj = {
      locationId: +localStorage.getItem('empLocationId'),
      pageNo: this.page,
      pageSize: this.pageSize,
      query: this.search !== '' ? this.search : null,
      sortOrder: this.sortColumn.sortOrder,
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
        console.log(serviceDetails, 'service')

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
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }
  paginate(event) {
    this.pageSize = +this.pageSize;
    this.page = event;
    this.getAllserviceSetupDetails();
  }
  paginatedropdown(event) {
    this.pageSize = +event.target.value;
    this.page = 1;
    this.getAllserviceSetupDetails();
  }
  searchKeyup(event) {
    if (event) {
        this.getAllserviceSetupDetails();
    }

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
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
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
    this.spinner.show();
    this.serviceSetup.deleteServiceSetup(data.ServiceId).subscribe(res => {
      if (res.status === 'Success') {
        this.spinner.hide();

        this.toastr.success(MessageConfig.Admin.SystemSetup.ServiceSetup.Delete, 'Success!');
        this.sortColumn = { sortBy: ApplicationConfig.Sorting.SortBy.ServiceSetup, sortOrder: ApplicationConfig.Sorting.SortOrder.ServiceSetup.order };
        this.page = 1;
        this.getAllserviceSetupDetails();
      } else {
        this.spinner.hide();

        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.spinner.hide();
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  changeSorting(column) {
    this.sortColumn = {
      sortBy: column,
      sortOrder: this.sortColumn.sortOrder == 'ASC' ? 'DESC' : 'ASC'
    }

    this.selectedCls(this.sortColumn)
    this.getAllserviceSetupDetails();
  }



  selectedCls(column) {
    if (column === this.sortColumn.sortBy && this.sortColumn.sortOrder === 'DESC') {
      return 'fa-sort-desc';
    } else if (column === this.sortColumn.sortBy && this.sortColumn.sortOrder === 'ASC') {
      return 'fa-sort-asc';
    }
    return '';
  }


  closePopupEmit(event) {
    if (event.status === 'saved') {
      this.sortColumn = { sortBy: ApplicationConfig.Sorting.SortBy.ServiceSetup, sortOrder: ApplicationConfig.Sorting.SortOrder.ServiceSetup.order };

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

