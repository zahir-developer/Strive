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
  updateServiceSetup(obj) {
    return this.http.post(`${UrlConfig.totalUrl.updateServiceSetup}`, obj);
  }
  deleteServiceSetup(id : number){
      return this.http.delete(`${UrlConfig.totalUrl.deleteServiceSetup}`+ id);
  }
  getServiceSetupById(id : number){
    return this.http.get(`${UrlConfig.totalUrl.getServiceSetupById}`+ id);
}
}