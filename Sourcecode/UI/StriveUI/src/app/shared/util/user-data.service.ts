import { Injectable } from '@angular/core';
import { AuthenticateObservableService } from '../observable-service/authenticate-observable.service';
import { BehaviorSubject } from 'rxjs';
import { UrlConfig } from '../services/url.config';
import { HttpUtilsService } from './http-utils.service';

@Injectable({
  providedIn: 'root'
})
export class UserDataService {
  isAuthenticated = false;
  userDetails: any = {};
  private header: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  public headerName = this.header.asObservable();
  private unReadMessage: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  public unReadMessageDetail = this.unReadMessage.asObservable();
  constructor(private authenticateObservableService: AuthenticateObservableService, private http: HttpUtilsService) {
  }
  setUserSettings(loginToken) {
    this.isAuthenticated = true;
    const token = JSON.parse(loginToken);
    console.log(token, 'token');
    // if (token?.EmployeeDetails?.RolePermissionViewModel !== undefined && token?.EmployeeDetails?.RolePermissionViewModel !== null) {
    //   this.userDetails.views = token.EmployeeDetails.RolePermissionViewModel;
    // }
    localStorage.setItem('authorizationToken', token.Token);
    localStorage.setItem('refreshToken', token.RefreshToken);
    if (token?.EmployeeDetails?.EmployeeLocations?.length > 1) {
      localStorage.setItem('empLocationId', JSON.stringify(token?.EmployeeDetails?.EmployeeLocations));
    } else {
      localStorage.setItem('empLocationId', token.EmployeeDetails.EmployeeLocations[0].LocationId);
    }
    if (token?.EmployeeDetails?.RolePermissionViewModel !== undefined && token?.EmployeeDetails?.RolePermissionViewModel !== null) {
      this.setViews(token?.EmployeeDetails?.RolePermissionViewModel);
      // this.userDetails.views = token.EmployeeDetails.RolePermissionViewModel;
    }
    this.setHeaderName(token.EmployeeDetails?.EmployeeLogin?.Firstname + ' ' +
      token.EmployeeDetails?.EmployeeLogin?.LastName);
    this.getUnreadMessage(token.EmployeeDetails?.EmployeeLogin?.EmployeeId);
    localStorage.setItem('employeeName', token.EmployeeDetails?.EmployeeLogin?.Firstname + ' ' +
      token.EmployeeDetails?.EmployeeLogin?.LastName);
    localStorage.setItem('drawerId', token.EmployeeDetails.Drawer[0].DrawerId);
    localStorage.setItem('empId', token.EmployeeDetails?.EmployeeLogin?.EmployeeId);
    localStorage.setItem('roleId', token.EmployeeDetails.EmployeeRoles[0].Roleid);
    localStorage.setItem('employeeFirstName', token.EmployeeDetails.EmployeeLogin.Firstname);
    localStorage.setItem('employeeLastName', token.EmployeeDetails.EmployeeLogin.LastName);
    
      localStorage.setItem('RolePermission',JSON.stringify(token.EmployeeDetails.RolePermissionViewModel) );
    
              
    this.authenticateObservableService.setIsAuthenticate(this.isAuthenticated);
  }
  setHeaderName(headerName) {
    this.header.next(headerName);
  }

  setViews(views) {
    this.userDetails.views = views;
    localStorage.setItem('views', JSON.stringify(views));
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
