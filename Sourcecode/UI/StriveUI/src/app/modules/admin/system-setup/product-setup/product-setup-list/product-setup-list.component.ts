import { Component, OnInit } from '@angular/core';
import { ProductService } from 'src/app/shared/services/data-service/product.service';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageConfig } from 'src/app/shared/services/messageConfig';

@Component({
  selector: 'app-product-setup-list',
  templateUrl: './product-setup-list.component.html',
  styleUrls: ['./product-setup-list.component.css']
})
export class ProductSetupListComponent implements OnInit {
  productSetupDetails = [];
  showDialog = false;
  selectedData: any;
  isDesc: boolean = false;
  column: string = 'ProductName';
  headerData: string;
  isEdit: boolean;
  isTableEmpty: boolean;
  search: any = '';
  collectionSize: number = 0;
  pageSize: number;
  pageSizeList: number[];
  page: number;
  isLoading: boolean;
  constructor(private productService: ProductService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService, private confirmationService: ConfirmationUXBDialogService) { }

  ngOnInit() {
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
    this.productService.ProductSearch(obj).subscribe(data => {
      this.isLoading = false;
      if (data.status === 'Success') {
        const location = JSON.parse(data.resultData);
        this.productSetupDetails = location.ProductSearch;
        if (this.productSetupDetails.length === 0) {
          this.isTableEmpty = true;
        } else {
          this.sort('ProductName')
          this.collectionSize = Math.ceil(this.productSetupDetails.length / this.pageSize) * 10;
          this.isTableEmpty = false;
        }
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.isLoading = false;
    });
  }

  // Get All Product
  getAllproductSetupDetails() {
    this.isLoading = true;
    this.productService.getProduct().subscribe(data => {
      this.isLoading = false;
      if (data.status === 'Success') {
        const product = JSON.parse(data.resultData);
        this.productSetupDetails = product.Product;
        if (this.productSetupDetails.length === 0) {
          this.isTableEmpty = true;
        } else {
          this.collectionSize = Math.ceil(this.productSetupDetails.length / this.pageSize) * 10;
          this.isTableEmpty = false;
        }
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.isLoading = false;
    });
  }
  sort(property) {
    this.isDesc = !this.isDesc; //change the direction    
    this.column = property;
    let direction = this.isDesc ? 1 : -1;

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
    this.productService.deleteProduct(data.ProductId).subscribe(res => {
      if (res.status === "Success") {
        this.toastr.success(MessageConfig.Admin.SystemSetup.ProductSetup.Delete, 'Success!');
        this.getAllproductSetupDetails();
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
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
