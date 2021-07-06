import { Injectable } from '@angular/core';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';

@Injectable({
  providedIn: 'root'
})
export class MakeService {
  constructor(private http: HttpUtilsService) { }
  

  getMake() {
    return this.http.get(`${UrlConfig.Auth.allmake}`);
  }

  getColor() {
    return this.http.get(`${UrlConfig.Auth.color}`);
  }
}
