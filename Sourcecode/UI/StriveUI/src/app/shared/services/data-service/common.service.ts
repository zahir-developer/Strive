import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';

@Injectable({
  providedIn: 'root'
})
export class CommonService {

  constructor(private http: HttpUtilsService) { }

  getCustomerPrint(obj) : Observable<any> {
    return this.http.post(`${UrlConfig.common.getCustomerPrint}`, obj);
  }

  getVehicleCopy(obj) : Observable<any> {
    return this.http.post(`${UrlConfig.common.getVehiclePrint}`, obj);
  }
}
