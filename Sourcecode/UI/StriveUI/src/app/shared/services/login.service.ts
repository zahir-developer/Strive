import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpUtilsService } from '../util/http-utils.service';
import { UrlConfig } from './url.config';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private http: HttpUtilsService) { }
  public userAuthentication(loginObj): Observable<any> {
    return this.http.post(`${UrlConfig.Auth.login}`, loginObj);
  }
  refreshToken(paramsObj): Observable<any> {
    return this.http.post(`${UrlConfig.Auth.refreshToken}`, {params: {token: paramsObj.token, refreshToken: paramsObj.refreshToken}});
  }
  createAccount(customerObj): Observable<any> {
    return this.http.post(`${UrlConfig.Auth.customerSignup}`, customerObj );
  }

  emailIdExists(email) {
    return this.http.get(`${UrlConfig.Auth.emailIdExists}` + email );
  }
  
}
