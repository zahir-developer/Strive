import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { SalesService } from 'src/app/shared/services/data-service/sales.service';

@Component({
  selector: 'app-sale-gift-card',
  templateUrl: './sale-gift-card.component.html',
  styleUrls: ['./sale-gift-card.component.css']
})
export class SaleGiftCardComponent implements OnInit {
  giftCardForm: FormGroup;
  amountList: any = [];
  isOtherAmount: boolean;
  submitted: boolean;
  @Input() ItemDetail?: any;
  constructor(
    private activeModal: NgbActiveModal,
    private fb: FormBuilder,
    private salesService: SalesService,
    private messageService: MessageServiceToastr
  ) { }

  ngOnInit(): void {
    this.submitted = false;
    this.isOtherAmount = false;
    this.giftCardForm = this.fb.group({
      number: ['', Validators.required],
      activeDate: ['', Validators.required],
      amount: ['', Validators.required],
      others: ['']
    });
    this.amountList = [
      {
        label: '$10',
        value: 10
      },
      {
        label: '$25',
        value: 25
      },
      {
        label: '$50',
        value: 50
      },
      {
        label: '$100',
        value: 100
      },
      {
        label: 'Other',
        value: 0
      },
    ];
  }

  closeModal() {
    this.activeModal.close();
  }

  selectedAmount(event) {
    if (+event.target.value === 0) {
      this.isOtherAmount = true;
    } else {
      this.isOtherAmount = false;
    }
  }

  get f() {
    return this.giftCardForm.controls;
  }

  generateNumber() {
    const cardNumber = Math.floor(100000 + Math.random() * 900000);
    this.giftCardForm.patchValue({
      number: cardNumber
    });
  }

  save() {
    this.submitted = true;
    const formObj = {
      job: {
        jobId: 0,
        ticketNumber: this.ItemDetail.ticketNumber.toString(),
        locationId: +localStorage.getItem('empLocationId'),
        clientId: null,
        vehicleId: null,
        make: 0,
        model: 0,
        color: 0,
        jobType: 1,
        jobDate: new Date(),
        timeIn: new Date(),
        estimatedTimeOut: new Date(),
        actualTimeOut: new Date(),
        jobStatus: 1,
        isActive: true,
        isDeleted: false,
        createdBy: 1,
        createdDate: new Date(),
        updatedBy: 1,
        updatedDate: new Date(),
        notes: ''
      },
      jobItem: [{
        jobItemId: 0,
        jobId: 0,
        serviceId: this.ItemDetail.selectedService?.id,
        // itemTypeId: this.selectedService.type === 'product' ? 6 : 3,
        commission: 0,
        price: this.isOtherAmount ? this.giftCardForm.value.others : this.giftCardForm.value.amount,
        quantity: +this.ItemDetail.quantity,
        reviewNote: null,
        isActive: true,
        isDeleted: false,
        createdBy: 1,
        createdDate: new Date(),
        updatedBy: 1,
        updatedDate: new Date(),
        employeeId: +localStorage.getItem('empId')
      }],
      JobProductItem: {
        jobProductItemId: 0,
        jobId: 0,
        productId: this.ItemDetail.selectedService?.id,
        commission: 0,
        price: this.isOtherAmount ? this.giftCardForm.value.others : this.giftCardForm.value.amount,
        quantity: +this.ItemDetail.quantity,
        reviewNote: null,
        isActive: true,
        isDeleted: false,
        createdBy: 1,
        createdDate: new Date(),
        updatedBy: 1,
        updatedDate: new Date()
      }
    };
    if (this.ItemDetail.selectedService.type === 'service') {
      formObj.JobProductItem = null;
    } else {
      formObj.jobItem = null;
    }
    this.salesService.addItem(formObj).subscribe(data => {
      if (data.status === 'Success') {
        this.submitted = false;
        this.activeModal.close(true);
        // this.messageService.showMessage({ severity: 'success', title: 'Success', body: 'Item added successfully' });
        // this.isSelected = true;
        // this.ticketNumber = this.newTicketNumber;
        // this.getDetailByTicket(false);
        // this.addItemForm.controls.quantity.enable();
        // this.addItemFormInit();
        // this.submitted = false;
      } else {
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }

}
