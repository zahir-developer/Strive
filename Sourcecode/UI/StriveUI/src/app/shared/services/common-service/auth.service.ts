import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';
import { map } from 'rxjs/operators';
import { UserDataService } from '../../util/user-data.service';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  userDetails: any;

  constructor(private http: HttpUtilsService, private userService: UserDataService) { }
  login(loginData: any): Observable<any> {
    return this.http.post(`${UrlConfig.totalUrl.login}`, loginData).pipe(map((user) => {
      if (user !== null && user !== undefined) {
          this.userService.setUserSettings(user.resultData);
          return user;
      }
      return user;
    }));
  }
  isAuthenticated() {
const token = localStorage.getItem('authorizationToken');
// if (token !== null) {
// if (!this.jwtHelper.isTokenExpired(token) && this.userService.isAuthenticated) {
// return true;
// } else {
//   return false;
// }
// }
  }
}
