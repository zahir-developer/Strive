import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { WeatherService } from '../../services/common-service/weather.service';
import * as moment from 'moment';
@Component({
  selector: 'app-temperature',
  templateUrl: './temperature.component.html',
  styleUrls: ['./temperature.component.css']
})
export class TemperatureComponent implements OnInit {
@Output() weatherData = new EventEmitter();
  temperature: any;
  constructor(private weatherService: WeatherService) { }

  ngOnInit() {
    this.getWeatherDetails();
  }
  getWeatherDetails = () => {
    this.weatherService.data.subscribe((data: any) => {
      if (data !== undefined) {
      this.temperature = Math.floor(data?.currentWeather?.temporature);
      }
  });

  }
}
