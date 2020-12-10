import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { StateService } from '../../services/common-service/state.service';
import * as _ from 'underscore';
import { ChangeDetectorRef } from '@angular/core';
@Component({
  selector: 'app-city',
  templateUrl: './city.component.html',
  styleUrls: ['./city.component.css']
})
export class CityComponent implements OnInit {
  @Input() isView: any;
  @Input() selectedCityId: any;
  @Output() selectCity = new EventEmitter();
  city = '';
  submitted: boolean;
  cities: any = [];
  states: string[] = [
    'Alabama',
    'Alaska',
    'Arizona',
    'Arkansas',
    'California',
    'Colorado',
    'Connecticut',
    'Delaware',
    'Florida',
    'Georgia',
    'Hawaii',
    'Idaho'];
  constructor(private cdRef: ChangeDetectorRef, private stateService: StateService) { }

  ngOnInit(): void {
    this.submitted = false;
    this.getCity();
  }

  ngAfterViewChecked(){
    if (this.selectedCityId !== undefined) {
      this.cdRef.detectChanges();
    }
  }

  getCity() {
    this.stateService.getCityList('CITY').subscribe(res => {
      const city = JSON.parse(res.resultData);
      if (city.Codes.length > 0) {
        this.cities = city.Codes.map(item => {
          return {
            value: item.CodeId,
            name: item.CodeValue
          };
        });
        this.setCity();
      }
    }, (err) => {
    });
  }

  selectedCity(event) {
    this.selectCity.emit(event);
  }
  setCity() {
    if (this.selectedCityId !== undefined) {
      this.city = this.selectedCityId;
    }
  }
}
