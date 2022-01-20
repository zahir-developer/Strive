import { Injectable } from '@angular/core';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';

@Injectable({
  providedIn: 'root'
})
export class MakeService {
  constructor(private http: HttpUtilsService) { }
  

  VehicleCopy() {
    return this.http.get(`${UrlConfig.common.getVehiclePrint}`);
  }

  CustomerCopy() {
    return this.http.get(`${UrlConfig.common.getCustomerPrint}`);
  }

}
