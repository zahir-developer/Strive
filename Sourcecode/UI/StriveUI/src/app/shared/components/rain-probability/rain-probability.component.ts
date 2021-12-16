import { Component, OnInit } from '@angular/core';
import { WeatherService } from '../../services/common-service/weather.service';
import { ToastrService } from 'ngx-toastr';
import { MessageConfig } from '../../services/messageConfig';

@Component({
  selector: 'app-rain-probability',
  templateUrl: './rain-probability.component.html'
})
export class RainProbabilityComponent implements OnInit {
  rainPrediction: any;

  constructor(private weatherService: WeatherService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.getWeatherDetails();
    
  }
  getWeatherDetails = () => {
    this.weatherService.data.subscribe((data: any) => {
      if (data !== undefined) {
      this.rainPrediction = data.currentWeather?.rainPercentage;
      }
  }, (err) => {
    this.toastr.error(MessageConfig.CommunicationError, 'Error!');
  });
}
}