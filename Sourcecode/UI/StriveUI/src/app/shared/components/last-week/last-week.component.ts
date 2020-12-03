import { Component, OnInit } from '@angular/core';
import { WeatherService } from '../../services/common-service/weather.service';

@Component({
  selector: 'app-last-week',
  templateUrl: './last-week.component.html',
  styleUrls: ['./last-week.component.css']
})
export class LastWeekComponent implements OnInit {
  weatherdata: any;

  constructor(private weatherService: WeatherService) { }

  ngOnInit(): void {
    this.getWeatherDetails();
  }
  getWeatherDetails = () => {
    this.weatherService.data.subscribe((x:any)=> {
      this.weatherdata = x.lastWeekWeather;
  });
}
}
