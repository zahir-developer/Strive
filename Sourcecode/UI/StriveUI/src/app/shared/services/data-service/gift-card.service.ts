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
}
