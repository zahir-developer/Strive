import { Component, OnInit, Output, EventEmitter } from '@angular/core';


@Component({
  selector: 'app-month-picker',
  templateUrl: './month-picker.component.html'
})
export class MonthPickerComponent implements OnInit {
  mm: any;
  @Output() emitMonth = new EventEmitter();
  months = [
    { val: '01', name: 'Jan' },
    { val: '02', name: 'Feb' },
    { val: '03', name: 'Mar' },
    { val: '04', name: 'Apr' },
    { val: '05', name: 'May' },
    { val: '06', name: 'Jun' },
    { val: '07', name: 'Jul' },
    { val: '08', name: 'Aug' },
    { val: '09', name: 'Sep' },
    { val: '10', name: 'Oct' },
    { val: '11', name: 'Nov' },
    { val: '12', name: 'Dec' }
  ];

  constructor() { }

  ngOnInit(): void {
    this.getMonth();
  }
  getMonth() {
    const today = new Date();
    this.mm = today.getMonth() + 1;
    if (this.mm < 10) {
      this.mm = '0' + this.mm;
    }
  }
  monthChange(event) {
    this.emitMonth.emit(event.target.value);
  }
}
