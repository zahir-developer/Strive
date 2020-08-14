import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';

@Injectable({
  providedIn: 'root'
})
export class MembershipService {

  constructor(private http: HttpUtilsService) { }

  getMembership(): Observable<any> {
    return this.http.get(`${UrlConfig.totalUrl.getAllMembership}`);
  }
  addMembership(obj) {
    return this.http.post(`${UrlConfig.totalUrl.addMembership}`, obj);
  }
  updateMembership(obj) {
    return this.http.post(`${UrlConfig.totalUrl.updateMembership}`, obj);
  } 
  deleteMembership(id : number){
    return this.http.delete(`${UrlConfig.totalUrl.deleteMembership}` + id);
  }
  getMembershipById(id : number){
    return this.http.get(`${UrlConfig.totalUrl.getMembershipById}` + id);
  } 
  getMembershipService(): Observable<any> {
    return this.http.get(`${UrlConfig.totalUrl.getMembershipService}`);
  }
  getMembershipVehicle(): Observable<any> {
    return this.http.get(`${UrlConfig.totalUrl.getAllVehicle}`);
  }
}
