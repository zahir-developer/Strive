import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';


@Injectable({
  providedIn: 'root'
})
export class ServiceSetupService {

  constructor(private http: HttpUtilsService) { }
  getServiceSetup(obj): Observable<any> {
    return this.http.post(`${UrlConfig.ServiceSetup.getServiceSetup}`, obj);
  }
  getServiceType(): Observable<any> {
    return this.http.get(`${UrlConfig.ServiceSetup.getServiceType}`);
  }
  addServiceSetup(obj) {
    return this.http.post(`${UrlConfig.ServiceSetup.addServiceSetup}`, obj);
  }
  updateServiceSetup(obj) {
    return this.http.post(`${UrlConfig.ServiceSetup.updateServiceSetup}`, obj);
  }
  deleteServiceSetup(serviceId: number) {
    return this.http.delete(`${UrlConfig.ServiceSetup.deleteServiceSetup}`, { params: { id: serviceId } });
  }
  getServiceSetupById(serviceId: number) {
    return this.http.get(`${UrlConfig.ServiceSetup.getServiceSetupById}`, { params: { id: serviceId } });
  }
  ServiceSearch(obj) {
    return this.http.post(`${UrlConfig.ServiceSetup.getServiceSearch}`, obj);
  }

  getAllServiceDetail(locationId: any) {
    return this.http.get(`${UrlConfig.ServiceSetup.getAllServiceDetail}`, { params: { locationId } });
  }
}