import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { SalesService } from 'src/app/shared/services/data-service/sales.service';
import { Message } from '@angular/compiler/src/i18n/i18n_ast';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';

@Component({
  selector: 'app-edit-item',
  templateUrl: './edit-item.component.html',
  styleUrls: ['./edit-item.component.css']
})
export class EditItemComponent implements OnInit {
  editItemForm: FormGroup;
  @Input() ItemDetail: any;
  @Input() JobId: any;
  constructor(private fb: FormBuilder, private activeModal: NgbActiveModal, private salesService: SalesService,
    private messageService: MessageServiceToastr) { }

  ngOnInit(): void {
    console.log(this.ItemDetail);
    this.formInit();
    if (this.ItemDetail !== undefined) {
      this.editItemForm.patchValue({
        itemName: this.ItemDetail.ServiceName,
        quantity: this.ItemDetail.Quantity, price: this.ItemDetail.Price
      });
    }
  }
  formInit() {
    this.editItemForm = this.fb.group({
      itemName: [''],
      quantity: ['', Validators.required],
      price: ['', Validators.required]
    });
  }
  closeModal() {
    this.activeModal.close();
  }
  saveItem() {
    if (this.editItemForm.invalid) {
      return;
    }
    const updateObj = {
      jobItemId: this.ItemDetail?.JobItemId ? this.ItemDetail?.JobItemId : 0,
      serviceId: this.ItemDetail?.ServiceId ? this.ItemDetail?.ServiceId : 0,
      quantity: this.editItemForm.value.quantity,
      price: +this.editItemForm.value.price
    };
    const updateProductObj = [{
      jobProductItemId: this.ItemDetail?.JobProductItemId ? this.ItemDetail?.JobProductItemId : 0,
      jobId: this.ItemDetail?.JobId ? this.ItemDetail?.JobId : 0,
      productId: this.ItemDetail?.ProductId ? this.ItemDetail?.ProductId : 0,
      commission: 0,
      price: +this.editItemForm.value.price,
      quantity: this.editItemForm.value.quantity,
      reviewNote: null,
      isActive: true,
      isDeleted: false,
      createdBy: 0,
      createdDate: new Date(),
      updatedBy: 0,
      updatedDate: new Date()
    }];
    if (this.ItemDetail?.ProductId) {
      const productObj = {
        jobProductItem: updateProductObj
      };
      this.updateProductItem(productObj);
    } else {
      this.updateServiceItem(updateObj);
    }
  }
  updateServiceItem(updateObj) {
    this.salesService.updateItem(updateObj).subscribe(data => {
      console.log(data);
      if (data.status === 'Success') {
        this.messageService.showMessage({ severity: 'success', title: 'Success', body: 'Item upated successfully' });
        this.activeModal.close();
      } else {
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    }, (err) => {
      this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
    });
  }
  updateProductItem(updateObj) {
    this.salesService.updateProductItem(updateObj).subscribe(data => {
      if (data.status === 'Success') {
        this.messageService.showMessage({ severity: 'success', title: 'Success', body: 'Item upated successfully' });
        this.activeModal.close();
      } else {
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    }, (err) => {
      this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
    });
  }
}
