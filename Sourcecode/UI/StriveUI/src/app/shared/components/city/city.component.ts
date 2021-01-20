import { Component, OnInit, Input, Output, EventEmitter, AfterViewChecked } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { StateService } from '../../services/common-service/state.service';
import * as _ from 'underscore';
import { ChangeDetectorRef } from '@angular/core';
@Component({
  selector: 'app-city',
  templateUrl: './city.component.html',
  styleUrls: ['./city.component.css']
})
export class CityComponent implements OnInit, AfterViewChecked {
  @Input() isView: any;
  @Input() selectedCityId?: any;
  @Input() selectedStateId?: any;

  @Input() State?: any;

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
  cityId: any;
  constructor(private cdRef: ChangeDetectorRef, private stateService: StateService) { }

  ngOnInit(): void {
    this.submitted = false;
    if (this.selectedStateId !== undefined) {
      this.getCity(this.selectedStateId);
    }
  }

  ngAfterViewChecked() {
    if (this.selectedCityId !== undefined) {
      this.cdRef.detectChanges();
    }

  }

  getCity(city) {
    const cityValue = city ? city : this.selectedStateId;
    this.stateService.getCityByStateId(cityValue).subscribe(res => {
      const cityList = JSON.parse(res.resultData);
      if (cityList.cities.length > 0) {
        this.cities = cityList.cities.map(item => {
          return {
            value: item.CityId,
            name: item.CityName
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
