import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';


@Injectable({
    providedIn: 'root'
})
export class VehicleService {
    addVehicle: any =[];

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
    getVehicleById(vehicleId: number) {
        return this.http.get(`${UrlConfig.totalUrl.getVehicleById}`, { params: { id: vehicleId } });
    }
    getVehicleId(vehicleId: number) {
        return this.http.get(`${UrlConfig.totalUrl.getVehicleId}`, { params: { id: vehicleId } });
    }
    getVehicleMembership(): Observable<any> {
        return this.http.get(`${UrlConfig.totalUrl.getVehicleMembership}`);
    }
    getVehicleColor(): Observable<any> {
        return this.http.get(`${UrlConfig.totalUrl.getVehicleColor}`);
    }
    getVehicleMake(): Observable<any> {
        return this.http.get(`${UrlConfig.totalUrl.getVehicleMake}`);
    }
    getVehicleModel(): Observable<any> {
        return this.http.get(`${UrlConfig.totalUrl.getVehicleModel}`);
    }
    getVehicleUpcharge(): Observable<any> {
        return this.http.get(`${UrlConfig.totalUrl.getVehicleUpcharge}`);
    }
}