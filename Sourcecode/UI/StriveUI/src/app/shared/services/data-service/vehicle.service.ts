import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';


@Injectable({
    providedIn: 'root'
})
export class VehicleService {

    constructor(private http: HttpUtilsService) { }
    getVehicle(): Observable<any> {
        return this.http.get(`${UrlConfig.totalUrl.getAllVehicle}`);
    }
    updateVehicle(obj) {
        return this.http.post(`${UrlConfig.totalUrl.updateVehicle}`, obj);
    }
    deleteVehicle(vehicleId: number) {
        return this.http.delete(`${UrlConfig.totalUrl.deleteVehicle}` , { params: { id: vehicleId } });
    }
    getVehicleById(vehicleId: number) {
        return this.http.get(`${UrlConfig.totalUrl.getVehicleById}` , { params: { id: vehicleId } });
    }
    getVehicleColor(): Observable<any> {
        return this.http.get(`${UrlConfig.totalUrl.getVehicleColor}`);
    }
}