import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';


@Injectable({
    providedIn: 'root'
})
export class VehicleService {
    addVehicle: object;
    vehicleValue: object;

    constructor(private http: HttpUtilsService) { }
    getVehicle(): Observable<any> {
        return this.http.get(`${UrlConfig.totalUrl.getAllVehicle}`);
    }
    updateVehicle(obj) {
        return this.http.post(`${UrlConfig.totalUrl.updateVehicle}`, obj);
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
        return this.http.get(`${UrlConfig.totalUrl.getVehicleMembership}`);
    }
    getVehicleCodes() {
        return this.http.post(`${UrlConfig.totalUrl.getVehicleCodes}`);
    }
    getMembershipService(): Observable<any> {
      return this.http.get(`${UrlConfig.totalUrl.getMembershipService}`);
    }
}