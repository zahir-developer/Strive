import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { GetCodeService } from 'src/app/shared/services/data-service/getcode.service';
import { ProductService } from 'src/app/shared/services/data-service/product.service';
import * as moment from 'moment';

@Component({
  selector: 'app-create-edit-employee-hand-book',
  templateUrl: './create-edit-employee-hand-book.component.html',
  styleUrls: ['./create-edit-employee-hand-book.component.css']
})
export class CreateEditEmployeeHandBookComponent implements OnInit {

  handbookSetupForm: FormGroup;
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
  createdDate: any;
  headerName: string;
  constructor(private fb: FormBuilder,
     private toastr: ToastrService, private product: ProductService, private getCode: GetCodeService) { }

  ngOnInit() {
    if (localStorage.getItem('employeeName') !== undefined) {
      this.headerName = localStorage.getItem('employeeName');
    }    
    this.getAllVendor();
    this.Status = [{id : 0,Value :"Active"}, {id :1 , Value:"InActive"}];    
    this.formInitialize();
    this.isChecked = false;
    this.submitted = false;
    if (this.isEdit === true) {
      this.handbookSetupForm.reset();
      this.getProductById();
    }
  }

  formInitialize() {
    this.handbookSetupForm = this.fb.group({
      createdDate: [''],
      name: ['', Validators.required],
      createdName: [''],
      uploadBy:['',Validators.required]
    });
    this.handbookSetupForm.patchValue({status : 0}); 
  }
  
  // Get Size
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
  
  showText(data) {
    if (data === '33') {
      this.textDisplay = true;
      this.handbookSetupForm.get('other').setValidators([Validators.required]);
    } else {
      this.textDisplay = false;
      this.handbookSetupForm.get('other').clearValidators();
      this.handbookSetupForm.get('other').reset();
    }
  }
  // Get Product By Id
  getProductById() {
    this.product.getProductById(this.selectedData.ProductId).subscribe(data => {
      if (data.status === "Success") {
        const pType = JSON.parse(data.resultData);
        this.selectedProduct = pType.Product;
        this.handbookSetupForm.patchValue({
          createdDate: this.selectedProduct.createdDate,
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
        this.fileName= this.selectedProduct.FileName;
        this.fileUploadformData = this.selectedProduct.Base64;
        if (this.selectedProduct.Size === 33) {
          this.textDisplay = true;
          this.handbookSetupForm.controls['other'].patchValue(this.selectedProduct.SizeDescription);
        }
        this.change(this.selectedProduct.IsTaxable);
      } else {
        this.toastr.error('Communication Error', 'Error!');
      }
    });
  }

  get f() {
    return this.handbookSetupForm.controls;
  }

  change(data) {
    this.handbookSetupForm.value.taxable = data;
    if (data === true) {
      this.isChecked = true;
      this.handbookSetupForm.get('taxAmount').setValidators([Validators.required]);
    } else {
      this.isChecked = false;
      this.handbookSetupForm.get('taxAmount').clearValidators();
      this.handbookSetupForm.get('taxAmount').reset();
    }
  }

  // Add/Update Product
  submit() {
    this.submitted = true;
    if (this.handbookSetupForm.invalid) {
      return;
    }
    if(this.fileName === null){   
      return;
    }
    const formObj = {
      productCode: null,
      productDescription: null,
      createdDate: new Date(),
      productId: this.isEdit ? this.selectedProduct.ProductId : 0,
      locationId: this.handbookSetupForm.value.locationName,
      productName: this.handbookSetupForm.value.name,      
      fileName: this.fileName,
      thumbFileName: this.fileThumb,
      base64: this.fileUploadformData,
      cost: this.handbookSetupForm.value.cost,
      isTaxable: this.isChecked,
      taxAmount: this.isChecked ? this.handbookSetupForm.value.taxAmount : 0,
      size: this.handbookSetupForm.value.size,
      sizeDescription: this.textDisplay ? this.handbookSetupForm.value.other : null,
      quantity: this.handbookSetupForm.value.quantity,
      quantityDescription: null,
      isActive: this.handbookSetupForm.value.status == 0 ? true : false,
      vendorId: this.handbookSetupForm.value.vendor,
      thresholdLimit: this.handbookSetupForm.value.thresholdAmount,
      isDeleted: false,
    
      updatedDate: new Date(),
      price: this.handbookSetupForm.value.suggested
    };
    if (this.isEdit === true) {
      this.product.updateProduct(formObj).subscribe(data => {
        if (data.status === 'Success') {        
          this.toastr.success('Record Updated Successfully!!', 'Success!');
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        } else {
          this.toastr.error('Communication Error', 'Error!');
          this.handbookSetupForm.reset();
          this.submitted = false;
        }
      });
    } else {
      this.product.addProduct(formObj).subscribe(data => {
        if (data.status === 'Success') {        
          this.toastr.success('Record Saved Successfully!!', 'Success!');
          this.closeDialog.emit({ isOpenPopup: false, status: 'saved' });
        } else {
          this.toastr.error('Communication Error', 'Error!');
          this.handbookSetupForm.reset();
          this.submitted = false;
        }
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
      const DateCreated = fileToLoad.lastModifiedDate;   
      this.createdDate = moment(DateCreated).format('l');
    this.handbookSetupForm.controls['createdDate'].setValue(this.createdDate);
    this.handbookSetupForm.controls['createdName'].setValue(this.headerName);


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
        console.log(this.fileName,this.fileUploadformData.length);
      }, 5000);
    }
  }

  clearDocument() {
    this.fileName = null;
    this.fileThumb= null;
    this.fileUploadformData = null;
  this.handbookSetupForm.controls['createdDate'].setValue('');
  this.handbookSetupForm.controls['createdName'].setValue('');

  }
  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }
}

