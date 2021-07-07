import { Injectable } from '@angular/core';
import { AuthenticateObservableService } from '../observable-service/authenticate-observable.service';
import { BehaviorSubject } from 'rxjs';
import { UrlConfig } from '../services/url.config';
import { HttpUtilsService } from './http-utils.service';
import { WeatherService } from '../services/common-service/weather.service';

@Injectable({
  providedIn: 'root'
})
export class UserDataService {
  isAuthenticated = false;
  userDetails: any = {};
  private header: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  public headerName = this.header.asObservable();
  private city: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  public cityName = this.city.asObservable();
  private location: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  public locationName = this.location.asObservable();
  private nav: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  public navName = this.nav.asObservable();
  private unReadMessage: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  public unReadMessageDetail = this.unReadMessage.asObservable();
  constructor(
    private weatherService: WeatherService,
    private authenticateObservableService: AuthenticateObservableService, private http: HttpUtilsService) {
  }
  setUserSettings(loginToken) {
    this.isAuthenticated = true;
    const token = JSON.parse(loginToken);
    console.log(token, 'token');

    if (token.EmployeeDetails !== undefined) {
      this.setSides(JSON.stringify(token?.EmployeeDetails?.RolePermissionViewModel));
      localStorage.setItem('authorizationToken', token.Token);
      localStorage.setItem('refreshToken', token.RefreshToken);
      if (token?.EmployeeDetails?.EmployeeLocations) {
        this.setCityName(token?.EmployeeDetails?.EmployeeLocations[0]?.CityName);
        this.setLocationName(token?.EmployeeDetails?.EmployeeLocations[0]?.LocationName);
        localStorage.setItem('employeeCityName', JSON.stringify(token?.EmployeeDetails?.EmployeeLocations[0]?.CityName));
        localStorage.setItem('employeeLocationName', JSON.stringify(token?.EmployeeDetails?.EmployeeLocations[0]?.LocationName));

      }
      if (token?.EmployeeDetails?.EmployeeLocations) {
        if (token?.EmployeeDetails?.EmployeeLocations?.length > 2) {
          localStorage.setItem('empLocation', JSON.stringify(token?.EmployeeDetails?.EmployeeLocations));
        } else {
          localStorage.setItem('empLocation', JSON.stringify(token?.EmployeeDetails?.EmployeeLocations));
          localStorage.setItem('empLocationId', token?.EmployeeDetails?.EmployeeLocations[0]?.LocationId);
          this.setCityName(token?.EmployeeDetails?.EmployeeLocations[0]?.CityName);
          this.setLocationName(token?.EmployeeDetails?.EmployeeLocations[0]?.LocationName);
        }
      }

      this.weatherService.getWeather()

      if (token?.EmployeeDetails?.EmployeeRoles?.length) {
        localStorage.setItem('empRoles', token?.EmployeeDetails?.EmployeeRoles[0]?.RoleName);
      }
      if (token?.EmployeeDetails?.RolePermissionViewModel !== undefined && token?.EmployeeDetails?.RolePermissionViewModel !== null) {
        this.setViews(token?.EmployeeDetails?.RolePermissionViewModel);
        localStorage.setItem('roleId', token?.EmployeeDetails?.EmployeeRoles[0]?.Roleid);
      }
      this.setHeaderName(token?.EmployeeDetails?.EmployeeLogin?.FirstName + ' ' +
        token?.EmployeeDetails?.EmployeeLogin?.LastName);

      this.getUnreadMessage(token?.EmployeeDetails?.EmployeeLogin?.EmployeeId);
      localStorage.setItem('employeeName', token?.EmployeeDetails?.EmployeeLogin?.FirstName + ' ' +
        token?.EmployeeDetails?.EmployeeLogin?.LastName);
      if (token?.EmployeeDetails?.Drawer) {
        localStorage.setItem('drawerId', token?.EmployeeDetails?.Drawer[0].DrawerId);

      }
      localStorage.setItem('empId', token?.EmployeeDetails?.EmployeeLogin?.EmployeeId);
      localStorage.setItem('employeeFirstName', token?.EmployeeDetails?.EmployeeLogin?.FirstName);
      localStorage.setItem('employeeLastName', token?.EmployeeDetails?.EmployeeLogin?.LastName);

    }
    else if (token.ClientDetails !== undefined) {
      this.setViews(token?.ClientDetails?.RolePermissionViewModel);
      this.setSides(JSON.stringify(token?.ClientDetails?.RolePermissionViewModel));
      localStorage.setItem('authorizationToken', token.Token);
      localStorage.setItem('refreshToken', token.RefreshToken);
      localStorage.setItem('clientId', token.ClientDetails.ClientDetail.ClientId);
      localStorage.setItem('employeeName', token.ClientDetails.ClientDetail.FirstName + ' ' +
        token.ClientDetails.ClientDetail.LastName);
      this.setHeaderName(token.ClientDetails.ClientDetail.FirstName + ' ' +
      token.ClientDetails.ClientDetail.LastName);
      localStorage.setItem('roleId', token.ClientDetails.RolePermissionViewModel[0].RoleId);
      localStorage.setItem('employeeFirstName', token.ClientDetails.ClientDetail.FirstName);
      localStorage.setItem('employeeLastName', token.ClientDetails.ClientDetail.LastName);

      localStorage.setItem('empRoles', token.ClientDetails.RolePermissionViewModel[0].RoleName);
      localStorage.setItem('isAuthenticated', 'true');
    }

    this.authenticateObservableService.setIsAuthenticate(this.isAuthenticated);
  }
  setHeaderName(headerName) {
    this.header.next(headerName);
  }
  setCityName(cityName) {
    this.city.next(cityName);
  }
  setLocationName(Name) {
    this.location.next(Name);
  }
  setViews(views) {
    this.userDetails.views = views;
    localStorage.setItem('views', JSON.stringify(views));
  }
  setSides(navName) {
    this.nav.next(navName)
    localStorage.setItem('navName', navName);

  }

  getUnreadMessage(id) {
    this.http.get(`${UrlConfig.Messenger.getUnReadMessageCount}` + id).subscribe(res => {
      const unReadCount = JSON.parse(res.resultData);
      console.log(unReadCount, 'unread');
      if (unReadCount?.UnreadMessage.getUnReadMessageCountViewModels !== null) {
        this.unReadMessage.next(unReadCount?.UnreadMessage.getUnReadMessageCountViewModels);
      }
    });
  }
}
