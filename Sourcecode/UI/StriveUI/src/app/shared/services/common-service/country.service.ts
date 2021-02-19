import { Injectable } from '@angular/core';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';

@Injectable({
  providedIn: 'root'
})
export class CountryService {

  constructor(private http: HttpUtilsService) { }
  getCountriesList() {
    return this.http.get(`${UrlConfig.common.countryList}`);
  }
}
