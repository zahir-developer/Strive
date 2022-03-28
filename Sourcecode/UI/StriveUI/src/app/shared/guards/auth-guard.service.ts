import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthorizeService } from '../services/common-service/authorize.service';
import { AuthService } from '../services/common-service/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router, private authorizeService: AuthorizeService, private authService: AuthService) {
  }
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    if (localStorage.getItem('isAuthenticated') === 'true') {

      var token = localStorage.getItem('authorizationToken');
  
      var refreshTokenExpiry = new Date(localStorage.getItem('refreshTokenExpiry'));

      var tokenExpiry = new Date(localStorage.getItem('tokenExpiry'));

      var currentTime = new Date();

      if (currentTime >= tokenExpiry)
          return this.router.navigate(['/unauthorized']);
      else if (refreshTokenExpiry <= currentTime)
          return this.tryRefreshingTokens(token);
     
      if (next.data.authorization) {
        if (!this.authorizeService.routingLevelAccess(next.data.authorization)) {
          
          return false;
        } else {
          return true;
        }
      }
    }
    else {
      this.router.navigate(['/unauthorized']);
      return false;
    }
   
  }

  
  private async tryRefreshingTokens(token: string): Promise<boolean> {
    // Try refreshing tokens using refresh token
    const refreshToken: string = localStorage.getItem("refreshToken");
    if (!token || !refreshToken) {
      return false;
    }
    const credentials = JSON.stringify({ accessToken: token, refreshToken: refreshToken });
    let isRefreshSuccess: boolean;
    try {
      var response = this.authService.refreshToken();
      /*.subscribe(
        (res) => {
          console.log(res, 'refresh')
          const token = JSON.parse(res.resultData);

          // If token refresh is successful, set new tokens in local storage.
          const newToken = token.Token;
          const newRefreshToken = token.RefreshToken;
          localStorage.setItem("token", newToken);
          localStorage.setItem("refreshToken", newRefreshToken);
          isRefreshSuccess = true;
        });*/
    }
    catch (ex) {
      isRefreshSuccess = false;
    }
    return isRefreshSuccess;
  }

  canActivateChild(childRoute: ActivatedRouteSnapshot, state: RouterStateSnapshot):
   Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    if (localStorage.getItem('isAuthenticated') === 'true') {
      if (childRoute.data.authorization) {
        if (!this.authorizeService.routingLevelAccess(childRoute.data.authorization)) {
          this.router.navigate(['/login']);
          return false;
        } else {
          return true;
        }
      }
      return true;
    }
    this.router.navigate(['/login']);
    return false;
  }


}
