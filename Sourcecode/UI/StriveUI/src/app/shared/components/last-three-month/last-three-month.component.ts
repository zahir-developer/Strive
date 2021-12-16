import { Component, Input, OnInit } from '@angular/core';
import { WeatherService } from '../../services/common-service/weather.service';

@Component({
  selector: 'app-last-three-month',
  templateUrl: './last-three-month.component.html'
})
export class LastThreeMonthComponent implements OnInit {

  weatherThreeMonth: any;
@Input() targetBusiness?: any;
  constructor(private weatherService: WeatherService) { }

  ngOnInit(): void {
    this.getWeatherDetails();
  }
  getWeatherDetails()  {
      this.weatherThreeMonth = this.targetBusiness.WeatherPrediction.WeatherPredictionLastThirdMonth;
  }
}
