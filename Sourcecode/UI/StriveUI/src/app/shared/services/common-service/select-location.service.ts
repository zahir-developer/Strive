import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SelectLocationService {

  constructor() { }

  public locationName: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  public obsLocationName = this.locationName.asObservable();

  public cityName: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  public obsCityName = this.cityName.asObservable();

  setLocationCity(location, city) {
    this.locationName.next(location);
    this.cityName.next(city);
  }

}
