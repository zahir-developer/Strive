import { Injectable } from '@angular/core';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';
import { Subject, BehaviorSubject } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import * as moment from 'moment';

@Injectable({
  providedIn: 'root'
})
export class WeatherService {
  public data: BehaviorSubject<string> = new BehaviorSubject('');
  weatherData: any;
  weatherWeekData: any;
  weatherMonthData: any;

  constructor(private http: HttpUtilsService) {

    setTimeout(() => {
      if (localStorage.getItem('isAuthenticated')?.toString() === 'true') {
        this.getWeather();
      }
    }, 1000);
  }
  getWeather() {
    const locationId = localStorage.getItem('empLocationId');
    if (locationId != null) {
      this.http.get(`${UrlConfig.weather.getWeather}` + locationId).subscribe((data: any) => {
        this.weatherData = data;
        this.weatherWeekData = data?.lastWeekWeather;
        this.weatherMonthData = data?.lastMonthWeather;

        this.data.next(this.weatherData);



      });
    }

  }
  UpdateWeather(obj) {
    return this.http.post(`${UrlConfig.weather.saveWeather}`, obj);
  }
  getTargetBusinessData(locationId, dateTime) {
    locationId = localStorage.getItem('empLocationId');

    return this.http.get(`${UrlConfig.weather.getTargetBusinessData}` + locationId + '/' + dateTime);
  }
}
