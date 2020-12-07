import { Component, Input, OnInit } from '@angular/core';
import { WeatherService } from '../../services/common-service/weather.service';

@Component({
  selector: 'app-last-week',
  templateUrl: './last-week.component.html',
  styleUrls: ['./last-week.component.css']
})
export class LastWeekComponent implements OnInit {
  weatherdata: any;
  @Input() targetBusiness : any;
    constructor(private weatherService: WeatherService) { }
  
    ngOnInit(): void {
      this.getWeatherDetails();
    }
    getWeatherDetails ()  {
    if(this.targetBusiness.WeatherPrediction.WeatherPredictionOneWeek){
        this.weatherdata = this.targetBusiness.WeatherPrediction.WeatherPredictionOneWeek;
      }  else {
          this.weatherdata = {
            'WashCount' : 0,
            'RainProbability': '-',
            'TargetBusiness' : '-'
          }
  }
  }
}