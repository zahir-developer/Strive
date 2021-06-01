import { Component, OnInit } from '@angular/core';
import { ProductService } from 'src/app/shared/services/data-service/product.service';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

@Component({
  selector: 'app-product-setup-list',
  templateUrl: './product-setup-list.component.html',
  styleUrls: ['./product-setup-list.component.css']
})
export class ProductSetupListComponent implements OnInit {
  productSetupDetails = [];
  showDialog = false;
  selectedData: any;

  headerData: string;
  isEdit: boolean;
  isTableEmpty: boolean;
  search: any = '';
  collectionSize: number = 0;
  pageSize: number;
  pageSizeList: number[];
  page: number;
  isLoading: boolean;
  isDesc: boolean;
  sortBy: string;
  sortColumn: { sortBy: any; sortOrder: string; };
  searchUpdate = new Subject<string>();
  Status: any;
  searchStatus: any;
  constructor(
    private productService: ProductService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService, private confirmationService: ConfirmationUXBDialogService) {
    // Debounce search.
    this.searchUpdate.pipe(
      debounceTime(ApplicationConfig.debounceTime.sec),
      distinctUntilChanged())
      .subscribe(value => {
        this.getAllproductSetupDetails();
      });
  }

  ngOnInit() {
    this.Status = [{ id: false, Value: 'InActive' }, { id: true, Value: 'Active' }, { id: '', Value: 'All' }];
    this.searchStatus = true;
    this.sortColumn = {
      sortBy: ApplicationConfig.Sorting.SortBy.ProductSetup, sortOrder: ApplicationConfig.Sorting.SortOrder.ProductSetup.order
    };

    this.isLoading = false;
    this.page = ApplicationConfig.PaginationConfig.page;
    this.pageSize = ApplicationConfig.PaginationConfig.TableGridSize;
    this.pageSizeList = ApplicationConfig.PaginationConfig.Rows;
    this.getAllproductSetupDetails();

  }

  productSearch() {
    this.page = 1;
    const obj = {
      productSearch: this.search
    };
    this.isLoading = true;
    this.productService.getProduct(obj).subscribe(data => {
      this.isLoading = false;
      if (data.status === 'Success') {
        const location = JSON.parse(data.resultData);
        this.productSetupDetails = location.ProductSearch;
        if (this.productSetupDetails.length === 0) {
          this.isTableEmpty = true;
        } else {
          this.sort(ApplicationConfig.Sorting.SortBy.ProductSetup)
          this.collectionSize = Math.ceil(this.productSetupDetails.length / this.pageSize) * 10;
          this.isTableEmpty = false;
        }
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.isLoading = false;
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  // Get All Product
  getAllproductSetupDetails() {
    const obj = {
      productSearch: this.search
    };
    this.isLoading = true;
    this.productService.getProduct(obj).subscribe(data => {
      this.isLoading = false;
      if (data.status === 'Success') {
        const product = JSON.parse(data.resultData);
        this.productSetupDetails = product.ProductSearch;
        if (this.productSetupDetails.length === 0) {
          this.isTableEmpty = true;
        } else {
          this.sort(ApplicationConfig.Sorting.SortBy.ProductSetup);
          this.collectionSize = Math.ceil(this.productSetupDetails.length / this.pageSize) * 10;
          this.isTableEmpty = false;
        }
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.isLoading = false;
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  sort(property) {
    this.sortColumn = {
      sortBy: property,
      sortOrder: ApplicationConfig.Sorting.SortOrder.ProductSetup.order
    }
    this.sorting(this.sortColumn)
    this.selectedCls(this.sortColumn)

  }
  sorting(sortColumn) {
    let direction = sortColumn.sortOrder == 'ASC' ? 1 : -1;
    let property = sortColumn.sortBy;
    this.productSetupDetails.sort(function (a, b) {
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
  changesort(property) {
    this.sortColumn = {
      sortBy: property,
      sortOrder: this.sortColumn.sortOrder == 'ASC' ? 'DESC' : 'ASC'
    }

    this.selectedCls(this.sortColumn)
    this.sorting(this.sortColumn)

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
    this.getAllproductSetupDetails();
  }
  paginatedropdown(event) {
    this.pageSize = +event.target.value;
    this.page = this.page;
    this.getAllproductSetupDetails();
  }
  edit(data) {
    this.selectedData = data;
    this.showDialog = true;
  }
  delete(data) {
    this.confirmationService.confirm('Delete Product', `Are you sure you want to delete this product? All related 
  information will be deleted and the product cannot be retrieved?`, 'Yes', 'No')
      .then((confirmed) => {
        if (confirmed === true) {
          this.confirmDelete(data);
        }
      })
      .catch(() => { });
  }

  // Delete Product
  confirmDelete(data) {
    this.spinner.show();
    this.productService.deleteProduct(data.ProductId).subscribe(res => {
      if (res.status === "Success") {
        this.spinner.hide();

        this.toastr.success(MessageConfig.Admin.SystemSetup.ProductSetup.Delete, 'Success!');
        this.getAllproductSetupDetails();
      } else {
        this.spinner.hide();

        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.spinner.hide();
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  closePopupEmit(event) {
    if (event.status === 'saved') {
      this.getAllproductSetupDetails();
    }
    this.showDialog = event.isOpenPopup;
  }
  add(data, productDetails?) {
    if (data === 'add') {
      this.headerData = 'Add New Product';
      this.selectedData = productDetails;
      this.isEdit = false;
      this.showDialog = true;
    } else {
      this.headerData = 'Edit Product';
      this.selectedData = productDetails;
      this.isEdit = true;
      this.showDialog = true;
    }
  }
}
