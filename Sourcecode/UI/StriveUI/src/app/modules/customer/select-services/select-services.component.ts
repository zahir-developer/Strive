import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-select-services',
  templateUrl: './select-services.component.html',
  styleUrls: ['./select-services.component.css']
})
export class SelectServicesComponent implements OnInit {
  @Output() dashboardPage = new EventEmitter();
  @Output() selectLocation = new EventEmitter();
  constructor() { }

  ngOnInit(): void {
  }

  backToDashboard() {
    this.dashboardPage.emit();
  }

  nextPage() {
    this.selectLocation.emit();
  }

}
