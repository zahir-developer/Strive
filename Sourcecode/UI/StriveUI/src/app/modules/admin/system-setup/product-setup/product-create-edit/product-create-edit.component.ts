import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ProductService } from 'src/app/shared/services/data-service/product.service';
import { GetCodeService } from 'src/app/shared/services/data-service/getcode.service';
import { LocationService } from 'src/app/shared/services/data-service/location.service';

@Component({
  selector: 'app-product-create-edit',
  templateUrl: './product-create-edit.component.html',
  styleUrls: ['./product-create-edit.component.css']
})
export class ProductCreateEditComponent implements OnInit {
  productSetupForm: FormGroup;
  prodType: any;
  size: any;
  Status: any;
  Vendor: any;
  locationName: any;
  isChecked: boolean;
  @Output() closeDialog = new EventEmitter();
  @Input() selectedData?: any;
  @Input() isEdit?: any;
  submitted: boolean;
  selectedProduct: any;
  textDisplay: boolean;
  constructor(private fb: FormBuilder, private toastr: ToastrService, private locationService: LocationService, private product: ProductService, private getCode: GetCodeService) { }

  ngOnInit() {
    this.getProductType();
    this.getAllLocation();
    this.getAllVendor();
    this.Status = ["Active", "InActive"];
    this.formInitialize();
    this.isChecked = false;
    this.submitted = false;
    if (this.isEdit === true) {
      this.productSetupForm.reset();
      this.getProductById();
    }
  }

  formInitialize() {
    this.productSetupForm = this.fb.group({
      productType: ['', Validators.required],
      locationName: ['', Validators.required],
      name: ['', Validators.required],
      size: ['',],
      quantity: ['',],
      cost: ['', Validators.required],
      taxable: ['',],
      taxAmount: ['',],
      status: ['',],
      vendor: ['',],
      thresholdAmount: ['',],
      other: ['',]
    });
  }

  getProductType() {
    this.getCode.getCodeByCategory("PRODUCTTYPE").subscribe(data => {
      if (data.status === "Success") {
        const pType = JSON.parse(data.resultData);
        this.prodType = pType.Codes;
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
    this.getSize();
  }
  getSize() {
    this.getCode.getCodeByCategory("SIZE").subscribe(data => {
      if (data.status === "Success") {
        const pSize = JSON.parse(data.resultData);
        this.size = pSize.Codes;
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }

  getAllVendor() {
    this.product.getVendor().subscribe(data => {
      if (data.status === 'Success') {
        const vendor = JSON.parse(data.resultData);
        this.Vendor = vendor.Vendor
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    })
  }

  getAllLocation() {
    this.locationService.getLocation().subscribe(data => {
      if (data.status === 'Success') {
        const location = JSON.parse(data.resultData);
        this.locationName = location.Location;
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }

  showText(data) {
    if (data === '33') {
      this.textDisplay = true;
      this.productSetupForm.get('other').setValidators([Validators.required]);
    } else {
      this.textDisplay = false;
      this.productSetupForm.get('other').clearValidators();
    }
  }

  getProductById() {
    this.product.getProductById(this.selectedData.ProductId).subscribe(data => {
      if (data.status === "Success") {
        const pType = JSON.parse(data.resultData);
        this.selectedProduct = pType.Product;
        this.productSetupForm.patchValue({
          productType: this.selectedProduct.ProductType,
          locationName: this.selectedProduct.LocationId,
          name: this.selectedProduct.ProductName,
          cost: this.selectedProduct.Cost,
          taxable: this.selectedProduct.IsTaxable,
          taxAmount: this.selectedProduct.TaxAmount !== 0 ? this.selectedProduct.TaxAmount : "",
          size: this.selectedProduct.Size,
          quantity: this.selectedProduct.Quantity,
          status: this.selectedProduct.IsActive ? "Active" : "InActive",
          vendor: this.selectedProduct.VendorId,
          thresholdAmount: this.selectedProduct.ThresholdLimit
        });
        if (this.selectedProduct.Size === 33) {
          this.textDisplay = true;
          this.productSetupForm.controls['other'].patchValue(this.selectedProduct.SizeDescription);
        }
        this.change(this.selectedProduct.IsTaxable);
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }

  get f() {
    return this.productSetupForm.controls;
  }

  change(data) {
    this.productSetupForm.value.taxable = data;
    if (data === true) {
      this.isChecked = true;
      this.productSetupForm.get('taxAmount').setValidators([Validators.required]);
    } else {
      this.isChecked = false;
      this.productSetupForm.get('taxAmount').reset();
      this.productSetupForm.get('taxAmount').clearValidators();
    }
  }
  submit() {
    this.submitted = true;
    if (this.productSetupForm.invalid) {
      return;
    }
    const sourceObj = [];
    const formObj = {
      productCode: null,
      productDescription: null,
      productType: this.productSetupForm.value.productType,
      productId: this.isEdit ? this.selectedProduct.ProductId : 0,
      locationId: this.productSetupForm.value.locationName,
      productName: this.productSetupForm.value.name,
      cost: this.productSetupForm.value.cost,
      isTaxable: this.isChecked,
      taxAmount: this.isChecked ? this.productSetupForm.value.taxAmount : 0,
      size: this.productSetupForm.value.size,
      sizeDescription: this.textDisplay ? this.productSetupForm.value.other : null,
      quantity: this.productSetupForm.value.quantity,
      quantityDescription: null,
      isActive: this.productSetupForm.value.status === "Active" ? true : false,
      vendorId: this.productSetupForm.value.vendor,
      thresholdLimit: this.productSetupForm.value.thresholdAmount
    };
    sourceObj.push(formObj);
    this.product.updateProduct(sourceObj).subscribe(data => {
      if (data.status === 'Success') {
        if (this.isEdit === true) {
          this.toastr.success('Record Updated Successfully!!', 'Success!');
        } else {
          this.toastr.success('Record Saved Successfully!!', 'Success!');
        }
        this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
      } else {
        this.toastr.error('Communication Error', 'Error!');
        this.productSetupForm.reset();
        this.submitted = false;
      }
    });
  }
  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }
}
