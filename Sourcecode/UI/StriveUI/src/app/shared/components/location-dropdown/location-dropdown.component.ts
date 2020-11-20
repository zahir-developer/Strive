import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { LocationService } from '../../services/data-service/location.service';
import { MessageServiceToastr } from '../../services/common-service/message.service';

@Component({
  selector: 'app-location-dropdown',
  templateUrl: './location-dropdown.component.html',
  styleUrls: ['./location-dropdown.component.css']
})
export class LocationDropdownComponent implements OnInit {
  location = [];
  locationId: any;
  locationName = '';
  @Output() emitLocation = new EventEmitter();
  constructor(private locationService: LocationService, private messageService: MessageServiceToastr) { }

  ngOnInit(): void {
    this.locationId = +localStorage.getItem('empLocationId');
    this.getLocation();
  }
getLocation() {
  this.locationService.getLocation().subscribe(res => {
    if (res.status === 'Success') {
      const location = JSON.parse(res.resultData);
      this.location = location.Location;
      this.getLocationNameById(this.locationId);
    } else {
      this.messageService.showMessage({ severity: 'error', title: 'Error', body: 'Communication Error' });
    }
  });
}
changeLocation(event) {
  this.emitLocation.emit(event.target.value);
  this.getLocationNameById(event.target.value);
}
getLocationNameById(id) {
  const locationName = this.location.filter(item => +item.LocationId === +id);
  this.locationName = locationName[0].LocationName;
}
}
