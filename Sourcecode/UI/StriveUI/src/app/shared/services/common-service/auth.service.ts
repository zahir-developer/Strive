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
  private loggedIn: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  get isLoggedIn() {
    return this.loggedIn.asObservable();
  }
  constructor(private http: HttpUtilsService, private userService: UserDataService, private router: Router,
              private route: ActivatedRoute) {
    if (localStorage.getItem('isAuthenticated') === 'true') {
      this.loggedIn.next(true);
    } 
    // else {
    //   this.loggedIn.next(false);
    // }
  }
  login(loginData: any): Observable<any> {
    return this.http.post(`${UrlConfig.totalUrl.login}`, loginData).pipe(map((user) => {
      if (user !== null && user !== undefined) {
        if (user.status === 'Success') {
          localStorage.setItem('isAuthenticated', 'true');
          this.loggedIn.next(true);
          this.userService.setUserSettings(user.resultData);
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
    this.router.navigate([`/login`], { relativeTo: this.route });
  }
}
