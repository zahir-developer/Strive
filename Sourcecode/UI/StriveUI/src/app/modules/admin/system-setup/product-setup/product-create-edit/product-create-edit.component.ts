import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ProductService } from 'src/app/shared/services/data-service/product.service';
import { GetCodeService } from 'src/app/shared/services/data-service/getcode.service';
import { LocationService } from 'src/app/shared/services/data-service/location.service';
import { NgxSpinnerService } from 'ngx-spinner';

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
  fileName: any = null;
  isLoading: boolean;
  fileUploadformData: any = null;
  fileThumb: any = null;
  costErrMsg: boolean = false;
  priceErrMsg: boolean = false;
  employeeId: number;
  constructor(
    private fb: FormBuilder,
    private toastr: ToastrService,
    private locationService: LocationService,
    private product: ProductService,
    private getCode: GetCodeService,
    private spinner: NgxSpinnerService
    ) { }

  ngOnInit() {
    this.employeeId = +localStorage.getItem('empId');
    this.textDisplay = false;
    this.getProductType();
    this.getAllLocation();
    this.getAllVendor();
    this.Status = [{ id: 0, Value: "Active" }, { id: 1, Value: "InActive" }];
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
      other: ['',],
      suggested: ['', Validators.required]
    });
    this.productSetupForm.patchValue({ status: 0 });
    if (this.isEdit !== true) {
      this.productSetupForm.controls.status.disable();
    } else {
      this.productSetupForm.controls.status.enable();
    }
  }
  // Get ProductType
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
  // Get Size
  getSize() {
    this.getCode.getCodeByCategory("SIZE").subscribe(data => {
      if (data.status === "Success") {
        const pSize = JSON.parse(data.resultData);
        this.size = pSize.Codes;
        const other = this.size.filter(i => i.CodeValue === "Other")[0];
        this.size = this.size.filter(i => i.CodeValue !== "Other");
        this.size.push(other);
        console.log(this.size, 'size');
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }
  // Get All Vendors
  getAllVendor() {
    this.product.getVendor().subscribe(data => {
      if (data.status === 'Success') {
        const vendor = JSON.parse(data.resultData);
        this.Vendor = vendor.Vendor.filter(item => item.IsActive === 'True');
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    })
  }
  // Get All Location
  getAllLocation() {
    this.locationService.getLocation().subscribe(data => {
      if (data.status === 'Success') {
        const location = JSON.parse(data.resultData);
        this.locationName = location.Location.filter(item => item.IsActive === true);
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }

  showText(data) {
    const size = this.size.filter( item => item.CodeValue === 'Other');
    if (size.length > 0) {
      const id = size[0].CodeId;
      if (+data === id) {
        this.textDisplay = true;
        this.productSetupForm.get('other').setValidators([Validators.required]);
      } else {
        this.textDisplay = false;
        this.productSetupForm.get('other').clearValidators();
        this.productSetupForm.get('other').reset();
      }
    }
  }
  // Get Product By Id
  getProductById() {
    this.product.getProductById(this.selectedData.ProductId).subscribe(data => {
      if (data.status === "Success") {
        const pType = JSON.parse(data.resultData);
        this.selectedProduct = pType.Product;
        this.productSetupForm.patchValue({
          productType: this.selectedProduct.ProductType,
          locationName: this.selectedProduct.LocationId,
          name: this.selectedProduct.ProductName,
          cost: this.selectedProduct?.Cost?.toFixed(2),
          suggested: this.selectedProduct?.Price?.toFixed(2),
          taxable: this.selectedProduct.IsTaxable,
          taxAmount: this.selectedProduct.TaxAmount !== 0 ? this.selectedProduct.TaxAmount : "",
          size: this.selectedProduct.Size,
          quantity: this.selectedProduct.Quantity,
          status: this.selectedProduct.IsActive ? 0 : 1,
          vendor: this.selectedProduct.VendorId,
          thresholdAmount: this.selectedProduct.ThresholdLimit
        });
        this.fileName = this.selectedProduct.originalFileName;
        this.fileUploadformData = this.selectedProduct.Base64;
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
      this.productSetupForm.get('taxAmount').clearValidators();
      this.productSetupForm.get('taxAmount').reset();
    }
  }

  // Add/Update Product
  submit() {
    this.submitted = true;
    if (this.productSetupForm.invalid) {
      if (this.productSetupForm.value.cost !== "") {
        if (Number(this.productSetupForm.value.cost) <= 0) {
          this.costErrMsg = true;
          return;
        } else {
          this.costErrMsg = false;
        }
      }
      return;
    }
    this.productSetupForm.controls.status.enable();
    const formObj = {
      productCode: null,
      productDescription: null,
      productType: this.productSetupForm.value.productType,
      productId: this.isEdit ? this.selectedProduct.ProductId : 0,
      locationId: this.productSetupForm.value.locationName,
      productName: this.productSetupForm.value.name,
      fileName: this.fileName,
      thumbFileName: this.fileThumb,
      base64: this.fileUploadformData,
      cost: this.productSetupForm.value.cost,
      isTaxable: this.isChecked,
      taxAmount: this.isChecked ? this.productSetupForm.value.taxAmount : 0,
      size: this.productSetupForm.value.size,
      sizeDescription: this.textDisplay ? this.productSetupForm.value.other : null,
      quantity: this.productSetupForm.value.quantity,
      quantityDescription: null,
      isActive: this.productSetupForm.value.status === 0 ? true : false,
      vendorId: this.productSetupForm.value.vendor,
      thresholdLimit: this.productSetupForm.value.thresholdAmount,
      isDeleted: false,
      createdBy: this.employeeId,
      createdDate: this.isEdit ? this.selectedProduct.CreatedDate : new Date(),
      updatedBy: this.employeeId,
      updatedDate: new Date(),
      price: this.productSetupForm.value.suggested
    };
    if (this.isEdit === true) {
      this.spinner.show();
      this.product.updateProduct(formObj).subscribe(data => {
        this.spinner.hide();
        if (data.status === 'Success') {
          this.toastr.success('Record Updated Successfully!!', 'Success!');
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        } else {
          this.toastr.error('Communication Error', 'Error!');
          this.productSetupForm.reset();
          this.submitted = false;
        }
      }, (err) => {
        this.spinner.hide();
      });
    } else {
      this.spinner.show();
      this.product.addProduct(formObj).subscribe(data => {
        this.spinner.hide();
        if (data.status === 'Success') {
          this.toastr.success('Record Saved Successfully!!', 'Success!');
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        } else {
          this.toastr.error('Communication Error', 'Error!');
          this.productSetupForm.reset();
          this.submitted = false;
        }
      }, (err) => {
        this.spinner.hide();
      });
    }
  }
  fileNameChanged() {
    let filesSelected: any;
    filesSelected = document.getElementById('customFile');
    filesSelected = filesSelected.files;
    if (filesSelected.length > 0) {
      const fileToLoad = filesSelected[0];
      this.fileName = fileToLoad.name;
      this.fileThumb = this.fileName.substring(this.fileName.lastIndexOf('.') + 1);
      let fileReader: any;
      fileReader = new FileReader();
      fileReader.onload = function (fileLoadedEventTigger) {
        let textAreaFileContents: any;
        textAreaFileContents = document.getElementById('customFile');
        textAreaFileContents.innerHTML = fileLoadedEventTigger.target.result;
      };
      fileReader.readAsDataURL(fileToLoad);
      this.isLoading = true;
      setTimeout(() => {
        let fileTosaveName: any;
        fileTosaveName = fileReader.result.split(',')[1];
        this.fileUploadformData = fileTosaveName;
        this.isLoading = false;
        console.log(this.fileName, this.fileUploadformData.length);
      }, 5000);
    }
  }

  clearDocument() {
    this.fileName = null;
    this.fileThumb = null;
    this.fileUploadformData = null;
  }
  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }
}
