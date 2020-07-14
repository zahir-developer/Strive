import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { CrudOperationService } from 'src/app/shared/services/crud-operation.service';

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
  isChecked: boolean;
  @Output() closeDialog = new EventEmitter();
  @Input() selectedData?: any;
  @Input() isEdit?: any;
  submitted: boolean;
  constructor(private fb: FormBuilder, private toastr: ToastrService,private crudService: CrudOperationService) { }

  ngOnInit() {

    this.productType=["Merchandize","Food and Beverages","Inventory Items"];
    this.size=["S","M","L","XL"];
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
      thresholdAmount: ['',]
    });
    this.submitted = false;
    if (this.selectedData !== undefined && this.selectedData.length !== 0) {
      this.productSetupForm.reset();
      this.productSetupForm.setValue({
        productType: this.selectedData.ProductType,
        locationName: this.selectedData.LocationName,
        name: this.selectedData.Name,
        cost: this.selectedData.Cost,
        taxable: this.selectedData.Taxable,
        taxAmount: this.selectedData.TaxAmount,
        size: this.selectedData.Size,
        quantity: this.selectedData.Quantity,
        status: this.selectedData.Status,
        vendor: this.selectedData.Vendor,
        thresholdAmount: this.selectedData.ThresholdAmount        
      });
    }
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
      productType: this.productSetupForm.value.productType,
      productId: 1,
      locationName: this.productSetupForm.value.locationName,
      name: this.productSetupForm.value.name,
      cost: this.productSetupForm.value.cost,
      taxable: this.productSetupForm.value.taxable,
      taxAmount: this.productSetupForm.value.taxAmount,
      size: this.productSetupForm.value.size,
      quantity: this.productSetupForm.value.quantity,
      status: this.productSetupForm.value.status,
      vendor: this.productSetupForm.value.vendor,
      thresholdAmount: this.productSetupForm.value.thresholdAmount
    };
    sourceObj.push(formObj);
    this.crudService.productsetupdetails.push(formObj);
        if (this.isEdit === true) {
          this.toastr.success('Record Updated Successfully!!', 'Success!');
        } else {
          this.toastr.success('Record Saved Successfully!!', 'Success!');
        }
  }
  cancel() {
    this.closeDialog.emit({ isOpenPopup: false, status: 'unsaved' });
  }
}
