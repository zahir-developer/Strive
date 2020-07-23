import { Injectable } from '@angular/core';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';

@Injectable({
  providedIn: 'root'
})
export class WeatherService {

  constructor(private http: HttpUtilsService) { }
  getWeather() {
    return this.http.post(`${UrlConfig.totalUrl.getWeather}`);
  }
}
