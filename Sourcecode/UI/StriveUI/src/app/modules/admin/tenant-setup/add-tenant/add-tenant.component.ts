import { ViewChild } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { CityComponent } from 'src/app/shared/components/city/city.component';
import { StateDropdownComponent } from 'src/app/shared/components/state-dropdown/state-dropdown.component';

@Component({
  selector: 'app-add-tenant',
  templateUrl: './add-tenant.component.html',
  styleUrls: ['./add-tenant.component.css']
})
export class AddTenantComponent implements OnInit {
  @ViewChild(StateDropdownComponent) stateDropdownComponent: StateDropdownComponent;
  @ViewChild(CityComponent) cityComponent: CityComponent;
  State: any;
  city: any;
  constructor() { }

  ngOnInit(): void {
  }

  getSelectedStateId(event) {
    this.State = event;
    this.cityComponent.getCity(event);
  }

  selectCity(event) {
    this.city = event;
  }

}
