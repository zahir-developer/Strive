import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';


@Injectable({
  providedIn: 'root'
})
export class VehicleService {
  addVehicle: object;
  vehicleValue: any;

  constructor(private http: HttpUtilsService) { }
  getVehicle(obj) {
    return this.http.post(`${UrlConfig.vehicle.getAllVehicle}`, obj);
  }
  updateVehicle(obj) {
    return this.http.post(`${UrlConfig.vehicle.updateVehicle}`, obj);
  }
  saveVehicle(obj) {
    return this.http.post(`${UrlConfig.vehicle.addVehicle}`, obj);
  }
  deleteVehicle(vehicleId: number) {
    return this.http.delete(`${UrlConfig.vehicle.deleteVehicle}`, { params: { id: vehicleId } });
  }
  getVehicleByClientId(clientId: number) {
    return this.http.get(`${UrlConfig.vehicle.getVehicleByClientId}`, { params: { id: clientId } });
  }
  getVehicleById(vehicleId: number) {
    return this.http.get(`${UrlConfig.vehicle.getVehicleById}`, { params: { id: vehicleId } });
  }
  getVehicleMembership(locId: number) {
    return this.http.get(`${UrlConfig.MembershipSetup.getMembershipByName}` + locId);
  }
  getVehicleCodes() {
    return this.http.post(`${UrlConfig.vehicle.getVehicleCodes}`);
  }
  getMembershipService(): Observable<any> {
    return this.http.get(`${UrlConfig.MembershipSetup.getMembershipService}`);
  }
  getMembershipById(id: number) {
    return this.http.get(`${UrlConfig.MembershipSetup.getMembershipById}` + id);
  }
  getUpchargeService(locationId): Observable<any> {
    return this.http.get(`${UrlConfig.ServiceSetup.getAllServiceDetail}`, { params: { locationId } });
  }
  getVehicleMembershipDetailsByVehicleId(id) {
    return this.http.get(`${UrlConfig.vehicle.getVehicleMembershipDetailsByVehicleId}`, { params: { id } });
  }

  getAllVehicleThumbnail(id) {
    return this.http.get(`${UrlConfig.vehicle.getAllVehicleThumbnail}` + id);
  }

  getVehicleImageById(id) {
    return this.http.get(`${UrlConfig.vehicle.getVehicleImageById}` + id);
  }

  GetMembershipDiscountStatus(clientId) {
    return this.http.get(`${UrlConfig.vehicle.getMembershipDiscountStatus}` + clientId);
  }
}
