import { Component, OnInit, AfterViewInit, ViewChild, ChangeDetectorRef } from '@angular/core';
import { BsDaterangepickerDirective, BsDatepickerConfig } from 'ngx-bootstrap/datepicker/ngx-bootstrap-datepicker';

@Component({
  selector: 'app-date-picker',
  templateUrl: './date-picker.component.html',
  styleUrls: ['./date-picker.component.css']
})
export class DatePickerComponent implements OnInit, AfterViewInit {
  @ViewChild('dp', { static: false }) datepicker: BsDaterangepickerDirective;
  bsConfig: Partial<BsDatepickerConfig>;
  maxDate = new Date();
  date = new Date();
  constructor( private cd: ChangeDetectorRef) { }

  ngOnInit(): void {
  }
  ngAfterViewInit() {
    this.bsConfig = Object.assign({}, { maxDate: this.maxDate, dateInputFormat: 'MM/DD/YYYY' });
    this.datepicker.setConfig();
    this.cd.detectChanges();
  }
  onValueChange(event) {
  }
}
