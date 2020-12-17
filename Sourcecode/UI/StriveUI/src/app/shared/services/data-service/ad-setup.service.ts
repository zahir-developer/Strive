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
    return this.http.get(`${UrlConfig.totalUrl.getadSetup}`);
  }
  getAdType(): Observable<any> {
    return this.http.get(`${UrlConfig.totalUrl.getServiceType}`);
  }
  addAdSetup(obj) {
    return this.http.post(`${UrlConfig.totalUrl.insertadSetup}`, obj);
  }
  updateAdSetup(obj) {
    return this.http.post(`${UrlConfig.totalUrl.updateadSetup}`, obj);
  }
  deleteAdSetup(id: number) {
    return this.http.delete(`${UrlConfig.totalUrl.deleteadSetup}`, { params: { id: id } });
  }
  getAdSetupById(id: number) {
    return this.http.get(`${UrlConfig.totalUrl.getByIdadSetup}`, { params: { id: id } });
  }
  
}