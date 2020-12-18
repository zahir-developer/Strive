import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-appointment-configuration',
  templateUrl: './appointment-configuration.component.html',
  styleUrls: ['./appointment-configuration.component.css']
})
export class AppointmentConfigurationComponent implements OnInit {
  @Output() dashboardPage = new EventEmitter();
  constructor() { }

  ngOnInit(): void {
  }

  backToDashboard() {
    this.dashboardPage.emit();
  }

}