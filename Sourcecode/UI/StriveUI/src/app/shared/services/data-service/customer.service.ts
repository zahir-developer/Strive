import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private http: HttpUtilsService) { }

  getDailySalesReport(obj){
    return this.http.post(`${UrlConfig.reports.getDailySalesReport}`, obj);
  }

  getLocation(): Observable<any> {
    return this.http.get(`${UrlConfig.totalUrl.getLocation}`);
  }

  getServices(): Observable<any> {
    return this.http.get(`${UrlConfig.totalUrl.getServiceSetup}`);
  }

  getVehicleByClientId(clientId: number) {
    return this.http.get(`${UrlConfig.totalUrl.getVehicleByClientId}`, { params: { id: clientId } });
  }

  getAvailablilityScheduleTime(obj) {
    return this.http.post(`${UrlConfig.dashboard.getAvailablilityScheduleTime}`, obj);
  }

  getWashTimeByLocationId(id) {
    return this.http.get(`${UrlConfig.totalUrl.getLocationById}`, { params: { id }});
  }
}
