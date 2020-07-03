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
    return this.http.get(`${UrlConfig.totalUrl.getLocation}`);
  }
  updateLocation(obj) {
    return this.http.post(`${UrlConfig.totalUrl.updateLocation}`, obj);
  }
  deleteLocation(id : number){
      return this.http.delete(`${UrlConfig.totalUrl.deleteLocation}`+ id);
  }
  getLocationById(id : number){
    return this.http.get(`${UrlConfig.totalUrl.getLocationById}`+ id);
}
}