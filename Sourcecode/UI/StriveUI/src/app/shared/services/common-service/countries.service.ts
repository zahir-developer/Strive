import { Injectable } from '@angular/core';
import { HttpUtilsService } from '../../util/http-utils.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CountriesService {

  constructor(private http: HttpClient) { }
  getCountries() {
    return this.http.get('assets/json/countries.json')
      .toPromise()
      .then(data => data );
  }
}
