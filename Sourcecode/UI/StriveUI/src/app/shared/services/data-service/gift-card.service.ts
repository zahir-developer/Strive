import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';

@Injectable({
  providedIn: 'root'
})
export class GiftCardService {

  constructor(private http: HttpUtilsService) { }

  getAllGiftCard(locationId): Observable<any> {
    return this.http.get(`${UrlConfig.totalUrl.getAllGiftCard}` + locationId);
  }
  getAllGiftCardHistory(giftCardId) {
    return this.http.get(`${UrlConfig.totalUrl.getAllGiftCardHistory}` + giftCardId);
  }
  getGiftCard(giftCardId) {
    return this.http.get(`${UrlConfig.totalUrl.getGiftCard}` + giftCardId);
  }
  saveGiftCard(obj) {
    return this.http.post(`${UrlConfig.totalUrl.saveGiftCard}` , obj);
  }
  updateStatus(obj) {
    return this.http.post(`${UrlConfig.totalUrl.updateStatus}` , obj);
  }
  addCardHistory(obj) {
    return this.http.post(`${UrlConfig.totalUrl.addCardHistory}` , obj);
  }
  updateBalance(giftCardId) {
    return this.http.post(`${UrlConfig.totalUrl.updateBalance}`, null, { params : { giftCardId }});
  }
  getBalance(giftCardNumber) {
    return this.http.get(`${UrlConfig.totalUrl.getBalance}` + giftCardNumber);
  }
}
