import { Component, OnInit } from '@angular/core';
import { WeatherService } from '../../services/common-service/weather.service';

@Component({
  selector: 'app-rain-probability',
  templateUrl: './rain-probability.component.html',
  styleUrls: ['./rain-probability.component.css']
})
export class RainProbabilityComponent implements OnInit {
  rainPrediction: any;

  constructor(private weatherService: WeatherService) { }

  ngOnInit(): void {
    this.getWeatherDetails();
  }
  getWeatherDetails = () => {
    this.weatherService.data.subscribe((data: any) => {
      this.rainPrediction = data.RainProbability;
  });
}
}