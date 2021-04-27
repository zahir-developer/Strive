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

  getTenantList() {
    return this.http.get(`${UrlConfig.tenantSetup.getTenantSetupList}`);
  }

  getTenantDetailById(id) {
    return this.http.get(`${UrlConfig.tenantSetup.getTenantDetailById}` + id);
  }
}
