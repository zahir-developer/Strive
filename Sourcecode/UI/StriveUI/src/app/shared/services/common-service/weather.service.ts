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

  constructor(private http: HttpUtilsService) {

    setTimeout(() => {
     this.getWeather();
    }, 1000);
  }
getWeather() {
  const locationId = 1;
  const date = moment(new Date()).format('MM-DD-YYYY');
  this.http.get(`${UrlConfig.totalUrl.getWeather}` + locationId + '/' + date).subscribe((data: any) => {
    this.weatherData = JSON.parse(data.resultData);
    this.data.next(this.weatherData.WeatherPrediction);
  });
}
UpdateWeather(obj){
  return this.http.post(`${UrlConfig.totalUrl.saveWeather}`, obj);
}
}
