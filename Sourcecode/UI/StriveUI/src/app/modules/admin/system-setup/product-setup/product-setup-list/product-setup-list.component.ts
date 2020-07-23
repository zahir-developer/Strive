import { Component, OnInit } from '@angular/core';
import { ProductService } from 'src/app/shared/services/data-service/product.service';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationUXBDialogService } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.service';
//import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';

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
  constructor(private productService: ProductService, private toastr: ToastrService, private confirmationService: ConfirmationUXBDialogService) { }

  ngOnInit() {
    this.isTableEmpty = true;
    this.getAllproductSetupDetails();

  }
  getAllproductSetupDetails() {
    this.productService.getProduct().subscribe(data => {
      if (data.status === 'Success') {
        const product = JSON.parse(data.resultData);
        this.productSetupDetails = product.Product.filter(item => item.IsActive === true);
        if (this.productSetupDetails.length === 0) {
          this.isTableEmpty = true;
        } else {
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
    this.confirmationService.confirm('Delete Product', `Are you sure you want to delete this product? All related 
  information will be deleted and the product cannot be retrieved?`, 'Yes', 'No')
      .then((confirmed) => {
        if (confirmed === true) {
          this.confirmDelete(data);
        }
      })
      .catch(() => { });
  }

  confirmDelete(data) {
    this.productService.deleteProduct(data.ProductId).subscribe(res => {
      if (res.status === "Success") {
        this.toastr.success('Record Deleted Successfully!!', 'Success!');
        this.getAllproductSetupDetails();
      } else {
        this.toastr.error('Communication Error', 'Error!');
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
