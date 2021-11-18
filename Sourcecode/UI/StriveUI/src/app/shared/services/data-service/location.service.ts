import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';


@Injectable({
  providedIn: 'root'
})
export class LocationService {

  constructor(private http: HttpUtilsService) { }
  getLocation(): Observable<any> {
    return this.http.get(`${UrlConfig.location.getLocation}`);
  }
  saveLocation(obj) {
    return this.http.post(`${UrlConfig.location.saveLocation}`, obj);
  }
  deleteLocation(locId: number) {
    return this.http.delete(`${UrlConfig.location.deleteLocation}`, { params: { id: locId } });
  }
  getLocationById(locId: number) {
    return this.http.get(`${UrlConfig.location.getLocationById}`, { params: { id: locId } });
  }
  updateLocation(obj) {
    return this.http.post(`${UrlConfig.location.updateLocation}`, obj);
  }
  LocationSearch(obj) {
    return this.http.post(`${UrlConfig.location.getLocationSearch}`, obj);
  }
  getMerchantSearch(obj) {
    return this.http.post(`${UrlConfig.location.getMerchantDetails}`, obj);
  }
}
