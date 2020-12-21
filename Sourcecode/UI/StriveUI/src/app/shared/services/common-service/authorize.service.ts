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
    
    if (routingPageId !== undefined) {
      const roleViews = JSON.parse(localStorage.getItem('views'));
      if (_.findWhere(roleViews, { ModuleName: routingPageId })) {
        return true;
      }
     else{
      return false;
     } 
    }
  }
}
