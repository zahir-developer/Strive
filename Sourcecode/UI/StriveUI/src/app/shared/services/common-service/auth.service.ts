import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';
import { map } from 'rxjs/operators';
import { UserDataService } from '../../util/user-data.service';
import { Router, ActivatedRoute } from '@angular/router';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  userDetails: any;
  public loggedIn: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  get isLoggedIn() {
    return this.loggedIn.asObservable();
  }
  constructor(private http: HttpUtilsService, private userService: UserDataService, private router: Router,
    private route: ActivatedRoute) {
    if (localStorage.getItem('isAuthenticated') === 'true') {
      this.loggedIn.next(true);
    }
  }
  login(loginData: any): Observable<any> {
    return this.http.post(`${UrlConfig.Auth.login}`, loginData).pipe(map((user) => {
      if (user !== null && user !== undefined) {
        if (user.status === 'Success') {
          this.userService.setUserSettings(user.resultData);
          return user;
        }
      }
      return user;
    }));
  }

  refershToken(obj) {
    return this.http.post(`${UrlConfig.Auth.refreshToken}`, obj).pipe(map((user) => {
      if (user !== null && user !== undefined) {
        if (user.status === 'Success') {
          return user;
        }
      }
      return user;
    }));
  }


  logout() {
    localStorage.setItem('isAuthenticated', 'false');
    this.loggedIn.next(false);
    localStorage.removeItem('authorizationToken');
    localStorage.removeItem('refreshToken');
    localStorage.removeItem('views');

    localStorage.removeItem('navName');

    localStorage.clear();


    document.documentElement.style.setProperty(`--primary-color`, '#1DC5B3');
    document.documentElement.style.setProperty(`--navigation-color`, '#24489A');
    document.documentElement.style.setProperty(`--secondary-color`, '#F2FCFE');
    document.documentElement.style.setProperty(`--tertiary-color`, '#10B7A5');
    document.documentElement.style.setProperty(`--body-color`, '#F2FCFE');
    this.router.navigate([`/login`], { relativeTo: this.route });
  }
}
