import { Injectable } from '@angular/core';
import { UserDataService } from '../../util/user-data.service';
import * as _ from 'underscore';

@Injectable({
  providedIn: 'root'
})
export class AuthorizeService {

  constructor(
    private userDataService: UserDataService
  ) { }

  routingLevelAccess(routingPageId) {
    console.log(routingPageId, this.userDataService.isAuthenticated, this.userDataService, 'authrize');
    if (localStorage.getItem('isAuthenticated') === 'true') {
      return true;
    }
    if (routingPageId !== undefined) {
      const roleViews = JSON.parse(localStorage.getItem('views'));
      if (_.findWhere(this.userDataService.userDetails.views, { ModuleName: routingPageId })) {
        return true;
      }
     else{
      return false;
     } 
    }
  }
}
