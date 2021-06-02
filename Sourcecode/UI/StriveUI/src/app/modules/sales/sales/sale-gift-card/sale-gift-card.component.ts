import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import * as moment from 'moment';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { GiftCardService } from 'src/app/shared/services/data-service/gift-card.service';
import { SalesService } from 'src/app/shared/services/data-service/sales.service';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { WashService } from 'src/app/shared/services/data-service/wash.service';

@Component({
  selector: 'app-sale-gift-card',
  templateUrl: './sale-gift-card.component.html'
})
export class SaleGiftCardComponent implements OnInit {
  giftCardForm: FormGroup;
  amountList: any = [];
  isOtherAmount: boolean;
  submitted: boolean;
  @Input() ItemDetail?: any;
  clientList: any;
  clientId: any;
  GiftcardNumberExist: any;
  constructor(
    private activeModal: NgbActiveModal,
    private fb: FormBuilder,
    private salesService: SalesService,
    private toastr: ToastrService,
    private giftCardService: GiftCardService,
    private spinner: NgxSpinnerService,
    private wash: WashService,
    private messageService: MessageServiceToastr
  ) { }

  ngOnInit(): void {
    this.submitted = false;
    this.isOtherAmount = false;
    this.giftCardForm = this.fb.group({
      number: ['', Validators.required],
      activeDate: ['', Validators.required],
      amount: ['', Validators.required],
      others: [''],
      clientId: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]]
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
    const giftCardObj = {
      status: false
    };
    this.activeModal.close(giftCardObj);
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
    this.giftCardExist(this.giftCardForm.value.number);
  }

  filterClient(event) {
    const filtered: any[] = [];
    const query = event.query;
    this.wash.getAllClients(query).subscribe(res => {
      if (res.status === 'Success') {
        const client = JSON.parse(res.resultData);
        client.ClientName.forEach(item => {
          item.fullName = item.FirstName + ' ' + item.LastName;
        });
        this.clientList = client.ClientName.map(item => {
          return {
            id: item.ClientId,
            name: item.fullName
          };
        });
      } else {
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: MessageConfig.CommunicationError });
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');

    });
  }
  selectedClient(event) {
    this.clientId = event.id;
  }

  save() {
    this.submitted = true;
    if (this.giftCardForm.invalid) {
      this.toastr.warning(MessageConfig.Mandatory, 'Warning!');
      return;
    }
    if (this.GiftcardNumberExist === true) {
      this.toastr.warning(MessageConfig.Admin.GiftCard.GiftCardAlreadyExists, 'Warning!');
      return;

    }
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
    this.spinner.show();
    this.salesService.addListItem(formObj).subscribe(data => {
      if (data.status === 'Success') {
        this.spinner.hide();
        this.submitted = false;
        this.saveGiftCard();
      } else {
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }
    }, (err) => {
      this.spinner.hide();
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
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
      updatedDate: moment(new Date()),
      clientId: this.clientId,
      email: this.giftCardForm.value.email,
    };
    const finalObj = {
      giftCard: cardObj
    };
    this.spinner.show();
    this.giftCardService.saveGiftCard(finalObj).subscribe(res => {
      this.spinner.hide();
      if (res.status === 'Success') {
        const card = JSON.parse(res.resultData);
        const giftCardObj = {
          status: true,
          cardId: card.Status,
          cardNumber: this.giftCardForm.value.number
        };
        this.activeModal.close(giftCardObj);
        this.toastr.success(MessageConfig.Sales.UpdateGiftCrd, 'Success!');
      } else {
        this.spinner.hide();
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        this.giftCardForm.reset();
      }
    }, (err) => {
      this.spinner.hide();
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  giftCardExist(event) {
    this.giftCardService.GiftCardAlreadyExists(event).subscribe(res => {
      if (res.status === 'Success') {
        const GiftcardNumber = JSON.parse(res.resultData);
        this.GiftcardNumberExist = GiftcardNumber.IsGiftCardAvailable;
        if (this.GiftcardNumberExist === true) {
          this.toastr.warning(MessageConfig.Admin.GiftCard.GiftCardAlreadyExists, 'Warning!');
          this.giftCardForm.patchValue({
            number: ''
          });
        }
        else {
          this.giftCardForm.patchValue({
            number: event
          });
        }
      } else {
        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      }

    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });

  }

}
