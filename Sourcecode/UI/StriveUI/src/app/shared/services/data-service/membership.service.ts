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
    return this.http.get(`${UrlConfig.MembershipSetup.getAllMembership}`);
  }
  addMembership(obj) {
    return this.http.post(`${UrlConfig.MembershipSetup.addMembership}`, obj);
  }
  deleteRestrictionMembershipVehicle(id){
    return this.http.get(`${UrlConfig.MembershipSetup.deleteRestrictionMembershipByVehicleId}` + id);
}
  updateMembership(obj) {
    return this.http.post(`${UrlConfig.MembershipSetup.updateMembership}`, obj);
  } 
  deleteMembership(id : number){
    return this.http.delete(`${UrlConfig.MembershipSetup.deleteMembership}` + id);
  }
  getMembershipById(id : number){
    return this.http.get(`${UrlConfig.MembershipSetup.getMembershipById}` + id);
  } 
  getMembershipService(): Observable<any> {
    return this.http.get(`${UrlConfig.MembershipSetup.getMembershipService}`);
  }
  searchMembership(obj) {
    return this.http.post(`${UrlConfig.MembershipSetup.membershipSearch}`, obj);
  }
}
