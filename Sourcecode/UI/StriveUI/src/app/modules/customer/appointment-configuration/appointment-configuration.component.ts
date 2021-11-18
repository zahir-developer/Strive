import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';

@Component({
  selector: 'app-appointment-configuration',
  templateUrl: './appointment-configuration.component.html'
})
export class AppointmentConfigurationComponent implements OnInit {
  @Output() dashboardPage = new EventEmitter();
  @Input() scheduleDetailObj?: any;
  @Input() selectedData?: any;
  constructor() { }

  ngOnInit(): void {
  }

  backToDashboard() {
    this.dashboardPage.emit();
  }

}
