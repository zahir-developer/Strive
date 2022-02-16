import { Component, OnInit, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { ClientService } from 'src/app/shared/services/data-service/client.service';
import { FormGroup, FormBuilder } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { ActivatedRoute, Router } from '@angular/router';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { DetailService } from 'src/app/shared/services/data-service/detail.service';
import { DashboardStaticsComponent } from 'src/app/shared/components/dashboard-statics/dashboard-statics.component';
import { distinctUntilChanged, debounceTime } from 'rxjs/operators';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-client-list',
  templateUrl: './client-list.component.html'
})
export class ClientListComponent implements OnInit {
  clientDetails = [];
  showDialog = false;
  selectedData: any;
  headerData: string;
  isEdit: boolean;
  isTableEmpty: boolean;
  isView: boolean;
  selectedClient: any;
  search: any = '';
  locationId = +localStorage.getItem('empLocationId');
  collectionSize: number = 0;

  pageSizeList: number[];
  page: number;
  pageSize: number;
  jobTypeId: any;
  searchUpdate = new Subject<string>();

  @ViewChild(DashboardStaticsComponent) dashboardStaticsComponent: DashboardStaticsComponent;
  sortColumn: { sortBy: string; sortOrder: string; };
  constructor(
    private client: ClientService, private toastr: ToastrService,
    private confirmationService: ConfirmationUXBDialogService,
    private spinner: NgxSpinnerService, private router: Router,
    private route: ActivatedRoute,
    private detailService: DetailService
  ) {
    // Debounce search.
    this.searchUpdate.pipe(
      debounceTime(ApplicationConfig.debounceTime.sec),
      distinctUntilChanged())
      .subscribe(value => {
        this.resetPagination();
        this.getAllClientDetails();
      });
  }

  ngOnInit() {
    this.sortColumn = { sortBy: ApplicationConfig.Sorting.SortBy.Client, sortOrder: ApplicationConfig.Sorting.SortOrder.Client.order };

    this.page = ApplicationConfig.PaginationConfig.page;
    this.pageSize = ApplicationConfig.PaginationConfig.TableGridSize;
    this.pageSizeList = ApplicationConfig.PaginationConfig.Rows;
    const paramsData = this.route.snapshot.queryParamMap.get('clientId');
    if (paramsData !== null) {
      const clientObj = {
        ClientId: paramsData
      };
      this.getClientById('view', clientObj);
    }
    this.getAllClientDetails();
  }

  newgetAllClientDetails() {
    this.page = 1;
    this.getAllClientDetails();
  }

  resetPagination() {
    this.page = 1;
  }

  // Get All Client
  getAllClientDetails() {
    const obj = {
      LocationId: this.locationId,
      PageNo: this.page,
      PageSize: this.pageSize,
      Query: this.search,
      SortOrder: this.sortColumn.sortOrder,
      SortBy: this.sortColumn.sortBy
    };
    this.spinner.show();
    this.client.getClient(obj).subscribe(data => {
      if (data.status === 'Success') {
        this.spinner.hide();
        this.getJobType();
        this.clientDetails = [];
        const client = JSON.parse(data.resultData);
        if (client.Client.clientViewModel !== null) {
          this.clientDetails = client.Client.clientViewModel;
        }
        const totalRowCount = client.Client.Count.Count;
        if (this.clientDetails.length === 0) {
          this.isTableEmpty = true;
        } else {
          this.collectionSize = Math.ceil(totalRowCount / this.pageSize) * 10;
          this.isTableEmpty = false;
        }
      } else {
        this.spinner.hide();

        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      this.spinner.hide();
    });
  }
  paginate(event) {
    this.pageSize = +this.pageSize;
    this.page = event;
    this.getAllClientDetails();
  }
  paginatedropdown(event) {
    this.pageSize = +event.target.value;
    this.page = 1;
    this.getAllClientDetails();
  }

  clientSearch() {
    const obj = {
      LocationId: this.locationId,
      PageNo: this.page,
      PageSize: this.pageSize,
      Query: this.search,
      SortOrder: this.sortColumn.sortOrder,
      SortBy: this.sortColumn.sortBy
    };
    this.client.getClient(obj).subscribe(data => {
      if (data.status === 'Success') {
        const client = JSON.parse(data.resultData);
        this.clientDetails = client.Client.clientViewModel;
        const totalRowCount = client.Client.Count.Count;
        if (this.clientDetails.length === 0) {
          this.isTableEmpty = true;
        } else {
          this.collectionSize = Math.ceil(totalRowCount / this.pageSize) * 10;
          this.isTableEmpty = false;
        }
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }
  delete(data) {
    this.confirmationService.confirm('Delete Client', `Are you sure you want to delete this client? All related 
    information will be deleted and the client cannot be retrieved?`, 'Yes', 'No')
      .then((confirmed) => {
        if (confirmed === true) {
          this.confirmDelete(data);
        }
      })
      .catch(() => { });
  }

  // Delete Client
  confirmDelete(data) {
    this.spinner.show();
    this.client.deleteClient(data.ClientId).subscribe(res => {
      if (res.status === 'Success') {
        this.spinner.hide();

        this.toastr.success(MessageConfig.Client.Delete, 'Success!');
        this.sortColumn = { sortBy: ApplicationConfig.Sorting.SortBy.Client, sortOrder: ApplicationConfig.Sorting.SortOrder.Client.order };
        this.page = 1;
        this.getAllClientDetails();
      } else {
        this.spinner.hide();

        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      this.spinner.hide();
    });
  }
  closePopupEmit(event) {
    if (event.status === 'saved') {
      this.sortColumn = { sortBy: ApplicationConfig.Sorting.SortBy.Client, sortOrder: ApplicationConfig.Sorting.SortOrder.Client.order };

      this.getAllClientDetails();
    }
    this.showDialog = event.isOpenPopup;
  }
  add(data, clientDet?) {
    if (data === 'add') {
      this.headerData = 'Add New Client';
      this.showDialog = true;
      this.selectedData = clientDet;
      this.isEdit = false;
      this.isView = false;
    } else {
      this.getClientById(data, clientDet);
    }
  }

  // Get Client By Id
  getClientById(data, client) {
    this.spinner.show();
    this.client.getClientById(client.ClientId).subscribe(res => {
      if (res.status === 'Success') {
        this.spinner.hide();

        const clientDetail = JSON.parse(res.resultData);
        this.selectedClient = clientDetail.Status[0];
        if (data === 'edit') {
          this.headerData = 'Edit Client';
          this.selectedData = this.selectedClient;
          this.isEdit = true;
          this.isView = false;
          this.showDialog = true;
        } else {
          this.headerData = 'View Client';
          this.selectedData = this.selectedClient;
          this.isEdit = true;
          this.isView = true;
          this.showDialog = true;
        }
      } else {
        this.spinner.hide();

        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      this.spinner.hide();
    });
  }

  navigateToCustmerDashboard(client) {
    this.router.navigate(['/customer'], { queryParams: { clientId: client.ClientId } });
  }

  changeSorting(column) {
    this.sortColumn = {
      sortBy: column,
      sortOrder: this.sortColumn.sortOrder == 'ASC' ? 'DESC' : 'ASC'
    }

    this.selectedCls(this.sortColumn)
    this.getAllClientDetails();
  }



  selectedCls(column) {
    if (column === this.sortColumn.sortBy && this.sortColumn.sortOrder === 'DESC') {
      return 'fa-sort-desc';
    } else if (column === this.sortColumn.sortBy && this.sortColumn.sortOrder === 'ASC') {
      return 'fa-sort-asc';
    }
    return '';
  }

  getJobType() {
    this.detailService.getJobType().subscribe(res => {
      if (res.status === 'Success') {
        const jobtype = JSON.parse(res.resultData);
        if (jobtype.GetJobType.length > 0) {
          jobtype.GetJobType.forEach(item => {
            if (item.valuedesc === 'Wash') {
              this.jobTypeId = item.valueid;
              if (this.dashboardStaticsComponent != undefined) {
                this.dashboardStaticsComponent.jobTypeId = this.jobTypeId;
                this.dashboardStaticsComponent.getDashboardDetails();
              }
            }
          });
        }
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  sendClientMail() {
    this.spinner.show();
    this.client.sendClientEmail().subscribe(res => {
      if (res.status === 'Success') {
        this.spinner.hide();
      } else {
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      this.spinner.hide();
    });
  }
}
