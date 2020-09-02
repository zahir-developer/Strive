import { Injectable } from '@angular/core';
import { HttpUtilsService } from '../../util/http-utils.service';
import { Observable, BehaviorSubject } from 'rxjs';
import { UrlConfig } from '../url.config';

@Injectable({
  providedIn: 'root'
})
export class WashService {
  public data: BehaviorSubject<string> = new BehaviorSubject('');
  dashBoardData: any;

  constructor(private http: HttpUtilsService) {
    setTimeout(() => {
      this.getDashBoard();
    }, 1000);
  }
  getAllWashes(): Observable<any> {
    return this.http.get(`${UrlConfig.totalUrl.getAllWash}`);
  }
  updateWashes(obj) {
    return this.http.post(`${UrlConfig.totalUrl.updateWash}`, obj);
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
  getMembership(vehicleId: number) {
    return this.http.get(`${UrlConfig.totalUrl.getMembershipByVehicle}`, { params: { id: vehicleId } });
  }
  getAllClient(): Observable<any> {
    return this.http.get(`${UrlConfig.totalUrl.getClient}`);
  }
  getVehicleByClientId(clientId: number) {
    return this.http.get(`${UrlConfig.totalUrl.getVehicleByClientId}`, { params: { id: clientId } });
  }
  getMembershipById(id: number) {
    return this.http.get(`${UrlConfig.totalUrl.getMembershipById}` + id);
  }

  // Get Dashboard Count
  getDashBoard() {
    const obj = {
      id: 1,
      date: new Date()
    };
    this.http.post(`${UrlConfig.totalUrl.getDashBoardCount}`, obj).subscribe((data: any) => {
      const wash = JSON.parse(data.resultData);
      this.dashBoardData = wash.Dashboard;
      this.data.next(this.dashBoardData);
      console.log(this.data);
    });
  }
}
