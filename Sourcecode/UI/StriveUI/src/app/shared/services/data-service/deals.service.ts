import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';
@Injectable({
  providedIn: 'root'
})
export class DealsService {

  constructor(private http: HttpUtilsService) { }
  getDealSetup(): Observable<any> {
    return this.http.get(`${UrlConfig.DealSetup.getDealSetupSetup}`);
  }

  addDealsSetup(obj) {
    return this.http.post(`${UrlConfig.DealSetup.insertDealSetup}`, obj);
  }

  updateDeals(status) {
    return this.http.get(`${UrlConfig.DealSetup.updatedeals}`, { params : { status } });
  }

}
