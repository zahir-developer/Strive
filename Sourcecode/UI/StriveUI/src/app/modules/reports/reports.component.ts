import { Component, OnInit, ViewChild } from '@angular/core';
import { HomeNavService } from 'src/app/shared/common-service/home-nav.service';


@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.css']
})
export class ReportsComponent implements OnInit {

  constructor(private homeNavigation: HomeNavService) { }

  ngOnInit(): void {
  }

  loadLandingPage() {
    this.homeNavigation.loadLandingPage();
  }

}
