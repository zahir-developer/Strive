import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';


@Injectable({
  providedIn: 'root'
})
export class ServiceSetupService {

  constructor(private http: HttpUtilsService) { }
  getServiceSetup(): Observable<any> {
    return this.http.get(`${UrlConfig.totalUrl.getServiceSetup}`);
  }
  getServiceType(): Observable<any> {
    return this.http.get(`${UrlConfig.totalUrl.getServiceType}`);
  }
  addServiceSetup(obj) {
    return this.http.post(`${UrlConfig.totalUrl.addServiceSetup}`, obj);
  }
  updateServiceSetup(obj) {
    return this.http.post(`${UrlConfig.totalUrl.updateServiceSetup}`, obj);
  }
  deleteServiceSetup(serviceId: number) {
    return this.http.delete(`${UrlConfig.totalUrl.deleteServiceSetup}`, { params: { id: serviceId } });
  }
  getServiceSetupById(serviceId: number) {
    return this.http.get(`${UrlConfig.totalUrl.getServiceSetupById}`, { params: { id: serviceId } });
  }
  ServiceSearch(obj) {
    return this.http.post(`${UrlConfig.totalUrl.getServiceSearch}`, obj);
  }
}