import { Component, OnInit } from '@angular/core';
import { WashService } from '../../services/data-service/wash.service';

@Component({
  selector: 'app-forecasted-cars',
  templateUrl: './forecasted-cars.component.html'
})
export class ForecastedCarsComponent implements OnInit {
  forecastedCars: any;
  current: any;
  constructor(private wash: WashService) { }

  ngOnInit() {
    this.getDashboardDetails();
  }

  // Get ForecastedCars
  getDashboardDetails = () => {
    const obj = {
      id: +localStorage.getItem('empLocationId'),
      date: new Date()
    };
    this.wash.getDashBoard(obj);
    this.wash.dashBoardData.subscribe((data: any) => {
        this.forecastedCars = data.ForecastedCars;
        this.current = data.Current;
    });
  }
}

