import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthorizeService } from '../services/common-service/authorize.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router, private authorizeService: AuthorizeService) {
  }
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    if (localStorage.getItem('isAuthenticated') === 'true') {
      if (next.data.authorization) {
        if (!this.authorizeService.routingLevelAccess(next.data.authorization)) {
          this.router.navigate(['/unauthorized']);
          return false;
        } else {
          return true;
        }
      }
    }else {
      this.router.navigate(['/unauthorized']);
      return false;
    }
   
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
