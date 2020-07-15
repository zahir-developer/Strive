import { Component, OnInit } from '@angular/core';
import { CountryService } from '../../services/common-service/country.service';

@Component({
  selector: 'app-country-dropdown',
  templateUrl: './country-dropdown.component.html',
  styleUrls: ['./country-dropdown.component.css']
})
export class CountryDropdownComponent implements OnInit {
  countryList = [];
  constructor(private countryService: CountryService) { }

  ngOnInit(): void {
    this.getCountriesList();
  }
  getCountriesList() {
    this.countryService.getCountriesList().subscribe(data => {
const country = JSON.parse(data.resultData);
this.countryList = country.Codes.map(item => {
  return {
    name: item.CodeValue,
    value: item.CodeId
  };
});
console.log(this.countryList);
    }, (err) => {
    });
  }

}
