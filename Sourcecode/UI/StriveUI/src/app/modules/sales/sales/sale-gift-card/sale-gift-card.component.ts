import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import * as moment from 'moment';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { GiftCardService } from 'src/app/shared/services/data-service/gift-card.service';
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
    private messageService: MessageServiceToastr,
    private giftCardService: GiftCardService
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
        jobType: null,
        jobDate: new Date(),
        timeIn: new Date(),
        estimatedTimeOut: new Date(),
        actualTimeOut: new Date(),
        jobStatus: null,
        isActive: true,
        isDeleted: false,
        createdBy: null,
        createdDate: new Date(),
        updatedBy: null,
        updatedDate: new Date(),
        notes: null
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
        createdBy: null,
        createdDate: new Date(),
        updatedBy: null,
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
        createdBy: null,
        createdDate: new Date(),
        updatedBy: null,
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
        this.saveGiftCard();
        this.activeModal.close(true);
      } else {
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }

  saveGiftCard() {
    const cardObj = {
      giftCardId: 0,
      locationId: +localStorage.getItem('empLocationId'),
      giftCardCode: this.giftCardForm.value.number,
      giftCardName: 'string',
      expiryDate: moment(this.giftCardForm.value.activeDate),
      comments: 'string',
      isActive: true,
      isDeleted: false,
      totalAmount: this.isOtherAmount ? this.giftCardForm.value.others : this.giftCardForm.value.amount,
      createdBy: 0,
      createdDate: moment(new Date()),
      updatedBy: 0,
      updatedDate: moment(new Date())
    };
    const finalObj = {
      giftCard: cardObj
    };
    this.giftCardService.saveGiftCard(finalObj).subscribe(res => {
      if (res.status === 'Success') {
      } else {
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
        this.giftCardForm.reset();
      }
    });
  }

}
