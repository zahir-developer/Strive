import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { HttpUtilsService } from '../../util/http-utils.service';
import { UrlConfig } from '../url.config';
import { map, tap } from 'rxjs/operators';
import { UserDataService } from '../../util/user-data.service';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthenticateObservableService } from '../../observable-service/authenticate-observable.service';
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
    private route: ActivatedRoute, private authenticate: AuthenticateObservableService) {
    if (localStorage.getItem('isAuthenticated') === 'true') {
      this.loggedIn.next(true);
    }
  }
  login(loginData: any): Observable<any> {
    return this.http.post(`${UrlConfig.Auth.login}`, loginData).pipe(tap((user) => {
      if (user !== null && user !== undefined) {
        if (user.status === 'Success') {
          this.userService.setUserSettings(user.resultData);
          return user;
        }
      }
      return user;
    }));
  }

  refreshToken() {
    const obj = {
      token: localStorage.getItem('authorizationToken'),
      refreshToken: localStorage.getItem('refreshToken')
    };
    return this.http.post(`${UrlConfig.Auth.refreshToken}`, obj).pipe(tap((user) => {
      if (user !== null && user !== undefined) {
        if (user.status === 'Success') {
          const token = JSON.parse(user.resultData);
          localStorage.setItem('authorizationToken', token.Token);
          localStorage.setItem('refreshToken', token.RefreshToken);
        }
      }
    }));
  }

  // refreshToken(): Observable<{accessToken: string; refreshToken: string}> {
  //   const refreshToken = localStorage.getItem('refreshToken');

  //   return this.http.post<{accessToken: string; refreshToken: string}>(
  //     `${environment.apiUrl}/refresh-token`,
  //     {
  //       refreshToken
  //     }).pipe(
  //       tap(response => {
  //         this.setToken('token', response.accessToken);
  //         this.setToken('refreshToken', response.refreshToken);
  //       })
  //   );
  // }



  logout() {
    this.clearCacheValue();
    this.loggedIn.next(false);
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

  refreshLogout() {
    this.clearCacheValue();
  }

  clearCacheValue() {
    this.authenticate.setIsAuthenticate(false);
    localStorage.setItem('isAuthenticated', 'false');
    localStorage.removeItem('authorizationToken');
    localStorage.removeItem('refreshToken');
  }

  sessionLogin(loginData: any) {
    return this.http.post(`${UrlConfig.Auth.login}`, loginData).pipe(tap((user) => {
      if (user !== null && user !== undefined) {
        if (user.status === 'Success') {
          const token = JSON.parse(user.resultData);
          localStorage.setItem('authorizationToken', token.Token);
          localStorage.setItem('refreshToken', token.RefreshToken);
          this.authenticate.setIsAuthenticate(true);
          return user;
        }
      }
      return user;
    }));
  }
}
