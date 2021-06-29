import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';

@Injectable({
  providedIn: 'root'
})
export class TenantSetupService {

  constructor(private http: HttpUtilsService) { }

  getModuleList(): Observable<any> {
    return this.http.get(`${UrlConfig.tenantSetup.getModuleList}`);
  }

  addTenant(obj) {
    return this.http.post(`${UrlConfig.tenantSetup.addTenantSetup}`, obj);
  }

  getTenantList(obj) {
    return this.http.post(`${UrlConfig.tenantSetup.getTenantSetupList}`, obj);
  }

  getTenantDetailById(id) {
    return this.http.get(`${UrlConfig.tenantSetup.getTenantDetailById}` + id);
  }

  updateTenant(obj) {
    return this.http.post(`${UrlConfig.tenantSetup.updateTenant}`, obj);
  }

  getStateList() {
    return this.http.get(`${UrlConfig.tenantSetup.getStateList}`);
  }

  getCityByStateId(stateID) {
    return this.http.get(`${UrlConfig.tenantSetup.getCityByStateId}` + stateID);
  }
  
  getMaxLocationCount(id) {
    return this.http.get(`${UrlConfig.tenantSetup.getMaxLocationCount}` + id);
  }
}
