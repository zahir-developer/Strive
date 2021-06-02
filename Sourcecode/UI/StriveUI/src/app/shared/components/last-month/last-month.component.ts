import { Component, Input, OnInit } from '@angular/core';
import { WeatherService } from '../../services/common-service/weather.service';

@Component({
  selector: 'app-last-month',
  templateUrl: './last-month.component.html'
})
export class LastMonthComponent implements OnInit {
  weatherMonth: any;
  @Input() targetBusiness?: any;
  constructor() { }

  ngOnInit(): void {
    this.getWeatherDetails();
  }
  getWeatherDetails() {
    this.weatherMonth = this.targetBusiness.WeatherPrediction.WeatherPredictionLastMonth;
  }
}
