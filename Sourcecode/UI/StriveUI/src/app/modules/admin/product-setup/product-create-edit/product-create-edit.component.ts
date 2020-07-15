import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { CrudOperationService } from 'src/app/shared/services/crud-operation.service';
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
  productType:any;
  size:any;
  Status:any;
  Vendor:any;
  locationName:any;
  isChecked: boolean;
  @Output() closeDialog = new EventEmitter();
  @Input() selectedData?: any;
  @Input() isEdit?: any;
  submitted: boolean;
  selectedProduct: void;
  textDisplay: boolean;
  constructor(private fb: FormBuilder, private toastr: ToastrService,private locationService: LocationService,private product: ProductService,private getCode: GetCodeService) { }

  ngOnInit() {

    this.getProductType();
    this.getAllLocation();
    this.Status=["Active","InActive"];
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
      other: ['', Validators.required]
    });
    this.submitted = false;
    if (this.isEdit === true) {
      this.productSetupForm.reset();
      this.getProductById();
    }
  }

  getProductType(){
    const globeCode = {globalCode:"PRODUCTTYPE"};
    this.getCode.getCodeByCategory("PRODUCTTYPE").subscribe(data =>{
      if(data.status === "Success"){
        const pType= JSON.parse(data.resultData);
        this.productType=pType.Codes;
      }else{
        this.toastr.error('Communication Error','Error!');
      }
    });
    this.getSize();
  }
  getSize(){
    this.getCode.getCodeByCategory("SIZE").subscribe(data =>{
      if(data.status === "Success"){
        const pSize= JSON.parse(data.resultData);
        this.size=pSize.Codes;
      }else{
        this.toastr.error('Communication Error','Error!');
      }
    });
  }

  getAllLocation() {
    this.locationService.getLocation().subscribe(data => {
      if (data.status === 'Success') {
        const location = JSON.parse(data.resultData);
        this.locationName = location.Location;
      }else{
        this.toastr.error('Communication Error','Error!');
      }
    });
  }

  showText(data){
    if(data === 'other'){
      this.textDisplay = true;
    }else{
      this.textDisplay = false;
    }
  }

  getProductById(){
    this.product.getProductById(this.selectedData.ProductId).subscribe(data =>{
      if(data.status === "Success"){
        const pType= JSON.parse(data.resultData);
        this.selectedProduct = pType.Product[0];
      this.productSetupForm.patchValue({
        productType: this.selectedData.ProductType,
        //locationName: this.selectedData.LocationName,
        name: this.selectedData.ProductName,
        cost: this.selectedData.Cost,
        taxable: this.selectedData.IsTaxable,
        taxAmount: this.selectedData.TaxAmount,
        size: this.selectedData.Size,
        quantity: this.selectedData.Quantity,
        status: this.selectedData.IsActive ? "Active" : "InActive",
        //vendor: this.selectedData.Vendor,
        thresholdAmount: this.selectedData.ThresholdLimit        
      });
    }
  });
  }

  get f(){
    return this.productSetupForm.controls;
  }

  change(data){
    this.productSetupForm.value.taxable = data;
    if(data === true){
      this.isChecked = true;
    }else{
      this.isChecked = false;
    }
  }
  submit() {
    this.submitted = true;
    if(this.productSetupForm.invalid){
      return;
    }
    const sourceObj = [];
    const formObj = {
      productCode:1,
      productDescription:null,
      productType: this.productSetupForm.value.productType,
      productId: 1,
      locationId: 1,
      productName: this.productSetupForm.value.name,
      cost: this.productSetupForm.value.cost,
      isTaxable: this.productSetupForm.value.taxable,
      taxAmount: this.productSetupForm.value.taxAmount,
      size: this.textDisplay ? this.productSetupForm.value.other : this.productSetupForm.value.size,
      sizeDescription:null,
      quantity: this.productSetupForm.value.quantity,
      quantityDescription:null,
      isActive: this.productSetupForm.value.status === "Active" ? true : false,
      vendorId: 0,
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
    }else{
      this.toastr.error('Communication Error','Error!');
      this.productSetupForm.reset();
      this.submitted=false;
    }
  });
  }
  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }
}
