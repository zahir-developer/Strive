import { Injectable } from '@angular/core';
import { HttpUtilsService } from '../../util/http-utils.service';
import { Observable, BehaviorSubject } from 'rxjs';
import { UrlConfig } from '../url.config';

@Injectable({
  providedIn: 'root'
})
export class WashService {
  private data: BehaviorSubject<string> = new BehaviorSubject('');
  dashBoardDetails: any;
  public dashBoardData = this.data.asObservable();
  constructor(private http: HttpUtilsService) {
  }
  getAllWashes(obj): Observable<any> {
    return this.http.post(`${UrlConfig.washes.getAllWash}` , obj);
  }
  updateWashes(obj) {
    return this.http.post(`${UrlConfig.washes.updateWash}`, obj);
  }
  getJobStatus(code) {
    return this.http.get(`${UrlConfig.common.getJobStatus}` + code);
  }
  addWashes(obj) {
    return this.http.post(`${UrlConfig.washes.addWash}`, obj);
  }
  getWashById(id: number) {
    return this.http.get(`${UrlConfig.washes.getWashById}` + id);
  }
  getByBarcode(id: number) {
    return this.http.get(`${UrlConfig.washes.getByBarcode}` + id);
  }
  deleteWash(washId: number) {
    return this.http.delete(`${UrlConfig.washes.deleteWash}`, { params: { id: washId } });
  }
  getServices(obj): Observable<any> {
    return this.http.post(`${UrlConfig.ServiceSetup.getServiceSetup}`, obj);
  }
  getServiceType(obj: string) {
    return this.http.get(`${UrlConfig.common.getCode}` + obj);
  }
  getVehicleColor() {
    return this.http.post(`${UrlConfig.vehicle.getVehicleCodes}`);
  }
  getTicketNumber(): Observable<any> {
 const locationId = localStorage.getItem('empLocationId');

    return this.http.get(`${UrlConfig.common.getTicketNumber}`+locationId);
  }
  getJobType() {
    return this.http.get(`${UrlConfig.details.getJobType}`);
  }
  getMembership(vehicleId: number) {
    return this.http.get(`${UrlConfig.vehicle.getVehicleMembershipDetailsByVehicleId}`, { params: { id: vehicleId } });
  }
  getAllClient(): Observable<any> {
    return this.http.get(`${UrlConfig.client.getClient}`);
  }
  getAllClientName(): Observable<any> {
    return this.http.get(`${UrlConfig.client.getClientName}`,name);
  }
  getAllClients(name): Observable<any> {
    return this.http.get(`${UrlConfig.client.getClientName}`+ name,{ params: { name: name } });
  }
  getVehicleByClientId(clientId: number) {
    return this.http.get(`${UrlConfig.vehicle.getVehicleByClientId}`, { params: { id: clientId } });
  }
  getMembershipById(id: number) {
    return this.http.get(`${UrlConfig.MembershipSetup.getMembershipById}` + id);
  }
  getVehicleById(vehicleId: number) {
      return this.http.get(`${UrlConfig.vehicle.getVehicleById}`, { params: { id: vehicleId } });
  }

  // Get Dashboard Count
  getDashBoard(obj) {
    this.http.post(`${UrlConfig.washes.getDashBoardCount}`, obj).subscribe((data: any) => {
      const wash = JSON.parse(data.resultData);
      this.dashBoardDetails = wash.Dashboard;
      this.data.next(this.dashBoardDetails);
    });
  }
}
