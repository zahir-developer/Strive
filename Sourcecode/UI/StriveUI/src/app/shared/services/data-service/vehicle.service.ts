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
    getVehicle(obj){
        return this.http.post(`${UrlConfig.totalUrl.getAllVehicle}`, obj);
    }
    updateVehicle(obj) {
        return this.http.post(`${UrlConfig.totalUrl.updateVehicle}`, obj);
    }
    saveVehicle(obj) {
        return this.http.post(`${UrlConfig.totalUrl.addVehicle}`, obj);
    }
    deleteVehicle(vehicleId: number) {
        return this.http.delete(`${UrlConfig.totalUrl.deleteVehicle}`, { params: { id: vehicleId } });
    }
    getVehicleByClientId(clientId: number) {
        return this.http.get(`${UrlConfig.totalUrl.getVehicleByClientId}`, { params: { id: clientId } });
    }
    getVehicleById(vehicleId: number) {
        return this.http.get(`${UrlConfig.totalUrl.getVehicleById}`, { params: { id: vehicleId } });
    }
    getVehicleMembership(): Observable<any> {
        return this.http.get(`${UrlConfig.totalUrl.getAllMembership}`);
    }
    getVehicleCodes() {
        return this.http.post(`${UrlConfig.totalUrl.getVehicleCodes}`);
    }
    getMembershipService(): Observable<any> {
      return this.http.get(`${UrlConfig.totalUrl.getMembershipService}`);
    }
    getMembershipById(id : number){
      return this.http.get(`${UrlConfig.totalUrl.getMembershipById}` + id);
    }    
    getUpchargeService(): Observable<any> {
      return this.http.get(`${UrlConfig.totalUrl.getServiceSetup}`);
    }
    getVehicleMembershipDetailsByVehicleId(id) {
        return this.http.get(`${UrlConfig.totalUrl.getVehicleMembershipDetailsByVehicleId}`, { params : { id }});
    }
}
