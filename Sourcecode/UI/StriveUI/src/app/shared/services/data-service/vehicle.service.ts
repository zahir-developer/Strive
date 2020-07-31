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
}