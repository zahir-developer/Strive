import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {

  constructor(private http : HttpUtilsService) { }

  getUncheckedVehicleDetails(locationId): Observable<any> {
    return this.http.get(`${UrlConfig.totalUrl.getUncheckedVehicleDetails}` + locationId);
  }

  checkoutVehicle(obj) {
    return this.http.post(`${UrlConfig.totalUrl.checkoutVehicle}`, obj);
  }

  holdVehicle(obj) {
    return this.http.post(`${UrlConfig.totalUrl.holdoutVehicle}`, obj);
  }

  completedVehicle(obj) {
    return this.http.post(`${UrlConfig.totalUrl.completedVehicle}`, obj);
  }
}
