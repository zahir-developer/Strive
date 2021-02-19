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
    return this.http.get(`${UrlConfig.AdSetup.getadSetup}`);
  }
  getAdType(): Observable<any> {
    return this.http.get(`${UrlConfig.ServiceSetup.getServiceType}`);
  }
  addAdSetup(obj) {
    return this.http.post(`${UrlConfig.AdSetup.insertadSetup}`, obj);
  }  
  updateAdSetup(obj) {
    return this.http.post(`${UrlConfig.AdSetup.updateadSetup}`, obj);
  }
  deleteAdSetup(id) {
    return this.http.delete(`${UrlConfig.AdSetup.deleteadSetup}` , { params: { id: id } });
  }
  getAdSetupById(id: number) {
    return this.http.get(`${UrlConfig.AdSetup.getByIdadSetup}`, { params: { id: id } });
  }
  
}