import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-year-picker',
  templateUrl: './year-picker.component.html'
})
export class YearPickerComponent implements OnInit {
  years: number[] = [];
  yy: number;
  @Output() emitYear = new EventEmitter();
  constructor() { }

  ngOnInit(): void {
    this.getYear();
  }
  getYear() {
    const today = new Date();
    this.yy = today.getFullYear();
    for (let i = (this.yy - 100); i <= this.yy; i++) {
      this.years.push(i);
    }
  }
  yearChange(event) {
    this.emitYear.emit(event.target.value);
  }

}
