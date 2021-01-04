import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import * as moment from 'moment';
import { GiftCardService } from 'src/app/shared/services/data-service/gift-card.service';
import { ToastrService } from 'ngx-toastr';
import { MessageServiceToastr } from 'src/app/shared/services/common-service/message.service';
import { Router } from '@angular/router';
import { GiftCardComponent } from '../gift-card.component';

@Component({
  selector: 'app-add-gift-card',
  templateUrl: './add-gift-card.component.html',
  styleUrls: ['./add-gift-card.component.css']
})
export class AddGiftCardComponent implements OnInit {
  giftCardForm: FormGroup;
  amountList: any = [];
  isOtherAmount: boolean;
  submitted: boolean;
  constructor(
    private activeModal: NgbActiveModal,
    private fb: FormBuilder,
    private giftCardService: GiftCardService,
    private toastr: ToastrService,
    
    private giftCardComponent :GiftCardComponent
    private messageService: MessageServiceToastr,
    private router: Router
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

  saveGiftCard() {
  
    this.submitted = true;
    if (this.giftCardForm.invalid) {
      this.messageService.showMessage({ severity: 'warning', title: 'Warning', body: 'Please Enter Mandatory fields' });
      return;
    }
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
    // this.giftCardComponent.getAllGiftCard(x=> x.)==finalObj.giftCard.giftCardCode
    
    // this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Giftcard code alredy exist' });
    this.giftCardService.saveGiftCard(finalObj).subscribe(res => {
      if (res.status === 'Success') {
        this.messageService.showMessage({ severity: 'success', title: 'Success', body: 'Gift Card Added Successfully!!' });
        this.activeModal.close(true);
        this.router.navigate(['/admin/gift-card']);
      } else {
        this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
        this.giftCardForm.reset();
      }
    });
  }

  generateNumber() {
    const cardNumber = Math.floor(100000 + Math.random() * 900000);
    this.giftCardForm.patchValue({
      number: cardNumber
    });
  }

}
