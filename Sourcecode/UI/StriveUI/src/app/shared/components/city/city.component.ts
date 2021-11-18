import { Component, OnInit, Input, Output, EventEmitter, AfterViewChecked } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { StateService } from '../../services/common-service/state.service';
import * as _ from 'underscore';
import { ChangeDetectorRef } from '@angular/core';
import { MessageConfig } from '../../services/messageConfig';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-city',
  templateUrl: './city.component.html'
})
export class CityComponent implements OnInit, AfterViewChecked {
  @Input() isView: any;
  @Input() selectedCityId?: any;
  @Input() selectedStateId?: any;

  @Input() State?: any;

  @Output() selectCity = new EventEmitter();
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
  city: { value: any; name: any; };
  selectValueCity: boolean;
  constructor(private cdRef: ChangeDetectorRef, private stateService: StateService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.submitted = false;
    this.selectValueCity = false;
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
      this.toastr.error(MessageConfig.CommunicationError, 'Error!');
    });
  }

  selectedCity(event) {
    this.selectValueCity = true;
    this.selectCity.emit(event.value.value);
  }
  setCity() {
    if (this.selectedCityId !== undefined) {
      this.selectValueCity = true;


      this.cities.map(item => {
        if (item.value == this.selectedCityId) {
          this.city = {
            value: item.value,
            name: item.name
          };
        }
      });

    }
  }
}
