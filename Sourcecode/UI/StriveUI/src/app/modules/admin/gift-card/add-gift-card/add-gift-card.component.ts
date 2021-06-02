import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import * as moment from 'moment';
import { GiftCardService } from 'src/app/shared/services/data-service/gift-card.service';
import { ToastrService } from 'ngx-toastr';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageConfig } from 'src/app/shared/services/messageConfig';
import { ClientService } from 'src/app/shared/services/data-service/client.service';
import { WashService } from 'src/app/shared/services/data-service/wash.service';

@Component({
  selector: 'app-add-gift-card',
  templateUrl: './add-gift-card.component.html',
  styles: [`
  .button-size {
    font-size: 15px !important;
}
  `]
})
export class AddGiftCardComponent implements OnInit {
  giftCardForm: FormGroup;
  amountList: any = [];
  clientList: any;
  clientId: any;
  isOtherAmount: boolean;
  submitted: boolean;
  GiftcardNumberExist: any;
  giftCardCode: any;
  constructor(
    private activeModal: NgbActiveModal,
    private fb: FormBuilder,
    private giftCardService: GiftCardService,
    private toastr: ToastrService,
    private messageService: MessageServiceToastr,
    private router: Router,
    private wash: WashService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.submitted = false;
    this.isOtherAmount = false;
    this.giftCardForm = this.fb.group({
      number: ['', Validators.required],
      activeDate: ['', Validators.required],
      amount: ['', Validators.required],
      others: [''],
      clientId: [''],
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
    this.activeModal.close(false);
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

  saveGiftCard() {
    this.submitted = true;
    if (this.giftCardForm.invalid) {
      this.toastr.warning(MessageConfig.Mandatory, 'Warning!');
      return;
    }
    if (this.GiftcardNumberExist === true) {
      this.toastr.warning(MessageConfig.Admin.GiftCard.GiftCardAlreadyExists, 'Warning!');
      return;

    }
    const cardObj = {
      giftCardId: 0,
      locationId: +localStorage.getItem('empLocationId'),
      giftCardCode: this.giftCardForm.value.number.toString(),
      giftCardName: null,
      activationDate: moment(this.giftCardForm.value.activeDate),
      comments: null,
      email: this.giftCardForm.value.email,
      isActive: true,
      isDeleted: false,
      totalAmount: this.isOtherAmount ? this.giftCardForm.value.others : this.giftCardForm.value.amount,
      createdBy: +localStorage.getItem('empId'),
      createdDate: moment(new Date()),
      updatedBy: +localStorage.getItem('empId'),
      updatedDate: moment(new Date()),
      clientId: this.clientId
    };
    const activityObj = {
      giftCardHistoryId: 0,
      giftCardId: 0,
      locationId: +localStorage.getItem('empLocationId'),
      transactionType: null,
      transactionAmount: this.isOtherAmount ? this.giftCardForm.value.others : this.giftCardForm.value.amount,
      transactionDate: moment(new Date()),
      comments: null,
      isActive: true,
      isDeleted: false,
      createdBy: +localStorage.getItem('empId'),
      createdDate: moment(new Date()),
      updatedBy: +localStorage.getItem('empId'),
      updatedDate: moment(new Date())
    };

    const finalObj = {
      giftCard: cardObj,
      giftCardHistory: activityObj

    };
    this.spinner.show();
    this.giftCardService.saveGiftCard(finalObj).subscribe(res => {
      if (res.status === 'Success') {
        this.spinner.hide();

        this.toastr.success(MessageConfig.Admin.GiftCard.Add, 'Success!');
        this.activeModal.close(+this.giftCardForm.value.number);
        this.router.navigate(['/admin/gift-card']);
      } else {
        this.spinner.hide();

        this.toastr.error(MessageConfig.CommunicationError, 'Error!');
        this.giftCardForm.reset();
      }
    }, (err) => {
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
      this.spinner.hide();
    });
  }

  generateNumber() {
    const cardNumber = Math.floor(100000 + Math.random() * 900000);
    this.giftCardForm.patchValue({
      number: cardNumber
    });
    this.giftCardExist(this.giftCardForm.value.number);
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
