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
  locations: any;
  locationId = '';
  @ViewChild(LoginComponent) login: LoginComponent;
  roleAccess = [];
  dashBoardModule: boolean = false;
  empLocation = [];
 
  constructor(private user: UserDataService,
    private landingservice: LandingService,
    private weatherService : WeatherService,
    private userService: UserDataService,
    private authService: AuthService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.empName = localStorage.getItem('employeeName');
    this.locations = JSON.parse(localStorage.getItem('empLocation'));
    this.locationId = JSON.parse(localStorage.getItem('empLocation'))[0].LocationId;

  }
  proceed() {
    if (this.locationId !== null) {
     

      const Id = this.locationId
      localStorage.setItem('empLocationId', Id);
      this.locations.forEach(element => {
        if(element.LocationId == this.locationId){
          this.empLocation.push(
element
            );
            localStorage.setItem('empLocationName',JSON.stringify(this.empLocation[0]?.LocationName));
            this.setLocationName(this.empLocation[0]?.LocationName)
        }
      });
     

      localStorage.setItem('isAuthenticated', 'true');
      this.authService.loggedIn.next(true);
      this.weatherService.getWeather();
      this.landingservice.routingPage()

      
  }
}

setLocationName(Name) {
  this.userService.setLocationName(Name);
}
}
