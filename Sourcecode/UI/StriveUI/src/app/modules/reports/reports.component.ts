import { Component, OnInit } from '@angular/core';
import { LandingService } from 'src/app/shared/services/common-service/landing.service';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html'
})
export class ReportsComponent implements OnInit {

  constructor(private landingservice:LandingService) { }

  ngOnInit(): void {
  }
  landing(){
    this.landingservice.loadTheLandingPage()
  }
}
