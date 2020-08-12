import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import * as moment from 'moment';
import { GiftCardService } from 'src/app/shared/services/data-service/gift-card.service';

@Component({
  selector: 'app-add-gift-card',
  templateUrl: './add-gift-card.component.html',
  styleUrls: ['./add-gift-card.component.css']
})
export class AddGiftCardComponent implements OnInit {
  giftCardForm: FormGroup;
  amountList: any = [];
  isOtherAmount: boolean;
  constructor(private activeModal: NgbActiveModal, private fb: FormBuilder, private giftCardService: GiftCardService) { }

  ngOnInit(): void {
    this.isOtherAmount = false;
    this.giftCardForm = this.fb.group({
      number: [''],
      activeDate: [''],
      amount: [''],
      others: ['']
    });
    this.amountList = [ // $10, $25, $50 or $100 
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
    console.log(event, 'amount');
    if (+event.target.value === 0) {
      this.isOtherAmount = true;
    } else {
      this.isOtherAmount = false;
    }
  }

  saveGiftCard() {
    const cardObj = {
      giftCardId: 0,
      locationId: 1,
      giftCardCode: 'string',
      giftCardName: 'string',
      expiryDate: moment(this.giftCardForm.value.activeDate),
      comments: 'string',
      isActive: true,
      isDeleted: false,
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
        this.activeModal.close();
      }
    });
  }

}
