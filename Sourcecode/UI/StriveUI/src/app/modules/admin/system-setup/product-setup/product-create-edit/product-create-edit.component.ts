import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ProductService } from 'src/app/shared/services/data-service/product.service';
import { GetCodeService } from 'src/app/shared/services/data-service/getcode.service';
import { LocationService } from 'src/app/shared/services/data-service/location.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { CodeValueService } from 'src/app/shared/common-service/code-value.service';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { EmployeeService } from 'src/app/shared/services/data-service/employee.service';

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
  base64Value = '';
  IsOtherSize: number;
  sizeId: number;
  dropdownSettings: IDropdownSettings = {};
  location: any;
  productSetupList: any = [];

  constructor(
    private fb: FormBuilder,
    private toastr: ToastrService,
    private locationService: LocationService,
    private product: ProductService,
    private getCode: GetCodeService,
    private spinner: NgxSpinnerService,
    private codeService: CodeValueService,
    private employeeService: EmployeeService
  ) { }

  ngOnInit() {
    this.employeeId = +localStorage.getItem('empId');
    this.textDisplay = false;
    this.getLocation();
    this.getAllVendor();
    this.getProductType();
    this.Status = [{ id: 0, Value: "Active" }, { id: 1, Value: "InActive" }];
    this.formInitialize();
    this.isChecked = false;
    this.submitted = false;
    if (this.isEdit === true) {
      this.productSetupForm.reset();
      this.getProductById();
      this.getSize();
    }
  }

  formInitialize() {
    this.productSetupForm = this.fb.group({
      productType: ['', Validators.required],
      locationName: [[], Validators.required],
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

    const prodTypeCodes = this.codeService.getCodeValueByType('ProductType');
    if (prodTypeCodes.length > 0) {
      this.prodType = prodTypeCodes;
    }

  }

  getLocation() {
    this.employeeService.getLocation().subscribe(res => {
      if (res.status === 'Success') {
        const location = JSON.parse(res.resultData);
        this.location = location.Location;
        this.location = this.location.map(item => {
          return {
            id: item.LocationId,
            name: item.LocationName
          };
        });
        this.dropdownSetting();
      }
    });
  }

  dropdownSetting() {
    this.dropdownSettings = {
      singleSelection: false,
      defaultOpen: false,
      idField: 'id',
      textField: 'name',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 1,
      allowSearchFilter: false
    };
  }

  // Get Size
  getSize() {
    const sizeCodes = this.codeService.getCodeValueByType('Size');
    if (sizeCodes.length > 0) {
      this.size = sizeCodes;
    }
  }
  // Get All Vendors
  getAllVendor() {
    this.product.getVendor().subscribe(data => {
      if (data.status === 'Success') {
        const vendor = JSON.parse(data.resultData);
        this.Vendor = vendor.Vendor;
        this.Vendor = this.Vendor.map(item => {
          return {
            id: item.VendorId,
            name: item.VendorName
          };
        });
        this.dropdownSetting();
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    });
  }
  // Get All Location
  getAllLocation() {
    this.product.getAllLocationName().subscribe(data => {
      if (data.status === 'Success') {
        const location = JSON.parse(data.resultData);
        this.locationName = location.Location;
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    });
  }

  showText(data) {
    const size = this.size.filter(item => item.CodeValue === 'Other');
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
    this.spinner.show();
    this.product.getProductById(this.selectedData.ProductId).subscribe(data => {
      if (data.status === "Success") {
        this.spinner.hide();

        const pType = JSON.parse(data.resultData);
        this.selectedProduct = pType.Product;
        let name = '';
        this.location.forEach( item => {
          if (+item.id === +this.selectedProduct.LocationId) {
            name = item.name;
          }
        });
        const locObj = {
          id : this.selectedProduct.LocationId,
          name
        };
        const selectedLocation = [];
        selectedLocation.push(locObj);
        let vendorName = '';
        this.Vendor.forEach( item => {
          if (+item.id === +this.selectedProduct.VendorId) {
            vendorName = item.name;
          }
        });
        const vendorObj = {
          id : this.selectedProduct.VendorId,
          name: vendorName
        };
        const selectedVendor = [];
        selectedVendor.push(vendorObj);
        this.dropdownSetting();
        this.productSetupForm.patchValue({
          productType: this.selectedProduct.ProductType,
          locationName: selectedLocation,
          name: this.selectedProduct.ProductName,
          cost: this.selectedProduct?.Cost?.toFixed(2),
          suggested: this.selectedProduct?.Price?.toFixed(2),
          taxable: this.selectedProduct.IsTaxable,
          taxAmount: this.selectedProduct.TaxAmount !== 0 ? this.selectedProduct.TaxAmount : "",
          size: this.selectedProduct.Size,
          quantity: this.selectedProduct.Quantity,
          status: this.selectedProduct.IsActive ? 0 : 1,
          vendor: selectedVendor,
          thresholdAmount: this.selectedProduct.ThresholdLimit
        });
        this.fileName = this.selectedProduct.FileName;
        this.base64Value = this.selectedProduct.Base64;
        this.fileUploadformData = this.selectedProduct.Base64;
        if (this.selectedProduct.Size !== null) {
          this.sizeId = this.selectedProduct.Size;
          const sizeObj = this.size.filter(item => item.CodeId === this.selectedProduct.Size);
          if (sizeObj !== null) {
            this.enableOtherSize(sizeObj);
          }
        }
        this.change(this.selectedProduct.IsTaxable);
      } else {
        this.spinner.hide();

        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.spinner.hide();
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
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
    const formObj = {
      Product: this.productSetupList
    };
    const obj: any = {};
    const productObj: any = {};
    const productList = [];
    this.productSetupForm.controls.status.enable();
    if (this.productSetupForm.value.locationName || this.productSetupForm.value.vendor) {
      (this.productSetupForm.value.locationName || []).forEach(item => {
        const vendorList = [];
        this.productSetupForm.value.vendor.forEach(vendor => {
          productObj.productCode = null;
          productObj.productDescription = null;
          productObj.productType = this.productSetupForm.value.productType;
          productObj.productId = this.isEdit ? this.selectedProduct.ProductId : 0;
          productObj.locationId = item.id;
          productObj.productName = this.productSetupForm.value.name;
          productObj.fileName = this.fileName;
          productObj.OriginalFileName = this.fileName;
          productObj.thumbFileName = this.fileThumb;
          productObj.base64 = this.fileUploadformData;
          productObj.cost = this.productSetupForm.value.cost;
          productObj.isTaxable = this.isChecked;
          productObj.taxAmount = this.isChecked ? this.productSetupForm.value.taxAmount : 0;
          productObj.size = this.productSetupForm.value.size;
          productObj.sizeDescription = this.textDisplay ? this.productSetupForm.value.other : null;
          productObj.quantity = this.productSetupForm.value.quantity;
          productObj.quantityDescription = null;
          productObj.isActive = this.productSetupForm.value.status === 0 ? true : false;
          productObj.thresholdLimit = this.productSetupForm.value.thresholdAmount;
          productObj.isDeleted = false;
          productObj.price = this.productSetupForm.value.suggested;
          vendorList.push({
            productVendorId: this.isEdit ? this.selectedProduct.ProductVendorId : 0,
            productId: this.isEdit ? this.selectedProduct.ProductId : 0,
            vendorId: vendor.id,
            isActive: true,
            isDeleted: false,
          });
        });
        obj.product = productObj;
        obj.productVendor = vendorList;
        productList.push(obj);
      });
    }
    const finalObj = {
      Product: productList
    };

    if (this.isEdit === true) {
      this.spinner.show();
      this.product.updateProduct(finalObj).subscribe(data => {
        this.spinner.hide();
        if (data.status === 'Success') {
          this.toastr.success(MessageConfig.Admin.SystemSetup.ProductSetup.Update, 'Success!');
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        } else {
          this.spinner.hide();

          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
          this.productSetupForm.reset();
          this.submitted = false;
        }
      }, (err) => {
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');

      });
    } else {
      this.spinner.show();
      this.product.addProduct(finalObj).subscribe(data => {
        if (data.status === 'Success') {
          this.spinner.hide();

          this.toastr.success(MessageConfig.Admin.SystemSetup.ProductSetup.Add, 'Success!');
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        } else {
          this.spinner.hide();

          this.toastr.error(MessageConfig.CommunicationError, 'Error!');
          this.productSetupForm.reset();
          this.submitted = false;
        }
      }, (err) => {
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');

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
      }, 500);
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

  downloadImage() {
    const linkSource = 'data:application/image;base64,' + this.base64Value;
    const downloadLink = document.createElement('a');
    const fileName = this.fileName;
    downloadLink.href = linkSource;
    downloadLink.download = fileName;
    downloadLink.click();
  }

  enableOtherSize(sizeObj) {
    let descriptionName = '';
    descriptionName = sizeObj[0].CodeValue;
    if (descriptionName === 'Other') {
      this.textDisplay = true;
      this.productSetupForm.patchValue({ other: this.selectedProduct.SizeDescription });
    }
  }
}
