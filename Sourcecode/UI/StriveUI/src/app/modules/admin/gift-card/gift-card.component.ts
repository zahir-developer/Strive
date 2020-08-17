import { Component, OnInit } from '@angular/core';
import { GiftCardService } from 'src/app/shared/services/data-service/gift-card.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbModalOptions, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AddGiftCardComponent } from '../gift-card/add-gift-card/add-gift-card.component';
import { AddActivityComponent } from '../gift-card/add-activity/add-activity.component';
import * as moment from 'moment';
import { ToastrService } from 'ngx-toastr';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';

@Component({
  selector: 'app-gift-card',
  templateUrl: './gift-card.component.html',
  styleUrls: ['./gift-card.component.css']
})
export class GiftCardComponent implements OnInit {
  giftCardForm: FormGroup;
  giftCardHistory: any = [];
  isActivity: boolean;
  activeDate: any = 'none';
  totalAmount: any = 0;
  submitted: boolean;
  giftCardID: any;
  constructor(
    private giftCardService: GiftCardService,
    private fb: FormBuilder,
    private modalService: NgbModal,
    private toastr: ToastrService,
    private messageService: MessageServiceToastr
  ) { }

  ngOnInit(): void {
    this.submitted = false;
    this.isActivity = false;
    this.giftCardForm = this.fb.group({
      number: ['', Validators.required]
    });
    this.getAllGiftCard();
  }

  getAllGiftCard() {
    const locationId = 1;
    this.giftCardService.getAllGiftCard(locationId).subscribe(res => {
      if (res.status === 'Success') {
        const giftcard = JSON.parse(res.resultData);
      }
    });
  }

  getAllGiftCardHistory(giftCardNumber) {
    this.giftCardService.getAllGiftCardHistory(giftCardNumber).subscribe(res => {
      if (res.status === 'Success') {
        const cardHistory = JSON.parse(res.resultData);
        this.giftCardHistory = cardHistory.GiftCardHistory;
      }
    });
  }

  get f() {
    return this.giftCardForm.controls;
  }

  getGiftCardDetail() {
    this.submitted = true;
    if (this.giftCardForm.invalid) {
      this.messageService.showMessage({ severity: 'warning', title: 'Warning', body: 'Please Enter Mandatory fields' });
      return;
    }
    const giftCardNumber = +this.giftCardForm.value.number;
    this.giftCardService.getGiftCard(giftCardNumber).subscribe(res => {
      if (res.status === 'Success') {
        const giftcardDetail = JSON.parse(res.resultData);
        if (giftcardDetail.GiftCardDetail.length > 0) {
          this.activeDate = moment(giftcardDetail.GiftCardDetail[0].ExpiryDate).format('YYYY-MM-DD');
          this.totalAmount = giftcardDetail.GiftCardDetail[0].TotalAmount;
          this.giftCardID = giftcardDetail.GiftCardDetail[0].GiftCardId;
          this.isActivity = true;
          this.updateBalance();
          this.getAllGiftCardHistory(giftCardNumber);
        } else {
          this.messageService.showMessage({ severity: 'info', title: 'Information', body: 'Invalid Card Number' });
          this.isActivity = false;
          this.activeDate = 'none';
          this.totalAmount = 0;
        }
      } else {
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
      }
    });
  }

  addGiftCard() {
    const ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false,
      size: 'lg'
    };
    this.modalService.open(AddGiftCardComponent, ngbModalOptions);
  }

  statusUpdate(card) {
    const finalObj = {
      giftCardId: card.GiftCardId,
      isActive: card.IsActive ? false : true
    };
    this.giftCardService.updateStatus(finalObj).subscribe(res => {
      if (res.status === 'Success') {
        this.getAllGiftCardHistory(card.GiftCardId);
      }
    });
  }

  addActivity() {
    const ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false,
      size: 'lg'
    };
    const modalRef = this.modalService.open(AddActivityComponent, ngbModalOptions);
    modalRef.componentInstance.giftCardNumber = +this.giftCardForm.value.number;
    modalRef.componentInstance.activeDate = this.activeDate;
    modalRef.componentInstance.totalAmount = this.totalAmount;
    modalRef.componentInstance.giftCardId = this.giftCardID;
    modalRef.result.then((result) => {
      if (result) {
        this.getAllGiftCardHistory(+this.giftCardForm.value.number);
        this.updateBalance();
      }
    });
  }

  cancelActvity() {
    this.activeDate = 'none';
    this.totalAmount = 0;
    this.isActivity = false;
    this.giftCardForm.reset();
  }

  updateBalance() {
    this.giftCardService.getBalance(+this.giftCardForm.value.number).subscribe(res => {
      if (res.status === 'Success') {
        const giftcardBalance = JSON.parse(res.resultData);
        if (giftcardBalance.GiftCardDetail.length > 0) {
          const balanceAmount = giftcardBalance.GiftCardDetail[0].BalaceAmount;
          if (balanceAmount > 0.00) {
            this.totalAmount = balanceAmount;
          }
        }
      }
    });
  }

}
