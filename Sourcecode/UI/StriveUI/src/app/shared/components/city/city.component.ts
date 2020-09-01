import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { StateService } from '../../services/common-service/state.service';

@Component({
  selector: 'app-city',
  templateUrl: './city.component.html',
  styleUrls: ['./city.component.css']
})
export class CityComponent implements OnInit {
  @Input() isView: any;
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
    console.log(event);
    this.selectCity.emit(event.id);
  }

  filterCountrySingle(event) {
    const query = event.query;
    this.httpClient.get('assets/json/countries.json').toPromise()
      .then((data: any) => {
        this.countries = data.data;
        this.filteredCountriesSingle = this.filterCountry(query, this.countries);
      });
  }
  filterCountry(query, countries) {
    // in a real application, make a request to a remote url with the query and return filtered results, for demo we filter at client side
    const filtered: any[] = [];
    for (let i = 0; i < countries.length; i++) {
      const country = countries[i];
      if (country.name.toLowerCase().indexOf(query.toLowerCase()) === 0) {
        filtered.push(country);
      }
    }
    return filtered;
  }
}
