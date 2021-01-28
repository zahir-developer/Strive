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
    return this.http.post(`${UrlConfig.totalUrl.getAllWash}` , obj);
  }
  updateWashes(obj) {
    return this.http.post(`${UrlConfig.totalUrl.updateWash}`, obj);
  }
  getJobStatus(code) {
    return this.http.get(`${UrlConfig.totalUrl.getJobStatus}` + code);
  }
  addWashes(obj) {
    return this.http.post(`${UrlConfig.totalUrl.addWash}`, obj);
  }
  getWashById(id: number) {
    return this.http.get(`${UrlConfig.totalUrl.getWashById}` + id);
  }
  getByBarcode(id: number) {
    return this.http.get(`${UrlConfig.totalUrl.getByBarcode}` + id);
  }
  deleteWash(washId: number) {
    return this.http.delete(`${UrlConfig.totalUrl.deleteWash}`, { params: { id: washId } });
  }
  getServices(): Observable<any> {
    return this.http.get(`${UrlConfig.totalUrl.getServiceSetup}`);
  }
  getServiceType(obj: string) {
    return this.http.get(`${UrlConfig.totalUrl.getCode}` + obj);
  }
  getVehicleColor() {
    return this.http.post(`${UrlConfig.totalUrl.getVehicleCodes}`);
  }
  getTicketNumber(): Observable<any> {
    return this.http.get(`${UrlConfig.totalUrl.getTicketNumber}`);
  }
  getJobType() {
    return this.http.get(`${UrlConfig.totalUrl.getJobType}`);
  }
  getMembership(vehicleId: number) {
    return this.http.get(`${UrlConfig.totalUrl.getMembershipByVehicle}`, { params: { id: vehicleId } });
  }
  getAllClient(): Observable<any> {
    return this.http.get(`${UrlConfig.totalUrl.getClient}`);
  }
  getAllClientName(): Observable<any> {
    return this.http.get(`${UrlConfig.totalUrl.getClientName}`,name);
  }
  getAllClients(name): Observable<any> {
    return this.http.get(`${UrlConfig.totalUrl.getClientName}`+ name,{ params: { name: name } });
  }
  getVehicleByClientId(clientId: number) {
    return this.http.get(`${UrlConfig.totalUrl.getVehicleByClientId}`, { params: { id: clientId } });
  }
  getMembershipById(id: number) {
    return this.http.get(`${UrlConfig.totalUrl.getMembershipById}` + id);
  }
  getVehicleById(vehicleId: number) {
      return this.http.get(`${UrlConfig.totalUrl.getVehicleById}`, { params: { id: vehicleId } });
  }

  // Get Dashboard Count
  getDashBoard(obj) {
    this.http.post(`${UrlConfig.totalUrl.getDashBoardCount}`, obj).subscribe((data: any) => {
      const wash = JSON.parse(data.resultData);
      this.dashBoardDetails = wash.Dashboard;
      this.data.next(this.dashBoardDetails);
    });
  }
}
