import { Component, OnInit } from '@angular/core';
import { GiftCardService } from 'src/app/shared/services/data-service/gift-card.service';

@Component({
  selector: 'app-gift-card',
  templateUrl: './gift-card.component.html',
  styleUrls: ['./gift-card.component.css']
})
export class GiftCardComponent implements OnInit {

  constructor(private giftCardService: GiftCardService) { }

  ngOnInit(): void {
    this.getAllGiftCard();
  }

  getAllGiftCard() {
    const locationId = 1;
    this.giftCardService.getAllGiftCard(locationId).subscribe( res => {
      
    });
  }

}
