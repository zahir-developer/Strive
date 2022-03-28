import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { WeatherService } from '../../services/common-service/weather.service';
import * as moment from 'moment';
import { ToastrService } from 'ngx-toastr';
import { MessageConfig } from '../../services/messageConfig';
@Component({
  selector: 'app-temperature',
  templateUrl: './temperature.component.html'
})
export class TemperatureComponent implements OnInit {
@Output() weatherData = new EventEmitter();
  temperature: any;
  constructor(private weatherService: WeatherService,
    private toastr: ToastrService) { }

  ngOnInit() {
    this.getWeatherDetails();
  }
  getWeatherDetails = () => {
    this.weatherService.data.subscribe((data: any) => {
      if (data !== undefined) {
      this.temperature = Math.floor(data?.currentWeather?.temporature);
      }
  }, (err) => {
    this.toastr.error(MessageConfig.CommunicationError, 'Error!');
  })

  }
}
