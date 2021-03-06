import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { TenantSetupService } from 'src/app/shared/services/data-service/tenant-setup.service';

@Component({
  selector: 'app-tenant-setup',
  templateUrl: './tenant-setup.component.html',
  styleUrls: ['./tenant-setup.component.css']
})
export class TenantSetupComponent implements OnInit {
  search = '';
  showDialog = false;
  tenantList = [];
  page: any;
  pageSize: any;
  pageSizeList: any[];
  collectionSize: number;
  tenantDetail: any;
  isEdit: boolean;
  tenantModule: any;
  sortColumn: { sortBy: string; sortOrder: string; };
  constructor(
    private tenantSetupService: TenantSetupService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.isEdit = false;
    this.sortColumn = {
      sortBy: ApplicationConfig.Sorting.SortBy.tenantSetup, sortOrder: ApplicationConfig.Sorting.SortOrder.tenantSetup.order };
    this.page = ApplicationConfig.PaginationConfig.page;
    this.pageSize = ApplicationConfig.PaginationConfig.TableGridSize;
    this.pageSizeList = ApplicationConfig.PaginationConfig.Rows;
    this.getTenantList();
  }

  addTenant() {
    this.showDialog = true;
  }


  getTenantList() {
    this.spinner.show();
    this.tenantSetupService.getTenantList().subscribe(res => {
      this.spinner.hide();
      if (res.status === 'Success') {
        const tenant = JSON.parse(res.resultData);
        if (tenant.AllTenant != null) {
          this.tenantList = tenant.AllTenant;
          this.collectionSize = Math.ceil(this.tenantList.length / this.pageSize) * 10;
        }
      }
    }, (err) => {
      this.spinner.hide();
    });
  }

  editTenant(tenant) {
    this.spinner.show();
    this.tenantSetupService.getTenantDetailById(tenant.ClientId).subscribe(res => {
      this.spinner.hide();
      this.showDialog = true;
      this.isEdit = true;
      this.tenantDetail = res.tenantViewModel;
      this.tenantModule = res.tenantModuleViewModel;
      // if (res.status === 'Success') {
      //   const tenantDetail = JSON.parse(res.resultData);
      //   if (tenantDetail.TenantById) {

      //   }
      // }
    }, (err) => {
      this.spinner.hide();
    });
  }

  closePopup() {
    this.isEdit = false;
    this.showDialog = false;
  }

  reloadGrid() {
    this.isEdit = false;
    this.showDialog = false;
    this.getTenantList();
  }

  changeSorting(column) {
    this.sortColumn = {
      sortBy: column,
      sortOrder: this.sortColumn.sortOrder === 'ASC' ? 'DESC' : 'ASC'
    };

    this.selectedCls(this.sortColumn);
    // this.getAllserviceSetupDetails();
  }

  selectedCls(column) {
    if (column === this.sortColumn.sortBy && this.sortColumn.sortOrder === 'DESC') {
      return 'fa-sort-desc';
    } else if (column === this.sortColumn.sortBy && this.sortColumn.sortOrder === 'ASC') {
      return 'fa-sort-asc';
    }
    return '';
  }

  paginate(event) {
    this.pageSize = +this.pageSize;
    this.page = event;
  }
  paginatedropdown(event) {
    this.pageSize = +event.target.value;
    this.page = this.page;
  }

}
