import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { ClientService } from 'src/app/shared/services/data-service/client.service';
import { FormGroup, FormBuilder } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { ActivatedRoute, Router } from '@angular/router';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';

@Component({
  selector: 'app-client-list',
  templateUrl: './client-list.component.html',
  styleUrls: ['./client-list.component.css']
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
  sort = { column: 'IsActive', descending: true };
  sortColumn: { column: string; descending: boolean; };
  pageSizeList: number[];
  page: number;
  pageSize: number;
  constructor(
    private client: ClientService, private toastr: ToastrService,
    private confirmationService: ConfirmationUXBDialogService,
    private spinner: NgxSpinnerService, private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
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

  // Get All Client
  getAllClientDetails() {
    const obj = {
      LocationId: this.locationId,
      PageNo: this.page,
      PageSize: this.pageSize,
      Query: this.search,
      SortOrder: null,
      SortBy: null
    };
    this.spinner.show();
    this.client.getClient(obj).subscribe(data => {
      this.spinner.hide();
      if (data.status === 'Success') {
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
        this.toastr.error('Communication Error', 'Error!');
      }
    }, (err) => {
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
    this.page = this.page;
    this.getAllClientDetails();
  }

  clientSearch() {
    const obj = {
      LocationId: this.locationId,
      PageNo: this.page,
      PageSize: this.pageSize,
      Query: this.search,
      SortOrder: null,
      SortBy: null
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
        this.toastr.error('Communication Error', 'Error!');
      }
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
    this.client.deleteClient(data.ClientId).subscribe(res => {
      if (res.status === 'Success') {
        this.toastr.success('Record Deleted Successfully!!', 'Success!');
        this.getAllClientDetails();
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }
  closePopupEmit(event) {
    if (event.status === 'saved') {
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
      this.spinner.hide();
      if (res.status === 'Success') {
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
        this.toastr.error('Communication Error', 'Error!');
      }
    }, (err) => {
      this.spinner.hide();
    });
  }

  navigateToCustmerDashboard(client) {
    this.router.navigate(['/customer'], { queryParams: { clientId: client.ClientId } });
  }

  changeSorting(column) {
    this.changeSortingDescending(column, this.sort);
    this.sortColumn = this.sort;
  }

  changeSortingDescending(column, sortingInfo) {
    if (sortingInfo.column === column) {
      sortingInfo.descending = !sortingInfo.descending;
    } else {
      sortingInfo.column = column;
      sortingInfo.descending = false;
    }
    return sortingInfo;
  }

  sortedColumnCls(column, sortingInfo) {
    if (column === sortingInfo.column && sortingInfo.descending) {
      return 'fa-sort-desc';
    } else if (column === sortingInfo.column && !sortingInfo.descending) {
      return 'fa-sort-asc';
    }
    return '';
  }

  selectedCls(column) {
    return this.sortedColumnCls(column, this.sort);
  }
}
