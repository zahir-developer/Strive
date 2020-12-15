import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-select-location',
  templateUrl: './select-location.component.html',
  styleUrls: ['./select-location.component.css']
})
export class SelectLocationComponent implements OnInit {
  @Output() selectionPage = new EventEmitter();
  @Output() selectAppointment = new EventEmitter();
  constructor() { }

  ngOnInit(): void {
  }

  cancel() {
    this.selectionPage.emit();
  }

  next() {
    this.selectAppointment.emit();
  }

}
