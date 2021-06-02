import { Component, Input, OnInit } from '@angular/core';
import { WeatherService } from '../../services/common-service/weather.service';

@Component({
  selector: 'app-last-week',
  templateUrl: './last-week.component.html'
})
export class LastWeekComponent implements OnInit {
  weatherdata: any;
  @Input() targetBusiness?: any;
  constructor(private weatherService: WeatherService) { }

  ngOnInit(): void {
    this.getWeatherDetails();
  }
  getWeatherDetails() {
    this.weatherdata = this.targetBusiness?.WeatherPrediction?.WeatherPredictionLastWeek;


  }
}