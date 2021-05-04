import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';

@Injectable({
  providedIn: 'root'
})
export class GiftCardService {

  constructor(private http: HttpUtilsService) { }
  GiftCardAlreadyExists(cardNumber){
    return this.http.get(`${UrlConfig.giftCard.giftCardExist}` + cardNumber,{ params: { giftCardCode: cardNumber } });

  }
  getAllGiftCard(obj): Observable<any> {
    return this.http.post(`${UrlConfig.giftCard.getAllGiftCard}` , obj );
  }
  getGiftCardHistoryByTicketNumber(cardNumber) {
    return this.http.get(`${UrlConfig.giftCard.getGiftCardHistoryByTicketNmber}`  + cardNumber,{ params: { giftCardCode: cardNumber } });
  }
  getAllGiftCardHistory(giftCardId) {
    return this.http.get(`${UrlConfig.giftCard.getAllGiftCardHistory}` + giftCardId);
  }
  getGiftCard(giftCardId) {
    return this.http.get(`${UrlConfig.giftCard.getGiftCard}` + giftCardId);
  }
  saveGiftCard(obj) {
    return this.http.post(`${UrlConfig.giftCard.saveGiftCard}` , obj);
  }
  updateStatus(obj) {
    return this.http.post(`${UrlConfig.giftCard.updateStatus}` , obj);
  }
  addCardHistory(obj) {
    return this.http.post(`${UrlConfig.giftCard.addCardHistory}` , obj);
  }
  updateBalance(giftCardId) {
    return this.http.post(`${UrlConfig.giftCard.updateBalance}`, null, { params : { giftCardId }});
  }
  getBalance(giftCardNumber) {
    return this.http.get(`${UrlConfig.giftCard.getBalance}` + giftCardNumber);
  }
  deleteGiftCard(id) {
    return this.http.delete(`${UrlConfig.giftCard.deleteGiftCard}` , { params : { id }} );
  }
}
