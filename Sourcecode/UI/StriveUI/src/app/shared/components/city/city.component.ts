import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { StateService } from '../../services/common-service/state.service';
import * as _ from 'underscore';
@Component({
  selector: 'app-city',
  templateUrl: './city.component.html',
  styleUrls: ['./city.component.css']
})
export class CityComponent implements OnInit {
  @Input() isView: any;
  @Input() selectedCityId: any;
  @Output() selectCity = new EventEmitter();
  city: string;
  country: any = '';
  filteredCountriesSingle = [];
  countries: any[];
  submitted: boolean;
  cities: any = [];
  filteredCity: any = [];
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
  constructor(private httpClient: HttpClient, private stateService: StateService) { }

  ngOnInit(): void {
    this.getCity();
    this.submitted = false;
  }

  getCity() {
    this.stateService.getCityList('CITY').subscribe(res => {
      const city = JSON.parse(res.resultData);
      if (city.Codes.length > 0) {
        this.cities = city.Codes.map(item => {
          return {
            id: item.CodeId,
            name: item.CodeValue
          };
        });
        this.setCity();
      }
      console.log(city);
    });
  }

  filterCity(event) {
    const filtered: any[] = [];
    const query = event.query;
    for (const i of this.cities) {
      const client = i;
      if (client.name.toLowerCase().indexOf(query.toLowerCase()) === 0) {
        filtered.push(client);
      }
    }
    this.filteredCity = filtered;
  }

  selectedCity(event) {
    console.log(this.country);

    this.selectCity.emit(event.id);
  }
  setCity() {
    let patchData;
    if (this.selectedCityId !== undefined) {
      patchData = _.findWhere(this.cities, { id: this.selectedCityId });
      this.country = patchData;
    }
  }
}
