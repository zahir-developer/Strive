import { Component, OnInit, ViewChild } from '@angular/core';
import { AuthService } from '../shared/services/common-service/auth.service';
import { Router, ActivatedRoute } from '@angular/router';
import { LoginComponent } from '../login/login.component';
import { UserDataService } from '../shared/util/user-data.service';
import { LandingService } from '../shared/services/common-service/landing.service';

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
    private authService: AuthService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.empName = localStorage.getItem('employeeName');
    this.location = JSON.parse(localStorage.getItem('empLocationId'));
    this.locationId = JSON.parse(localStorage.getItem('empLocationId'))[0].LocationId;

  }
  proceed() {
    if (this.locationId !== null) {
   
      this.landingservice.loadTheLandingPage()

  }
}
}
