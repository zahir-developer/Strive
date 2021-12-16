import { Component, OnInit, Output, EventEmitter, Input, ChangeDetectorRef, AfterViewChecked } from '@angular/core';
import { CountryService } from '../../services/common-service/country.service';

@Component({
  selector: 'app-country-dropdown',
  templateUrl: './country-dropdown.component.html'
})
export class CountryDropdownComponent implements OnInit, AfterViewChecked {
  countryList = [];
  country: any;
  @Output() countryId = new EventEmitter();
  @Input() selectedCountryId: any;
  @Input() isdisable: any;
  submitted: boolean;
  constructor(private countryService: CountryService, private cdRef: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.submitted = false;
    if (this.isdisable === undefined || this.isdisable === false) {
      this.isdisable = false;
    } else {
      this.isdisable = true;
    }

    this.getCountriesList();
  }

  ngAfterViewChecked(){
    if (this.selectedCountryId !== undefined) {
      this.cdRef.detectChanges();
    }
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
      const countryValue = this.countryList.filter( item => item.name === 'USA');
      if (countryValue.length > 0) {
        this.country = countryValue[0].value;
      }
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
