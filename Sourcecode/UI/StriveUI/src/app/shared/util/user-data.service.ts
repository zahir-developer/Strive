import { Injectable } from '@angular/core';
import { AuthenticateObservableService } from '../observable-service/authenticate-observable.service';

@Injectable({
  providedIn: 'root'
})
export class UserDataService {
  isAuthenticated = false;
  constructor(private authenticateObservableService: AuthenticateObservableService) {
  }
  setUserSettings(loginToken) {
    this.isAuthenticated = true;
    const token = JSON.parse(loginToken);
    localStorage.setItem('authorizationToken', token.Token);
    localStorage.setItem('refreshToken', token.RefreshToken);
    localStorage.setItem('empLocationId', token.EmployeeDetails.EmployeeLocations[0].LocationId);
    localStorage.setItem('employeeName', token.EmployeeDetails?.EmployeeLogin?.Firstname + ' ' +
    token.EmployeeDetails?.EmployeeLogin?.LastName);
    localStorage.setItem('drawerId', token.EmployeeDetails.Drawer[0].DrawerId);
    localStorage.setItem('empId', token.EmployeeDetails?.EmployeeLogin?.EmployeeId);
    localStorage.setItem('roleId', token.EmployeeDetails.EmployeeRoles[0].Roleid);

    this.authenticateObservableService.setIsAuthenticate(this.isAuthenticated);
  }
}
