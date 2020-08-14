import { Component, OnInit } from '@angular/core';
import { WashService } from '../../services/data-service/wash.service';

@Component({
  selector: 'app-forecasted-cars',
  templateUrl: './forecasted-cars.component.html',
  styleUrls: ['./forecasted-cars.component.css']
})
export class ForecastedCarsComponent implements OnInit {
  forecastedCars: any;

  constructor(private wash: WashService) { }

  ngOnInit() {
    this.getDashboardDetails();
  }

  // Get ForecastedCars
  getDashboardDetails = () => {
    this.wash.data.subscribe((data: any) => {
      if (data.ForecastedCars !== undefined) {
        this.forecastedCars = data.ForecastedCars.ForecastedCars;
      }
    });
  }
}

