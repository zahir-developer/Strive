import { Injectable } from '@angular/core';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';

@Injectable({
  providedIn: 'root'
})
export class StateService {

  constructor(private http: HttpUtilsService) { }
  getStatesList() {
    return this.http.get(`${UrlConfig.common.stateList}`);
  }

  getCityList(code) {
    return this.http.get(`${UrlConfig.common.cityList}` + code);
  }
  getCityByStateId(Id) {
    return this.http.get(`${UrlConfig.common.cityByStateId}`+  Id);
  }
}
