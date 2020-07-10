import { Component, OnInit } from '@angular/core';
import { CrudOperationService } from 'src/app/shared/services/crud-operation.service';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { ConfirmationService } from 'primeng/api';
import { ProductService } from 'src/app/shared/services/data-service/product.service';
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
  constructor(private productService: ProductService,private fb: FormBuilder,private confirmationService: ConfirmationService) { }

  ngOnInit() {
    //this.getAllproductSetupDetails();
  }
  getAllproductSetupDetails() {
    this.productService.getProduct().subscribe(data =>{
      if (data.status === 'Success') {
        const product = JSON.parse(data.resultData);
        this.productSetupDetails = product.Product;
      }
    })
  }
edit(data) {
this.selectedData = data;
this.showDialog = true;
}
delete(data) {
  const index = this.productSetupDetails.map(x => x.id).indexOf(data.id);
  if (index > -1) {
    this.confirmationService.confirm({
      header: 'Delete',
      message: 'Do you want to continue?',
      acceptLabel: 'Yes',
      rejectLabel: 'Cancel',
      accept: () => {
        this.productSetupDetails.splice(index, 1);
      },
      reject: () => {
      }
    });    
  }
}
closePopupEmit(event) {
  if(event.status === 'saved') {
    this.getAllproductSetupDetails();
  }
  this.showDialog = event.isOpenPopup;
}
add( data, productDetails?) {
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
