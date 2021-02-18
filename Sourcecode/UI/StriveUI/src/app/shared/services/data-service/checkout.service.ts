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
    return this.http.get(`${UrlConfig.checkOut.getUncheckedVehicleDetails}` + locationId);
  }

  checkoutVehicle(obj) {
    return this.http.post(`${UrlConfig.checkOut.checkoutVehicle}`, obj);
  }

  holdVehicle(obj) {
    return this.http.post(`${UrlConfig.checkOut.holdoutVehicle}`, obj);
  }

  completedVehicle(obj) {
    return this.http.post(`${UrlConfig.checkOut.completedVehicle}`, obj);
  }
}
