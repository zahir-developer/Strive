import { Injectable } from '@angular/core';
import { UserDataService } from '../../util/user-data.service';
import * as _ from 'underscore';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthorizeService {

  constructor(
    private userDataService: UserDataService,
    private router: Router
  ) { }

  routingLevelAccess(routingPageId) {
    if (localStorage.getItem('isAuthenticated') === 'true') {
      if (routingPageId !== undefined) {
        const roleViews = JSON.parse(localStorage.getItem('views'));
        console.log(roleViews, 'views');
        console.log(this.userDataService.userDetails, 'module');
        if (_.findWhere(roleViews, { ModuleName: routingPageId })) {
          return true;
        } else if (_.findWhere(roleViews, { ViewName: routingPageId })) {
          return true;
        }
        else {
          return false;
        }
      }
    } else {
      this.router.navigate(['/login']);
      return false;
    }

  }
}
