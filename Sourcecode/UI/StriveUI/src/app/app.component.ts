import { Component } from '@angular/core';
import { UserDataService } from './shared/util/user-data.service';
import { AuthenticateObservableService } from './shared/observable-service/authenticate-observable.service';
// import { Idle, DEFAULT_INTERRUPTSOURCES } from '@ng-idle/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'StriveUI';
  isUserAuthenticated = false;
  constructor(private user: UserDataService, private authService: AuthenticateObservableService) {
    this.isUserAuthenticated = this.user.isAuthenticated;
    console.log(this.isUserAuthenticated);
    console.log(this.authService.getIsAuthenticate);
  }
}
