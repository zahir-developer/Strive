import { Component, OnInit, ViewChild } from '@angular/core';
import { AuthService } from '../shared/services/common-service/auth.service';
import { Router, ActivatedRoute } from '@angular/router';
import { LoginComponent } from '../login/login.component';
import { UserDataService } from '../shared/util/user-data.service';
import { LandingService } from '../shared/services/common-service/landing.service';
import { WeatherService } from '../shared/services/common-service/weather.service';

@Component({
  selector: 'app-select-location',
  templateUrl: './select-location.component.html',
  styleUrls: ['./select-location.component.css']
})
export class SelectLocationComponent implements OnInit {
  empName: any;
  location: any;
  locationId = '';
  @ViewChild(LoginComponent) login: LoginComponent;
  roleAccess = [];
  dashBoardModule: boolean = false;

  constructor(private user: UserDataService,
    private landingservice: LandingService,
    private weatherService : WeatherService,
    private authService: AuthService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.empName = localStorage.getItem('employeeName');
    this.location = JSON.parse(localStorage.getItem('empLocation'));
    this.locationId = JSON.parse(localStorage.getItem('empLocation'))[0].LocationId;

  }
  proceed() {
    if (this.locationId !== null) {
     

      const Id = this.locationId
      localStorage.setItem('empLocationId', Id);
      localStorage.setItem('isAuthenticated', 'true');
      this.authService.loggedIn.next(true);
      this.weatherService.getWeather();
      this.landingservice.routingPage()

      
  }
}
}
