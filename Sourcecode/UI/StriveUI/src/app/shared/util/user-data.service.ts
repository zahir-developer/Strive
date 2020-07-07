import { Injectable } from '@angular/core';
import { AuthenticateObservableService } from '../observable-service/authenticate-observable.service';

@Injectable({
  providedIn: 'root'
})
export class UserDataService {
  isAuthenticated = false;
  constructor(private authenticateObservableService: AuthenticateObservableService) {
    const authData = localStorage.getItem('authorizationToken');
    if (authData) {
      this.setIsAuthenticate();
    }
  }
  setIsAuthenticate() {
    this.isAuthenticated = true;
  }
  setUserSettings(loginToken) {
    console.log(loginToken);
    this.isAuthenticated = true;
    const token = JSON.parse(loginToken);
    localStorage.setItem('authorizationToken', token.Token);
    localStorage.setItem('refreshToken', token.RefreshToken);
    this.authenticateObservableService.setIsAuthenticate(this.isAuthenticated);
  }
}
