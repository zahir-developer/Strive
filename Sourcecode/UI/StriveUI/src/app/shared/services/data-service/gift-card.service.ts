import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';

@Injectable({
  providedIn: 'root'
})
export class GiftCardService {

  constructor(private http: HttpUtilsService) { }

  getAllGiftCard(obj): Observable<any> {
    return this.http.post(`${UrlConfig.giftCard.getAllGiftCard}` , obj );
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
}
