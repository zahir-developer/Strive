import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-city',
  templateUrl: './city.component.html',
  styleUrls: ['./city.component.css']
})
export class CityComponent implements OnInit {
  city: string;
  country: any = '';
  filteredCountriesSingle = [];
  countries: any[];
  submitted: boolean;
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
  constructor(private httpClient: HttpClient) { }

  ngOnInit(): void {
    this.submitted = false;
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
