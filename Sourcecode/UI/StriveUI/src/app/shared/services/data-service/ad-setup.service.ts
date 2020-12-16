import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';

@Injectable({
  providedIn: 'root'
})
export class AdSetupService {

  constructor(private http: HttpUtilsService) { }
  getAdSetup(): Observable<any> {
    return this.http.get(`${UrlConfig.totalUrl.getServiceSetup}`);
  }
  getAdType(): Observable<any> {
    return this.http.get(`${UrlConfig.totalUrl.getServiceType}`);
  }
  addAdSetup(obj) {
    return this.http.post(`${UrlConfig.totalUrl.addServiceSetup}`, obj);
  }
  updateAdSetup(obj) {
    return this.http.post(`${UrlConfig.totalUrl.updateServiceSetup}`, obj);
  }
  deleteAdSetup(serviceId: number) {
    return this.http.delete(`${UrlConfig.totalUrl.deleteServiceSetup}`, { params: { id: serviceId } });
  }
  getAdSetupById(serviceId: number) {
    return this.http.get(`${UrlConfig.totalUrl.getServiceSetupById}`, { params: { id: serviceId } });
  }
  
}