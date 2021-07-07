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
import { ApplicationConfig } from 'src/app/shared/services/ApplicationConfig';

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
  productVendorList: any = [];
  venderGroup: any;
  productList = [];
  vendorList = [];

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
    this.Status = [{ id: 1, Value: "Active" }, { id: 0, Value: "Inactive" }];
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
      locationName: [[], Validators.required],
      name: ['', Validators.required],
      size: ['',],
      quantity: [0, Validators.required],
      cost: ['', Validators.required],
      taxable: ['',],
      taxAmount: ['',],
      status: ['',],
      vendor: ['',],
      thresholdAmount: ['',],
      other: ['',],
      suggested: ['', Validators.required],
      type: ['']
    });
    this.productSetupForm.patchValue({ status: 1 });
    if (this.isEdit !== true) {
      this.productSetupForm.controls.status.disable();
    } else {
      this.productSetupForm.controls.status.enable();
    }
    this.getSize();
  }
  // Get ProductType
  getProductType() {

    const prodTypeCodes = this.codeService.getCodeValueByType(ApplicationConfig.CodeValueByType.ProductType);
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
            item_id: item.LocationId,
            item_text: item.LocationName
          };
        });
        this.dropdownSetting();
      }
    });
  }

  dropdownSetting() {
    this.dropdownSettings = {
      singleSelection: ApplicationConfig.dropdownSettings.singleSelection,
      defaultOpen: ApplicationConfig.dropdownSettings.defaultOpen,
      idField: ApplicationConfig.dropdownSettings.idField,
      textField: ApplicationConfig.dropdownSettings.textField,
      itemsShowLimit: ApplicationConfig.dropdownSettings.itemsShowLimit,
      enableCheckAll: ApplicationConfig.dropdownSettings.enableCheckAll,
      allowSearchFilter: ApplicationConfig.dropdownSettings.allowSearchFilter
    };
  }

  // Get Size
  getSize() {
    const sizeCodes = this.codeService.getCodeValueByType(ApplicationConfig.CodeValueByType.Size);
    if (sizeCodes.length > 0) {
      this.size = sizeCodes;
    } else {
      this.getCode.getCodeByCategory(ApplicationConfig.CodeValueByType.Size).subscribe(res => {
        if (res.status === 'Success') {
          const code = JSON.parse(res.resultData);
          this.size = code.Codes;
        }
      });
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
            item_id: item.VendorId,
            item_text: item.VendorName
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
    const size = this.size.filter(item => item.CodeValue === ApplicationConfig.CodeValue.Other);
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
    this.product.getProductDetailById(this.selectedData.ProductId).subscribe(data => {
      if (data.status === "Success") {
        this.spinner.hide();
        const pType = JSON.parse(data.resultData);
        this.selectedProduct = pType.Product.ProductDetail;
        const Vendors = pType.Product.ProductVendor;
        let name = '';

        //location 
        const selectedLocation = [];
        const locObj = {
          item_id: this.selectedProduct.LocationId,
          item_text: this.selectedProduct.LocationName,
        };



        selectedLocation.push(locObj);

        //Vendor
        const selectedVendors = [];

        if (Vendors !== null) {
          Vendors.forEach(item => {
            selectedVendors.push(
              {
                vendorId: item.VendorId,
                productId: item.ProductId,
                productVendorId: item.ProductVendorId,
                isDeleted: item.IsDeleted,
                item_id: item.VendorId,
                item_text: item.VendorName
              });

            this.productVendorList.push(
              {
                ProductVendorId: item.ProductVendorId,
                ProductId: item.ProductId,
                VendorId: item.VendorId,
                IsActive: true,
                IsDeleted: false
              });
          });
        }

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
          status: this.selectedProduct.IsActive ? 1 : 0,
          vendor: selectedVendors,
          thresholdAmount: this.selectedProduct.ThresholdLimit
        });

        this.fileName = this.selectedProduct.FileName;
        this.base64Value = this.selectedProduct.Base64;
        this.fileUploadformData = this.selectedProduct.Base64;

        if (this.selectedProduct.Size !== null) {
          this.sizeId = this.selectedProduct.Size;
          if (this.size !== undefined) {
            const sizeObj = this.size.filter(item => item.CodeId === this.selectedProduct.Size);
            if (sizeObj !== null) {
              this.enableOtherSize(sizeObj);
            }
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

  onVendorDeSelect(vendor) {
    if (this.productVendorList.length > 0) {
      this.productVendorList.forEach(item => {
        if (item.VendorId === vendor.item_id) {
          item.IsDeleted = true;
        }
      });
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


    const obj: any = {};
    const productObj: any = {};
    this.productSetupForm.controls.status.enable();
    if (this.productSetupForm.value.locationName || this.productSetupForm.value.vendor) {
      this.productList = [];
      this.vendorList = [];

      if (this.productSetupForm.value.vendor.length !== 0) {
        this.productSetupForm.value.vendor.forEach(vendor => {
          this.vendorList.push({
            productVendorId: this.isEdit && vendor.productVendorId !== undefined ? vendor.productVendorId : 0,
            productId: this.isEdit ? this.selectedProduct.ProductId : 0,
            vendorId: vendor.item_id,
            isActive: true,
            isDeleted: false,
          });
        });
        if (this.productVendorList.length > 0) {
          const deletedVendors = this.productVendorList.filter(s => s.IsDeleted === true);

          if (deletedVendors.length > 0) {
            deletedVendors.forEach(vendor => {
              this.vendorList.push(vendor);
            });
          }
        }
      }

      for (let i = 0; i < this.productSetupForm.value.locationName.length; i++) {
        const product = {
          "product": {
            "productCode": null,
            "productDescription": null,
            "productType": this.productSetupForm.value.productType,
            "productId": this.isEdit ? this.selectedProduct.ProductId : 0,
            "locationId": this.productSetupForm.value.locationName[i].item_id,
            "productName": this.productSetupForm.value.name,
            "fileName": this.fileName,
            "OriginalFileName": this.fileName,
            "thumbFileName": this.fileThumb,
            "base64": this.fileUploadformData,
            "cost": this.productSetupForm.value.cost,
            "isTaxable": this.isChecked,
            "taxAmount": this.isChecked ? this.productSetupForm.value.taxAmount : 0,
            "size": this.productSetupForm.value.size,
            "sizeDescription": this.textDisplay ? this.productSetupForm.value.other : null,
            "quantity": this.productSetupForm.value.quantity,
            "quantityDescription": null,
            "isActive": +this.productSetupForm.value.status === 1 ? true : false,
            "thresholdLimit": this.productSetupForm.value.thresholdAmount,
            "isDeleted": false,
            "price": this.productSetupForm.value.suggested
          },
          "productVendor": this.vendorList
        }
        this.productList.push(product);
      }
    }

    const finalObj = {
       Product: this.productList
    };
    console.log(finalObj, 'new object');
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

  settingType(event) {
    const type = event.target.value;
    if (type === 'plus') {
      const quantity = this.productSetupForm.value.quantity;
      this.productSetupForm.patchValue({
        quantity: quantity + 1
      });
    } else {
      const quantity = this.productSetupForm.value.quantity;
      if (+quantity !== 0) {
        this.productSetupForm.patchValue({
          quantity: quantity - 1
        });
      }
    }
  }
}
