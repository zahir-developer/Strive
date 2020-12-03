import { Component, OnInit } from '@angular/core';
import { WeatherService } from '../../services/common-service/weather.service';

@Component({
  selector: 'app-last-month',
  templateUrl: './last-month.component.html',
  styleUrls: ['./last-month.component.css']
})
export class LastMonthComponent implements OnInit {
  weatherMonth: any;

  constructor(private weatherService: WeatherService) { }

  ngOnInit(): void {
    this.getWeatherDetails();
  }
  getWeatherDetails = () => {
    this.weatherService.data.subscribe((data: any) => {
      this.weatherMonth = data.lastMonthWeather;
  });
}
}
