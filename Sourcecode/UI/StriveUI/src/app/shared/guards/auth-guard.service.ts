import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
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
      console.log(next.data, 'URL CHAnges');
      if (next.data.authorization) {
        if (!this.authorizeService.routingLevelAccess(next.data.authorization)) {
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
