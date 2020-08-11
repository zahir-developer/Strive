import { Component, OnInit } from '@angular/core';
import { GiftCardService } from 'src/app/shared/services/data-service/gift-card.service';
import { FormGroup, FormBuilder } from '@angular/forms';
import { NgbModalOptions, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AddGiftCardComponent } from '../gift-card/add-gift-card/add-gift-card.component';

@Component({
  selector: 'app-gift-card',
  templateUrl: './gift-card.component.html',
  styleUrls: ['./gift-card.component.css']
})
export class GiftCardComponent implements OnInit {
  giftCardForm: FormGroup;
  giftCardHistory: any = [];
  isActivity: boolean;
  constructor(private giftCardService: GiftCardService, private fb: FormBuilder, private modalService: NgbModal) { }

  ngOnInit(): void {
    this.isActivity = false;
    this.giftCardForm = this.fb.group({
      number: ['']
    });
    this.getAllGiftCard();
    // this.getAllGiftCardHistory();
    // this.getGiftCard();
  }

  getAllGiftCard() {
    const locationId = 1;
    this.giftCardService.getAllGiftCard(locationId).subscribe( res => {
      if (res.status === 'Success') {
        const giftcard = JSON.parse(res.resultData);
        console.log(giftcard);
      }
    });
  }

  getAllGiftCardHistory(giftId) {
    this.giftCardService.getAllGiftCardHistory(giftId).subscribe(res => {
      if (res.status === 'Success') {
        const cardHistory = JSON.parse(res.resultData);
        this.giftCardHistory = cardHistory.GiftCardHistory;
        console.log(cardHistory, 'giftcardHistory');
      }
    });
  }

  getGiftCardDetail() {
    const giftId = +this.giftCardForm.value.number;
    this.giftCardService.getGiftCard(giftId).subscribe(res => {
      if (res.status === 'Success') {
        const giftcardDetail = JSON.parse(res.resultData);
        console.log(giftcardDetail, 'giftcardDetail');
        this.isActivity = true;
        this.getAllGiftCardHistory(giftId);
      }
    });
  }

  addGiftCard() {
    const ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false,
      size: 'lg'
    };
    const modalRef = this.modalService.open(AddGiftCardComponent, ngbModalOptions);
  }

  statusUpdate(card) {
    const finalObj = {
      giftCardId: card.GiftCardId,
      isActive: card.IsActive ? false : true
    };
    this.giftCardService.updateStatus(finalObj).subscribe( res => {
      if (res.status === 'Success') {
        this.getAllGiftCardHistory(card.GiftCardId);
      }
    });
  }

}
