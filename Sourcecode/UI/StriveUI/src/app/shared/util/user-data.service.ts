import { Injectable } from '@angular/core';
import { AuthenticateObservableService } from '../observable-service/authenticate-observable.service';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserDataService {
  isAuthenticated = false;
  private header: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  public headerName = this.header.asObservable();
  constructor(private authenticateObservableService: AuthenticateObservableService) {
  }
  setUserSettings(loginToken) {
    this.isAuthenticated = true;
    const token = JSON.parse(loginToken);
    localStorage.setItem('authorizationToken', token.Token);
    localStorage.setItem('refreshToken', token.RefreshToken);
    localStorage.setItem('empLocationId', token.EmployeeDetails.EmployeeLocations[0].LocationId);
    this.setHeaderName(token.EmployeeDetails?.EmployeeLogin?.Firstname + ' ' +
    token.EmployeeDetails?.EmployeeLogin?.LastName);
    localStorage.setItem('employeeName', token.EmployeeDetails?.EmployeeLogin?.Firstname + ' ' +
    token.EmployeeDetails?.EmployeeLogin?.LastName);
    localStorage.setItem('drawerId', token.EmployeeDetails.Drawer[0].DrawerId);
    localStorage.setItem('empId', token.EmployeeDetails?.EmployeeLogin?.EmployeeId);
    localStorage.setItem('roleId', token.EmployeeDetails.EmployeeRoles[0].Roleid);
    localStorage.setItem('employeeFirstName', token.EmployeeDetails.EmployeeLogin.Firstname);
    localStorage.setItem('employeeLastName', token.EmployeeDetails.EmployeeLogin.LastName);
    this.authenticateObservableService.setIsAuthenticate(this.isAuthenticated);
  }
  setHeaderName(headerName) {
    this.header.next(headerName);
  }
}
