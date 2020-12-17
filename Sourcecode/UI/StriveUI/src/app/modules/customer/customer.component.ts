import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})
export class CustomerComponent implements OnInit {
  dashboardScreen: boolean;
  servicesScreen: boolean;
  locationScreen: boolean;
  appointmentScreen: boolean;
  previewScreen: boolean;
  confirmationScreen: boolean;
  scheduleDetailObj: any = {};
  constructor() { }

  ngOnInit(): void {
    this.dashboardScreen = true;
    this.servicesScreen = false;
    this.locationScreen = false;
    this.appointmentScreen = false;
    this.previewScreen = false;
    this.confirmationScreen = false;
  }

  selectServcie(event) {
    this.dashboardScreen = false;
    this.servicesScreen = true;
  }

  dashboardPage(event) {
    this.dashboardScreen = true;
    this.servicesScreen = false;
    this.locationScreen = false;
    this.appointmentScreen = false;
    this.previewScreen = false;
    this.confirmationScreen = false;
  }

  selectLocation(event) {
    this.locationScreen = true;
    this.servicesScreen = false;
  }

  selectionPage(event) {
    this.locationScreen = false;
    this.servicesScreen = true;
  }

  selectAppointment(event) {
    this.locationScreen = false;
    this.appointmentScreen = true;
  }

  previewAppointment(event) {
    this.appointmentScreen = false;
    this.previewScreen = true;
  }

  locationPage(event) {
    this.appointmentScreen = false;
    this.locationScreen = true;
  }

  confirmation(event) {
    this.previewScreen = false;
    this.confirmationScreen = true;
  }

  appointmentPage(event) {
    this.previewScreen = false;
    this.appointmentScreen = true;
  }

}
