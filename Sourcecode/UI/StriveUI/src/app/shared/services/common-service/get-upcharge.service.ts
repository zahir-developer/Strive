import { Injectable } from '@angular/core';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';

@Injectable({
  providedIn: 'root'
})
export class GetUpchargeService {

  constructor(private http: HttpUtilsService) { }
  

  getUpcharge(obj) {
    return this.http.post(`${UrlConfig.common.getUpchargeType}`, obj);
  }
}
