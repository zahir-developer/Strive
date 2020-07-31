import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { CountryService } from '../../services/common-service/country.service';

@Component({
  selector: 'app-country-dropdown',
  templateUrl: './country-dropdown.component.html',
  styleUrls: ['./country-dropdown.component.css']
})
export class CountryDropdownComponent implements OnInit {
  countryList = [];
  country = 38;
  @Output() countryId = new EventEmitter();
  @Input() selectedCountryId: any;
  @Input() isdisable: any;
  constructor(private countryService: CountryService) { }

  ngOnInit(): void {
    if (this.isdisable === undefined || this.isdisable === false) {
      this.isdisable = false;
    } else {
this.isdisable = true;
    }

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
      this.setValue();
    }, (err) => {
    });
  }
  countrySelection(event) {
    this.countryId.emit(event);
  }

  setValue() {
    if (this.selectedCountryId !== undefined) {
      this.country = this.selectedCountryId;
    }
  }

}
