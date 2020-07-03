import { Component, OnInit } from '@angular/core';
import { CountriesService } from '../shared/services/common-service/countries.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-helps',
  templateUrl: './helps.component.html',
  styleUrls: ['./helps.component.css']
})
export class HelpsComponent implements OnInit {
  country: any;
  filteredCountriesSingle = [];
  countries: any[];
  constructor(private countryService: CountriesService, private httpClient: HttpClient) { }

  ngOnInit(): void {
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
